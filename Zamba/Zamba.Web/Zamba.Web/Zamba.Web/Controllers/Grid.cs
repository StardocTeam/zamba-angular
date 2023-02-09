using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Zamba;
using Zamba.Core;
using Zamba.Services;

namespace Presenters
{
    public class Grid
    {
        private IGrid view;
        private long userid;
        private ArrayList _hideColumns;
        private ArrayList _hideColumnsTask;
        private Zamba.Core.IUser user;

        RightsBusiness RiB = new RightsBusiness();

        #region Constantes

        private const string DISKGROUPIDCOLUMNNAME = "DISK_GROUP_ID";
        private const string DOC_ID_COLUMNNAME = "Doc_ID";
        private const string DOCID_COLUMNNAME = "DocId";
        private const string DOC_FILE_COLUMNNAME = "DOC_FILE";
        private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
        private const string DISK_VOL_ID_COLUMNNAME = "disk_Vol_id";
        private const string DISK_VOL_PATH_COLUMNNAME = "DISK_VOL_PATH";
        private const string DO_STATE_ID_COLUMNNAME = "Do_State_ID";
        private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
        private const string IMAGEN_COLUMNNAME = "Imagen";
        private const string PLATTER_ID_COLUMNNAME = "PLATTER_ID";
        private const string VOL_ID_COLUMNNAME = "VOL_ID";
        private const string OFFSET_COLUMNNAME = "OFFSET";
        private const string ICON_ID_COLUMNNAME = "ICON_ID";
        private const string SHARED_COLUMNNAME = "SHARED";
        private const string VER_PARENT_ID_COLUMNNAME = "ver_Parent_id";
        private const string VERSION_COLUMNNAME = "version";
        private const string ROOTID_COLUMNNAME = "RootId";
        private const string ORIGINAL_FILENAME_COLUMNNAME = "original_Filename";
        private const string NUMEROVERSION_COLUMNNAME = "NumeroVersion";
        private const string NUMERO_DE_VERSION_COLUMNNAME = "numero de version";
        private const string CRDATE_COLUMNNAME = "crdate";
        private const string NAME1_COLUMNNAME = "NAME1";

        private const string STEP_ID_COLUMNNAME = "Step_Id";
        private const string ICONID_COLUMNNAME = "IconId";
        private const string CHECKIN_COLUMNNAME = "CheckIn";
        private const string TASK_ID_COLUMNNAME = "Task_ID";
        private const string WFSTEPID_COLUMNNAME = "WfStepId";
        private const string ASIGNADO_COLUMNNAME = "Asignado";
        private const string STATE_COLUMNNAME = "State";
        private const string ESTADO_TAREA_COLUMNNAME = "Estado";
        private const string SITUACION_COLUMNNAME = "Situacion";
        private const string EXPIREDATE_COLUMNNAME = "ExpireDate";
        private const string VENCIMIENTO_COLUMNNAME = "Vencimiento";
        private const string NAME_COLUMNNAME = "Name";
        private const string TASK_STATE_ID_COLUMNNAME = "task_state_id";
        private const string INGRESO_COLUMNNAME = "Ingreso";
        private const string USER_ASIGNED_COLUMNNAME = "User_Asigned";
        private const string USER_ASIGNED_BY_COLUMNNAME = "User_Asigned_By";
        private const string DATE_ASIGNED_BY_COLUMNNAME = "Date_asigned_By";
        private const string REMARK_COLUMNNAME = "Remark";
        private const string TAG_COLUMNNAME = "Tag";
        private const string DOCTYPEID_COLUMNNAME = "DoctypeId";
        private const string TASKCOLOR_COLUMNNAME = "TaskColor";
        private const string VER_COLUMNNAME = "Ver";
        private const string WORK_ID_COLUMNNAME = "Work_Id";
        private const string NOMBRE_DOCUMENTO_COLUMNNAME = "Nombre Documento";
        private const string SOLICITUD_COLUMNNAME = "Solicitud";

        #endregion

        #region Variables Privadas

        private string nombreDocumento_currUserConfig;
        private string imagen_currUserConfig;
        private string ver_currUserConfig;
        private string EstadoTarea_currUserConfig;
        private string Asignado_currUserConfig;
        private string Situacion_currUserConfig;
        private string Nombre_Original_currUserConfig;
        private string TipoDocumento_currUserConfig;

