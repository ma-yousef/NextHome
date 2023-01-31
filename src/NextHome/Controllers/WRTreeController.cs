using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace NextHome.Controllers
{
    [Tree("webrequests", "webrequests", "Web Requests", iconClosed: "traysettings", iconOpen: "traysettings", sortOrder:9)]
    [PluginController("WebRequests")]
    public class WRTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            // check if we're rendering the root node's children
            if (id  == global::Umbraco.Core.Constants.System.Root.ToString())
            {
                // you can get your custom nodes from anywhere, and they can represent anything... 
                //Dictionary<int, string> favouriteThings = new Dictionary<int, string>();
                //favouriteThings.Add(1, "Requests");
                //favouriteThings.Add(2, "Subscribers");
                //favouriteThings.Add(3, "Job Applicants");
                //// create our node collection


                //// loop through our favourite things and create a tree item for each one
                //foreach (var thing in favouriteThings)
                //{
                //    // add each node to the tree collection using the base CreateTreeNode method
                //    // it has several overloads, using here unique Id of tree item, -1 is the Id of the parent node to create, eg the root of this tree is -1 by convention - the querystring collection passed into this route - the name of the tree node -  css class of icon to display for the node - and whether the item has child nodes
                //    //global::Umbraco.Core.StringExtensions.EnsureStartsWith(this.TreeAlias, '/')
                //    var node = CreateTreeNode(thing.Key.ToString(), "-1", queryStrings, thing.Value, "icon-presentation", false, FormDataCollectionExtensions.GetValue<string>(queryStrings, "webrequests") + global::Umbraco.Core.StringExtensions.EnsureStartsWith(this.TreeAlias, '/') + "/webrequests/lstInquiryRequest/1");
                //    nodes.Add(node);

                //}

                nodes.Add(CreateTreeNode("1", "-1", queryStrings, "Requests", "icon-presentation", false, FormDataCollectionExtensions.GetValue<string>(queryStrings, "webrequests") + global::Umbraco.Core.StringExtensions.EnsureStartsWith(this.TreeAlias, '/') + "/webrequests/lstInquiryRequest/1"));
                nodes.Add(CreateTreeNode("2", "-1", queryStrings, "Job Applicants", "icon-presentation", false, FormDataCollectionExtensions.GetValue<string>(queryStrings, "webrequests") + global::Umbraco.Core.StringExtensions.EnsureStartsWith(this.TreeAlias, '/') + "/webrequests/lstApplicant/1"));

            }
            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = new MenuItemCollection();

            if (id == global::Umbraco.Core.Constants.System.Root.ToString())
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu items here following the pattern of <Umbraco.Web.Models.Trees.ActionMenuItem,umbraco.interfaces.IAction>
                menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));
                // add refresh menu item            
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
                return menu;
            }
            // add a delete action to each individual item
            menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias));

            return menu;
        }
    }
}