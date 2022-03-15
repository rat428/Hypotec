using Hypotec.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Service.IService
{
    public interface IJobsService
    {
        Task<List<XMLJobs>> CareersJobs();
        Task<bool> SaveCareersJobs(XMLJobs xMLJobs);
        Task<List<XMLResource>> ResourceList();
        Task<List<XMLAgentSlot>> AgentSlotList();
        Task<bool> SaveResource(XMLResource xMLResource);
        Task<bool> UpdateResource(XMLResource xMLResource);
        
        Task<bool> UpdateActivAndDeactiveSlot(string dayTime, string id);
        Task<XMLResource> GetResourceById(string id);
        Task<bool> RemoveResource(string id);
        


    }
}
