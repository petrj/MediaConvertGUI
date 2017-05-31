using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;


namespace MediaConvertGUI
{
	public static class MediaConvertGUIConfiguration
	{
		public static List<string> OpenWithApplications{ get; set; }
		public static List<MediaCodec> VideoCodecs { get; set; }
		public static List<MediaCodec> AudioCodecs { get; set; }
		public static List<MediaContainer> Containers { get; set; }
		public static string MediaInfoPath;
		public static string FFMpegPath;

		public static Dictionary<decimal,string> DefaultVideoBitRates { get; set; }
		public static Dictionary<decimal,string> DefaultSamplingRates { get; set; }
		public static Dictionary<decimal,string> DefaultAudioBitrates { get; set; }

		public static MediaContainer DefaultContainer
		{
			get 
			{				
				if (Containers == null)
					return null;

				foreach (var c in Containers) 
				{
					if (c.Default)
						return c;
				}

				return GetContainerByName ("none");
			}
		}

		public static List<string> ContainersAsList(bool enabledForEncodeOnly = false)
		{
			var res = new List<string>();

			if (Containers == null)
				return res;

			foreach (var c in Containers) 
			{
				if ((!enabledForEncodeOnly)  || (c.Encode))
				res.Add (c.Name);
			}

			return res;
		}

		public static List<string> VideoCodecsAsList(bool enabledForEncodeOnly = false)
		{
			var res = new List<string>();

			if (VideoCodecs == null)
				return res;

			foreach (var codec in VideoCodecs) 
			{
				if ((!enabledForEncodeOnly)  || (codec.Encode))
					res.Add (codec.Name);
			}

			return res;
		}

		public static List<string> AudioCodecsAsList(bool enabledForEncodeOnly = false)
		{
			var res = new List<string>();

			if (AudioCodecs == null)
				return res;

			foreach (var codec in AudioCodecs) 
			{
				if ((!enabledForEncodeOnly)  || (codec.Encode))
					res.Add (codec.Name);
			}

			return res;
		}

		public static MediaCodec GetVideoCodecByName(string name)
		{
			if (VideoCodecs == null)
				return null;

			foreach (var codec in VideoCodecs) 
			{
				if (codec.Name == name)
					return codec;
			}

			return null;
		}

		public static MediaCodec GetAudioCodecByName(string name)
		{
			if (AudioCodecs == null)
				return null;

			foreach (var codec in AudioCodecs) 
			{
				if (codec.Name == name)
					return codec;
			}

			return null;
		}

		public static MediaContainer GetContainerByName(string name)
		{
			if (Containers == null)
				return null;

			foreach (var c in Containers) 
			{
				if (c.Name == name)
					return c;
			}

			return null;
		}

		public static MediaContainer GetContainerByExt(string ext)
		{
			if (Containers == null)
				return null;

			foreach (var c in Containers) 
			{
				if (c.Extension == ext)
					return c;
			}

			return null;
		}

		private static void SaveDictionaryToXmlNode(Dictionary<decimal, string> dict,EnhancedXmlDocument xmlDoc, XmlElement parentNode, string mainNodeName , string nodeName)
		{
			var mainNode = xmlDoc.CreateElement(mainNodeName);
			parentNode.AppendChild(mainNode);

			foreach (var keyAndValue in dict)
			{
				var node = xmlDoc.CreateElement(nodeName);
				node.SetAttribute ("value", keyAndValue.Key.ToString());
				node.SetAttribute ("title", keyAndValue.Value);
				mainNode.AppendChild(node);
			}
		}

		private static void LoadDictionaryFromXmlNode(Dictionary<decimal, string> dict,EnhancedXmlDocument xmlDoc, string xpath)
		{
			var nodes = xmlDoc.SelectNodes(xpath);
			foreach (XmlElement nodeEl in nodes) 
			{
				decimal val = 0;
				string title = "";

				if (nodeEl.HasAttribute ("value")) 				
					val = Convert.ToDecimal (nodeEl.GetAttribute ("value"));

				if (nodeEl.HasAttribute ("title")) 				
					title= nodeEl.GetAttribute("title");

				if (title == "")
					title = val.ToString ("N0");

				dict.Add (val, title);
			}
		}

