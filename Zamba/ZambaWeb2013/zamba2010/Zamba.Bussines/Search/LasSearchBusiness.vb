Imports Zamba.Data
Imports Zamba.Core.Searchs
Imports Zamba.Servers
Imports Newtonsoft.Json
Imports System.Collections.Generic

Public Class LastSearchBusiness
    Inherits ZClass



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
        searchId = CoreData.GetNewID(IdTypes.LASTSEARCH)

    End Function

    Private Shared Function ReduceLength(text As String, Optional length As Int32 = 8) As String
        If text.Trim.Length > length Then
            Return text.Trim.Substring(0, length).Trim
        End If
        Return text.Trim
    End Function

    Public Function GetSearchName(ByVal search As Searchs.Search) As String

        Dim docTypesNames As List(Of String)
        Dim sbName As New Text.StringBuilder
        Dim name As String
        Dim i As Int32
        Dim docTypes As List(Of IDocType) = search.Doctypes
        Dim indexs As List(Of IIndex) = search.Indexs
        Dim docTypesLength As Int32 = 0
        Dim totalength As Int32 = 0
        Dim acceptedLength As Int32 = 50
        Dim minNameLegth As Int32 = 7

        Try
            If docTypes Is Nothing OrElse docTypes.Count <= 0 Then
                If Not docTypesNames Is Nothing Then
                    docTypesNames.Add("Buscar sobre múltiples entidades")
                End If
            Else
                    docTypesNames = New List(Of String)
                docTypesNames.Add(docTypes(0).Name.Trim)
                docTypesLength = docTypesNames(0).Length

                If docTypes.Count > 1 Then
                    docTypesNames.Add(docTypes(1).Name.Trim)
                    docTypesLength += docTypesNames(1).Length
                End If

                If docTypes.Count > 2 Then
                    docTypesNames.Add(String.Format(" y {0} más ", docTypes.Count - 2))
                    docTypesLength += docTypesNames(2).Length
                End If
            End If

            If indexs IsNot Nothing AndAlso indexs.Count > 0 Then
                sbName.Append(": ")

                For i = 0 To indexs.Count - 1
                    If Not (String.IsNullOrEmpty(indexs(i).Data.Trim) Or (indexs(i).[Operator].Trim().ToLower() = "es nulo")) Then

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
                                raiseerror(ex)
                                sbName.Append(indexs(i).Data.Trim)
                            End Try
                        Else
                            sbName.Append(indexs(i).Data.Trim)
                        End If

                        If indexs(i).[Operator].Trim = "Entre" Then
                            sbName.Append(" <= " & indexs(i).Data2.Trim)
                        End If
                        sbName.Append(", ")
                    ElseIf indexs(i).[Operator].Trim().ToLower() = "es nulo" Then
                        sbName.Append(indexs(i).Name.Trim)
                        sbName.Append(" ")
                        sbName.Append(indexs(i).[Operator].Trim)
                        sbName.Append(" ,")
                    End If


                Next
                sbName.Remove(sbName.Length - 2, 2)
                If Not String.IsNullOrEmpty(search.Textsearch) Then
                    sbName.Append(", " & search.Textsearch)
                End If
            ElseIf Not String.IsNullOrEmpty(search.Textsearch) Then
                sbName.Append(": ")
                sbName.Append(search.Textsearch)
            Else
                sbName.Append(" sin filtrar")
            End If

            totalength = sbName.Length + docTypesLength

            ' El "- 2" es por la coma que se le agrega.
            If totalength > acceptedLength - 2 Then
                Dim lengthDif = totalength - (acceptedLength - 2)
                Dim eachDocTypeLength = Math.Ceiling(lengthDif / If((docTypesNames.Count > 2), 2, docTypesNames.Count))
                Dim shortLength As Int32 = 0
                Dim almosOneWithLengthEight As Boolean = False

                For doctype As Int32 = 0 To docTypesNames.Count - 1
                    If doctype <> 2 Then
                        shortLength = (docTypesNames(doctype).Length - eachDocTypeLength)
                        If shortLength < minNameLegth OrElse almosOneWithLengthEight Then
                            docTypesNames(doctype) = ReduceLength(docTypesNames(doctype), minNameLegth)
                            almosOneWithLengthEight = True
                        Else
                            docTypesNames(doctype) = ReduceLength(docTypesNames(doctype), shortLength)
                        End If
                    End If
                Next
            End If
            If Not docTypesNames Is Nothing Then
                For docType As Integer = 0 To docTypesNames.Count - 1
                    If docType = 1 Then name &= ", "
                    name &= docTypesNames(docType)
                Next
                name = name.Trim & " " & sbName.ToString.Trim
            End If
            Return name
        Catch ex As Exception
            name = Now.ToString("dd-MM-yy hh.mm")
        Finally
            sbName.Clear()
            sbName = Nothing
            docTypes = Nothing
            indexs = Nothing
            docTypesNames = Nothing
        End Try
    End Function

    Public Overrides Sub Dispose()
    End Sub
End Class