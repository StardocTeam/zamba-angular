Public Interface IEditStepNode
    Inherits IBaseWFNode

    Sub IsInitialStep(ByVal Value As Boolean)
    Property FloatingNode() As IRuleTypeNode
    Property RightNode() As IRightNode
    Property ScheduleNode() As IRuleTypeNode
    Property EventNode() As IRuleTypeNode
    Property UpdateNode() As IRuleTypeNode
    Property OutputValidationNode() As IRuleTypeNode
    Property OutputNode() As IRuleTypeNode
    Property InputValidationNode() As IRuleTypeNode
    Property UserActionNode() As IRuleTypeNode
    Property WFStep() As IWFStep
    Property InputNode() As IRuleTypeNode
End Interface