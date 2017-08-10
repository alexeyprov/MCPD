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
    void MyMethod();
}

public interface IMyContractChannel : IMyContract,IClientChannel
{
}

public partial class MyContractProxy : ClientBase<IMyContract>,IMyContract
{
    
    public MyContractProxy()
    {
    }
    
    public MyContractProxy(string configurationName) : 
            base(configurationName)
    {
    }
    
    
    public void MyMethod()
    {
        InnerProxy.MyMethod();
    }
}
