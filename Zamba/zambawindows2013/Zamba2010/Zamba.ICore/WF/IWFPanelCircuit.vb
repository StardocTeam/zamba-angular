Public Interface IWFPanelCircuit
    '[Sebastian] 28-10-09 se agregó para realziar inyeccion de codigo en la regla design
    Sub UpdateRuleType()
    '[Sebastian] 28-10-09 se agregó para realziar inyeccion de codigo en la regla execute rule
    Sub OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)

End Interface
