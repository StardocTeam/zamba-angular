using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
//using MTOM_Library;
using System.Web.UI;


namespace Zamba.WebActiveX
{
    //Declaro el delegado que va a manejar el agregar de los steps en el arbol
    public delegate void ClosedActivex();
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [ProgId("Dendrite.WebForce.MMP.Web.OurActiveX")]
    [ClassInterface(ClassInterfaceType.AutoDual), ComSourceInterfaces(typeof(ControlEvents))] //Implementing interface that will be visible from JS
    [Guid("121C3E0E-DC6E-45dc-952B-A6617F0FAA32")]
    [ComVisible(true)]
    public class ActiveXObject
    {
        private string myParam = "Empty";

        public ActiveXObject()
        {
        }

        //Declaro una variable del delegado
        private ClosedActivex dClosedActivex = null;
        public event ControlEventHandler OnClose;

        /// <summary>
        /// Opens application. Called from JS
        /// </summary>
        [ComVisible(true)]
        public void Open()
        {
            //TODO: Replace the try catch in aspx with try catch below. The problem is that js OnClose does not register.
            try
            {
                Microsoft.Office.Interop.Word.ApplicationClass WA = new Microsoft.Office.Interop.Word.ApplicationClass();
                WA.Visible = true;
                //                WA.Activate();
                object missing = System.Reflection.Missing.Value;
                object param = (object)File;
                WA.Documents.Open(ref param, ref missing, ref missing, ref missing, ref missing, ref  missing, ref  missing, ref  missing, ref missing, ref missing, ref missing, ref  missing, ref missing, ref missing, ref  missing, ref missing);
                //                WA.Activate();
                //MessageBox.Show(myParam); //Show param that was passed from JS

                WA.DocumentBeforeClose += new Microsoft.Office.Interop.Word.ApplicationEvents4_DocumentBeforeCloseEventHandler(WA_DocumentBeforeClose);
                WA.DocumentBeforeSave += new Microsoft.Office.Interop.Word.ApplicationEvents4_DocumentBeforeSaveEventHandler(WA_DocumentBeforeSave);

                //                This code will write the file to the screen as an excel file
                //Response.Clear()
                //Response.ContentType = "application/vnd.ms-excel"
                //Response.AddHeader("Content-Disposition", _
                //    "attachment; filename=bom.xls")
                //Response.WriteFile(filepathgoeshere)
                //Response.Flush()
                //Response.Close()
            }
            catch (Exception e)
            {
                //ExceptionHandling.AppException(e);
                throw e;
            }
        }

        public void WA_DocumentBeforeClose(Microsoft.Office.Interop.Word.Document Doc, ref bool Cancel)
        {
            String Dir = "C:\\temp\\";
            //SaveLocalFile(Doc);
            if (System.IO.Directory.Exists(Dir) == false) System.IO.Directory.CreateDirectory(Dir);
            LocalFile = Dir + "doc1.docx";
            UploadDocument(LocalFile);
            //todo tirar el evento
            if (this.dClosedActivex != null)
                dClosedActivex();
        }
        String LocalFile = String.Empty;
        protected void WA_DocumentBeforeSave(Microsoft.Office.Interop.Word.Document Doc, ref bool SaveAsUI, ref bool Cancel)
        {
            Cancel = true;
            SaveLocalFile(Doc);
        }
        protected void SaveLocalFile(Microsoft.Office.Interop.Word.Document Doc)
        {
            String Dir = "C:\\temp\\";
            if (System.IO.Directory.Exists(Dir) == false) System.IO.Directory.CreateDirectory(Dir);
            LocalFile = Dir + "doc1.docx";
            object TempFile = (object)LocalFile;
            object missing = System.Reflection.Missing.Value;
            Doc.SaveAs(ref TempFile, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
        }
        String mfile = String.Empty;
        /// <summary>
        /// Parameter visible from JS
        /// </summary>
        [ComVisible(true)]
        public String File
        {
            get
            {
                return mfile;
            }
            set
            {
                mfile = value;
            }
        }


        //   public static MTOM_Library.MtomWebService.MTOM WebService;

        [ComVisible(true)]
        public void Save()
        {
            ////////            WebService = new MTOM_Library.MtomWebService.MTOM();

            ////////            //// get the list of files to download from the server
            ////////            //workerGetFileList.RunWorkerAsync();

            ////////            // set the default save folder
            ////////            this.txtSaveFolder.Text = Application.StartupPath;

            ////////            // configure the 'TaskPanel' which is used to dynamically show a progress bar + status message for each file transfer operation
            ////////            this.taskPanel1.RemoveItemsWhenFinished = true;
            ////////            this.taskPanel1.RemoveItemsOnError = false;
            ////////            this.taskPanel1.AutoSizeForm = false;

            ////////            // init the ThreadPool MaxThread size to the value in the control
            ////////            this.dudMaxThreads_ValueChanged(sender, e);



            ////////            string guid = (triplet as Triplet).First.ToString();
            ////////            string path = "C:\\Temp\\Doc1.docx"; ;
            ////////        long offset = Int64.Parse((triplet as Triplet).Third.ToString());

            ////////            FileTransferUpload ftu = new FileTransferUpload();
            ////////            ftu.WebService.CookieContainer = WebService.CookieContainer;	// copy the CookieContainer into the transfer object (for auth cookie, if relevant)
            ////////            ftu.Guid = guid;
            ////////            // set up the chunking options
            ////////           // if (this.chkAutoChunksize.Checked)
            ////////                ftu.AutoSetChunkSize = true;
            ////////          //  else
            ////////          //  {
            ////////            //    ftu.AutoSetChunkSize = false;
            ////////             //   ftu.ChunkSize = (int)this.dudChunkSize.Value * 1024;	// kb
            ////////          //  }

            ////////            // set the remote file name and start the background worker
            ////////            ftu.LocalFilePath = path;
            //////////            ftu.IncludeHashVerification = this.chkHash.Checked;
            ////////            ftu.IncludeHashVerification = false;
            ////////  //          ftu.ProgressChanged += new ProgressChangedEventHandler(ft_ProgressChanged);
            ////////    //        ftu.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ft_RunWorkerCompleted);
            ////////            ftu.RunWorkerSync(new DoWorkEventArgs(offset));
        }







        [ComVisible(true)]
        public void Close()
        {
            if (OnClose != null)
            {
            }
            else
            {
            }
        }



        ///	<summary>
        ///	Register the class as a	control	and	set	it's CodeBase entry
        ///	</summary>
        ///	<param name="key">The registry key of the control</param>
        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(key);

            sb.Replace(@"HKEY_CLASSES_ROOT\", "");
            // Open the CLSID\{guid} key for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // And create	the	'Control' key -	this allows	it to show up in
            // the ActiveX control container
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Next create the CodeBase entry	- needed if	not	string named and GACced.
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();
            // Finally close the main	key
            k.Close();
            MessageBox.Show("Registered");
        }

        ///	<summary>
        ///	Called to unregister the control
        ///	</summary>
        ///	<param name="key">Tke registry key</param>
        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open	HKCR\CLSID\{guid} for write	access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Delete the 'Control'	key, but don't throw an	exception if it	does not exist
            k.DeleteSubKey("Control", false);

            // Next	open up	InprocServer32
            //RegistryKey	inprocServer32 = 
            k.OpenSubKey("InprocServer32", true);

            // And delete the CodeBase key,	again not throwing if missing
            k.DeleteSubKey("CodeBase", false);

            // Finally close the main key
            k.Close();
            MessageBox.Show("UnRegistered");
        }

