using System;
using System.Collections.Generic;
using System.Text;

namespace Hypotec.Service.Dto
{
    public class LoanRateDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public string VendorProductName { get; set; }
        public string ProductDesc { get; set; }
        public string LastUpdate { get; set; }
        public string Rate { get; set; }
        public double Price { get; set; }
        public string Apr { get; set; }
        public int LoanAmount { get; set; }
        public double FeeAmount { get; set; }
        public double MonthlyPremium { get; set; }
        public double SavingAmount { get; set; }
        public int DownPayment { get; set; }
        public int ProductTerm { get; set; }
        public string YearlyDesc { get; set; }
        public string MonthlyCost { get; set; }
        public string SaveYear { get; set; }
        public string Cashout { get; set; }
        public double MortgageInsurence { get; set; }
        public double VaFunding  { get; set; }
        public double PMI { get; set; }
        public double NetFees { get; set; }
    }
}
