using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Core;
using System.Collections.Generic;
using System.Globalization;

public partial class Controls_Core_WCIndexs : System.Web.UI.UserControl
{

    #region Propiedades

    public ImageButton SaveChanges
    {
        get { return this.btnSaveChanges; }
        set { this.btnSaveChanges = value; }
    }

    public Table IndexsTable
    {
        get { return this.tblIndices; }
    }

    /// <summary>
    /// Gets or Sets the current Task Id
    /// </summary>
    public Int64? docId
    {
        get
        {
            Int64? NullableValue;
            Int64 Value;

            if (Int64.TryParse(hddocId.Value, out Value))
                NullableValue = Value;
            else
                NullableValue = null;

            return Value;
        }
        set
        {
            if (value.HasValue)
            {                        
              //[sebastian 03-02-2009] se le pasa a show index un array de indices para que se muestren
              //igual que en la busqueda.
              //ShowIndexsWithValues((Index[])IndexsArray.ToArray(typeof(Index)));
               ShowIndexsWithValues(value.Value);
              //LoadIndexs(value.Value);
              hddocId.Value = value.Value.ToString();
            }
            else
            {
                Clear();
                hddocId.Value = String.Empty;
            }

        }
    }
    /// <summary>
    /// Gets or Sets the current Task Id
    /// </summary>
    public Int32? DTId
    {
        get
        {
            Int32? NullableValue;
            Int32 Value;
            
            if (Int32.TryParse(hdDTId.Value, out Value))
                NullableValue = Value;
            else
                NullableValue = null;

            return Value;
        }
        set
        {
            if (value.HasValue)
            {
                LoadEsquemaIndexs(value.Value);
                hdDTId.Value = value.Value.ToString();
            }
            else
            {
                Clear();
                hdDTId.Value = String.Empty;
            }

        }
    }

    public Index[] CurrentIndexs
    {
        get
        {
            if (Session["CurrentIndexs"] != null)
                return (Index[])Session["CurrentIndexs"];
            else
                return new Index[0];
        }
        set
        {
            Session["CurrentIndexs"] = value;
        }
    }
    #endregion

    public string idButton;
    #region Eventos


