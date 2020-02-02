import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import FormHeader from './components/formHeader';
import FormInput from './components/formInput';
import Button from './components/button';
import DropDownList from './components/dropdownlist';

/*
validates the fields.
 */
const validationSchema = Yup.object().shape({
    Name: Yup.string()
             .min(1, '*Name is too short!')
             .max(255, '*Name is too long!')
             .required('*An employee name is required!'),
    Surname: Yup.string()
             .min(1, '*Surname is too short!')
             .max(255, '*Surname is too Long!')
             .required('*An employee surname is required!'),
    IDNumber: Yup.string().notRequired(),
    ContactNumber1 : Yup.string().required('*An employee contact number is required!'),
    ContactNumber2: Yup.string().notRequired(),
    Gender: Yup.string().required('*Gender is required!'),
    DateOfBirth: Yup.string() // Yup.date() has a silly bug at the moment, so I opted to use string
             .required('*Date of birth is required.'),
    Email: Yup.string().email('*This email address is not valid.')
             .max(255, '*This email address is too long!')
             .required('*An email address is required!'),
    AddressLine1: Yup.string()
             .max(500, '*The first part of the address is too long!')
             .required('*The first part of an address is always required!'),
    AddressLine2: Yup.string()
             .max(500, '*The second part of the address is too long!')
             .required('*The second part of the address is required!'),
    Town: Yup.string()
             .max(255, '*The town name is too long!')
             .required('*An employee town is required!'),
    PostalCode: Yup.string()
             .max(255, '*The postal code is too long!')
             .required('*A Postal Code is required!'),
    Country: Yup.string()
             .max(255, '*Too long!')
             .required('*A Country is required!'),
  });
  
  const EmployeeForm = (props) =>
  {
    return(
      <Formik
        initialValues = {
          {
            Name: '', 
            Surname: '', 
            DateOfBirth: '', 
            Gender: '',
            IDNumber: '',  // not required
            Email: '',
            ContactNumber1: '',
            ContactNumber2: '', 
            AddressLine1 : '',
            AddressLine2 : '',
            Town: '',
            PostalCode : '',
            Country : '',
            DOB : ''
          }
        }
        validationSchema = {validationSchema}
        onSubmit ={(values, {setSubmitting, resetForm}) => {
          setSubmitting(true);
          setTimeout(() => {
            values.DOB = new Date(values.DateOfBirth).toJSON(); // for transfer purposes... yup.date does not work correctly.. will try to come up with a solution soon.
            props.onSubmit(values); // values conform to the employee object that we are trying to build
            resetForm();
            setSubmitting(false);
          }, 500);
        }}
      >
        {
          (
            { 
              values, // represents the values I'm about to use for the form
              errors, 
              touched, 
              handleChange, 
              handleBlur, 
              handleSubmit, 
              isSubmitting, 
              setFieldValue
            }
          ) => (
            // form template
            <div className="d-flex flex-md-column justify-content-center">
              <div className="container-fluid">
                <form autoComplete="off" onSubmit={handleSubmit}>
                  <FormHeader HeaderName="Personal Details"></FormHeader>
                  <div className="d-flex flex-row">
                      <FormInput
                        HtmlForLabel="Name"
                        AddMargin = {true}
                        LabelName= "Name*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.Name}
                        Touched = {touched.Name}
                        Error = {errors.Name}
                      />
  
                      <FormInput
                        HtmlForLabel="Surname"
                        AddMargin = {false}
                        LabelName= "Surname*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.Surname}
                        Touched = {touched.Surname}
                        Error = {errors.Surname}
                      />
                  </div>
                  <div className="d-flex flex-row">
                      <FormInput
                        HtmlForLabel="DateOfBirth"
                        AddMargin = {true}
                        LabelName= "Date of Birth*"
                        InputType ="date"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.DateOfBirth}
                        Touched = {touched.DateOfBirth}
                        Error = {errors.DateOfBirth}
                      />
  
                      <FormInput
                        HtmlForLabel="IDNumber"
                        AddMargin = {true}
                        LabelName= "Identification Number"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.IDNumber}
                        Touched = {touched.IDNumber}
                        Error = {errors.IDNumber}
                      />
  
                      <DropDownList 
                        DDLTitle = "Gender"
                        Data = {[{Id: 1, Name: 'Other' }, {Id: 2 , Name: 'Female'}, { Id: 3, Name: 'Male'}]}
                        DDLChange = {handleChange}
                        DDLValue = {values.Gender}
                        HandleBlur = {handleBlur}
                        Touched = {touched.Gender}
                        Error = {errors.Gender}
                      />
  
                    
                  </div>
  
                  <FormHeader HeaderName="Contact Details"></FormHeader>
  
                  <div className="d-flex flex-row flex-fill">
                      <FormInput
                        HtmlForLabel="Email"
                        AddMargin = {true}
                        LabelName= "Email*"
                        InputType ="email"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.Email}
                        Touched = {touched.Email}
                        Error = {errors.Email}
                      />
  
                      <FormInput
                        HtmlForLabel="ContactNumber1"
                        AddMargin = {true}
                        LabelName= "Contact Number 1*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.ContactNumber1}
                        Touched = {touched.ContactNumber1}
                        Error = {errors.ContactNumber1}
                      />
  
                      <FormInput
                        HtmlForLabel="ContactNumber2"
                        AddMargin = {false}
                        LabelName= "Contact Number 2"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.ContactNumber2}
                        Touched = {touched.ContactNumber2}
                        Error = {errors.ContactNumber2}
                      />
                  </div>
  
                  <FormHeader HeaderName="Address"></FormHeader>
  
                  <div className="d-flex flex-column flex-fill">
                      <FormInput
                        HtmlForLabel="AddressLine1"
                        AddMargin = {false}
                        LabelName= "Address line 1*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.AddressLine1}
                        Touched = {touched.AddressLine1}
                        Error = {errors.AddressLine1}
                      />
  
                      <FormInput
                        HtmlForLabel="AddressLine2"
                        AddMargin = {false}
                        LabelName= "Address line 2*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.AddressLine2}
                        Touched = {touched.AddressLine2}
                        Error = {errors.AddressLine2}
                      />
                    
                    <div className="d-flex flex-row flex-fill">
                      <FormInput
                        HtmlForLabel="Town"
                        AddMargin = {true}
                        LabelName= "Town/ City*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.Town}
                        Touched = {touched.Town}
                        Error = {errors.Town}
                      />
  
                      <FormInput
                        HtmlForLabel="PostalCode"
                        AddMargin = {true}
                        LabelName= "Postal Code*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.PostalCode}
                        Touched = {touched.PostalCode}
                        Error = {errors.PostalCode}
                      />
  
                      <FormInput
                        HtmlForLabel="Country"
                        AddMargin = {false}
                        LabelName= "Country*"
                        InputType ="text"
                        HandleChange = {handleChange}
                        HandleBlur = {handleBlur}
                        InputValue = {values.Country}
                        Touched = {touched.Country}
                        Error = {errors.Country}
                      />
                      
                    </div>
  
                    <div className="d-flex flex-row flex-fill mt-3">
                      <small>* Required fields.</small>
                    </div>
  
                    <div className="d-flex flex-row flex-fill">
                      <Button 
                        ButtonType="submit"
                        IsDisabled={isSubmitting}
                        ButtonTitle="Submit"
                        ButtonClassName="btn btn-light form-label mt-3"
                      />
                    </div>
                  </div>
  
                </form>
              </div>
            </div>
  
          )
        }
      </Formik>
    );
  }


  export default EmployeeForm;