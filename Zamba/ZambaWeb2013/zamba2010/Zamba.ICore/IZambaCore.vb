Imports System
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

    Property PrintName() As String
    Property Parent() As IZambaCore
    Property IconId() As Int32
    Property ObjecttypeId() As Int32
    Property Childs() As Hashtable
    Function GetChild(ByVal Child_Id As Int32) As Object
    Function GetChild(ByVal Child_Name As String) As Object
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Event ChildAdded(ByVal Child As Object)
    Sub AddChild(ByVal Child As Object)
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Event ChildDeleted(ByVal Child As Object)
    Sub DelChild(ByVal Child_Id As Int32)

End Interface

