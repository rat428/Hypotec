using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "fee_list")]
	public class FeeList
	{

		[XmlElement(ElementName = "fee")]
		public Fee Fee { get; set; }
	}
}
