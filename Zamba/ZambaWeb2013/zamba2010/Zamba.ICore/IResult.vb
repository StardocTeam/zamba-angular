Public Interface IResult
    Inherits ISchedulable, IImageResult, IPrintable, IZBaseCore, IBaseImageFileResult

    Property DocType() As IDocType
    Property DocTypeId() As Int64
    Property Html() As String
    Property HtmlFile() As String
    Property FlagIndexEdited() As Boolean

    Property FolderId() As Int64
    ReadOnly Property IsMsg() As Boolean
    ReadOnly Property ISVIRTUAL() As Boolean
    ReadOnly Property IsOffice() As Boolean
    ReadOnly Property IsOpenOffice() As Boolean
    ReadOnly Property IsExcel() As Boolean
    ReadOnly Property IsWord() As Boolean
    ReadOnly Property IsTif() As Boolean
    ReadOnly Property IsPowerpoint() As Boolean
    ReadOnly Property IsMAG() As Boolean
    ReadOnly Property IsRTF() As Boolean
    ReadOnly Property IsPDF() As Boolean
    ReadOnly  Property IsHTML As Boolean
    ReadOnly Property IsEditable() As Boolean
    ReadOnly Property FileType() As FileType
    Property EncodedFile() As Byte()
    '[Ezequiel 12/03/09]
    Property OwnerID() As Int64
    ''' <summary>
    ''' Contiene una coleccion de objetos oLinked asociados a la instancia actual
    ''' </summary>
    ''' <remarks>
    ''' Cada objeto oLinked contiene un objeto DOCTYPE y una arraylist de DOC_IDS de cada documento puntual
    ''' </remarks>
    ''' <seealso>DOCTYPE</seealso>
    Property LinkResults() As Generic.List(Of IResult)
    ReadOnly Property Dates(ByVal DocumentDate As DocumentDates) As Date
    ReadOnly Property GetIndexById(ByVal Id As Int64) As IIndex
    ReadOnly Property IsText() As Boolean
    ReadOnly Property IsXoml() As Boolean
    Property Index() As Int64
    Property File_Format_ID() As Int32
    Property Format() As String
    Property Platter_Id() As Int32
    Property Thumbnails() As Int32
    Property Object_Type_Id() As Int32
    Property AutoName() As String
    Property DocumentalId() As Int32
    Property CurrentFormID() As Int64
      Property PreviusFormID() As Int64
    Property IsShared() As Boolean
    Property IsImportant() As Boolean
    Property IsFavorite() As Boolean

    Property TempId() As Integer
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Establece a Nothing la propiedad Picture del objeto Result
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub DisposePicture()
    Property ChildsResults() As Generic.List(Of IResult)

    Property IsEncrypted As Boolean
    Property EncryptPassword As String
    Property ParentVerId As Long
    Property VersionNumber As Integer
   readonly Property IsOffice2  As Boolean
    Property HasVersion As Integer
    Property RootDocumentId As Long
    Property barcodeInBase64 As String

    Enum DocumentDates
        Entrada
        Salida
        Creacion
        Edicion
    End Enum

End Interface
