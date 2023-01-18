'Imports Zamba.WFBusiness

Public Class UCIfTaskState
    Inherits ZRuleControl

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
    Friend WithEvents stateList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents optDifferent As System.Windows.Forms.RadioButton
    Friend WithEvents optEqual As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As ZLabel

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        FsButton1 = New ZButton()
        Label1 = New ZLabel()
        stateList = New System.Windows.Forms.ListView()
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
        tbRule.Controls.Add(stateList)
        tbRule.Controls.Add(FsButton1)
        tbRule.Controls.Add(Label1)
        tbRule.Size = New System.Drawing.Size(418, 324)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(426, 353)
        '
        'FsButton1
        '
        FsButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        FsButton1.FlatStyle = FlatStyle.Flat
        FsButton1.ForeColor = System.Drawing.Color.White
        FsButton1.Location = New System.Drawing.Point(283, 268)
        FsButton1.Name = "FsButton1"
        FsButton1.Size = New System.Drawing.Size(80, 32)
        FsButton1.TabIndex = 16
        FsButton1.Text = "Guardar"
        FsButton1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(13, 98)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(69, 20)
        Label1.TabIndex = 11
        Label1.Text = "Estado"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'stateList
        '
        stateList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        stateList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        stateList.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        stateList.FullRowSelect = True
        stateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        stateList.HideSelection = False
        stateList.Location = New System.Drawing.Point(16, 126)
        stateList.MultiSelect = False
        stateList.Name = "stateList"
        stateList.Size = New System.Drawing.Size(347, 136)
        stateList.TabIndex = 17
        stateList.UseCompatibleStateImageBehavior = False
        stateList.View = System.Windows.Forms.View.List
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
        optDifferent.Location = New System.Drawing.Point(21, 65)
        optDifferent.Name = "optDifferent"
        optDifferent.Size = New System.Drawing.Size(160, 30)
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
        Label2.Size = New System.Drawing.Size(269, 24)
        Label2.TabIndex = 18
        Label2.Text = "Seleccione el operador"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCIfTaskState
        '
        Name = "UCIfTaskState"
        Size = New System.Drawing.Size(426, 353)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Shadows ReadOnly Property MyRule() As IIfTaskState
        Get
            Return DirectCast(Rule, IIfTaskState)
        End Get
    End Property


    ' Constructor
    Public Sub New(ByRef IfTaskState As IIfTaskState, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfTaskState, _wfPanelCircuit)
        InitializeComponent()
        fillList()
        FillComprarator()
    End Sub

    Private Sub fillList()
        Try

            Dim tmp_enumerado As Zamba.Core.TaskStates
            Dim list As Array = [Enum].GetValues(GetType(Zamba.Core.TaskStates))
            stateList.Clear()
            Dim ListofSelectedStates As New ArrayList
            If Not IsNothing(MyRule.States) Then
                ListofSelectedStates.AddRange(MyRule.States.Split(MyRule.SEPARADOR_INDICE))

            End If

            For Each stateValue As Integer In list
                tmp_enumerado = stateValue
                For Each State As String In ListofSelectedStates
                    If String.Compare(stateValue, State) = 0 Then
                        stateList.Items.Add(New StateItem(tmp_enumerado, True))
                        Exit For
                    Else
                        stateList.Items.Add(New StateItem(tmp_enumerado, False))
                        Exit For
                    End If
                Next
            Next

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub FillComprarator()
        If Me.MyRule.Comp = Comparators.Equal Then
            optEqual.Checked = True
        Else
            optDifferent.Checked = True
        End If
    End Sub


    Private Sub FsButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FsButton1.Click
        Try
            Dim SelectedStates As String = String.Empty
            Dim SelectedStatesCount As Int16
            SelectedStatesCount = stateList.SelectedItems.Count

            For Each l As ListViewItem In stateList.Items
                DirectCast(l, StateItem).SelectedState = False
            Next

            If SelectedStatesCount > 0 Then
                For Each StateItem As StateItem In stateList.SelectedItems
                    StateItem.SelectedState = True
                    SelectedStates &= Int32.Parse(StateItem.state) & ","
                Next
            Else
                MsgBox("Debe seleccionar al menos un estado de la lista")
            End If

            MyRule().States = SelectedStates
            WFRulesBusiness.UpdateParamItem(MyRule, 0, MyRule().States)

            If optEqual.Checked = True Then
                MyRule.Comp = Comparators.Equal
                WFRulesBusiness.UpdateParamItem(MyRule, 1, Int32.Parse(MyRule.Comp))
            ElseIf optDifferent.Checked = True Then
                MyRule.Comp = Comparators.Different
                WFRulesBusiness.UpdateParamItem(MyRule, 1, Int32.Parse(MyRule.Comp))
            End If
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Catch ex As Exception
            Dim exn As New Exception("Error en UCDoChangeState.btnSeleccionar_Click(), excepción: " & ex.ToString)
            zamba.core.zclass.raiseerror(exn)
            MessageBox.Show("Excepción al guardar: " & ex.Message, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#Region "Clase item de la lista de estados"
    Private Class StateItem
        Inherits ListViewItem

        Protected _SelectedState As Boolean
        Protected m_state As Zamba.Core.TaskStates

        Public Property state() As Zamba.Core.TaskStates
            Get
                Return m_state
            End Get
            Set(ByVal Value As Zamba.Core.TaskStates)
                m_state = Value
            End Set
        End Property

        Public Property SelectedState() As Boolean
            Get
                Return _SelectedState
            End Get
            Set(ByVal Value As Boolean)
                _SelectedState = Value
                If Value = 0 Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property

        Sub New(ByVal p_State As Zamba.Core.TaskStates, ByVal SelectedState As Boolean)
            state = p_State
            Text = state.ToString()
            Me.SelectedState = SelectedState
        End Sub
    End Class
#End Region

End Class