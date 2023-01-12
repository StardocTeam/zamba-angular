using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core.Searchs;
using Zamba.Core;
using Zamba;
using Zamba.Framework;

namespace Zamba.Web.Models
{
    public class SearchHelper
    {
        public Search GetSearch(ref Filter filters)
        {
            Search s = new Search();

            foreach (var f in filters.Parameters)
	        {
                switch ((SearchFilterType)f.Type)
                {
                    case SearchFilterType.All:
                        break;
                    case SearchFilterType.Attribute:
                        IndexsBusiness indexBusiness = new IndexsBusiness();
                        IIndex index = indexBusiness.GetIndex(f.Id);
                        indexBusiness = null;
                        index.Data = f.Value;//DataTemp
                        s.AddIndex(index);
                        break;
                    case SearchFilterType.Entity:
                        DocTypesBusiness DTB = new DocTypesBusiness();
                        Zamba.Core.DocType doctype = DTB.GetDocType(f.Id);
                        DTB = null;
                        s.AddDocType(doctype);
                        break;
                    case SearchFilterType.UserId:
                        s.UserId = f.Id;
                        break;
                    case SearchFilterType.WorkflowId:
                        s.WorkflowId = f.Id;
                        break;
                    case SearchFilterType.StepId:
                        s.StepId = f.Id;
                        break;
                    case SearchFilterType.StepStateId:
                        s.StepStateId = f.Id;
                        break;
                    case SearchFilterType.TaskStateId:
                        s.TaskStateId = f.Id;
                        break;
                    default:
                        Console.WriteLine(f.Type);
                        break;
                }
	        }

            if (s.Indexs != null && s.Doctypes != null && s.Indexs.Count > 0 && s.Doctypes.Count > 0)
            {
                GetSameNameIndexes(s);
            }

            return s;
        }

        //Debido a que el front end no muestra indices con nombres repetido es necesario checkear si las entidades 
        //seleccionadas poseen indices con el mismo nombre y agregar los indices que falten
        public void GetSameNameIndexes(Search search)
        {
            List<IIndex> indexes = search.Indexs;
            List<IDocType> docTypes = search.Doctypes;

            foreach (DocType docType in docTypes)
            {
                foreach (Zamba.Core.Index docIndex in docType.Indexs)
                {
                    foreach (Zamba.Core.Index index in indexes)
                    {
                        if (index.Name == docIndex.Name && index.ID != docIndex.ID)
                        {
                            //docIndex.DataTemp = index.DataTemp;
                            docIndex.Data = index.Data;
                            search.AddIndex(docIndex);
                        }
                    }
                }
            }

            docTypes = null;
            indexes = null;
        }
    }
}