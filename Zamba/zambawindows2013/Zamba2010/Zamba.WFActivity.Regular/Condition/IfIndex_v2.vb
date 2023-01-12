Imports Zamba.Core
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Bussines
''' Class	 : Core.IfIndex
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Martin]	30/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Datos"), RuleDescription("Validar Indice"), RuleApproved("True"), RuleHelp("Compara el o los indices ingresados con una condición ingresada para poder tomar una desición determinada por el usuario"), RuleFeatures(False)> _
Public Class IfIndex_v2
    Inherits WFRuleParent
    Implements IIfIndex_v2, IRuleIFPlay
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _caseSensitive As Boolean
    Private playRule As Zamba.WFExecution.PlayIfIndex_v2

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Protected Const SEPARADOR_CONDICION As String = "*"
    Protected Const SEPARADOR_CAMPO As String = "|"
    Public Overrides Sub Dispose()

    End Sub
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="Id"></param>
    '''' <param name="ConditionRule"></param>
    '''' <param name="IndexId"></param>
    '''' <param name="Comp"></param>
    '''' <param name="Val"></param>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Martin]	30/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Condiciones As String, ByVal Variable As String, ByVal OpAnd As Boolean, ByVal casesensitive As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Me.Condiciones = Condiciones
        Me.Variable = Variable
        Me.OperatorAnd = OpAnd
        Me._caseSensitive = casesensitive
        Me.playRule = New Zamba.WFExecution.PlayIfIndex_v2(Me)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo generico que se invoca para ejecutar la regla, este es el punto de entrada
    ''' en la ejecucion de la misma.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	30/05/2006	Created
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''      [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Return playRule.Play(results, ifType)
    End Function

    Public Property Comparator() As Comparators Implements IIfIndex.Comparator
        Get
            Return _comparator
        End Get
        Set(ByVal value As Comparators)
            _comparator = value
        End Set
    End Property
    Public Property Condiciones() As String Implements IIfIndex.Condiciones
        Get
            Return _condiciones
        End Get
        Set(ByVal value As String)
            _condiciones = value
        End Set
    End Property
    Public Property IndexId() As Long Implements IIfIndex.IndexId
        Get
            Return _indexId
        End Get
        Set(ByVal value As Long)
            _indexId = value
        End Set
    End Property
    Public Property OperatorAnd() As Boolean Implements IIfIndex.OperatorAND
        Get
            Return _OperatorAnd
        End Get
        Set(ByVal value As Boolean)
            _OperatorAnd = value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que retorna el nombre a mostrar en el administrador, que indica la accion que realiza la regla
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Valido Indice"
        End Get
    End Property

#Region "Variables Locales"
    Private _condiciones As String
    Private _variable As String
    Private _indexId As Long
    Private _comparator As Comparators
    Private ComparativeValue As IndexValue
    Private sc As SortedList
    Private _OperatorAnd As Boolean
#End Region

#Region "Metodos Locales"

    'Public Property Valor() As String Implements IIfIndex.Valor
    '    Get
    '        Return ComparativeValue._Valor
    '    End Get
    '    Set(ByVal Value As String)
    '        ComparativeValue = New IndexValue(Value)
    '    End Set
    'End Property

    Public Property Variable() As String Implements IIfIndex.Variable
        Get
            Return _variable
        End Get
        Set(ByVal value As String)
            _variable = value
        End Set
    End Property

    Public Property CaseSensitive() As Boolean Implements IIfIndex_v2.CaseSensitive
        Get
            Return Me._caseSensitive
        End Get
        Set(ByVal value As Boolean)
            Me._caseSensitive = value
        End Set
    End Property

    Private Class IndexValue
        Implements IComparable
        Private _Tipo As Zamba.Core.IndexDataType = Nothing
        Public _Valor As String
        Public Sub New(ByVal Valor As String)
            _Valor = Valor
        End Sub

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Try
                Dim Ind As String
                Ind = obj
                If Ind = "" Then
                    Return 1
                End If
                'Numeros/Moneda
                If Tipo = IndexDataType.Numerico Or Tipo = IndexDataType.Numerico_Decimales Or Tipo = IndexDataType.Numerico_Largo Or Tipo = IndexDataType.Moneda Then
                    Dim f0, f1 As Decimal
                    f0 = Decimal.Parse(_Valor)
                    f1 = Decimal.Parse(Ind)
                    Return f0.CompareTo(f1)
                End If

                'Cadenas
                If Tipo = IndexDataType.Alfanumerico Or Tipo = IndexDataType.Alfanumerico_Largo Then
                    Return _Valor.CompareTo(Ind)
                End If

                'Fechas
                If Tipo = IndexDataType.Fecha Or Tipo = IndexDataType.Fecha_Hora Then
                    Dim f0, f1 As DateTime
                    f0 = DateTime.Parse(_Valor)
                    f1 = DateTime.Parse(Ind)
                    Return f0.CompareTo(f1)
                End If

            Catch
                Return 0
            End Try
        End Function
        Public Property Tipo() As Zamba.Core.IndexDataType
            Get
                Return _Tipo
            End Get
            Set(ByVal Value As Zamba.Core.IndexDataType)
                _Tipo = Value
            End Set
        End Property
    End Class
#End Region
End Class





