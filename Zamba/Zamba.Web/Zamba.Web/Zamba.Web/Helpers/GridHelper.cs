using System;
using System.Collections;
using System.Data;
using Zamba.Core;
using Zamba.Services;

namespace Zamba.Web.App_Code.Helpers
{
    public class GridHelper
    {
        public enum GridType
        {
            Task
        }

        /// <summary>
        /// Genera el Js necesario para generar las columnas de la grilla
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldData"></param>
        /// <param name="columnsData"></param>
        /// <param name="colNameOpenString"></param>
        public static void GenerateFormatScript(DataTable dt, ref string fieldData, ref string columnsData, GridType gridtype)
        {
            GenerateFormatScript(dt, ref fieldData, ref columnsData, string.Empty, gridtype);
        }

        /// <summary>
        /// Genera el Js necesario para generar las columnas de la grilla 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldData"></param>
        /// <param name="columnsData"></param>
        /// <param name="colNameOpenString"></param>
        public static void GenerateFormatScript(DataTable dt, ref string fieldData, ref string columnsData, string colNameOpenString, GridType gridtype)
        {
            if (dt != null)
            {
                ArrayList fields = new ArrayList();
                ArrayList cols = new ArrayList();

                string colnameCleaned;

                foreach (DataColumn column in dt.Columns)
                {
                    colnameCleaned = cleanColumnName(column.ColumnName);

                    fields.Add(String.Format("{{name: '{0}', type: '{1}'}}", colnameCleaned, GetGridFieldDataType(column.DataType)));

                    if (!string.IsNullOrEmpty(colNameOpenString) && colNameOpenString.ToLower().Equals(column.ColumnName.ToLower()))
                    {
                        cols.Add(String.Format("{{id: '{0}', text: \"{1}\", dataIndex: '{0}', hidden: {2}, width: 150, renderer: renderOpen}}", colnameCleaned, CleanColumnNameAccents(column.ColumnName), GetVisibility(column.ColumnName, gridtype)));
                    }
                    else
                    {
                        cols.Add(String.Format("{{id: '{0}', text: \"{1}\", dataIndex: '{0}', hidden: {2}, width: 150}}", colnameCleaned, CleanColumnNameAccents(column.ColumnName), GetVisibility(column.ColumnName, gridtype)));
                    }
                }

                fieldData = string.Join(",\n", (string[])fields.ToArray(typeof(string)));
                columnsData = string.Join(",\n", (string[])cols.ToArray(typeof(string)));
            }
        }

        /// <summary>
        /// Genera el Js necesario para generar el modelo de datos de la grilla
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldData"></param>
        /// <param name="columnsData"></param>
        /// <param name="colNameOpenString"></param>
        public static string GenerateDataScript(DataTable dt)
        {
            ArrayList fields = new ArrayList();
            ArrayList cols = new ArrayList();

            foreach (DataRow row in dt.Rows)
            {
                fields.Clear();

                for (int i = 0; i < row.ItemArray.Length; i++)
                    fields.Add(String.Format("\"{0}\": \"{1}\"", cleanColumnName(dt.Columns[i].ColumnName), System.Web.HttpContext.Current.Server.HtmlEncode(row.ItemArray[i].ToString().Trim())));

                cols.Add("{" + string.Join(",", (string[])fields.ToArray(typeof(string))) + "}");
            }

            return string.Join(",", (string[])cols.ToArray(typeof(string)));
        }

        /// <summary>
        /// Genera el script en JSON a partir de los datos obtenidos del DT
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldData"></param>
        /// <param name="columnsData"></param>
        /// <param name="colNameOpenString"></param>
        public static string generateJSON(string callBack, long totalCount, string columnsData)
        {
            string response = string.Empty;

            if (!string.IsNullOrEmpty(callBack))
                response = callBack + "(";

            response = response + "{";
            response = response + "\"totalCount\": \"" + totalCount.ToString() + "\",";
            response = response + "\"zmbdata\": [";
            response = response + columnsData;
            response = response + "]";
            response = response + "}";

            if (!string.IsNullOrEmpty(callBack))
                response = response + ");";

            return response;
        }

        private static string cleanColumnName(string colname)
        {
            colname = colname.Replace(" ", string.Empty);
            colname = colname.Replace("*", string.Empty);
            colname = colname.Replace("/", string.Empty);
            colname = colname.Replace(".", string.Empty);
            colname = colname.Replace("(", string.Empty);
            colname = colname.Replace(")", string.Empty);
            colname = colname.Replace("%", string.Empty);
            colname = colname.Replace("\\", string.Empty);
            colname = colname.Replace("$", string.Empty);
            colname = CleanColumnNameAccents(colname);

            return colname;
        }

        private static string CleanColumnNameAccents(string colname)
        {
            colname = colname.Replace("á", "a");
            colname = colname.Replace("é", "e");
            colname = colname.Replace("í", "i");
            colname = colname.Replace("ó", "o");
            colname = colname.Replace("ú", "u");
            colname = colname.Replace("ñ", "n");

            return colname;
        }

        private static string GetGridFieldDataType(Type dataColumnType)
        {
            switch (dataColumnType.ToString())
            {
                case "System.Boolean":
                    return "boolean";

                case "System.Int":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return "int";

                case "System.Decimal":
                    return "decimal";

                case "System.Date":
                case "System.DateTime":
                    return "date";

                case "System.Long":
                    return "float";

                default:
                    return "string";
            }
        }

