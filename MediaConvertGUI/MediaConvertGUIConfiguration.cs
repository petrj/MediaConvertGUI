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
		public static string MediaInfoPath;
		public static string FFMpegPath;

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

			MediaInfoPath = xmlDoc.GetSingleNodeValue("//MediaConvertGUIConfiguration/MediaInfoPath","mediainfo");
			FFMpegPath = xmlDoc.GetSingleNodeValue("//MediaConvertGUIConfiguration/FFMpegPath","ffmpeg");

			// Saving 
			Save(filename);
		}	

	}
}

