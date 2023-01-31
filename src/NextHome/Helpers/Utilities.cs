using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace NextHome.Helpers
{
    public static class Utilities
    {
        public static bool SendEmail(string emailFrom, string emailFromName, string[] emailTo, string mailBody, string subject, bool highPriority = false, string emailTemplate = null, bool IsHtml = false, AttachmentCollection AttachmentList = null)
        {
            try
            {
                if (emailTo == null || emailTo.Length == 0)
                {
                    throw new Exception("Mail To list is empty");
                }


                MailMessage mailMsg = new MailMessage();
                if (!String.IsNullOrEmpty(emailFrom))
                {
                    mailMsg.From = new MailAddress(emailFrom, emailFromName);
                    mailMsg.ReplyTo = new MailAddress(emailFrom, emailFromName);
                }
                else
                {
                    //mailMsg.From = new MailAddress("noreplay@" + ConfigurationManager.AppSettings["Domain"]);
                    mailMsg.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                }

                foreach (string mailAddress in emailTo)
                {
                    mailMsg.To.Add(mailAddress);
                }

                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = highPriority ? MailPriority.High : MailPriority.Normal;
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = IsHtml;
                mailMsg.Body = mailBody;

                if (emailTemplate != null)
                {
                    string templatesPath = ConfigurationManager.AppSettings["Domain"] + ConfigurationManager.AppSettings["EmailTemplatesPath"] + "/" + emailTemplate.ToString() + ".html";
                    var webRequest = System.Net.WebRequest.Create(templatesPath);

                    using (var response = webRequest.GetResponse())
                    using (var content = response.GetResponseStream())
                    using (var reader = new StreamReader(content))
                    {
                        string templateBody = reader.ReadToEnd();
                        if (emailTemplate != null && !string.IsNullOrEmpty(templateBody))
                        {
                            mailMsg.Body = templateBody.Replace("{TextBody}", mailBody);
                        }
                    }
                }


                if (AttachmentList != null)
                {
                    foreach (var item in AttachmentList)
                    { mailMsg.Attachments.Add(item); }
                }

                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailServer"], Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]));
                smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["EmailPassword"]);

                smtpClient.Send(mailMsg);
                return true;
            }
            catch (Exception e)
            {
                // throw e;

                return true;
            }
        }

    }
}