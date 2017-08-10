using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;
using System.ServiceModel;

namespace DataContractSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateAndWriteMessage();
            //ReadAndProcessMessage();
            
            //CreateAndWriteMessageBinary();
            //ReadAndProcessMessageBinary();
            
            //CreateAndWritePerson();
            //ReadAndProcessPerson();
            
            //CreateAndWritePersonBinary();
            //ReadAndProcessPersonBinary();

            //WritePersonXmlSerializer();
            //ReadPersonXmlSerializer();

            //WriterPersonSerializable();
            //ReadPersonSerializable();
            
            //WriterPersonDataContract();
            //ReadPersonDataContract();

            //WriterPersonNetDataContract();
            //ReadPersonNetDataContract();

            //WritePersonCircular();
            //ReadPersonCircular();
        }

        static void CreateAndWriteMessage()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Person><name>Bob</name><age>34</age></Person>");
            XmlNodeReader reader = new XmlNodeReader(doc);
            
            using (Message msg = Message.CreateMessage(
                MessageVersion.Soap11WSAddressing10,
                "http://example.org/person",
                XmlReader.Create(reader, null)))
            {
                using (XmlWriter writer = XmlWriter.Create("msg.xml"))
                {
                    msg.WriteMessage(writer);
                }
            }
        }
        static void CreateAndWriteMessageBinary()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Person><name>Bob</name><age>34</age></Person>");
            XmlNodeReader reader = new XmlNodeReader(doc);

            using (Message msg = Message.CreateMessage(
                MessageVersion.Soap11WSAddressing10,
                "http://example.org/person",
                XmlReader.Create(reader, null)))
            {
                using (FileStream fs = new FileStream("msg.bin", FileMode.Create))
                {
                    using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(fs))
                    {
                        msg.WriteMessage(writer);
                    }
                }
            }
        }

        static void ReadAndProcessMessage()
        {
            using (XmlReader reader = XmlReader.Create("msg.xml"))
            {
                using (Message msg = Message.CreateMessage(
                    reader, 1024, MessageVersion.Soap11WSAddressing10))
                {
                    // use your fancy XML stuff here (XPath, XSLT, etc)

                    XmlReader bodyReader = msg.GetReaderAtBodyContents();
                    if (reader.ReadToDescendant("name"))
                    {
                        Console.WriteLine("name: {0}",
                            reader.ReadElementContentAsString());
                        Console.WriteLine("age: {0}",
                            reader.ReadElementContentAsString());
                    }
                }

            }
        }
        static void ReadAndProcessMessageBinary()
        {
            using (FileStream fs = new FileStream("msg.bin", FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(fs))
                {
                    using (Message msg = Message.CreateMessage(
                        reader, 1024, MessageVersion.Soap11WSAddressing10))
                    {
                        // use your fancy XML stuff here (XPath, XSLT, etc)

                        XmlReader bodyReader = msg.GetReaderAtBodyContents();
                        if (reader.ReadToDescendant("name"))
                        {
                            Console.WriteLine("name: {0}",
                                reader.ReadElementContentAsString());
                            Console.WriteLine("age: {0}",
                                reader.ReadElementContentAsString());
                        }
                    }
                }
            }
        }

        static void CreateAndWritePerson()
        {
            Person person = new Person("Bob", 34);

            using (Message msg = Message.CreateMessage(
                MessageVersion.Soap11WSAddressing10, // SOAP version
                "http://example.org/person", // Action
                person, // object 
                new DataContractSerializer(typeof(Person)))) // serializer
            {
                using (XmlWriter writer = XmlWriter.Create("msg.xml"))
                {
                    msg.WriteMessage(writer);
                }
            }
        }
        static void CreateAndWritePersonBinary()
        {
            Person person = new Person("Bob", 34);

            using (Message msg = Message.CreateMessage(
                MessageVersion.Soap11WSAddressing10, 
                "http://example.org/person", 
                person, 
                new DataContractSerializer(typeof(Person)))) // serializer
            {
                using (FileStream fs = new FileStream("msg.bin", FileMode.Create))
                {
                    using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(fs))
                    {
                        msg.WriteMessage(writer);
                    }
                }
            }
        }

        static void ReadAndProcessPerson()
        {
            using (XmlReader reader = XmlReader.Create("msg.xml"))
            {
                using (Message msg = Message.CreateMessage(
                    reader, 1024, MessageVersion.Soap11WSAddressing10)) 
                {
                    // map the XML to the Person class
                    Person person = msg.GetBody<Person>();
                    Console.WriteLine("name: {0}", person.Name);
                    Console.WriteLine("age: {0}", person.Age);
                }

            }
        }
        static void ReadAndProcessPersonBinary()
        {
            using (FileStream fs = new FileStream("msg.bin", FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(fs))
                {
                    using (Message msg = Message.CreateMessage(
                        reader, 1024, MessageVersion.Soap11WSAddressing10))
                    {
                        // map the XML to the Person class
                        Person person = msg.GetBody<Person>(
                            new DataContractSerializer(typeof(Person)));
                        Console.WriteLine("name: {0}", person.Name);
                        Console.WriteLine("age: {0}", person.Age);
                    }
                }
            }
        }

        static void WritePersonXmlSerializer()
        {
            PersonXS p = new PersonXS("Bob", 34, "secret");
            p.spouse = new PersonXS("Jane", 33, "secret");
                
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Create))
            {
                using (XmlDictionaryWriter writer =
                    XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    XmlSerializer serializer =
                        new XmlSerializer(typeof(PersonXS));
                    serializer.Serialize(writer, p);

                    Console.WriteLine("Wrote person.xml...\n");
                }
            }
        }
        static void ReadPersonXmlSerializer()
        {
            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Open))
            {
                using (XmlDictionaryReader reader =
                    XmlDictionaryReader.CreateTextReader(fs))
                {
                    XmlSerializer serializer =
                        new XmlSerializer(typeof(PersonXS));
                    PersonXS person =
                        (PersonXS)serializer.Deserialize(reader);

                    Console.WriteLine("Person: {0} is married to {1}",
                        person.Name, person.spouse.Name);
                }
            }
        }

        static void WriterPersonSerializable()
        {
            PersonS p = new PersonS("Bob", 34, "secret");
            p.spouse = new PersonS("Jane", 33, "secret");

            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Create))
            {
                using (XmlDictionaryWriter writer =
                    XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof(PersonS));
                    serializer.WriteObject(writer, p);

                    Console.WriteLine("Wrote person.xml...\n");
                }
            }
        }
        static void ReadPersonSerializable()
        {
            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Open))
            {
                using (XmlDictionaryReader reader =
                   XmlDictionaryReader.CreateTextReader(fs))
                {
                    // instantiate DataContractSerializer
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof(PersonS));
                    // read object from XML stream
                    PersonS person = (PersonS)serializer.ReadObject(reader);

                    Console.WriteLine("{0} is married to {1}",
                        person.Name, person.spouse.Name);
                }
            }
        }

        static void WriterPersonDataContract()
        {
            PersonDC p = new PersonDC("Bob", 34, "secret");
            p.spouse = new PersonDC("Jane", 33, "secret");

            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Create))
            {
                using (XmlDictionaryWriter writer =
                    XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof(PersonDC));
                    serializer.WriteObject(writer, p);

                    Console.WriteLine("Wrote person.xml...\n");
                }
            }
        }
        static void ReadPersonDataContract()
        {
            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Open))
            {
                using (XmlDictionaryReader reader =
                   XmlDictionaryReader.CreateTextReader(fs))
                {
                    // instantiate DataContractSerializer
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof(PersonDC));
                    // read object from XML stream
                    PersonDC person = (PersonDC)serializer.ReadObject(reader);

                    Console.WriteLine("{0} is married to {1}",
                        person.Name, person.spouse.Name);
                }
            }
        }

        static void WriterPersonNetDataContract()
        {
            PersonDC p = new PersonDC("Bob", 34, "secret");
            p.spouse = new PersonDC("Jane", 33, "secret");

            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Create))
            {
                using (XmlDictionaryWriter writer =
                    XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    NetDataContractSerializer serializer =
                        new NetDataContractSerializer();
                    serializer.WriteObject(writer, p);

                    Console.WriteLine("Wrote person.xml...\n");
                }
            }
        }
        static void ReadPersonNetDataContract()
        {
            // construct file stream
            using (FileStream fs = new FileStream(
                        "person.xml", FileMode.Open))
            {
                using (XmlDictionaryReader reader =
                   XmlDictionaryReader.CreateTextReader(fs))
                {
                    // instantiate DataContractSerializer
                    NetDataContractSerializer serializer =
                        new NetDataContractSerializer();
                    // read object from XML stream
                    PersonDC person = (PersonDC)serializer.ReadObject(reader);

                    Console.WriteLine("{0} is married to {1}",
                        person.Name, person.spouse.Name);
                }
            }
        }

        static void WritePersonCircular()
        {
            PersonDC monica = new PersonDC("Bob", 34, "secret");
            PersonDC aaron = new PersonDC("Jane", 33, "secret");
            monica.spouse = aaron;
            aaron.spouse = monica;

            DataContractSerializer serializer = new DataContractSerializer(
                typeof(PersonDC), null, Int32.MaxValue, false, 
                /* preserve object references */ true, 
                null);
            
            using (FileStream fs = new FileStream("person.xml", FileMode.Create))
            {
                serializer.WriteObject(fs, monica);
            }
        }
        static void ReadPersonCircular()
        {
            DataContractSerializer serializer = new DataContractSerializer(
                typeof(PersonDC), null, Int32.MaxValue, false, 
                /* preserve object references */ true, 
                null);

            using (FileStream fs = new FileStream("person.xml", FileMode.Open))
            {
                PersonDC p = (PersonDC)serializer.ReadObject(fs);
                Console.WriteLine("{0} is married to {1}",
                    p.Name, p.spouse.Name);
            }
        }
         
    }
}
