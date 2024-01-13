using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    internal interface IPayrollService
    {
        public List<decimal> GeneratePayroll(int employeeID, DateTime startDate, DateTime endDate);
        public void GetPayrollById(int payrollID);
        public void GetPayrollsForEmployee(int employeeID);
        public void GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
