Public Class Cell
    Public rectLocation As RectangleF

    Dim fCellHeight As Single
    Dim fCellWidth As Single
    Dim fCellX As Single
    Dim fCellY As Single

    Dim fontCellFont As Font
    Dim haAlignment As HorizontalAlignment
    Dim sText As String

    Dim iColumnNumber As Integer

    Dim iRowNumber As Integer

    '/// <summary>
    '/// The Font of the text in the cell
    '/// </summary>
    Public Property CText() As String

        Get
            Return sText
        End Get
        Set(ByVal Value As String)
            sText = Value
        End Set
    End Property

    '/// <summary>
    '/// The Font of the text in the cell
    '/// </summary>
    Public Property Font() As Font

        Get
            Return fontCellFont
        End Get
        Set(ByVal Value As Font)
            fontCellFont = Value
        End Set

    End Property

    '/// <summary>
    '/// The Location of the cell
    '/// </summary>
    Public ReadOnly Property Location() As RectangleF
        Get
            Return rectLocation
        End Get
    End Property


    '/// <summary>
    '/// Set The location of the Cell
    '/// </summary>
    Protected WriteOnly Property RectangleLocation() As RectangleF
        Set(ByVal Value As RectangleF)
            rectLocation = Value
            fCellWidth = Value.Width
            fCellHeight = Value.Height
            fCellX = Value.X
            fCellY = Value.Y
        End Set
    End Property

    '/// <summary>
    '/// Get the Height of the cell
    '/// </summary>
    Public ReadOnly Property Height() As Single
        Get
            Return fCellHeight
        End Get

    End Property

    '/// <summary>
    '/// Get the Height of the cell
    '/// </summary>
    Public ReadOnly Property Width() As Single
        Get
            Return fCellWidth
        End Get
    End Property

    '/// <summary>
    '/// Get the Height of the cell
    '/// </summary>
    Public ReadOnly Property X() As Single
        Get
            Return fCellX
        End Get
    End Property

    '/// <summary>
    '/// Get the Height of the cell
    '/// </summary>
    Public ReadOnly Property Y() As Single
        Get
            Return fCellY
        End Get

    End Property

    '/// <summary>
    '/// The Column number of the Cell
    '/// </summary>
    Public Overridable ReadOnly Property ColumnNumber() As Integer

        Get
            Return iColumnNumber
        End Get
    End Property


    '/// <summary>
    '/// The Row number of the cell
    '/// </summary>
    Public ReadOnly Property RowNumber() As Integer
        Get
            Return iRowNumber
        End Get
    End Property

    '/// <summary>
    '/// The Horizonal Alignment cell
    '/// </summary>
    Public Property Alignment() As HorizontalAlignment
        Get
            Return haAlignment
        End Get
        Set(ByVal Value As HorizontalAlignment)
            haAlignment = Value
        End Set
    End Property

    Public Sub New()
    End Sub
    '/// <summary>
    '/// Create New Cell
    '/// </summary>
    '/// <param name="RowNumber"></param>
    '/// <param name="ColumnNumber"></param>
    '/// <param name="cellfont"></param>
    '/// <param name="align"></param>
    '/// <param name="location"></param>
    '/// <param name="text"></param>
    Public Sub New(ByVal RowNumber As Integer, ByVal ColumnNumber As Integer, ByVal cellfont As Font, ByVal align As HorizontalAlignment, ByVal location As RectangleF, ByVal text As String)

        iRowNumber = RowNumber
        iColumnNumber = ColumnNumber
        fontCellFont = cellfont
        haAlignment = align
        RectangleLocation = location
        sText = text

    End Sub

    '/// <summary>
    '/// Create new cell with default Text
    '/// </summary>
    '/// <param name="RowNumber"></param>
    '/// <param name="ColumnNumber"></param>
    '/// <param name="cellfont"></param>
    '/// <param name="align"></param>
    '/// <param name="location"></param>
    Public Sub New(ByVal RowNumber As Integer, ByVal ColumnNumber As Integer, ByVal cellfont As Font, ByVal align As HorizontalAlignment, ByVal location As RectangleF)

        iRowNumber = RowNumber
        iColumnNumber = ColumnNumber
        fontCellFont = cellfont
        haAlignment = align
        RectangleLocation = location
        sText = Nothing

    End Sub

    '/// <summary>
    '/// Create new cell with default Text and alignment
    '/// </summary>
    '/// <param name="RowNumber"></param>
    '/// <param name="ColumnNumber"></param>
    '/// <param name="cellfont"></param>
    '/// <param name="location"></param>
    Public Sub New(ByVal RowNumber As Integer, ByVal ColumnNumber As Integer, ByVal cellfont As Font, ByVal location As RectangleF)

        iRowNumber = RowNumber
        iColumnNumber = ColumnNumber
        fontCellFont = cellfont
        haAlignment = HorizontalAlignment.Left
        RectangleLocation = location
        sText = Nothing

    End Sub

    '/// <summary>
    '/// Create new cell with default alignment
    '/// </summary>
    '/// <param name="RowNumber"></param>
    '/// <param name="ColumnNumber"></param>
    '/// <param name="cellfont"></param>
    '/// <param name="location"></param>
    Public Sub New(ByVal RowNumber As Integer, ByVal ColumnNumber As Integer, ByVal cellfont As Font, ByVal location As RectangleF, ByVal text As String)
        iRowNumber = RowNumber
        iColumnNumber = ColumnNumber
        fontCellFont = cellfont
        haAlignment = HorizontalAlignment.Left
        RectangleLocation = location
        sText = text
    End Sub
End Class
