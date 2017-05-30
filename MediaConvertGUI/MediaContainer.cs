using System.Collections.Generic;
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
		public List<string> ExtensionList { get; set; }
		public string Link { get; set; }
		public bool Default  { get; set; }
		public bool Encode { get; set; }

		public MediaContainer()
		{
			ExtensionList = new List<string> ();
			Default = false;
			Encode = true;
		}

		public void SaveToXmlnode(EnhancedXmlDocument xmlDoc, XmlElement parentNode)
		{
			var node = xmlDoc.CreateElement("Container");
			node.SetAttribute ("name", Name);
			node.SetAttribute ("title", Title);
			node.SetAttribute ("ext", Extension);

			var exts = String.Join (",", ExtensionList);
			node.SetAttribute ("extList", exts);

			if (Default)			
				node.SetAttribute ("default", "true");

			if (!Encode)
				node.SetAttribute ("encode", "false");

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
			if (element.HasAttribute ("default")) 
			{
				container.Default = Convert.ToBoolean(element.GetAttribute ("default"));
			}
			if (element.HasAttribute ("extList")) 
			{
				foreach (var ext in element.GetAttribute ("extList").Split(',')) 
				{
					container.ExtensionList.Add (ext);
				}
			}
			if (element.HasAttribute ("encode")) 
			{
				container.Encode = Convert.ToBoolean(element.GetAttribute ("encode"));
			}

			return container;
		}
	}
}

