using System.Data.SqlClient;
using PayXpert.exception;

namespace PayXpert.dao
{
    internal class DatabaseContext
    {
        public static void GetDataFromDB(string q, SqlConnection conn, string customMessage, bool displayData)
        {
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            if (!reader.HasRows)
            {
                reader.Close();
                if (q.Contains("FROM Employee")) throw new EmployeeNotFoundException("Could not find employee with the specific ID!");
                else if (q.Contains("FROM Payroll")) throw new PayrollGenerationException("Could not find payroll details!");
                else if (q.Contains("FROM Tax")) throw new TaxCalculationException("Error calculating tax! No data fould!");
                else if (q.Contains("FROM FinancialRecord")) throw new FinancialRecordException("Could not retrieve financial records!");
                else throw new Exception("No data to show!");
            }
            if (displayData)
            {
                Console.WriteLine("\n"+customMessage);

                while (reader.Read())
                {
                    var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        Console.WriteLine(columns[i] + ": " + (data[i] is not DateTime ? data[i] : DateTime.Parse(data[i].ToString()).ToString("yyyy-MM-dd")));
                    }
                    Console.WriteLine(new String('-', 50));
                }
            }
            reader.Close();
        }
    }
}
