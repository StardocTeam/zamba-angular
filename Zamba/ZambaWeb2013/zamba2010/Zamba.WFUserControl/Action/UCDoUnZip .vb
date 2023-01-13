'Imports Zamba.WFBusiness

Public Class UCDoUnZip
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "


    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents btnGuardar As ZButton
    Friend WithEvents lblNameUser As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtNombreVar As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtFiles As AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtNameNewFile As TextoInteligenteTextBox
    Friend WithEvents ZLabel1 As ZLabel

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    <DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        btnGuardar = New ZButton()
        lblNameUser = New ZLabel()
        Label3 = New ZLabel()
        txtNombreVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtFiles = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtNameNewFile = New Zamba.AppBlock.TextoInteligenteTextBox()
        ZLabel1 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(txtNameNewFile)
        tbRule.Controls.Add(ZLabel1)
        tbRule.Controls.Add(txtFiles)
        tbRule.Controls.Add(txtNombreVar)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(lblNameUser)
        tbRule.Controls.Add(btnGuardar)
        tbRule.Size = New System.Drawing.Size(814, 432)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(822, 461)
        '
        'btnGuardar
        '
        btnGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.ForeColor = System.Drawing.Color.White
        btnGuardar.Location = New System.Drawing.Point(38, 191)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New System.Drawing.Size(100, 31)
        btnGuardar.TabIndex = 13
        btnGuardar.Tag = ""
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        '
        'lblNameUser
        '
        lblNameUser.AutoSize = True
        lblNameUser.BackColor = System.Drawing.Color.Transparent
        lblNameUser.Font = New Font("Verdana", 9.75!)
        lblNameUser.FontSize = 9.75!
        lblNameUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblNameUser.Location = New System.Drawing.Point(35, 30)
        lblNameUser.Name = "lblNameUser"
        lblNameUser.Size = New System.Drawing.Size(150, 16)
        lblNameUser.TabIndex = 65
        lblNameUser.Tag = "NOMBREFILE"
        lblNameUser.Text = "Archivos a comprimir:"
        lblNameUser.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!)
        Label3.FontSize = 9.75!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(35, 129)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(156, 16)
        Label3.TabIndex = 69
        Label3.Text = "Nombre de la variable:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtNombreVar
        '
        txtNombreVar.Location = New System.Drawing.Point(259, 122)
        txtNombreVar.Name = "txtNombreVar"
        txtNombreVar.Size = New System.Drawing.Size(277, 23)
        txtNombreVar.TabIndex = 70
        txtNombreVar.Text = ""
        '
        'txtFiles
        '
        txtFiles.Location = New System.Drawing.Point(259, 27)
        txtFiles.Name = "txtFiles"
        txtFiles.Size = New System.Drawing.Size(277, 23)
        txtFiles.TabIndex = 72
        txtFiles.Text = ""
        '
        'txtNameNewFile
        '
        txtNameNewFile.Location = New System.Drawing.Point(259, 74)
        txtNameNewFile.Name = "txtNameNewFile"
        txtNameNewFile.Size = New System.Drawing.Size(277, 23)
        txtNameNewFile.TabIndex = 74
        txtNameNewFile.Text = ""
        '
        'ZLabel1
        '
        ZLabel1.AutoSize = True
        ZLabel1.BackColor = System.Drawing.Color.Transparent
        ZLabel1.Font = New Font("Verdana", 9.75!)
        ZLabel1.FontSize = 9.75!
        ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZLabel1.Location = New System.Drawing.Point(35, 81)
        ZLabel1.Name = "ZLabel1"
        ZLabel1.Size = New System.Drawing.Size(218, 16)
        ZLabel1.TabIndex = 73
        ZLabel1.Text = "Nombre del archivo comprimido:"
        ZLabel1.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCDoUnZip
        '
        Name = "UCDoUnZip"
        Size = New System.Drawing.Size(822, 461)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region
    Dim CurrentRule As IDoUnZip


    Public Sub New(ByVal CurrentRule As IDoUnZip, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        Me.CurrentRule = CurrentRule
        txtFiles.Text = CurrentRule.files
        txtNombreVar.Text = CurrentRule.nameVar
        txtNameNewFile.Text = CurrentRule.nameNewFile
    End Sub



#Region "Eventos"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            If Not String.IsNullOrEmpty(txtFiles.Text) And Not String.IsNullOrEmpty(txtNombreVar.Text) And Not String.IsNullOrEmpty(txtNameNewFile.Text) Then

                CurrentRule.files = txtFiles.Text
                CurrentRule.nameVar = txtNombreVar.Text
                CurrentRule.nameNewFile = txtNameNewFile.Text

                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.files)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.nameVar)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.nameNewFile)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")

            Else
                MessageBox.Show("Error, debe completar los campos 'Nombre', 'Apellido' y 'Nombre de Usuario'")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ZLabel1_Click(sender As Object, e As EventArgs) Handles ZLabel1.Click

    End Sub
#End Region

End Class