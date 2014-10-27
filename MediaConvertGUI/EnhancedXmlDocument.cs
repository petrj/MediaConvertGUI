using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;

namespace MediaConvertGUI
{
	public class EnhancedXmlDocument : XmlDocument
	{
		public void CreateTextSingleValueElement(XmlNode parent,string name, string value)
		{
			var node = CreateElement(name);
			node.InnerText = value;
			parent.AppendChild(node);
		}

		public string GetSingleNodeValue(string xpath,string defaultValue)
		{
			var node = SelectSingleNode(xpath);
			if (node != null)
			{
				return node.InnerText;
			} else 
				return defaultValue;
		}
	}
}

