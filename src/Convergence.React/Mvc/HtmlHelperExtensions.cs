using System;
using System.Web;
using System.Web.Mvc;

namespace Convergence.React.Mvc
{
    public static class HtmlHelperExtensions
    {
        private static IUniversalReactEnvironment Environment
        {
            get
            {
                try
                {
                    return DependencyResolver.Current.GetService<IUniversalReactEnvironment>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to retrieve a scoped node environment", ex);
                }
            }
        }

        public static IHtmlString ReactComponent(this HtmlHelper htmlHelper, string component)
        {
            return new HtmlString(Async.RunSync(() => Environment.RenderReactComponentAsync(component)));
        }

        public static IHtmlString ReactComponentWithProps<T>(this HtmlHelper htmlHelper, string component, T props)
        {
            return new HtmlString(Async.RunSync(() => Environment.RenderReactComponentWithPropsAsync(component, props)));
        }

        public static IHtmlString ReactComponentWithJsonProps(this HtmlHelper htmlHelper, string component, string props)
        {
            return new HtmlString(Async.RunSync(() => Environment.RenderReactComponentWithJsonPropsAsync(component, props)));
        }

        public static IHtmlString RunScript(this HtmlHelper htmlHelper, string script, string name)
        {
            return new HtmlString(Async.RunSync(() => Environment.RunAsync<string>(script, name)));
        }

        public static IHtmlString WaitScript(this HtmlHelper htmlHelper, string script, string name)
        {
            return new HtmlString(Async.RunSync(() => Environment.WaitAsync<string>(script, name)));
        }

        public static IHtmlString ReactInitializeComponents(this HtmlHelper htmlHelper)
        {
            var tag = new TagBuilder("script")
            {
                InnerHtml = Environment.GetInitializerScript()
            };

            return new HtmlString(tag.ToString());
        }
    }
}
