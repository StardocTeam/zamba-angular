using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;

public partial class IndexSearchController : System.Web.UI.Page
{
    private Int32 IdDocType = 0;

    //Obtengo el valor del doctype y lleno la tabla
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["IdDocType"] != null && string.Compare(Request.QueryString["IdDocType"], string.Empty) != 0)
        {
            IdDocType = Int32.Parse(Request.QueryString["IdDocType"]);
        }
        else
            IdDocType = 533;

        createIndex();
    }


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        setComp();
    }

    #region Controles Dinamicos
    private void setLink()
    {
        if (Session["Sust"] != null)
        {
            createTable(Int32.Parse(Session["Sust"].ToString()));
            Session.Remove("Sust");
            this.CompModal.Hide();
            this.TableModal.Show();
        }
    }
    private void setComp()
    {
  //      foreach (TableRow r in this.TblIndex.Rows)
    //    {
            //Si viene del popup le agrego el texto selecionado
      //      if ((LinkButton)Session["clicked"] != null)
      //      {
      //          if (((LinkButton)r.Cells[1].Controls[0]).ID == ((LinkButton)Session["clicked"]).ID)
      //          {
      //              ((LinkButton)r.Cells[1].Controls[0]).Text = ((LinkButton)Session["clicked"]).Text;
      //          }
      //      }

      //      //Si el texto es "entre" lleva dos textbox, si es nulo ninguno
      //      string text = ((LinkButton)r.Cells[1].Controls[0]).Text;
      //      if (string.Compare(text.ToLower(), "entre") == 0)
      //      {
      //          //Si es de sustitucion
      //          if (r.Cells[2].Controls.Count > 1)
      //          {
      //              ((TextBox)r.Cells[2].Controls[0]).Width = 104;
      //              TextBox lbltext2 = new TextBox();
      //              lbltext2.Width = 115;
      //              r.Cells[2].Controls.AddAt(1, lbltext2);
      //          }
      //          else
      //          {
      //              r.Cells[2].Controls.Clear();
      //              TextBox lbltext = new TextBox();
      //              lbltext.Width = 125;
      //              r.Cells[2].Controls.Add(lbltext);
      //              TextBox lbltext2 = new TextBox();
      //              lbltext2.Width = 125;
      //              r.Cells[2].Controls.Add(lbltext2);
      //          }
      //      }
      //      else if (string.Compare(text.ToLower(), "es nulo") == 0)
      //      {
      //          r.Cells[2].Controls.Clear();
      //      }
      ////  }
    }

    private void createIndex()
    {
        //Obtengo los indices del tipo de documento

        Zamba.Core.Index[] array = Zamba.Core.Indexs_Factory.GetIndexsSchema(IdDocType);
        Int32 numRow = 0;

        foreach (Index indice in array)
        {
    //        addIndex(this.TblIndex, indice, numRow);
            numRow++;
        }
    }

    //Agrego el indice a la tabla
    private void addIndex(System.Web.UI.WebControls.Table table,
                                                Zamba.Core.Index indice, Int32 numRow)
    {
        System.Web.UI.WebControls.TableRow row = new System.Web.UI.WebControls.TableRow();
        row.EnableViewState = true;

        System.Web.UI.WebControls.TableCell cellName = new System.Web.UI.WebControls.TableCell();
        cellName.EnableViewState = true;
        cellName.HorizontalAlign = HorizontalAlign.Left;

        Label lblname = new Label();
        lblname.Text = indice.Name;
        lblname.EnableViewState = true;

        cellName.Controls.Add(lblname);

        System.Web.UI.WebControls.TableCell cellComp = new System.Web.UI.WebControls.TableCell();
        cellComp.EnableViewState = true;
        cellComp.HorizontalAlign = HorizontalAlign.Center;
        cellComp.Width = 60;

        //Comparador
        LinkButton Comp = new LinkButton();
        Comp.Click += new EventHandler(complink_click);
   //     Comp.ID = "Comp-" + TblIndex.Rows.Count.ToString();
        Comp.Text = "=";
        Comp.Font.Underline = false;

        cellComp.Controls.Add(Comp);

        System.Web.UI.WebControls.TableCell cellchoose = new System.Web.UI.WebControls.TableCell();
        cellchoose.EnableViewState = true;
        cellchoose.HorizontalAlign = HorizontalAlign.Left;

        //Si es dropdow agrego un combo y le cargo los valores
        if (indice.DropDown == IndexAdditionalType.DropDown)
        {
            DropDownList drop = new DropDownList();
            drop.Width = 254;
            cargarCombo(drop, indice);
            cellchoose.Controls.Add(drop);
        }
        //Si es autosustitucion le agrego un texto y un boton
        else if (indice.DropDown == IndexAdditionalType.AutoSustitución)
        {
            TextBox lbltext = new TextBox();
            lbltext.Width = 234;
            Button btnSus = new Button();
            btnSus.Width = 20;
            btnSus.ID = indice.ID.ToString();
            btnSus.Text = "...";
            btnSus.BackColor = System.Drawing.Color.Moccasin;
            btnSus.Click += new EventHandler(btnSus_Click);
            //Si el indice esta guardado en sesion le agrego el valor q tiene
            if (Session[indice.ID.ToString()] != null)
                lbltext.Text = Session[indice.ID.ToString()].ToString();

            cellchoose.Controls.Add(lbltext);
            cellchoose.Controls.Add(btnSus);
        }
        else
        {
            TextBox lbltext = new TextBox();
            lbltext.Width = 254;
            cellchoose.Controls.Add(lbltext);
        }

        System.Web.UI.WebControls.TableCell cellOrder = new System.Web.UI.WebControls.TableCell();
        cellOrder.EnableViewState = true;

        LinkButton abc = new LinkButton();
        abc.Click += new EventHandler(link_click);
        abc.ID = "LinkButton" + indice.ID.ToString();
        abc.Text = "Abc";
        abc.Font.Underline = false;

        cellOrder.Controls.Add(abc);

        cellName.ID = "cellName" + indice.ID.ToString();
        cellchoose.ID = "cellchoose" + indice.ID.ToString();
        cellOrder.ID = "cellOrder" + indice.ID.ToString();
        cellComp.ID = "cellComp-" + indice.ID.ToString();

        row.Cells.Add(cellName);
        row.Cells.Add(cellComp);
        row.Cells.Add(cellchoose);
        row.Cells.Add(cellOrder);
        row.ID = "row" + indice.ID.ToString();
        table.Rows.Add(row);
    }

    //Cargar el combo
    private void cargarCombo(DropDownList drop, Index indice)
    {
        ArrayList list = Zamba.Core.Indexs_Factory.GetDropDownList(Int32.Parse(indice.ID.ToString()));

        foreach (string name in list)
        {
            drop.Items.Add(name);
        }
    }

    private void createTable(Int32 IdIndex)
    {
        this.TblSus.Rows.Clear();
        DataTable dt = AutoSubstitutionBussines.GetIndexData(IdIndex, false);

        System.Web.UI.WebControls.TableRow row = new System.Web.UI.WebControls.TableRow();
        row.EnableViewState = true;

        System.Web.UI.WebControls.TableCell cellCodigo = new System.Web.UI.WebControls.TableCell();
        cellCodigo.EnableViewState = true;
        cellCodigo.HorizontalAlign = HorizontalAlign.Center;
        cellCodigo.Text = "Código";
        cellCodigo.BackColor = System.Drawing.Color.Moccasin;
        cellCodigo.Font.Bold = true;

        System.Web.UI.WebControls.TableCell cellName = new System.Web.UI.WebControls.TableCell();
        cellName.EnableViewState = true;
        cellName.HorizontalAlign = HorizontalAlign.Left;
        cellName.Text = "Descripción";
        cellName.BackColor = System.Drawing.Color.Moccasin;
        cellName.Font.Bold = true;

        row.Cells.Add(cellCodigo);
        row.Cells.Add(cellName);

        this.TblSus.Rows.Add(row);

        foreach (DataRow dr in dt.Rows)
            addItem(this.TblSus, dr);

    }

    private void addItem(System.Web.UI.WebControls.Table table,
                                                DataRow dr)
    {
        System.Web.UI.WebControls.TableRow row = new System.Web.UI.WebControls.TableRow();
        row.EnableViewState = true;

        #region codigo
        System.Web.UI.WebControls.TableCell cellCodigo = new System.Web.UI.WebControls.TableCell();
        cellCodigo.EnableViewState = true;
        cellCodigo.HorizontalAlign = HorizontalAlign.Right;

        LinkButton lnkCodigo = new LinkButton();
        lnkCodigo.Click += new EventHandler(link_click);
        lnkCodigo.ID = "Cod-" + dr[0].ToString() + "-" + dr[1].ToString();
        lnkCodigo.Text = dr[0].ToString();
        lnkCodigo.Font.Underline = false;

        cellCodigo.Width = lnkCodigo.Width;
        cellCodigo.Controls.Add(lnkCodigo);
        #endregion

        #region descripcion
        System.Web.UI.WebControls.TableCell cellName = new System.Web.UI.WebControls.TableCell();
        cellName.EnableViewState = true;
        cellName.HorizontalAlign = HorizontalAlign.Left;

        LinkButton lnkDescrip = new LinkButton();
        lnkDescrip.Click += new EventHandler(suslink_click);
        lnkDescrip.ID = dr[0].ToString() + "-" + dr[1].ToString();
        lnkDescrip.Text = dr[1].ToString();
        lnkDescrip.Font.Underline = false;

        cellName.Controls.Add(lnkDescrip);
        #endregion

        row.Cells.Add(cellCodigo);
        row.Cells.Add(cellName);
        table.Rows.Add(row);
    }
    #endregion

    #region links

    //Muestra el popup de los comparadores
    private void complink_click(object sender, EventArgs e)
    {
        Session.Add("clicked", (LinkButton)sender);
        this.TableModal.Hide();
        this.CompModal.Show();
    }
 
    //guarda el nuevo nombre en la sesion
    protected void btnComp_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)Session["clicked"];
        link.Text = ((LinkButton)sender).Text;
    }

    //Cambia el valor del ordenamiento del texto
    private void link_click(object sender, EventArgs e)
    {
        LinkButton abc = (LinkButton)sender;
        this.TableModal.Hide();
        this.CompModal.Hide();

        switch (abc.Text)
        {
            case "Abc":
                {
                    abc.Text = "A-Z";
                    break;
                }
            case "A-Z":
                {
                    abc.Text = "Z-A";
                    break;
                }
            case "Z-A":
                {
                    abc.Text = "Abc";
                    break;
                }
        }
    }

    /// <summary>
    /// Click del boton de tabla de sustitucion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnSus_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnSus = (Button)sender;
            Char mander = Char.Parse("-");
            createTable(Int32.Parse(btnSus.ID.Split(mander)[0]));
            this.TableModal.Show();

            //Session.Add("Sust", btnSus.ID.Split(mander)[0]);
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Click de la eleccion de item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void suslink_click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Char mander = Char.Parse("-");
        Session.Add(link.ID.Split(mander)[0], link.ID.Split(mander)[1]);
    }
    #endregion


}
