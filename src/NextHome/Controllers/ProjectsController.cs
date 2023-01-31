using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace NextHome.Controllers
{
    public class ProjectsController : RenderMvcController
    {
        // GET: Projects
        public ActionResult Project(RenderModel model, string projectName)
        {
            return base.Index(model);
        }
    }
}