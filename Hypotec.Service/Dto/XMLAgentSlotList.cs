﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hypotec.Service.Dto
{

    [Serializable]
    [XmlRoot("ResourceList"), XmlType("ResourceList")]
    public class XMLAgentSlotList
    {
        public XMLAgentSlot XMLAgentSlot { get; set; }
    }
}
