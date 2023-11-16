Imports System.Collections.Generic
Imports System.IO

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.Result
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Objeto result que contiene todas las propiedades de un documento
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	10/10/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> Public Class Result
    Inherits BaseImageFileResult
    'Implements ISchedulable, IImageResult, IPrintable, IIndexable
    Implements IResult, IDisposable

#Region " Atributos "
    Private m_CurrentFormID As Int64 = -1
    Private _doctypeid As Int64
    Private _folderid As Int64
    Private _Index As Int64
    Private _isopen As Boolean
    Private _File_Format_ID As Int32
    Private _Format As String
    Private _Platter_Id As Int32
    Private _Thumbnails As Int32
    Private _Object_Type_Id As Int32
    Private _AutoName As String
    Private _PrintPicture As IZPicture = Nothing
    Private _indexs As List(Of IIndex) = Nothing
    Private _IsImage As Boolean
    Private _linkresults As List(Of IResult) = Nothing
    Private _ChildsResults As List(Of IResult) = Nothing
    '[Ezequiel 12/03/09]
    Private _ownerID As Int64
    Private _html As String
    Private _htmlfile As String
    Private _isShared As Boolean
    Private _isMsg As Boolean
    Private _encodedFile As Byte()

    Private _isEncrypted As Boolean
    Private _encryptPassword As String

    Public UserID As Int32
    'Public Shadows OriginalName As String
    Public FlagDocumentEdited As Boolean
    Public ParentVerId As Int64
    Public HasVersion As Integer
    Public VersionNumber As Integer
    Public RootDocumentId As Int64
    Private _isFavorite As Boolean
    Private _isImportant As Boolean
    Private _tempId As Integer

    Enum DocumentDates
        Entrada
        Salida
        Creacion
        Edicion
    End Enum
#End Region