        private string version_UserConfig;
        private string NroVersion_UserConfig;

        private string FechaCreacion_currUserConfig;
        private string FechaModificacion_currUserConfig;

        #endregion

        public Grid(long UserId, IGrid View, Zamba.Core.IUser user)
        {
            this.view = View;
            this.userid = UserId;
            this.user = user;

            UserPreferences UserPreferences = new UserPreferences();

            nombreDocumento_currUserConfig = GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME;
            imagen_currUserConfig = GridColumns.IMAGEN_COLUMNNAME;
            ver_currUserConfig = GridColumns.VER_COLUMNNAME;
            TipoDocumento_currUserConfig = GridColumns.DOC_TYPE_NAME_COLUMNNAME;
            EstadoTarea_currUserConfig = GridColumns.STATE_COLUMNNAME;
            Asignado_currUserConfig = GridColumns.ASIGNADO_COLUMNNAME;
            Situacion_currUserConfig = GridColumns.SITUACION_COLUMNNAME;
            Nombre_Original_currUserConfig = GridColumns.ORIGINAL_FILENAME_COLUMNNAME;
            version_UserConfig = GridColumns.VERSION_COLUMNNAME;
            NroVersion_UserConfig = GridColumns.NUMERO_DE_VERSION_COLUMNNAME;
            FechaCreacion_currUserConfig = GridColumns.CRDATE_COLUMNNAME;
            FechaModificacion_currUserConfig = GridColumns.LASTUPDATE_COLUMNNAME;
        }

        //se quito el parametro userid  a todas las funciones de los servicios
        public void LoadDocTypes()
        {
            WFStepBusiness WFSB = new WFStepBusiness();

            DataTable dt = WFSB.GetDocTypesByWfStepAsDT(this.view.StepId);
            WFSB = null;

            view.cmbDocTypes.DataTextField = DOC_TYPE_NAME_COLUMNNAME;
            view.cmbDocTypes.DataValueField = DOC_TYPE_ID_COLUMNNAME;
            view.cmbDocTypes.DataSource = dt;
            view.cmbDocTypes.DataBind();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    view.cmbDocTypes.Enabled = false;
                }
                else
                {
                    view.cmbDocTypes.Enabled = true;

                    DataRow dr = dt.NewRow();
                    dr[DOC_TYPE_ID_COLUMNNAME] = 0;
                    dr[DOC_TYPE_NAME_COLUMNNAME] = "Todas las tareas";
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                view.cmbDocTypes.Enabled = false;
            }
        }

      

        public Object GetTaskByTaskIdAndDocTypeId(Int64 TaskId, Int64 DocTypeId, Int64 WFStepId, Int32 PageSize)
        {
            return new STasks().GetTaskByTaskIdAndDocTypeIdAndStepId(TaskId, DocTypeId, WFStepId, PageSize);
        }

        public DataTable LoadTaskHistory(int taskId)
        {
            DataSet ds = new DataSet();

            STasks Tasks = new STasks();
            ds = Tasks.GetTaskHistory(taskId);

            return ds.Tables[0];
        }

        public DataTable LoadIndexHistory(int taskId)
        {
            DataSet ds = new DataSet();

            STasks Tasks = new STasks();
            ds = Tasks.GetOnlyIndexsHistory(taskId);

            return ds.Tables[0];
        }

        public DataTable LoadMailHistory(long DocId)
        {
            DataSet datasetmailhist = new DataSet();
            STasks Tasks = new STasks();
            datasetmailhist = Tasks.getHistory(DocId);
            return datasetmailhist.Tables[0];
        }

        public DataTable LoadSearchResult(string[] qrys)
        {
            DataSet datasetmailhist = new DataSet();

            STasks Tasks = new STasks();

            /////////////////////////////////
            SResult Result = new SResult();
            return Result.webRunSearch(qrys);
            ///////////////////////////////////

        }

