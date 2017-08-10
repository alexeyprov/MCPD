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
   [SecurityPermission(SecurityAction.Demand,ControlThread = true)]
   public class ThreadPoolSynchronizer : SynchronizationContext,IDisposable
   {
      class WorkerThread
      {
         ThreadPoolSynchronizer m_Context;
         public Thread m_ThreadObj;
         bool m_EndLoop;

         public int ManagedThreadId
         {
            get
            {
               return m_ThreadObj.ManagedThreadId;
            }
         }

         internal WorkerThread(string name,ThreadPoolSynchronizer context)
         {
            m_Context = context;

            m_EndLoop = false;
            m_ThreadObj = null;

            m_ThreadObj = new Thread(Run);
            m_ThreadObj.IsBackground = true;
            m_ThreadObj.Name = name;
            m_ThreadObj.Start();
         }
         bool EndLoop
         {
            set
            {
               lock(this)
               {
                  m_EndLoop = value;
               }
            }
            get
            {
               lock(this)
               {
                  return m_EndLoop;
               }
            }
         }
         void Start()
         {
            Debug.Assert(m_ThreadObj != null);
            Debug.Assert(m_ThreadObj.IsAlive == false);
            m_ThreadObj.Start();
         }
         void Run()
         {
            Debug.Assert(SynchronizationContext.Current == null);
            SynchronizationContext.SetSynchronizationContext(m_Context);

            while(EndLoop == false)
            {
               WorkItem workItem = m_Context.GetNext();
               if(workItem != null)
               {
                  workItem.CallBack();
               }
            }
         }
         public void Kill()
         {
            //Kill is called on client thread - must use cached thread object
            Debug.Assert(m_ThreadObj != null);
            if(m_ThreadObj.IsAlive == false)
            {
               return;
            }
            EndLoop = true;

            //Wait for thread to die
            m_ThreadObj.Join();
         }
      }

      WorkerThread[] m_WorkerThreads;
      Queue<WorkItem> m_WorkItemQueue;
      Semaphore m_ItemAdded;

      protected Semaphore ItemAdded
      {
         get
         {
            return m_ItemAdded;
         }
         set
         {
            m_ItemAdded = value;
         }
      }

      public ThreadPoolSynchronizer(uint poolSize) : this(poolSize,"Pooled Thread: ")
      {}

      public ThreadPoolSynchronizer(uint poolSize,string poolName)
      {
         if(poolSize == 0)
         {
            throw new InvalidOperationException("Pool size cannot be zero");
         }
         m_ItemAdded = new Semaphore(0,Int32.MaxValue);
         m_WorkItemQueue = new Queue<WorkItem>();

         m_WorkerThreads = new WorkerThread[poolSize];
         for(int index = 0;index<poolSize;index++)
         {
            m_WorkerThreads[index] = new WorkerThread(poolName + " " + (index+1),this);
         }
      }
      virtual internal void QueueWorkItem(WorkItem workItem)
      {
         lock(m_WorkItemQueue)
         {
            m_WorkItemQueue.Enqueue(workItem);
            ItemAdded.Release();
         }
      }
      protected virtual bool QueueEmpty
      {
         get
         {
            lock(m_WorkItemQueue)
            {
               if(m_WorkItemQueue.Count > 0)
               {
                  return false;
               }
               return true;
            }
         }
      }
      internal virtual WorkItem GetNext()
      {
         ItemAdded.WaitOne();
         lock(m_WorkItemQueue)
         {
            if(m_WorkItemQueue.Count == 0)
            {
               return null;
            }
            return m_WorkItemQueue.Dequeue();
         }
      }
      public void Dispose()
      {
         Close();
      }
      public override SynchronizationContext CreateCopy()
      {
         return this;
      }
      public override void Post(SendOrPostCallback method,object state)
      {
         WorkItem workItem = new WorkItem(method,state);
         QueueWorkItem(workItem);
      }
      public override void Send(SendOrPostCallback method,object state)
      {
         //If already on the correct context, must invoke now to avoid deadlock
         if(SynchronizationContext.Current == this)
         {
            method(state);
            return;
         }
         WorkItem workItem = new WorkItem(method,state);
         QueueWorkItem(workItem);
         workItem.AsyncWaitHandle.WaitOne();
      }
      public void Close()
      {
         if(ItemAdded.SafeWaitHandle.IsClosed)
         {
            return;
         }
         ItemAdded.Release(Int32.MaxValue);

         foreach(WorkerThread thread in m_WorkerThreads)
         {
            thread.Kill();
         }
         ItemAdded.Close();
      }
      public void Abort()
      {
         ItemAdded.Release(Int32.MaxValue);

         foreach(WorkerThread thread in m_WorkerThreads)
         {
            thread.m_ThreadObj.Abort();
         }
         ItemAdded.Close();
      }
   }
}