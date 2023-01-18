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

public partial class Controls_Forum_WCForum : System.Web.UI.UserControl
{
    public delegate void NewSubjectClick(Int64? docId);
    private NewSubjectClick subjectClick = null;

    public event NewSubjectClick NewSubject
    {
        add
        {
            this.subjectClick += value;
        }
        remove
        {
            this.subjectClick -= value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tvwMensajesForo.Visible == false)
            tvwMensajesForo.Visible = true;
        txtMensaje.Text = "";
         if (IsPostBack)
            //ucNuevoMensaje.OnAdd += new Added(ucNuevoMensaje_OnAdd);

         if (null == tvwMensajesForo.SelectedNode && tvwMensajesForo.Nodes.Count > 0)
         tvwMensajesForo.Nodes[0].Selected = true;

        // [Gaston] 16/02/2009  Se comento el código porque si se presiona sobre el botón Nuevo Tema aparece la ventana de responder tema
         //if (tvwMensajesForo.SelectedNode != null)
         //{
         //    pnlPopUpModal.Visible = true;
         //    txtMensaje.Text = tvwMensajesForo.SelectedNode.ToolTip;
         //}
    }

    public void ucNuevoMensaje_OnAdd()
    {
        Int64 docId;
        Int64.TryParse(hdnResultId2.Value, out docId);
        MostrarMensajesForo(docId);
    }

    /*  void ucAgregarMensaje_OnAdd()
    {
        hdnNuevoMensaje.Value = hdnNuevoMensaje.Value;
     
    }*/
 
    public void MostrarMensajesForo(Int64 docId)
    {
        ArrayList ArrayMensajes = new ArrayList();
        ArrayList ArrayRespuestas = new ArrayList();
        Zamba.Core.ZForoBusiness.GetAllMessages(docId ,ArrayMensajes,ArrayRespuestas);
        hdnResultId2.Value = docId.ToString();
        //ucNuevoMensaje.setResultId(Int64.Parse(hdnResultId2.Value));
        CargarTreeViewWebForo(ArrayMensajes, ArrayRespuestas);

        if (docId != 0)
            btnNuevoTema.Enabled = true;
        else
            btnNuevoTema.Enabled = false;
    }

