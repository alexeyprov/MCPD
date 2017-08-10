//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;

[CallbackBehavior(UseSynchronizationContext = false)]
partial class MyClientForm : Form,IMyContractCallback
{
   public MyClientForm()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      MyContractClient proxy = new MyContractClient(this);
      proxy.MyMethod();
      proxy.Close();
   }
   public void OnMyCallback()
   {
      MessageBox.Show("OnMyCallback","MyClientForm");
   }
}



