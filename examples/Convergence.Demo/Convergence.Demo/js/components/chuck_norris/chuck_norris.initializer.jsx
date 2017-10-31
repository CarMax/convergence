import { create } from './store';
import { getJoke } from './store/actions';
import ChuckNorrisApp from './chuck_norris.app';

const store = create();

export default (id, callback) => {
    store.dispatch(getJoke(id))
        .then(
            () => callback(null, JSON.stringify({ state: store.getState() })),
            error => callback(error)
        );
};