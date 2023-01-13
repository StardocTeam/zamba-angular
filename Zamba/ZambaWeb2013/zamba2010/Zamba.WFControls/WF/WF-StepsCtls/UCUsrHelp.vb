Imports Zamba.Core

Public Class UCUsrHelp
    Inherits ZControl

    Private ruleIds As Generic.List(Of Int64)
    Private stepid As Int64
    Private TextHelp As String = String.Empty
    Public Sub New(ByVal stepid As Int64, ByVal ruleIds As Generic.List(Of Int64), ByVal helpMessage As String)
        Me.ruleIds = ruleIds
        Me.stepid = stepid
        InitializeComponent()

        lblMessage.Text = helpMessage

        If ruleIds(0) = -1 Then
            grpRuleHelp.Visible = False
            grpExecutionMode.Visible = False
        End If
    End Sub


    Private Sub UCUsrHelp_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim dtDescription As DataTable = Nothing
        Try
            'Carga el texto del mensaje de ayuda.
            If grpRuleHelp.Visible AndAlso ruleIds.Count > 0 AndAlso ruleIds(0) <> -1 Then
                dtDescription = WFRulesBusiness.GetRuleOption(stepid, ruleIds(0), RuleSectionOptions.Regla, RulePreferences.UserActionHelp, 0, False)
                If dtDescription IsNot Nothing Then
                    txtDescription.Text = dtDescription.Rows(0)("ObjExtraData").ToString
                End If

                'Se selecciona el primero ya que todas las reglas de un mismo 
                'evento compartirán  el mismo modo de ejecución de tareas.
                If WFRulesBusiness.IsExecutionTaskByTask(stepid, ruleIds(0), False) Then
                    rdoTaskByTask.Checked = True
                Else
                    rdoAllTasks.Checked = True
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Se produjo un error inesperado al cargar los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dtDescription IsNot Nothing Then
                dtDescription.Dispose()
                dtDescription = Nothing
            End If
        End Try
    End Sub


    Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSaveHelp.Click
        '''------------------------------------------
        ''' [19/02/2010] Ale Ruetalo
        ''' La finalidad de este UsrControl es ingresar/Modificar una Ayuda o Descripicion sobre las UserActions
        ''' Guardando/Modificando esta Info en la Base de Datos
        '''------------------------------------------
        Dim RuleHelp As String = WFRulesBusiness.GetRuleOption(ruleIds(0), RuleSectionOptions.Regla, RulePreferences.UserActionHelp, 0, True, stepid, WFRulesBusiness.ValueTypes.Extra)

        ' Compara si hay cambios con el Textbox para hacer el Insert / Update
        If String.Compare(txtDescription.Text, RuleHelp) <> 0 Then
            WFBusiness.SetRulesPreferencesSinObjectId _
            (ruleIds(0), _
            RuleSectionOptions.Regla, _
            RulePreferences.UserActionHelp, 0, _
            txtDescription.Text)
        End If

        'Guarda los cambios del modo de ejecución de las tareas.
        If rdoAllTasks.Checked Then
            For Each id As Int64 In ruleIds
                WFBusiness.SetRulesPreferences(id, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 1)
            Next
        Else
            For Each id As Int64 In ruleIds
                WFBusiness.SetRulesPreferences(id, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 0)
            Next
        End If
    End Sub
End Class