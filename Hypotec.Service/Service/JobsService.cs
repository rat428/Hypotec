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
    public class JobsService : IJobsService
    {

        public JobsService()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CareersJobs"></param>
        /// <returns></returns>
        public async Task<List<XMLJobs>> CareersJobs()
        {
            XmlDocument doc = new XmlDocument();
            var path = Directory.GetCurrentDirectory();
            path = path + "\\wwwroot\\XMLFile\\Jobs.xml";
            doc.Load(path);
            List<XMLJobs> objJobs = new List<XMLJobs>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                XMLJobs objjob = new XMLJobs();

                string text = node.InnerText; //or loop through its children as well
                objjob.Id = node.ChildNodes[0].InnerText;
                objjob.Title = node.ChildNodes[1].InnerText;
                objjob.SubTitle = node.ChildNodes[2].InnerText;
                objjob.Description = node.ChildNodes[3].InnerText;
                objJobs.Add(objjob);
            }
            return await Task.Run(() => objJobs).ConfigureAwait(false);
        }
        public async Task<bool> SaveCareersJobs(XMLJobs xMLJobs)
        {
            var path = Directory.GetCurrentDirectory();
            XmlDocument XmlDocObj = new XmlDocument();
            //loading XML File in memory
            path = path + "\\wwwroot\\XMLFile\\Jobs.xml";
            XmlDocObj.Load(path);
            //Select root node which is already defined
            XmlNode RootNode = XmlDocObj.SelectSingleNode("JobList");
            //Creating one child node with tag name book
            XmlNode bookNode = RootNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Job", ""));
            //adding node title to book node and inside it data taking from tbTitle TextBox

            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Id", "")).InnerText = xMLJobs.Id;
            //adding node Author to book node and inside it data taking from tbAuthor TextBox
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Title", "")).InnerText = xMLJobs.Title;
            //adding node Year to book node and inside it data taking from tbYear TextBox
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "SubTitle", "")).InnerText = xMLJobs.SubTitle;
            //adding node tbPrice to book node and inside it data taking from tbPrice TextBox
            bookNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Description", "")).InnerText = xMLJobs.Description;

            //after adding node, saving BookStore.xml back to the server
            XmlDocObj.Save(path);
            return await Task.Run(() => true).ConfigureAwait(false);
        }
        /// <summary>
        /// Save resorce into xml file
        /// </summary>
        /// <param name="xMLResource"></param>
        /// <returns></returns>
        public async Task<bool> SaveResource(XMLResource xMLResource)
        {
            var path = Directory.GetCurrentDirectory();
            XmlDocument XmlDocObj = new XmlDocument();
            if (xMLResource.Image.Length > 0)
            {
                xMLResource.ImagePath = "/images/Resource/" + xMLResource.Image.FileName;
                if (xMLResource.Image != null || xMLResource.Image.Length > 0)

                {
                    var path1 = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\images\\Resource\\",
                                xMLResource.Image.FileName);
                    using (var stream = new FileStream(path1, FileMode.Create))
                    {
                        await xMLResource.Image.CopyToAsync(stream);
                    }
                }
            }


            //loading XML File in memory
            path = path + "\\wwwroot\\XMLFile\\Resource.xml";

            XDocument xDoc = XDocument.Load(path);
            int maxNr = xDoc.Root.Elements().Max(x => (int)x.Element("Id"));
            maxNr = maxNr + 1;
            XmlDocObj.Load(path);
            xMLResource.Id = maxNr.ToString();
            //Select root node which is already defined
            XmlNode RootNode = XmlDocObj.SelectSingleNode("ResourceList");
            //Creating one child node with tag name book
            XmlNode resourceNode = RootNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Resource", ""));
            //adding node title to book node and inside it data taking from tbTitle TextBox

            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Id", "")).InnerText = xMLResource.Id;
            //adding node Author to book node and inside it data taking from tbAuthor TextBox
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Tag", "")).InnerText = xMLResource.Tag;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Header", "")).InnerText = xMLResource.Header;
            //adding node Year to book node and inside it data taking from tbYear TextBox
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Description", "")).InnerText = xMLResource.Description;
            //adding node tbPrice to book node and inside it data taking from tbPrice TextBox
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "ImagePath", "")).InnerText = xMLResource.ImagePath;
            resourceNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Status", "")).InnerText = "true";
            //after adding node, saving BookStore.xml back to the server
            XmlDocObj.Save(path);
            return true;
        }
        /// <summary>
        /// update resorce data into xml database
        /// </summary>
        /// <param name="xMLResource"></param>
        /// <returns></returns>
        public async Task<bool> UpdateResource(XMLResource xMLResource)
        {
            var path = Directory.GetCurrentDirectory();
            if (xMLResource.Image != null)
            {
                xMLResource.ImagePath = "/images/Resource/" + xMLResource.Image.FileName;
                if (xMLResource.Image != null || xMLResource.Image.Length > 0)

                {
                    var path1 = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\images\\Resource\\",
                                xMLResource.Image.FileName);
                    using (var stream = new FileStream(path1, FileMode.Create))
                    {
                        await xMLResource.Image.CopyToAsync(stream).ConfigureAwait(true);
                    }
                }
            }
            XDocument doc = new XDocument();
            XMLResource objjob = new XMLResource();
            path = path + "\\wwwroot\\XMLFile\\Resource.xml";

            doc = XDocument.Load(path);
            XElement node = doc.Root.Elements("Resource").Where(i => (int)i.Element("Id") == Convert.ToInt32(xMLResource.Id)).FirstOrDefault();
            node.SetElementValue("Tag", xMLResource.Tag);
            node.SetElementValue("Header", xMLResource.Header);
            node.SetElementValue("Description", xMLResource.Description);
            if (xMLResource.Image != null)
            {
                node.SetElementValue("ImagePath", xMLResource.ImagePath);
            }


            //after adding node, saving BookStore.xml back to the server
            doc.Save(path);
            return await Task.Run(() => true).ConfigureAwait(false);
        }
        /// <summary>
        /// Delete resorce data by id from xml file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveResource(string id)
        {
            try
            {
                var path = Directory.GetCurrentDirectory();

                XDocument doc = new XDocument();
                XMLResource objjob = new XMLResource();
                path = path + "\\wwwroot\\XMLFile\\Resource.xml";

                doc = XDocument.Load(path);
                XElement node = doc.Root.Elements("Resource").Where(i => (int)i.Element("Id") == Convert.ToInt32(id)).FirstOrDefault();
                node.SetElementValue("Status", "false");
                doc.Save(path);
                return await Task.Run(() => true).ConfigureAwait(false);
            }

            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// get all resource list
        /// </summary>
        /// <returns></returns>
        public async Task<List<XMLResource>> ResourceList()
        {
            XmlDocument doc = new XmlDocument();
            var path = Directory.GetCurrentDirectory();
            path = path + "\\wwwroot\\XMLFile\\Resource.xml";
            doc.Load(path);
            List<XMLResource> objResorces = new List<XMLResource>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                XMLResource objjob = new XMLResource();

                string text = node.InnerText; //or loop through its children as well
                objjob.Id = node.ChildNodes[0].InnerText;
                objjob.Tag = node.ChildNodes[1].InnerText;
                objjob.Header = node.ChildNodes[2].InnerText;
                objjob.Description = node.ChildNodes[3].InnerText;
                objjob.ImagePath = node.ChildNodes[4].InnerText;
                objjob.Status = node.ChildNodes[5].InnerText;
                objResorces.Add(objjob);
            }
            return await Task.Run(() => objResorces.Where(x => x.Status == "true").ToList()).ConfigureAwait(false);
        }
        /// <summary>
        /// get resource by id from xml file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<XMLResource> GetResourceById(string id)
        {
            XDocument doc = new XDocument();
            XMLResource objjob = new XMLResource();
            var path = Directory.GetCurrentDirectory();
            path = path + "\\wwwroot\\XMLFile\\Resource.xml";

            doc = XDocument.Load(path);
            XElement node = doc.Root.Elements("Resource").Where(i => (int)i.Element("Id") == Convert.ToInt32(id)).FirstOrDefault();

            objjob.Id = node.Descendants("Id").FirstOrDefault().Value;
            objjob.Tag = node.Descendants("Tag").FirstOrDefault().Value;
            objjob.Header = node.Descendants("Header").FirstOrDefault().Value;
            objjob.Description = node.Descendants("Description").FirstOrDefault().Value;
            objjob.ImagePath = node.Descendants("ImagePath").FirstOrDefault().Value;
            return await Task.Run(() => objjob).ConfigureAwait(false);
        }
        /// <summary>
        /// get AgentSlotList from xml file
        /// </summary>
        /// <returns></returns>
        public async Task<List<XMLAgentSlot>> AgentSlotList()
        {
            try
            {

                XmlDocument doc = new XmlDocument();

                var path = Directory.GetCurrentDirectory();
                path = path + "\\wwwroot\\XMLFile\\AgentSlot.xml";
                doc.Load(path);

                DateTime dt = DateTime.Now;
                string str = dt.ToString("dddd");
                List<XMLAgentSlot> objAgentSlot = new List<XMLAgentSlot>();
                List<XMLAgentSlotList> objAgentSlotList = new List<XMLAgentSlotList>();
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XMLAgentSlot objSlots = new XMLAgentSlot();
                    List<XMLSlot> lstxMLSlot = new List<XMLSlot>();

                    List<XMLSlotStatus> lstSlotStatus = new List<XMLSlotStatus>();

                    string text = node.InnerText; //or loop through its children as well
                    objSlots.Id = node.ChildNodes[0].InnerText;
                    objSlots.DayName = node.ChildNodes[1].InnerText;
                    var slots = node.ChildNodes[2].InnerText;
                    var slotStatusList = node.ChildNodes[3].InnerText;
                    objSlots.Flag = node.ChildNodes[4].InnerText;
                    foreach (var slot in slots.Split(',').ToList())
                    {
                        XMLSlot xMLSlot = new XMLSlot();
                        xMLSlot.Slot = slot;

                        lstxMLSlot.Add(xMLSlot);
                    }
                    foreach (var status in slotStatusList.Split(',').ToList())
                    {
                        XMLSlotStatus slotStatus = new XMLSlotStatus();
                        slotStatus.SlotStatus = status;

                        lstSlotStatus.Add(slotStatus);
                    }

                    objSlots.Slots = lstxMLSlot;
                    objSlots.SlotStatus = lstSlotStatus;
                    objAgentSlot.Add(objSlots);
                }
                List<XMLAgentSlot> newAgentSlot = new List<XMLAgentSlot>();

                int dayNumber = (int)dt.DayOfWeek;

                int startPoint = 0;

                if (dayNumber == 0)
                {
                    startPoint = 7 - 1;
                    await UpdateActiveSlot(7).ConfigureAwait(false);

                }
                else
                {
                    startPoint = dayNumber - 1;
                    await UpdateActiveSlot(dayNumber).ConfigureAwait(false);
                }

                for (; dayNumber <= 7; dayNumber += 1)
                {
                    if (dayNumber == 0)
                    {
                        newAgentSlot.Add(objAgentSlot[7 - 1]);
                    }
                    else
                    {
                        newAgentSlot.Add(objAgentSlot[dayNumber - 1]);
                    }
                    if (dayNumber == 7)
                    {
                        dayNumber = 0;
                    }
                    if (dayNumber == startPoint)
                    {
                        break;
                    }
                }
                return await Task.Run(() => newAgentSlot).ConfigureAwait(false);
            }
            catch (Exception exn)
            {
                return null;
            }
        }

        /// <summary>
        /// update status to deactivate used by id
        /// </summary>
        /// <param name="dayTime"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateActivAndDeactiveSlot(string dayTime, string id)
        {
            try

            {
                string[] timeday = dayTime.Split(' ');
                List<XMLAgentSlot> xMLAgentSlot = new List<XMLAgentSlot>();

                XDocument doc = new XDocument();
                var path = Directory.GetCurrentDirectory();
                path = path + "\\wwwroot\\XMLFile\\AgentSlot.xml";

                doc = XDocument.Load(path);
                XElement node = doc.Root.Elements("Days").Where(i => (int)i.Element("Id") == Convert.ToInt32(id)).FirstOrDefault();
                var flagValue = node.Descendants("SlotStatus").FirstOrDefault().Value;
                var Slots = node.Descendants("Slots").FirstOrDefault().Value;
                string[] lstFlage = flagValue.Split(',');
                string[] lstSlots = Slots.Split(',');
                string str = "";
                int i = 0;
                foreach (var time in lstFlage)
                {
                    string timaValue = timeday[3] + " " + timeday[4];
                    if (lstSlots[i] == timaValue)
                    {
                        if (i < 9)
                        {
                            str = str + "Deactive,";
                        }
                        else
                        {
                            str = str + "Deactive";
                        }
                    }
                    else if (i < 9)
                    {

                        str = str + lstFlage[i] + ",";
                    }
                    else
                    {
                        str = str + lstFlage[i];
                    }
                    i++;
                }

                node.SetElementValue("SlotStatus", str);
                doc.Save(path);
                return await Task.Run(() => true).ConfigureAwait(false);

            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// /UpdateActiveSlot
        /// </summary>
        /// <param name="dayId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateActiveSlot(int dayId)
        {
            try

            {
                List<XMLAgentSlot> xMLAgentSlot = new List<XMLAgentSlot>();
                XDocument doc = new XDocument();
                var path = Directory.GetCurrentDirectory();
                path = path + "\\wwwroot\\XMLFile\\AgentSlot.xml";
                doc = XDocument.Load(path);
                XElement node = doc.Root.Elements("Days").Where(i => (int)i.Element("Id") == Convert.ToInt32(dayId)).FirstOrDefault();
                var flag = node.Descendants("Flag").FirstOrDefault();
                if (flag.Value == "false")
                {
                    for (var counter = 1; counter < 8; counter++)
                    {
                        XElement objNode = doc.Root.Elements("Days").Where(i => (int)i.Element("Id") == Convert.ToInt32(counter)).FirstOrDefault();
                        int day_Name = Convert.ToInt32(objNode.Descendants("Id").FirstOrDefault().Value);
                        var flagValue = objNode.Descendants("Flag").FirstOrDefault().Value;
                        var activeStatus = objNode.Descendants("SlotStatus").FirstOrDefault().Value;
                        if (flagValue.ToString() == "false" && day_Name == dayId)
                        {
                            string str = "Active,Active,Active,Active,Active,Active,Active,Active,Active,Active";
                            objNode.SetElementValue("Flag", "true");
                            node.SetElementValue("SlotStatus", str);
                            doc.Save(path);

                            // flag = true and slotstatur = all active
                        }
                        else if (day_Name != dayId && flagValue.ToString() == "True")
                        {
                            objNode.SetElementValue("Flag", "false");
                            doc.Save(path);


                        }

                    }
                }
                return await Task.Run(() => true).ConfigureAwait(false);
            }
            catch (Exception exn)
            {
                return false;
            }

        }

    }

}
