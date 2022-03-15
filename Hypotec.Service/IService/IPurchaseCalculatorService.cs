using Hypotec.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Service.IService
{
    public interface IPurchaseCalculatorService
    {
        Task<List<LoanRateDto>> GetPurchaseCalculator(SearchCalculatorDto searchCalculatorDto);
    }
}
