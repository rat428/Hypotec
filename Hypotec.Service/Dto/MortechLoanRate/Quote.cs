using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
    [XmlRoot(ElementName = "quote")]
    public class Quote
    {

        [XmlElement(ElementName = "quote_detail")]
        public QuoteDetail QuoteDetail { get; set; }

        [XmlAttribute(AttributeName = "parent_id")]
        public int ParentId { get; set; }

        [XmlAttribute(AttributeName = "product_id")]
        public int ProductId { get; set; }

        [XmlAttribute(AttributeName = "vendor_name")]
        public string VendorName { get; set; }

        [XmlAttribute(AttributeName = "vendor_product_name")]
        public string VendorProductName { get; set; }

        [XmlAttribute(AttributeName = "vendor_product_code")]
        public string VendorProductCode { get; set; }

        [XmlAttribute(AttributeName = "docType")]
        public string DocType { get; set; }

        [XmlAttribute(AttributeName = "creditGrade")]
        public string CreditGrade { get; set; }

        [XmlAttribute(AttributeName = "productDesc")]
        public string ProductDesc { get; set; }

        [XmlAttribute(AttributeName = "productTerm")]
        public int ProductTerm { get; set; }

        [XmlAttribute(AttributeName = "initialArmTerm")]
        public string InitialArmTerm { get; set; }

        [XmlAttribute(AttributeName = "intOnlyMonths")]
        public int IntOnlyMonths { get; set; }

        [XmlAttribute(AttributeName = "ARMIndex")]
        public string ARMIndex { get; set; }

        [XmlAttribute(AttributeName = "pricingStatus")]
        public string PricingStatus { get; set; }

        [XmlAttribute(AttributeName = "lastUpdate")]
        public string LastUpdate { get; set; }

        //[XmlText]
        //public string Text { get; set; }
    }
}
