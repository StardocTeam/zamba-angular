Imports ZAMBA.Servers
Imports Zamba.Core
'Imports Zamba.DBAccess
<Ipreprocess.PreProcessName("Importar datos"), Ipreprocess.PreProcessHelp("Toma el código del archivo de texto y trae todos los datos elegidos. Parametros: Id Consulta, Id Campo clave")> _
Public Class ippImportControl
    Implements Ipreprocess
    Dim DsCon As New DsConfig

    'TODO ERROR: REFERENCIAR DE BUSSINES A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Dim con1 As IConnection ', con2

    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region
#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        For Each f As String In Files
            processFile(f, param(0))
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        If IO.File.Exists(File) Then
            Dim Sr As New IO.StreamReader(File)
            Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

            Dim SW As New IO.StreamWriter(Dir & "\Ztempfile.txt")
            'Dim sdb As New CWizard

            Dim sql As String
            Dim campos() As String
            Dim i As Int32
            Dim linea As New System.Text.StringBuilder
            Dim DsAddData As DataSet
            Dim parametros() As String = param.Split(","c)
            If parametros.Length >= 2 Then
                Dim arrayWhere As New ArrayList
                While Sr.Peek <> -1
                    campos = Sr.ReadLine.Split("|"c)
                    For i = 1 To parametros.Length - 1  'el primer parametro no lo tomo
                        arrayWhere.Add(campos(parametros(i)))
                    Next
                    'sql = sdb.MakeSelect(CInt(parametros(0)), arrayWhere)
                    'se cambió por:
                    sql = DataBaseAccessBusiness.UCWizard.MakeSelect(CInt(parametros(0)), arrayWhere)


                    ' If IsNumeric(campos(parametros(i))) Then
                    ' sql &= campos(CType(DsCon.DsConfig.Rows(0).Item("Campo")))
                    'Else
                    '   sql = "'" & campos(parametros(i)) & "'"
                    'End If
                    'Ejecuto la consulta en la conexion de Zsdbconfig
                    ' DsAddData = con2.ExecuteDataset(CommandType.Text, sql)
                    DsAddData = Server.Con.ExecuteDataset(CommandType.Text, sql)
                    'linea = campos(CType(DsCon.DsConfig.Rows(0).Item("Campo"))) & "|"
                    For i = 0 To DsAddData.Tables(0).Columns.Count - 1
                        If DsAddData.Tables(0).Rows.Count = 1 Then
                            linea.Append(DsAddData.Tables(0).Rows(0).Item(i))
                            linea.Append("|")
                        Else
                            linea.Append("|")
                            LogError("El código " & campos(DsCon.DsConfig.Rows(0).Item("Campo")) & " tiene " & DsAddData.Tables(0).Rows.Count.ToString & " coincidencias en la base de datos. ")
                        End If
                    Next
                    For i = 0 To campos.Length - 1
                        linea.Append(campos(i))
                        linea.Append("|")
                    Next
                    linea.Replace(linea.ToString, linea.ToString.Substring(0, linea.Length - 1))
                    SW.WriteLine(linea.ToString)
                    linea.Append("")
                End While
                SW.Close()
                Sr.Close()
                Try
                    IO.File.Delete(File)
                    IO.File.Move(Dir & "\Ztempfile.txt", File)
                    IO.File.Delete(Dir & "\Ztempfile.txt")
                Catch ex As Exception
                End Try
            Else
                RaiseEvent PreprocessError("El Archivo no existe")
                RaiseEvent PreprocessMessage("El archivo no existe")
            End If
        Else
            RaiseEvent PreprocessError("Falta algun parametro para el preproceso")
        End If
        Return File
    End Function
    Private Shared Sub LogError(ByVal msn As String)
        Dim sw As New IO.StreamWriter(".\ErrorImport.txt", True)
        sw.WriteLine(msn)
        sw.Close()
    End Sub
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Toma el código del archivo de texto y trae todos los datos elegidos. Parametros: Id Consulta, Id Campo clave"
    End Function
#Region "New"
    Public Sub New()
        Try
            Dim Server As New Server(Nothing)
            If IO.File.Exists(".\zdbconfig.xml") Then
                DsCon.ReadXml(".\zdbconfig.xml")
            Else
                Dim row As DsConfig.DsConfigRow = DsCon.DsConfig.NewDsConfigRow
                row.Campo = 0
                row.Consulta = 1
                DsCon.Tables(0).Rows.Add(row)
                DsCon.WriteXml(".\zdbconfig.xml")
            End If
            Try

                con1 = Server.Con
                Dim ds As DataSet = Nothing
                Try
                    Dim sql As String = "Select Servername,usuario,clave,servertype,DB from zqueryname where id=" & DsCon.DsConfig.Rows(0).Item("Consulta")
                    ds = con1.ExecuteDataset(CommandType.Text, sql)
                Catch
                End Try
                'Instancio la conexion alternativa
                Try
                    If ds.Tables(0).Rows.Count = 1 Then
                        Server.MakeConnection(ds.Tables(0).Rows(0).Item(3), ds.Tables(0).Rows(0).Item(0), Zamba.Tools.Encryption.DecryptString(ds.Tables(0).Rows(0).Item(4), key, iv), ds.Tables(0).Rows(0).Item(1), ds.Tables(0).Rows(0).Item(2))
                        '  con2 = server.Con(True)
                    End If
                    Server.MakeConnection()
                    con1 = Server.Con
                    Server.dispose()
                Catch ex As Exception
                End Try
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
            'verificar esto
        Catch
        End Try
    End Sub
#End Region

End Class
