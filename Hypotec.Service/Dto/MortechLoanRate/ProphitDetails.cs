using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "profit_detail")]
	public class ProfitDetail
	{

		[XmlElement(ElementName = "planned_profit")]
		public PlannedProfit PlannedProfit { get; set; }

		[XmlElement(ElementName = "corporate_profit")]
		public CorporateProfit CorporateProfit { get; set; }

		[XmlElement(ElementName = "profit_adjustment")]
		public ProfitAdjustment ProfitAdjustment { get; set; }

		[XmlElement(ElementName = "rounding_profit")]
		public RoundingProfit RoundingProfit { get; set; }

		[XmlElement(ElementName = "user_profit")]
		public UserProfit UserProfit { get; set; }

		[XmlElement(ElementName = "loan_officer_compensation")]
		public LoanOfficerCompensation LoanOfficerCompensation { get; set; }

		[XmlElement(ElementName = "lender_paid_ysp")]
		public LenderPaidYsp LenderPaidYsp { get; set; }

		[XmlElement(ElementName = "over_par_addl_profit")]
		public OverParAddlProfit OverParAddlProfit { get; set; }

		[XmlElement(ElementName = "over_par_profit")]
		public OverParProfit OverParProfit { get; set; }

		[XmlElement(ElementName = "over_max_price_addl_profit")]
		public OverMaxPriceAddlProfit OverMaxPriceAddlProfit { get; set; }

		[XmlElement(ElementName = "addl_over_par_borrower_rebate")]
		public AddlOverParBorrowerRebate AddlOverParBorrowerRebate { get; set; }

		[XmlElement(ElementName = "amt_from_borrower")]
		public AmtFromBorrower AmtFromBorrower { get; set; }

		[XmlElement(ElementName = "loan_officer_profit")]
		public LoanOfficerProfit LoanOfficerProfit { get; set; }

		[XmlElement(ElementName = "company_profit")]
		public CompanyProfit CompanyProfit { get; set; }

		[XmlAttribute(AttributeName = "total_profit_percent")]
		public double TotalProfitPercent { get; set; }

		[XmlAttribute(AttributeName = "total_profit_dollar")]
		public double TotalProfitDollar { get; set; }
	}
}
