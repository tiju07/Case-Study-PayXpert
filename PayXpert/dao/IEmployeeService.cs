using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    internal interface IEmployeeService
    {
        public void GetEmployeeById(int employeeId);
        public void GetAllEmployees();
        public void AddEmployee(string firstName, string lastName, DateTime DateOfBirth, string Gender, string Email, string PhoneNumber, string Address, string Designation, DateTime JoiningDate, DateTime? TerminationDate);
        public void UpdateEmployee(int employeeID, string? firstName, string? lastName, DateTime? DateOfBirth, string? Gender, string? Email, string? PhoneNumber, string? Address, string? Designation, DateTime? JoiningDate, DateTime? TerminationDate);
        public void RemoveEmployee(int employeeId);
    }
}
