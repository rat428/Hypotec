using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "results")]
	public class Results
	{

		[XmlElement(ElementName = "quote")]
		public List<Quote> Quote { get; set; }

		[XmlElement(ElementName = "eligibility")]
		public Eligibility Eligibility { get; set; }

		[XmlAttribute(AttributeName = "index")]
		public int Index { get; set; }

		[XmlAttribute(AttributeName = "product_group_id")]
		public int ProductGroupId { get; set; }

		[XmlAttribute(AttributeName = "product_id")]
		public int ProductId { get; set; }

		[XmlAttribute(AttributeName = "size")]
		public int Size { get; set; }

		[XmlAttribute(AttributeName = "product_name")]
		public string ProductName { get; set; }

		[XmlAttribute(AttributeName = "lockTerm")]
		public int LockTerm { get; set; }

		[XmlAttribute(AttributeName = "termType")]
		public string TermType { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
