Imports System.Text
Imports Zamba.Data

Public Class UCDoGoHome
    Inherits ZRuleControl

    Public Sub New(ByRef rule As IDoGoHome, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub
End Class
