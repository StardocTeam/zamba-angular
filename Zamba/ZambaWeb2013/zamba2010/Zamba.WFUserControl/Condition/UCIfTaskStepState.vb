'Imports Zamba.WFBusiness

Public Class UCIfTaskStepState
    Inherits ZRuleControl

    Public Shadows ReadOnly Property MyRule() As IIfTaskStepState
        Get
            Return DirectCast(Rule, IIfTaskStepState)
        End Get
    End Property

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl1 reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents FsButton1 As ZButton
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents listaEstado As System.Windows.Forms.CheckedListBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents optDifferent As System.Windows.Forms.RadioButton
    Friend WithEvents optEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As ZLabel

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        FsButton1 = New ZButton()
        Label1 = New ZLabel()
        listaEstado = New System.Windows.Forms.CheckedListBox()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        optDifferent = New System.Windows.Forms.RadioButton()
        optEqual = New System.Windows.Forms.RadioButton()
        Label2 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(optDifferent)
        tbRule.Controls.Add(optEqual)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(listaEstado)
        tbRule.Controls.Add(FsButton1)
        tbRule.Controls.Add(Label1)
        tbRule.Size = New System.Drawing.Size(399, 302)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(407, 331)
        '
        'FsButton1
        '
        FsButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        FsButton1.FlatStyle = FlatStyle.Flat
        FsButton1.ForeColor = System.Drawing.Color.White
        FsButton1.Location = New System.Drawing.Point(275, 271)
        FsButton1.Name = "FsButton1"
        FsButton1.Size = New System.Drawing.Size(72, 28)
        FsButton1.TabIndex = 16
        FsButton1.Text = "Guardar"
        FsButton1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(13, 96)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(176, 24)
        Label1.TabIndex = 11
        Label1.Text = "Estado"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'listaEstado
        '
        listaEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        listaEstado.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        listaEstado.Location = New System.Drawing.Point(16, 123)
        listaEstado.Name = "listaEstado"
        listaEstado.Size = New System.Drawing.Size(331, 138)
        listaEstado.TabIndex = 17
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 200
        '
        'optDifferent
        '
        optDifferent.BackColor = System.Drawing.Color.Transparent
        optDifferent.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        optDifferent.Location = New System.Drawing.Point(21, 63)
        optDifferent.Name = "optDifferent"
        optDifferent.Size = New System.Drawing.Size(160, 34)
        optDifferent.TabIndex = 20
        optDifferent.Text = " <>"
        optDifferent.UseVisualStyleBackColor = False
        '
        'optEqual
        '
        optEqual.BackColor = System.Drawing.Color.Transparent
        optEqual.Font = New Font("Tahoma", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        optEqual.Location = New System.Drawing.Point(21, 38)
        optEqual.Name = "optEqual"
        optEqual.Size = New System.Drawing.Size(160, 30)
        optEqual.TabIndex = 19
        optEqual.Text = " ="
        optEqual.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(13, 13)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(243, 24)
        Label2.TabIndex = 18
        Label2.Text = "Seleccione el operador"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCIfTaskStepState
        '
        Name = "UCIfTaskStepState"
        Size = New System.Drawing.Size(407, 331)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByRef p_IfTaskStepState As IIfTaskStepState, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(p_IfTaskStepState, _wfPanelCircuit)
        InitializeComponent()
        FillList()
        FillComparator()
    End Sub


    ' Carga la lista de estados...
    Private Sub FillList()

        listaEstado.Items.Clear()
        Dim ruleStep As WFStep = WFStepBusiness.GetStepById(MyRule.WFStepId)
        For Each state As WFStepState In ruleStep.States
            Dim Find As Boolean = False
            For Each s As String In MyRule.States.Split(MyRule.SEPARADOR_INDICE)
                If String.Compare(state.ID, s) = 0 Then
                    Find = True
                    Exit For
                End If
            Next
            listaEstado.Items.Add(state.Name, Find)
        Next
    End Sub
    Private Sub FillComparator()
        If Me.MyRule.Comp = Comparators.Equal Then
            optEqual.Checked = True
        Else
            optDifferent.Checked = True
        End If
    End Sub

    ' Devuelve una secuencia de ids separados por coma
    '[Sebastian] 26-06-2009 se cambio de function a sub porque no devuelve nada
    'previamente se hizo un find all reference
    'Protected Function SetListId() As String
    Protected Sub SetListId()
        MyRule.States = String.Empty
        For Each item As String In listaEstado.CheckedItems
            MyRule.States &= GetStateId(item).ToString & MyRule.SEPARADOR_INDICE
        Next
    End Sub
    Private Function GetStateId(ByVal StateName As String) As Int32
        Dim ruleStep As WFStep = WFStepBusiness.GetStepById(MyRule.WFStepId)
        For Each state As WFStepState In ruleStep.States
            If String.Compare(state.Name, StateName, True) = 0 Then
                Return state.ID
            End If
        Next
        Return 0
    End Function

    '' Marca los items seleccionados por el usuario...
    Protected Sub markSelectedState()

        '    'For Each item As wfstepstate In Me.listaEstado.SelectedItems
        '    '    CType(item, StateItem).SelectedState = True
        '    'Next
    End Sub


    Private Sub FsButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FsButton1.Click
        Try
            SetListId()
            ' Se pasan los id seleccionados...
            If 0 < listaEstado.SelectedIndices.Count Then
                WFRulesBusiness.UpdateParamItem(MyRule, 0, MyRule.States)
                markSelectedState()
            End If

            ' Se pasa el comparador...
            If optEqual.Checked = True AndAlso MyRule.Comp <> Comparators.Equal Then
                MyRule.Comp = Comparators.Equal
                WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.Comp)
            ElseIf optDifferent.Checked = True AndAlso MyRule.Comp <> Comparators.Different Then
                MyRule.Comp = Comparators.Different
                WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.Comp)
            End If
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Catch ex As Exception
            Dim exn As New Exception("Error en UCIfTaskStepState.FsButton1_Click, excepción: " & ex.ToString)
            Zamba.Core.ZClass.raiseerror(exn)
            MessageBox.Show("Excepción al guardar: " & ex.Message, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    '#Region "Clase item de la lista en la vista"
    '    Protected Class StateItem
    '        Inherits ListViewItem

    '        Public State As wfstepstate
    '        Private _SelectedState As Boolean

    '        Public Property SelectedState() As Boolean
    '            Get
    '                Return _SelectedState
    '            End Get
    '            Set(ByVal Value As Boolean)
    '                _SelectedState = Value
    '                If Value = 0 Then
    '                    Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '                Else
    '                    Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '                End If
    '            End Set
    '        End Property


    '        Sub New(ByVal State As wfstepstate, ByVal SelectedState As Boolean)
    '            Me.State = State
    '            Me.Text = Me.State.Name
    '            Me.SelectedState = SelectedState
    '        End Sub

    '    End Class

    '#End Region

End Class