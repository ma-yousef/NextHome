using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace NextHome.Helpers
{
    public class DictionaryDropDownPropertyConverter : IPropertyValueConverter
    {
        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
                return null;

            return JsonConvert.DeserializeObject<KeyValuePair<string, string>>(source.ToString());
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null || UmbracoContext.Current == null) 
            {
                return null;
            }

            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return new KeyValuePair<string,string>(((KeyValuePair<string, string>)source).Key, umbHelper.GetDictionaryValue(((KeyValuePair<string, string>)source).Key));
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return source.ToString();
        }

        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("DictionaryDropDown");
        }
    }
}