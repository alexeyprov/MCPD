//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace ServiceModelEx
{
   public static class SecurityHelper
   {
      public static void ImpersonateAll(this ServiceHostBase host)
      {
         if(host.State == CommunicationState.Opened)
         {
            throw new InvalidOperationException("Host is already opened");
         }
         host.Authorization.ImpersonateCallerForAllOperations = true;
         host.Description.ImpersonateAll();
      }
      public static void ImpersonateAll(this ServiceDescription description)
      {
         foreach(ServiceEndpoint endpoint in description.Endpoints)
         {
            if(endpoint.Contract.Name == "IMetadataExchange")
            {
               continue;
            } 
            foreach(OperationDescription operation in endpoint.Contract.Operations)
            {
               OperationBehaviorAttribute attribute = operation.Behaviors.Find<OperationBehaviorAttribute>();
               if(attribute != null)
               {
                  if(attribute.Impersonation == ImpersonationOption.NotAllowed)
                  {
                     Trace.WriteLine("Overriding impersonation setting of " + endpoint.Contract.Name + "." + operation.Name);
                  }
                  attribute.Impersonation = ImpersonationOption.Required; 
                  continue;
               }
            }
         }
      }
   }
}