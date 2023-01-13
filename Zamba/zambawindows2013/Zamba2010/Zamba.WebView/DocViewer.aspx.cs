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
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class DocViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string Url = Request.QueryString["fullpath"];//gvDocuments.SelectedRow.Cells[gvDocuments.SelectedRow.Cells.Count - 1].Text;
                string replaceFor = System.Web.Configuration.WebConfigurationManager.AppSettings["ReplaceFor"].ToString();
                string replaceValue = System.Web.Configuration.WebConfigurationManager.AppSettings["ReplaceValue"].ToString();
                Url = Url.Replace(replaceFor, replaceValue);

                //if (String.Compare(Url, string.Empty) != 0)
                //{
                //    Url = MakeLocalCopy(Url, false);
                //    Response.Redirect(Url); 
                //}
                //else
                //{
                    Int32 doctypeId = 0, i = 0, docId = 0;
                    Int32.TryParse(Request.QueryString["doctype"], out doctypeId);
                    Int32.TryParse(Request.QueryString["docid"], out docId);

                    if (doctypeId > 0 && docId > 0)
                    {
                        //Ver si es formulario virtual
                        ZwebForm[] Forms = FormBussines.GetShowAndEditForms(doctypeId);
                        if (Forms != null && Forms.Length > 0)
                        {
                            for (i = 0; i < Forms.Length; i++)
                                if (Forms[i].Type == FormTypes.Show)
                                {
                                    //Copiar los archivos locales
                           
                                    Trace.Write ("valor del path local" + Forms[i].Path);
                                    Url = MakeLocalCopy(Forms[i].Path, true);
                                    Trace.Write("valor de la url del formulario: " + Url);
                                    StreamReader str = new StreamReader(Url);
                                    Trace.Write("valor de str que es el streamreader:" + str);
                                    try
                                    {

                                        StreamReader a = new StreamReader(str.BaseStream);

                                        string strHtml = FormBussines.GetInnerHTML(a.ReadToEnd()).ToUpper();
                                        if (strHtml.Contains("</HEAD"))
                                        {
                                            Char Mander = Char.Parse("§");
                                            strHtml = strHtml.ToUpper().Replace("</HEAD>", "§").Split(Mander)[1];
                                        }

                                        //cargar indices 
                                        List<IIndex> indices = IndexsBussines.GetIndexsFromZI(docId);

                                        if (indices != null)
                                        {
                                            foreach (Index Indice in indices)
                                            {
                                                try
                                                {
                                                    if (strHtml.ToUpper().Contains("ZAMBA_INDEX_" + Indice.ID))
                                                    {

                                                        String item = strHtml.Substring(0, strHtml.LastIndexOf("ZAMBA_INDEX_" + Indice.ID) + ("ZAMBA_INDEX_ " + Indice.ID).Length);
                                                        item = item.Substring(item.LastIndexOf("<"));

                                                        String aux2 = strHtml.Substring(strHtml.LastIndexOf("ZAMBA_INDEX_" + Indice.ID) + ("ZAMBA_INDEX_ " + Indice.ID).Length);
                                                        aux2 = aux2.Substring(0, aux2.IndexOf(">"));

                                                        item = item + aux2 + ">";

                                                        aux2 = FormBussines.AsignValue(Indice, item);

                                                        strHtml = strHtml.Replace(item, aux2);
                                                                                                               
                                                       
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Zamba.Core.ZClass.raiseerror(ex);
                                                }
                                            }
                                        }
                                        //Muestra el formulario virtual
                                        
                                        strHtml = strHtml + AsociatedTable(docId ,doctypeId );
                                        
                                        this.formBrowser.InnerHtml = strHtml;
                                        lblDoc.Text = "";
                                    }
                                    catch (Exception ex)
                                    {
                                        lblDoc.Text = "No se ha podido cargar el documento";
                                        writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
                                    }
                                    finally
                                    {
                                        str.Close();
                                        str.Dispose();
                                        str = null;
                                    }
                                }
                               // else
                                    //lblDoc.Text = "No se ha podido cargar el documento";
                        }
                        else
                        {
                            Url = MakeLocalCopy(Url, false);
                            Response.Redirect(Url);
                       }


                   }
                //}
            }
        }
        catch (Exception ex)
        {
            lblDoc.Text = "No se ha podido cargar el documento";
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    /// <summary>
    /// Hace la copia local del archivo
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private String MakeLocalCopy(String path, Boolean isVirtual)
    {
        System.IO.FileInfo fi;
        System.IO.FileInfo fa;

        try
        {
            Trace.Write ("adentro de copia local");
            fi = new System.IO.FileInfo(path);
            fa = new System.IO.FileInfo(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + fi.Name);
            Trace.Write("valor de fi " + fi );
            Trace.Write("valore de fa " + fa);
            try
            {
                Trace.Write ("dentro del try para comprobar si el archivo existe");
                if (fa.Exists == true)
                {
                    Trace.Write("1");
                    if (fa.IsReadOnly == true)
                    {
                        fa.IsReadOnly = false;
                        Trace.Write("3");
                    }
                    Trace.Write("2");
                    Trace.Write("valor de la propiedad es lectura" + fa.IsReadOnly);
                    Trace.Write("ubicacion de fa"+fa.FullName );
                    fa.Delete();
                    Trace.Write("el formulario fue borrado (fa)");
                }
                Trace.Write("4");
                if (fa.Directory.Exists == false)
                    fa.Directory.Create();
                Trace.Write("el diretorio fue creado");

                System.IO.File.Copy(fi.FullName, fa.FullName);
                Trace.Write("el formulario fue copiado");
                if (isVirtual == true)
                {
                    Trace.Write("es formulario virtual");
                    Trace.Write("fullname del formulario" + fa.FullName);
                    return fa.FullName;                    
                }
                else
                    return ".\\temp\\" + fi.Name;
            }
            catch (Exception ex)
            {
                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
            return string.Empty;
        }
        finally
        {
            fi = null;
            fa = null;
        }
    }

    /// <summary>
    /// Escribe un log con el error ocurrido en el servidor
    /// </summary>
    /// <param name="message"></param>
    private void writeLog(String message)
    {
        try
        {
            string path = System.Web.HttpRuntime.AppDomainAppPath + "Exceptions\\";

            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path += "Exception ";
            path += System.DateTime.Now.ToString().Replace(".", "").Replace(":", "");
            path = path.Replace("/", "-");
            path += ".txt";

            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(message);
            writer.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string AsociatedTable(Int64 DocId, Int64 DocTypeId)
    {

        Result CurrentResult = Results_Business.GetResult(DocId, DocTypeId);
        ArrayList ArrayAsoc = DocAsociatedBussines.getAsociatedResultsFromResult(CurrentResult);
        string EditDate = string.Empty;
        string CreateDate = string.Empty;
        string DocName = string.Empty;
        string table = string.Empty;
        string AsignedUser=string.Empty;
        string State = string.Empty ;
        string HtmlRow = "<td>ButtonView</td><td>DocName</td><td>AsignedUser</td><td>DocTypeName</td><td>CreateDate</td><td>OriginalName</td><td>EditDate</td>";
        string htmlHeadLine = "<table><tr><td></td></tr><tr><td></td></tr><tr><td>DOCUMENTOS ASOCIADOS</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr></table>";
        string htmlTitle = "<tr><td></td><td>Nombre de Documento</td><td>Usuario Asignado</td><td>Tipo de Documento</td><td>Fecha de Creación</td><td>Nombre del Archivo</td><td>Fecha de Modificacin</td></tr>";
        string ButonVer = "<input id=view type=button value=ver>";

        if (ArrayAsoc.Count > 0)
        {
            foreach (Result asociated in ArrayAsoc)
            {
                CreateDate = asociated.CreateDate.ToString();
                EditDate = asociated.EditDate.ToString();
                DocName = asociated.Name;
                if (WFTaskBussines.GetTaskIdByDocId(asociated.ID) == -1)

                    AsignedUser = string.Empty;
                    //AsignedUser = WFUserBussines.GetUsersByStepID(WFStepBussines.GetStepIdByTaskId(WFTaskBussines.GetTaskIdByDocId(asociated.ID)))[0];
                //else
                    

                HtmlRow = HtmlRow.ToLower().Replace("docname", DocName);
                HtmlRow = HtmlRow.ToLower().Replace("asigneduser", AsignedUser);
                HtmlRow = HtmlRow.ToLower().Replace("doctypename", DocTypesBusiness.GetDocTypeName((Int32)asociated.DocTypeId));
                HtmlRow = HtmlRow.ToLower().Replace("createdate", CreateDate);
                HtmlRow = HtmlRow.ToLower().Replace("editdate", EditDate);
                HtmlRow = HtmlRow.ToLower().Replace("originalname", asociated.OriginalName.Substring(asociated.OriginalName.LastIndexOf("\\") + 1));
                HtmlRow = HtmlRow.ToLower().Replace("buttonview", ButonVer);
            }

            table = htmlHeadLine + "<table>" + htmlTitle + HtmlRow + "</table>";

            return table;
        }
        return string.Empty;
    }
}
