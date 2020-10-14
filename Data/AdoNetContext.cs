using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class AdoNetContext : IDisposable
    {
        private IDbConnection connection;
        private bool ownsConnection;
        private IDbTransaction transaction;

        public AdoNetContext(string connectionString, bool ownsConnection)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            this.ownsConnection = ownsConnection;
            transaction = connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            var command = connection.CreateCommand();
            command.Transaction = transaction;
            return command;
        }

        public void SaveChanges()
        {
            if(transaction == null)
            {
                throw new InvalidOperationException("Transaction is al voltooid.");
            }
            transaction.Commit();
            transaction = null;
        }



        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
            if (connection != null && ownsConnection)
            {
                connection.Close();
                connection = null;
            }
        }
    }
}
