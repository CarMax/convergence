import { combineReducers, createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import jokes from './reducers';

const root = combineReducers({
    jokes
});

export const create = (state) => createStore(
    root,
    state,
    applyMiddleware(thunk)
);
