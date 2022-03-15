using Hypotec.Service.Dto.MortechLoanRate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto
{
    [System.Xml.Serialization.XmlRoot("mortech")]
    public class Mortech
    {

        [XmlElement(ElementName = "header")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "results")]
        public List<Results> Results { get; set; }
    }

}