        public DataTable LoadDocAsoc(Int64 taskId, Int64 DocTypeId, Int64 WFStepId, Int32 PageSize)
        {
            DataTable dt = new DataTable();

            STasks Tasks = new STasks();

            ITaskResult task = Tasks.GetTaskByTaskIdAndDocTypeIdAndStepId(taskId, DocTypeId, WFStepId, PageSize);

            if (task != null)
            {
                dt = Tasks.getAsociatedDTResultsFromResult(task, 0, false, user, false);

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add(imagen_currUserConfig, typeof(Image)).SetOrdinal(1);

                    if (dt.Columns.Contains(nombreDocumento_currUserConfig))
                        dt.Columns[nombreDocumento_currUserConfig].SetOrdinal(0);

                    dt.Columns[FechaCreacion_currUserConfig].SetOrdinal(dt.Columns.Count - 1);
                    dt.Columns[FechaModificacion_currUserConfig].SetOrdinal(dt.Columns.Count - 1);
                    dt.Columns[Nombre_Original_currUserConfig].SetOrdinal(dt.Columns.Count - 1);
                    dt.Columns["Numero de Version"].ColumnName = this.NroVersion_UserConfig;
                    dt.Columns["Entidad"].ColumnName = this.TipoDocumento_currUserConfig;
                }


                return dt;
            }
            return dt;
        }

        public void GenerateGridScript(DataTable dt, string gridKey, string gridType)
        {
            if (dt != null)
            {
                ArrayList fields = new ArrayList();
                ArrayList colsOption = new ArrayList();

                string a = gridKey;

                if (dt.Rows.Count == 0)
                {
                    view.GridScript = "no hay datos";
                }
                else
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        string colname = column.ColumnName.Replace(" ", "").Replace("*", "").Replace("/", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace("%", "").Replace("\\", "").Replace("$", "");

                        switch (gridType)
                        {
                            case "DocAsoc":

                                if (!GetVisibility(column.ColumnName))
                                {
                                    fields.Add(String.Format("{{name: '{0}', type:'{1}', hidden:true}}", colname, getGridFieldDataType(column.DataType)));
                                    colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\", hidden:true}}", colname, column.ColumnName));
                                }
                                else
                                {
                                    fields.Add(String.Format("{{name: '{0}', type:'{1}'}}", colname, getGridFieldDataType(column.DataType)));
                                    colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\"}}", colname, column.ColumnName));
                                }

                                break;

                            case "Task":

                                if (SetColumnVisible(column.ColumnName) == false)
                                {
                                    fields.Add(String.Format("{{name: '{0}', type:'{1}', hidden:true}}", colname, getGridFieldDataType(column.DataType)));
                                    colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\", hidden:true}}", colname, column.ColumnName));
                                }
                                else
                                {
                                    fields.Add(String.Format("{{name: '{0}', type:'{1}'}}", colname, getGridFieldDataType(column.DataType)));
                                    colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\"}}", colname, column.ColumnName));
                                }

                                break;

                            case "MailHistory":

                                if (column.ColumnName.Contains("PATH"))
                                {
                                    fields.Add(String.Format("{{name: '{0}', type:'{1}', hidden:true}}", colname, getGridFieldDataType(column.DataType)));
                                    colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\", hidden:true}}", colname, column.ColumnName));
                                }
                                else
                                {
                                    fields.Add(String.Format("{{name: '{0}', type:'{1}'}}", colname, getGridFieldDataType(column.DataType)));
                                    colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\"}}", colname, column.ColumnName));
                                }
                                break;

                            default:

                                fields.Add(String.Format("{{name: '{0}', type:'{1}'}}", colname, getGridFieldDataType(column.DataType)));
                                colsOption.Add(String.Format("{{id: '{0}', header: \"{1}\"}}", colname, column.ColumnName));
                                break;

                        }
                    }

                    string ds = string.Format("var dsOption" + gridKey + " = {{fields: [{0}], recordType: 'array'}}", string.Join(",\n", (string[])fields.ToArray(typeof(string))));
                    string cols = string.Format("var colsOption" + gridKey + " = [{0}];", string.Join(",\n", (string[])colsOption.ToArray(typeof(string))));

                    view.GridScript = ds + "\n" + cols;
                }
            }
        }


        private string getGridFieldDataType(Type DataColumnType)
        {
            switch (DataColumnType.ToString())
            {
                case "System.Boolean":
                    return "bool";

                case "System.Int":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return "integer";

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

        private void verifyIfOverdueTask(ref DataRow row, ref Hashtable daysAndColors)
        {
            if (!((row[VENCIMIENTO_COLUMNNAME] == null)))
            {
                // Si la fecha de vencimiento es menor a la fecha actual
                if ((DateTime.Compare(((DateTime)row[VENCIMIENTO_COLUMNNAME]).Date, DateTime.Now.Date) == -1))
                {
                    ArrayList numbersMinorsToDifDays = new ArrayList();
                    TimeSpan dif = DateTime.Now.Date - ((DateTime)row[VENCIMIENTO_COLUMNNAME]).Date;

                    foreach (DictionaryEntry elem in daysAndColors)
                    {
                        // Si la cantidad de días de diferencia que se llevan la fecha de vencimiento y la fecha actual es mayor al número de la colección
                        if ((Convert.ToInt32(dif.TotalDays) > Convert.ToInt32(elem.Key)))
                        {
                            // Agregar número a colección temporal
                            numbersMinorsToDifDays.Add(Convert.ToInt32(elem.Key));
                        }
                    }

                    if (numbersMinorsToDifDays.Count > 0)
                    {
                        int previousNumber = 0;

                        // Busco el número más grande de la colección
                        foreach (int elem in numbersMinorsToDifDays)
                        {
                            if (previousNumber == 0)
                            {
                                previousNumber = elem;
                            }
                            else
                            {
                                if (elem > previousNumber)
                                {
                                    previousNumber = elem;
                                }
                            }
                        }

                        putColorInRow(ref row, ref previousNumber, ref daysAndColors);
                        numbersMinorsToDifDays.Clear();
                    }
                }
                else
                {
                    row[TASKCOLOR_COLUMNNAME] = "color: NEGRO";
                }
            }
        }

        private void putColorInRow(ref DataRow row, ref int previousNumber, ref Hashtable daysAndColors)
        {
            if (daysAndColors.Contains(previousNumber.ToString()))
            {
                switch (((string)daysAndColors[previousNumber.ToString()]).ToUpper())
                {
                    case "ROJO":
                        row[TASKCOLOR_COLUMNNAME] = "color: ROJO";
                        break;
                    case "VERDE":
                        row[TASKCOLOR_COLUMNNAME] = "color: VERDE";
                        break;
                    case "AMARILLO":
                        row[TASKCOLOR_COLUMNNAME] = "color: AMARILLO";
                        break;
                    case "AZUL":
                        row[TASKCOLOR_COLUMNNAME] = "color: AZUL";
                        break;
                    case "VIOLETA":
                        row[TASKCOLOR_COLUMNNAME] = "color: VIOLETA";
                        break;
                    case "GRIS":
                        row[TASKCOLOR_COLUMNNAME] = "color: GRIS";
                        break;
                }
            }
        }

        private bool GetVisibility(String ColName)
        {
            _hideColumns = new ArrayList();
            SRights Rights = new SRights();

            long aux;

            if (_hideColumns.Count == 0)
            {

                _hideColumns.Add("doc_id");
                _hideColumns.Add("doc_type_id");
                _hideColumns.Add("ver_parent_id");
                _hideColumns.Add("disk_group_id");
                _hideColumns.Add("platter_id");
                _hideColumns.Add("vol_id");
                _hideColumns.Add("offset");
                _hideColumns.Add("icon_id");
                _hideColumns.Add("shared");
                _hideColumns.Add("rootid");
                _hideColumns.Add("original_filename");
                _hideColumns.Add("disk_vol_id");
                _hideColumns.Add("disk_vol_path");
                _hideColumns.Add("doc_file");

                //if (useVersion == true)
                //{
                //    _hideColumns.Add("iddoc");
                //}
                //else
                //{
                _hideColumns.Add(version_UserConfig.ToLower());
                _hideColumns.Add(NroVersion_UserConfig.ToLower());
                //}

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowResultNameColumn, -1))
                {
                    _hideColumns.Add(nombreDocumento_currUserConfig.ToLower());
                }
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowIconNameColumn, -1))
                {
                    _hideColumns.Add(imagen_currUserConfig.ToLower());
                }
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowDocumentTypeColumn, -1))
                {
                    _hideColumns.Add(TipoDocumento_currUserConfig.ToLower());
                }
                //if (useVersion && RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowVersionColumn, -1))
                //{
                //    _hideColumns.Add(version_UserConfig.ToLower());
                //}
                //if (useVersion && RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowVersionNumberColumn, -1))
                //{
                //    _hideColumns.Add(NroVersion_UserConfig.ToLower());
                //}
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowCreatedDateColumn, -1))
                {
                    _hideColumns.Add(FechaCreacion_currUserConfig.ToLower());
                }
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowLastEditDateColumn, -1))
                {
                    _hideColumns.Add(FechaModificacion_currUserConfig.ToLower());
                }
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.AssociatedWebResultGridShowOriginalName, -1))
                {
                    _hideColumns.Add(Nombre_Original_currUserConfig.ToLower());
                }
            }

            if (_hideColumns.Contains(ColName.ToLower()))
            {
                return false;
            }
            else if (ColName.StartsWith("I") && Int64.TryParse(ColName.Remove(0, 1), out aux))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool GetVisibilityTaskGrid(String ColName)
        {
            _hideColumnsTask = new ArrayList();

            SRights Rights = new SRights();
            long aux;

            if (_hideColumnsTask.Count == 0)
            {
                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridShowResultNameColumn, -1))
                    _hideColumnsTask.Add(nombreDocumento_currUserConfig);

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridShowIconNameColumn, -1))
                    _hideColumnsTask.Add(imagen_currUserConfig);

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridVerColumn, -1))
                    _hideColumnsTask.Add(ver_currUserConfig);

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridShowTaskStateColumn, -1))
                    _hideColumnsTask.Add(EstadoTarea_currUserConfig);

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridShowAssignedToColumn, -1))
                    _hideColumnsTask.Add(Asignado_currUserConfig);

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridShowSituationColumn, -1))
                    _hideColumnsTask.Add(Situacion_currUserConfig);

                if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.Grids, RightsType.TaskWebResultGridShowOriginalName, -1))
                    _hideColumnsTask.Add(Nombre_Original_currUserConfig);
            }

            if (_hideColumnsTask.Contains(ColName))
            {
                return false;
            }
            else if ((ColName.StartsWith("I") && Int64.TryParse(ColName.Remove(0, 1), out aux)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SetColumnVisible(String name)
        {
            UserPreferences UP = new UserPreferences();

            if (name.Contains("Ver"))
                return false;
            if (name.Contains("Task_ID"))
                return false;
            if (name.Contains("Do_State_ID"))
                return false;
            if (name.Contains("Imagen"))
                return false;
            if (name.Contains("IconId"))
                return false;
            if (name.Contains("Ingreso"))
                return bool.Parse(UP.getValue("ColumnCheckInVisible", UPSections.WorkFlow, "False"));
            if (name.Contains("Vencimiento"))
                return bool.Parse(UP.getValue("ColumnExpireDateVisible", UPSections.WorkFlow, "False"));
            if (name.Contains("User_Asigned"))
                return false;
            if (name.Contains("User_Asigned_By"))
                return false;
            if (name.Contains("Date_asigned_By"))
                return false;
            if (name.Contains("task_state_id"))
                return false;
            if (name.Contains("Remark"))
                return false;
            if (name.Contains("Tag"))
                return false;
            if (name.Contains("Work_Id"))
                return false;
            if (name.Contains("State"))
                return false;
            if (name.Contains("WfStepId"))
                return false;
            if (name.Contains("DocId"))
                return false;
            if (name.Contains("DoctypeId"))
                return false;
            if (name.Contains("TaskColor"))
                return false;

            if (name.Contains(nombreDocumento_currUserConfig))
                return GetVisibilityTaskGrid(nombreDocumento_currUserConfig);
            if (name.Contains(imagen_currUserConfig))
                return GetVisibilityTaskGrid(imagen_currUserConfig);
            if (name.Contains(EstadoTarea_currUserConfig))
                return GetVisibilityTaskGrid(EstadoTarea_currUserConfig);
            if (name.Contains(Asignado_currUserConfig))
                return GetVisibilityTaskGrid(Asignado_currUserConfig);
            if (name.Contains(Situacion_currUserConfig))
                return GetVisibilityTaskGrid(Situacion_currUserConfig);
            if (name.Contains(ver_currUserConfig))
                return GetVisibilityTaskGrid(ver_currUserConfig);
            if (name.Contains(Nombre_Original_currUserConfig))
                return GetVisibilityTaskGrid(Nombre_Original_currUserConfig);

            return true;
        }
    }
}
