using System.Data;
using System.Data.SqlClient;

namespace Chucksoft.Entities.Interfaces.Resources
{
    public interface ISqlServer
    {
        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <returns></returns>
        T Scalar<T>(string cmdText);

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        T Scalar<T>(string cmdText, SqlParameter[] parms);

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <returns>Result Set from procedure execution</returns>
        SqlDataReader Reader(string storedProcedure);

        /// <summary>
        /// Retrieves the values.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        DataTable RetrieveValues(SqlDataReader reader);

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="values">values to be passed into procedure</param>
        /// <returns>Result Set from procedure execution</returns>
        SqlDataReader Reader(string storedProcedure, SqlParameter[] values);

        /// <summary>
        /// Executes the procedure
        /// </summary>
        /// <param name="storedProc">name of stored procedure</param>
        /// <returns>rows affected</returns>
        int NonQuery(string storedProc);

        /// <summary>
        /// Executes the procedure and passes the paramters to the stored procedure
        /// </summary>
        /// <param name="storedProc">name of the stored procedure to be executed</param>
        /// <param name="parms">Sql Parameters to be passed to the Sql Server</param>
        /// <returns>rows affected</returns>
        int NonQuery(string storedProc, SqlParameter[] parms);

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <returns>Returns a fully populated parameter</returns>
        SqlParameter MakeParameter(string parameterName, SqlDbType dbType, int size, object parmValue);

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <param name="direction">What direction the parameter is...</param>
        /// <returns>Returns a fully populated parameter</returns>
        SqlParameter MakeParameter(string parameterName, SqlDbType dbType, int size, object parmValue, ParameterDirection direction);
    }
}