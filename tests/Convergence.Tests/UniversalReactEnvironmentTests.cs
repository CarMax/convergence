using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Convergence.React;
using System.Threading.Tasks;

namespace Convergence.Tests
{
    [TestClass]
    public class UniversalReactEnvironmentTests
    {
        private IUniversalReactEnvironment _environment;

        [TestInitialize]
        public void Init()
        {
            var configuration = new NodeConfiguration();

            configuration.AddScript("./dist/app.js");

            configuration.AddScript(
                React.Constants.CONVERGENCE_REACT_GLOBALS_TO_LOCALS_SCRIPT,
                React.Constants.CONVERGENCE_REACT_GLOBALS_TO_LOCALS_FILE_NAME);

            var pool = new NodeEnvironmentPool(configuration);

            _environment = new UniversalReactEnvironment(pool);
        }

        [TestMethod]
        public async Task CanRenderHelloWorld()
        {
            var result = await _environment.RenderReactComponentAsync("Components.HelloWorld");

            Assert.IsTrue(result.Contains("Hello World!"));
        }

        [TestMethod]
        public async Task CanRenderAsyncReduxComponents()
        {
            var html = await _environment.WaitAsync<string>("Components.ChuckNorris.Render(15, edgeDone);", "stuff");
            Assert.IsTrue(html.Contains("Chuck Norris"));

            var state = await _environment.RunAsync<string>("Components.ChuckNorris.GetState()", "more stuff");
            Assert.IsTrue(state.Contains("Chuck Norris"));
        }
    }
}
