Imports Zamba.Core
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Core.Searchs
Imports Zamba.Servers
Imports System.Xml.Serialization
Imports System.IO
Imports Newtonsoft.Json

Public Class LastSearchBusiness
    Inherits ZClass

    Private lsd As LastSearchData = Nothing

    Public Sub New()
        Try
            lsd = New LastSearchData
            GetLastSearchsData()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub GetLastSearchsData()
        If Cache.Search.LastSearchs.Count = 0 Then
            Dim dt As DataTable = lsd.GetSearchs(Membership.MembershipHelper.CurrentUser.ID)
            For Each row As DataRow In dt.Rows
                Dim search As New LastSearch(CLng(row("searchid")), row("name").ToString)
                Cache.Search.LastSearchs.Insert(0, search)
            Next
            dt.Dispose()
            dt = Nothing
        End If
    End Sub

    Public Overrides Sub Dispose()
        lsd = Nothing
    End Sub

    ''' <summary>
    ''' Guarda la ultima busqueda realizada de forma serializada
    ''' </summary>
    ''' <param name="Doctypes"></param>
    ''' <param name="Indexs"></param>
    ''' <param name="Results"></param>
    ''' <param name="SQL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal search As Searchs.Search)
        Dim t As Transaction = Nothing
        Dim rollback As Boolean = False
        Dim searchId As Int64
        Dim SerializedSearch As String
        Dim currentUserId = Membership.MembershipHelper.CurrentUser.ID

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando ultima busqueda")
        Try
            searchId = CoreData.GetNewID(IdTypes.LASTSEARCH)
            SerializedSearch = SerializeObjectSearch(search)

            Dim ls As New LastSearch(searchId, search.Name, SerializedSearch)

            t = New Transaction
            lsd.Add(ls, currentUserId, t)
            lsd.AddSearchObject(ls, t)
            Cache.Search.LastSearchs.Insert(0, ls)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ultima busqueda guardada con exito")

            If Cache.Search.LastSearchs.Count > 20 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Removiendo ultimas busquedas excedentes")
                Dim searchToDelete As LastSearch = Cache.Search.LastSearchs(20)
                lsd.Delete(searchToDelete.Id)
                Cache.Search.LastSearchs.RemoveAt(20)
                searchToDelete.Dispose()
                searchToDelete = Nothing
            End If
            t.Commit()

        Catch ex As Threading.SynchronizationLockException
            rollback = True
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        Catch ex As Threading.ThreadAbortException
            rollback = True
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        Catch ex As Threading.ThreadInterruptedException
            rollback = True
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        Catch ex As Threading.ThreadStateException
            rollback = True
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        Catch ex As Exception
            rollback = True
            ZClass.raiseerror(ex)
        End Try

        If rollback AndAlso t IsNot Nothing Then
            Try
                If t.Transaction IsNot Nothing AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                    t.Rollback()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

    End Function

    Public Shared Function LastSearchAlreadyExist(searchName As String) As Boolean
        Dim _lastSearchExist As Boolean = False
        For Each ls As ILastSearch In Cache.Search.LastSearchs
            If ls.Name = searchName Then
                _lastSearchExist = True
                Exit For
            End If
        Next

        Return _lastSearchExist
    End Function

    Public Shared Function SerializeObjectSearch(toSerialize As Searchs.Search) As String
        Return JsonConvert.SerializeObject(toSerialize, Formatting.None, New JsonSerializerSettings() With {.TypeNameHandling = TypeNameHandling.Auto})
    End Function

    Public Shared Function DeserializeObjectSearch(toDeserialize As String) As Searchs.Search
        Return JsonConvert.DeserializeObject(Of Searchs.Search)(toDeserialize, New JsonSerializerSettings() With {.TypeNameHandling = TypeNameHandling.Auto})
    End Function


    ''' <summary>
    ''' Obtiene el nombre de la busqueda
    ''' </summary>
    ''' <param name="DocTypes"></param>
    ''' <param name="Indexs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSearchName(ByVal search As Searchs.Search) As String
        Dim sbName As New System.Text.StringBuilder
        Dim name As String
        Dim i As Int32
        Dim docTypes As List(Of IDocType) = search.Doctypes
        Dim indexs As List(Of IIndex) = search.Indexs


        Try
            If docTypes Is Nothing OrElse docTypes.Count = 0 Then
                sbName.Append("Buscar sobre múltiples entidades")
            Else

                sbName.Append(docTypes(0).Name.Trim)


                If docTypes.Count > 1 Then
                    If docTypes.Count = 2 Then
                        sbName.Append(" y ")
                    Else
                        sbName.Append(", ")
                    End If
                    sbName.Append(docTypes(1).Name.Trim)

                End If
                If docTypes.Count > 2 Then
                    sbName.Append(" y ")
                    sbName.Append(docTypes.Count - 2)
                    sbName.Append("...")
                End If
            End If

            If indexs IsNot Nothing AndAlso indexs.Count > 0 Then
                sbName.Append(": ")

                For i = 0 To indexs.Count - 1
                    If Not String.IsNullOrEmpty(indexs(i).Data.Trim) Then

                        sbName.Append(indexs(i).Name.Trim)
                        sbName.Append(" ")
                        sbName.Append(indexs(i).[Operator].Trim)
                        sbName.Append(" ")

                        If Not String.IsNullOrEmpty(indexs(i).dataDescription.Trim) Then
                            sbName.Append(indexs(i).dataDescription.Trim)
                        ElseIf indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                            Try
                                Dim query As String
                                query = String.Concat("Select descripcion from slst_s", indexs(i).Column.TrimStart("I"), " where codigo = ", indexs(i).Data.Trim)
                                Dim queryRead As System.Data.IDataReader
                                queryRead = Server.Con.ExecuteReader(CommandType.Text, query)
                                While queryRead.Read()
                                    sbName.Append(queryRead.GetString(0))
                                End While
                                If sbName.Length = 0 Then sbName.Append(indexs(i).Data.Trim)
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                                sbName.Append(indexs(i).Data.Trim)
                            End Try
                        Else
                            sbName.Append(indexs(i).Data.Trim)
                        End If

                        If indexs(i).[Operator].Trim = "Entre" Then
                            sbName.Append(" <= " & indexs(i).Data2.Trim)
                        End If
                        sbName.Append(", ")
                    End If
                Next
                sbName.Remove(sbName.Length - 2, 2)
            ElseIf Not String.IsNullOrEmpty(search.Textsearch) Then
                sbName.Append(": ")
                sbName.Append(search.Textsearch)
            Else
                sbName.Append(" sin filtrar")
            End If

            name = sbName.ToString.Trim

        Catch ex As Exception
            name = Now.ToString("dd-MM-yy hh.mm")

        Finally
            sbName.Clear()
            sbName = Nothing
            docTypes = Nothing
            indexs = Nothing
        End Try

        Return name
    End Function

    Public Function GetSerializedSearchObject(id As Long) As Searchs.Search
        Dim serializedSearch As String = lsd.GetSerializedSearchObject(id)
        Dim deserializedSearch As Searchs.Search = DeserializeObjectSearch(serializedSearch)
        Return deserializedSearch
    End Function

    ''' <summary>
    ''' Obtiene las ultimas busquedas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLastSearchs() As List(Of LastSearch)
        GetLastSearchsData()
        Return Cache.Search.LastSearchs.ToList
    End Function

    Public Function GetSQL(ByVal id As Int32) As List(Of String)
        Dim sqlList As New List(Of String)
        Dim dt As DataTable = lsd.GetSQL(id)

        For i As Int32 = 0 To dt.Rows.Count - 1
            sqlList.Add(dt.Rows(i)(0))
        Next

        dt.Dispose()
        dt = Nothing

        Return sqlList
    End Function

    Public Function GetLastSearchByName(ByVal name As String) As LastSearch
        For Each ls As LastSearch In Cache.Search.LastSearchs
            If ls.Name = name Then
                Return ls
            End If
        Next
        Return Nothing
    End Function

    Public Shared Sub DeleteAll()
        LastSearchData.DeleteAll(Membership.MembershipHelper.CurrentUser.ID)
        Cache.Search.LastSearchs.Clear()
    End Sub

End Class