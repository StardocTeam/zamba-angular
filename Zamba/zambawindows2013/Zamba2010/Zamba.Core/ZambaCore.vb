Imports System.Diagnostics.CodeAnalysis
Imports System.Drawing

<Serializable()> Public MustInherit Class ZambaCore
    Inherits ZBaseCore
    Implements IZambaCore

#Region " Atributos "
    Private _location As Point = Nothing
    Private _CreateDate As Date = Date.Now()
    Private _EditDate As Date = Date.Now()
    Private _Help As String = String.Empty
    Private _color As String = String.Empty
    Private _Width As Int32
    Private _Height As Int32
    Private _tasksCount As Int32

    Private _printName As String = String.Empty
    Private _parent As IZambaCore = Nothing
    'Private _docTypeId as Int64
    Private _iconId As Int32
    Private _objecttypeid As Int32
    Private _childs As Hashtable = Nothing
#End Region

#Region " Propiedades "
    Public Property Location() As Point Implements IZambaCore.Location
        Get
            If IsNothing(_location) Then _location = New Point()

            Return _location
        End Get
        Set(ByVal Value As Point)
            _location = Value
        End Set
    End Property
    Public Property color() As String Implements IZambaCore.color
        Get
            Return _color
        End Get
        Set(ByVal Value As String)
            _color = Value
        End Set
    End Property
    Public Property Width() As Int32 Implements IZambaCore.Width
        Get
            Return _Width
        End Get
        Set(ByVal Value As Int32)
            _Width = Value
        End Set
    End Property
    Public Property Height() As Int32 Implements IZambaCore.Height
        Get
            Return _Height
        End Get
        Set(ByVal Value As Int32)
            _Height = Value
        End Set
    End Property
    Public Property CreateDate() As Date Implements IZambaCore.CreateDate
        Get
            Return _CreateDate
        End Get
        Set(ByVal Value As Date)
            _CreateDate = Value
        End Set
    End Property
    Public Property EditDate() As Date Implements IZambaCore.EditDate
        Get
            Return _EditDate
        End Get
        Set(ByVal Value As Date)
            _EditDate = Value
        End Set
    End Property
    Public Property Help() As String Implements IZambaCore.Help
        Get
            Return _Help
        End Get
        Set(ByVal Value As String)
            _Help = Value
        End Set
    End Property
    Property TasksCount() As Int32 Implements IZambaCore.TasksCount
        Get
            Return _tasksCount
        End Get
        Set(ByVal value As Int32)
            _tasksCount = value
        End Set
    End Property

    Public Overridable Property PrintName() As String Implements IZambaCore.PrintName
        Get
            Return _printName
        End Get
        Set(ByVal Value As String)
            _printName = Value.Trim
        End Set
    End Property
    Public Overridable Property Parent() As IZambaCore Implements IZambaCore.Parent
        Get
            Return _parent
        End Get
        Set(ByVal Value As IZambaCore)
            _parent = Value
        End Set
    End Property
    Public Overridable Property IconId() As Int32 Implements IZambaCore.IconId
        Get
            Return _iconId
        End Get
        Set(ByVal Value As Int32)
            _iconId = Value
        End Set
    End Property
    Public Overridable Property ObjecttypeId() As Int32 Implements IZambaCore.ObjecttypeId
        Get
            Return _objecttypeid
        End Get
        Set(ByVal Value As Int32)
            _objecttypeid = Value
        End Set
    End Property
    Public Property Childs() As Hashtable Implements IZambaCore.Childs
        Get
            If IsNothing(_childs) Then CallForceLoad(Me)
            If IsNothing(_childs) Then _childs = New Hashtable()
            Return _childs
        End Get
        Set(ByVal Value As Hashtable)
            Try
                _childs = Value
            Catch ex As Exception
                raiseerror(ex)
            End Try
        End Set
    End Property

    Public Property LastModified As String Implements IZambaCore.LastModified
    Public Property Status As Object Implements IZambaCore.Status
    Public Property Image As Object Implements IZambaCore.Image

#End Region

#Region " Constructores "
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal IconId As Int32, ByVal ObjectTypeId As Int32, ByVal Parent As ZambaCore)
        Me.New()
        Me.ID = Id
        Me.Name = Name
        Me.IconId = IconId
        Me.ObjecttypeId = ObjectTypeId
        Me.Parent = Parent
    End Sub
#End Region

    Public Function GetChild(ByVal Child_Id As Int32) As Object Implements IZambaCore.GetChild
        Try
            If Childs.ContainsKey(Child_Id) Then
                Return Childs(Child_Id)
            End If
            Return Nothing
        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetChild(ByVal Child_Name As String) As Object Implements IZambaCore.GetChild
        Try
            For Each o As Object In Childs.Values

                If String.Compare(o.name.ToString(), Child_Name) = 0 Then
                    Return o
                End If
                'If o.Name Is Child_Name Then Return o
            Next
            Return Nothing
        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try
    End Function

    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Event ChildAdded(ByVal Child As Object) Implements IZambaCore.ChildAdded
    Public Sub AddChild(ByVal Child As Object) Implements IZambaCore.AddChild
        Try
            Childs.Add(Child.id, Child)
            RaiseEvent ChildAdded(Child)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Event ChildDeleted(ByVal Child As Object) Implements IZambaCore.ChildDeleted
    Public Sub DelChild(ByVal Child_Id As Int32) Implements IZambaCore.DelChild
        Try
            If Childs.ContainsKey(Child_Id) Then
                Childs.Remove(Child_Id)
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

End Class