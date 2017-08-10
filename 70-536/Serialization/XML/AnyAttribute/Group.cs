using System.Xml;
using System.Xml.Serialization;

public class Group
{
	public string GroupName;
	
	[XmlAnyAttribute]
	public XmlAttribute[] UnkAttributes;
}
