Imports System.Reflection
Imports ZAMBA.Core
Imports Zamba.Servers
Imports Zamba.Data
Imports System.Windows.Forms
Imports System.Collections.Generic
'Imports Zamba.CommonLibrary
Imports System.Reflection.Assembly
Public Class PreProcessFactory
    Inherits ZClass
    Public Overrides Sub Dispose()

    End Sub
    Public Event PreProcessMessage(ByVal msg As String)
    Public Event PreprocessError(ByVal errormsg As String)

    Public Function Preprocess(ByVal PreProcessList As ArrayList, ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal Test As Boolean = False) As ArrayList
        Dim i As Integer
        Dim pre As Ipreprocess
        Try
            Dim q As Int32 = PreProcessList.Count - 1

            For i = 0 To q
                Dim pprocess As PreProcessOBJ = PreProcessList(i)
                pre = ReturnpreProcessInstance(PreProcessList(i).Name)
                'RemoveHandler pre.PreprocessMessage, AddressOf PreProcessMessageSub
                'RemoveHandler pre.PreprocessError, AddressOf PreProcessErrorSub
                'AddHandler pre.PreprocessMessage, AddressOf PreProcessMessageSub
                'AddHandler pre.PreprocessError, AddressOf PreProcessErrorSub
                Dim paramlist As New ArrayList
                paramlist.AddRange(pprocess.Param.Split(","))
                Files = pre.process(Files, paramlist, , Test)
                If isnothing(Files) OrElse Files(0) = Nothing Then
                    Exit For
                End If
            Next
            Return Files
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en PreProcess " & ex.ToString)
            Zamba.core.zclass.raiseerror(New Exception("Error al Ejecutar el preproceso " & PreProcessList(i).Name & ". " & ex.ToString))
        End Try
        Return Nothing
    End Function

    Public Shared Function ReturnpreProcessInstance(ByVal PreProcessName As String) As Ipreprocess
        Dim a As Reflection.Assembly = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.PreProcess.dll")
        Dim types As New ArrayList(a.GetTypes())
        If Not PreProcessName.StartsWith("ipp") Then
            PreProcessName = "ipp" & PreProcessName
        End If
        For Each type As Type In types
            If type.Name.ToUpper.ToUpper() = PreProcessName.ToUpper() Then
                Dim instance As Object
                'instance = GetExecutingAssembly.CreateInstance(type.Name)
                instance = Activator.CreateInstance(type)
                Return DirectCast(instance, Ipreprocess)
                Exit Function
            End If
        Next
        Return Nothing
    End Function

    Public Sub preprocessFile(ByVal PreProcessName As String, ByVal file As String, ByVal param As String, Optional ByVal test As Boolean = False)
        Try

            Dim pre As Ipreprocess = ReturnpreProcessInstance(PreProcessName)
            Dim result As String
            'RemoveHandler pre.PreprocessMessage, AddressOf PreProcessMessageSub
            'RemoveHandler pre.PreprocessError, AddressOf PreProcessErrorSub
            'AddHandler pre.PreprocessMessage, AddressOf PreProcessMessageSub
            'AddHandler pre.PreprocessError, AddressOf PreProcessErrorSub
            result = pre.processFile(file, param, , test)
        Catch ex As Exception
            Throw New Exception("Error al Ejecutar el preproceso " & PreProcessName & ". " & ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Devuelve una lista de clases de procesos que empiezan con ipp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProcess() As List(Of Type)
        Dim typeList As New List(Of Type)
        'Dim hash As New Hashtable
        Dim a As Reflection.Assembly = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.PreProcess.dll")
        Dim types As New ArrayList(a.GetTypes())
        For Each type As Type In types
            If type.Name.ToUpper.IndexOf("IPP") >= 0 AndAlso type.Name.ToUpper <> "IPPOSTPROCESS" Then
                typeList.Add(type)
            End If
        Next
        types = Nothing
        Return typeList
    End Function


    ''' <summary>
    ''' Guarda los preprocesos
    ''' </summary>
    ''' <param name="preid"></param>
    ''' <param name="preprocess"></param>
    ''' <param name="isImport"></param>
    ''' <remarks></remarks>
    Public Shared Sub savePreprocess(ByVal preid As Integer, ByVal preprocess As ArrayList, ByVal isImport As Boolean)
        If isImport = True Then
            PreProcess_Factory.saveImportPreprocess(preid, preprocess)
        Else
            PreProcess_Factory.saveMonitorPreprocess(preid, preprocess)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los preprocesos
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="isImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getPreprocess(ByVal id As Integer, ByVal isImport As Boolean) As ArrayList
        Dim dsp As New dsIPPreprocess
        Dim PreProcessList As New ArrayList
        Try
            If isImport = True Then
                dsp = PreProcess_Factory.GetImportPreProcessById(id)
            Else
                dsp = PreProcess_Factory.GetMonitorPreProcessById(id)
            End If

            Dim i As Integer
            For i = 0 To dsp.Ip_Preprocess.Count - 1

                Dim pre As New PreProcessOBJ

                pre.id = dsp.Ip_Preprocess(i).IP_ID
                If Not IsDBNull(dsp.Ip_Preprocess(i).PREPROCESSNAME) Then
                    pre.name = dsp.Ip_Preprocess(i).PREPROCESSNAME
                Else
                    pre.name = String.Empty
                End If
                Try
                    If Not IsDBNull(dsp.Ip_Preprocess(i).PREPROCESSPARAM) Then
                        pre.Param = dsp.Ip_Preprocess(i).PREPROCESSPARAM
                    Else
                        pre.Param = String.Empty
                    End If
                Catch ex As System.Data.StrongTypingException
                    pre.Param = String.Empty
                End Try

                pre.order = dsp.Ip_Preprocess(i).ORDERpos

                PreProcessList.Add(pre)
            Next
        Catch ex As Exception
            Return PreProcessList
        End Try
        Return PreProcessList
    End Function
    ''' <summary>
    ''' Devuelve todos los nombres de los preprocesos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllProcessNames() As ArrayList
        Dim processList As New ArrayList

        For Each type As Type In GetProcess()
            For Each item As Object In type.GetCustomAttributes(True)
                If item.GetType.Name = "PreProcessName" Then
                    processList.Add(DirectCast(item, Ipreprocess.PreProcessName).Name)
                End If
            Next
        Next
        Return processList
    End Function

    ''' <summary>
    ''' Devuelve todos los nombres de los preprocesos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllProcessNamesAndTypes() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Name")
        dt.Columns.Add("Value")

        For Each type As Type In GetProcess()
            For Each item As Object In type.GetCustomAttributes(True)
                If item.GetType.Name = "PreProcessName" Then
                    Dim dr As DataRow = dt.NewRow()
                    dr("Name") = DirectCast(item, Ipreprocess.PreProcessName).Name
                    dr("Value") = type.Name

                    dt.Rows.Add(dr)
                End If
            Next
        Next
        Return dt
    End Function

    ''' <summary>
    ''' Devuelve los procesos que el usuario puede ver
    ''' </summary>
    ''' <param name="user"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProcessByUser(ByVal user As iuser) As ArrayList
        'Dim procesosFiltrados As New ArrayList
        ''Dim asm As Assembly = Assembly.LoadFile("Zamba.PreProcess")

        'For Each type As Type In GetProcess()
        '    For Each item As Object In type.GetCustomAttributes(True)
        '        If item.GetType.Name = "PreProcessName" Then
        '            Dim processName As String = DirectCast(item, Ipreprocess.PreProcessName).Name
        '            Dim idProceso As Int64 = ProcessFactory.GetProcessIDByName(processName)
        '            If GetRight(user, idProceso) Then
        '                procesosFiltrados.Add(processName)
        '            End If
        '            Exit For
        '        End If
        '    Next
        'Next

        'Return procesosFiltrados
        ' Return PreProcessFactory.GetProcessByUs
        Return ProcessFactory.GetProcessByUserId(Membership.MembershipHelper.CurrentUser.ID)
    End Function

    ''' <summary>
    ''' Valida el permiso del User con el ID del proceso
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="processId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetRight(ByVal user As iuser, ByVal processId As Integer) As Boolean
        Return UserBusiness.Rights.GetUserRights(user, ObjectTypes.Preproceso, Zamba.Core.RightsType.Use, processId)
    End Function

    'Public Shared Function GetIdByProcessName(ByVal preProcessName As String) As Int32
    '    Dim Query As String = "SELECT preprocessid FROM preprocessids WHERE preprocessname = '"
    '    Query = Query + preProcessName
    '    Query = Query + "'"
    '    Dim retorno As Object = Server.Con.ExecuteScalar(CommandType.Text, Query)
    '    If retorno Is DBNull.Value Then
    '        Return 0
    '    Else
    '        Return CInt(retorno)
    '    End If
    'End Function
    Public Shared Function getmethods() As ArrayList
        Dim result As New ArrayList
        Dim asem As Reflection.Assembly = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.PreProcess.dll")
        Dim typ As New ArrayList(asem.GetTypes())
        For Each t As System.Type In typ
            If t.Name.ToUpper.IndexOf("IPP") >= 0 AndAlso t.Name.ToUpper <> "IPPOSTPROCESS" Then
                result.Add(t.Name)
            End If
        Next
        Return result
    End Function
End Class

Public Class ProccessIdName

    Private _Id As Integer
    Private _Name As String

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Name
    End Function
   
End Class

