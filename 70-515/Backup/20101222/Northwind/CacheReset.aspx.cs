using System;
using System.Collections;
using System.Messaging;
using System.Web.UI;


public partial class Northwind_CacheReset : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void btnReset_Click(object sender, EventArgs e)
	{
		MessageQueue queue = MessageQueueCacheDependency.CreateOrOpenQueue(ConstantsHelper.APPLICATION_MESSAGE_QUEUE);
		foreach (DictionaryEntry cacheEntry in Cache)
		{
			string cacheKey = cacheEntry.Key.ToString();
			if (cacheKey.StartsWith(ConstantsHelper.EMPLPOYEE_IMAGE_CACHE_KEY_PREFIX))
			{
				queue.Send(cacheKey);
			}
		}
	}

}
