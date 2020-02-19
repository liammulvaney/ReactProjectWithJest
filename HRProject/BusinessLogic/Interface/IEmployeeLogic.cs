using Models.Models;
using RepositoryPattern.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interface
{
    public interface IEmployeeLogic
    {
        IUnitOfWork _UOW 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PartitionId"></param>
        /// <returns></returns>
        Employee GetEmployee(Guid Id, Guid PartitionId);

        /// <summary>
        /// Retrieves a list of all employees
        /// Uses STP: People.STP_EMPLOYEE_GETEMPLOYEES
        /// </summary>
        /// <param name="PartitionId">The Partition Section</param>
        /// <returns>retrieves a list of Employees</returns>
        List<Employee> GetAllEmployees(Guid PartitionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        void AddEmployee(Employee employee);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        void EditEmployee(Employee employee);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PartitionId"></param>
        void DeleteEmployee(Guid Id, Guid PartitionId);
    }
}
