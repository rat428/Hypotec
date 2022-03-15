using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "adjustments")]
	public class Adjustments
	{

		[XmlElement(ElementName = "adjustment_detail")]
		public List<AdjustmentDetail> AdjustmentDetail { get; set; }

		[XmlAttribute(AttributeName = "total_price_adj")]
		public double TotalPriceAdj { get; set; }

		[XmlAttribute(AttributeName = "total_rate_adj")]
		public double TotalRateAdj { get; set; }

		[XmlAttribute(AttributeName = "total_margin_adj")]
		public double TotalMarginAdj { get; set; }
	}
}
