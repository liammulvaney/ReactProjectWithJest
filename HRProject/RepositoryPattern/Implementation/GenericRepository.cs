using RepositoryPattern.Interface;
using SQLDataClientFunctions.SQLDataFunctionWrappers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RepositoryPattern.Implementation
{
    public class GenericRepository : IRepository
    {
        private IDatabase _Database { get; set; }
        public GenericRepository(IDatabase Context)
        {
            _Database = Context;
        }

        public void Create<TEntity>(string StoredProcedure, TEntity Entity)
        {
            _Database.ExecuteSQLCommand(StoredProcedure, _Database.GenerateParameters(Entity));
        }

        public TEntity Read<TEntity>(string StoredProcedure, Guid Id, Guid PartitionId)
        {
            SqlParameter[] parameters = { new SqlParameter("@Id", Id), new SqlParameter("@PartitionId", PartitionId) };
            return _Database.ReturnFromSQL<TEntity>(StoredProcedure, parameters).FirstOrDefault();
        }

        public void Update<TEntity>(string StoredProcedure, TEntity Entity)
        {
            _Database.ExecuteSQLCommand(StoredProcedure, _Database.GenerateParameters(Entity));
        }

        public void Delete<TEntity>(string StoredProcedure, Guid Id, Guid PartitionId)
        {
            SqlParameter[] parameters = { new SqlParameter("@Id", Id), new SqlParameter("@PartitionId", PartitionId) };
            _Database.ExecuteSQLCommand(StoredProcedure, parameters);
        }

        public List<TEntity> GetAll<TEntity>(string StoredProcedure, Guid PartitionId)
        {
            SqlParameter[] parameters = { new SqlParameter("@PartitionId", PartitionId) };
            return _Database.ReturnFromSQL<TEntity>(StoredProcedure, parameters);
        }
    }
}
