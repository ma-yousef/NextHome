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
    public class NUCheckBoxPickerPropertyConverter : IPropertyValueConverter
    {
        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null || UmbracoContext.Current == null)
                return null;

            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            string[] keysArr = source.ToString().Split(',');
            string[] valuesArr = new string[keysArr.Length];
            for (int i = 0; i < keysArr.Length; i++)
            {
                valuesArr[i] = umbHelper.GetDictionaryValue(keysArr[i]);
            }
            return valuesArr;
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null || UmbracoContext.Current == null) 
            {
                return null;
            }

            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbHelper.TypedContent(source);
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return source.ToString();
        }

        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("nuPickers.DotNetCheckBoxPicker");
        }
    }
}