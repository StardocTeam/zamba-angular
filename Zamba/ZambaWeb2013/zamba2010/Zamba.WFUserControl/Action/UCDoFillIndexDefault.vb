'Imports Zamba.WFBusiness
Imports zamba.Indexs

Public Class UCDoFillIndexDefault
    Inherits ZRuleControl

#Region "Variables Locales"
    Private _display As DisplayindexCtl
    Private _selectedIndex As Index
    Private _currentRule As IDoFillIndexDefault
    Friend WithEvents optFechaHora As System.Windows.Forms.RadioButton
    Friend WithEvents optUsuario As System.Windows.Forms.RadioButton
    Friend WithEvents optUsuarioWindows As System.Windows.Forms.RadioButton
    Friend WithEvents optNombrePC As System.Windows.Forms.RadioButton
    Friend WithEvents panelList As System.Windows.Forms.Panel
    Public Property Display() As DisplayindexCtl
        Get
            Return _display
        End Get
        Set(ByVal value As DisplayindexCtl)
            _display = value
        End Set
    End Property
    Public Property CurrentRule() As IDoFillIndexDefault
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IDoFillIndexDefault)
            _currentRule = value
        End Set
    End Property
    Public Property SelectedIndex() As Index
        Get
            Return _selectedIndex
        End Get
        Set(ByVal value As Index)
            _selectedIndex = value
        End Set
    End Property

#End Region

