using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "fees")]
	public class Fees
	{

		[XmlElement(ElementName = "fee_list")]
		public FeeList FeeList { get; set; }
	}
}
