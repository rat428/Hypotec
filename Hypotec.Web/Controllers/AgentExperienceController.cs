using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hypotec.Service.IService;
using Hypotec.Web.Models;
using Hypotec.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hypotec.Web.Controllers
{
    public class AgentExperienceController : Controller
    {
        private readonly EmailManager _emailManager;
        // GET: AgentExperienceController
        private readonly IMapper _mapper;
        private readonly IJobsService _jobsService;

        public AgentExperienceController(IMapper mapper, IJobsService jobsService, EmailManager emailManager)
        {

            _emailManager = emailManager;
            _mapper = mapper;
            _jobsService = jobsService;

        }
        /// <summary>
        ///  method of AgentExperience page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AgentExperience()
        {

            if (TempData["successBody"] != null)
            {
                ViewBag.Message = TempData["successBody"];
                return await Task.Run(() => View()).ConfigureAwait(false);
            }
            else
            {
                ViewBag.Message = null;
                return await Task.Run(() => View()).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// method of AgentExperience to send mail of slot book
        /// </summary>
        /// <param name="slotBookModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AgentExperience(SlotBookModel slotBookModel)
        {
            bool isSuccess = await Task.Run(() => _emailManager.SendAgentExperience(slotBookModel)).ConfigureAwait(false);
            TempData["successBody"] = isSuccess;
            return Json(new { redirect = Url.Action("AgentExperience") });
        }




    }
}
