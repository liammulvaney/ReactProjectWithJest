using BusinessLogic.Implementation;
using BusinessLogic.Interface;
using Models.Models;
using RepositoryPattern.Implementation;
using SQLDataClientFunctions.SQLDataFunctionWrappers;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class HRProject : IDBContext
    {
        public string _ConnectionString { get; set; }
        public HRProject()
        {
            _ConnectionString = "Server=DESKTOP-NC8VNI7;DataBase=project;Integrated Security=SSPI";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IDBContext context = new HRProject();
            IEmployeeLogic employeeLogic = new EmployeeLogic(new UnitOfWork(new GenericRepository(new Database(context))));
            List<Employee> employees = employeeLogic.GetAllEmployees(new Guid("713d427d-3964-4585-9d43-06803b1f7064"));
            Console.ReadKey();
        }
    }
}
