using System;
using System.Data;
using System.IO;
using System.Xml.Serialization;

class DataSetTest
{
	private static DataSet PrepareDataset()
	{
		DataSet ds = new DataSet("Friends");
		
		DataTable t = ds.Tables.Add("People");
		t.Columns.Add("Name");
		t.Columns.Add("DoB");

		t.Rows.Add(new object[] {"Andrey Z", new DateTime(1982, 4, 17)});
                t.Rows.Add(new object[] {"Alexander Z", new DateTime(1982, 4, 23)});

		return ds;
	}

	private static void Main()
	{
		XmlSerializer ser = new XmlSerializer(typeof(DataSet));
		using (FileStream fs = new FileStream("Friends.xml", FileMode.Create))
		{
			DataSet ds = PrepareDataset();
			ser.Serialize(fs, ds);
		}
	}
}