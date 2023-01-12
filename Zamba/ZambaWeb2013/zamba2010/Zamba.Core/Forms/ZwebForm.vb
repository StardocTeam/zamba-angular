

Imports Zamba
Imports Zamba.Core

Public Class ZwebForm
    Inherits ZClass
    Implements IZwebForm

#Region "Atributos"
    Private _id As Int32
    Private _name As String = String.Empty
    Private _description As String = String.Empty
    Private _path As String = String.Empty
    Private _type As FormTypes
    Private _objectTypeId As IdTypes = IdTypes.ZWEBFORM
    Private _parentId As Int32
    Private _step_id As Int32
    Private _docTypeId As Int64
    Private _useRuleRights As Boolean
    Private _extensions As New ArrayList()
    Private _modifiedDate As DateTime = DateTime.MinValue
    Private _rebuild As Boolean
    Private _tempFullPath As String = String.Empty
    Private _tempPathName As String = String.Empty
    Private _encodedFile As Byte()


#End Region

#Region "Propiedades"

    Public Property EncodedFile As Byte() Implements IZwebForm.EncodedFile
        Get
            Return _encodedFile
        End Get
        Set(value As Byte())
            _encodedFile = value
        End Set
    End Property
    ''' <summary>
    ''' Si se utilizaran o no los permisos de las reglas
    ''' </summary>
    ''' <value></value>
    ''' <history>   Marcelo 29/01/2010 Created</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property useRuleRights() As Boolean Implements IZwebForm.useRuleRights
        Get
            Return _useRuleRights
        End Get
        Set(ByVal value As Boolean)
            _useRuleRights = value
        End Set
    End Property
    Public Property DocTypeId() As Int64 Implements IZwebForm.DocTypeId
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Int64)
            _docTypeId = value
        End Set
    End Property

    Public Property ObjectTypeId() As IdTypes Implements IZwebForm.ObjectTypeId
        Get
            Return _objectTypeId
        End Get
        Set(ByVal value As IdTypes)
            _objectTypeId = value
        End Set
    End Property
    Public Property ParentId() As Int32 Implements IZwebForm.ParentId
        Get
            Return _parentId
        End Get
        Set(ByVal value As Int32)
            _parentId = value
        End Set
    End Property
    Public Property Path() As String Implements IZwebForm.Path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
    Public Property Step_id() As Int32 Implements IZwebForm.Step_id
        Get
            Return _step_id
        End Get
        Set(ByVal value As Int32)
            _step_id = value
        End Set
    End Property
    Public Property Type() As FormTypes Implements IZwebForm.Type
        Get
            Return _type
        End Get
        Set(ByVal value As FormTypes)
            _type = value
        End Set
    End Property
    Public Property Description() As String Implements IZwebForm.Description
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property Name() As String Implements IZwebForm.Name
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property
    Public Property ID() As Int32 Implements IZwebForm.ID
        Get
            Return _id
        End Get
        Set(ByVal Value As Int32)
            _id = Value
        End Set
    End Property
    Public Property ModifiedTime() As DateTime Implements IZwebForm.ModifiedTime
        Get
            Return _modifiedDate
        End Get
        Set(ByVal Value As DateTime)
            _modifiedDate = Value
        End Set
    End Property
    Public Property ReBuild() As Boolean Implements IZwebForm.Rebuild
        Get
            Return _rebuild
        End Get
        Set(value As Boolean)
            _rebuild = value
        End Set
    End Property

    Public ReadOnly Property TempFullPath() As String Implements IZwebForm.TempFullPath
        Get
            If String.IsNullOrEmpty(_tempFullPath) Then
                Dim tempDir As IO.DirectoryInfo
                Try
                    tempDir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
                    If tempDir.Exists = False Then
                        tempDir.Create()
                    End If
                Catch
                    tempDir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\temp")
                    If tempDir.Exists = False Then
                        tempDir.Create()
                    End If
                End Try

                _tempFullPath = tempDir.FullName & Path.Remove(0, Path.LastIndexOf("\"))
            End If

            Return _tempFullPath
        End Get
    End Property
    Public ReadOnly Property TempPathName() As String Implements IZwebForm.TempPathName
        Get
            If String.IsNullOrEmpty(_tempPathName) Then
                _tempPathName = Path.Remove(0, Path.LastIndexOf("\") + 1)
            End If

            Return _tempPathName
        End Get
    End Property

#End Region

#Region "Constructores"
    Public Sub New()

    End Sub
    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Description As String, ByVal Type As FormTypes, ByVal Objecttypeid As Int32, ByVal path As String, ByVal DocTypeId As Int64, ByVal useRuleRights As Boolean, ByVal rebuild As Boolean)
        _id = Id
        _name = Name
        _description = Description
        _type = Type
        _objectTypeId = CType(Objecttypeid, IdTypes)
        _path = path
        _docTypeId = DocTypeId
        _rebuild = rebuild
    End Sub
    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Description As String, ByVal Type As FormTypes, ByVal path As String, ByVal DocTypeId As Int64, ByVal useRuleRights As Boolean, ByVal rebuild As Boolean)
        _id = Id 'FactoryForms.GetFormNewId
        _name = Name
        _description = Description
        _type = Type
        _path = path
        _docTypeId = DocTypeId
        _rebuild = rebuild
    End Sub
#End Region
    Public Overrides Sub Dispose()
    End Sub
End Class