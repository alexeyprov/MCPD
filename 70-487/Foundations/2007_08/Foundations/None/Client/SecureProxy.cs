//IDesign Inc. 2007 
//Questions? Comments? go to 
//http://www.idesign.net


using System.ServiceModel;
using ServiceModelEx;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;


public partial class MyContractSecureClient : SecureClientBase<IMyContract>,IMyContract
{

   public MyContractSecureClient(ServiceSecurity mode) : base(mode)
   {}
   public MyContractSecureClient(string userName,string password) : base(userName,password)
   {}
   public MyContractSecureClient(string domain,string userName,string password,TokenImpersonationLevel impersonationLevel) : base(domain,userName,password,impersonationLevel)
   {}
   public MyContractSecureClient(string domain,string userName,string password) : base(domain,userName,password)
   {}
   public MyContractSecureClient(string clientCertificateName) : base(clientCertificateName)
   {}
   public MyContractSecureClient(StoreLocation storeLocation,StoreName storeName,X509FindType findType,string clientCertificateName) : base(storeLocation,storeName,findType,clientCertificateName)
   {}

   public void MyMethod()
   {
     Channel.MyMethod();
   }
}
