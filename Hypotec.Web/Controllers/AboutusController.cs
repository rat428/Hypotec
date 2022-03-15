using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hypotec.Web.Models;
using Hypotec.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Hypotec.Web.Controllers
{
    public class AboutusController : Controller
    {
        private readonly EmailManager _emailManager;

        public AboutusController(EmailManager emailManager)
        {
            _emailManager = emailManager;
        }
        /// <summary>
        /// open to Contact page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Contact()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Send email to freddy
        /// </summary>
        /// <param name="contactMessaageModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Contact(ContactMessaageModel contactMessaageModel)
        {
            bool isSuccess = _emailManager.SendEmailToFreddy(contactMessaageModel);
            ViewBag.Message = isSuccess;
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open to Careers page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Careers()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open to CareerDetails page with post id
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CareerDetails(int postid)
        {
            ViewBag.PostId = postid.ToString();
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// send message to carrer detail
        /// </summary>
        /// <param name="carrerDetailModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CareerDetails(CarrerDetailModel carrerDetailModel)
        {
            bool isSuccess = _emailManager.SendEmailToCarrer(carrerDetailModel);
            ViewBag.PostId = carrerDetailModel.PostId;
            ViewBag.Message = isSuccess;
            ModelState.Clear();
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open to Company page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Company()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open blog page 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Blogs()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open Blog Details
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> BlogDetails()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open Heter Iska Klali
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> HeterIskaKlali()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open CommunicationOptOut
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CommunicationOptOut()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// send email by CommunicationOptOut
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<IActionResult> CommunicationOptOut(CommunicationOptOutModel communicationOptOutModel)
        {
            bool isSuccess = _emailManager.SendEmailToOptOut(communicationOptOutModel);
            ViewBag.Message = isSuccess;
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open AffiliatedBusinesses
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AffiliatedBusinesses()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Open SiteAccessibility
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SiteAccessibility()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open TermsOfUse page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> TermsOfUse()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open EmailAndTextPolicy page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EmailAndTextPolicy()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open SecurityPrivacy page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SecurityPrivacy()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open DisclosuresLicenses page
        /// </summary>
        /// <returns></returns>
        ///      
        public async Task<IActionResult> DisclosuresLicenses()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open PrivacyPolicy page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PrivacyPolicy()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open StateLicensing page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> StateLicensing()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open AgentBooking page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AgentBooking()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open ReviewUs page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ReviewUs()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open BuyersClosingCosts page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> BuyersClosingCosts()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open SellingYourHome page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SellingYourHome()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open HowToLandADeal page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> HowToLandADeal()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open PreApprovedForAMortgage page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PreApprovedForAMortgage()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// open RefinanceYourMortgage page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RefinanceYourMortgage()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// send email to ReviewUsMessage
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReviewUsMessage(ReviewModel reviewModel)
        {
            bool isSuccess = _emailManager.SendMessageReview(reviewModel);
            ViewBag.Message = isSuccess;
            return await Task.Run(() => View("ReviewUs")).ConfigureAwait(false);
        }
        /// <summary>
        /// semd email to ReviewUsContactMessage
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReviewUsContactMessage(ReviewModel reviewModel)
        {
            bool isSuccess = _emailManager.SendContctUsMessage(reviewModel);
            ViewBag.Message = isSuccess;
            return await Task.Run(() => View("ReviewUs")).ConfigureAwait(false);
        }
        /// <summary>
        /// open EmployeeBenifit page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EmployeeBenifit()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
    }
}
