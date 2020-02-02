import React from 'react';

const Button = (props) => {
    return (
        <button 
            type={props.ButtonType} 
            disabled={props.IsDisabled}
            className={props.ButtonClassName}
            >
                {props.ButtonTitle}
            </button>
    );
};

export default Button;