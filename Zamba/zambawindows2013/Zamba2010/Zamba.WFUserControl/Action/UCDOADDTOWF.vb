Imports Zamba.Data

Public Class UCDOADDTOWF
    Inherits ZRuleControl

    'Regla que se va a configurar
    Dim CurrentRule As IDOADDTOWF

    'El New debe recibir la regla a configurar
    Public Sub New(ByRef CurrentRule As IDOADDTOWF, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule
        Dim dsParaitems As DataSet = WFRulesBusiness.GetRuleParamItems(CurrentRule.ID, False)
        For Each row As DataRow In dsParaitems.Tables(1).Rows
            '[Sebastian 08-06-2009] evalua si tiene el check de la configuracion para mostrar la tarea tildado
            If String.Compare(row("item").ToString, "1") = 0 And String.Compare(row("value").ToString.ToLower, "show") = 0 Then
                chkShowOrNot.Checked = True
            End If

        Next
        'Me.chkExecuteRuleWhenResult.Checked = 
        this_Load()
    End Sub
    Public DsWf As New DsWF

#Region "Metodos Locales"
    Private Sub this_Load()
        Try
            Dim i As Int32 = 0
            DsWf = WFFactory.GetWFs(0)
            cargarWF()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Carga los WF existentes en el ListView
    Private Sub cargarWF()
        Dim WorkId As Int64 = 0
        'Esto se comento porque no levantaba la configuracion de la wfruleparamitems. 
        'Seleccionaba el WF actual
        'If Not IsNothing(MyRule) Then
        '    WorkId = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
        'End If
        For Each wf As DsWF.WFRow In DsWf.WF
            Try
                If CurrentRule.WorkId = wf.Work_ID Then
                    ListView1.Items.Add(New WFItem(wf, True))
                Else
                    ListView1.Items.Add(New WFItem(wf, False))
                End If
            Catch
            End Try
        Next
    End Sub

    Private Sub FsButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FsButton1.Click
        Try
            If ListView1.Items.Count > 0 Then
                If ListView1.SelectedItems.Count > 0 AndAlso Not ListView1.SelectedItems(0) Is Nothing Then
                    Dim WFItem As WFItem = ListView1.SelectedItems(0)
                    'guardo el o los parametros de configuracion
                    WFRulesBusiness.UpdateParamItem(CurrentRule, 0, WFItem.wf.Work_ID)
                    If chkShowOrNot.Checked = True Then
                        WFRulesBusiness.UpdateParamItem(CurrentRule, 1, "Show")
                    Else
                        WFRulesBusiness.UpdateParamItem(CurrentRule, 1, "NotShow")
                    End If
                    UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
                    'Pongo en negrita el workflow usado
                    For Each l As ListViewItem In ListView1.Items
                        DirectCast(l, WFItem).SelectedStep = False
                    Next
                    WFItem.SelectedStep = True

                    'Cambio el workflow en la regla
                    MyRule.WorkId = WFItem.wf.Work_ID
                Else
                    MsgBox("Debe seleccionar un Workflow", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#End Region
    Public Shadows ReadOnly Property MyRule() As IDOADDTOWF
        Get
            Return DirectCast(Rule, IDOADDTOWF)
        End Get
    End Property
    Private Class WFItem
        Inherits ListViewItem
        Public wf As DsWF.WFRow
        Private _SelectedStep As Boolean
        Private _SelectedCheck As Boolean

        Sub New(ByVal WF As DsWF.WFRow, ByVal SelectedStep As Boolean)
            Me.wf = WF
            Text = WF.Name
            Me.SelectedStep = SelectedStep
        End Sub

        Public Property SelectedStep() As Boolean
            Get
                Return _SelectedStep
            End Get
            Set(ByVal Value As Boolean)
                _SelectedStep = Value
                'Si es el que se usa en la regla actualmente va en negrita
                If Value = 0 Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property
    End Class
    'Actualiza el nombre de la etapa inicial
    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim WFStep As WFStep
        If ListView1.Items.Count > 0 Then
            If ListView1.SelectedItems.Count > 0 Then
                Dim WFItem As WFItem = ListView1.SelectedItems(0)
                If WFItem.wf.InitialStepId > 0 Then
                    WFStep = WFStepBusiness.GetStepById(WFItem.wf.InitialStepId)
                    lblInicial.Text = WFStep.Name
                End If
            End If
        End If
    End Sub
End Class