        public static bool GetVisibility(String colName, GridType gridtype)
        {
            return SetColumnVisible(colName, gridtype);
        }

        public static bool IsNumeric(object expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private static bool SetColumnVisible(String name, GridType gridtype)
        {
            UserPreferences UP = new UserPreferences();
            name = name.ToLower();

            if (name == ("i"))
                return false;
            if (name.Contains("task_id"))
                return false;
            if (name.Contains("do_state_id"))
                return false;
            if (name.Contains("imagen"))
                return false;
            if (name.Contains("iconid"))
                return false;
            if (name.Trim() == "ingreso")
                return bool.Parse(UP.getValue("ColumnCheckInVisible", UPSections.WorkFlow, "False"));
            if (name.Contains("vencimiento"))
                return bool.Parse(UP.getValue("ColumnExpireDateVisible", UPSections.WorkFlow, "False"));
            if (name.Contains("user_asigned"))
                return false;
            if (name.Contains("user_asigned_by"))
                return false;
            if (name.Contains("date_asigned_by"))
                return false;
            if (name.Contains("task_state_id"))
                return false;
            if (name.Contains("remark"))
                return false;
            if (name.Contains("tag"))
                return false;
            if (name.Contains("work_id"))
                return false;
            if (name.Contains("state"))
                return false;
            if (name.Contains("wfstepid"))
                return false;
            if (name.Contains("docid"))
                return false;
            if (name.Contains("doc_id"))
                return false;
            if (name.Contains("doctypeid"))
                return false;
            if (name.Contains("doc_type_id"))
                return false;
            if (name.Contains("taskcolor"))
                return false;

            if (name.Contains("ver_parent_id"))
                return false;
            if (name.Contains("disk_group_id"))
                return false;
            if (name.Contains("platter_id"))
                return false;
            if (name.Contains("vol_id"))
                return false;
            if (name.Contains("offset"))
                return false;
            if (name.Contains("icon_id"))
                return false;
            if (name.Contains("shared"))
                return false;
            if (name.Contains("rootid"))
                return false;
            if (name.Contains("original_filename"))
                return false;
            if (name.Contains("disk_vol_id"))
                return false;
            if (name.Contains("disk_vol_path"))
                return false;
            if (name.Contains("doc_file"))
                return false;
            if (name.Contains("version"))
                return false;
            if (name.Contains("exclusive"))
                return false;
            if (gridtype == GridType.Task)
            {
                ArrayList hiddenColumns = GetHiddenColumns();
                string taskColName;

                taskColName = GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME;
                if (name.Contains(taskColName.ToLower()))
                    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                taskColName = GridColumns.STATE_COLUMNNAME;
                if (name.Contains(taskColName.ToLower()))
                    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                taskColName = GridColumns.ASIGNADO_COLUMNNAME;
                if (name.Contains(taskColName.ToLower()))
                    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                taskColName = GridColumns.SITUACION_COLUMNNAME;
                if (name.Contains(taskColName.ToLower()))
                    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                taskColName = GridColumns.ORIGINAL_FILENAME_COLUMNNAME;
                if (name.Contains(taskColName.ToLower()))
                    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                //Esta columna debe ser visible para el funcionamiento correcto de la grilla
                //taskColName = GetColumnName(sup, "ColumnNameVer", "Ver");
                //if (name.Contains(taskColName.ToLower()))
                //    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                //taskColName = GetColumnName(sup, "ColumnNameImagen", "Imagen");
                //if (name.Contains(taskColName.ToLower()))
                //    return GetVisibilityTaskGrid(taskColName, hiddenColumns);

                hiddenColumns.Clear();
                hiddenColumns = null;
            }

            UP = null;
            return true;
        }

        private static bool GetVisibilityTaskGrid(String columnName, ArrayList hiddenColumns)
        {
            long aux;
            if (hiddenColumns.Contains(columnName))
                return false;
            else if (columnName.StartsWith("i") && Int64.TryParse(columnName.Remove(0, 1), out aux))
                return false;
            else
                return true;
        }

        private static ArrayList GetHiddenColumns()
        {
            ArrayList hiddenCols = new ArrayList();
            SRights sr = new SRights();

            if (!GetGridRight(sr, RightsType.TaskWebResultGridShowOriginalName))
                hiddenCols.Add(GridColumns.ORIGINAL_FILENAME_COLUMNNAME);

            //Esta columna debe ser visible para el funcionamiento correcto de la grilla
            //if (!GetGridRight(sr, RightsType.TaskWebResultGridVerColumn))
            //    hiddenCols.Add(GetColumnName(sup, "ColumnNameVer", "Ver"));

            //Esta columna estaba comentada también, pero desconozco si afecta el funcionamiento
            //if (!GetGridRight(sr, RightsType.TaskWebResultGridShowOriginalName))
            //    hiddenCols.Add(GetColumnName(sup, "ColumnNameImagen", "Imagen"));

          
            sr = null;

            return hiddenCols;
        }

        private static bool GetGridRight(SRights sr, RightsType rt)
        {
            return new RightsBusiness().GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Grids, rt, -1);
        }

        private static string GetColumnName( string columnNameOption, string defaultColumnName)
        {
            UserPreferences UP = new UserPreferences();
            return UP.getValue(columnNameOption, UPSections.UserPreferences, defaultColumnName);
           
        }
    }

}