using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "fee")]
	public class Fee
	{

		[XmlAttribute(AttributeName = "hudline")]
		public string Hudline { get; set; }

		[XmlAttribute(AttributeName = "description")]
		public string Description { get; set; }

		[XmlAttribute(AttributeName = "feeamount")]
		public double Feeamount { get; set; }

		[XmlAttribute(AttributeName = "prepaid")]
		public bool Prepaid { get; set; }

		[XmlAttribute(AttributeName = "section")]
		public string Section { get; set; }

		[XmlAttribute(AttributeName = "paymenttype")]
		public string Paymenttype { get; set; }
	}
}
