using Zamba.AppBlock;
using Zamba.Core;
using System.IO;
using System;
using System.Collections;
using System.Windows.Forms;
using Zamba.Viewers;
using Zamba.AppBlock;
using FormulariosDinamicos;
using Zamba.Viewers;
using Zamba;

public partial class UCForm : ZControl
{
    public UCForm(Int64 FormID)
    {

        //El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent();
        //FormBrowser = new FormBrowser();
        // AddHandler FormBrowser.RefreshIndexs, AddressOf ActualizarIndices
        //Agregar cualquier inicialización después de la llamada a InitializeComponent()
        _formID = FormID;

        LoadFormBrowser();
    }

    private Int64 _formID;

    FormBrowser FormBrowser;
    Int64 selectedIndex;

    private FormComparer _formsComparer = new FormComparer();
    #region "Load"

    private void LoadFormBrowser()
    {
        try
        {
            ZwebForm frm = FormBusiness.GetForm(_formID);
            txtpath.Text = frm.Path;
            txtname.Text = frm.Name;
            txtEntity.Text = DocTypesBusiness.GetDocTypeName(frm.DocTypeId,false);
            txtType.Text = frm.Type.ToString();
            FormBrowser = new FormBrowser();
            FormBrowser.Dock = DockStyle.Fill;
            this.Panel1.Controls.Add(FormBrowser);          
            FormBrowser.Navigate(txtpath.Text.Trim());
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }
      
    #endregion
   
    #region "Select"


    bool cancelSelectEvent;

    #endregion

    /// <summary>
    /// Convierte el valor del cbotype (Edit,Insert,Search,Show,WorkFlow)
    /// al español, tal como se muestra en el ListBox1. En caso de no 
    /// encontrar ninguno devuelve el mismo valor.
    /// </summary>
    /// <history>
    /// [Tomas 11/03/09]    Created
    /// </history>
    private string GetSpanishNameType(string nameType)
    {

        switch (nameType.ToLower())
        {
            case "edit":
                return "Editar";
            case "insert":
                return "Insertar";
            case "search":
                return "Busqueda";
            case "show":
                return "Visualizar";
            case "workflow":
                return "Workflow";
            case "WebInsert":
                return "InsertarWeb";
            case "WebSearch":
                return "BusquedaWeb";
            case "WebEdit":
                return "WebEdit";
            case "WebShow":
                return "WebShow";
            case "WebWorkFlow":
                return "WebWorkFlow";
            default:
                return nameType;
        }

    }

    /// <summary>
    /// Comparador de formularios electronicos que ordena por nombre
    /// </summary>
    /// <remarks></remarks>
    private class FormComparer : System.Collections.IComparer
    {

        private ZwebForm _first;

        private ZwebForm _second;
        public int Compare(object x, object y)
        {

            _first = (Zamba.Core.ZwebForm)x;
            _second = (Zamba.Core.ZwebForm)y;
            return (string.Compare(_first.Name, _second.Name));

        }

    }

    /// <summary>
    /// Carga una coleccion con los tipos de forms
    /// </summary>
    /// <param name="types"></param>
    /// <remarks></remarks>
    /// <history>dalbarellos 17.04.2009</history>
    private static void GetFormTypes(System.Collections.Generic.List<string> types)
    {
        types.Add(FormTypes.Search.ToString());
        types.Add(FormTypes.Edit.ToString());
        types.Add(FormTypes.Show.ToString());
        types.Add(FormTypes.WorkFlow.ToString());
        types.Add(FormTypes.Insert.ToString());
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void btncond_Click(object sender, EventArgs e)
    {
        AddConditions();
    }
    private void AddConditions()
    {
        try 
        {
            ZwebForm frm = FormBusiness.GetForm(_formID);
            var doctypeid = frm.DocTypeId;

            frmAttributeCondition frmConditions = new frmAttributeCondition(_formID, doctypeid);
                ErrorProvider1.Clear();
                DynamicFormState state = new DynamicFormState(doctypeid, true, _formID);
                FormBusiness.GetDynamicFormState(ref state);
                state.FormName = txtname.Text;
                state.DoctypeName = txtEntity.Text;


                frmAbmZfrmDesc frmAddFrmConditions = new frmAbmZfrmDesc(ref state);
                frmAddFrmConditions.Tag = "openFromBtnConditions";
                frmAddFrmConditions.ShowDialog();    

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    private void btnAttributeCondition_Click(object sender, EventArgs e)
    {
         ZwebForm frm = FormBusiness.GetForm(_formID);
        var doctypeid = frm.DocTypeId;

        frmAttributeCondition frmConditions = new frmAttributeCondition(_formID, doctypeid);
        frmConditions.ShowDialog();
       
    }

    private void btnOpenTestCases_Click(object sender, EventArgs e)
    {
        string ztcApp = ZOptBusiness.GetValue("ZTCApplication");
        string ztcPath = System.Windows.Forms.Application.StartupPath + "/" + ztcApp;
        string parameters = ObjectTypes.FormulariosElectronicos + " " + _formID + " " ;
        System.Diagnostics.Process.Start(ztcPath, parameters);
     
    }

   
   
}
