using System;
using System.ServiceModel;

namespace TaskClient
{
    public static class CommunicationObjectExtensions
    {
        private sealed class DisposableWrapper : IDisposable
        {
            private readonly ICommunicationObject _object;

            public DisposableWrapper(ICommunicationObject @object)
            {
                _object = @object;
            }

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                if (_object.State == CommunicationState.Faulted)
                {
                    _object.Abort();
                }
                else
                {
                    _object.Close();
                }
            }

            #endregion
        }

        public static IDisposable AsDisposable(this ICommunicationObject @object)
        {
            return new DisposableWrapper(@object);
        }
    }
}
