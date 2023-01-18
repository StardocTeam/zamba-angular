''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'CLASE QUE PERMITE REALIZAR UN ABM DE ROWS DE LA TABLA IP_FOLDER                       '
'''''''''''''''''''
Imports ZAMBA.Servers
Imports System.data
'Imports Zamba.AppBlock
Imports Zamba.Core
'Imports Zamba.Data

Public NotInheritable Class ImportsFactory
    Inherits ZClass



    Public Shared Sub DelIPLIST(ByVal Id As Int32)
        Dim Strdelete As String = "DELETE FROM IP_TGRP where ID =" & Id
        Server.Con.ExecuteNonQuery(CommandType.Text, Strdelete)
    End Sub

    Public Shared Sub InsertIPLIST(ByVal Name As String, ByVal Description As String, ByVal Enabled As Int32)
        Dim strinsert As String = "INSERT INTO IP_TGRP (ID,NAME,DESCRIPTION,ENABLED) VALUES (" & CoreData.GetNewID(IdTypes.IPLIST) & ",'" & Name & "','" & Description & "'," & Enabled & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub

    Public Shared Function getProcess(ByVal IPLIST As Int32) As DSIpType
        Dim strselect As String = "Select * from IP_type where IP_GROUP = " & IPLIST & " ORDER BY IP_NAME"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        dstemp.Tables(0).TableName = "IP_TYPE"
        Dim DsProcess As New DSIpType
        DsProcess.Merge(dstemp)
        Return DsProcess
    End Function

    Public Shared Function GetProcessList() As DsIPList
        Dim DsIpList As New DsIPList
        Dim Ds As DataSet
        Dim strselect As String = "Select * from IP_TGRP ORDER BY NAME"
        Ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Ds.Tables(0).TableName = DsIpList.IP_TGRP.TableName
        DsIpList.Merge(Ds)
        Return DsIpList
    End Function

    'Public Shared Sub Dispose()
    '    Server.Con.dispose()
    'End Sub
    'METODO QUE PERMITE LLENAR UN DATASET CON LAS CONFIGURACIONES
    Public Shared Function GetConfigurations(ByVal IPFolder As dsIPFolder, ByVal UserId As Int32) As dsIPFolder
        Try
            Dim STRSELECT As String = "select NOMBRE,PATH,ID,NOMBREMAQUINA, SERVICE,USER_ID,ip_folderconf.TIMER from ip_folder inner join ip_folderconf on id_carpeta=id"
            Dim DSTEMP As DataSet
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, STRSELECT)
            DSTEMP.Tables(0).TableName = "IP_Folder"
            IPFolder.Merge(DSTEMP)
            Return IPFolder
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return IPFolder
        End Try
    End Function

    'METODO QUE PERMITE LLENAR UN DATASET CON LOS USUARIOS Y SUS IDs
    'Public Function GetUsers(ByVal UserTable As dsUserTable) As dsUserTable
    '    Try
    '        Dim Strselect As String = "SELECT * FROM USRTABLE"
    '        Return Server.Con.ExecuteDataset(CommandType.Text, Strselect)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        Return UserTable
    '    End Try
    'End Function

    ''METODO QUE PERMITE BORRAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    'Public Function DeleteRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean


    '    Try
    '        Dim strdelete As String = "DELETE FROM IP_FOLDER WHERE ID = " & Row.Item("Id")
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        Return False
    '    End Try
    '    Return True
    'End Function

    'METODO QUE PERMITE MODIFICAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    Public Shared Function UpdateRow(ByVal row As DataRow) As Boolean
        Try
            'ESTO HAY QUE REVISARLO
            Dim i As Byte
            If Boolean.Parse(row("Service").ToString()) Then
                i = 1
            Else
                i = 0
            End If
            'PACKAGE UPDATE_IP_FOLDER_pkg
            'PROC Update_IPFolder
            Dim strUpdate As String = "UPDATE IP_FOLDER SET Nombre='" & row("Nombre").ToString().Trim & "', Path='" & row("Path").ToString() & "', Timer=" & row("Timer").ToString() & ", Service=" & i & " WHERE User_Id=" & row("User_Id").ToString()
            Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    ''METODO QUE PERMITE GUARDAR UNA NUEVA ROW EN LA TABLA DE LA BASE DE DATOS
    'Public Function StoreRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean

    '    Try

    '        'ESTO HAY QUE REVISARLO
    '        Dim i As Byte
    '        If Row("Service") Then
    '            i = 1
    '        Else
    '            i = 0
    '        End If
    '        Dim strInsert As String = "INSERT INTO IP_FOLDER (Nombre,Path,Timer,Service,User_Id,NombreMaquina) VALUES ('" _
    '        & Row.Nombre & "', '" & Row.Path & "', " & Row("Timer") & ", " & i & ", " & Row("User_Id") & ", '" & Environment.MachineName & "')"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)

    '        Return False
    '    End Try
    '    Return True
    'End Function

    Public Shared Function GetConfigurations(ByVal IPFolder As dsIPFolder) As dsIPFolder
        Try
            Dim strselect As String = "SELECT * FROM IP_Folder"
            'Dim strselect As String = "SELECT * FROM IP_Folder WHERE NombreMaquina = '" & Machine & "'"
            Dim DSTEMP As DataSet
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strselect)

            DSTEMP.Tables(0).TableName = IPFolder.Tables(0).TableName
            ''IPFolder.Clear()
            'Dim c As DataColumn
            'Dim i As Int32
            'For i = 0 To IPFolder.Tables(0).Columns.Count - 1
            '    c = DSTEMP.Tables(0).Columns(i)
            '    IPFolder.Tables(0).Columns(i).ColumnName = c.ColumnName
            'Next
            IPFolder.Merge(DSTEMP)
            Return IPFolder
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return IPFolder
        End Try
    End Function

    Public Shared Function GetRowByID(ByVal Path As String, ByVal IPFolder As dsIPFolder) As dsIPFolder


        Try
            Dim strselect As String = "SELECT * FROM IP_Folder WHERE Path = '" & Path & "'"
            Dim DSTEMP As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
            DSTEMP.Tables(0).TableName = "IP_Folder"
            IPFolder.Merge(DSTEMP)
            Return IPFolder
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return IPFolder
        End Try
    End Function


    'METODO QUE PERMITE BORRAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    Public Shared Function DeleteRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean
        Try
            Dim strdelete As String = "DELETE FROM IP_FOLDER WHERE ID = " & Row.Item("Id")
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            strdelete = "DELETE FROM IP_FOLDERCONF WHERE ID_CARPETA = " & Row.Item("Id")
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    'METODO QUE PERMITE MODIFICAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    Public Shared Function UpdateRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean
        Try
            Dim strUpdate As String = "UPDATE IP_FOLDER SET Nombre='" & Row.NOMBRE.Trim & "', Path='" & Row.PATH & "' WHERE Id=" & Row.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    'METODO QUE PERMITE GUARDAR UNA NUEVA ROW EN LA TABLA DE LA BASE DE DATOS
    Public Shared Function StoreRow(ByVal Row As dsIPFolder.IP_FolderRow, ByVal iId As Int32) As Boolean
        Try
            Dim strInsert As String = "INSERT INTO IP_FOLDER (ID,Nombre,Path,NombreMaquina) VALUES (" & iId.ToString() & ",'" & Row.NOMBRE & "', '" & Row.PATH & "', '" & Row.NOMBREMAQUINA & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)

            Dim DSIpFolder As New dsIPFolder
            Dim DSTEMP As DataSet
            Dim STRSELECT As String = "SELECT * FROM IP_Folder WHERE Path = '" & Row.PATH & "'"
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, STRSELECT)
            DSTEMP.Tables(0).TableName = "IP_Folder"
            DSIpFolder.Merge(DSTEMP)

            Dim strInsert1 As String = "INSERT INTO IP_FolderConf (Id_Carpeta, Timer, Alarma, Domingo, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Tipo_Conf)" _
             & "VALUES (" & DSIpFolder.IP_Folder.Rows(0).Item("Id") & ", 90000, '00:00 AM', 0, 0, 0, 0, 0, 0, 0, 2)"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try
        Return True
    End Function



    'METODO QUE PERMITE GUARDAR UNA NUEVA ROW EN LA TABLA DE LA BASE DE DATOS
    Public Shared Function StoreIpFolderRow(ByVal Row As dsIPFolder.IP_FolderRow, ByVal iId As Int32) As Decimal
        Try
            Dim strInsert As String = "INSERT INTO IP_FOLDER (ID,Nombre,Path,NombreMaquina) VALUES (" & iId.ToString() & ",'" & Row.NOMBRE & "', '" & Row.PATH & "', '" & Row.NOMBREMAQUINA & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)


            Dim DSIpFolder As New dsIPFolder
            Dim DSTEMP As DataSet
            Dim STRSELECT As String = "SELECT * FROM IP_Folder WHERE Path = '" & Row.PATH & "'"
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, STRSELECT)
            DSTEMP.Tables(0).TableName = "IP_Folder"
            DSIpFolder.Merge(DSTEMP)

            Dim strInsert1 As String = "INSERT INTO IP_FolderConf (Id_Carpeta, Timer, Alarma, Domingo, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Tipo_Conf)" _
             & "VALUES (" & DSIpFolder.IP_Folder.Rows(0).Item("Id") & ", 90000, '00:00 AM', 0, 0, 0, 0, 0, 0, 0, 2)"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return -1
        End Try
        Return iId
    End Function

    Public Overrides Sub Dispose()

    End Sub


    Public Shared Function GetFolderData(ByVal FolderPath As String) As dsIPFolder
        Dim dsCarpetas As New dsIPFolder
        Dim DSTEMP As DataSet
        Dim strselect As String = "SELECT * FROM IP_Folder WHERE Path='" & FolderPath & "'" ' and Nombremaquina='"
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        DSTEMP.Tables(0).TableName = "IP_Folder"
        dsCarpetas.Merge(DSTEMP)
        Return dsCarpetas
    End Function

    Public Shared Function GetIPFOLDERCONFByFolderId(ByVal FolderId As Int32) As dsIPFolderConf
        Dim dsIPFolderConf As New dsIPFolderConf
        Dim strSelect As String = "SELECT * FROM IP_FolderConf WHERE Id_Carpeta = " & FolderId
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        DSTEMP.Tables(0).TableName = "IP_FolderConf"
        dsIPFolderConf.Merge(DSTEMP)
        Return dsIPFolderConf
    End Function

End Class
