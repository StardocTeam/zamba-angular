Imports Zamba.Core
Imports Zamba.Data
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.IfCantidadDocumentos
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Zamba WorkFlow
''' Evalua si existe un documento asociado de un entidad
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Patricio]	09/06/2006	Created
'''     [Marcelo]	03/09/2007	Modified
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Documentos Asociados"), RuleDescription("Validar Documento Asociado"), RuleHelp("Valida la existencia de documentos asociados"), RuleFeatures(False)> _
Public Class IfDocAsocExist
    Inherits WFRuleParent
    Implements IIfDocAsocExist, IRuleIFPlay
    Private _isLoaded As Boolean
    Private _isFull As Boolean
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
    Public Overrides Sub Dispose()

    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal TipoDeDocumento As Int32, ByVal Existencia As Boolean, ByVal Condiciones As String, ByVal OpAnd As Boolean)
        MyBase.New(Id, Name, wfstepId)
        Me._TipoDeDocumento = TipoDeDocumento
        Me._Existencia = Existencia
        Me.Condiciones = Condiciones
        Me.OperatorAnd = OpAnd
    End Sub

    Public Property TipoDeDocumento() As Int32 Implements IIfDocAsocExist.TipoDeDocumento
        Get
            Return _TipoDeDocumento
        End Get
        Set(ByVal value As Int32)
            _TipoDeDocumento = value
        End Set
    End Property
    Public Property Existencia() As Boolean Implements IIfDocAsocExist.Existencia
        Get
            Return _Existencia
        End Get
        Set(ByVal value As Boolean)
            _Existencia = value
        End Set
    End Property

    Public Property Comparator() As Comparators Implements IIfDocAsocExist.Comparator
        Get
            Return _comparator
        End Get
        Set(ByVal value As Comparators)
            _comparator = value
        End Set
    End Property
    Public Property Condiciones() As String Implements IIfDocAsocExist.Condiciones
        Get
            Return _condiciones
        End Get
        Set(ByVal value As String)
            _condiciones = value
        End Set
    End Property
    Public Property IndexId() As Long Implements IIfDocAsocExist.IndexId
        Get
            Return _indexId
        End Get
        Set(ByVal value As Long)
            _indexId = value
        End Set
    End Property
    Public Property OperatorAnd() As Boolean Implements IIfDocAsocExist.OperatorAND
        Get
            Return _OperatorAnd
        End Get
        Set(ByVal value As Boolean)
            _OperatorAnd = value
        End Set
    End Property

    Public _TipoDeDocumento As Int32
    Public _Existencia As Boolean
    Private _OperatorAnd As Boolean
    Private _condiciones As String
    Private _indexId As Long
    Private _comparator As Comparators


    ''' <summary>
    ''' Devuelve un Sorted List de objetos TaskResults, los cuales tienen documentos asociados
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns>Cada TaskResult contiene un campo LinkResults, el cual contiene los objetos DOCTYPES asociados al mismo</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfDocAsocExist()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfDocAsocExist()
        Return playRule.Play(results, Me)
    End Function
    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Try
            Dim playRule As New Zamba.WFExecution.PlayIfDocAsocExist()
            Return playRule.Play(results, Me, ifType)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return results
    End Function
End Class