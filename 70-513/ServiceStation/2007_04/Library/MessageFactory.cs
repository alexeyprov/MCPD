using System;
using System.ServiceModel.Channels;

namespace Library
{
    public class MessageFactory
    {
        public static Message CreateAddCustomerMessage(MessageVersion version)
        {
            Customer cust = new Customer("Bob", "bob@abc.com");
            return Message.CreateMessage(version, "urn:add-customer", cust);
        }
    }
}
