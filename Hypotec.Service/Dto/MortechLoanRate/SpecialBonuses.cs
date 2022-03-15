using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
	[XmlRoot(ElementName = "special_bonuses")]
	public class SpecialBonuses
	{

		[XmlAttribute(AttributeName = "total_special_bonus_adj")]
		public double TotalSpecialBonusAdj { get; set; }
	}
}
