using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto
{

    [Serializable]
    [XmlRoot("ResourceList"), XmlType("ResourceList")]
    public class XMLAgentSlot
    {
        public string Id { get; set; }
        public string DayName { get; set; }
        public string Flag { get; set; }
        public List<XMLSlot> Slots { get; set; }
        public List<XMLSlotStatus> SlotStatus { get; set; }
    }
}
