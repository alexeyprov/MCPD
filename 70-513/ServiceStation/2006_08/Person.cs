using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DataContractSamples
{
    [XmlRoot(Namespace="http://example.org/person")]
    public class PersonXS
    {
        private string name;
        private double age;
        [XmlIgnore]
        public string sensitiveData;
        [XmlElement(Order = 2)]
        public PersonXS spouse;
 
        public PersonXS() { }
        public PersonXS(string name, double age, string data)
        {
            this.name = name;
            this.age = age;
            this.sensitiveData = data;
        }
        [XmlElement(Order=1)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [XmlAttribute]
        public double Age
        {
            get { return age; }
            set { age = value; }
        }
    }

    [Serializable]
    public class PersonS
    {
        private string name;
        private double age;
        [NonSerialized]
        private string sensitiveData;
        public PersonS spouse;

        public PersonS() { Console.WriteLine("Constructor called");  }
        public PersonS(string name, double age, string data)
        {
            this.name = name;
            this.age = age;
            this.sensitiveData = data;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Age
        {
            get { return age; }
            set { age = value; }
        }
    }

    [DataContract(Namespace = "http://example.org/person")]
    public class PersonDC
    {
        [DataMember(Name = "Name", Order = 1,
           IsRequired = true)]
        private string name;
        private double age;
        private string sensitiveData;
        [DataMember(Name = "Spouse", Order = 3)]
        public PersonDC spouse;

        public PersonDC() { Console.WriteLine("Constructor called"); }
        public PersonDC(string name, double age, string data)
        {
            this.name = name;
            this.age = age;
            this.sensitiveData = data;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember(Order = 2, IsRequired = true)]
        public double Age
        {
            get { return age; }
            set { age = value; }
        }
    }

    [DataContract]
    public class Person
    {
        private string name;
        private double age;
        public Person spouse;

        public Person() { }
        public Person(string name, double age)
        {
            this.name = name;
            this.age = age;
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public double Age
        {
            get { return age; }
            set { age = value; }
        }
    }

}
