using Examine;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using umbraco;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;

namespace NextHome.Controllers
{
    [Route("api/[controller]")]
    public class SearchApiController : UmbracoApiController
    {
        [HttpGet]
        public string Test()
        {
            return "ws";
        }

        [HttpGet]
        public object SearchUnits(string lang, int pageIndex, int pageSize, string location = null, string unitType = null, int? bedRooms = null, int? bathRooms = null, float? areaFrom = null, float? areaTo = null, decimal? priceFrom = null, decimal? priceTo = null)
        {
            IContent root = Services.ContentService.GetRootContent().Where(i => Services.DomainService.GetAssignedDomains(i.Id, true).FirstOrDefault().LanguageIsoCode.Contains(lang)).FirstOrDefault();
            var units = uQuery.GetNodesByType("unit").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id);

            if (!String.IsNullOrEmpty(location))
            {
                units = units.Where(u => u.Parent.GetProperty<string>("project_City").Contains(location));
            }

            if (!String.IsNullOrEmpty(unitType))
            {
                units = units.Where(u => u.GetProperty<string>("unit_Type").Contains(unitType));
            }

            if (bedRooms.HasValue)
            {
                units = units.Where(u => u.GetProperty<int?>("unit_BedRooms") >= bedRooms);
            }

            if (bathRooms.HasValue)
            {
                units = units.Where(u => u.GetProperty<int?>("unit_BathRooms") >= bathRooms);
            }

            if (areaFrom.HasValue)
            {
                units = units.Where(u => !u.GetProperty<float?>("unit_Area").HasValue || u.GetProperty<float?>("unit_Area") >= areaFrom);
            }

            if (areaTo.HasValue)
            {
                units = units.Where(u => !u.GetProperty<float?>("unit_Area").HasValue || u.GetProperty<float?>("unit_Area") <= areaTo);
            }

            if (priceFrom.HasValue)
            {
                units = units.Where(u => !u.GetProperty<decimal?>("unit_Price").HasValue || u.GetProperty<decimal?>("unit_Price") >= priceFrom);
            }

            if (priceTo.HasValue)
            {
                units = units.Where(u => !u.GetProperty<decimal?>("unit_Price").HasValue || u.GetProperty<decimal?>("unit_Price") <= priceTo);
            }

            var result = units.OrderByDescending(u => u.GetProperty<bool>("unit_IsFeatured")).Skip(pageIndex * pageSize).Take(pageSize).Select(u => new
            {
                UnitName = u.Name,
                UnitUrl = u.Url,
                ProjectName = u.Parent.Name,
                MainImage = Umbraco.TypedMedia(u.GetProperty<Udi>("unit_MainImage")).Url,
                UnitType = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(u.GetProperty<string>("unit_Type")).Value,
                BedRooms = u.GetProperty<int>("unit_BedRooms"),
                BathRooms = u.GetProperty<int>("unit_BathRooms"),
                UnitArea = u.GetProperty<float?>("unit_Area"),
                IsSoldOut = u.GetProperty<bool>("unit_IsSoldOut")
            });

            return Json(result);
        }
    }
}
