using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop;
using Zamba.Core;
using Zamba.Tools;
using Microsoft.Office.Core;
using Zamba.Office.Outlook;
using System.Windows.Forms;

namespace Zamba.FileTools
{
    [CLSCompliant(false)]
    public class Interop
    {

        public void ExtractTextFromMsgAttach(string filename, ref string alltext)
        {
            bool Errors = false;

            try
            {
                Microsoft.Office.Interop.Outlook.Application outlookApp = Zamba.Office.Outlook.SharedOutlook.GetOutlookApp();
				if (outlookApp != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Mail: " + filename);
                    Microsoft.Office.Interop.Outlook.MailItem mail = outlookApp.Session.OpenSharedItem(filename);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Mail Obtenido: " + filename);

                    //Guardo texto del mail.
                    alltext += mail.Body;
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Mail Body: " + alltext);
                    alltext += " ";
					alltext += mail.To;
					alltext += " ";
					alltext += mail.Subject;
					alltext += " ";
					alltext += mail.SenderName;
					alltext += " ";
					alltext += mail.CC;
					alltext += " ";
					alltext += mail.BCC;
					alltext += " ";

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Mail Texto Parcial: " + alltext);


                    for (int i = 1; i < mail.Attachments.Count + 1; i++)
					{
                        try
                        {

                        if (mail.Attachments[i].Type == Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue)
						{
							string fileNameAtt = null;
							try
							{
								//Obtengo extensión del adjunto
								fileNameAtt = Path.GetExtension(mail.Attachments[i].FileName).ToLower();
							}
							catch (Exception ex)
							{
								fileNameAtt = "";
								Zamba.Core.ZClass.raiseerror(ex);
							}

							if (isText(fileNameAtt))
							{
								string rutaDoc = FileBusiness.GetUniqueFileName(Zamba.Tools.EnvironmentUtil.GetTempDir("\\OfficeTemp\\").FullName, "attachOutlook", fileNameAtt);
								mail.Attachments[i].SaveAsFile(rutaDoc);

								if (fileNameAtt == ".msg")
								{
									ExtractTextFromMsgAttach(rutaDoc, ref alltext);
								}
								else
								{
									alltext += GetFileTextAttach(rutaDoc, alltext, Errors);
								}

								File.Delete(rutaDoc);
							}

						}
                        }
                        catch (Exception ex)
                        {
                            Zamba.Core.ZClass.raiseerror(ex);
                        }

                    }
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Mail Texto Completo: " + alltext);

                }
            }
            catch (Exception)
            {

                try
                {
                    string nameHtmlFile = filename.Replace(".msg", ".html");
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Nombre html " + nameHtmlFile);
                    if (File.Exists(nameHtmlFile))
                    {
                        Zamba.FileTools.Binary binaryB = new Zamba.FileTools.Binary();
                        alltext += binaryB.GetFileText(nameHtmlFile, Errors);
                    }
                }
                catch (Exception ex1)
                {

                    Zamba.Core.ZClass.raiseerror(ex1);
                }
            }
            finally
            {

            }
        }

        public static bool isText(string filename)
        {
            if (string.IsNullOrEmpty(filename.Trim()))
            {
                return false;
            }

            FileInfo Fi = new FileInfo(filename.Trim().ToLower());

            if ((string.Compare(Fi.Extension, ".msg", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".xlsx", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".docx", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".doc", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".wri", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".pdf", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".htm", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".html", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".xls", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".txt", true) == 0)) return true;
            if ((string.Compare(Fi.Extension, ".rtf", true) == 0)) return true;
            return false;
        }

    

    //Se comenta ya que se modifico la forma en la que se obtiene la instancia de Outlook JB
    //public static Microsoft.Office.Interop.Outlook.Application GetOutlookApplication()
    //{
    //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obtengo instancia de Outlook");
    //    bool outlookIsOpen = false;

    //    ProcessStartInfo startInfo = new ProcessStartInfo("outlook");
    //    startInfo.WindowStyle = ProcessWindowStyle.Minimized;

