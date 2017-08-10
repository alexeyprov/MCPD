using System;
using System.Text;
using System.Xml.Serialization;

public enum Option
{
	[XmlEnum("ABS")]
	AntiBlockingSystem,
	ClimateControl,
	XenonLights
}

public enum PrimaryTrait
{
	Weight,
	Price,
	Model
}

public class Car
{
	[XmlChoiceIdentifier("_definitionMetadata")]
	[XmlElement("Weight", Type=typeof(double))]
	[XmlElement("Price", Type=typeof(decimal))]
	[XmlElement("Model", Type=typeof(string))]
	public object Definition;

	[XmlIgnore]
	public PrimaryTrait _definitionMetadata;

	[XmlChoiceIdentifier("_optionsMetadata")]
	[XmlElement("ABS", Type=typeof(int))]
	[XmlElement("ClimateControl", Type=typeof(bool))]
	[XmlElement("XenonLights", Type=typeof(double))]
	public object[] Options;

	[XmlIgnore]
	public Option[] _optionsMetadata;

	public static Car CreateInstance()
	{
		Car c = new Car();

		c.Definition = 10000m;
		c._definitionMetadata = PrimaryTrait.Price;

		c.Options = new object[] {2, true};
		c._optionsMetadata = new Option[] 
			{Option.AntiBlockingSystem, Option.ClimateControl};

		return c;
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("Car, {0} = {1}{2}Options:{2}",
			_definitionMetadata,
			Definition,
			Environment.NewLine);

		if (_optionsMetadata != null && Options != null)
		{
			int len = Math.Min(Options.Length, 
				_optionsMetadata.Length);
			for (int i = 0; i < len; i++)
			{
				sb.AppendFormat("{0} = {1} ({2}){3}",
					_optionsMetadata[i],
					Options[i],
					Options[i].GetType(),
					Environment.NewLine);
			}
		}

		return sb.ToString();
	}
}