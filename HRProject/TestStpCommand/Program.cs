using Models.Models;
using SQLDataClientFunctions.SQLDataFunctionWrappers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TestStpCommand
{
    public class DBContext : IDBContext
    {
        public string ConnectionString { get; set; }
        public DBContext() {
            ConnectionString = "Server=DESKTOP-NC8VNI7;DataBase=project;Integrated Security=SSPI";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@PartitionId", new Guid("713d427d-3964-4585-9d43-06803b1f7064")) }; 
            List<Employee> results = new Database(new DBContext()).ReturnFromSQL<Employee>("People.STP_EMPLOYEE_GETEMPLOYEES", sqlParameters);
            Console.WriteLine("Done");
        }
    }
}
