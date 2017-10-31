using System;
using System.Threading.Tasks;

namespace Convergence
{
    public interface INodeEnvironment
    {
        Task RunAsync(string script, string filename);
        Task<T> RunAsync<T>(string script, string filename);
        Task WaitAsync(string script, string filename);
        Task<T> WaitAsync<T>(string script, string filename);
    }

    public class NodeEnvironment : INodeEnvironment, IDisposable
    {
        private Func<object, Task<object>> _env;
        private bool isDisposed = false;

        public NodeEnvironment(INodeConfiguration nodeConfiguration)
        {
            _env = EdgeJs.Edge.Func(nodeConfiguration.GetEdgeFunction());

            foreach (var script in nodeConfiguration.GetScripts())
            {
                Async.RunSync(() => RunAsync(script.Value, script.Key));
            }
        }

        public async Task RunAsync(string script, string filename)
        {
            await _env(new { script = new { text = script, filename = filename } });
        }

        public async Task<T> RunAsync<T>(string script, string filename)
        {
            var result = await _env(new { script = new { text = script, filename = filename, isQuery = true } });
            return result != null ? (T)result : default(T);
        }

        public Task WaitAsync(string script, string filename)
        {
            return _env(new { script = new { text = script, filename = filename, async = true } });
        }

        public async Task<T> WaitAsync<T>(string script, string filename)
        {
            var result = await _env(new { script = new { text = script, filename = filename, isQuery = true, async = true } });
            return result != null ? (T)result : default(T);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _env(new { cleanup = true }).RunSynchronously();
                    _env = null;
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
