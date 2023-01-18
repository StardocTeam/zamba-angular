Imports System.Drawing
Imports System.Collections.Generic
Imports System.Xml.Serialization

<AttributeUsage(AttributeTargets.Class)> <Serializable()> _
Public Class WFStepState
    Inherits ZambaCore
    Implements IWFStepState


    Public Sub New(ByVal ID As Int64, ByVal Name As String, ByVal Description As String, ByVal Initial As Boolean)
        MyBase.New(ID, Name, 0, 0, Nothing)
        Me.Initial = Initial
        Me.Description = Description
    End Sub
    Public Sub New(ByVal ID As Int64)
        MyBase.New(ID, String.Empty, 0, 0, Nothing)
        Me.Initial = False
        Me.Description = String.Empty
    End Sub

    Private Sub New()
    End Sub
    Private mDescription As String
    Private mInitial As Boolean

    Public Property Description() As String Implements IWFStepState.Description
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is WFStepState Then
            If DirectCast(obj, WFStepState).ID = ID Then
                Return True
            End If
        ElseIf TypeOf obj Is Int64 Then
            If Int64.Parse(obj.ToString()) = ID Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Property Initial() As Boolean Implements IWFStepState.Initial
        Get
            Return mInitial
        End Get
        Set(ByVal value As Boolean)
            mInitial = value
        End Set
    End Property

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub
End Class
