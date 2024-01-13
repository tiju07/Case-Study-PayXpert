using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PayXpert.exception;

namespace PayXpert.dao 
{

    internal class ValidationService
    {
        private static List<string> genders = new List<string>() { "Male", "Female", "Others" };
        /// <summary>
        /// Validation for the function AddEmployee in EmployeeService class
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Gender"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Address"></param>
        /// <param name="Designation"></param>
        /// <param name="JoiningDate"></param>
        /// <param name="TerminationDate"></param>
        /// <returns></returns>
        public static bool AddEmployeeValidation(string firstName, string lastName, DateTime DateOfBirth, string Gender, string Email, string PhoneNumber, string Address, string Designation, DateTime JoiningDate, DateTime? TerminationDate)
        {

            List<string> genders = new List<string>() { "Male", "Female", "Others" };

            if (!Regex.IsMatch(firstName, "([a-zA-Z])")) { throw new InvalidInputException("Invalid First Name!");}

            if (!Regex.IsMatch(lastName, "([a-zA-Z])")) { throw new InvalidInputException("Invalid Lase Name!"); }

            if (!genders.Contains(Gender)) { throw new InvalidInputException("Invalid Gender!");}

            if (!Regex.IsMatch(Email, "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$")) { throw new InvalidInputException("Invalid Email!"); }

            if (!Regex.IsMatch(PhoneNumber, "\\+([0-9]{12,})")) { throw new InvalidInputException("Invalid Phone Number!"); }

            if (Address.Length < 3) { throw new InvalidInputException("Invalid Address!"); }
            if (Designation.Length < 2) { throw new InvalidInputException("Invalid Designation!"); }

            return true;
        }

        /// <summary>
        /// Validation for the function UpdateEmployee in the EmployeeService class
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Gender"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Address"></param>
        /// <param name="Designation"></param>
        /// <param name="JoiningDate"></param>
        /// <param name="TerminationDate"></param>
        /// <returns></returns>
        public static bool UpdateEmployeeValidation(string? firstName, string? lastName, DateTime? DateOfBirth, string? Gender, string? Email, string? PhoneNumber, string? Address, string? Designation, DateTime? JoiningDate, DateTime? TerminationDate)
        {
            if (firstName != null && !Regex.IsMatch(firstName, "([a-zA-Z])")) { throw new InvalidInputException("Invalid First Name!"); }

            if (lastName != null && !Regex.IsMatch(lastName, "([a-zA-Z])")) { throw new InvalidInputException("Invalid Lase Name!"); }

            if (Gender != null && !genders.Contains(Gender)) { throw new InvalidInputException("Invalid Gender!"); }

            if (Email != null && !Regex.IsMatch(Email, "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$")) { throw new InvalidInputException("Invalid Email!"); }

            if (PhoneNumber != null && !Regex.IsMatch(PhoneNumber, "\\+([0-9]{12,})")) { throw new InvalidInputException("Invalid Phone Number!"); }

            if (Address != null && Address.Length < 3) { throw new InvalidInputException("Invalid Address!"); }

            if (Designation != null && Designation.Length < 3) { throw new InvalidInputException("Invalid Designation!"); }
            //For the DateOnly variables, the validation will depend on how input is taken.

            return true;
        }

        /// <summary>
        /// Validation for the function AddFinancialRecord in the FinancialRecordService class
        /// </summary>
        /// <param name="recordDate"></param>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public static bool AddFinancialRecordValidation(int recordDate, string description, double amount, string recordType)
        {
            //All permissible record types
            List<string> recordTypes = ["expense", "income", "tax payment"];

            if (recordDate > (int)DateTime.Now.Year) throw new InvalidInputException("Invalid Record Date!"); ;
            if (!Regex.IsMatch(description, "([a-zA-Z]+)([0-9]*)")) throw new InvalidInputException("Invalid Description!");
            if (amount < 0) throw new InvalidInputException("Invalid Amount!"); ;
            if (!recordTypes.Contains(recordType.ToLower())) throw new InvalidInputException("Invalid Record Type!");

            return true;
        }
    }
}