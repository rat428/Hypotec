using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "costs_adjustment")]
	public class CostsAdjustment
	{

		[XmlAttribute(AttributeName = "percent_adj")]
		public double PercentAdj { get; set; }

		[XmlAttribute(AttributeName = "dollar_adj")]
		public double DollarAdj { get; set; }
	}
}
