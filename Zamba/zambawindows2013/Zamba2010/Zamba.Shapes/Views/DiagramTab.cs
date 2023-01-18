using System.Windows.Forms;
using MindFusion.Diagramming.WinForms;
using Zamba.Core;
using Zamba.Shapes.Controllers;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Telerik.WinControls.UI;
using System;
using System.Drawing;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming;
using Zamba.AppBlock;

namespace Zamba.Shapes.Views
{
    class DiagramTab : Telerik.WinControls.UI.RadPageViewPage
    {
        public DiagramView DiagramView { get; set; }
        public ZToolBar DiagramToobar { get; set; }
        public ToolStripComboBox TbbActorsFilter { get; set; }
        public ToolStripComboBox TbbCategoryFilter { get; set; }
        public string PreviousDiagram { get; set; }
        IDiagram _currDiag;
        object[] _params;
        public object[] Parameters { get{return this._params;} set{this._params=value;}}

        public DiagramTab(IDiagram diagram, object[] parameters, bool doNotAddToolbar)
        {
            try
            {
                _currDiag = diagram;
                _params = parameters;

                //Agrega el diagrama al controlador de diagramas de windows
                this.DiagramView = new DiagramView((MindFusion.Diagramming.Diagram)diagram);
                SetDiagramViewProperties(this.DiagramView);

                //Agrega el controlador de diagramas al tab
                this.Controls.Add(this.DiagramView);

                //Agrega la toolbar con los filtros de diagramas
                if (!doNotAddToolbar)
                {
                    AddToolbar();
                    this.Controls.Add(DiagramToobar);
                }

                //Id para no abrir dos veces el mismo diagrama
                if (parameters != null)
                {

                    switch (parameters[0].GetType().Name)
                    {
                        case "TableNode":
                            this.Name = diagram.DiagramType.ToString() + ";" + ((TableNode)parameters[0]).Id.ToString();
                            break;
                        default:
                            if (parameters[0] is ICore)
                            {
                                this.Name = diagram.DiagramType.ToString() + ";" + ((ICore)parameters[0]).ID.ToString();
                            }
                            else
                            {
                                this.Name = diagram.DiagramType.ToString() + ";" + ((GenericShape)parameters[0]).Id.ToString();
                            }

                            break;

                    }

                }
                else
                {
                    this.Name = diagram.DiagramType.ToString() + ";" + String.Empty;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        void AddToolbar()
        {
            //Se agrega la toolbar
            this.DiagramToobar = new ZToolBar();
            this.DiagramToobar.Dock = DockStyle.Top;
            this.DiagramToobar.BringToFront();
            this.DiagramToobar.Height = 25;

            if (_currDiag.DiagramActors != null && _currDiag.DiagramActors.Rows.Count > 0)
                LoadActorFilter();

            if (_currDiag.DiagramType == DiagramType.WorkFlowRules)
            {
                if (this.DiagramToobar.Items.Count > 0)
                    this.DiagramToobar.Items.Add(new ToolStripSeparator());

                LoadCategoryFilter();
            }

            if (DiagramToobar.Items.Count > 0)
                DiagramToobar.Visible = true;
            else
                DiagramToobar.Visible = false;
        }

        private void LoadCategoryFilter()
        {
            TbbCategoryFilter = new ToolStripComboBox() { Name = "Categoria" };
            TbbCategoryFilter.ToolTipText = "Filtrar por categoría de regla";
            TbbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            this.DiagramToobar.Items.Add("Categoría");
            this.DiagramToobar.Items.Add(TbbCategoryFilter);

            for (int i = 1; i <= 10; i++)
            {
                TbbCategoryFilter.Items.Add(i.ToString());
            }

            TbbCategoryFilter.Text = _params[1].ToString();
            TbbCategoryFilter.SelectedIndexChanged += new System.EventHandler(tbbCategory_Click);
        }

        void tbbCategory_Click(object sender, System.EventArgs e)
        {
            DiagramTab previousTab = ((Diagram)_currDiag).PreviousDiagramTab;
            Int32 category = Int32.Parse(((ToolStripComboBox)sender).Text);
            IDiagram tempDiagram;

            tempDiagram = new GenericController().ApplyRuleCategoryFilter(category, _currDiag.DiagramType, _params);

            if (tempDiagram != null)
            {
                _currDiag = tempDiagram;
                ((Diagram)_currDiag).PreviousDiagramTab = previousTab;
                RefreshDiagramView(_currDiag);
            }
        }

        private void LoadActorFilter()
        {
            TbbActorsFilter = new ToolStripComboBox() { Name = "Actor" };
            TbbActorsFilter.ToolTipText = "Filtrar por actor";
            TbbActorsFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            TbbActorsFilter.Items.Add("Todos");
            TbbActorsFilter.Text = "Todos";

            this.DiagramToobar.Items.Add(new ToolStripLabel("Actor"));
            this.DiagramToobar.Items.Add(TbbActorsFilter);

            DataView view = new DataView(_currDiag.DiagramActors);
            DataTable table = view.ToTable(true, "name");

            foreach (DataRow row in table.Rows)
            {
                TbbActorsFilter.Items.Add(row["NAME"].ToString());
            }

            TbbActorsFilter.SelectedIndexChanged += new System.EventHandler(mnuActors_Click);
        }
        
        void mnuActors_Click(object sender, System.EventArgs e)
        {
            DiagramTab previousTab = ((Diagram)_currDiag).PreviousDiagramTab;
            IDiagram tempDiagram;

            if (((ToolStripComboBox)sender).Text == "Todos")
                tempDiagram = GenericController.GetDiagram(_currDiag.DiagramType, _params);
            else
                tempDiagram = new GenericController().ApplyActorFilter(((ToolStripComboBox)sender).Text, _currDiag.DiagramType, _params);

            if (tempDiagram != null)
            {
                _currDiag = tempDiagram;
                ((Diagram)_currDiag).PreviousDiagramTab = previousTab;
                RefreshDiagramView(_currDiag);
            }
        }

        public void RefreshDiagramView(IDiagram diagram)
        {
            if (diagram != null)
            {
                //JP: falta ver si se puede refrescar de una forma mas "elegante" el diagrama
                DiagramView dv = new DiagramView((MindFusion.Diagramming.Diagram)diagram);
                SetDiagramViewProperties(dv);
                UcDiagrams ucDiagram = (UcDiagrams)((RadPageView)this.Parent).Parent;
                dv.Diagram.NodeDoubleClicked += ucDiagram.DiagramNodeDoubleClicked;
                dv.Diagram.NodeClicked += ucDiagram.DiagramNodeClicked;
                dv.MouseWheel += ucDiagram.diagramView1_MouseWheel;

                this.Controls.Remove(this.DiagramView);
                this.DiagramView = dv;
                this.Controls.Add(dv);
                this.Controls.Remove(DiagramToobar);
                this.Controls.Add(DiagramToobar);
                this.DiagramView.ScrollTo(DiagramView.ScrollX, 0);
            }
        }

        void SetDiagramViewProperties(DiagramView diagramView)
        {
            diagramView.Diagram.BackBrush = new MindFusion.Drawing.SolidBrush(Color.White);
            diagramView.Diagram.ShadowBrush = new MindFusion.Drawing.SolidBrush(Color.FromArgb(30, Color.Black));
            diagramView.BackColor = System.Drawing.Color.White;
            diagramView.Diagram.NodeEffects.Add(new MindFusion.Diagramming.GlassEffect());
            diagramView.ShowScrollbars = true;
            diagramView.AutoScroll = true;
            diagramView.Behavior = MindFusion.Diagramming.Behavior.DoNothing;
            diagramView.Dock = DockStyle.Fill;
        }
    } 
}
