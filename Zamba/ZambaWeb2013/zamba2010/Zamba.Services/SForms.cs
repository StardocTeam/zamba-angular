using System;
using Zamba.Core;
using System.Collections;
using System.Text.RegularExpressions;
using Zamba.Core.HelperForm;
using System.Collections.Generic;
using System.Data;

namespace Zamba.Services
{
    public class SForms : IService
    {
        private FormBusiness FB = new FormBusiness();

        public SForms()
        {
        }
         

        #region IService Members
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Forms;
        }
        #endregion

        public IZwebForm[] GetShowAndEditForms(Int64 DocTypeId)
        {
           return FB.GetShowAndEditForms(Int32.Parse(DocTypeId.ToString()));
        }

        public IZwebForm[] GetAllForms(Int32 DocTypeId)
        {
            return FB.GetAllForms(DocTypeId);
        }

        public IZwebForm GetForm(Int64 formId)
        {
            return FB.GetForm(formId);
        }

        public string GetInnerHTML(string CompleteHtml)
        {
           return FB.GetInnerHTML(CompleteHtml);
        }

        public String AsignValue(IIndex I,String E, IResult result)
        {
           return FB.AsignValue(I, E, result);
        }

        public String AsignVarValue(string varValue, String E, IResult result)
        {
            return FB.AsignVarValue(varValue, E, result);
        }
        public ArrayList GetVirtualDocumentsByRightsOfCreate(FormTypes type)
        {
           return FB.GetVirtualDocumentsByRightsOfCreate(type, Membership.MembershipHelper.CurrentUser.ID);
        }

        public Boolean buscarHtmlIframe(Match item)
        {
            return HelperFormVirtual.buscarHtmlIframe(item);
        }

        public Boolean buscarHtmlForm(Match item)
        {
            return HelperFormVirtual.buscarHtmlForm(item);
        }

        public INewResult CreateVirtualDocument(Int64 DocTypeId)
        {
            return new FormBusiness().CreateVirtualDocument(DocTypeId);
        }

        public MatchCollection ParseHtml(String path, String item)
        {
            return HelperFormVirtual.ParseHtml(path, item);
        }

        public Int64 buscarTagZamba(Match item)
        {
            return HelperFormVirtual.buscarTagZamba(item);
        }

        public void replazarAtributoAction(ref String tag, String action)
        {
            HelperFormVirtual.replazarAtributoAction(ref tag, action);
        }

        public void replazarAtributoSrc(ref String tag, String path)
        {
            HelperFormVirtual.replazarAtributoSrc(ref tag, path);
        }

        public IDtoTag instanceDtoTag(String oldTag, String newTag)
        {
            return HelperFormVirtual.instanceDtoTag(oldTag, newTag);
        }

        public void replazarAtributoId(ref String tag, Int64 id)
        {
            HelperFormVirtual.replazarAtributoId(ref tag, id);
        }

        public string actualizarHtml(List<IDtoTag> tags, String html)
        {
            return HelperFormVirtual.actualizarHtml(tags, html);
        }

        public String RemoveSrcTag(String tag)
        {
            return HelperFormVirtual.RemoveSrcTag(tag);
        }

        public bool EvaluateDynamicFormConditions(ref Result myResult, DataSet ds)
        {
            return FB.EvaluateDynamicFormConditions(ref myResult, ds);
        }


        /// <summary>
        /// Obtiene una lista de condiciones de un formulario
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
        public List<IZFormCondition> GetFormConditions(long formID)
        {
            return FB.GetFormConditions(formID);
        }

        /// <summary>
        /// Obtiene una condicion por su id
        /// </summary>
        /// <param name="conditionID"></param>
        /// <returns></returns>
        public IZFormCondition GetCondition(long conditionID)
        {
            return FB.GetCondition(conditionID);
        }

        /// <summary>
        /// Verifica si la entidad contiene formularios
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        public bool HasForms(Int64 docTypeId)
        {
            return FB.HasForms(docTypeId);
        }
    }
}
