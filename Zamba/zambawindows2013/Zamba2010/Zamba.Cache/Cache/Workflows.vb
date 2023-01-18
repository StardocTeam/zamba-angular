Namespace Cache
    Public Class Workflows
        Public Shared Sub clearAll()
            Try
                hsRulesEnable.Clear()
                hsRuleParamsDS.Clear()
                hsWFStepsDS.Clear()
                hsWFStepStatesDS.Clear()
                hsStepsStates.Clear()
                hsStepsRulesDS.Clear()
                hsSteps.Clear()
                hsStepsOpt.Clear()
                hsStepsColors.Clear()
                hsWorkflow.Clear()
                hsWorkflowNames.Clear()
                hsStepsByUserRestrictedDoctypes.Clear()
                hsWFStepState.Clear()
                hsWFStepId.Clear()
                hsDocTypesAssWFbyStepId.Clear()
                HSRulesByStepAndType.Clear()
                HsRulesPreferencesByStepId.Clear()
                HsDynamicbuttonsByWorkflowId.Clear()
                hsUserWFIdsAndNamesWithSteps.Clear()
                hsCanExecuteRules.Clear()
                hsEntityWorkflows.clear
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        'Guarda las reglas por wf en un DS
        'Public Shared hsWFRulesDS As New Hashtable
        'Guarda la habilitacion de las reglas
        Public Shared hsRulesEnable As New Hashtable

        'Public Shared hsStepRules As New Hashtable

        'Guarda las reglas por step en un DS
        'Public Shared hsStepRulesDS As New Hashtable

        'Guarda las etapas por wf en un DS
        Public Shared hsWFStepsDS As New Hashtable
        'Guarda los estados x etapas por wf en un DS
        Public Shared hsWFStepStatesDS As New Hashtable

        'Guarda los estados de cada etapa
        Public Shared hsStepsStates As New Hashtable
        'Guarda los datos de todas las reglas de la etapa
        Public Shared hsStepsRulesDS As New Hashtable()
        'Guarda los nombres y ids de las etapas por workflow
        Public Shared hsSteps As New Hashtable()
        'Guarda las opciones de las etapas (WFStepOpt)
        Public Shared hsStepsOpt As New Hashtable()
        'Guarda los colores de las etapas
        Public Shared hsStepsColors As New Hashtable()
        'Guarda los Workflows
        Public Shared hsWorkflow As New Hashtable()
        'Guarda el nombre de los Workflows
        Public Shared hsWorkflowNames As New Hashtable()
        'Guarda los StepsByUserRestrictedDoctypes
        Public Shared hsStepsByUserRestrictedDoctypes As New Hashtable()
        '[Pablo] 25/08/2010
        'Guarda el estado de la lista (getStateFromList)
        Public Shared hsWFStepState As New Hashtable()
        '[Pablo] 25/08/2010
        'Guarda el id de etapa (GetWFStepIdbyRuleID)
        Public Shared hsWFStepId As New Hashtable()
        '[Javier] 14/10/2010
        Public Shared hsDocTypesAssWFbyStepId As New Hashtable()
        Public Shared hsEntityWorkflows As New Hashtable

        'Guarda los ids de reglas por etapa-tipoderegla
        Public Shared HSRulesByStepAndType As New Hashtable()
        
        'Guarda los Botones Dinamic por WorkflowId
        Public Shared HsDynamicbuttonsByWorkflowId As New Hashtable()
        
        'Guarda los parametros de las reglas en un DS
        Public Shared hsRuleParamsDS As New Hashtable
        'Guarda las preferencias de las reglas en un DS por stepid
        Public Shared HsRulesPreferencesByStepId As New Hashtable
        'Guarda etapas iniciales de workflows
        Public Shared HsInitialStepOfWFs As New Hashtable()
        Public Shared hsUserWFIdsAndNamesWithSteps As New List(Of EntityView)
        Public Shared hsCanExecuteRules As New Hashtable()
        ''[Ezequiel] 01/05/2011 - Clase en la cual se guardan las opciones de reglas.
        'Public Class HSRulesOptions
        '    Public Shared Lock As New Object
        '    Private Shared _DsRulesOptions As New DataTable

        '    Public Shared Function ContainsByRuleId(ByVal ruleid As Int64) As Boolean
        '        If _DsRulesOptions.Rows.Count = 0 OrElse _DsRulesOptions.Select("id = " & ruleid.ToString).Length = 0 Then
        '            Return False
        '        Else
        '            Return True
        '        End If
        '    End Function


        '    Public Shared Function ContainsByWorkId(ByVal workid As Int64) As Boolean
        '        If _DsRulesOptions.Rows.Count = 0 OrElse _DsRulesOptions.Select("workid = " & workid.ToString).Length = 0 Then
        '            Return False
        '        Else
        '            Return True
        '        End If
        '    End Function

        '    Public Shared Function GetByRuleId(ByVal ruleid As Int64) As DataTable
        '        Dim ds As DataTable = _DsRulesOptions.Clone
        '        Dim drs As DataRow() = _DsRulesOptions.Select("id = " & ruleid.ToString)
        '        For Each dr As DataRow In drs
        '            ds.ImportRow(dr)
        '        Next
        '        Return ds
        '    End Function

        '    Public Shared Sub Add(ByVal dt As DataTable)
        '        _DsRulesOptions.Merge(dt)
        '    End Sub

        '    ''' <summary>
        '    ''' Quita la regla de la cache
        '    ''' </summary>
        '    ''' <param name="ruleid">ID de la regla</param>
        '    ''' <history>Marcelo Modified 27/03/2012 Se agrega validaciones</history>
        '    ''' <remarks></remarks>
        '    Public Shared Sub RemoveByRuleId(ByVal ruleid As Int64)
        '        Try
        '            If Not IsNothing(_DsRulesOptions) AndAlso _DsRulesOptions.Columns.Count > 0 Then
        '                Dim drs As DataRow() = _DsRulesOptions.Select("id = " & ruleid.ToString)
        '                For Each dr As DataRow In drs
        '                    _DsRulesOptions.Rows.Remove(dr)
        '                Next
        '            End If
        '        Catch ex As Exception
        '           ZClass.raiseerror(ex)
        '        End Try
        '    End Sub
        'End Class

        '[Ezequiel] 02/05/2011 - Clase la cual guarda las reglas
        Public Class HSRules
            Public Shared Lock As New Object
            Private Shared _lsRules As New Generic.Dictionary(Of Int64, IWFRuleParent)

            Public Shared Sub AddRuleList(ByVal rulelist As Generic.List(Of IWFRuleParent))
                SyncLock (_lsRules)
                    For Each Rule As IWFRuleParent In rulelist
                        Add(Rule)
                    Next
                End SyncLock
            End Sub

            ''' <summary>
            ''' Agrega una regla al cache
            ''' </summary>
            ''' <param name="rule"></param>
            ''' <remarks></remarks>
            Public Shared Sub Add(ByVal rule As IWFRuleParent)
                If Not IsNothing(rule) Then
                    SyncLock (_lsRules)
                        If IsNothing(_lsRules) Then
                            _lsRules = New Generic.Dictionary(Of Int64, IWFRuleParent)
                        End If

                        If _lsRules.ContainsKey(rule.ID) Then
                            _lsRules(rule.ID) = rule
                        Else
                            _lsRules.Add(rule.ID, rule)
                        End If
                    End SyncLock
                End If
            End Sub

            ''' <summary>
            ''' Obtiene la regla por el ID de la regla
            ''' </summary>
            ''' <param name="ruleid"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Shared Function GetByRuleID(ByVal ruleid As Int64) As IWFRuleParent
                If _lsRules.ContainsKey(ruleid) Then
                    Return _lsRules.Item(ruleid)
                Else
                    Return Nothing
                End If
            End Function

            ''' <summary>
            ''' Obtiene si la cache tiene el ID de la regla
            ''' </summary>
            ''' <param name="ruleid"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Shared Function Contains(ByVal ruleid As Int64) As Boolean
                Return _lsRules.ContainsKey(ruleid)
            End Function

            ''' <summary>
            ''' Limpia los caches de las reglas
            ''' </summary>
            ''' <remarks></remarks>
            Public Shared Sub ClearAll()
                _lsRules.Clear()
            End Sub
        End Class
    End Class
End Namespace