    protected void CargarTreeViewWebForo(ArrayList ArrayMensajes, ArrayList ArrayRespuestas)
    {   
        tvwMensajesForo.Nodes.Clear();
        Int32 i;
        Int32 ii;

        if (ArrayMensajes.Count  != 0)
        {
            //habilito los botones de ver y responder si hay mensajes en el foro
            btnResponderForo1.Enabled = true;
            for (i = 0; i != ArrayMensajes.Count; i++)
            {
                MensajeForo Mensaje = (MensajeForo)ArrayMensajes[i];
                TreeNode ForoNode = new TreeNode(Mensaje.Mensaje);
                Int32 len = Mensaje.Mensaje.Length - 1;

                if (len > 70)
                    len = 70;
                /*se agrego lo que esta abajo para poder evitar el error que se producia al intentar cargar un
                 * mensaje sin contenido (vacio), en el arbol o querer guardar un mensaje vacio. Ya que el metodo
                 * utilizado no admite guardar mensaje vacio.
                 * 
                string msg2 = string.Empty;

                if (len > -1)
                    msg2 = Mensaje.Mensaje.Substring(0, len);
                 *(sebastian)
                 */

                string msg2 = string.Empty;

                if (len > -1)
                    msg2 = Mensaje.Mensaje.Substring(0, len);

                ForoNode.Text = string.Concat(Mensaje.Name, "(" + Mensaje.Fecha.ToShortDateString(), " " + Mensaje.UserName + ")");
                //ForoNode.NodeFont = new Font(Font, FontStyle.Bold);
                //[Ezequiel] Se carga en el value si es hijo o padre y las id para luego identificarlas al momento de eliminarlos.
                ForoNode.Value = "P@" + Mensaje.ParentId.ToString() + "@" + Mensaje.ID.ToString();
                ForoNode.ToolTip = Mensaje.Mensaje;
                tvwMensajesForo.Nodes.Add(ForoNode);
                hdnIdMensaje.Value = ForoNode.Value.ToString().Split('@')[2];
                //ucNuevoMensaje.sethdnIdMensaje2(Int64.Parse(hdnIdMensaje.Value));

                ArrayList ArrayRespuestasNodo = BuscarRespuestas(ArrayRespuestas, ((MensajeForo)ArrayMensajes[i]).ID);

                for (ii = 0; ii != ArrayRespuestasNodo.Count; ii++)
                {
                    MensajeForo MensajeRes = (MensajeForo)ArrayRespuestasNodo[ii];

                    TreeNode ForoSubNode = new TreeNode(MensajeRes.Mensaje);
                    len = MensajeRes.Mensaje.Length - 1;
                    if (len > 40)
                        len = 40;

                    /*se agrego lo que esta abajo para poder evitar el error que se producia al intentar cargar un
                     * mensaje sin contenido (vacio), en el arbol o querer guardar un mensaje vacio. Ya que el metodo
                     * utilizado no admite guardar mensaje vacio.
                     * 
                        string msg = string.Empty;

                        if (len > -1)
                            msg = Mensaje.Mensaje.Substring(0, len);
                     * (sebastian)
                     */

                    string msg = string.Empty;

                    if (len > -1)
                        msg = MensajeRes.Mensaje.Substring(0, len);

                    ForoSubNode.Text = string.Concat(MensajeRes.Name, "(" + MensajeRes.Fecha.ToShortDateString(), " " + MensajeRes.UserName + ")");
                    //[Ezequiel] Se carga en el value si es hijo o padre y las id para luego identificarlas al momento de eliminarlos.
                    ForoSubNode.Value = "H@" + MensajeRes.ParentId.ToString() + "@" + MensajeRes.ID.ToString();
                    ForoSubNode.ToolTip = MensajeRes.Mensaje;
                    ForoNode.ChildNodes.Add(ForoSubNode);
                }
                ForoNode.ExpandAll();
            }

        }
        else
        {
            //deshabilito los botones de respuesta y ver del foro si no hay mensajes que mostrar y responder
            btnResponderForo1.Enabled = false;
        }
    }

    public ArrayList BuscarRespuestas(ArrayList ArrayRespuestas, Int64 IdMensaje)
    {

        ArrayList ArrayRespuestasNodo = new ArrayList();

        if (ArrayRespuestas != null)
        {
            int i;
            for (i = 0; i != ArrayRespuestas.Count; i++)
            {

                if (((MensajeForo)ArrayRespuestas[i]).ID == IdMensaje)
                    ArrayRespuestasNodo.Add(ArrayRespuestas[i]);
            }
        }

        return ArrayRespuestasNodo;
    }

    /// <summary>
    /// Evento que se ejecuta cuando se presiona el botón "Nuevo Tema"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]  16/02/2009  Modified  Llamada al evento NewSubject
    ///     [Gaston]  17/02/2009  Modified  Se agrego Session["NewSubject"]
    /// </history>
    protected void btnNuevoTema_Click(object sender, EventArgs e)
    {
        // Se coloca NewSubject en uno para evitar que se active el evento gvDocuments_SelectedIndexChanged 
        // (Consultar en WCResults - evento gvDocuments_SelectedIndexChanged)
        Session["NewSubject"] = "1";
        pnlPopUpModal.Visible = false;
        hdnNuevoMensaje.Value  = "1";
        //ucNuevoMensaje.sethdnNuevoMensaje2(Int64.Parse( hdnNuevoMensaje.Value));
        txtMensaje.Text = " ";//blanqueo la caja de texto
        //ucNuevoMensaje.subjet = " ";//blquero la caja de texto
        //StudentModal.Visible = true;
        //tvwMensajesForo.Visible = false;
        // Se ejecuta el evento NewSubject, necesario para poder mostrar la vista 2 (en Results.aspx) y escribir un nuevo tema
        subjectClick(null);
        //PopupDialog.Show();   
    }

