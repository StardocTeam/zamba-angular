using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;
using Zamba.Services;

namespace Zamba.Framework
{
    class FormsBrowserComponent
    {

        UserPreferences UP = new UserPreferences();
        RightsBusiness RiB = new RightsBusiness();
        UserBusiness UB = new UserBusiness();
        FormBusiness FB = new FormBusiness();


        //private string CompleteFormIndexs(IResult res, string strHtml)
        //{
        //    SIndex SIndex = new SIndex();
        //    SForms SForms = new SForms();
        //    string strIndexId;
        //    List<IIndex> indices;

        //        indices = SIndex.GetIndexs(res.ID, res.DocTypeId);

        //    if (indices != null)
        //    {
        //        System.Collections.Generic.List<long> GIDs = new System.Collections.Generic.List<long>();
        //        //Esta variable debe estar en false para que tome los permisos por atributos
        //        Boolean UseIndexsRights = Boolean.Parse(UP.getValue("UseViewRightsForIndexsOnForm", UPSections.FormPreferences, "False"));
        //        if (!(UseIndexsRights))
        //        {
        //            foreach (Zamba.Core.UserGroup UGroup in Zamba.Membership.MembershipHelper.CurrentUser.Groups)
        //            {
        //                GIDs.Add(((Zamba.Core.ZBaseCore)(UGroup)).ID);
        //            }
        //            GIDs.Add(Zamba.Membership.MembershipHelper.CurrentUser.ID);
        //        }

        //        bool input;
        //        bool specificAtributesRights;
        //        bool specificEditAtribute;
        //        string loweredItem;
        //        string item;
        //        string aux2;
        //        string aux3;
        //        int idStartIndex;
        //      bool  isReindex = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, res.DocTypeId);

        //        foreach (IIndex indice in indices)
        //        {
        //            try
        //            {
        //                //Busca la existencia del índice en el formulario
        //                strIndexId = "\"ZAMBA_INDEX_" + indice.ID + "\"";
        //                if (!ContainsCaseInsensitive(strHtml, strIndexId))
        //                {
        //                    strIndexId = "'ZAMBA_INDEX_" + indice.ID + "'";
        //                    if (!ContainsCaseInsensitive(strHtml, strIndexId))
        //                    {
        //                        strIndexId = string.Empty;
        //                    }
        //                }

        //                //Verifica si encontró el índice
        //                if (!string.IsNullOrEmpty(strIndexId))
        //                {
        //                    if (this.CurrentZFrom.ContainsKey(indice.ID))
        //                        this.CurrentZFrom[indice.ID] = indice.Data;
        //                    else
        //                        this.CurrentZFrom.Add(indice.ID, indice.Data);

        //                    idStartIndex = strHtml.LastIndexOf("id=" + strIndexId, StringComparison.InvariantCultureIgnoreCase) + (strIndexId).Length + 3;
        //                    item = strHtml.Substring(0, idStartIndex);
        //                    Int32 lastindex = item.LastIndexOf("<");

        //                    if (lastindex == -1)
        //                        throw new Exception(string.Format("El atributo {0} esta mal formado.", strHtml));

        //                    item = item.Substring(lastindex);
        //                    aux2 = strHtml.Substring(idStartIndex);
        //                    aux2 = aux2.Substring(0, aux2.IndexOf(">"));
        //                    item = item + aux2 + ">";
        //                    loweredItem = item.ToLower();

        //                    input = loweredItem.Contains("input") || loweredItem.Contains("textarea");
        //                    aux3 = string.Empty;

        //                    if (!(UseIndexsRights))
        //                    {
        //                        specificEditAtribute = UB.GetIndexRightValue(res.DocType.ID, indice.ID, user.ID, RightsType.IndexEdit);
        //                        //specificAtributesRights = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.DocTypes, RightsType.ViewRightsByIndex,res.DocTypeId, this.user.ID);
        //                        specificAtributesRights = RiB.GetSpecificAttributeRight(this.user, res.DocTypeId);

