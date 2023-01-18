'listo
Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_AddColumn_DOC_I_Fecha
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub


#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        'TODO store: SPGetDoc_type_id

        Dim sql As String
        Dim i As Int32
        'Dim sb As System.Text.StringBuilder

        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select doc_type_id from doc_type order by doc_type_id")
        For i = 0 To ds.Tables(0).Rows.Count - 1

            Try
                sql = "Drop Trigger crFecha" & ds.Tables(0).Rows(i).Item(0).ToString()
                If GenerateScripts = False Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
            End Try
            Try
                sql = "Alter table DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString() & " Drop column Fecha"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString()) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
            End Try
            If Servers.Server.ServerType = Servers.Server.DBTYPES.MSSQLServer OrElse Servers.Server.ServerType = Servers.Server.DBTYPES.MSSQLServer7Up Then
                sql = "Alter table DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString() & " Add Fecha smalldatetime Null default (convert(smalldatetime,convert(char,getdate(),103),103))"
            Else
                sql = "ALTER TABLE DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString() & " ADD (FECHA DATE DEFAULT Sysdate)"
            End If
            Try
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString()) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                Dim exn As New Exception("ERROR | " & ex.ToString & " en " & sql)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
                Exit For
            End Try


            If Servers.Server.ServerType = Servers.Server.DBTYPES.MSSQLServer OrElse Servers.Server.ServerType = Servers.Server.DBTYPES.MSSQLServer7Up Then
                sql = "Update DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString() & " set Fecha= convert(smalldatetime,convert(char(11),crdate,103),103)"
            Else
                sql = "Update DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString() & " set Fecha= crdate"
            End If

            Try
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("DOC_I" & ds.Tables(0).Rows(i).Item(0).ToString()) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                Dim exn As New Exception("ERROR | " & ex.ToString & " en " & sql)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
                Exit For
            End Try

        Next

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Create View volumenesview As Select top 100 percent  dg.disk_group_name Lista, dv.disk_vol_name Volumen, Disk_vol_path Ruta, Disk_vol_files Archivos,disk_vol_size MB, (disk_vol_size - (disk_vol_size_len/1024)) Libre from disk_group dg inner join Disk_group_R_Disk_volume on Disk_group_R_Disk_volume.DISK_GROUP_ID=dg.DISK_GROUP_ID inner join Disk_volume dv on dv.DISK_VOL_ID=Disk_group_R_Disk_volume.Disk_VOLUME_ID order by dg.disk_group_name,dv.disk_vol_name"
        Else
            sql = "Create or replace View volumenesview As Select dg.disk_group_name Lista, dv.disk_vol_name Volumen, Disk_vol_path Ruta, Disk_vol_files Archivos,disk_vol_size MB, (disk_vol_size - (disk_vol_size_len/1024)) Libre from disk_group dg inner join Disk_group_R_Disk_volume on Disk_group_R_Disk_volume.DISK_GROUP_ID=dg.DISK_GROUP_ID inner join Disk_volume dv on dv.DISK_VOL_ID=Disk_group_R_Disk_volume.Disk_VOLUME_ID order by dg.disk_group_name,dv.disk_vol_name"
        End If

        Try
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            Dim exn As New Exception("ERROR | " & ex.ToString & " en " & sql)
            'ZException.Log(exn, False)
            MessageBox.Show(exn.Message)
        End Try
        Return True
    End Function

#End Region

#Region "Propiedades"

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Agrega la columna 'Fecha' a todas las DOC_I con la fecha y sin hora. Crea un valor por defecto con la fecha del día por cada DOC_I para actualizarla"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AddColumn_DOC_I_Fecha"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AddColumn_DOC_I_Fecha
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("30/01/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property

    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 30
        End Get
    End Property

#End Region

End Class
