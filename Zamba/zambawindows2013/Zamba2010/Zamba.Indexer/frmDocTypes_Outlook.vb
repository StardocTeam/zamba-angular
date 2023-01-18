Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Controls
Imports Zamba.Tools



''' <summary>
''' Esta clase crea un form y muestra los tipos de documentos del usuario logueado actualmente.
''' </summary>
''' <remarks></remarks>
Public Class frmDocTypes_Outlook

    Public UCDocTypes As UCDocTypes
    Public m_Error As String = String.Empty


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        UCDocTypes = New UCDocTypes()
        UCDocTypes.Dock = DockStyle.Fill
        Panel1.SuspendLayout()
        Panel1.Controls.Add(UCDocTypes)
        Panel1.ResumeLayout()
        Me.Visible = False
    End Sub

    Private Sub frmDocTypes_Outlook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Zamba.Core.Users.User.CurrentUser = UserBusiness.GetUserByName(Environment.UserName)

        Try
            Dim doctypes As ArrayList = DocTypesBusiness.GetDocTypesbyUserRightsOfView(Zamba.Core.UserBusiness.CurrentUser.ID, RightsType.View)
            If doctypes.Count() > 0 Then
                UCDocTypes.ListBox1.DataSource = doctypes
                UCDocTypes.ListBox1.DisplayMember = "Name"
                UCDocTypes.ListBox1.ValueMember = "Id"
            End If
            Me.Visible = True
        Catch ex As Exception
            m_Error = ex.Message
            Me.Close()
        End Try
    End Sub

    Private Sub SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub cmd_Cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Cancelar.Click
        Me.Close()
    End Sub
End Class