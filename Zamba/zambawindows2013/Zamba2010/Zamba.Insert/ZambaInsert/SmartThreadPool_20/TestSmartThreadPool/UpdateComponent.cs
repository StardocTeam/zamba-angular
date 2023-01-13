using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using System.Threading;
using System.Data;
using System.Diagnostics;

namespace Zamba.ThreadPool
{
    public class UpdateComponent
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
      RemoteUpdate RI = new RemoteUpdate();
      public Int64 HandleDocumentsToUpdate(Int64 WorkItem)
    {
        Int64 RowsCount;
        lock (RI)
        {
            RowsCount = RI.ReserveUpdatesStatus(WorkItem);
        }
          return RowsCount;
        
    }

    Results_Business RB = new Results_Business();
    public Int64 UpdateDocuments(Int64 WorkItem)
    {
             
        DataTable Dt;
        lock (RI)
        {

            Dt = RI.GetDocumentsToRemoteUpdate(WorkItem);
            
            byte[] DocumentBynary = null;
            String CurrentName = String.Empty;
            Int64 CurrentDocTypeId = -1;
            String FileExtension = String.Empty;
            Int64 CurrentId = -1;
            Int64 TemporaryId = -1;
            Dictionary<NewResult, Byte[]> TaskList = new Dictionary<NewResult, Byte[]>();
            Int64 DocumentUpdated = 0;
           
             if (null != Dt)
                {
                    Trace.WriteLineIf(ZTrace.IsInfo,"Cantidad de documentos a actualizar: " + Dt.Rows.Count);
                   foreach (DataRow CurrentRow in Dt.Rows)
                    {
                                                          
                  TemporaryId = Int64.Parse(CurrentRow["TemporaryID"].ToString());
                  Trace.WriteLineIf(ZTrace.IsInfo,"TemporaryId: " + TemporaryId);
               
                  DataTable dtIndex;
                  dtIndex = RI.GetIndexKeysToRemoteUpdate(TemporaryId);
  
                             Dictionary<Int64, String> dcKeyIndex = new Dictionary<long,string>();  
 foreach (DataRow CurrentKeyRow in dtIndex.Rows)
 {
                    Trace.WriteLineIf(ZTrace.IsInfo,"Keys");
                    Trace.WriteLineIf(ZTrace.IsInfo,"IndexId: " + CurrentKeyRow["IndexId"].ToString());
                    Trace.WriteLineIf(ZTrace.IsInfo,"IndexValue: " + CurrentKeyRow["IndexValue"].ToString());
                    dcKeyIndex.Add(Int64.Parse(CurrentKeyRow["IndexId"].ToString()), CurrentKeyRow["IndexValue"].ToString());
 }


 dtIndex = RI.GetIndexToRemoteUpdate(TemporaryId);

                             Dictionary<Int64, String> dcValueIndex = new Dictionary<long,string>();  
             
 foreach (DataRow CurrentKeyRow in dtIndex.Rows)
 {
                                Trace.WriteLineIf(ZTrace.IsInfo,"Values");
                                Trace.WriteLineIf(ZTrace.IsInfo,"IndexId: " + CurrentKeyRow["IndexId"].ToString());
                                Trace.WriteLineIf(ZTrace.IsInfo,"IndexValue: " + CurrentKeyRow["IndexValue"].ToString());
                                dcValueIndex.Add(Int64.Parse(CurrentKeyRow["IndexId"].ToString()), CurrentKeyRow["IndexValue"].ToString());
 }


 RI.UpdateDocuments(Int64.Parse(CurrentRow["DocTypeId"].ToString()), dcKeyIndex, dcValueIndex, TemporaryId);
 DocumentUpdated++;
                        }
                   }
             return DocumentUpdated;
        }
     
    }



    private void SaveDocumentUpdated(Int64 temporaryId, Int64 ResultId)
    {
        RI.SaveDocumentRemoteUpdated(temporaryId);
    }
    private  void SaveDocumentError(Int64 temporaryId, String errorMessage)
    {
        RI.SaveDocumentRemoteUpdateError(temporaryId, errorMessage);
    }
    #endregion


    }
}
