using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Editors;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace NextHome.App_Code
{
    [Umbraco.Web.Mvc.PluginController("NH")]
    public class DictionaryDropDownController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<DictionaryDropDownModel> GetDictionaryItems(string dictionaryKey, int parentContentId)
        {
            Content node = (Content)ApplicationContext.Current.Services.ContentService.GetById(parentContentId);
            if (node == null)
                return null;
            var dictionary = new Umbraco.Web.Dictionary.DefaultCultureDictionary(node.GetCultureInfo());
            return dictionary.GetChildren(dictionaryKey).Select(i=> new DictionaryDropDownModel { Key = i.Key, Value = i.Value});
        }
    }

    public class DictionaryDropDownModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}