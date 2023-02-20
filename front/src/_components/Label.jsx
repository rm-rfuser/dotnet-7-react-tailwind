import React from 'react';
import PropTypes from 'prop-types';

const propTypes = {
    text: PropTypes.string
};

function Label({ text }) {
    return (
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">{text}</label>
    );
}

Label.propTypes = propTypes;
export { Label };