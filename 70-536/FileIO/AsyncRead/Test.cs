// AsyncRead.Program
[DebuggerStepThrough, AsyncStateMachine(typeof(AsyncTestStateMachine))]
private static void AsyncTest()
{
    AsyncTestStateMachine machine = new AsyncTestStateMachine
    {
        _builder = AsyncVoidMethodBuilder.Create(),
        _state = -1
    }

    machine._builder.Start<AsyncTestStateMachine>(ref machine);
}

[CompilerGenerated]
private sealed class AsyncTestStateMachine : IAsyncStateMachine
{
    public int _state;
    public AsyncVoidMethodBuilder _builder;
    private FileStream _fs;
    private byte[] _data;
    private int _bytesRead;
    private int _result;
    private TaskAwaiter<int> _awaiter;

    void IAsyncStateMachine.MoveNext()
    {
        int num = _state;
        try
        {
            if (num != 0)
            {
                _fs = File.OpenRead("AsyncRead.exe");
            }
            try
            {
                TaskAwaiter<int> taskAwaiter;
                if (num != 0)
                {
                    _data = new byte[100];
                    taskAwaiter = _fs.ReadAsync(_data, 0, _data.Length).GetAwaiter();
                    if (!taskAwaiter.IsCompleted)
                    {
                        num = (_state = 0);
                        _awaiter = taskAwaiter;
                        AsyncTestStateMachine machine = this;
                        _builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, AsyncTestStateMachine>(ref taskAwaiter, ref machine);
                        return;
                    }
                }
                else
                {
                    taskAwaiter = _awaiter;
                    _awaiter = default(TaskAwaiter<int>);
                    num = (_state = -1);
                }
                int result = taskAwaiter.GetResult();
                taskAwaiter = default(TaskAwaiter<int>);
                _result = result;
                _bytesRead = _result;
                Program.DumpThread("Async");
                Program.PrintData(_data, _bytesRead);
                _data = null;
            }
            finally
            {
                if (num < 0 && _fs != null)
                {
                    ((IDisposable)_fs).Dispose();
                }
            }
            _fs = null;
        }
        catch (Exception exception)
        {
            _state = -2;
            _builder.SetException(exception);
            return;
        }
        _state = -2;
        _builder.SetResult();
    }

    [DebuggerHidden]
    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }
}
