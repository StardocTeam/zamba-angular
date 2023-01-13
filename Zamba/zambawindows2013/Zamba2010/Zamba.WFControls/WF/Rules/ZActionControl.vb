'Imports Zamba.AppBlock
'Imports Zamba.Core
''Public MustInherit Class ZRuleControl
'Public Class ZRuleControl
'    Inherits ZColorControl

'    Sub New()
'        MyBase.new()
'    End Sub

'    ''' -----------------------------------------------------------------------------
'    ''' <summary>
'    ''' Regla de Accion que se configurara en el control que hereda del presente
'    ''' </summary>
'    ''' <remarks>
'    ''' La regla debe heredar de WFRuleParent
'    ''' </remarks>
'    ''' <history>
'    ''' 	[Martin]	30/05/2006	Created
'    ''' </history>
'    ''' -----------------------------------------------------------------------------
'    Protected RuleAction As WFRuleParent
'    Sub New(ByVal RuleAction As WFRuleParent)
'        MyBase.New()
'        Me.RuleAction = RuleAction
'    End Sub
'    ''' -----------------------------------------------------------------------------
'    ''' <summary>
'    ''' Evento que actualiza el nombre de la regla en el administrador y en el boton del cliente
'    ''' </summary>
'    ''' <param name="RuleAction"></param>
'    ''' <remarks>
'    ''' </remarks>
'    ''' <history>
'    ''' 	[Martin]	30/05/2006	Created
'    ''' </history>
'    ''' -----------------------------------------------------------------------------
'    Public Event UpdateMaskName(ByVal RuleAction As WFRuleParent)
'    Protected Sub RaiseUpdateMaskName()
'        RaiseEvent UpdateMaskName(RuleAction)
'    End Sub
'End Class
