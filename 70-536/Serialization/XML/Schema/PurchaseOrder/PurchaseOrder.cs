namespace PurchaseOrder
{
	using System;
	using System.Xml.Serialization;
	using System.Collections;
	using System.IO;
	using System.Xml;
	
	public class PurchaseOrder
	{
		private BuyerInfo			_BuyerInformation;
		private ShippingInfo		_ShipToAddress;
		private BillingInfo			_BillToAddress;
		private	PaymentInformation	_PaymentInformation;
		private LineItems			_LineItems;
		private string				_PurchaseOrderId;
		private string				_CorrelationID;
		private string				_OriginatorID;
		private string				_PurchaseOrderDate;
		private string				_Comment;
		private string				_ShipTerms;
		private	decimal				_ShippingCost = 0.0m;
		private decimal				_TaxesAndFees = 0.0m;
		private decimal				_Total = 0.0m;
				
		public PurchaseOrder()
		{
			_BuyerInformation = new BuyerInfo();
			_PaymentInformation = new PaymentInformation();
			_BillToAddress = new BillingInfo();
			_ShipToAddress = new ShippingInfo();
			_LineItems = new LineItems();			
		}

		public PurchaseOrder(string POXml)
		{
			_BuyerInformation = new BuyerInfo();
			_PaymentInformation = new PaymentInformation();
			_BillToAddress = new BillingInfo();
			_ShipToAddress = new ShippingInfo();
			_LineItems = new LineItems();	

			try
			{
				this.Validate(POXml);

				StringReader strReader = new StringReader(POXml);
				XmlTextReader reader = new XmlTextReader(strReader);

				while (reader.Read())
				{
					if(reader.NodeType == XmlNodeType.Element)
					{
						switch(reader.LocalName)
						{
							case "PurchaseOrder":
								this.CorrelationID = reader.GetAttribute("CorrelationID").ToString();
								this.OriginatorID = reader.GetAttribute("OriginatorID").ToString();
								break;
							case "Comment":
								this._Comment = reader.ReadString();
								break;
							case "PurchaseOrderID":
								this.PurchaseOrderId = reader.ReadString();
								break;
							case "PurchaseOrderDate":
								this.PurchaseOrderDate = reader.ReadString();
								break;
							case "ShipTerms":
								this._ShipTerms = reader.ReadString();
								break;
							/* these are derived members
							case "ShippingCost":
								this.ShippingCost = reader.ReadCurrency();
								break;
							case "SubTotal":
								this.SubTotal = reader.ReadCurrency();
								break;
							case "TaxesAndFees":
								this.TaxesAndFees = reader.ReadCurrency();
								break;
							case "Total":
								this.Total = reader.ReadCurrency();
								break;
							*/
							case "PaymentInformation":
								this.PaymentInformation.Deserialize(ref reader);
								break;
							case "BuyerInformation":
								this.BuyerInformation.Deserialize(ref reader);
								break;
							case "ShippingInformation":
								this.ShipToAddress.Deserialize(ref reader);
								break;
							case "BillingInformation":
								this.BillToAddress.Deserialize(ref reader);
								break;
							case "OrderLineItems":
								this.LineItems.Deserialize(ref reader);
								break;
							default:
								break;
						}
					}
				}
			}

			finally
			{
			}
		}


		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public BuyerInfo BuyerInformation
		{
			get
			{
				return _BuyerInformation;
			}
			set
			{
				_BuyerInformation = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public LineItems LineItems
		{
			get
			{
				return _LineItems;
			}
			set
			{
				_LineItems = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public BillingInfo BillToAddress
		{
			get
			{
				return _BillToAddress;
			}
			set
			{
				_BillToAddress = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public ShippingInfo ShipToAddress
		{
			get
			{
				return _ShipToAddress;
			}
			set
			{
				_ShipToAddress = value;
			}
		}


		[System.Xml.Serialization.XmlAttribute()]
		public string CorrelationID
		{
			get
			{
				return _CorrelationID;
			}
			set
			{
				_CorrelationID = value;
			}
		}


		[System.Xml.Serialization.XmlAttribute()]
		public string OriginatorID
		{
			get
			{
				return _OriginatorID;
			}
			set
			{
				_OriginatorID = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public string PurchaseOrderId
		{
			get
			{
				return _PurchaseOrderId;
			}
			set
			{
				_PurchaseOrderId = value;
			}
		}

		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public string PurchaseOrderDate
		{
			get
			{
				return _PurchaseOrderDate;
			}
			set
			{
				_PurchaseOrderDate = value;
			}
		}

		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				_Comment = value;
			}
		}
		
		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public string ShipTerms
		{
			get
			{
				return _ShipTerms;
			}
			set
			{
				_ShipTerms = value;
			}
		}
		
		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public decimal SubTotal
		{
			get
			{
				decimal _sub = 0.0m;
				
				foreach(LineItem item in LineItems)
				{
					
					_sub += item.ExtendedPrice;
				}

				return _sub;
			}
		}
		
		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public decimal ShippingCost
		{
			get
			{
				return _ShippingCost;
			}
			set
			{
				_ShippingCost = value;
			}
		}
		
		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public decimal TaxesAndFees
		{
			get
			{
				return _TaxesAndFees;
			}
			set
			{
				_TaxesAndFees = value;
			}
		}
		
		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public decimal Total
		{
			get
			{
				return _Total;
			}
		}
		
		[System.Xml.Serialization.XmlElement(IsNullable=false)]
		public PaymentInformation PaymentInformation
		{
			get
			{
				return _PaymentInformation;
			}
			set
			{
				_PaymentInformation = value;
			}
		}

		public string Serialize()
		{
			StringWriter w = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(w);

			writer.Formatting =Formatting.Indented;
			writer.Indentation= 5;

			writer.WriteStartDocument();
				writer.WriteStartElement("", "PurchaseOrder", "");
					writer.WriteAttribute("","CorrelationID","", this.CorrelationID);
					writer.WriteAttribute("","OriginatorID","", this.OriginatorID);
					writer.WriteElementString("Comment","", this.Comment);
					writer.WriteElementString("PurchaseOrderID","", this.PurchaseOrderId);
					writer.WriteElementString("PurchaseOrderDate","", this.PurchaseOrderDate);
					this.BuyerInformation.Serialize(ref writer);
					this.BillToAddress.Serialize(ref writer);
					this.ShipToAddress.Serialize(ref writer);
					this.LineItems.Serialize(ref writer);
					writer.WriteElementString("ShipTerms","", this.ShipTerms);
					writer.WriteElementString("ShippingCost","", this.ShippingCost.ToString());
					writer.WriteElementString("SubTotal","", this.SubTotal.ToString());
					writer.WriteElementString("TaxesAndFees","", this.TaxesAndFees.ToString());
					writer.WriteElementString("Total","", this.Total.ToString());
					this.PaymentInformation.Serialize(ref writer);
				writer.WriteEndElement();
			writer.WriteEndDocument();

			writer.Flush();
			
			this.Validate(w.ToString());

			return (w.ToString());
		}
		
		private bool Validate(string POXml)
		{		
			try
			{
				POValidator.IMain XSDValidator = new POValidator.IMain();

				XSDValidator.Validate(POXml);
				return true;
			}
			finally
			{
			}
		}

	}
}