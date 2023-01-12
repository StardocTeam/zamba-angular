''' -----------------------------------------------------------------------------
''' Project	 : ZMessages
''' Class	 : Messages.AutoMail
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Esta clase contiene la información de un template de mail que se guarda en la base 
''' para reutilizarlo
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Gonzalo]	30/03/2005	Created
''' 	[Gaston]	07/08/2008	Modified    Nueva propiedad PathImages
''' </history>
''' -----------------------------------------------------------------------------
Public Class AutoMail
    Implements IAutoMail

#Region " Atributos "
    Private _iD As Integer
    Private _name As String = String.Empty
    Private _to As String = String.Empty
    Private _CC As String = String.Empty
    Private _CCO As String = String.Empty
    Private _Subject As String = String.Empty
    Private _From As String = String.Empty
    Private _body As String = String.Empty
    Private _confirm As Boolean = False
    Private _AddDocument As Boolean = False
    Private _AddLink As Boolean = False
    Private _attachmentsPaths As New Collections.Generic.List(Of String)()
    Private __Attach As New ArrayList
    Private _pathImages As New Collections.Generic.List(Of String)()
    Private _taskId As Integer
    Private _docTypeId As Integer
#End Region

#Region " Propiedades "
    Public Property Body() As String Implements IAutoMail.Body
        Get
            Return _body
        End Get
        Set(ByVal Value As String)
            Me._body = Value
        End Set
    End Property
    Public Property Subject() As String Implements IAutoMail.Subject
        Get
            Return _Subject
        End Get
        Set(ByVal Value As String)
            _Subject = Value
        End Set
    End Property
    Public Property From() As String Implements IAutoMail.From
        Get
            Return _From
        End Get
        Set(ByVal Value As String)
            Me._From = Value
        End Set
    End Property
    Public Property Name() As String Implements IAutoMail.Name
        Get
            Return Me._name
        End Get
        Set(ByVal Value As String)
            Me._name = Value
        End Set
    End Property
    Public Property MailTo() As String Implements IAutoMail.MailTo
        Get
            Return Me._to
        End Get
        Set(ByVal Value As String)
            Me._to = Value
        End Set
    End Property
    Public Property CC() As String Implements IAutoMail.CC
        Get
            Return Me._CC
        End Get
        Set(ByVal Value As String)
            Me._CC = Value
        End Set
    End Property
    Public Property CCO() As String Implements IAutoMail.CCO
        Get
            Return Me._CCO
        End Get
        Set(ByVal Value As String)
            Me._CCO = Value
        End Set
    End Property
    Public Property Confirmation() As Boolean Implements IAutoMail.Confirmation
        Get
            Return Me._confirm
        End Get
        Set(ByVal Value As Boolean)
            Me._confirm = Value
        End Set
    End Property
    Public Property AddDocument() As Boolean Implements IAutoMail.AddDocument
        Get
            Return Me._AddDocument
        End Get
        Set(ByVal Value As Boolean)
            Me._AddDocument = Value
        End Set
    End Property
    Public Property AddLink() As Boolean Implements IAutoMail.AddLink
        Get
            Return Me._AddLink
        End Get
        Set(ByVal Value As Boolean)
            Me._AddLink = Value
        End Set
    End Property
    Public Property AttachmentsPaths() As Collections.Generic.List(Of String) Implements IAutoMail.AttachmentsPaths
        Get
            Return _attachmentsPaths
        End Get
        Set(ByVal value As Collections.Generic.List(Of String))
            _attachmentsPaths = value
        End Set
    End Property
    Public Property _Attach() As ArrayList Implements IAutoMail._Attach
        Get
            Return __Attach
        End Get
        Set(ByVal value As ArrayList)
            __Attach = value
        End Set
    End Property

    Public Property ID() As Integer Implements IAutoMail.ID
        Get
            Return _iD
        End Get
        Set(ByVal value As Integer)
            _iD = value
        End Set
    End Property

    Public Property TaskId() As Integer Implements IAutoMail.TaskID
        Get
            Return _taskId
        End Get
        Set(ByVal value As Integer)
            _taskId = value
        End Set
    End Property


    Public Property DocTypeID() As Integer Implements IAutoMail.DocTypeID
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Integer)
            _docTypeId = value
        End Set
    End Property


    Public Property PathImages() As Collections.Generic.List(Of String) Implements IAutoMail.PathImages
        Get
            Return (_pathImages)
        End Get
        Set(ByVal value As Collections.Generic.List(Of String))
            _pathImages = value
        End Set
    End Property

#End Region

End Class