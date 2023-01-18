'Imports Zamba.WFBusiness
Imports Zamba.Data

Public Class UCDOGenerateExcel
    'Los controles de Reglas de Accion deben heredar de ZRuleControl
    Inherits ZRuleControl


    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtTitle As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents PnlIndex As System.Windows.Forms.Panel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtFooter As TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents cmbDocTypes As ComboBox

#Region " Código generado por el Diseñador de Windows Forms "
    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        cmbDocTypes = New ComboBox
        Label1 = New ZLabel
        Panel1 = New System.Windows.Forms.Panel
        btnAceptar = New ZButton
        PnlIndex = New System.Windows.Forms.Panel
        txtFooter = New TextBox
        Label4 = New ZLabel
        txtTitle = New Zamba.AppBlock.TextoInteligenteTextBox
        Label2 = New ZLabel
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Panel1)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(txtTitle)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(cmbDocTypes)
        tbRule.Size = New System.Drawing.Size(534, 435)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(542, 461)
        '
        'cmbDocTypes
        '
        cmbDocTypes.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDocTypes.FormattingEnabled = True
        cmbDocTypes.Location = New System.Drawing.Point(8, 26)
        cmbDocTypes.Name = "cmbDocTypes"
        cmbDocTypes.Size = New System.Drawing.Size(146, 21)
        cmbDocTypes.TabIndex = 0
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(8, 8)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(99, 13)
        Label1.TabIndex = 1
        Label1.Text = "Entidad"
        '
        'Panel1
        '
        Panel1.AutoScroll = True
        Panel1.BackColor = System.Drawing.Color.Gainsboro
        Panel1.Controls.Add(btnAceptar)
        Panel1.Controls.Add(PnlIndex)
        Panel1.Controls.Add(txtFooter)
        Panel1.Controls.Add(Label4)
        Panel1.Location = New System.Drawing.Point(8, 49)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(514, 373)
        Panel1.TabIndex = 2
        '
        'btnAceptar
        '
        btnAceptar.Location = New System.Drawing.Point(381, 328)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(70, 24)
        btnAceptar.TabIndex = 3
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = True
        '
        'PnlIndex
        '
        PnlIndex.Location = New System.Drawing.Point(11, 9)
        PnlIndex.Name = "PnlIndex"
        PnlIndex.Size = New System.Drawing.Size(265, 321)
        PnlIndex.TabIndex = 5
        '
        'txtFooter
        '
        txtFooter.Location = New System.Drawing.Point(100, 330)
        txtFooter.Name = "txtFooter"
        txtFooter.Size = New System.Drawing.Size(230, 21)
        txtFooter.TabIndex = 4
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(5, 333)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(95, 13)
        Label4.TabIndex = 3
        Label4.Text = "Pie del Documento"
        '
        'txtTitle
        '
        txtTitle.Location = New System.Drawing.Point(213, 26)
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New System.Drawing.Size(265, 21)
        txtTitle.TabIndex = 1
        txtTitle.Text = ""
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(213, 8)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(107, 13)
        Label2.TabIndex = 0
        Label2.Text = "Título del Documento"
        '
        'UCDOGenerateExcel
        '
        BackColor = System.Drawing.Color.Gainsboro
        Name = "UCDOGenerateExcel"
        Size = New System.Drawing.Size(542, 461)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region


    'El New debe recibir la regla a configurar
    Public Sub New(ByRef CurrentRule As IDoGenerateExcel, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub


    'Para guardar los parametros de una regla se usa el siguiente metodo, al que se le pasa:
    'la regla en si, un item y el valor
    'WFRulesBusiness.UpdateParamItemAction(CurrentRule, 0, StepItem.WFStep.Id)

    'Se llama a este metodo para actualizar el nombre en el administrador al finalizar la configuracion
    'Me.RaiseUpdateMaskName()

    ''' <summary>
    ''' Carga los valores ya asignados a la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub this_Load()
        Try

            If (MyRule.DocTypeId <> 0) Then
                'Le cargo al combo de tipos de documento todos los tipos existentes
                cmbDocTypes.DataSource = DocTypesBusiness.GetDocTypeNames
                bolIni = True

                cmbDocTypes.Text = DocTypesBusiness.GetDocTypeName(MyRule.DocTypeId, True)

                txtTitle.Text = MyRule.Title
                txtFooter.Text = MyRule.Footer

                CargarIndices()
            Else
                'Le cargo al combo de tipos de documento todos los tipos existentes
                bolIni = True
                cmbDocTypes.DataSource = DocTypesBusiness.GetDocTypeNames
                cmbDocTypes.SelectedIndex = 0
            End If

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub


    Private bolIni As Boolean = False
    ''' <summary>
    ''' Cargo los atributos guardados en la base
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarIndices()
        Try
            PnlIndex.Controls.Clear()
            Dim y As Int32 = 0
            Dim x As Int32 = 0
            Dim title As Boolean = True
            PnlIndex.Size = New Point(299, PnlIndex.Size.Height)

            If MyRule.DocTypeId <> 0 Then
                Dim strIndex As String = MyRule.Index
                While strIndex <> ""
                    'Obtengo el item (// separa por items y | separa por valor)
                    Dim strItem = strIndex.Split("//")(0)
                    Dim id As Int32 = Int(strItem.split("|")(0))
                    Dim name As String = strItem.split("|")(1)
                    Dim bolShow As Boolean = Boolean.Parse(strItem.split("|")(2))
                    Dim bolSum As Boolean = Boolean.Parse(strItem.split("|")(3))
                    Dim bolCount As Boolean = Boolean.Parse(strItem.split("|")(4))

                    'Creo el visualizador del indice y lo agrego al panel
                    If title = True Then
                        Dim vis As New UCDOGenerateExcelVis(id, name, bolShow, bolSum, bolCount, True)
                        PnlIndex.Controls.Add(vis)
                        vis.Location = New Point(x, y)
                    Else
                        Dim vis As New UCDOGenerateExcelVis(id, name, bolShow, bolSum, bolCount, False)
                        title = False
                        PnlIndex.Controls.Add(vis)
                        vis.Location = New Point(x, y)
                    End If

                    y += 30
                    If y > 290 Then
                        x += 220
                        y = 0
                        PnlIndex.Size = New Point(PnlIndex.Size.Width * 2, PnlIndex.Size.Height)
                        title = True
                    End If

                    strIndex = strIndex.Remove(0, strIndex.Split("//")(0).Length)
                    If strIndex.Length > 0 Then
                        strIndex = strIndex.Remove(0, 2)
                    End If
                End While
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoGenerateExcel
        Get
            Return DirectCast(Rule, IDoGenerateExcel)
        End Get
    End Property

    ''' <summary>
    ''' Cambio de entidad vuelvo a llenar el panel de atributos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbDocTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDocTypes.SelectedIndexChanged
        Try
            If bolIni = True Then
                PnlIndex.Controls.Clear()
                Dim i As Int32 = 0
                If cmbDocTypes.Text <> "" Then
                    Dim td As DataSet
                    'Obtengo los atributos del docType
                    td = IndexsBusiness.GetIndexSchemaAsDataSet(DocTypesFactory.GetDocTypeIdByName(cmbDocTypes.Text)) ' DocTypesFactory.getIndexByDocTypeId(DocTypesFactory.GetDocTypeIdByName(Me.cmbDocTypes.Text))

                    Dim y As Int32 = 0
                    Dim x As Int32 = 0
                    Dim title As Boolean = True
                    PnlIndex.Size = New Point(299, PnlIndex.Size.Height)

                    For i = 0 To td.Tables(0).Rows.Count - 1
                        Dim id As Int32 = td.Tables(0).Rows(i).Item(0)
                        Dim name As String = td.Tables(0).Rows(i).Item(1).ToString.Trim

                        'Creo el visualizador del indice y lo agrego al panel
                        If title = True Then
                            Dim vis As New UCDOGenerateExcelVis(id, name, False, False, False, True)
                            PnlIndex.Controls.Add(vis)
                            vis.Location = New Point(x, y)
                        Else
                            Dim vis As New UCDOGenerateExcelVis(id, name, False, False, False, False)
                            title = False
                            PnlIndex.Controls.Add(vis)
                            vis.Location = New Point(x, y)
                        End If

                        y += 30
                        If y > 290 Then
                            x += 220
                            y = 0
                            PnlIndex.Size = New Point(PnlIndex.Size.Width * 2, PnlIndex.Size.Height)
                            title = True
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        If cmbDocTypes.Text <> "" Then
            MyRule.DocTypeId = DocTypesFactory.GetDocTypeIdByName(cmbDocTypes.Text)
            MyRule.Title = txtTitle.Text
            MyRule.Index = setIndex()
            MyRule.footer = txtFooter.Text

            WFRulesBusiness.UpdateParamItem(Rule, 0, MyRule.DocTypeId)
            WFRulesBusiness.UpdateParamItem(Rule, 1, MyRule.Title)
            WFRulesBusiness.UpdateParamItem(Rule, 2, myrule.index)
            WFRulesBusiness.UpdateParamItem(Rule, 3, MyRule.Footer)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        End If
    End Sub

    Private Function setIndex() As String
        Dim builder As New System.Text.StringBuilder()
        For Each c As UCDOGenerateExcelVis In PnlIndex.Controls
            builder.append(c.id & "|" & c.indexName & "|" & c.bolshow & "|" & c.bolsum & "|" & c.bolcount & "//")
        Next
        Return builder.ToString()
    End Function
End Class

