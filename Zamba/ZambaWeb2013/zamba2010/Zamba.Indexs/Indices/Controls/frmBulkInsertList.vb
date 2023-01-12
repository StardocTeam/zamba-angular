Imports Zamba.Core
Imports System.Text

Public Class frmBulkInsertList
    Inherits Zamba.AppBlock.ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal _indexid As Int32)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Me.IndexId = _indexid
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents BtnCancelar As Zamba.AppBlock.ZButton1
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnaceptar As Zamba.AppBlock.ZButton
    Friend WithEvents btnFile As Zamba.AppBlock.ZButton
    Friend WithEvents txtfile As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmBulkInsertList))
        Me.lblstatus = New System.Windows.Forms.Label
        Me.BtnCancelar = New Zamba.AppBlock.ZButton1
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnaceptar = New Zamba.AppBlock.ZButton
        Me.btnFile = New Zamba.AppBlock.ZButton
        Me.txtfile = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ZIconList
        '

        '
        'lblstatus
        '
        Me.lblstatus.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(32, 88)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(320, 18)
        Me.lblstatus.TabIndex = 16
        Me.lblstatus.Text = "Inactivo"
        Me.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCancelar
        '
        Me.BtnCancelar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnCancelar.Location = New System.Drawing.Point(176, 128)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(120, 25)
        Me.BtnCancelar.TabIndex = 15
        Me.BtnCancelar.Text = "Cancelar"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 17)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Archivo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnaceptar
        '
        Me.btnaceptar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnaceptar.Location = New System.Drawing.Point(48, 128)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(120, 25)
        Me.btnaceptar.TabIndex = 12
        Me.btnaceptar.Text = "Aceptar"
        '
        'btnFile
        '
        Me.btnFile.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnFile.Location = New System.Drawing.Point(288, 32)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(80, 26)
        Me.btnFile.TabIndex = 11
        Me.btnFile.Text = "Buscar"
        '
        'txtfile
        '
        Me.txtfile.BackColor = System.Drawing.Color.White
        Me.txtfile.Location = New System.Drawing.Point(24, 32)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.Size = New System.Drawing.Size(264, 21)
        Me.txtfile.TabIndex = 10
        Me.txtfile.Text = ""
        '
        'frmBulkInsertList
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(392, 190)
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.BtnCancelar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnaceptar)
        Me.Controls.Add(Me.btnFile)
        Me.Controls.Add(Me.txtfile)
        Me.DockPadding.All = 2
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBulkInsertList"
        Me.Text = "Inserción másiva"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Eventos"
    Public Event Actualizarlista()
#End Region
    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        Me.Close()
    End Sub
    Dim IndexId As Int32
    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Try
            Dim dlg As New OpenFileDialog
            dlg.ShowDialog()
            txtfile.Text = dlg.FileName
        Catch
        End Try
    End Sub


    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        Try
            Dim t1 As New Process(AddressOf Procesar)
            t1.Invoke(Me.txtfile.Text.Trim)
            If Me.Cursor Is Cursors.Default Then
                Me.Dispose()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Delegate Sub Process(ByVal filename As String)

    Private Sub Procesar(ByVal filename As String)
        Dim noInsertados As New Generic.List(Of String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim sr As IO.StreamReader
            If IO.File.Exists(Me.txtfile.Text.Trim) Then
                sr = New IO.StreamReader(Me.txtfile.Text.Trim)
                '                Dim max As Int32
                Dim linea As String
                '               max = AutoSubstitutionBusiness.GetMax(Me.IndexId)
                'If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, sql)) Then
                '    max = Server.Con.ExecuteScalar(CommandType.Text, sql)
                'End If
                'If max = 0 Then max = 1
                'max += 1
                While sr.Peek <> -1
                    linea = sr.ReadLine.Trim
                    linea = linea.Replace("'", "").Replace(Chr(34), "")
                    If linea.Trim <> "" Then
                        'Si no se pudo realizar la inserción
                        If Not AutoSubstitutionBusiness.InsertIntoIListAsBoolean(Me.IndexId, linea) Then
                            noInsertados.Add(linea)
                        End If
                        'AutoSubstitutionBusiness.InsertIntoIList(Me.IndexId, linea)
                        'max += 1
                    End If
                End While
                sr.Close()
            Else
                MessageBox.Show("El archivo no existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch

        Finally

            'Si hay líneas que no pudo insertar:
            If noInsertados.Count > 0 Then

                'Las presenta todas en un StringBuilder:
                Dim sBuilder As New StringBuilder

                sBuilder.Append("Los siguientes valores no pudieron ser insertados:")
                For Each s As String In noInsertados
                    sBuilder.AppendLine()
                    sBuilder.Append("'")
                    sBuilder.Append(s)
                    sBuilder.Append("'")
                Next

                'Y las muestra como advertencia:
                MessageBox.Show(sBuilder.ToString, "Advertencia", MessageBoxButtons.OKCancel)

                'Si todas las líneas pudieron ser insertadas:
            Else
                MessageBox.Show("Importación Finalizada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Me.Cursor = Cursors.Default

            RaiseEvent Actualizarlista()

        End Try
    End Sub

End Class
