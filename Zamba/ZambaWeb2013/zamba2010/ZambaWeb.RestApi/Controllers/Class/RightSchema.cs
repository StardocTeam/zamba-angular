using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Zamba.Core;
using System.Collections;
using System.Runtime.Caching;
using Zamba;

namespace ZambaWeb.RestApi.Models
{
    public class RightsSchema
    {
        UserBusiness UB = new UserBusiness();
        public List<Entity> GetRightsSchema(Int64 userID, FillTypes FillType)
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
                IUser currentUser = UB.ValidateLogIn(userID, ClientType.WebApi);
            }

            List<Entity> entities = new List<Entity>();
            if (Zamba.Core.Cache.DocTypesAndIndexs.hsFiltersEntities.ContainsKey(userID) == false)
            {
                lock (Zamba.Core.Cache.DocTypesAndIndexs.hsFiltersEntities)
                {
                    if (Zamba.Core.Cache.DocTypesAndIndexs.hsFiltersEntities.ContainsKey(userID) == false)
                    {
                        DocTypesBusiness DTB = new DocTypesBusiness();
                        List<DocType> docTypes = DTB.GetDocTypesbyUserRights(userID, RightsType.View);
                        DTB = null;
                        foreach (DocType doctype in docTypes)
                        {
                            Entity entity = new Entity(doctype.ID, doctype.Name, 0, doctype.ObjecttypeId);

                            if (FillType == FillTypes.WithIndexs)
                            {
                                IndexsBusiness IB = new IndexsBusiness();
                                List<IIndex> Indexs = IB.getIndexsByDocTypeIdAndRightTypeAsIIndex(doctype.ID, userID, RightsType.IndexView);

                                if (Indexs.Count >= 1)
                                {
                                    foreach (IIndex i in Indexs)
                                    {
                                        entity.addIndex(new Index(i.ID, i.Name, doctype.ID, (int)i.Type));
                                    }
                                    entities.Add(entity);
                                }
                            }
                            else
                            {
                                entities.Add(entity);
                            }
                        }

                        Zamba.Core.Cache.DocTypesAndIndexs.hsFiltersEntities.Add(userID, entities);
                        return entities;
                    }
                }
            }

            entities = (List<Entity>)Zamba.Core.Cache.DocTypesAndIndexs.hsFiltersEntities[userID];
            return entities;

        }


        public List<Word> GetIndexWords(Int64 userID, int entity, int index)
        {
            //var ent = GetRightsSchema(userID);
            var w = new List<Word>();
            DataTable dsw = IndexsBusiness.getWordsByDocType(userID, RightsType.IndexView, entity, index);
            if (dsw.Rows.Count >= 1)
            {
                foreach (DataRow r in dsw.Rows)
                {
                    if (Convert.ToInt64(r[1].ToString()) == index)
                        w.Add(new Word(Convert.ToInt64(r[4]), r[5].ToString(), entity));
                }
            }
            return w;
        }

        public List<Entity> GetEntities(Int64 userID, FillTypes FillType)
        {
            return GetRightsSchema(userID, FillType);
        }
    }
}