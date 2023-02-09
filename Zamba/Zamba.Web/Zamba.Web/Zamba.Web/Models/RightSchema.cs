using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Zamba.Core;
using System.Collections;
using System.Runtime.Caching;

namespace Zamba.Web.Models
{
    public class RightsSchema
    {
        private List<Entity> entities;

        public RightsSchema(long userID)
        {
            entities = new List<Entity>();
            Dictionary<long, string> entityNames;
            ObjectCache cache = MemoryCache.Default;
            DocTypesBusiness DTB = new DocTypesBusiness();

            //check the cache for entities names   
            if (!cache.Contains("AllEntityNames"))
            {
                //if it isn't there, get it from DB
                DataTable dt = DTB.GetDocTypeNamesAndIds();
                entityNames = new Dictionary<long, string>();
                foreach (DataRow row in dt.Rows)
                {
                    entityNames.Add(Convert.ToInt64(row[0]), row[1].ToString().Trim());
                }
                //Save entities names in cache
                cache.Add("AllEntityNames", entityNames, new CacheItemPolicy());

            }
            entityNames = (Dictionary<long, string>)cache.Get("AllEntityNames");
            List<DocType> docTypes = DTB.GetDocTypesbyUserRights(userID, RightsType.View);
            DTB = null; 
            foreach (DocType doc in docTypes)
            {
                Entity entity = new Entity(doc.ID, entityNames[doc.ID], 0);
                IndexsBusiness IB = new IndexsBusiness();

                entity.indexes =  IB.getIndexsByDocTypeIdAndRightTypeAsIIndex(Convert.ToInt64(doc.ID), userID, RightsType.IndexView);
                entities.Add(entity);
            }
        }

        public List<Entity> GetEntities()
        {
           return entities;
        }
    }
}