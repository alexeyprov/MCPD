//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;

public partial class HostForm : Form
{
   public HostForm()
   {
      InitializeComponent();
      Text = AppDomain.CurrentDomain.FriendlyName;
   }
}