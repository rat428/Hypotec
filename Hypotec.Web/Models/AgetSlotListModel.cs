using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Web.Models
{

    public class AgetSlotListModel
    {
        public List<AgentSlotModel> AgentSlotModels { get; set; }
        public SlotBookModel SlotBookModel { get; set; }
    }
}
