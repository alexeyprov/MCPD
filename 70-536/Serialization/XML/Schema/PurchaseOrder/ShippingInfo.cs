namespace PurchaseOrder
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;

    public class ShippingInfo : XmlBase
    {
		private string					_Name;
		private BriefContact			_BriefContact;
		private ShippingStreetAddress	_StreetAddress;

		public ShippingInfo()
		{
			_Name = "";
			_BriefContact = new BriefContact();
			_StreetAddress = new ShippingStreetAddress();
		}

		public ShippingStreetAddress StreetAddress
		{
			get
			{
				return this._StreetAddress;
			}
			set
			{
				this._StreetAddress = value;	
			}
		}

		public class ShippingStreetAddress : AddressBase
		{

			private string _HouseColor;

			public ShippingStreetAddress()
			{
				_HouseColor = "";
			}

			public string HouseColor
			{
				get
				{
					return _HouseColor;
				}
				set
				{
					_HouseColor = value;
				}
			}

			override public void Serialize(ref XmlTextWriter writer)
			{
				try
				{
					writer.WriteStartElement("", "StreetAddress", "");
						writer.WriteElementString ("AddressCode",null,this.AddressCode);

						IEnumerator enumAddressLine = this.AddressLine.GetEnumerator();
						while(enumAddressLine.MoveNext())
						{
							writer.WriteElementString ("AddressLine",null, enumAddressLine.Current.ToString());
						}
				
						writer.WriteElementString ("City",null, this.City);
						writer.WriteElementString ("State_Province",null, this.StateProvice);
						writer.WriteElementString ("PostalCode",null, this.PostalCode);
						writer.WriteElementString ("Country",null, this.Country);
						writer.WriteElementString ("Room",null, this.Room);
						writer.WriteElementString ("Building",null, this.Building);
						/* Add writer code to write out the HouseColor element */
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
							case("AddressCode"):
								this.AddressCode = reader.ReadString();
								break;
							case("AddressLine"):
								this.AddressLine.Add(reader.ReadString());
								break;
							case("City"):
								this.City = reader.ReadString();
								break;
							case("State_Province"):
								this.StateProvice = reader.ReadString();
								break;
							case("PostalCode"):
								this.PostalCode = reader.ReadString();
								break;
							case("Country"):
								this.Country = reader.ReadString();
								break;
							case("Room"):
								this.Room = reader.ReadString();
								break;
							case("Building"):
								this.Building = reader.ReadString();
								break;
							case("HouseColor"):
								this.HouseColor = reader.ReadString();
								break;
							default:
								break;
						}
					}
					
					if(reader.LocalName == "StreetAddress" && reader.NodeType == XmlNodeType.EndTag)
					{
						break;
					}
				}
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
			try
			{
				writer.WriteStartElement("", "ShippingInformation", "");
					writer.WriteElementString("Name","", this.Name);
					this.StreetAddress.Serialize(ref writer);
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
				
				if(reader.LocalName == "ShippingInformation" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}

    }
}
