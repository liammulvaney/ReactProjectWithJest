import React from 'react';
import ReactDOM from 'react-dom';
import App from '../App';
import EmployeeForm from '../employeeform'; // this is important for parent child components. If App component has child component please add the reference
import { shallow } from 'enzyme';
import ADDEMPLOYEE from '../APICalls/employee.repository';
import * as axios from 'axios';
jest.mock('axios');

// this is called a smoke test. Here we are simply rendering the app and employee form components within a div
it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
});

// this renders a shadow component of the App, found in App.js
describe('test onSubmit function', () =>
{
  // the dummy object that will be used for our series of tests.
  const Employee = 
    {
      Name: "Liam",
      Surname: "Mulvaney",
      DateOfBirth: "1992-07-14",
      Gender: "3",
      IDNumber: "",
      Email: "liam.m.m.mulvaney@gmail.com",
      ContactNumber1: "27485413",
      ContactNumber2: "",
      AddressLine1: "12, Flat 1",
      AddressLine2: "Triq ix-xitwa",
      Town: "Qormi",
      PostalCode: "QRM 5233",
      Country: "Malta",
      DOB: "1992-07-14T00:00:00.000Z"
    };

  // this test only renders the app component
  it('renders without crashing', () => {
    shallow(<App />);
  });

  // check if the state of the app component is updated when the on click function is clicked.
  it('test that "App" component state stores the values sent from the employee form', () => { 
    const wrapper = shallow(<App/>);
    const instance = wrapper.instance();
    instance.onSubmit(Employee);
    expect(wrapper.state()).toEqual({ Employee : Employee });
  });  
  
  // jest offers a way to mock async functions... This is how I'll mock the API
  it('Fetches a successful message with the add employee API Call', async () => {
    const data = { data: "Ok"}; // this is the mocked response from the back end
    const wrapper = shallow(<App/>);
    const instance = wrapper.instance();
    const APIURL = 'https://Employee/Add'; // URL will change. This is just a proof of concept.
    instance.onSubmit(Employee);
    expect(wrapper.state()).toEqual({ Employee : Employee });
    axios.post.mockImplementationOnce(() => Promise.resolve(data));
    await expect(instance.onSubmit(Employee)).resolves.toEqual(data);
  });

  it('Fetches an error message with the add employee API Call', async () => {
    const errorMessage = { data: "Bad request"}; // this is the mocked response from the back end
    const wrapper = shallow(<App/>);
    const instance = wrapper.instance();
    instance.onSubmit(Employee);
    expect(wrapper.state()).toEqual({ Employee : Employee });
    axios.post.mockImplementationOnce(() => Promise.reject(new Error(errorMessage.data)));
    await expect(instance.onSubmit(Employee)).rejects.toThrow(errorMessage.data); 
  });

});

//npm test -- --coverage  <-- command give test coverage report

