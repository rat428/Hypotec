using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Hypotec.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hypotec.Web.Controllers
{
    public class HomeController : Controller
    {
 
        private readonly ILoanRateService _loanRateService;
        public HomeController(ILoanRateService loanRateService)
        {
            _loanRateService = loanRateService;

        }
        /// <summary>
        /// method of Index to open index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Method of IndexRate to get loan rate
        /// </summary>
        /// <param name="searchRateDto"></param>
        /// <returns></returns>
        /// ///
        [HttpPost]
        public async Task<IActionResult> IndexRate(SearchRateDto searchRateDto)
        {
            var loanRateDtos = await _loanRateService.GetIndexRate(searchRateDto).ConfigureAwait(false);
            return Json(loanRateDtos);
        }
        /// <summary>
        /// Privacy
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
