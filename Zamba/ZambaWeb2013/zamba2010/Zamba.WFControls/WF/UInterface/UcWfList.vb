
Imports Zamba.Core
Imports Zamba.Data
Public Class UcWfList
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        LoadWfs()
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents chklist As System.Windows.Forms.CheckedListBox
    Friend WithEvents BtnServer As ZButton
    Friend WithEvents BtnCancell As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UcWfList))
        chklist = New System.Windows.Forms.CheckedListBox
        BtnServer = New ZButton
        BtnCancell = New ZButton
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'chklist
        '
        chklist.BackColor = System.Drawing.Color.White
        chklist.Location = New System.Drawing.Point(16, 16)
        chklist.Name = "chklist"
        chklist.Size = New System.Drawing.Size(224, 276)
        chklist.TabIndex = 0
        '
        'BtnServer
        '
        BtnServer.DialogResult = System.Windows.Forms.DialogResult.None
        BtnServer.Location = New System.Drawing.Point(256, 24)
        BtnServer.Name = "BtnServer"
        BtnServer.Size = New System.Drawing.Size(96, 48)
        BtnServer.TabIndex = 2
        BtnServer.Text = "Iniciar Servicio"
        '
        'BtnCancell
        '
        BtnCancell.DialogResult = System.Windows.Forms.DialogResult.None
        BtnCancell.Location = New System.Drawing.Point(256, 264)
        BtnCancell.Name = "BtnCancell"
        BtnCancell.Size = New System.Drawing.Size(96, 23)
        BtnCancell.TabIndex = 4
        BtnCancell.Text = "Cancelar"
        '
        'UcWfList
        '
        Controls.Add(BtnCancell)
        Controls.Add(BtnServer)
        Controls.Add(chklist)
        Name = "UcWfList"
        Size = New System.Drawing.Size(376, 320)
        ResumeLayout(False)

    End Sub

#End Region

    Dim dsWf As dsWf
    Private Sub LoadWfs()
        dsWf = WFFactory.GetWFs(CInt(UserBusiness.Rights.CurrentUser.ID))
        Dim I As Int16
        For I = 0 To CShort(dsWf.WF.Count - 1)
            chklist.Items.Add(dsWf.WF(I))
            chklist.DisplayMember = "Name"
            chklist.ValueMember = "Work_ID"
        Next
    End Sub
    '    Dim TService As Z159.WorkFlow.WFService.Tipo
    Public Event Started(ByVal Array As System.Collections.Generic.List(Of Int64))

    Private Sub Iniciar()
        Dim SelectedWFS As New System.Collections.Generic.List(Of Int64)
        Dim i As Int16
        For i = 0 To CShort(chklist.CheckedItems.Count - 1)
            Dim WFID As Int64 = DirectCast(chklist.CheckedItems(i), DsWF.WFRow).Work_ID
            SelectedWFS.Add(WFID)
        Next
        startservices(SelectedWFS)
    End Sub
    'Private Sub startservices(ByVal Array As ArrayList)
    '    Try
    '        Dim i As Int16
    '        For i = 0 To Array.Count - 1
    '            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf StartOneService, i)
    '        Next
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Sub StartOneService(ByVal state As Object)

    'End Sub
    Private Sub startservices(ByVal WfIds As System.Collections.Generic.List(Of Int64))
        Try
            '    Dim service As New WorkFlow.WFService(TService, SelectedWFS)
            RaiseEvent Started(WfIds)
            'el new llama al startservice
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#Region "Botones"
    Private Sub btnCliente_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            'TService = WFService.Tipo.Cliente
            Iniciar()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub BtnServer_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnServer.Click
        Try
            'TService = WFService.Tipo.Servicio
            Iniciar()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btnMonitor_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            '    TService = WFService.Tipo.Monitoreo
            Iniciar()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub BtnCancell_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCancell.Click
        Try
            Visible = False
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
