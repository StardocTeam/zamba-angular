Public Interface IChromiumQuickSearch

  'sub SelectResult(ByVal docTypeId As Integer,
  '                               ByVal docId As Integer,
  '                               Optional ByVal taskId As Integer = 0,
  '                              Optional ByVal stepId As Integer = 0) 
    sub DoAction(ByVal action As string) 

 '   sub DownloadFile(ByVal file As string) 

    Function ShowMessage(ByVal title As string, ByVal message As string) as String
  
End Interface
