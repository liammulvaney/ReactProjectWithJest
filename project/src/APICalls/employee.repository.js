import axios from 'axios';
import APIURL from './Urls/URLs';

export const API = APIURL.ADD_EMPLOYEE; // The employee URL API

const ADDEMPLOYEE = async (Employee) => {
    return await axios.post(API, { Employee: Employee })
}

export default ADDEMPLOYEE;
