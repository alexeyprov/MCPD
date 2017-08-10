namespace MyWcfService
{
    public class MyService : IMyService
    {
#if V1
        public string GetData(int value)
        {
            return "You Entered: " + value;
        }
#endif

#if V2
        //Case1 : Adding new parameters to the operation signature 
        public string GetData(int value, int newvalue)
        {
            return $"You entered {value}. Newly Entered Value {newvalue}";
        }
#endif

#if V3
        //Case2 : Removing parameters from an operation signature 
        public string GetData()
        {
            return "No parameters";
        } 
#endif

#if V4
        //Case3 : Modifying parameter types of operations - compatible
        public string GetData(string strVal)
        {
            return "You Entered: " + strVal;
        } 
#endif

#if V5
        //Case4 : Modifying parameter types of operations - incompatible
        public string GetData(System.DateTime dtVal)
        {
            return "You Entered: " + dtVal;
        } 
#endif

#if V6
        //Case5 : Modifying return value of operations
        public int GetData(int value)
        {
            return value;
        } 
#endif

#if V7
        //Case6 : Adding new operations 
        public string GetData(int value)
        {
            return "You Entered: " + value;
        }

        public int GetNewData(int value)
        {
            return value;
        }
#endif

#if V8
        //Case8 : Removing operation
        public int GetNewData(int value)
        {
            return value;
        }
#endif
    }
}
