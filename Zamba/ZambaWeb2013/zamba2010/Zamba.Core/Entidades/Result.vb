Imports System.Collections.Generic
Imports System.IO
Imports Microsoft.Win32
Imports Zamba.Core

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
    Implements IResult





#Region " Atributos "
    Private m_CurrentFormID As Int64 = -1
    Private _doctypeid As Int64

    Private _Index As Int64
    Private _isopen As Boolean
    Private _File_Format_ID As Int32
    Private _Format As String
    Private _Platter_Id As Int32
    Private _Thumbnails As Int32
    Private _Object_Type_Id As Int32
    Private _AutoName As String
    'Private _DocumentalId As Int32
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
    Private _isVideo As Boolean

    Public UserID As Int32
    'Public Shadows OriginalName As String
    Public FlagDocumentEdited As Boolean
    Public _hasVersion As Integer
    Public _rootDocumentId As Int64
    Private _ParentVerId As Int64
    Private _VersionNumber As Integer

    Private _IsFavorite As Boolean
    Private _IsImportant As Boolean

    Enum DocumentDates
        Entrada
        Salida
        Creacion
        Edicion
    End Enum
#End Region

#Region " Propiedades "
    Public Property ParentVerId() As Int64 Implements IResult.ParentVerId
        Get
            Return _ParentVerId
        End Get
        Set(ByVal value As Int64)
            _ParentVerId = value
        End Set
    End Property

    Public Property VersionNumber() As Integer Implements IResult.VersionNumber
        Get
            Return _VersionNumber
        End Get
        Set(ByVal value As Integer)
            _VersionNumber = value
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
    Public ReadOnly Property IsMsg() As Boolean Implements IResult.IsMsg
        Get
            If IsNothing(FullPath) Then
                Return False
            End If
            Dim Fi As New FileInfo(FullPath)

            If String.Compare(Fi.Extension.ToUpper, ".MSG") = 0 Then
                _isMsg = True
            Else
                _isMsg = False
            End If

            Return _isMsg

        End Get

    End Property
    Public ReadOnly Property IsVideo() As Boolean Implements IResult.IsVideo
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".MP4") = 0 OrElse
                   String.Compare(Fi.Extension.ToUpper, ".WEBM") = 0 OrElse
                   String.Compare(Fi.Extension.ToUpper, ".OGG") Then
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
                DocTypeId = Value.ID
            End If
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
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
            If String.IsNullOrEmpty(File) AndAlso String.IsNullOrEmpty(FullPath) Then 'If (File Is Nothing OrElse Me.File = "") AndAlso (Me.FullPath Is Nothing OrElse Me.FullPath = "") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public ReadOnly Property IsOffice() As Boolean Implements IResult.IsOffice
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)

                'If Fi.Extension.ToUpper = ".DOC" OrElse Fi.Extension.ToUpper = ".XLS" OrElse Fi.Extension.ToUpper = ".PPT" OrElse Fi.Extension.ToUpper = ".VSD" OrElse Fi.Extension.ToUpper = ".POT" OrElse Fi.Extension.ToUpper = ".XLT" OrElse Fi.Extension.ToUpper = ".DOT" OrElse Fi.Extension.ToUpper = ".PDF" OrElse Fi.Extension.ToUpper = ".TXT" OrElse Fi.Extension.ToUpper = ".PPS" Then
                'MARCELO: quite que la extension .txt sea office ya que generaba problemas
                If String.Compare(Fi.Extension.ToUpper, ".DOC") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".DOCX") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".XLSX") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".XLS") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".PPT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".PPTX") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".VSD") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".POT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".XLT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".DOT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".DOTX") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".PDF") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".PPS") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".PPSX") = 0 Then
                    Return True
                    'MARCELO: comente esto ya que hace lo mismo que la linea de arriba
                    'Else
                    'If Fi.Extension.ToUpper = ".DOC" OrElse Fi.Extension.ToUpper = ".XLS" OrElse Fi.Extension.ToUpper = ".PPT" OrElse Fi.Extension.ToUpper = ".VSD" OrElse Fi.Extension.ToUpper = ".POT" OrElse Fi.Extension.ToUpper = ".XLT" OrElse Fi.Extension.ToUpper = ".DOT" OrElse Fi.Extension.ToUpper = ".PDF" OrElse Fi.Extension.ToUpper = ".TXT" OrElse Fi.Extension.ToUpper = ".PPS" Then
                    'Return True
                Else
                    Return False
                End If
                'End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    Public ReadOnly Property IsOpenOffice() As Boolean Implements IResult.IsOpenOffice
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)

                If String.Compare(Fi.Extension.ToUpper, ".ODT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".ODS") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".ODP") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".ODG") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".ODF") = 0 Then
                    Return True
                Else
                    Return False
                End If
                'End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    Public ReadOnly Property IsOffice2() As Boolean Implements IResult.IsOffice2
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If

                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".DOC") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".XLS") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".PPT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".POT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".DOT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".XLT") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".RTF") = 0 Then
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
    Public ReadOnly Property IsPowerpoint() As Boolean Implements IResult.IsPowerpoint
        Get
            If Me.ISVIRTUAL Then
                Return False
            End If
            Try
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".PPT") = 0 OrElse
                String.Compare(Fi.Extension.ToUpper, ".PPTX") = 0 OrElse
                String.Compare(Fi.Extension.ToUpper, ".POT") = 0 Then
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
    Public ReadOnly Property IsXPS() As Boolean Implements IResult.IsXPS
        Get
            If Me.ISVIRTUAL Then
                Return False
            End If
            Try
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".XPS") = 0 Then
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

    Public ReadOnly Property IsPDF() As Boolean Implements IResult.IsPDF
        Get
            If Me.ISVIRTUAL Then
                Return False
            End If
            Try
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".PDF") = 0 Then
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
    Public ReadOnly Property IsExcel() As Boolean Implements IResult.IsExcel
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If

                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".XLS") = 0 OrElse
                String.Compare(Fi.Extension.ToUpper, ".XLSX") = 0 OrElse
                String.Compare(Fi.Extension.ToUpper, ".XLT") = 0 Then
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
    Public ReadOnly Property IsCSV() As Boolean Implements IResult.IsCSV
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If

                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".CSV") = 0 Then
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
    Public ReadOnly Property IsWord() As Boolean Implements IResult.IsWord
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".DOC") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".DOCX") = 0 Then
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
    Public ReadOnly Property IsHTML() As Boolean Implements IResult.IsHTML
        Get
            Try
                If Me.ISVIRTUAL Then
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

    Public ReadOnly Property IsRTF() As Boolean Implements IResult.IsRTF
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".RTF") = 0 Then
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
    Public ReadOnly Property IsTif() As Boolean Implements IResult.IsTif
        Get
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".TIF") = 0 OrElse _
                String.Compare(Fi.Extension.ToUpper, ".TIFF") = 0 Then
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
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                If Not IsNothing(FullPath) Then
                    Dim Fi As New FileInfo(FullPath)
                    If String.Compare(Fi.Extension.ToUpper, ".MAG") = 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                raiseerror(ex)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public ReadOnly Property Indice(ByVal Nombre As String) As String
        Get
            For Each I As IIndex In Me.Indexs
                If String.Compare(I.Name, Nombre, True) = 0 Then
                    If I.DropDown = IndexAdditionalType.LineText Then
                        Return I.Data
                        'Else
                        'Return I.Data & " - " & I.dataDescription
                    Else
                        'Agregado Diego porque no Obtenia valores para indices de Sustitucion
                        Return I.Data
                    End If
                End If
            Next
            Return String.Empty
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Property CurrentFormID() As Int64 Implements IResult.CurrentFormID
        Get
            Return m_CurrentFormID
        End Get
        Set(ByVal value As Int64)
            m_CurrentFormID = value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Property ModalFormID() As Int64 Implements IResult.ModalFormID

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

    Public Overridable Property Fecha_Fin() As Date Implements ISchedulable.Fecha_Fin
        Get
            If String.Compare(Me.ToString, "Zamba.Core.TaskResult") = 0 Then
                Return CType(Me, TaskResult).ExpireDate
            End If
        End Get
        Set(ByVal value As Date)
        End Set
    End Property
    Public Overridable Property Fecha_Inicio() As Date Implements ISchedulable.Fecha_Inicio
        Get
            If String.Compare(Me.ToString, "Zamba.Core.TaskResult") = 0 Then
                Return CType(Me, TaskResult).CheckIn
            End If
        End Get
        Set(ByVal value As Date)
        End Set
    End Property

    Public Overridable Property UserId1() As Int64 Implements ISchedulable.UserId
        Get
        End Get
        Set(ByVal value As Int64)
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
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Indices() As List(Of IIndex)
        Get
            If IsNothing(_indexs) Then CallForceLoad(Me)
            If IsNothing(_indexs) Then _indexs = New List(Of IIndex)
            Return _indexs
        End Get
    End Property
    Public ReadOnly Property GetIndexById(ByVal Id As Int64) As IIndex Implements IResult.GetIndexById
        Get
            For Each index As IIndex In Me.Indexs
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
            Try
                If Me.ISVIRTUAL Then
                    Return False
                End If
                Dim Fi As New FileInfo(FullPath)
                If String.Compare(Fi.Extension.ToUpper, ".TXT") = 0 Then
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

    Public ReadOnly Property IsXoml() As Boolean Implements IResult.IsXoml
        Get
            If Not IsNothing(File) Then
                If Me.File.ToUpper.EndsWith(".XOML") Then
                    Return True
                Else
                    Return False
                End If
            ElseIf Not IsNothing(FullPath) Then
                If Me.FullPath.ToUpper.EndsWith(".XOML") Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property
    Public ReadOnly Property IsImage() As Boolean Implements IPrintable.IsImage
        Get
            If Not IsNothing(File) Then
                If IsNothing(FullPath) = False Then
                    Try
                        If FullPath.ToUpper.EndsWith(".TIF") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".JPG") OrElse FullPath.ToUpper.EndsWith(".JPEG") OrElse FullPath.ToUpper.EndsWith(".TIFF") OrElse FullPath.ToUpper.EndsWith(".TIF") OrElse FullPath.ToUpper.EndsWith(".BMP") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".PCX") OrElse FullPath.ToUpper.EndsWith(".PNG") OrElse FullPath.ToUpper.EndsWith(".ICO") Then
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
                        If File.ToUpper.EndsWith(".TIF") OrElse File.ToUpper.EndsWith(".GIF") OrElse File.ToUpper.EndsWith(".JPG") OrElse File.ToUpper.EndsWith(".JPEG") OrElse File.ToUpper.EndsWith(".TIFF") OrElse File.ToUpper.EndsWith(".TIF") OrElse File.ToUpper.EndsWith(".BMP") OrElse File.ToUpper.EndsWith(".GIF") OrElse File.ToUpper.EndsWith(".PCX") OrElse File.ToUpper.EndsWith(".PNG") OrElse File.ToUpper.EndsWith(".ICO") Then
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
                    If FullPath.ToUpper.EndsWith(".TIF") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".JPG") OrElse FullPath.ToUpper.EndsWith(".JPEG") OrElse FullPath.ToUpper.EndsWith(".TIFF") OrElse FullPath.ToUpper.EndsWith(".TIF") OrElse FullPath.ToUpper.EndsWith(".BMP") OrElse FullPath.ToUpper.EndsWith(".GIF") OrElse FullPath.ToUpper.EndsWith(".PCX") OrElse FullPath.ToUpper.EndsWith(".PNG") OrElse FullPath.ToUpper.EndsWith(".ICO") Then
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
    Public Property Platter_Id() As Int32 Implements IResult.Platter_Id
        Get
            Return _Platter_Id
        End Get
        Set(ByVal Value As Int32)
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
    'Public Property DocumentalId() As Int32 Implements IResult.DocumentalId
    '    Get
    '        Return _DocumentalId
    '    End Get
    '    Set(ByVal Value As Int32)
    '        _DocumentalId = Value
    '    End Set
    'End Property
    Public Property PrintPicture() As IZPicture Implements IPrintable.PrintPicture
        Get
            Try
                If _PrintPicture Is Nothing OrElse _PrintPicture.Image Is Nothing Then
                    _PrintPicture = New ZPicture(Me.PrintName)
                End If
            Catch ex As Exception
                _PrintPicture = New ZPicture(Me.FullPath)
            End Try
            Return _PrintPicture
        End Get
        Set(ByVal Value As IZPicture)
            _PrintPicture = CType(Value, ZPicture)
        End Set
    End Property
    Public Property HasVersion() As Integer Implements IResult.HasVersion
        Get
            Return _hasVersion
        End Get
        Set(ByVal value As Integer)
            _hasVersion = value
        End Set
    End Property
    Public Property RootDocumentId() As Long Implements IResult.RootDocumentId
        Get
            Return _rootDocumentId
        End Get
        Set(ByVal value As Long)
            _rootDocumentId = value
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
                        _Picture = New ZPicture(Me.FullPath)
                    End If
                End If
            Catch ex As Exception
                _Picture = New ZPicture(Me.FullPath)
            End Try

            Return _Picture
        End Get
        Set(ByVal Value As IZPicture)
            _Picture = CType(Value, ZPicture)
        End Set
    End Property

    Public Property FolderId As Long Implements IResult.FolderId


    Public ReadOnly Property IsEditable As Boolean Implements IResult.IsEditable


    Public ReadOnly Property FileType As FileType Implements IResult.FileType


    Private Property IResult_EncodedFile As Byte() Implements IResult.EncodedFile


    Public Property DocumentalId As Integer Implements IResult.DocumentalId

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Property PreviusFormID As Long Implements IResult.PreviusFormID


    Public Property IsImportant As Boolean Implements IResult.IsImportant
        Get
           Return _IsImportant
        End Get
        Set(value As Boolean)
           _IsImportant=value
        End Set
    End Property

    Public Property IsFavorite As Boolean Implements IResult.IsFavorite
        Get
             Return _IsFavorite
        End Get
        Set(value As Boolean)
            _IsFavorite=Value
        End Set
    End Property

    Public Property TempId As Integer Implements IResult.TempId


    Public Property IsEncrypted As Boolean Implements IResult.IsEncrypted


    Public Property EncryptPassword As String Implements IResult.EncryptPassword

    Public Property barcodeInBase64 As String Implements IResult.barcodeInBase64



