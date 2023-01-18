using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Zamba.Core;
using System.Collections;
using System.Runtime.Caching;

namespace ZambaWeb.RestApi.Models
{
    public class RightsSchema
    {
        private List<Entity> entities;

        public RightsSchema()
        {
            entities = new List<Entity>();
            Dictionary<long, string> entityNames;
            ObjectCache cache = MemoryCache.Default;
            long userID = 7;//obtener por parametros
            //check the cache for entities names   
            if (!cache.Contains("AllEntityNames"))
            {
                //if it isn't there, get it from DB
                DataTable dt = DocTypesBusiness.GetDocTypeNamesAndIds();
                entityNames = new Dictionary<long, string>();
                foreach (DataRow row in dt.Rows)
                {
                    entityNames.Add(Convert.ToInt64(row[0]), row[1].ToString().Trim());
                }
                //Save entities names in cache
                cache.Add("AllEntityNames", entityNames, new CacheItemPolicy());

            }
            entityNames = (Dictionary<long, string>)cache.Get("AllEntityNames");

            ArrayList docTypes = DocTypesBusiness.GetDocTypesbyUserRightsOfView(userID, RightsType.View);

            foreach (DocType doc in docTypes)
            {
                Entity entity = new Entity(doc.ID, entityNames[doc.ID], 0);

                DataTable ds = IndexsBusiness.getIndexByDocTypeId(Convert.ToInt32(doc.ID), userID, RightsType.IndexView);
                foreach (DataRow row in ds.Rows) 
                {
                    entity.addIndex(new Index(Convert.ToInt64(row[0]), row[1].ToString().Trim(), doc.ID));
                }
                entities.Add(entity);
            }
        }

        public List<Entity> GetEntities()
        {
           return entities;
        }
    }
}