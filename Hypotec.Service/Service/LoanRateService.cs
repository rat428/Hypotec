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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hypotec.Service.Service
{
    public class LoanRateService : ILoanRateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public HypotecDetail _hypotecDetail;
        public LoanRateService(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext applicationDbContext, IOptions<HypotecDetail> hypotecDetail)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hypotecDetail = hypotecDetail.Value;
        }
        /// <summary>
        ///  get all puchase and refinace data from mortec api
        /// </summary>
        /// <param name="searchRateDto"></param>
        /// <returns></returns>
        public async Task<List<LoanRateDto>> GetIndexRate(SearchRateDto searchRateDto)
        {
            try
            {
                RestClient client = new RestClient();
                List<LoanRateDto> lstloanRateDto = new List<LoanRateDto>();
                if (searchRateDto.zipCode != null && searchRateDto.loanPurpose == 0 && searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 == null && searchRateDto.indaxRate != "IndexPage")
                {
                    client = new RestClient(GetPurchaseLoanRateUrl(searchRateDto));
                }
                else if (searchRateDto.zipCode != null && searchRateDto.loanPurpose == 1 && searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 == null && searchRateDto.indaxRate != "IndexPage")
                {
                    client = new RestClient(GetRefinanceLoanRateUrl(searchRateDto));
                }
                else if (searchRateDto.loanProduct1 == null && searchRateDto.loanProduct3 == "Yes" && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 != null && searchRateDto.indaxRate != "IndexPage")
                {
                    client = new RestClient(GetloanProduct3LoanRateUrl(searchRateDto));
                }
                else if (searchRateDto.loanProduct2 != null && searchRateDto.loanProduct2 == "Yes" && searchRateDto.loanProduct1 == null && searchRateDto.loanProduct3 == null && searchRateDto.indaxRate != "IndexPage")
                {
                    client = new RestClient(GetloanProduct2LoanRateUrl(searchRateDto));
                }
                else if (searchRateDto.loanProduct1 != null && searchRateDto.loanProduct1 == "Yes" && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 == null && searchRateDto.indaxRate != "IndexPage")
                {
                    client = new RestClient(GetloanProduct1LoanRateUrl(searchRateDto));
                    Thread.Sleep(1000);
                }
                else
                {
                    client = new RestClient(GetIndexLoanRateUrl());
                    Thread.Sleep(1000);
                }
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Task.Run(() => client.Execute(request));
                Thread.Sleep(1000);
                XmlSerializer serializer = new XmlSerializer(typeof(Mortech));
                using (StringReader reader = new StringReader(response.Content))
                {
                    var mortecRate = (Mortech)serializer.Deserialize(reader);
                    int i = 0;
                    int j = 0;
                    int k = 0;
                    foreach (var quotes in mortecRate.Results)
                    {
                        foreach (var quote in quotes.Quote)
                        {
                            LoanRateDto loanRateDto = new LoanRateDto();
                            if (searchRateDto.loanPurpose == 1)
                            {
                                loanRateDto.MonthlyCost = ("$" + MonthlyCostRe(quote.QuoteDetail.LoanAmount, quote.QuoteDetail.Rate, quote.ProductTerm)).ToString();
                            }
                            else
                            {
                                loanRateDto.MonthlyCost = ("$" + MonthlyCost(quote.QuoteDetail.LoanAmount, quote.QuoteDetail.Rate, quote.ProductTerm, quote.QuoteDetail.DownPayment)).ToString();

                            }
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
                            if (searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 == null)
                            {
                                if (quotes.ProductName == "Conf 15 Yr  Fixed ")
                                {
                                    loanRateDto.YearlyDesc = "15 Year Fixed";
                                }
                                else if (quotes.ProductName == "Govt VA 30 Yr  Fixed ")
                                {
                                    loanRateDto.YearlyDesc = "VA 30 Yr  Fixed";
                                }
                                else if (quotes.ProductName == "Govt FHA 15 Yr  Fixed ")
                                {
                                    loanRateDto.YearlyDesc = "FHA 15 Yr  Fixed";
                                }
                                else if (quotes.ProductName == "Govt FHA 30 Yr  Fixed ")
                                {
                                    loanRateDto.YearlyDesc = "FHA 30 Yr  Fixed";
                                }
                                else if (quotes.ProductName == "Conf 30 Yr  Fixed ")
                                {
                                    loanRateDto.YearlyDesc = "30 Year Fixed";
                                }
                                lstloanRateDto.Add(loanRateDto);
                            }
                            else if (searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 != null)
                            {
                                if (loanRateDto.ProductTerm == 30 && i < 3)
                                {
                                    loanRateDto.YearlyDesc = "30 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                            }
                            else if (searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 != null && searchRateDto.loanProduct3 == null)
                            {
                                if (loanRateDto.ProductTerm == 20 && i < 3)
                                {
                                    loanRateDto.YearlyDesc = "20 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }

                            }
                            else if (searchRateDto.loanProduct1 != null && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 == null)
                            {
                                if (loanRateDto.ProductTerm == 15 && i < 3)
                                {
                                    loanRateDto.YearlyDesc = "15 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                            }
                            else if (searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 != null && searchRateDto.loanProduct3 != null)
                            {
                                if (loanRateDto.ProductTerm == 30 && i < 2)
                                {
                                    loanRateDto.YearlyDesc = "30 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                                if (loanRateDto.ProductTerm == 20 && j < 2)
                                {
                                    loanRateDto.YearlyDesc = "20 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    j++;

                                }

                            }
                            else if (searchRateDto.loanProduct1 != null && searchRateDto.loanProduct2 != null && searchRateDto.loanProduct3 == null)
                            {
                                if (loanRateDto.ProductTerm == 20 && i < 2)
                                {
                                    loanRateDto.YearlyDesc = "20 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                                if (loanRateDto.ProductTerm == 10 && j < 2)
                                {
                                    loanRateDto.YearlyDesc = "10 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    j++;

                                }
                            }
                            else if (searchRateDto.loanProduct1 != null && searchRateDto.loanProduct2 == null && searchRateDto.loanProduct3 != null)
                            {
                                if (loanRateDto.ProductTerm == 30 && i < 2)
                                {
                                    loanRateDto.YearlyDesc = "30 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                                if (loanRateDto.ProductTerm == 10 && j < 2)
                                {
                                    loanRateDto.YearlyDesc = "10 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    j++;

                                }
                            }
                            else if (searchRateDto.loanProduct1 == null && searchRateDto.loanProduct2 != null && searchRateDto.loanProduct3 != null)
                            {
                                if (loanRateDto.ProductTerm == 20 && i < 2)
                                {
                                    loanRateDto.YearlyDesc = "20 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                                if (loanRateDto.ProductTerm == 30 && j < 2)
                                {
                                    loanRateDto.YearlyDesc = "30 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    j++;

                                }
                            }
                            else if (searchRateDto.loanProduct1 != null && searchRateDto.loanProduct2 != null && searchRateDto.loanProduct3 != null)
                            {
                                if (loanRateDto.ProductTerm == 20 && i < 1)
                                {
                                    loanRateDto.YearlyDesc = "20 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    i++;
                                }
                                if (loanRateDto.ProductTerm == 30 && j < 1)
                                {
                                    loanRateDto.YearlyDesc = "30 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    j++;

                                }
                                if (loanRateDto.ProductTerm == 10 && k < 1)
                                {
                                    loanRateDto.YearlyDesc = "10 Year Fixed";
                                    lstloanRateDto.Add(loanRateDto);
                                    k++;
                                }
                            }


                        }
                    }

                    List<LoanRateDto> lstloanRateDtoOrdered = new List<LoanRateDto>();
                    if (searchRateDto.loanProduct1 == "Yes" || searchRateDto.loanProduct2 == "Yes" || searchRateDto.loanProduct3 == "Yes")
                    {
                        lstloanRateDtoOrdered = lstloanRateDto.ToList();
                        return await Task.Run(() => lstloanRateDtoOrdered).ConfigureAwait(false);
                    }
                    else
                    {
                        foreach (var item in lstloanRateDto)
                        {

                            if (item.YearlyDesc == "30 Year Fixed")
                            {
                                lstloanRateDtoOrdered.Add(lstloanRateDto.Where(x => x.YearlyDesc == "30 Year Fixed").FirstOrDefault());
                            }
                            if (item.YearlyDesc == "15 Year Fixed")
                            {
                                lstloanRateDtoOrdered.Add(lstloanRateDto.Where(x => x.YearlyDesc == "15 Year Fixed").FirstOrDefault());
                            }
                            if (searchRateDto.indaxRate == "IndexPage")
                            {
                                if (item.YearlyDesc == "VA 30 Yr  Fixed")
                                {
                                    lstloanRateDtoOrdered.Add(lstloanRateDto.Where(x => x.YearlyDesc == "VA 30 Yr  Fixed").FirstOrDefault());
                                }
                                if (item.YearlyDesc == "FHA 15 Yr  Fixed")
                                {
                                    lstloanRateDtoOrdered.Add(lstloanRateDto.Where(x => x.YearlyDesc == "FHA 15 Yr  Fixed").FirstOrDefault());
                                }
                            }
                            else
                            {
                                if (item.YearlyDesc == "FHA 30 Yr  Fixed")
                                {
                                    lstloanRateDtoOrdered.Add(lstloanRateDto.Where(x => x.YearlyDesc == "FHA 30 Yr  Fixed").FirstOrDefault());
                                }
                                if (item.YearlyDesc == "VA 30 Yr  Fixed")
                                {
                                    lstloanRateDtoOrdered.Add(lstloanRateDto.Where(x => x.YearlyDesc == "VA 30 Yr  Fixed").FirstOrDefault());
                                }
                            }
                        }

                        return await Task.Run(() => lstloanRateDtoOrdered).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// calculate monthly payment used  by down payment
        /// </summary>
        /// <param name="LoanAmount"></param>
        /// <param name="InterestRate"></param>
        /// <param name="PaymentPeriods"></param>
        /// <param name="DownPayment"></param>
        /// <returns></returns>
        private double MonthlyCost(double LoanAmount, double InterestRate, int PaymentPeriods, double DownPayment)
        {

            double totalAfterDiscount = LoanAmount - DownPayment;//(LoanAmount * 20 / 100);

            int Payment = 0;
            InterestRate = InterestRate / (12 * 100); // one month interest 
            PaymentPeriods = PaymentPeriods * 12; // one month period 
            return Payment = Convert.ToInt32((totalAfterDiscount * InterestRate * Math.Pow(1 + InterestRate, PaymentPeriods)) / (Math.Pow(1 + InterestRate, PaymentPeriods) - 1));
        }
        /// <summary>
        /// calculate monthly payment by loan amount and intrest rate
        /// </summary>
        /// <param name="LoanAmount"></param>
        /// <param name="InterestRate"></param>
        /// <param name="PaymentPeriods"></param>
        /// <returns></returns>
        private double MonthlyCostRe(double LoanAmount, double InterestRate, int PaymentPeriods)
        {

            double totalAfterDiscount = LoanAmount;//(LoanAmount * 20 / 100);

            int Payment = 0;
            InterestRate = InterestRate / (12 * 100); // one month interest 
            PaymentPeriods = PaymentPeriods * 12; // one month period 
            return Payment = Convert.ToInt32((totalAfterDiscount * InterestRate * Math.Pow(1 + InterestRate, PaymentPeriods)) / (Math.Pow(1 + InterestRate, PaymentPeriods) - 1));
        }
        /// <summary>
        /// calculate ltv payment
        /// </summary>
        /// <param name="LoanAmount"></param>
        /// <param name="homeValue"></param>
        /// <returns></returns>
        private double Ltv(double LoanAmount, double homeValue)
        {
            return (LoanAmount / homeValue);
        }
        /// <summary>
        /// purchase mortec url
        /// </summary>
        /// <param name="searchRateDto"></param>
        /// <returns></returns>
        /// 
        private string GetPurchaseManageLoanRateUrl(SearchRateDto searchRateDto)
        {
            string url = _hypotecDetail.Url;
            if (searchRateDto.zipCode == null)
            {
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + "200000" + HypotecConst.downPayment + "50000" + HypotecConst.proptype + "15" + HypotecConst.firstTimeHomeBuyer + "1" + HypotecConst.propertyZip + "91206" + HypotecConst.fico + "700" + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.loanProduct2 + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed + HypotecConst.loanProduct4 + HypotecConst.TwentyFiveYearFixed + HypotecConst.loanProduct5 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else if (searchRateDto.all == "Yes")
            {
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.loanProduct2 + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed + HypotecConst.loanProduct4 + HypotecConst.TwentyFiveYearFixed + HypotecConst.loanProduct5 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else if (string.IsNullOrEmpty(searchRateDto.all))
            {
                if (searchRateDto.loanProduct1 == "Yes")
                {
                    url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
                    if (searchRateDto.loanProduct2 == "Yes")
                    {
                        url = url + HypotecConst.loanProduct2 + HypotecConst.TwentyYearFixed;
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    else
                    {
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    return url;
                }
                if (searchRateDto.loanProduct2 == "Yes")
                {
                    url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TwentyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
                    if (searchRateDto.loanProduct1 == "Yes")
                    {
                        url = url + HypotecConst.loanProduct2 + HypotecConst.TenYearFixed;
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    else
                    {
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    return url;
                }
                if (searchRateDto.loanProduct3 == "Yes")
                {
                    url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
                    if (searchRateDto.loanProduct1 == "Yes")
                    {
                        url = url + HypotecConst.loanProduct2 + HypotecConst.TenYearFixed;
                        if (searchRateDto.loanProduct2 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed;
                        }
                    }
                    else
                    {
                        if (searchRateDto.loanProduct2 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed;
                        }
                    }
                    return url;
                }
            }

            return url;

        }
        private string GetRefinanceManageLoanRateUrl(SearchRateDto searchRateDto)
        {
            double ltv = Ltv(Convert.ToDouble(searchRateDto.purchasePrice), Convert.ToDouble(searchRateDto.homeValue));
            string url = _hypotecDetail.Url;
            if (searchRateDto.zipCode == null)
            {
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + "200000" + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + "15" + HypotecConst.firstTimeHomeBuyer + "1" + HypotecConst.propertyZip + "91206" + HypotecConst.fico + "700" + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.loanProduct2 + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed + HypotecConst.loanProduct4 + HypotecConst.TwentyFiveYearFixed + HypotecConst.loanProduct5 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else if (searchRateDto.all == "Yes")
            {
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.loanProduct2 + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed + HypotecConst.loanProduct4 + HypotecConst.TwentyFiveYearFixed + HypotecConst.loanProduct5 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else if (string.IsNullOrEmpty(searchRateDto.all))
            {
                if (searchRateDto.loanProduct1 == "Yes")
                {
                    url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
                    if (searchRateDto.loanProduct2 == "Yes")
                    {
                        url = url + HypotecConst.loanProduct2 + HypotecConst.TwentyYearFixed;
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    else
                    {
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    return url;
                }
                if (searchRateDto.loanProduct2 == "Yes")
                {
                    url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TwentyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
                    if (searchRateDto.loanProduct1 == "Yes")
                    {
                        url = url + HypotecConst.loanProduct2 + HypotecConst.TenYearFixed;
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    else
                    {
                        if (searchRateDto.loanProduct3 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.thirtyYearFixed;
                        }
                    }
                    return url;
                }
                if (searchRateDto.loanProduct3 == "Yes")
                {
                    url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
                    if (searchRateDto.loanProduct1 == "Yes")
                    {
                        url = url + HypotecConst.loanProduct2 + HypotecConst.TenYearFixed;
                        if (searchRateDto.loanProduct2 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed;
                        }
                    }
                    else
                    {
                        if (searchRateDto.loanProduct2 == "Yes")
                        {
                            url = url + HypotecConst.loanProduct3 + HypotecConst.TwentyYearFixed;
                        }
                    }
                    return url;
                }
            }
            return url;
        }
        private string GetIndexLoanRateUrl()
        {

            string url = _hypotecDetail.Url;

            return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + "&loan_amount=" + "200000" + "&parent_id=" + "4004" + "&productList=2,4,22,26";
        }
        private string GetPurchaseLoanRateUrl(SearchRateDto searchRateDto)
        {

            string url = _hypotecDetail.Url;
            return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanamount + searchRateDto.purchasePrice + "&parent_id=" + "4004" + "&productList=2,4,23,26" + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.loanPurpose + searchRateDto.loanPurpose;
        }
        private string GetRefinanceLoanRateUrl(SearchRateDto searchRateDto)
        {

            string url = _hypotecDetail.Url;
            return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanamount + searchRateDto.purchasePrice + "&parent_id=" + "4004" + "&productList=2,4,23,26" + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.loanPurpose + searchRateDto.loanPurpose;
        }
        private string GetloanProduct1LoanRateUrl(SearchRateDto searchRateDto)
        {
            if (searchRateDto.loanPurpose == 1)
            {
                double ltv = Ltv(Convert.ToDouble(searchRateDto.purchasePrice), Convert.ToDouble(searchRateDto.homeValue));
                string url = _hypotecDetail.Url;
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else
            {
                string url = _hypotecDetail.Url;
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + HypotecConst.downPayment + searchRateDto.downPayment + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TenYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;

            }
        }
        private string GetloanProduct2LoanRateUrl(SearchRateDto searchRateDto)
        {
            if (searchRateDto.loanPurpose == 1)
            {
                double ltv = Ltv(Convert.ToDouble(searchRateDto.purchasePrice), Convert.ToDouble(searchRateDto.homeValue));
                string url = _hypotecDetail.Url;
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TwentyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else
            {
                string url = _hypotecDetail.Url;
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.TwentyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;

            }
        }
        private string GetloanProduct3LoanRateUrl(SearchRateDto searchRateDto)
        {
            if (searchRateDto.loanPurpose == 1)
            {
                double ltv = Ltv(Convert.ToDouble(searchRateDto.purchasePrice), Convert.ToDouble(searchRateDto.homeValue));
                string url = _hypotecDetail.Url;
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.ltv + ltv + HypotecConst.appraisedvalue + searchRateDto.homeValue + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;
            }
            else
            {
                string url = _hypotecDetail.Url;
                return url = url + "request_id=" + _hypotecDetail.RequestId + "&customerId=" + _hypotecDetail.CustomerId + "&thirdPartyName=" + _hypotecDetail.ThirdPartyName + "&licenseKey=" + _hypotecDetail.LicenseKey + "&emailAddress=" + _hypotecDetail.EmailAddress + HypotecConst.loanPurpose + searchRateDto.loanPurpose + HypotecConst.loanamount + searchRateDto.purchasePrice + HypotecConst.loanPurpose + HypotecConst.proptype + searchRateDto.propertyType + HypotecConst.firstTimeHomeBuyer + searchRateDto.firstTimeBuyer + HypotecConst.downPayment + searchRateDto.downPayment + HypotecConst.propertyZip + searchRateDto.zipCode + HypotecConst.fico + searchRateDto.creditScore + HypotecConst.loanProduct1 + HypotecConst.thirtyYearFixed + HypotecConst.propertyUsage + searchRateDto.propertyUsage + HypotecConst.targetPrice;

            }
        }
    }

}
