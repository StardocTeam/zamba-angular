Namespace Analysis
    ''' <summary>
    ''' Representa el nodo de una regla DO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RuleNode
        Implements IRuleNode

#Region "Atributos"
        ''' <summary>
        ''' Nombre de la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Private _name As String
        ''' <summary>
        ''' Tipo de la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Private _type As String
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Nombre de la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String Implements IRuleNode.Name
            Get
                Return _name
            End Get
        End Property
        ''' <summary>
        ''' Tipo de la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Type() As String Implements IRuleNode.Type
            Get
                Return _type
            End Get
        End Property
#End Region

#Region "Contructores"
        Public Sub New(ByVal name As String, ByVal type As String)
            _name = name
            _type = type
        End Sub
#End Region
    End Class
End Namespace