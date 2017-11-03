# Convergence

A fusion of .NET and Node.js that support script evaluation and rendering universal react components.

The core Convergence package is library agnostic and can be easily extended to work with Angular, Vue, etc.

The React Convergence package extends the core library with React specific environments and helpers.

Convergence uses the [edge](https://github.com/tjanczuk/edge) open source project as its script engine and is optimized for performance.

## Installation

Core

`install-package Convergence`

React for Mvc (.NET Framework)

`install-package Convergence.React`

## Usage

Render Universal React Components

```csharp
// Index.cshtml

@{
    // Enable React Hydration 
    RequestFlags.SetStateFlag();
    // Prepare Redux State
    var state = @Html.WaitScript("Components.Name.Init(parms, edgeDone);", "init");
}

@Html.ReactComponentWithJsonProps("Components.Name.App", state.ToString())
```

Hydrate all of your components when after the DOM has loaded

```csharp
// _Layout.cshtml

...
    @if(RequestFlags.GetStateFlag())
    {
        @Html.ReactInitializeComponents()
    }
</body>
</html>
```

## Getting Started

Convergence isolates its runtime environment by request via Dependency Injection.

The first step is to configure your node environment and register it with your container:

```csharp
// create a custom node environment
var nodeConfiguration = new NodeConfiguration();

nodeConfiguration
    // your dependencies and components
    .AddScript(HostingEnvironment.MapPath("~/app.js")) 
    // bootstrap globals to locals
    .AddScript(React.Constants.CONVERGENCE_REACT_GLOBALS_TO_LOCALS_SCRIPT,
        React.Constants.CONVERGENCE_REACT_GLOBALS_TO_LOCALS_FILE_NAME);

container.RegisterInstance<INodeConfiguration>(nodeConfiguration);
```

Then register the remaining dependencies...

```csharp
container.RegisterType<INodeEnvironmentPool, NodeEnvironmentPool>();
container.RegisterType<IUniversalReactEnvironment, UniversalReactEnvironment>(new PerRequestLifetimeManager()); 
```

Now you can access the node environment from your controllers...

```csharp
public class HomeController : Controller
{
    private readonly IUniversalReactEnvironment _environment;
    public HomeController(IUniversalReactEnvironment environment)
    {
        _environment = environment;
    }

    public ActionResult Index()
    {
        // do some math
        var result = await _environment.RunAsync<string>("'2 + 2 = ' + (2 + 2)", "math");
        return View(result);
    }
}
```

Or from your views with our MVC Helpers...

```csharp
<div>@Html.RunScript("'5 + 2 = ' + (5 + 2)", "math")</div>
```

> For more details checkout the [examples folder](examples/).

## Version

* Convergence 1.0.0
* Convergence.React 1.0.0

## License

MIT
