using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core.Searchs;
using Zamba.Core;
using Zamba;
using Zamba.Membership;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Models
{
    public class SearchHelper
    {
        public SearchHelper()
        {
            ZCore ZC = new ZCore();
            if (Zamba.Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.Web");
            // IUser currentUser = UserBusiness.ValidateLogIn(22242, ClientType.WebApi);
            ZC.VerifyFileServer();
        }

        public Search GetSearch(ref Filter filters)
        {
            Search s = new Search();
            RightsSchema rightsSchema = new RightsSchema();
            var user = MembershipHelper.CurrentUser;
            DocTypesBusiness DTB = new DocTypesBusiness();


            foreach (var f in filters.Parameters)
            {
                switch (f.Type)
                {
                    case SearchFilterType.All:
                        if (s.Doctypes == null)
                            s.Doctypes = DTB.GetDocTypesbyUserRights(user.ID, RightsType.Buscar).ToList<IDocType>();


                        
                        //ESTE ESTA OK PORQUE VALIDA LAS ENTIDADES QUE EL USUARIO TIENE PERMISO


                        //s.Doctypes = ZCore.GetInstance().GetDocTypes();
                        //s.Doctypes = rightsSchema.GetEntities(user.ID, FillTypes.WithIndexs);

                        if (f.Value != null && f.Value != string.Empty)
                        {
                            if (string.IsNullOrEmpty(s.Textsearch))
                                s.Textsearch = f.Value;
                            else
                                s.Textsearch = string.Concat(s.Textsearch, " ", f.Value);
                        }
                        else
                        {
                            throw new Exception("Debe Ingresar Parametros de Busqueda");
                        }
                        break;

                    case SearchFilterType.Attribute:
                        IndexsBusiness indexBusiness = new IndexsBusiness();
                        IIndex index = indexBusiness.GetIndex(f.Id);
                        indexBusiness = null;
                        index.Data = f.Value;//DataTemp
                        index.Operator = f.Operator;
                        s.AddIndex(index);
                        //ESTE ESTA OK PORQUE VALIDA LAS ENTIDADES QUE EL USUARIO TIENE PERMISO

                        break;

                    case SearchFilterType.Entity:
                        DocType doctype = DTB.GetDocType(f.Id);
                        var Doctypes = DTB.GetDocTypesbyUserRights(user.ID, RightsType.Buscar).ToList<IDocType>();
                        //ESTE ESTA OK PORQUE VALIDA LAS ENTIDADES QUE EL USUARIO TIENE PERMISO
                       foreach (IDocType e in Doctypes)
                        {
                            if (e.ID == doctype.ID)
                            {
                                s.AddDocType(doctype);
                                break;

                            }
                        }

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

            if (s.Doctypes == null || s.Doctypes.Count == 0) {
                s.Doctypes = DTB.GetDocTypesbyUserRights(user.ID, RightsType.Buscar).ToList<IDocType>();
            }

            if (s.Doctypes == null || s.Doctypes.Count == 0)
            {
                throw new Exception("El usuario no tiene permiso para consultar esa entidad");
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
                            docIndex.DataTemp = index.DataTemp;
                            docIndex.Operator = index.Operator;
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