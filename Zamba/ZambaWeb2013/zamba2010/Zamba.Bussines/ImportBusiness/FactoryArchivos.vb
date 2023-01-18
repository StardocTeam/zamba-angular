Imports System.data
Imports ZAMBA.Servers
Imports System.Collections
Imports Zamba.AppBlock
Imports zamba.core

Public Class FactoryArchivos
    Public Shared Sub Dispose()
        Server.Con.dispose()
    End Sub

    ' Dim archivos As New DsIPTask

    Public Shared Function GetArchivosBloqueados() As DsIPTask
        'TODO Falta cambiar por store procedure
        Dim ds As New DataSet
        Dim archivos As New DsIPTask
        If Server.ServerType = DBTypes.Oracle9 OrElse Server.ServerType = DBTypes.Oracle Then
            Dim dstemp As New DataSet
            Dim parValues() As Object = {2}
            dstemp = Server.Con.ExecuteDataset("zsp_lock_100.GetBlockeds", parValues)
            dstemp.Tables(0).TableName = "IP_Task"
            ds.Merge(dstemp)
        Else
            Dim parameters() As Object = Nothing
            ds = Server.Con.ExecuteDataset("FactoryArchivos_GetArchivosBloqueados", parameters)
            ds.Tables(0).TableName = "IP_Task"
            archivos.Merge(ds)
        End If
        Return archivos
    End Function

    Public Shared Sub DelArchivoPendiente(ByVal TaskID As Integer)
        Dim StrDelete As String = "DELETE FROM IP_TASK where (ID = " & TaskID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
    End Sub

    Public Shared Sub BloquearArchivos(ByVal Files As ArrayList, ByVal User As String)
        Dim i As Integer
        For i = 0 To Files.Count - 1
            'PACKAGE UPDATE_IP_TASK_BLOQUEAR_pkg
            'PROC Update_IPTask_Block
            Dim Strupdate As String = "UPDATE IP_TASK SET BLOQUEADO = 1, USUARIO = " & User & " WHERE FILE_PATH = '" & Files(i) & "'"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Next
    End Sub

    Public Shared Sub DesBloquearArchivos()
        'UPDATE_IP_TASK_DESBLOQUEAR_pkg 
        'proc Update_IPTask_unblock
        Dim StrUpdate As String = "UPDATE IP_TASK SET MAQUINA = '" & Environment.MachineName & "', BLOQUEADO = " & 1
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
    End Sub

    Public Shared Sub DesBloquearTodosArchivos()
        'PACKAGE UPDATE_IP_TASK_UNBLOCKALL_pkg
        'Update_IPTask_unblockAll
        Dim StrUpdate As String = "UPDATE IP_TASK SET BLOQUEADO = 0 WHERE BLOQUEADO = 1"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
    End Sub

    Public Shared Sub DeleteArchivoIndexado(ByVal Path As String)
        Dim StrDelete As String = "DELETE FROM IP_TASK WHERE FILE_PATH = '" & Path & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
    End Sub

    Public Shared Sub BloquearArchivos(ByVal User As String)
        'PACKAGE UPDATE_IP_TASK_BLOQUEAR_2_pkg 
        'PROCEDURE(Update_IPTask_block2)
        Dim strUpdate As String = "UPDATE IP_Task SET Bloqueado = 1, Maquina = '" & Environment.MachineName & "', Usuario = '" & User & "' WHERE Bloqueado = 0"
        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
    End Sub

    Public Shared Function GetCountArchivosPendientesPC() As Int32
        Dim strSelect As String = "SELECT COUNT(Id) FROM IP_Folder WHERE NombreMaquina = '" & Environment.MachineName & "'"
        Return Server.Con.ExecuteScalar(CommandType.Text, strSelect)
    End Function

    Public Shared Function GetArchivosPendientesPC() As DsIPTask
        Dim strSelect As String = "SELECT Id FROM IP_Folder WHERE NombreMaquina = '" & Environment.MachineName & "'"
        Dim dsIds As DataSet
        dsIds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Dim StrWhere As String = ""
        If dsIds.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dsIds.Tables(0).Rows.Count - 1
                If i = 0 Then
                    StrWhere = "where id_configuracion = " & dsIds.Tables(0).Rows(i).Item("Id")
                Else
                    StrWhere = StrWhere & " or id_configuracion = " & dsIds.Tables(0).Rows(i).Item("Id")
                End If
            Next

            Dim StrUpdate As String = "UPDATE IP_TASK SET Bloqueado = 1, Maquina = '" & Environment.MachineName & "', Usuario = '" & Environment.UserName & "' " & StrWhere & " and bloqueado = 0"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
        End If

        Dim strSelect1 As String = "SELECT IP_Task.Id AS ID, IP_Task.File_Path AS Ruta, IP_Task.Zip_Origen AS Archivo_Zip FROM IP_Task WHERE Bloqueado = 1 and Maquina = '" & Environment.MachineName & "'"
        Dim dsfiles As New DsIPTask
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strSelect1)
        DSTEMP.Tables(0).TableName = "IP_Task"
        dsfiles.Merge(DSTEMP)
        Return dsfiles
    End Function

    Public Shared Function GetArchivosPendientesTodas() As DsIPTask
        'PACKAGE UPDATE_IP_TASK_BLOQUEAR_2_pkg 
        'PROCEDURE(Update_IPTask_block2)
        Dim strUpdate As String = "UPDATE IP_Task SET Bloqueado = 1, Maquina = '" & Environment.MachineName & "', Usuario = '" & Environment.UserName & "' WHERE Bloqueado = 0"
        Dim strSelect As String = "SELECT IP_Task.Id AS ID, IP_Task.File_Path AS Ruta, IP_Task.Zip_Origen AS Archivo_Zip FROM IP_Task WHERE Bloqueado = 1 and Maquina = '" & Environment.MachineName & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
        Dim archivos As New DsIPTask
        Dim Dstemp As DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Dstemp.Tables(0).TableName = "IP_TASK"
        archivos.Merge(Dstemp)
        Return archivos
    End Function

End Class
