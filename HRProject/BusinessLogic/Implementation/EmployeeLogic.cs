using BusinessLogic.Interface;
using Models.Models;
using RepositoryPattern.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Implementation
{
    public class EmployeeLogic : IEmployeeLogic
    {
        public IUnitOfWork _UOW { get; set; }

        public EmployeeLogic(IUnitOfWork UOW) 
        {
            _UOW = UOW;
        }

        public void AddEmployee(Employee employee)
        {
            using (_UOW)
                _UOW._Repository.Create("", employee);
        }

        public void DeleteEmployee(Guid Id, Guid PartitionId)
        {
            using (_UOW)
                _UOW._Repository.Delete<Employee>("", Id, PartitionId);
        }

        public Employee GetEmployee(Guid Id, Guid PartitionId)
        {
            Employee employee = null;            
            using (_UOW)
                employee = _UOW._Repository.Read<Employee>("", Id, PartitionId);
            return employee;
        }

        public List<Employee> GetAllEmployees(Guid PartitionId)
        {
            List<Employee> employees = null;
            using (_UOW)
                employees = _UOW._Repository.GetAll<Employee>("People.STP_EMPLOYEE_GETEMPLOYEES", PartitionId);
            return employees;
        }

        public void EditEmployee(Employee employee)
        {
            using (_UOW)
                _UOW._Repository.Update("", employee);
        }
    }
}
