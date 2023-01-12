
Public Class ImageHelper

#Region "Properties"
    Private m_pathTemporal As String
    Private m_PathOut As String
    Private m_PathOriginal As String
    Private m_Extension As String
    Friend m_img As Image
    Private m_ListaImagenes As IList
    Private m_codec As Imaging.ImageCodecInfo

    Public Property PathOriginal() As String
        Get
            Return m_PathOriginal
        End Get
        Set(ByVal value As String)
            Try
                If value = "" Then
                    Throw New ArgumentException("El path no es valido.")
                End If
                m_PathOriginal = value
                m_Extension = System.IO.Path.GetExtension(value)
                If m_Extension = "" Then
                    Throw New ArgumentException("La extension del archivo no es valida.")
                End If
                m_Extension = m_Extension.ToLower().Replace(".", "")

                If m_Extension = "tif" OrElse m_Extension = "tiff" Then
                    m_Extension = "tiff"
                Else
                    Throw New ArgumentException("El formato de la imagen es incorrecto." & _
                                                vbNewLine & "Solo es admitido el tipo tiff.")
                End If
                CrearPathTemporal()
                getCodec()
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            End Try
        End Set
    End Property

    ''' <summary>
    '''  Imagen guardada
    ''' </summary>
    ''' 

    Public ReadOnly Property Image() As Image
        Get
            Return m_img
        End Get
    End Property

    ''' <summary>
    '''  Lista de Pics
    ''' </summary>
    ''' 

    Public Property PicArray() As IList
        Get
            Return m_ListaImagenes
        End Get
        Set(ByVal value As IList)
            m_ListaImagenes = value
        End Set
    End Property

    Public ReadOnly Property PathDestino() As String
        Get
            Return m_PathOut
        End Get
    End Property

    Public ReadOnly Property Codec() As Imaging.ImageCodecInfo
        Get
            Return m_codec
        End Get
    End Property


#End Region

#Region "Ctor"
    Public Sub New()
        m_pathTemporal = System.IO.Path.Combine(Application.StartupPath, "Zamba.EditorPrototypeTemp")
    End Sub
#End Region

#Region "Temporal"
    ''' <summary>
    ''' Genera un path temporal
    ''' </summary>
    ''' <remarks>El path se especifica por medio de la propiedad PathOriginal</remarks>
    Private Sub CrearPathTemporal()
        Dim fileName As String

        Try
            fileName = System.IO.Path.GetFileName(m_PathOriginal)

            If System.IO.Directory.Exists(m_pathTemporal) Then
                Try
                    System.IO.Directory.Delete(m_pathTemporal, True)
                    System.IO.Directory.CreateDirectory(m_pathTemporal)
                Catch ex As Exception
                    'Si ocurre este error significa que hay un proceso bloqueando algun archivo,
                    'por ello se crea un nombre seguro con un guid.
                    Dim m_guid As Guid
                    m_guid = Guid.NewGuid()
                    fileName = m_guid.ToString & System.IO.Path.GetExtension(m_PathOriginal)
                End Try
            Else
                System.IO.Directory.CreateDirectory(m_pathTemporal)
            End If

            m_PathOut = System.IO.Path.Combine(m_pathTemporal, fileName)
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
#End Region

#Region "Method"
    Public Function Save() As String
        Try
            MakeNewImage()
            Return m_PathOut
        Catch ex As Exception
            Throw New ArgumentException("No se puede guardar los cambios sobre la imagen." & _
                                         vbNewLine & ex.Message)
        End Try
    End Function
#End Region

#Region "Codec"
    ''' <summary>
    '''  Busca el codec correspondiente a la extension del archivo
    ''' </summary>
    Private Sub getCodec()
        Dim codecs() As Imaging.ImageCodecInfo = Imaging.ImageCodecInfo.GetImageEncoders()
        m_codec = Nothing
        For Each co As Imaging.ImageCodecInfo In codecs
            If co.MimeType = "image/" & m_Extension Then
                m_codec = co
                Exit For
            End If
        Next
        If IsNothing(m_codec) Then
            Throw New ArgumentException("Imposible obtener el encoder para este tipo de archivo.")
        End If
    End Sub
#End Region

#Region "Overrrides"
    ''' <summary>
    ''' Genera una imagen con el resultado de la modificacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MakeNewImage()
        Dim m_helper As AbstractPersistImage = New PersistImageImpl(Me)
        Try
            For Each P As Pic In m_ListaImagenes
                m_helper.Save(P.OriginalImage)
            Next
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
#End Region

End Class

