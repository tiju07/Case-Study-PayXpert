namespace PayXpert.entity
{
    class Employee
    {
        private int employeeID;
        private string firstName = null!;
        private string lastName = null!;
        private DateTime dateOfBirth;
        private string gender = null!;
        private string email = null!;
        private string phoneNumber = null!;
        private string address = null!;
        private string position = null!;
        private DateTime joiningDate;
        private DateTime? terminationDate;
        
        public int EmployeeID { get => employeeID; private set { } }
        public string FirstName { get => firstName; set { firstName = value; } }
        public string LastName { get => lastName; set { lastName = value; } }
        public DateTime DateOfBirth { get => dateOfBirth; set { dateOfBirth = value; } }
        public string Gender { get => gender; set { gender = value; } }
        public string Email { get => email; set { email = value; } }
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; } }
        public string Address { get => address; set { address = value; } }
        public string Position { get => position; set { position = value; } }
        public DateTime JoiningDate { get => joiningDate; set { joiningDate = value; } }
        public DateTime? TerminationDate { get => terminationDate; set { terminationDate = value; } }

        public Employee() {}

        public Employee(string firstName, string lastName, DateTime dateOfBirth, string gender, string email, string phoneNumber, string address, string position, DateTime joiningDate, DateTime? terminationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Position = position;
            JoiningDate = joiningDate;
            TerminationDate = terminationDate;
        }

        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth, string gender, string email, string phoneNumber, string address, string position, DateTime joiningDate, DateTime? terminationDate)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Position = position;
            JoiningDate = joiningDate;
            TerminationDate = terminationDate;
        }

        public void CalculateAge()
        {
            //double age = DateTime.Now.Subtract(DateOfBirth).TotalHours;
            int age = DateTime.Now.Year - DateOfBirth.Year;
            Console.WriteLine($"The age of this employee is {age} years.");
        }


    }
}
