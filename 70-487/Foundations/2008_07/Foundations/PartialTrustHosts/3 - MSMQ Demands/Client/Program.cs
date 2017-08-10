//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Messaging;


static class Program
{
   const string QueueName = ".\\private$\\MyServiceQueue";

   static void Main()
   {
      if(MessageQueue.Exists(QueueName) == false)
      {
         MessageQueue.Create(QueueName,true);
      }
      Application.Run(new MyClientForm());
   }
 
}