using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using System.Data;
using System.Text;
using Zamba.Services;
using Zamba.Web.App_Code.Helpers;
using Zamba.Web.Helpers;

namespace Zamba.Web
{
    /// <summary>
    /// Summary description for FormControlsController
    /// </summary>
    public class FormControlsController
    {
        #region Constantes
        const string STR_OPTION_FORMAT = "<option value=\"{0}\">{1}</option>";
        const string STR_QUERY_EQUAL = " = ";
        const string STR_QUERY_APOSTROPHE = "'";
        const string HTML_TR_FORMAT = "<tr>{0}</tr>";
        const string HTML_TH_FORMAT = "<th>{0}</th>";
        const string HTML_TD_FORMAT = "<td>{0}</td>";
        const string HTML_PARAMBODY = "<tbody class=\"hideopentask\" editablecolumns=\"{0}\">";
        const string HTML_EDITABLE_CELL = "<td><input type=\"text\" ZOriginalValue=\"\" value=\"{0}\" {1} id=\"{2}\" name=\"{3}\"/></td>";
        const string HTML_ATTRIBUTE_TEMPLATE = "{0}=\"{1}\"";
        const string HTML_INPUT_TABLE_ID_TEMPLATE = "{0}-{1}-{2}";
        #endregion

        public FormControlsController()
        {
        }

        #region SelectOptions
        /// <summary>
        /// Obtiene los options para un select desde una zvar en formato string
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="dataSourceName">Nombre de zvar para obtener los options</param>
        /// <param name="displayMember">Columna que se usara como caption de los options</param>
        /// <param name="valueMember">Columna que se usara como valor de los options</param>
        /// <param name="filterColumn">Columna para filtrar valores, usar String.Empty si no se quiere filtrar</param>
        /// <param name="filterValue">Valor de filtrado, si filterColumn = string.Empty no se tendra en cuenta</param>
        /// <returns></returns>
        public FieldOptions GetZVarOptions(string controlId,
                                                                            string dataSourceName,
                                                                            string displayMember,
                                                                            string valueMember,
                                                                            string filterColumn,
                                                                            string filterValue)
        {
          
                FieldOptions fo = new FieldOptions();
                fo.ControlId = controlId;

                if (string.IsNullOrEmpty(dataSourceName))
                    throw new Exception("No se encuentra nombre de fuente de datos");

                object dataSource;

                if (dataSourceName.ToLower().StartsWith("zfillselect"))
                {
                    SZQuery sQuery = new SZQuery();
                    //Quitar el fillselect y dejar query | parametros
                    string query = dataSourceName.ToLower().Replace("zfillselect(", string.Empty);
                    query = query.Remove(query.Length - 1, 1);
                    //Obtener los parametros de query
                    string[] queryParamValues = filterValue.Split(new char[] { '|' });
                    //Obtener la query
                    string[] splitedQueryAndParams = query.Split(new char[] { '|' });
                    query = splitedQueryAndParams[0];

                    //Si hay parametros, los obtengo
                    string[] queryParamNames = null;
                    if (splitedQueryAndParams.Length > 0)
                    {
                        queryParamNames = splitedQueryAndParams[1].Split(new char[] { ',' });
                    }

                    //Llenar el source
                    dataSource = sQuery.FillSource(query, queryParamValues, queryParamNames);
                    sQuery = null;

                    //Convertir el source a las opciones de select
                    fo.SelectOptions = SetSelectOptions(dataSource, displayMember, valueMember, string.Empty, string.Empty);
                }
                else
                {
                    if (!VariablesInterReglas.ContainsKey(dataSourceName))
                        throw new Exception("Variable de datos no encontrada");

                    dataSource = VariablesInterReglas.get_Item(dataSourceName);

                    if (dataSource == null)
                        throw new Exception("Variable de datos vacia");

                    fo.SelectOptions = SetSelectOptions(dataSource, displayMember, valueMember, filterColumn, filterValue);
                }

                return fo;
           
        }

