using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    internal interface ITaxService
    {
        public void CalculateTax(int employeeId, int taxYear);
        public void GetTaxById(int taxId);
        public decimal GetTaxesForEmployee(int employeeId);
        public void GetTaxesForYear(int taxYear);
    }
}
