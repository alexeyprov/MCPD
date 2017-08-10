//2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Runtime.Serialization;
using System.Xml;
using MyNamespace;


class CustomerResolver : DataContractResolver
{
   string Namespace
   {
      get
      {
         return typeof(Customer).Namespace ?? "global";
      }   
   }

   string Name
   {
      get
      {
         return typeof(Customer).Name;
      }   
   }
   public override Type ResolveName(string typeName,string typeNamespace,Type declaredType,DataContractResolver knownTypeResolver)
   {
      if(typeName == Name && typeNamespace == Namespace)
      {
         return typeof(Customer);
      }
      else
      {
         return knownTypeResolver.ResolveName(typeName,typeNamespace,declaredType,null);
      }
   }
   public override bool TryResolveType(Type type,Type declaredType,DataContractResolver knownTypeResolver,out XmlDictionaryString typeName,out XmlDictionaryString typeNamespace)
   {
      if(type == typeof(Customer))
      {
         XmlDictionary dictionary = new XmlDictionary();
         typeName      = dictionary.Add(Name);
         typeNamespace = dictionary.Add(Namespace);

         return true;
      }
      else
      {
         return knownTypeResolver.TryResolveType(type,declaredType,null,out typeName,out typeNamespace);
      }
   }
}
