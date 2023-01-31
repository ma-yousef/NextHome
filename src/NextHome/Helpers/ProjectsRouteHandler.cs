using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace NextHome.Helpers
{
    public class ProjectsRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            //System.Net.WebUtility.UrlDecode()            
            List<string> segments = umbracoContext.HttpContext.Request.Url.LocalPath.Split('/').ToList();
            segments.RemoveAt(2);
            string url = String.Join("/", segments);
            var contnt =  umbracoContext.ContentCache.GetByRoute(url);
            
            return contnt;
        }
    }
}