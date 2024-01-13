using System;
using System.Data.SqlClient;


namespace PayXpert.util
{
    public class DBConnUtil
    {
        public static SqlConnection ReturnConnectionObject()
        {
            string connString = DBPropertyUtil.GetConnectionString();
            Console.WriteLine(connString);
            return new SqlConnection(connString);
        }
    }
}
