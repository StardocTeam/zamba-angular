Imports Zamba.Core

<RuleCategory("Base de Datos"), RuleDescription("Mostrar Tabla de Datos"), RuleHelp("Permite realizar una consulta y mostrar los datos obtenidos de la consulta"), RuleFeatures(True)> <Serializable()> _
Public Class DOShowTable
    Inherits WFRuleParent
    Implements IDOShowTable

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


    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Private m_SQLSelectId As Int32
    Private m_VarSource As String
    Private m_ShowColumns As String
    Private m_ShowToUser As Boolean
    Private m_SelectMultiRow As Boolean
    Private m_VarDestiny As String
    Private m_GetSelectedCols As String
    Private m_ShowCheckColumn As Boolean
    Private m_ShowDataOnly As Boolean
    Private m_CheckedItems As String
    Private m_CheckedItemsColumn As String
    Private m_EditColumns As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_SQLSelectId As Int32, ByVal p_VarSource As String, ByVal p_ShowColumns As String, ByVal p_SelectMultiRow As Boolean, ByVal p_GetSelectedCols As String, ByVal p_VarDestiny As String, ByVal p_ShowCheckColumn As Boolean, ByVal p_ShowDataOnly As Boolean, ByVal p_CheckedItems As String, ByVal p_CheckedItemsColumn As String, ByVal p_EditColumns As String)
        MyBase.New(Id, Name, WFStepid)
        Me.m_SQLSelectId = p_SQLSelectId
        Me.m_VarSource = p_VarSource
        Me.m_ShowColumns = p_ShowColumns
        Me.m_SelectMultiRow = p_SelectMultiRow
        Me.m_VarDestiny = p_VarDestiny
        Me.m_GetSelectedCols = p_GetSelectedCols
        Me.m_ShowCheckColumn = p_ShowCheckColumn
        Me.m_ShowDataOnly = p_ShowDataOnly
        Me.m_CheckedItems = p_CheckedItems
        Me.m_CheckedItemsColumn = p_CheckedItemsColumn
        Me.m_EditColumns = p_EditColumns
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOSHOWTABLE(Me)
        Return playRule.Play(results, Me)
    End Function
    '[Javier] 08/07/11 - Agregado el método.
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOSHOWTABLE(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ShowDoShowTable
            Return playRule.PlayWeb(results, Params, Me)
        Else
            If Params.Contains("CancelRule") Then
                If Params("CancelRule") Then
                    If Me.ThrowExceptionIfCancel Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    Else
                        Params.Clear()

                        ExecutionResult = RuleExecutionResult.PendingEventExecution
                        RulePendingEvent = RulePendingEvents.CancelExecution
                        Return Nothing
                    End If
                End If
            End If

            '  Params.Clear()

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return playRule.PlayWebSecondExecution(results, Params, Me)
        End If
    End Function

    Public Property GetSelectedCols() As String Implements Core.IDOShowTable.GetSelectedCols
        Get
            Return Me.m_GetSelectedCols
        End Get
        Set(ByVal value As String)
            Me.m_GetSelectedCols = value
        End Set
    End Property

    Public Property SelectMultiRow() As Boolean Implements Core.IDOShowTable.SelectMultiRow
        Get
            Return Me.m_SelectMultiRow
        End Get
        Set(ByVal value As Boolean)
            Me.m_SelectMultiRow = value
        End Set
    End Property

    Public Property ShowColumns() As String Implements Core.IDOShowTable.ShowColumns
        Get
            Return Me.m_ShowColumns
        End Get
        Set(ByVal value As String)
            Me.m_ShowColumns = value
        End Set
    End Property


    Public Property SQLSelectId() As Integer Implements Core.IDOShowTable.SQLSelectId
        Get
            Return Me.m_SQLSelectId
        End Get
        Set(ByVal value As Integer)
            Me.m_SQLSelectId = value
        End Set
    End Property

    Public Property VarDestiny() As String Implements Core.IDOShowTable.VarDestiny
        Get
            Return Me.m_VarDestiny
        End Get
        Set(ByVal value As String)
            Me.m_VarDestiny = value
        End Set
    End Property

    Public Property VarSource() As String Implements Core.IDOShowTable.VarSource
        Get
            Return Me.m_VarSource
        End Get
        Set(ByVal value As String)
            Me.m_VarSource = value
        End Set
    End Property

    Public Property ShowCheckColumn() As Boolean Implements Core.IDOShowTable.ShowCheckColumn
        Get
            Return m_ShowCheckColumn
        End Get
        Set(ByVal value As Boolean)
            m_ShowCheckColumn = value
        End Set
    End Property

    Public Property ShowDataOnly() As Boolean Implements Core.IDOShowTable.ShowDataOnly
        Get
            Return m_ShowDataOnly
        End Get
        Set(ByVal value As Boolean)
            m_ShowDataOnly = value
        End Set
    End Property


    ''' <summary>
    ''' Filas a ser seleccionadas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedItems() As String Implements IDOShowTable.CheckedItems
        Get
            Return m_CheckedItems
        End Get
        Set(ByVal value As String)
            m_CheckedItems = value
        End Set
    End Property

    ''' <summary>
    ''' Columna por la que se comparara
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedItemsColumn() As String Implements IDOShowTable.CheckedItemsColumn
        Get
            Return m_CheckedItemsColumn
        End Get
        Set(ByVal value As String)
            m_CheckedItemsColumn = value
        End Set
    End Property

    ''' <summary>
    ''' Columnas de edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EditColumns() As String Implements IDOShowTable.EditColumns
        Get
            Return m_EditColumns
        End Get
        Set(ByVal value As String)
            m_EditColumns = value
        End Set
    End Property
End Class