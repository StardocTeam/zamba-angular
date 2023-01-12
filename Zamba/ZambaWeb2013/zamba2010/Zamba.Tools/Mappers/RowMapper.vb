Imports System.ComponentModel.DataAnnotations
Imports System.Reflection

Public Class RowMapper


    Public Shared Function Map(Of T As New)(ByVal dataRow As DataRow) As T
        Dim item = New T

        For Each column As DataColumn In dataRow.Table.Columns
            Dim [property] = GetProperty(GetType(T), column.ColumnName)

            If [property] IsNot Nothing AndAlso dataRow(column) IsNot DBNull.Value AndAlso dataRow(column).ToString IsNot "NULL" Then
                [property].SetValue(item, ChangeType(dataRow(column), [property].PropertyType), Nothing)
            End If
        Next

        Return item
    End Function

    Private Shared Function GetProperty(ByVal type As Type, ByVal attributeName As String) As PropertyInfo

        Dim [property] = type.GetProperty(attributeName, BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)

        If [property] IsNot Nothing Then
            Return [property]
        End If

        Return type.GetProperties.Where(Function(p) p.IsDefined(GetType(DisplayAttribute), False) AndAlso p.GetCustomAttributes(GetType(DisplayAttribute), False).Cast(Of DisplayAttribute).Single.Name Is attributeName).FirstOrDefault

    End Function

    Private Shared Function ChangeType(ByVal value As Object, ByVal type As Type) As Object

        If type.IsGenericType AndAlso type.GetGenericTypeDefinition.Equals(GetType(Nullable(Of))) Then

            If value Is Nothing Then
                Return Nothing
            End If

            Return Convert.ChangeType(value, Nullable.GetUnderlyingType(type))
        End If

        Return Convert.ChangeType(value, type)

    End Function

End Class


