import api from './api';
import types from './actions.types';

const setJoke = text => ({type: types.SET_JOKE, text});

const apiError = error => ({type: types.API_ERROR, error});

export const getJoke = (id) => (dispatch, getState) => {
    const fetch = !!id ? api.getJoke(id) : api.getRandomJoke();
    
    return fetch.then(
        response => dispatch(setJoke(response.data.value.joke)),
        error => dispatch(apiError(error.message)),
    );
}