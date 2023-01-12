Public Class FrmGeneralRulesABM
    Implements IDisposable

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub InitializeData(Optional ByVal title As String = "", _
                              Optional ByVal buttonTypeFilter As ButtonType = ButtonType.All, _
                              Optional ByVal wfId As Int64 = 0)
        UcButtons.InitializeData(title, buttonTypeFilter, wfId)
    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If UcButtons IsNot Nothing Then
                    UcButtons.Dispose()
                    UcButtons = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
End Class