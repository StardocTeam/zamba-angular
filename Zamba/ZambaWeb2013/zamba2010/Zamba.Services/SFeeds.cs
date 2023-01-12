using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SFeeds:IService
    {
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Feeds;
        }

        public List<ZFeed> GetFeeds(long userId)
        {
            ZFeedBusiness feedBusiness = new ZFeedBusiness();

            return feedBusiness.GetFeeds(userId);
        }
    }
}
