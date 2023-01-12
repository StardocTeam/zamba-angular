Imports Zamba.Core

<RuleMainCategory("Base de Datos"), RuleCategory("Consultas"), RuleSubCategory(""), RuleDescription("Mostrar Tabla de Edicion de Datos"), RuleHelp("Permite realizar una consulta y mostrar los datos obtenidos de la consulta"), RuleFeatures(True)> <Serializable()> _
Public Class DOShowEditTable
    Inherits WFRuleParent
    Implements IDOShowEditTable, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Private playRule As Zamba.WFExecution.PlayDOSHOWEDITTABLE
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
        m_SQLSelectId = p_SQLSelectId
        m_VarSource = p_VarSource
        m_ShowColumns = p_ShowColumns
        m_SelectMultiRow = p_SelectMultiRow
        m_VarDestiny = p_VarDestiny
        m_GetSelectedCols = p_GetSelectedCols
        m_ShowCheckColumn = p_ShowCheckColumn
        m_ShowDataOnly = p_ShowDataOnly
        m_CheckedItems = p_CheckedItems
        m_CheckedItemsColumn = p_CheckedItemsColumn
        m_EditColumns = p_EditColumns

        playRule = New WFExecution.PlayDOSHOWEDITTABLE(Me)
    End Sub

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

    Public Property GetSelectedCols() As String Implements Core.IDOShowEditTable.GetSelectedCols
        Get
            Return m_GetSelectedCols
        End Get
        Set(ByVal value As String)
            m_GetSelectedCols = value
        End Set
    End Property

    Public Property SelectMultiRow() As Boolean Implements Core.IDOShowEditTable.SelectMultiRow
        Get
            Return m_SelectMultiRow
        End Get
        Set(ByVal value As Boolean)
            m_SelectMultiRow = value
        End Set
    End Property

    Public Property ShowColumns() As String Implements Core.IDOShowEditTable.ShowColumns
        Get
            Return m_ShowColumns
        End Get
        Set(ByVal value As String)
            m_ShowColumns = value
        End Set
    End Property


    Public Property SQLSelectId() As Integer Implements Core.IDOShowEditTable.SQLSelectId
        Get
            Return m_SQLSelectId
        End Get
        Set(ByVal value As Integer)
            m_SQLSelectId = value
        End Set
    End Property

    Public Property VarDestiny() As String Implements Core.IDOShowEditTable.VarDestiny
        Get
            Return m_VarDestiny
        End Get
        Set(ByVal value As String)
            m_VarDestiny = value
        End Set
    End Property

    Public Property VarSource() As String Implements Core.IDOShowEditTable.VarSource
        Get
            Return m_VarSource
        End Get
        Set(ByVal value As String)
            m_VarSource = value
        End Set
    End Property

    Public Property ShowCheckColumn() As Boolean Implements Core.IDOShowEditTable.ShowCheckColumn
        Get
            Return m_ShowCheckColumn
        End Get
        Set(ByVal value As Boolean)
            m_ShowCheckColumn = value
        End Set
    End Property

    Public Property ShowDataOnly() As Boolean Implements Core.IDOShowEditTable.ShowDataOnly
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
    Public Property CheckedItems() As String Implements IDOShowEditTable.CheckedItems
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
    Public Property CheckedItemsColumn() As String Implements IDOShowEditTable.CheckedItemsColumn
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
    Public Property EditColumns() As String Implements IDOShowEditTable.EditColumns
        Get
            Return m_EditColumns
        End Get
        Set(ByVal value As String)
            m_EditColumns = value
        End Set
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
End Class