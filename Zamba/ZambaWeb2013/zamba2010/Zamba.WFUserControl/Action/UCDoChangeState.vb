Public Class UCDoChangeState
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend Shadows WithEvents lblSeleccionarEstado As ZLabel
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend Shadows WithEvents cmbEtapa As ComboBox
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        lblSeleccionarEstado = New ZLabel()
        btnSeleccionar = New ZButton()
        ListView1 = New System.Windows.Forms.ListView()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        cmbEtapa = New ComboBox()
        Label1 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(cmbEtapa)
        tbRule.Controls.Add(ListView1)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(lblSeleccionarEstado)
        tbRule.Size = New System.Drawing.Size(396, 348)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(404, 374)
        '
        'lblSeleccionarEstado
        '
        lblSeleccionarEstado.BackColor = System.Drawing.Color.Transparent
        lblSeleccionarEstado.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblSeleccionarEstado.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblSeleccionarEstado.Location = New System.Drawing.Point(12, 57)
        lblSeleccionarEstado.Name = "lblSeleccionarEstado"
        lblSeleccionarEstado.Size = New System.Drawing.Size(120, 16)
        lblSeleccionarEstado.TabIndex = 11
        lblSeleccionarEstado.Text = "Nuevo estado"
        lblSeleccionarEstado.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnSeleccionar
        '
        btnSeleccionar.Location = New System.Drawing.Point(143, 319)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(66, 23)
        btnSeleccionar.TabIndex = 13
        btnSeleccionar.Text = "Aceptar"
        '
        'ListView1
        '
        ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        ListView1.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ListView1.FullRowSelect = True
        ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListView1.HideSelection = False
        ListView1.Location = New System.Drawing.Point(15, 76)
        ListView1.MultiSelect = False
        ListView1.Name = "ListView1"
        ListView1.Size = New System.Drawing.Size(312, 235)
        ListView1.TabIndex = 14
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 200
        '
        'cmbEtapa
        '
        cmbEtapa.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEtapa.FormattingEnabled = True
        cmbEtapa.Location = New System.Drawing.Point(164, 22)
        cmbEtapa.Name = "cmbEtapa"
        cmbEtapa.Size = New System.Drawing.Size(163, 21)
        cmbEtapa.TabIndex = 15
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(103, 22)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(55, 16)
        Label1.TabIndex = 11
        Label1.Text = "Etapa:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCDoChangeState
        '
        Name = "UCDoChangeState"
        Size = New System.Drawing.Size(404, 374)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private this As IDoChangeState
    Public Sub New(ByRef p_DoChangeState As IDoChangeState, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(p_DoChangeState, _wfPanelCircuit)
        InitializeComponent()
        SuspendLayout()
        Try
            CargarEtapas()
            'CargarEstados(myrule.StepId)
            If (cmbEtapa.Items.Count > 0) Then
                CargarEstados(cmbEtapa.SelectedValue)
            End If
            'For Each state As wfstepstate In this.WFStep.States.Values
            '    If state Is this.State Then
            '        Me.ListView1.Items.Add(New StateItem(state, True))
            '    Else
            '        Me.ListView1.Items.Add(New StateItem(state, False))
            '    End If
            'Next
            RemoveHandler cmbEtapa.SelectedIndexChanged, AddressOf cmbEtapa_SelectedIndexChanged
            AddHandler cmbEtapa.SelectedIndexChanged, AddressOf cmbEtapa_SelectedIndexChanged
            tbctrMain.Dock = DockStyle.Fill
            tbctrMain.Size = New Size(1500, 1500)
            '[Sebastian 17-04-09] Esto setea cuando se crea una regla do change state a que la primera etapa de la lista 
            'sea la definida como estado inicial. El cero es un parametro para que elija el primero de 
            ' de estados la(lista)

            SelectStateAtBegin(0)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        ResumeLayout()
    End Sub

    Private Sub CargarEstados(ByVal p_iStepId As Int32)
        Dim Wfstep As WFStep
        ListView1.SuspendLayout()
        Try
            Wfstep = WFStepBusiness.GetStepById(p_iStepId, True)
            ListView1.Clear()
            If Not Wfstep Is Nothing Then
                For Each estado As WFStepState In Wfstep.States
                    If estado.ID = MyRule.StateId Then
                        ListView1.Items.Add(New StateItem(estado, True))
                    Else
                        ListView1.Items.Add(New StateItem(estado, False))
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        ListView1.ResumeLayout()
    End Sub

    Private Sub CargarEtapas()
        Dim i_dsSteps As DsSteps
        cmbEtapa.SuspendLayout()
        Try
            Dim workId As Int64 = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
            'Dim wf As IWorkFlow
            'wf = WFBusiness.GetWorkflowByStepId(MyRule.WFStepId)

            'If Not IsNothing(wf) Then
            If workId > 0 Then
                i_dsSteps = WFStepBusiness.GetDsSteps(workId)
                If Not i_dsSteps Is Nothing Then
                    cmbEtapa.DataSource = i_dsSteps.WFSteps
                    'cmbEtapa.DataSource = WF.Steps.Values
                    cmbEtapa.ValueMember = "Step_Id"
                    'cmbEtapa.ValueMember = "ID"
                    cmbEtapa.DisplayMember = "Name"
                    ' Esto se agregó para que venga seleccionada por default la etapa en la cual se está posicionado
                    If MyRule.StepId > 0 Then
                        cmbEtapa.SelectedValue = Convert.ToInt64(MyRule.StepId)
                    ElseIf MyRule.WFStepId > 0 Then
                        cmbEtapa.SelectedValue = Convert.ToInt64(MyRule.WFStepId)
                    End If
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        cmbEtapa.ResumeLayout()
    End Sub

    Private Class StateItem
        Inherits ListViewItem
        Public State As WFStepState
        Private _SelectedState As Boolean

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

        Sub New(ByVal State As WFStepState, ByVal SelectedState As Boolean)
            Me.State = State
            Text = Me.State.Name
            Me.SelectedState = SelectedState
        End Sub
    End Class
    ''' <summary>
    ''' [Sebastian 17-04-09] Setea el estado de la configuracion de la regla según el parametro que se
    ''' le pase. En este caso esta hecho para que siempre sea el primero de los estados (o sea el cero).
    ''' </summary>
    ''' <param name="ItemToSelect"></param>
    ''' <remarks></remarks>
    Private Sub SelectStateAtBegin(ByVal ItemToSelect As Int32)
        Try
            Dim OneSelected As Boolean = False
            Dim StateItem As StateItem
            '[Sebastian 17-04-09] Verifico si la regla ya tiene algún estado configurado como inicial.
            For Each item As StateItem In ListView1.Items
                If item.SelectedState = True Then
                    OneSelected = True
                    Exit For
                End If
            Next
            '[Sebastian 17-04-09] si el flag ONESELECTED es true quiere decir que
            'la regla ya tiene un estado configurada como inicial y entonces no
            'la cambio.
            If OneSelected = False Then
                If ListView1.Items.Count > 0 Then
                    StateItem = ListView1.Items(ItemToSelect)
                    Dim StepId As Int32 = cmbEtapa.SelectedValue

                    WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, StateItem.State.ID)
                    WFRulesBusiness.UpdateParamItem(MyRule.ID, 1, StepId)
                    UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
                    For Each l As ListViewItem In ListView1.Items
                        DirectCast(l, StateItem).SelectedState = False
                    Next
                    StateItem.SelectedState = True
                    MyRule.StateId = StateItem.State.ID
                    MyRule.StepId = StepId
                End If
            End If

        Catch ex As Exception
            Dim exn As New Exception("Error en UCDoChangeState.btnSeleccionar_Click(), excepción: " & ex.ToString)
            Zamba.Core.ZClass.raiseerror(exn)
            MessageBox.Show("Excepción al guardar: " & ex.Message, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            '[Sebastian 17-04-09]  Me.ListView1.SelectedItems.Count se agrego esto porque sino lanzaba
            'exception con la segunda parte de la condicion porque no podia acceder al elemento cero.
            If ListView1.SelectedItems.Count > 0 AndAlso Not ListView1.SelectedItems(0) Is Nothing Then
                Dim StateItem As StateItem = ListView1.SelectedItems(0)
                Dim StepId As Int32 = cmbEtapa.SelectedValue

                WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, StateItem.State.ID)
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 1, StepId)
                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
                For Each l As ListViewItem In ListView1.Items
                    DirectCast(l, StateItem).SelectedState = False
                Next
                StateItem.SelectedState = True
                MyRule.StateId = StateItem.State.ID
                MyRule.StepId = StepId
            End If
        Catch ex As Exception
            Dim exn As New Exception("Error en UCDoChangeState.btnSeleccionar_Click(), excepción: " & ex.ToString)
            Zamba.Core.ZClass.raiseerror(exn)
            MessageBox.Show("Excepción al guardar: " & ex.Message, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Shadows ReadOnly Property MyRule() As IDoChangeState
        Get
            Return DirectCast(Rule, IDoChangeState)
        End Get
    End Property

    Private Sub cmbEtapa_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            CargarEstados(cmbEtapa.SelectedValue)
        Catch
        End Try
    End Sub

End Class