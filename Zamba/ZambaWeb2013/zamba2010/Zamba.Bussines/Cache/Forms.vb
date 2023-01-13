Imports System.Collections.Generic

Namespace Cache
    Public Class Forms
        Public Shared DicHasForms As New Dictionary(Of Long, Boolean)
        Public Shared DicForms As New Dictionary(Of Long, ZwebForm())
        Public Shared FormsConditions As New Dictionary(Of Long, List(Of IZFormCondition))
        Public Shared VirtualDocumentsByRightsOfCreate As New Dictionary(Of String, ArrayList)
        Public Shared Function RemoveCurrentInstance() As ZCore
           DicHasForms.Clear
            DicForms.Clear()
            VirtualDocumentsByRightsOfCreate.Clear()
        End Function
    End Class
End Namespace