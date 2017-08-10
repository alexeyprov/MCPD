using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;

namespace DemoService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class GetHeadersService : IGetHeadersService
	{
		string IGetHeadersService.GetHeaderValue(string name)
		{
			if (String.IsNullOrEmpty(name))
			{
				return String.Empty;
			}

			MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

			switch (name.ToLower())
			{
				case "action":
					return headers.Action;

				case "from":
					return headers.From.Uri.ToString();

				case "to":
					return headers.To.ToString();

				default:
					return "Unknown header";
			}
		}

		string IGetHeadersService.GetCustomHeaderValue()
		{
			MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

			int idx = headers.FindHeader("serviceType", "http://tempuri.org");

			if (idx >= 0)
			{
				return headers.GetHeader<string>(idx);
				//using (XmlReader reader = headers.GetReaderAtHeader(idx))
				//{
				//    XmlDocument doc = new XmlDocument();

				//    doc.Load(reader);

				//    return doc.SelectSingleNode("/*/subscriptionType").InnerXml;
				//}
			}

			return "Header not found";
		}
	}
}
