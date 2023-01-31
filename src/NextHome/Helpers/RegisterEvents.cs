using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web;

namespace NextHome.Helpers
{
    public class RegisterEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);
            var languages = applicationContext.Services.LocalizationService.GetAllLanguages();
            var projectMenuItems = applicationContext.Services.ContentService.GetContentOfContentType(1095);
            var projectType = applicationContext.Services.LocalizationService.GetDictionaryItemByKey("ProjectType");
            var projectTypes = applicationContext.Services.LocalizationService.GetDictionaryItemChildren(projectType.Key);

            Dictionary<string, string> projMenuLang = new Dictionary<string, string>();
            foreach (var projMenu in projectMenuItems)
            {
                var lang = languages.Where(l => l.IsoCode == ((Umbraco.Core.Models.Content)projMenu).GetCultureInfo().Name).FirstOrDefault();
                if (!projMenuLang.ContainsKey(lang.IsoCode))
                    projMenuLang.Add(lang.IsoCode, projMenu.Name);
            }

            foreach (var item in projectTypes)
            {
                foreach (var translated in item.Translations)
                {
                    if (!projMenuLang.Keys.Any(i => i == translated.Language.IsoCode.ToString()))
                        continue;

                    var projects = projMenuLang[translated.Language.IsoCode.ToString()];
                    RouteTable.Routes.MapUmbracoRoute(
                        String.Format("Project{0}{1}Route", translated.Language.IsoCode, item.ItemKey),
                        String.Format("{0}/{1}/{{projectName}}", projects, translated.Value),
                        new
                        {
                            controller = "Projects",
                            action = "Project"
                        },
                        new ProjectsRouteHandler());

                    RouteTable.Routes.MapUmbracoRoute(
                        String.Format("Building{0}{1}Route", translated.Language.IsoCode, item.ItemKey),
                        String.Format("{0}/{1}/{{projectName}}/{{buildingName}}", projects, translated.Value),
                        new
                        {
                            controller = "Building",
                            action = "Building"
                        },
                        new ProjectsRouteHandler());
                }
            }


        }

    }
}