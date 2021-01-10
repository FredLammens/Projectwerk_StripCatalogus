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
            OpenConnection(db);
            this.ownsConnection = ownsConnection;
        }

        /// <summary>
        /// Determines connection string based on given db string.
        /// </summary>
        /// <param name="db">Which database to use, default is production.</param>
        /// <returns>The connection string.</returns>
        private void OpenConnection(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"Files\appsettings.json", optional: false);

            string connectionString = null;

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    {
                        connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                        connection = new SqlConnection(connectionString);
                        connection.Open();
                    }
                    break;
                case "Test":
                    {
                        connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                        connection = new SqlConnection(connectionString);
                        connection.Open();
                        EmptyDatabase();
                    }
                    break;
            }
        }
        /// <summary>
        /// Empties the databse for test purposes.
        /// </summary>
        private void EmptyDatabase()
        {
            BeginTransaction();
            using (var command = CreateCommand())
            {
                command.CommandText = "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' " +
                                      "EXEC sp_MSForEachTable 'DELETE FROM ?' " +
                                      "DBCC CHECKIDENT('Authors', RESEED, 0); " +
                                      "DBCC CHECKIDENT('Comics', RESEED, 0); " +
                                      "DBCC CHECKIDENT('Series', RESEED, 0); " +
                                      "DBCC CHECKIDENT('Deliveries', RESEED, 0); " +
                                      "DBCC CHECKIDENT('Orders', RESEED, 0); " +
                                      "DBCC CHECKIDENT('Publishers', RESEED, 0); " +
                                      "DBCC CHECKIDENT('Stock', RESEED, 0); " +
                                      "EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL' ";
                command.ExecuteNonQuery();
            }
            Commit();
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
            if (transaction == null)
            {
                throw new InvalidOperationException("Transaction is al voltooid.");
            }
            transaction.Commit();
            transaction = null;
        }


        /// <summary>
        /// Disposes of the transaction and or connection.
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
        /// <summary>
        /// Begins a new transaction.
        /// </summary>
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }
    }
}
