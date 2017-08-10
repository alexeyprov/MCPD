namespace PurchaseOrder
{
    using System;
	using System.Collections;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;

    public class BillingInfo : XmlBase
    {
        private string					_Name;
		private BillingStreetAddress	_StreetAddress;
		private BriefContact			_BriefContact;
		
		public BillingInfo()
		{
			_Name = "";
			_BriefContact = new BriefContact();
			_StreetAddress = new BillingStreetAddress();
		}

		public class BillingStreetAddress : AddressBase
		{
			public BillingStreetAddress()
			{
			}
		}

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}
		
		public BillingStreetAddress StreetAddress
		{
			get
			{
				return this._StreetAddress;
			}
		}

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
		
		override public void Serialize(ref XmlTextWriter writer)
		{
			writer.WriteStartElement("", "BillingInformation", "");
			writer.WriteElementString("Name","", this.Name);
			this.StreetAddress.Serialize(ref writer);
			this.BriefContact.Serialize(ref writer);
			writer.WriteEndElement();
		}

		override public void Deserialize(ref XmlTextReader reader)
		{
			while (reader.Read())
			{
				if(reader.NodeType == XmlNodeType.Element)
				{
					switch(reader.LocalName)
					{
						case("Name"):
							this.Name = reader.ReadString();
							break;
						case("StreetAddress"):
							this.StreetAddress.Deserialize(ref reader);
							break;
						case("BriefContact"):
							this.BriefContact.Deserialize(ref reader);
							break;
						default:
							break;
					}
				}
				
				if(reader.LocalName == "BillingInformation" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}
    }
}
