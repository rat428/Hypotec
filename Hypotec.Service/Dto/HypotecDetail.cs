using System;
using System.Collections.Generic;
using System.Text;

namespace Hypotec.Service.Dto
{
    public class HypotecDetail
    {
        public string CustomerId { get; set; }
        public string ThirdPartyName { get; set; }
        public string LicenseKey { get; set; }
        public string EmailAddress { get; set; }
        public int RequestId { get; set; }
        public string Url { get; set; }

    }
}
