Namespace Analysis
    Public Class IfRuleNode
        Inherits RuleNode
        Implements IIfRuleNode

#Region "Atributos"
        ''' <summary>
        ''' Cantidad de tareas que eran invalidas segun el criterio de la regla
        ''' </summary>
        ''' <remarks></remarks>
        Private _invalidCount As Int64
        ''' <summary>
        ''' Cantidad de tareas que eran validas segun el criterio de la regla
        ''' </summary>
        ''' <remarks></remarks>
        Private _validCount As Int64
        ''' <summary>
        ''' El criterio de validacion de la regla
        ''' </summary>
        ''' <remarks></remarks>
        Private _validation As String
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Cantidad de tareas que eran invalidas segun el criterio de la regla
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property InvalidCount() As Long Implements IIfRuleNode.InvalidCount
            Get
                Return _invalidCount
            End Get
        End Property
        ''' <summary>
        ''' El criterio de validacion de la regla
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Validation() As String Implements IIfRuleNode.Validation
            Get
                Return _validation
            End Get
        End Property
        ''' <summary>
        ''' Cantidad de tareas que eran validas segun el criterio de la regla
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidCount() As Long Implements IIfRuleNode.ValidCount
            Get
                Return _validCount
            End Get
        End Property
#End Region

#Region "Contructores"
        Public Sub New(ByVal name As String, ByVal type As String, ByVal invalidCount As Int64, ByVal validCount As Int64, ByVal validation As String)
            MyBase.New(name, type)
            _invalidCount = invalidCount
            _validCount = validCount
            _validation = validation
        End Sub
#End Region
    End Class

End Namespace