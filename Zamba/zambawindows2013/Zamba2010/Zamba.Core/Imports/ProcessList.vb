'Imports zamba.imports


Public Class ProcessList
    Implements IProcessList

#Region " Atributos "
    Private _Id As Int32
    Private _Name As String = String.Empty
    Private _description As String = String.Empty
    Private _enabled As Boolean = False
    Private _dsProcess As New DataSet
#End Region

#Region " Propiedades "
    Public Property Id() As Int32 Implements IProcessList.Id
        Get
            Return _Id
        End Get
        Set(ByVal Value As Int32)
            _Id = Value
        End Set
    End Property
    Public Property Name() As String Implements IProcessList.Name
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
    Public Property Description() As String Implements IProcessList.Description
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property
    Public Property Enabled() As Boolean Implements IProcessList.Enabled
        Get
            Return _enabled
        End Get
        Set(ByVal Value As Boolean)
            _enabled = Value
        End Set
    End Property
    Public Property DsProcess() As DataSet Implements IProcessList.DsProcess
        Get
            Return _dsProcess
        End Get
        Set(ByVal value As DataSet)
            _dsProcess = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()

    End Sub
#End Region

    Public AskConfirmations As Boolean = False

End Class