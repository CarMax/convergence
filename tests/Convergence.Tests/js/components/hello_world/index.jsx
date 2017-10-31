import React from 'react';
import PropTypes from 'prop-types';

const HelloWorld = ({ message }) => (
    <div>
        {message}
    </div>
);

HelloWorld.propTypes = {
    message: PropTypes.string,
};

HelloWorld.defaultProps = {
    message: 'Hello World!',
};

export default HelloWorld;