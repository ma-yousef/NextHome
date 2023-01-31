using nuPickers.Shared.DotNetDataSource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace NextHome.App_Code
{
    public class FacilitiesDataSource : IDotNetDataSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            Content node = (Content)Umbraco.Core.ApplicationContext.Current.Services.ContentService.GetById(contextId);
            if (node == null)
                return null;
            var dictionary = new Umbraco.Web.Dictionary.DefaultCultureDictionary(node.GetCultureInfo());
            return dictionary.GetChildren("Facilities");
        }
    }

    public class ProjectTypeDataSource : IDotNetDataSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            Content node = (Content)Umbraco.Core.ApplicationContext.Current.Services.ContentService.GetById(contextId);
            if (node == null)
                return null;
            var dictionary = new Umbraco.Web.Dictionary.DefaultCultureDictionary(node.GetCultureInfo());
            return dictionary.GetChildren("ProjectType");
        }
    }

    public class UnitTypeDataSource : IDotNetDataSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            Content node = (Content)Umbraco.Core.ApplicationContext.Current.Services.ContentService.GetById(contextId);
            if (node == null)
                return null;
            var dictionary = new Umbraco.Web.Dictionary.DefaultCultureDictionary(node.GetCultureInfo());
            return dictionary.GetChildren("UnitType");
        }
    }

    public class FloorDataSource : IDotNetDataSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            Content node = (Content)Umbraco.Core.ApplicationContext.Current.Services.ContentService.GetById(contextId);
            if (node == null)
                return null;
            var dictionary = new Umbraco.Web.Dictionary.DefaultCultureDictionary(node.GetCultureInfo());
            return dictionary.GetChildren("Floor");
        }
    }

    public class FlooringDataSource : IDotNetDataSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            Content node = (Content)Umbraco.Core.ApplicationContext.Current.Services.ContentService.GetById(contextId);
            if (node == null)
                return null;
            var dictionary = new Umbraco.Web.Dictionary.DefaultCultureDictionary(node.GetCultureInfo());
            return dictionary.GetChildren("Flooring");
        }
    }
}