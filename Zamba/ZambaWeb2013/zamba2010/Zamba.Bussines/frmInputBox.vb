Public Class frmInputBox



    Private UserWarning As Boolean
    Public CrossPressed As Boolean = False

    Public Property Name() As String
        Get
            Return txtUserText.Text
        End Get
        Set(ByVal value As String)
            txtUserText.Text = value
        End Set
    End Property


    ''' <summary>
    ''' cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcept.Click
        Me.Close()
        Me.DialogResult = DialogResult.OK
    End Sub
    ' Dim UserWarned As Boolean = False


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
        'muestra al usuario la cantidad de caracteres que le resta para escribir
        lblChracters.ForeColor = Color.Black
        lblChracters.Text = "Caracteres restantes: " & txtUserText.MaxLength - txtUserText.Text.Length

        'estoy validando que la cantidad de caracteres seleccionando sea la misma que los que hay en el text box.
        'en ese caso quiere decir que esta todo el texto seleccionado del textbox, por lo que si apreto una tecla estoy
        'sobreescribiendo el texto seleccionado. por lo cual no necesito validar nada ya que comienzo de cero.
        ' If txtUserText.SelectionLength <> txtUserText.Text.Length Then
        If txtUserText.Text.Length > 20 Then ' AndAlso UserWarned = False Then
            'If Object.Equals(e.GetType, GetType(KeyEventArgs)) AndAlso DirectCast(e, KeyEventArgs).KeyData <> Keys.Back _
            ' AndAlso UserWarning = True Then
            'If UserWarning = True Then
            'If MessageBox.Show("Recuerde que solo se veran los primeros 20 caracteres en el Cliente", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information) =DialogResult.OK Then
            lblMax20.Text = "Se visualizarán los primeros 20 caracteres en zamba Cliente"
            'UserWarned = True
            'End If
        Else
            lblMax20.Text = String.Empty
        End If
        'End If
        'End If
    End Sub

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
    Public Sub New(ByVal UserQuestion As String, ByVal txtBoxmaxLenght As Int64, ByVal DefaultRuleName As String, Optional ByVal frmName As String = "Zamba Software", Optional ByVal WarnUser As Boolean = False)
        MyBase.New()
        InitializeComponent()

        txtUserText.MaxLength = txtBoxmaxLenght
        lblText.Text = UserQuestion
        lblChracters.Text = "Caracteres restantes: " & txtUserText.MaxLength - DefaultRuleName.Length
        Me.Text = frmName
        UserWarning = WarnUser
        RemoveHandler txtUserText.TextChanged, AddressOf txtUserText_TextChanged
        Me.txtUserText.Text = DefaultRuleName
        AddHandler txtUserText.TextChanged, AddressOf txtUserText_TextChanged
        Me.lblMax20.Text = String.Empty
        Me.lblChracters.Text = String.Empty
    End Sub

    Private Sub frmInputBox_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing



    End Sub

End Class