namespace PurchaseOrder
{
    using System;
	using System.Xml;

    abstract public class XmlBase
    {
        public XmlBase()
        {
        }

		abstract public void	Serialize(ref XmlTextWriter writer);

		abstract public void	Deserialize(ref XmlTextReader reader);
    }
}
