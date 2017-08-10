//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Diagnostics;

class Program
{
   static void Main(string[] args)
   {
      MyContractClient proxy = new MyContractClient();

      Console.WriteLine("Start chattting with services");
           
      Console.WriteLine();

      Console.WriteLine("Type 'r' for whole rows or 'c' for individual characters");
          
      Console.WriteLine();
  
      char character = Console.ReadKey().KeyChar;

      Console.WriteLine();

      Console.WriteLine("Type '^' (Shift+6) to end chat");
            
      Console.WriteLine();

      if(character == 'c')
      {
         while(character != '^')
         {
            Console.ForegroundColor = (ConsoleColor)(Convert.ToInt32(character) % 16);
            character = Console.ReadKey().KeyChar;
            proxy.OnCharacter(character);
         }
      }
      else
      {
         Debug.Assert(character == 'r');

         string line = "";
         while(line.EndsWith("^") == false)
         {
            line = Console.ReadLine();
            proxy.OnText(line);
         }
      }
      Console.WriteLine("Chat ends");

      proxy.Close();
   }
}
