using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.exception;
using PayXpert.util;

namespace PayXpert.dao
{
    public class PayrollService : IPayrollService
    {
        List<decimal> payrollDetails;
        SqlConnection conn = null!;
        SqlDataReader reader;
        SqlCommand cmd;
        public List<decimal> GeneratePayroll(int employeeID, DateTime startDate, DateTime endDate)
        {
            payrollDetails = new List<decimal>();
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = "SELECT BasicSalary, OvertimePay, Deductions, PayPeriodStartDate, PayPeriodEndDate FROM Payroll WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int payPeriodStartDate = DateTime.Parse(dr.GetValue(3).ToString()).Year;
                    int payPeriodEndDate = DateTime.Parse(dr.GetValue(4).ToString()).Year;
                    int months = payPeriodStartDate == payPeriodEndDate ? 1 : (payPeriodEndDate - payPeriodStartDate);
                    decimal totPay = ((decimal)dr.GetValue(0) * months) + (decimal)dr.GetValue(1) - (decimal)dr.GetValue(2);

                    payrollDetails.Add((decimal)dr.GetValue(0));
                    payrollDetails.Add((decimal)dr.GetValue(1));
                    payrollDetails.Add((decimal)dr.GetValue(2));
                    payrollDetails.Add(totPay);
                }
                dr.Close();
                return payrollDetails;
            }
        }

        public void GetPayrollById(int payrollID)
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Payroll WHERE PayrollID = {payrollID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payroll details for ID: {payrollID}", true);
            }
        }

        public void GetPayrollsForEmployee(int employeeID)
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Payroll WHERE EmployeeID = {employeeID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payrolls for the employee with ID: {employeeID}", true);
            }
        }

        public void GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string sd = startDate.Year.ToString() + '-' + startDate.Month.ToString() + '-' + startDate.Day.ToString();
                string ed = endDate.Year.ToString() + '-' + endDate.Month.ToString() + '-' + endDate.Day.ToString();

                string q = $"SELECT * FROM Payroll where PayPeriodStartDate >= \'{sd}\' and PayPeriodEndDate < \'{ed}\'";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payrolls between {sd} and {ed}", true);
            }
        }

        public decimal GrossSalaryCalculator(int employeeID)
        {
            SqlCommand cmd;
            SqlDataReader dr = null!;
            decimal basicPay, overtimePay;
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string q = $"SELECT BasicSalary, OvertimePay FROM Payroll WHERE EmployeeID = {employeeID}";
                cmd = new SqlCommand(q, conn);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    throw new PayrollGenerationException("Could not get payroll details for the specified employee!");
                }
                dr.Read();
                basicPay = (decimal)dr[0];
                overtimePay = (decimal)dr[1];
                dr.Close();
                return (basicPay * 12) + overtimePay;
            }
        }

        public decimal NetSalaryAfterDeductions(int employeeID)
        {
            SqlConnection conn = null!;
            SqlCommand cmd;
            SqlDataReader dr = null!;
            decimal netSalary = 0, taxAmount = 0;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                string q = $"SELECT SUM(NetSalary) NetSalary, SUM(TaxAmount) TaxAmount FROM Payroll P JOIN Tax T ON P.EmployeeID = T.EmployeeID WHERE P.EmployeeID = {employeeID} GROUP BY P.EmployeeID;";
                cmd = new SqlCommand(q, conn);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    throw new EmployeeNotFoundException("Could not get payroll details for the specified employee!");
                }
                dr.Read();
                netSalary = (decimal)dr[0];
                taxAmount = (decimal)dr[1];
            }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); dr.Close(); }
            return (netSalary * 12) - taxAmount;
        }
    }
}
