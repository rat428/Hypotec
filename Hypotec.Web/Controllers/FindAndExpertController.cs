using AutoMapper;
using Hypotec.Service.IService;
using Hypotec.Web.Models;
using Hypotec.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hypotec.Web.Controllers
{
    public class FindAndExpertController : Controller
    {
        private readonly EmailManager _emailManager;
        private readonly IMapper _mapper;
        private readonly IJobsService _jobsService;
        public FindAndExpertController(EmailManager emailManager, IMapper mapper, IJobsService jobsService)
        {
            _mapper = mapper;
            _jobsService = jobsService;
            _emailManager = emailManager;
        }
        /// <summary>
        /// Method of index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// method of FindExpert page
        /// </summary>
        /// <returns></returns>
        public IActionResult FindExpert()
        {
            return View();
        }
        /// <summary>
        /// method of MortgageAdvisor page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MortgageAdvisor()
        {
            TempData["data1"] = "MortgageAdvisor";
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
        /// method of RealEstateAgent page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RealEstateAgent()
        {
            TempData["data1"] = "RealEstateAgent";
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
        /// methof of agent slot book
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AgentSlot()
        {
            List<AgentSlotModel> lstAdvisorModel = new List<AgentSlotModel>();
            var xMLAdvisorList = await _jobsService.AgentSlotList().ConfigureAwait(false);
            lstAdvisorModel = _mapper.Map<List<AgentSlotModel>>(xMLAdvisorList);

            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// method of AgentSlotBooking to book a call 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AgentSlotBooking()
        {
            try
            {
                List<AgentSlotModel> lstAdvisorModel = new List<AgentSlotModel>();
                var xMLAdvisorList = await _jobsService.AgentSlotList().ConfigureAwait(false);
                lstAdvisorModel = _mapper.Map<List<AgentSlotModel>>(xMLAdvisorList);
                return await Task.Run(() => View(lstAdvisorModel)).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// method of AgentSlotBooking to send mail of book a call
        /// </summary>
        /// <param name="slotBookModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AgentSlotBooking(SlotBookModel slotBookModel)
        {
            try
            {
                string str = TempData["data1"].ToString();
                ViewBag.pageRedirect = TempData["data1"].ToString();
                bool isUpdated = await _jobsService.UpdateActivAndDeactiveSlot(slotBookModel.Time, slotBookModel.Id).ConfigureAwait(false);
                if (isUpdated)
                {
                    bool isSuccess =await _emailManager.SendAgentSlot(slotBookModel).ConfigureAwait(false);
                    TempData["successBody"] = isSuccess;
                }
                if (str == "RealEstateAgent")
                {
                    return Json(new { redirect = Url.Action("RealEstateAgent") });
                }
                else if (str == "Refinance")
                {
                    return Json(new { redirect = Url.Action("Refinance", "LoanRate") });
                }
                else
                {
                    return Json(new { redirect = Url.Action("MortgageAdvisor") });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
