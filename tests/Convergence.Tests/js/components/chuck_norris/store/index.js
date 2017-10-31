import { combineReducers, createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import jokes from './reducers';

const root = combineReducers({
    jokes
});

export const create = () => createStore(
    root,
    applyMiddleware(thunk)
);
