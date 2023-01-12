Imports Zamba.core
Imports Zamba.Viewers
Public Class UcIndexs
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            If Panel1 IsNot Nothing Then
                Panel1.Dispose()
                Panel1 = Nothing
            End If
            If Label1 IsNot Nothing Then
                Label1.Dispose()
                Label1 = Nothing
            End If
            If lblDocType IsNot Nothing Then
                lblDocType.Dispose()
                lblDocType = Nothing
            End If
            If Label4 IsNot Nothing Then
                Label4.Dispose()
                Label4 = Nothing
            End If
            If PanelIndexs IsNot Nothing Then
                PanelIndexs.Dispose()
                PanelIndexs = Nothing
            End If
            If Result IsNot Nothing Then
                Result.Dispose()
                Result = Nothing
            End If
            If UCIndexViewer IsNot Nothing Then
                UCIndexViewer.Dispose()
                UCIndexViewer = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lblDocType As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents PanelIndexs As System.Windows.Forms.Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UcIndexs))
        Panel1 = New ZPanel
        PanelIndexs = New System.Windows.Forms.Panel
        lblDocType = New ZLabel
        Label4 = New ZLabel
        Label1 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'Panel1
        '
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(PanelIndexs)
        Panel1.Controls.Add(lblDocType)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(232, 208)
        Panel1.TabIndex = 1
        '
        'PanelIndexs
        '
        PanelIndexs.BackColor = System.Drawing.Color.Transparent
        PanelIndexs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelIndexs.Location = New System.Drawing.Point(8, 56)
        PanelIndexs.Name = "PanelIndexs"
        PanelIndexs.Size = New System.Drawing.Size(216, 144)
        PanelIndexs.TabIndex = 26
        '
        'lblDocType
        '
        lblDocType.BackColor = System.Drawing.Color.Transparent
        lblDocType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lblDocType.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        lblDocType.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblDocType.Location = New System.Drawing.Point(16, 32)
        lblDocType.Name = "lblDocType"
        lblDocType.Size = New System.Drawing.Size(192, 16)
        lblDocType.TabIndex = 25
        lblDocType.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        Label4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label4.Location = New System.Drawing.Point(24, 16)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(104, 16)
        Label4.TabIndex = 24
        Label4.Text = "Entidad"
        '
        'Label1
        '
        Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(210, 4)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(15, 19)
        Label1.TabIndex = 23
        Label1.Text = "X"
        '
        'UcIndexs
        '
        Controls.Add(Panel1)
        Name = "UcIndexs"
        Size = New System.Drawing.Size(232, 208)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region
    Private UCIndexViewer As UCIndexViewer = Nothing
    Private Result As TaskResult
    Public Sub New(ByRef Result As TaskResult)
        MyBase.New()
        InitializeComponent()
        Me.Result = result
        loadIndexs()
    End Sub
    Private Sub loadIndexs()
        Try
            lblDocType.Text = Result.Parent.Name
        Catch ex As Exception
            Dim exn As New Exception("Error en UcIndexs.loadIndexs() al cargar Result.Parent.Name, excepción " & ex.ToString)
            zamba.core.zclass.raiseerror(exn)
        End Try

        Try
            UCIndexViewer = New UCIndexViewer()
            UCIndexViewer.Dock = DockStyle.Fill
            PanelIndexs.Controls.Add(UCIndexViewer)
            UCIndexViewer.ShowIndexs(Result.ID, Result.DocTypeId, False)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#Region "Close"
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label1.Click
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UcIndexs_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
