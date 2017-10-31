using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Convergence.React
{
    public interface IUniversalReactEnvironment : INodeEnvironment
    {
        Task<string> RenderReactComponentAsync(string component);
        Task<string> RenderReactComponentWithPropsAsync<T>(string component, T props);
        Task<string> RenderReactComponentWithJsonPropsAsync(string component, string json);
        string GetInitializerScript();
    }

    public class UniversalReactEnvironment : ScopeNodeEnvironment, IUniversalReactEnvironment
    {
        private readonly IList<string> _componentInitializers;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private bool? _shouldUseRehydrate;

        public UniversalReactEnvironment(INodeEnvironmentPool nodeEnvironmentPool)
            : base(nodeEnvironmentPool)
        {
            _componentInitializers = new List<string>();

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };
        }

        public Task<string> RenderReactComponentAsync(string component)
        {
            return RenderReactComponentWithJsonPropsAsync(component, null);
        }

        public Task<string> RenderReactComponentWithPropsAsync<T>(string component, T props)
        {
            string propsJson = null;

            // serialize props if not null
            if (props != null)
            {
                propsJson = JsonConvert.SerializeObject(props, _jsonSerializerSettings);
            }

            return  RenderReactComponentWithJsonPropsAsync(component, propsJson);
        }

        public async Task<string> RenderReactComponentWithJsonPropsAsync(string component, string json)
        {
            // confirm component exists
            if (!await ComponentExists(component))
            {
                throw new ArgumentException($"Unable to find a component named {component}, make sure it is available in NodeConfiguration class via .AddScript()");
            }

            var componentInit = ComponentInitializer(component, json);

            var innerHtml = await RunAsync<string>($"ReactDOMServer.renderToString({componentInit})");

            var identifier = $"react_{Guid.NewGuid()}";

            if(await ShouldUseRehydrate())
            {
                _componentInitializers.Add($"ReactDOM.hydrate({componentInit}, document.getElementById('{identifier}'));");
            }
            else
            {
                _componentInitializers.Add($"ReactDOM.render({componentInit}, document.getElementById('{identifier}'));");
            }
            

            return string.Format($"<div id='{identifier}'>{innerHtml}</div>");
        }

        public string GetInitializerScript()
        {
            return _componentInitializers.Aggregate((a, b) => a + Environment.NewLine + b);
        }

        private string ComponentInitializer(string component, string props = null)
        {
            return string.IsNullOrWhiteSpace(props) ? $"React.createElement({component})" :
                $"React.createElement({component},{props})";
        }

        private Task<bool> ComponentExists(string component)
        {
            return RunAsync<bool>($"typeof {component} !== 'undefined'");
        }

        private async Task<bool> ShouldUseRehydrate()
        {
            if(!_shouldUseRehydrate.HasValue)
            {
                _shouldUseRehydrate = await RunAsync<bool>($"typeof ReactDOM.hydrate !== 'undefined'");
            }

            return _shouldUseRehydrate.Value;
        }
    }
}
