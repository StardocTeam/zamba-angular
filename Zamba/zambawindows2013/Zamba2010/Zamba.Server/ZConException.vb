Imports ZAMBA
Imports Zamba.Tools
Public Class ZConnectionException
    Inherits System.Exception
    Public Shared Sub Log(ByVal Ex As System.Exception, Optional ByVal CommandText As String = "", Optional ByVal SendMail As Boolean = True)
        Dim Source As New System.Text.StringBuilder
        Dim WinUser As New System.Text.StringBuilder
        Dim Machine As New System.Text.StringBuilder
        Dim Obs1 As New System.Text.StringBuilder
        Dim obs2 As New System.Text.StringBuilder
        Dim obs3 As New System.Text.StringBuilder
        Dim Id As Int32 = 0

        Try
            Source.Append(CommandText.ToString)
        Catch
        End Try
        Try
            Source.Append(Ex.Source.ToString)
            Source.Append("-")
            Source.Append(Ex.TargetSite.Name.ToString)
        Catch
        End Try

        Try
            Obs1.Append(Ex.StackTrace.ToString)
        Catch
        End Try
        Try
            Obs1.Append(Ex.TargetSite.ToString)
        Catch
        End Try
        Try
            Obs1.Append(Ex.InnerException.Message.ToString)
            Obs1.Append(" ")
            Obs1.Append(Ex.InnerException.Source.ToString)
        Catch
        End Try
        Try
            WinUser.Append(Environment.UserName.ToString)
            Machine.Append(Environment.MachineName.ToString)
        Catch
        End Try
      
        Try
            Dim Ds As New DsConExcep
            Ds.Excep.AddExcepRow(Id, Now.ToString, Source.ToString, Ex.Message, Ex.ToString, Obs1.ToString, obs2.ToString, obs3.ToString, WinUser.ToString, Machine.ToString)
            Ds.AcceptChanges()
            Dim Dir As IO.DirectoryInfo = New IO.DirectoryInfo(Membership.MembershipHelper.AppTempPath & "\Exceptions")

            Dim filename As New System.Text.StringBuilder
            filename.Append(Dir.FullName.ToString)
            filename.Append("\")
            filename.Append("Excep ")
            filename.Append(Now.Day.ToString)
            filename.Append("-")
            filename.Append(Now.Month.ToString)
            filename.Append("-")
            filename.Append(Now.Year.ToString)
            filename.Append(" - ")
            filename.Append(Now.Hour.ToString)
            filename.Append("-")
            filename.Append(Now.Minute.ToString)
            filename.Append("-")
            filename.Append(Now.Second.ToString)
            filename.Append("-")
            filename.Append(".txt")
            Dim Fi As New IO.FileInfo(filename.ToString)
            Ds.WriteXml(Fi.FullName, XmlWriteMode.DiffGram)

        Catch
        End Try
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal Reason As String, Optional ByVal Clase As String = "Indefinida", Optional ByVal Procedimiento As String = "Indefinido", Optional ByVal Linea As Int32 = 0)
        ExMessage = ex.Message
        Me.Reason = Reason
        Me.Clase = Clase
        Me.Procedimiento = Procedimiento
        Me.Linea = Linea
    End Sub

    Private _clase As String
    Private _EXMessage As String
    Dim _Reason As String
    Private _linea As Int32
    Private _procedimiento As String


    Public Property ExMessage() As String
        Get
            Return _EXMessage
        End Get
        Set(ByVal Value As String)
            _EXMessage = Value
        End Set
    End Property

    Public Property Reason() As String
        Get
            Return _Reason
        End Get
        Set(ByVal Value As String)
            _Reason = Value
        End Set
    End Property
    Public Property Clase() As String
        Get
            Return _clase
        End Get
        Set(ByVal Value As String)
            _clase = Value
        End Set
    End Property

    Public Property Procedimiento() As String
        Get
            Return _procedimiento
        End Get
        Set(ByVal Value As String)
            _procedimiento = Value
        End Set
    End Property
    Public Property Linea() As Int32
        Get
            Return _linea
        End Get
        Set(ByVal Value As Int32)
            _linea = Value
        End Set
    End Property
End Class
