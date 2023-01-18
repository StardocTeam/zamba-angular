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

public delegate void Added();
public partial class Controls_Forum_WebUserControl : System.Web.UI.UserControl
{
    //Declaro una variable del delegado
    private Added dAdd = null;
    /// <summary>
    /// Manejo del addStep
    /// </summary>
    public event Added OnAdd
    {
        add
        {
            this.dAdd += value;
        }
        remove
        {
            this.dAdd -= value;

        }
    }
    public string subjet
    {
        get { return txtAsunto.Text; }
        set {  txtAsunto.Text = value; }

    }

    public void setResultId(Int64 ResultId)
    {
        hdnResultId.Value = ResultId.ToString();
    }

    public void sethdnNuevoMensaje2(Int64  valor)
    {
        hdnNuevoMensaje2.Value = valor.ToString ();

    }

    public void sethdnIdMensaje2(Int64 IdMensaje)
    {
      hdnIdMensaje2.Value = IdMensaje.ToString();
        
    }
   

    protected void Page_Load(object sender, EventArgs e)
    {
       // txtMensaje.Text = string.Empty;
    }

    public TextBox NuevoMensajeTxt
    {
        get { return this.txtMensaje; }
        set { this.txtMensaje = value; }
    }

    protected void btnGuardarMensaje_Click(object sender, EventArgs e)
    {
        string Asunto = txtAsunto.Text;
        string Texto = txtMensaje.Text;

        if (txtMensaje.Text == null)



            txtMensaje.Text = " ";
    
        InsertarNuevoMensaje(Asunto, Texto, Int64.Parse(Session["UserId"].ToString()), Int64.Parse(hdnResultId.Value) );

        if (this.dAdd != null)
            dAdd();
    }


   

    private void InsertarNuevoMensaje(string Asunto, string Texto, Int64 IUserId, Int64 docId)
    {

            //sebastian: inserta nuevo mensaje, 1 para nuevo tema, 2 para respuesta

        if (Int32.Parse( hdnNuevoMensaje2.Value) == 1)
        {
            Int64 ResultId = docId;
            Int32 Parent = 0;
            Int32 IdMsg = ZForoBusiness.SiguienteId(ResultId);
            string nuevo_asunto = Asunto  + "-"; //se agrego ß para realizarf parseo

            //Para guardar un nuevo tema
            ZForoBusiness.InsertMessage(ResultId, 0, IdMsg, Parent, nuevo_asunto , Texto, DateTime.Today, (Int32)IUserId, 0);
            //Después de guardarse el mensaje, se actualiza el U_TIME (fecha y hora de la ùltima acción) y se registra la acción en USER_HST
            UserBusiness.Rights.SaveAction(IdMsg, ObjectTypes.Foro, RightsType.NuevoTemaGuardar, "", 0);
        }

        if (Int32.Parse( hdnNuevoMensaje2.Value) == 2)
        {
            //se agrego "-" para poder hacer un parseo del asunto para agregarlo a momento de contestar 
            //el mensaje, por lo que no deberia usarse como caracter en el asunto
          string asunto_res = Asunto + "-" ;
          Int32  IdMsg = Int32.Parse(hdnIdMensaje2.Value);
          Int32 Parent = ZForoBusiness.SiguienteParent(docId ,IdMsg);
          Int64 ResultId = docId;

          //' Para guardar un responder
          
            ZForoBusiness.InsertMessage(ResultId, 0, IdMsg, Parent, asunto_res, Texto , DateTime.Today, (Int32)IUserId, 0);
          //' Después de guardarse la respuesta, se actualiza el U_TIME (fecha y hora de la ùltima acción) y se registra la acción en USER_HST
          UserBusiness.Rights.SaveAction(IdMsg, ObjectTypes.Foro, RightsType.ResponderGuardar, "", 0);
        
        }
    }

    protected void hdnResultId_ValueChanged(object sender, EventArgs e)
    {
    }

    protected void hdnNuevoMensaje2_ValueChanged(object sender, EventArgs e)
    {
    }

    protected void hdnIdMensaje2_ValueChanged(object sender, EventArgs e)
    {
    }

    protected void txtAsunto_TextChanged(object sender, EventArgs e)
    {
    }
}
