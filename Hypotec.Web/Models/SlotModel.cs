using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace Hypotec.Web.Models
{

    [Serializable]
    [XmlRoot("ResourceList"), XmlType("ResourceList")]
    public class SlotModel
    {
        public string Slot { get; set; }
    }
}
