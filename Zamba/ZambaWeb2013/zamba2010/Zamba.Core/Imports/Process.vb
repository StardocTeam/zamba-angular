

'Imports zamba.imports

Public Class Process
    Implements IProcess

#Region " Atributos "
    Private _id As Int32
    Private _name As String = String.Empty
    Private _path As String = String.Empty
    Private _caracter As String = String.Empty
    Private _flagAcceptBlankData As Boolean = True
    Private _verify As Boolean = True
    Private _docType As IDocType
    Private _move As Boolean = False
    Private _index As New ArrayList
    Private _indexOrder As New ArrayList
    Private _flagBackUp As Boolean = False
    Private _replace As Boolean = False
    Private _checkBatch As Boolean = True
    Private _flagSourceVariable As Boolean = False
    Private _flagDeleteSourceFile As Boolean = False
    Private _flagMultipleFile As Boolean = False
    Private _multipleCaracter As String = String.Empty
    Private _createFolder As Boolean = False
    Private _ipGroup As Int32
    Private _preprocessNames As New ArrayList
    Private _backUpPath As String = String.Empty
    Private _userId As Int32 = 9999
    Private _type As ProcessTypes = ProcessTypes.Common
    Private _askConfirmation As Boolean = False
    Private _dsProcessIndex As New DsIpIndex
    Private _History As IProcessHistory
#End Region

#Region " Propiedades "
    Public Property ID() As Int32 Implements IProcess.ID
        Get
            Return _id
        End Get
        Set(ByVal value As Int32)
            _id = value
        End Set
    End Property
    Public Property Name() As String Implements IProcess.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property Path() As String Implements IProcess.Path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
    Public Property Caracter() As String Implements IProcess.Caracter
        Get
            Return _caracter
        End Get
        Set(ByVal value As String)
            _caracter = value
        End Set
    End Property
    'Public DocTypeId As Int32
    Public Property DocType() As iDocType Implements IProcess.DocType
        Get
            Return _docType
        End Get
        Set(ByVal value As IDocType)
            _docType = value
        End Set
    End Property
    Public Property Index() As ArrayList Implements IProcess.Index
        Get
            Return _index
        End Get
        Set(ByVal value As ArrayList)
            _index = value
        End Set
    End Property
    Public Property IndexOrder() As ArrayList Implements IProcess.IndexOrder
        Get
            Return _indexOrder
        End Get
        Set(ByVal value As ArrayList)
            _indexOrder = value
        End Set
    End Property
    Public Property Move() As Boolean Implements IProcess.Move
        Get
            Return _move
        End Get
        Set(ByVal value As Boolean)
            _move = value
        End Set
    End Property
    Public Property Verify() As Boolean Implements IProcess.Verify
        Get
            Return _verify
        End Get
        Set(ByVal value As Boolean)
            _verify = value
        End Set
    End Property
    Public Property FlagAceptBlankData() As Boolean Implements IProcess.FlagAceptBlankData
        Get
            Return _flagAcceptBlankData
        End Get
        Set(ByVal value As Boolean)
            _flagAcceptBlankData = value
        End Set
    End Property
    Public Property FlagBackUp() As Boolean Implements IProcess.FlagBackUp
        Get
            Return _flagBackUp
        End Get
        Set(ByVal value As Boolean)
            _flagBackUp = value
        End Set
    End Property
    Public Property Replace() As Boolean Implements IProcess.Replace
        Get
            Return _replace
        End Get
        Set(ByVal value As Boolean)
            _replace = value
        End Set
    End Property
    Public Property CheckBatch() As Boolean Implements IProcess.CheckBatch 'esta propiedad obliga a realizar una verificacion de los DOC_IDS insertados
        Get
            Return _checkBatch
        End Get
        Set(ByVal value As Boolean)
            _checkBatch = value
        End Set
    End Property
    Public Property FlagDelSourceFile() As Boolean Implements IProcess.FlagDelSourceFile
        Get
            Return _flagDeleteSourceFile
        End Get
        Set(ByVal value As Boolean)
            _flagDeleteSourceFile = value
        End Set
    End Property
    Public Property FlagSourceVariable() As Boolean Implements IProcess.FlagSourceVariable
        Get
            Return _flagSourceVariable
        End Get
        Set(ByVal value As Boolean)
            _flagSourceVariable = value
        End Set
    End Property
    Public Property FlagMultipleFiles() As Boolean Implements IProcess.FlagMultipleFiles
        Get
            Return _flagMultipleFile
        End Get
        Set(ByVal value As Boolean)
            _flagMultipleFile = value
        End Set
    End Property
    Public Property MultipleCaracter() As String Implements IProcess.MultipleCaracter
        Get
            Return _multipleCaracter
        End Get
        Set(ByVal value As String)
            _multipleCaracter = value
        End Set
    End Property
    Public Property CreateFolder() As Boolean Implements IProcess.CreateFolder
        Get
            Return _createFolder
        End Get
        Set(ByVal value As Boolean)
            _createFolder = False
        End Set
    End Property
    Public Property IP_GROUP() As Int32 Implements IProcess.IP_GROUP
        Get
            Return _ipGroup
        End Get
        Set(ByVal value As Int32)
            _ipGroup = value
        End Set
    End Property
    Public Property preProcessNames() As ArrayList Implements IProcess.preProcessNames
        Get
            Return _preprocessNames
        End Get
        Set(ByVal value As ArrayList)
            _preprocessNames = value
        End Set
    End Property
    Public Property BackUpPath() As String Implements IProcess.BackUpPath
        Get
            Return _backUpPath
        End Get
        Set(ByVal value As String)
            _backUpPath = value
        End Set
    End Property
    Public Property UserId() As Int32 Implements IProcess.UserId
        Get
            Return _userId
        End Get
        Set(ByVal value As Int32)
            _userId = value
        End Set
    End Property
    Public Property type() As ProcessTypes Implements IProcess.type
        Get
            Return _type
        End Get
        Set(ByVal value As ProcessTypes)
            _type = value
        End Set
    End Property
    Public Property AskConfirmations() As Boolean Implements IProcess.AskConfirmations
        Get
            Return _askConfirmation
        End Get
        Set(ByVal value As Boolean)
            _askConfirmation = value
        End Set
    End Property
    Public Property DsProcessIndex() As DsIpIndex Implements IProcess.DsProcessIndex
        Get
            Return _dsProcessIndex
        End Get
        Set(ByVal value As DsIpIndex)
            _dsProcessIndex = value
        End Set
    End Property
    Public Property History() As IProcessHistory Implements IProcess.History
        Get
            If IsNothing(_History) Then
                _History = New ProcessHistory
                'Me.Histories.ProcessHistory(Me.HistoryIndex).ID, Me.ID, Me.Histories.ProcessHistory(Me.HistoryIndex).User_Id, Me.Histories.ProcessHistory(Me.HistoryIndex).Process_Date)
            End If
            Return _History
        End Get
        Set(ByVal Value As IProcessHistory)
            _History = Value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()

    End Sub
#End Region

End Class


Public Class PreProcessOBJ
    Public id As Integer
    Public name As String
    Public Param As String
    Public order As Integer
    Public Sub New()

    End Sub
    Public Sub New(ByVal idp As Integer, ByVal nombre As String, ByVal par As String, ByVal ord As Integer)
        Me.id = idp
        Me.name = nombre
        Me.Param = par
        Me.order = ord
    End Sub
End Class