        /// <summary>
        /// Convierte el datasource pasado en un string con los options, respetando valor a mostrar, valor de opcion y filtrado
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="filterColumn"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        private static string SetSelectOptions(object dataSource, string displayMember, string valueMember, string filterColumn, string filterValue)
        {
            string selectOptions;

            DataTable dtSource = dataSource as DataTable;
            if (dtSource == null)
            {
                DataSet ds = dataSource as DataSet;
                if (ds != null)
                {
                    dtSource = ds.Tables[0];
                }
            }

            if (dtSource != null)
            {
                //Si no debe filtrar se convierte la tabla a options normalmente, sino se convierte el resultado del filtrado.
                if (string.IsNullOrEmpty(filterColumn))
                    selectOptions = TableToOptions(dtSource, displayMember, valueMember);
                else
                    selectOptions = TableToOptions(FilterTable(dtSource, filterColumn, filterValue), displayMember, valueMember);
            }
            else
                throw new Exception("Tipo de variable no reconocido");

            return selectOptions;
        }

        private static DataTable FilterTable(DataTable dtSource, string filterColumn, string filterValue)
        {
            if (!dtSource.Columns.Contains(filterColumn))
                throw new Exception("Columna de filtrado no encontrada");

            DataView dv = null;
            StringBuilder sb = null;

            try
            {
                dv = new DataView(dtSource);
                sb = new StringBuilder();
                //Obtengo el tipo de la columna a filtrar
                Type columnType = dtSource.Columns[filterColumn].DataType;
                //armo la condicion de filtro, columna
                sb.Append(filterColumn);
                //operador
                sb.Append(STR_QUERY_EQUAL);
                //valor, dependiendo si agregar comillas o no
                if (columnType == typeof(char) || columnType == typeof(string))
                {
                    sb.Append(STR_QUERY_APOSTROPHE);
                    sb.Append(filterValue);
                    sb.Append(STR_QUERY_APOSTROPHE);
                }
                else
                    sb.Append(filterValue);

                dv.RowFilter = sb.ToString();
                return dv.ToTable();
            }
            finally
            {
                if (dv == null)
                {
                    dv.Dispose();
                    dv = null;
                }
                if (sb == null)
                {
                    sb.Capacity = 0;
                    sb.Length = 0;
                    sb = null;
                }
            }
        }

        private static string TableToOptions(DataTable dt, string displayMember, string valueMember)
        {
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            if (!dt.Columns.Contains(displayMember))
                throw new Exception("No existe columna a mostrar");
            if (!dt.Columns.Contains(valueMember))
                throw new Exception("No existe valor a utilizar");

            StringBuilder sb = null;
            try
            {
                sb = new StringBuilder();
                long rowCount = dt.Rows.Count;
                //Se obtienen los indices de posicion de las columnas a usar
                int displayMemberPosition = dt.Columns[displayMember].Ordinal;
                int valueMemberPosition = dt.Columns[valueMember].Ordinal;

                for (int i = 0; i < rowCount; i++)
                {
                    //Por cada fila se va apendeando un nuevo option en base a una constante de formato, se usa item array para dar mas performance
                    sb.AppendFormat(STR_OPTION_FORMAT, dt.Rows[i].ItemArray[valueMemberPosition], dt.Rows[i].ItemArray[displayMemberPosition]);
                }

                return sb.ToString();
            }
            finally
            {
                if (sb == null)
                {
                    sb.Capacity = 0;
                    sb.Length = 0;
                    sb = null;
                }
            }
        }
        #endregion

        #region htmlTables
        /// <summary>
        /// Obtiene los options para un select desde una zvar en formato string
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="dataSourceName">Nombre de zvar para obtener los options</param>
        /// <param name="displayMember">Columna que se usara como caption de los options</param>
        /// <param name="valueMember">Columna que se usara como valor de los options</param>
        /// <param name="filterColumn">Columna para filtrar valores, usar String.Empty si no se quiere filtrar</param>
        /// <param name="filterValue">Valor de filtrado, si filterColumn = string.Empty no se tendra en cuenta</param>
        /// <returns></returns>
        public FieldOptions GetZDynamicTable(string controlId,
                                                                            string dataSourceName,
                                                                            string showColumns,
                                                                            string filterFieldId,
                                                                            string editableColumns,
                                                                            string editableColumnsAttributes,
                                                                            string filterValues,
                                                                            string additionalValidationButton,
                                                                            string postAjaxFuncion)
        {
           
                FieldOptions fo = new FieldOptions();
                fo.ControlId = controlId;
                fo.AdditionalValidationButton = additionalValidationButton;
                fo.PostAjaxFunction = postAjaxFuncion;

                if (string.IsNullOrEmpty(dataSourceName))
                    throw new Exception("No se encuentra nombre de fuente de datos");

                object dataSource;
                dataSource = FillSource(dataSourceName, filterValues);

                //Convertir el source a las opciones de select
                fo.SelectOptions = SetHTMLRows(dataSource, showColumns, editableColumns, editableColumnsAttributes, string.Empty, string.Empty);

                return fo;
          
        }

