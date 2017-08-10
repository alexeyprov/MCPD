using System;
using System.Xml;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Library
{
    public class CustomerService : IUniversalOneWay
    {
        #region IUniversalOneWay Members

        public void ProcessMessage(Message msg)
        {
            Console.WriteLine("Message received...");

            XmlDocument doc = new XmlDocument();
            doc.Load(msg.GetReaderAtBodyContents());

            Console.WriteLine("Customer: {0} ({1})",
                doc.SelectSingleNode("/*/*[local-name()='Name']").InnerText,
                doc.SelectSingleNode("/*/*[local-name()='Email']").InnerText);
        }

        #endregion
    }
}
