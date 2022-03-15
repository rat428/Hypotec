using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Hypotec.Web.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRefinanceCalculatorService _refinanceCalculatorService;
        private readonly IPurchaseCalculatorService _purchaseCalculatorService;
        public CalculatorController(IMapper mapper, IRefinanceCalculatorService refinanceCalculatorService, IPurchaseCalculatorService purchaseCalculatorService)
        {
            _mapper = mapper;
            _purchaseCalculatorService = purchaseCalculatorService;
            _refinanceCalculatorService = refinanceCalculatorService;
        }
        /// <summary>
        /// get loan rate for Purchase
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PurchaseCalculator()
        {
            return await Task.Run(() => View("PurchaseCalculator")).ConfigureAwait(false);
        }
        /// <summary>
        /// method of PurcahseCalculator to get all loan rate
        /// </summary>
        /// <param name="searchCalculatorDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PurcahseCalculator(SearchCalculatorDto searchCalculatorDto)
        {
            var refinanceCalculator = await _purchaseCalculatorService.GetPurchaseCalculator(searchCalculatorDto).ConfigureAwait(false);
            return Json(refinanceCalculator);
        }
        /// <summary>
        /// get loan rate for RefinanceCalculator
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RefinanceCalculator()
        {
            return await Task.Run(() => View("RefinanceCalculator")).ConfigureAwait(false);
        }
        /// <summary>
        /// get loan rate for Purchase
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RefinanceCalculator(SearchCalculatorDto searchCalculatorDto)
        {
            var refinanceCalculator = await _refinanceCalculatorService.GetRefinanceCalculator(searchCalculatorDto).ConfigureAwait(false);
            return Json(refinanceCalculator);
        }
    }
}
