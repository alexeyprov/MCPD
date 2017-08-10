namespace PurchaseOrder
{
	using System;
	using System.Collections;
	using System.Xml.Serialization;
	
	public class Item
	{
		string	_Name;
		string	_Data;
		
		public Item(object obj)
		{
			Item refItem;
			refItem = (Item)obj;

			_Name = refItem.Name;
			_Data = refItem.Data;
		}

		public Item()
		{
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
		
		[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
		public string Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
			}
		}
	}

	[System.Xml.Serialization.XmlInclude(typeof(Item))]
	public class List: IEnumerable
	{
		ArrayList _list;
		
		public List()
		{
			_list = new ArrayList();
		}

		

		public bool Add(object foo)
		{
			//Item number = new Item();

			//number.Data = Number;
			//number.Name = System.Convert.ToString((System.Convert.ToInt32(_list.Count) + 1));
			
			_list.Add(foo);

			return (true);
		}



		public long Count()
		{
			return _list.Count;
		}

		
		public IEnumerator GetEnumerator()
		{
			return((IEnumerator) new ListEnumerator(this));
		}

		[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
		public class ListEnumerator: IEnumerator
		{
			int index;
			List _List;

			public ListEnumerator(List list)
			{
				this._List = list;
				index = -1;
			}

			public bool MoveNext()
			{
				index++;
				if (index >= _List._list.Count)
					return(false);
				else
					return(true);
			}

			public void Reset()
			{
				index = -1;
			}

			[System.Xml.Serialization.XmlElementAttribute(IsNullable=false)]
			public Item Current
			{
				get
				{
					return (Item)((_List._list[index]));
				}
			}

			
			 object IEnumerator.Current
			{
				get
				{
					return (Current);
				}
			}
		}
	}

}
