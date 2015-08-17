using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Chucksoft.Entities.Interfaces.Resources;

namespace Chucksoft.Resources.Database
{
    public class SqlServer : ISqlServer
    {
        protected static string GetConnectionStringKey()
        {
            return "PhotoDatabase";
        }

        /// <summary>
        /// Called when [pre connection string open].
        /// </summary>
        /// <returns></returns>
        protected static SqlConnection OnPreConnectionStringOpen()
        {
            string connectionStringKey = GetConnectionStringKey();
            SqlConnection connection = !string.IsNullOrEmpty(connectionStringKey) ? GetConnection(connectionStringKey) : new SqlConnection();

            return connection;
        }

        /// <summary>
        /// Populates the Command with the commandText and array of parameters
        /// </summary>
        /// <param name="commandText">Inline Sql or stored procedure name</param>
        /// <param name="parameters">parameters to be passed into procedure call</param>
        /// <returns>A SqlCommand with associated commandText and SqlParameters</returns>
        private static SqlCommand GetCommand(string commandText, SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(commandText);
            cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        /// <summary>
        /// Creates the SqlCommand and sets the command Text
        /// </summary>
        /// <param name="commandText">Inline Sql or stored procedure name</param>
        /// <returns>A SqlCommand with associated commandText</returns>
        private static SqlCommand GetCommand(string commandText)
        {
            string key = GetConnectionStringKey();

            SqlConnection connection = GetConnection(key);
            SqlCommand cmd = new SqlCommand(commandText, connection) { CommandType = CommandType.StoredProcedure };

            return cmd;
        }

        /// <summary>
        /// Gets first connection in the connection section of the configuration file.
        /// </summary>
        /// <returns>A non opened SqlConnection</returns>
        private static SqlConnection GetConnection(string key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <returns></returns>
        public T Scalar<T>(string cmdText)
        {
            return Scalar<T>(cmdText, new SqlParameter[] {});
        }


        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public T Scalar<T>(string cmdText, SqlParameter[] parms)
        {
            T result;

            //Get command
            using (SqlCommand command = GetCommand(cmdText, parms))
            {
                //encapsulate the connection into a using statement to ensure proper
                //closing and disposal
                using (SqlConnection connnection = command.Connection)
                {
                    connnection.Open();
                    result = (T)command.ExecuteScalar();
                }
            }

            return result;
        }

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <returns>Result Set from procedure execution</returns>
        public SqlDataReader Reader(string storedProcedure)
        {
            return Reader(storedProcedure, new SqlParameter[] { });
        }


        /// <summary>
        /// Retrieves the values.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public DataTable RetrieveValues(SqlDataReader reader)
        {
            //Discover the columns
            DataTable table = DiscoverColumns(reader);

            //managing resources...
            using (reader)
            {
                //read until we run out of rows
                while (reader.Read())
                {
                    //get a new row
                    DataRow row = table.NewRow();

                    //Set the column value
                    for (int index = 0; index < reader.VisibleFieldCount; index++)
                    {
                        row[reader.GetName(index)] = reader.GetValue(index);
                    }

                    //Add our newly created and populated row.
                    table.Rows.Add(row);
                }
            }

            return table;
        }

        /// <summary>
        /// Discovers the columns.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static DataTable DiscoverColumns(DbDataReader reader)
        {
            DataTable table = new DataTable();

            //Dynamically discover and add the columns in the resultSet
            for (int index = 0; index < reader.VisibleFieldCount; index++)
            {
                string columnName = reader.GetName(index);

                if (!table.Columns.Contains(columnName))
                {
                    table.Columns.Add(new DataColumn(columnName));
                }
            }

            return table;
        }

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="values">values to be passed into procedure</param>
        /// <returns>Result Set from procedure execution</returns>
        public SqlDataReader Reader(string storedProcedure, SqlParameter[] values)
        {
            SqlDataReader reader;

            //Get command
            using (SqlCommand command = GetCommand(storedProcedure, values))
            {
                command.Connection.Open();
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }

            return reader;
        }

        /// <summary>
        /// Executes the procedure
        /// </summary>
        /// <param name="storedProc">name of stored procedure</param>
        /// <returns>rows affected</returns>
        public int NonQuery(string storedProc)
        {
            return NonQuery(storedProc, new SqlParameter[] { });
        }

        /// <summary>
        /// Executes the procedure and passes the paramters to the stored procedure
        /// </summary>
        /// <param name="storedProc">name of the stored procedure to be executed</param>
        /// <param name="parms">Sql Parameters to be passed to the Sql Server</param>
        /// <returns>rows affected</returns>
        public int NonQuery(string storedProc, SqlParameter[] parms)
        {
            int returnValue;

            //Get command
            using (SqlCommand command = GetCommand(storedProc, parms))
            {
                //encapsulate the connection into a using statement to ensure proper
                //closing and disposal
                using (SqlConnection connnection = command.Connection)
                {
                    connnection.Open();
                    returnValue = command.ExecuteNonQuery();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <returns>Returns a fully populated parameter</returns>
        public SqlParameter MakeParameter(string parameterName, SqlDbType dbType, int size, object parmValue)
        {
            return MakeParameter(parameterName, dbType, size, parmValue, ParameterDirection.Input);
        }

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <param name="direction">What direction the parameter is...</param>
        /// <returns>Returns a fully populated parameter</returns>
        public SqlParameter MakeParameter(string parameterName, SqlDbType dbType, int size, object parmValue, ParameterDirection direction)
        {
            SqlParameter parm = new SqlParameter(parameterName, dbType, size) { Value = parmValue, Direction = direction };
            return parm;
        }
    }
}
