Imports Zamba.Core

<RuleMainCategory("Atributos"), RuleCategory("Autocompletar"), RuleSubCategory(""), RuleDescription("Autocompletar Atributos"), RuleHelp("Permite ejecutar la configuracion de autocompletado de los atributos"), RuleFeatures(False)> <Serializable()> _
Public Class DOAutoComplete
    Inherits WFRuleParent
    Implements IDOAutoComplete, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOAutoComplete
    Private _isValid As Boolean

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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
        playRule = New Zamba.WFExecution.PlayDOAutoComplete(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As System.Collections.SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    'Protected Overloads Overrides Function Play(ByVal Result As Core.TaskResult) As Core.TaskResult
    '    Dim AC As AutocompleteBCBusiness
    '    Dim index As Zamba.Core.Index

    '    Try
    '        'Obtiene el campo IndexKey relacionado con AutoComplete del primer
    '        'documento. Es es porque deberían ser todos del mismo tipo.
    '        index = AutoCompleteBarcode_FactoryBusiness.getIndexKey(Result.DocTypeId)
    '        'Si ocurre un error en este punto, es porque index 
    '        'es Nothing que quiere decir que el docuemento no tiene 
    '        'atributos para autocompletado
    '        If Not IsNothing(index) Then
    '            'Obtiene una instancia del Objeto AutoComplete
    '            AC = AutoCompleteBarcode_FactoryBusiness.GetComplete(Result.DocTypeId, index.Id)
    '            'Siempre AC deberia ser una instancia
    '            If Not IsNothing(AC) Then
    '                'Actuliza el valor del indice.
    '                'Dicho valor es utilizado para el seguimiento del documento dentro 
    '                'de un WorkFlow.

    '                index.DataTemp = findIn(result.Indexs, index).Data

    '                'Obtiene los datos del documento
    '                result = AC.Complete(result, index)

    '                'Persiste los cambios en el documento
    '                Results_Factory.SaveIndexData(DirectCast(result, result), True)
    '            End If
    '        End If
    '        Return result
    '    Catch ex As Exception
    '        Throw New ArgumentException("Error al intentar autocompletar el documento." & vbNewLine & "Motivo del Error: " & ex.Message & vbNewLine)
    '    Finally
    '        If Not IsNothing(AC) Then
    '            AC.Dispose()
    '            AC = Nothing
    '        End If
    '        If Not IsNothing(index) Then
    '            index = Nothing
    '        End If
    '    End Try
    'End Function
End Class

