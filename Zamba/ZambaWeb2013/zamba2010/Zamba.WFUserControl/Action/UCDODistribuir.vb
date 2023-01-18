'Imports Zamba.WFBusiness

Public Class UCDODISTRIBUIR
    'Los controles de Reglas de Accion deben heredar de ZRuleControl
    Inherits ZRuleControl


#Region " Código generado por el Diseñador de Windows Forms "
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents FsButton1 As ZButton
    Friend WithEvents ZPanel1 As ZPanel
    Private WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Label4 = New ZLabel()
        ListView1 = New System.Windows.Forms.ListView()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        FsButton1 = New ZButton()
        ZPanel1 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        ZPanel1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(ListView1)
        tbRule.Controls.Add(ZPanel1)
        tbRule.Controls.Add(Label4)
        tbRule.Size = New System.Drawing.Size(421, 401)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(429, 430)
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Dock = System.Windows.Forms.DockStyle.Top
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(3, 3)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(415, 24)
        Label4.TabIndex = 10
        Label4.Text = "Seleccione la etapa de distribucion"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'ListView1
        '
        ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListView1.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ListView1.FullRowSelect = True
        ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListView1.HideSelection = False
        ListView1.Location = New System.Drawing.Point(3, 27)
        ListView1.MultiSelect = False
        ListView1.Name = "ListView1"
        ListView1.Size = New System.Drawing.Size(415, 320)
        ListView1.TabIndex = 11
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 200
        '
        'FsButton1
        '
        FsButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        FsButton1.FlatStyle = FlatStyle.Flat
        FsButton1.ForeColor = System.Drawing.Color.White
        FsButton1.Location = New System.Drawing.Point(302, 12)
        FsButton1.Name = "FsButton1"
        FsButton1.Size = New System.Drawing.Size(87, 27)
        FsButton1.TabIndex = 12
        FsButton1.Text = "Guardar"
        FsButton1.UseVisualStyleBackColor = False
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(FsButton1)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 347)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(415, 51)
        ZPanel1.TabIndex = 15
        '
        'UCDODISTRIBUIR
        '
        Name = "UCDODISTRIBUIR"
        Size = New System.Drawing.Size(429, 430)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ZPanel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region


    Dim CurrentRule As IDoDistribuir
    'El New debe recibir la regla a configurar
    Public Sub New(ByRef CurrentRule As IDoDistribuir, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
        Me.CurrentRule = CurrentRule
    End Sub


    'Para guardar los parametros de una regla se usa el siguiente metodo, al que se le pasa:
    'la regla en si, un item y el valor
    'WFRulesBusiness.UpdateParamItemAction(CurrentRule, 0, StepItem.WFStep.Id)

    'Se llama a este metodo para actualizar el nombre en el administrador al finalizar la configuracion
    'Me.RaiseUpdateMaskName()

#Region "Metodos Locales"
    Private Sub this_Load()
        Try
            FillSteps()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub FillSteps()
        Dim wfid As Int64 = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
        If wfid > 0 Then
            Dim stepsIDsAndNames As Dictionary(Of Int64, String) = WFStepBusiness.GetStepsIdAndNamebyWorkflowId(wfid)

            For Each sID As Int64 In stepsIDsAndNames.Keys
                Try
                    If sID = Rule.WFStepId Then
                        Exit Try
                    End If
                    If MyRule.NewWFStepId = sID Then
                        ListView1.Items.Add(New StepItem(sID, stepsIDsAndNames(sID), True))
                    Else
                        ListView1.Items.Add(New StepItem(sID, stepsIDsAndNames(sID), False))
                    End If
                Catch
                End Try
            Next
        End If
    End Sub

    Private Sub FsButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FsButton1.Click
        Try
            If ListView1.Items.Count > 0 Then
                If Not ListView1.SelectedItems(0) Is Nothing Then
                    Dim StepItem As StepItem = ListView1.SelectedItems(0)

                    'guardo el o los parametros de configuracion
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 0, StepItem.WFStepID)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 1, 0)
                    For Each l As ListViewItem In ListView1.Items
                        DirectCast(l, StepItem).SelectedStep = False
                    Next

                    StepItem.SelectedStep = True
                    MyRule.NewWFStepId = StepItem.WFStepID

                    'llamo a este metodo para actualizar el nombre en el administrador
                    If MyRule.NewWFStepId > 0 Then
                        Dim StepName As String = Zamba.Core.WFStepBusiness.GetStepNameById(MyRule.NewWFStepId)
                        MyRule.Name = "Envio a " & StepName
                    Else
                        MyRule.Name = "Distribuir SIN CONFIGURAR"
                    End If
                    WFRulesBusiness.UpdateRuleNameByID(MyRule.ID, MyRule.Name)

                    UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")

                    RaiseUpdateMaskName()
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Public Shadows ReadOnly Property MyRule() As IDoDistribuir
        Get
            Return DirectCast(Rule, IDoDistribuir)
        End Get
    End Property

    Private Class StepItem
        Inherits ListViewItem
        Public WFStepID As Int64
        Private _SelectedStep As Boolean
        Private _SelectedCheck As Boolean

        Sub New(ByVal stepID As Int64, ByVal name As String, ByVal SelectedStep As Boolean)
            WFStepID = stepID
            Text = name
            Me.SelectedStep = SelectedStep
        End Sub

        Public Property SelectedStep() As Boolean
            Get
                Return _SelectedStep
            End Get
            Set(ByVal Value As Boolean)
                _SelectedStep = Value
                If Value = 0 Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property
    End Class
End Class