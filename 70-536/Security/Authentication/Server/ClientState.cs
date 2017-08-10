using System.Net.Security;
using System.Text;
using System.Threading;

// ClientState is the AsyncState object.
internal class ClientState
{
	// Data Members
    private AuthenticatedStream _authStream;
    private byte[] _buffer = new byte[2048];
    private StringBuilder _message = new StringBuilder();
    AutoResetEvent _waiter = new AutoResetEvent(false);

	// Construction/Destruction
    internal ClientState(AuthenticatedStream a)
    {
        _authStream = a;
    }

	// Properties
    internal AuthenticatedStream AuthenticatedStream
    {
        get
		{
			return _authStream;
		}
    }

    internal byte[] Buffer
    {
        get
		{
			return _buffer;
		}
    }

    internal string Message
    {
        get 
        {
			return _message.ToString(); 
        }
	}

	// Operations
	internal void Wait()
	{
		_waiter.WaitOne();
	}
	
	internal void Signal()
	{
		_waiter.Set();
	}

	internal void FlushBuffer(int bytesRead)
	{
		char[] data = Encoding.UTF8.GetChars(_buffer, 0, bytesRead);
		_message.Append(data);
	}
}