Imports Zamba.Core
Imports Zamba.Core.WF.WF
Imports System.Windows.Forms
Imports Zamba.Data

''' <summary>
''' Se encarga de manejar los botones dinámicos de Zamba.
''' Son botones que se pueden configurar desde el administrador y 
''' se pueden ubicar en diferentes partes de Zamba. 
''' </summary>
''' <remarks></remarks>
Public Class GenericRuleManager
    ''' <summary>
    ''' Obtiene los filtros solicitados dependiendo de las opciones de visualización
    ''' del botón, la habilitación de la regla y los permisos del usuario.
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="place"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserButtons(ByVal filter As ButtonType, _
                                            ByVal place As ButtonPlace, _
                                            Optional ByVal wfId As Int64 = 0) As DataTable
        Dim tempDt As DataTable = Nothing
        Dim dtBut As DataTable = Nothing

        Try
            If filter = ButtonType.Rule Then
                tempDt = GenericButtonBusiness.GetRuleButtons(place, wfId)

                If tempDt.Rows.Count > 0 Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "* Se han encontrado " & tempDt.Rows.Count & " botones dinámicos en " & place.ToString)

                    Dim stepid As Int64
                    Dim ruleId As Int64
                    Dim ruleEnabled As Boolean = False
                    Dim dt, dt2 As DataTable

                    'Se guardarán aquellos botones a mostrar
                    dtBut = tempDt.Clone()

                    'Se recorren los botones verificando cuales serán los que se agregarán dependiendo de sus permisos y habilitaciones
                    For Each btn As DataRow In tempDt.Rows
                        ruleId = CLng(btn.Item("RULEID"))
                        stepid = CLng(btn.Item("step_id"))
                        Trace.WriteLineIf(ZTrace.IsInfo, "* Verificando habilitación de la regla " & ruleId)

                        'Verifica si la regla esta habilitada
                        If WFRulesBusiness.GetRuleEstado(ruleId, True) Then
                            'Busca si pertenece a un workflow específico
                            If (IsDBNull(btn.Item("WfID")) OrElse btn.Item("WfID").ToString = "0") AndAlso Not CBool(btn.Item("NeedRights")) Then
                                Trace.WriteLineIf(ZTrace.IsInfo, "* La regla es habilitada por no pertenecer a un proceso y no verificar permisos de usuario")
                                ruleEnabled = True
                            Else
                                'Verifica si el usuario tiene permisos de ejecución sobre esa etapa 
                                If WFFactory.CanExecuteRules(ruleId, Membership.MembershipHelper.CurrentUser.ID) Then
                                    ruleEnabled = True
                                    Dim selectionvalue As RulePreferences = WFRuleBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(stepid, ruleId, False)
                                    If selectionvalue = RulePreferences.HabilitationSelectionUser Then
                                        'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
                                        dt = WFStepBusiness.GetStepUserGroupsIdsAsDS(WFStepBusiness.GetStepIdByRuleId(ruleId, True), Membership.MembershipHelper.CurrentUser.ID)
                                        'Se Obtienen los ids de GRUPOS DESHABILITADOS
                                        dt2 = WFRulesBusiness.GetRuleOptionsDT(True, stepid)

                                        Dim dv As New DataView(dt2)
                                        dv.RowFilter = "ruleid=" & ruleId & " and SectionId=" & RuleSectionOptions.Habilitacion & " And ObjectId=" & RulePreferences.HabilitationTypeGroup
                                        'Por cada Grupo del usuario logeado y que tiene permiso la etapa, se fija si esta deshabilitado
                                        For Each rUser As DataRow In dt.Rows
                                            ruleEnabled = True
                                            For Each r As DataRow In dv.ToTable.Rows
                                                If rUser.Item(0).ToString() = r.Item("Objvalue").ToString() Then
                                                    ruleEnabled = False
                                                    Exit For
                                                End If
                                            Next

                                            'Si el grupo esta habilitado salgo asi queda habilitada la regla
                                            If ruleEnabled = True Then Exit For
                                        Next
                                        dv.Dispose()
                                        dv = Nothing
                                    End If
                                    selectionvalue = Nothing

                                    If ruleEnabled Then
                                        Trace.WriteLineIf(ZTrace.IsInfo, "* La regla se encuentra habilitada")
                                    Else
                                        Trace.WriteLineIf(ZTrace.IsInfo, "* La regla se encuentra deshabilitada por la solapa habilitación o permisos de etapa/wf")
                                    End If
                                Else
                                    Trace.WriteLineIf(ZTrace.IsInfo, "* No tiene permisos para ejecutar este workflow o etapa")
                                    ruleEnabled = False
                                End If
                            End If
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "* La regla se encuentra deshabilitada")
                            ruleEnabled = False
                        End If

                        If ruleEnabled Then
                            'Se agrega el botón a la lista de botones a mostrar
                            Dim row As DataRow = dtBut.NewRow()
                            row.Item("RULEID") = btn.Item("RULEID")
                            row.Item("BUTTONID") = btn.Item("BUTTONID")
                            row.Item("PARAMS") = btn.Item("PARAMS")
                            row.Item("NEEDRIGHTS") = btn.Item("NEEDRIGHTS")
                            row.Item("CAPTION") = btn.Item("CAPTION").ToString
                            row.Item("BUTTONORDER") = btn.Item("BUTTONORDER")

                            If dtBut.Columns.Contains("iconid") Then
                                row.Item("iconid") = btn.Item("iconid") 'Jere
                            End If

                            dtBut.Rows.Add(row)
                        End If
                    Next

                    If dtBut.Rows.Count > 0 Then
                        Return dtBut
                    Else
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            End If

        Catch ex As Exception
            Throw New Exception("Ocurrió un error al procesar los botones dinámicos del usuario ubicados en " & place.ToString, ex)
        Finally
            If tempDt IsNot Nothing Then
                tempDt.Dispose()
                tempDt = Nothing
            End If
        End Try

    End Function

    ''' <summary>
    ''' Aplica el mismo formato de nombre de reglas que al abrir una tarea
    ''' </summary>
    ''' <param name="caption"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function FormatCaption(ByVal caption As String)
        If caption.Length > 20 Then
            Return caption.Substring(0, 17) & "..."
        Else
            Return caption
        End If
    End Function

    ''' <summary>
    ''' Carga los botones dinámicos en una Barra de herramientas
    ''' </summary>
    ''' <param name="tb">ToolStrip donde se agregaran los botones dinámicos</param>
    ''' <param name="indexNextTo">El Atributo en relación a los controles desde donde se insertaran los botones</param>
    ''' <param name="buttonType">El tipo de botón que por defecto será de tipo Regla</param>
    ''' <param name="buttonPlace">Con este parámetro se establece que botones buscar</param>
    ''' <remarks></remarks>
    Public Shared Sub LoadDynamicButtons(ByRef tb As ToolStrip, _
                                         ByVal indexNextTo As Int32, _
                                         ByVal showIcon As Boolean, _
                                         ByVal buttonPlace As ButtonPlace, _
                                         ByVal Result As Object, _
                                         Optional ByVal buttonType As ButtonType = ButtonType.Rule, _
                                         Optional ByVal wfId As Int64 = 0)

        'Verifica si tiene permisos de utilizar el módulo de workflow
        If RightsBusiness.GetUserRights(ObjectTypes.ModuleWorkFlow, RightsType.Use) Then
            Dim tempDt As DataTable = Nothing
            Dim butIndex As Int32 = indexNextTo
            Try
                'Se obtienen los botones dinámicos para la barra de inicio de zamba
                tempDt = GetUserButtons(buttonType, buttonPlace, wfId)

                'Verifico que existan resultados
                If tempDt IsNot Nothing Then
                    'Se recorren los botones habilitados para mostrar
                    For Each row As DataRow In tempDt.Rows
                        'Se genera el botón 
                        Dim but As New GenericRuleButton(Result)

                        With but
                            .Name = row("BUTTONID").ToString
                            .Text = FormatCaption(row("CAPTION").ToString)
                            .ToolTipText = row("CAPTION").ToString
                            .BackColor = System.Drawing.Color.Transparent
                            .Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                            .ForeColor = System.Drawing.Color.Black
                            .ImageTransparentColor = System.Drawing.Color.Magenta
                            .Tag = "DynamicButton"

                            'Se asigna el ícono correspondiente a el botón dinámico.
                            If showIcon Then
                                If Not row("iconid") Is Nothing Then
                                    If row("iconid") = 0 Then
                                        .Image = My.Resources.gear
                                    Else
                                        .Image = My.Resources.mail
                                    End If
                                End If

                            End If

                            AddHandler .Click, AddressOf GenericRuleManager.ExecuteRuleEventHandler
                        End With

                        'Se agrega a la barra
                        tb.Items.Insert(butIndex, but)
                    Next
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If tempDt IsNot Nothing Then
                    tempDt.Dispose()
                    tempDt = Nothing
                End If
            End Try
        Else
            Trace.WriteLineIf(ZTrace.IsError, "No tiene permisos para utilizar el módulo de Workflow")
        End If
    End Sub

    ''' <summary>
    ''' Utilizado para asociar el evento click del boton que ejecuta reglas
    ''' </summary>
    ''' <param name="sender">Botón dinámico de tipo Rule</param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteRuleEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        Dim Button As GenericRuleButton = DirectCast(sender, GenericRuleButton)

        Dim TaskResult As ITaskResult = GetTask(Button.ObjectButton.objectResult)

        Try
            'Se obtiene el id de la regla a ejecutar
            Dim ruleId As Int64 = Int64.Parse(Button.Name.Replace("zamba_rule_", String.Empty))

            If TaskResult Is Nothing OrElse WFTaskBusiness.LockTask(TaskResult.TaskId) Then

                'Se agrega una tarea temporal para comenzar la ejecución de reglas
                Dim tempTasks As New Generic.List(Of ITaskResult)

                'agrego el taskResult con los datos de la tarea
                tempTasks.Add(TaskResult)

                'Se obtiene la regla a ejecutar
                Dim rule As IRule = WFRulesBusiness.GetInstanceRuleById(ruleId, True)

                'Ejecución de reglas
                Dim engine As New WFRulesBusiness()
                Return engine.ExecutePrimaryRule(rule, tempTasks, Nothing)
            Else
                MessageBox.Show("La tarea esta siendo ejecutada por otro usuario o servicio, no se podra accionar en este momento sobre la misma.", "Ejecucion de Tarea", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not TaskResult Is Nothing Then WFTaskBusiness.UnLockTask(TaskResult.TaskId)
        End Try
    End Function

    ''' <summary>
    ''' Devuelve la tarea actual, u obtiene la tarea a partir del documento
    ''' </summary>
    ''' <param name="objectResult">Recibe un objeto de tipo Result o TaskResult</param>
    ''' <remarks></remarks>
    Private Shared Function GetTask(ByVal objectResult As Object) As Core.TaskResult
        Dim Tasks As Generic.List(Of Core.ITaskResult) = New Generic.List(Of Core.ITaskResult)

        If Not objectResult Is Nothing Then
            If String.Compare(objectResult.GetType.ToString(), "Zamba.Core.Result") = 0 Then
                'obtengo las tareas del documento
                Tasks = Core.WF.WF.WFTaskBusiness.GetAllTasksByDocId(objectResult.ID, objectResult.DocTypeId, 0)
            Else
                Tasks.Add(DirectCast(objectResult, Core.TaskResult))
            End If
        Else
            Tasks.Add(New Core.TaskResult)
        End If

        Return Tasks(0)
    End Function
End Class
