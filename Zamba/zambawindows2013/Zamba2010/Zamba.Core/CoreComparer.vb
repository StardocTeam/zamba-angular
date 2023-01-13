Imports System.Collections.Generic

Public Class CoreComparer
    Implements IEqualityComparer(Of ICore)


    Public Function Equals1(x As ICore, y As ICore) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of ICore).Equals
        Return x.ID = y.ID
    End Function

    Public Function GetHashCode1(obj As ICore) As Integer Implements System.Collections.Generic.IEqualityComparer(Of ICore).GetHashCode
        Return obj.ID.GetHashCode()
    End Function
End Class