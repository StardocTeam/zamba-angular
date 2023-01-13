'Imports Zamba.WFBusiness

Public Class UCDoAsignToGroup
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

    Friend WithEvents btnSave As ZButton
    Friend WithEvents lblGroup As ZLabel
    Friend WithEvents txtGroup As TextBox
    Friend WithEvents lblUsr As ZLabel
    Friend WithEvents txtUser As TextBox

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    <DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        txtUser = New TextBox()
        lblUsr = New ZLabel()
        lblGroup = New ZLabel()
        txtGroup = New TextBox()
        btnSave = New ZButton()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(btnSave)
        tbRule.Controls.Add(lblGroup)
        tbRule.Controls.Add(txtGroup)
        tbRule.Controls.Add(lblUsr)
        tbRule.Controls.Add(txtUser)
        '
        'txtUser
        '
        txtUser.Location = New System.Drawing.Point(95, 55)
        txtUser.Name = "txtUser"
        txtUser.Size = New System.Drawing.Size(361, 23)
        txtUser.TabIndex = 0
        '
        'lblUsr
        '
        lblUsr.AutoSize = True
        lblUsr.BackColor = System.Drawing.Color.Transparent
        lblUsr.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblUsr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblUsr.Location = New System.Drawing.Point(26, 58)
        lblUsr.Name = "lblUsr"
        lblUsr.Size = New System.Drawing.Size(62, 16)
        lblUsr.TabIndex = 1
        lblUsr.Text = "Usuario:"
        lblUsr.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblGroup
        '
        lblGroup.AutoSize = True
        lblGroup.BackColor = System.Drawing.Color.Transparent
        lblGroup.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblGroup.Location = New System.Drawing.Point(36, 92)
        lblGroup.Name = "lblGroup"
        lblGroup.Size = New System.Drawing.Size(52, 16)
        lblGroup.TabIndex = 3
        lblGroup.Text = "Grupo:"
        lblGroup.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtGroup
        '
        txtGroup.Location = New System.Drawing.Point(94, 89)
        txtGroup.Name = "txtGroup"
        txtGroup.Size = New System.Drawing.Size(362, 23)
        txtGroup.TabIndex = 2
        '
        'btnSave
        '
        btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.ForeColor = System.Drawing.Color.White
        btnSave.Location = New System.Drawing.Point(362, 132)
        btnSave.Name = "btnSave"
        btnSave.Size = New System.Drawing.Size(81, 25)
        btnSave.TabIndex = 4
        btnSave.Text = "Guardar"
        btnSave.UseVisualStyleBackColor = True
        '
        'UCDoAsignToGroup
        '
        Name = "UCDoAsignToGroup"
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region
    Dim CurrentRule As IDoAsignToGroup


    Public Sub New(ByVal CurrentRule As IDoAsignToGroup, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        Me.CurrentRule = CurrentRule
        txtUser.Text = CurrentRule.usuario
        txtGroup.Text = CurrentRule.grupo

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            If Not String.IsNullOrEmpty(txtUser.Text) AndAlso Not String.IsNullOrEmpty(txtGroup.Text) Then

                CurrentRule.usuario = txtUser.Text
                CurrentRule.grupo = txtGroup.Text

                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.usuario)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.grupo)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.usuario & "(" & CurrentRule.grupo & ")")

            Else
                MessageBox.Show("Error, debe completar los campos 'Usuario' y 'Grupo'")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#Region "Eventos"


#End Region

End Class