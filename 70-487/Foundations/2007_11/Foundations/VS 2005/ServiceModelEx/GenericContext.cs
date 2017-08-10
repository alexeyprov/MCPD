//2008 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Diagnostics;


namespace ServiceModelEx
{
   [DataContract]
   public class GenericContext<T>
   {
      static GenericContext()
      {
         //Verify [DataContract] or [Serializable] on T
         Debug.Assert(IsDataContract(typeof(T)) || typeof(T).IsSerializable);
      }
      static bool IsDataContract(Type type)
      {
         object[] attributes = type.GetCustomAttributes(typeof(DataContractAttribute),false);
         return attributes.Length == 1;
      }

      [DataMember]
      public readonly T Value;

      public GenericContext(T value)
      {
         Value = value;
      }
      public GenericContext() : this(default(T))
      {}
      public static GenericContext<T> Current
      {
         get
         {
            OperationContext context = OperationContext.Current;
            if(context == null)
            {
               return null;
            }
            try
            {
               string typeName = typeof(T).ToString().Replace("`","");
               typeName = typeName.Replace("[","");
               typeName = typeName.Replace("]","");
               string nameSpace = typeof(T).Namespace ?? "";

               return context.IncomingMessageHeaders.GetHeader<GenericContext<T>>(typeName,nameSpace);
            }
            catch(Exception exception)
            {
               Debug.Assert(exception is MessageHeaderException && exception.Message == "There is not a header with name " + typeof(GenericContext<T>) + " and namespace " + typeof(GenericContext<T>).Namespace + " in the message.");
               return null;
            }
         }
         set
         {
            OperationContext context = OperationContext.Current;
            Debug.Assert(context != null);

            string typeName = typeof(T).ToString().Replace("`","");
            typeName = typeName.Replace("[","");
            typeName = typeName.Replace("]","");
            string nameSpace = typeof(T).Namespace ?? "";

            //Having multiple GenericContext<T> headers is an error
            bool headerExists = false;
            try
            {
               context.OutgoingMessageHeaders.GetHeader<GenericContext<T>>(typeName,nameSpace);
               headerExists = true;
            }
            catch(MessageHeaderException exception)
            {
               Debug.Assert(exception.Message == "There is not a header with name " + typeName + " and namespace " + nameSpace + " in the message.");
            }
            if(headerExists)
            {
               throw new InvalidOperationException("A header with name " + typeName + " and namespace " + nameSpace + " already exists in the message.");
            }
            MessageHeader<GenericContext<T>> genericHeader = new MessageHeader<GenericContext<T>>(value);
            context.OutgoingMessageHeaders.Add(genericHeader.GetUntypedHeader(typeName,nameSpace));
         }
      }
   }
}