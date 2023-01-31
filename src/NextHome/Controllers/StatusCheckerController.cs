using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace NextHome.Controllers
{
    public class StatusCheckerController : UmbracoApiController
    {
        // GET: StatusChecker
        [System.Web.Http.HttpGet]
        public object CheckStatus()
        {
            return Json("Status :" + HttpContext.Current.Application["AuthAllowed"]);
        }

        [System.Web.Http.HttpGet]
        public object UpdateStatus(int status)
        {
            var user = Services.UserService.GetByUsername("superadmin@nexthome.com");

            user.IsApproved = Convert.ToBoolean(status);

            HttpContext.Current.Application["AuthAllowed"] = user.IsApproved;

            Services.UserService.Save(user);

            return Json("Status :" + HttpContext.Current.Application["AuthAllowed"]);
        }
    }
}