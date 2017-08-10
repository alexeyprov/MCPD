namespace PurchaseOrder
{
    using System;
	using System.Xml;
 
    public class LineItem : XmlBase
    {
        
		private int		_LineNumber;
		private string	_ProductId;
		private decimal	_QuantityOrdered = 0.0m;
		private string	_Description;
		private	decimal	_ExtendedPrice = 0.0m;
		private	int		_QuantityAvailable = 0;
		private int		_QuantityBackOrdered = 0;
		
		public LineItem()
        {
			_LineNumber = 0;
			_ProductId = "";
			_QuantityOrdered = 0.0m;
			_Description = "";
			_ExtendedPrice = 0.0m;
			_QuantityAvailable = 0;
			_QuantityBackOrdered = 0;
        }

		public int LineNumber
		{
			get
			{
				return _LineNumber;
			}
		}
		
		internal void SetLineNumber(int num)
		{
			_LineNumber = num;
		}

		public string ProductId
		 {
			 get
			 {
				 return _ProductId;
			 }
			 set
			 {
				 _ProductId = value;
			 }
		 }

		 public decimal QuantityOrdered
		 {
			 get
			 {
				 return _QuantityOrdered;
			 }
			 set
			 {
				 _QuantityOrdered = value;
			 }
		 }
		 
		 public string Description
		 {
			 get
			 {
				 return _Description;
			 }
			 set
			 {
				 _Description = value;
			 }
		 }

		 public decimal ExtendedPrice
		 {
			 get
			 {
				 return _ExtendedPrice;
			 }
			 set
			 {
				 _ExtendedPrice = value;
			 }
		 }

		 public int QuantityAvailable
		 {
			 get
			 {
				 return _QuantityAvailable;
			 }
			 set
			 {
				 _QuantityAvailable = value;
			 }
		 }

		 public int QuantityBackOrdered
		 {
			 get
			 {
				 return _QuantityBackOrdered;
			 }
			 set
			 {
				 _QuantityBackOrdered = value;
			 }
		 }

		override public void Serialize(ref XmlTextWriter writer)
		{
			try
			{
				writer.WriteStartElement("","OrderLineItem","");
				writer.WriteElementString ("LineNumber",null, this.LineNumber.ToString());
				writer.WriteElementString ("ProductID",null, this.ProductId.ToString());
				writer.WriteElementString ("Description",null, this.Description.ToString());
				writer.WriteElementString ("QuantityOrdered",null, this.QuantityBackOrdered.ToString());
				writer.WriteElementString ("ExtendedPrice",null, this.ExtendedPrice.ToString());
				writer.WriteElementString ("QuantityAvailable",null, this.QuantityAvailable.ToString());
				writer.WriteElementString ("QuantityBackOrdered",null, this.QuantityBackOrdered.ToString());
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
						case("LineNumber"):
							_LineNumber = reader.ReadInt32();
							break;
						case("ProductID"):
							_ProductId = reader.ReadString();
							break;
						case("Description"):
							_Description = reader.ReadString();
							break;
						case("QuantityOrdered"):
							_QuantityOrdered = reader.ReadDecimal();
							break;
						case("ExtendedPrice"):
							_ExtendedPrice = reader.ReadDecimal();
							break;
						case("QuantityAvailable"):
							_QuantityAvailable = reader.ReadInt32();
							break;
						case("QuantityBackOrdered"):
							_QuantityBackOrdered = reader.ReadInt32();
							break;
						default:
							break;
					}
				}
				
				if(reader.LocalName == "OrderLineItem" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}
    }
}