    protected void gvSustitutionList_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if (ViewState["idButton"] != null)
        {
            string id = ViewState["idButton"] as string;
            TextBox tx = tblIndices.FindControl(id) as TextBox;
            tx.Text = (sender as GridView).SelectedValue.ToString();
        }
        pnlPopUpModal.Visible = false;                
    }

    protected void btnCancel_Onclick(object sender, EventArgs e)
    {
        pnlPopUpModal.Visible = false;
       // PanelCalendario.Visible = false;
    }

   
    protected void cmdPopup_Click(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        DataTable dt =  AutoSubstitutionBussines.GetIndexData(Convert.ToInt32(boton.CommandArgument), true);
        gvSustitutionList.DataSource = dt;
        gvSustitutionList.DataKeyNames = new string[] { "Codigo"};
        gvSustitutionList.DataBind();
        // Se guarda el id del botón en la variable global. De esta forma, cuando se seleccione una fila, parte del contenido de la fila tiene que 
        //ir al textbox que se creo de forma dinámica, y para identificar ese textbox es necesario primero identificar al botón que se cliqueo. Por 
        // eso, cuando se recorra la tabla tblIndices, y el SkinId del botón sea igual al SkinId del idButton, entonces el textbox que esta al lado 
        // del botón guarda el contenido de parte de la fila seleccionada
        
        //guarda el boton seleccionado en el cliente
        ViewState["idButton"] = boton.CommandArgument;
        
        pnlPopUpModal.Visible=true  ;
    }


    #endregion

    private void LoadIndexs(Int64 docId)
    {
        Visible = true;

        hddocId.Value = docId.ToString();

        List<IIndex> docIndexs = Zamba.Services.Index.GetIndexByDocId(docId);
        tblIndices.Controls.Clear();

        TableRow CurrentRow = null;
        TableCell TcIndexName = null;
        TableCell TcIndexValue = null;
        TextBox LbValue = null;
        if (docIndexs != null)
        {
            foreach (IIndex CurrentIndex in docIndexs)
            {
                TcIndexName = new TableCell();
                TcIndexName.Text = CurrentIndex.Name;
                TcIndexName.ToolTip = CurrentIndex.ID.ToString();
                TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

                LbValue = new TextBox();
                LbValue.Enabled = true;
                LbValue.Text = CurrentIndex.Data;
                LbValue.ToolTip = CurrentIndex.Data;

                
                TcIndexValue = new TableCell();
                TcIndexValue.Controls.Add(LbValue);

                CurrentRow = new TableRow();
                CurrentRow.Cells.Add(TcIndexName);
                CurrentRow.Cells.Add(TcIndexValue);

                tblIndices.Rows.Add(CurrentRow);
            }
        }

        #region Dispose
        if (null != CurrentRow)
        {
            CurrentRow.Dispose();
            CurrentRow = null;
        }

        if (null != TcIndexName)
        {
            TcIndexName.Dispose();
            TcIndexName = null;
        }
        if (null != TcIndexValue)
        {
            TcIndexValue.Dispose();
            TcIndexValue = null;
        }
        if (null != LbValue)
        {
            LbValue.Dispose();
            LbValue = null;
        }
        if (null != docIndexs)
        {
            docIndexs.Clear();
            docIndexs = null;
        }
        //habilito el boton guardar valores de los indices, en caso de modificarlos.
        //[sebastian 02-02-2009]
        //if (btnSaveIndexValue.Visible == false)
        //    btnSaveIndexValue.Visible = true;

        #endregion

    }


    private void LoadEsquemaIndexs(Int32 DTId)
    {
        Visible = true;

        hdDTId.Value = DTId.ToString();

        DataTable DTIndexs = Zamba.Services.Index.getIndexByDocTypeId(DTId).Tables[0];

        tblIndices.Controls.Clear();

        TableRow CurrentRow = null;
        TableCell TcIndexName = null;
        TableCell TcIndexValue = null;
        TextBox LbValue = null;

        foreach (DataRow CurrentIndex in DTIndexs.Rows)
        {
            TcIndexName = new TableCell();
            TcIndexName.Text = CurrentIndex["Name"].ToString();
            TcIndexName.ToolTip = CurrentIndex["ID"].ToString();
            TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

            LbValue = new TextBox();
            LbValue.Enabled = true;
            LbValue.Text = CurrentIndex["Data"].ToString();
            LbValue.ToolTip = CurrentIndex["Data"].ToString();
                      LbValue.Attributes.Add("OnClick", "IndexsChanged");
            LbValue.ID = "Index" + CurrentIndex["Index_Id"].ToString();
            TcIndexValue = new TableCell();
            TcIndexValue.Controls.Add(LbValue);

            CurrentRow = new TableRow();
            CurrentRow.Cells.Add(TcIndexName);
            CurrentRow.Cells.Add(TcIndexValue);

            tblIndices.Rows.Add(CurrentRow);
        }

        #region Dispose
        if (null != CurrentRow)
        {
            CurrentRow.Dispose();
            CurrentRow = null;
        }

        if (null != TcIndexName)
        {
            TcIndexName.Dispose();
            TcIndexName = null;
        }
        if (null != TcIndexValue)
        {
            TcIndexValue.Dispose();
            TcIndexValue = null;
        }
        if (null != LbValue)
        {
            LbValue.Dispose();
            LbValue = null;
        }
        if (null != DTIndexs)
        {
            DTIndexs.Clear();
            DTIndexs = null;
        }

        #endregion
    }


    protected void IndexsChanged(object sender, EventArgs e)
    {
        foreach (Index indice in this.CurrentIndexs)
        {
            if (indice.ID == int.Parse(((WebControl)sender).ID))
            {
                indice.Data = ((System.Web.UI.WebControls.TextBox)sender).Text;
            }
        }
    }
    /// <summary>
    /// Clears the inner controls 
    /// </summary>
    public void Clear()
    {
        tblIndices.Controls.Clear();
    }

    public void MostrarIndices(Int64 docId)
    {
        this.docId = docId;
    }
    public void MostrarEsquemaIndices(Int32 DTId)
    {
        this.DTId = DTId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Index[] GetIndexs()
    {
        Index[] indices = (Index[])Session["CurrentIndexs"];
        string dato = string.Empty;
        foreach (TableRow row in tblIndices.Rows)
        {
            foreach (Index indice in indices)
            {
                if (indice.Name.ToString() == row.Cells[0].Text)
                {
                                                           
                    dato = getControlText(row.Cells[1].Controls[0]);
                    indice.DataTemp = dato;
                    indice.Data = dato;

                    break;
                }
            }
        }
        return indices;
    }

    /// <summary>
    /// Retorna el valor de la propiedad text del control.
    /// Si el control no tiene la propiedad text retorna vacio.
    /// </summary>
    /// <param name="ctr">Control</param>
    /// <returns>valor de la propiedad text</returns>
    protected string getControlText(System.Web.UI.Control ctr)
    {
        string dato = string.Empty;

        if (ctr is DropDownList)
            dato = ((DropDownList)ctr).Text;
        else if (ctr is TextBox)
            dato = ((TextBox)ctr).Text;

         
        return dato;
    }
    
    /// <summary>
    ///  Crea una grilla a partir de una 
    ///  lista de indices
    /// </summary>    
    /// <param name="Indexs">Lista de indices</param>    
    /// <history>
    ///     [Ezequiel]  20/01/2009  Modified
    ///     [Ezequiel]  17/02/2009  Modified - Se modifico para visualizar operadores de busqueda.
    ///     [Tomas]     25/03/2009  Modified    Se agrega al final del método un Focus al botón
    ///                                         de búsqueda para que al presionar la tecla Enter
    ///                                         me realize la búsqueda automáticamente.
    /// </history>
    public void ShowIndexs(Index[] Indexs)
    {

        if (Page.IsPostBack || Session["ShowIndexOfInsert"] != null)
                this.CurrentIndexs = Indexs;
            tblIndices.Controls.Clear();

            if ((this.CurrentIndexs.Length == 0 && ((System.Collections.Generic.List<int>)Session["SelectedsDocTypesIds"]).Count == 0 && !Page.Request.Url.OriginalString.Contains("/Insert/Insert.aspx")) || (Page.Request.Url.OriginalString.Contains("/Insert/Insert.aspx") && Session["ShowIndexOfInsert"] == null))
            {
                this.lblSelectIndex.Visible = true;
            }
            else
            {
                this.lblSelectIndex.Visible = false;
                TableRow CurrentRow = null;
                TableCell TcIndexName = null;
                TableCell TcIndexValue = null;
                TableCell TcCmdPopup = null;
                Label LbName = null;
                TextBox TbValue = null;
                Button BtPopup = null;
                DropDownList cbValue = null;
                AjaxControlToolkit.CalendarExtender Calendario = null;
                
                DropDownList cbFilters = null;
                Table TbNewIndex = null;
                TableRow TrNewIndex = null;
                TableCell TcNewIndex = null;
                UpdatePanel UpNewIndex = null;
                TableCell TcFilterSearch = null;

                int lenind = 0;
                foreach (IIndex CurrentIndex in (Page.IsPostBack ? Indexs : this.CurrentIndexs))
                {
                    if (CurrentIndex.Name.Length > lenind)
                        lenind = CurrentIndex.Name.Length;
                }

                lenind *= 6;

                foreach (IIndex CurrentIndex in (Page.IsPostBack ? Indexs : this.CurrentIndexs))
                {
                    if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))
                    {
                        TrNewIndex = new TableRow();
                        TcNewIndex = new TableCell();
                        TbNewIndex = new Table();
                        TrNewIndex.Cells.Add(TcNewIndex);
                    }
                    //Aca me quede ---------Emiliano------------------------------
                    TcIndexName = new TableCell();
                    LbName = new Label();
                    LbName.Text = CurrentIndex.Name;
                    LbName.Width = lenind;
                    LbName.ID = CurrentIndex.ID.ToString() + "L";
                    LbName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                    TcIndexName.Controls.Add(LbName);

                    if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))
                    {
                        TcFilterSearch = new TableCell();
                        cbFilters = new DropDownList();
                        cbFilters.Width = 80;
                        if (CurrentIndex.Type.ToString().Contains("Alfanumerico"))
                        {
                            cbFilters.Items.Add("=");
                            cbFilters.Items.Add("Empieza");
                            cbFilters.Items.Add("Termina");
                            cbFilters.Items.Add("Contiene");
                            cbFilters.Items.Add("Distinto");
                            cbFilters.Items.Add("Alguno");
                            cbFilters.Items.Add("Es nulo");
                        }
                        else
                        {
                            cbFilters.Items.Add("=");
                            cbFilters.Items.Add(">");
                            cbFilters.Items.Add(">=");
                            cbFilters.Items.Add("<");
                            cbFilters.Items.Add("<=");
                            cbFilters.Items.Add("<>");
                            cbFilters.Items.Add("Entre");
                            cbFilters.Items.Add("Es nulo");

                        }
                        cbFilters.SelectedIndexChanged += new EventHandler(this.SetFilterSearch);
                        cbFilters.ID = "SF" + CurrentIndex.ID.ToString();
                        cbFilters.AutoPostBack = true;
                        TcFilterSearch.Controls.Add(cbFilters);
                    }

                    if (CurrentIndex.Type == IndexDataType.Fecha)
                    {
                        TbValue = new TextBox();
                        TbValue.Text = CurrentIndex.Data;
                        TbValue.Width = 150;
                        TbValue.ToolTip = CurrentIndex.Data;
                        TbValue.ID = CurrentIndex.ID.ToString();
          
                        Calendario = new AjaxControlToolkit.CalendarExtender();
                        Calendario.Format = GetDateFormat();
                        Calendario.TargetControlID = TbValue.ID;

                        TcIndexValue = new TableCell();
                        TcIndexValue.Controls.Add(TbValue);
                        TcIndexValue.Controls.Add(Calendario);

                        CurrentRow = new TableRow();
                        CurrentRow.Cells.Add(TcIndexName);
                        if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))
                            CurrentRow.Cells.Add(TcFilterSearch);
                        CurrentRow.Cells.Add(TcIndexValue);
                        CurrentRow.ToolTip = "Haga click para insertar la fecha";

                        if (!CurrentIndex.Type.ToString().Contains("Alfanumerico"))
                        {

                            TbValue = new TextBox();
                            TbValue.Text = CurrentIndex.Data;
                            TbValue.Width = 150;
                            TbValue.ToolTip = CurrentIndex.Data;
                            TbValue.ID = CurrentIndex.ID.ToString() + "ID2";

                            Calendario = new AjaxControlToolkit.CalendarExtender();
                            Calendario.Format = GetDateFormat();
                            Calendario.TargetControlID = TbValue.ID;
                            
                            TcIndexValue = new TableCell();
                            TcIndexValue.Visible = false;
                            TcIndexValue.Controls.Add(TbValue);
                            TcIndexValue.Controls.Add(Calendario);
                            CurrentRow.Cells.Add(TcIndexValue);
                        }
                    }

                    else
                    {
                        switch (CurrentIndex.DropDown)
                        {
                            case IndexAdditionalType.AutoSustitución:

                                TbValue = new TextBox();
                                TbValue.Text = CurrentIndex.Data;
                                TbValue.Width = 150;
                                TbValue.ToolTip = CurrentIndex.Data;
                                TbValue.ID = CurrentIndex.ID.ToString();


                                BtPopup = new Button();
                                BtPopup.Enabled = true;
                                BtPopup.Text = "...";
                                BtPopup.CommandArgument = CurrentIndex.ID.ToString();
                                BtPopup.Click += new EventHandler(cmdPopup_Click);

                                TcIndexValue = new TableCell();
                                TcIndexValue.Controls.Add(TbValue);

                                TcCmdPopup = new TableCell();
                                TcCmdPopup.Controls.Add(BtPopup);

                                CurrentRow = new TableRow();
                                CurrentRow.Cells.Add(TcIndexName);
                                if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))                                
                                    CurrentRow.Cells.Add(TcFilterSearch);
                                CurrentRow.Cells.Add(TcIndexValue);
                                CurrentRow.Cells.Add(TcCmdPopup);


                                break;
                            case IndexAdditionalType.DropDown:

                                cbValue = new DropDownList();
                                cbValue.Enabled = true;
                                cbValue.AutoPostBack = false;
                                cbValue.Items.Add("");
                                foreach (object item in (IndexsBussines.retrieveArraylist((Int32)CurrentIndex.ID)))
                                {
                                    cbValue.Items.Add(new ListItem(item.ToString(), item.ToString()));
                                }
                                cbValue.ToolTip = CurrentIndex.Data;
                                cbValue.ID = CurrentIndex.ID.ToString();
                                cbValue.Width = 156;
                                cbValue.EnableViewState = true;


                                TcIndexValue = new TableCell();
                                TcIndexValue.Controls.Add(cbValue);



                                CurrentRow = new TableRow();
                                CurrentRow.Cells.Add(TcIndexName);
                                if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))
                                    CurrentRow.Cells.Add(TcFilterSearch);
                                CurrentRow.Cells.Add(TcIndexValue);

                                break;
                            case IndexAdditionalType.LineText:

                                //TbValue = new TextBox();
                                //TbValue.Enabled = true;
                                //TbValue.Text = CurrentIndex.Data;
                                //TbValue.ToolTip = CurrentIndex.Data;
                                //TbValue.Width = 150;
                                TbValue = new TextBox();
                                TbValue.Text = CurrentIndex.Data;
                                TbValue.Width = 150;
                                TbValue.ToolTip = CurrentIndex.Data;
                                TbValue.ID = CurrentIndex.ID.ToString();

                                TcIndexValue = new TableCell();
                                TcIndexValue.Controls.Add(TbValue);

                                CurrentRow = new TableRow();
                                CurrentRow.Cells.Add(TcIndexName);
                                if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))
                                    CurrentRow.Cells.Add(TcFilterSearch);
                                CurrentRow.Cells.Add(TcIndexValue);

                                if (!CurrentIndex.Type.ToString().Contains("Alfanumerico"))
                                {

                                    TbValue = new TextBox();
                                    TbValue.Text = CurrentIndex.Data;
                                    TbValue.Width = 150;
                                    TbValue.ToolTip = CurrentIndex.Data;
                                    TbValue.ID = CurrentIndex.ID.ToString() + "ID2";

                                    TcIndexValue = new TableCell();
                                    TcIndexValue.Controls.Add(TbValue);
                                    TcIndexValue.Visible = false;
                                    CurrentRow.Cells.Add(TcIndexValue);

                                }

                                break;
                            default:
                                break;
                        }
                    }

                    if (Request.Url.ToString().Contains("WebClient/Search/Search.aspx"))
                    {
                        TbNewIndex.Rows.Add(CurrentRow);
                        UpNewIndex = new UpdatePanel();
                        UpNewIndex.EnableViewState = true;
                        UpNewIndex.ID = CurrentIndex.ID.ToString() + "UP";
                        UpNewIndex.UpdateMode = UpdatePanelUpdateMode.Conditional;
                        TbNewIndex.ID = CurrentIndex.ID.ToString() + "TB";
                        UpNewIndex.ContentTemplateContainer.Controls.Add(TbNewIndex);
                        TcNewIndex.Controls.Add(UpNewIndex);
                        tblIndices.Rows.Add(TrNewIndex);
                    }
                    else
                        tblIndices.Rows.Add(CurrentRow);
                    
                    //---------------------------------------
                    //TcIndexName = new TableCell();
                    //TcIndexName.Text = CurrentIndex.Name;
                    //TcIndexName.ToolTip = CurrentIndex.ID.ToString();
                    //TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

                    //LbValue = new TextBox();
                    //LbValue.Enabled = true;
                    //LbValue.Text = CurrentIndex.Data;
                    //LbValue.ToolTip = CurrentIndex.Data;

                    //LbValue.Attributes.Add("OnTextChanged", "IndexsChanged");
                    //LbValue.ID = "Index" + CurrentIndex.ID.ToString();

                    //TcIndexValue = new TableCell();
                    //TcIndexValue.Controls.Add(LbValue);

                    //CurrentRow = new TableRow();
                    //CurrentRow.Cells.Add(TcIndexName);
                    //CurrentRow.Cells.Add(TcIndexValue);

                    //tblIndices.Rows.Add(CurrentRow);


                }

                #region Dispose
                if (null != CurrentRow)
                {
                    CurrentRow.Dispose();
                    CurrentRow = null;
                }

                if (null != TcIndexName)
                {
                    TcIndexName.Dispose();
                    TcIndexName = null;
                }
                if (null != TcIndexValue)
                {
                    TcIndexValue.Dispose();
                    TcIndexValue = null;
                }
                if (null != TbValue)
                {
                    TbValue.Dispose();
                    TbValue = null;
                }
                if (null != Indexs)
                {
                    Indexs = null;
                }

                #endregion
            }
            btnSaveChanges.Focus();
       
    }

    /// <summary>
    /// Gets the date format depending on the user´s culture.
    /// </summary>
    /// <returns></returns>
    /// <history>
    ///     [Tomas]     25/03/09    Created
    /// </history>
    private string GetDateFormat()
    {
        return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern.ToString();
    }


    public void ShowIndexsWithValues(Int64 docid)
    {
        List<IIndex> IndexsList = Zamba.Services.Index.GetIndexByDocId(docid);
        Index[] Indexs = (Index[])ArrayList.Adapter(IndexsList).ToArray(typeof(Index));
        //if (Page.IsPostBack)
        //    this.CurrentIndexs = Indexs;
          tblIndices.Controls.Clear();

        //if (this.CurrentIndexs.Length == 0 && ((System.Collections.Generic.List<int>)Session["SelectedsDocTypesIds"]).Count == 0)
        //{
        //    this.lblSelectIndex.Visible = true;
        //}
        //else
        //{
            this.lblSelectIndex.Visible = false;

            TableRow CurrentRow = null;
            TableCell TcIndexName = null;
            TableCell TcIndexValue = null;
            TableCell TcCmdPopup = null;
            TextBox TbValue = null;
            Button BtPopup = null;
            DropDownList cbValue = null;
            AjaxControlToolkit.CalendarExtender Calendario = null;

            foreach (Index CurrentIndex in Indexs)
            {

                //Aca me quede ---------Emiliano------------------------------
                TcIndexName = new TableCell();
                TcIndexName.Text = CurrentIndex.Name;
                TcIndexName.ToolTip = CurrentIndex.ID.ToString();
                TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

                if (CurrentIndex.Type == IndexDataType.Fecha)
                {
                    TbValue = new TextBox();
                    TbValue.Text = CurrentIndex.Data;
                    TbValue.Width = 150;
                    TbValue.ToolTip = CurrentIndex.Data;
                    TbValue.ID = CurrentIndex.ID.ToString();

                    Calendario = new AjaxControlToolkit.CalendarExtender();
                    //[Tomas]     25/03/2009  Modified    Se agrega al final del método un Focus al botón
                    //                                    de búsqueda para que al presionar la tecla Enter
                    //                                    me realize la búsqueda automáticamente.
                    Calendario.Format = GetDateFormat();
                    Calendario.TargetControlID = TbValue.ID;

                    TcIndexValue = new TableCell();
                    TcIndexValue.Controls.Add(TbValue);
                    TcIndexValue.Controls.Add(Calendario);

                    CurrentRow = new TableRow();
                    if ( Results_Business.GetResult(docid,DocTypesBusiness.GetDocTypeIdByDocId(docid)).isShared  )
                    {
                        TbValue.Enabled = false;
                        Calendario.Enabled = false;
                    }
                    CurrentRow.Cells.Add(TcIndexName);
                    CurrentRow.Cells.Add(TcIndexValue);
                    CurrentRow.ToolTip = "Haga click para insertar la fecha";

                }

                else
                {
                    switch (CurrentIndex.DropDown)
                    {
                        case IndexAdditionalType.AutoSustitución:

                            TbValue = new TextBox();
                            TbValue.Text = CurrentIndex.Data;
                            TbValue.Width = 150;
                            TbValue.ToolTip = CurrentIndex.Data;
                            TbValue.ID = CurrentIndex.ID.ToString();


                            BtPopup = new Button();
                            BtPopup.Enabled = true;
                            BtPopup.Text = "...";
                            BtPopup.CommandArgument = CurrentIndex.ID.ToString();
                            BtPopup.Click += new EventHandler(cmdPopup_Click);

                            TcIndexValue = new TableCell();
                            TcIndexValue.Controls.Add(TbValue);

                            TcCmdPopup = new TableCell();
                            TcCmdPopup.Controls.Add(BtPopup);

                            if (Results_Business.GetResult(docid, DocTypesBusiness.GetDocTypeIdByDocId(docid)).isShared)
                            {
                                TbValue.Enabled = false;
                                BtPopup.Enabled = false;
                            }

                            CurrentRow = new TableRow();
                            CurrentRow.Cells.Add(TcIndexName);
                            CurrentRow.Cells.Add(TcIndexValue);
                            CurrentRow.Cells.Add(TcCmdPopup);


                            break;
                        case IndexAdditionalType.DropDown:

                            cbValue = new DropDownList();
                            cbValue.Enabled = true;
                            cbValue.Items.Add("");
                            foreach (object item in (IndexsBussines.retrieveArraylist((Int32)CurrentIndex.ID)))
                            {
                                cbValue.Items.Add(new ListItem(item.ToString(), item.ToString()));
                            }
                            cbValue.ToolTip = CurrentIndex.Data;
                            cbValue.Width = 156;
                            cbValue.SelectedValue = CurrentIndex.Data;
                            cbValue.ID = CurrentIndex.ID.ToString();
                            


                            TcIndexValue = new TableCell();
                            TcIndexValue.Controls.Add(cbValue);

                            if (Results_Business.GetResult(docid, DocTypesBusiness.GetDocTypeIdByDocId(docid)).isShared)
                            {
                                cbValue.Enabled = false;
                            }

                            CurrentRow = new TableRow();
                            CurrentRow.Cells.Add(TcIndexName);
                            CurrentRow.Cells.Add(TcIndexValue);

                            break;
                        case IndexAdditionalType.LineText:

                            TbValue = new TextBox();
                            TbValue.Enabled = true;
                            TbValue.Text = CurrentIndex.Data;
                            TbValue.ToolTip = CurrentIndex.Data;
                            TbValue.Width = 150;
                            TbValue.ID = CurrentIndex.ID.ToString();

                            TcIndexValue = new TableCell();
                            TcIndexValue.Controls.Add(TbValue);
                            if (Results_Business.GetResult(docid, DocTypesBusiness.GetDocTypeIdByDocId(docid)).isShared)
                            {
                                TbValue.Enabled = false;
                            }
                            CurrentRow = new TableRow();
                            CurrentRow.Cells.Add(TcIndexName);
                            CurrentRow.Cells.Add(TcIndexValue);

                            break;
                        default:
                            break;
                    }
                }

                tblIndices.Rows.Add(CurrentRow);
        
            }

            #region Dispose
            if (null != CurrentRow)
            {
                CurrentRow.Dispose();
                CurrentRow = null;
            }

            if (null != TcIndexName)
            {
                TcIndexName.Dispose();
                TcIndexName = null;
            }
            if (null != TcIndexValue)
            {
                TcIndexValue.Dispose();
                TcIndexValue = null;
            }
            if (null != TbValue)
            {
                TbValue.Dispose();
                TbValue = null;
            }
            if (null != Indexs)
            {
                Indexs = null;
            }

            #endregion
        //}
    }

    public void ShowIndexsWithValues(Index[] Indexs)
    {
        tblIndices.Controls.Clear();
        this.lblSelectIndex.Visible = false;

        TableRow CurrentRow = null;
        TableCell TcIndexName = null;
        TableCell TcIndexValue = null;
        TableCell TcCmdPopup = null;
        TextBox TbValue = null;
        Button BtPopup = null;
        DropDownList cbValue = null;
        AjaxControlToolkit.CalendarExtender Calendario = null;

        foreach (Index CurrentIndex in Indexs)
        {

            //Aca me quede ---------Emiliano------------------------------
            TcIndexName = new TableCell();
            TcIndexName.Text = CurrentIndex.Name;
            TcIndexName.ToolTip = CurrentIndex.ID.ToString();
            TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

            if (CurrentIndex.Type == IndexDataType.Fecha)
            {
                TbValue = new TextBox();
                TbValue.Text = CurrentIndex.Data;
                TbValue.Width = 150;
                TbValue.ToolTip = CurrentIndex.Data;
                TbValue.ID = CurrentIndex.ID.ToString();

                Calendario = new AjaxControlToolkit.CalendarExtender();
                //[Tomas]     25/03/2009  Modified    Se agrega al final del método un Focus al botón
                //                                    de búsqueda para que al presionar la tecla Enter
                //                                    me realize la búsqueda automáticamente.
                Calendario.Format = GetDateFormat();
                Calendario.TargetControlID = TbValue.ID;

                TcIndexValue = new TableCell();
                TcIndexValue.Controls.Add(TbValue);
                TcIndexValue.Controls.Add(Calendario);

                CurrentRow = new TableRow();
                CurrentRow.Cells.Add(TcIndexName);
                CurrentRow.Cells.Add(TcIndexValue);
                CurrentRow.ToolTip = "Haga click para insertar la fecha";

            }

            else
            {
                switch (CurrentIndex.DropDown)
                {
                    case IndexAdditionalType.AutoSustitución:

                        TbValue = new TextBox();
                        TbValue.Text = CurrentIndex.Data;
                        TbValue.Width = 150;
                        TbValue.ToolTip = CurrentIndex.Data;
                        TbValue.ID = CurrentIndex.ID.ToString();


                        BtPopup = new Button();
                        BtPopup.Enabled = true;
                        BtPopup.Text = "...";
                        BtPopup.CommandArgument = CurrentIndex.ID.ToString();
                        BtPopup.Click += new EventHandler(cmdPopup_Click);

                        TcIndexValue = new TableCell();
                        TcIndexValue.Controls.Add(TbValue);

                        TcCmdPopup = new TableCell();
                        TcCmdPopup.Controls.Add(BtPopup);
                        

                        CurrentRow = new TableRow();
                        CurrentRow.Cells.Add(TcIndexName);
                        CurrentRow.Cells.Add(TcIndexValue);
                        CurrentRow.Cells.Add(TcCmdPopup);


                        break;
                    case IndexAdditionalType.DropDown:

                        cbValue = new DropDownList();
                        cbValue.Enabled = true;
                        cbValue.Items.Add("");
                        foreach (object item in (IndexsBussines.retrieveArraylist((Int32)CurrentIndex.ID)))
                        {
                            cbValue.Items.Add(new ListItem(item.ToString(), item.ToString()));
                        }
                        cbValue.ToolTip = CurrentIndex.Data;
                        cbValue.Width = 156;
                        cbValue.SelectedValue = CurrentIndex.Data;
                        cbValue.ID = CurrentIndex.ID.ToString();



                        TcIndexValue = new TableCell();
                        TcIndexValue.Controls.Add(cbValue);

                        CurrentRow = new TableRow();
                        CurrentRow.Cells.Add(TcIndexName);
                        CurrentRow.Cells.Add(TcIndexValue);

                        break;
                    case IndexAdditionalType.LineText:

                        TbValue = new TextBox();
                        TbValue.Enabled = true;
                        TbValue.Text = CurrentIndex.Data;
                        TbValue.ToolTip = CurrentIndex.Data;
                        TbValue.Width = 150;
                        TbValue.ID = CurrentIndex.ID.ToString();

                        TcIndexValue = new TableCell();
                        TcIndexValue.Controls.Add(TbValue);
                        CurrentRow = new TableRow();
                        CurrentRow.Cells.Add(TcIndexName);
                        CurrentRow.Cells.Add(TcIndexValue);

                        break;
                    default:
                        break;
                }
            }

            tblIndices.Rows.Add(CurrentRow);

        }

        #region Dispose
        if (null != CurrentRow)
        {
            CurrentRow.Dispose();
            CurrentRow = null;
        }

        if (null != TcIndexName)
        {
            TcIndexName.Dispose();
            TcIndexName = null;
        }
        if (null != TcIndexValue)
        {
            TcIndexValue.Dispose();
            TcIndexValue = null;
        }
        if (null != TbValue)
        {
            TbValue.Dispose();
            TbValue = null;
        }
        if (null != Indexs)
        {
            Indexs = null;
        }

        #endregion
        //}
    }

    /// <summary>
    /// Evento que se invoca al elegir un tipo de filtro en la busqueda y rediseña la tabla en base al filtro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Ezequiel 17/02/09 - Created</history>
    private void SetFilterSearch(object sender, EventArgs e)
    {
        Index[] indexlist = this.GetIndexs();
        Int64 Position;
        //Ezequiel: Valido si el filtro elegido es "Entre" asi visualizo el segundo textbox"
        if (Int64.TryParse(((DropDownList)sender).ID.Replace("SF", ""), out Position) && ((DropDownList)sender).SelectedValue.CompareTo("Entre") == 0)
        {
            for (int x = 0; x < tblIndices.Rows.Count; x++)
            {

                if (string.Compare(((DropDownList)sender).ID,((DropDownList)(((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[1].Controls[0])).ID) == 0)
                {
                    ((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[3].Visible = true;
                }
            }
        }

            //Ezequiel: Si el operador es diferente de "Entre" oculto el segundo textbox.
        else if (((DropDownList)sender).Items.FindByText("Entre") != null && !((DropDownList)sender).SelectedValue.Contains("Entre"))
        {
            for (int x = 0; x < tblIndices.Rows.Count; x++)
                if (string.Compare(((DropDownList)sender).ID, ((DropDownList)(((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[1].Controls[0])).ID) == 0)
                    ((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[3].Visible = false;
        }

        //Ezequiel: Valido si el operador es nulo asi oculto los textbox o no
        if (((DropDownList)sender).SelectedValue.CompareTo("Es nulo") == 0)
        {
            for (int x = 0; x < tblIndices.Rows.Count; x++)
                if (string.Compare(((DropDownList)sender).ID, ((DropDownList)(((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[1].Controls[0])).ID) == 0)
                {
                    ((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[2].Visible = false;
                    if (((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells.Count > 3)
                        ((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[3].Visible = false;
                }
        }
        if (((DropDownList)sender).SelectedValue.CompareTo("Es nulo") != 0)
        {
            for (int x = 0; x < tblIndices.Rows.Count; x++)
                if (string.Compare(((DropDownList)sender).ID, ((DropDownList)(((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[1].Controls[0])).ID) == 0)
                {
                    ((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[2].Visible = true;
                    if (((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells.Count > 3)
                        ((Table)(((UpdatePanel)(tblIndices.Rows[x].Cells[0].Controls[0])).ContentTemplateContainer.Controls[0])).Rows[0].Cells[3].Controls[0].Visible = true;
                }
        }

    }


    /// <summary>
    /// Evento que se invoca al elegir un tipo de filtro en la busqueda y rediseña la tabla en base al filtro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Ezequiel 17/02/09 - Created</history>
    public Index[] MakeSearchIndexsList()
    {
        Index[] indices = (Index[])Session["CurrentIndexs"];
        string dato = string.Empty;
        foreach (TableRow row in tblIndices.Rows)
        {
            foreach (Index indice in indices)
            {
                if (indice.Name.ToString() == ((Label)((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[0].Controls[0]).Text)
                {

                    dato = getControlText(((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[2].Controls[0]);
                    indice.Operator = getControlText(((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[1].Controls[0]);
                    indice.DataTemp = dato;
                    indice.Data = dato;
                    if (((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells.Count == 4 && ((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[3].Visible == true)
                    {
                        dato = getControlText(((Table)((UpdatePanel)row.Cells[0].Controls[0]).ContentTemplateContainer.Controls[0]).Rows[0].Cells[3].Controls[0]);
                        indice.DataTemp2 = dato;
                        indice.Data2 = dato;
                    }
                    break;
                }
            }
        }
        return indices;
    }
}
