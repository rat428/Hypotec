using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class JobsModel
    {
        public string Title { get; set; }
     
        public string SubTitle { get; set; }
   
        public string Description { get; set; }


    }
}
