using AutoMapper;
using Hypotec.Data.Data;
using Hypotec.Data.Entity;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Microsoft.AspNetCore.Identity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace Hypotec.Service.Service
{
    public class AdvisorService : IAdvisorService
    {

        public AdvisorService()
        {

        }
        /// <summary>
        /// Save advisor detail into xml file
        /// </summary>
        /// <param name="xMLAdvisor"></param>
        /// <returns></returns>
        public async Task<bool> SaveAdvisor(XMLAdvisor xMLAdvisor)
        {
            var path = Directory.GetCurrentDirectory();
            XmlDocument XmlDocObj = new XmlDocument();

            List<string> listAdd = new List<string>();
            List<string> listLat = new List<string>();
            List<string> listLon = new List<string>();
            string lststate = string.Empty;
            //List<String> fulladd;
            string[] fulladd;
            string[] latitude;
            string[] longitude;
            if (xMLAdvisor.LicenseState.Length > 0)
            {
                foreach (var item in xMLAdvisor.LicenseState)
                {
                    string[] address = item.Split("@");
                    listLon.Add(address[2]);
                    listLat.Add(address[1]);
                    listAdd.Add(address[0]);
                }
            }
            fulladd = listAdd.ToArray();
            latitude = listLat.ToArray();
            longitude = listLon.ToArray();
            string advisorlongitude = String.Join("@", longitude);
            string advisorlatitude = String.Join("@", latitude);
            string advisorAddrress = String.Join("@", fulladd);

            if (xMLAdvisor.Image.Length > 0)
            {
                xMLAdvisor.ImagePath = "/images/Advisor/" + xMLAdvisor.Image.FileName;
                if (xMLAdvisor.Image != null || xMLAdvisor.Image.Length > 0)

                {
                    var path1 = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\images\\Advisor\\",
                                xMLAdvisor.Image.FileName);
                    using (var stream = new FileStream(path1, FileMode.Create))
                    {
                        await xMLAdvisor.Image.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }
            }
            //loading XML File in memory
            path = path + "\\wwwroot\\XMLFile\\Advisor.xml";

            XDocument xDoc = XDocument.Load(path);
            int maxNr = xDoc.Root.Elements().Max(x => (int)x.Element("Id"));
            maxNr = maxNr + 1;
            XmlDocObj.Load(path);
            xMLAdvisor.Id = maxNr.ToString();
            //Select root node which is already defined
            XmlNode RootNode = XmlDocObj.SelectSingleNode("AdvisorList");
            //Creating one child node with tag name book
            XmlNode resourceNode = RootNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Advisor", ""));
            //adding node title to book node and inside it data taking from tbTitle TextBox

            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Id", "")).InnerText = xMLAdvisor.Id;
            //adding node Author to book node and inside it data taking from tbAuthor TextBox
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Name", "")).InnerText = xMLAdvisor.Name;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "RegistrationNumber", "")).InnerText = xMLAdvisor.RegistrationNumber;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "LicenseState", "")).InnerText = "";
            //adding node Year to book node and inside it data taking from tbYear TextBox
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "ImagePath", "")).InnerText = xMLAdvisor.ImagePath;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Email", "")).InnerText = xMLAdvisor.Email;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Address", "")).InnerText = advisorAddrress;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Office", "")).InnerText = xMLAdvisor.Office;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Ext", "")).InnerText = xMLAdvisor.Ext;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Text", "")).InnerText = xMLAdvisor.Text;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Call", "")).InnerText = xMLAdvisor.Call;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "About", "")).InnerText = xMLAdvisor.About;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "StatusFlag", "")).InnerText = "true";
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Longitude", "")).InnerText = advisorlongitude;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Latitude", "")).InnerText = advisorlatitude;

            //after adding node, saving advisor.xml back to the server
            XmlDocObj.Save(path);
            return await Task.Run(() => true).ConfigureAwait(false);
        }
        /// <summary>
        /// update advisor into xml file
        /// </summary>
        /// <param name="xMLAdvisor"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAdvisor(XMLAdvisor xMLAdvisor)
        {
            List<string> listAdd = new List<string>();
            List<string> listLat = new List<string>();
            List<string> listLon = new List<string>();
            string lststate = string.Empty;
            //List<String> fulladd;
            string[] fulladd;
            string[] latitude;
            string[] longitude;
            if (xMLAdvisor.LicenseState.Length > 0)
            {
                foreach (var item in xMLAdvisor.LicenseState)
                {
                    string[] address = item.Split("@");
                    listLon.Add(address[2]);
                    listLat.Add(address[1]);
                    listAdd.Add(address[0]);
                }
            }
            fulladd = listAdd.ToArray();
            latitude = listLat.ToArray();
            longitude = listLon.ToArray();
            string advisorlongitude = String.Join("@", longitude);
            string advisorlatitude = String.Join("@", latitude);
            string advisorAddrress = String.Join("@", fulladd);
            var path = Directory.GetCurrentDirectory();
            if (xMLAdvisor.Image != null)
            {
                xMLAdvisor.ImagePath = "/images/Advisor/" + xMLAdvisor.Image.FileName;
                if (xMLAdvisor.Image != null || xMLAdvisor.Image.Length > 0)

                {
                    var path1 = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\images\\Advisor\\",
                                xMLAdvisor.Image.FileName);
                    using (var stream = new FileStream(path1, FileMode.Create))
                    {
                        await xMLAdvisor.Image.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }
            }
            XDocument doc = new XDocument();
            XMLResource objjob = new XMLResource();
            path = path + "\\wwwroot\\XMLFile\\Advisor.xml";

            doc = XDocument.Load(path);
            XElement node = doc.Root.Elements("Advisor").Where(i => (int)i.Element("Id") == Convert.ToInt32(xMLAdvisor.Id)).FirstOrDefault();
            node.SetElementValue("Name", xMLAdvisor.Name);
            node.SetElementValue("RegistrationNumber", xMLAdvisor.RegistrationNumber);
            //node.SetElementValue("LicenseState", lststate);
            node.SetElementValue("Email", xMLAdvisor.Email);
            node.SetElementValue("Address", advisorAddrress);
            node.SetElementValue("Office", xMLAdvisor.Office);
            node.SetElementValue("Ext", xMLAdvisor.Ext);
            node.SetElementValue("Text", xMLAdvisor.Text);
            node.SetElementValue("Call", xMLAdvisor.Call);
            if (xMLAdvisor.About != null)
            {
                node.SetElementValue("About", xMLAdvisor.About);
            }
            node.SetElementValue("Longitude", advisorlongitude);
            node.SetElementValue("Latitude", advisorlatitude);


            if (xMLAdvisor.Image != null)
            {
                node.SetElementValue("ImagePath", xMLAdvisor.ImagePath);
            }


            //after adding node, saving BookStore.xml back to the server
            doc.Save(path);
            return await Task.Run(() => true).ConfigureAwait(false);
        }
        /// <summary>
        /// delete advisor by id from xml file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAdvisor(string id)
        {
            try
            {


                var path = Directory.GetCurrentDirectory();

                XDocument doc = new XDocument();
                XMLResource objjob = new XMLResource();
                path = path + "\\wwwroot\\XMLFile\\Advisor.xml";

                doc = XDocument.Load(path);
                XElement node = doc.Root.Elements("Advisor").Where(i => (int)i.Element("Id") == Convert.ToInt32(id)).FirstOrDefault();
                node.SetElementValue("StatusFlag", "false");
                doc.Save(path);
                return await Task.Run(() => true).ConfigureAwait(false);
            }

            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        ///  get advisor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<XMLAdvisor> GetAdvisorById(string id)
        {
            XDocument doc = new XDocument();
            XMLAdvisor objjob = new XMLAdvisor();
            var path = Directory.GetCurrentDirectory();
            path = path + "\\wwwroot\\XMLFile\\Advisor.xml";
            doc = XDocument.Load(path);
            XElement node = doc.Root.Elements("Advisor").Where(i => (int)i.Element("Id") == Convert.ToInt32(id)).FirstOrDefault();
            string listFullAddress = string.Empty;
            objjob.Id = node.Descendants("Id").FirstOrDefault().Value;
            objjob.Name = node.Descendants("Name").FirstOrDefault().Value;
            objjob.RegistrationNumber = node.Descendants("RegistrationNumber").FirstOrDefault().Value;
            string strLatitude = node.Descendants("Latitude").FirstOrDefault().Value;
            string strLongitude = node.Descendants("Longitude").FirstOrDefault().Value;
            string strState = node.Descendants("Address").FirstOrDefault().Value;
            string[] strLat = strLatitude.Split("@");
            string[] strLong = strLongitude.Split("@");
            string[] strAdd = strState.Split("@");
            for (var counter = 0; counter < strAdd.Length; counter++)
            {
                if (counter == 0)
                {
                    listFullAddress = listFullAddress + strAdd[counter] + "@" + strLat[counter] + "@" + strLong[counter];
                }
                else
                {
                    listFullAddress = listFullAddress + "&&&" + strAdd[counter] + "@" + strLat[counter] + "@" + strLong[counter];

                }
            }
            string[] strSpillByHashAdd = listFullAddress.Split("&&&");
            objjob.LicenseState = strSpillByHashAdd;
            objjob.ImagePath = node.Descendants("ImagePath").FirstOrDefault().Value;
            objjob.Email = node.Descendants("Email").FirstOrDefault().Value;
            objjob.Address = node.Descendants("Address").FirstOrDefault().Value;
            objjob.Office = node.Descendants("Office").FirstOrDefault().Value;
            objjob.Ext = node.Descendants("Ext").FirstOrDefault().Value;
            objjob.Text = node.Descendants("Text").FirstOrDefault().Value;
            objjob.Call = node.Descendants("Call").FirstOrDefault().Value;
            objjob.About = node.Descendants("About").FirstOrDefault().Value;
            objjob.Langitude = node.Descendants("Longitude").FirstOrDefault().Value;
            objjob.Latitude = node.Descendants("Latitude").FirstOrDefault().Value;
            return await Task.Run(() => objjob).ConfigureAwait(false);
        }
        /// <summary>
        /// get all advisor from xml database
        /// </summary>
        /// <returns></returns>
        public async Task<List<XMLAdvisor>> AdvisorList()
        {
            XmlDocument doc = new XmlDocument();
            var path = Directory.GetCurrentDirectory();
            path = path + "\\wwwroot\\XMLFile\\Advisor.xml";
            doc.Load(path);
            List<XMLAdvisor> objAdvisor = new List<XMLAdvisor>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                XMLAdvisor advisor = new XMLAdvisor();


                advisor.Id = node.ChildNodes[0].InnerText;
                advisor.Name = node.ChildNodes[1].InnerText;
                advisor.RegistrationNumber = node.ChildNodes[2].InnerText;
                string strState = node.ChildNodes[6].InnerText;
                string[] str = strState.Split("@");
                advisor.LicenseState = str;
                advisor.ImagePath = node.ChildNodes[4].InnerText;
                advisor.Email = node.ChildNodes[5].InnerText;
                advisor.Address = node.ChildNodes[6].InnerText;
                advisor.Office = node.ChildNodes[7].InnerText;
                advisor.Ext = node.ChildNodes[8].InnerText;
                advisor.Text = node.ChildNodes[9].InnerText;
                advisor.Call = node.ChildNodes[10].InnerText;
                advisor.About = node.ChildNodes[11].InnerText;
                advisor.StatusFlag = node.ChildNodes[12].InnerText;
                advisor.Langitude = node.ChildNodes[13].InnerText;
                advisor.Latitude = node.ChildNodes[14].InnerText;
                objAdvisor.Add(advisor);
            }
            return await Task.Run(() => objAdvisor.Where(x => x.StatusFlag == "true").ToList()).ConfigureAwait(false);
        }
        /// <summary>
        /// find advisor by advisor name
        /// </summary>
        /// <param name="advisorName"></param>
        /// <returns></returns>
        public async Task<XMLAdvisor> FindByAdvisorName(string advisorName)
        {
            XmlDocument doc = new XmlDocument();
            var path = Directory.GetCurrentDirectory();
            path = path + "\\wwwroot\\XMLFile\\Advisor.xml";
            doc.Load(path);
            XMLAdvisor advisor = new XMLAdvisor();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.ChildNodes[1].InnerText == advisorName)
                {

                    advisor.Id = node.ChildNodes[0].InnerText;
                    advisor.Name = node.ChildNodes[1].InnerText;
                    advisor.RegistrationNumber = node.ChildNodes[2].InnerText;
                    string strState = node.ChildNodes[6].InnerText;
                    string[] str = strState.Split("@");
                    advisor.LicenseState = str;
                    advisor.ImagePath = node.ChildNodes[4].InnerText;
                    advisor.Email = node.ChildNodes[5].InnerText;
                    advisor.Address = node.ChildNodes[6].InnerText;
                    advisor.Office = node.ChildNodes[7].InnerText;
                    advisor.Ext = node.ChildNodes[8].InnerText;
                    advisor.Text = node.ChildNodes[9].InnerText;
                    advisor.Call = node.ChildNodes[10].InnerText;
                    advisor.About = node.ChildNodes[11].InnerText;
                    advisor.StatusFlag = node.ChildNodes[12].InnerText;
                    advisor.Langitude = node.ChildNodes[13].InnerText;
                    advisor.Latitude = node.ChildNodes[14].InnerText;
                }

            }
            return await Task.Run(() => advisor).ConfigureAwait(false);
        }

    }

}
