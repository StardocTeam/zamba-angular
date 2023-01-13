Imports System.Windows.Forms

Public Class GenericRuleButton
    Inherits ToolStripButton

    Public ObjectButton As Zamba.Core.RuleButton = Nothing

    Public Sub New(ByRef ResultType As Object)
        MyBase.New()

        ObjectButton = New Zamba.Core.RuleButton()
        ObjectButton.objectResult = ResultType
    End Sub
End Class
