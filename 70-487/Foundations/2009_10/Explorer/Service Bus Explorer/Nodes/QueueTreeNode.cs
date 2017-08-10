//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


using System.Windows.Forms;
using System.ServiceModel.Description;

namespace ServiceModelEx
{
   class QueueTreeNode : ServiceBusTreeNode 
   {
      public QueueTreeNode(ExplorerForm form,ServiceBusNode serviceBusNode,string name) : base(form,serviceBusNode,name,ExplorerForm.QueueIndex)
      {}

      public override void DisplayControl()
      {
         Form.DisplayQueueControl(ServiceBusNode,Credential);
      }
   }
}
