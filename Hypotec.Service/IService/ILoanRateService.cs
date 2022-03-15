using Hypotec.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Service.IService
{
    public interface ILoanRateService
    {
        Task<List<LoanRateDto>> GetIndexRate(SearchRateDto searchRateDto);
    }
}
