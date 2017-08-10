//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


using System.Windows.Forms;
using System.ServiceModel.Description;
using System.Diagnostics;

namespace ServiceModelEx
{
   class SolutionTreeNode : ServiceBusTreeNode 
   {
      public SolutionTreeNode(ExplorerForm form) : base(form,null,"Unspecified Solution",ExplorerForm.UnspecifiedSolutionIndex)
      {} 
      public SolutionTreeNode(ExplorerForm form,string solution) : this(form,solution,ExplorerForm.SolutionIndex)
      {}
      public SolutionTreeNode(ExplorerForm form,string solution,int imageIndex) : base(form,null,solution,imageIndex)
      {}

      public override void DisplayControl()
      {
         if(Text == "Unspecified Solution")
         {
            Form.DisplayBlankControl();
            Form.SelectSolutionTextBox();
         }
         Form.DisplaySolutionControl(Text);
      }
   }
}
