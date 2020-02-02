import React from 'react';

const Error = ({touched, message}) => {
    return <small className={ message && touched ? "form-alert" : "form-alert hide-element"}>{message}</small>;
};

export default Error;