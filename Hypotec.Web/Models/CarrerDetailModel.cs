using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class CarrerDetailModel
    {
       
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
    
        public string PrimaryPhone { get; set; }
   
        public string Email { get; set; }
        public string PostId { get; set; }
  
        public IFormFile Attachment { get; set; }
    }
}
