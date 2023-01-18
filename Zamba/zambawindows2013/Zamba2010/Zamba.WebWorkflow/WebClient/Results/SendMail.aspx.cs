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

public partial class DocViewer : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack != true)
        {

            try
            {
                
                if (Session["DocSelTB"] != null && ((Result)Session["DocSelTB"]).ISVIRTUAL != true)
                {

                    string filename = ((Result)Session["DocSelTB"]).FullPath.Remove(0, Session["DocSelTB"].ToString().LastIndexOf("\\") + 1);
                    WCSendMail.ClearAttachments();
                    WCSendMail.SetInitialState();
                    WCSendMail.AddAttach(filename, ((Result)Session["DocSelTB"]).FullPath);
                    
                }
                else
                {
                    //[sebastian 17-02-2009]Genero un formulario virtual con los indices cargados para luego adjuntarlo al mail.
                    GenerateFillVirtualFormAndAttach();

                }
            }
            catch (Exception ex)
            {
                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
                

            }
        }
    }

    /// <summary>
    /// Genera el formulario virtual en la carpeta temporal para luego poder atacharlo al mail que se envía.
    /// [sebastian 17-02-2009]
    /// </summary>
    private void GenerateFillVirtualFormAndAttach()
    {
        string filename = Int32.Parse(((Result)Session["DocSelTB"]).ID.ToString()) + ".html";
        string Url = string.Empty;
        StreamReader a = null;
        StreamReader str = null;
        string filepath = System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + Int32.Parse(((Result)Session["DocSelTB"]).ID.ToString()) + ".html";
        try
        {
            
            string replaceFor = System.Web.Configuration.WebConfigurationManager.AppSettings["ReplaceFor"].ToString();
            string replaceValue = System.Web.Configuration.WebConfigurationManager.AppSettings["ReplaceValue"].ToString();
            Int64 DocTypeId = DocTypesBusiness.GetDocTypeIdByDocId(((Result)Session["DocSelTB"]).ID);


            try
            {

                //Obtengo los formularios virtuales del tipo de documento.
                ZwebForm[] Forms = FormBussines.GetShowAndEditForms(Int32.Parse(DocTypeId.ToString()));
                //Compruebo que este lleno forms
                if (Forms != null && Forms.Length > 0)
                {
                    //recorro todos los formularios hasta encontrar el de tipo correspondiente (show)
                    //que es el mismo que se se usa para visualizar en la grilla de resultados
                    for (Int32 i = 0; i < Forms.Length; i++)
                        if (Forms[i].Type == FormTypes.Show)
                        {
                            //Se realiza una copia local del formulario original.
                            Trace.Write("valor del path local" + Forms[i].Path);
                            //Realizo una copia local del formulario
                            Url = MakeLocalCopy(Forms[i].Path, true);
                            Trace.Write("valor de la url del formulario: " + Url);
                            str = new StreamReader(Url);
                            Trace.Write("valor de str que es el streamreader:" + str);


                            a = new StreamReader(str.BaseStream);

                            //Obtengo el codigo HTML del formulario
                            string strHtml = FormBussines.GetInnerHTML(a.ReadToEnd());


                            //Obtengo los indices
                            List<IIndex> indices = IndexsBussines.GetIndexsFromZI(Int32.Parse(((Result)Session["DocSelTB"]).ID.ToString()));

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

                                            item = item + " readonly=\readonly\"" + aux2 + ">";
                                            //Cargo en el indice del formulario el valor del mismo
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


                            if (System.IO.File.Exists(filepath))
                            {
                                
                                System.IO.File.Delete(filepath);
                            }                         
                            //Formulario completo
                            string NewstrHtml = strHtml;
                            //Guardo el formulario en la carpeta temporal con el id como nombre, generando un archivo HTML
                            StreamWriter write = new StreamWriter(filepath);// (archivo);
                            write.AutoFlush = true;
                            write.Write(NewstrHtml);
                            write.Close();




                            //Realizo el Attach del formulario al mail.


                            WCSendMail.ClearAttachments();
                            WCSendMail.SetInitialState();
                            WCSendMail.AddAttach(filename, filepath);



                        }

                }
            }
            catch (Exception ex)
            {
                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);

            }

            finally
            {
                filename = null;
                Url = null;
                if (a != null)
                {
                    a.Close();
                    a.Dispose();
                    a = null;
                }

                if (str != null)
                {
                    str.Close();
                    str.Dispose();
                    str = null;
                }
                
            }
        }

        
        
            
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }

      
           
            
    }

   

    /// <summary>
    /// Metodo que sirve para realizar una copia local del formulario [sebastian 17-02-2009]
    /// </summary>
    /// <param name="path">ruta oridinal del formulario</param>
    /// <param name="isVirtual"> variable que indice si es virtual o no el archivo en el cual estoy pasado la ruta</param>
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
