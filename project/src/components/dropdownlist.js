import React from 'react';
import Error from './inputError';

const DropDownList = (props) => {
    const data = props.Data.map((options) => 
        <option key={options.Id} value = { options.Id }> {options.Name}</option>
    );

    return (
        <div className="form-group flex-fill">
            <label className="form-label col-form-label col-form-label-sm pt-1 pb-0">{ props.DDLTitle }</label>
            <select 
                className="form-control form-control-sm"
                onChange={props.DDLChange} 
                value={props.DDLValue}
                onBlur= {props.HandleBluer}
                id = {props.DDLTitle}
                name = {props.DDLTitle}
            >
                { data }
            </select>
            <Error touched={props.Touched} message={props.Error}></Error>
        </div>
    );
};


export default DropDownList; 