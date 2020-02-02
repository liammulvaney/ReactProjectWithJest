import React from 'react';
import { shallow } from 'enzyme';
import EmployeeForm from '../employeeform';

it('shallow rendering of new employee form', () => {
    shallow(<EmployeeForm/>);
});