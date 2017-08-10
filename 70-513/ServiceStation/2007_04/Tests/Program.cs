using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Library;

namespace Tests
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            //ReadFromTextWriteToBinary();
            //ReadFromBinaryWriteToMtom();
            //ReadFromMtomWriteToText();
            //CreateMessageFromScratch1();
            //CreateMessageFromScratch2();
            //CreateMessageFromObject();
            //ReadInMessage();
            //CheckState();
            //CreateMessageWithHeadersAndProperties();
        }
        #endregion

        static void SerializeUsingXDW()
        {
            Customer cust = new Customer("Bob", "bob@abc.com");
            DataContractSerializer dcs = new DataContractSerializer(typeof(Customer));
            FileStream fs = new FileStream("output.xml", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateTextWriter(fs))
            {
                dcs.WriteObject(xw, cust);
            }
        }

        static void ReadFromTextWriteToBinary()
        {
            // read from text (XML 1.0) representation
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\customer.xml");
            
            // write to binary representation
            FileStream custBinStream = new FileStream(
                "customer.bin", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateBinaryWriter(
                custBinStream))
            {
                doc.WriteTo(xw);
            }
        }

        static void ReadFromBinaryWriteToMtom()
        {
            // read from binary representation
            XmlDocument doc = new XmlDocument();
            FileStream custBinStream = new FileStream(
                "customer.bin", FileMode.Open);
            using (XmlReader xr = XmlDictionaryReader.CreateBinaryReader(
                custBinStream, XmlDictionaryReaderQuotas.Max))
            {
                doc.Load(xr);
            }

            // write to MTOM representation
            FileStream custMtomStream = new FileStream(
                "customer.mtom", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateMtomWriter(
                custMtomStream, Encoding.UTF8, 1024, "text/xml"))
            {
                doc.WriteTo(xw);
            }

        }

        static void ReadFromMtomWriteToText()
        {
            // read from MTOM representation
            XmlDocument doc = new XmlDocument();
            FileStream custMtomStream = new FileStream(
                "customer.mtom", FileMode.Open);
            using (XmlReader xr = XmlDictionaryReader.CreateMtomReader(
                custMtomStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max))
            {
                doc.Load(xr);
            }

            // write to text (XML 1.0) representation
            doc.Save("customer.xml");
        }

        static void CreateMessageFromScratch1()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\customer.xml");

            Message m = Message.CreateMessage(
                MessageVersion.Soap11WSAddressingAugust2004, "urn:add-customer", 
                    new XmlNodeReader(doc));

            FileStream fs = new FileStream("message.xml", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateTextWriter(fs))
            {
                m.WriteMessage(xw);
            }
        }

        static void CreateMessageFromScratch2()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\customer.xml");

            Message m = Message.CreateMessage(
                MessageVersion.None, "urn:add-customer",
                    new XmlNodeReader(doc));

            FileStream fs = new FileStream("message2.xml", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateTextWriter(fs))
            {
                m.WriteMessage(xw);
            }
        }

        static void CreateMessageFromObject()
        {
            Customer cust = new Customer("Bob", "bob@abc.com");

            Message m = Message.CreateMessage(
                MessageVersion.Soap11WSAddressingAugust2004, "urn:add-customer", cust);

            FileStream fs = new FileStream("message3.xml", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateTextWriter(fs))
            {
                m.WriteMessage(xw);
            }
        }

        static void ReadInMessage()
        {
            FileStream fs = new FileStream("message.xml", FileMode.Open);
            using (XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas.Max))
            {
                Message m = Message.CreateMessage(
                    reader, 1024, MessageVersion.Soap11WSAddressingAugust2004);

                XmlDocument doc = new XmlDocument();
                doc.Load(m.GetReaderAtBodyContents());
                Console.WriteLine(doc.InnerXml);
            }
        }

        static void CreateMessageWithHeadersAndProperties()
        {
            Customer cust = new Customer("Bob", "bob@abc.com");

            Message m = Message.CreateMessage(
                MessageVersion.Default, "urn:add-customer", cust);

            m.Headers.Add(
                MessageHeader.CreateHeader(
                    "ContextId", "http://example.org/customHeaders", Guid.NewGuid()));

            m.Properties.Add("abc", "123");
            m.Properties.Add("def", "456");

            FileStream fs = new FileStream("message4.xml", FileMode.Create);
            using (XmlWriter xw = XmlDictionaryWriter.CreateTextWriter(fs))
            {
                m.WriteMessage(xw);
            }
        }
    }
}
