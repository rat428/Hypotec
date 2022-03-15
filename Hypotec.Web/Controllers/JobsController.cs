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
    public class JobsController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IJobsService _jobsService;


        public JobsController(IMapper mapper, IJobsService jobsService)
        {
            _jobsService = jobsService;
            _mapper = mapper;

        }
        /// <summary>
        /// method of add AddJobs
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddJobs()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Add AddJobs
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddJobs(JobsModel jobsModel)
        {
            try
            {
                if (jobsModel != null)
                {
                    var jobsDetail = _mapper.Map<XMLJobs>(jobsModel);
                    bool IsSuccess = await _jobsService.SaveCareersJobs(jobsDetail);

                }
                return View();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
