import React from 'react';
import PropTypes from 'prop-types';
import { Provider } from 'react-redux'
import { create } from './store';
import ChuckNorrisContainer from './chuck_norris.container';

const ChuckNorrisApp = ({ store }) => {
    const local = store || create();

    return (
        <Provider store={local} >
            <ChuckNorrisContainer />
        </Provider>
    );
};

ChuckNorrisApp.propTypes = {
    store: PropTypes.object
};

ChuckNorrisApp.defaultProps =  {
    store: null
};

export default ChuckNorrisApp;