#End Region

#Region "Contructores"
    Public Sub New()

    End Sub

    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal FullPath As String, ByVal OffSet As Int32, ByVal DISK_VOL_PATH As String, ByVal Disk_Group_Id As Int32)
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
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
    Public Sub New(ByVal DocId As Int64, _
                   ByVal DocType As DocType, _
                   ByVal Name As String, _
                   ByVal IconId As Int32, _
                                    ByVal Index As Int64, _
                   ByVal CrDate As Date, _
                   ByVal EditDate As Date)
        Me.IconId = IconId
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
        Me.Name = Name
        Me.Index = Index
        Me.CreateDate = CrDate
        Me.EditDate = EditDate
    End Sub
    'este se usa para el new de las busquedas normales
    Public Sub New(ByVal DocId As Int64, _
                   ByVal DocType As DocType, _
                   ByVal Name As String, _
                   ByVal IconId As Int32, _
                            ByVal Index As Int64, _
                   ByVal Disk_Group_Id As Int32, _
                   ByVal Doc_File As String, _
                   ByVal OffSet As Int32, _
                   ByVal DISK_VOL_PATH As String, _
                   ByVal CrDate As Date, _
                   ByVal EditDate As Date)
        Me.IconId = IconId
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
        Me.Name = Name
        Me.Index = Index
        Me.Disk_Group_Id = Disk_Group_Id
        Me.Doc_File = Doc_File
        Me.OffSet = OffSet
        '  Me.DocTypeName = DocTypeName
        Me.CreateDate = CrDate
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

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DocId">DOCID</param>
    ''' <param name="DocType">Objeto Doc_Type</param>
    ''' <param name="Name">Nombre</param>
    ''' <param name="CrDate">Fecha de Creación</param>
    ''' <param name="EditDate">Fecha de Edición</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal DocId As Int64, _
               ByVal DocType As DocType, _
               ByVal Name As String, _
                       ByVal CrDate As Date, _
               ByVal EditDate As Date)
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
        Me.Name = Name
        Me.CreateDate = CrDate
        Me.EditDate = EditDate
    End Sub
    'este se usa para la busuqeda de documentos asociados, con el newsearch
    Public Sub New(ByVal DocId As Int64, _
                   ByVal DocType As DocType, _
                   ByVal Name As String, _
                   ByVal IconId As Int32, _
                             ByVal Index As Int64, _
                   ByVal Disk_Group_Id As Int32, _
                   ByVal Doc_File As String, _
                   ByVal OffSet As Int32, _
                   ByVal DISK_VOL_PATH As String, _
                   ByVal DocTypeName As String, _
                   ByVal crdate As Date, _
                   ByVal editdate As Date, _
                   ByVal Ver_Parent_id As Int64, _
                   ByVal version As Int32, _
                   ByVal RootId As Int64, _
                   ByVal original_Filename As String, _
                   ByVal NumeroVersion As Integer)

        Me.New(DocId, DocType, Name, IconId, Index, Disk_Group_Id, Doc_File, OffSet, DISK_VOL_PATH, crdate, editdate)

        Me.CreateDate = crdate
        Me.EditDate = editdate
        Me.ParentVerId = Ver_Parent_id
        Me._hasVersion = version
        Me.VersionNumber = NumeroVersion
        Me._rootDocumentId = RootId
        Me.OriginalName = original_Filename
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una instancia de Result en base a un archivo
    ''' </summary>
    ''' <param name="File">Archivo para el cual se creará el Result</param>
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
            Me.Name = arch.Name
            Me.CreateDate = Now
            Me.EditDate = Now
        End If
    End Sub

    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal IconId As Int32, ByVal Index As Int64, ByVal Disk_Group_Id As Int32, ByVal Doc_File As String, ByVal OffSet As Int32, ByVal DISK_VOL_PATH As String, ByVal Do_State As String, ByVal Do_State_Id As Int32, ByVal Step_Id As Int32)
        Me.IconId = IconId
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
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
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
        Me.IconId = IconId
        Me.Name = Name

        'Me.File_Format_ID = File_Format_ID
        'Me.Format = Format
        'Me.Platter_Id = Platter_Id
        'Me.Thumbnails = Thumbnails
        'Me.Object_Type_Id = Object_Type_Id
        'Me.AutoName = AutoName
        'Me.DocTypeName = DocTypeName
    End Sub

