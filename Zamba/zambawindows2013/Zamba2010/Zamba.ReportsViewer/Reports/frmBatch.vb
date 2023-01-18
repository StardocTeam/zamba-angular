Imports System.Windows.Forms.Form

Public Class frmBatch
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents btnaceptar As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.btnaceptar = New System.Windows.Forms.Button
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(48, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Lote"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(120, 32)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(112, 20)
        Me.TextBox1.TabIndex = 4
        Me.TextBox1.Text = ""
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox1
        '
        Me.CheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(56, 144)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(168, 16)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Mostrar todos los lotes"
        '
        'btnaceptar
        '
        Me.btnaceptar.Location = New System.Drawing.Point(152, 176)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(88, 23)
        Me.btnaceptar.TabIndex = 6
        Me.btnaceptar.Text = "Aceptar"
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(48, 64)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(184, 69)
        Me.ListBox1.TabIndex = 7
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(248, 64)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.TabIndex = 8
        Me.btnRemove.Text = "Quitar"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(248, 32)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "Agregar"
        '
        'frmBatch
        '
        Me.AcceptButton = Me.btnaceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(352, 221)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.btnaceptar)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmBatch"
        Me.Text = "Selecciones el Lote"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event Batch(ByVal dato() As String, ByVal All As Boolean)
    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        Try
            If MyClass.CheckBox1.Checked = True Then
                RaiseEvent Batch(Nothing, True)
            Else
                If Me.ListBox1.Items.Count = 0 Then
                    MessageBox.Show("Debe Ingresar algun valor", "informes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Dim Lotes(Me.ListBox1.Items.Count - 1) As String
                For i As Int32 = 0 To Me.ListBox1.Items.Count - 1
                    Lotes(i) = Me.ListBox1.Items.Item(i)
                Next
                RaiseEvent Batch(Lotes, False)
            End If
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If MyClass.CheckBox1.Checked = True Then
            Me.TextBox1.Enabled = False
            Me.btnAdd.Enabled = False
            Me.btnRemove.Enabled = False
            Me.ListBox1.Enabled = False
        Else
            Me.TextBox1.Enabled = True
            Me.btnAdd.Enabled = True
            Me.btnRemove.Enabled = True
            Me.ListBox1.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.ListBox1.Items.Add(Me.TextBox1.Text)
            Me.TextBox1.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            Me.ListBox1.Items.Remove(Me.ListBox1.SelectedItem)
        Catch ex As Exception
        End Try
    End Sub
End Class
