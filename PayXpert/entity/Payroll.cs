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
        private int payrollID;
        private int employeeID;
        private SqlDateTime payPeriodStartDate;
        private SqlDateTime payPeriodEndDate;
        private double basicSalary;
        private double overtimePay;
        private double deductions;
        private double netSalary;

        public int PayrollID { get => payrollID; private set { } }
        public int EmployeeID { get => employeeID; set { employeeID = value; } }
        public SqlDateTime PayPeriodStartDate { get => payPeriodStartDate; set { payPeriodStartDate = value; } }
        public SqlDateTime PayPeriodEndDate { get => payPeriodEndDate; set { payPeriodEndDate = value; } }
        public double BasicSalary { get => basicSalary; set { basicSalary = value; } }
        public double OvertimePay { get => overtimePay; set { overtimePay = value; } }
        public double Deductions { get => deductions; set { deductions = value; } }
        public double NetSalary { get => netSalary; set { netSalary = value; } }
    }
}
