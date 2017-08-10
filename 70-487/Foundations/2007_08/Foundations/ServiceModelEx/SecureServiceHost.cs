//IDesign Inc. 2007 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;

namespace ServiceModelEx
{
   public class SecureServiceHost : ServiceHost
   {
      /// <summary>
      /// Can only call before openning the host
      /// </summary>
      public void ImpersonateAll()
      {
         if(State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         SecurityHelper.ImpersonateAll(this);
      }
      /// <summary>
      /// Can only call before openning the host
      /// </summary>
      /// <param name="mode">If set to ServiceSecurity.Anonymous,ServiceSecurity.BusinessToBusiness or ServiceSecurity.Internet then the service certificate must be listed in config file</param>
      public void SetSecurityBehavior(ServiceSecurity mode,bool useAspNetProviders,string applicationName,bool impersonateAll)
      {
         SetSecurityBehavior(mode,StoreLocation.LocalMachine,StoreName.My,X509FindType.FindBySubjectName,null,useAspNetProviders,applicationName,impersonateAll);
      }
      /// <summary>
      /// Can only call before openning the host
      /// </summary>
      /// <param name="mode">Certificate is looked up by name from LocalMachine/My store</param>
      public void SetSecurityBehavior(ServiceSecurity mode,string serviceCertificateName,bool useAspNetProviders,string applicationName,bool impersonateAll) 
      {
         SetSecurityBehavior(mode,StoreLocation.LocalMachine,StoreName.My,X509FindType.FindBySubjectName,serviceCertificateName,useAspNetProviders,applicationName,impersonateAll);
      }

      /// <summary>
      /// Can only call before openning the host
      /// </summary>
      public void SetSecurityBehavior(ServiceSecurity mode,StoreLocation storeLocation,StoreName storeName,X509FindType findType,string serviceCertificateName,bool useAspNetProviders,string applicationName,bool impersonateAll)
      {
         if(State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         SecurityBehavior securityBehavior = new SecurityBehavior(mode,storeLocation,storeName,findType,serviceCertificateName);

         securityBehavior.UseAspNetProviders = useAspNetProviders;
         securityBehavior.ApplicationName = applicationName;
         securityBehavior.ImpersonateAll = impersonateAll;

         Description.Behaviors.Add(securityBehavior);
      }
   }
}