#Region "Export"
    Public Sub New(ByVal DocId As Int64, ByVal DocType As DocType, ByVal Name As String, ByVal IconId As Int32, ByVal dOC_fILE As String, ByVal Disk_Group_Id As Int32)
        Me.IconId = IconId
        Me.ID = DocId
        Me.Parent = DocType
        Me.DocTypeId = Me.Parent.ID
        Me.Name = Name

        Me.Disk_Group_Id = Disk_Group_Id
        Me.Doc_File = dOC_fILE
        '   Me.DocTypeName = DocTypeName
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando Result, Cantidad de Parametros 7, Export" _
                          & vbCrLf & "Asignando Path Volumen: " & DISK_VOL_PATH _
                          & vbCrLf & "Asignando Id Volumen: " & Disk_Group_Id)
    End Sub

#End Region

#End Region

#Region "Metodos Públicos"
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
        Me.Picture = Nothing
    End Sub

#End Region

#Region "Eventos"
    Public Shared Event UpdateLinkIndex(ByVal indexId As Int32, ByVal value As Object, ByVal DoctypeId As Int32)
#End Region

    '[Tomas]    23/10/2009  Useless code.
    'Public Class ResultView
    '    Public ReadOnly Property Nombre() As String
    '        Get
    '            Return "Hola"
    '        End Get
    '    End Property
    'End Class

    'salva el result con otro docfile
    'Public Sub Save(ByVal fullpath As String)
    '    Dim Fi As New IO.FileInfo(fullpath)
    '    fi.CopyTo(

    'End Sub

    'Public Sub Reversion(ByVal NewFilename As String, ByVal OffInfo As OfficeDocumentInfo)
    '    ' Dim s = Me.Doc_File
    'End Sub



End Class
