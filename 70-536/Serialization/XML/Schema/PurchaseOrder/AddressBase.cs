namespace PurchaseOrder
{
    using System;
	using System.Collections;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;

    public class AddressBase : XmlBase
    {
		private string		_AddressCode;
		private ArrayList	_AddressLine;
		private string		_StateProvince;
		private string		_City;
		private string		_PostalCode;
		private string		_Country;
		private string		_Building;
		private string		_Room;

		public AddressBase()
		{
			_AddressCode = "";
			_AddressLine = new ArrayList();
			_StateProvince = "";
			_City = "";
			_PostalCode = "";
			_Country = "";
			_Building = "";
			_Room = "";
		}

		[XmlElementAttribute(IsNullable=false)]
		public string AddressCode
		{
			get
			{
				return _AddressCode;
			}
			set
			{
				_AddressCode = value;
			}
		}
    
		[XmlElementAttribute(IsNullable=false)]
		public ArrayList AddressLine
		{
			get
			{
				return _AddressLine;
			}
			set
			{
				_AddressLine = value;
			}
		}
    
		[XmlElementAttribute(IsNullable=false)]
		public string City
		{
			get
			{
				return _City;
			}
			set
			{
				_City = value;
			}
		}
    
		[XmlElementAttribute(IsNullable=false)]
		public string StateProvice
		{
			get
			{
				return _StateProvince;
			}
			set
			{
				_StateProvince = value;
			}
		}
    
		[XmlElementAttribute(IsNullable=false)]
		public string PostalCode
		{
			get
			{
				return _PostalCode;
			}
			set
			{
				_PostalCode = value;
			}
		}
    
		[XmlElementAttribute(IsNullable=false)]
		public string Country
		{
			get
			{
				return _Country;
			}
			set
			{
				_Country = value;
			}
		}
    
		[XmlElementAttribute(IsNullable=false)]
		public string Building
		{
			get
			{
				return _Building;
			}
			set
			{
				_Building = value;
			}
		}	
    
		[XmlElementAttribute(IsNullable=true)]
		public string Room
		{
			get
			{
				return _Room;
			}
			set
			{
				_Room = value;
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
}
