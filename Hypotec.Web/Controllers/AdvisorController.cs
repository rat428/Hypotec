using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Hypotec.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hypotec.Web.Controllers
{
    public class AdvisorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAdvisorService _advisorService;


        public AdvisorController(IMapper mapper, IAdvisorService advisorService)
        {
            _advisorService = advisorService;
            _mapper = mapper;

        }
        /// <summary>
        /// List Of all advisor
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AllAdvisor()
        {
            try
            {
                List<AdvisorModel> lstAdvisorModel = new List<AdvisorModel>();
                var xMLAdvisorList = await _advisorService.AdvisorList().ConfigureAwait(false);
                lstAdvisorModel = _mapper.Map<List<AdvisorModel>>(xMLAdvisorList);
                return await Task.Run(() => View(lstAdvisorModel)).ConfigureAwait(false);

            }
            catch (Exception)
            {
                return View();
            }
        }

        /// <summary>
        /// method of open advisor page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Advisor()
        {
            try
            {


                return await Task.Run(() => View()).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// find all advisor by lat and long and serach parameter
        /// </summary>
        /// <param name="serachAdvisor"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="radius"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AdvisorList(string serachAdvisor, string latitude, string longitude, int recordCount)
        {
            if (serachAdvisor == null)
            {
                serachAdvisor = "";
            }
            List<AdvisorModel> lstAdvisorModel = new List<AdvisorModel>();
            List<AdvisorModel> lstAdvisorModel1 = new List<AdvisorModel>();
            List<AdvisorModel> objListAdvisorModelArray = new List<AdvisorModel>();
            List<AdvisorModel> objListAdvisor = new List<AdvisorModel>();
            var xMLAdvisorList = await _advisorService.AdvisorList().ConfigureAwait(false);
            lstAdvisorModel = _mapper.Map<List<AdvisorModel>>(xMLAdvisorList);
            if (serachAdvisor.Length > 0)
            {
                serachAdvisor = serachAdvisor.Replace(",", " ");
                string[] words = serachAdvisor.Split(' ');
                if (recordCount > 0)
                {
                    int recordCounter = 0;
                    double distanceInKM = 0;

                    foreach (var objAdvisor in lstAdvisorModel)
                    {
                        bool flagwordfound = false;
                        //if (longitude == null && latitude == null)
                        //{
                        // search when exact match case START************************
                        for (int wCount = 0; wCount < words.Count(); wCount++)
                        {
                            flagwordfound = false;
                            if (!string.IsNullOrEmpty(words[wCount]))
                            {
                                if (objAdvisor.Address.ToLower().Contains(words[wCount].ToLower()))
                                {
                                    string[] addresslist = objAdvisor.Address.Split("@");
                                    int cnt = 0;
                                    for (; cnt < addresslist.Count(); cnt++)
                                    {
                                        if (objAdvisor.Address.ToLower().Contains(String.Concat("##", words[wCount], "##")) || objAdvisor.Address.ToUpper().Contains(String.Concat("##", words[wCount], "##")))
                                        {
                                            words[wCount] = String.Concat("##", words[wCount], "##");
                                        }
                                        if (addresslist[cnt].ToLower().Contains(words[wCount].ToLower()))
                                        {
                                            objAdvisor.Address = addresslist[cnt].Replace("##", "");
                                            flagwordfound = true;
                                            break;
                                        }
                                    }
                                    if (flagwordfound == true)
                                    {
                                        objListAdvisor.Add(objAdvisor);
                                        recordCounter = recordCounter + 1;
                                        break;
                                    }
                                }
                            }
                           
                        }
                        //if (flagwordfound == false)
                        //{
                        //    string[] addlist = objAdvisor.Address.Split("@");
                        //    string[] longitudelist = objAdvisor.Langitude.Split("@");
                        //    string[] latitudelist = objAdvisor.Latitude.Split("@");
                        //    int latlongCount = 0;
                        //    bool flag = false;
                        //    for (; latlongCount < longitudelist.Count(); latlongCount++)
                        //    {
                        //        distanceInKM = (distance(Convert.ToDouble(longitude), Convert.ToDouble(latitude), Convert.ToDouble(longitudelist[latlongCount]), Convert.ToDouble(latitudelist[latlongCount])));
                        //        if (distanceInKM <= radius)
                        //        {
                        //            objAdvisor.Address = addlist[latlongCount].Replace("##", "");
                        //            flag = true;
                        //            break;
                        //        }

                        //    }
                        //    if (flag == true)
                        //    {
                        //        objListAdvisor.Add(objAdvisor);
                        //        recordCounter = recordCounter + 1;

                        //    }
                        //}
                        //}
                        //else
                        //{

                        //}
                        if (recordCounter == recordCount) { break; }
                    }

                }

            }
            return PartialView("_AdvisorList", objListAdvisor);

        }
        /// <summary>
        /// get Miles used by lat and long
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="otherLongitude"></param>
        /// <param name="otherLatitude"></param>
        /// <returns></returns>
        public double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));

            //double distance = 0;

            //double dLat = (otherLatitude - latitude) / 180 * Math.PI;
            //double dLong = (otherLongitude - longitude) / 180 * Math.PI;

            //double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
            //            + Math.Cos(otherLatitude) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            //double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            ////Calculate radius of earth
            //// For this you can assume any of the two points.
            //double radiusE = 6378135; // Equatorial radius, in metres
            //double radiusP = 6356750; // Polar Radius

            ////Numerator part of function
            //double nr = Math.Pow(radiusE * radiusP * Math.Cos(latitude / 180 * Math.PI), 2);
            ////Denominator part of the function
            //double dr = Math.Pow(radiusE * Math.Cos(latitude / 180 * Math.PI), 2)
            //                + Math.Pow(radiusP * Math.Sin(latitude / 180 * Math.PI), 2);
            //double radius = Math.Sqrt(nr / dr);

            ////Calaculate distance in metres.
            //distance = radius * c;
            //distance=  0.000621371 * distance;
            //return distance;
        }

        /// <summary>
        ///  method of open add advisor page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddAdvisor()
        {
            try
            {
                return await Task.Run(() => View(StatList())).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary> 
        /// method of add advisor page
        /// </summary>
        /// <param name="advisorModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAdvisor(AdvisorModel advisorModel)
        {
            try
            {
                XMLAdvisor xMLAdvisor = new XMLAdvisor();
                xMLAdvisor = _mapper.Map<XMLAdvisor>(advisorModel);

                bool isSuccess = await _advisorService.SaveAdvisor(xMLAdvisor).ConfigureAwait(false);
                ViewBag.Message = isSuccess;
                ModelState.Clear();
                return View(StatList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// method of open edit advisor page with details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditAdvisor(string id)
        {
            try
            {
                AdvisorModel lstAdvisorModel = new AdvisorModel();

                AdvisorModel objAdvisor = new AdvisorModel();
                var xMLAdvisor = await _advisorService.GetAdvisorById(id).ConfigureAwait(false);
                lstAdvisorModel = _mapper.Map<AdvisorModel>(xMLAdvisor);
                objAdvisor = StatList();
                lstAdvisorModel.lstState = objAdvisor.lstState;
                return await Task.Run(() => View(lstAdvisorModel)).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// method of update advisor page to update advisor detail
        /// </summary>
        /// <param name="advisorModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateAdvisor(AdvisorModel advisorModel)
        {
            try
            {
                XMLAdvisor xMLAdvisor = new XMLAdvisor();
                xMLAdvisor = _mapper.Map<XMLAdvisor>(advisorModel);
                bool isSuccess = await _advisorService.UpdateAdvisor(xMLAdvisor).ConfigureAwait(false);

                ViewBag.Message = isSuccess;
                ModelState.Clear();
                return await Task.Run(() => View("EditAdvisor", StatList())).ConfigureAwait(false);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        ///  method of delete advisor 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteAdvisor(string id)
        {
            try
            {

                bool isSuccess = await _advisorService.RemoveAdvisor(id).ConfigureAwait(false);

                ViewBag.Message = isSuccess;
                List<AdvisorModel> lstAdvisorModel = new List<AdvisorModel>();
                var xMLAdvisorList = await _advisorService.AdvisorList().ConfigureAwait(false);
                lstAdvisorModel = _mapper.Map<List<AdvisorModel>>(xMLAdvisorList);
                return await Task.Run(() => View("AllAdvisor", lstAdvisorModel)).ConfigureAwait(false);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// method of open agent profile with detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AgentProfile(string id)
        {

            AdvisorModel lstAdvisorModel = new AdvisorModel();
            var xMLAdvisor = await _advisorService.FindByAdvisorName(id).ConfigureAwait(false);
            lstAdvisorModel = _mapper.Map<AdvisorModel>(xMLAdvisor);
            return await Task.Run(() => View(lstAdvisorModel)).ConfigureAwait(false);
        }
        /// <summary>
        /// bind state with address in a dropdown
        /// </summary>
        /// <returns></returns>
        public static AdvisorModel StatList()
        {

            AdvisorModel advisorModel = new AdvisorModel();
            //Creating object of CheckBoxList model class
            CheckBoxList ChkItems = new CheckBoxList();
            //Additng items to the list
            List<CheckBoxModel> ChkItem = new List<CheckBoxModel>()
            {
              new CheckBoxModel {Value="6380 Wilshire Blvd, Suite 1602 Los Angeles ##CA## 90048 United States @34.063360 @-118.368120",Text="CA",IsChecked=true },
              new CheckBoxModel {Value="4999 Eisenhower Pkwy Macon ##GA## 31206 United States @32.812080 @-83.719950",Text="GA",IsChecked=false },
              new CheckBoxModel {Value="1217 N Horner Blvd Sanford ##NC## 27330 United States @35.4967502 @-79.1999508",Text="NC",IsChecked=false },
              new CheckBoxModel {Value="1 Convent Rd Morristown ##NJ## 07960 United States @40.7776935 @-74.444823",Text="NJ" ,IsChecked=false},
              new CheckBoxModel {Value="6722 Hwy 20, Bouckville ##NY## 13310, United States @42.8890634 @-75.55148319999999",Text="NY",IsChecked=false },
              new CheckBoxModel {Value="3838 Oak Lawn Ave, Dallas ##TX## 75219, United States @32.8144645 @-96.8014862",Text="TX" ,IsChecked=false},
              new CheckBoxModel {Value="652 Lake Dillon Dr, Dillon ##CO## 80435, United States @39.62698899999999 @-106.0475317",Text="CO" ,IsChecked=false},
              new CheckBoxModel {Value="40 E Chicago Ave, Chicago, ##IL## 60611 @41.8968546 @-87.626696",Text="IL" ,IsChecked=false},
              new CheckBoxModel {Value="3717 Boston St, Baltimore, ##MD## 21224 @39.2767426 @-76.56485289999999",Text="MD" ,IsChecked=false},
              new CheckBoxModel {Value="1425 Broadway, Seattle, ##WA## 98122 @47.61365199999999 @-122.320996",Text="WA" ,IsChecked=false},
              new CheckBoxModel {Value="12587 Fair Lakes Circle, Fairfax, ##VA## 22033 @38.85818 @-77.3814354",Text="VA" ,IsChecked=false},
              new CheckBoxModel {Value="2868 Stelzer Rd, Columbus, ##OH## 43219 @40.0316099 @-82.90845949999999",Text="OH" ,IsChecked=false},
              new CheckBoxModel {Value="1151 Freeport Rd, Pittsburgh, ##PA## 15238 @40.48840560000001 @-79.88192049999999",Text="PA" ,IsChecked=false},
              new CheckBoxModel {Value="3688 Airport Blvd, Ste B, Mobile, ##AL## 36608 @30.6762709 @-88.13454089999999",Text="AL" ,IsChecked=false},
              new CheckBoxModel {Value="3319 Greenfield Rd, Dearborn, ##MI## 48120 @42.3059665 @-83.18856269999999",Text="MI" ,IsChecked=false},
              new CheckBoxModel {Value="15 Lincoln St, Wakefield, ##MA## 01880 @42.5031587 @-71.06929749999999",Text="MA" ,IsChecked=false},
              new CheckBoxModel {Value="2817 West End Ave #126 Park Place, Nashville, ##TN## 37203 @36.1439139 @-86.8122405",Text="TN" ,IsChecked=false},
              new CheckBoxModel {Value="10055 Yamato Rd Ste 305, Boca Raton Florida 33498-6102 United States @26.395290 @-80.205130",Text="FL" ,IsChecked=false},

            };
            ChkItems.CheckBoxItems = ChkItem;
            advisorModel.lstState = ChkItems.CheckBoxItems.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Value.ToString()
            }).ToList();
            return advisorModel;

        }

        private double distance(double lon1, double lat1, double lon2, double lat2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                //if (unit == 'K')
                //{
                //    dist = dist * 1.609344;
                //}
                //else if (unit == 'N')
                //{
                //    dist = dist * 0.8684;
                //}
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

    }
}
