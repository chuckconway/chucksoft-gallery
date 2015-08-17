using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Chucksoft.Resources.Data.SqlServer
{
    public class SqlServerClient : DbClient
    {
        /// <summary>
        /// Implement provide specific command
        /// </summary>
        public override DbCommand Command
        {
            get { return new SqlCommand(); }
        }

        /// <summary>
        /// Implement provide specific Connection
        /// </summary>
        public override DbConnection Connection
        {
            get { return new SqlConnection(); }
        }

        /// <summary>
        /// Implement provide specific CommandType
        /// </summary>
        public override CommandType CommandType
        {
            get { return CommandType.StoredProcedure; }
        }

        /// <summary>
        /// Implement provide specific Parameter
        /// </summary>
        public override DbParameter Parameter
        {
            get { return new SqlParameter(); }
        }

        /// <summary>
        /// Implement provide specific ParameterArray
        /// </summary>
        public override DbParameter[] ParameterArray
        {
            get { return new SqlParameter[] { }; }
        }
    }
}
