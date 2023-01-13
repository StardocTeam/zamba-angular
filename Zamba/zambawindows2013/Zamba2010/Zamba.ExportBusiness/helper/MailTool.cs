using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
//using Zamba.ExportaOutlook.RServices;
using Zamba.Services.RemoteEntities;
using Zamba.Services.RemoteInterfaces;
using Zamba.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Windows.Forms;
using Zamba.Services.Remoting;
using Zamba;
namespace ExportaOutlook.Helper
{
    /// <summary>
    /// Clase que tiene la funcionalidad de procesar
    /// los mails para exportar.
    /// </summary>
    public class MailTool
    {
        #region Propiedades
        private string _htmlBody;
        private string _Error;
        private int _cantidadProcesar = 0;
        private int _noExportados = 0;
        private string _serverPath;

        private ZambaRemoteClass ZRC = new ZambaRemoteClass();
        public static string FlagRequestText = "Exportado a Zamba";

        /// <summary>
        /// Cantida de elemntos a procesar
        /// </summary>
        public int CantidadProcesar
        {
            get { return _cantidadProcesar; }
            set { _cantidadProcesar = value; }
        }

        /// <summary>
        /// Cantidad de mails no exportados
        /// </summary>
        public int NoExportados
        {
            get { return _noExportados; }
            set { _noExportados = value; }
        }

        /// <summary>
        /// Ultimo mensaje de error
        /// </summary>
        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Procesa el conjunto de elementos seleccionados.
        /// </summary>    
        public List<IMail> GetPendingMails(Outlook.Application app, bool isAutoExport, Int64 doctypeid, Dictionary<long, IIndex> indexs, bool insertattach, bool duplicated)
        {
            List<IMail> sel = new List<IMail>();

            //Obtiene la lista con los items seleccionados
            if (!isAutoExport)
            {
                sel = CheckSelectedItems(app, doctypeid, indexs, insertattach, duplicated);
            }
            else
            {
                //Se obtiene la configuracion que asocia carpetas de outlook con entidades de zamba
                List<IFolderMap> folderMaps = ZRC.GetFolderDocTypeMaps(isAutoExport);

                //Verifica si existen asociaciones
                if (folderMaps != null && folderMaps.Count > 0)
                {
                    List<IMail> singleFolder = new List<IMail>();

                    //Recorre las asociaciones
                    foreach (FolderMap folderMap in folderMaps)
                    {
                        //Exporta mails no leidos de esa carpeta
                        singleFolder = CheckUnreadMails(app, folderMap);

                        //Agrega los mails procesados
                        for (int i = 0; i < singleFolder.Count; i++)
                        {
                            sel.Add(singleFolder[i]);
                        }
                    }
                }

                folderMaps.Clear();
                folderMaps = null;
            }

            ZTrace.WriteLineIf(ZTrace.IsError, "----------------------------------------------------------");
            return sel;
        }

