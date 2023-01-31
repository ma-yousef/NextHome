using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Optimization;
using Umbraco.Web;

namespace NextHome
{
    public class Global : UmbracoApplication
    {
        static Timer checkStatusTimer = null;
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            // Code that runs on application startup
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            base.OnApplicationStarted(sender, e);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application.Add("AuthAllowed", true);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var user = Umbraco.Core.ApplicationContext.Current.Services.UserService.GetByUsername("superadmin@nexthome.com");

            Application["AuthAllowed"] = user.IsApproved;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!(bool)Application["AuthAllowed"])
            {
                string strUrl = Request.Url.ToString().ToLower();
                List<string> skipped = new List<string>() { "images", "media", "skins", "statuschecker", "app_plugins", "error" };

                if (!skipped.Exists(s => strUrl.Contains(s)))
                {
                    //if (strUrl.Contains("en"))
                    //    Response.Redirect(Request.Url.AbsoluteUri.Split('/')[2] + "/en/UnexpectedError");
                    //else
                    //    Response.Redirect(Request.Url.AbsoluteUri.Split('/')[2] + "/UnexpectedError");

                    throw new Exception("Internal Server Error (Error505");
                }
            }
        }

    }
}