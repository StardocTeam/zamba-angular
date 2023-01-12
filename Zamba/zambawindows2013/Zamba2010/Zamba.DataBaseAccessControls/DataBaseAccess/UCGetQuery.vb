
Public Class UCGetQuery
    Inherits System.Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents chk1 As System.Windows.Forms.RadioButton
    Friend WithEvents Chk2 As System.Windows.Forms.RadioButton
    Friend WithEvents BtnAceptar As ZAMBA.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.chk1 = New System.Windows.Forms.RadioButton
        Me.Chk2 = New System.Windows.Forms.RadioButton
        Me.BtnAceptar = New ZAMBA.AppBlock.ZButton
        Me.SuspendLayout()
        '
        'chk1
        '
        Me.chk1.Checked = True
        Me.chk1.Location = New System.Drawing.Point(24, 24)
        Me.chk1.Name = "chk1"
        Me.chk1.Size = New System.Drawing.Size(184, 24)
        Me.chk1.TabIndex = 0
        Me.chk1.TabStop = True
        Me.chk1.Text = "Crear una nueva Consulta"
        '
        'Chk2
        '
        Me.Chk2.Location = New System.Drawing.Point(24, 56)
        Me.Chk2.Name = "Chk2"
        Me.Chk2.Size = New System.Drawing.Size(184, 24)
        Me.Chk2.TabIndex = 1
        Me.Chk2.Text = "Obtener Consulta Guardada"
        '
        'BtnAceptar
        '
        Me.BtnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.BtnAceptar.Location = New System.Drawing.Point(80, 144)
        Me.BtnAceptar.Name = "BtnAceptar"
        Me.BtnAceptar.Size = New System.Drawing.Size(72, 24)
        Me.BtnAceptar.TabIndex = 2
        Me.BtnAceptar.Text = "Aceptar"
        '
        'UCGetQuery
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(215, Byte), CType(228, Byte), CType(247, Byte))
        Me.Controls.Add(Me.BtnAceptar)
        Me.Controls.Add(Me.Chk2)
        Me.Controls.Add(Me.chk1)
        Me.Name = "UCGetQuery"
        Me.Size = New System.Drawing.Size(304, 256)
        Me.ResumeLayout(False)

    End Sub

#End Region
    ' Dim datos As DataSet
    Public Event DeshabilitarBotones()
    Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAceptar.Click
        If chk1.Checked Then
            Me.Dispose()
        End If
        If Chk2.Checked Then
            Me.Controls.Clear()
            Dim frm2 As New FrmConfig
            Me.Controls.Add(frm2)
            RaiseEvent DeshabilitarBotones()
            'Me.Parent.Visible = False
            'Me.Parent.Dispose()
            'Me.Parent.Dispose()
            'Me.Visible = False
        End If
    End Sub

    'Private Function ObtenerQuery(ByVal file As String) As String
    '    Dim DsQuery As New DsPersist
    '    Dim sql As String
    '    Try
    '        If IO.File.Exists(file) Then
    '            DsQuery.ReadXml(file)
    '        Else
    '            Windows.Forms.MessageBox.Show("El archivo no Existe", "", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '        End If
    '    Catch
    '    End Try
    '    Try
    '        Dim i As Int16
    '        sql = "Select "
    '        For i = 0 To DsQuery.SelectColumns.Rows.Count - 1
    '            sql &= DsQuery.SelectColumns.Rows(i).Item(0) & ", "
    '        Next
    '        If sql.EndsWith(" ,") Then sql = sql.Substring(0, sql.Length - 2)
    '        sql &= " from " & DsQuery.SeletTable.Rows(0).Item(0) & " Where "
    '        For i = 0 To DsQuery.Claves.Rows.Count - 1
    '            sql &= DsQuery.Claves.Rows(i).Item(0)
    '        Next
    '    Catch ex As Exception

    '    End Try
    '    Return sql
    'End Function
End Class
