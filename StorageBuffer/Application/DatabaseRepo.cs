using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer.Application
{
    public class DatabaseRepo
    {
        private readonly string connectionString;

        public DatabaseRepo()
        {
            connectionString = "Server=EALSQL1.eal.local;Database=B_DB24_2018;User Id=B_STUDENT24;Password=B_OPENDB24;";
        }

        SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
