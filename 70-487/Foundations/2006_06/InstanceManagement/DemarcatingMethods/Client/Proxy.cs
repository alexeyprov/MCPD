//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;



[ServiceContract]
public interface IMyContract
{
   [OperationContract]
   void StartSession();

   [OperationContract]
   void CannotStart();

   [OperationContract]
   void EndSession();

   [OperationContract]
   void StartAndEndSession();
}

public interface IMyContractChannel : IMyContract,IClientChannel
{
}

public partial class MyContractProxy : ClientBase<IMyContract>,IMyContract
{
    
    public MyContractProxy()
    {
    }
    
    public MyContractProxy(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public MyContractProxy(string endpointConfigurationName,string remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public MyContractProxy(string endpointConfigurationName,EndpointAddress remoteAddress) : 
            base(endpointConfigurationName,remoteAddress)
    {
    }
    
    public MyContractProxy(Binding binding,EndpointAddress remoteAddress) : 
            base(binding,remoteAddress)
    {
    }
    
    public void StartSession()
    {
        InnerProxy.StartSession();
    }
    
    public void CannotStart()
    {
        InnerProxy.CannotStart();
    }
    
    public void EndSession()
    {
        InnerProxy.EndSession();
    }
    
    public void StartAndEndSession()
    {
        InnerProxy.StartAndEndSession();
    }
}
