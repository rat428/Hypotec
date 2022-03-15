using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "planned_costs")]
	public class PlannedCosts
	{

		[XmlAttribute(AttributeName = "costs_percent")]
		public double CostsPercent { get; set; }

		[XmlAttribute(AttributeName = "costs_dollar")]
		public double CostsDollar { get; set; }
	}
}
