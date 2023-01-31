using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace NextHome.Controllers
{
    public class BuildingController : RenderMvcController
    {
        // GET: Building
        public ActionResult Building(RenderModel model, string projectName, string buildingName)
        {
            return base.Index(model);
        }
    }
}