namespace PayXpert.entity
{
    internal class Tax
    {
        private int taxID;
        private int employeeID;
        private int taxYear;
        private double taxableIncome;
        private double taxAmount;

        public int TaxID { get => taxID; private set { } }
        public int EmployeeID { get => employeeID; set { employeeID = value; } }
        public int TaxYear { get => taxYear; set { taxYear = value; } }
        public double TaxableIncome { get => taxableIncome; set { taxableIncome = value; } }
        public double TaxAmount { get => taxAmount; set { taxAmount = value; } }

        public Tax() { }

        public Tax(int employeeID, int taxYear, double taxableIncome, double taxAmount)
        {
            EmployeeID = employeeID;
            TaxYear = taxYear;
            TaxableIncome = taxableIncome;
            TaxAmount = taxAmount;
        }
    }
}
