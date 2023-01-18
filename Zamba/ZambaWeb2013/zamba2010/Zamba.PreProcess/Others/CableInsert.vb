Imports ZAMBA.AppBlock
Imports ZAMBA.Servers
Imports ZAMBA.Core
Imports Zamba.Data
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Cable"), Ipreprocess.PreProcessHelp("Inserta el documento monitoreado. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo y un parámetro opcional (UPPER,LOWER) que especifica si los datos se insertan en mayúscula o minúscula")> _
Public Class ippCableINSERT
    Inherits ZClass
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Inserta el documento monitoreado. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo y un parámetro opcional (UPPER,LOWER) que especifica si los datos se insertan en mayúscula o minúscula"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Dim processed As Int32
    Dim Errors As Int32
    Dim Doctypename As String = "DocType"
    '    Dim docTypeId As Int32 = -1
    Dim DocType As DocType
    ' Dim UpperOrLower As String = ""
    Dim IndexName As String = "Index"


    'Private Sub GetParameters(ByVal param As String)
    Private Sub GetParameters(ByVal param As ArrayList)
        'Dim params As String() = param.Split(",")
        'Dim params() As String
        'params = param.ToArray
        ' Dim i As Integer
        Me.Doctypename = param(0)
        Me.IndexName = param(1)
        Try
            '  Me.UpperOrLower = param(2)
        Catch ex As Exception
            Trace.WriteLine("Error Obteniendo Parametros " & ex.ToString)
        End Try

    End Sub

    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        'Trace.WriteLine(FM.FullFilename)
        Trace.WriteLine(Files(0))
        'If Not IsNothing(FM.FullFilename) Then
        If Not IsNothing(Files(0)) Then
            'Dim fi As New IO.FileInfo(FM.FullFilename)
            Dim fi As New IO.FileInfo(Files(0))

            Try
                Me.GetParameters(param)
            Catch ex As Exception
                Trace.WriteLine("Error Obteniendo Parametros " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                Return Nothing
            End Try

            Try
                Me.DocType = DocTypesFactory.GetDocType(Me.Doctypename)
                If Me.DocType.ID = 0 Then
                    zamba.core.zclass.raiseerror(New Exception("No existe el tipo de documento " & Me.Doctypename))
                    Return Nothing
                End If
            Catch ex As Exception
                Trace.WriteLine("Error asignado tipo de documento " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                Return Nothing
            End Try


            Dim Document As NewResult
            Document = Results_Business.GetNewNewResult(Me.DocType)
            '  Document.DocTypeName = Me.Doctypename

            Dim Fin As IO.FileInfo
            ' Dim x As Int32
            Try
                Me.processed += 1

                Dim indexPosition As Int32 = -1

                Dim i As Integer
                'Limpio el objeto document y busco la pos del indice 
                For i = 0 To Document.Indexs.Count - 1
                    Document.Indexs(i).data = ""
                    If Document.Indexs(i).name.toupper = Me.IndexName.ToUpper Then
                        indexPosition = i
                    End If
                Next
                If indexPosition = -1 Then
                    Trace.WriteLine("El indice ingresado no corresponde al tipo de documento")
                    zamba.core.zclass.raiseerror(New Exception("El indice ingresado no corresponde al tipo de documento"))
                    Return Nothing
                End If

                'Document.File = FM.FullFilename
                Document.File = Files(0)

                'Fin = New IO.FileInfo(FM.FullFilename)
                Fin = New IO.FileInfo(Files(0))

                'TODO Falta decir cual es el la posicion del indice

                If Not IsNothing(param) Then
                    If param.IndexOf("UPPER") <> -1 Then
                        Document.Indexs(indexPosition).data = Fin.Name.Substring(0, fi.Name.LastIndexOf(".")).ToUpper
                        Document.Indexs(indexPosition).datatemp = Fin.Name.Substring(0, fi.Name.LastIndexOf(".")).ToUpper
                    Else
                        If param.IndexOf("LOWER") <> -1 Then
                            Document.Indexs(indexPosition).data = Fin.Name.Substring(0, fi.Name.LastIndexOf(".")).ToLower
                            Document.Indexs(indexPosition).datatemp = Fin.Name.Substring(0, fi.Name.LastIndexOf(".")).ToLower
                        Else
                            Document.Indexs(indexPosition).data = Fin.Name.Substring(0, fi.Name.LastIndexOf("."))
                            Document.Indexs(indexPosition).datatemp = Fin.Name.Substring(0, fi.Name.LastIndexOf("."))
                        End If
                    End If
                Else
                    Document.Indexs(indexPosition).data = Fin.Name.Substring(0, fi.Name.LastIndexOf("."))
                    Document.Indexs(indexPosition).datatemp = Fin.Name.Substring(0, fi.Name.LastIndexOf("."))
                End If
                Document.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID)
                'move, reindex, reemplazar,ShowCuestion
                'Results_Business.CableInsertDocument(Document, True, False, True, False)
                Results_Business.InsertDocumentCable(Document, indexPosition)

            Catch ex As Exception
                Trace.WriteLine("Error " & Errors & " " & ex.ToString)
                Errors += 1
                RaiseEvent PreprocessError(ex.ToString)
                Files = Nothing
            End Try
        End If
        Return Files
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return String.Empty
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
#Region "Validaciones"
    'Private Function ValidateFileName(ByVal filename As String) As Boolean
    '    ChequearTipoParte(filename.Substring(0, 2))
    'End Function




#End Region

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Insertar Cable"
        End Get
    End Property
    Public Overrides Sub Dispose()
    End Sub
End Class
