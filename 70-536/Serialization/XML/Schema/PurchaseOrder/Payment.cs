namespace PurchaseOrder
{
	using System;
	using System.IO;
	using System.Xml;
	
	public class PaymentInformation : XmlBase
	{
		
		private string _CardType;
		private string _CardExpires;
		private string _CardName;
		private string _CardNumber;
		private string _CardIssueCode;
		
		public PaymentInformation()
		{
			_CardType = "";
			_CardExpires = "";
			_CardName = "";
			_CardNumber = "";
			_CardIssueCode = "";
		}

		public string CardType
		{
			get
			{		
				return _CardType;
			}
			set
			{
				_CardType = value;
			}
		}

		public string CardExpires
		{
			get
			{
				return _CardExpires;
			}
			set
			{
				_CardExpires = value;
			}
		}
		
		public string CardName
		{
			get
			{
				return _CardName;
			}
			set
			{
				_CardName = value;
			}
		}

		public string CardNumber
		{
			get
			{
				return _CardNumber;
			}
			set
			{
				_CardNumber = value;
			}
		}

		public string CardIssueCode
		{
			get
			{
				return _CardIssueCode;
			}
			set
			{
				_CardIssueCode = value;
			}
		}


		override public void Serialize(ref XmlTextWriter writer)
		{
			try
			{
				writer.WriteStartElement("", "PaymentInformation", "");
				writer.WriteElementString ("CardExpires",null, this.CardExpires);
				writer.WriteElementString ("CardIssueCode",null, this.CardIssueCode);
				writer.WriteElementString ("CardName",null, this.CardName);
				writer.WriteElementString ("CardNumber",null, this.CardNumber);
				writer.WriteElementString ("CardType",null, this.CardType);
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
						case("CardExpires"):
							_CardExpires = reader.ReadString();
							break;
						case("CardIssueCode"):
							_CardIssueCode = reader.ReadString();
							break;
						case("CardName"):
							_CardName = reader.ReadString();
							break;
						case("CardNumber"):
							_CardNumber = reader.ReadString();
							break;
						case("CardType"):
							_CardType = reader.ReadString();
							break;
						default:
							break;
					}
				}
				
				if(reader.LocalName == "PaymentInformation" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}
	}
}
