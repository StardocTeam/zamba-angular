using System;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using System.Collections.Generic;

namespace Presenters
{
    public class Arbol
    {
        private IArbol view;
        private long userid;
        private IUser user;

        public Arbol(long userId, IArbol View, IUser user)
        {
            this.view = View;
            this.userid = userId;
            this.user = user;
        }

        public void FillWF()
        {
            try
            {
                //Se cambia el tipo a la variable WorkFlows para no tener que realizar el ".ToArray".
                List<EntityView> WorkFlows;

                SWorkflow SWorkflow = new SWorkflow();

                WorkFlows = SWorkflow.GetUserWFIdsAndNamesWithSteps(userid);

                if (WorkFlows != null && WorkFlows.Count > 0)
                {
                    TreeNode Root;
                    TreeNode WFNode;
                    TreeNode StepNode;

                    Root = new TreeNode("Procesos");

                    //Se comenta esta linea ya que no es usada en otro lado.
                    //ArrayList StepsOfRestrictedDoctypes = Zamba.Core.WFBusiness.GetStepsByUserRestrictedDoctypes(Zamba.Membership.MembershipHelper.CurrentUser.ID);

                    //Se agrega el count para cambiar el "foreach" a "for".
                    int countWF = WorkFlows.Count;

                    for (int i = 0; i < countWF;i++)
                    {
                        WFNode = new TreeNode(WorkFlows[i].Name, WorkFlows[i].ID.ToString());

                        //Se agrega el count para cambiar el "foreach" a "for".
                        int countCE = WorkFlows[i].ChildsEntities.Count;

                        EntityView step;

                        for (int j = 0; j < countCE; j++)
                        {
                            try
                            {
                                //Se hace una copia del step para acceder con mayor velocidad.
                                step = WorkFlows[i].ChildsEntities[j];

                                //Int64 count = step.ChildCount;
                                //Int64 count = Zamba.Core.WF.WF.WFTaskBusiness.GetTaskCount(step.ID, true, Zamba.Membership.MembershipHelper.CurrentUser.ID);

                                //StepNode = new TreeNode(step.Name + " (" + count + ")", step.ID.ToString(), "~/Content/Images/Arbol/bullet_ball_glass_red.png");
                                StepNode = new TreeNode(step.Name + " (actualizando...)", step.ID.ToString());

                                WFNode.ChildNodes.Add(StepNode);
                            }
                            catch (Exception exStep)
                            {
                                Zamba.AppBlock.ZException.Log(exStep);
                            }
                        }

                        Root.ChildNodes.Add(WFNode);
                    }

                    this.view.WFTreeView.Nodes.Clear();
                    this.view.WFTreeView.Nodes.Add(Root);
                }

                CollapseTreeView();
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }

        public void FillCmbFormTypes()
        {
            try
            {
                SForms SForms = new SForms();
                var arrayforms = SForms.GetVirtualDocumentsByRightsOfCreate(FormTypes.WebInsert);
                this.view.CmbFormType.DataSource = arrayforms;
                this.view.CmbFormType.DataTextField = "Name";
                this.view.CmbFormType.DataValueField = "ID";
                this.view.CmbFormType.DataBind();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public void CollapseTreeView()
        {
            if (this.view.WFTreeView.Nodes.Count > 0)
                foreach (TreeNode node in this.view.WFTreeView.Nodes[0].ChildNodes)
                    node.Collapse();
        }

        public void getWorkflowAndStepSelected(ref Int32 WFId, ref Int32 StepId)
        {
            try
            {
                TreeNode SelectedNode = this.view.WFTreeView.SelectedNode;

                if (SelectedNode != null)
                {
                    //ver si tiene parent
                    if (SelectedNode.Parent != null)
                    {
                        //si el parent es <> "procesos" entonces es una etapa, sino es un wf
                        if (SelectedNode.Parent.Text != "Procesos")
                            WFId = Int32.Parse(SelectedNode.Parent.Value);

                        StepId = Int32.Parse(SelectedNode.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }
    }
}