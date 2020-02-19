using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SQLDataClientFunctions.SQLDataFunctionWrappers
{
    /// <summary>
    /// Contains the SQL Connection String Property and the constructor which will set up the database connection string. It also contains the function that executes the stp
    /// </summary>
    public partial class Database : IDatabase
    {
        private string _SQLConnectionString { get; set; }
        public Database(IDBContext Context)
        {
            _SQLConnectionString = Context._ConnectionString;
        }

        /// <summary>
        /// Generates the SQL Parameters
        /// </summary>
        /// <typeparam name="TEntity">the generic class parameter</typeparam>
        /// <param name="Entity">the object</param>
        /// <returns>the sql parameter array</returns>
        public SqlParameter[] GenerateParameters<TEntity>(TEntity Entity)
        {
            return InitSQLParameterArray(GetEntityPropertyInfo<TEntity>(), Entity); //<--- the Sql Parameter array is printed here
        }

        /// <summary>
        /// returns an array of SQL Parameters
        /// </summary>
        /// <typeparam name="TEntity">The generic used to represent the class</typeparam>
        /// <param name="Properties">the reflected property info called from the class</param>
        /// <param name="Entity">the class object used to reproduce the SQL Parameter array</param>
        /// <returns>an SQL Parameter Array from the class</returns>
        private SqlParameter[] InitSQLParameterArray<TEntity>(PropertyInfo[] Properties, TEntity Entity)
        {
            int counter = 0;
            SqlParameter[] sqlParameters = new SqlParameter[Properties.Length];
            while(counter < Properties.Length)
            {
                sqlParameters[counter] = new SqlParameter($"@{Properties[counter].Name}", Properties[counter].GetValue(Entity, null) ?? DBNull.Value);
                counter++;
            }
            return sqlParameters;
        }
    }

    /// <summary>
    /// contains the ReturnFromSQL Function. This will be used as a Get Function from the database
    /// </summary>
    public partial class Database: IDatabase
    {        
        /// <summary>
        /// Returns a List of Items from the Database
        /// </summary>
        /// <param name="StoredProcedure">the Stored Procedure To Run</param>
        /// <param name="Parameters">The SQL Paramaters</param>
        public List<TEntity> ReturnFromSQL<TEntity>(string StoredProcedure, SqlParameter[] Parameters) 
        {            
            List<TEntity> Results;
            using (SqlConnection sqlConnection = new SqlConnection(_SQLConnectionString))
            {
                // Executes the Stored Procedure and returns the results from the database
                Results = ReadResults<TEntity>(ExecuteSTP(sqlConnection, StoredProcedure, Parameters));
            }
            return Results;
        }

        /// <summary>
        /// reads the Entity Properties using Reflection and uses the 
        /// </summary>
        /// <typeparam name="TEntity">The Entity Class to Represent</typeparam>
        /// <returns>A queryable results from the database</returns>
        public List<TEntity> ReadResults<TEntity>(SqlCommand SQLCommand)
        {
            List<TEntity> returnedResults = new List<TEntity> { };
            using (SqlDataReader dataReader = SQLCommand.ExecuteReader())
            {
                // iterates through the returned results and adds them to the list of objects
                while (dataReader.Read())
                {
                    returnedResults.Add(CreateInstanceOfSQLResult<TEntity>(dataReader));
                }
            }
            return returnedResults;
        }

        /// <summary>
        /// Creates an object to add to the list of items returned from the database
        /// </summary>
        /// <typeparam name="TEntity">the object to create an instance</typeparam>
        /// <param name="dataReader">the SQL Data Reader</param>
        /// <returns>the record from the database</returns>
        public TEntity CreateInstanceOfSQLResult<TEntity>(SqlDataReader dataReader) 
        {
            int infoListCounter = 0;            
            PropertyInfo[] EntityInfoList = GetEntityPropertyInfo<TEntity>();
            TEntity entityObject = Activator.CreateInstance<TEntity>(); // we created an instance of the object          

            while (infoListCounter < EntityInfoList.Length)
            {
                EntityInfoList[infoListCounter].SetValue(entityObject, dataReader[EntityInfoList[infoListCounter].Name]); // the SQL Data Reader property is converted to the TEntity Type
                infoListCounter++;
            }

            return entityObject;
        }

        /// <summary>
        /// returns the Property Info of the Entity Classes
        /// </summary>
        /// <typeparam name="TEntity">The Entity return Type</typeparam>
        /// <returns>an array with the property List</returns>
        public PropertyInfo[] GetEntityPropertyInfo<TEntity>()
        {
            return typeof(TEntity).GetProperties(); // returns the list of class properties
        }


        /// <summary>
        /// Executes the Stored Procedure. 
        /// </summary>
        /// <param name="Connection">The Connection String of the database</param>
        /// <param name="StoredProcedure">The Name of the Stored Procedure</param>
        /// <param name="Parameters">the SQL Paramaters</param>
        public SqlCommand ExecuteSTP(SqlConnection Connection, string StoredProcedure, SqlParameter[] Parameters)
        {
            Connection.Open(); //open Sql Connection           
            SqlCommand SQLCommand = new SqlCommand(StoredProcedure, Connection); // 1.  create a command object identifying the stored procedure           
            SQLCommand.CommandType = CommandType.StoredProcedure; // 2. set the command object so it knows to execute a stored procedure           
            SQLCommand.Parameters.AddRange(Parameters);// 3. Add the SQL Parameters
            return SQLCommand;
        }
    }

    public partial class Database: IDatabase
    {
        /// <summary>
        /// Executes an SQL stored Procedure. Used for stp's like create, update, delete
        /// </summary>
        /// <param name="StoredProcedure">The Stored Procedure name</param>
        /// <param name="Parameters">the sql paramater/s array</param>
        public int ExecuteSQLCommand(string StoredProcedure, SqlParameter[] Parameters)
        {
            int affectedRows = -1;
            using (SqlConnection sqlConnection = new SqlConnection(_SQLConnectionString))
            {
                // Executes the Stored Procedure
                affectedRows = FinalizeQuery(ExecuteSTP(sqlConnection, StoredProcedure, Parameters));
            }
            return affectedRows;
        }

        public int FinalizeQuery(SqlCommand sqlCommand)
        {
            return sqlCommand.ExecuteNonQuery();
        }
    }
    
}
