using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Web.Models
{

    [Serializable]
    [XmlRoot("ResourceList"), XmlType("ResourceList")]
    public class AgentSlotModel
    {
        public string Id { get; set; }
        public string DayName { get; set; }
        public List<SlotModel> Slots { get; set; }
        public List<SlotStatusModel> SlotStatus { get; set; }
        public SlotBookModel SlotBook { get; set; }
    }
}
