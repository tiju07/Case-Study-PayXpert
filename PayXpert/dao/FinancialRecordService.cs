using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.exception;
using PayXpert.util;

namespace PayXpert.dao
{
    public class FinancialRecordService : IFinancialRecordService
    {
        public void AddFinancialRecord(int employeeId, int recordDate, string description, double amount, string recordType)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }

                //bool validationResul;
                
                ValidationService.AddFinancialRecordValidation(recordDate, description, amount, recordType);

                DatabaseContext.GetDataFromDB($"SELECT * FROM Employee WHERE EmployeeID = {employeeId}", conn, "", false);
                //if (!validationResult) { throw new InvalidInputException("At least one of your inputs was incorrect. Check your data and try again!"); }

                string q = "INSERT INTO FinancialRecord(EmployeeID, RecordDate, Description, Amount, RecordType) VALUES (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@RecordDate", recordDate);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@RecordType", recordType);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) { Console.WriteLine("Record added successfully!"); }
                else { throw new FinancialRecordException("Error adding record!!"); }
            }
            catch (FinancialRecordException frex) { Console.WriteLine(frex.Message); }
            catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.ToString()); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetFinancialRecordById(int recordId)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM FinancialRecord WHERE RecordID={recordId}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the financial records with ID: {recordId}", true);
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message);  }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetFinancialRecordsForDate(int recordDate)
        {
            SqlConnection conn = null!;
            try
            {
                if (recordDate > DateTime.Now.Year) { throw new InvalidInputException("Invalid record date!! Please enter a year less than the current year."); }
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM FinancialRecord WHERE RecordDate={recordDate}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the financial records for the year {recordDate}", true);
            }
            catch (FinancialRecordException frex) { Console.WriteLine(frex.Message); throw new FinancialRecordException(frex.Message); }
            catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetFinancialRecordsForEmployee(int employeeId)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionObject();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM FinancialRecord WHERE EmployeeID={employeeId}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the financial records for the employee with ID: {employeeId}", true);
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (FinancialRecordException frex) { Console.WriteLine(frex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }
    }
}
