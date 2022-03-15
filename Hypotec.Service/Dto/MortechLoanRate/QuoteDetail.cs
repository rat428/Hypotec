using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto.MortechLoanRate
{
    [XmlRoot(ElementName = "quote_detail")]
    public class QuoteDetail
    {

        [XmlElement(ElementName = "ratesheet_price")]
        public double RatesheetPrice { get; set; }

        [XmlElement(ElementName = "srp")]
        public double Srp { get; set; }

        [XmlElement(ElementName = "intra_day_adjustment")]
        public double IntraDayAdjustment { get; set; }

        [XmlElement(ElementName = "adjustments")]
        public Adjustments Adjustments { get; set; }

        [XmlElement(ElementName = "special_bonuses")]
        public object SpecialBonuses { get; set; }

        [XmlElement(ElementName = "costs_and_profit")]
        public CostsAndProfit CostsAndProfit { get; set; }

        [XmlElement(ElementName = "fees")]
        public Fees Fees { get; set; }

        [XmlAttribute(AttributeName = "rate")]
        public double Rate { get; set; }

        [XmlAttribute(AttributeName = "price")]
        public double Price { get; set; }

        [XmlAttribute(AttributeName = "originationFee")]
        public int OriginationFee { get; set; }

        [XmlAttribute(AttributeName = "apr")]
        public double Apr { get; set; }

        [XmlAttribute(AttributeName = "prepayType")]
        public string PrepayType { get; set; }

        [XmlAttribute(AttributeName = "indexValue")]
        public string IndexValue { get; set; }

        [XmlAttribute(AttributeName = "margin")]
        public double Margin { get; set; }

        [XmlAttribute(AttributeName = "cap1")]
        public int Cap1 { get; set; }

        [XmlAttribute(AttributeName = "cap2")]
        public int Cap2 { get; set; }

        [XmlAttribute(AttributeName = "cap3")]
        public int Cap3 { get; set; }

        [XmlAttribute(AttributeName = "piti")]
        public double Piti { get; set; }

        [XmlAttribute(AttributeName = "lockURL")]
        public string LockURL { get; set; }

        [XmlAttribute(AttributeName = "downPayment")]
        public int DownPayment { get; set; }

        [XmlAttribute(AttributeName = "loanAmount")]
        public int LoanAmount { get; set; }

        [XmlAttribute(AttributeName = "upfrontFee")]
        public double UpfrontFee { get; set; }

        [XmlAttribute(AttributeName = "monthlyPremium")]
        public double MonthlyPremium { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
