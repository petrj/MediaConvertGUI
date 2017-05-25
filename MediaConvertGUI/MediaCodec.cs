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

			return codec;
		}
	}
}

