Imports Zamba.AdminControls
Imports Zamba.Core


Public Class frmSelectUsersForo
    Inherits Zamba.AppBlock.ZForm

    Friend WithEvents ucPrincipal As UCSelectUsersForo

    Public Sub New(ByVal typeId As GroupToNotifyTypes, ByVal msgId As Int64)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.ucPrincipal = New UCSelectUsersForo(typeId, msgId)
        Me.Controls.Add(Me.ucPrincipal)
        Me.ucPrincipal.Dock = DockStyle.Fill
    End Sub

    Public Sub ucPrincipal_eCerrar(ByVal OK As Boolean) Handles ucPrincipal.eCerrar
        If OK Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
        Me.Close()
    End Sub

    Public ReadOnly Property notifyIds() As Generic.List(Of Int64)
        Get
            If Not IsNothing(ucPrincipal) Then
                Return ucPrincipal.notifyIds
            Else
                Return Nothing
            End If
        End Get
    End Property
End Class