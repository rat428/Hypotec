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
    public class RefinanceCalculatorService : IRefinanceCalculatorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public HypotecDetail _hypotecDetail;
        public RefinanceCalculatorService(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext applicationDbContext, IOptions<HypotecDetail> hypotecDetail)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hypotecDetail = hypotecDetail.Value;
        }
        public async Task<List<LoanRateDto>> GetRefinanceCalculator(SearchCalculatorDto searchCalculatorDto)
        {
            RestClient client = new RestClient();
            client = new RestClient(GetRefinanceManagePaymentRateUrl(searchCalculatorDto));

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
                        loanRateDto.NetFees = Math.Round(quote.QuoteDetail.UpfrontFee,2);
                        loanRateDto.SavingAmount = searchCalculatorDto.MonthlyPayment - MonthlyCost(quote.QuoteDetail.LoanAmount, quote.QuoteDetail.Rate, quote.ProductTerm);
                        if (searchCalculatorDto.RefinanceGoal == "ShortenTerm")
                        {
                            if (searchCalculatorDto.RemainingTerm > 15)
                            {
                                int year = 0;
                                if (quotes.ProductName == "Conf 15 Yr  Fixed ")
                                {
                                    year = searchCalculatorDto.RemainingTerm - 15;
                                    loanRateDto.SaveYear = year.ToString();
                                    loanRateDto.ProductDesc = "CONVENTIONAL 15 YR FIXED";
                                }
                                if (quotes.ProductName == "Conf 10 Yr  Fixed ")
                                {
                                    year = searchCalculatorDto.RemainingTerm - 10;
                                    loanRateDto.SaveYear = year.ToString();
                                    loanRateDto.ProductDesc = "CONVENTIONAL 10 YR FIXED";
                                }
                                if (quotes.ProductName == "Govt FHA 15 Yr  Fixed ")
                                {
                                    year = searchCalculatorDto.RemainingTerm - 15;
                                    loanRateDto.SaveYear = year.ToString();
                                    loanRateDto.ProductDesc = "FHA 15 YR FIXED";
                                    loanRateDto.MortgageInsurence = Math.Round((1.75 / 100) * quote.QuoteDetail.LoanAmount,2);
                                   
                                    //double mi = quote.QuoteDetail.LoanAmount / 15;
                                    loanRateDto.PMI = Math.Round((.85 / 100) * (quote.QuoteDetail.LoanAmount / 12),2);

                                }
                                if (quotes.ProductName == "Govt VA 15 Yr  Fixed ")
                                {
                                    year = searchCalculatorDto.RemainingTerm - 15;
                                    loanRateDto.SaveYear = year.ToString();
                                    loanRateDto.ProductDesc = "VA 15 YR FIXED";
                                    loanRateDto.VaFunding = Math.Round((3.6 / 100) * quote.QuoteDetail.LoanAmount, 2);
                                }


                            }
                        }
                        if (searchCalculatorDto.RefinanceGoal == "LowerPayment")
                        {
                            if (quotes.ProductName == "Govt VA 30 Yr  Fixed ")
                                loanRateDto.VaFunding = Math.Round((3.6 / 100) * quote.QuoteDetail.LoanAmount, 2);
                            {
                                loanRateDto.ProductDesc = " VA 30 YR FIXED";
                            }
                            if (quotes.ProductName == "Conf 30 Yr  Fixed ")
                            {
                                loanRateDto.ProductDesc = "CONVENTIONAL 30 YR FIXED";
                            }
                            if (quotes.ProductName == "Govt VA 30 Yr UST ARM 5/1")
                            {
                                loanRateDto.VaFunding = Math.Round((3.6 / 100) * quote.QuoteDetail.LoanAmount, 2);
                                loanRateDto.ProductDesc = "VA 5 YR ARM";
                            }
                            if (quotes.ProductName == "Conf 30 Yr SOFR ARM 5 Yr Fixed/6 Mo")
                            {
                                loanRateDto.ProductDesc = "5 YR ARM";
                            }
                            if (quotes.ProductName == "Govt FHA 30 Yr  Fixed ")
                            {
                                loanRateDto.ProductDesc = "FHA 30 YR FIXED";
                                loanRateDto.MortgageInsurence = Math.Round((1.75 / 100) * quote.QuoteDetail.LoanAmount, 2);

                                //double mi = quote.QuoteDetail.LoanAmount / 15;
                                loanRateDto.PMI = Math.Round((.85 / 100) * (quote.QuoteDetail.LoanAmount / 12), 2);
                            }
                        }
                        if (searchCalculatorDto.RefinanceGoal == "CashOut")
                        {
                            loanRateDto.Cashout = searchCalculatorDto.CashOut.ToString();
                            if (quotes.ProductName == "Govt VA 30 Yr  Fixed ")
                            {
                                loanRateDto.VaFunding = Math.Round((3.6 / 100) * quote.QuoteDetail.LoanAmount, 2);
                                loanRateDto.ProductDesc = " VA 30 YR FIXED";
                            }
                            if (quotes.ProductName == "Conf 30 Yr  Fixed ")
                            {
                                loanRateDto.ProductDesc = "CONVENTIONAL 30 YR FIXED";
                            }
                            if (quotes.ProductName == "Govt VA 30 Yr UST ARM 5/1")
                            {
                                loanRateDto.VaFunding = Math.Round((3.6 / 100) * quote.QuoteDetail.LoanAmount, 2);
                                loanRateDto.ProductDesc = "VA 5 YR ARM";
                            }
                            if (quotes.ProductName == "Conf 30 Yr SOFR ARM 5 Yr Fixed/6 Mo")
                            {
                                loanRateDto.ProductDesc = "5 YR ARM";
                            }
                            if (quotes.ProductName == "Govt FHA 30 Yr  Fixed ")
                            {
                                loanRateDto.ProductDesc = "FHA 30 YR FIXED";
                                loanRateDto.MortgageInsurence = Math.Round((1.75 / 100) * quote.QuoteDetail.LoanAmount, 2);

                                //double mi = quote.QuoteDetail.LoanAmount / 15;
                                loanRateDto.PMI = Math.Round((.85 / 100) * (quote.QuoteDetail.LoanAmount / 12), 2);
                            }
                            if (quotes.ProductName == "Govt FHA 30 Yr UST ARM 5/1")
                            {
                                loanRateDto.ProductDesc = "FHA 5 YR ARM";
                                loanRateDto.MortgageInsurence = Math.Round((1.75 / 100) * quote.QuoteDetail.LoanAmount, 2);

                                //double mi = quote.QuoteDetail.LoanAmount / 15;
                                loanRateDto.PMI = Math.Round((.85 / 100) * (quote.QuoteDetail.LoanAmount / 12), 2);
                            }

                        }
                        lstloanRateDto.Add(loanRateDto);
                    }
                }
                return await Task.Run(() => lstloanRateDto).ConfigureAwait(false);
            }
        }
        private double MonthlyCost(double LoanAmount, double InterestRate, int PaymentPeriods)
        {

            double totalAfterDiscount = LoanAmount;

            int Payment = 0;
            InterestRate = InterestRate / (12 * 100); // one month interest 
            PaymentPeriods = PaymentPeriods * 12; // one month period 
            return Payment = Convert.ToInt32((totalAfterDiscount * InterestRate * Math.Pow(1 + InterestRate, PaymentPeriods)) / (Math.Pow(1 + InterestRate, PaymentPeriods) - 1));
        }
        //private double MonthlyCost(double LoanAmount, double InterestRate, int PaymentPeriods, double DownPayment)
        //{

        //    double totalAfterDiscount = LoanAmount ;//(LoanAmount * 20 / 100);

        //    int Payment = 0;
        //    InterestRate = InterestRate / (12 * 100); // one month interest 
        //    PaymentPeriods = PaymentPeriods * 12; // one month period 
        //    return Payment = Convert.ToInt32((totalAfterDiscount * InterestRate * Math.Pow(1 + InterestRate, PaymentPeriods)) / (Math.Pow(1 + InterestRate, PaymentPeriods) - 1));
        //}
        private string GetRefinanceManagePaymentRateUrl(SearchCalculatorDto searchCalculatorDto)
        {
            string url = _hypotecDetail.Url;
            if (searchCalculatorDto.RefinanceGoal == "LowerPayment")
            {
                if (searchCalculatorDto.YearlyInsurance > 0 && searchCalculatorDto.YearlyTaxes > 0 && searchCalculatorDto.includeVAY == "true")
                {
                    return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + 1 + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.pmi + searchCalculatorDto.MonthlyPayment + HypotecConst.vaType + 1 + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + HypotecConst.insurance + searchCalculatorDto.YearlyInsurance + HypotecConst.taxes + searchCalculatorDto.YearlyTaxes + "&parent_id=" + "2297" + "&productList=26,4,418";
                }
                if (searchCalculatorDto.includeVAY == "true" && searchCalculatorDto.YearlyInsurance <= 0 && searchCalculatorDto.YearlyTaxes <= 0)
                {
                    return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + 1 + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.pmi + searchCalculatorDto.MonthlyPayment + HypotecConst.vaType + 1 + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + "&parent_id=" + "2297" + "&productList=26,4,418";
                }
                if (searchCalculatorDto.YearlyInsurance > 0 && searchCalculatorDto.YearlyTaxes > 0 && searchCalculatorDto.includeVAY == "false")
                {
                    return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + 1 + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.pmi + searchCalculatorDto.MonthlyPayment + HypotecConst.vaType + 1 + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + HypotecConst.insurance + searchCalculatorDto.YearlyInsurance + HypotecConst.taxes + searchCalculatorDto.YearlyTaxes + "&parent_id=" + "2297" + "&productList=4,2653,23";
                }
                if (searchCalculatorDto.includeVAY == "false" && searchCalculatorDto.YearlyInsurance <= 0 && searchCalculatorDto.YearlyTaxes <= 0)
                {
                    return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + 1 + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.pmi + searchCalculatorDto.MonthlyPayment + HypotecConst.vaType + 1 + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + "&parent_id=" + "2297" + "&productList=4,2653,23";
                }

            }
            else if (searchCalculatorDto.RefinanceGoal == "ShortenTerm")
            {

                if (searchCalculatorDto.RemainingTerm > 15)
                {
                    if (searchCalculatorDto.includeVAY == "true")
                    {
                        return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + "&parent_id=" + "2297" + "&productList=25,1,22" + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + HypotecConst.loanPurpose + "1";
                    }
                    else
                    {
                        return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + "&parent_id=" + "2297" + "&productList=1,2,22" + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + HypotecConst.loanPurpose + "1";
                    }
                }
                if(searchCalculatorDto.RemainingTerm < 15 && searchCalculatorDto.RemainingTerm > 10)
                {
                    if (searchCalculatorDto.includeVAY == "true")
                    {
                        return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + "&parent_id=" + "2297" + "&productList=1" + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + HypotecConst.loanPurpose + "1";
                    }
                    else
                    {
                        return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + "&parent_id=" + "2297" + "&productList=1" + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + HypotecConst.loanPurpose + "1";
                    }
                }
            }
            else if (searchCalculatorDto.RefinanceGoal == "CashOut")
            {
                if (searchCalculatorDto.includeVAY == "true")
                {
                    return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + 2 + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.vaType + 1 + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + "&parent_id=" + "2297" + "&productList=26,418,4";
                }
                else
                {
                    return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + 2 + HypotecConst.loanamount + searchCalculatorDto.CurrentLoanBalanceAmount + HypotecConst.vaType + 1 + HypotecConst.propertyZip + searchCalculatorDto.ZipCode + HypotecConst.fico + searchCalculatorDto.Fico + HypotecConst.appraisedvalue + searchCalculatorDto.HomeValue + "&parent_id=" + "2297" + "&productList=4,23,125";
                }
            }
            return url;
        }
    }
}
