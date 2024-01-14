using System;
using System.Configuration;

namespace PayXpert.util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("DefaultConnection");
        }
    }
}