    //    System.Diagnostics.Process.Start(startInfo);

    //    do
    //    {
    //        try
    //        {
    //            OutlookApplication = (Microsoft.Office.Interop.Outlook.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
    //            outlookIsOpen = true;
    //        }
    //        catch (System.Runtime.InteropServices.COMException ex)
    //        {
    //            //Outlook is loading required components, so it isn't disponible.
    //            if (ex.ErrorCode.ToString() == "-2147221021") outlookIsOpen = false;
    //        }
    //        catch (Exception ex)
    //        {
    //            Zamba.Core.ZClass.raiseerror(ex);
    //        }

    //        System.Threading.Thread.Sleep(1000);

    //    } while (outlookIsOpen == false);

    //    //Agrego esta linea que minimiza outlook para disparar el 'hide when minimize' de outlook, para que
    //    //quede visible el ícono en la barra de tareas en la parte inferior derecha de la pantalla.
    //    OutlookApplication.ActiveExplorer().WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMinimized;

    //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obtención de instancia de Outlook realizada correctamente");
    //    return OutlookApplication;

    //}


    public static string GetFileTextAttach(string rutaDoc, string alltext, bool Errors = false)
        {
            string ext = Path.GetExtension(rutaDoc).ToLower();

            if (ext.Contains(".docx") | ext.Contains(".doc")| ext.Contains(".dot"))
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo adjunto Word");

                SpireTools sp = new SpireTools();
                alltext += " " + sp.GetTextFromDoc(rutaDoc);
                sp = null;
            }
            else if (ext.Contains(".xls") | ext.Contains(".xlsx"))
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo adjunto Excel");

                SpireTools sp = new SpireTools();
                alltext += " " + sp.GetTextFromExcel(rutaDoc);
                sp = null;
            }
            else if (ext.Contains(".pdf"))
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo adjunto PDF");

                ITextSharp ITS = new ITextSharp();
                alltext += " " + ITS.GetTextFromPDF(rutaDoc);
                ITS = null;
            }
            else if (ext.Contains(".ppt") | ext.Contains(".pps"))
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo adjunto PowerPoint");

                Interop interopB = new Interop();
                alltext += " " + interopB.GetTextFromPPT(rutaDoc);
                interopB = null;
            }
            else if (ext.Contains(".txt") | ext.Contains(".rtf"))
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo adjunto de forma Binaria");

                Binary BinaryB = new Binary();
                alltext += " " + BinaryB.GetFileText(rutaDoc, Errors);
                BinaryB = null;
            }

            return alltext;
        }

        public string GetTextFromPPT(string filename)
        {
            string presentation_text = "";

            Microsoft.Office.Interop.PowerPoint.Application PowerPoint_App = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Interop.PowerPoint.Presentations multi_presentations = PowerPoint_App.Presentations;

            //Se abre PowerPoint sin mostrar interfaz de usuario
            Microsoft.Office.Interop.PowerPoint.Presentation presentation = multi_presentations.Open(filename, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);

            try
            {

                for (int i = 0; i < presentation.Slides.Count; i++)
                {
                    foreach (var item in presentation.Slides[i + 1].Shapes)
                    {
                        var shape = (Microsoft.Office.Interop.PowerPoint.Shape)item;
                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            if (shape.TextFrame.HasText == MsoTriState.msoTrue)
                            {
                                var textRange = shape.TextFrame.TextRange;
                                var text = textRange.Text;
                                presentation_text += text + " ";

                                textRange = null;
                                text = null;
                            }
                        }
                        shape = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            finally
            {
                presentation.Close();

                if (presentation != null)
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(presentation);
                    presentation = null;
                }

                //Se comenta ya que no funciona el quit.
                //PowerPoint_App.Quit();

                if (PowerPoint_App != null)
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(PowerPoint_App);
                    PowerPoint_App = null;
                }
            }

            return presentation_text;
        }

    }
}
