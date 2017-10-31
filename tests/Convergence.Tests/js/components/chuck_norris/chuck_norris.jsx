import React from 'react';
import PropTypes from 'prop-types';

const ChuckNorris = ({joke, error}) => {
   if(error) {
       return (
           <div>An Error Occurred [{error}]</div>
       );
   }

   if(joke) {
    return (
           <div>Joke: {joke}</div>
       );
   }

   return null;
};

ChuckNorris.propTypes = {
    jokeId: PropTypes.number,
    error: PropTypes.string,
};

ChuckNorris.defaultProps = {
    jokeId: null,
    error: null,
};

export default ChuckNorris;