#Region " Código generado por el Diseñador de Windows Forms "
    Public Sub New(ByRef DoFillIndexDefault As IDoFillIndexDefault, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoFillIndexDefault, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoFillIndexDefault

        Try
            'cargo los todos los atributos
            LoadIndexs()
            lstIndices.DisplayMember = "Name"

            'verifico cual es el atributo ya configurado
            Dim i As Integer

            'selecciono el primer indice por defecto por si la regla nunca fue configurada
            lstIndices.SelectedIndex = 0

            If (CurrentRule.IndexID > 0) Then
                'busco si el atributo configurado existe en el listado y lo selecciono
                For i = 0 To lstIndices.Items.Count - 1
                    Dim localIndex As Index = DirectCast(lstIndices.Items.Item(i), Index)

                    If localIndex.ID = CurrentRule.IndexID Then
                        SelectedIndex = ZCore.GetIndex(CurrentRule.IndexID)
                        lstIndices.Items.Item(i) = SelectedIndex
                        lstIndices.SelectedIndex = i
                        Exit For
                    End If

                Next

            End If

            'cargo el texto ya guardado y selecciono la opcion correspondiente
            Select Case CurrentRule.TEXTODEFAULT
                Case "FECHA Y HORA ACTUAL"
                    optFechaHora.Checked = True
                Case "USUARIO ACTUAL"
                    optUsuario.Checked = True
                Case "USUARIO WINDOWS"
                    optUsuarioWindows.Checked = True
                Case "NOMBRE DE PC"
                    optNombrePC.Checked = True

                Case Else
                    optFechaHora.Checked = True
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean) 'Form reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents lstIndices As ListBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents lblIndices As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents lblType As ZLabel

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        lstIndices = New ListBox()
        btnAceptar = New ZButton()
        lblIndices = New ZLabel()
        Label2 = New ZLabel()
        lblType = New ZLabel()
        panelList = New System.Windows.Forms.Panel()
        optFechaHora = New System.Windows.Forms.RadioButton()
        optUsuario = New System.Windows.Forms.RadioButton()
        optUsuarioWindows = New System.Windows.Forms.RadioButton()
        optNombrePC = New System.Windows.Forms.RadioButton()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        panelList.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(optNombrePC)
        tbRule.Controls.Add(optUsuarioWindows)
        tbRule.Controls.Add(optUsuario)
        tbRule.Controls.Add(optFechaHora)
        tbRule.Controls.Add(btnAceptar)
        tbRule.Controls.Add(panelList)
        tbRule.Size = New System.Drawing.Size(528, 443)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(536, 472)
        '
        'lstIndices
        '
        lstIndices.DisplayMember = "INDEX_NAME"
        lstIndices.ItemHeight = 16
        lstIndices.Location = New System.Drawing.Point(21, 36)
        lstIndices.Name = "lstIndices"
        lstIndices.Size = New System.Drawing.Size(440, 212)
        lstIndices.Sorted = True
        lstIndices.TabIndex = 33
        '
        'btnAceptar
        '
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(349, 326)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(87, 31)
        btnAceptar.TabIndex = 30
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'lblIndices
        '
        lblIndices.BackColor = System.Drawing.Color.Transparent
        lblIndices.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblIndices.Location = New System.Drawing.Point(3, 9)
        lblIndices.Name = "lblIndices"
        lblIndices.Size = New System.Drawing.Size(248, 24)
        lblIndices.TabIndex = 29
        lblIndices.Text = "Seleccione el Atributo"
        lblIndices.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(18, 257)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(187, 16)
        Label2.TabIndex = 34
        Label2.Text = "Tipo de datos del Atributo:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblType
        '
        lblType.AutoSize = True
        lblType.BackColor = System.Drawing.Color.Transparent
        lblType.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblType.Location = New System.Drawing.Point(149, 257)
        lblType.Name = "lblType"
        lblType.Size = New System.Drawing.Size(40, 16)
        lblType.TabIndex = 35
        lblType.Text = "TIPO"
        lblType.TextAlign = ContentAlignment.MiddleLeft
        '
        'panelList
        '
        panelList.BackColor = System.Drawing.Color.Transparent
        panelList.Controls.Add(lblIndices)
        panelList.Controls.Add(lstIndices)
        panelList.Controls.Add(Label2)
        panelList.Controls.Add(lblType)
        panelList.Dock = System.Windows.Forms.DockStyle.Top
        panelList.Location = New System.Drawing.Point(3, 3)
        panelList.Name = "panelList"
        panelList.Size = New System.Drawing.Size(522, 275)
        panelList.TabIndex = 37
        '
        'optFechaHora
        '
        optFechaHora.AutoSize = True
        optFechaHora.Checked = True
        optFechaHora.Location = New System.Drawing.Point(21, 284)
        optFechaHora.Name = "optFechaHora"
        optFechaHora.Size = New System.Drawing.Size(96, 20)
        optFechaHora.TabIndex = 38
        optFechaHora.TabStop = True
        optFechaHora.Text = "FechaHora"
        optFechaHora.UseVisualStyleBackColor = True
        '
        'optUsuario
        '
        optUsuario.AutoSize = True
        optUsuario.Location = New System.Drawing.Point(21, 307)
        optUsuario.Name = "optUsuario"
        optUsuario.Size = New System.Drawing.Size(120, 20)
        optUsuario.TabIndex = 39
        optUsuario.Text = "Usuario actual"
        optUsuario.UseVisualStyleBackColor = True
        '
        'optUsuarioWindows
        '
        optUsuarioWindows.AutoSize = True
        optUsuarioWindows.Location = New System.Drawing.Point(21, 330)
        optUsuarioWindows.Name = "optUsuarioWindows"
        optUsuarioWindows.Size = New System.Drawing.Size(137, 20)
        optUsuarioWindows.TabIndex = 40
        optUsuarioWindows.Text = "Usuario Windows"
        optUsuarioWindows.UseVisualStyleBackColor = True
        '
        'optNombrePC
        '
        optNombrePC.AutoSize = True
        optNombrePC.Location = New System.Drawing.Point(21, 353)
        optNombrePC.Name = "optNombrePC"
        optNombrePC.Size = New System.Drawing.Size(118, 20)
        optNombrePC.TabIndex = 41
        optNombrePC.Text = "Nombre de PC"
        optNombrePC.UseVisualStyleBackColor = True
        '
        'UCDoFillIndexDefault
        '
        Name = "UCDoFillIndexDefault"
        Size = New System.Drawing.Size(536, 472)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        panelList.ResumeLayout(False)
        panelList.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Cargar"
    Private Sub UCDoFillIndex_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Try
        '    'cargo los todos los atributos
        '    LoadIndexs()
        '    lstIndices.DisplayMember = "Name"

        '    'verifico cual es el atributo ya configurado
        '    Dim i As Integer

        '    'selecciono el primer indice por defecto por si la regla nunca fue configurada
        '    lstIndices.SelectedIndex = 0

        '    'busco si el atributo configurado eiste en el listado y lo selecciono
        '    For i = 0 To lstIndices.Items.Count - 1
        '        Dim localIndex As Index = DirectCast(lstIndices.Items.Item(i), Index)
        '        If localIndex.ID = CurrentRule.Index.ID Then
        '            SelectedIndex = CurrentRule.Index
        '            lstIndices.Items.Item(i) = CurrentRule.Index
        '            lstIndices.SelectedIndex = i
        '            Exit For
        '        End If
        '    Next

        '    'cargo el texto ya guardado y selecciono la opcion correspondiente
        '    Select Case CurrentRule.TEXTODEFAULT
        '        Case "FECHA Y HORA ACTUAL"
        '            Me.optFechaHora.Checked = True
        '        Case "USUARIO ACTUAL"
        '            Me.optUsuario.Checked = True
        '        Case "USUARIO WINDOWS"
        '            Me.optUsuarioWindows.Checked = True
        '        Case "NOMBRE DE PC"
        '            Me.optNombrePC.Checked = True

        '        Case Else
        '            Me.optFechaHora.Checked = True
        '    End Select
        'Catch ex As Exception
        '    zamba.core.zclass.raiseerror(ex)
        'End Try
    End Sub

#End Region