        /// <summary>
        /// Obtiene un objeto mail
        /// </summary>
        public IMail GetMail(Outlook.Application app, object o, Boolean DoBackUp)
        {
            Int32 index = 0;
            IMail m = new Mail();
            ZTrace.WriteLineIf(ZTrace.IsError, "get Item");

            if (!(o is Outlook.MailItem) && !(o is Outlook.ReportItem))
            {
                _Error = "Algunos elementos seleccionados no son del tipo mail.\nSolo se pueden exportan elementos del tipo mail.\nVerifique los elementos seleccionados y vuelva a realizar la operacion.";
                ZTrace.WriteLineIf(ZTrace.IsError, _Error);
                throw new ArgumentException(_Error);
            }

            _cantidadProcesar++;
            Outlook.MailItem mail = null;
            Outlook.ReportItem report = null;
            string guid = null;
            if (o is Outlook.MailItem)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "El mail es de tipo MailItem.");
                mail = o as Outlook.MailItem;
                m.FlagRequest = mail.FlagRequest;
                m.isReport = false;
                guid = mail.EntryID;
                m.OriginalMail = mail;
            }
            else if (o is Outlook.ReportItem)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "El mail es de tipo ReportItem.");
                report = o as Outlook.ReportItem;
                //mail = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Outlook.MailItem;
                //.Subject = report.Subject;
                m.subject = report.Subject;
                //m.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText;
                
                m.body = report.Body;
                m.to = Environment.UserName;
                //mail.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                m.isReport = true;
                guid = report.EntryID;
                m.OriginalMail = report;
            }

            index++;

            //Si el mail ya se exporto no se vuelve a exportar
            if (!IsExported(o))
            {
                //Obtiene el contenido del cuerpo del mail en formato html o el cuerpo del reporte
                _htmlBody = mail!=null ? mail.HTMLBody : report.Body;

                //Se valida el subject del mail
                m.type = "msg";
                m.FileName = guid + "." + m.type;

                if (DoBackUp) BackUpMail(m, guid);


                //Crea la carpeta donde se va almacenar la exportacion de este mail en particular.
                //Una carpeta por cada mail.
                try
                {
                    //Extrae las propiedades del mail
                    if (o is Outlook.MailItem)
                    {
                        GetProperties(mail, m, app);
                    }
                    else if (o is Outlook.ReportItem)
                    {
                        GetProperties(mail, report, m);
                    }
                }
                catch (Exception e)
                {
                    _Error = "La creacion del backup de la carpeta mail ha fallado.";
                    ZClass.raiseerror(e);
                    ZTrace.WriteLineIf(ZTrace.IsError, _Error + "\n" + e.ToString());
                }



                m.index = index;
                ZTrace.WriteLineIf(ZTrace.IsError, "----------------------------------------------------------");

            }

            else
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "El mail " + mail.Subject + " ya se ha exportado.");
                _noExportados++;
            }

            return m;


        }

        public void BackUpMail(IMail iMail, string guid)
        {
            string backupPath = GetBackupPath(guid);

            if (!string.IsNullOrEmpty(backupPath))
            {
                Outlook.MailItem currentMail = null;
                Outlook.ReportItem currentReport = null;

                if (iMail.OriginalMail is Outlook.MailItem)
                {
                    currentMail = ((Outlook.MailItem)iMail.OriginalMail);
                    currentMail.SaveAs(backupPath, Outlook.OlSaveAsType.olMSG);
                }
                else
                {

                    currentReport = ((Outlook.ReportItem)iMail.OriginalMail);
                    currentReport.SaveAs(backupPath, Outlook.OlSaveAsType.olMSG);
                }

                ZTrace.WriteLineIf(ZTrace.IsError, "El backup del mail se ha ejecutado con éxito.");

                iMail.folder = Path.GetDirectoryName(backupPath);
                iMail.storeName = Path.Combine(iMail.folder, "mail.msg");
                //Crea una carpeta para las imagenes adjuntas.
                iMail.folderAttachMail = iMail.folder;

                if (currentMail != null)
                {
                    //Obtiene los adjuntos del mail.
                    ZTrace.WriteLineIf(ZTrace.IsError, "Attachments found: " + currentMail.Attachments.Count.ToString());
                    iMail.adjuntos = new List<IAdjunto>();
                    foreach (Outlook.Attachment att in currentMail.Attachments)
                    {
                        try
                        {
                            ZTrace.WriteLineIf(ZTrace.IsError, "-----------");
                            IAdjunto adj = new Adjunto();
                            adj.name = att.FileName;
                            ZTrace.WriteLineIf(ZTrace.IsError, "FileName: " + att.FileName);
                            String storeName = Path.Combine(iMail.folder, att.FileName);
                            Int64 i = 0;

                            while (File.Exists(storeName))
                            {
                                i++;
                                storeName = Path.Combine(iMail.folder, Path.GetFileNameWithoutExtension(att.FileName) + "(" + i + ")" + Path.GetExtension(att.FileName));
                            }

                            if (storeName.Length > 254)
                                storeName = storeName.Substring(0, 254 - Path.GetExtension(att.FileName).Length) + Path.GetExtension(att.FileName);

                            adj.storeName = storeName;

                            //Guarda el adjunto                                       
                            if (SaveAttach(currentMail, iMail, att, adj))
                            {
                                iMail.adjuntos.Add(adj);
                            }

                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);

                        }
                    }

                    //Guarda el mail en formato html 
                    SaveHTML(currentMail, iMail);

                }
                else if (currentReport != null)
                {

                    //Guarda el reporte en formato html 
                    SaveReport(currentReport, iMail);
                }

            }
            else
            {
                throw new Exception("No se puede acceder a la ruta de BackUp Local");
            }
        }


        /// <summary>
        /// Obtiene la ruta para hacer backup del mail
        /// </summary>
        private string GetBackupPath(string guid)
        {

            string LocalBackUpPath = Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "ZBOutlook\\" + guid + "\\");
            ZTrace.WriteLineIf(ZTrace.IsError, "Obteniendo carpeta personal de usuario para almacenar un backup del mail " + LocalBackUpPath);


            if (!Directory.Exists(LocalBackUpPath))
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Creando directorio: " + LocalBackUpPath);
                Directory.CreateDirectory(LocalBackUpPath);
            }

            //Agrega el mensaje
            LocalBackUpPath = Path.Combine(LocalBackUpPath, "mail.msg");

            //Devuelve la ruta del backup
            ZTrace.WriteLineIf(ZTrace.IsError, "La ruta del mail para hacer backup es: " + LocalBackUpPath);

            return LocalBackUpPath;
        }


        /// <summary>
        /// Comprueba la existencia de mensajes nuevos.
        /// </summary>
        public List<IMail> CheckUnreadMails(Outlook.Application app, FolderMap folderMap)
        {
            ZTrace.WriteLineIf(ZTrace.IsError, "Verificando la existencia de la carpeta: " + folderMap.FolderName);
            Outlook.MAPIFolder folder = (Outlook.MAPIFolder)Helper.Folder.CheckFolderExists(app, folderMap.FolderEntryId, folderMap.FolderName);

            if (folder == null)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "La carpeta '" + folderMap.FolderName + "' no ha sido encontrada. ");
                return null;
            }
            ZTrace.WriteLineIf(ZTrace.IsError, "Carpeta encontrada. CustomFolder = " + folder.Name + " Path: " + folder.FullFolderPath);
            ZTrace.WriteLineIf(ZTrace.IsError, "Items en Carpeta: " + folder.Items.Count.ToString());

            string restriction;

            ZTrace.WriteLineIf(ZTrace.IsError, "# Buscando mails comunes no leidos y no exportados.");
            restriction = UserPreferences.getValueForMachine("FilterFolder", UPSections.ExportaPreferences, "[MessageClass]='IPM.Note' AND [Mileage] <> '1' AND [FlagRequest] <> 'Exportado a Zamba'");

            List<IMail> lstMails = SearchNewMails(restriction, folder, app, folderMap);

            ZTrace.WriteLineIf(ZTrace.IsError, "# Buscando acuses de recibo no leidos.");
            restriction = "[MessageClass]>'REPORT.IPM.NOTD' AND [MessageClass]<'REPORT.IPM.NOTF' AND [Mileage] <> '1'";
            List<IMail> lstReports = SearchNewMails(restriction, folder, app, folderMap);

            //ZTrace.WriteLineIf(ZTrace.IsError, "# Buscando todos los mails no leidos y no exportados.");
            //restriction = " [FlagRequest] <> " + FlagRequestText;
            //List<IMail> lstAllMails = SearchNewMails(restriction, folder, app,  folderMap);


            //Se unen todos los mails y acuses de recibo
            List<IMail> lstSum = new List<IMail>();
            if (lstMails != null && lstMails.Count > 0) lstSum.AddRange(lstMails);
            if (lstReports != null && lstReports.Count > 0) lstSum.AddRange(lstReports);
            //if (lstAllMails != null && lstAllMails.Count > 0) lstSum.AddRange(lstAllMails);

            lstMails = null;
            lstReports = null;
            //lstAllMails = null;
            ZTrace.WriteLineIf(ZTrace.IsError, "Fin de la verificación.");

            return lstSum;
        }

        private List<IMail> SearchNewMails(
            string restriction,
            Outlook.MAPIFolder folder,
            Outlook.Application app,
            FolderMap folderMap)
        {
            Outlook.Items mails = null;
            List<IMail> listMails = null;

            ZTrace.WriteLineIf(ZTrace.IsError, "# Buscando mails con Restriccion: " + restriction);

            if (restriction != string.Empty)
                mails = folder.Items.Restrict(restriction);
            else
                mails = folder.Items;

            ZTrace.WriteLineIf(ZTrace.IsError, "Mails obtenidos tras la búsqueda: " + Convert.ToString(mails.Count));
            listMails = new List<IMail>();

            Int32 limitMails = 10;
            Int32 currentMailCount = 0;
            foreach (object mail in mails)
            {
                try
                {
                    if (IsExported(mail))
                        break;

                    currentMailCount++;

                    if (currentMailCount <= limitMails)
                    {
                        IMail zMail = GetMail(app, mail, true);
                        zMail.isAutoExport = true;
                        zMail.DoctypeId = folderMap.DocTypeId;

                        if (folderMap.AttachDocTypeId > 0)
                        {
                            zMail.insertAttachAsDoc = true;
                            zMail.AttachDocTypeId = folderMap.AttachDocTypeId;
                            foreach (IAdjunto adjunto in zMail.adjuntos)
                                adjunto.DocTypeId = folderMap.AttachDocTypeId;
                        }

                        listMails.Add(zMail);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);

                }

            }

            mails = null;
            ZTrace.WriteLineIf(ZTrace.IsError, "Mails a procesar: " + listMails.Count.ToString());

            return listMails;
        }


        /// <summary>
        /// Retorna la seleccion de items de outlook
        /// </summary>
        private List<IMail> CheckSelectedItems(Outlook.Application app, Int64 doctypeid, Dictionary<long, IIndex> indexs, bool insertattach, bool duplicated)
        {
            ZTrace.WriteLineIf(ZTrace.IsError, "exec @checkSelectedItems(Outlook.Explorer ex)");
            List<IMail> selList = null;

            try
            {
                if (app.ActiveExplorer().Selection != null)
                {
                    selList = new List<IMail>();
                    foreach (object _mail in app.ActiveExplorer().Selection)
                    {
                        IMail _m = GetMail(app, _mail, true);
                        _m.DoctypeId = doctypeid;
                        _m.indexs = indexs;
                        _m.isAutoExport = false;
                        _m.insertAttachAsDoc = insertattach;
                        _m.Duplicated = duplicated;
                        selList.Add(_m);
                    }
                }
            }
            catch (Exception _ex)
            {
                _Error = "No se ha seleccionado ningun elemento.\nSeleccione un mail y vuelva a realizar la operacion";
                ZClass.raiseerror(_ex);
                ZTrace.WriteLineIf(ZTrace.IsError, _Error + " " + _ex);
                throw new ArgumentException(_Error);
            }

            ZTrace.WriteLineIf(ZTrace.IsError, "get Count: " + app.ActiveExplorer().Selection.Count.ToString());
            if (app.ActiveExplorer().Selection.Count == 0)
            {
                _Error = "No se ha seleccionado ningun elemento.\nSeleccione un mail y vuelva a realizar la operacion";
                ZTrace.WriteLineIf(ZTrace.IsError, _Error);
                throw new ArgumentException(_Error);
            }

            ZTrace.WriteLineIf(ZTrace.IsError, "endexec @checkSelectedItems(Outlook.Explorer ex)");
            return selList;
        }

        /// <summary>
        /// Verifica si hay elementos seleccionados.
        /// </summary>
        public bool CheckSelectedItems(Outlook.Explorer ex)
        {
            ZTrace.WriteLineIf(ZTrace.IsError, "Verificando la existencia de mails seleccionados.");

            try
            {
                if (ex.Selection.Count == 0 || (ex.Selection.Count == 1 && IsExported(ex.Selection[1])))
                    return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Guarda el archivo adjunto
        /// </summary>
        private bool SaveAttach(Outlook.MailItem mail, IMail m, Outlook.Attachment att, IAdjunto adj)
        {
            try
            {

                bool ret = false;
                ZTrace.WriteLineIf(ZTrace.IsError, "StoreName: " + adj.storeName);
                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "-----------");
                    att.SaveAsFile(adj.storeName);
                    ZTrace.WriteLineIf(ZTrace.IsError, "File Saved");
                    if (IsImage(adj.name))
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "This file is an image.");
                        att.SaveAsFile(Path.Combine(m.folderAttachMail, adj.name));
                        ZTrace.WriteLineIf(ZTrace.IsError, "Save file mail attach.");

                        ZTrace.WriteLineIf(ZTrace.IsError, "Mapping attach");
                        //modifica el ruta del archivo adjunto dentro del cuerpo del mail                    
                        _htmlBody = _htmlBody.Replace("SRC=\"" + adj.name + "\"", "SRC=\"" + Path.Combine(Path.GetFileName(m.folderAttachMail), adj.name) + "\"");
                    }
                    ret = true;
                }
                catch (Exception _ex)
                {
                    _Error = "No se puede completar la operacion.\nHa ocurrido un error al intentar general el temporal de exportacion.";
                    ZClass.raiseerror(_ex);
                    ZTrace.WriteLineIf(ZTrace.IsError, _Error + " " + _ex.ToString());
                    //throw new ArgumentException(_Error);
                }
                return ret;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        /// <summary>
        /// Guarda el mail en formato html
        /// </summary>
        private void SaveHTML(Outlook.MailItem mail, IMail m)
        {

            string strHTMLPath = string.Empty;
            ZTrace.WriteLineIf(ZTrace.IsError, "Mail StoreName: " + m.storeName);
            try
            {
                //Guarda el mail localmente
                //bool DoMailBackup = false;
                //if (DoMailBackup)
                if (!File.Exists(m.storeName))
                    mail.SaveAs(m.storeName, Outlook.OlSaveAsType.olMSG);

                if (mail.BodyFormat == Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML)
                {
                    strHTMLPath = m.storeName.ToLower().Replace(".msg", ".html");
                    mail.SaveAs(strHTMLPath, Outlook.OlSaveAsType.olHTML);
                }
                else
                {
                    strHTMLPath = m.storeName.ToLower().Replace(".msg", ".txt");
                    mail.SaveAs(strHTMLPath, Outlook.OlSaveAsType.olTXT);
                }
                m.SavedAsHTML = true;

                m.OriginalMail = mail;
            }
            catch (Exception _ex)
            {
                _Error = "No se puede completar la operacion.\nHa ocurrido un error al intentar generar el temporal de exportación.";
                ZClass.raiseerror(_ex);
                ZTrace.WriteLineIf(ZTrace.IsError, _Error + " " + _ex.ToString());
                //  throw new ArgumentException(_Error);
            }
            ZTrace.WriteLineIf(ZTrace.IsError, "File Saved");
        }



        /// <summary>
        /// Guarda el report en formato html
        /// </summary>
        private void SaveReport(Outlook.ReportItem report, IMail m)
        {
            string strHTMLPath = string.Empty;
            ZTrace.WriteLineIf(ZTrace.IsError, "Mail StoreName: " + m.storeName);
            try
            {
                //Guarda el mail localmente
                if (!File.Exists(m.storeName))
                    report.SaveAs(m.storeName, Outlook.OlSaveAsType.olMSG);
                m.OriginalMail = report;
            }
            catch (Exception _ex)
            {
                _Error = "No se puede completar la operacion.\nHa ocurrido un error al intentar generar el temporal de exportación.";
                ZClass.raiseerror(_ex);
                ZTrace.WriteLineIf(ZTrace.IsError, _Error + " " + _ex.ToString());
                //  throw new ArgumentException(_Error);
            }
            ZTrace.WriteLineIf(ZTrace.IsError, "File Saved");
        }

        /// <summary>
        /// Obtiene un GUID
        /// </summary>
        private string GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Obtiene las propiedades del mail de MailItem y actualiza los valores del objeto m
        /// </summary>
        private static void GetProperties(Outlook.MailItem mail, IMail m, Outlook.Application app)
        {
            //Obtiene las propiedades del mail.
            ZTrace.WriteLineIf(ZTrace.IsError, "Obteniendo las propiedades del MailItem...");

            m.sender = getSenderEmailAddress(mail); //.SenderEmailAddress;
            ZTrace.WriteLineIf(ZTrace.IsError, "Sender: " + m.sender);

            if (mail.To != null) m.to = ChangeNameToAddress(mail, mail.To, app);
            ZTrace.WriteLineIf(ZTrace.IsError, "To: " + m.to);

            if (mail.CC != null) m.CC = ChangeNameToAddress(mail, mail.CC, app);
            ZTrace.WriteLineIf(ZTrace.IsError, "CC: " + m.CC);

            if (mail.BCC != null) m.BCC = ChangeNameToAddress(mail, mail.BCC, app);
            ZTrace.WriteLineIf(ZTrace.IsError, "BCC: " + m.BCC);

            ZTrace.WriteLineIf(ZTrace.IsError, "RecievedTime: " + mail.ReceivedTime);
            m.receivedDate = mail.ReceivedTime.ToString();

            ZTrace.WriteLineIf(ZTrace.IsError, "Subject: " + mail.Subject);
            if (mail.Subject != null && mail.Subject.Length > 0) m.subject = mail.Subject.Trim();

            ZTrace.WriteLineIf(ZTrace.IsError, "Resumen del mensaje: " + ShortenMessage(mail.Body));
            if (mail.Body != null && mail.Body.Length > 0) m.body = mail.Body;

            ZTrace.WriteLineIf(ZTrace.IsError, "ID: " + mail.EntryID);
            m.entryId = mail.EntryID;
        }


        private static string getSenderEmailAddress(Outlook.MailItem mail)
        {
            Outlook.AddressEntry sender = mail.Sender;
            string SenderEmailAddress = "";

            if (sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry || sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "IsExchangeUser");
                Outlook.ExchangeUser exchUser = sender.GetExchangeUser();
                if (exchUser != null)
                {
                    SenderEmailAddress = exchUser.PrimarySmtpAddress;
                }
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Is NOT ExchangeUser");
                SenderEmailAddress = mail.SenderEmailAddress;
            }

            return SenderEmailAddress;
        }

        /// <summary>
        /// Obtiene las propiedades del Reporte de ReportItem y actualiza los valores del objeto m
        /// </summary>
        private static void GetProperties(Outlook.MailItem mail, Outlook.ReportItem reporte, IMail m)
        {
            string _senderName = Environment.UserName;
            //Obtiene las propiedades del mail.
            ZTrace.WriteLineIf(ZTrace.IsError, "Obteniendo las propiedades del ReportItem...");
            ZTrace.WriteLineIf(ZTrace.IsError, "Sender: " + _senderName);
            m.sender = _senderName;

            //if (reporte.To != null) m.to = reporte.To.Trim();
            ZTrace.WriteLineIf(ZTrace.IsError, "To: " + m.to);

            //if (mail.CC != null) m.CC = mail.CC.Trim();
            //ZTrace.WriteLineIf(ZTrace.IsError, "CC: " + mail.CC);

            //if (mail.BCC != null) m.BCC = mail.BCC.Trim();
            //ZTrace.WriteLineIf(ZTrace.IsError, "BCC: " + mail.BCC);

            ZTrace.WriteLineIf(ZTrace.IsError, "RecievedTime: " + reporte.LastModificationTime);
            m.receivedDate = reporte.LastModificationTime.Date.ToString();

            ZTrace.WriteLineIf(ZTrace.IsError, "Subject: " + reporte.Subject);
            m.subject = reporte.Subject.Trim();

            ZTrace.WriteLineIf(ZTrace.IsError, "Resumen del mensaje: " + ShortenMessage(reporte.Body));
            m.body = reporte.Body;

            ZTrace.WriteLineIf(ZTrace.IsError, "ID: " + reporte.EntryID);
            m.entryId = reporte.EntryID;
        }

        private static string ShortenMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return message;
            if (message.Length <= 1000) return message;
            return message.Substring(0, 1000);
        }

        /// <summary>
        /// Cambia el nombre del contacto por su dirección de mail.
        /// </summary>
        private static string ChangeNameToAddress(Outlook.MailItem mail, string addresses, Outlook.Application app)
        {
            //Se obtienela cantidad de destinatarios
            string[] sep = { ";" };

            //Se separan los mails y se remueven los caracteres inválidos
            addresses = addresses.Replace("'", String.Empty).Replace(" (", String.Empty).Replace(")", String.Empty).Replace("; ", ";");
            string[] contacts = addresses.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            sep = null;
            addresses = null;

            //Se remueven los posibles espacios generados por los reemplazos de caracteres
            for (int i = 0; i < contacts.Length; i++)
                contacts[i] = contacts[i].Trim();

            //Se crea una lista con los recipients para luego modificarla
            //a gusto sin depender de mail.Recipients que es readonly.
            //Se comienza por el 1 porque los Recipients comienzan desde ese índice.
            List<string[]> recipients = new List<string[]>();
            for (int i = 1; i <= mail.Recipients.Count; i++)
            {
                string[] tempArray = { String.Empty, String.Empty };
                tempArray[0] = mail.Recipients[i].Name.Replace("'", String.Empty).Replace(" (", String.Empty).Replace(")", String.Empty);

                string address = GetSMTPAddressByRecipient(mail.Recipients[i]);

                tempArray[1] = address != string.Empty ? address : mail.Recipients[i].Address;

                recipients.Add(tempArray);
            }

            //Por cada contacto, revisa si tiene que reemplazar el mail.
            StringBuilder sbTo = new StringBuilder();
            for (int i = 0; i < contacts.Length; i++)
                for (int j = 0; j < recipients.Count; j++)
                {
                    //Verifica si el nombre del contacto o la dirección de mail coinciden 
                    if (string.Compare(contacts[i], recipients[j][0]) == 0 || string.Compare(contacts[i], recipients[j][1]) == 0)
                    {
                        //Se agrega la dirección de mail.
                        sbTo.Append(recipients[j][1]);
                        sbTo.Append(";");

                        //Se remueve para que no busque nuevamente por este elemento.
                        recipients.RemoveAt(j);
                        break;
                    }
                }
            //Se remueve el último ';'.
            if (sbTo.Length > 0)
                sbTo.Remove(sbTo.Length - 1, 1);

            contacts = null;
            recipients.Clear();
            recipients = null;
            return sbTo.ToString().Trim();
        }

        private static string GetSMTPAddressByRecipient(Outlook.Recipient recip)
        {
            try
            {
                string smtpAddress = string.Empty;
                const string PR_SMTP_ADDRESS = "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";

                Outlook.PropertyAccessor pa = recip.PropertyAccessor;
                smtpAddress = pa.GetProperty(PR_SMTP_ADDRESS).ToString();

                return smtpAddress;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Determina si el archivo es un archivo de imagen.
        /// Este proceso se realiza segun la extension del mismo.
        /// </summary>
        private bool IsImage(string file)
        {
            string ext;
            ext = Path.GetExtension(file).ToLower();
            if (ext.Contains("jpg") || ext.Contains("gif") || ext.Contains("png") || ext.Contains("jpeg") || ext.Contains("bmp"))
                return true;
            else
                return false;
        }



        /// <summary>
        /// Marca un mail como no exportado
        /// </summary>
        public void SetError(IMail _mail)
        {
            Outlook.MailItem mail = null;
            if (!_mail.isReport)
            {
                mail = _mail.OriginalMail as Outlook.MailItem;
                mail.FlagRequest = "Error al exportar el mail a Zamba";
                mail.FlagIcon = Microsoft.Office.Interop.Outlook.OlFlagIcon.olRedFlagIcon;
                mail.Save();
                mail.Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olSave);
            }
        }



        /// <summary>
        /// Verifica si el mail ya se ha exportado a zamba.
        /// Para realizar la verificacion busca el texto 
        /// "El mail se ha exportado a zamba"
        /// </summary>
        public bool IsExported(object mail)
        {
            bool ret = false;

            if (mail is Outlook.MailItem)
            {
                Outlook.MailItem m = mail as Outlook.MailItem;
                if (!String.IsNullOrEmpty(m.FlagRequest))
                {
                    ret = m.FlagRequest.Contains(FlagRequestText);
                }
                else
                    ret = false;
                m = null;
            }
            else if (mail is Outlook.ReportItem)
            {
                Outlook.ReportItem r = mail as Outlook.ReportItem;
                ret = r.Subject.Contains(FlagRequestText);
                r = null;
            }

            return ret;
        }

        ///<summary>
        ///Verifica que el item se encuentre en la selección 
        ///</summary>
        ///<param name="sel"></param>
        ///<param name="mail"></param>
        ///<returns></returns>
        public bool CheckEntryId(Outlook.Selection sel, object mail)
        {
            ZTrace.WriteLineIf(ZTrace.IsError, "Verificando si el e-mail está en la selección actual...");
            bool ret = false;

            foreach (object item in sel)
            {
                if (mail is Outlook.MailItem && item is Outlook.MailItem)
                {
                    Outlook.MailItem _mail = (Outlook.MailItem)mail;
                    Outlook.MailItem _item = (Outlook.MailItem)item;
                    if (_mail.EntryID == _item.EntryID)
                    {
                        ret = true;
                        break;
                    }

                }
                else if (mail is Outlook.ReportItem && item is Outlook.ReportItem)
                {
                    Outlook.ReportItem _mail = (Outlook.ReportItem)mail;
                    Outlook.ReportItem _item = (Outlook.ReportItem)item;
                    if (_mail.EntryID == _item.EntryID)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            if (ret)
                ZTrace.WriteLineIf(ZTrace.IsError, "Se a encontrado el e-mail en la selección exitosamente !!");
            else
                ZTrace.WriteLineIf(ZTrace.IsError, "El e-mail NO se encuentra en la selección");

            ZTrace.WriteLineIf(ZTrace.IsError, "Verificación finalizada");
            return ret;
        }

        #endregion

        internal static FolderInfo SelectFolder(Outlook.Application app)
        {
            FolderInfo folder = new FolderInfo();
            Outlook.MAPIFolder mapi;

            try
            {
                //Muestra un form para elegir la carpeta.
                mapi = (Outlook.MAPIFolder)app.GetNamespace("MAPI").PickFolder();

                if (mapi != null)
                {
                    folder.Path = mapi.FullFolderPath;
                    folder.EntryId = mapi.EntryID;
                }
                else
                {
                    folder = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar la carpeta de outlook.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al seleccionar la carpeta de outlook.\n" + ex.ToString());
                folder = null;
            }
            finally
            {
                mapi = null;
            }

            return folder;
        }


        public List<String> ExtractAttachsAsFiles(string MsgFile, String newFilesDirectory)
        {
            List<String> Attachs = new List<String>();

            Zamba.Office.Outlook.OutlookInterop myOutlook = Zamba.Office.Outlook.SharedOutlook.GetOutlook();

            Outlook.MailItem currentMail = myOutlook.GetMailItemFromFile(MsgFile, false, FormWindowState.Minimized, false);

            if (currentMail != null)
            {
                //Obtiene los adjuntos del mail.
                ZTrace.WriteLineIf(ZTrace.IsError, "Attachments found: " + currentMail.Attachments.Count.ToString());

                foreach (Outlook.Attachment att in currentMail.Attachments)
                {
                    try
                    {
                        String storeName = Path.Combine(newFilesDirectory, att.FileName);
                        Int64 i = 0;
                        while (File.Exists(storeName))
                        {
                            i++;
                            storeName = Path.Combine(newFilesDirectory, Path.GetFileNameWithoutExtension(att.FileName) + "(" + i + ")" + Path.GetExtension(att.FileName));
                        }
                        if (storeName.Length > 254)
                            storeName = storeName.Substring(0, 254 - Path.GetExtension(att.FileName).Length) + Path.GetExtension(att.FileName);

                        //Guarda el adjunto        
                        att.SaveAsFile(storeName);
                        Attachs.Add(storeName);
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                }
            }
            return Attachs;
        }
    }
}