    protected void PopUpDialog_OnUnload(object sender, EventArgs e)
    {
        //StudentModal.Visible = false;
    }

    public Button RespFor
    {
        get { return this.btnResponderForo1; }
        set { this.btnResponderForo1 = value; }
    }

    protected void btnResponderForo_Click(object sender, EventArgs e)
    {
    //    ucNuevoMensaje.NuevoMensajeTxt.Text = "";//blanqueo el text box. [sebastian]
    //    ucNuevoMensaje.subjet = "";//blanqueo el text box.[sebastian]
    //    hdnNuevoMensaje.Value = "2";        
    //    ucNuevoMensaje.sethdnNuevoMensaje2(Int64.Parse(hdnNuevoMensaje.Value));
    //    el caracter "-" se puso para poder realizar el parseo del asunto, por lo que
    //    no deberia usarse como caracter valido en el asunto.
    //    ucNuevoMensaje.subjet =  "re: " +tvwMensajesForo.SelectedNode.Text.Split('-')[0];        
    //     PopupDialog.Show();
    //    StudentModal.Visible = true;
        pnlPopUpModal.Visible = false;
    //    tvwMensajesForo.Visible = false;
        Session["NewSubject"] = "1";
    }

    protected void hdnResultId2_ValueChanged(object sender, EventArgs e)
    {
    }

    protected void tvwMensajesForo_SelectedNodeChanged(object sender, EventArgs e)
    {
        // [Gaston] 16/02/2009  Si el nodo es distinto de null entonces aparece la ventana para responder el tema
        if (tvwMensajesForo.SelectedNode != null)
        {
            pnlPopUpModal.Visible = true;
            txtMensaje.Text = tvwMensajesForo.SelectedNode.ToolTip;

            Int64 docId;
            Int64.TryParse(hdnResultId2.Value, out docId);
            IUser Cuser = UserBusiness.CurrentUser();

            this.btnEliminarForo.Enabled = UserBusiness.Rights.GetUserRights(Cuser, Zamba.Core.ObjectTypes.DocTypes, RightsType.DeleteMsgForum,Convert.ToInt32(DocTypesBusiness.GetDocTypeIdByDocId(docId)));
            if (tvwMensajesForo.SelectedNode.Value.Split('@')[0] == "P")
            {
                //ucNuevoMensaje.sethdnIdMensaje2(Convert.ToInt64(tvwMensajesForo.SelectedNode.Value.Split('@')[2]));
                btnResponderForo1.Enabled = true;
                Session["ParentIdResp"] = tvwMensajesForo.SelectedNode.Value.Split('@')[2] + "@" + tvwMensajesForo.SelectedNode.Text.Split('-')[0];
            }
            else
            {
                btnResponderForo1.Enabled = false;
                Session["ParentIdResp"] = null;
            }
        }

        //pnlPopUpModal.Visible = true;
        //txtMensaje.Text = tvwMensajesForo.SelectedNode.ToolTip ;
        //sebastian: id mensaje. en caso de que sea una respuesta la que se esta seleccionando no se toma el selectedvalue porque
        //se produciria un error al querer pasa un string como long
    }

    protected void btnCerrarPopUpModal_Click(object sender, EventArgs e)
    {
        pnlPopUpModal.Visible=false;

        if (tvwMensajesForo.SelectedNode!= null)
        tvwMensajesForo.SelectedNode.Selected = false;
    }

    protected void txtMensaje_TextChanged(object sender, EventArgs e)
    { }

    protected void btnEliminarForo_Click(object sender, EventArgs e)
    {
        if (tvwMensajesForo.SelectedNode != null)
        {
            Int64 docId;
            Int64.TryParse(hdnResultId2.Value, out docId);
            ZForoBusiness.DeleteMessage(docId, Convert.ToInt32(tvwMensajesForo.SelectedNode.Value.Split('@')[1]), Convert.ToInt32(tvwMensajesForo.SelectedNode.Value.Split('@')[2]));
            MostrarMensajesForo(docId);
            pnlPopUpModal.Visible = false;
        }
    }
}
  

