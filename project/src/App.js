import React from 'react';
import './App.css'
import EmployeeForm from './employeeform';
import ADDEMPLOYEE from './APICalls/employee.repository';

export default class App extends React.Component {
  constructor(props)
  {
    super(props);
    this.state = {
      Employee : {}
    }
    this.onSubmit = this.onSubmit.bind(this);
  }

  onSubmit = async (Employee) =>{
    this.setState({ Employee });
    const message = await ADDEMPLOYEE(this.state.Employee); // at the moment... the call to the back end does not exist, as I still need to create it. However, I've mocked this api call using jest. it can be found in app.test.js beneath the __test__ folder
    return message;  
  };

  render(){
    return (
      <EmployeeForm onSubmit={ (employee) => this.onSubmit(employee)}></EmployeeForm>
    );
  }
}






