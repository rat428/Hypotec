using System;
using System.Collections.Generic;
using System.Text;

namespace Hypotec.Service.Dto
{
    public class SearchCalculatorDto
    {
        public string RefinanceGoal { get; set; }
        public double CurrentLoanBalanceAmount { get; set; }
        public double DownPayment { get; set; }
        public string LoanTerm { get; set; }
        public double MonthlyPayment { get; set; }
        public double InterestRate { get; set; }
        public string MonthlyHomeownersAssociation { get; set; }
        public double YearlyTaxes { get; set; }
        public double YearlyInsurance { get; set; }
        public int RemainingTerm { get; set; }
        public double CashOut { get; set; }
        public double HomeValue { get; set; }
        public int ZipCode { get; set; }
        public int Fico { get; set; }
        public string includeVAY { get; set; }







    }
}
