import path from 'path';
import webpack from 'webpack';

const base = {
    target: 'node',
    entry: {
        app: './js/index.js'
    },
    output: {
        path: path.resolve('dist'),
        filename: '[name].js'
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

export default base;
