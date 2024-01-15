using PayXpert.dao;
using PayXpert.exception;
namespace PayExpert_Tests
{
    public class UnitTests
    {
        private PayrollService payrollService;
        private EmployeeService employeeService;
        private TaxService taxService;
        private FinancialRecordService financialRecordService;

        [SetUp]
        public void SetUp()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\app.config", AppDomain.CurrentDomain.BaseDirectory));
            payrollService = new PayrollService();
            employeeService = new EmployeeService();
            taxService = new TaxService();
            financialRecordService = new FinancialRecordService();

        }

        [TestCase(1, (double)321125m)]
        [TestCase(12, (double)787879.0m)]
        [TestCase(21, (double)604828m)]
        public void CalculateGrossSalaryForEmployee(int employeeID, double grossSalary)
        {
            Assert.AreEqual((double)payrollService.GrossSalaryCalculator(employeeID), grossSalary);
        }

        [TestCase(1, (double)281663m)]
        [TestCase(12, (double)685174m)]
        [TestCase(21, (double)610775m)]
        public void CalculateNetSalaryAfterDeductions(int employeeID, double netSalary)
        {
            Assert.AreEqual(payrollService.NetSalaryAfterDeductions(employeeID), netSalary);
        }

        [TestCase(1, 92233)]
        [TestCase(5, 27574.0d)]
        [TestCase(8, 141977.0d)]
        [TestCase(9, 45985.0d)]
        [TestCase(21, 92233.0d)]
        public void VerifyTaxCalculationForHighIncomeEmployee(int ID, double expected)
        {
            Assert.AreEqual((double)taxService.GetTaxesForEmployee(ID), expected);
        }

        DateTime startDate = new DateTime(2022, 5, 12);
        DateTime endDate = new DateTime(2023, 8, 1);
        [TestCase(1, 31158.0d)]
        [TestCase(17, 103204.0d)]
        [TestCase(9, 186591.0d)]
        [TestCase(29, 143963.0d)]
        [TestCase(21, 157830.0d)]
        public void ProcessPayrollForMultipleEmployees(int ID, double expected)
        {
            List<decimal> lst = payrollService.GeneratePayroll(ID, startDate, endDate);
            Console.WriteLine(lst[3]);
            Assert.AreEqual((double)lst[3], expected);
        }

        [Test]
        public void VerifyErrorHandlingForInvalidEmployeeData()
        {
            Assert.Throws<InvalidInputException>(() => employeeService.AddEmployee("Richard", "sdfasdf121", new DateTime(2001, 4, 2), "Male", "richardjk@gmail.com", "+919546857413", "P.O. Box 420, 3564 Lacinia Rd.", "Project Lead", new DateTime(2020, 7, 5), null));

            Assert.Throws<InvalidInputException>(() => employeeService.UpdateEmployee(2010, "Richard", "Houston", new DateTime(2001, 4, 2), "Male", "richardjk@gmail.", "+919546857413", "P.O. Box 420, 3564 Lacinia Rd.", "Project Lead", new DateTime(2020, 7, 5), null));

            Assert.Throws<EmployeeNotFoundException>(() => financialRecordService.AddFinancialRecord(1000, 2023, "Desc", 5412, "Income"));
        }

    }
}