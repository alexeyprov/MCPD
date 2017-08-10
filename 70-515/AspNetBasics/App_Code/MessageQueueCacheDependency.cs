using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Summary description for MessageQueueCacheDependency
/// </summary>
public class MessageQueueCacheDependency : CacheDependency
{
	private string _messageName;
	private MessageQueue _queue;
	//private IAsyncResult _peekResult;
	//private IAsyncResult _receiveResult;

	public MessageQueueCacheDependency(string queueName, string messageName)
	{
		_messageName = messageName;
		_queue = CreateOrOpenQueue(queueName);

		// Set the formatter to indicate body contains a string.
		_queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

		// Start peeking messages
		_queue.BeginPeek(MessageQueue.InfiniteTimeout, _messageName, _queue_PeekCompleted);
	}

	private void _queue_PeekCompleted(IAsyncResult result)
	{
		if (result != null && Object.ReferenceEquals(result.AsyncState, _messageName))
		{
			Message m = _queue.EndPeek(result);
			if (m.Body.ToString() == _messageName)
			{
				_queue.Receive();
				NotifyDependencyChanged(this, EventArgs.Empty);
			}
			else
			{
				_queue.BeginPeek(MessageQueue.InfiniteTimeout, _messageName, _queue_PeekCompleted);
			}
		}
	}

	public static MessageQueue CreateOrOpenQueue(string queueName)
	{
		if (MessageQueue.Exists(queueName))
		{
			return new MessageQueue(queueName);
		}

		return MessageQueue.Create(queueName);
	}

	protected override void DependencyDispose()
	{
		if (_queue != null)
		{
			_queue.Close();
			_queue = null;
		}

		base.DependencyDispose();
	}

}
