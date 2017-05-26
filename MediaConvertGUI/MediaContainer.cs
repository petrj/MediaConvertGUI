using System;
using System.Text;
using System.Xml;


namespace MediaConvertGUI
{
	public class MediaContainer
	{		
		public string Name { get; set; }
		public string Title { get; set; }
		public string Extension { get; set; }
		public string Link { get; set; }

		public void SaveToXmlnode(EnhancedXmlDocument xmlDoc, XmlElement parentNode)
		{
			var node = xmlDoc.CreateElement("Container");
			node.SetAttribute ("name", Name);
			node.SetAttribute ("title", Title);
			node.SetAttribute ("ext", Extension);
			node.SetAttribute ("link", Link);

			parentNode.AppendChild(node);
		}

		public static MediaContainer CreateFromXmlnode(XmlElement element)
		{
			var container = new MediaContainer ();
			if (element.HasAttribute ("name")) 
			{
				container.Name = element.GetAttribute ("name");
			}
			if (element.HasAttribute ("title")) 
			{
				container.Title = element.GetAttribute ("title");
			}
			if (element.HasAttribute ("link")) 
			{
				container.Link = element.GetAttribute ("link");
			}
			if (element.HasAttribute ("ext")) 
			{
				container.Extension = element.GetAttribute ("ext");
			}

			return container;
		}
	}
}

