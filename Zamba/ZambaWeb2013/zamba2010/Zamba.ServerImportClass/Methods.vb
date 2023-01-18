Imports zamba.Core
Imports System.Net
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.InteropServices

Public Class Methods
    Public Shared Sub InstanciarUsuario()
        Trace.WriteLine("Obteniendo los permisos de usuario")
        'JNC
        'Zamba.Core.RightComponent.ValidateLogIn(Int32.Parse(UserPreferences.getValue("UserId", Sections.UserPreferences, 0)))
        Trace.WriteLine("Los permisos se obtuvieron correctamente")
    End Sub

    Public Shared Sub AddTrace(ByVal startPath As String)
        Try
            If Boolean.Parse(UserPreferences.getValue("WithTrace", Sections.UserPreferences, True)) Then
                Dim level As Int32 = CType(UserPreferences.getValue("TraceLevel", Sections.UserPreferences, "1"), Int32)
                ZTrace.SetLevel(level, "Zamba Import Servidor")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub WritePort()
        If IO.File.Exists(".\port.txt") = False Then
            Dim sw As New IO.StreamWriter(".\port.txt", False)
            sw.WriteLine("9001")
            sw.WriteLine("TCP")
            sw.WriteLine("N")
            sw.WriteLine("0")
            sw.Close()
        End If
    End Sub

    Public Shared Sub ReadPort(ByRef puerto As String, ByRef Tipo As String, ByRef Threading As String, ByRef periodo As Int64)
        Dim sr As New IO.StreamReader(".\port.txt")
        puerto = sr.ReadLine
        Tipo = sr.ReadLine
        Threading = sr.ReadLine
        Try
            periodo = Int64.Parse(sr.ReadLine)
        Catch ex As Exception
            periodo = 0
        End Try
        sr.Close()
    End Sub

    Public Shared Sub AddToServers(ByVal NombreServidor As String, ByVal IP As String, ByVal Puerto As Int32, ByVal Objeto As String, ByVal Interfaz As String, ByVal Descripcion As String, ByRef NewId As int32)
        Try
            'todo que se fije si ya existe y lo actualice
            NewId = Zamba.Data.CoreData.GetNewID(IdTypes.ZServidores)
            Dim sql As String = "Insert into ZServidores (Id,NombreServidor,IP,Puerto,Objeto,Interfaz,Descripcion)values(" & NewId & ",'" & NombreServidor & "','" & IP & "','" & Puerto & "','" & Objeto & "','" & Interfaz & "','" & Descripcion & "')"
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetIp() As String
        Try
            Trace.WriteLine("IP:")
            Dim Host As String = Dns.GetHostName
            Dim IPs As IPHostEntry = Dns.GetHostEntry(Host)
            Dim Direcciones As IPAddress() = IPs.AddressList
            Trace.WriteLine(Direcciones(0).ToString())
            Return Direcciones(0).ToString()
        Catch ex As Exception
            Return "0.0.0.0"
        End Try
    End Function

    Public Shared Sub SetEngine()
        Trace.WriteLine("Ingreso con mas de un parametro")
        Dim ZServEngine As New ZServEngine()
        ZServEngine.Run1() 'SI RECIBE UN PARAMETRO QUE INGRESE LA LINEA
    End Sub

    Public Shared Sub SetChannel(ByVal puerto As String, ByVal tipo As String, ByVal Threading As String, ByVal periodo As String, ByRef remObj As WellKnownServiceTypeEntry, ByVal StartUpPath As String)
        Trace.WriteLine("Ingreso con 0 parametro")

        If tipo.ToString.ToUpper = "TCP" Then
            Dim channel As New TcpServerChannel(Integer.Parse(puerto))
            Try
                ChannelServices.RegisterChannel(channel)
            Catch
            End Try
        End If
        If tipo.ToString.ToUpper = "HTTP" Then
            Dim channel As New Http.HttpServerChannel(Integer.Parse(puerto))
            Try
                ChannelServices.RegisterChannel(channel)
            Catch
            End Try
        End If
        If Threading = "Y" AndAlso periodo > 0 Then
            Dim ZSrv As New ZEngineService(Long.Parse(periodo), StartUpPath)
        Else
            remObj = New WellKnownServiceTypeEntry(GetType(ZServEngine), "ZServEngine", WellKnownObjectMode.Singleton)
            RemotingConfiguration.RegisterWellKnownServiceType(remObj)
            Trace.WriteLine("Servidor Registrado en: TCP, Puerto " & puerto)
            Trace.WriteLine(remObj.ToString)
        End If
    End Sub

    Public Shared Sub RemoveFromServers(ByVal newId As int32)
        Try
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "Delete ZServidores Where Id =" & NewId)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class
