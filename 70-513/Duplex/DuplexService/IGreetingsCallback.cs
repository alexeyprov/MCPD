using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace DuplexService
{
	[ServiceContract(Namespace = "DuplexService")]
	public interface IGreetingsCallback
	{
		[OperationContract(IsOneWay = true)]
		void GreetingGenerated(string greeting);
	}
}
