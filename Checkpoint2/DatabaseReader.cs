using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WCS
{
    public class DatabaseReader
    {
        public List<object[]> ReturnDataDB(string query)
        {
            IDatabase database = new Database();
            SqlConnection Connection = database.Connection;

            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = Connection;
            SqlDataReader reader = cmd.ExecuteReader();

            List<object[]> data = new List<object[]>();

            while (reader.Read())
            {
                object[] outputlocal = new object[reader.FieldCount];
                reader.GetValues(outputlocal);
                data.Add(outputlocal);
            }
            reader.Close();
            return data;
        }
    }
}
