'   Entidad utiilizada para representar las condiciones dinamicas de los formularios
'History:
'   ´07/09/2012:    Created     Javier
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
        Me.ID = -1
        Me.IndexToValidate = -1
        Me.Comparator = Comparators.Equal
        Me.ComparateValue = String.Empty
        Me.TargetIndex = -1
        Me.TargetAction = FormActions.NonAction
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
