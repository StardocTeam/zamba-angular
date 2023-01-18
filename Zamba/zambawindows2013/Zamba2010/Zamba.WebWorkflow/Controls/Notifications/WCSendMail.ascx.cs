using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Core;
using System.Collections.Generic;
using System.IO;
public partial class Controls_WCSendMail : System.Web.UI.UserControl
{
    #region "Atributos - Constantes"
    //Maximo tamaño de adjuntos ( 5mb)
    const Int64 MAX_ATTACHS_SIZE = 5120;
    const string ATTACHSLENGHT_1 = "restan ";
    const string ATTACHSLENGHT_2 = " KB para archivos adjuntos";
    #endregion
    #region Eventos
    /// <summary>
    /// Evento load de la pagina, se recupera el mail y nombre de usuario del usuario actual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtCurrentUserMail.Text = UserBusiness.CurrentUser().eMail.Mail;
            txtCurrentUserName.Text = UserBusiness.CurrentUser().Name;
        }
    }
    /// <summary>
    /// Evento click para envio de mail, se enviara el mail y en caso satisfactorio se eliminan los archivos temporales
    /// y se oculta gran parte del control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        List<string> attachs = GetAttachs();

        if (MessagesBussines.SendMail(txtEmailTo.Text, string.Empty, string.Empty, txtAsunto.Text, txtMessageBody.Text, true, chkAddLink.Checked, (Result)Session["DocSelTB"], attachs))
        {
            DivPrincipal.Visible = false;
            lblErrors.Text = "El mail se envio con exito";
        }
        else
        {
            DivPrincipal.Visible = false;
            lblErrors.Text = "Error al enviar mail, verifique los datos de conexion";
        }

        lblErrors.Visible = true;
    }
    /// <summary>
    /// evento click del boton de cancelar, vacia la session y oculta gran parte del control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    protected void btnCancelMail_Click(object sender, EventArgs e)
    {
        Session["Attachs"] = null;
        DivPrincipal.Visible = false;
        lblErrors.Text = "Envio de mail cancelado";
        lblErrors.Visible = true;
    }
    /// <summary>
    /// Evento click del boton de Agregar adjunto,se genera una copia al servidor del archivo cliente, 
    /// se suma a la coleccion y se llama al metodo que carga la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    protected void btnAddAttach_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile == false)
            return;

        Int64 PostedLenght = (FileUpload1.PostedFile.ContentLength / 1024);
        Int64 attachsLenght = GetAttachsSizeLenght();
        if ((MAX_ATTACHS_SIZE - (PostedLenght + attachsLenght)) < 0)
        {
            lblAttachsLenght.Text = "No se pudo adjuntar el archivo por ser de tamaño superior al limite";
            return;
        }
        string pathTemp = Server.MapPath("~/temp");
        string file = string.Empty;
        string originalName = string.Empty;
        //Genera el nombre del archivo
        file = System.IO.Path.Combine(pathTemp, FileUpload1.FileName);
        originalName = System.IO.Path.GetFileName(FileUpload1.FileName);
        //Sube el archivo
        try
        {
            FileUpload1.SaveAs(file);
        }
        catch (System.IO.IOException ex)
        {
            throw ex;
        }


        string filename = FileUpload1.FileName;

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
    /// Evento de seleccion en grilla de adjuntos, se utiliza para eliminar un adjunto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>Diego 18-09-2008 Created</history>
    protected void gvAttachs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (null == Session["Attachs"])
            return;

        string filename = ((GridView)(sender)).DataKeys[e.NewSelectedIndex].Value.ToString();
        Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];
        attachs.Remove(filename);
        LoadGridview();

    }    
    #endregion
    #region Metodos
    /// <summary>
    /// Limpia los adjuntos de la grilla y de session
    /// </summary>
    /// <history>Diego 18-09-2008 Created</history>
    public void ClearAttachments()
    {
        Session["Attachs"] = null;
        gvAttachs.DataSource = null;
        gvAttachs.DataBind();
    }

    /// <summary>
    /// Agrega un adjunto en la grilla, utilizado para inicializar el control con el documento que lo llama
    /// </summary>
    /// <param name="filename">Nombre de archivo, con extension</param>
    /// <param name="fullpath">ruta para localizar el archivo en disco</param>
    /// <history>Diego 18-09-2008 Created</history>
    public void AddAttach(string filename, string fullpath)
    {
        if (File.Exists(fullpath) == false)
            return;

        string pathTemp = Server.MapPath("~/temp");
        string file = System.IO.Path.Combine(pathTemp, filename);

        //[sebastian 19-02-2009] se comento el código porque la copia que se realizaba en esta parte
        //pisaba la copia modificada en el la generacion del formulario para enviarlo por mail. y esa 
        //copia que se genera para enviar por mail es la que tiene los valores cargados y los que corresponden.
        //sino de la forma anterior siempre adjuntaba un formulario vacio.
        //try
        //{
        //    if (File.Exists(file))
        //        File.Delete(file);

        //    File.Copy(fullpath, file);
        //}
        //catch (Exception ex)
        //{ 
        
        //}


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
                    dt.Rows.Add(Results_Business.GetFileIcon(attachs[filepath]), filepath, attachs[filepath], (file.Length / 1024) + "KB");
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
            ZClass.raiseerror(ex);
        }
        finally
        {
            file = null;
        }
    }
    /// <summary>
    /// retorna una lista con los adjuntos, utilizado para el envio de mail
    /// </summary>
    /// <returns></returns>
    /// <history>Diego 18-09-2008 Created</history>
    private List<string> GetAttachs()
    {
        Dictionary<string, string> attachs = (Dictionary<string, string>)Session["Attachs"];
        if (attachs == null)
            return null;

        List<string> fullpaths = new List<string>();
        fullpaths.AddRange(attachs.Values);
        return fullpaths;

    }
    /// <summary>
    /// Inicializa el control
    /// </summary>
    /// <history>Diego 18-09-2008 Created</history>
    public void SetInitialState()
    {
        txtAsunto.Text = "Mail enviado desde Zamba.Web";
        txtEmailTo.Text = string.Empty;
        txtMessageBody.Text = string.Empty;
        DivPrincipal.Visible = true;
        lblErrors.Visible = false;
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
    #endregion

    
}

