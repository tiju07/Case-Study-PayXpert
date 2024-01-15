using PayXpert.dao;
using PayXpert.exception;
using System.Text.RegularExpressions;
namespace PayXpert.main
{
    internal class MainModule
    {
        static void Main(string[] args)
        {
            try
            {

                EmployeeService employeeService = new EmployeeService();
                PayrollService payrollService = new PayrollService();
                TaxService taxService = new TaxService();
                FinancialRecordService financialRecordService = new FinancialRecordService();
                int choice = int.MinValue;
                do
                {
                    Console.WriteLine("\nFollowing actions are available:\n" +
                    "1. Get an employee using ID\n" +
                    "2. Get all employees\n" +
                    "3. Add en employee\n" +
                    "4. Update an existing employee\n" +
                    "5. Calculate age of an employee\n" +
                    "6. Remove an employee\n" +
                    "7. Generate Payroll for an employee\n" +
                    "8. Get Payroll using a specific ID\n" +
                    "9. Get Payrolls for a specific employee\n" +
                    "10. Get Payrolls for a specific period\n" +
                    "11. Get a report of all Payrolls \n" +
                    "12. Calculate Tax for an employee for a specific year\n" +
                    "13. Get Taxes for a specific ID\n" +
                    "14. Get Taxes for a specific employee\n" +
                    "15. Get Taxes for a specific year\n" +
                    "16. Get a report of all Taxes \n" +
                    "17. Add a Financial Record\n" +
                    "18. Get a Financial Record by it's ID\n" +
                    "19. Get Financial Records for a specific employee\n" +
                    "20. Get Financial Records for a specific year\n" +
                    "21. Get a report of all Financial Records\n" +
                    "22. Get Gross Salary of an employee\n" +
                    "0. Exit the menu");
                    Console.Write("\nEnter your choice: ");
                    while (!int.TryParse(Console.ReadLine(), out choice) && (choice <= 0 || choice >= 18))
                    {
                        Console.WriteLine("Invalid choice. Try again!");
                        Console.Write("\nEnter your choice: ");
                    }
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                Console.Write("\nEnter the ID of the employee: ");
                                int id;
                                while (!int.TryParse(Console.ReadLine(), out id))
                                {
                                    Console.WriteLine("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID of the employee: ");
                                }
                                employeeService.GetEmployeeById(id);

                            }
                            catch (Exception ex) { }
                            break;
                        case 2:
                            try
                            {
                                employeeService.GetAllEmployees();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                            break;
                        case 3:
                            string firstName, lastName, gender, email, phoneNumber, address, designation;
                            DateTime dateOfBirth;
                            DateTime? terminationDate;
                            while (true)
                            {
                                DateTime joiningDate;
                                try
                                {
                                    Console.Write("\nEnter details for the employee:-");
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter First Name: ");
                                            firstName = Console.ReadLine().ToString();
                                            if (!Regex.IsMatch(firstName, "^[a-zA-Z]+$")) { throw new InvalidInputException("Invalid First Name!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter Last Name: ");
                                            lastName = Console.ReadLine().ToString();
                                            if (!Regex.IsMatch(lastName, "^[a-zA-Z]+$")) { throw new InvalidInputException("Invalid Last Name!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    dateOfBirth = DateTime.MaxValue;
                                    while (true)
                                    {
                                        Console.Write("\nEnter Date of Birth in \"YYYY-MM-DD\" format: ");
                                        try
                                        {
                                            dateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                            break;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format or invalid! Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }

                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter gender of the employee: ");
                                            gender = Console.ReadLine();
                                            if (gender != "Male" && gender != "Female" && gender != "Others") { throw new InvalidInputException("Invalid Gender!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter Email ID of the employee: ");
                                            email = Console.ReadLine();
                                            if (!Regex.IsMatch(email, "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$")) { throw new InvalidInputException("Invalid Email!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter phone number of the employee: ");
                                            phoneNumber = Console.ReadLine();
                                            if (!Regex.IsMatch(phoneNumber, "\\+([0-9]{12,})")) { throw new InvalidInputException("Invalid Phone Number!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter address of the employee: ");
                                            address = Console.ReadLine();
                                            if (address.Length < 3) { throw new InvalidInputException("Invalid Address!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter designation of the employee: ");
                                            designation = Console.ReadLine();
                                            if (designation.Length < 2) { throw new InvalidInputException("Invalid First Name!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }

                                    joiningDate = DateTime.MaxValue;
                                    while (true)
                                    {
                                        Console.Write("\nEnter Joining Date of the employee in \"YYYY-MM-DD\" format: ");
                                        try
                                        {
                                            joiningDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                            break;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    terminationDate = null;
                                    while (true)
                                    {
                                        Console.Write("\nEnter Termination Date of the employee in \"YYYY-MM-DD\" format, \"-\" if not available: ");
                                        var temp = Console.ReadLine();
                                        if (temp != "-")
                                        {
                                            try
                                            {
                                                terminationDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                                break;
                                            }
                                            catch (ArgumentNullException anex)
                                            {
                                                Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                            }
                                            catch (FormatException fex)
                                            {
                                                Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                            }
                                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                                        }
                                        else
                                        {
                                            terminationDate = null;
                                            break;
                                        }
                                    }
                                    employeeService.AddEmployee(firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);
                                    break;
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            break;
                        case 4:
                            int employeeID;
                            while(true)
                            {
                                DateTime? joiningDate;
                                try
                                {
                                    Console.WriteLine("\nEnter details to update for the employee(If you do not wish to update a specific information, just type \"-\"):-");
                                    Console.Write("Enter the ID of the employee which needs to be updated: ");
                                    while (int.TryParse(Console.ReadLine(), out employeeID) && !ValidationService.EmployeeIDIsValid(employeeID))
                                    {
                                        throw new InvalidInputException("Invalid ID! Please recheck and try again.");
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter First Name: ");
                                            firstName = Console.ReadLine().ToString();
                                            if (firstName == "-") { firstName = null; break; }
                                            if (!Regex.IsMatch(firstName, "^[a-zA-Z]+$")) { throw new InvalidInputException("Invalid First Name!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }

                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter Last Name: ");
                                            lastName = Console.ReadLine().ToString();
                                            if (lastName == "-") { lastName = null; break; }
                                            if (!Regex.IsMatch(lastName, "^[a-zA-Z]+$")) { throw new InvalidInputException("Invalid Last Name!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    dateOfBirth = DateTime.MaxValue;
                                    while(true)
                                    {
                                        Console.Write("\nEnter Date of Birth of the employee in \"YYYY-MM-DD\" format, \"-\" if no update is required: ");
                                        string temp = Console.ReadLine();
                                        if (temp != "-")
                                        {
                                            try
                                            {
                                                dateOfBirth = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                                break;
                                            }
                                            catch (ArgumentNullException anex)
                                            {
                                                Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                            }
                                            catch (FormatException fex)
                                            {
                                                Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                            }
                                        }
                                        else { break; }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter Gender: ");
                                            gender = Console.ReadLine().ToString();
                                            if (gender == "-") { gender = null; break; }
                                            if (gender != "Male" && gender != "Female" && gender != "Others") { throw new InvalidInputException("Invalid Gender!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter Email: ");
                                            email = Console.ReadLine().ToString();
                                            if (email == "-") { email = null; break; }
                                            if (!Regex.IsMatch(email, "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$")) { throw new InvalidInputException("Invalid Gender!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter phone number of the employee: ");
                                            phoneNumber = Console.ReadLine();
                                            if (phoneNumber == "-") {  phoneNumber = null; break; }
                                            if (!Regex.IsMatch(phoneNumber, "\\+([0-9]{12,})")) { throw new InvalidInputException("Invalid Phone Number!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter address of the employee: ");
                                            address = Console.ReadLine();
                                            if (address == "-") {  address = null; break; }
                                            if (address.Length < 3) { throw new InvalidInputException("Invalid Address!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("\nEnter designation of the employee: ");
                                            designation = Console.ReadLine();
                                            if (designation == "-") {  designation = null; break; }
                                            if (designation.Length < 2) { throw new InvalidInputException("Invalid First Name!"); }
                                            break;
                                        }
                                        catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    }
                                    joiningDate = null;
                                    while(true)
                                    {
                                        Console.Write("\nEnter Joining Date of the employee in \"YYYY-MM-DD\" format, \"-\" if no update is required: ");
                                        string temp = Console.ReadLine();
                                        if (temp != "-")
                                        {
                                            try
                                            {
                                                joiningDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                                break;
                                            }
                                            catch (ArgumentNullException anex)
                                            {
                                                Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                            }
                                            catch (FormatException fex)
                                            {
                                                Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                            }
                                        }
                                        else { break; }
                                    }

                                    terminationDate = null;
                                    while (true)
                                    {
                                        Console.Write("\nEnter Termination Date of the employee in \"YYYY-MM-DD\" format, \"-\" if not available, empty to set it to null: ");
                                        string temp = Console.ReadLine();
                                        if (temp == "")
                                        {
                                            terminationDate = DateTime.MinValue;
                                            break;
                                        }
                                        else if (temp != "-")
                                        {
                                            try
                                            {
                                                terminationDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                                break;
                                            }
                                            catch (ArgumentNullException anex)
                                            {
                                                Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                            }
                                            catch (FormatException fex)
                                            {
                                                Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                            }
                                        }
                                        else { terminationDate = null; break; }
                                    }
                                    employeeService.UpdateEmployee(employeeID, firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);
                                    break;
                                }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 5:
                            while(true)
                            {
                                try
                                {
                                    Console.Write("\nEnter the ID of the employee whose age is to be calculated: ");
                                    if (!int.TryParse(Console.ReadLine(), out employeeID))
                                    {
                                        throw new InvalidDataException("Invalid ID. Please try again.");
                                    }
                                    employeeService.CalculateAge(employeeID);
                                    break;
                                }catch(InvalidDataException idex) { Console.WriteLine(idex.Message); }
                                catch (Exception ex) { Console.WriteLine(ex.Message);  }
                            }
                            break;
                        case 6:
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter ID of employee to delete: ");
                                    if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new InvalidInputException("Invalid Input! Try again.");
                                    employeeService.RemoveEmployee(employeeID);
                                    break;
                                }catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                catch (Exception ex) { }
                            }
                            break;
                        case 7:
                            DateTime startDate = DateTime.MinValue;
                            DateTime endDate = DateTime.MaxValue;
                            while(true)
                            {
                                try
                                {
                                    Console.Write("\nEnter ID of employee whose payroll is to be generated: ");
                                    if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    while(true)
                                    {
                                        Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                        try
                                        {
                                            startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                            break;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    while(true)
                                    {
                                        Console.Write("\nEnter End Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                        try
                                        {
                                            endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                            break;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    List<decimal> lst = payrollService.GeneratePayroll(employeeID, startDate, endDate);
                                    if(lst.Count == 0) { throw new PayrollGenerationException("Could not generate payroll for the given details! No data found!"); }
                                    Console.WriteLine($"Basic Pay: {lst[0]}\nOvertime Pay: {lst[1]}\nDeductions : {lst[2]}\nNet Salary: {lst[3]}");
                                    break;
                                }catch (EmployeeNotFoundException enfex) { }
                                catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message);  }
                            }
                            break;
                        case 8:
                            int payrollID;
                            
                            while(true)
                            {
                                try
                                {
                                    Console.Write("\nEnter ID of the specific payroll: ");
                                    if (!int.TryParse(Console.ReadLine(), out payrollID)) throw new Exception("Invalid Input! Try again.");
                                    payrollService.GetPayrollById(payrollID);
                                    break;
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message);  }
                            }
                            break;
                        case 9:
                            
                            while(true)
                            {
                                try
                                {
                                    Console.Write("\nEnter ID of employee to get payrolls of: ");
                                    if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new InvalidInputException("Invalid Input! Try again.");
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    payrollService.GetPayrollsForEmployee(employeeID);
                                    break;
                                }catch (EmployeeNotFoundException enfex) { }
                                catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            break;
                        case 10:
                            startDate = DateTime.MinValue;
                            endDate = DateTime.MaxValue;
                            while(true)
                            {
                                try
                                {
                                    while(true)
                                    {
                                        Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                        try
                                        {
                                            startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                            break;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    
                                    while(true)
                                    {
                                        Console.Write("\nEnter End Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                        try
                                        {
                                            endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                            break;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    payrollService.GetPayrollsForPeriod(startDate, endDate);
                                    break;
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            break;
                        case 11:
                            try
                            {
                                ReportGenerator.GeneratePayrollReport();
                            }catch (Exception ex) { Console.WriteLine(ex.Message); }
                            break;
                        case 12:
                            int year;
                            while(true)
                            {

                                try
                                {
                                    Console.Write("\nEnter the ID of the employee: ");
                                    while (!int.TryParse(Console.ReadLine(), out employeeID))
                                    {
                                        Console.WriteLine("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the ID of the employee: ");
                                    }
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    Console.Write("\nEnter year to calculate tax: ");
                                    while (!int.TryParse(Console.ReadLine(), out year))
                                    {
                                        Console.WriteLine("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter year to calculate tax: ");
                                    }
                                    taxService.CalculateTax(employeeID, year);
                                    break;
                                }catch (EmployeeNotFoundException enfex) { }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            break;
                        case 13:
                            int taxID;
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the ID to get tax details of: ");
                                    while (!int.TryParse(Console.ReadLine(), out taxID))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the ID to get tax details of: ");
                                    }
                                    taxService.GetTaxById(taxID);
                                    break;
                                }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 14:
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the ID of the employee to get tax details of: ");
                                    while (!int.TryParse(Console.ReadLine(), out employeeID))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the ID of the employee to get tax details of: ");
                                    }
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    taxService.GetTaxesForEmployee(employeeID);
                                    break;
                                }catch (EmployeeNotFoundException enfex) { }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 15:
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the year to get tax details of: ");
                                    while (!int.TryParse(Console.ReadLine(), out year))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the year to get tax details of: ");
                                    }
                                    taxService.GetTaxesForYear(year);
                                    break;
                                }
                                catch (TaxCalculationException tcex) { break; }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 16:
                            try
                            {
                                ReportGenerator.GenerateTaxReport();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                            break;
                        case 17:
                            int recordDate = DateTime.MaxValue.Year;
                            string description;
                            double amount;
                            string recordType;
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the ID of the employee: ");
                                    while (!int.TryParse(Console.ReadLine(), out employeeID))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the ID of the employee: ");
                                    }
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    Console.Write("\nEnter the Year of the record: ");
                                    while (int.TryParse(Console.ReadLine(), out recordDate) && recordDate > DateTime.Now.Year)
                                    {
                                        Console.Write("\nWrong entry!");
                                        Console.Write("\nEnter the Year of the record: ");
                                    }
                                    Console.Write("\nEnter a Description for the record: ");
                                    description = Console.ReadLine();
                                    Console.Write("\nEnter an Amount for the financial record: ");
                                    while (!double.TryParse(Console.ReadLine(), out amount))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts numeric inputs.");
                                        Console.Write("\nEnter an Amount for the financial record: ");
                                    }
                                    Console.Write("\nEnter the Type of the record: ");
                                    recordType = Console.ReadLine();
                                    financialRecordService.AddFinancialRecord(employeeID, recordDate, description, amount, recordType);
                                    break;
                                }catch (EmployeeNotFoundException enfex) { }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 18:
                            int recordID;
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the ID to get financial record details of: ");
                                    while (!int.TryParse(Console.ReadLine(), out recordID))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the ID to get financial record details of: ");
                                    }
                                    financialRecordService.GetFinancialRecordById(recordID);
                                    break;
                                }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 19:
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the ID of the employee to get financial record details of: ");
                                    while (!int.TryParse(Console.ReadLine(), out employeeID))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the ID of the employee to get financial record details of: ");
                                    }
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    financialRecordService.GetFinancialRecordsForEmployee(employeeID);
                                    break;
                                }catch (EmployeeNotFoundException enfex) { }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 20:
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the year to get financial record details of: ");
                                    while (!int.TryParse(Console.ReadLine(), out year))
                                    {
                                        Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                        Console.Write("\nEnter the year to get financial record details of: ");
                                    }
                                    financialRecordService.GetFinancialRecordsForDate(year);
                                    break;
                                }
                                catch (FinancialRecordException frex) { break; }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 21:
                            try
                            {
                                ReportGenerator.GenerateFinancialRecordReport();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                            break;
                        case 22:
                            while(true)
                            {
                                
                                try
                                {
                                    Console.Write("\nEnter the ID of the employee to calcluate gross salary of: ");
                                    while (!int.TryParse(Console.ReadLine(), out employeeID))
                                    {
                                        Console.WriteLine("\nInvalid input! Please enter an integer.");
                                        Console.Write("\nEnter the ID of the employee to calcluate gross salary of: ");
                                    }
                                    ValidationService.EmployeeIDIsValid(employeeID);
                                    decimal grossSalary = payrollService.GrossSalaryCalculator(employeeID);
                                    Console.WriteLine($"Gross Salary of Employee with ID {employeeID} is: {grossSalary}");
                                    break;
                                }
                                catch (Exception ex) {  }
                            }
                            break;
                        case 0:
                            Console.Write("\nExiting...");
                            break;
                        default:
                            Console.Write("\nInvalid choice. Please enter a choice in integer format between 0 and 18.");
                            break;
                    }
                    Thread.Sleep(2000);
                } while (choice != 0);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
