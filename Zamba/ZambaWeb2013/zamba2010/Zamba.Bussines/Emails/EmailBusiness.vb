Imports Zamba.Data
Imports Zamba.Core
Imports System.Collections.Generic

Public Class EmailBusiness

    Inherits ZClass

    Public Shared Function GetEmailExportPath() As String
        Return Email_Factory.GetEmailExportPath()
    End Function

    Public Shared Function GetMailHistoryEnabled() As Boolean
        GetMailHistoryEnabled = Email_Factory.GetMailHistoryEnabled()
    End Function

    Public Shared Sub SaveEmailExportPath(ByVal path As String)
        Email_Factory.SaveEmailExportPath(path)
    End Sub

    Public Shared Sub UpdateEmailExportPath(ByVal path As String)
        Email_Factory.UpdateEmailExportPath(path)
    End Sub

    Public Shared Function getExportBody() As Boolean
        getExportBody = Email_Factory.getExportBody()
    End Function

    Public Shared Function getExportDoc() As Boolean
        getExportDoc = Email_Factory.getExportDoc()
    End Function

    Public Shared Sub saveExportBody(ByVal value As Boolean)
        Email_Factory.SaveExportBody(value)
    End Sub

    Public Shared Sub saveExportDoc(ByVal value As Boolean)
        Email_Factory.SaveExportDoc(value)
    End Sub

    Public Shared Sub saveMailHistory(ByVal value As Boolean)
        Email_Factory.SaveMailHistory(value)
    End Sub

    Public Shared Function getHistory(ByVal DocId As Long) As DataSet
        getHistory = Email_Factory.getHistory(DocId)
    End Function

    Public Shared Function GetMailExtension(ByVal id As Int64) As String
        Dim path As String = Email_Factory.GetMailPath(id)
        Return IO.Path.GetExtension(path)
    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Function getAllImapProcesses() As DataTable
        Dim path As DataTable = Email_Factory.getAllImapProcesses()
        Return path
    End Function

    Public Function getImapProcess(Id As Integer) As DataTable
        Dim path As DataTable = Email_Factory.getImapProcess(Id)
        Return path
    End Function

    Public Function InsertObjectImap(dtoObjectImap As Zamba.Core.DTOObjectImap) As Boolean
        Dim path As Boolean = Email_Factory.InsertImapObject(dtoObjectImap)
        Return path
    End Function
    Public Function DeleteProcessImap(ByVal processId As Int64) As Boolean
        Dim path As Boolean = Email_Factory.DeleteProcessImap(processId)
        Return path
    End Function
    Public Function UpdateImapProcess(dtoObjectImap As Zamba.Core.DTOObjectImap) As Boolean
        Dim path As Boolean = Email_Factory.UpdateImapProcess(dtoObjectImap)
        Return path
    End Function

    Public Function SetProcessActiveState(processId As Int64, isActiveState As Int64) As Boolean
        Dim path As Boolean = Email_Factory.SetProcessActiveState(processId, isActiveState)
        Return path
    End Function

    Public Function GetEmailsUsersOfTask(docIds As List(Of String)) As Object
        Dim result As DataTable = Email_Factory.GetEmailsUsersOfTask(docIds)
        Return result
    End Function
End Class