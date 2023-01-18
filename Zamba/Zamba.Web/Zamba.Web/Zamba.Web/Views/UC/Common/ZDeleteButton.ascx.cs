using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba;
using Zamba.Core;
using Zamba.Services;


public partial class Views_UC_Common_ZDeleteButton : System.Web.UI.UserControl
{
    IResult _result;
    RightsBusiness RiB = new RightsBusiness();
    Results_Business RB = new Results_Business();

    public IResult Result
    {
        get
        {
            return this._result;
        }
        set
        {
            this._result = value;
        }
    }
    


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnOk_Click(Object sender, EventArgs e)
    {
        RightsBusiness RiB = new RightsBusiness();
        Results_Business RB = new Results_Business();
        try
        {

            if (Result != null)
            {

                if (_result.HasVersion == 1 &&
                    RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.DeleteVersions, _result.DocType.ID))
                {
                    if (_result.RootDocumentId == 0)
                    {
                        //si el documento es root (no tiene padres) primero debe eliimnar sus versiones
                        //hijas y luego eliminar este - HasVersion Va a cambiar a 0
                        ShowDeleteErrorMessage("No Puede eliminar un Documento Padre del sistema de versiones. Primero elimine sus versiones");
                    }
                    else
                    {
                        //si tiene versiones hijas no solo hay que eliminar el result
                        //tambien hay que actualizar los results hijos
                        //para que no queden huerfanos
                        DeleteResult((Result)_result);
                        RB.DeleteResultFromWorkflows(_result.ID);
                    }
                }
                else
                {
                    if (_result.HasVersion == 0)
                    {
                        DeleteResult((Result)_result);
                        RB.DeleteResultFromWorkflows(_result.ID);
                    }
                    else
                    {
                        ShowDeleteErrorMessage("No tiene permisos para eliminar documentos Versionados");
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            ShowDeleteErrorMessage("Ha ocurrido un error al eliminar el documento");
        }
        finally {
            RiB = null;

        }
    }

    private void ShowDeleteErrorMessage(string msj)
    {
        lblMessage.Visible = true;
        lblMessage.Text = msj;
        //btnDelete.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideLoading", "hideLoading();", true);
    }

    private void DeleteResult(IResult r)
    {
        if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Delete, r.DocType.ID))
        {
            Int64 DocumentId = r.ID;
            string DocName = r.Name;
            bool ok = false;

            try
            {
                NotesBusiness.DeleteNotes(r.ID);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }

            try
            {
                if (r.HasVersion == 1)
                {
                    //Actualiza los datos de las versiones
                    RB.UpdateResultsVersionedDataWhenDelete(r.DocType.ID, r.ParentVerId, r.ID, r.RootDocumentId);
                }

                //Se obtiene el parentid de base porque en la coleccion de results
                //puede venir a desactualizado ( ya que se modifica en ejecucion)
                //TODO: PERFORMANCE: verificar que sea cierto.
                Int64 parentid = RB.GetParentVersionId(r.DocType.ID, r.ID);

                //Se elimina el documento
                RB.Delete(ref r, true, true);
                ok = true;

                if (r.ParentVerId > 0 && RB.CountChildsVersions(r.DocType.ID, r.ParentVerId) == 0)
                {
                    // en caso de que se elimine un child y haya que actualizar el padre (HasVersion = 0)
                    RB.UpdateLastResultVersioned(r.DocType.ID, parentid);
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                ShowDeleteErrorMessage("Ha ocurrido un error al eliminar el documento");
            }

            //Se guarda el historial de la acción
            SUsers u = null;
            try
            {
                u = new SUsers();
                u.SaveAction(DocumentId, ObjectTypes.Documents, RightsType.Delete, DocName);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
            finally
            {
                u = null;
            }

            //Si el documento fue correctamente eliminado, se cierra el tab
            if (ok) CerrarTab();
        }
        else
        {
            ShowDeleteErrorMessage("Usted no tiene permisos para eliminar el documento");
        }
    }

    private IResult GetResult(Int32 docId, Int32 doctypeId)
    {
        SResult SResult = new SResult();
        return SResult.GetResult(docId, doctypeId, true);
    }

    public void CerrarTab()
    {
        //Se cambia la forma que usa el script dado que no se debe escribir el scrip directamente en la página.
        //Se cambia por un document.ready para que reconozca funciones de la master.
        //Response.Write("<script language='javascript'> { hideLoading(); parent.CloseTask(1,true);}</script>");
        string Script = "$(document).ready(function(){ hideLoading(); isClosingTask = true; parent.CloseTask(1,true);});";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClosingScript", Script, true);
    }  

}