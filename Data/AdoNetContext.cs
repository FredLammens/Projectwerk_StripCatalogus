using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class AdoNetContext : IDisposable
    {
        /// <summary>
        /// Connection to the database.
        /// </summary>
        private IDbConnection connection;
        /// <summary>
        /// Wheter this context owns the connection.
        /// </summary>
        private bool ownsConnection;
        /// <summary>
        /// The transaction used in the context.
        /// </summary>
        private IDbTransaction transaction;

        /// <summary>
        /// Makes a AdoNewtContext object
        /// </summary>
        /// <param name="connectionString">Connection string for the database</param>
        /// <param name="ownsConnection">Wheter this context owns the connection.</param>
        public AdoNetContext(string connectionString, bool ownsConnection)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            this.ownsConnection = ownsConnection;
            transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <returns>A new command object.</returns>
        public IDbCommand CreateCommand()
        {
            var command = connection.CreateCommand();
            command.Transaction = transaction;
            return command;
        }

        /// <summary>
        /// Saves all changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            if(transaction == null)
            {
                throw new InvalidOperationException("Transaction is al voltooid.");
            }
            transaction.Commit();
            transaction = null;
        }


        /// <summary>
        /// Disposes of the trasactiomn and or connection.
        /// </summary>
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
