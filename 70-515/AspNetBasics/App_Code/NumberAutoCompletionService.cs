using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class NumberAutoCompletionService
{
	// To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
	// To create an operation that returns XML,
	//     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
	//     and include the following line in the operation body:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
	public List<string> GetNextChunk(string prefixText, int count)
	{
		List<string> result = new List<string>();
		int current = 0;

		if (Int32.TryParse(prefixText, out current))
		{
			int groupIndex = 0, groupLimit = 10;

			for (int i = 0; i < count; ++i)
			{
				if (groupIndex == groupLimit)
				{
					groupIndex = 0;
					groupLimit *= 10;
				}
				result.Add((current * groupLimit + groupIndex++).ToString());
			}
		}

		return result;
	}

	// Add more operations here and mark them with [OperationContract]
}
