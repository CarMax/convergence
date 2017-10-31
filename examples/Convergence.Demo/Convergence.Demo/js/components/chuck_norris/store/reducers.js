import types from './actions.types';

export default (state = {}, action) => {
    switch (action.type) {
        case types.API_ERROR:
            return { ...state, ...{ error: action.error } };
        case types.SET_JOKE:
            return { ...state, ...{ text: action.text } };
        default:
            return state;
    }
}