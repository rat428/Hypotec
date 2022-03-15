using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "header")]
	public class Header
	{

		[XmlElement(ElementName = "errorNum")]
		public int ErrorNum { get; set; }

		[XmlElement(ElementName = "errorDesc")]
		public string ErrorDesc { get; set; }
	}
}
