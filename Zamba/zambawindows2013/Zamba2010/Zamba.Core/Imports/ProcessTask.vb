
Public Class ProcessTask
    Implements IProcessTask

#Region " Atributos "
    Private _id As Integer
    Private _ProcessGroup As String = String.Empty
    Private _hora As String
    Private _dia As DayOfWeek
    Private _maquina As String = String.Empty
    Private _UserId As Int32
#End Region

#Region " Propiedades "
    Public Property Id() As Integer Implements IProcessTask.Id
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property
    Public Property ProcessGroup() As String Implements IProcessTask.ProcessGroup
        Get
            Return _ProcessGroup
        End Get
        Set(ByVal Value As String)
            _ProcessGroup = Value
        End Set
    End Property
    Public Property Hora() As String Implements IProcessTask.Hora
        Get
            Return _hora
        End Get
        Set(ByVal Value As String)
            _hora = Value
        End Set
    End Property
    Public Property Dia() As DayOfWeek Implements IProcessTask.Dia
        Get
            Return _dia
        End Get
        Set(ByVal Value As DayOfWeek)
            _dia = Value
        End Set
    End Property
    Public Property Maquina() As String Implements IProcessTask.Maquina
        Get
            Return _maquina
        End Get
        Set(ByVal Value As String)
            _maquina = Value
        End Set
    End Property
    Public Property UserId() As Integer Implements IProcessTask.UserId
        Get
            Return _UserId
        End Get
        Set(ByVal Value As Integer)
            _UserId = Value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()

    End Sub
#End Region

    Private Shared Function getTime() As String
        Dim alarm As String = String.Empty
        Dim hour As String = String.Empty
        Dim minutes As String = String.Empty
        If TimeOfDay.Minute < 10 Then
            minutes = "0" & TimeOfDay.Minute
        Else
            minutes = TimeOfDay.Minute.ToString
        End If
        If TimeOfDay.Hour < 12 Then
            minutes = minutes & " AM"
            hour = TimeOfDay.Hour.ToString()
        Else
            Dim aux As Integer = TimeOfDay.Hour - 12
            hour = aux.ToString()
            minutes = minutes & " PM"
        End If
        If Int32.Parse(hour) < 10 Then
            hour = "0" & hour
        End If
        alarm = hour & ":" & minutes
        Return alarm
    End Function
    Public Function CompareTimes() As Boolean Implements IProcessTask.CompareTimes
        Dim time As String = getTime()
        If Dia = Now.DayOfWeek And Hora = time Then
            Return True
        Else
            Return False
        End If
    End Function
End Class