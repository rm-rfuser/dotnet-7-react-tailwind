import React from 'react';
import PropTypes from 'prop-types';

const propTypes = {
    text: PropTypes.string,
    to: PropTypes.string
};

function SubmitButton({ text }) {
    return (
        <button type="submit" className="text-white bg-gray-800 hover:bg-gray-900 focus:outline-none focus:ring-4 focus:ring-gray-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-gray-800 dark:hover:bg-gray-700 dark:focus:ring-gray-700 dark:border-gray-700">
            {text}
        </button>
    );
}

SubmitButton.propTypes = propTypes;
export { SubmitButton };