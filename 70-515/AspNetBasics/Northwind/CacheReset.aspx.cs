using System;
using System.Collections;
using System.Messaging;
using System.Web.UI;

namespace Northwind.UI
{
	public partial class CacheResetPage : Page
	{
		protected void btnReset_Click(object sender, EventArgs e)
		{
#if MSMQ_CONFIGURED
		MessageQueue queue = MessageQueueCacheDependency.CreateOrOpenQueue(ConstantsHelper.APPLICATION_MESSAGE_QUEUE);
		foreach (DictionaryEntry cacheEntry in Cache)
		{
			string cacheKey = cacheEntry.Key.ToString();
			if (cacheKey.StartsWith(ConstantsHelper.EMPLPOYEE_IMAGE_CACHE_KEY_PREFIX))
			{
				queue.Send(cacheKey);
			}
		}
#endif
		}

	}
	
}