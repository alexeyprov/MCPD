using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

[Serializable]
public class OrderItem :
	ISerializable,
	IEquatable<OrderItem>
{
	// Construction/Destruction
	public OrderItem(string product, decimal price, uint count, double vat)
	{
		_product = product;
		_price = price;
		_count = count;
		RecalcTotal(vat);
	}

	protected OrderItem(SerializationInfo info, StreamingContext ctx)
	{
		// Check source before deserializing
		if (!CheckContext(ctx))
		{
			throw new SerializationException("Bad context state during deserialization");
		}

		_product = info.GetString(PROD_FIELD);
		_price = info.GetDecimal(PRICE_FIELD);
		_count = info.GetUInt32(COUNT_FIELD);
		RecalcTotal(info.GetDouble(VALUE_ADDED_TAX_FIELD));	
	}

	// Overrides
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext ctx)
	{
		// Check source before serializing
		if (!CheckContext(ctx))
		{
			throw new SerializationException("Bad context state during serialization");
		}

		info.AddValue(PROD_FIELD, _product);
		info.AddValue(PRICE_FIELD, _price);
		info.AddValue(COUNT_FIELD, _count);
		info.AddValue(VALUE_ADDED_TAX_FIELD, ((IDictionary) ctx.Context)[VALUE_ADDED_TAX_FIELD]);
	}

	public virtual bool Equals(OrderItem oi)
	{
		if (null == oi)
		{
			return false;
		}

		return (_product == oi._product &&
			_price == oi._price &&
			_count == oi._count &&
			_total == oi._total);
	}

	public override string ToString()
	{
		return String.Format("{0} items of \"{1}\" ({2:c} per item): {3:c}",
			_count,
			_product,
			_price,
			_total);
	}
	// Implementation
	protected void RecalcTotal(double vat)
	{
		if (vat <= 0)
		{
			vat = 1;
		}
		_total = _price * _count * ((decimal) vat);
	}

	protected bool CheckContext(StreamingContext ctx)
	{
		return 0 == (ctx.State & (StreamingContextStates.Remoting |
			 					  StreamingContextStates.CrossMachine |
			 					  StreamingContextStates.Clone));
	}

	// Constants
	private const string PROD_FIELD = "PRODUCT";
	private const string COUNT_FIELD = "COUNT";
	private const string PRICE_FIELD = "PRICE";
	public const string VALUE_ADDED_TAX_FIELD = "VAT";

	// Data Members
	private string _product;
	private decimal _price;
	private uint _count;
	private decimal _total;
}