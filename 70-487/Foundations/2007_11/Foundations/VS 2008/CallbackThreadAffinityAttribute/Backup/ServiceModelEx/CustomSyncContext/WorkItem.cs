//2008 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Security.Permissions;

namespace ServiceModelEx
{
   [Serializable]
   internal class WorkItem
   {
      object m_State;
      SendOrPostCallback m_Method;
      ManualResetEvent m_AsyncWaitHandle;

      public WaitHandle AsyncWaitHandle
      {
         get
         {
            return m_AsyncWaitHandle;
         }
      }

      internal WorkItem(SendOrPostCallback method,object state)
      {
         m_Method = method;
         m_State = state;
         m_AsyncWaitHandle = new ManualResetEvent(false);
      }

      //This method is called on the worker thread to execute the method
      internal void CallBack()
      {
         m_Method(m_State);
         m_AsyncWaitHandle.Set();
      }
   }
}