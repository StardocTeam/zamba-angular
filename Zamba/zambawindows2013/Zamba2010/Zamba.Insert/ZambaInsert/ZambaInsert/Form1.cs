using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Zamba.Core;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ZambaInsert
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UserBusiness.Rights.ValidateLogIn(Int32.Parse(UserPreferences.getValue("UserId", UserPreferences.Sections.UserPreferences, "0")));
            HandleDocumentsToInsert();
            Close();
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            HandleDocumentsToInsert();
        }
        #region Handle Documents

        RemoteInsert RI = new RemoteInsert();
        RemoteUpdate RU = new RemoteUpdate();

        private static void HandleDocumentsToInsert()
        {
            //////try
            //////{
            //////    Trace.Listeners.Add(new TextWriterTraceListener(Application.StartupPath + "\\Exceptions\\Trace Insert - " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + ".log", "Insert"));
            //////    Trace.AutoFlush = true;
            //////    Trace.WriteLine("Insertando");
            //////    DataTable Dt = RI.GetDocumentsToInsert(3);
            //////    byte[] DocumentBynary = null;
            //////    String CurrentName = String.Empty;
            //////    Int64 CurrentDocTypeId = -1;
            //////    String FileExtension = String.Empty;
            //////  //  Dictionary<Int64, string> CurrentIndexs = new Dictionary<Int64, string>();
            //////    Int64 CurrentId = -1;
            //////    Int64 TemporaryId = -1;
            //////    Dictionary<NewResult, Byte[]> TaskList = new Dictionary<NewResult, Byte[]>();

            //////    try
            //////    {
            //////        if (null != Dt && Dt.Rows.Count > 0)
            //////        {
            //////            Trace.WriteLine("Documentos a insertar:" + Dt.Rows.Count);
            //////            foreach (DataRow CurrentRow in Dt.Rows)
            //////            {
            //////                TemporaryId = Int64.Parse(CurrentRow["TemporaryID"].ToString());
            //////                Trace.WriteLine("TemporaryID: " + TemporaryId);

            //////                CurrentId = TemporaryId;

            //////                CurrentDocTypeId = Int64.Parse(CurrentRow["DocTypeId"].ToString());
            //////                CurrentName = CurrentRow["DocumentName"].ToString();
            //////                Trace.WriteLine("Name: " + CurrentName);
            //////                FileExtension = CurrentRow["FileExtension"].ToString();
            //////                Trace.WriteLine("Extension: " + FileExtension);
            //////                try
            //////                {
            //////                    if (CurrentRow["SerializedFile"] != DBNull.Value)
            //////                        DocumentBynary = (Byte[])CurrentRow["SerializedFile"];
            //////                    Trace.WriteLine("Serializado Correcto");
            //////                }
            //////                catch
            //////                {
            //////                    Trace.WriteLine("Error en el serializado");
            //////                }

            //////                DataTable dtIndex = RI.GetIndexToRemoteInsert(TemporaryId);
            //////                if (dtIndex != null)
            //////                {
            //////                    Trace.WriteLine("Cantidad de indices:" + dtIndex.Rows.Count);
            //////                    //foreach (DataRow drIndex in dtIndex.Rows)
            //////                    //{
            //////                    //    Trace.WriteLine("IndexID: " + Int64.Parse(drIndex["IndexId"].ToString()));
            //////                    //    Trace.WriteLine("IndexValue: " + drIndex["IndexValue"].ToString());
            //////                    //    CurrentIndexs.Add(Int64.Parse(drIndex["IndexId"].ToString()), drIndex["IndexValue"].ToString());
            //////                    //}

            //////                    if (DocTypesList.ExistsDocType(CurrentDocTypeId))
            //////                    {
            //////                        Trace.WriteLine("Insertando");
            //////                        Trace.WriteLine("§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§");
            //////                        Int64 Resultid = Results_Business.Insert(CurrentName, DocumentBynary, FileExtension, CurrentDocTypeId, dtIndex);
            //////                        Trace.WriteLine("§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§");
            //////                        Trace.WriteLine("ResultId: " + Resultid);
            //////                        if (Resultid > 0)
            //////                        {
            //////                            Trace.WriteLine("Salvando Estado");
            //////                            SaveDocumentInserted(CurrentId, Resultid);
            //////                        }
            //////                    }
            //////                    else
            //////                    {
            //////                        Trace.WriteLine("No existe el Id de Tipo de Documento " + CurrentDocTypeId.ToString());
            //////                        SaveDocumentError(CurrentId, "No existe el Id de Tipo de Documento " + CurrentDocTypeId.ToString());
            //////                    }

            //////                   // CurrentIndexs.Clear();
            //////                }
            //////            }
            //////        }
            //////    }
            //////    catch (Exception ex)
            //////    {
            //////        Trace.WriteLine(ex.ToString());
            //////    }
            //////    finally
            //////    {
            //////        #region Dispose

            //////        if (null != DocumentBynary)
            //////        {
            //////            Array.Clear(DocumentBynary, 0, DocumentBynary.Length);
            //////            DocumentBynary = null;
            //////        }

            //////        //if (null != CurrentIndexs)
            //////        //{
            //////        //    CurrentIndexs.Clear();
            //////        //    CurrentIndexs = null;
            //////        //}

            //////        if (null != TaskList)
            //////        {
            //////            TaskList.Clear();
            //////            TaskList = null;
            //////        }

            //////        Dt.Dispose();
            //////        Dt = null;
            //////        #endregion
            //////    }
            //////    RU.RunDocumentUpdates();
            //////}
            //////finally
            //////{
            //////    if (Trace.Listeners["Insert"] != null)
            //////    {
            //////        Trace.Listeners["Insert"].Close();
            //////        Trace.Listeners["Insert"].Dispose();
            //////    }
            //////}
        }


        private static void SaveDocumentInserted(Int64 temporaryId, Int64 ResultId)
        {
            ////RI.SaveDocumentInserted(temporaryId, ResultId);
        }
        private static void SaveDocumentError(Int64 temporaryId, String errorMessage)
        {
            ////RI.SaveDocumentError(temporaryId, errorMessage);
        }

        //public static void Insert(Dictionary<NewResult, Byte[]> documents)
        //{
        //    Dictionary<Int64, String> Indexs = new Dictionary<long, string>();
        //    List<Int64> DocumentsIds = new List<long>(documents.Keys.Count);

        //    foreach (NewResult CurrentNewResult in documents.Keys)
        //    {
        //        Indexs.Clear();
        //        DocumentsIds.Add(CurrentNewResult.ID);

        //        foreach (Index myIndex in CurrentNewResult.Indexs)
        //        {
        //            if (!Indexs.ContainsKey(myIndex.ID))
        //                Indexs.Add(myIndex.ID, myIndex.Data);
        //        }

        //        Results_Business.Insert(CurrentNewResult.Name, documents[CurrentNewResult], CurrentNewResult.DocType.ID, Indexs);
        //    }
        //}


        //'Inserta los documentos temporales en zamba
        //Private Shared Sub Insert(ByVal documents As Dictionary(Of NewResult, Byte()))
        //    Dim Indexs As New Dictionary(Of Int64, String)
        //    Dim DocumentsIds As New List(Of Int64)(documents.Keys.Count)

        //    For Each CurrentNewResult As NewResult In documents.Keys
        //        Indexs.Clear()

        //        DocumentsIds.Add(CurrentNewResult.ID)

        //        For Each myIndex As Index In CurrentNewResult.Indexs
        //            If Not Indexs.ContainsKey(myIndex.ID) Then
        //                Indexs.Add(myIndex.ID, myIndex.Data)
        //            End If
        //        Next

        //        Insert(CurrentNewResult.Name, documents.Item(CurrentNewResult), CurrentNewResult.DocType.ID, Indexs)
        //    Next

        //    Results_Factory.RemoveDocumentsToInsert(DocumentsIds)
        //End Sub 
        #endregion
    }
}