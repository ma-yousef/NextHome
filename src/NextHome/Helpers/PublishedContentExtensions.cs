using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Helpers;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace NextHome.Helpers
{
    public static class PublishedContentExtensions
    {
        public static bool HasGridValue(this IPublishedContent contentItem, string alias)
        {
            if (contentItem.HasValue(alias))
            {
                var stringValue = contentItem.GetPropertyValue<string>(alias);
                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    var grid = Json.Decode(stringValue);

                    if (grid.sections[0].rows.Length >= 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static IEnumerable<Videolizer.Umbraco.UmbVideolizerVideo> GetGridVideos(this IPublishedContent contentItem, string alias)
        {
            List<Videolizer.Umbraco.UmbVideolizerVideo> lstVidoes = new List<Videolizer.Umbraco.UmbVideolizerVideo>();
            if (contentItem.HasValue(alias))
            {
                var stringValue = contentItem.GetPropertyValue<string>(alias);
                
                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    var grid = JObject.Parse(stringValue);

                    var tokens = grid.SelectTokens("$.....value");
                    foreach (JToken tocken in tokens)
                    {
                        lstVidoes.Add(JsonConvert.DeserializeObject<Videolizer.Umbraco.UmbVideolizerVideo>(tocken.ToString()));
                    }
                }
            }

            return lstVidoes;

        }
    }
}