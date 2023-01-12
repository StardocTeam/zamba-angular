using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Zamba.Core;
using Zamba.CoreExt;
using Zamba.Data;
namespace Zamba.ZTC
{
    public class TCBusiness
    {
        public static void AssingTCToProject(decimal TCId, decimal projectId, decimal objecttypeid)
        {
            var dbTools = new DBToolsExt();

            try
            {
                TCEntities dbContext;
                if(dbTools.UseWindowsAuthentication)
                    dbContext = new TCEntities(ControlsFactory.EntityConnectionString);
                else
                    dbContext = new TCEntities(ControlsFactory.EntityConnectionString, dbTools.DataBaseSchema);
                
                var PRO = new PRJ_R_O();

                PRO.OBJID = TCId;
                PRO.OBJTYP = objecttypeid;
                PRO.PRJID = projectId;

                dbContext.AddToPRJ_R_O(PRO);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                dbTools = null;
            }
        }

        public static Decimal GetProjectId(decimal TCId, decimal ObjectTypeId)
        {
            IQueryable<PRJ_R_O> query = from p in ControlsFactory.dbContext.PRJ_R_O
                                        where p.OBJID == TCId &&
                                              p.OBJTYP == ObjectTypeId
                                        select p;

            if (query.Count() > 0)
            {
                PRJ_R_O Prj = query.Single();

                return Prj.PRJID;
            }
            else
                return 0;
        }


        public DataTable GetTestCase(Decimal TestCaseId)
        {
            DataTable dtTC = ZTCData.GetTestCase(TestCaseId);

            return dtTC;
        }

        public DataTable GetTCD(Int64 TestCaseId)
        {
            DataTable dtTCD = ZTCData.GetTCD(TestCaseId);

            return dtTCD;
        }

        internal void CopyPaste(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                //you could change the code below according to your custom logic    
                string copyStr = ConvertSelectedDataToString((RadGridView) sender);
                Clipboard.SetDataObject(copyStr);
            }
        }

        /// <summary>   
        /// Prepare string in format suitable to paste in Excel sheet   
        /// </summary>   
        /// <param name="grid">Source selection RadGridView </param>   
        /// <returns>Formated string for clipboard</returns>   
        private string ConvertSelectedDataToString(RadGridView grid)
        {
            var strBuild = new StringBuilder();

            for (int row = 0; row < grid.SelectedRows.Count; row++)
            {
                for (int cell = 0; cell < grid.SelectedRows[row].Cells.Count; cell++)
                {
                    strBuild.Append(grid.SelectedRows[row].Cells[cell].Value);
                    strBuild.Append("\t");
                }

                strBuild.Append("\n");
            }

            return strBuild.ToString();
        }


        public bool Export_Excel(string fullname, RadGridView grid, ExportTypes exp)
        {
            try
            {
                switch (exp)
                {
                    case ExportTypes.CSV:
                        ExportToCSV expCSV;
                        expCSV = new ExportToCSV(grid);
                        expCSV.FileExtension = "csv";
                        expCSV.RunExport(fullname);
                        break;
                    case ExportTypes.Excel:
                        ExportToExcelML expExcel;
                        expExcel = new ExportToExcelML(grid);
                        expExcel.ExportVisualSettings = true;
                        expExcel.FileExtension = "xls";
                        expExcel.RunExport(fullname);
                        break;
                    case ExportTypes.PDF:
                        var expPDf = new ExportToPDF(grid);
                        expPDf.ExportVisualSettings = true;
                        expPDf.FitToPageWidth = true;
                        expPDf.FileExtension = "pdf";
                        expPDf.RunExport(fullname);
                        break;
                    case ExportTypes.Word:
                        ExportToHTML expWord;
                        expWord = new ExportToHTML(grid);
                        expWord.ExportVisualSettings = true;
                        expWord.FileExtension = "doc";
                        expWord.RunExport(fullname);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        /// <summary>
        /// Obtiene el nombre que tendra la solapa
        /// </summary>
        /// <param name="ObjectTypeId"></param>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public static String GetObjectName(Int64 ObjectTypeId, Int64 ObjectId)
        {
            try
            {
                String name = string.Empty;
                switch (ObjectTypeId)
                {
                    case 1:
                        name = "Indice: " + IndexsBusiness.GetIndexNameById(ObjectId);
                        break;
                    case 2:
                        name = " Tipo de Documento: " + DocTypesBusiness.GetDocTypeName(ObjectId, true);
                        break;
                    case 3:
                        //Implementar...
                        //return "Archivo: " + FileBusiness.GetUniqueFileName
                    case 5:
                        name = "Usuario: " + UserGroupBusiness.GetUserorGroupNamebyId(ObjectId);
                        break;
                    case 42:
                        name = "Etapa: " + WFStepBusiness.GetStepNameById(ObjectId);
                        break;
                    case 43:
                        name = "Proceso: " + WFRulesBusiness.GetRuleNameById(ObjectId);
                        break;
                    case 52:
                        ZwebForm form = FormBusiness.GetForm(ObjectId);
                        if (form != null)
                        {
                            name = "Formulario: " + GetFormNameType(form);
                            form.Dispose();
                            form = null;
                        }
                        break;
                    case 55:
                        name = "Workflow: " + WFBusiness.GetWorkflowNameByWFId(ObjectId);
                        break;
                    case 61:
                        name = "Módulo de Reportes: ";
                        break;
                    case 103:
                        name = "Grupo de Usuario: " + UserGroupBusiness.GetUserorGroupNamebyId(ObjectId);
                        break;
                    case 105:
                    case 109:
                        name = "Modulo: " + ZTCData.GetTypeDescription(ObjectId);
                        break;
                }
                return name;
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }

        public static string GetFormNameType(ZwebForm form)
        {
            //Se agrega if contemplando que el formulario no sea nulo y en caso de que lo sea retorne string.empty
            if (form != null)
            {
                switch (form.Type)
                {
                        //Se corrige los retornos, antes se asignaba al form.name y se retornaba, generando que cada
                        //vez que se vuelva a llamar se aniden los tipos.
                    case FormTypes.All:
                        return form.Name + " - Todo";
                    case FormTypes.Edit:
                        return form.Name + " - Editar";
                    case FormTypes.Insert:
                        return form.Name + " - Insertar";
                    case FormTypes.Search:
                        return form.Name + " - Busqueda";
                    case FormTypes.Show:
                        return form.Name + " - Visualizar";
                    case FormTypes.WebEdit:
                        return form.Name + " - EditarWeb";
                    case FormTypes.WebInsert:
                        return form.Name + " - InsertarWeb";
                    case FormTypes.WebSearch:
                        return form.Name + " - BusquedaWeb";
                    case FormTypes.WebShow:
                        return form.Name + " - VisualizarWeb";
                    case FormTypes.WebWorkFlow:
                        return form.Name + " - WorkFlowWeb";
                    case FormTypes.WorkFlow:
                        return form.Name + " - Workflow";
                    default:
                        return form.Name;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}