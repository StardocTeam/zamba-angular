
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.ZBatch
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear Objetos Lote, utilizado en el Indexer
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public MustInherit Class ZBatch
    Inherits ZambaCore
    Implements IZBatch

#Region "Atributos"
    Private _createDate As Date = Now
    Private _editDate As Date = Now
    Private _finalizedDate As Date = Nothing
    Private _results As ArrayList = Nothing
    Private _userId As Int32 = 9999
#End Region

#Region "Propiedades"
    Public Property CreateDate() As Date Implements IZBatch.CreateDate
        Get
            Return _createDate
        End Get
        Set(ByVal value As Date)
            _createDate = value
        End Set
    End Property
    Public Property EditDate() As Date Implements IZBatch.EditDate
        Get
            Return _editDate
        End Get
        Set(ByVal value As Date)
            _editDate = value
        End Set
    End Property
    Public Property FinalizedDate() As Date Implements IZBatch.FinalizedDate
        Get
            Return _finalizedDate
        End Get
        Set(ByVal value As Date)
            _finalizedDate = value
        End Set
    End Property
    Public Property Results() As ArrayList Implements IZBatch.Results
        Get
            If IsNothing(_results) Then CallForceLoad(Me)
            If IsNothing(_results) Then _results = New ArrayList()

            Return _results
        End Get
        Set(ByVal value As ArrayList)
            _results = value
        End Set
    End Property
    Public Property UserId() As Int32 Implements IZBatch.UserId
        Get
            Return _userId
        End Get
        Set(ByVal value As Int32)
            _userId = value
        End Set
    End Property
#End Region

#Region "Constructores"
    Private Sub New()
        MyBase.new()
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Id">Id del Lote</param>
    ''' <param name="Name">Nombre del Lote</param>
    ''' <param name="Userid">Opcional. Id del Usuario</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int32, ByVal Name As String, Optional ByVal Userid As Int32 = 9999)
        Me.New()
        Me.ID = Id
        Me.UserId = Userid
        Me.IconId = 19
        Me.Name = Name.ToString
    End Sub
#End Region



End Class