//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Threading;
using System.ServiceModel.Persistence;

namespace ServiceModelEx
{
   public class TransactionalInstanceProvider : MemoryProvider
   {
      public TransactionalInstanceProvider(Guid id) : base(id,new TransactionalInstanceStore<Guid,object>())
      {}
   }
}
