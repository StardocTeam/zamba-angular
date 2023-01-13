using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Web;
using Presenters;
using System.Web.Services;
using System.Web.Script.Services;
using Zamba;

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

    UserPreferences UP = new UserPreferences();

    private Presenters.Arbol _presenter;

    public Presenters.Arbol Presenter
    {
        get
        {
            if (_presenter != null)
                return _presenter;
            else
            {
                _presenter = new Presenters.Arbol( this, Zamba.Membership.MembershipHelper.CurrentUser);
                return _presenter;
            }
        }

        set
        {
            _presenter = value;
        }
    }




    public Arbol()
    {
        Load += Page_Load;
    }

    bool loading = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                RightsBusiness RiB = new RightsBusiness();
                loading = true;
                btnInsertar.Visible = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.InsertWeb, RightsType.View);
                if (btnInsertar.Visible)
                    Presenter.FillCmbFormTypes();
            }

            if (Page.IsPostBack) FillWF();
            //string script = "$(document).ready(function() { FixWfTree(); AddStepCountHandler(); });";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WfTreeScripts", script, true);
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
    }

    public void Refresh()
    {
        UserPreferences UP = new UserPreferences();
        try
        {

            string lastWFStep = UP.getValue("UltimoWFStepUtilizado", UPSections.WorkFlow, 0);
            int LastWFStepUsed = lastWFStep != string.Empty ? int.Parse(lastWFStep) : 0;
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
                            SelectedNodeChanged?.Invoke(WFId, LastWFStepUsed, 0);
                            break; // TODO: might not be correct. Was : Exit For
                        }

                        foreach (TreeNode NodeChild in nodeMain.ChildNodes)
                        {
                            if (NodeChild.Value == LastWFStepUsed.ToString())
                            {
                                nodeMain.Expand();
                                NodeChild.Select();
                                SelectedNodeChanged?.Invoke(WFId, LastWFStepUsed, 0);
                                nodeselected = true;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        if (nodeselected)
                            break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            else
            {
                Presenter.getWorkflowAndStepSelected(ref WFId, ref stepID);
                if (SelectedNodeChanged != null)
                {
                    SelectedNodeChanged(WFId, stepID, 0);
                    StepId.Value = stepID.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
        finally
        {
            UP = null;
        }
    }

    private void RefreshTree()
    {

        try
        {
            Session["RefreshGrid"] = true;
            Presenter.FillWF();
            int LastWFStepUsed = int.Parse(UP.getValue("UltimoWFStepUtilizado", UPSections.WorkFlow, 0));
            Int32 WFId = default(Int32);
            Presenter.getWorkflowAndStepSelected(ref WFId, ref LastWFStepUsed);
            if (ArbolProcesos.Nodes.Count > 0)
            {
                SelectStepNode(LastWFStepUsed);
                WFTreeRefreshed?.Invoke(LastWFStepUsed);
            }
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
    }

    private long SelectStepNode(Int64 stepId)
    {
        ArbolProcesos.Nodes[0].Expand();
        StepId.Value = stepId.ToString();
        foreach (TreeNode nodeMain in ArbolProcesos.Nodes[0].ChildNodes)
        {
            foreach (TreeNode NodeChild in nodeMain.ChildNodes)
            {
                if (NodeChild.Value == stepId.ToString())
                {
                    nodeMain.Expand();
                    NodeChild.Select();
                }
            }
        }
        return stepId;
    }

    public void FillWF()
    {
        try
        {
            Presenter.FillWF();
            //Si el arbol no posee nodos, se dispara el evento para que no muestre el control
            if (ArbolProcesos.Nodes.Count == 0)
            {
                WFTreeIsEmpty?.Invoke();
                return;
            }
            else if ((ArbolProcesos.SelectedNode == null))
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
        get { return ArbolProcesos; }
        set { ArbolProcesos = value; }
    }

    public DropDownList CmbFormType
    {
        get { return ddltipodeform; }
        set { ddltipodeform = value; }
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
        StepId.Value = null;
        StepId.Value = SelectStepNode(stepId).ToString();
        //Session["StepLive"] = StepId.Value;

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