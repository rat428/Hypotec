using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "adjustment_detail")]
	public class AdjustmentDetail
	{

		[XmlAttribute(AttributeName = "desc")]
		public string Desc { get; set; }

		[XmlAttribute(AttributeName = "price_adj")]
		public double PriceAdj { get; set; }

		[XmlAttribute(AttributeName = "rate_adj")]
		public double RateAdj { get; set; }

		[XmlAttribute(AttributeName = "margin_adj")]
		public double MarginAdj { get; set; }

		[XmlAttribute(AttributeName = "applied")]
		public bool Applied { get; set; }
	}
}
