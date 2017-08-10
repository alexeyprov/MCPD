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
using System.ServiceModel.Channels;
using System.Diagnostics;
using System.ServiceModel.Description;

namespace Router
{
    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2008/01")]
    public interface IRouterService
    {
        [OperationContract(Action = "*", ReplyAction = "*")]
        Message ProcessMessage(Message requestMessage);

    }

   [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RouterService : IRouterService
    {

        #region IRouterService Members
 
        public Message ProcessMessage(Message requestMessage)
        {

            
            using (ChannelFactory<IRouterService> factory = new ChannelFactory<IRouterService>("serviceEndpoint"))
            {
                IRouterService proxy = factory.CreateChannel();

                using (proxy as IDisposable)
                {
                    // log request message
                    IClientChannel clientChannel = proxy as IClientChannel;
                    Console.WriteLine(String.Format("Request received at {0}, to {1}\r\n\tAction: {2}", DateTime.Now, clientChannel.RemoteAddress.Uri.AbsoluteUri, requestMessage.Headers.Action));

                    // invoke service
                    Message responseMessage = proxy.ProcessMessage(requestMessage);

                    // log response message
                    Console.WriteLine(String.Format("Reply received at {0}\r\n\tAction: {1}", DateTime.Now, responseMessage.Headers.Action));
                    Console.WriteLine();

                    return responseMessage;
                }
            }
           

        }

        #endregion
        
    }
}
