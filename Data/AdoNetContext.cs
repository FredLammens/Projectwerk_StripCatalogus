using Microsoft.Extensions.Configuration;
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
        /// <param name="db">Which database to use, defult is production.</param>
        /// <param name="ownsConnection">Wheter this context owns the connection.</param>
        public AdoNetContext(bool ownsConnection, string db = "Production")
        {
            connection = new SqlConnection(GetConnectionString(db));
            connection.Open();
            this.ownsConnection = ownsConnection;
            transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Determines connection string based on given db string.
        /// </summary>
        /// <param name="db">Which database to use, default is production.</param>
        /// <returns>The connection string.</returns>
        private String GetConnectionString(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"Files\appsettings.json", optional: false);

            string connectionString = null;

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;
                case "Test":
                    connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }

            return connectionString;
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
        public void Commit()
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
