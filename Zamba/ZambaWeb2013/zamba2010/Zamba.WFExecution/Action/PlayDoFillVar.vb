''' -----------------------------------------------------------------------------
''' Project	 : Zamba.WFExecution
''' Class	 : PlayDoFillVar
''' -----------------------------------------------------------------------------
''' <summary>
''' Play de la regla DoFillVar
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Gaston]	17/12/2008	Created
''' 	[Marcelo]	15/09/2009	Modified - Se agrega concatenacion
''' </history>
''' -----------------------------------------------------------------------------

Public Class PlayDoFillVar

    Private _myRule As IDoFillVar
    Private newMessage As String
    Private concValue As String
    Private _variableName As String

    Sub New(ByVal rule As IDoFillVar)
        Me._myRule = rule
    End Sub


    ''' <summary>
    ''' Play de la regla DOFillVar
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            For Each r As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valor de la variable ingresada por el usuario " & Me._myRule.variableValue)
                Me.newMessage = String.Empty

                _myRule.variableName = _myRule.variableName.Trim(vbLf)
                _myRule.variableValue = _myRule.variableValue.Trim(vbLf)

                newMessage = WFRulesBusiness.ResolveText(_myRule.variableValue, r)

                'newMessage = VarInterReglas.ReconocerVariablesValuesSoloTexto(_myRule.variableValue)
                'newMessage = TextoInteligente.ReconocerCodigo(newMessage, r)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando si el nombre de la variable se encuentra en la colección VariablesInterReglas (" & Me._myRule.variableName & ")")
                Me._variableName = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._myRule.variableName)
                'Me._variableName = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._variableName.TrimEnd, r).Trim
                _variableName = WFRuleParent.ObtenerNombreVariable(_variableName)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizar concatenacion: " & Me._myRule.useConc)
                If Me._myRule.useConc = False Then

                    If TextoInteligente.AsignItemFromSmartText(VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._myRule.variableName), r, newMessage) = False Then

                        If (VariablesInterReglas.ContainsKey(Me._variableName) = False) Then
                            VariablesInterReglas.Add(Me._variableName, newMessage)
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigno a la variable {0} el valor: {1}", _variableName, newMessage))
                        Else
                            Dim oldvalue As String
                            Dim oldVarValue As Object = VariablesInterReglas.Item(_variableName)
                            If oldVarValue IsNot Nothing AndAlso TypeOf (oldVarValue) Is String AndAlso String.IsNullOrEmpty(oldVarValue) Then
                                oldvalue = String.Empty
                            ElseIf TypeOf (oldVarValue) Is String Then
                                oldvalue = VariablesInterReglas.Item(_variableName).ToString
                            End If
                            VariablesInterReglas.Item(_variableName) = newMessage
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigno a la variable {0} que tenia previamente el valor {2} el valor: {1}", _variableName, newMessage, oldvalue))
                        End If
                    End If
                Else

                    If Not _variableName.Contains("<<") AndAlso (VariablesInterReglas.ContainsKey(_variableName) = False) Then
                        VariablesInterReglas.Add(_variableName, newMessage)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigno a la variable {0} el valor: {1}", _variableName, newMessage))
                    ElseIf Not _variableName.Contains("<<") Then
                        Dim oldvalue As String
                        Dim oldVarValue As Object = VariablesInterReglas.Item(_variableName)
                        If oldVarValue IsNot Nothing AndAlso TypeOf (oldVarValue) Is String AndAlso String.IsNullOrEmpty(oldVarValue) Then
                            oldvalue = String.Empty
                        ElseIf TypeOf (oldVarValue) Is String Then
                            oldvalue = VariablesInterReglas.Item(_variableName).ToString
                        End If
                        concValue = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.concValue)
                        concValue = TextoInteligente.ReconocerCodigo(concValue, r)

                        VariablesInterReglas.Item(_variableName) = VariablesInterReglas.Item(_variableName).ToString() & _myRule.concValue & newMessage
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigno a la variable {0} que tenia previamente el valor {2} el valor: {1}", _variableName, VariablesInterReglas.Item(_variableName).ToString() & _myRule.concValue & newMessage, oldvalue))
                    End If

                    Dim finalvariable = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.variableName)
                    Dim finalvariablevalue As String
                    If finalvariable.Contains("<<") Then
                        finalvariablevalue = TextoInteligente.ReconocerCodigo(finalvariable, r)
                    End If

                    If String.IsNullOrEmpty(_variableName) Then
                        TextoInteligente.AsignItemFromSmartText(finalvariable, r, newMessage)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigna a {0} el valor: {1}", finalvariable, newMessage))
                    Else
                        If finalvariablevalue = String.Empty Then
                            TextoInteligente.AsignItemFromSmartText(finalvariable, r, newMessage)
                        Else
                            TextoInteligente.AsignItemFromSmartText(finalvariable, r, finalvariablevalue & _myRule.concValue & newMessage)
                        End If

                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigna a {0} el valor: {1}", finalvariable, finalvariable & _myRule.concValue & newMessage))
                    End If
                End If
            Next
        Finally
            VarInterReglas = Nothing
            newMessage = Nothing
            concValue = Nothing
        End Try
        Return results
    End Function
End Class