using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Web.UI.JavaScript;
using System.Web.Routing;
using System.Web.Mvc;
using Umbraco.Web;
using NextHome.Controllers;
using System.Configuration;

namespace NextHome.App_Code
{
    public class ServerVariableParserEvent : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ServerVariablesParser.Parsing += ServerVariablesParser_Parsing;
        }

        void ServerVariablesParser_Parsing(object sender, Dictionary<string, object> e)
        {
            if (HttpContext.Current == null) return;
            var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

            var mainDictionary = new Dictionary<string, object>();
            mainDictionary.Add("WRApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<WRApiController>(controller => controller.GetAllRquests()));
            
            if (!e.Keys.Contains("DomainUrl"))
                e.Add("DomainUrl", ConfigurationManager.AppSettings["Domain"]);

            if (!e.Keys.Contains("WebRequests"))
                e.Add("WebRequests", mainDictionary);

        }
    }
}