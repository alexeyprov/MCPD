namespace POTest
{
    using System;
	using System.IO;
	using System.Text;
	using PurchaseOrder;

    public class Test
    {
		

        public Test()
        {
        }

        public static int Main(string[] args)
        {

			FileStream f = new FileStream(System.Environment.CurrentDirectory + "\\" + "po.xml", FileMode.Open, FileAccess.Read);

			StringBuilder POXml = new StringBuilder();
			POXml.Length = 0;

			StreamReader r = new StreamReader(f);
			r.BaseStream.Seek(0, SeekOrigin.Begin);

			while (r.Peek() > -1) 
			{
			POXml.Append(r.ReadLine() + "\n");
			}
			
			try
			{
				PurchaseOrder PO = new PurchaseOrder(POXml.ToString());
				Console.WriteLine("Deserialization successful, press the enter key to serialize.");
				Console.ReadLine();
				Console.Write(PO.Serialize());
				Console.ReadLine();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				Console.Read();
			}

            return 0;
        }
    }
}
