Imports Zamba.Core

Public Class InsertFormDialog

    Private newResult As INewResult
    Private form As ZwebForm
    Private formBrowser As FormBrowser
    Private _result As IResult

    ''' <summary>
    ''' Result insertado desde formBrowser
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Result() As IResult
        Get
            Return _result
        End Get
        Set(ByVal value As IResult)
            _result = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    ''' <summary>
    ''' Genera una instancia nueva cargando un formulario particular con los datos de un Result
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="formId"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal result As INewResult, ByVal formId As Int64)

        ' This call is required by the designer.
        InitializeComponent()

        'Se obtiene el form
        DialogResult = DialogResult.Cancel
        form = FormBusiness.GetForm(formId)
        newResult = result
        newResult.CurrentFormID = form.ID

        'Se acopla el formBrowser al formulario
        formBrowser = New FormBrowser
        Panel2.Controls.Add(formBrowser)
        formBrowser.Dock = DockStyle.Fill
        AddHandler formBrowser.ResultModified, AddressOf ResultModified
        TextBox1.Enabled = False

        'Se comenta el panel inferior que permite realizar inserciones de documentos .doc.
        'Se creara un checkbox para configurar la visibilidad del mismo.
        Panel1.Visible = False

    End Sub

    ''' <summary>
    ''' Carga el formulario en el FormBrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub InsertFormDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Si todo se encuentra correctamente instanciado se muestra el formulario
        If newResult IsNot Nothing AndAlso form IsNot Nothing AndAlso formBrowser IsNot Nothing Then
            Try
                formBrowser.ShowDocument(newResult, form)
            Catch ex As ZambaEx
                ZClass.raiseerror(ex)
                MessageBox.Show(ex.Message, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("Ocurrió un error al cargar el formulario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el Result creado desde FormBrowser y cierra el formulario con la señal de OK.
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub ResultModified(ByVal result As IResult)
        _result = result
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As EventArgs) Handles Button1.Click
        ReplaceDocument(_result)
    End Sub

    Dim OpenFileDialogResult As DialogResult = DialogResult.Cancel

    Private Sub ReplaceDocument(ByRef Result As Result)
        Try
            OpenFileDialog1.CheckFileExists = True
            OpenFileDialog1.CheckPathExists = True
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Title = "Insercion de adjunto"
            OpenFileDialog1.ValidateNames = True
            OpenFileDialog1.Filter = "Archivos de imagen  (*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF;*.PCX;*.PCX;*.DOC;*.DOCX;*.XLS;*.XLSX;*.PDF)|*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF;*.PCX;*.DOC;*.DOCX;*.XLS;*.XLSX;*.PDF"
            OpenFileDialogResult = OpenFileDialog1.ShowDialog()
            TextBox1.Text = OpenFileDialog1.FileName
            formBrowser.SetDocumentFile(TextBox1.Text)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class