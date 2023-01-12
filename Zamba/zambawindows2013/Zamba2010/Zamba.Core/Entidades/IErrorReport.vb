Imports System.Collections.Generic
Imports Zamba.Core

Public Interface IErrorReport
    Property Attachments As List(Of ErrorReportAttachment)
    Property Comments As String
    Property Created As Date
    Property Description As String
    Property Id As Long
    Property Machine As String
    Property State As ErrorReportStates
    ReadOnly Property StateDescription As String
    Property Subject As Object
    Property Updated As Date
    Property UserId As Long
    Property Version As String
    Property WinUser As String
    Sub Dispose()
End Interface
