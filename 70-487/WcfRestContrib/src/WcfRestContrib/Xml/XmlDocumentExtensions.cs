﻿using System.Xml;

namespace WcfRestContrib.Xml
{
    public static class XmlDocumentExtensions
    {
        public static void SortAlphabetically<T>(this XmlDocument document) where T : XmlNode
        {
            document.DocumentElement.SortAlphabetically<T>(true);
        }
    }
}
