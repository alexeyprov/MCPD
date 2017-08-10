using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace DynamicRouter
{
   internal class RoundRobinList<T> : IEnumerable<T>
   {
      private T[] _items;
      private int _head;

      public RoundRobinList(IEnumerable<T> endpoints)
      {
         if (endpoints == null || endpoints.Count<T>() == 0) { throw new ArgumentException("One or more items must be provided", "endpoints"); }

         //Copy the list to ensure it doesn't change on us (and so we can lock() on our private copy) 
         _items = endpoints.ToArray();
      }

      public IEnumerator<T> GetEnumerator()
      {
         int currentHead;
         lock (_items)
         {
            currentHead = _head++;

            if (_head == _items.Length)
            {
               //Wrap back to the start 
               _head = 0;
            }
         }

         //If one just wanted to return 1 endpoint as opposed to a list with 
         //backup endpoints this 'yield' is all that would be needed. 
         //yield return this.endpoints[currentHead]; 
         //return results [current] ... [last] 

         for (int i = currentHead; i < _items.Length; i++)
         {
            yield return _items[i];
         }

         //return wrap-around (if any) [0] ... [current-1] 
         for (int i = 0; i < currentHead; i++)
         {
            yield return _items[i];
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return this.GetEnumerator();
      }
   }

}