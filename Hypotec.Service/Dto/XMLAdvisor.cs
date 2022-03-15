using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto
{

    [Serializable]
    [XmlRoot("AdvisorList"), XmlType("AdvisorList")]
    public class XMLAdvisor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] LicenseState { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Office { get; set; }
        public string Text { get; set; }
        public string Call { get; set; }
        public string About { get; set; }
        public string Ext { get; set; }
        public string StatusFlag { get; set; }
        public string RegistrationNumber { get; set; }
        
        public IFormFile Image { get; set; }
        public string Langitude { get; set; }
        public string Latitude { get; set; }
    }
}
