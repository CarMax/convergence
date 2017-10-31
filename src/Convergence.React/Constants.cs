namespace Convergence.React
{
    public static class Constants
    {
        public const string CONVERGENCE_REACT_GLOBALS_TO_LOCALS_SCRIPT = @"
            var React = global.React;
            var ReactDOM = global.ReactDOM;
            var ReactDOMServer = global.ReactDOMServer;
            var Components = global.Components;
        ";

        public const string CONVERGENCE_REACT_GLOBALS_TO_LOCALS_FILE_NAME = "globals-to-locals.js";
    }
}
