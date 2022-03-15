using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class EmailModel
    {
        public string UserName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
