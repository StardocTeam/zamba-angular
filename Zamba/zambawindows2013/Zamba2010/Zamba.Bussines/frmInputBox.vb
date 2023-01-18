Public Class frmInputBox
    Public CrossPressed As Boolean = False
    Private Property CheckNotEmpty() As String

    Public Property Name() As String
        Get
            Return txtUserText.Text
        End Get
        Set(ByVal value As String)
            txtUserText.Text = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblMax20.Text = String.Empty
        lblChracters.Text = String.Empty
    End Sub

    ''' <summary>
    '''  Configura las funcionalidades del input box
    ''' </summary>
    ''' <history>
    ''' [Sebastian] 19-10-2009 CREATED Configura las funcionalidades del input box
    ''' </history>
    ''' <param name="txtBoxmaxLenght"> maximo de caracteres que se pueden inresar en el txtbox</param>
    ''' <param name="UserQuestion">Pregunta o indicación que verá el usuario en el formulario</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal UserQuestion As String, _
                   ByVal txtBoxmaxLenght As Int64, _
                   ByVal DefaultRuleName As String, _
                   Optional ByVal frmName As String = "Zamba Software", _
                   Optional ByVal checkNotEmptyText As Boolean = True, _
                   Optional ByVal blnPass As Boolean = False)
        MyBase.New()
        InitializeComponent()

        txtUserText.MaxLength = txtBoxmaxLenght
        lblText.Text = UserQuestion
        lblChracters.Text = "Caracteres restantes: " & txtUserText.MaxLength - DefaultRuleName.Length
        Me.Text = frmName
        CheckNotEmpty = checkNotEmptyText
        RemoveHandler txtUserText.TextChanged, AddressOf txtUserText_TextChanged
        Me.txtUserText.Text = DefaultRuleName
        Me.txtUserText.UseSystemPasswordChar = blnPass
        AddHandler txtUserText.TextChanged, AddressOf txtUserText_TextChanged
        Me.lblMax20.Text = String.Empty
        Me.lblChracters.Text = String.Empty
    End Sub

    ''' <summary>
    ''' cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcept.Click
        OK()
    End Sub

    Private Sub OK()
        If CheckNotEmpty Then
            If txtUserText.Text.Trim.Length = 0 Then
                lblMax20.Text = "Debe completar el campo para continuar"
                Exit Sub
            End If
        End If

        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub txtUserText_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUserText.KeyDown
        If e.KeyCode = Keys.Enter Then Me.OK()
    End Sub

    ''' <summary>
    ''' muestra al usuario la cantidad de caracteres disponibles para escribir dentro del textbox
    ''' </summary>
    ''' <history>
    ''' 
    ''' </history>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtUserText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserText.TextChanged
        lblChracters.Text = "Caracteres restantes: " & txtUserText.MaxLength - txtUserText.Text.Length

        If txtUserText.Text.Length > 20 Then
            lblMax20.Text = "Se visualizarán los primeros 20 caracteres en zamba Cliente"
        Else
            lblMax20.Text = String.Empty
        End If
    End Sub

    Private Sub frmInputBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

End Class