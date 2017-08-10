using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace BusinessEntities
{
	public sealed class ObjectFactory
	{
		/// <summary>
		/// Loads list of BookInfo from persistent storage (embedded resource)
		/// </summary>
		/// <returns>Should be List to support addition/deletion</returns>
		public static List<BookInfo> GetListOfBooks()
		{
			// Load books array
			XmlSerializer ser = new XmlSerializer(typeof(BookInfo[]), new XmlRootAttribute("BookInfos"));
			using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream("BusinessEntities.Books.xml"))
			{
				BookInfo[] ar = (BookInfo[])ser.Deserialize(stm);
				return new List<BookInfo>(ar);
			}
		}
	}
}
