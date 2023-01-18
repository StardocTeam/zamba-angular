Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Data
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms

Public Class Header
    Inherits Cell

    Dim iColumnNumber As Integer

    Public Overrides ReadOnly Property ColumnNumber() As Integer
        Get
            Return iColumnNumber
        End Get
    End Property


    '/// <summary>
    '/// Create New Header
    '/// </summary>
    '/// <param name="RowNumber"></param>
    '/// <param name="ColumnNumber"></param>
    '/// <param name="cellfont"></param>
    '/// <param name="align"></param>
    '/// <param name="location"></param>
    '/// <param name="text"></param>
    Public Sub New(ByVal ColumnNumber As Integer, ByVal Headerfont As Font, ByVal align As HorizontalAlignment, ByVal location As RectangleF, ByVal text As String)
        'MyBase.New()
        iColumnNumber = ColumnNumber
        Font = Headerfont
        Alignment = align
        RectangleLocation = location
        CText = text
    End Sub

    '/// <summary>
    '/// Create New Header with default alignment
    '/// </summary>
    '/// <param name="RowNumber"></param>
    '/// <param name="ColumnNumber"></param>
    '/// <param name="cellfont"></param>
    '/// <param name="align"></param>
    '/'// <param name="location"></param>
    '/// <param name="text"></param>
    Public Sub New(ByVal ColumnNumber As Integer, ByVal Headerfont As Font, ByVal location As RectangleF, ByVal text As String)
        iColumnNumber = ColumnNumber
        Font = Headerfont
        Alignment = HorizontalAlignment.Left
        rectLocation = location
        CText = text
    End Sub
End Class