        private static object FillSource(string dataSourceName, string filterValues)
        {
            object dataSource = null;
            if (dataSourceName.ToLower().StartsWith("zfillselect"))
            {
                SZQuery sQuery = new SZQuery();
                //Quitar el fillselect y dejar query | parametros
                string query = dataSourceName.ToLower().Replace("zfillselect(", string.Empty);
                query = query.Remove(query.Length - 1, 1);
                //Obtener los parametros de query
                string[] queryParamValues = filterValues.Split(new char[] { '|' });
                //Obtener la query
                string[] splitedQueryAndParams = query.Split(new char[] { '|' });
                query = splitedQueryAndParams[0];

                //Si hay parametros, los obtengo
                string[] queryParamNames = null;
                if (splitedQueryAndParams.Length > 0)
                {
                    queryParamNames = splitedQueryAndParams[1].Split(new char[] { ',' });
                }

                //Llenar el source
                dataSource = sQuery.FillSource(query, queryParamValues, queryParamNames);
                sQuery = null;
            }
            else
            {
                //TODO implementar para variables de zamba.

                //if (!VariablesInterReglas.ContainsKey(dataSourceName))
                //    throw new Exception("Variable de datos no encontrada");

                //dataSource = VariablesInterReglas.get_Item(dataSourceName);

                //if (dataSource == null)
                //    throw new Exception("Variable de datos vacia");

                //fo.SelectOptions = SetHTMLRows(dataSource, showColumns, gType, filterColumn, filterValue);
            }
            return dataSource;
        }

        /// <summary>
        /// Obtiene en un string todos los tr y td del cuerpo de una tabla para el datasource
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="showColumns"></param>
        /// <param name="editableColumns"></param>
        /// <param name="editableColumnsAttributes"></param>
        /// <param name="filterColumn"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        private static string SetHTMLRows(object dataSource, string showColumns, string editableColumns,
                                                                            string editableColumnsAttributes, string filterColumn, string filterValue)
        {
            string selectOptions;

            DataTable dtSource = dataSource as DataTable;
            if (dtSource == null)
            {
                DataSet ds = dataSource as DataSet;
                if (ds != null)
                {
                    dtSource = ds.Tables[0];
                }
            }

            if (dtSource != null)
            {
                //Si no debe filtrar se convierte la tabla a options normalmente, sino se convierte el resultado del filtrado.
                if (string.IsNullOrEmpty(filterColumn))
                    selectOptions = TableHTMLRows(dtSource, showColumns, editableColumns, editableColumnsAttributes);
                else
                    selectOptions = TableHTMLRows(FilterTable(dtSource, filterColumn, filterValue), showColumns, editableColumns, editableColumnsAttributes);
            }
            else
                throw new Exception("Tipo de variable no reconocido");

            return selectOptions;
        }

        /// <summary>
        /// Obtiene en un string todos los tr y td del cuerpo de una tabla para el datasource
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="showColumns"></param>
        /// <param name="editableColumns"></param>
        /// <param name="editableColumnsAttributes"></param>
        /// <returns></returns>
        private static string TableHTMLRows(DataTable dtSource, string showColumns, string editableColumns, string editableColumnsAttributes)
        {
            string a = string.Empty;
            a = LoadZVarTableHeader(a, dtSource.Columns, string.Empty);
            //Se crea un tbody para la parametrizacion de valores como columnas editables
            a = LoadZVarTableBody(a, dtSource.Rows, string.Format(HTML_PARAMBODY, editableColumns), editableColumnsAttributes);
            return a;
        }

