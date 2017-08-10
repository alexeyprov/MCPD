using System;
using System.Runtime.Serialization;

// There should be only one instance of this type per AppDomain
[Serializable]
public sealed class Singleton : ISerializable {
   // This is the one instance of this type
   private static readonly Singleton theOneObject = new Singleton();

   // Here are the instance fields
   public String someString;
   public Int32 someNumber;

   // Private constructor allowing this type to construct the singleton
   private Singleton() { 
      // Do whatever is necessary to initialize the singleton
      someString = "This is a string field";
      someNumber = 123;
   }

   // Method returning a reference to the singleton
   public static Singleton GetSingleton() { 
      return theOneObject; 
   }

   // Method called when serializing a Singleton
   // I recommend using an Explicit Interface Method Impl. here
   void ISerializable.GetObjectData(
      SerializationInfo info, StreamingContext context) {

      info.SetType(typeof(SingletonSerializationHelper));
      // No other values need to be added
   }

   // NOTE: The special constructor is NOT necessary because 
   // it's never called
}

[Serializable]
internal sealed class SingletonSerializationHelper : IObjectReference {
   // This object has no fields (although it could)

   // Method called after this object is deserialized
   public Object GetRealObject(StreamingContext context) {
      return Singleton.GetSingleton();
   }
}

