using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Hypotec.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hypotec.Web.Controllers
{
    public class LoanRateController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoanRateService _loanRateService;
        public LoanRateController(IMapper mapper, ILoanRateService loanRateService)
        {
            _mapper = mapper;
            _loanRateService = loanRateService;

        }
        /// <summary>
        /// 
        /// get loan rate for Purchase
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Purchase()
        {
            
            return await Task.Run(() => View("Purchase")).ConfigureAwait(false);
        }
        /// <summary>
        /// method of IndexRate
        /// </summary>
        /// <param name="searchRateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> IndexRate(SearchRateDto searchRateDto)
        {
            var loanRateDtos = await _loanRateService.GetIndexRate(searchRateDto).ConfigureAwait(false);
            return Json(loanRateDtos);
        }     
        /// <summary>
        /// get loan rate for Purchase
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Refinance()
        {
            TempData["data1"] = "Refinance";
            if (TempData["successBody"] != null)
            {
                ViewBag.Message = TempData["successBody"];
                return await Task.Run(() => View("Refinance")).ConfigureAwait(false);
            }
            else
            {
                ViewBag.Message = null;
                return await Task.Run(() => View("Refinance")).ConfigureAwait(false);
            }
        
        }
       
    }
}
