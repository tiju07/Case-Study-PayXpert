using System.Data.SqlClient;
using PayXpert.util;
using PayXpert.exception;
using PayXpert.entity;

namespace PayXpert.dao
{
    public class EmployeeService : IEmployeeService
    {
        SqlConnection conn = null!;
        SqlCommand cmd = null!;
        SqlDataReader dr = null!;
        public void AddEmployee(string firstName, string lastName, DateTime dateOfBirth, string gender, string email, string phoneNumber, string address, string designation, DateTime joiningDate, DateTime? terminationDate)
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }

                //bool validationResult;
                ValidationService.AddEmployeeValidation(firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);

                //if (!validationResult) { throw new InvalidInputException("At least one of your inputs was incorrect. Check your data and try again!"); }
                string dob = dateOfBirth.Year.ToString() + "-" + dateOfBirth.Month.ToString() + "-" + dateOfBirth.Day.ToString();
                string jd = joiningDate.Year.ToString() + "-" + joiningDate.Month.ToString() + "-" + joiningDate.Day.ToString();

                var q = "INSERT INTO Employee values (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Designation, @JoiningDate, @TerminationDate)";
                cmd = new SqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@Firstname", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dob);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@JoiningDate", jd);
                if (terminationDate != null)
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", terminationDate.Value.Year.ToString() + "-" + terminationDate.Value.Month.ToString() + "-" + terminationDate.Value.Day.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", DBNull.Value);
                }

                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated > 0) { Console.WriteLine("Employee created successfully!"); }
                else { Console.WriteLine("Error creating employee!"); }
            }
        }

        public void GetAllEmployees()
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                var q = "SELECT * FROM Employee";
                DatabaseContext.GetDataFromDB(q, conn, "Following is the list of all employees:", true);
            }
        }

        public void GetEmployeeById(int employeeID)
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Employee WHERE EmployeeID = {employeeID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following is the details of employee with ID: {employeeID}", true);
            }
        }

        public void RemoveEmployee(int employeeID)
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"DELETE FROM Employee where EmployeeID = {employeeID}";

                cmd = new SqlCommand(q, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) { Console.WriteLine("Deleted successfully!"); }
                else if (rowsAffected == 0) { throw new EmployeeNotFoundException("Could not find any employee with the specific ID!"); }
                else { Console.WriteLine("Could not delete record!"); }
            }
        }

        public void UpdateEmployee(int employeeID, string? firstName, string? lastName, DateTime? dateOfBirth, string? gender, string? email, string? phoneNumber, string? address, string? designation, DateTime? joiningDate, DateTime? terminationDate)
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }

                //bool validationResult;
                ValidationService.UpdateEmployeeValidation(firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);

                //if (!validationResult) { throw new InvalidInputException("You have entered invalid data! Check your data and try again!"); }

                string dob = null!, jd = null!;
                if (dateOfBirth != null)
                {
                    dob = dateOfBirth.Value.Year.ToString() + "-" + dateOfBirth.Value.Month.ToString() + "-" + dateOfBirth.Value.Day.ToString();
                }
                if (joiningDate != null)
                {
                    jd = joiningDate.Value.Year.ToString() + "-" + joiningDate.Value.Month.ToString() + "-" + joiningDate.Value.Day.ToString();
                }
                string q = $"UPDATE Employee SET {(firstName != null ? "FirstName=@FirstName," : "")} {(lastName != null ? "LastName=@LastName," : "")} {(dob != null ? "DateOfBirth=@DOB," : "")} {(gender != null ? "Gender=@Gender," : "")} {(email != null ? "Email=@Email," : "")} {(phoneNumber != null ? "PhoneNumber=@PhoneNumber, " : "")} {(address != null ? "Address=@Address, " : "")}{(designation != null ? "Designation=@Designation," : "")} {(jd != null ? "JoiningDate=@JoiningDate," : "")} {(terminationDate != null ? "TerminationDate=@TerminationDate" : "")} WHERE EmployeeID=@EmployeeID";
                if (!q.Contains("TerminationDate"))
                {
                    q = new string(q.ToCharArray().Reverse().ToArray());
                    int temp = q.IndexOf(',');
                    q = q.Remove(temp, 1);
                    q = new string(q.ToCharArray().Reverse().ToArray());
                }

                //To get to know if the employee exists or not in the database before updating it -> throws EmployeeNotFoundException
                DatabaseContext.GetDataFromDB($"SELECT * FROM Employee WHERE EmployeeID = {employeeID}", conn, "", false);

                cmd = new SqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                if (firstName != null) { cmd.Parameters.AddWithValue("@Firstname", firstName); }

                if (lastName != null) cmd.Parameters.AddWithValue("@LastName", lastName);
                if (dob != null) cmd.Parameters.AddWithValue("@DOB", dob);
                if (gender != null) cmd.Parameters.AddWithValue("@Gender", gender);
                if (email != null) cmd.Parameters.AddWithValue("@Email", email);
                if (phoneNumber != null) cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                if (address != null) cmd.Parameters.AddWithValue("@Address", address);
                if (designation != null) cmd.Parameters.AddWithValue("@Designation", designation);
                if (jd != null) cmd.Parameters.AddWithValue("@JoiningDate", jd);
                if (terminationDate == DateTime.MinValue) { cmd.Parameters.AddWithValue("@TerminationDate", DBNull.Value); }
                else if (terminationDate != null)
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", terminationDate.Value.Year.ToString() + "-" + terminationDate.Value.Month.ToString() + "-" + terminationDate.Value.Day.ToString());
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) { Console.WriteLine("Updated employee details successfully!"); }
                else { Console.WriteLine("Error updating employee details!"); }
            }
        }

        public void CalculateAge(int employeeID)
        {
            using(conn = DBConnUtil.ReturnConnectionObject())
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) throw new DatabaseConnectionException("Could not connect to the DB!");
                string q = $"SELECT * FROM Employee where EmployeeID = {employeeID}";
                cmd = new SqlCommand(q, conn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    Employee emp = null!;
                    while (dr.Read())
                    {
                        emp = new Employee((int)dr[0], (string)dr[1], (string)dr[2], DateTime.Parse(dr[3].ToString()), (string)dr[4], (string)dr[5], (string)dr[6], (string)dr[7], (string)dr[8], DateTime.Parse(dr[9].ToString()), dr[10] != "" ? DateTime.Parse(dr[10].ToString()) : null)
                        {

                        };
                    }
                    emp.CalculateAge();
                }
                else
                {
                    throw new EmployeeNotFoundException("Could not find employee with the specific ID!");
                }
            }
        }
    }
}