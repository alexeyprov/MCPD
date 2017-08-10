using System.Runtime.Serialization;

class CarSurrogate : ISerializationSurrogate
{
	private const string MAKE_PROP = "MAKE";
	private const string MODEL_PROP = "MODEL";

	void ISerializationSurrogate.GetObjectData(object obj, SerializationInfo si, StreamingContext ctx)
	{
		Car c = (Car) obj;
	
		si.AddValue(MAKE_PROP, c.Make);
		si.AddValue(MODEL_PROP, c.Model);
	}

	object ISerializationSurrogate.SetObjectData(object obj, SerializationInfo si, StreamingContext ctx, ISurrogateSelector sel)
	{
		Car c = (Car) obj;
	
		c.Make = si.GetString(MAKE_PROP);
		c.Model = si.GetString(MODEL_PROP);

		return null; // formatters ignore this
	}
}