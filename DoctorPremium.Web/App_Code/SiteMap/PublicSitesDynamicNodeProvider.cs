using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorPremium.Services.Interfaces;
using MvcSiteMapProvider;

namespace DoctorPremium.Web.SiteMap
{
	public class PublicSitesDynamicNodeProvider : DynamicNodeProviderBase
	{
		private IPublicSiteService _publicSiteService;
		public PublicSitesDynamicNodeProvider(IPublicSiteService publicSiteService)
        {
			_publicSiteService = publicSiteService;
        }

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
		{
			var nodes = new List<DynamicNode>();
			var items = _publicSiteService.GetActivePublicSites();
			foreach (var item in items)
			{
				DynamicNode dynamicNode = new DynamicNode
				{
					Title = String.Format("Site for user {0}",item.UserId),
					ParentKey = "KeyOfParentNode",
					Key = "PublicSite" + item.Id,
					Action = "Index",
					Controller = "PublicSite",
				};
				dynamicNode.RouteValues.Add("id", item.Id);
				yield return dynamicNode;
			}
		}
	}
}