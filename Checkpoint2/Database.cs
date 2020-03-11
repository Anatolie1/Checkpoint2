using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WCS
{
    sealed class Database: IDatabase
    {
        public SqlConnection Connection { get; private set; }
        private Database instance = null;

        public Database()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"AG\SQLEXPRESS";
            builder.InitialCatalog = "Checkpoint2";
            builder.IntegratedSecurity = true;
            Connection = new SqlConnection(builder.ConnectionString);
            Connection.Open();
        }

        public Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }
    }
}
