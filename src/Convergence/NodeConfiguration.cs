using System.Collections.Generic;
using System.IO;

namespace Convergence
{
    public interface INodeConfiguration
    {
        INodeConfiguration EdgeFunction(string function);        
        INodeConfiguration AddScript(string path);
        INodeConfiguration AddScript(string script, string path);
        string GetEdgeFunction();
        IDictionary<string, string> GetScripts();
    }

    public class NodeConfiguration : INodeConfiguration
    {
        private readonly Dictionary<string, string> _scripts;
        private readonly IList<string> _files;
        private string _edgeFunction = Constants.CONVERGENCE_EDGE_FUNCTION;

        public NodeConfiguration()
        {
            _scripts = new Dictionary<string, string>();
            _files = new List<string>();
        }

        public INodeConfiguration EdgeFunction(string function)
        {
            _edgeFunction = function;

            return this;
        }

        public INodeConfiguration AddScript(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found at path {path}");
            }

            _files.Add(path);
            CacheScript(path);

            return this;
        }

        public INodeConfiguration AddScript(string script, string path)
        {
            if (!_scripts.ContainsKey(path))
            {
                _scripts.Add(path, script);
            }
            else
            {
                _scripts[path] = script;
            }

            return this;
        }

        public string GetEdgeFunction()
        {
            return _edgeFunction;
        }

        public IDictionary<string, string> GetScripts()
        {
            return _scripts;
        }

        private void CacheScript(string path)
        {
            var script = File.ReadAllText(path);

            if (!_scripts.ContainsKey(path))
            {
                _scripts.Add(path, script);
            }
            else
            {
                _scripts[path] = script;
            }
        }
    }
}