        //                        if (this.IsShowing && (!isReindex || (specificAtributesRights && !specificEditAtribute)))
        //                        {
        //                            if (!input)
        //                            {
        //                                if (!loweredItem.Contains("disabled"))
        //                                {
        //                                    aux3 = item.Substring(0, item.IndexOf(">")) + " disabled=\"disabled\" >";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (!loweredItem.Contains("readonly"))
        //                                {
        //                                    aux3 = item.Substring(0, item.IndexOf(">")) + " readOnly=\"readonly\" class=\"ReadOnly\" >";
        //                                }
        //                                else
        //                                {
        //                                    aux3 = item.Substring(0, item.IndexOf(">")) + " class=\"ReadOnly\" >";
        //                                }
        //                            }

        //                            //agrego los indices de tipo fecha a un arraylist
        //                            if (indice.Type == IndexDataType.Fecha)
        //                            {
        //                                dateIndex.Add("zamba_index_" + indice.ID.ToString() + "\"");
        //                            }
        //                        }
        //                    }

        //                    if (String.Compare(aux3, String.Empty) == 0)
        //                        aux2 = SForms.AsignValue(indice, item, res);
        //                    else
        //                        //si no tengo el permiso entonces deshabilito el elemento
        //                        aux2 = SForms.AsignValue(indice, aux3, res);

        //                    aux2 = SetIndexAttributes(indice, aux2);
        //                    if (!string.IsNullOrEmpty(aux2))
        //                        strHtml = strHtml.Replace(item, aux2);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ZClass.raiseerror(ex);
        //            }
        //        }

        //        if (dateIndex.Count > 0)
        //        {
        //            //deshabilito el datePicker de cada indice de tipo fecha
        //            var i = 0;
        //            String script = "$(document).ready(function(){";
        //            for (i = 0; i < dateIndex.Count; i++)
        //            {
        //                script = script + "if ($(\"#" + dateIndex[i] + ").length > 0) $(\"#" + dateIndex[i] + ").datepicker(\"disable\");";

        //            }
        //            script = script + "});";
        //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "", script, true);
        //        }
        //    }

        //    if (ContainsCaseInsensitive(strHtml, "ZAMBA_DOC_ID"))
        //    {
        //        String item = strHtml.Substring(0, strHtml.LastIndexOf("ZAMBA_DOC_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_ID").Length);
        //        item = item.Substring(item.LastIndexOf("<"));
        //        String aux2 = strHtml.Substring(strHtml.LastIndexOf("ZAMBA_DOC_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_ID").Length);
        //        aux2 = aux2.Substring(0, aux2.IndexOf(">"));

        //        item = item + aux2 + ">";
        //        Index indaux = new Index();
        //        indaux.Data = res.ID.ToString();
        //        indaux.DataTemp = res.ID.ToString();
        //        aux2 = SForms.AsignValue(indaux, item, res);

        //        strHtml = strHtml.Replace(item, aux2);
        //    }

        //    if (ContainsCaseInsensitive(strHtml, "ZAMBA_DOC_T_ID"))
        //    {
        //        String item = strHtml.Substring(0, strHtml.LastIndexOf("ZAMBA_DOC_T_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_T_ID").Length);
        //        item = item.Substring(item.LastIndexOf("<"));
        //        String aux2 = strHtml.Substring(strHtml.LastIndexOf("ZAMBA_DOC_T_ID", StringComparison.InvariantCultureIgnoreCase) + ("ZAMBA_DOC_T_ID").Length);
        //        aux2 = aux2.Substring(0, aux2.IndexOf(">"));

        //        item = item + aux2 + ">";
        //        Index indaux = new Index();
        //        indaux.Data = res.DocTypeId.ToString();
        //        indaux.DataTemp = res.DocTypeId.ToString();
        //        aux2 = SForms.AsignValue(indaux, item, res);

        //        strHtml = strHtml.Replace(item, aux2);
        //    }


        //    return strHtml;
        //}
    }
}
