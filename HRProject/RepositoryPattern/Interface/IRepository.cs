using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Interface
{
    public interface IRepository
    {

        /// <summary>
        /// creates an entry in the database
        /// </summary>
        /// <typeparam name="TEntity">The Model class Entity Affected</typeparam>
        /// <param name="StoredProcedure">the Stored Procedure Name</param>
        /// <param name="Entity">The Model affected</param>
        void Create<TEntity>(string StoredProcedure, TEntity Entity);

        /// <summary>
        /// reads an entry from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam> 
        /// <param name="StoredProcedure"></param>
        /// <param name="Id"></param>
        /// <param name="PartitionId"></param>
        /// <returns> fetches one record from the database</returns>
        TEntity Read<TEntity>(string StoredProcedure, Guid Id, Guid PartitionId);

        /// <summary>
        /// Updates a Record from the Database
        /// </summary>
        /// <typeparam name="TEntity">represents the class used</typeparam>
        /// <param name="StoredProcedure">the stored procedure name</param>
        /// <param name="Entity">The Record parameters to Update</param>
        void Update<TEntity>(string StoredProcedure, TEntity Entity);

        /// <summary>
        /// Deletes an entry from the database
        /// </summary>
        /// <typeparam name="TEntity">represents the class used</typeparam>
        /// <param name="StoredProcedure">the stored procedure name</param>
        /// <param name="Id">the Id primary key</param>
        /// <param name="PartitionId">the segment partition</param>
        void Delete<TEntity>(string StoredProcedure, Guid Id, Guid PartitionId);

        /// <summary>
        /// Retrieves a list from the database
        /// </summary>
        /// <typeparam name="TEntity">represents the class</typeparam>
        /// <param name="StoredProcedure">the stored procedure used</param>
        /// <param name="PartitionId">The Partition Id</param>
        /// <returns>a list from the database</returns>
        List<TEntity> GetAll<TEntity>(string StoredProcedure, Guid PartitionId);
    }
}
