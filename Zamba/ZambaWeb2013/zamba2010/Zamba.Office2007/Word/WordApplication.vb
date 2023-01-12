Imports Microsoft.Office.Interop.Word
Imports System.Runtime.InteropServices.Marshal

Public Class WordApplication
    Implements IDisposable

    Private _app As Application
    Public ReadOnly Property App() As Application
        Get
            Return _app
        End Get
    End Property

    Public Property Visible() As Boolean
        Get
            Return _app.Visible
        End Get
        Set(ByVal value As Boolean)
            _app.Visible = value
        End Set
    End Property

    Public Sub New()
        _app = New Application()
    End Sub

    Public Function OpenDocument(ByVal path As String) As Object
        Dim nada As Object = Type.Missing
        Return _app.Documents.Open(path, nada, nada, nada, nada, nada, nada, nada, nada, nada, nada, nada, nada, nada, nada, nada)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If _app IsNot Nothing Then
                    If _app.Documents IsNot Nothing AndAlso _app.Documents.Count > 0 Then
                        For i As Int32 = 0 To _app.Documents.Count - 1
                            Try
                                _app.Documents(i).Close(False)
                            Catch 
                            End Try
                        Next
                    End If

                    Try
                        _app.Quit(False)
                        ReleaseComObject(_app)
                        _app = Nothing
                    Catch
                    End Try
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
