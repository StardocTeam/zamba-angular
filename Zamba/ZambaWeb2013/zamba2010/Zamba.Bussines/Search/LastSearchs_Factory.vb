Imports ZAMBA.Servers
Imports ZAMBA.Core
Imports System.IO
Imports System.Windows.Forms
Public Class LastSearchs_Factory
    Inherits ZClass

   
    Public Overrides Sub Dispose()

    End Sub
    '    Shared LS As New LS
    Shared ReadOnly Property dir() As DirectoryInfo
        Get
            If IsNothing(_Dir) Then
                _Dir = New IO.DirectoryInfo(Membership.MembershipHelper.AppTempPath & "\LTS")
            End If
            Return _Dir
        End Get
    End Property

    Shared _Dir As DirectoryInfo
    Shared fi As IO.FileInfo
    

#Region "Historial de busquedas realizadas"
    Public Shared Loaded As Boolean

    Shared LS3 As New DsLastSearchs
    Public Shared Sub LoadLastSearch3(Optional ByVal Reload As Boolean = False)
        Try
            If Loaded = False OrElse Reload = True Then
                If Not Dir.Exists Then Dir.Create()
                'Else
                fi = New IO.FileInfo(Dir.FullName & "\LS.xml")
                If fi.Exists Then
                    Try
                        LS3.Clear()
                        LS3.ReadXml(fi.FullName)
                    Catch
                        DelLsFile3(fi)
                    End Try
                Else
                    LS3.Clear()
                End If
                'End If
                Loaded = True
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub
    Private Shared DuplicateId As Int32
    Private Shared Function IsNameDuplicated3(ByVal name As String) As Boolean
        Try
            Dim i As Int32
            For i = 0 To LS3.LS.Count - 1
                If LS3.LS(i).NAME = name Then
                    DuplicateId = LS3.LS(i).ID
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Return True
        End Try
    End Function
    Private Shared Function GetSearchName3(ByVal DocTypes As ArrayList, ByVal Indexs() As Core.Index) As String
        Dim Name As New System.Text.StringBuilder
        Try
            Dim i As Int32
            If DocTypes.Count > 3 Then
                Name.Append("Documentos ")
            ElseIf DocTypes.Count = 2 Then
                Name.Append(DocTypes(0).Name.trim & " & ")
                Name.Append(DocTypes(1).Name.trim & " ")
            ElseIf DocTypes.Count = 3 Then
                Name.Append(DocTypes(0).Name.trim & " & ")
                Name.Append(DocTypes(1).Name.trim & " & ")
                Name.Append(DocTypes(2).Name.trim & " ")
            Else
                Name.Append(DocTypes(0).Name.trim & " ")
            End If

            For i = 0 To Indexs.Length - 1
                If Indexs(i).Data.Trim <> "" Then
                    Name.Append(Indexs(i).Name.Trim & " " & Indexs(i).[Operator].Trim & " " & Indexs(i).Data.Trim & " ")
                    If Indexs(i).[Operator].Trim = "Entre" Then
                        Name.Append(" & " & Indexs(i).Data2.Trim & " ")
                    End If
                End If
            Next
            If Name.Length = 0 Then Name.Append(Now.ToString("dd-MM-yy hh.mm"))
            Return Name.ToString
        Catch ex As Exception
            Return Now.ToString("dd-MM-yy hh.mm")
        Finally
            Name = Nothing
        End Try
    End Function
    Private Shared Function CheckMaxLastSearch3() As Int32
        'TODO Martin: Hacer que la cantidad de busquedas sea configurable en las preferencias de usuario
        Dim Count As Int32 = LS3.LS.Count
        If Count >= 20 Then
            Dim idtoreplace As Int32
            idtoreplace = LS3.LS(0).ID
            LS3.LS(0).Delete()
            Return idtoreplace
        Else
            Return LS3.LS.Count + 1
        End If
    End Function
    Private Shared Sub DelLsFile3(ByVal fi As IO.FileInfo)
        Try
            fi.Delete()
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub

    
    Public Shared Function GetLastSearchs3() As ArrayList
        Dim LastSearchs As New ArrayList

        Try
            LoadLastSearch3()
            Dim i As Int32
            Dim Name As String
            Dim Id As Int32
            For i = LS3.LS.Count - 1 To 0 Step -1
                Name = LS3.LS(i).NAME
                Id = LS3.LS(i).ID
                If Name.Trim = "" Then Name = Now.ToString
                LastSearchs.Add(New Searchs.LastSearch(Id, Name))
            Next
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
        Return LastSearchs
    End Function
    Public Shared Function GetLastSearchSQL3(ByVal Id As Int32) As String
        Try
            LoadLastSearch3()
            Dim i As Int32
            For i = LS3.LS.Count - 1 To 0 Step -1
                If Id = LS3.LS(i).ID Then
                    Return LS3.LS(i).SQL
                End If
            Next
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Guarda en un archivo serealizado la ultima busqueda
    ''' </summary>
    ''' <param name="NewId"></param>
    ''' <param name="Results"></param>
    ''' <history>   Marcelo     Modified    20/08/2009</history>
    ''' <remarks></remarks>
    Public Shared Sub SavelastSearchResults3(ByVal NewId As Int32, ByVal Results As DataTable)
        If IsNothing(Results) OrElse Results.Rows.Count = 0 OrElse NewId = 0 Then
            Exit Sub
        End If

        Try
            Dim Path As String = Dir.FullName & "\Results" & NewId & ".osl"
            If IO.File.Exists(Path) Then
                IO.File.Delete(Path)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Try
            Dim objStream As IO.Stream = IO.File.Open(Dir.FullName & "\Results" & NewId & ".osl", IO.FileMode.Create)
            Dim objFormatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            objFormatter.Serialize(objStream, Results)
            objStream.Close()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Obtiene la busqueda realizada anteriormente
    ''' </summary>
    ''' <param name="Id">Id de la busqueda</param>
    ''' <returns></returns>
    ''' <history>   Marcelo Modified 20/08/2009</history>
    ''' <remarks></remarks>
    Public Shared Function GetLastSearchResults3(ByVal Id As Int32) As DataTable
        Dim results As New DataTable
        Try
            Dim objStream As System.IO.Stream = IO.File.Open(Dir.FullName & "\Results" & Id & ".osl", IO.FileMode.Open)
            Dim objFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            results = DirectCast(objFormatter.Deserialize(objStream), DataTable)
            objStream.Close()
            Return results
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            Return results
        End Try
    End Function
#End Region

    
    Public Sub DeleteLastSearch()
        Try
            For Each f As FileInfo In dir.GetFiles
                Try
                    f.Delete()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class