#Region " Propiedades "

    Public Property EncodedFile() As Byte() Implements IResult.EncodedFile
        Get
            Return _encodedFile
        End Get
        Set(ByVal value As Byte())
            _encodedFile = value
        End Set
    End Property
    Private _FlagIndexEdited As Boolean
    Public Property FlagIndexEdited() As Boolean Implements IResult.FlagIndexEdited
        Get
            Return _FlagIndexEdited
        End Get
        Set(ByVal value As Boolean)
            _FlagIndexEdited = value
        End Set
    End Property

    Public Property ChildsResults() As List(Of IResult) Implements IResult.ChildsResults
        Get
            Return _ChildsResults
        End Get
        Set(ByVal value As List(Of IResult))
            _ChildsResults = value
        End Set
    End Property

    ''' <summary>
    ''' Valida si el archivo es de outlook
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsMsg() As Boolean Implements IResult.IsMsg
        Get
            If IsNothing(FullPath) Then
                Return False
            End If
            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".msg") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".MSG") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    Public Property isShared() As Boolean Implements IResult.IsShared
        Get
            Return _isShared
        End Get
        Set(ByVal value As Boolean)
            _isShared = value
        End Set
    End Property
    Public Property Html() As String Implements IResult.Html
        Get
            Return _html
        End Get
        Set(ByVal value As String)
            _html = value
        End Set
    End Property
    Public Property HtmlFile() As String Implements IResult.HtmlFile
        Get
            Return _htmlfile
        End Get
        Set(ByVal value As String)
            _htmlfile = value
        End Set
    End Property
    Public Property OwnerID() As Int64 Implements IResult.OwnerID
        Get
            Return _ownerID
        End Get
        Set(ByVal value As Int64)
            _ownerID = value
        End Set
    End Property
    Public Property DocType() As IDocType Implements IResult.DocType
        Get
            Return CType(Parent, IDocType)
        End Get
        Set(ByVal Value As IDocType)
            Parent = Value
            If Value IsNot Nothing Then
                _doctypeid = Value.ID
            End If
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property DocTypeId() As Int64 Implements IResult.DocTypeId
        Get
            If _doctypeid = 0 Then
                If Not IsNothing(DocType) Then
                    Return DocType.ID
                Else
                    Return 0
                End If
            Else
                Return _doctypeid
            End If
        End Get
        Set(ByVal value As Int64)
            _doctypeid = value
        End Set
    End Property
    Public ReadOnly Property ISVIRTUAL() As Boolean Implements IResult.ISVIRTUAL
        Get

            If String.IsNullOrEmpty(File) AndAlso (String.IsNullOrEmpty(FileName) OrElse Not HasExtension(FileName)) AndAlso (FullPath Is Nothing OrElse FullPath = "" OrElse Not HasExtension(FullPath)) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Function HasExtension(doc_File As String) As Boolean
        If String.IsNullOrEmpty(doc_File) OrElse doc_File.Length = 0 Then
            Return False
        End If
        Dim fileInfo As FileInfo = New FileInfo(doc_File)
        If Not fileInfo Is Nothing AndAlso fileInfo.Extension IsNot Nothing AndAlso fileInfo.Extension.Length > 0 Then
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' True si el documento puede ser editado a nivel extensi�n del archivo.
    ''' </summary>
    Public ReadOnly Property IsEditable() As Boolean Implements IResult.IsEditable
        Get
            Try
                If ISVIRTUAL OrElse IsPDF OrElse IsMsg OrElse IsImage OrElse IsMAG OrElse IsText Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsOffice() As Boolean Implements IResult.IsOffice
        Get
            Try
                If ISVIRTUAL Then
                    Return False
                End If
                Try
                    If String.IsNullOrEmpty(FullPath) = False Then
                        If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                            If FullPath.ToLower().EndsWith(".doc") OrElse
                                FullPath.ToLower().EndsWith(".docx") OrElse
                                FullPath.ToLower().EndsWith(".xls") OrElse
                                FullPath.ToLower().EndsWith(".xlsx") OrElse
                                FullPath.ToLower().EndsWith(".ppt") OrElse
                                FullPath.ToLower().EndsWith(".pptx") OrElse
                                FullPath.ToLower().EndsWith(".vsd") OrElse
                                FullPath.ToLower().EndsWith(".pot") OrElse
                                FullPath.ToLower().EndsWith(".xlt") OrElse
                                FullPath.ToLower().EndsWith(".dot") OrElse
                                FullPath.ToLower().EndsWith(".dotx") OrElse
                                FullPath.ToLower().EndsWith(".pdf") OrElse
                                FullPath.ToLower().EndsWith(".pps") OrElse
                                FullPath.ToLower().EndsWith(".ppsx") Then
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Dim Fi As New FileInfo(FullPath)
                            If String.Compare(Fi.Extension.ToUpper, ".DOC") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".DOCX") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".XLSX") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".XLS") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".PPT") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".PPTX") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".VSD") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".POT") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".XLT") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".DOT") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".DOTX") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".PDF") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".PPS") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".PPSX") = 0 Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                Catch ex As Exception
                    raiseerror(ex)
                    Return False
                End Try
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsOpenOffice() As Boolean Implements IResult.IsOpenOffice
        Get
            Try
                If ISVIRTUAL Then
                    Return False
                End If

                Try
                    If String.IsNullOrEmpty(FullPath) = False Then
                        If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                            If FullPath.ToLower().EndsWith(".odt") OrElse
                                FullPath.ToLower().EndsWith(".ods") OrElse
                                FullPath.ToLower().EndsWith(".odp") OrElse
                                FullPath.ToLower().EndsWith(".odg") OrElse
                                FullPath.ToLower().EndsWith(".odf") Then
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Dim Fi As New FileInfo(FullPath)
                            If String.Compare(Fi.Extension.ToUpper, ".ODT") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".ODS") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".ODP") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".ODG") = 0 OrElse
                                String.Compare(Fi.Extension.ToUpper, ".ODF") = 0 Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                Catch ex As Exception
                    raiseerror(ex)
                    Return False
                End Try
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsPowerpoint() As Boolean Implements IResult.IsPowerpoint
        Get
            If ISVIRTUAL Then
                Return False
            End If
            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".ppt") OrElse
                            FullPath.ToLower().EndsWith(".pptx") OrElse
                            FullPath.ToLower().EndsWith(".pot") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".PPT") = 0 OrElse
                        String.Compare(Fi.Extension.ToUpper, ".PPTX") = 0 OrElse
                        String.Compare(Fi.Extension.ToUpper, ".POT") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsPDF() As Boolean Implements IResult.IsPDF
        Get
            If ISVIRTUAL Then
                Return False
            End If
            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".pdf") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".PDF") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsExcel() As Boolean Implements IResult.IsExcel
        Get
            If ISVIRTUAL Then
                Return False
            End If

            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".xls") OrElse
                            FullPath.ToLower().EndsWith(".xlsx") OrElse
                            FullPath.ToLower().EndsWith(".xlt") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".XLS") = 0 OrElse
                        String.Compare(Fi.Extension.ToUpper, ".XLSX") = 0 OrElse
                        String.Compare(Fi.Extension.ToUpper, ".XLT") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsWord() As Boolean Implements IResult.IsWord
        Get
            If ISVIRTUAL Then
                Return False
            End If

            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".doc") OrElse
                            FullPath.ToLower().EndsWith(".docx") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".DOC") = 0 OrElse
                        String.Compare(Fi.Extension.ToUpper, ".DOCX") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    Public ReadOnly Property FileType() As FileType Implements IResult.FileType
        Get
            If IsExcel Then
                Return Core.FileType.EXCEL
            ElseIf IsImage Then
                Return Core.FileType.IMAGE
            ElseIf IsMAG Then
                Return Core.FileType.MAG
            ElseIf IsMsg Then
                Return Core.FileType.MSG
            ElseIf IsPDF Then
                Return Core.FileType.PDF
            ElseIf IsPowerpoint Then
                Return Core.FileType.POWERPOINT
            ElseIf IsText Then
                Return Core.FileType.TXT
            ElseIf IsTif Then
                Return Core.FileType.TIF
            ElseIf IsWord Then
                Return Core.FileType.WORD
            ElseIf IsXoml Then
                Return Core.FileType.XOML
            End If
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsTif() As Boolean Implements IResult.IsTif
        Get
            If ISVIRTUAL Then
                Return False
            End If

            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".tif") OrElse
                            FullPath.ToLower().EndsWith(".tiff") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".TIF") = 0 OrElse
                        String.Compare(Fi.Extension.ToUpper, ".TIFF") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    Public ReadOnly Property Dates(ByVal DocumentDate As IResult.DocumentDates) As Date Implements IResult.Dates
        Get
            Select Case DocumentDate
                Case IResult.DocumentDates.Creacion ' DocumentDates.Creacion
                    Return CreateDate
                Case IResult.DocumentDates.Edicion '  DocumentDates.Edicion
                    Return EditDate
            End Select
        End Get
    End Property
    Public ReadOnly Property IsMAG() As Boolean Implements IResult.IsMAG
        Get
            If ISVIRTUAL Then
                Return False
            End If

            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".mag") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".MAG") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsHtml() As Boolean Implements IResult.IsHtml
        Get
            Try
                If ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".HTML") = 0 OrElse
                String.Compare(Fi.Extension.ToUpper, ".HTM") = 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsRTF() As Boolean Implements IResult.IsRTF
        Get
            If ISVIRTUAL Then
                Return False
            End If

            If String.IsNullOrEmpty(FullPath) = False Then
                If String.Compare(Path.GetExtension(FullPath), ".rtf") = 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Nombre"})>
    Public ReadOnly Property Indice(ByVal Nombre As String) As String
        Get
            For Each I As IIndex In Indexs
                If String.Compare(I.Name, Nombre, True) = 0 Then
                    If I.DropDown = IndexAdditionalType.LineText Then
                        Return I.Data
                        'Else
                        'Return I.Data & " - " & I.dataDescription
                    Else
                        'Agregado Diego porque no Obtenia valores para atributos de Sustitucion
                        Return I.Data
                    End If
                End If
            Next
            Return String.Empty
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property CurrentFormID() As Int64 Implements IResult.CurrentFormID
        Get
            Return m_CurrentFormID
        End Get
        Set(ByVal value As Int64)
            m_CurrentFormID = value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property PreviusFormID() As Int64 Implements IResult.PreviusFormID
    Public Property IsOpen() As Boolean Implements IPrintable.IsOpen
        Get
            Return _isopen
        End Get
        Set(ByVal Value As Boolean)
            _isopen = Value
        End Set
    End Property
    ''' <summary>
    ''' Contiene una coleccion de objetos oLinked asociados a la instancia actual
    ''' </summary>
    ''' <remarks>
    ''' Cada objeto oLinked contiene un objeto DOCTYPE y una arraylist de DOC_IDS de cada documento puntual
    ''' </remarks>
    ''' <seealso>DOCTYPE</seealso>
    Public Property LinkResults() As List(Of IResult) Implements IResult.LinkResults
        Get
            If IsNothing(_linkresults) Then CallForceLoad(Me)
            If IsNothing(_linkresults) Then _linkresults = New List(Of IResult)()

            Return _linkresults
        End Get
        Set(ByVal value As List(Of IResult))
            _linkresults = value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Overridable Property Fecha_Fin() As Date Implements ISchedulable.Fecha_Fin
        Get
            If String.Compare(ToString, "Zamba.Core.TaskResult") = 0 Then
                Return CType(Me, TaskResult).ExpireDate
            End If
        End Get
        Set(ByVal value As Date)
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Overridable Property Fecha_Inicio() As Date Implements ISchedulable.Fecha_Inicio
        Get
            If String.Compare(ToString, "Zamba.Core.TaskResult") = 0 Then
                Return CType(Me, TaskResult).CheckIn
            End If
        End Get
        Set(ByVal value As Date)
        End Set
    End Property

    Public Overridable Property UserId1() As Integer Implements ISchedulable.UserId
        Get
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Public Property Indexs() As List(Of IIndex) Implements IIndexable.Indexs
        Get
            If IsNothing(_indexs) Then CallForceLoad(Me)
            If IsNothing(_indexs) Then _indexs = New List(Of IIndex)
            Return _indexs
        End Get
        Set(ByVal Value As List(Of IIndex))
            _indexs = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property Atributos() As List(Of IIndex)
        Get
            If IsNothing(_indexs) Then CallForceLoad(Me)
            If IsNothing(_indexs) Then _indexs = New List(Of IIndex)
            Return _indexs
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property Indices() As List(Of IIndex)
        Get
            If IsNothing(_indexs) Then CallForceLoad(Me)
            If IsNothing(_indexs) Then _indexs = New List(Of IIndex)
            Return _indexs
        End Get
    End Property
    Public ReadOnly Property GetIndexById(ByVal Id As Int32) As IIndex Implements IResult.GetIndexById
        Get
            For Each index As IIndex In Indexs
                If index.ID = Id Then
                    Return index
                    Exit For
                End If
            Next
            Return Nothing
        End Get
    End Property


    Public Property Index() As Int64 Implements IResult.Index
        Get
            Return _Index
        End Get
        Set(ByVal Value As Int64)
            _Index = Value
        End Set
    End Property
    'Public Property DocTypeId() As Int32
    '    Get
    '        Return _doctypeid
    '    End Get
    '    Set(ByVal Value As Int32)
    '        _doctypeid = Value
    '    End Set
    'End Property
    Public ReadOnly Property IsText() As Boolean Implements IResult.IsText
        Get
            If ISVIRTUAL Then
                Return False
            End If

            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".txt") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".TXT") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    Public ReadOnly Property IsXoml() As Boolean Implements IResult.IsXoml
        Get
            If ISVIRTUAL Then
                Return False
            End If

            Try
                If String.IsNullOrEmpty(FullPath) = False Then
                    If FullPath(FullPath.Length - 4) = "." OrElse FullPath(FullPath.Length - 5) = "." Then
                        If FullPath.ToLower().EndsWith(".xoml") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Dim Fi As New FileInfo(FullPath)
                        If String.Compare(Fi.Extension.ToUpper, ".XOML") = 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsImage() As Boolean Implements IPrintable.IsImage
        Get
            If Not IsNothing(File) Then
                If IsNothing(FullPath) = False Then
                    Try
                        If FullPath.ToUpper.EndsWith(".TIF") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".JPG") OrElse FullPath.ToUpper.EndsWith(".JPEG") OrElse FullPath.ToUpper.EndsWith(".TIFF") OrElse FullPath.ToUpper.EndsWith(".BMP") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".PCX") OrElse FullPath.ToUpper.EndsWith(".PCX") OrElse FullPath.ToUpper.EndsWith(".PNG") Then
                            Return True
                        Else
                            Return False
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                        Return False
                    End Try
                Else
                    Try
                        'todo: ver sacar la lista o que sea configurable
                        'todo: que se registren con el viewer de zamba
                        If File.ToUpper.EndsWith(".TIF") OrElse File.ToUpper.EndsWith(".GIF") OrElse File.ToUpper.EndsWith(".JPG") OrElse File.ToUpper.EndsWith(".TIFF") OrElse File.ToUpper.EndsWith(".BMP") OrElse File.ToUpper.EndsWith(".GIF") OrElse File.ToUpper.EndsWith(".PCX") OrElse File.ToUpper.EndsWith(".PNG") Then
                            Return True
                            'Filter
                            'Archivos de imagen  (*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF;*.PCX)|*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF;*.PCX
                        Else
                            Return False
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                        Return False
                    End Try
                End If
            ElseIf Not IsNothing(FullPath) Then
                Try
                    If FullPath.ToUpper.EndsWith(".TIF") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".JPG") OrElse FullPath.ToUpper.EndsWith(".TIFF") OrElse FullPath.ToUpper.EndsWith(".BMP") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".PCX") OrElse FullPath.ToUpper.EndsWith(".PNG") Then
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    raiseerror(ex)
                    Return False
                End Try
            End If
            Return False
        End Get
    End Property
    Public Property File_Format_ID() As Int32 Implements IResult.File_Format_ID
        Get
            Return _File_Format_ID
        End Get
        Set(ByVal Value As Int32)
            _File_Format_ID = Value
        End Set
    End Property
    Public Property Format() As String Implements IResult.Format
        Get
            Return _Format
        End Get
        Set(ByVal Value As String)
            _Format = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property UsuarioCreadorId() As Int64
        Get
            Return _Platter_Id
        End Get
        Set(ByVal Value As Int64)
            _Platter_Id = Value
        End Set
    End Property

    Public Property Platter_Id() As Int64 Implements IResult.Platter_Id
        Get
            Return _Platter_Id
        End Get
        Set(ByVal Value As Int64)
            _Platter_Id = Value
        End Set
    End Property

    Public Property Thumbnails() As Int32 Implements IResult.Thumbnails
        Get
            Return _Thumbnails
        End Get
        Set(ByVal Value As Int32)
            _Thumbnails = Value
        End Set
    End Property
    Public Property Object_Type_Id() As Int32 Implements IResult.Object_Type_Id
        Get
            Return _Object_Type_Id
        End Get
        Set(ByVal Value As Int32)
            _Object_Type_Id = Value
        End Set
    End Property
    Public Property AutoName() As String Implements IResult.AutoName
        Get
            Return _AutoName
        End Get
        Set(ByVal Value As String)
            _AutoName = Value
        End Set
    End Property

    Public Property PrintPicture() As IZPicture Implements IPrintable.PrintPicture
        Get
            Try
                If _PrintPicture Is Nothing OrElse _PrintPicture.Image Is Nothing Then
                    _PrintPicture = New ZPicture(PrintName)
                End If
            Catch ex As Exception
                _PrintPicture = New ZPicture(FullPath)
            End Try
            Return _PrintPicture
        End Get
        Set(ByVal Value As IZPicture)
            _PrintPicture = CType(Value, ZPicture)
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property IsImportant() As Boolean Implements IResult.IsImportant
        Get
            Return _isImportant
        End Get
        Set(ByVal Value As Boolean)
            _isImportant = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property IsFavorite() As Boolean Implements IResult.IsFavorite
        Get
            Return _isFavorite
        End Get
        Set(ByVal Value As Boolean)
            _isFavorite = Value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property IsEncrypted As Boolean Implements IResult.IsEncrypted
        Get
            Return _isEncrypted
        End Get
        Set(ByVal value As Boolean)
            _isEncrypted = value
        End Set
    End Property

    Public Property EncyptPassword As String Implements IResult.EncryptPassword
        Get
            Return _encryptPassword
        End Get
        Set(ByVal value As String)
            _encryptPassword = value
        End Set
    End Property
