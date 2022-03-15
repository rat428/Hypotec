using Hypotec.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Service.IService
{
    public interface IRefinanceCalculatorService
    {
        Task<List<LoanRateDto>> GetRefinanceCalculator(SearchCalculatorDto searchCalculatorDto);
    }
}
