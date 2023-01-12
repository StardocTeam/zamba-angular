Public Interface IZFormCondition
    Property ID As Int64
    Property IndexToValidate As Int64
    Property Comparator As Comparators
    Property ComparateValue As String
    Property TargetIndex As Int64
    Property TargetAction As FormActions
End Interface
