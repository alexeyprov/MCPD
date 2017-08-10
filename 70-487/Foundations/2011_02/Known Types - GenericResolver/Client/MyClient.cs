//2011 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using ServiceModelEx;

partial class MyClient : Form
{
   public MyClient()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      Customer customer = new Customer();
      customer.FirstName = "Juval";
      customer.LastName = "Lowy";
      customer.CustomerNumber = 123;

      Person person = new Person();
      person.FirstName = "Juval";
      person.LastName = "Lowy";
      person.FavoritColor = "Blue";

      ContactB contactB = new ContactB();
      contactB.FirstName = "Juval";
      contactB.LastName = "Lowy";
      contactB.Something = "AAAA";
      contactB.SomethingElse = "BBBB";

      ContainerClass.NestedClass nested = new ContainerClass.NestedClass();
      nested.FirstName = "Juval";
      nested.LastName = "Lowy";
      nested.Something = "AAAA";

      ContactManagerClient proxy = new ContactManagerClient();

      proxy.AddGenericResolver();
      //proxy.AddGenericResolver(typeof(Customer),typeof(Person),typeof(ContactB));
    
      proxy.AddContact(customer);
      proxy.AddContact(person);
      proxy.AddContact(contactB);
      proxy.AddContact(nested);
      
  
      Contact[] contacts = proxy.GetContacts();

      proxy.Close();
   }
}



