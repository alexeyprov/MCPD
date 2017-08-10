//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Threading;

namespace ServiceModelEx
{
   partial class SolutionViewControl : NodeViewControl
   {
      public SolutionViewControl()
      {
         InitializeComponent();
      }
      public void Refresh(string solution)
      {
         string address = @"http://portal.ex.azure.microsoft.com/View.aspx?name="+solution;
         m_WebBrowser.Navigate(address);
      }
   }
}
