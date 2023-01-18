using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

public partial class Arbol : System.Web.UI.UserControl, IArbol
{

    /// <summary>
    /// Una vez finalizado el refresco del arbol de wf se eleva este evento para actualizar la grilla
    /// </summary>
    /// <param name="StepId"></param>
    /// <remarks></remarks>
    public event WFTreeRefreshedEventHandler WFTreeRefreshed;
    public delegate void WFTreeRefreshedEventHandler(Int32 StepId);
    public event WFTreeIsEmptyEventHandler WFTreeIsEmpty;
    public delegate void WFTreeIsEmptyEventHandler();
    public event SelectedNodeChangedEventHandler SelectedNodeChanged;
    public delegate void SelectedNodeChangedEventHandler(Int32 WFId, Int32 StepId, Int32 DocTypeId);

    private Presenters.Arbol Presenter;
    public Arbol()
    {
        Load += Page_Load;
        Init += Page_Init;
    }
    protected void Page_Init(object sender, System.EventArgs e)
    {
        if ((Session["User"] != null))
        {
            Presenter = new Presenters.Arbol(int.Parse(Session["UserId"].ToString()), this, (IUser)Session["User"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["User"] != null))
        {
            btnInsertar.Visible = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.InsertWeb, Zamba.Core.RightsType.View);
            if (btnInsertar.Visible)
            {
                Presenter.FillCmbFormTypes();
            }
        }

        string script = "$(document).ready(function() { FixWfTree(); AddStepCountHandler(); });";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WfTreeScripts", script, true);
    }

    public void Refresh()
    {
        try
        {
            int LastWFStepUsed = int.Parse(UserPreferences.getValue("UltimoWFStepUtilizado", Sections.WorkFlow, 0));
            Int32 WFId = default(Int32);
            Int32 stepID = default(Int32);

            if (LastWFStepUsed > 0)
            {
                Presenter.getWorkflowAndStepSelected(ref WFId, ref LastWFStepUsed);
                SelectStepNode(LastWFStepUsed);

                if (ArbolProcesos.Nodes.Count > 0)
                {
                    bool nodeselected = false;

                    foreach (TreeNode nodeMain in ArbolProcesos.Nodes[0].ChildNodes)
                    {
                        if (nodeMain.Value == LastWFStepUsed.ToString())
                        {
                            nodeMain.Select();
                            if (SelectedNodeChanged != null)
                            {
                                SelectedNodeChanged(WFId, LastWFStepUsed, 0);
                            }
                            break; // TODO: might not be correct. Was : Exit For
                        }

                        foreach (TreeNode NodeChild in nodeMain.ChildNodes)
                        {
                            if (NodeChild.Value == LastWFStepUsed.ToString())
                            {
                                nodeMain.Expand();
                                NodeChild.Select();
                                if (SelectedNodeChanged != null)
                                {
                                    SelectedNodeChanged(WFId, LastWFStepUsed, 0);
                                }
                                nodeselected = true;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        if (nodeselected)
                            break; // TODO: might not be correct. Was : Exit For
                    }



                }
            }
            else {
                Presenter.getWorkflowAndStepSelected(ref WFId, ref stepID);
                if (SelectedNodeChanged != null)
                {
                    SelectedNodeChanged(WFId, stepID, 0);
                }
            }
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
    }

    private void RefreshTree()
    {
        try
        {
            Presenter.FillWF();

            int LastWFStepUsed = int.Parse(UserPreferences.getValue("UltimoWFStepUtilizado", Sections.WorkFlow, 0));
            Int32 WFId = default(Int32);

            Presenter.getWorkflowAndStepSelected(ref WFId, ref LastWFStepUsed);

            if (ArbolProcesos.Nodes.Count > 0)
            {
                SelectStepNode(LastWFStepUsed);
                if (WFTreeRefreshed != null)
                {
                    WFTreeRefreshed(LastWFStepUsed);
                }
            }
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
    }

    private void SelectStepNode(Int64 stepId)
    {
        ArbolProcesos.Nodes[0].Expand();


        foreach (TreeNode nodeMain in ArbolProcesos.Nodes[0].ChildNodes)
        {
            if (nodeMain.Value == stepId.ToString())
            {
                nodeMain.Select();
            }

            foreach (TreeNode NodeChild in nodeMain.ChildNodes)
            {
                if (NodeChild.Value == stepId.ToString())
                {
                    nodeMain.Expand();
                    NodeChild.Select();
                }
            }
        }
    }

    public void FillWF()
    {
        try
        {
            Presenter.FillWF();

            //Si el arbol no posee nodos, se dispara el evento para que no muestre el control
            if (ArbolProcesos.Nodes.Count == 0)
            {
                if (WFTreeIsEmpty != null)
                {
                    WFTreeIsEmpty();
                }
                return;
            }

            if ((ArbolProcesos.SelectedNode == null))
            {
                Refresh();
            }
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
    }

    public TreeView WFTreeView
    {
        get { return this.ArbolProcesos; }
        set { this.ArbolProcesos = value; }
    }

    //Protected Sub btncontraer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnContraer.Click
    //    Presenter.CollapseTreeView()
    //End Sub

    public System.Web.UI.WebControls.DropDownList CmbFormType
    {
        get { return this.ddltipodeform; }
        set { this.ddltipodeform = value; }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshTree();
    }

    protected void ArbolProcesos_SelectedNodeChanged(object sender, EventArgs e)
    {
        Int32 wfId = default(Int32);
        Int32 stepId = default(Int32);
        Presenter.getWorkflowAndStepSelected(ref wfId, ref stepId);
        SelectStepNode(stepId);

        if (SelectedNodeChanged != null)
        {
            SelectedNodeChanged(wfId, stepId, 0);
        }
        if (WFTreeRefreshed != null)
        {
            WFTreeRefreshed(stepId);
        }
    }
  
}