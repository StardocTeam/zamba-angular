Imports Zamba.Core
Imports Zamba.Browser

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.ClientControls
''' Class	 : frmAddOrUpdateTemplate
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que se ejecuta cuando se quiere agregar un nuevo template, o bien, actualizar uno ya existente
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
'''     [Gaston]	21/04/2008	Created
''' </history>
''' -----------------------------------------------------------------------------

Public Class frmAddOrUpdateTemplate
    Inherits Zamba.AppBlock.ZForm

#Region "Atributos"

    ' Para que no se ejecute el errorProvider del txtPath, si el usuario pasa al botón Buscar con el tab y el txtPath está vacío (la 1º vez)
    Private controllerErrorInPath As Boolean = True
    Private Browser As New Zamba.Browser.WebBrowser
    Private mName As String = Nothing
    Private mPath As String = Nothing
    Private mDescription As String = Nothing

#End Region

#Region "Constructores"

    'Constructor para el Agregar
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Constructor para el Actualizar
    Public Sub New(ByVal path As String, ByVal name As String, ByVal description As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.mPath = path
        Me.mName = name
        Me.mDescription = description

    End Sub

#End Region

#Region "Propiedades"

    Public ReadOnly Property Path() As String
        Get
            Return (Me.mPath)
        End Get
    End Property

    Public Shadows ReadOnly Property Name() As String
        Get
            Return (Me.mName)
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return (Me.mDescription)
        End Get
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga la ventana
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAddOrUpdateTemplate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Si el path es Nothing entonces la ventana es para agregar un template, sino será para actualizar un template
        If (Me.Path Is Nothing) Then
            Me.Text = "Agregar Template"
            btnAddOrUpdate.Text = "Agregar"
        Else
            Me.txtPath.Text = mPath
            Me.txtName.Text = mName
            Me.txtDescription.Text = mDescription
            Me.Text = "Actualizar Template"
            btnAddOrUpdate.Text = "Actualizar"
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExplore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExplore.Click

        Try

            Dim Dlg As New OpenFileDialog
            Dlg.ShowDialog()
            Me.txtPath.Text = Dlg.FileName

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botòn AddOrUpdate (Agregar o Actualizar)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddOrUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrUpdate.Click
        addOrUpdateTemplate()
    End Sub

#Region "Eventos de Validación"

    ''' <summary>
    ''' Evento que se ejecuta cuando el control se está validando (txtPath, txtName o txtDescription)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPath_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPath.Validating

        If (controllerErrorInPath = False) Then
            executeErrorProvider(Me.txtPath, "Debe especificar un template", ErrorProvider1)
        End If

    End Sub

    Private Sub txtName_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        executeErrorProvider(Me.txtName, "Debe especificar un nombre", ErrorProvider2)
    End Sub

    Private Sub txtDescription_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDescription.Validating
        executeErrorProvider(Me.txtDescription, "Debe especificar una descripción", ErrorProvider3)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando una tecla es liberada. En este caso serviria cuando el usuario presiona tab y se translada al botòn Buscar,
    ''' txtName o txtDescription
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExplore_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnExplore.KeyUp
        controllerErrorInPath = False
    End Sub

    Private Sub txtName_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp, txtDescription.KeyUp

        If (txtPath.Text = "") Then
            ErrorProvider1.SetError(Me.txtPath, "Debe especificar un template válido")
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se hace un click sobre la caja de texto Nombre o sobre la caja de texto Descripción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Click, txtDescription.Click

        controllerErrorInPath = False
        If (txtPath.Text = "") Then
            ErrorProvider1.SetError(Me.txtPath, "Debe especificar un template válido")
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor de la propiedad Text ya sea del txtPath, txtName o txtDescription cambia. Este evento sirve para
    ''' controlar si debe habilitarse o no el botón btnAddOrUpdate (Agregar o Actualizar). Se habilita cuando todas las cajas de texto tienen algo
    ''' en su interior
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPath.TextChanged, txtName.TextChanged, txtDescription.TextChanged

        If ((txtPath.Text <> "") AndAlso (txtName.Text <> "") AndAlso (txtDescription.Text <> "")) Then
            btnAddOrUpdate.Enabled = True
        Else
            btnAddOrUpdate.Enabled = False
        End If

    End Sub

#End Region

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que sirve para agregar los contenidos de las cajas de texto a los miembros privados de la clase. Para asì, después devolver el valor
    ''' de estas cajas de texto mediante propiedades a la ventana padre (donde se muestran los templates)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addOrUpdateTemplate()

        Try

            If (verifyIfThePathExists() = True) Then

                Me.mPath = txtPath.Text.Trim()
                Me.mName = txtName.Text.Trim()
                Me.mDescription = txtDescription.Text.Trim()
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que verifica que la caja de texto no este vacía. Si esta vacía entonces se ejecuta el errorProvider que le corresponde a cada caja
    ''' de texto. Si no, se limpia el errorProvider (si es que está, si no, no pasa nada)
    ''' </summary>
    ''' <param name="textbox"></param>
    ''' <param name="message"></param>
    ''' <param name="errorProvider"></param>
    ''' <remarks></remarks>
    Private Sub executeErrorProvider(ByRef textbox As System.Windows.Forms.TextBox, ByRef message As String, ByRef errorProvider As System.Windows.Forms.ErrorProvider)

        If (textbox.Text.Trim() = "") Then
            errorProvider.SetError(textbox, message)
        Else
            errorProvider.Clear()
        End If

    End Sub

    ''' <summary>
    ''' Método que verifica que el path sea válido y que el archivo exista
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verifyIfThePathExists() As Boolean

        Dim Fi As New IO.FileInfo(Me.txtPath.Text.Trim)

        ' Si el archivo no existe
        If Fi.Exists = False Then
            Me.ErrorProvider1.SetError(Me.txtPath, "Camino inválido o archivo inexistente")
            Return False
        Else
            Me.ErrorProvider1.Clear()
            Return True
        End If

    End Function

#End Region

End Class