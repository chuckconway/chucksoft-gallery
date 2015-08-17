using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
    public abstract class DbClient
    {
        /// <summary>
        /// Gets the type of the command.
        /// </summary>
        /// <value>The type of the command.</value>
        public abstract CommandType CommandType { get;}

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public abstract DbCommand Command { get;}

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public abstract DbConnection Connection { get;}

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public abstract DbParameter Parameter { get;}

        /// <summary>
        /// Gets the parameter array.
        /// </summary>
        /// <value>The parameter array.</value>
        public abstract DbParameter[] ParameterArray { get; }


        /// <summary>
        /// Populates the Command with the commandText and array of parameters
        /// </summary>
        /// <param name="commandText">Inline Sql or stored procedure name</param>
        /// <param name="parameters">parameters to be passed into procedure call</param>
        /// <returns>A SqlCommand with associated commandText and SqlParameters</returns>
        private DbCommand GetCommand(string commandText, DbParameter[] parameters)
        {
            DbCommand cmd = GetCommand(commandText);
            cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        /// <summary>
        /// Creates the SqlCommand and sets the command Text
        /// </summary>
        /// <param name="commandText">Inline Sql or stored procedure name</param>
        /// <returns>A SqlCommand with associated commandText</returns>
        private DbCommand GetCommand(string commandText)
        {
            DbConnection connection = GetConnection();
            DbCommand cmd = Command; //new DbCommand(commandText, connection) { CommandType = CommandType.StoredProcedure };
            cmd.CommandText = commandText;
            cmd.Connection = connection;
            cmd.CommandType = CommandType;

            return cmd;
        }

        /// <summary>
        /// Gets first connection in the connection section of the configuration file.
        /// </summary>
        /// <returns>A non opened SqlConnection</returns>
        private  DbConnection  GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            DbConnection connection = Connection;//new SqlConnection(connectionString);
            connection.ConnectionString = connectionString;

            return connection;
        }

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <param name="cmdText">The CMD text.</param>
        /// <returns></returns>
        public object Scalar(string cmdText)
        {
            object result;

            //Get command
            using (DbCommand command = GetCommand(cmdText))
            {
                //encapsulate the connection into a using statement to ensure proper
                //closing and disposal
                using (DbConnection connnection = command.Connection)
                {
                    connnection.Open();
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the first row of the result set
        /// </summary>
        /// <param name="storedProcedure">procedure to be executed</param>
        /// <param name="parms">parameters to be passed into procedure call</param>
        /// <returns>the first row of the results set</returns>
        public object Scalar(string storedProcedure, DbParameter[] parms)
        {
            object result;

            //Get command
            using (DbCommand command = GetCommand(storedProcedure, parms))
            {
                //encapsulate the connection into a using statement to ensure proper
                //closing and disposal
                using (DbConnection connnection = command.Connection)
                {
                    connnection.Open();
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <returns>Result Set from procedure execution</returns>
        public DbDataReader Reader(string storedProcedure)
        {
            DbParameter[] parameter = ParameterArray;
            return Reader(storedProcedure, parameter);
        }

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="values">values to be passed into procedure</param>
        /// <returns>Result Set from procedure execution</returns>
        public DbDataReader Reader(string storedProcedure, DbParameter[] values)
        {
            DbDataReader reader;

            //Get command
            using (DbCommand command = GetCommand(storedProcedure, values))
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
            DbParameter[] parameters = ParameterArray;

            return NonQuery(storedProc, parameters);
        }

        /// <summary>
        /// Executes the procedure and passes the paramters to the stored procedure
        /// </summary>
        /// <param name="storedProc">name of the stored procedure to be executed</param>
        /// <param name="parms">Sql Parameters to be passed to the Sql Server</param>
        /// <returns>rows affected</returns>
        public int NonQuery(string storedProc, DbParameter[] parms)
        {
            int returnValue;

            //Get command
            using (DbCommand command = GetCommand(storedProc, parms))
            {
                //encapsulate the connection into a using statement to ensure proper
                //closing and disposal
                using (DbConnection connnection = command.Connection)
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
        public DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue)
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
        public DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue, ParameterDirection direction)
        {
            //Set Values
            DbParameter parm = Parameter;
            parm.ParameterName = parameterName;
            parm.DbType = dbType;
            parm.Value = parmValue;
            parm.Direction = direction;

            return parm;
        }
    }
}
