using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.entity
{
    internal class Payroll
    {
        private int payrollID; // You cannot get or set the value
        private int employeeID;
        private DateTime payPeriodStartDate;
        private DateTime payPeriodEndDate;
        private double basicSalary;
        private double overtimePay;
        private double deductions;
        private double netSalary;

        public int PayrollID { get => payrollID; private set { } } //Can get the value, but cannot set/change it
        public int EmployeeID { get => employeeID; set { employeeID = value; } }
        public DateTime PayPeriodStartDate { get => payPeriodStartDate; set { payPeriodStartDate = value; } }
        public DateTime PayPeriodEndDate { get => payPeriodEndDate; set { payPeriodEndDate = value; } }
        public double BasicSalary { get => basicSalary; set { basicSalary = value; } }
        public double OvertimePay { get => overtimePay; set { overtimePay = value; } }
        public double Deductions { get => deductions; set { deductions = value; } }
        public double NetSalary { get => netSalary; set { netSalary = value; } }

        public Payroll() { }

        public Payroll(int employeeID, DateTime payPeriodStartDate, DateTime payPeriodEndDate, double basicSalary, double overtimePay, double deductions, double netSalary)
        {
            EmployeeID = employeeID;
            PayPeriodStartDate = payPeriodStartDate;
            PayPeriodEndDate = payPeriodEndDate;
            BasicSalary = basicSalary;
            OvertimePay = overtimePay;
            Deductions = deductions;
            NetSalary = netSalary;
        }
    }
}