#End Region

#Region "Picture"
    Private _Picture As ZPicture = Nothing
    Public Property Picture() As IZPicture Implements IImageResult.Picture
        Get
            Try
                If IsImage = True Then
                    If _Picture Is Nothing OrElse _Picture.Image Is Nothing Then
                        _Picture = New ZPicture(FullPath)
                    End If
                End If
            Catch ex As Exception
                _Picture = New ZPicture(FullPath)
            End Try

            Return _Picture
        End Get
        Set(ByVal Value As IZPicture)
            If Value Is Nothing AndAlso _Picture IsNot Nothing Then
                If _Picture.Image IsNot Nothing Then
                    _Picture.Image.Dispose()
                    _Picture.Image = Nothing
                End If
                _Picture = Nothing
            Else
                _Picture = CType(Value, ZPicture)
            End If

        End Set
    End Property

    Public Property TempId As Integer Implements IResult.TempId
        Get
            Return _tempId
        End Get
        Set(value As Integer)
            _tempId = value
        End Set
    End Property
#End Region

#Region "Contructores"
    Public Sub New()
    End Sub
    Public Property FolderId() As Int64 Implements IResult.FolderId
        Get
            Return _folderid
        End Get
        Set(ByVal Value As Int64)
            _folderid = Value
        End Set
    End Property

    Public Property barcodeInBase64 As String Implements IResult.barcodeInBase64

    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal Doc_File As String, ByVal OffSet As Int32, ByVal DISK_VOL_PATH As String, ByVal Disk_Group_Id As Int32)
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.Name = Name
        ' Me.DocTypeName = DocTypeName
        Me.Doc_File = Doc_File
        Me.OffSet = OffSet
        Me.DISK_VOL_PATH = DISK_VOL_PATH
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando Result, Cantidad de Parametros 7" _
                          & vbCrLf & "Asignando Path Volumen: " & DISK_VOL_PATH _
                          & vbCrLf & "Asignando Id Volumen: " & Disk_Group_Id)
        Me.Disk_Group_Id = Disk_Group_Id
    End Sub

    Public Sub New(ByVal DocId As Int64,
                   ByVal DocType As DocType,
                   ByVal Name As String,
                   ByVal IconId As Int32,
         ByVal Index As Int64,
                   ByVal CrDate As Date,
                   ByVal EditDate As Date)
        Me.IconId = IconId
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.Name = Name

        Me.Index = Index

        CreateDate = CrDate
        Me.EditDate = EditDate
    End Sub

    'este se usa para el new de las busquedas normales
    Public Sub New(ByVal DocId As Int64,
                   ByVal DocType As DocType,
                   ByVal Name As String,
                   ByVal IconId As Int32,
                   ByVal Index As Int64,
                   ByVal Disk_Group_Id As Int32,
                   ByVal Doc_File As String,
                   ByVal OffSet As Int32,
                   ByVal DISK_VOL_PATH As String,
                   ByVal CrDate As Date,
                   ByVal EditDate As Date)
        Me.IconId = IconId
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.Name = Name

        Me.Index = Index
        Me.Disk_Group_Id = Disk_Group_Id
        Me.Doc_File = Doc_File
        Me.OffSet = OffSet
        '  Me.DocTypeName = DocTypeName
        CreateDate = CrDate
        Me.EditDate = EditDate
        'Me.File_Format_ID = File_Format_ID
        'Me.Format = Format
        'Me.Platter_Id = Platter_Id
        'Me.Thumbnails = Thumbnails
        'Me.Object_Type_Id = Object_Type_Id
        'Me.AutoName = AutoName
        Me.DISK_VOL_PATH = DISK_VOL_PATH
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando Result, Cantidad de Parametros 12" _
                          & vbCrLf & "Asignando Path Volumen: " & DISK_VOL_PATH _
                          & vbCrLf & "Asignando Id Volumen: " & Disk_Group_Id)
    End Sub


    Public Sub New(ByVal DocId As Int64,
               ByVal DocType As DocType,
               ByVal Name As String,
       ByVal CrDate As Date,
               ByVal EditDate As Date)
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.Name = Name

        CreateDate = CrDate
        Me.EditDate = EditDate
    End Sub

    'este se usa para la busuqeda de documentos asociados, con el newsearch
    Public Sub New(ByVal DocId As Int64,
                   ByVal DocType As DocType,
                   ByVal Name As String,
                   ByVal IconId As Int32,
         ByVal Index As Int64,
                   ByVal Disk_Group_Id As Int32,
                   ByVal Doc_File As String,
                   ByVal OffSet As Int32,
                   ByVal DISK_VOL_PATH As String,
                   ByVal DocTypeName As String,
                   ByVal crdate As Date,
                   ByVal editdate As Date,
                   ByVal Ver_Parent_id As Int64,
                   ByVal version As Int32,
                   ByVal RootId As Int64,
                   ByVal original_Filename As String,
                   ByVal NumeroVersion As Integer)

        Me.New(DocId, DocType, Name, IconId, Index, Disk_Group_Id, Doc_File, OffSet, DISK_VOL_PATH, crdate, editdate)

        CreateDate = crdate
        Me.EditDate = editdate
        ParentVerId = Ver_Parent_id
        HasVersion = version
        VersionNumber = NumeroVersion
        RootDocumentId = RootId
        OriginalName = original_Filename
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una instancia de Result en base a un archivo
    ''' </summary>
    ''' <param name="File">Archivo para el cual se crear� el Result</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal File As String)
        Me.File = File
        If IsNothing(File) OrElse String.IsNullOrEmpty(File) Then
        Else
            Dim arch As New FileInfo(File)
            Name = arch.Name
            CreateDate = Now
            EditDate = Now
        End If
    End Sub

    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal IconId As Int32, ByVal Index As Int64, ByVal Disk_Group_Id As Int32, ByVal Doc_File As String, ByVal OffSet As Int32, ByVal DISK_VOL_PATH As String, ByVal Do_State As String, ByVal Do_State_Id As Int32, ByVal Step_Id As Int32)
        Me.IconId = IconId
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.Name = Name

        Me.Index = Index
        Me.Disk_Group_Id = Disk_Group_Id
        Me.Doc_File = Doc_File
        Me.OffSet = OffSet
        '  Me.DocTypeName = DocTypeName

        'Me.File_Format_ID = File_Format_ID
        'Me.Format = Format
        'Me.Platter_Id = Platter_Id
        'Me.Thumbnails = Thumbnails
        'Me.Object_Type_Id = Object_Type_Id
        'Me.AutoName = AutoName
        Me.DISK_VOL_PATH = DISK_VOL_PATH
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando Result, Cantidad de Parametros 13" _
                          & vbCrLf & "Asignando Path Volumen: " & DISK_VOL_PATH _
                          & vbCrLf & "Asignando Id Volumen: " & Disk_Group_Id)
    End Sub


    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal IconId As Int32)
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.IconId = IconId
        Me.Name = Name

    End Sub


    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal IconId As Int32, ByVal dOC_fILE As String, ByVal Disk_Group_Id As Int32)
        Me.IconId = IconId
        ID = DocId
        Parent = DocType
        DocTypeId = Parent.ID
        Me.Name = Name

        Me.Disk_Group_Id = Disk_Group_Id
        Me.Doc_File = dOC_fILE
        '   Me.DocTypeName = DocTypeName
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando Result, Cantidad de Parametros 7, Export" _
                          & vbCrLf & "Asignando Path Volumen: " & DISK_VOL_PATH _
                          & vbCrLf & "Asignando Id Volumen: " & Disk_Group_Id)
    End Sub

    'este se usa para la busuqeda de documentos asociados, con el newsearch
    Public Sub New(ByVal DocId As Int64,
                   ByVal DocType As DocType,
                   ByVal Name As String,
                   ByVal IconId As Int32,
                   ByVal FolderId As Int64,
                   ByVal Index As Int64,
                   ByVal Disk_Group_Id As Int32,
                   ByVal Doc_File As String,
                   ByVal OffSet As Int32,
                   ByVal DISK_VOL_PATH As String,
                   ByVal DocTypeName As String,
                   ByVal crdate As Date,
                   ByVal editdate As Date,
                   ByVal Ver_Parent_id As Int64,
                   ByVal version As Int32,
                   ByVal RootId As Int64,
                   ByVal original_Filename As String,
                   ByVal NumeroVersion As Integer)

        Me.New(DocId, DocType, Name, IconId, FolderId, Index, Disk_Group_Id, Doc_File, OffSet, DISK_VOL_PATH, crdate.ToString(), editdate.ToString())

        'Me.New(DocId, DocTypeId, Name, IconId, FolderId, Index, Disk_Group_Id, Doc_File, OffSet, DISK_VOL_PATH)

        CreateDate = crdate
        Me.EditDate = editdate
        ParentVerId = Ver_Parent_id
        HasVersion = version
        VersionNumber = NumeroVersion
        RootDocumentId = RootId
        OriginalName = original_Filename
    End Sub

