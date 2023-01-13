Imports Zamba.AppBlock
Imports Zamba.Servers
Imports System.Windows.Forms

Public Class frmActualizaciones
    Inherits Zamba.AppBlock.ZForm

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
    'Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    'Friend WithEvents Button1 As ZButton
    'Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    'Friend WithEvents Label1 As System.Windows.Forms.Label
    'Friend WithEvents Label2 As System.Windows.Forms.Label
    'Friend WithEvents Label3 As System.Windows.Forms.Label
    'Friend WithEvents lbldate As System.Windows.Forms.Label
    'Friend WithEvents lblver As System.Windows.Forms.Label
    'Friend WithEvents ZBluePanel1 As Zamba.AppBlock.ZBluePanel
    'Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ZBluePanel1 As Zamba.AppBlock.ZBluePanel
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbldate As System.Windows.Forms.Label
    Friend WithEvents lblver As System.Windows.Forms.Label
    Friend WithEvents btnEjecutar As Zamba.AppBlock.ZButton
    Friend WithEvents LblAyuda As System.Windows.Forms.Label
    Friend WithEvents LblFecha As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtfind As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents zbtnfind As Zamba.AppBlock.ZButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbresultados As System.Windows.Forms.ListBox
    Friend WithEvents chkfile As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblmodify As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmActualizaciones))
        Me.ZBluePanel1 = New Zamba.AppBlock.ZBluePanel
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblmodify = New System.Windows.Forms.Label
        Me.chkfile = New System.Windows.Forms.CheckBox
        Me.lbresultados = New System.Windows.Forms.ListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.zbtnfind = New Zamba.AppBlock.ZButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtfind = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LblFecha = New System.Windows.Forms.Label
        Me.LblAyuda = New System.Windows.Forms.Label
        Me.btnEjecutar = New Zamba.AppBlock.ZButton
        Me.lblver = New System.Windows.Forms.Label
        Me.lbldate = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        Me.ZBluePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ZBluePanel1
        '
        Me.ZBluePanel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ZBluePanel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ZBluePanel1.Controls.Add(Me.Label7)
        Me.ZBluePanel1.Controls.Add(Me.lblmodify)
        Me.ZBluePanel1.Controls.Add(Me.chkfile)
        Me.ZBluePanel1.Controls.Add(Me.lbresultados)
        Me.ZBluePanel1.Controls.Add(Me.Label6)
        Me.ZBluePanel1.Controls.Add(Me.zbtnfind)
        Me.ZBluePanel1.Controls.Add(Me.Label5)
        Me.ZBluePanel1.Controls.Add(Me.txtfind)
        Me.ZBluePanel1.Controls.Add(Me.Label4)
        Me.ZBluePanel1.Controls.Add(Me.Label3)
        Me.ZBluePanel1.Controls.Add(Me.Label2)
        Me.ZBluePanel1.Controls.Add(Me.LblFecha)
        Me.ZBluePanel1.Controls.Add(Me.LblAyuda)
        Me.ZBluePanel1.Controls.Add(Me.btnEjecutar)
        Me.ZBluePanel1.Controls.Add(Me.lblver)
        Me.ZBluePanel1.Controls.Add(Me.lbldate)
        Me.ZBluePanel1.Controls.Add(Me.Label1)
        Me.ZBluePanel1.Controls.Add(Me.ListBox1)
        Me.ZBluePanel1.Controls.Add(Me.CheckedListBox1)
        Me.ZBluePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ZBluePanel1.Location = New System.Drawing.Point(2, 2)
        Me.ZBluePanel1.Name = "ZBluePanel1"
        Me.ZBluePanel1.Size = New System.Drawing.Size(740, 442)
        Me.ZBluePanel1.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(576, 248)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(152, 16)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Ultima Modificación"
        '
        'lblmodify
        '
        Me.lblmodify.BackColor = System.Drawing.Color.Transparent
        Me.lblmodify.Location = New System.Drawing.Point(576, 272)
        Me.lblmodify.Name = "lblmodify"
        Me.lblmodify.Size = New System.Drawing.Size(160, 17)
        Me.lblmodify.TabIndex = 27
        '
        'chkfile
        '
        Me.chkfile.Location = New System.Drawing.Point(592, 368)
        Me.chkfile.Name = "chkfile"
        Me.chkfile.Size = New System.Drawing.Size(120, 16)
        Me.chkfile.TabIndex = 26
        Me.chkfile.Text = "Imprimir a archivo"
        '
        'lbresultados
        '
        Me.lbresultados.Location = New System.Drawing.Point(8, 376)
        Me.lbresultados.Name = "lbresultados"
        Me.lbresultados.Size = New System.Drawing.Size(560, 56)
        Me.lbresultados.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 360)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 16)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Paquetes relacionados:"
        '
        'zbtnfind
        '
        Me.zbtnfind.BackColor = System.Drawing.Color.Silver
        Me.zbtnfind.DialogResult = System.Windows.Forms.DialogResult.None
        Me.zbtnfind.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.zbtnfind.ForeColor = System.Drawing.Color.Black
        Me.zbtnfind.Location = New System.Drawing.Point(368, 336)
        Me.zbtnfind.Name = "zbtnfind"
        Me.zbtnfind.Size = New System.Drawing.Size(200, 24)
        Me.zbtnfind.TabIndex = 23
        Me.zbtnfind.Text = "Buscar!"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(368, 280)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 16)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Buscar:"
        '
        'txtfind
        '
        Me.txtfind.Location = New System.Drawing.Point(368, 304)
        Me.txtfind.Name = "txtfind"
        Me.txtfind.Size = New System.Drawing.Size(200, 21)
        Me.txtfind.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(368, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(184, 16)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Excepciones:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(184, 16)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Paquetes:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(576, 320)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 16)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Versión:"
        '
        'LblFecha
        '
        Me.LblFecha.BackColor = System.Drawing.Color.Transparent
        Me.LblFecha.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFecha.Location = New System.Drawing.Point(576, 192)
        Me.LblFecha.Name = "LblFecha"
        Me.LblFecha.Size = New System.Drawing.Size(152, 16)
        Me.LblFecha.TabIndex = 17
        Me.LblFecha.Text = "Fecha de creación:"
        '
        'LblAyuda
        '
        Me.LblAyuda.BackColor = System.Drawing.Color.Transparent
        Me.LblAyuda.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAyuda.Location = New System.Drawing.Point(576, 24)
        Me.LblAyuda.Name = "LblAyuda"
        Me.LblAyuda.Size = New System.Drawing.Size(152, 16)
        Me.LblAyuda.TabIndex = 16
        Me.LblAyuda.Text = "Descripción:"
        '
        'btnEjecutar
        '
        Me.btnEjecutar.BackColor = System.Drawing.Color.Silver
        Me.btnEjecutar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnEjecutar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEjecutar.ForeColor = System.Drawing.Color.Black
        Me.btnEjecutar.Location = New System.Drawing.Point(584, 400)
        Me.btnEjecutar.Name = "btnEjecutar"
        Me.btnEjecutar.Size = New System.Drawing.Size(144, 32)
        Me.btnEjecutar.TabIndex = 15
        Me.btnEjecutar.Text = "Ejecutar"
        '
        'lblver
        '
        Me.lblver.BackColor = System.Drawing.Color.Transparent
        Me.lblver.Location = New System.Drawing.Point(576, 344)
        Me.lblver.Name = "lblver"
        Me.lblver.Size = New System.Drawing.Size(150, 17)
        Me.lblver.TabIndex = 4
        '
        'lbldate
        '
        Me.lbldate.BackColor = System.Drawing.Color.Transparent
        Me.lbldate.Location = New System.Drawing.Point(576, 216)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(160, 17)
        Me.lbldate.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(576, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 136)
        Me.Label1.TabIndex = 2
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.White
        Me.ListBox1.Location = New System.Drawing.Point(368, 32)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(200, 238)
        Me.ListBox1.TabIndex = 1
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.White
        Me.CheckedListBox1.Location = New System.Drawing.Point(8, 32)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(344, 324)
        Me.CheckedListBox1.TabIndex = 0
        '
        'frmActualizaciones
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(744, 446)
        Me.Controls.Add(Me.ZBluePanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmActualizaciones"
        Me.Text = "Zamba - Paquetes de Base de Datos"
        Me.ZBluePanel1.ResumeLayout(False)
        Me.ZBluePanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim Acts As New SortedList
    'Trae los paquetes ya instalados. Si no existe la tabla la crea
    Private Sub CheckZpaq()
        Dim sql As String
        Try
            DsPaq = Server.Con.ExecuteDataset(CommandType.Text, "Select * from zpaq")
        Catch ex As Exception
            Try
                If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                    sql = "Create table Zpaq(Numero numeric(9) not null,Name nvarchar(200) not null,Installed int not null, Dateinstalled nvarchar(50) Null)"
                Else
                    sql = "Create table Zpaq(Numero Number(10) not null,Name varchar2(200) not null,Installed Number(10) not null, Dateinstalled varchar2(50) Null)"
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                DsPaq = Server.Con.ExecuteDataset(CommandType.Text, "Select * from zpaq")
            Catch
                MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim writer As System.IO.StreamWriter = Nothing
        Try
            Dim server As New Server
            server.MakeConnection()
            server.dispose()

            CheckZpaq()

            Me.Acts = Me.GetExistingAct()
            Me.CheckedListBox1.CheckOnClick = False

            writer = New System.IO.StreamWriter(Application.StartupPath & "Paquetes.log")
            writer.WriteLine("Iniciando secuencia de paquetes")

            For Each paq As IPAQ In Acts.Values()
                '    EjecutarPaquete(paq, writer)
                Me.CheckedListBox1.Items.Add(paq, paq.installed)
            Next

            writer.WriteLine("Secuencia terminada")
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(writer) Then
                writer.Close()
                writer.Dispose()
                writer = Nothing
            End If
        End Try
    End Sub
    'Devuelve los paquetes q hay en el assembly
    Public Function GetExistingAct() As SortedList
        Try
            Dim Acts As New SortedList
            Dim asem As System.Reflection.Assembly = Reflection.Assembly.Load(Reflection.Assembly.GetExecutingAssembly.FullName)
            Dim typ As New ArrayList(asem.GetTypes)
            For Each t As System.Type In typ
                If t.Name.ToLower.StartsWith("paq") Then
                    Try
                        Dim PAQ As IPAQ = DirectCast(Activator.CreateInstance(t), IPAQ)
                        PAQ.installed = IsInstalled(PAQ)
                        Acts.Add(PAQ.orden, PAQ)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                End If
            Next
            Return Acts
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Dim DsPaq As New DataSet
    'Verifica si el paquete ya fue instalado
    Private Function IsInstalled(ByVal PAQ As IPAQ) As Boolean
        Try
            Dim dv As New DataView(DsPaq.Tables(0))
            dv.RowFilter = "Numero= " & PAQ.number
            If IsNothing(dv) OrElse dv.Count = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
    Private Sub SetInstalled(ByVal paq As IPAQ)
        Try
            Dim strinsert As String
            strinsert = "INSERT INTO ZPAQ (Numero,Name,Installed,DateInstalled) VALUES (" & paq.number & ",'" & paq.name & "',1,'" & Now.ToString("dd-MM-yyyy") & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            If Me.ListBox1.SelectedIndex <> -1 Then
                MessageBox.Show(Me.ListBox1.SelectedItem.ToString, "Zamba - Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Excepción al querer mostrar error, excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        Try
            If Me.CheckedListBox1.SelectedIndex <> -1 Then
                Me.Label1.Text = Me.CheckedListBox1.SelectedItem.description.ToString()
                Try
                    Me.lbldate.Text = Me.CheckedListBox1.SelectedItem.CreateDate.ToString
                Catch
                End Try
                Try
                    If Me.CheckedListBox1.SelectedItem.EditDate <> "01/01/2001" Then
                        Me.lblmodify.Text = Me.CheckedListBox1.SelectedItem.EditDate.ToString()
                    Else
                        Me.lblmodify.Text = "Sin Modificaciones"
                    End If
                Catch
                End Try
                Try
                    Me.lblver.Text = Me.CheckedListBox1.SelectedItem.ZVersion.ToString()
                Catch
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnEjecutar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEjecutar.Click
        Dim Paquete As IPAQ
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.CheckedListBox1.SelectedIndex <> -1 Then
                If Me.CheckedListBox1.CheckedItems.Contains(Me.CheckedListBox1.SelectedItem) Then

                    Paquete = DirectCast(Me.CheckedListBox1.SelectedItem, IPAQ)
                    EjecutarPaquete(Paquete)
                    If Paquete.installed = True Then
                        Me.CheckedListBox1.SetItemChecked(Me.CheckedListBox1.SelectedIndex, True)
                        MessageBox.Show("Paquete Actualizado", "Zamba - Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Errores al ejecutar el paquete", "Zamba - Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            End If
        Catch ex As Exception
            Me.ListBox1.Items.Add(ex)
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Excepción al ejecutar el paquete: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub EjecutarPaquete(ByRef paquete As IPAQ, Optional ByVal writer As System.IO.StreamWriter = Nothing)
        Try
            Me.Cursor = Cursors.WaitCursor
            'For Each dependece As Int64 In paquete.dependenciesIDs
            'If DirectCast(Me.Acts(dependece), IPAQ).installed = False Then
            'Exit Sub
            'End If
            'Next

            If paquete.execute(Me.chkfile.Checked) Then
                If Not IsNothing(writer) Then
                    writer.WriteLine("Paquete " & paquete.name & " instalado correctamente")
                End If
                paquete.installed = True
                SetInstalled(paquete)
            Else
                If Not IsNothing(writer) Then
                    writer.WriteLine("Paquete " & paquete.name & " no se ha instalado")
                End If
                paquete.installed = False
            End If
        Catch ex As Exception
            If ex.Message().ToUpper.StartsWith("PAQ_") Then
                If Not IsNothing(writer) Then
                    writer.WriteLine(ex.Message())
                Else
                    Me.ListBox1.Items.Add(ex.Message())
                End If
            Else
                Zamba.Core.ZClass.raiseerror(ex)
                Me.ListBox1.Items.Add(ex)
            End If
            paquete.installed = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub zbtnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles zbtnfind.Click
        Me.lbresultados.DataSource = Me.FindPackage(Me.txtfind.Text)
        Me.lbresultados.Refresh()
    End Sub
    Private Function FindPackage(ByVal name As String) As ArrayList
        Dim vec As New ArrayList
        Dim asem As System.Reflection.Assembly = Reflection.Assembly.Load(asem.GetExecutingAssembly.FullName)
        Dim typ As New ArrayList(asem.GetTypes)
        For Each t As System.Type In typ
            If t.Name.ToLower.StartsWith("paq") Then
                Try
                    Dim PAQ As IPAQ = Activator.CreateInstance(t)
                    '    PAQ = New NewPAQ(t.Name)
                    If PAQ.description.ToLower.IndexOf(name.ToLower) <> -1 Then
                        vec.Add(PAQ.name)
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            End If
        Next
        Return vec
    End Function
End Class