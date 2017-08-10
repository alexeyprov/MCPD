using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class DummyWebservice : WebService
{
	[WebMethod]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string HelloToYou(string name)
	{
		return "Hello " + name;
	}

	[WebMethod]
	public string sayHello()
	{
		return "hello ";
	}

}