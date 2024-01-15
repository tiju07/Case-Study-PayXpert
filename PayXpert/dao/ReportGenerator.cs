using PayXpert.exception;
using PayXpert.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    public class ReportGenerator
    {
        static SqlConnection conn;
        static SqlCommand cmd;
        static SqlDataReader reader;
        public static void GeneratePayrollReport()
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string q = "SELECT * FROM Payroll";
                DatabaseContext.GetDataFromDB(q, conn, "-----------Payroll Report-----------\n", true);
            }
        }

        public static void GenerateTaxReport()
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string q = "SELECT * FROM Tax";
                DatabaseContext.GetDataFromDB(q, conn, "-----------Tax Report-----------\n", true);
            }
        }

        public static void GenerateFinancialRecordReport()
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string q = "SELECT * FROM FinancialRecord";
                DatabaseContext.GetDataFromDB(q, conn, "-----------Financial Record Report-----------\n", true);
            }
        }

        public static void PayStubGenerator(int employeeID)
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string q = $"select Employee.*, Payroll.*, Tax.TaxAmount from Employee join Payroll on Employee.EmployeeID = Payroll.EmployeeID join Tax on Payroll.EmployeeID = Tax.EmployeeID where Payroll.EmployeeID = {employeeID}";
                cmd = new SqlCommand(q, conn);
                reader = cmd.ExecuteReader();
                var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                if (!reader.HasRows) { throw new PayrollGenerationException("Error generating Pay Stub for the employee!"); }
                Console.WriteLine($"---------------Pay Stub For Employee with ID: {employeeID}---------------");
                Console.WriteLine($"Employee ID: {employeeID}");
                while (reader.Read())
                {
                    var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (columns[i] == "EmployeeID" || columns[i] == "PayrollID") continue;
                        Console.WriteLine(columns[i] + ": " + (data[i] is not DateTime ? data[i] : DateTime.Parse(data[i].ToString()).ToString("yyyy-MM-dd")));
                    }
                }
                PayrollService payrollService = new PayrollService();
                decimal netSalary = payrollService.NetSalaryAfterDeductions(employeeID);
                Console.WriteLine($"Net Salary: {netSalary}");
                Console.WriteLine(new String('-', 50));
            }
        }
    
        public static void IncomeStatementGenerator()
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string qryIncome = "SELECT SUM(Amount) from FinancialRecord WHERE LOWER(RecordType) = 'income' GROUP BY RecordType";
                string qryExpense = "SELECT SUM(Amount) from FinancialRecord WHERE LOWER(RecordType) = 'expense' GROUP BY RecordType";
                string qryTax = "SELECT SUM(Amount) from FinancialRecord WHERE LOWER(RecordType) = 'tax payment' GROUP BY RecordType";

                cmd = new SqlCommand(qryIncome, conn);
                decimal income = (decimal)cmd.ExecuteScalar();
                cmd = new SqlCommand(qryExpense, conn);
                decimal expense = (decimal)cmd.ExecuteScalar();
                cmd = new SqlCommand(qryTax, conn);
                decimal tax = (decimal)cmd.ExecuteScalar();
                Console.WriteLine("------------------Income Statement------------------");
                Console.WriteLine($"Total Income: {income}");
                Console.WriteLine($"Total Expenses(Excluding taxes): {expense}");
                Console.WriteLine($"Total Expenses(Taxes): {tax}");
                Console.WriteLine($"Net Income: {income-expense-tax}");
            }
        }

        public static void TaxSummaryGenerator()
        {
            using (conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                string qryTax = "SELECT COUNT(*) from FinancialRecord WHERE LOWER(RecordType) = 'tax payment' GROUP BY RecordType";

                cmd = new SqlCommand(qryTax, conn);
                int taxRecords = (int)cmd.ExecuteScalar();
                Console.WriteLine("");

                qryTax = "SELECT SUM(Amount) from FinancialRecord WHERE LOWER(RecordType) = 'tax payment' GROUP BY RecordType";
                cmd = new SqlCommand(qryTax, conn);
                decimal tax = (decimal)cmd.ExecuteScalar();

                Console.WriteLine("------------------Tax Summary------------------");
                Console.WriteLine($"Number of tax records: {taxRecords}");
                Console.WriteLine($"Total Taxes: {tax}");
            }
        }
    }
}