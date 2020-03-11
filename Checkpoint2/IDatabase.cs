using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WCS
{
    interface IDatabase
    {
        SqlConnection Connection { get; }
        Database Instance { get; }
    }
}