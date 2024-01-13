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
        public List<decimal> GeneratePayroll(int employeeID, DateTime startDate, DateTime endDate)
        {
            payrollDetails = new List<decimal>();
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = "SELECT BasicSalary, OvertimePay, Deductions FROM Payroll WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataReader dr = cmd.ExecuteReader();
                //Console.WriteLine($"Payroll for employee with ID: {employeeID}");

                int months = (endDate - startDate).Days / 30;

                while (dr.Read())
                {
                    decimal totPay = ((decimal)dr.GetValue(0) * months) + (decimal)dr.GetValue(1) - (decimal)dr.GetValue(2);
                    //Console.WriteLine($"Details: \nBasic Salary: {dr.GetValue(0)} \t Overtime Pay: {dr.GetValue(1)} \t Deductions: {dr.GetValue(2)}");
                    //Console.WriteLine($"Total Pay: {totPay}");
                    payrollDetails.Add((decimal)dr.GetValue(0));
                    payrollDetails.Add((decimal)dr.GetValue(1));
                    payrollDetails.Add((decimal)dr.GetValue(2));
                    payrollDetails.Add(totPay);
                    Console.WriteLine($"4th element of list: {payrollDetails[3]}");
                    return payrollDetails;
                }
                dr.Close();
                return payrollDetails;
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
            return payrollDetails;
        }

        public void GetPayrollById(int payrollID)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Payroll WHERE PayrollID = {payrollID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payroll details for ID: {payrollID}", true);
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetPayrollsForEmployee(int employeeID)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Payroll WHERE EmployeeID = {employeeID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payrolls for the employee with ID: {employeeID}", true);
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (EmployeeNotFoundException enfex) { Console.WriteLine(enfex.Message); throw new Exception(enfex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string sd = startDate.Year.ToString() + '-' + startDate.Month.ToString() + '-' + startDate.Day.ToString();
                string ed = endDate.Year.ToString() + '-' + endDate.Month.ToString() + '-' + endDate.Day.ToString();

                string q = $"SELECT * FROM Payroll where PayPeriodStartDate >= \'{sd}\' and PayPeriodEndDate < \'{ed}\'";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payrolls between {sd} and {ed}", true);
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public decimal GrossSalaryCalculator(int employeeID)
        {
            SqlConnection conn = null!;
            SqlCommand cmd;
            SqlDataReader dr = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                string q = $"SELECT BasicSalary, OvertimePay FROM Payroll WHERE EmployeeID = {employeeID}";
                cmd = new SqlCommand(q, conn);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    throw new EmployeeNotFoundException("Could not get payroll details for the specified employee!");
                }
                dr.Read();
                decimal basicPay = (decimal)dr[0];
                decimal overtimePay = (decimal)dr[1];
                return (basicPay * 12) + overtimePay;
            }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(); }
            finally { conn.Close(); dr.Close(); }
        }

        public decimal NetSalaryAfterDeductions(int employeeID)
        {
            SqlConnection conn = null!;
            SqlCommand cmd;
            SqlDataReader dr = null!;

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
                decimal netSalary = (decimal)dr[0];
                decimal taxAmount = (decimal)dr[1];
                return (netSalary * 12) - taxAmount;
            }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(); }
            finally { conn.Close(); dr.Close(); }
        } 
    }
}
