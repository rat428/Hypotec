using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class SendMailByAdvisor
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email id is required")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

    }
}
