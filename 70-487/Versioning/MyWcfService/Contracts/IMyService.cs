using System.ServiceModel;

namespace MyWcfService
{
    [ServiceContract]
    public interface IMyService
    {
#if V1
        [OperationContract]
        string GetData(int value);
#endif

#if V2
        [OperationContract]
        //Case1 : Adding new parameters to the operation signature 
        string GetData(int value,int newvalue);
#endif

#if V3
        [OperationContract]
        //Case2 : Removing parameters from an operation signature 
        string GetData(); 
#endif

#if V4
        [OperationContract]
        //Case3 : Modifying parameter types of operations - compatible
        string GetData(string value); 
#endif

#if V5
        [OperationContract]
        //Case4 : Modifying parameter types of operations - incompatible
        string GetData(System.DateTime value);
#endif

#if V6
        [OperationContract]
        //Case5 : Modifying return value types 
        int GetData(int value); 
#endif

#if V7
        //Case6 : Adding new operations 

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        int GetNewData(int value);
#endif

#if V8
        //Case7 : Removing operation
        [OperationContract]
        int GetNewData(int value);
#endif
    }
}
