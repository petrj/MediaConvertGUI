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


		public static List<string> ContainersAsList()
		{
			var res = new List<string>();

			if (Containers == null)
				return res;

			foreach (var c in Containers) 
			{
				res.Add (c.Name);
			}

			return res;
		}

		public static List<string> VideoCodecsAsList()
		{
			var res = new List<string>();

			if (VideoCodecs == null)
				return res;

			foreach (var codec in VideoCodecs) 
			{
				res.Add (codec.Name);
			}

			return res;
		}

		public static List<string> AudioCodecsAsList()
		{
			var res = new List<string>();

			if (AudioCodecs == null)
				return res;

			foreach (var codec in AudioCodecs) 
			{
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
				var node = xmlDoc.CreateElement("Codec");
				node.SetAttribute ("name", codec.Name);
				node.SetAttribute ("title", codec.Title);
				node.SetAttribute ("link", codec.Link);
				node.SetAttribute ("cmd", codec.Command);
				node.SetAttribute ("hwaccel", codec.HWAcceleration);
				videoCodecsNode.AppendChild(node);
			}

			foreach (var codec in AudioCodecs)
			{
				var node = xmlDoc.CreateElement("Codec");
				node.SetAttribute ("name", codec.Name);
				node.SetAttribute ("title", codec.Title);
				node.SetAttribute ("link", codec.Link);
				node.SetAttribute ("cmd", codec.Command);
				node.SetAttribute ("hwaccel", codec.HWAcceleration);
				audioCodecsNode.AppendChild(node);
			}

			var containersNode = xmlDoc.CreateElement("AvailableContainers");
			rootNode.AppendChild (containersNode);

			foreach (var container in Containers) 
			{
				container.SaveToXmlnode (xmlDoc, containersNode);
			}

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

			// Saving 
			Save(filename);
		}	

	}
}

