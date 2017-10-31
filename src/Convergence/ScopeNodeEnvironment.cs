using System;
using System.Threading.Tasks;

namespace Convergence
{
    public class ScopeNodeEnvironment : INodeEnvironment, IDisposable
    {
        private readonly INodeEnvironmentPool _nodeEnvironmentPool;
        private readonly NodeEnvironment _nodeEnvironment;
        private bool isDisposed = false;

        public ScopeNodeEnvironment(INodeEnvironmentPool nodeEnvironmentPool)
        {
            _nodeEnvironmentPool = nodeEnvironmentPool;
            _nodeEnvironment = nodeEnvironmentPool.Get();
        }

        public Task RunAsync(string script, string filename = "inline-script.js")
        {
            return _nodeEnvironment.RunAsync(script, filename);
        }

        public Task<T> RunAsync<T>(string script, string filename = "inline-script.js")
        {
            return _nodeEnvironment.RunAsync<T>(script, filename);
        }

        public Task WaitAsync(string script, string filename = "async-inline-script.js")
        {
            return _nodeEnvironment.WaitAsync(script, filename);
        }

        public Task<T> WaitAsync<T>(string script, string filename = "async-inline-script.js")
        {
            return _nodeEnvironment.WaitAsync<T>(script, filename);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _nodeEnvironmentPool.Return(_nodeEnvironment);
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
