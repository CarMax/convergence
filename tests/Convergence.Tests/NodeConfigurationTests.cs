using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Convergence.Tests
{
    [TestClass]
    public class NodeConfigurationTests
    {
        private INodeConfiguration _configuration;

        [TestInitialize]
        public void Init()
        {
            _configuration = new NodeConfiguration();
        }

        [TestMethod]
        public void CanGetDefaultEdgeFunction()
        {
            Assert.AreEqual(_configuration.GetEdgeFunction(), Constants.CONVERGENCE_EDGE_FUNCTION);
        }

        [TestMethod]
        public void CanOverrideDefaultEdgeFunction()
        {
            var overrideFunc = "Hello World!";
            _configuration.EdgeFunction(overrideFunc);
            Assert.AreEqual(_configuration.GetEdgeFunction(), overrideFunc);
        }

        [TestMethod]
        public void CanReadFile()
        {
            var path = "./test.js";
            var content = "console.log('hello world!'";

            using (var writter = File.CreateText(path))
            {
                writter.WriteAsync(content);
            }

            _configuration.AddScript(path);

            var scripts = _configuration.GetScripts();

            File.Delete(path);

            Assert.AreEqual(1, scripts.Count);
            Assert.IsTrue(scripts.ContainsKey(path));
            Assert.AreEqual(content, scripts[path]);
        }

        [TestMethod]
        public void CanAcceptString()
        {
            var path = "./test.js";
            var content = "console.log('hello world!'";

            _configuration.AddScript(content, path);

            var scripts = _configuration.GetScripts();

            Assert.AreEqual(1, scripts.Count);
            Assert.IsTrue(scripts.ContainsKey(path));
            Assert.AreEqual(content, scripts[path]);
        }

        [TestMethod]
        public void CanOverrideScript()
        {
            var path = "./test.js";
            var content = "console.log('hello world!'";

            _configuration
                .AddScript(content, path)
                .AddScript(content, path);

            var scripts = _configuration.GetScripts();

            Assert.AreEqual(1, scripts.Count);
            Assert.IsTrue(scripts.ContainsKey(path));
            Assert.AreEqual(content, scripts[path]);
        }
    }
}
