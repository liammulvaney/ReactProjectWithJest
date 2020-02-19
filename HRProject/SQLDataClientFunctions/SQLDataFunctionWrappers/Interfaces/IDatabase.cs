using System.Collections.Generic;
using System.Data.SqlClient;

namespace SQLDataClientFunctions.SQLDataFunctionWrappers
{
    public interface IDatabase
    {

        /// <summary>
        /// Generates the SQL Parameters based on the signature of the class
        /// </summary>
        /// <typeparam name="TEntity">The class representation</typeparam>
        /// <param name="Entity">The object used</param>
        /// <returns>an array of SQL Paramaters</returns>
        SqlParameter[] GenerateParameters<TEntity>(TEntity Entity);

        /// <summary>
        /// Returns a list of results from the database
        /// </summary>
        /// <typeparam name="TEntity">The Model class chosen</typeparam>
        /// <param name="StoredProcedure">The stored procedure name</param>
        /// <param name="Parameters">the sql parameters needed to execute the stored procedure</param>
        /// <returns>a list of entries from the database</returns>
        List<TEntity> ReturnFromSQL<TEntity>(string StoredProcedure, SqlParameter[] Parameters);

        /// <summary>
        /// Execute an stp like create, update, delete from the database.
        /// </summary>
        /// <param name="StoredProcedure">the stored procedure name</param>
        /// <param name="Parameters">the sql parameters needed to execute the stored procedure</param>
        /// <returns>affected number of Rows</returns>
        int ExecuteSQLCommand(string StoredProcedure, SqlParameter[] Parameters);
    }
}
