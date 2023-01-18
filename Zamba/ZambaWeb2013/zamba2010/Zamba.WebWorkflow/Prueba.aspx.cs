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
using Subgurim.Controles;
using System.IO;
using System.Collections.Generic;

public partial class Prueba : System.Web.UI.Page
{
    #region "Atributos - Constantes"
    //Maximo tamaño de adjuntos ( 5mb)
    const Int64 MAX_ATTACHS_SIZE = 5120;
    const string ATTACHSLENGHT_1 = "restan ";
    const string ATTACHSLENGHT_2 = " KB para archivos adjuntos";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (FileUploaderAJAX1.IsPosting)
        {
            attach(FileUploaderAJAX1.PostedFile);
        }
    }

    private void attach(HttpPostedFileAJAX pf)
    {
        Int64 PostedLenght = (pf.ContentLength / 1024);
        Int64 attachsLenght = GetAttachsSizeLenght();

        if ((MAX_ATTACHS_SIZE - (PostedLenght + attachsLenght)) < 0)
        {
            lblAttachsLenght.Text = "No se pudo adjuntar el archivo por ser de tamaño superior al limite";
            return;
        }

        string pathTemp = ("temp1");
        string file = string.Empty;
        string originalName = string.Empty;

        //Genera el nombre del archivo
        file = System.IO.Path.Combine(pathTemp, pf.FileName);
        originalName = System.IO.Path.GetFileName(pf.FileName);
        //Sube el archivo

        try
        {
            FileUploaderAJAX1.SaveAs(file);
            //FileUpload1.SaveAs(file);
        }
        catch (System.IO.IOException ex)
        {
            throw ex;
        }

        string filename = pf.FileName;

        Dictionary<string, string> attachs = null;
        if (Session["Attachs"] != null)
        {
            attachs = (Dictionary<string, string>)Session["Attachs"];
            if (attachs.ContainsKey(filename) == false)
            {
                attachs.Add(filename, file);
            }
        }
        else
        {
            attachs = new Dictionary<string, string>();
            attachs.Add(filename, file);
        }
        Session["Attachs"] = attachs;

        LoadGridview();
    }

    /// <summary>
    /// Obtiene el tamaño en kilobytes de los archivos ya adjuntos
    /// </summary>
    /// <returns></returns>
    /// <history>Diego 18-09-2008 Created</history>
    private Int64 GetAttachsSizeLenght()
    {
        string filesizes;
        filesizes = lblAttachsLenght.Text.Replace(ATTACHSLENGHT_1, string.Empty);
        filesizes = filesizes.Replace(ATTACHSLENGHT_2, string.Empty);
        try
        {
            return MAX_ATTACHS_SIZE - (Int64.Parse(filesizes));
        }
        catch
        {
            LoadGridview();
            filesizes = lblAttachsLenght.Text.Replace(ATTACHSLENGHT_1, string.Empty);
            filesizes = filesizes.Replace(ATTACHSLENGHT_2, string.Empty);
            return MAX_ATTACHS_SIZE - (Int64.Parse(filesizes));
        }
    }

    /// <summary>
    /// Realiza la carga de la grilla de adjuntos
    /// </summary>
    /// <history>Diego 18-09-2008 Created</history>
    public void LoadGridview()
    {
        FileInfo file = null;
        try
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("Icono");
            DataColumn dc2 = new DataColumn("Nombre de Adjunto");
            DataColumn dc3 = new DataColumn("AttachPath");
            DataColumn dc4 = new DataColumn("Tamaño");
            Int64 AttachsSize = 0;
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];

            foreach (string filepath in attachs.Keys)
            {
                if (File.Exists(attachs[filepath]))
                {
                    file = new FileInfo(attachs[filepath]);
                    //dt.Rows.Add(Results_Business.GetFileIcon(attachs[filepath]), filepath, attachs[filepath], (file.Length / 1024) + "KB");
                    AttachsSize += (file.Length / 1024);
                    GC.Collect();
                    GC.Collect();
                }
            }

            if (dt.Rows.Count == 0)
            {
                gvAttachs.DataSource = null;
                gvAttachs.DataBind();
                lblAttachsLenght.Text = ATTACHSLENGHT_1 + MAX_ATTACHS_SIZE + ATTACHSLENGHT_2;
                return;
            }

            gvAttachs.ShowHeader = false;
            gvAttachs.Columns.Clear();

            ImageField Imagen_doc = new ImageField();
            Imagen_doc.ShowHeader = true;
            Imagen_doc.HeaderText = "Icono";
            Imagen_doc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            Imagen_doc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            Imagen_doc.DataImageUrlField = "Icono";
            Imagen_doc.DataImageUrlFormatString = "../../icono.aspx?id={0}";
            this.gvAttachs.Columns.Add(Imagen_doc);

            ButtonField btnDelete = new ButtonField();
            btnDelete.HeaderText = "Eliminar";
            btnDelete.ShowHeader = true;
            btnDelete.DataTextField = "Nombre de Adjunto";
            btnDelete.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            btnDelete.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            btnDelete.CommandName = "Select";
            this.gvAttachs.Columns.Add(btnDelete);

            BoundField f = new BoundField();
            f.DataField = "AttachPath";
            f.ShowHeader = true;
            f.HeaderText = "AttachPath";
            f.Visible = false;
            this.gvAttachs.Columns.Add(f);
            this.gvAttachs.DataKeyNames = new string[] { "Nombre de Adjunto" };

            BoundField g = new BoundField();
            g.DataField = "Tamaño";
            g.ShowHeader = true;
            g.HeaderText = "Tamaño";
            this.gvAttachs.Columns.Add(g);
            lblAttachsLenght.Text = ATTACHSLENGHT_1 + (MAX_ATTACHS_SIZE - AttachsSize).ToString() + ATTACHSLENGHT_2;
            this.gvAttachs.DataSource = dt;
            this.gvAttachs.DataBind();

        }
        catch (Exception ex)
        {
            //ZClass.raiseerror(ex);
        }
        finally
        {
            file = null;
        }
    }

    /// <summary>
    /// Evento que se ejecuta al hacer click sobre el botón "Enviar"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]    11/02/2009   Created    
    /// </history>
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        //if ((!String.IsNullOrEmpty(txtSubject.Text)) && (!String.IsNullOrEmpty(txtBody.Text)) && (!String.IsNullOrEmpty(txtEmailTo.Text)))
        //{
        //    List<string> attachs = GetAttachs();

        //    if (MessagesBussines.SendMail(txtEmailTo.Text, string.Empty, string.Empty, txtSubject.Text, txtBody.Text, false, attachs))
        //        lblErrors.Text = "El mail se envio con éxito";
        //    else
        //        lblErrors.Text = "Error al enviar mail, verifique los datos de conexion";

        //    txtSubject.Text = null;
        //    txtBody.Text = null;
        //    txtEmailTo.Text = null;
        //}
        //else
        //{
        //    if ((String.IsNullOrEmpty(txtSubject.Text)) || (String.IsNullOrEmpty(txtBody.Text)))
        //        lblErrors.Text = "El asunto y el cuerpo no pueden estar vacíos";
        //    else
        //        lblErrors.Text = "El email a enviar no puede estar vacío";
        //}

        //lblErrors.Visible = true;
    }

    /// <summary>
    /// evento click del boton de cancelar, vacia la session y oculta gran parte del control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    protected void btnCancelMail_Click(object sender, EventArgs e)
    {
        //Session["Attachs"] = null;
        //DivPrincipal.Visible = false;
        //lblErrors.Text = "Envio de mail cancelado";
        //lblErrors.Visible = true;
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        if (this.MultiView1.ActiveViewIndex == 0)
        {
            this.MultiView1.SetActiveView(this.View2);
            this.UpdPnlMultiView.Update();
        }
        else
        {
            this.MultiView1.SetActiveView(this.View1);
            this.UpdPnlMultiView.Update();
        }
    }
}
