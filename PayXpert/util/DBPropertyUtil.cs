using System;
using System.Configuration;

namespace PayXpert.util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString()
        {
            //ConfigurationManager.AppSettings.Get("DBConnectionString")
            var temp = ConfigurationManager.AppSettings.AllKeys;
            var connStr = ConfigurationManager.AppSettings;

            return ConfigurationManager.AppSettings.Get("DefaultConnection");
        }
    }
}