		public static void Save(string filename)
		{
			var xmlDoc = new EnhancedXmlDocument();
			var rootNode = xmlDoc.CreateElement("MediaConvertGUIConfiguration");
			xmlDoc.AppendChild(rootNode);

			xmlDoc.CreateTextSingleValueElement(rootNode,"MediaInfoPath",MediaInfoPath);
			xmlDoc.CreateTextSingleValueElement(rootNode,"FFMpegPath",FFMpegPath);

			var appNode = xmlDoc.CreateElement("Applications");
			rootNode.AppendChild(appNode);

			var appOpenWithNode = xmlDoc.CreateElement("OpenWith");
			appNode.AppendChild(appOpenWithNode);

			var appOpenWithCommentNode = xmlDoc.CreateComment(" <Application>vlc</Application> ");
			appOpenWithNode.AppendChild(appOpenWithCommentNode);

			foreach (var app in OpenWithApplications)
			{
				var node = xmlDoc.CreateElement("Application");
				node.InnerText = app;
				appOpenWithNode.AppendChild(node);
			}

			var codecsNode = xmlDoc.CreateElement("AvailableCodecs");
			var videoCodecsNode = xmlDoc.CreateElement("Video");
			var audioCodecsNode = xmlDoc.CreateElement("Audio");

			rootNode.AppendChild (codecsNode);
			codecsNode.AppendChild (videoCodecsNode);
			codecsNode.AppendChild (audioCodecsNode);

			foreach (var codec in VideoCodecs)
			{
				codec.SaveToXmlnode (xmlDoc, videoCodecsNode);
			}

			foreach (var codec in AudioCodecs)
			{
				codec.SaveToXmlnode (xmlDoc, audioCodecsNode);
			}

			var containersNode = xmlDoc.CreateElement("AvailableContainers");
			rootNode.AppendChild (containersNode);

			foreach (var container in Containers) 
			{
				container.SaveToXmlnode (xmlDoc, containersNode);
			}

			SaveDictionaryToXmlNode (DefaultVideoBitRates, xmlDoc, rootNode, "DefaultVideoBitrates", "Bitrate");
			SaveDictionaryToXmlNode (DefaultSamplingRates, xmlDoc, rootNode, "DefaultSamplingRates", "Rate");
			SaveDictionaryToXmlNode (DefaultAudioBitrates, xmlDoc, rootNode, "DefaultAudioBitrates", "Bitrate");

			xmlDoc.Save(filename);
		}

		/// <summary>
		/// Loads the configuration.
		/// </summary>
		/// <param name='path'>
		/// Path to xml config file
		/// </param>
		public static void Load(string filename)
		{	
			OpenWithApplications = new List<string>();
			VideoCodecs = new List<MediaCodec> ();
			AudioCodecs = new List<MediaCodec> ();
			Containers =  new List<MediaContainer> ();

			DefaultVideoBitRates = new Dictionary<decimal, string> ();
			DefaultSamplingRates = new Dictionary<decimal, string> ();
			DefaultAudioBitrates = new Dictionary<decimal, string> ();

			if (!Path.IsPathRooted(filename))
			{
				// adding app path
				filename = Path.Combine(SupportMethods.AppPath+Path.DirectorySeparatorChar,filename);
			}

			if (!File.Exists(filename))
			{
				// creating default configuration
				Save(filename);
			}

			var xmlDoc = new EnhancedXmlDocument();
			xmlDoc.Load(filename);

			var appOpenWithNodes = xmlDoc.SelectNodes("//MediaConvertGUIConfiguration/Applications/OpenWith/Application");
			foreach(XmlNode appNode in appOpenWithNodes)
			{
				OpenWithApplications.Add( appNode.InnerText);
			}

			var codecNodes = xmlDoc.SelectNodes("//MediaConvertGUIConfiguration/AvailableCodecs/Video/Codec");
			foreach(XmlElement codecNode in codecNodes)
			{			
				VideoCodecs.Add( MediaCodec.CreateFromXmlnode(codecNode));
			}

			var audioCodecNodes = xmlDoc.SelectNodes("//MediaConvertGUIConfiguration/AvailableCodecs/Audio/Codec");
			foreach(XmlElement codecNode in audioCodecNodes)
			{			
				AudioCodecs.Add( MediaCodec.CreateFromXmlnode(codecNode));
			}

			var contNodes = xmlDoc.SelectNodes("//MediaConvertGUIConfiguration/AvailableContainers/Container");
			foreach(XmlElement cont in contNodes)
			{			
				Containers.Add( MediaContainer.CreateFromXmlnode(cont));
			}

			MediaInfoPath = xmlDoc.GetSingleNodeValue("//MediaConvertGUIConfiguration/MediaInfoPath","mediainfo");
			FFMpegPath = xmlDoc.GetSingleNodeValue("//MediaConvertGUIConfiguration/FFMpegPath","ffmpeg");


			LoadDictionaryFromXmlNode (DefaultVideoBitRates, xmlDoc, "//MediaConvertGUIConfiguration/DefaultVideoBitrates/Bitrate");
			LoadDictionaryFromXmlNode (DefaultSamplingRates, xmlDoc, "//MediaConvertGUIConfiguration/DefaultSamplingRates/Rate");
			LoadDictionaryFromXmlNode (DefaultAudioBitrates, xmlDoc, "//MediaConvertGUIConfiguration/DefaultAudioBitrates/Bitrate");

			// Saving 
			Save(filename);
		}	

	}
}

