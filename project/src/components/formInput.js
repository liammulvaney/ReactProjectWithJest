import React from 'react';
import Error from './inputError';


const FormInput = (props) => {
    return(
        <div className={props.AddMargin ? "form-group flex-fill mr-2 mb-1" : "form-group flex-fill mb-1"}>
            <label htmlFor={props.HtmlForLabel} className="form-label col-form-label col-form-label-sm pt-1 pb-0"> {props.LabelName}</label>
            <input
                type= {props.InputType}
                name= {props.HtmlForLabel} 
                id= {props.HtmlForLabel}
                onChange= {props.HandleChange}
                onBlur = {props.HandleBlur}
                value = {props.InputValue}
                className={props.Touched && props.Error? "form-control form-control-sm": "form-control form-control-sm"}
            />
            <Error touched={props.Touched} message={props.Error}></Error>
        </div>
    );
}
  

export default FormInput;