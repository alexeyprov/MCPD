namespace PurchaseOrder
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;

	public class BriefContact : XmlBase
	{
		private string		_Name;
		private string		_Email;
		private ArrayList	_Phone;
		
		public BriefContact()
		{
			_Name = "";
			_Email ="";
			_Phone = new ArrayList();
		}

		[XmlElementAttribute(IsNullable=false)]
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

		[XmlElementAttribute(IsNullable=false)]
		public string Email
		{
			get
			{
				return _Email;
			}
			set
			{
				_Email = value;
			}
		}

		[XmlElementAttribute(IsNullable=false)]
		public ArrayList Phone
		{
			get
			{
				return _Phone;
			}
			set
			{
				_Phone = value;
			}
		}


		override public void Serialize(ref XmlTextWriter writer)
		{
			try
			{
				writer.WriteStartElement("", "BriefContact", "");
				
				writer.WriteElementString ("Name",null, _Name);
				writer.WriteElementString ("Email",null, _Email);
				
				System.Collections.IEnumerator enumPhone = _Phone.GetEnumerator();
				while(enumPhone.MoveNext())
				{
					writer.WriteElementString ("Phone",null, enumPhone.Current.ToString());
				}
				
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
							_Name = reader.ReadString();
							break;
						case("Email"):
							_Email = reader.ReadString();
							break;
						case("Phone"):
							_Phone.Add(reader.ReadString());
							break;
						default:
							break;
					}
				}
				
				if(reader.LocalName == "BriefContact" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}
	}
}

