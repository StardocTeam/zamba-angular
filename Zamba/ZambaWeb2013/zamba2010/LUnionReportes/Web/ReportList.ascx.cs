using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Zamba.ReportBuilder.Business;
using System.Data;

namespace Web
{

    public partial class ReportList : System.Web.UI.UserControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RadTreeView2.Nodes.Count == 6)
            {
                RadTreeNode reportNode = new RadTreeNode("Informes Dinamicos");
                
                ReportBuilderFactory rpf = new ReportBuilderFactory();
                DataSet reports = rpf.GetAllQueryIdsAndNames();

                if (reports != null && reports.Tables.Count > 0)
                foreach (DataRow dr in rpf.GetAllQueryIdsAndNames().Tables[0].Rows)
                {
                    RadTreeNode DinamicReportNode = new RadTreeNode(dr[1].ToString());
                    DinamicReportNode.Category = "Report";
                    DinamicReportNode.Value = dr[0].ToString();
                    reportNode.Nodes.Add(DinamicReportNode);
                }

                RadTreeView2.Nodes.Add(reportNode);
            }

            LoadReports();
        }
           

        public event EventHandler ShowReport;
        public event EventHandler ClearReport;


        private static void ShowCheckedNodes(RadTreeView treeView, Label label)
        {
            string message = string.Empty;
            IList<RadTreeNode> nodeCollection = treeView.CheckedNodes;
            foreach (RadTreeNode node in nodeCollection)
            {
                message += node.FullPath + "<br/>";
            }
            label.Text = message;
        }

      

        private void LoadReports()
        {
            if (ClearReport != null)
                ClearReport(String.Empty, null);

            foreach (RadTreeNode node in RadTreeView2.Nodes)
                if (node.Category == "Report")
                {
                    String ReportName = node.Value;

                    if (node.Checked)
                    {
                        if (ShowReport != null)
                            ShowReport(ReportName, EventArgs.Empty);
                    }
                }
                else
                {
                    foreach (RadTreeNode nodeR in node.Nodes)
                        if (nodeR.Category == "Report")
                        {
                            String ReportName = nodeR.Value;

                            if (nodeR.Checked || node.Checked)
                            {
                                if (ShowReport != null)
                                    ShowReport(ReportName, EventArgs.Empty);
                            }
                        }
                }
        }


        protected void RadTreeView2_NodeCheck(object o, RadTreeNodeEventArgs e)
        {
            //Verifica que solo sea un nodo padre
            if (e.Node.ParentNode == null)
            {
                //Verifica si se acaba de tildar
                if (e.Node.Checked)
                {
                    //Tilda a los hijos y expande el nodo para visualizar los hijos
                    e.Node.CheckChildNodes();
                    e.Node.Expanded = true;
                }
                else
                {
                    //Destilda todos sus nodos hijos
                    foreach (RadTreeNode node in e.Node.Nodes)
                    {
                        node.Checked = false;
                    }
                }
            }
            
            LoadReports();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }


    }
}