#Region "Guardar"
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim DataTypeError As Boolean = False
        '8-5-07 
        SelectedIndex = Display.Index
        '----
        CurrentRule.IndexID = SelectedIndex.ID
        'CurrentRule.Index.ID = _selectedIndex.ID
        If IsNothing(SelectedIndex) Then Exit Sub

        '--16/5/07
        If optFechaHora.Checked = True Then
            CurrentRule.TEXTODEFAULT = "FECHA Y HORA ACTUAL"
        End If
        If optUsuario.Checked = True Then
            CurrentRule.TEXTODEFAULT = "USUARIO ACTUAL"
        End If
        If optUsuarioWindows.Checked = True Then
            CurrentRule.TEXTODEFAULT = "USUARIO WINDOWS"
        End If
        If optNombrePC.Checked = True Then
            CurrentRule.TEXTODEFAULT = "NOMBRE DE PC"
        End If
        '--
        Select Case SelectedIndex.Type
            Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                If optNombrePC.Checked Or optUsuario.Checked Or optUsuarioWindows.Checked Then
                    WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.IndexID.ToString)
                    WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.TEXTODEFAULT.ToString)
                    UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
                Else
                    MessageBox.Show("No se puede Ingresar " + CurrentRule.TEXTODEFAULT + " en el atributo " + Trim(SelectedIndex.Name) + " - difieren en el tipo de datos", "Zamba", MessageBoxButtons.OK)
                End If
            Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                If optFechaHora.Checked = True Then
                    WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.IndexID.ToString)
                    WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.TEXTODEFAULT.ToString)
                    UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
                Else
                    MessageBox.Show("No se puede Ingresar " + CurrentRule.TEXTODEFAULT + " en el atributo " + Trim(SelectedIndex.Name) + " - difieren en el tipo de datos", "Zamba", MessageBoxButtons.OK)
                End If
            Case Else
                MessageBox.Show("No se puede Ingresar " + CurrentRule.TEXTODEFAULT + " en el atributo " + Trim(SelectedIndex.Name) + " - difieren en el tipo de datos", "Zamba", MessageBoxButtons.OK)
        End Select

    End Sub

#End Region

#Region "Validaciones y Cuestiones Internas del disenador"
    Private Function ValidDataType() As Boolean
        Select Case _selectedIndex.Type
            Case IndexDataType.Alfanumerico
                Return True
            Case IndexDataType.Alfanumerico_Largo
                Return True
            Case IndexDataType.Fecha
                Dim timeTryParse As DateTime
                Return DateTime.TryParse(SelectedIndex.DataTemp, timeTryParse)
            Case IndexDataType.Fecha_Hora
                Dim timeTryParse As DateTime
                Return DateTime.TryParse(SelectedIndex.DataTemp, timeTryParse)
            Case IndexDataType.Moneda
                Return True 'TODO Validar dato Moneda
            Case IndexDataType.Numerico
                Dim intTryParse As Integer
                Return Integer.TryParse(SelectedIndex.DataTemp, intTryParse)
            Case IndexDataType.Numerico_Decimales
                Dim longTryParse As Long
                Return Long.TryParse(SelectedIndex.DataTemp, longTryParse)
            Case IndexDataType.Numerico_Largo
                Dim int As Int64
                Return Int64.TryParse(SelectedIndex.DataTemp, int)
            Case IndexDataType.Si_No
                Dim int As Integer
                Return Integer.TryParse(SelectedIndex.DataTemp, int) ' int > 0 false ; i = 0 true
        End Select
    End Function

    Private Sub LoadIndexs()
        For Each item As Object In (IndexsBusiness.getAllIndexes).Values
            Dim index As Index = DirectCast(item, Index)
            lstIndices.Items.Add(index)
            index = Nothing
        Next
    End Sub
    Private Sub lstIndices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstIndices.SelectedIndexChanged
        Try
            SelectedIndex = DirectCast(lstIndices.SelectedItem, Index)
            lblType.Text = _selectedIndex.Type.ToString.Replace("_", " ")
            Display = New DisplayindexCtl(SelectedIndex, True)
            'panelValues.Controls.Clear()

            '----08-05-07

            '------

            ' panelValues.Controls.Add(Display)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shadows ReadOnly Property MyRule() As IDoFillIndex
        Get
            Return DirectCast(Rule, IDoFillIndex)
        End Get
    End Property

    Private Sub optFechaHora_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        'panelValues.Controls.Add(Display)


    End Sub

    Private Sub optFechaHora_Click(ByVal sender As Object, ByVal e As EventArgs)
        'SelectedIndex = Display.Index

    End Sub

    Private Sub optFechaHora_CheckedChanged_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles optFechaHora.CheckedChanged

    End Sub

    Private Sub optFechaHora_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles optFechaHora.Click
        'If Me.optFechaHora.Checked = True Then
        '    Me.txtValor.Text = ""
        '    Me.txtValor.Text = "FECHA Y HORA ACTUAL"
        '    Me.panelValues.Enabled = True
        'End If

    End Sub

    Private Sub optUsuario_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optUsuario.Click
        'If Me.optUsuario.Checked = True Then
        '    Me.txtValor.Text = ""
        '    Me.txtValor.Text = "USUARIO ACTUAL"
        '    Me.panelValues.Enabled = True
        'End If

    End Sub

    Private Sub optManual_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optUsuarioWindows.Click
        'If Me.optManual.Checked = True Then
        '    Me.txtValor.Text = ""
        '    Me.panelValues.Enabled = True
        'End If
    End Sub

#End Region

End Class