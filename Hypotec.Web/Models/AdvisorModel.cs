using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class AdvisorModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] LicenseState { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Office { get; set; }
        public string Ext { get; set; }
        public string Text { get; set; }
        public string Call { get; set; }
        public string About { get; set; }
        public string StatusFlag { get; set; }
        public string RegistrationNumber { get; set; }
        public IFormFile Image { get; set; }
        public string Langitude { get; set; }
        public string Latitude { get; set; }
        public List<SelectListItem> lstState { get; set; }
    }
    public class CheckBoxModel
    {
        //Value of checkbox 
        public string Value { get; set; }
        //description of checkbox 
        public string Text { get; set; }
        //whether the checkbox is selected or not
        public bool IsChecked { get; set; }
    }
    public class State
    {
        public string Value { get; set; }

    }
    public class CheckBoxList
    {
        //use CheckBoxModel class as list 
        public List<CheckBoxModel> CheckBoxItems { get; set; }
    }
}
