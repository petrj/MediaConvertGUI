using System;
using System.Text;
using System.Xml;

namespace MediaConvertGUI
{
	public class MediaCodec
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
		public string Command { get; set; }
		public string HWAcceleration { get; set; }
		public bool Encode { get; set; }

		public MediaCodec()
		{
			Encode = true;
		}

		public void SaveToXmlnode(EnhancedXmlDocument xmlDoc, XmlElement parentNode)
		{
			var node = xmlDoc.CreateElement("Codec");
			node.SetAttribute ("name", Name);
			node.SetAttribute ("title", Title);
			node.SetAttribute ("link", Link);
			node.SetAttribute ("cmd", Command);

			if (!string.IsNullOrEmpty(HWAcceleration))
				node.SetAttribute ("hwaccel", HWAcceleration);

			if (!Encode)
				node.SetAttribute ("encode", "false");

			parentNode.AppendChild(node);
		}

		public static MediaCodec CreateFromXmlnode(XmlElement element)
		{
			var codec = new MediaCodec ();
			if (element.HasAttribute ("name")) 
			{
				codec.Name = element.GetAttribute ("name");
			}
			if (element.HasAttribute ("title")) 
			{
				codec.Title = element.GetAttribute ("title");
			}
			if (element.HasAttribute ("link")) 
			{
				codec.Link = element.GetAttribute ("link");
			}
			if (element.HasAttribute ("cmd")) 
			{
				codec.Command = element.GetAttribute ("cmd");
			}
			if (element.HasAttribute ("hwaccel")) 
			{
				codec.HWAcceleration = element.GetAttribute ("hwaccel");
			}
			if (element.HasAttribute ("encode")) 
			{
				codec.Encode = Convert.ToBoolean(element.GetAttribute ("encode"));
			}

			return codec;
		}
	}
}

