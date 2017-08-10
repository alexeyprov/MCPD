//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;


[ServiceContract]
interface IMyChat
{
   [OperationContract(IsOneWay = true)]
   void OnCharacter(char character);

   [OperationContract(IsOneWay = true)]
   void OnText(string text);

}

class MyContractClient : ClientBase<IMyChat>,IMyChat
{
   public void OnCharacter(char character)
   {
      Channel.OnCharacter(character);
   }
   public void OnText(string text)
   {
      Channel.OnText(text);
   }
}
