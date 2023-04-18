namespace Zamba.FormBrowser
{
    using System;
    using System.Collections.Generic;
    using Zamba.Core;
    using Zamba.Membership;
    using System.IO;
    using Zamba.FormBrowser.Helpers;
    using System.Text.RegularExpressions;
    using System.Web;
    using Zamba.Core.HelperForm;
    using System.Data;
    using System.Text;
    using Zamba.Core.WF.WF;
    using System.Collections;
    using Zamba.Core.DocTypes.DocAsociated;
    using System.Linq;
    using System.Diagnostics;
    using Zamba.FormBrowser.Razor;
    using Zamba.Tools;
    using System.Web;
    using System.Runtime.Remoting.Contexts;
    using System.Configuration;

    public class FormBrowserController
    {
        private const string STANDARD_ERROR_STRING = @"<p>Error al cargar el formulario, pongase en contacto con el administrador del sistema</p>";

        private FormBrowserPreferences _pref;
        private IResult _res;
        private static FormBusiness _formService;
        private string _additionalScripts;

        public IZwebForm CurrentForm { get; set; }
        public IUser CurrentUser { get; set; }
        private string CurrentFormString { get; set; }

        public string DocUrl
        {
            get
            {
                ZOptBusiness zoptb = new ZOptBusiness();
                String CurrentTheme = zoptb.GetValue("CurrentTheme");
                zoptb = null;

                if (HttpContext.Current == null)
                {
                    ZTrace.WriteLineIf(TraceLevel.Error, "[ERROR]: HttpContext.Current no existe. ");
                    return Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "/api/b2b/GetDocFileGETMethod?DocTypeId={0}&DocId={1}&UserID={2}";
                }

                HttpRequest req = HttpContext.Current.Request;

                return GetDocFileUrl;
            }
        }

        public IResult CurrentResult
        {
            get { return _res; }
            set { _res = value; }
        }
        public FormBrowserPreferences AsocPreferences
        {
            get { return _pref; }
            set { _pref = value; }
        }

        private IIndex GetAttributeInResult(long indexId)
        {
            return CurrentResult.Indexs.OfType<IIndex>().Where(ind => ind.ID == indexId).SingleOrDefault();
        }

        #region Business_Seams
        //En esta region se hacen todas las llamadas a clases externas(como business)
        //Esto se usa para poder desacoplar el controller y que en los test no haya tanta dependencia de una db

        protected string GetHTMLString(IZwebForm form)
        {
            ZOptBusiness zoptb = new ZOptBusiness();
            String CurrentTheme = zoptb.GetValue("CurrentTheme");
            zoptb = null;
            string rutaTemp = MembershipHelper.AppFormPath(CurrentTheme) + form.Path.Substring(form.Path.LastIndexOf("\\"));
            FormBusiness FB = new FormBusiness();
            FB.CopyWebForm(form, rutaTemp);
            FB = null;
            //Leo el archivo
            StreamReader str = new StreamReader(rutaTemp);

            string strHtml = str.ReadToEnd();

            str.Close();
            str.Dispose();
            return strHtml;
        }

        protected string getHtmlSection(string formString, HTMLSection hTMLSection)
        {
            return HTML.getHtmlSection(formString, hTMLSection);
        }

        protected string actualizarHtml(List<IDtoTag> listaTags, string htmlForm)
        {
            return HelperFormVirtual.actualizarHtml(listaTags, htmlForm);
        }

        protected IDtoTag instanceDtoTag(string p, string tag)
        {
            return HelperFormVirtual.instanceDtoTag(p, tag);
        }

        protected void replazarAtributoSrc(ref string tag, string path)
        {
            HelperFormVirtual.replazarAtributoSrc(ref tag, path);
        }

        protected long buscarTagZamba(Match item)
        {
            return HelperFormVirtual.buscarTagZamba(item);
        }

        protected bool buscarHtmlIframe(Match item)
        {
            return HelperFormVirtual.buscarHtmlIframe(item);
        }

        protected MatchCollection ParseHtml(string formLower, string p)
        {
            return HelperFormVirtual.ParseHtml(formLower, p);
        }

        protected bool GetSpecificAttributeRight(IUser CurrentUser, long docTypeId, int userId)
        {
            return new RightsBusiness().GetSpecificAttributeRight(CurrentUser, docTypeId);
        }



        protected string AsignValue(IIndex index, string loweredItem, IResult CurrentResult, object optionSource)
        {
            return new FormBusiness().AsignValue(index, loweredItem, CurrentResult);
        }



        protected string SetTagDecorators(IIndex index, string tag, ITaskResult taskResult)
        {
            return AtributesHelper.SetTagAttributes(index, tag, taskResult);
        }

        public virtual Hashtable GetAsociatedIndexsRights(IResult res)
        {
            UserBusiness UB = new UserBusiness();
            Hashtable IRI = UB.GetAssociatedIndexsRightsCombined(this.CurrentResult.DocTypeId, res.DocTypeId, CurrentUser.ID);
            UB = null;
            return IRI;
        }

        protected bool GetUserRights(Int64 UserId, ObjectTypes objectTypes, RightsType rightsType, long p)
        {
            return new RightsBusiness().GetUserRights(UserId, objectTypes, rightsType, p);
        }


        #endregion

        #region Constructores
        public FormBrowserController(IResult res, long formId, IUser user)
            : this(res, new FormBusiness().GetForm(formId), user)
        {
        }

        public FormBrowserController()
        {
            if (_formService == null)
                _formService = new FormBusiness();
            if (_pref == null)
                _pref = new FormBrowserPreferences();
        }

        public FormBrowserController(IResult res, IZwebForm form, IUser user)
            : this()
        {
            this._res = res;
            this.CurrentForm = form;
            this.CurrentUser = user;
        }
        #endregion

        #region Render
        /// <summary>
        /// Obtiene el string para renderar, ya pasado por vista razor
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string GetBaseHTMLString(IZwebForm form)
        {
            string formString;
            ZRazorEngine zrEng;
            //Analizar la liberacion de dll para que el rebuild puedea funcionar en todo momento
            if (ZRazorEngine.IsFormGenerated(form) && !form.Rebuild)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se regenerara el formulario");
                zrEng = new ZRazorEngine(form);
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "NO se regenera el formulario");
                formString = GetHTMLString(CurrentForm);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Html del Formulario:" + formString);
                zrEng = new ZRazorEngine(form, formString);
            }
            formString = zrEng.Execute(VariablesInterReglas.clone());
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Html del Formulario Procesado:" + formString);
            return formString;
        }

        public string RenderForm(HTMLSection htmlSection)
        {
            try
            {
                //Obtengo el html, si no fue compilado o tiene la marca de compilar
                //genera la clase(para usar o no razor) y luego devuelve el html
                string formString = GetBaseHTMLString(CurrentForm);
                CurrentFormString = formString;

                if (htmlSection != HTMLSection.ALL)
                {
                    formString = getHtmlSection(formString, htmlSection);
                }

                formString = HTML.ClearWhiteSpaces(formString);
                formString = ResolveIframes(formString);
                formString = ResolveAllIndexs(formString);
                //formString = ResolveAllAsociatesList(formString, null);
                formString = ResolveAllAsociateAttributes(formString, null, Membership.MembershipHelper.CurrentUser.ID);
                formString = ResolveAllZVarTable(formString);
                formString = ResolveAllDocIdField(formString);
                formString = ResolveAllDocTypeIdField(formString);

                if (htmlSection != HTMLSection.ALL && formString.IndexOf("<div id=\"dynamic_filter\">", StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    formString = "<div id=\"dynamic_filter\"></div>" + formString;
                }

                return formString;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return STANDARD_ERROR_STRING;
            }
        }

        public string GetFormHead()
        {
            return getHtmlSection(CurrentFormString, HTMLSection.HEAD);
        }

        /// <summary>
        /// Esta funcion debe ser llamada luego de haber llamado a RenderForm
        /// </summary>
        /// <returns></returns>
        public string GetAdditionalScripts()
        {
            return _additionalScripts;
        }

        public string ResolveIframes(string htmlForm)
        {
            string strToReturn = htmlForm;
            string formLower = htmlForm.ToLower();
            if (formLower.Contains("iframe"))
            {
                List<IDtoTag> listaTags = new List<IDtoTag>();

                //Si tiene un iframe, busco el documento asociado
                MatchCollection matches = null;
                matches = ParseHtml(formLower, "iframe");

                //Entrar por aca si el html tiene la palabra iframe
                if (matches != null)
                {
                    Int64 id;
                    //bool useOriginal;
                    string path;
                    IDtoTag dto;

                    foreach (Match item in matches)
                    {
                        if (buscarHtmlIframe(item))
                        {
                            id = buscarTagZamba(item);
                            //useOriginal = false;
                            if (id <= 0)
                            {
                                path = string.Format(DocUrl, CurrentResult.DocTypeId, CurrentResult.ID, CurrentUser.ID);

                                string tag = item.Value;
                                replazarAtributoSrc(ref tag, path);
                                dto = instanceDtoTag(item.Value, tag);
                                listaTags.Add(dto);
                                //useOriginal = true;
                            }
                        }
                        strToReturn = actualizarHtml(listaTags, htmlForm);
                    }
                }
            }
            return strToReturn;
        }

        #region Indexs

        public string ResolveAllIndexs(string html)
        {
            StringBuilder sb = new StringBuilder(html);

            string indexTagId;
            IIndex ind;
            IEnumerable<string> fields = HTML.GetHTMLItem(html, "input", "(id=\"zamba_index_(.*?)\"|name=\"zamba_index_(.*?)\")").Union(
                HTML.GetHTMLItem(html, "textarea", "(id=\"zamba_index_(.*?)\"|name=\"zamba_index_(.*?)\")"));

            string[] splittedValues;
            foreach (string item in fields)
            {
                indexTagId = GetIndexIdentifier(item, out splittedValues);

                if (!string.IsNullOrEmpty(indexTagId))
                {
                    ind = GetAttributeInResult(long.Parse(splittedValues[2]));
                    if (ind == null)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Indice:" + indexTagId + " no encontrado, para el elemento: " + item);
                    else
                        sb.Replace(item, ResolveIndexInput(item, ind, true));
                }
            }

            fields = HTML.GetHTMLItem(html, "select", "(id=\"zamba_index_(.*?)\"|name=\"zamba_index_(.*?)\")");
            foreach (string item in fields)
            {
                indexTagId = GetIndexIdentifier(item, out splittedValues);

                if (!string.IsNullOrEmpty(indexTagId))
                {
                    ind = GetAttributeInResult(long.Parse(splittedValues[2]));
                    if (ind == null)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Indice:" + indexTagId + " no encontrado, para el elemento: " + item);
                    else
                        sb.Replace(item, ResolveIndexDropDown(item, ind, true));
                }
            }

            return sb.ToString();
        }

        private static string GetIndexIdentifier(string item, out string[] splittedValues)
        {
            string indexTagId = HTML.GetAttributeValue(item, "id");
            splittedValues = indexTagId.Split('_');

            if (string.IsNullOrEmpty(indexTagId) || splittedValues.Length < 3)
            {
                indexTagId = HTML.GetAttributeValue(item, "name");
                splittedValues = indexTagId.Split('_');

                if (splittedValues.Length < 3)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Elemento: " + item + " mal formado");
                    return string.Empty;
                }
                return indexTagId;
            }
            else
            {
                return indexTagId;
            }
        }

        #region DropDownIndexs
        public string ResolveIndexDropDown(string item, IIndex index, bool useIndexRights)
        {
            UserBusiness UB = new UserBusiness();
            bool specificEditAtribute, specificAtributesRights;
            if (useIndexRights)
            {
                specificEditAtribute = UB.GetIndexRightValue(CurrentResult.DocTypeId, index.ID, CurrentUser.ID, RightsType.IndexEdit);
                specificAtributesRights = GetSpecificAttributeRight(CurrentUser, CurrentResult.DocTypeId, 0);
            }
            else
            {
                specificEditAtribute = false;
                specificAtributesRights = false;
            }
            UB = null;
            return ResolveIndexDropDown(item, index, useIndexRights, specificAtributesRights, specificEditAtribute, null);
        }

        public string ResolveIndexDropDown(string item, IIndex index, bool useIndexRights, bool specificAtributesRights, bool specificEditAtribute, object ddSource)
        {
            item = HTML.GetStarTag(item);
            string loweredItem = item.ToLower();
            bool isInsert = CurrentForm.Type == FormTypes.Insert || CurrentForm.Type == FormTypes.WebInsert;

            if (!loweredItem.StartsWith("<select"))
                throw new Exception("El elemento:'" + item + "' no inicia correctamente.");

            //TODO: usar regurlar expression para obtener y agregar atributos
            if (!isInsert && (!CurrentResult.DocType.IsReindex || (specificAtributesRights && !specificEditAtribute)))
            {
                if (!loweredItem.Contains("disabled"))
                {
                    item = item.Substring(0, loweredItem.IndexOf(">")) + " disabled=\"disabled\">";
                }
            }

            item = AsignValue(index, item, CurrentResult, ddSource);

            item = SetTagDecorators(index, item, CurrentResult as ITaskResult);
            item += "</select>";

            return item;
        }

        #endregion

        #region InputIndexs

        public string ResolveIndexInput(string item, IIndex index, bool useIndexRights)
        {
            UserBusiness UB = new UserBusiness();
            bool specificEditAtribute, specificAtributesRights;
            if (useIndexRights)
            {
                specificEditAtribute = UB.GetIndexRightValue(CurrentResult.DocTypeId, index.ID, CurrentUser.ID, RightsType.IndexEdit);
                specificAtributesRights = GetSpecificAttributeRight(CurrentUser, CurrentResult.DocTypeId, 0);
            }
            else
            {
                specificEditAtribute = false;
                specificAtributesRights = false;
            }
            UB = null;
            return ResolveIndexInput(item, index, useIndexRights, specificAtributesRights, specificEditAtribute);
        }

        public string ResolveIndexInput(string item, IIndex index, bool useIndexRights, bool specificAtributesRights, bool specificEditAtribute)
        {
            string loweredItem = item.ToLower();
            bool isInsert = CurrentForm.Type == FormTypes.Insert || CurrentForm.Type == FormTypes.WebInsert;

            if (!(loweredItem.StartsWith("<input") || loweredItem.StartsWith("<textarea")))
                throw new Exception("El elemento:'" + item + "' no inicia correctamente.");

            //TODO: usar regurlar expression para obtener y agregar atributos
            if (!isInsert && (!CurrentResult.DocType.IsReindex || (specificAtributesRights && !specificEditAtribute)))
            {
                if (!loweredItem.Contains("readonly"))
                {
                    item = item.Substring(0, item.IndexOf("/>")) + " readonly=\"readonly\" class=\"ReadOnly\"/>";
                }
                else
                {
                    item = item.Substring(0, item.IndexOf("/>")) + " class=\"ReadOnly\"/>";
                }

                _additionalScripts = _additionalScripts + "$(\"#zamba_index_" + index.ID + "\").datepicker(\"disable\");";
            }

            if (loweredItem.StartsWith("<textarea"))
                item = HTML.GetStarTag(item);

            item = AsignValue(index, item, CurrentResult, null);

            if (loweredItem.StartsWith("<textarea"))
                item += "</textarea>";

            item = SetTagDecorators(index, item, CurrentResult as ITaskResult);

            return item;
        }
        #endregion

        #endregion

        #region AsocTables

        //public string ResolveAllAsociatesList(string html, List<IResult> asocDocs)
        //{
        //    List<string> fields = HTML.GetHTMLItem(html, "table", "id=\"zamba_associated_documents_(.*?)\"");
        //    StringBuilder sb = new StringBuilder(html);
        //    foreach (string item in fields)
        //    {
        //        if (asocDocs == null)
        //            sb.Replace(item, ResolveAsociateDocumentTable(item));
        //        else
        //            sb.Replace(item, ResolveAsociateDocumentTable(item, HTML.GetAttributeValue(item, "id"), asocDocs));
        //    }

        //    return sb.ToString();
        //}

        //public string ResolveAsociateDocumentTable(string item)
        //{
        //    string tableId = HTML.GetAttributeValue(item, "id");
        //    string[] splitedValues = tableId.Split('_');
        //    long asocDocTypeId = -1;

        //    if (splitedValues.Length != 4 || !long.TryParse(splitedValues[3], out asocDocTypeId))
        //    {
        //        throw new Exception("El id de la tabla no esta correctemente construido, item:" + item);
        //    }

        //    DataTable asociatedResults = null;
        //    asociatedResults = GetAsociatedResultsFromResultAsList(asocDocTypeId, CurrentResult, 9999, Membership.MembershipHelper.CurrentUser.ID);

        //    return ResolveAsociateDocumentTable(item, tableId, asociatedResults);
        //}

        //public string ResolveAsociateDocumentTable(string item, string tableId, DataTable asocResults)
        //{
        //    string tagClass = HTML.GetAttributeValue(item, "class");
        //    StringBuilder itemToReturn = new StringBuilder();
        //    bool onlyWF = false;

        //    asocResults.Sort(new Comparison<IResult>((doc, doc2) => (int)(doc2.ID - doc.ID)));

        //    switch (tagClass)
        //    {
        //        case "tablesorter":
        //            DataTable dt = ParseAsocResults(asocResults, onlyWF, tableId);
        //            itemToReturn = ConvertToHTMLTable(item, dt, FormTableTypes.Asociates);
        //            break;
        //        case "gallery":
        //            string ulID = tableId;
        //            string liTemplate = "<li><img data-frame=\"" + DocUrl + "\" src=\"" + DocUrl + "\" title=\"{3}\" description=\"{4}\"/>";
        //            string ulTemplate = "<ul id=\"{0}\">{1}</ul>";
        //            string scriptTemplate = "$('#{0}').galleryView({1});";
        //            StringBuilder sbList = new StringBuilder();
        //            int count = asocResults.Count;

        //            string[] temp = tableId.Split('_');
        //            int indexToDescriptionID = int.Parse(temp[4]);
        //            string strDescription;
        //            int indexCount;

        //            for (int i = 0; i < count; i++)
        //            {
        //                indexCount = asocResults[i].Indexs.Count;
        //                strDescription = string.Empty;
        //                for (int j = 0; j < indexCount; j++)
        //                {
        //                    if (((IIndex)asocResults[i].Indexs[j]).ID == indexToDescriptionID)
        //                    {
        //                        strDescription = ((IIndex)asocResults[i].Indexs[j]).Data;
        //                    }
        //                }
        //                sbList.AppendFormat(liTemplate, asocResults[i].DocTypeId,
        //                    asocResults[i].ID, CurrentUser.ID, asocResults[i].Name, strDescription);
        //            }

        //            itemToReturn.AppendFormat(string.Format(ulTemplate, tableId, sbList.ToString()));
        //            _additionalScripts += "$(docuemnt).ready(function(){" +
        //                string.Format(scriptTemplate, ulID, "{panel_width: 300,panel_height: 200,frame_width: 55,show_overlays: true}") +
        //                "});";
        //            itemToReturn.Append(HTML.TABLE_ENDTAG);
        //            break;
        //    }

        //    return itemToReturn.ToString();
        //}

        private string LoadTableBody(string itemToReturn, DataTable dt, bool useExecButton, string idDocColumnName,
            string docTypeIdColumnName, long docTypeId)
        {
            StringBuilder htmlRows = new StringBuilder();
            int columnCount = dt.Columns.Count;
            DataColumnCollection columns = dt.Columns;
            DataRowCollection rows = dt.Rows;
            StringBuilder Link;
            foreach (DataRow dr in rows)
            {
                if (useExecButton)
                {
                    ZOptBusiness zoptb = new ZOptBusiness();
                    String CurrentTheme = zoptb.GetValue("CurrentTheme");
                    zoptb = null;
                    Link = new StringBuilder();
                    Link.Append("<tr class=\"FormRowStyle\"><td style=\"text-decoration:none\" width=\"20\"><a href=\"");

                    if (string.IsNullOrEmpty(docTypeIdColumnName))
                        Link.AppendFormat(HTML.GetTaskSelectorURL(CurrentTheme), dr[idDocColumnName], docTypeId);
                    else
                        Link.AppendFormat(HTML.GetTaskSelectorURL(CurrentTheme), dr[idDocColumnName], dr[docTypeIdColumnName]);

                    Link.Append("\" style=\"text-decoration:none\" ><img height=\"20\" src=\"");
                    Link.Append(HTML.GetExecuteImgURL(CurrentTheme));
                    Link.Append("\" border=\"0\"/></a></td>");
                    htmlRows.Append(Link.ToString());
                }
                else
                    htmlRows.Append("<tr class=\"FormRowStyle\">");

                for (int i = 0; i < columnCount; i++)
                {
                    if (!(string.Compare(columns[i].ColumnName, idDocColumnName, true) == 0 || string.Compare(columns[i].ColumnName, docTypeIdColumnName, true) == 0))
                    {
                        htmlRows.Append("<td>");
                        htmlRows.Append(dr[columns[i].ColumnName]);
                        htmlRows.Append("</td>");
                    }
                }

                htmlRows.Append("</tr>");
            }
            return htmlRows.ToString();
        }

        private string LoadTableHeader(string itemToReturn, DataColumnCollection dcs, bool useExecuteColumn,
            string idDocColumnName, string docTypeIdColumnName)
        {
            //se agrega una columna para el link de abrir la tarea
            StringBuilder HeaderRow = new StringBuilder();

            if (useExecuteColumn)
                HeaderRow.Append("<tr><th width=\"20\"></th>");
            else
                HeaderRow.Append("<tr>");

            //Agrego columnas de atributos
            foreach (DataColumn Column in dcs)
            {
                if (!(string.Compare(Column.ColumnName, idDocColumnName, true) == 0 || string.Compare(Column.ColumnName, docTypeIdColumnName, true) == 0))
                    HeaderRow.AppendFormat(HTML.TABLE_HEADER_FORMAT, Column.ColumnName);
                else
                {  //HeaderRow.AppendFormat(HTML.TABLE_HEADER_INVISIBLE_FORMAT, Column.ColumnName);
                }
            }

            HeaderRow.Append("</tr>");

            return HeaderRow.ToString();
        }

        private DataTable ParseAsocResults(List<IResult> asocResults, bool onlyWF, string tableId)
        {
            //En caso de que el contendido sea vacio retornamos una tabla vacia
            if (asocResults.Count == 0)
                return new DataTable();

            DataTable dt = new DataTable();
            SetZvarColumns(tableId, dt);

            SetPreferencesColumns(dt);
            //Se toma para cargar las columnas un result
            SetIndexColumns(dt, asocResults[0]);

            DataRow dr;
            foreach (IResult currResult in asocResults)
            {
                dr = ConvertResultToRow(dt, currResult, tableId);
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private static DataRow ConvertResultToRow(DataTable dt, IResult currResult, string tableId)
        {
            DataRow dr;
            dr = dt.NewRow();
            dr["IdDoc"] = currResult.ID;
            dr["DoctypeId"] = currResult.DocType.ID;

            if (dt.Columns.Contains("Nombre"))
                dr["Nombre"] = currResult.Name;
            if (dt.Columns.Contains("Ruta Documento"))
                dr["Ruta Documento"] = currResult.RealFullPath();
            if (dt.Columns.Contains("Creado"))
                dr["Creado"] = currResult.CreateDate;
            if (dt.Columns.Contains("Entidad"))
                dr["Entidad"] = currResult.Parent.Name;
            if (dt.Columns.Contains("Modificado"))
                dr["Modificado"] = currResult.EditDate;
            if (dt.Columns.Contains("Numero de Version"))
                dr["Numero de Version"] = currResult.VersionNumber;
            if (dt.Columns.Contains("ParentId"))
                dr["ParentId"] = currResult.ParentVerId;


            foreach (IIndex currIndex in currResult.Indexs)
            {
                //Si Data tiene un valor que se le asigne al Item
                if (!string.IsNullOrEmpty(currIndex.Data) && dt.Columns.Contains(currIndex.Name))
                {
                    if (currIndex.DropDown == IndexAdditionalType.LineText)
                        if (currIndex.Type == IndexDataType.Si_No)
                            if (int.Parse(currIndex.Data) == 0)
                                dr[currIndex.Name] = "No";
                            else
                                dr[currIndex.Name] = "Si";
                        else
                            dr[currIndex.Name] = currIndex.Data;
                    else
                        if (string.Compare(string.Empty, currIndex.dataDescription) != 0)
                        dr[currIndex.Name] = currIndex.dataDescription;
                    else
                        dr[currIndex.Name] = currIndex.Data;
                }
            }

            ITaskResult task;
            if (currResult is ITaskResult)
                task = (ITaskResult)currResult;
            else
            {
                WFTaskBusiness WTB = new WFTaskBusiness();
                task = WTB.GetTaskByDocIdAndDocTypeId(currResult.ID, currResult.DocTypeId);
                WTB = null;
            }

            if (task != null)
            {
                Boolean IsGroup = false;
                if (dt.Columns.Contains("Estado"))
                    dr["Estado"] = task.State.Name;
                if (dt.Columns.Contains("Usuario Asignado"))
                    dr["Usuario Asignado"] = new UserGroupBusiness().GetUserorGroupNamebyId(task.AsignedToId, ref IsGroup);
            }

            if (dt.Columns.Contains("Original"))
            {
                if (string.IsNullOrEmpty(currResult.OriginalName))
                    dr["Original"] = currResult.Name;
                else
                {
                    FileInfo fi = new FileInfo(currResult.OriginalName);
                    dr["Original"] = fi.Name;
                }
            }

            SetZVarColumValues(tableId, dr);

            return dr;
        }

        private static void SetZVarColumValues(string tableId, DataRow dr)
        {
            StringBuilder InnerHtml = new StringBuilder();
            char mander = '§';
            char pipe = '/';
            char equal = '=';

            if (string.IsNullOrEmpty(tableId) == false)
            {
                string textItem2 = null;
                string textAux = null;

                if (tableId.Split(mander).Length > 1)
                    foreach (string btn in tableId.Split(mander))
                    {
                        InnerHtml.Remove(0, InnerHtml.Length);
                        String[] items = btn.Split(pipe);
                        Int32 itemNum = default(Int32);
                        String[] zvarItems = null;
                        string @params = null;

                        InnerHtml.Append("&nbsp;<INPUT id=");
                        InnerHtml.Append(Convert.ToChar(34));

                        //Si tiene zvar
                        if (items.Length > 2)
                        {
                            textItem2 = items[2].ToString();
                            InnerHtml.Append(items[0] + "_");

                            while (string.IsNullOrEmpty(textItem2) == false)
                            {
                                textAux = textItem2.Remove(0, 5);
                                zvarItems = textAux.Remove(textAux.IndexOf(")")).Split(equal);
                                textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1);

                                if (Int32.TryParse(zvarItems[1].ToString(), out itemNum) == false)
                                {
                                    if (zvarItems[1].ToString().ToLower().Contains("length"))
                                    {
                                        itemNum = dr.Table.Columns.Count - Int32.Parse(zvarItems[1].ToString().Split(Char.Parse("-"))[1]);
                                    }
                                }
                                InnerHtml.Append("zvar(" + zvarItems[0].ToString() + "=" + dr[itemNum].ToString() + ")");

                                @params = @params + "'" + dr.ItemArray[itemNum].ToString() + "',";
                            }
                        }
                        else
                        {
                            InnerHtml.Append(items[0]);
                        }

                        InnerHtml.Append(Convert.ToChar(34));
                        InnerHtml.Append(" type=button onclick=");

                        //Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                        //sino se llama a SetRuleId por default
                        if (items.Length > 3)
                        {
                            InnerHtml.Append(Convert.ToChar(34));
                            InnerHtml.Append(items[3] + "(this, ");
                            InnerHtml.Append(@params.Substring(0, @params.Length - 1).Replace("\\", "\\\\"));
                            InnerHtml.Append(");");
                            InnerHtml.Append(Convert.ToChar(34));
                        }
                        else
                        {
                            InnerHtml.Append(Convert.ToChar(34));
                            InnerHtml.Append("SetRuleId(this);");
                            InnerHtml.Append(Convert.ToChar(34));
                        }

                        InnerHtml.Append(" value = ");
                        InnerHtml.Append(Convert.ToChar(34));
                        InnerHtml.Append(items[1]);
                        InnerHtml.Append(Convert.ToChar(34));
                        InnerHtml.Append(" Name = ");
                        InnerHtml.Append(Convert.ToChar(34));
                        InnerHtml.Append(items[0]);
                        InnerHtml.Append(Convert.ToChar(34));
                        InnerHtml.Append(" >");

                        dr[items[1]] = InnerHtml.ToString();
                    }
            }
        }

        private static void SetZvarColumns(string tableId, DataTable dt)
        {
            char mander = '§';
            char pipe = '/';


            if (!string.IsNullOrEmpty(tableId))
            {
                string[] splittedValues = tableId.Split(mander);
                if (splittedValues.Length <= 1)
                    tableId = string.Empty;
                else
                {
                    string[] items;
                    foreach (string btn in splittedValues)
                    {
                        items = btn.Split(pipe);
                        dt.Columns.Add(items[1]);
                    }
                }
            }
        }

        private void SetPreferencesColumns(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("IdDoc"));
            dt.Columns.Add(new DataColumn("DoctypeId"));


            dt.Columns.Add(new DataColumn("Nombre"));

            dt.Columns.Add(new DataColumn("Estado"));

            dt.Columns.Add(new DataColumn("Usuario Asignado"));

            dt.Columns.Add(new DataColumn("Creado"));

            dt.Columns.Add(new DataColumn("Entidad"));

            dt.Columns.Add(new DataColumn("Modificado"));
            if (_pref.UseOrignalName)
                dt.Columns.Add(new DataColumn("Original"));
            if (_pref.UseVersionNumber)
                dt.Columns.Add(new DataColumn("Numero de Version"));
            if (_pref.UseParentId)
                dt.Columns.Add(new DataColumn("ParentId"));

            if (_pref.UseDocPath)
                dt.Columns.Add(new DataColumn("Ruta Documento"));
        }

        private void SetIndexColumns(DataTable dt, IResult res)
        {
            Hashtable IRI = GetAsociatedIndexsRights(res);
            bool viewAssociateRightsByIndex = GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, this.CurrentResult.DocTypeId);
            string colName;
            AssociatedIndexsRightsInfo IR;
            foreach (IIndex currIndex in res.Indexs)
            {
                bool ShowIndex = false;
                if (viewAssociateRightsByIndex)
                {
                    IR = (AssociatedIndexsRightsInfo)IRI[currIndex.ID];

                    //26/10/2011: Se cambia el tipo de permiso a buscar.
                    if (IR != null && IR.GetIndexRightValue(RightsType.AssociateIndexView))
                        ShowIndex = true;
                }
                else
                {
                    ShowIndex = true;
                }

                if (ShowIndex)
                {
                    colName = currIndex.Name.Trim();
                    if (!dt.Columns.Contains(colName))
                    {
                        dt.Columns.Add(colName, typeof(string));
                    }
                }
            }
        }
        #endregion

        #region AsocAttributes

        public string ResolveAsocAttribute(string item, List<IResult> asociatedResults, Int64 UserId)
        {
            string loweredItem = item.ToLower();
            string itemId = HTML.GetAttributeValue(loweredItem, "id");
            string[] splitedValues = itemId.Split('_');
            long asocDocTypeId = -1;
            long indexId = -1;

            if (splitedValues.Length != 5 || !long.TryParse(splitedValues[2], out asocDocTypeId)
                || !long.TryParse(splitedValues[4], out indexId))
                throw new Exception("El id del atributo no esta correctemente construido, item:" + item);

            if (asociatedResults == null)
            {
                asociatedResults = new List<IResult>();
                DataTable dt = DocAsociatedBusiness.getAsociatedResultsFromResultAsList(asocDocTypeId, CurrentResult, 1, UserId, false);

                if (dt != null)
                {
                    Results_Business Rb = new Results_Business();
                    DocTypesBusiness DTB = new DocTypesBusiness();
                    Int64 doctypeid = Int64.Parse(dt.Rows[0]["doc_type_id"].ToString());
                    IResult r = new Result(Int64.Parse(dt.Rows[0]["doc_id"].ToString()), DTB.GetDocType(doctypeid), dt.Rows[0]["Name"].ToString(), 0);
                    Rb.CompleteDocument(ref r, dt.Rows[0]);
                    asociatedResults.Add(r);
                    DTB = null;
                    Rb = null;
                }

            }

            if (asociatedResults == null || asociatedResults.Count == 0)
                return string.Empty;

            IResult asocRes = asociatedResults[0];

            return ResolveAsocAttribute(loweredItem, asocRes, indexId);
        }

        public string ResolveAsocAttribute(string loweredItem, IResult asocRes, long indexId)
        {
            IIndex indx = asocRes.Indexs.OfType<IIndex>().Where(ind => ind.ID == indexId).SingleOrDefault();

            if (indx == null)
                throw new Exception("No se ha encontrado el indice con id " + indexId + ", item:" + loweredItem);

            loweredItem = AsignValue(indx, loweredItem, asocRes, null);

            return loweredItem;
        }

        public string ResolveAllAsociateAttributes(string html, List<IResult> asociatedResults, Int64 UserId)
        {
            List<string> fields = HTML.GetHTMLItem(html, "input", "id=\"zamba_asoc_(.*?)_index_(.*?)\"");
            StringBuilder sb = new StringBuilder(html);
            foreach (string item in fields)
            {
                sb.Replace(item, ResolveAsocAttribute(item, asociatedResults, UserId));
            }

            return sb.ToString();
        }

        public string GetDocFileUrl
        {
            get
            {
                HttpRequest req = HttpContext.Current.Request;
                return EnvironmentUtil.curProtocol(req) + @"://" + req.ServerVariables["HTTP_HOST"] + ConfigurationManager.AppSettings["RestApiUrl"] + @"/api/b2b/GetDocFileGETMethod?DocTypeId={0}&DocId={1}&UserID={2}&ConvertToPDF=true";
            }
        }

        #endregion

        #region ZVar

        public string ResolveAllZVarTable(string html)
        {
            List<string> fields = HTML.GetHTMLItem(html, "table", "id=\"zamba_zvar\\(([^\\)]*)\\)\"");
            StringBuilder sb = new StringBuilder(html);
            foreach (string item in fields)
            {
                sb.Replace(item, ResolveZVarTable(item));
            }

            return sb.ToString();
        }

        public string ResolveZVarTable(string item)
        {
            string loweredItem = item.ToLower();
            string tableId = HTML.GetAttributeValue(item, "id");
            if (string.IsNullOrEmpty(tableId))
                throw new Exception("El id de la tabla no esta correctemente construido, item:" + item);

            string zvarName = HTML.GetZambaZvarName(tableId);
            if (string.IsNullOrEmpty(zvarName))
                throw new Exception("El id de la tabla no esta correctemente construido, item:" + item);

            if (!VariablesInterReglas.ContainsKey(zvarName))
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "La variable:" + zvarName + " no se ha encontrado al cargar item: " + item);
                return string.Empty;
            }

            object zvarValue = VariablesInterReglas.get_Item(zvarName);
            if (zvarValue == null)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable: " + zvarName + " vacia al cargar item: " + item);
                return string.Empty;
            }

            DataTable dt = zvarValue as DataTable;
            if (dt == null)
            {
                DataSet ds = zvarValue as DataSet;
                if (ds == null || ds.Tables.Count == 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable: " + zvarName + " de tipo incorrecto al cargar item: " + item);
                    return string.Empty;
                }
                dt = ds.Tables[0];
            }

            StringBuilder itemToReturn = ConvertToHTMLTable(item, dt, FormTableTypes.ZVar);

            return itemToReturn.ToString();
        }

        private StringBuilder ConvertToHTMLTable(string item, DataTable dt, FormTableTypes type)
        {
            string starTag = HTML.GetStarTag(item);
            StringBuilder itemToReturn = new StringBuilder(starTag);
            string innerHTML = HTML.GetInnerHTML(item);
            string bodyConfig = GetBodyConfig(innerHTML);

            if (!string.IsNullOrEmpty(bodyConfig))
                innerHTML = innerHTML.Replace(bodyConfig, string.Empty);

            itemToReturn.Append(innerHTML);

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                if (type == FormTableTypes.Asociates)
                {
                    itemToReturn.Append(LoadTableHeader(starTag, dt.Columns, true, "iddoc", "doctypeid"));
                    itemToReturn.Append(LoadTableBody(starTag, dt, true, "iddoc", "doctypeid", -1));
                }
                else
                {
                    bool useExcecuteButton = (!string.IsNullOrEmpty(bodyConfig)
                        && bodyConfig.IndexOf("hideopentask", StringComparison.CurrentCultureIgnoreCase) == -1);

                    string docIdColName;
                    string docTypeIdColName;
                    long docTypeId;
                    SetDocIdColumnNameAndDocTypeId(dt, bodyConfig, out docIdColName, out docTypeIdColName, out docTypeId);
                    itemToReturn.Append(LoadTableHeader(starTag, dt.Columns, useExcecuteButton, docIdColName, docTypeIdColName));
                    itemToReturn.Append(LoadTableBody(starTag, dt, useExcecuteButton, docIdColName, docTypeIdColName, docTypeId));
                }
            }

            itemToReturn.Append(HTML.TABLE_ENDTAG);
            return itemToReturn;
        }

        private static void SetDocIdColumnNameAndDocTypeId(DataTable dt, string bodyConfig, out string docIdColName, out string docTypeIdColName, out long docTypeId)
        {
            docIdColName = string.Empty;
            docTypeIdColName = string.Empty;
            docTypeId = -1;
            string bodyId = HTML.GetAttributeValue(bodyConfig, "id");

            String[] zvarsItems = bodyConfig.Split(char.Parse("/"));
            if (zvarsItems.Length > 2)
            {
                string[] Items = zvarsItems[2].Trim().Split(')');
                //Para la columna de docId
                string[] values = Items[0].Replace("zvar(", string.Empty).Split('=');
                docIdColName = dt.Columns[int.Parse(values[1])].ColumnName;
                //Para el docTypeid
                docTypeId = long.Parse(Items[1].Split('=')[1]);
            }
        }

        private string GetBodyConfig(string innerHTML)
        {
            List<string> sToReturn = HTML.GetHTMLItem(innerHTML, "tbody");
            if (sToReturn == null || sToReturn.Count == 0)
                return string.Empty;
            else
                return sToReturn[0];
        }

        #endregion

        #region GenericFields

        public string ResolveDocIdField(string item)
        {
            Index indaux = new Index();
            indaux.Data = CurrentResult.ID.ToString();
            return AsignValue(indaux, item, CurrentResult, null);
        }

        public string ResolveDocIdTypeField(string item)
        {
            Index indaux = new Index();
            indaux.Data = CurrentResult.DocTypeId.ToString();
            return AsignValue(indaux, item, CurrentResult, null);
        }

        public string ResolveAllDocIdField(string html)
        {
            List<string> fields = HTML.GetHTMLItem(html, "input", "id=\"ZAMBA_DOC_ID(.*?)?\"");
            StringBuilder sb = new StringBuilder(html);
            foreach (string item in fields)
            {
                sb.Replace(item, ResolveDocIdField(item));
            }

            return sb.ToString();
        }

        public string ResolveAllDocTypeIdField(string html)
        {
            List<string> fields = HTML.GetHTMLItem(html, "input", "id=\"ZAMBA_DOC_T_ID(.*?)?\"");
            StringBuilder sb = new StringBuilder(html);
            foreach (string item in fields)
            {
                sb.Replace(item, ResolveDocIdTypeField(item));
            }

            return sb.ToString();
        }
        #endregion

        #endregion
    }
}