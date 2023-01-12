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
using System.Windows.Forms;
using Zamba.Core;
using System.IO;
using System.Collections.Generic;

public partial class WebBrowser_WebBrowser : System.Web.UI.Page
{

    private string temppath;
    protected void Page_PreRender(object sender, EventArgs e)
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
                    //Int32.TryParse(Request.QueryString["doctype"], out doctypeId);
                    
                    Int32.TryParse(Request.QueryString["docid"], out docId);
                    doctypeId =(Int32)DocTypesBusiness.GetDocTypeIdByDocId(docId);

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
                                    using (StreamReader str = new StreamReader(Url))
                                    {
                                        Trace.Write("valor de str que es el streamreader:" + str);
                                        try
                                        {

                                            StreamReader a = new StreamReader(str.BaseStream);

                                            //string strHtml = FormBussines.GetInnerHTML(a.ReadToEnd()).ToUpper();
                                            string strHtml = FormBussines.GetInnerHTML(a.ReadToEnd());
                                            //if (strHtml.ToUpper().Contains("</HEAD"))
                                            //{
                                            //    Char Mander = Char.Parse("§");
                                            //    strHtml = strHtml.ToUpper().Replace("</HEAD>", "§").Split(Mander)[1];
                                            //}

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
                                                            String item = strHtml.Substring(0, strHtml.ToUpper().LastIndexOf("ZAMBA_INDEX_" + Indice.ID) + ("ZAMBA_INDEX_ " + Indice.ID).Length);
                                                            item = item.Substring(item.LastIndexOf("<"));

                                                            String aux2 = strHtml.Substring(strHtml.ToUpper().LastIndexOf("ZAMBA_INDEX_" + Indice.ID) + ("ZAMBA_INDEX_ " + Indice.ID).Length);
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



                                            string NewstrHtml = Server.HtmlDecode(strHtml);


                                            //using (FileStream archivo = new FileStream(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\paginaprueba.html", FileMode.Create))
                                            //{
                                            //System.IO.StreamWriter writer = new System.IO.StreamWriter(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\paginapruebafruta.html");
                                            //writer.Write(NewstrHtml);
                                            //writer.Close();
                                            System.Random num = new Random();

                                            temppath = Server.MapPath("~/temp") + "\\paginaprueba" + num.Next(99999).ToString() + ".html";
                                            using (StreamWriter write = new StreamWriter(temppath))
                                            {
                                                write.AutoFlush = true;
                                                write.Write(NewstrHtml);
                                                write.WriteLine("</form>");
                                                write.WriteLine("</body>");
                                                write.WriteLine("</html>");
                                                //write.Write("Fruta");
                                            }
                                            //}

                                            formBrowser.Attributes.Add("src", temppath);
                                            lblDoc.Text = "";
                                        }
                                        catch (Exception ex)
                                        {
                                            lblDoc.Text = "No se ha podido cargar el documento";
                                            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
                                        }
                                        finally
                                        {
                                            System.IO.FileInfo fa = new System.IO.FileInfo(temppath);
                                            if (fa.Extension.CompareTo(".html") == 0 || fa.Extension.CompareTo(".htm") == 0)
                                                Response.Redirect(Request.ApplicationPath + "\\temp\\" + fa.Name);
                                            udpIframe.Update();
                                        }
                                    }
                                }
                               // else
                                    //lblDoc.Text = "No se ha podido cargar el documento";
                        }
                        else
                        {
                            try
                            {
                                Url = MakeLocalCopy(Url, false);
                                Response.Redirect(Url);
                            }
                            catch (System.Threading.ThreadAbortException ex)
                            {
                                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);

                            }
                            catch (Exception ex)
                            {
                                lblDoc.Text = "No se ha podido cargar el documento";
                                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
                            }
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
            Trace.Write("adentro de copia local");
            fi = new System.IO.FileInfo(path);
            fa = new System.IO.FileInfo(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + fi.Name);
            Trace.Write("valor de fi " + fi);
            Trace.Write("valore de fa " + fa);
            try
            {
                Trace.Write("dentro del try para comprobar si el archivo existe");
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
                    Trace.Write("ubicacion de fa" + fa.FullName);
                    fa.Delete();
                    Trace.Write("el formulario fue borrado (fa)");
                }
                Trace.Write("4");
                if (fa.Directory.Exists == false)
                {
                    fa.Directory.Create();
                    fa.IsReadOnly = true;
                }
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
                    return Request.ApplicationPath + "\\temp\\" + fa.Name;
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









    }

