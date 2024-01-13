using PayXpert.dao;
using PayXpert.exception;
namespace PayXpert.main
{
    internal class MainModule
    {
        static void Main(string[] args)
        {

            EmployeeService employeeService = new EmployeeService();
            PayrollService payrollService = new PayrollService();
            TaxService taxService = new TaxService();
            FinancialRecordService financialRecordService = new FinancialRecordService();
            bool mainFlag = false;
            int choice;
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
                "11. Calculate Tax for an employee for a specific year\n" +
                "12. Get Taxes for a specific ID\n" +
                "13. Get Taxes for a specific employee\n" +
                "14. Get Taxes for a specific year\n" +
                "15. Add a Financial Record\n" +
                "16. Get a Financial Record by it's ID\n" +
                "17. Get Financial Records for a specific employee\n" +
                "18. Get Financial Records for a specific year\n" +
                "19. Get Gross Salary of an employee\n" +
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
                        employeeService.GetAllEmployees();
                        break;
                    case 3:
                        bool flag = true;
                        do
                        {
                            mainFlag = false;
                            try
                            {
                                Console.Write("\nEnter details for the employee:-");
                                Console.Write("\nEnter First Name: ");
                                string firstName = Console.ReadLine().ToString();
                                Console.Write("\nEnter Last Name: ");
                                string lastName = Console.ReadLine().ToString();
                                DateTime dateOfBirth = DateTime.MaxValue;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Date of Birth in \"YYYY-MM-DD\" format: ");
                                    try
                                    {
                                        dateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                Console.Write("\nEnter gender of the employee: ");
                                string gender = Console.ReadLine();
                                Console.Write("\nEnter Email ID of the employee: ");
                                string email = Console.ReadLine();
                                Console.Write("\nEnter phone number of the employee: ");
                                string phoneNumber = Console.ReadLine();
                                Console.Write("\nEnter address of the employee: ");
                                string address = Console.ReadLine();
                                Console.Write("\nEnter designation of the employee: ");
                                string designation = Console.ReadLine();
                                DateTime joiningDate = DateTime.MaxValue;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Joining Date of the employee in \"YYYY-MM-DD\" format: ");
                                    try
                                    {
                                        joiningDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                DateTime? terminationDate = null;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Termination Date of the employee in \"YYYY-MM-DD\" format, \"-\" if not available: ");
                                    var temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            terminationDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
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
                                    else
                                    {
                                        terminationDate = null;
                                        break;
                                    }
                                } while (flag);
                                employeeService.AddEmployee(firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 4:
                        int employeeID;
                        flag = true;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.WriteLine("\nEnter details to update for the employee(If you do not wish to update a specific information, just type \"-\"):-");
                                Console.Write("Enter the ID of the employee which needs to be updated: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    throw new InvalidInputException("Invalid ID! Please recheck and try again.");
                                }
                                Console.Write("\nEnter First Name: ");
                                string? temp = Console.ReadLine();
                                string? firstName = temp != "-" ? temp : null;
                                Console.Write("\nEnter Last Name: ");
                                temp = Console.ReadLine();
                                string? lastName = temp != "-" ? temp : null;
                                DateTime? dateOfBirth = null;
                                do
                                {
                                    Console.Write("\nEnter Date of Birth of the employee in \"YYYY-MM-DD\" format, \"-\" if no update is required: ");
                                    temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            dateOfBirth = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
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
                                    else { flag = false; }
                                } while (flag);
                                Console.Write("\nEnter Gender of the employee: ");
                                temp = Console.ReadLine();
                                string? gender = temp != "-" ? temp : null;
                                Console.Write("\nEnter Email ID of the employee: ");
                                temp = Console.ReadLine();
                                string? email = temp != "-" ? temp : null;
                                Console.Write("\nEnter Phone Number of the employee: ");
                                temp = Console.ReadLine();
                                string? phoneNumber = temp != "-" ? temp : null;
                                Console.Write("\nEnter Address of the employee: ");
                                temp = Console.ReadLine();
                                string? address = temp != "-" ? temp : null;
                                Console.Write("\nEnter Designation of the employee: ");
                                temp = Console.ReadLine();
                                string? designation = temp != "-" ? temp : null;
                                DateTime? joiningDate = null;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Joining Date of the employee in \"YYYY-MM-DD\" format, \"-\" if no update is required: ");
                                    temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            joiningDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
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
                                    else { flag = false; }
                                } while (flag);
                                DateTime? terminationDate = null;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Termination Date of the employee in \"YYYY-MM-DD\" format, \"-\" if not available: ");
                                    temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            terminationDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
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
                                    else if (temp == "")
                                    {
                                        terminationDate = null;
                                    }
                                    else { flag = false; }
                                } while (flag);
                                employeeService.UpdateEmployee(employeeID, firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 5:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee whose age is to be calculated: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    throw new InvalidDataException("Invalid ID. Please try again.");
                                }
                                employeeService.CalculateAge(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 6:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter ID of employee to delete: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                employeeService.RemoveEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 7:
                        DateTime startDate = DateTime.MinValue;
                        DateTime endDate = DateTime.MaxValue;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter ID of employee whose payroll is to be generated: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter End Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                List<decimal> lst = payrollService.GeneratePayroll(employeeID, startDate, endDate);
                                Console.WriteLine("Count of lst: " + lst.Count);
                                mainFlag = false;
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 8:
                        int payrollID;
                        mainFlag = true;
                        do
                        {
                            try
                            {
                                Console.Write("\nEnter ID of the specific payroll: ");
                                if (!int.TryParse(Console.ReadLine(), out payrollID)) throw new Exception("Invalid Input! Try again.");
                                payrollService.GetPayrollById(payrollID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 9:
                        mainFlag = true;
                        do
                        {
                            try
                            {
                                Console.Write("\nEnter ID of employee to get payrolls of: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                payrollService.GetPayrollsForEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 10:
                        startDate = DateTime.MinValue;
                        endDate = DateTime.MaxValue;
                        mainFlag = true;
                        do
                        {
                            try
                            {
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter End Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                payrollService.GetPayrollsForPeriod(startDate, endDate);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 11:
                        int year;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee: ");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.WriteLine("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID of the employee: ");
                                }
                                Console.Write("\nEnter year to calculate tax: ");
                                while (!int.TryParse(Console.ReadLine(), out year))
                                {
                                    Console.WriteLine("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter year to calculate tax: ");
                                }
                                taxService.CalculateTax(employeeID, year);
                                mainFlag = false;
                            }
                            catch (TaxCalculationException ex) { mainFlag = false; }
                            catch (Exception ex) { Console.WriteLine(ex.Message); mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 12:
                        int taxID;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID to get tax details of: ");
                                while (!int.TryParse(Console.ReadLine(), out taxID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID to get tax details of: ");
                                }
                                taxService.GetTaxById(taxID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 13:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee to get tax details of: ");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID of the employee to get tax details of: ");
                                }
                                taxService.GetTaxesForEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 14:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the year to get tax details of: ");
                                while (!int.TryParse(Console.ReadLine(), out year))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the year to get tax details of: ");
                                }
                                taxService.GetTaxesForYear(year);
                                mainFlag = false;
                            }
                            catch(TaxCalculationException tcex) { mainFlag = false; }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 15:
                        int recordDate;
                        string description;
                        double amount;
                        string recordType;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee: ");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID of the employee: ");
                                }
                                Console.Write("\nEnter the Year of the record: ");
                                while (!int.TryParse(Console.ReadLine(), out recordDate))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
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
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 16:
                        int recordID;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID to get financial record details of: ");
                                while (!int.TryParse(Console.ReadLine(), out recordID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID to get financial record details of: ");
                                }
                                financialRecordService.GetFinancialRecordById(recordID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 17:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee to get financial record details of: ");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the ID of the employee to get financial record details of: ");
                                }
                                financialRecordService.GetFinancialRecordsForEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 18:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the year to get financial record details of: ");
                                while (!int.TryParse(Console.ReadLine(), out year))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                    Console.Write("\nEnter the year to get financial record details of: ");
                                }
                                financialRecordService.GetFinancialRecordsForDate(year);
                                mainFlag = false;
                            }
                            catch (FinancialRecordException frex) { mainFlag = false; }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 19:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee to calcluate gross salary of: ");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.WriteLine("\nInvalid input! Please enter an integer.");
                                    Console.Write("\nEnter the ID of the employee to calcluate gross salary of: ");
                                }
                                decimal grossSalary = payrollService.GrossSalaryCalculator(employeeID);
                                Console.WriteLine($"Gross Salary of Employee with ID {employeeID} is: {grossSalary}");
                                mainFlag = false;
                            }catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
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
    }
}
