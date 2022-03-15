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
    public class ResourceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IJobsService _jobsService;


        public ResourceController(IMapper mapper, IJobsService jobsService)
        {
            _jobsService = jobsService;
            _mapper = mapper;

        }
        /// <summary>
        /// methofd of AllResources to get all resource
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AllResources()
        {
            try
            {
                List<ResourceModel> lstResourceModel = new List<ResourceModel>();
                var xMLResourceList = await _jobsService.ResourceList().ConfigureAwait(false);
                lstResourceModel = _mapper.Map<List<ResourceModel>>(xMLResourceList);
                return await Task.Run(() => View(lstResourceModel)).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// method of AddResource 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddResource()
        {
            try
            {
                return await Task.Run(() => View()).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Method of AddResource to save detail in xml file
        /// </summary>
        /// <param name="resourceModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddResource(ResourceModel resourceModel)
        {
            try
            {
                XMLResource xMLResource = new XMLResource();
                xMLResource = _mapper.Map<XMLResource>(resourceModel);
                bool isSuccess = await _jobsService.SaveResource(xMLResource).ConfigureAwait(false);
                ViewBag.Message = isSuccess;
                ModelState.Clear();
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Method of EditResources to get resource by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditResources(string id)
        {
            try
            {
                ResourceModel lstResourceModel = new ResourceModel();
                var xMLResourceList = await _jobsService.GetResourceById(id).ConfigureAwait(false);
                lstResourceModel = _mapper.Map<ResourceModel>(xMLResourceList);
                return await Task.Run(() => View(lstResourceModel)).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// Method of update resource to update detail in xml file
        /// </summary>
        /// <param name="resourceModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateResource(ResourceModel resourceModel)
        {
            try
            {
                XMLResource xMLResource = new XMLResource();
                xMLResource = _mapper.Map<XMLResource>(resourceModel);
                bool isSuccess = await _jobsService.UpdateResource(xMLResource).ConfigureAwait(false);
                ViewBag.Message = isSuccess;
                ModelState.Clear();
                return await Task.Run(() => View("EditResources")).ConfigureAwait(false);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Method of delete resource by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                bool isSuccess = await _jobsService.RemoveResource(id).ConfigureAwait(false);

                ViewBag.Message = isSuccess;

                List<ResourceModel> lstResourceModel = new List<ResourceModel>();
                var xMLResourceList = await _jobsService.ResourceList().ConfigureAwait(false);
                lstResourceModel = _mapper.Map<List<ResourceModel>>(xMLResourceList);
                return await Task.Run(() => View("AllResources", lstResourceModel)).ConfigureAwait(false);


            }

            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Method resource page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Resource()
        {
            try
            {
                List<ResourceModel> lstResourceModel = new List<ResourceModel>();
                var xMLResourceList = await _jobsService.ResourceList().ConfigureAwait(false);
                lstResourceModel = _mapper.Map<List<ResourceModel>>(xMLResourceList);
                return await Task.Run(() => View(lstResourceModel)).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// method of get all reource list
        /// </summary>
        /// <param name="searchArticleModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResourceList(SearchArticleModel searchArticleModel)
        {
            try
            {
                string[] strArray = searchArticleModel.Tag.Split(' ');



                string tag = strArray[0];
                //searchArticleModel.Tag.Remove(searchArticleModel.Tag.Length - 2, 2);



                //array.SkipLast(1).ToArray();
                List<ResourceModel> lstResourceModel = new List<ResourceModel>();
                var xMLResourceList = await _jobsService.ResourceList().ConfigureAwait(false);
                lstResourceModel = _mapper.Map<List<ResourceModel>>(xMLResourceList);
                if (searchArticleModel.Description == null)
                {
                    searchArticleModel.Description = "";
                }
                if (tag == "All")
                {
                    ;
                }
                else
                {
                    lstResourceModel = lstResourceModel.Where(x => x.Tag.Contains(tag)).ToList();
                }
                if (searchArticleModel.Description.Length > 0)
                {
                    lstResourceModel = lstResourceModel.Where(x => x.Description.Contains(searchArticleModel.Description.ToLower()) || x.Description.Contains(searchArticleModel.Description.ToUpper()) || x.Header.Contains(searchArticleModel.Description.ToLower()) || x.Header.Contains(searchArticleModel.Description.ToUpper())).ToList();
                }

                return PartialView("_ResourceList", lstResourceModel);

            }
            catch (Exception)
            {
                throw;
            }
        }

       

        public async Task<IActionResult> Article(string id)
        {
            try
            {
                ResourceModel resourceModel = new ResourceModel();
                List<ResourceModel> lstResourceModel = new List<ResourceModel>();
                var xMLResourceList = await _jobsService.ResourceList().ConfigureAwait(false);
                lstResourceModel = _mapper.Map<List<ResourceModel>>(xMLResourceList);
                if (id.Length > 0)
                {
                    resourceModel = lstResourceModel.Where(x => x.Id.Equals(id)).FirstOrDefault();
                }
                return View(resourceModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
