Namespace Analysis
    Public Class IfRuleNodeHistory
        Inherits IfRuleNode
        Implements IIfRuleNodeHistory
#Region "Atributos"
        ''' <summary>
        ''' Representa si el al pasar la tarea por la regla se valido o no
        ''' </summary>
        ''' <remarks></remarks>
        Private _validated As Boolean
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Representa si el al pasar la tarea por la regla se valido o no
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Validated() As Boolean Implements IIfRuleNodeHistory.Validated
            Get
                Return _validated
            End Get
        End Property
#End Region

#Region "Propiedades"
        Public Sub New(ByVal name As String, ByVal type As String, ByVal invalidCount As Int64, ByVal validCount As Int64, ByVal validation As String, ByVal validated As Boolean)
            MyBase.New(name, type, invalidCount, validCount, validation)
            _validated = validated
        End Sub
#End Region

    End Class
End Namespace