#End Region

#Region "Metodos P�blicos"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece a Nothing la propiedad Picture del objeto Result
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub DisposePicture() Implements IResult.DisposePicture
        Picture = Nothing
    End Sub

    Private _disposed As Boolean
    Public Overrides Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Para evitar que se haga dispose 2 veces
        If Not _disposed Then
            If disposing Then
                Dim i As Int16

                'Se comenta porque esta en algun lado por referencia
                'If Not IsNothing(DocType) Then
                '    DocType.Dispose()
                'End If

                If Not IsNothing(_indexs) Then
                    For i = 0 To _indexs.Count - 1
                        'DirectCast(_indexs(i), Index).Dispose()
                        _indexs(i) = Nothing
                    Next
                    _indexs.Clear()
                End If

                If Not IsNothing(_linkresults) Then
                    For i = 0 To _linkresults.Count - 1
                        _linkresults(i).Dispose()
                        _linkresults(i) = Nothing
                    Next
                    _linkresults.Clear()
                End If

                If Not IsNothing(_ChildsResults) Then
                    For i = 0 To _ChildsResults.Count - 1
                        _ChildsResults(i).Dispose()
                        _ChildsResults(i) = Nothing
                    Next
                    _ChildsResults.Clear()
                End If

                DisposePicture()
            End If

            ' Indicates that the instance has been disposed.
            _disposed = True

            _linkresults = Nothing
            _indexs = Nothing
            _ChildsResults = Nothing
            'DocType = Nothing

            MyBase.Dispose()
        End If
    End Sub

    Public Function isDisposed() As Boolean
        Return _disposed
    End Function


#End Region



End Class