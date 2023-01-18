using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Zamba.Core;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using Zamba.Shapes.Helpers;

namespace Zamba.Shapes.Views
{
    public partial class UcExportControl : Form
    {
        private UcDiagrams ucDiagrams;
        private int DiagramCount = 0;
        UCWfExportPanel wfPnl = null;
        
        //templates
        private string DiagramMasterTemplate = "Templates\\diagramTemplate.htm";
        private string DiagramStyleSheet = "Templates\\diagramStyleSheet.htm";
        private string DiagramRow = "Templates\\diagramRow.htm";
        private string DiagramUseCaseTemplate = "Templates\\diagramUseCaseTemplate.htm";
        private string DiagramUseCaseStepsGrid = "Templates\\diagramUseCaseStepsGrid.htm";

		public IWFNodeHelper NodeHelper { get; private set; }

		public UcExportControl(IWFNodeHelper WFNodeHelper)
        {
            InitializeComponent();
			NodeHelper = WFNodeHelper;

		}

        private void UcExportControl_Load(object sender, EventArgs e)
        {
            loadFixedReports();
        }
    

       
        #region private methods
        private void loadFixedReports()
        {
            this.chkHome.Text = DiagramTypeName.GetTypeName(Core.DiagramType.Home);
            this.chkActors.Text = DiagramTypeName.GetTypeName(Core.DiagramType.Actors);
            this.chkWorkflows.Text = DiagramTypeName.GetTypeName(Core.DiagramType.Workflows);
            this.chkInterfaces.Text = DiagramTypeName.GetTypeName(Core.DiagramType.Interfaces);
            this.chkReports.Text = DiagramTypeName.GetTypeName(Core.DiagramType.Reports);
            this.chkEntities.Text = DiagramTypeName.GetTypeName(Core.DiagramType.DocType);
        }

        private StringBuilder RenderToHTML(Bitmap diagram_image, StringBuilder row)
        {
            try
            {
                var ms = new MemoryStream();
                Bitmap image = diagram_image;
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] data = ms.ToArray();
                string base64ConvertedString = Convert.ToBase64String(data);

                row.Replace("diagram_image", "data:image/png;base64," + base64ConvertedString);

                return row;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            return null;
        }        

