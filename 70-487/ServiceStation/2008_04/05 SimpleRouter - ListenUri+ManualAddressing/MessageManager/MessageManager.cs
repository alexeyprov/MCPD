// © 2007-2008 Michele Leroux Bustamante. All rights reserved 
// Book: Learning WCF, O'Reilly
// Book Blog: www.thatindigogirl.com
// Michele's Blog: www.dasblonde.net
// IDesign: www.idesign.net

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MessageManager
{

    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2008/01")]
    public interface IMessageManagerService
    {
        [OperationContract]
        string SendMessage(string msg);

        [OperationContract]
        void SendOneWayMessage(string msg);
    }

    public class MessageManagerService : IMessageManagerService
    {

        #region IMessageManagerService Members

        string IMessageManagerService.SendMessage(string msg)
        {
            string s = string.Format("Message received at {0}: {1}", DateTime.Now, msg);
            Console.WriteLine(s);
            return s;
        }

        void IMessageManagerService.SendOneWayMessage(string msg)
        {
            string s = string.Format("One-way message received at {0}: {1}", DateTime.Now, msg);
            Console.WriteLine(s);
        }

        #endregion
    }



}
