''' <summary>
''' Se utiliza como workaround de un problema que tiene la clase de spire PdfViewer al hacer scroll sobre la hoja.
''' Para solucionarlo se sobreescribe el método WndProc y se atrapa la exception generada. De esta manera el 
''' scroll funciona y no genera exception. Al no tener la toolbar del PdfViewer se agrega un combo para tener al
''' menos la funcionalidad del zoom.
''' </summary>
''' <remarks></remarks>
Public Class PdfDocumentViewerFix
    Inherits Spire.PdfViewer.Forms.PdfDocumentViewer

    Private cboZoomMode As ComboBox

    ''' <summary>
    ''' Genera un objeto que hereda de Spire.PdfViewer.Forms.PdfDocumentViewer y corrige un error al hacer scroll sobre el documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        'Tipo de zoom
        cboZoomMode = New ComboBox
        With cboZoomMode
            .Top = 10
            .Left = 10
            .Width = 100
            .Height = 30
            .DropDownStyle = ComboBoxStyle.DropDownList
            .Items.AddRange(New Object() {"Zoom: Defecto", "Zoom: Página", "Zoom: Ancho"})
            .SelectedIndex = 0
        End With
        AddHandler cboZoomMode.SelectedIndexChanged, AddressOf ChangeZoomMode
        Controls.Add(cboZoomMode)

        'TODO: los botones de zoom de + y - no fueron agregados ya que la clase es medio complicada configurar a gusto.
        'Por el momento se deja unicamente el combo que permite visualizar la hoja sin inconvenientes.

        ''Zoom acercar
        'Dim btnZoomIn As New Button
        'With btnZoomIn
        '    .Top = 50
        '    .Left = 20
        '    .Width = 20
        '    .Height = 20
        '    .Text = "+"
        'End With
        'AddHandler btnZoomIn.Click, AddressOf ZoomIn
        'Me.Controls.Add(btnZoomIn)

        ''Zoom alejar
        'Dim btnZoomOut As New Button
        'With btnZoomOut
        '    .Top = 80
        '    .Left = 20
        '    .Width = 20
        '    .Height = 20
        '    .Text = "-"
        'End With
        'AddHandler btnZoomOut.Click, AddressOf ZoomOut
        'Me.Controls.Add(btnZoomOut)
    End Sub

    ''' <summary>
    ''' Modifica el tipo de zoom sobre el documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZoomMode(sender As Object, e As EventArgs)
        Select Case cboZoomMode.SelectedIndex
            Case 0
                ZoomTo(ZoomMode.Default)
            Case 1
                ZoomTo(ZoomMode.FitPage)
            Case 2
                ZoomTo(ZoomMode.FitWidth)
        End Select
    End Sub

    ''' <summary>
    ''' Este es el fix necesario para que la clase PdfDocumentViewer funcionara al hacer el scroll sobre el documento
    ''' </summary>
    ''' <param name="m"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            MyBase.WndProc(m)
        Catch ex As OverflowException
            'La libreria presenta un error al usar la rueda del mouse para recorrer el pdf
            'Este es un workaround para solucionar dicho error
        Catch ex As Exception
        End Try
    End Sub

    'Protected Overrides Sub Dispose(disposing As Boolean)
    '    MyBase.Dispose(disposing)

    'End Sub

End Class
