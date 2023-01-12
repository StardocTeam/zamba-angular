Public Class ZToolBar
    Inherits System.Windows.Forms.ToolStrip
    Private components As System.ComponentModel.IContainer

    Public Sub New()
        MyBase.New
    End Sub

    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ZToolBar))

    End Sub

    Const WM_LBUTTONDOWN As UInteger = &H201
    Const WM_LBUTTONUP As UInteger = &H202

    Private Shared down As Boolean = False

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_LBUTTONUP AndAlso Not down Then
            m.Msg = CInt(WM_LBUTTONDOWN)
            MyBase.WndProc(m)
            m.Msg = CInt(WM_LBUTTONUP)
        End If

        If m.Msg = WM_LBUTTONDOWN Then
            down = True
        End If
        If m.Msg = WM_LBUTTONUP Then
            down = False
        End If

        MyBase.WndProc(m)
    End Sub
End Class
