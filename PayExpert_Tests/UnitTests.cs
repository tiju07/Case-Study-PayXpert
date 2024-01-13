using PayXpert.dao;
using PayXpert.exception;
namespace PayExpert_Tests
{
    public class UnitTests
    {
        private PayrollService payrollService;
        private EmployeeService employeeService;
        private TaxService taxService;
        [SetUp]
        public void SetUp()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\app.config", AppDomain.CurrentDomain.BaseDirectory));
            payrollService = new PayrollService();
            employeeService = new EmployeeService();
            taxService = new TaxService();

        }
        /*
        [Test]
        public void CheckConfigFile()
        {
            Assert.AreEqual("SERVER=LAPTOP-TMML8LN7;DATABASE=PayExpert;Trusted_Connection=True", ConfigurationSettings.AppSettings["DefaultConnection"]);
            Console.WriteLine(ConfigurationSettings.AppSettings["DefaultConnection"]);
        }
        */
        [Test]
        public void CalculateGrossSalaryForEmployee()
        {
            Assert.That(payrollService.GrossSalaryCalculator(1), Is.EqualTo(321125m));
        }

        [Test]
        public void CalculateNetSalaryAfterDeductions()
        {
            Assert.That(payrollService.NetSalaryAfterDeductions(1), Is.EqualTo(281663m));
        }

        [TestCase(1, 92233)]
        [TestCase(5, 27574.0d)]
        [TestCase(8, 141977.0d)]
        [TestCase(9, 45985.0d)]
        [TestCase(21, 92233.0d)]
        public void VerifyTaxCalculationForHighIncomeEmployee(int ID, decimal expected)
        {
            Assert.That((double)taxService.GetTaxesForEmployee(ID), Is.EqualTo((double)expected));
        }

        DateTime startDate = new DateTime(2022, 5, 12);
        DateTime endDate = new DateTime(2023, 8, 1);
        [TestCase(1, (double)370874)]
        [TestCase(17, (double)1349683)]
        [TestCase(9, (double)1274439m)]
        [TestCase(29, (double)1010939)]
        [TestCase(21, (double)703683)]
        public void ProcessPayrollForMultipleEmployees(int ID, decimal expected)
        {
            List<decimal> lst = payrollService.GeneratePayroll(ID, startDate, endDate);
            Console.WriteLine(lst[3]);
            Assert.That(lst[3], Is.EqualTo((decimal)expected));
        }

        [Test]
        public void VerifyErrorHandlingForInvalidEmployeeData()
        {
            Assert.Throws<Exception>(() => employeeService.AddEmployee("123", "Jackson", new DateTime(2001, 4, 2), "Male", "richardjk@gmail.com", "+919546857413", "P.O. Box 420, 3564 Lacinia Rd.", "Project Lead", new DateTime(2020, 7, 5), null));
        }
    }
}