using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NetHttpBindingLibrary
{
    public class NetHttpBinding : Binding
    {
        private BinaryMessageEncodingBindingElement binary =
            new BinaryMessageEncodingBindingElement();
        private HttpTransportBindingElement http =
            new HttpTransportBindingElement();

        public override BindingElementCollection CreateBindingElements()
        {
            return new BindingElementCollection(new BindingElement[] { binary, http });
        }

        public TransferMode TransferMode
        {
            get
            {
                return http.TransferMode;
            }
            set
            {
                http.TransferMode = value;
            }
        }

        public bool UseDefaultWebProxy
        {
            get
            {
                return http.UseDefaultWebProxy;
            }
            set
            {
                http.UseDefaultWebProxy = value;
            }
        }

        public override string Scheme
        {
            get { return "http"; }
        }
    }
}

