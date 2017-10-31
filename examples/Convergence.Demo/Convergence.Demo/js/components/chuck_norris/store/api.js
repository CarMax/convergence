import axios from 'axios';
import proxy from 'https-proxy-agent';

let httpsAgent = null;

if (process && process.env && process.env.http_tunnel_proxy) {
    httpsAgent = new proxy(process.env.http_tunnel_proxy);
}

var api = axios.create({
    baseURL: 'https://api.icndb.com',
    httpsAgent
});

export default {
    getJoke: (id) => api.get(`/jokes/${id}`),
    getRandomJoke: () => api.get('/jokes/random?limitTo=[nerdy]'),
};