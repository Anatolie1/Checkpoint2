using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WCS
{
    public class Display
    {
        public static void DisplayAgenda( int personid, string startdate, string enddate)
        {
            string query = $"EXECUTE sp_Event {personid}, '{startdate}', '{enddate}'";
            DisplayData(query);
        }
        public static void DisplayCursusStudents(string cursusname)
        {
            string query = $"EXECUTE sp_Cursus_Students '{cursusname}'";
            DisplayData(query);
        }
        public static void DisplayCursusQuests(string cursusname)
        {
            string query = $"EXECUTE sp_Quest_Expedition '{cursusname}'";
            DisplayData(query);
        }
        static void DisplayData(string query)
        {
            DatabaseReader reader = new DatabaseReader();
            List<object[]> dataDB = reader.ReturnDataDB(query);

            foreach (object[] item in dataDB)
            {
                Console.WriteLine(String.Join("\t", item));
            }
        }
    }
}
