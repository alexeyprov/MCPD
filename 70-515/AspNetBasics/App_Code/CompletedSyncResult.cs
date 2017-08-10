using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

/// <summary>
/// Helper class to signal that an asynchronous operation was completed synchronously
/// </summary>
public class CompletedSyncResult<T> : IAsyncResult
{
	#region Private Fields

	Exception _exception;
	object _state;
	T _result;

	#endregion

	#region Constructor

	public CompletedSyncResult(Exception ex, object state, AsyncCallback callback)
	{
		_exception = ex;
		_state = state;

		if (callback != null)
		{
			callback(this);
		}
	}

	public CompletedSyncResult(T result, object state, AsyncCallback callback)
	{
		_result = result;
		_state = state;

		if (callback != null)
		{
			callback(this);
		}
	}
	
	#endregion

	#region Properties

	public Exception Exception
	{
		get
		{
			return _exception;
		}
	}

	public T Result
	{
		get
		{
			if (_exception != null)
			{
				throw _exception;
			}
			return _result;
		}
	}

	#endregion

	#region IAsyncResult Members

	public object AsyncState
	{
		get
		{
			return _state;
		}
	}

	public WaitHandle AsyncWaitHandle
	{
		get
		{
			return null;
		}
	}

	public bool CompletedSynchronously
	{
		get
		{
			return true;
		}
	}

	public bool IsCompleted
	{
		get
		{
			return true;
		}
	}

	#endregion
}
