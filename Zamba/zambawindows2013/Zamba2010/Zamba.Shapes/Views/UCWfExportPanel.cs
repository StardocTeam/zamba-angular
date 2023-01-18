using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using System.Collections;

namespace Zamba.Shapes.Views
{
    public partial class UCWfExportPanel : Form
    {
       private WFTreeDiagram Tree;

       private bool selectedNodes {get; set;}
       public bool PrintTestCases { get; set; }
       public ArrayList CheckedNodes { get; set; }
		public IWFNodeHelper NodeHelper { get; private set; }

		public UCWfExportPanel(IWFNodeHelper WFNodeHelper)
        {
            InitializeComponent();
			NodeHelper = WFNodeHelper;
			//load tree            
			LoadWFTree();
        }

        public void LoadWFTree()
        {
            try
            {               
                Application.DoEvents();

                long currentUserID = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                Zamba.AppBlock.ZIconsList iconList = default(Zamba.AppBlock.ZIconsList);

                System.Collections.Generic.List<Zamba.Core.EntityView> Workflows = WFBusiness.GetUserWFIdsAndNamesWithSteps(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                System.Collections.Generic.List<Core.WorkFlow> WFs = new System.Collections.Generic.List<Core.WorkFlow>();
                foreach (Zamba.Core.EntityView W in Workflows)
                {
                    WFs.Add(WFBusiness.GetWFbyIdFilteredbyStepId(W.ID, W.ChildsEntities));
                }

                IUserGroup group = null;
                Tree = new WFTreeDiagram(WFs, iconList, group, true, false, false, false, NodeHelper);

                Tree.Name = "Zamba Software - Listado de Workflows";

                Tree.Dock = DockStyle.Fill;
                pnlWfTree.Controls.Add(Tree);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        private void btnSelectNodes_Click(object sender, EventArgs e)
        {
            try
            {
                CheckedNodes = Tree.CheckedNodes;
                selectedNodes = true;
                PrintTestCases = chkUseTestCases.Checked;
                this.Close();
            }
            catch (System.Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        private void UCWfExportPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Tree.CheckedNodes != null)
                    if (!selectedNodes)
                        Tree.CheckedNodes.Clear();
            }
            catch (System.Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }
      
    }
}
