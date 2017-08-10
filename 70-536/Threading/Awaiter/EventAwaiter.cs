using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.CompilerServices;

public sealed class EventAwaiter<TEventArgs> : INotifyCompletion
{
	private ConcurrentQueue<TEventArgs> _events;
	private Action _continuation;

	public EventAwaiter()
	{
		_events = new ConcurrentQueue<TEventArgs>();
	}

	public EventAwaiter<TEventArgs> GetAwaiter()
	{
		return this;
	}

	public TEventArgs GetResult()
	{
		TEventArgs e;
		_events.TryDequeue(out e);
		return e;
	}

	public bool IsCompleted
	{
		get
		{
			return _events.Count > 0;
		}
	}

	void INotifyCompletion.OnCompleted(Action continuation)
	{
		Volatile.Write(ref _continuation, continuation);
	}

	public void HandleEvent(object sender, TEventArgs e)
	{
		_events.Enqueue(e);
		Action continuation = Interlocked.Exchange(ref _continuation, null);

		if (continuation != null)
		{
			continuation();
		}
	}
}