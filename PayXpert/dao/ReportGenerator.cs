using PayXpert.exception;
using PayXpert.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    public class ReportGenerator
    {
        static SqlConnection conn;
        public static void GeneratePayrollReport()
        {
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                string q = "SELECT * FROM Payroll";
                DatabaseContext.GetDataFromDB(q, conn, "-----------Payroll Report-----------\n", true);
            }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }

        public static void GenerateTaxReport()
        {
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                string q = "SELECT * FROM Tax";
                DatabaseContext.GetDataFromDB(q, conn, "-----------Tax Report-----------\n", true);
            }
            catch (TaxCalculationException tcex) { Console.WriteLine(tcex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }

        public static void GenerateFinancialRecordReport()
        {
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                string q = "SELECT * FROM FinancialRecord";
                DatabaseContext.GetDataFromDB(q, conn, "-----------Financial Record Report-----------\n", true);
            }
            catch (FinancialRecordException frex) { Console.WriteLine(frex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }
    }
}
