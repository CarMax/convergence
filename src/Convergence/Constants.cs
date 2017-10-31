namespace Convergence
{
    public static class Constants
    {
        public const string CONVERGENCE_EDGE_FUNCTION = @"
                const vm = require('vm');

                const sandbox = {
                    require: require,
                    console: console,
                    process: process,
                    Buffer: Buffer,
                    clearTimeout: clearTimeout,
                    setTimeout: setTimeout,
                    global: {}
                };

                const context = new vm.createContext(sandbox);

                return function (data, callback) {
                    try {
                        let response = null;
                        if (data) {
                            // run script files
                            if (data.script) {
                                context.edgeDone = callback;
                                // work around for promises
                                setImmediate(() => { });
                                const script = new vm.Script(data.script.text, { filename: data.script.filename, displayErrors: true });
                                var result = script.runInContext(context);

                                if (data.script.isQuery) {
                                    response = result;
                                }

                                if (!data.script.async) {
                                    callback(null, response);
                                }
                            } else if (data.cleanup) {
                                delete context;
                                delete sandbox;
                                delete vm;
                                callback();
                            } else {
                                throw new Error('Bad or unhandled data passed to edge.js');
                            }
                        } else {
                            throw new Error('No data passed to edge.js');
                        }

                    } catch (ex) {
                        callback(ex, null);
                    }
                }
            ";
    }
}
