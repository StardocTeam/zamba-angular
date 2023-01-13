Imports Zamba.Core
Imports System.Text

Public Class frmBulkInsertList
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal _indexid As Int32)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        IndexId = _indexid
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If lblstatus IsNot Nothing Then
                    lblstatus.Dispose()
                    lblstatus = Nothing
                End If
                If txtfile IsNot Nothing Then
                    txtfile.Dispose()
                    txtfile = Nothing
                End If
                If btnFile IsNot Nothing Then
                    btnFile.Dispose()
                    btnFile = Nothing
                End If
                If btnaceptar IsNot Nothing Then
                    btnaceptar.Dispose()
                    btnaceptar = Nothing
                End If
                If BtnCancelar IsNot Nothing Then
                    BtnCancelar.Dispose()
                    BtnCancelar = Nothing
                End If
                If Label1 IsNot Nothing Then
                    Label1.Dispose()
                    Label1 = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lblstatus As ZLabel
    Friend WithEvents BtnCancelar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents btnaceptar As ZButton
    Friend WithEvents btnFile As ZButton
    Friend WithEvents txtfile As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblstatus = New Zamba.AppBlock.ZLabel()
        Me.BtnCancelar = New Zamba.AppBlock.ZButton()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.btnaceptar = New Zamba.AppBlock.ZButton()
        Me.btnFile = New Zamba.AppBlock.ZButton()
        Me.txtfile = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblstatus
        '
        Me.lblstatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblstatus.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblstatus.FontSize = 9.75!
        Me.lblstatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblstatus.Location = New System.Drawing.Point(205, 79)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(490, 50)
        Me.lblstatus.TabIndex = 16
        Me.lblstatus.Text = "Inactivo"
        Me.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCancelar
        '
        Me.BtnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancelar.ForeColor = System.Drawing.Color.White
        Me.BtnCancelar.Location = New System.Drawing.Point(387, 150)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(111, 29)
        Me.BtnCancelar.TabIndex = 15
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(34, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 20)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Archivo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnaceptar
        '
        Me.btnaceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnaceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaceptar.ForeColor = System.Drawing.Color.White
        Me.btnaceptar.Location = New System.Drawing.Point(255, 150)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(111, 29)
        Me.btnaceptar.TabIndex = 12
        Me.btnaceptar.Text = "Aceptar"
        Me.btnaceptar.UseVisualStyleBackColor = False
        '
        'btnFile
        '
        Me.btnFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFile.ForeColor = System.Drawing.Color.White
        Me.btnFile.Location = New System.Drawing.Point(583, 40)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(112, 23)
        Me.btnFile.TabIndex = 11
        Me.btnFile.Text = "Buscar"
        Me.btnFile.UseVisualStyleBackColor = False
        '
        'txtfile
        '
        Me.txtfile.BackColor = System.Drawing.Color.White
        Me.txtfile.Location = New System.Drawing.Point(208, 40)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.Size = New System.Drawing.Size(369, 23)
        Me.txtfile.TabIndex = 10
        '
        'frmBulkInsertList
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(755, 184)
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.BtnCancelar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnaceptar)
        Me.Controls.Add(Me.btnFile)
        Me.Controls.Add(Me.txtfile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(0, 0)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(0, 100)
        Me.Name = "frmBulkInsertList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Inserción masiva"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Eventos"
    Public Event Actualizarlista()
#End Region
    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCancelar.Click
        Close()
    End Sub
    Dim IndexId As Int32
    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnFile.Click
        Try
            Dim dlg As New OpenFileDialog
            dlg.ShowDialog()
            txtfile.Text = dlg.FileName
        Catch
        End Try
    End Sub


    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnaceptar.Click
        Try
            Dim t1 As New Process(AddressOf Procesar)
            t1.Invoke(txtfile.Text.Trim)
            If Cursor Is Cursors.Default Then
                Dispose()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Delegate Sub Process(ByVal filename As String)

    Private Sub Procesar(ByVal filename As String)
        Dim noInsertados As New Generic.List(Of String)
        Try
            Cursor = Cursors.WaitCursor
            Dim sr As IO.StreamReader
            If IO.File.Exists(txtfile.Text.Trim) Then
                sr = New IO.StreamReader(txtfile.Text.Trim)
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
                        If Not AutoSubstitutionBusiness.InsertIntoIListAsBoolean(IndexId, linea) Then
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

            Cursor = Cursors.Default

            RaiseEvent Actualizarlista()

        End Try
    End Sub

End Class
