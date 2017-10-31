import { create } from './store';
import { getJoke } from './store/actions';
import ChuckNorrisApp from './chuck_norris.app';

const store = create();

export const render = (id, callback) => {
    store.dispatch(getJoke(id))
        .then(
            () => {
                const dom = ReactDOMServer.renderToString(<ChuckNorrisApp store={store} />);
                callback(null, dom);
            },
            error => callback(error)
        );
};

export const getState = () => JSON.stringify(store.getState());