using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace Hypotec.Web.Models
{

 
    public class SearchArticleModel
    {
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}
