//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Messaging;
using System.Reflection;
using System.Transactions;

static class Program
{
   const string QueueName = ".\\private$\\MyServiceQueue";
   //Easiest way to create partial trust client is to permit only what is required

   static void Main()
   {
      if(MessageQueue.Exists(QueueName) == false)
      {
         MessageQueue.Create(QueueName,true);
      } 

      DisplayForm();
   }

   //Permissions to display form and execute
   [SecurityPermission(SecurityAction.PermitOnly,Flags=SecurityPermissionFlag.Execution)]
   [UIPermission(SecurityAction.PermitOnly,Window=UIPermissionWindow.SafeTopLevelWindows)]

   //Permissions to call over MSMQ without security
   [MessageQueuePermission(SecurityAction.PermitOnly,PermissionAccess = MessageQueuePermissionAccess.Send,Path = QueueName)]
   static void DisplayForm()
   {
      Application.Run(new MyClientForm());
   }
   
}