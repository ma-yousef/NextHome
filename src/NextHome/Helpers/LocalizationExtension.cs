using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace NextHome.Helpers
{
    public static class LocalizationExtension
    {
        public static ILanguage GetLanguageByContent(this ILocalizationService localizationService, IContent content)
        {
            ServiceContext services = Umbraco.Core.ApplicationContext.Current.Services;

            if (content == null) return null;

            // There isn't a DomainService in Umbraco yet so we need to use the legacy Domain API
            var domain = services.DomainService.GetAssignedDomains(content.Id, true).FirstOrDefault(); // dummy domain name : example: *1234

            return (domain == null)
              ? localizationService.GetLanguageByContent(content.Parent()) // recursive
              : localizationService.GetLanguageByIsoCode(domain.LanguageIsoCode);
        }

        public static CultureInfo GetCultureInfo(this Content content)
        {
            ServiceContext services = Umbraco.Core.ApplicationContext.Current.Services;
            var lang = services.LocalizationService.GetLanguageByContent(services.ContentService.GetById(content.Id));
            return lang == null ? CultureInfo.CurrentCulture : lang.CultureInfo;
        }
    }
}