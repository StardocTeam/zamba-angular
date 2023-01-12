Imports System.IO
Imports System
Imports System.Text

Public Class frmZDBFixerErrors

    Public Sub New(ByVal sErrors As String, Optional ByVal frmTitle As String = "")
        InitializeComponent()
        Me.txtErrors.Text = sErrors
        If Not String.IsNullOrEmpty(frmTitle) Then
            Me.Text = frmTitle
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnGenerateFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateFile.Click
        Dim tmpSFD As New SaveFileDialog()
        tmpSFD.Filter = "txt Files (*.txt)|*.txt|All Files (*.*)|*.*"
        tmpSFD.FilterIndex = 0
        tmpSFD.RestoreDirectory = True
        tmpSFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        tmpSFD.DefaultExt = "*.txt"
        tmpSFD.FileName = "ZDBFixerErrors.Trace"
        If tmpSFD.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                If Not String.IsNullOrEmpty(tmpSFD.FileName) Then
                    Dim traceFileStream As New IO.FileInfo(tmpSFD.FileName)
                    Dim traceStreamWriter As StreamWriter = traceFileStream.CreateText()
                    traceStreamWriter.Write(Me.txtErrors.Text)
                    traceStreamWriter.Close()
                    MessageBox.Show("Se ha creado el archivo correctamente.", "Zamba DBFixer - Errores del Proceso", MessageBoxButtons.OK)
                    Me.Close()
                Else
                    MessageBox.Show("No ha seleccionado ningún nombre para el archivo, el archivo no puede ser creado.", "Zamba DBFixer - Errores del Proceso", MessageBoxButtons.OK)
                End If
            Catch
                MessageBox.Show("Ha ocurrido un error al guardar el archivo. Favor de intentarlo nuevamente", "Zamba DBFixer - Errores del Proceso", MessageBoxButtons.OK)
            End Try
        End If
    End Sub

    Private Sub frmZDBFixerErrors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtErrors.BackColor = Color.White
        Dim ttpBtnGenerateFile As New ToolTip()
        ttpBtnGenerateFile.SetToolTip(Me.btnGenerateFile, "Exportar errores a un archivo de texto")
        Dim ttpBtnCerrar As New ToolTip()
        ttpBtnCerrar.SetToolTip(Me.btnCerrar, "Cerrar esta ventana y volver al Zamba DBFixer")
    End Sub

End Class