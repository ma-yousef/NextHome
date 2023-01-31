using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using umbraco;
using umbraco.NodeFactory;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;

namespace NextHome.Controllers
{
    public class ServiceApiController : UmbracoApiController
    {
        string domainUrl = System.Configuration.ConfigurationManager.AppSettings["Domain"];

        [HttpGet]
        public object GetProjects(string lang)
        {

            IContent root = getRootContent(lang);
            var projects = uQuery.GetNodesByType("project").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id);

            var result = projects.Select(u => new
            {
                ProjectId = u.Id,
                Name = u.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(u.GetProperty<Udi>("project_MainImage")).Url,
                Description = u.GetProperty<string>("project_Description"),
                ProjectType = u.GetProperty<string>("project_Type"),
                City = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(u.GetProperty<string>("project_City")).Value,
                District = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(u.GetProperty<string>("project_District")).Value,
                FullAddress = u.GetProperty<string>("project_FullAddress")
            });

            return Json(result);
        }

        [HttpGet]
        public object GetProjectDetails(string lang, int projectId)
        {

            IContent root = getRootContent(lang);
            var project = uQuery.GetNodesByType("project").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id && i.Id == projectId).SingleOrDefault();


            // dynamic videos = JsonConvert.DeserializeObject(project.GetProperty<string>("project_Videos"));
            //string[] videosArr = new string[videos.sections[0].rows[0].areas.Count];

            //videos.sections[0].rows[0].areas[1].controls[0].value.url.ToString()

            //for (int i = 0; i < videos.sections[0].rows[0].areas.Count; i++)
            //{
            //    videosArr[i] = videos.sections[0].rows[0].areas[i].controls[0].value.embedUrl.ToString();
            //}
            //string videoUrl = (videos != null && videos.sections.Count > 0 && videos.sections[0].rows.Count > 0 && videos.sections[0].rows[0].areas.Count > 0) ?
            //    videos.sections[0].rows[0].areas[0].controls[0].value.embedUrl.ToString() : String.Empty;

            var result = new
            {
                ProjectId = project.Id,
                Name = project.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(project.GetProperty<Udi>("project_MainImage")).Url,
                Description = Regex.Replace(project.GetProperty<string>("project_Description"), "<[^>]*>", String.Empty),
                ProjectType = project.GetProperty<string>("project_Type"),
                City = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(project.GetProperty<string>("project_City")).Value,
                District = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(project.GetProperty<string>("project_District")).Value,
                FullAddress = project.GetProperty<string>("project_FullAddress"),
                AreaFrom = project.GetProperty<string>("project_AreaFrom"),
                AreaTo = project.GetProperty<string>("project_AreaTo"),
                PriceFrom = project.GetProperty<string>("project_PriceFrom"),
                PriceTo = project.GetProperty<string>("project_PriceTo"),
                Latitude = project.GetProperty<string>("project_Latitude"),
                Longitude = project.GetProperty<string>("project_Longitude"),
                ProjectFacilities = project.GetProperty<string>("project_Facilities").Split(','),
                Images = getImagesList(project.GetProperty<string>("project_Images")),
                VideoURL = getVideoUrl(project.GetProperty<string>("project_Videos")),
                HasBuildings = project.GetDescendantNodesByType("building").Count() > 0
            };

