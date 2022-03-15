using AutoMapper;
using Hypotec.Data.Data;
using Hypotec.Data.Entity;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hypotec.Service.Service
{
    public class PurchaseCalculatorService : IPurchaseCalculatorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public HypotecDetail _hypotecDetail;
        public PurchaseCalculatorService(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext applicationDbContext, IOptions<HypotecDetail> hypotecDetail)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hypotecDetail = hypotecDetail.Value;
        }
        public async Task<List<LoanRateDto>> GetPurchaseCalculator(SearchCalculatorDto searchCalculatorDto)
        {
            RestClient client = new RestClient();
            client = new RestClient(GetPurchaseManagePaymentRateUrl(searchCalculatorDto));

            List<LoanRateDto> lstloanRateDto = new List<LoanRateDto>();
            List<LoanRateDto> lstloanRateDtoOrderBy = new List<LoanRateDto>();
            List<LoanRateDto> lstloanRateDtoResult = new List<LoanRateDto>();
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            XmlSerializer serializer = new XmlSerializer(typeof(Mortech));
            using (StringReader reader = new StringReader(response.Content))
            {
                var mortecRate = (Mortech)serializer.Deserialize(reader);
                foreach (var quotes in mortecRate.Results)
                {
                    foreach (var quote in quotes.Quote)
                    {
                        LoanRateDto loanRateDto = new LoanRateDto();
                        loanRateDto.MonthlyCost = MonthlyCost(quote.QuoteDetail.LoanAmount, quote.QuoteDetail.Rate, quote.ProductTerm).ToString();
                        loanRateDto.Apr = quote.QuoteDetail.Apr + "%".ToString();
                        loanRateDto.LoanAmount = quote.QuoteDetail.LoanAmount;
                        loanRateDto.DownPayment = quote.QuoteDetail.DownPayment;
                        loanRateDto.Price = quote.QuoteDetail.Price;
                        loanRateDto.ProductName = quotes.ProductName;
                        loanRateDto.ProductId = quotes.ProductId;
                        loanRateDto.ProductDesc = quote.ProductDesc;
                        loanRateDto.VendorName = quote.VendorName;
                        loanRateDto.VendorProductName = quote.VendorProductName;
                        loanRateDto.Rate = quote.QuoteDetail.Rate + "%".ToString();
                        loanRateDto.ProductTerm = quote.ProductTerm;
                        loanRateDto.FeeAmount = quote.QuoteDetail.Fees.FeeList.Fee.Feeamount;
                        loanRateDto.FeeAmount = quote.QuoteDetail.MonthlyPremium;
                        loanRateDto.NetFees = Math.Round(quote.QuoteDetail.UpfrontFee);
                        loanRateDto.SavingAmount = searchCalculatorDto.MonthlyPayment - Convert.ToDouble(loanRateDto.MonthlyCost);
                        lstloanRateDto.Add(loanRateDto);
                    }
                }

                return await Task.Run(() => lstloanRateDto).ConfigureAwait(false);
            }
        }
        private double MonthlyCost(double LoanAmount, double InterestRate, int PaymentPeriods)
        {

            double totalAfterDiscount = LoanAmount - (LoanAmount * 20 / 100);

            int Payment = 0;
            InterestRate = InterestRate / (12 * 100); // one month interest 
            PaymentPeriods = PaymentPeriods * 12; // one month period 
            return Payment = Convert.ToInt32((totalAfterDiscount * InterestRate * Math.Pow(1 + InterestRate, PaymentPeriods)) / (Math.Pow(1 + InterestRate, PaymentPeriods) - 1));
        }
        private string GetPurchaseManagePaymentRateUrl(SearchCalculatorDto searchCalculatorDto)
        {
            string url = _hypotecDetail.Url;

            return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress  + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.downPayment + searchCalculatorDto.DownPayment + HypotecConst.loanProduct1 + searchCalculatorDto.LoanTerm + HypotecConst.insurance + searchCalculatorDto.YearlyInsurance + HypotecConst.taxes + searchCalculatorDto.YearlyTaxes;
        }
    }
}