        /// <summary>
        /// Carga el contenido de un DataTable en el body de una tabla Html del documento
        /// </summary>
        /// <param name="table">Tabla HTML donde se cargaran los datos</param>
        /// <param name="drs">Rows que van a ser cargadas</param>
        /// <param name="body">Cuerpo de la tabla</param>
        /// <remarks></remarks>
        public static string LoadZVarTableBody(String table, DataRowCollection drs, string body, string editableColumnsAttributes)
        {
            String CurrentRow = String.Empty;
            Dictionary<int, List<string[]>> dic = null;
            if (!string.IsNullOrEmpty(editableColumnsAttributes))
            {
                dic = ParseColumConfiguration(editableColumnsAttributes);
            }

            int rowOrdinal = 0;

            string[] editablecolumns = null;
            if (body.ToLower().Contains("editablecolumns"))
                editablecolumns = HTML.GetAttributeValue(body, "editablecolumns").Split(',');

            foreach (DataRow dr in drs)
            {
                rowOrdinal++;
                CurrentRow = CurrentRow + "<tr>";

                if (string.IsNullOrEmpty(body.Trim()) == false)
                {

                    if (body.ToLower().Contains("zamba_rule"))
                    {
                        StringBuilder Link = new StringBuilder();
                        Link.Append("<tr class=\"FormRowStyle\"><td style='text-decoration: none'  width='20'><input type='submit' class='submit' value='Ingresar' ");

                        //Hay un error con este metodo, que para obtener el atributo quita el nombre del atributo del contenido del mismos, ejemplo de cantidad si el atributo es id queda cantad
                        string idvalue = HTML.GetAttributeValue(body, "id");

                        String[] zvarsItems = idvalue.Split(char.Parse("/"));
                        if (zvarsItems.Length > 2)
                        {

                            string[] Items = zvarsItems[2].Trim().Split(char.Parse(")"));

                            String FirstStringForRuleId = zvarsItems[0].Trim();
                            Int32 Start = FirstStringForRuleId.IndexOf("zamba_rule");
                            Int32 End = FirstStringForRuleId.Length - Start;
                            string ruleId = FirstStringForRuleId.Substring(Start, End);

                            Link.Append(" onclick=" + Convert.ToChar(34) + "SetRuleIdAndZvar(this,'" + ruleId + "','");

                            string[] editablecols = null;

                            if (body.ToLower().Contains("editablecolumns"))
                            {
                                string colsvalue = HTML.GetAttributeValue(body, "editablecolumns");
                                editablecols = colsvalue.Split(Char.Parse(","));

                            }

                            foreach (String item in Items)
                            {
                                if (string.IsNullOrEmpty(item) == false)
                                {
                                    string[] values = item.Split(char.Parse("("));
                                    String row = string.Empty;
                                    if (item.ToLower().Contains("zvar"))
                                    {
                                        values[0] = "id";
                                        row = values[0] + "=";
                                        Link.Append(row);
                                        if (editablecols != null && editablecols.Contains(values[1].Replace("]", string.Empty)))
                                        {
                                            Link.Append("' + $(this).closest('tr').find('td:eq(" + values[1].Replace("]", string.Empty) + ")').children('input').val() + '");
                                        }
                                        else
                                        {
                                            Link.Append("' + $(this).closest('tr').find('td:eq(" + values[1].Replace("]", string.Empty) + ")').text() + '");
                                        }
                                    }

                                    Link.Append("&");
                                }
                            }
                            Link.Remove(Link.Length - 4, 4);
                            Link.Append(")" + Convert.ToChar(34));
                        }

                        Link.Append(" style=" + Convert.ToChar(34) + "" + Convert.ToChar(34) + " /></td>");


                        CurrentRow = CurrentRow.Replace("<tr>", Link.ToString());
                    }
                    else if (body.ToLower().Contains("hideopentask"))
                    {

                    }
                    else
                    {
                        StringBuilder Link = new StringBuilder();
                        Link.Append("<tr class=\"FormRowStyle\"><td style='text-decoration: none'  width='20'><a href=" + Convert.ToChar(34));
                        Link.Append(Zamba.Web.Helpers.Tools.GetProtocol(HttpContext.Current.Request));
                        Link.Append(HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
                        Link.Append(HttpContext.Current.Request.ApplicationPath);
                        Link.Append("/Views/WF/TaskSelector.ashx?");

                        String[] zvarsItems = body.Split(char.Parse("/"));
                        if (zvarsItems.Length > 2)
                        {
                            string[] Items = zvarsItems[2].Trim().Split(char.Parse(")"));

                            foreach (String item in Items)
                            {
                                if (string.IsNullOrEmpty(item) == false)
                                {
                                    string[] values = item.Split(char.Parse("="));
                                    String row = string.Empty;
                                    if (item.ToLower().Contains("zvar"))
                                    {
                                        row = item.Replace("zvar(", string.Empty);
                                        Link.Append(row.Split(char.Parse("="))[0]);
                                    }
                                    else
                                    {
                                        Link.Append(values[0]);
                                    }

                                    if (values.Length > 1)
                                    {
                                        Link.Append("=");
                                        if (string.IsNullOrEmpty(row))
                                        {
                                            Link.Append(values[1]);
                                        }
                                        else
                                        {
                                            Link.Append(dr[Int32.Parse(values[1])]);
                                        }
                                    }
                                    Link.Append("&");
                                }
                            }
                            Link.Remove(Link.Length - 1, 1);
                        }
                        Link.Append("&userId=");
                        Link.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);

                        Link.Append(Convert.ToChar(34));
                        Link.Append(" style=" + Convert.ToChar(34) + "text-decoration: none" + Convert.ToChar(34) + " ><img height='20' src='");
                        Link.Append(Zamba.Web.Helpers.Tools.GetProtocol(HttpContext.Current.Request));
                        Link.Append(HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
                        Link.Append(HttpContext.Current.Request.ApplicationPath);
                        Link.Append("/Content/Images/Toolbars/play.png' border='0'/> </a></td>");
                        CurrentRow = CurrentRow.Replace("<tr>", Link.ToString());

                    }
                }

                CurrentRow += GetHTMLCells(dr, editablecolumns, dic, rowOrdinal) + "</tr>";
            }
            return table + CurrentRow;
        }

        /// <summary>
        /// Convierte una lista de string[nombre atributo][valor] en atributos html
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string GetAttributesString(List<string[]> list)
        {
            StringBuilder sbAttributes = new StringBuilder();
            try
            {
                int maxAttributes = list.Count;
                for (int i = 0; i < maxAttributes; i++)
                {
                    sbAttributes.AppendFormat(HTML_ATTRIBUTE_TEMPLATE, list[i][0], list[i][1]);
                }
                return sbAttributes.ToString();
            }
            finally
            {
                if (sbAttributes != null)
                {
                    sbAttributes.Length = 0;
                    sbAttributes.Capacity = 0;
                }
            }
        }

        /// <summary>
        /// Parsea la configuracion de columnas en un diccionario que tiene como key el ordinal de la columna y como value la lista de atributos para los input
        /// </summary>
        /// <param name="editableColumnsAttributes"></param>
        /// <returns></returns>
        private static Dictionary<int, List<string[]>> ParseColumConfiguration(string editableColumnsAttributes)
        {
            if (string.IsNullOrEmpty(editableColumnsAttributes))
                return null;

            Dictionary<int, List<string[]>> dicToReturn = new Dictionary<int, List<string[]>>();
            string[] columnsConfiguration = editableColumnsAttributes.Split('^');
            string[] splittedConfiguration;
            int configCount = columnsConfiguration.Length;
            int colPosition;
            List<string[]> listAttributes;
            for (int i = 0; i < configCount; i++)
            {
                columnsConfiguration[i] = columnsConfiguration[i].Replace("[", string.Empty);
                columnsConfiguration[i] = columnsConfiguration[i].Replace("]", string.Empty);
                splittedConfiguration = columnsConfiguration[i].Split('|');
                colPosition = int.Parse(splittedConfiguration[0]);
                listAttributes = new List<string[]>();
                foreach (string item in splittedConfiguration.Skip(1))
                {
                    listAttributes.Add(item.Split(':'));
                }
                dicToReturn.Add(colPosition, listAttributes);
            }

            return dicToReturn;
        }

        /// <summary>
        /// Otiene el string de cada una de las celdas de un DataRow dado
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="editablecolumns"></param>
        /// <param name="columnConfigurations"></param>
        /// <param name="rowOrdinal"></param>
        /// <returns></returns>
        private static string GetHTMLCells(DataRow dr, string[] editablecolumns, Dictionary<int, List<string[]>> columnConfigurations, int rowOrdinal)
        {
            StringBuilder sbCell = new StringBuilder();
            try
            {
                int cellCount = dr.ItemArray.Length;
                bool isCellEditable;
                object CellValue;
                string sCellValue;
                string editableCellAtributes;
                Random r = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                string idName;
                int randomId = r.Next();

                //Por cada columna de la fila
                for (int i = 1; i <= cellCount; i++)
                {
                    //Verifica se es editable(si hay columnas editables, verifica que la actual este dentro de ellas
                    isCellEditable = editablecolumns != null && editablecolumns.Contains(i.ToString());
                    //Obtengo el valor de la columna
                    CellValue = dr.ItemArray[i - 1];

                    if (CellValue is DateTime)
                    {
                        sCellValue = CellValue.ToString();
                        if (sCellValue.Length > 10)
                            sCellValue = sCellValue.Substring(0, 10);
                    }
                    else
                        sCellValue = HttpUtility.HtmlEncode(CellValue.ToString());

                    if (isCellEditable)
                    {
                        editableCellAtributes = string.Empty;
                        if (columnConfigurations != null && columnConfigurations.ContainsKey(i))
                        {
                            editableCellAtributes = GetAttributesString(columnConfigurations[i]);
                        }

                        //Armo el id del input (numero random-posicion de fila-posicion de columna)
                        idName = string.Format(HTML_INPUT_TABLE_ID_TEMPLATE, randomId.ToString(), rowOrdinal, i.ToString());
                        sbCell.AppendFormat(HTML_EDITABLE_CELL, sCellValue, editableCellAtributes, idName, idName);
                    }
                    else    
                    {
                        sbCell.AppendFormat(HTML_TD_FORMAT, sCellValue);
                    }
                }

                return sbCell.ToString();
            }
            finally
            {
                if (sbCell != null)
                {
                    sbCell.Length = 0;
                    sbCell.Capacity = 0;
                }
            }
        }

        /// <summary>
        /// Carga las columnas header de un DataTable en una tabla Html del documento
        /// para variables zvar
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dcs"></param>
        /// <remarks></remarks>
        public static string LoadZVarTableHeader(String table, DataColumnCollection dcs, String body)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                String HeaderColumn = string.Empty;

                if (string.IsNullOrEmpty(body.Trim()) == false && body.ToLower().Contains("hideopentask") == false)
                {
                    sb.AppendLine("<th>Ejecutar</th>");
                }

                //Agrego columnas de atributos
                foreach (DataColumn Column in dcs)
                {
                    sb.AppendFormat(HTML_TH_FORMAT, Column.ColumnName);
                }

                return table + string.Format(HTML_TR_FORMAT, sb.ToString());
            }
            finally
            {
                if (sb != null)
                {
                    sb.Length = 0;
                    sb.Capacity = 0;
                    sb = null;
                }
            }
        }

        #endregion

        #region AutoComplete
        /// <summary>
        /// Devuelve una lista de key|value para completar las opciones de un autocomplete
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dataSourceName"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="additionalFilters"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetAutoCompleteOptions(string query,
                                                                            string dataSourceName, string displayMember, string valueMember, string additionalFilters)
        {
           
                List<KeyValuePair<string, string>> aOp;

                if (string.IsNullOrEmpty(dataSourceName))
                    throw new Exception("No se encuentra nombre de fuente de datos");

                //agrego a los filtros adicionales el valor del ingresado en el textbox
                if (additionalFilters.Length > 0)
                    additionalFilters = additionalFilters.Insert(0, query + "|");
                else
                    additionalFilters = query;

                object dataSource;
                dataSource = FillSource(dataSourceName, additionalFilters);

                //Llamo al parse del source a key|value
                aOp = ParseSourceToDatakey(dataSource, valueMember, displayMember);

                return aOp;
          
        }

        /// <summary>
        /// Convierte el source en una lista de key|value, la key será el displayMember, y el value será el valueMember
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> ParseSourceToDatakey(object dataSource, string valueMember, string displayMember)
        {
            List<KeyValuePair<string, string>> toReturn = new List<KeyValuePair<string, string>>();

            //Intenta obtener el source. Tipos aceptados: DataTable o DataSet
            DataTable dtSource = dataSource as DataTable;
            if (dtSource == null)
            {
                DataSet ds = dataSource as DataSet;
                if (ds != null)
                {
                    dtSource = ds.Tables[0];
                }
                ds = null;
            }

            KeyValuePair<string, string> kp;

            //Si hay source itera por las filas del source para irlo parseando.
            if (dtSource != null)
            {
                foreach (DataRow item in dtSource.Rows)
                {
                    kp = new KeyValuePair<string, string>(item[displayMember].ToString(), item[valueMember].ToString());
                    toReturn.Add(kp);
                }
            }
            else
                throw new Exception("Tipo de variable no reconocido");

            dtSource = null;

            return toReturn;
        }

        #endregion
    }

}