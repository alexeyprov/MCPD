//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;

namespace DuplexDemo
{
   public partial class MyForm : Form
   {
        ServiceHost host;
      public MyForm()
      {
         InitializeComponent();
      }

        private void MyForm_Load(object sender, EventArgs e)
        {
            host = new ServiceHost(typeof(MyService), new Uri("http://localhost:8000/"));
            host.Open();
        }

        private void MyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            host.Close();
        }
    }
}