import path from 'path';
import webpack from 'webpack';

const node = {
    target: 'node',
    entry: {
        app: './js/index.js'
    },
    output: {
        path: path.resolve('dist'),
        filename: '[name].node.js'
    },
    module: {
        rules: [
            { test: /\.jsx?$/, loader: 'babel-loader', exclude: /node_modules/ }
        ]
    },
    resolve: {
        extensions: ['.js', '.json', '.jsx']
    },
    plugins: [
        new webpack.IgnorePlugin(/vertx/)
    ]
};


const web = {
    target: 'web',
    entry: {
        app: './js/index.js'
    },
    output: {
        path: path.resolve('dist'),
        filename: '[name].web.js'
    },
    module: {
        rules: [
            { test: /\.jsx?$/, loader: 'babel-loader', exclude: /node_modules/ }
        ]
    },
    resolve: {
        extensions: ['.js', '.json', '.jsx']
    },
    plugins: [
        new webpack.NormalModuleReplacementPlugin(/https-proxy-agent/,
            path.resolve('js/mock-https-proxy-agent.js'))
    ]
};

export default [node, web];
