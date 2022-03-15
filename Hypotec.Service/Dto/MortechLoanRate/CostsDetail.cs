using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "costs_detail")]
	public class CostsDetail
	{

		[XmlElement(ElementName = "planned_costs")]
		public PlannedCosts PlannedCosts { get; set; }

		[XmlElement(ElementName = "costs_adjustment")]
		public CostsAdjustment CostsAdjustment { get; set; }

		[XmlAttribute(AttributeName = "total_costs_percent")]
		public double TotalCostsPercent { get; set; }

		[XmlAttribute(AttributeName = "total_costs_dollar")]
		public double TotalCostsDollar { get; set; }
	}
}
