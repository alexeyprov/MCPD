//2011 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using ServiceModelEx;

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

[DataContract]
class Person : Contact
{
   [DataMember]
   public string FavoritColor
   {get;set;}
}



[DataContract]
class ContactA : Contact
{
   [DataMember]
   public string Something
   {get;set;}
}


[DataContract]
class ContactB : ContactA
{
   [DataMember]
   public string SomethingElse
   {get;set;}
}

[DataContract]
class ContainerClass
{
   [DataContract]
   public class NestedClass : Contact
   {
      [DataMember]
      public string Something
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

