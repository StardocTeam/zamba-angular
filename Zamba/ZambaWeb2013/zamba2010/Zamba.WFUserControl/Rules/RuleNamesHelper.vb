Imports Zamba.AdminControls
Imports Zamba.Core.Enumerators

Public Class RuleNamesHelper
    Public Shared Sub ChangeName(ByVal userActionNode As RuleTypeNode)
        Try
            Dim BaseNode As BaseWFNode = userActionNode.Nodes(0)
            Dim NewName As String = InputBox("Ingrese el nuevo nombre de la regla", "Edición de Reglas", DirectCast(BaseNode, RuleNode).RuleName)

            If ((NewName <> String.Empty) AndAlso (NewName <> DirectCast(BaseNode, RuleNode).RuleName)) Then
                DirectCast(BaseNode, RuleNode).RuleName = NewName
                WFRulesBusiness.UpdateRuleNameByID(DirectCast(BaseNode, RuleNode).RuleId, DirectCast(BaseNode, RuleNode).RuleName)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para cambiar el nombre de una acción de usuario

    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomás] 17/04/2009  Created
    ''' [Sebastian] 19/10/2009 Modified se agrego la llamada a un formulario que simula un input  box
    ''' </history>
    Public Shared Sub ChangeUserActionName(ByVal userActionNode As RuleTypeNode)
        Dim frm As frmInputBox
        Try
            'Obtiene el nodo de la acción de usuario
            Dim BaseNode As BaseWFNode = userActionNode.Nodes(0)
            Dim oldName As String = BaseNode.Parent.Text.Trim
            Dim NewName As String
            'Obtengo el nuevo nombre
            frm = New frmInputBox("Ingrese el nombre de la acción de usuario", 2000, oldName, "Acción de Usuario")
            frm.StartPosition = FormStartPosition.CenterParent
            frm.BringToFront()
            frm.ShowDialog()
            If frm.DialogResult = DialogResult.OK Then
                NewName = frm.Name
            End If
            'Si este no es vacío se aplican los cambios
            If (Not String.IsNullOrEmpty(NewName) AndAlso String.Compare(NewName, oldName) <> 0) Then
                If (NewName.Length <= 2000) Then

                    'Este método sirve tanto para el update como para el insert
                    WFBusiness.SetRulesPreferences(DirectCast(BaseNode, RuleNode).RuleId, RuleSectionOptions.Regla, RulePreferences.UserActionName, 0, NewName)

                    'Actualiza el nodo del tree
                    userActionNode.UpdateUserActionNodeName(NewName)
                Else
                    Dim ex As New Exception("El tamaño máximo para el nombre de la acción de usuario excede los 2000 caracteres")
                    ZClass.raiseerror(ex)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            frm.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para cambiar el nombre de una regla
    ''' </summary>
    ''' <param name="ruleNode">Instancia de una regla seleccionada</param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    16/04/2009  Modified    Se agrego un límite para la cantidad de caracteres máximo que puede tener el nombre de una regla
    '''     [Tomas]     11/06/2009  Modified    Se modifica la forma en que cambia el nombre de la acción de usuario. Se valida para que
    '''                                         si tiene existe un nombre en la Zruleopt no lo modifique.
    '''     [Sebastian] 19/10/2009 Modified se agrego la llamada a un formulario que simula un input  box
    '''     [Tomas]     06/11/2009  Modified    Se modifica la validacion ya que arrojaba exceptions cuando no debia. Se libera la memoria.
    ''' </history>
    Public Shared Sub ChangeRuleName(ByVal ruleNode As RuleNode)
        Dim frm As frmInputBox
        Dim BaseNode As BaseWFNode = ruleNode
        Dim NewName As String
        Try
            frm = New frmInputBox("Ingrese el nombre de la regla", 2000, DirectCast(BaseNode, RuleNode).RuleName, "Nombre de Regla")
            frm.StartPosition = FormStartPosition.CenterParent
            frm.BringToFront()
            frm.ShowDialog()
            If frm.DialogResult <> DialogResult.Cancel Then
                NewName = frm.Name.Replace(Chr(39), String.Empty)
            End If
            If ((Not String.IsNullOrEmpty(NewName)) AndAlso String.Compare(NewName, DirectCast(BaseNode, RuleNode).RuleName) <> 0) Then
                DirectCast(BaseNode, RuleNode).RuleName = NewName
                WFRulesBusiness.UpdateRuleNameByID(DirectCast(BaseNode, RuleNode).RuleId, DirectCast(BaseNode, RuleNode).RuleName)
                ruleNode.UpdateRuleNodeName(DirectCast(BaseNode, RuleNode).RuleId, DirectCast(BaseNode, RuleNode).RuleName)

                If (DirectCast(BaseNode, RuleNode).ParentType = TypesofRules.AccionUsuario) Then
                    'Obtiene el dataset donde se encuentra nombre de la acción de usuario asociada a esa regla
                    Dim dt As DataTable = WFRulesBusiness.GetRuleOption(DirectCast(BaseNode, RuleNode).WFStepId, DirectCast(BaseNode, RuleNode).RuleId, 0, 43, 0, False)
                    'Valida si existen datos
                    If Not IsNothing(dt) AndAlso dt.Rows.Count = 0 Then
                        'Si no tiene nombre la acción de usuario lo modifica por defecto con el nombre de la primer regla
                        DirectCast(BaseNode, RuleNode).PrevVisibleNode.Text = DirectCast(BaseNode, RuleNode).RuleName
                    End If
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            If Not IsNothing(BaseNode) Then BaseNode = Nothing
            If Not IsNothing(NewName) Then NewName = Nothing
        End Try
    End Sub
End Class
