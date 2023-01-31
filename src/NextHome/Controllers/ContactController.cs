using NextHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using NextHome.Helpers;
using System.Configuration;
using System.Text;
using System.IO;

namespace NextHome.Controllers
{
    public class ContactController : SurfaceController
    {
        [HttpPost]
        public ActionResult SendRequest(InquiryRequest request)
        {
            if (String.IsNullOrEmpty(request.PhoneNo)) // trap for bot requests
            {
                StringBuilder mailBody = new StringBuilder(request.Message);
                mailBody.AppendLine();
                mailBody.AppendLine();
                mailBody.AppendLine("---------------------------------------------");
                mailBody.AppendLine(request.FullName);
                mailBody.AppendLine(request.MobileNo);
                Utilities.SendEmail(request.Email, request.FullName, new string[] { ConfigurationManager.AppSettings["DefaultEmail"] }, mailBody.ToString(), String.IsNullOrEmpty(request.ProjectName) ? "Message from Website" : request.ProjectName);
                new WRDataAccess(DatabaseContext.Database).InsertEntity(request);
            }
            return Content("Ok");
        }

        [HttpPost]
        public ActionResult UploadCV(string fullName, string vacancyName, string mobileNo, string email, string address, List<HttpPostedFileBase> files)
        {
            string path = String.Empty;
            if (files.Count > 0)
            {
                try
                {
                    string[] strArr = files[0].FileName.Split('.');
                    string ext = String.Empty;

                    if (strArr.Length > 0)
                        ext = strArr[strArr.Length - 1];

                    string fileName = Guid.NewGuid().ToString() + "." + ext;
                    path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    files[0].SaveAs(path);
                    Applicant applicant = new Applicant
                    {
                        FullName = fullName,
                        VacancyName = vacancyName,
                        CVFileName = fileName,
                        MobileNo = mobileNo,
                        Email = email,
                        Address =  address
                    };

                    new WRDataAccess(DatabaseContext.Database).InsertEntity(applicant);
                }
                catch (Exception ex)
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);

                    return Content(ex.Message);
                }
            }
            return Content("Ok");
        }
    }
}