        private System.Drawing.Bitmap getDiagram(Core.DiagramType diagram)
        {
            try
            {
                return ucDiagrams.GenerateDiagramImage(diagram);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            return null;
        }

        private void ShowReport(StringBuilder diagramHTML)
        {
            try
            {
                var savefile = new SaveFileDialog
                {
                    AddExtension = true,
                    AutoUpgradeEnabled = true,
                    CheckPathExists = true,
                    DefaultExt = ".htm",
                    FileName = "Informe de Graficos.htm",
                    RestoreDirectory = true
                };

                DialogResult dialogResult = savefile.ShowDialog();

                if (dialogResult == DialogResult.Cancel)
                    return;

                var SW = new StreamWriter(savefile.FileName);
                SW.AutoFlush = true;
                SW.Write(diagramHTML);
                SW.Close();
                SW.Dispose();

                System.Diagnostics.Process.Start(savefile.FileName);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }
        #endregion
        #region public methods
        public Bitmap GenerateDinamicDiagram(ShapeType shapeType, int EntityID)
        {
            try
            {
                switch (shapeType)
                {
                    case ShapeType.Workflow:
                        return ucDiagrams.GenerateWorkflowDiagram(EntityID);
                    case ShapeType.Step:
                        return ucDiagrams.GenerateStepDiagram(EntityID);
                    case ShapeType.Rule:
                        return ucDiagrams.GenerateRuleDiagram(EntityID);
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            return null;
        }

        public void RenderRuleConditions(StringBuilder row, Int32 ruleID)
        {
            string preCondition, PostCondition;
            DataTable grdSteps;
            WFRulesBusinessExt wfrb;

            try
            {
                IRule rule = WFRulesBusiness.GetInstanceRuleById(ruleID,true);
                wfrb = new WFRulesBusinessExt();
                preCondition = wfrb.GetRuleCondition(rule.ID);

                grdSteps = wfrb.GetUseCaseTypeSteps(rule).Tables[0];

                UCUseCase ucTC = new UCUseCase(rule);
                StringBuilder pst = new StringBuilder();
                PostCondition = ucTC.GetPostCondition(rule, pst);

                //reemplazo datos
                row.Replace("diagram_UC_Title", rule.Name);
                row.Replace("diagram_UC_PreCondition", preCondition);


                //recorro el datatable agregando los pasos del caso de uso
                foreach (DataRow gridrow in grdSteps.Rows)
                {
                    row.Replace("diagram_UC_StepsGrid", GetDiagramUseCaseStepsGrid());
                    row.Replace("diagram_UC_ID", gridrow[0].ToString());
                    row.Replace("diagram_UC_Description", gridrow[1].ToString());
                }
                row.Replace("diagram_UC_StepsGrid", "");
                row.Replace("diagram_UC_PostCondition", PostCondition);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            finally { wfrb = null; }
        }

        #endregion
        #region events
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            List<Core.DiagramType> diagrams = new List<Core.DiagramType>();
            try
            {
                System.Drawing.Bitmap diagram_image = null;
                ucDiagrams = new UcDiagrams { Dock = DockStyle.Fill };
                var diagramHTML = new StringBuilder();


                diagramHTML.Append(GetDiagramStyleSheet());
                diagramHTML.Append(GetDiagramTemplate());

                if (chkHome.Checked)
                    diagrams.Add(Core.DiagramType.Home);
                if (chkActors.Checked)
                    diagrams.Add(Core.DiagramType.Actors);
                if (chkWorkflows.Checked)
                    diagrams.Add(Core.DiagramType.Workflows);
                if (chkInterfaces.Checked)
                    diagrams.Add(Core.DiagramType.Interfaces);
                if (chkReports.Checked)
                    diagrams.Add(Core.DiagramType.Reports);
                if (chkEntities.Checked)
                    diagrams.Add(Core.DiagramType.DocType);

                //------------------------------------------------------------------------------------------------
                diagramHTML.Replace("Zamba.LogoCliente", ZOptBusiness.GetValue("IconClientLogoOnTestCaseTemplate"));
                diagramHTML.Replace("Zamba.LogoZamba", ZOptBusiness.GetValue("IconZambaLogoOnTestCaseTemplate"));

                StringBuilder row = new StringBuilder();
                foreach (Core.DiagramType dgm in diagrams)
                {
                    DiagramCount++;
                    row.Append(GetDiagramRow());

                    row.Replace("diagram_title", DiagramCount.ToString() + ") " + dgm.ToString());

                    row.Replace("diagram_description", DiagramsHelper.GetDiagramTypeDescripcion(dgm));

                    row.Replace("diagram_UseCase_row", "");

                    //obtengo el diagrama
                    diagram_image = getDiagram(dgm);
                    row = RenderToHTML(diagram_image, row);
                }

                //agrego los diagramas dinamicos
                if (wfPnl != null)
                    if (wfPnl.CheckedNodes != null)
                        if (wfPnl.CheckedNodes.Count > 0)
                        {
                            DiagramCount++;
                            char[] pipe = { '|' };
                            foreach (System.String entity in wfPnl.CheckedNodes)
                            {
                                string entityID, shapeType;
                                entityID = entity.Split(pipe)[0];
                                shapeType = entity.Split(pipe)[1];
                                Bitmap image = null;
                                switch (shapeType)
                                {
                                    case "Workflow":
                                        image = GenerateDinamicDiagram(ShapeType.Workflow, Convert.ToInt32(entityID));
                                        row.Append(GetDiagramRow());
                                        row.Replace("diagram_title", DiagramCount.ToString() + ") " + WFBusiness.GetWorkflowNameByWFId(Convert.ToInt64(entityID)));
                                        row.Replace("diagram_description", "");
                                        row.Replace("diagram_UseCase_row", "");
                                        row = RenderToHTML(image, row);
                                        break;
                                    case "Step":
                                        image = GenerateDinamicDiagram(ShapeType.Step, Convert.ToInt32(entityID));
                                        row.Append(GetDiagramRow());
                                        row.Replace("diagram_title", DiagramCount.ToString() + ") " + WFStepBusiness.GetStepNameById(Convert.ToInt64(entityID)));
                                        row.Replace("diagram_description", "");
                                        row.Replace("diagram_UseCase_row", "");
                                        row = RenderToHTML(image, row);
                                        break;
                                    case "Rule":
                                        Int32 ruleID = Convert.ToInt32(entityID);
                                        image = GenerateDinamicDiagram(ShapeType.Rule, ruleID);
                                        row.Append(GetDiagramRow());
                                        row.Replace("diagram_title", DiagramCount.ToString() + ") " + WFRulesBusiness.GetRuleNameById(Convert.ToInt64(entityID)));
                                        row.Replace("diagram_description", "");
                                        row = RenderToHTML(image, row);

                                        if (wfPnl.PrintTestCases)
                                        {
                                            row.Replace("diagram_UseCase_row", GetDiagramUseCaseTemplate());
                                            
                                            RenderRuleConditions(row, ruleID);
                                        }
                                        else
                                            row.Replace("diagram_UseCase_row", "");
                                        break;
                                }
                            }
                        }

                diagramHTML.Replace("diagramRow", row.ToString());

                this.Close();
                this.Dispose();

                //muestro el informe al usuario
                ShowReport(diagramHTML);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al generar el informe.");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }        

        private void btnLoadWFs_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                wfPnl = new UCWfExportPanel(NodeHelper);
                wfPnl.Height = 600;
                wfPnl.Width = 400;
                wfPnl.Show();
                wfPnl.BringToFront();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            finally  {this.Cursor = Cursors.Default;}
        }
        #endregion
        #region templates
        private String GetDiagramTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, DiagramMasterTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetDiagramUseCaseTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, DiagramUseCaseTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetDiagramUseCaseStepsGrid()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, DiagramUseCaseStepsGrid));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetDiagramStyleSheet()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, DiagramStyleSheet));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetDiagramRow()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, DiagramRow));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        #endregion
    }
}
