using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLayer.Extension_Methods
{
    /// <summary>
    /// Extends Command class
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Adds a parameterto the command
        /// </summary>
        /// <param name="command">Command to add to.</param>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (name == null) throw new ArgumentNullException("name");

            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
        }
    }
}
