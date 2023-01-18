Imports Zamba.Core
Public Class UC_Info
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "



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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents lblIngreso As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents lblAsignadoPor As ZLabel
    Friend WithEvents Label9 As ZLabel
    Friend WithEvents lblDateAsignado As ZLabel
    Friend WithEvents Label10 As ZLabel
    Friend WithEvents lblDocId As ZLabel
    Friend WithEvents lblNameDocId As ZLabel
    Friend WithEvents Label1 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(UC_Info))
        Panel1 = New ZPanel
        Label1 = New ZLabel
        lblDateAsignado = New ZLabel
        Label10 = New ZLabel
        lblAsignadoPor = New ZLabel
        Label9 = New ZLabel
        lblIngreso = New ZLabel
        Label4 = New ZLabel
        lblNameDocId = New ZLabel
        lblDocId = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(lblDocId)
        Panel1.Controls.Add(lblNameDocId)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(lblDateAsignado)
        Panel1.Controls.Add(Label10)
        Panel1.Controls.Add(lblAsignadoPor)
        Panel1.Controls.Add(Label9)
        Panel1.Controls.Add(lblIngreso)
        Panel1.Controls.Add(Label4)
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(279, 202)
        Panel1.TabIndex = 0
        '
        'Label1
        '
        Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Label1.Font = New Font("Verdana", 11.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.Blue
        Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Label1.Location = New System.Drawing.Point(258, 2)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(15, 19)
        Label1.TabIndex = 23
        '
        'lblDateAsignado
        '
        lblDateAsignado.BackColor = System.Drawing.Color.Transparent
        lblDateAsignado.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDateAsignado.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblDateAsignado.Location = New System.Drawing.Point(43, 122)
        lblDateAsignado.Name = "lblDateAsignado"
        lblDateAsignado.Size = New System.Drawing.Size(152, 16)
        lblDateAsignado.TabIndex = 22
        lblDateAsignado.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.BackColor = System.Drawing.Color.Transparent
        Label10.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label10.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label10.Location = New System.Drawing.Point(16, 104)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(151, 13)
        Label10.TabIndex = 21
        Label10.Text = "FECHA DE ASIGNACION:"
        '
        'lblAsignadoPor
        '
        lblAsignadoPor.BackColor = System.Drawing.Color.Transparent
        lblAsignadoPor.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblAsignadoPor.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblAsignadoPor.Location = New System.Drawing.Point(43, 78)
        lblAsignadoPor.Name = "lblAsignadoPor"
        lblAsignadoPor.Size = New System.Drawing.Size(152, 16)
        lblAsignadoPor.TabIndex = 20
        lblAsignadoPor.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.BackColor = System.Drawing.Color.Transparent
        Label9.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label9.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label9.Location = New System.Drawing.Point(16, 60)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(104, 13)
        Label9.TabIndex = 19
        Label9.Text = "ASIGNADO POR:"
        '
        'lblIngreso
        '
        lblIngreso.BackColor = System.Drawing.Color.Transparent
        lblIngreso.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblIngreso.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblIngreso.Location = New System.Drawing.Point(43, 34)
        lblIngreso.Name = "lblIngreso"
        lblIngreso.Size = New System.Drawing.Size(152, 16)
        lblIngreso.TabIndex = 15
        lblIngreso.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label4.Location = New System.Drawing.Point(16, 16)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(128, 13)
        Label4.TabIndex = 14
        Label4.Text = "FECHA DE INGRESO:"
        '
        'lblNameDocId
        '
        lblNameDocId.AutoSize = True
        lblNameDocId.BackColor = System.Drawing.Color.Transparent
        lblNameDocId.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblNameDocId.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblNameDocId.Location = New System.Drawing.Point(16, 154)
        lblNameDocId.Name = "lblNameDocId"
        lblNameDocId.Size = New System.Drawing.Size(119, 13)
        lblNameDocId.TabIndex = 24
        lblNameDocId.Text = "TIPO DOCUMENTO:"
        '
        'lblDocId
        '
        lblDocId.BackColor = System.Drawing.Color.Transparent
        lblDocId.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDocId.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblDocId.Location = New System.Drawing.Point(43, 167)
        lblDocId.Name = "lblDocId"
        lblDocId.Size = New System.Drawing.Size(230, 25)
        lblDocId.TabIndex = 25
        lblDocId.TextAlign = ContentAlignment.MiddleLeft
        '
        'UC_Info
        '
        Controls.Add(Panel1)
        Name = "UC_Info"
        Size = New System.Drawing.Size(282, 202)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Dim Result As TaskResult
    Public Sub New(ByRef Result As ITaskResult)
        MyBase.New()
        InitializeComponent()
        Me.Result = Result

        SetCheckIn()
        SetAsignedBy()
        SetAsignedDate()
        SetDocId()
    End Sub

    Private Sub SetCheckIn()
        Try
            If Result.CheckIn = #12:00:00 AM# Then
                lblIngreso.Text = String.Empty
            Else
                '[sebastian] 10-06-2009 se agrego tostring para salvar warning
                lblIngreso.Text = Result.CheckIn.ToString
            End If
        Catch ex As Exception
            lblIngreso.Text = String.Empty
        End Try
    End Sub
    Private Sub SetAsignedBy()
        Try
            lblAsignadoPor.Text = UserGroupBusiness.GetUserorGroupNamebyId(Result.AsignedById)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub SetAsignedDate()
        Try
            If Result.AsignedDate = #12:00:00 AM# Then
                lblDateAsignado.Text = String.Empty
            Else
                '[sebastian] 10-06-2009 se salvo warning
                lblDateAsignado.Text = Result.AsignedDate.ToString
            End If
        Catch ex As Exception
            lblDateAsignado.Text = String.Empty
        End Try
    End Sub
    '(pablo) - created
    Private Sub SetDocId()
        Try
            lblDocId.Text = DocTypesBusiness.GetDocTypeName(Result.DocTypeId, True) + " (" + Result.ID.ToString + ")"
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Close"
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label1.Click
        CloseControl()
    End Sub
    Public Sub CloseControl()
        Try
            If Parent IsNot Nothing Then
                Parent.Controls.Remove(Me)
            End If
            Dispose()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UcInfo_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
