using System;
using System.Runtime.Serialization;

namespace Library
{
    [DataContract(Namespace = "http://example.org/customer")]
    public class Customer
    {
        public Customer() { }
        public Customer(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
        [DataMember]
        public string Name;
        [DataMember]
        public string Email;
    }
}
