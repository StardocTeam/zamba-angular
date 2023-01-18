using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.Core.Cache;

namespace Zamba.Core.Cache.Tests
{
	[TestClass()]
	public class CacheTest
	{
		[TestMethod()]
		public void ClearAllCacheTest()
		{
			CacheBusiness.ClearAllCache();
		}

		[TestMethod()]
		public void DocTypesAndIndexsclearAllTest()
		{
			DocTypesAndIndexs.clearAll();
		}

		[TestMethod()]
		public void OutlookclearAllTest()
		{
			Outlook.clearAll();
		}

		[TestMethod()]
		public void ResultsclearAllTest()
		{
			Results.clearAll();
		}

		[TestMethod()]
		public void SearchClearAllTest()
		{
			Search.ClearAll();
		}

		[TestMethod()]
		public void UsersAndGroupsClearAllTest()
		{
			UsersAndGroups.ClearAll();
		}

		[TestMethod()]
		public void VolumesClearAllTest()
		{
			Volumes.ClearAll();
		}

		[TestMethod()]
		public void WebServicesClearAllTest()
		{
			WebServices.ClearAll();
		}

		[TestMethod()]
		public void WorkflowsclearAllTest()
		{
			Workflows.clearAll();
		}
	}
}
