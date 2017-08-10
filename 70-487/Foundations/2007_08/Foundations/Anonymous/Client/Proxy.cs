//IDesign Inc. 2007 
//Questions? Comments? go to 
//http://www.idesign.net


using System.ServiceModel;

[ServiceContract]
public interface IMyContract
{
    [OperationContract]
    void MyMethod();
}

public partial class MyContractClient : ClientBase<IMyContract>,IMyContract
{
   public MyContractClient()
   {}
   public MyContractClient(string configurationName) : base(configurationName)
   {}

   public void MyMethod()
   {
      Channel.MyMethod();
   }
}
