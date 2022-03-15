using Hypotec.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Service.IService
{
    public interface IAdvisorService
    {

        Task<List<XMLAdvisor>> AdvisorList();
        Task<bool> SaveAdvisor(XMLAdvisor xMLAdvisor);
        Task<bool> UpdateAdvisor(XMLAdvisor xMLAdvisor);
        Task<XMLAdvisor> GetAdvisorById(string id);
        Task<bool> RemoveAdvisor(string id);
        Task<XMLAdvisor> FindByAdvisorName(string advisorName);
        



    }
}
