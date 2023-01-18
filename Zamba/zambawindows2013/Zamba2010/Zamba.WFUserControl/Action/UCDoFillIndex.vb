'Imports Zamba.WFBusiness
Imports Zamba.Indexs
Imports Zamba.AdminControls

Public Class UCDoFillIndex
    Inherits ZRuleControl

    Private _display As DisplayindexCtl
    Private _selectedIndex As Index
    Private _currentRule As IDoFillIndex
    Friend WithEvents panelValues As System.Windows.Forms.Panel
    Friend WithEvents lblSecondaryValue As ZLabel
    Friend WithEvents txtSecondaryValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkVerFuncionesValor As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverWriteIndex As System.Windows.Forms.CheckBox
    Friend WithEvents btnEliminar As ZButton
    Friend WithEvents btnAgregar As ZButton
    Friend WithEvents lstCondiciones As ListBox
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents ZPanel3 As ZPanel
    Friend WithEvents panelList As System.Windows.Forms.Panel
    Public Property Display() As DisplayindexCtl
        Get
            Return _display
        End Get
        Set(ByVal value As DisplayindexCtl)
            _display = value
        End Set
    End Property
    Public Property CurrentRule() As IDoFillIndex
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IDoFillIndex)
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
#Region " Código generado por el Diseñador de Windows Forms "
    Public Sub New(ByRef DoFillIndex As IDoFillIndex, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoFillIndex, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoFillIndex
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
        Me.lstIndices = New System.Windows.Forms.ListBox()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.lblIndices = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.lblType = New Zamba.AppBlock.ZLabel()
        Me.panelList = New System.Windows.Forms.Panel()
        Me.panelValues = New System.Windows.Forms.Panel()
        Me.ZPanel3 = New Zamba.AppBlock.ZPanel()
        Me.chkVerFuncionesValor = New System.Windows.Forms.CheckBox()
        Me.chkOverWriteIndex = New System.Windows.Forms.CheckBox()
        Me.lblSecondaryValue = New Zamba.AppBlock.ZLabel()
        Me.txtSecondaryValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lstCondiciones = New System.Windows.Forms.ListBox()
        Me.btnEliminar = New Zamba.AppBlock.ZButton()
        Me.btnAgregar = New Zamba.AppBlock.ZButton()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.ZPanel2 = New Zamba.AppBlock.ZPanel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.panelList.SuspendLayout()
        Me.panelValues.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.ZPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.SplitContainer1)
        Me.tbRule.Controls.Add(Me.ZPanel2)
        Me.tbRule.Controls.Add(Me.ZPanel1)
        Me.tbRule.Controls.Add(Me.panelValues)
        Me.tbRule.Controls.Add(Me.panelList)
        Me.tbRule.Size = New System.Drawing.Size(760, 573)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(768, 602)
        '
        'lstIndices
        '
        Me.lstIndices.DisplayMember = "INDEX_NAME"
        Me.lstIndices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstIndices.ItemHeight = 16
        Me.lstIndices.Location = New System.Drawing.Point(0, 24)
        Me.lstIndices.Name = "lstIndices"
        Me.lstIndices.Size = New System.Drawing.Size(754, 176)
        Me.lstIndices.Sorted = True
        Me.lstIndices.TabIndex = 33
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(512, 6)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(102, 35)
        Me.btnAceptar.TabIndex = 30
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'lblIndices
        '
        Me.lblIndices.BackColor = System.Drawing.Color.Transparent
        Me.lblIndices.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblIndices.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblIndices.FontSize = 9.75!
        Me.lblIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblIndices.Location = New System.Drawing.Point(0, 0)
        Me.lblIndices.Name = "lblIndices"
        Me.lblIndices.Size = New System.Drawing.Size(754, 24)
        Me.lblIndices.TabIndex = 29
        Me.lblIndices.Text = "Seleccione el Atributo"
        Me.lblIndices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(19, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(187, 16)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Tipo de datos del Atributo:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblType.FontSize = 9.75!
        Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblType.Location = New System.Drawing.Point(212, 3)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(40, 16)
        Me.lblType.TabIndex = 35
        Me.lblType.Text = "TIPO"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelList
        '
        Me.panelList.BackColor = System.Drawing.Color.Transparent
        Me.panelList.Controls.Add(Me.lstIndices)
        Me.panelList.Controls.Add(Me.lblIndices)
        Me.panelList.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelList.Location = New System.Drawing.Point(3, 3)
        Me.panelList.Name = "panelList"
        Me.panelList.Size = New System.Drawing.Size(754, 200)
        Me.panelList.TabIndex = 37
        '
        'panelValues
        '
        Me.panelValues.BackColor = System.Drawing.Color.Transparent
        Me.panelValues.Controls.Add(Me.ZPanel3)
        Me.panelValues.Controls.Add(Me.chkVerFuncionesValor)
        Me.panelValues.Controls.Add(Me.chkOverWriteIndex)
        Me.panelValues.Controls.Add(Me.lblType)
        Me.panelValues.Controls.Add(Me.Label2)
        Me.panelValues.Controls.Add(Me.lblSecondaryValue)
        Me.panelValues.Controls.Add(Me.txtSecondaryValue)
        Me.panelValues.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelValues.Location = New System.Drawing.Point(3, 203)
        Me.panelValues.Name = "panelValues"
        Me.panelValues.Size = New System.Drawing.Size(754, 118)
        Me.panelValues.TabIndex = 36
        '
        'ZPanel3
        '
        Me.ZPanel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZPanel3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel3.Location = New System.Drawing.Point(0, 87)
        Me.ZPanel3.Name = "ZPanel3"
        Me.ZPanel3.Size = New System.Drawing.Size(754, 31)
        Me.ZPanel3.TabIndex = 42
        '
        'chkVerFuncionesValor
        '
        Me.chkVerFuncionesValor.AutoSize = True
        Me.chkVerFuncionesValor.BackColor = System.Drawing.Color.Transparent
        Me.chkVerFuncionesValor.Location = New System.Drawing.Point(22, 61)
        Me.chkVerFuncionesValor.Name = "chkVerFuncionesValor"
        Me.chkVerFuncionesValor.Size = New System.Drawing.Size(179, 20)
        Me.chkVerFuncionesValor.TabIndex = 40
        Me.chkVerFuncionesValor.Text = "Ver Funciones de Valor"
        Me.chkVerFuncionesValor.UseVisualStyleBackColor = False
        '
        'chkOverWriteIndex
        '
        Me.chkOverWriteIndex.AutoSize = True
        Me.chkOverWriteIndex.BackColor = System.Drawing.Color.Transparent
        Me.chkOverWriteIndex.Location = New System.Drawing.Point(469, 6)
        Me.chkOverWriteIndex.Name = "chkOverWriteIndex"
        Me.chkOverWriteIndex.Size = New System.Drawing.Size(189, 20)
        Me.chkOverWriteIndex.TabIndex = 41
        Me.chkOverWriteIndex.Text = "No sobreescribir atributo"
        Me.chkOverWriteIndex.UseVisualStyleBackColor = False
        '
        'lblSecondaryValue
        '
        Me.lblSecondaryValue.AutoSize = True
        Me.lblSecondaryValue.BackColor = System.Drawing.Color.Transparent
        Me.lblSecondaryValue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblSecondaryValue.FontSize = 9.75!
        Me.lblSecondaryValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSecondaryValue.Location = New System.Drawing.Point(19, 30)
        Me.lblSecondaryValue.Name = "lblSecondaryValue"
        Me.lblSecondaryValue.Size = New System.Drawing.Size(117, 16)
        Me.lblSecondaryValue.TabIndex = 38
        Me.lblSecondaryValue.Text = "Valor secundario"
        Me.lblSecondaryValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSecondaryValue
        '
        Me.txtSecondaryValue.Location = New System.Drawing.Point(145, 30)
        Me.txtSecondaryValue.Name = "txtSecondaryValue"
        Me.txtSecondaryValue.Size = New System.Drawing.Size(606, 21)
        Me.txtSecondaryValue.TabIndex = 19
        Me.txtSecondaryValue.Text = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 358)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.SplitContainer1.Panel1Collapsed = True
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lstCondiciones)
        Me.SplitContainer1.Size = New System.Drawing.Size(754, 167)
        Me.SplitContainer1.SplitterDistance = 158
        Me.SplitContainer1.TabIndex = 39
        '
        'lstCondiciones
        '
        Me.lstCondiciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstCondiciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCondiciones.ItemHeight = 16
        Me.lstCondiciones.Location = New System.Drawing.Point(0, 0)
        Me.lstCondiciones.Name = "lstCondiciones"
        Me.lstCondiciones.Size = New System.Drawing.Size(754, 167)
        Me.lstCondiciones.TabIndex = 33
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.ForeColor = System.Drawing.Color.White
        Me.btnEliminar.Location = New System.Drawing.Point(137, 5)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(97, 26)
        Me.btnEliminar.TabIndex = 32
        Me.btnEliminar.Text = "Eliminar Seleccionado"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgregar.ForeColor = System.Drawing.Color.White
        Me.btnAgregar.Location = New System.Drawing.Point(25, 5)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(96, 26)
        Me.btnAgregar.TabIndex = 31
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.btnEliminar)
        Me.ZPanel1.Controls.Add(Me.btnAgregar)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(3, 321)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(754, 37)
        Me.ZPanel1.TabIndex = 40
        '
        'ZPanel2
        '
        Me.ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel2.Controls.Add(Me.btnAceptar)
        Me.ZPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZPanel2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel2.Location = New System.Drawing.Point(3, 525)
        Me.ZPanel2.Name = "ZPanel2"
        Me.ZPanel2.Size = New System.Drawing.Size(754, 45)
        Me.ZPanel2.TabIndex = 41
        '
        'UCDoFillIndex
        '
        Me.Name = "UCDoFillIndex"
        Me.Size = New System.Drawing.Size(768, 602)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.panelList.ResumeLayout(False)
        Me.panelValues.ResumeLayout(False)
        Me.panelValues.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ZPanel1.ResumeLayout(False)
        Me.ZPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    ''' Método que sirve para validar el tipo de Atributo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidDataType(ByVal Index As Zamba.Core.Index) As Boolean

        Select Case Index.Type
            Case IndexDataType.Alfanumerico
                Return True
            Case IndexDataType.Alfanumerico_Largo
                Return True
            Case IndexDataType.Fecha
                Dim timeTryParse As DateTime
                Return DateTime.TryParse(Index.DataTemp, timeTryParse)
            Case IndexDataType.Fecha_Hora
                Dim timeTryParse As DateTime
                Return DateTime.TryParse(Index.DataTemp, timeTryParse)
            Case IndexDataType.Moneda
                Return True 'TODO Validar dato Moneda
            Case IndexDataType.Numerico
                Dim intTryParse As Integer
                Return Integer.TryParse(Index.DataTemp, intTryParse)
            Case IndexDataType.Numerico_Decimales
                Dim longTryParse As Long
                Return Long.TryParse(Index.DataTemp, longTryParse)
            Case IndexDataType.Numerico_Largo
                Dim int As Int64
                Return Int64.TryParse(Index.DataTemp, int)
            Case IndexDataType.Si_No
                Dim int As Integer
                Return Integer.TryParse(Index.DataTemp, int) ' int > 0 false ; i = 0 true
        End Select

    End Function

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	06/10/2008	Modified
    ''' </history> 
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        If lstCondiciones.Items.Count > 0 Then
            Rule = CurrentRule

            lstIndices_SelectedIndexChanged(Nothing, Nothing)
            Dim totRules As Int32

            CurrentRule.ClearRule()

            For Each Item As String In lstCondiciones.Items
                If Item.Trim() <> String.Empty Then
                    CurrentRule.IndexId &= Zamba.Core.IndexsBusiness.GetIndexId(Item.Split("|")(0)) & "|"
                    CurrentRule.PrimaryValue &= Item.Split("|")(1) & "|"
                    CurrentRule.SecondaryValue &= Item.Split("|")(2) & "|"
                    CurrentRule.OverWriteIndex &= CInt(CBool(Item.Split("|")(3))).ToString() & "|"
                End If
            Next

            CurrentRule.IndexId = CurrentRule.IndexId.Substring(0, CurrentRule.IndexId.Length - 1)
            CurrentRule.PrimaryValue = CurrentRule.PrimaryValue.Substring(0, CurrentRule.PrimaryValue.Length - 1)
            CurrentRule.SecondaryValue = CurrentRule.SecondaryValue.Substring(0, CurrentRule.SecondaryValue.Length - 1)
            CurrentRule.OverWriteIndex = CurrentRule.OverWriteIndex.Substring(0, CurrentRule.OverWriteIndex.Length - 1)

            totRules = CurrentRule.IndexId.Split("|").Length

            If CurrentRule.IndexId.Equals(String.Empty) Then Exit Sub

            If Not CurrentRule.PrimaryValue.Split("|").Length.Equals(totRules) _
                OrElse Not CurrentRule.SecondaryValue.Split("|").Length.Equals(totRules) OrElse Not CurrentRule.OverWriteIndex.Split("|").Length.Equals(totRules) Then
                MessageBox.Show("No todos los atributos poseen la misma cantidad de parametros, Verifique", "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.IndexId)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.PrimaryValue)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 2, CurrentRule.SecondaryValue)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 3, CurrentRule.OverWriteIndex)
            UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
        End If
    End Sub

    Private Sub UCDoFillIndex_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try


            If SplitContainer1.Panel1.Controls.Count = 0 Then

                Dim uc As New UCZRuleValueFunctions()
                uc.Dock = DockStyle.Fill
                AddHandler uc.ItemSelected, AddressOf AddRuleValueFunction

                SplitContainer1.Panel1.Controls.Add(uc)

            End If

            LoadIndexs()
            lstIndices.DisplayMember = "Name"
            If String.IsNullOrEmpty(CurrentRule.IndexId) = False Then
                For i As Int32 = 0 To CurrentRule.IndexId.Split("|").Length - 1
                    If Not String.IsNullOrEmpty(CurrentRule.OverWriteIndex.Split("|")(i)) Then
                        lstCondiciones.Items.Add(Zamba.Core.IndexsBusiness.GetIndexName(CurrentRule.IndexId.Split("|")(i), False) & "|" _
                                                    & CurrentRule.PrimaryValue.Split("|")(i) & "|" _
                                                    & CurrentRule.SecondaryValue.Split("|")(i) & "|" _
                                                    & CBool(CurrentRule.OverWriteIndex.Split("|")(i)).ToString())
                    Else
                        lstCondiciones.Items.Add(Zamba.Core.IndexsBusiness.GetIndexName(CurrentRule.IndexId.Split("|")(i), False) & "|" _
                                                    & CurrentRule.PrimaryValue.Split("|")(i) & "|" _
                                                    & CurrentRule.SecondaryValue.Split("|")(i) & "|" _
                                                    & "true")
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub LoadIndexs()
        Dim hash As Hashtable = IndexsBusiness.getAllIndexes
        If Not IsNothing(hash) AndAlso hash.Values.Count > 0 Then
            For Each item As Object In hash.Values
                Dim index As Index = DirectCast(item, Index)
                lstIndices.Items.Add(index)
                index = Nothing
            Next
        End If
    End Sub
    Private Sub lstIndices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstIndices.SelectedIndexChanged
        Try
            SelectedIndex = DirectCast(lstIndices.SelectedItem, Index)
            lblType.Text = _selectedIndex.Type.ToString.Replace("_", " ")

            ZPanel3.Controls.Clear()
            Display = New DisplayindexCtl(SelectedIndex, True)
            Display.Dock = DockStyle.Fill
            If Display IsNot Nothing AndAlso Not ZPanel3.Controls.Contains(Display) Then
                ZPanel3.Controls.Add(Display)
            End If
            txtSecondaryValue.Text = String.Empty
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

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkVerFuncionesValor.CheckedChanged
        SplitContainer1.Panel1Collapsed = Not chkVerFuncionesValor.Checked
    End Sub

    Private Sub AddRuleValueFunction(ByVal Valuefunction As String)
        txtSecondaryValue.Text += " " + Valuefunction
    End Sub
    Public Shared Event OverWrite(ByVal OverWrite As Boolean)

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Try
            If Display Is Nothing Then Exit Sub
            Dim response As MsgBoxResult = MsgBoxResult.Ok
            If Display.Index.DropDown And String.IsNullOrEmpty(txtSecondaryValue.Text) And String.IsNullOrEmpty(Display.Index.DataTemp) Then
                'If (String.IsNullOrEmpty(txtSecondaryValue.Text)) Then
                'MessageBox.Show("El valor escrito no se puede asignar al indice, verifique que el valor sea del tipo de dato del Atributo", "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                response = MessageBox.Show("Asignará un valor vacio al indice especificado, ¿esta seguro que desea asignar este valor?", "Zamba - WorkFlow - Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                'End If

                'ElseIf Not ValidDataType(Display.Index) Then
                '    response = MessageBox.Show("El valor escrito no se puede asignar al indice, verifique que el valor sea del tipo de dato del Atributo", "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            If response = MsgBoxResult.Ok Then
                lstCondiciones.Items.Add(Display.Index.Name & "|" & IIf(String.IsNullOrEmpty(Display.Index.DataTemp), String.Empty, Display.Index.DataTemp.ToString()) & "|" & txtSecondaryValue.Text.ToString() & "|" & chkOverWriteIndex.Checked.ToString())
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        Try
            lstCondiciones.Items.RemoveAt(lstCondiciones.SelectedIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lstCondiciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstCondiciones.SelectedIndexChanged
        Try
            SetIndicesValues(Zamba.Core.IndexsBusiness.GetIndexIdByName(DirectCast(lstCondiciones.Items(lstCondiciones.SelectedIndex), String).Split("|")(0).ToString().Trim()), DirectCast(lstCondiciones.Items(lstCondiciones.SelectedIndex), String).Split("|")(1).ToString())
            txtSecondaryValue.Text = DirectCast(lstCondiciones.Items(lstCondiciones.SelectedIndex), String).Split("|")(2).ToString()
            chkOverWriteIndex.Checked = Boolean.Parse(DirectCast(lstCondiciones.Items(lstCondiciones.SelectedIndex), String).Split("|")(3).ToString())
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SetIndicesValues(ByVal intId As Int64, ByVal Data As String)
        Dim i As Integer

        For i = 0 To lstIndices.Items.Count - 1
            Dim localIndex As Index = DirectCast(lstIndices.Items.Item(i), Index)
            If localIndex.ID = intId Then
                lstIndices.SelectedIndex = i
                SelectedIndex = lstIndices.Items(lstIndices.SelectedIndex)
                SelectedIndex.Data = Data
                SelectedIndex.Name = localIndex.Name
                lstIndices.Items.Item(i) = SelectedIndex
                Exit For
            End If
        Next
    End Sub
End Class