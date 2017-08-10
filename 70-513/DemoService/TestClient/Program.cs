using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			using (HeaderWcfService.GetHeadersServiceClient proxy =
				new HeaderWcfService.GetHeadersServiceClient("WSHttpBinding_IGetHeadersService"))
			{
				Console.Write("Enter header name: ");
				string header = Console.ReadLine();

				Console.WriteLine("Header value: " + proxy.GetHeaderValue(header));
				Console.WriteLine("Custom header value: " + proxy.GetCustomHeaderValue());
			}
		}
	}
}
