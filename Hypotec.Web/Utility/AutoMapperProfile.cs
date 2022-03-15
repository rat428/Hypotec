using AutoMapper;
using Hypotec.Data.Entity;
using Hypotec.Service.Dto;
using Hypotec.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // mapping for User module 
            CreateMap<UserModel, AppUserDto>();
            CreateMap<JobsModel, XMLJobs>();
            CreateMap<XMLJobs, JobsModel>();
            CreateMap<ApplicationUser, AppUserDto>();
            CreateMap<AppUserDto, ApplicationUser>();
            CreateMap<XMLResource, ResourceModel>();
            CreateMap<ResourceModel, XMLResource>();
            CreateMap<AdvisorModel, XMLAdvisor>();
            CreateMap<XMLAdvisor, AdvisorModel>();
            CreateMap<AgentSlotModel, XMLAgentSlot>();
            CreateMap<XMLAgentSlot, AgentSlotModel>();
            CreateMap<SlotModel, XMLSlot>();
            CreateMap<XMLSlot, SlotModel>();
            CreateMap<SlotStatusModel, XMLSlotStatus>();
            CreateMap<XMLSlotStatus, SlotStatusModel>();




        }
    }
}
