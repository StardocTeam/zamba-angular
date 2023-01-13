Public Class ZWebFormCondition
    Implements IZFormCondition

#Region "Implemented Properties"
    Property ID() As Int64 Implements IZFormCondition.ID
    Property IndexToValidate() As Int64 Implements IZFormCondition.IndexToValidate
    Property Comparator() As Comparators Implements IZFormCondition.Comparator
    Property ComparateValue() As String Implements IZFormCondition.ComparateValue
    Property TargetIndex() As Int64 Implements IZFormCondition.TargetIndex
    Property TargetAction() As FormActions Implements IZFormCondition.TargetAction
#End Region

#Region "Constructors"
    Sub New()
        ID = -1
        IndexToValidate = -1
        Comparator = Comparators.Equal
        ComparateValue = String.Empty
        TargetIndex = -1
        TargetAction = FormActions.NonAction
    End Sub

    Sub New(ByVal id As Int64, ByVal indexToValidate As Int64, ByVal comparator As Comparators, ByVal comparateValue As String, _
            ByVal targetIndex As Int64, ByVal targetAction As FormActions)

        Me.ID = id
        Me.IndexToValidate = indexToValidate
        Me.Comparator = comparator
        Me.ComparateValue = comparateValue
        Me.TargetIndex = targetIndex
        Me.TargetAction = targetAction
    End Sub
#End Region
End Class
