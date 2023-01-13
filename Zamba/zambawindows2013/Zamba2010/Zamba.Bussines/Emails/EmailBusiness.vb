Imports Zamba.Data

Public Class EmailBusiness

    Inherits ZClass

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history>Modified Marcelo Se cambia el metodo para que utilice ZOPT bussines</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEmailExportPath() As String
        Try
            Return ZOptBusiness.GetValue("EMAILSPATH")
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history>Modified Marcelo Se cambia el metodo para que utilice ZOPT</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getExportBody() As Boolean
        Try
            Return ZOptBusiness.GetValue("EXPORT_EMAIL_BODY")
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history>Modified Marcelo Se cambia el metodo para que utilice ZOPT bussines</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getExportDoc() As Boolean
        Try
            Return ZOptBusiness.GetValue("EXPORT_EMAIL_DOC")
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
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
        Try
            getHistory = Email_Factory.getHistory(DocId)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Function


    Public Overrides Sub Dispose()

    End Sub


End Class
