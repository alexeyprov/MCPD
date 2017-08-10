using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloWcf
{
	// NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in App.config.
	[ServiceContract]
	public interface IGetCostService
	{
		[OperationContract]
		string GetTotalCost(ProductData composite);
	}

	// Use a data contract as illustrated in the sample below to add composite types to service operations
	[DataContract]
	public class ProductData
	{
		int _itemCount;
		string _name;
		float _itemPrice;

		[DataMember]
		public int ItemCount
		{
			get
			{
				return _itemCount;
			}
			set
			{
				_itemCount = value;
			}
		}

		[DataMember]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		[DataMember]
		public float ItemPrice
		{
			get
			{
				return _itemPrice;
			}
			set
			{
				_itemPrice = value;
			}
		}
	}
}
