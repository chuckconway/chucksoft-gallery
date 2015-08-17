using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace Chucksoft.Resources.Data.SqlCe
{
    public class SqlCeClient : DbClient
    {
        /// <summary>
        /// Gets the type of the command.
        /// </summary>
        /// <value>The type of the command.</value>
        public override CommandType CommandType
        {
            get { return CommandType.Text; }
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public override DbCommand Command
        {
            get { return new SqlCeCommand(); }
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public override DbConnection Connection
        {
            get { return new SqlCeConnection(); }
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public override DbParameter Parameter
        {
            get { return new SqlCeParameter(); }
        }

        /// <summary>
        /// Gets the parameter array.
        /// </summary>
        /// <value>The parameter array.</value>
        public override DbParameter[] ParameterArray
        {
            get { return new SqlCeParameter[] { }; }
        }
    }
}