        // ====================================================================================================================================
        /// <summary>
        /// Uploads the specified Word Document.
        /// The document will be uploaded to the server and will be stored in the path specified as "ServerLocalPath"
        /// </summary>
        /// <param name="FullFileName">Specifies the Full Path (local path) of the Word Document</param>
        // ====================================================================================================================================
        public void UploadDocument(string FullFileName)
        {
            string thisFile = FullFileName;

            Stardoc.svcWordDocumentUploader.FileUploader oFileUploader = new Stardoc.svcWordDocumentUploader.FileUploader();

            #region Gather File Information

            int iFileSize = 0;
            string strFileName = null;
            string strShortName = null;
            System.IO.FileInfo oInfo = null;
            try
            {
                oInfo = new System.IO.FileInfo(thisFile);
                iFileSize = (int)oInfo.Length;
                strFileName = oInfo.FullName;
                strShortName = oInfo.Name;
            }
            catch (System.Exception ex)
            {
                oInfo = null;
                throw new System.Exception("Error while trying to get file information: " + ex.ToString(), ex);
            }

            #endregion Gather File Information

            #region Retrieve File Contents

            System.IO.FileStream fs = null;

            // Open the Stream
            try
            {
                fs = new System.IO.FileStream(strFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Error while accesing the file: " + ex.ToString(), ex);
            }

            // Read the stream
            byte[] arrFileData = null;
            try
            {
                arrFileData = new byte[iFileSize];
                fs.Read(arrFileData, 0, iFileSize);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Error while reading file contents: " + ex.ToString(), ex);
            }

            // Close the stream
            try { fs.Close(); }
            catch (System.Exception) { }

            fs = null;

            #endregion Retrieve File Contents

            #region Upload the data

            // Upload the file using normal HTTP traffic
            oFileUploader.UploadData(iFileSize, arrFileData, this.File, true);

            #endregion Upload the data

            oFileUploader = null;
        }

        public event ClosedActivex OnClosedActivex
        {
            add
            {
                this.dClosedActivex += value;
            }
            remove
            {
                this.dClosedActivex -= value;

            }
        }
    }

    /// <summary>
    /// Event handler for events that will be visible from JavaScript
    /// </summary>
    public delegate void ControlEventHandler(string redirectUrl);


    /// <summary>
    /// This interface shows events to javascript
    /// </summary>
    [Guid("68BD4E0D-D7BC-4cf6-BEB7-CAB950161E79")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ControlEvents
    {
        //Add a DispIdAttribute to any members in the source interface to specify the COM DispId.
        [DispId(0x60020001)]
        void OnClose(string redirectUrl); //This method will be visible from JS
    }


}