using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using System.Threading;
using System.Data;
using System.Diagnostics;

namespace Zamba.Insert.Components
{
    public class InsertComponent
    {

    //      #region Attributes
  //  private Timer _timer = null;
  //  private TimerCallback _timerCallback = new TimerCallback(ZambaInsert.ExecuteService);
  //  private object _state = null;
   // #endregion

      public void OnStart()
    {
        try
        {
            UserBusiness.Rights.ValidateLogIn(Int32.Parse(UserPreferences.getValue("UserId", UserPreferences.Sections.UserPreferences, "0")));
           // _timer = new System.Threading.Timer(_timerCallback, _state, 60000, Int64.Parse(UserPreferences.getValue("SQLInsertTimer", UserPreferences.Sections.UserPreferences, "360000")));
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    //private  void ExecuteService(Object state)
    //{
    //    try
    //    {
    //        InsertComponent.HandleDocumentsToInsert();
    //    }
    //    catch (Exception ex)
    //    {
    //        ZClass.raiseerror(ex);
    //    }
    //}

    #region Handle Documents
      RemoteInsert RI = new RemoteInsert();
    public Int64 HandleDocumentsToInsert(Int64 WorkItem)
    {
        Int64 RowsCount;
        lock (RI)
        {
            RowsCount = RI.ReserveInsertsStatus(WorkItem);
        }
          return RowsCount;
        
    }

    Results_Business RB = new Results_Business();
    public Int64 InsertDocuments(Int64 WorkItem)
    {
        DataTable Dt;
        lock (RI)
        {
            Dt = RI.GetDocumentsToInsert(WorkItem);

            byte[] DocumentBynary = null;
            String CurrentName = String.Empty;
            Int64 CurrentDocTypeId = -1;
            String FileExtension = String.Empty;
            //  Dictionary<Int64, string> CurrentIndexs = new Dictionary<Int64, string>();
            Int64 CurrentId = -1;
            Int64 TemporaryId = -1;
            Dictionary<NewResult, Byte[]> TaskList = new Dictionary<NewResult, Byte[]>();
            Int64 DocumentInserted = 0;

            try
            {
                if (null != Dt)
                {
                    // foreach (DataRow CurrentRow in Dt.Rows)
                    // {
                    //     TemporaryId = Int64.Parse(CurrentRow["TemporaryID"].ToString());
                    //RemoteInsert.SaveDocumentStatus(TemporaryId, 4, 0);
                    // }

                    foreach (DataRow CurrentRow in Dt.Rows)
                    {
                        try
                        {
                            TemporaryId = Int64.Parse(CurrentRow["TemporaryID"].ToString());
                            CurrentId = TemporaryId;

                            CurrentDocTypeId = Int64.Parse(CurrentRow["DocTypeId"].ToString());
                            CurrentName = CurrentRow["DocumentName"].ToString();
                            FileExtension = CurrentRow["FileExtension"].ToString();
                            if (!(CurrentRow["SerializedFile"] is DBNull))
                            {
                                DocumentBynary = (Byte[])CurrentRow["SerializedFile"];
                            }
                                                   DataTable dtIndex;


                            dtIndex = RI.GetIndexToRemoteInsert(TemporaryId);


                            if (DocTypesList.ExistsDocType(CurrentDocTypeId))
                            {
                                Int64 ResultId = RI.Insert(CurrentName, DocumentBynary, FileExtension, CurrentDocTypeId, dtIndex);
                                Trace.WriteLine("Mail del usuario: " + UserBusiness.CurrentUser().ID);
                                SaveDocumentInserted(CurrentId, ResultId);
                            }
                            else
                                SaveDocumentError(CurrentId, "No existe el Id de Tipo de Documento " + CurrentDocTypeId.ToString());

                            DocumentInserted++;
                        }
                        catch (Exception ex)
                        {
                            SaveDocumentError(CurrentId, ex.ToString());
                            Zamba.Core.ZClass.raiseerror(ex);
                        }
                    }
                }
              //  RemoteUpdate RU = new RemoteUpdate();
               // RU.RunDocumentUpdates();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            finally
            {
                #region Dispose

                if (null != DocumentBynary)
                {
                    Array.Clear(DocumentBynary, 0, DocumentBynary.Length);
                    DocumentBynary = null;
                }

                //if (null != CurrentIndexs)
                //{
                //    CurrentIndexs.Clear();
                //    CurrentIndexs = null;
                //}

                if (null != TaskList)
                {
                    TaskList.Clear();
                    TaskList = null;
                }

                Dt.Dispose();
                Dt = null;
                #endregion
            }
            return DocumentInserted;
        }
    }



    private  void SaveDocumentInserted(Int64 temporaryId,Int64 ResultId)
    {
        RI.SaveDocumentInserted(temporaryId,ResultId);
    }
    private  void SaveDocumentError(Int64 temporaryId, String errorMessage)
    {
        RI.SaveDocumentError(temporaryId, errorMessage);
    }
    #endregion


    }
}
