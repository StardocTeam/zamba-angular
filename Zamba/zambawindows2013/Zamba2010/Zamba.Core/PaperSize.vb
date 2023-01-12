Imports Zamba.Core

''' <summary>
''' Clase encargada de suministrar los tamaños de los diferentes tipos de hoja.
''' </summary>
''' <remarks>Los tipos de hoja y los tamaños son los establecidos por la norma ISO.</remarks>
Public Class PaperSize

    Private _size As PaperSizes
    Public Property Tamaño() As PaperSizes
        Get
            Return _size
        End Get
        Set(ByVal value As PaperSizes)
            _size = value
        End Set
    End Property

    Private _alto As Int32
    Public Property Alto() As Int32
        Get
            Return _alto
        End Get
        Set(ByVal value As Int32)
            _alto = value
        End Set
    End Property

    Private _ancho As Int32
    Public Property Ancho() As Int32
        Get
            Return _ancho
        End Get
        Set(ByVal value As Int32)
            _ancho = value
        End Set
    End Property

    ''' <summary>
    ''' Crea un objeto con sus propiedades de alto y ancho cargadas dependiendo del tipo de papel especificado.
    ''' </summary>
    ''' <param name="size">Tipo de papel donde se obtendrán las medidas</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal size As PaperSizes)
        _size = size
        LoadSizeValues(size)
    End Sub

    ''' <summary>
    ''' Carga las medidas de la hoja seleccionada
    ''' </summary>
    ''' <param name="size"></param>
    ''' <remarks>Los tamaños fueron obtenidos de la siguiente página web:
    '''          http://www.cl.cam.ac.uk/~mgk25/iso-paper.html
    '''          Al parecer son tamaños establecidos por la norma ISO.
    ''' </remarks>
    Private Sub LoadSizeValues(ByVal size As PaperSizes)
        Select Case size
            Case PaperSizes._2A0
                Ancho = 1682
                Alto = 2378
            Case PaperSizes._4A0
                Ancho = 1189
                Alto = 1682
            Case PaperSizes.A0
                Ancho = 841
                Alto = 1189
            Case PaperSizes.A1
                Ancho = 594
                Alto = 841
            Case PaperSizes.A2
                Ancho = 420
                Alto = 594
            Case PaperSizes.A3
                Ancho = 297
                Alto = 420
            Case PaperSizes.A4
                Ancho = 210
                Alto = 297
            Case PaperSizes.A5
                Ancho = 148
                Alto = 210
            Case PaperSizes.A6
                Ancho = 105
                Alto = 210
            Case PaperSizes.A7
                Ancho = 74
                Alto = 105
            Case PaperSizes.A8
                Ancho = 52
                Alto = 74
            Case PaperSizes.A9
                Ancho = 37
                Alto = 52
            Case PaperSizes.A10
                Ancho = 26
                Alto = 37
            Case PaperSizes.B0
                Ancho = 1000
                Alto = 1414
            Case PaperSizes.B1
                Ancho = 707
                Alto = 1000
            Case PaperSizes.B2
                Ancho = 500
                Alto = 707
            Case PaperSizes.B3
                Ancho = 353
                Alto = 500
            Case PaperSizes.B4
                Ancho = 250
                Alto = 353
            Case PaperSizes.B5
                Ancho = 176
                Alto = 250
            Case PaperSizes.B6
                Ancho = 125
                Alto = 176
            Case PaperSizes.B7
                Ancho = 88
                Alto = 125
            Case PaperSizes.B8
                Ancho = 62
                Alto = 88
            Case PaperSizes.B9
                Ancho = 44
                Alto = 62
            Case PaperSizes.B10
                Ancho = 31
                Alto = 44
            Case PaperSizes.C0
                Ancho = 917
                Alto = 1297
            Case PaperSizes.C1
                Ancho = 648
                Alto = 917
            Case PaperSizes.C2
                Ancho = 458
                Alto = 648
            Case PaperSizes.C3
                Ancho = 324
                Alto = 458
            Case PaperSizes.C4
                Ancho = 229
                Alto = 324
            Case PaperSizes.C5
                Ancho = 162
                Alto = 229
            Case PaperSizes.C6
                Ancho = 114
                Alto = 162
            Case PaperSizes.C7
                Ancho = 81
                Alto = 114
            Case PaperSizes.C8
                Ancho = 57
                Alto = 81
            Case PaperSizes.C9
                Ancho = 40
                Alto = 57
            Case PaperSizes.C10
                Ancho = 28
                Alto = 40
        End Select

    End Sub
End Class

