using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Zamba.Core;

public partial class UCUseCase : UserControl
{
    public UCUseCase(IRule rule)
    {
        InitializeComponent();

        try
        {
            var list = new Dictionary<int, string>();
            foreach (object v in Enum.GetValues(typeof(ExportTypes)))
            {
                list.Add((int)v, Enum.GetName(typeof(ExportTypes), (int)v));
            }
            cmbExportTypes.ComboBox.DisplayMember = "Value";
            cmbExportTypes.ComboBox.ValueMember = "Key";
            cmbExportTypes.ComboBox.DataSource = new BindingSource(list, null);

            cmbExportTypes.ComboBox.SelectedIndex = 0;

            list.Clear();
            list = null;

            txtTitle.Text = rule.Name;

            WFRulesBusinessExt wfrb = new WFRulesBusinessExt();

            txtPrecondicion.Text = wfrb.GetRuleCondition(rule.ID);

            grdSteps.DataSource = wfrb.GetUseCaseTypeSteps(rule).Tables[0];
            grdSteps.Refresh();
            grdSteps.Update();

            StringBuilder pst = new StringBuilder();
            txtPostCondition.Text = GetPostCondition(rule, pst);

            wfrb = null;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    List<Int64> ProcessedRules = new List<Int64>();
    
    /// <summary>
    /// Obtiene las post condiciones de la regla
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="pst"></param>
    /// <returns></returns>
    public string GetPostCondition(IRule rule, StringBuilder pst)
    {
        try
        {
            if (rule != null)
            {
                switch (rule.RuleClass.ToLower())
                {
                    case "dodistribuir":
                        pst.Append("Distribucion a etapa ");
                        pst.Append(WFStepBusiness.GetStepNameById(((IDoDistribuir)rule).NewWFStepId));
                        pst.Append(Environment.NewLine);
                        break;
                    case "dochangestate":
                        pst.Append("Cambio de estado a ");
                        pst.Append(WFStepStatesComponent.GetStepStateById(((IDoChangeState)rule).StateId).Name);
                        pst.Append(Environment.NewLine);
                        break;
                    case "doinputindex":
                        pst.Append("Modificacion del atributo completado por usuario ");
                        pst.Append(IndexsBusiness.GetIndexNameById(((IDOInputIndex)rule).Index));
                        pst.Append(Environment.NewLine);
                        break;
                    case "dorequestdata":
                        pst.Append("Modificaciones de atributos completados por usuario");
                        pst.Append(Environment.NewLine);
                        break;
                    case "dofillindex":
                        Int64 indexID;
                        if (Int64.TryParse(((IDoFillIndex)rule).IndexId, out indexID))
                        {
                            pst.Append("Modificacion del atributo completado automaticamente ");
                            pst.Append(IndexsBusiness.GetIndexNameById(indexID));
                        }
                        else
                        {
                            pst.Append("Modificacion de atributos completados automaticamente");
                        }
                        pst.Append(Environment.NewLine);
                        break;
                    case "dofillindexdefault":
                        pst.Append("Modificacion del atributo completado automaticamente ");
                        pst.Append(IndexsBusiness.GetIndexNameById(((IDoFillIndexDefault)rule).IndexID));
                        pst.Append(Environment.NewLine);
                        break;
                    case "dogeneratetaskresult":
                        pst.Append("Documento generado para entidad ");
                        pst.Append(DocTypesBusiness.GetDocTypeName(((IDOGenerateTaskResult)rule).docTypeId, true));
                        pst.Append(Environment.NewLine);
                        break;
                    case "doasign":
                        if (((IDoAsign)rule).UserId > 0)
                        {
                            pst.Append("Asignacion de tarea a ");
                            pst.Append(UserGroupBusiness.GetUserorGroupNamebyId(((IDoAsign)rule).UserId));
                        }
                        else
                            pst.Append("Asignacion de tarea");
                        pst.Append(Environment.NewLine);
                        break;
                    case "doaddasociateddocument":
                        pst.Append("Agregado de documento para entidad ");
                        pst.Append(((IDoAddAsociatedDocument)rule).AsociatedDocType.Name);
                        pst.Append(Environment.NewLine);
                        break;
                    case "doexecuterule":
                        Int64 RuleID = ((IDOExecuteRule)rule).RuleID;

                        if (ProcessedRules.Contains(RuleID) == false)
                        {
                            ProcessedRules.Add(RuleID);
                            IRule exeRule = WFRulesBusiness.GetInstanceRuleById(RuleID, true);
                            GetPostCondition(exeRule, pst);
                            exeRule.Dispose();
                            exeRule = null;
                        }

                        break;
                    case "dodelete":
                        pst.Append("Borrado de documento");
                        pst.Append(Environment.NewLine);
                        break;
                    case "doaddasociatedform":
                        pst.Append("Agregado de documento con formulario ");
                        pst.Append(FormBusiness.GetForm(((IDoAddAsociatedForm)rule).FormID).Name);
                        pst.Append(Environment.NewLine);
                        break;

                }

                foreach (IRule childRule in rule.ChildRules)
                {
                    GetPostCondition(childRule, pst);
                }
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

        return pst.ToString();
    }

    private void LoadSteps(DataTable Dt)
    {
        try
        {
            grdSteps.MasterTemplate.AutoGenerateColumns = true;

            grdSteps.DataSource = Dt;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Exporta el contenido de la grilla a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnExport_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog();
        try
        {
            sfd.Title = "Ingrese la ruta y el nombre del archivo de Excel";
            switch ((ExportTypes)Enum.Parse(typeof(ExportTypes), cmbExportTypes.ComboBox.SelectedValue.ToString())
                )
            {
                case ExportTypes.CSV:
                    sfd.Filter = "CSV files (*.csv)|*.csv";
                    break;
                case ExportTypes.Excel:
                    sfd.Filter = "excel files (*.xls)|*.xls";
                    break;
                case ExportTypes.PDF:
                    sfd.Filter = "PDF files (*.pdf)|*.pdf";
                    break;
                case ExportTypes.Word:
                    sfd.Filter = "Word files (*.doc)|*.doc";
                    break;
            }

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    var TCB = new TCBusiness();
            //    if (TCB.Export_Excel(sfd.FileName, grdSteps,
            //                         (ExportTypes)
            //                         Enum.Parse(typeof(ExportTypes),
            //                                    cmbExportTypes.ComboBox.SelectedValue.ToString())))
            //        MessageBox.Show("Exportacion realizada con exito", "Zamba Software", MessageBoxButtons.OK);
            //    else
            //        MessageBox.Show("Ha ocurrido un error en la exportacion", "Zamba Software", MessageBoxButtons.OK);
            //}
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            sfd.Dispose();
            sfd = null;
        }
    }
}
