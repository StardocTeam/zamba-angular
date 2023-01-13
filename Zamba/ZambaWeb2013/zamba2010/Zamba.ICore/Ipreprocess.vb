''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Interface	 : Core.Ipreprocess
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Interfaz para preproceso 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan] 26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Interface Ipreprocess
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Files">Arraylist de archivos a procesar</param>
    ''' <param name="param">OPCIONAL: Arraylist de parametros</param>
    ''' <param name="xml">archivo XML</param>
    ''' <param name="Test">Si es True, realiza una prueba sobre una copia temporal del archivo. Se usa para previsualizar el resultado</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList

    Function GetXml(Optional ByVal xml As String = Nothing) As String
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Procesa un archivo
    ''' </summary>
    ''' <param name="File">Archivo a procesar</param>
    ''' <param name="param">Opcional, parametros para el preproceso</param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String
    Sub SetXml(Optional ByVal xml As String = Nothing)
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la ayuda del preproceso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Function GetHelp() As String

    Event PreprocessMessage(ByVal msg As String)
    Event PreprocessError(ByVal Errormsg As String)

    ''' CADA PREPROCESO DEBE TENER ESTE ATRIBUTO YA QUE ES USADO PARA VALIDAR PERMISOS
    <AttributeUsage(AttributeTargets.Class)> _
    Class PreProcessName
        Inherits Attribute

        Protected _name As String
        Public Property Name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal Value As String)
                Me._name = Value
            End Set
        End Property
        Public Sub New(ByVal Description As String)
            Me.Name = Description
        End Sub

    End Class

    '<AttributeUsage(AttributeTargets.Class)> _
    ' Class PreProcessDescription
    '    Inherits Attribute

    '    Protected _Description As String

    '    Public Property Description() As String
    '        Get
    '            Return _Description
    '        End Get
    '        Set(ByVal Value As String)
    '            _Description = Value
    '        End Set
    '    End Property
    '    Public Sub New(ByVal descriptionString As String)
    '        Description = descriptionString
    '    End Sub
    'End Class

    <AttributeUsage(AttributeTargets.Class)> _
     Class PreProcessHelp
        Inherits Attribute

        Protected _help As String
        Public Property Help() As String
            Get
                Return _help
            End Get
            Set(ByVal Value As String)
                _help = Value
            End Set
        End Property
        Public Sub New(ByVal helpString As String)
            Help = helpString
        End Sub

    End Class
End Interface

'El dia 16/01/2006 se elimino la interface:

'Public Interface IFolderPreProcess

'    Function process(Byval FM As FolderMonitor, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False)

'    Function GetXml(Optional ByVal xml As String = Nothing) As String

'    Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String

'    Sub SetXml(Optional ByVal xml As String = Nothing)

'    Function GetHelp() As String
'    Event PreprocessMessage(ByVal msg As String)
'    Event PreprocessError(ByVal Errormsg As String)

'End Interface

'La estructura:
'Public Structure FolderMonitor
'    Public FullFilename As String
'    Public TempPath As String
'    Public BackupPath As String
'End Structure
'Se reemplazo por un arraylist donde la componentes representan de esta forma los campos de la estructura:
'Files(0) = FullFilename
'Files(1) = BackupPath
'Files(2) = TempPath






' Visual Basic requires the AttributeUsage be specified.




