import React from 'react';

const FormHeader = (props) => {
    return (
        <div className="d-flex flex-row pt-2">
            <h5>{props.HeaderName}</h5>
        </div>
    );
};

export default FormHeader;
