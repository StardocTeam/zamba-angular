''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Interface	 : Core.IZambaCore
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Interfaz para la implementación de ZambaCore
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Marcelo]	15/12/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Interface IZambaCore
    Inherits ICore

    Property Location() As Drawing.Point
    Property TasksCount() As Int32
    Property color() As String
    Property Width() As Int32
    Property Height() As Int32
    Property CreateDate() As Date
    Property EditDate() As Date
    Property Help() As String

    Property PrintName() As String
    Property Parent() As IZambaCore
    Property IconId() As Int32
    Property ObjecttypeId() As Int32
    Property Childs() As Hashtable
    Property LastModified As String
    Property Status As Object
    Property Image As Object
    Function GetChild(ByVal Child_Id As Int32) As Object
    Function GetChild(ByVal Child_Name As String) As Object
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Event ChildAdded(ByVal Child As Object)
    Sub AddChild(ByVal Child As Object)
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Event ChildDeleted(ByVal Child As Object)
    Sub DelChild(ByVal Child_Id As Int32)

End Interface

