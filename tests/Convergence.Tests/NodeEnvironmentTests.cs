using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Convergence.Tests
{
    [TestClass]
    public class NodeEnvironmentTests
    {
        private INodeEnvironment _environment;

        [TestMethod]
        public async Task CanRunAScript()
        {
            _environment = new NodeEnvironment(new NodeConfiguration());

            var result = await _environment.RunAsync<int>("3 + 5", "math");

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public async Task CanHandlePromises()
        {
            _environment = new NodeEnvironment(new NodeConfiguration());

            var watch = new Stopwatch();
            watch.Start();

            var result = await _environment.WaitAsync<int>(@"
                setTimeout(() => {
                  edgeDone(null, 5 + 3);
                }, 500);
            ", "math");

            watch.Stop();

            Assert.AreEqual(8, result);
            Assert.IsTrue(watch.Elapsed.TotalSeconds > 0 && watch.Elapsed.TotalSeconds < 1);
        }


        [TestMethod]
        public async Task CanLoadWebpackBundle()
        {
            var configuration = new NodeConfiguration();
            configuration.AddScript("./dist/app.js");

            _environment = new NodeEnvironment(configuration);

            var result = await _environment.RunAsync<bool>("!!global.React && !!global.Components", "verification");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CanProvideExecutionIsolation()
        {
            var configuration = new NodeConfiguration();
            _environment = new NodeEnvironment(configuration);

            await _environment.RunAsync("global.blah = 8;", "verification");
            var result = await _environment.RunAsync<int?>("global.blah", "verification");

            Assert.AreEqual(8, result);

            var newEnvironent = new NodeEnvironment(configuration);

            var check = await newEnvironent.RunAsync<int?>("global.blah", "verification");

            Assert.IsNull(check);
        }
    }
}
