namespace PurchaseOrder
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Xml;

	public class LineItems : XmlBase, IEnumerable
	{
		ArrayList _LineItemsList;
	
		public LineItems()
		{
			_LineItemsList = new ArrayList();
		}
		
		public IEnumerator GetEnumerator()
		{
			return((IEnumerator) new LineItemsEnumerator(this));
		}

		public int Count()
		{
			return(_LineItemsList.Count);
		}

		public bool Add(LineItem item)
		{
			item.SetLineNumber(_LineItemsList.Count + 1);
			_LineItemsList.Add(item);

			return true;
		}

		override public void Serialize(ref XmlTextWriter writer)
		{
			try
			{
				writer.WriteStartElement("", "OrderLineItems", "");

				foreach(LineItem lineItem in this)
				{
					lineItem.Serialize(ref writer);
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
						case "OrderLineItem":
							LineItem item = new LineItem();
							item.Deserialize(ref reader);
							this.Add(item);
							break;
						default:
							break;
					}
				}
				
				if(reader.LocalName == "OrderLineItems" && reader.NodeType == XmlNodeType.EndTag)
				{
					break;
				}
			}
		}

		class LineItemsEnumerator : IEnumerator
		{
			int			_index;
			LineItems	_LineItems;
			
			public LineItemsEnumerator(LineItems item)
			{
				this._LineItems = item;
				this._index = -1;
			}
			
			public bool MoveNext()
			{
				_index++;

				if  (_index >= _LineItems._LineItemsList.Count)
				{
					return(false);
				}
				else
				{
					return (true);
				}
			}

			public object Current
			{
				get
				{	
					return _LineItems._LineItemsList[_index];
				}
			}

			public void Reset()
			{
				_index = -1;
			}
		}
	}
}
