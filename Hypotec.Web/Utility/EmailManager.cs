
using Hypotec.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Web.Utility
{
    public class EmailManager
    {

        readonly IConfiguration _configuration;
        public EmailModel _emailModel;
        public EmailManager(IOptions<EmailModel> emailModel, IConfiguration configuration)
        {
            _emailModel = emailModel.Value;
            _configuration = configuration;
        }
        /// <summary>
        /// send mail to email
        /// </summary>
        /// <param name="To"></param>
        /// <param name="link"></param>
        /// <param name="Subjet"></param>
        /// <param name="body"></param>
        public void SendEmail(string To, string link, string Subjet, string body)
        {
            try
            {
                var client = new SendGridClient("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");
                StringBuilder emailTemplate = new StringBuilder();
                string from = _configuration.GetValue<string>("SendGridMailSetting:Emailfrom");
                string subject = Subjet;
                string Body = body + link;

                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = Body,
                };
                msgs.AddTo(new EmailAddress(To, "Hypotec-MGLD"));
                var responses = client.SendEmailAsync(msgs);

            }

            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// SendEmailToFreddy
        /// </summary>
        /// <param name="contactMessaageModel"></param>
        /// <returns></returns>
        public bool SendEmailToFreddy(ContactMessaageModel contactMessaageModel)
        {
            try
            {
                var client = new SendGridClient("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");
                StringBuilder emailTemplate = new StringBuilder();
                string from = _configuration.GetValue<string>("SendGridMailSetting:Emailfrom");
                const string subject = "Message";
                string body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\Contact.html");
                body = body.Replace("#Message", contactMessaageModel.MessageToFreddy);

                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                msgs.AddTo(new EmailAddress("hypotec@yopmail.com", "Hypotec-MGLD"));
                var responses = client.SendEmailAsync(msgs);
                return true;
            }

            catch (Exception)
            {
                return false;

            }
        }
        /// <summary>
        /// SendEmailToOptOut
        /// </summary>
        /// <param name="communicationOptOutModel"></param>
        /// <returns></returns>
        public bool SendEmailToOptOut(CommunicationOptOutModel communicationOptOutModel)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "\\wwwroot\\Template\\pathotecLogo";
                var client = new SendGridClient("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");
                StringBuilder emailTemplate = new StringBuilder();
                string from = _configuration.GetValue<string>("SendGridMailSetting:Emailfrom");
                const string subject = "Communication-Opt-Out";
                string body = PopulateBody(communicationOptOutModel);
                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                msgs.AddTo(new EmailAddress(communicationOptOutModel.EmailAddress, "Hypotec-MGLD"));
                var responses = client.SendEmailAsync(msgs);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// SendEmailToCarrer
        /// </summary>
        /// <param name="carrerDetailModel"></param>
        /// <returns></returns>
        public bool SendEmailToCarrer(CarrerDetailModel carrerDetailModel)
        {

            try
            {
                var client = new SendGridClient("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");
                StringBuilder emailTemplate = new StringBuilder();
                var from = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"));
                const string subject = "Jobs";
                string body = JobsBody(carrerDetailModel);
                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                string readData = string.Empty;
                if (carrerDetailModel.Attachment.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        carrerDetailModel.Attachment.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();
                        readData = Convert.ToBase64String(fileBytes);
                    }
                }

                msgs.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {
                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content = readData,
                        Filename = carrerDetailModel.Attachment.FileName,
                        Type = carrerDetailModel.Attachment.Name,
                        Disposition = "attachment"
                    }
                };
                msgs.AddTo(new EmailAddress(carrerDetailModel.Email, "Hypotec-MGLD"));
                var response = client.SendEmailAsync(msgs);
                return true;
            }


            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// SendAgentSlot
        /// </summary>
        /// <param name="slotBookModel"></param>
        /// <returns></returns>
        public async Task<bool> SendAgentSlot(SlotBookModel slotBookModel)
        {
            try
            {
                //   var client = new SendGridClient("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");
                
                    // var client = new SendGridClient("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");
                var client = new SendGridClient("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");
                
                StringBuilder emailTemplate = new StringBuilder();
                const string subject = "Slot Booking";
                string body = SlotBody(slotBookModel);
                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                var recipients = new List<EmailAddress>
                {
            new EmailAddress(slotBookModel.Email, slotBookModel.Name),
                   new EmailAddress("hypotec@yopmail.com", "Hypotec-MGLD")
                };
                msgs.AddTos(recipients);
                var response = await client.SendEmailAsync(msgs).ConfigureAwait(false);
                if (response.IsSuccessStatusCode == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// SendMessageReview
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns></returns>
        public bool SendMessageReview(ReviewModel reviewModel)
        {
            try
            {
                var client = new SendGridClient("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");
                const string subject = "Review Message";
                string body = ReviewMessage(reviewModel);
                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                msgs.AddTo(new EmailAddress(reviewModel.Email, "Hypotec-MGLD"));
                var response = client.SendEmailAsync(msgs);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// SendContctUsMessage
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns></returns>
        public bool SendContctUsMessage(ReviewModel reviewModel)
        {
            try
            {
                var client = new SendGridClient("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");
                StringBuilder emailTemplate = new StringBuilder();
                const string subject = "Contact Us Message";
                string body = ReviewContactUs(reviewModel);
                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                msgs.AddTo(new EmailAddress(reviewModel.Email, "Hypotec-MGLD"));
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// SendAgentExperience
        /// </summary>
        /// <param name="slotBookModel"></param>
        /// <returns></returns>
        public bool SendAgentExperience(SlotBookModel slotBookModel)
        {
            try
            {
                var client = new SendGridClient("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");
                StringBuilder emailTemplate = new StringBuilder();
                string from = _configuration.GetValue<string>("SendGridMailSetting:Emailfrom");
                const string subject = "Agent Experience";
                string body = AgentExperienceBody(slotBookModel);

                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                msgs.AddTo(new EmailAddress(slotBookModel.Email, "Hypotec-MGLD"));
                var responses = client.SendEmailAsync(msgs);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// SendEmailToRequestCallBack
        /// </summary>
        /// <param name="sendMailByAdvisor"></param>
        /// <returns></returns>
        public bool SendEmailToRequestCallBack(SendMailByAdvisor sendMailByAdvisor)
        {
            try
            {
                var client = new SendGridClient("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");
                const string subject = "Request Call Back";
                string body = RequestCallBackBody(sendMailByAdvisor);

                var msgs = new SendGridMessage()
                {
                    From = new EmailAddress(_configuration.GetValue<string>("SendGridMailSetting:Emailfrom"), _configuration.GetValue<string>("SendGridMailSetting:EmailfromName")),
                    Subject = subject,
                    HtmlContent = body,
                };
                msgs.AddTo(new EmailAddress(sendMailByAdvisor.EmailId, "Hypotec-MGLD"));
                var response = client.SendEmailAsync(msgs);
                return true;

            }


            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// PopulateBody
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string PopulateBody(CommunicationOptOutModel model)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                var path = Directory.GetCurrentDirectory();
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\CommunicationOptOut.html");
                if (model.Address != null)
                {

                    body = body.Replace("#Address", model.Address);
                    body = body.Replace("#City", model.City);
                    body = body.Replace("#State", model.State);
                    body = body.Replace("#ZipCode", model.ZipCode);
                    body = body.Replace("<div id='divMail' style='display:none'>", "<div id='divMail' style='display:block'>");
                }
                else
                {
                    body = body.Replace("#Address", "");
                    body = body.Replace("#City", "");
                    body = body.Replace("#State", "");
                    body = body.Replace("#ZipCode", "");
                    //body = body.Replace("<div id='divMail' style='display:none'>", "<div id='divMail' style='display:block'>");
                }

                if (model.PhoneNumberCall != null)
                {
                    body = body.Replace("#PhoneNumberCall", model.PhoneNumberCall);
                    body = body.Replace("<div id='divCall' style='display:none'>", "<div id='divCall' style='display:block'>");

                }
                else
                {
                    body = body.Replace("#PhoneNumberCall", "");
                    body = body.Replace("Phone Number:", "User did not otp for this option");
                }

                if (model.PhoneNumberText != null)
                {
                    body = body.Replace("#PhoneNumberText", model.PhoneNumberText);
                    body = body.Replace("<div id='divText' style='display:none'>", "<div id='divText' style='display:block'>");

                }
                else
                {
                    body = body.Replace("#PhoneNumberText", "");
                    body = body.Replace("Phone Number:", "User did not otp for this option");
                }

                if (model.EmailAddress != null)
                {
                    body = body.Replace("#EmailAddress", model.EmailAddress);
                    body = body.Replace("<div id='divEmail' style='display:none'>", "<div id='divEmail' style='display:block'>");

                }
                else
                {
                    body = body.Replace("#EmailAddress", model.EmailAddress);
                    body = body.Replace("Email Address:", "User did not otp for this option");
                }

                body = body.Replace("#Slct", model.Slct);
                return body;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// JobsBody
        /// </summary>
        /// <param name="carrerDetailModel"></param>
        /// <returns></returns>
        private string JobsBody(CarrerDetailModel carrerDetailModel)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                var path = Directory.GetCurrentDirectory();
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\Jobs.html");
                body = body.Replace("#FirstName", carrerDetailModel.FirstName);
                body = body.Replace("#LastName", carrerDetailModel.LastName);
                body = body.Replace("#PrimaryPhone", carrerDetailModel.PrimaryPhone);
                body = body.Replace("#Email", carrerDetailModel.Email);
                if (carrerDetailModel.PostId == "0")
                {
                    body = body.Replace("#Officer", "CRM Administrator");
                }
                if (carrerDetailModel.PostId == "1")
                {
                    body = body.Replace("#Officer", "Loan Officer");
                }
                if (carrerDetailModel.PostId == "2")
                {
                    body = body.Replace("#Officer", "Loan Processor");
                }
                if (carrerDetailModel.PostId == "3")
                {
                    body = body.Replace("#Officer", "Social Media Specialist");
                }
                if (carrerDetailModel.PostId == "4")
                {
                    body = body.Replace("#Officer", "Underwriter");
                }
                return body;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// SlotBody
        /// </summary>
        /// <param name="slotBookModel"></param>
        /// <returns></returns>
        private string SlotBody(SlotBookModel slotBookModel)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\AgentSlotBook.html");
                body = body.Replace("#Name", slotBookModel.Name);
                body = body.Replace("#Phone", slotBookModel.Phone);
                body = body.Replace("#Email", slotBookModel.Email);
                body = body.Replace("#Time", slotBookModel.Time);
                return body;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// ReviewMessage
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns></returns>
        private string ReviewMessage(ReviewModel reviewModel)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\ReviewMessage.html");
                body = body.Replace("#Name", reviewModel.Name);
                body = body.Replace("#PhoneNumber", reviewModel.PhoneNumber);
                body = body.Replace("#Email", reviewModel.Email);
                body = body.Replace("#Message", reviewModel.Message);
                return body;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// ReviewContactUs
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns></returns>
        private string ReviewContactUs(ReviewModel reviewModel)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\ReviewContactMessage.html");
                body = body.Replace("#Name", reviewModel.Name);
                body = body.Replace("#PhoneNumber", reviewModel.PhoneNumber);
                body = body.Replace("#Email", reviewModel.Email);
                body = body.Replace("#Message", reviewModel.Message);
                return body;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// AgentExperienceBody
        /// </summary>
        /// <param name="slotBookModel"></param>
        /// <returns></returns>
        private string AgentExperienceBody(SlotBookModel slotBookModel)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                var path = Directory.GetCurrentDirectory();
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\AgentExperience.html");
                body = body.Replace("#Phone", slotBookModel.Phone);
                body = body.Replace("#Email", slotBookModel.Email);
                body = body.Replace("#AreasCovered", slotBookModel.AreasCovered);
                body = body.Replace("#TellUs", slotBookModel.TellUs);
                body = body.Replace("#Name", slotBookModel.Name);
                return body;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// RequestCallBackBody
        /// </summary>
        /// <param name="sendMailByAdvisor"></param>
        /// <returns></returns>
        private string RequestCallBackBody(SendMailByAdvisor sendMailByAdvisor)
        {
            string body = string.Empty;
            string CurrentPath = string.Empty;
            try
            {
                var path = Directory.GetCurrentDirectory();
                body = System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\wwwroot\Template\RequestCallBack.html");
                body = body.Replace("#Name", sendMailByAdvisor.Name);
                body = body.Replace("#PhoneNumber", sendMailByAdvisor.PhoneNumber);
                body = body.Replace("#EmailId", sendMailByAdvisor.EmailId);
                body = body.Replace("#Message", sendMailByAdvisor.Message);
                return body;
            }
            catch
            {
                throw;
            }

        }
    }
}
