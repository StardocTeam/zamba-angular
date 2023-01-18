Imports Zamba.Core
Imports Zamba.Data
Imports System.Xml.Serialization

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.WFBusiness
''' Class	 : WFBusiness.DOAutoComplete
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''     modulo de Implementacion para la Rule Action AutoComplete
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[oscar]	06/06/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Datos"), RuleDescription("Autocompletar Indices"), RuleHelp("Permite completar de forma automática el indice del tipo de docuemento"), RuleFeatures(False)> <Serializable()> _
Public Class DOAutoComplete
    Inherits WFRuleParent
    Implements IDOAutoComplete
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOAutoComplete
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
        Me.playRule = New Zamba.WFExecution.PlayDOAutoComplete(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As System.Collections.SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    'Protected Overloads Overrides Function Play(ByVal Result As Core.TaskResult) As Core.TaskResult
    '    Dim AC As AutocompleteBCBusiness
    '    Dim index As Zamba.Core.Index

    '    Try
    '        'Obtiene el campo IndexKey relacionado con AutoComplete del primer
    '        'documento. Es es porque deberían ser todos del mismo tipo.
    '        index = AutoCompleteBarcode_FactoryBusiness.getIndexKey(result.DocType.Id)
    '        'Si ocurre un error en este punto, es porque index 
    '        'es Nothing que quiere decir que el docuemento no tiene 
    '        'indices para autocompletado
    '        If Not IsNothing(index) Then
    '            'Obtiene una instancia del Objeto AutoComplete
    '            AC = AutoCompleteBarcode_FactoryBusiness.GetComplete(result.DocType.Id, index.Id)
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

