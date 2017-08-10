//2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System.Runtime.Serialization;
using System.ServiceModel;
using MyNamespace;

namespace MyNamespace
{
   [DataContract]
   class Contact
   {
      [DataMember]
      public string FirstName
      {get;set;}

      [DataMember]
      public string LastName
      {get;set;}
   }

   [DataContract]
   class Customer : Contact
   {
      [DataMember]
      public int CustomerNumber
      {get;set;}
   }
}

[ServiceContract]
interface IContactManager
{
   [OperationContract]
   void AddContact(Contact contact);

   [OperationContract]
   Contact[] GetContacts();
}

class ContactManagerClient : ClientBase<IContactManager>,IContactManager
{
   public void AddContact(Contact contact)
   {
      Channel.AddContact(contact);
   }

   public Contact[] GetContacts()
   {
      return Channel.GetContacts();
   }
}

