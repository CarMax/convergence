import React from 'react';
import PropTypes from 'prop-types';

const ChuckNorris = ({ joke, error, getJoke }) => {
    if (error) {
        return (
            <div>An Error Occurred [{error}]</div>
        );
    }

    if (joke) {
        return (
            <div className="is-clearfix">
                <div className="subtitle is-4">"{joke}"</div>
                <a
                    className="button is-primary"
                    onClick={() => { getJoke(); }}
                >
                    Another!
                </a>
            </div>
        );
    }

    return null;
};

ChuckNorris.propTypes = {
    jokeId: PropTypes.number,
    error: PropTypes.string,
    getJoke: PropTypes.func.isRequired,
};

ChuckNorris.defaultProps = {
    jokeId: null,
    error: null,
};

export default ChuckNorris;