            return Json(result);
        }

        public object GetProjectBuildings(string lang, int projectId)
        {
            IContent root = getRootContent(lang);
            var buildings = uQuery.GetNodesByType("building").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id)
                .Where(b => b.Parent.Id == projectId);


            var result = buildings.Select(u => new
            {
                BuildingId = u.Id,
                BuildingName = u.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(u.GetProperty<Udi>("building_Image")).Url,
            });

            return Json(result);
        }

        public object GetUnits(string lang, int pageIndex = 0, int pageSize = 1000, int? projectId = null, string location = null, string unitType = null, int? bedRooms = null, int? bathRooms = null, float? areaFrom = null, float? areaTo = null, decimal? priceFrom = null, decimal? priceTo = null)
        {
            IContent root = getRootContent(lang);
            var units = uQuery.GetNodesByType("unit").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id);

            if (projectId.HasValue)
            {
                units = units.Where(u => u.GetAncestorOrSelfNodes().Any(i => i.Id == projectId));
            }

            if (!String.IsNullOrEmpty(location) && location.ToLower() != "null" && location.ToLower() != "undefined")
            {
                units = units.Where(u => u.GetAncestorOrSelfNodes().Any(i => i.GetProperty<string>("project_City").Contains(location)));
            }

            if (!String.IsNullOrEmpty(unitType) && unitType.ToLower() != "null" && unitType.ToLower() != "undefined")
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
                UnitId = u.Id,
                UnitName = u.Name,
                ProjectName = u.Parent.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(u.GetProperty<Udi>("unit_MainImage")).Url,
                UnitType = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(u.GetProperty<string>("unit_Type")).Value,
                Description = toShortString(getRawText(u.GetProperty<string>("unit_Description"))),
                BedRooms = u.GetProperty<int>("unit_BedRooms"),
                BathRooms = u.GetProperty<int>("unit_BathRooms"),
                UnitArea = u.GetProperty<float?>("unit_Area"),
                IsSoldOut = u.GetProperty<bool>("unit_IsSoldOut")
            });

            return Json(result);
        }

        public object GetUnitDetails(int unitId)
        {
            var unit = uQuery.GetNodesByType("unit").Where(i => i.Id == unitId).SingleOrDefault();
            var project = unit.GetAncestorNodes().Where(i => i.NodeTypeAlias == "project").FirstOrDefault();
            var result = new
            {
                UnitId = unit.Id,
                UnitName = unit.Name,
                ProjectName = project.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(unit.GetProperty<Udi>("unit_MainImage")).Url,
                Type = getDictionaryValue(unit.GetProperty<string>("unit_Type")),
                Category = getDictionaryValue(unit.GetProperty<string>("unit_Category")),
                Floor = getDictionaryValue(unit.GetProperty<string>("unit_Floor")),
                Flooring = getDictionaryValue(unit.GetProperty<string>("unit_Flooring")),
                Description = getRawText(unit.GetProperty<string>("unit_Description")),
                BedRooms = unit.GetProperty<int>("unit_BedRooms"),
                BathRooms = unit.GetProperty<int>("unit_BathRooms"),
                Area = unit.GetProperty<float?>("unit_Area"),
                IsSoldOut = unit.GetProperty<bool>("unit_IsSoldOut"),
                Images = getImagesList(unit.GetProperty<string>("unit_Images")),
                // Project Info
                ProjectDescription = getRawText(project.GetProperty<string>("project_Description")),
                Latitude = project.GetProperty<string>("project_Latitude"),
                Longitude = project.GetProperty<string>("project_Longitude"),
                FullAddress = project.GetProperty<string>("project_FullAddress"),
                Facilities = project.GetProperty<string>("project_Facilities").Split(','),
                City = getDictionaryValue(project.GetProperty<string>("project_City")),
                District = getDictionaryValue(project.GetProperty<string>("project_District")),
            };
            return Json(result);
        }

        public object GetAboutUs(string lang)
        {
            IContent root = getRootContent(lang);
            var aboutUs = uQuery.GetNodesByType("aboutUs").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id).SingleOrDefault();
            var result = new
            {
                Images = getImagesList(aboutUs.GetProperty<string>("aboutus_Images")),
                OurVision = getRawText(aboutUs.GetProperty<string>("aboutus_OurVision")),
                OurValues = getRawText(aboutUs.GetProperty<string>("aboutus_OurValues")),
                OurMission = getRawText(aboutUs.GetProperty<string>("aboutus_OurMission")),
                WhoWeAre = getRawText(aboutUs.GetProperty<string>("aboutus_WhoWeAre")),
                AboutUs = getRawText(aboutUs.GetProperty<string>("aboutus_AboutUs")),
                VideoUrl = getVideoUrl(aboutUs.GetProperty<string>("aboutus_video"))
            };
            return Json(result);
        }

        public object GetOffers(string lang)
        {
            IContent root = getRootContent(lang);
            var offers = root.Descendants().Where(i => i.Published)
                .Select(i => i.GetValue("global_Offer"))
                .Where(i => i != null)
                .Select(i => parseOffer(i)).Where(i => i != null).ToList();

            var rootOffer = root.GetValue("global_Offer");

            if (rootOffer != null)
                offers.Add(parseOffer(rootOffer));

            return Json(offers);
        }

        public object GetOfferDetails(int offerId)
        {
            var offer = uQuery.GetNodesByType("offer").Where(i => i.Id == offerId).SingleOrDefault();
            var result = new
            {
                OfferId = offer.Id,
                Name = offer.GetProperty<string>("offer_Name"),
                ImageUrl = domainUrl + Umbraco.TypedMedia(offer.GetProperty<Udi>("offer_MobileImage")).Url,
                ShowAfter = offer.GetProperty<string>("offer_showAfter"),
                HideAfter = offer.GetProperty<string>("offer_hideAfter"),
                LinkUrl = offer.GetProperty<string>("offer_LinkUrl"),
            };
            return Json(result);
        }

        public object GetNews(string lang, int pageIndex = 0, int pageSize = 1000)
        {
            IContent root = getRootContent(lang);
            var news = uQuery.GetNodesByType("newsItem").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id);
            var result = news.Skip(pageIndex * pageSize).Take(pageSize).Select(n => new
            {
                NewsId = n.Id,
                Title = n.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(n.GetProperty<Udi>("newsitem_MainImage")).Url,
                Description = toShortString(getRawText(n.GetProperty<string>("newsitem_Description")))
            });
            return Json(result);
        }

        public object GetNewsDetails(int newsId)
        {
            var news = uQuery.GetNodesByType("newsItem").Where(i => i.Id == newsId).SingleOrDefault();
            var result = new
            {
                NewsId = news.Id,
                Title = news.Name,
                MainImgUrl = domainUrl + Umbraco.TypedMedia(news.GetProperty<Udi>("newsitem_MainImage")).Url,
                Description = getRawText(news.GetProperty<string>("newsitem_Description")),
                Images = getImagesList(news.GetProperty<string>("newsitem_Gallery")),
            };
            return Json(result);
        }

        public object GetAllMedia(string lang)
        {
            IContent root = getRootContent(lang);
            var projects = uQuery.GetNodesByType("project").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id);
            var result = new
            {
                Projects = projects.Select(p => new
                {
                    ProjectName = p.Name,
                    MainImgUrl = domainUrl + Umbraco.TypedMedia(p.GetProperty<Udi>("project_MainImage")).Url,
                    Images = p.GetProperty<string>("project_Images").Split(',').Select(i => domainUrl + Umbraco.TypedMedia(Udi.Parse(i)).Url)
                }),

                Videos = projects.Select(p => getVideoUrl(p.GetProperty<string>("project_Videos"))).Distinct()
            };
            return Json(result);
        }

        public object GetJobVacancies(string lang)
        {
            IContent root = getRootContent(lang);
            var jobs = uQuery.GetNodesByType("jobVacancy").Where(i => i.GetAncestorByPathLevel(1).Id == root.Id);
            var result = jobs.Select(j => new
            {
                JobId = j.Id,
                JobName = j.Name,
                Description = getRawText(j.GetProperty<string>("job_Description")),
                Email = j.GetProperty<string>("job_Email")
            });
            return Json(result);
        }


        public object GetSearchLookups(string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            var result = new
            {
                //Umbraco.CultureDictionary.Culture
                UnitTypes = Umbraco.CultureDictionary.GetChildren("UnitType").Select(i => new { i.Key, i.Value }),
                Locations = Umbraco.CultureDictionary.GetChildren("Locations").Select(i => new { i.Key, i.Value })
            };
            return Json(result);
        }

        private IContent getRootContent(string lang)
        {
            return Services.ContentService.GetRootContent()
                 .Where(i => Services.DomainService.GetAssignedDomains(i.Id, true)
                 .FirstOrDefault().LanguageIsoCode == lang).FirstOrDefault();
        }

        private string toShortString(string str)
        {
            return str.Length > 50 ? str.Substring(0, 50) + "..." : str;
        }

        private string getRawText(string text)
        {
            return Regex.Replace(text, "<[^>]*>", String.Empty);
        }

        private string getDictionaryValue(string keyValuePair)
        {
            try
            {
                return JsonConvert.DeserializeObject<KeyValuePair<string, string>>(keyValuePair).Value;
            }
            catch
            {
                return keyValuePair;
            }
               
        }

        private string getVideoUrl(string vid)
        {
            try
            {
                dynamic videos = JsonConvert.DeserializeObject(vid);
                //string[] videosArr = new string[videos.sections[0].rows[0].areas.Count];

                //videos.sections[0].rows[0].areas[1].controls[0].value.url.ToString()

                //for (int i = 0; i < videos.sections[0].rows[0].areas.Count; i++)
                //{
                //    videosArr[i] = videos.sections[0].rows[0].areas[i].controls[0].value.embedUrl.ToString();
                //}
                return (videos != null
                    && videos.sections.Count > 0
                    && videos.sections[0].rows.Count > 0
                    && videos.sections[0].rows[0].areas.Count > 0) ?
                    videos.sections[0].rows[0].areas[0].controls[0].value.embedUrl.ToString() : String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }

        private IEnumerable<string> getImagesList(string strImages)
        {
            if (String.IsNullOrEmpty(strImages))
                return null;
            String[] udiArr = strImages.Split(',');
            if (udiArr.Length > 0)
                return udiArr.Select(i => domainUrl + Umbraco.TypedMedia(Udi.Parse(i)).Url);
            else
                return null;
        }

        private object parseOffer(object value)
        {
            try
            {
                dynamic offer = JsonConvert.DeserializeObject(value.ToString());

                return new
                {
                    OfferName = offer[0].offer_Name,
                    ImageUrl = domainUrl + Umbraco.TypedMedia(Udi.Parse(offer[0].offer_MobileImage.ToString())).Url,
                    LinkUrl = offer[0].offer_LinkUrl
                };
            }
            catch
            {
                return null;
            }
        }
    }
}