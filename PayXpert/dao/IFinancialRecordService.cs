using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    internal interface IFinancialRecordService
    {
        public void AddFinancialRecord(int employeeId, int recordDate, string description, double amount, string recordType);
        public void GetFinancialRecordById(int recordId);
        public void GetFinancialRecordsForEmployee(int employeeId);
        public void GetFinancialRecordsForDate(int recordDate);
    }
}
