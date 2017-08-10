namespace PurchaseOrder
{
	using System;
	using System.Xml;

	public class BuyerInfo : XmlBase
	{
		private BriefContact	_BriefContact;
		private string			_CustomerId;
		
		public BuyerInfo()
		{
			_CustomerId = "";
			_BriefContact = new BriefContact();
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public BriefContact BriefContact
		{
			get
			{
				return _BriefContact;
			}
			set
			{
				_BriefContact = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public string CustomerId
		{
			get
			{
				return _CustomerId;
			}
			set
			{
				_CustomerId = value;
			}
		}

		override public void Serialize(ref XmlTextWriter writer)
		{
			try
			{
				writer.WriteStartElement("", "BuyerInformation", "");
				writer.WriteElementString("CustomerID","", this.CustomerId);
				this.BriefContact.Serialize(ref writer);
				writer.WriteEndElement();
			}
			
			finally
			{	
			}

		}

		override public void Deserialize(ref XmlTextReader reader)
		{
			while (reader.Read())
			{
				if(reader.NodeType == XmlNodeType.Element)
				{
					switch(reader.LocalName)
					{
						case("CustomerID"):
							_CustomerId = reader.ReadString();
							break;
						case("BriefContact"):
							this.BriefContact.Deserialize(ref reader);
							break;
						default:
							break;
					}
				}
				
				if(reader.LocalName == "BuyerInformation" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}
	}
}
