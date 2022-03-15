using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "costs_and_profit")]
	public class CostsAndProfit
	{

		[XmlElement(ElementName = "profit_detail")]
		public ProfitDetail ProfitDetail { get; set; }

		[XmlElement(ElementName = "costs_detail")]
		public CostsDetail CostsDetail { get; set; }

		[XmlAttribute(AttributeName = "profit_table")]
		public string ProfitTable { get; set; }

		[XmlAttribute(AttributeName = "total_cost_profit_dollar")]
		public double TotalCostProfitDollar { get; set; }

		[XmlAttribute(AttributeName = "total_cost_profit_percent")]
		public double TotalCostProfitPercent { get; set; }
	}
}
