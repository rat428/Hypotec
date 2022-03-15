using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "eligibility")]
	public class Eligibility
	{

		[XmlElement(ElementName = "eligibilityCheck")]
		public string EligibilityCheck { get; set; }

		[XmlElement(ElementName = "comments")]
		public string Comments { get; set; }

		[XmlElement(ElementName = "productSummaryLink")]
		public string ProductSummaryLink { get; set; }

		[XmlElement(ElementName = "productGuidelineLink")]
		public string ProductGuidelineLink { get; set; }
	}
}
