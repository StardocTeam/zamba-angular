Imports System.Collections.Generic

<Serializable>
Public Class ErrorReport
    Implements IDisposable

    Public Property Id() As Int64
    Public Property Subject()
    Public Property Description() As String
    Public Property State() As ErrorReportStates
    Public Property UserId() As Int64
    Public Property Created() As DateTime
    Public Property Updated() As DateTime
    Public Property Comments() As String
    Public Property Attachments() As List(Of ErrorReportAttachment)
    Public Property Machine() As String
    Public Property Version() As String
    Public Property WinUser() As String
    Public ReadOnly Property StateDescription() As String
        Get
            Return [Enum].GetName(GetType(ErrorReportStates), _State)
        End Get
    End Property

    Public Sub New()
        _Attachments = New List(Of ErrorReportAttachment)
    End Sub

    Public Sub New(ByVal subject As String, _
                   ByVal description As String)
        _Subject = subject
        _Description = description
        If Membership.MembershipHelper.CurrentUser IsNot Nothing Then
            _UserId = Membership.MembershipHelper.CurrentUser.ID
        Else
            _UserId = 0
        End If
        _Attachments = New List(Of ErrorReportAttachment)
            _WinUser = Environment.UserName
        _Machine = Environment.MachineName
        _Version = My.Application.Info.Version.ToString
    End Sub

    Public Sub New(ByVal id As Int64, _
                   ByVal subject As String, _
                   ByVal description As String, _
                   ByVal state As ErrorReportStates, _
                   ByVal userId As Int64, _
                   ByVal created As DateTime, _
                   ByVal updated As DateTime, _
                   ByVal comments As String, _
                   ByVal winUser As String, _
                   ByVal machine As String, _
                   ByVal version As String)
        _Id = id
        _Subject = subject
        _Description = description
        _Comments = comments
        _State = state
        _UserId = userId
        _Created = created
        _Updated = updated
        _Attachments = New List(Of ErrorReportAttachment)
        _WinUser = winUser
        _Machine = machine
        _Version = version
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If _Attachments IsNot Nothing Then
                    For i As Int32 = 0 To _Attachments.Count - 1
                        _Attachments(i).Dispose()
                        _Attachments(i) = Nothing
                    Next
                    _Attachments.Clear()
                    _Attachments = Nothing
                End If
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
