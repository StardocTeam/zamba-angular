Imports Zamba.AppBlock
Imports Zamba.Servers
Imports Zamba.Core

Public Class frmPaquetes
    Inherits Zamba.AppBlock.ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        startPackage = EnumPaquetes.PAQ_NoPackage
    End Sub

    Public Sub New(ByVal startPackage As EnumPaquetes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.startPackage = startPackage
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
    'Friend WithEvents Label1 As ZLabel
    'Friend WithEvents Label2 As ZLabel
    'Friend WithEvents Label3 As ZLabel
    'Friend WithEvents lbldate As ZLabel
    'Friend WithEvents lblver As ZLabel
    'Friend WithEvents ZPanel As Zamba.AppBlock.ZPanel
    'Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ZPanel As ZPanel
    Friend WithEvents chklstPaquetes As System.Windows.Forms.CheckedListBox
    Friend WithEvents lstLog As ListBox
    Friend WithEvents lblDescripcion As ZLabel
    Friend WithEvents lbldate As ZLabel
    Friend WithEvents lblver As ZLabel
    Friend WithEvents btnEjecutar As ZButton
    Friend WithEvents LblAyuda As ZLabel
    Friend WithEvents LblFecha As ZLabel
    Friend WithEvents lblVersion As ZLabel
    Friend WithEvents lblPaquetes As ZLabel
    Friend WithEvents lblLog As ZLabel
    Friend WithEvents txtfind As TextBox
    Friend WithEvents lblBuscar As ZLabel
    Friend WithEvents zbtnfind As ZButton
    Friend WithEvents lblPaquetesRelacionados As ZLabel
    Friend WithEvents lbresultados As ListBox
    Friend WithEvents lblUltimaModificacion As ZLabel
    Friend WithEvents lblmodify As ZLabel

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaquetes))
        ZPanel = New ZPanel
        lblmodify = New ZLabel
        lblver = New ZLabel
        lbldate = New ZLabel
        lblDescripcion = New ZLabel
        lblUltimaModificacion = New ZLabel
        lbresultados = New ListBox
        lblPaquetesRelacionados = New ZLabel
        zbtnfind = New ZButton
        lblBuscar = New ZLabel
        txtfind = New TextBox
        lblLog = New ZLabel
        lblPaquetes = New ZLabel
        lblVersion = New ZLabel
        LblFecha = New ZLabel
        LblAyuda = New ZLabel
        btnEjecutar = New ZButton
        lstLog = New ListBox
        chklstPaquetes = New System.Windows.Forms.CheckedListBox
        ZPanel.SuspendLayout()
        SuspendLayout()
        '
        'ZPanel
        '
        ZPanel.AutoSize = True
        ZPanel.Controls.Add(lblmodify)
        ZPanel.Controls.Add(lblver)
        ZPanel.Controls.Add(lbldate)
        ZPanel.Controls.Add(lblDescripcion)
        ZPanel.Controls.Add(lblUltimaModificacion)
        ZPanel.Controls.Add(lbresultados)
        ZPanel.Controls.Add(lblPaquetesRelacionados)
        ZPanel.Controls.Add(zbtnfind)
        ZPanel.Controls.Add(lblBuscar)
        ZPanel.Controls.Add(txtfind)
        ZPanel.Controls.Add(lblLog)
        ZPanel.Controls.Add(lblPaquetes)
        ZPanel.Controls.Add(lblVersion)
        ZPanel.Controls.Add(LblFecha)
        ZPanel.Controls.Add(LblAyuda)
        ZPanel.Controls.Add(btnEjecutar)
        ZPanel.Controls.Add(lstLog)
        ZPanel.Controls.Add(chklstPaquetes)
        ZPanel.Dock = System.Windows.Forms.DockStyle.Fill
        ZPanel.Location = New System.Drawing.Point(2, 2)
        ZPanel.Name = "ZPanel"
        ZPanel.Size = New System.Drawing.Size(735, 507)
        ZPanel.TabIndex = 0
        '
        'lblmodify
        '
        lblmodify.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblmodify.BackColor = System.Drawing.Color.Transparent
        lblmodify.Location = New System.Drawing.Point(569, 102)
        lblmodify.Name = "lblmodify"
        lblmodify.Size = New System.Drawing.Size(160, 13)
        lblmodify.TabIndex = 27
        '
        'lblver
        '
        lblver.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblver.BackColor = System.Drawing.Color.Transparent
        lblver.Location = New System.Drawing.Point(621, 32)
        lblver.Name = "lblver"
        lblver.Size = New System.Drawing.Size(102, 13)
        lblver.TabIndex = 4
        '
        'lbldate
        '
        lbldate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lbldate.BackColor = System.Drawing.Color.Transparent
        lbldate.Location = New System.Drawing.Point(569, 69)
        lbldate.Name = "lbldate"
        lbldate.Size = New System.Drawing.Size(160, 13)
        lbldate.TabIndex = 3
        '
        'lblDescripcion
        '
        lblDescripcion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblDescripcion.BackColor = System.Drawing.Color.Transparent
        lblDescripcion.Location = New System.Drawing.Point(571, 137)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New System.Drawing.Size(160, 289)
        lblDescripcion.TabIndex = 2
        '
        'lblUltimaModificacion
        '
        lblUltimaModificacion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblUltimaModificacion.AutoSize = True
        lblUltimaModificacion.BackColor = System.Drawing.Color.Transparent
        lblUltimaModificacion.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblUltimaModificacion.Location = New System.Drawing.Point(569, 89)
        lblUltimaModificacion.Name = "lblUltimaModificacion"
        lblUltimaModificacion.Size = New System.Drawing.Size(117, 13)
        lblUltimaModificacion.TabIndex = 28
        lblUltimaModificacion.Text = "Ultima Modificación"
        '
        'lbresultados
        '
        lbresultados.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lbresultados.Location = New System.Drawing.Point(8, 441)
        lbresultados.Name = "lbresultados"
        lbresultados.Size = New System.Drawing.Size(560, 56)
        lbresultados.TabIndex = 25
        '
        'lblPaquetesRelacionados
        '
        lblPaquetesRelacionados.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        lblPaquetesRelacionados.AutoSize = True
        lblPaquetesRelacionados.BackColor = System.Drawing.Color.Transparent
        lblPaquetesRelacionados.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblPaquetesRelacionados.Location = New System.Drawing.Point(8, 425)
        lblPaquetesRelacionados.Name = "lblPaquetesRelacionados"
        lblPaquetesRelacionados.Size = New System.Drawing.Size(138, 13)
        lblPaquetesRelacionados.TabIndex = 24
        lblPaquetesRelacionados.Text = "Paquetes relacionados:"
        '
        'zbtnfind
        '
        zbtnfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        zbtnfind.BackColor = System.Drawing.Color.Silver
        zbtnfind.DialogResult = System.Windows.Forms.DialogResult.None
        zbtnfind.Font = New Font("Tahoma", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        zbtnfind.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        zbtnfind.Location = New System.Drawing.Point(368, 395)
        zbtnfind.Name = "zbtnfind"
        zbtnfind.Size = New System.Drawing.Size(200, 24)
        zbtnfind.TabIndex = 23
        zbtnfind.Text = "Buscar!"
        '
        'lblBuscar
        '
        lblBuscar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        lblBuscar.AutoSize = True
        lblBuscar.BackColor = System.Drawing.Color.Transparent
        lblBuscar.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblBuscar.Location = New System.Drawing.Point(365, 349)
        lblBuscar.Name = "lblBuscar"
        lblBuscar.Size = New System.Drawing.Size(48, 13)
        lblBuscar.TabIndex = 22
        lblBuscar.Text = "Buscar:"
        '
        'txtfind
        '
        txtfind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtfind.Location = New System.Drawing.Point(368, 368)
        txtfind.Name = "txtfind"
        txtfind.Size = New System.Drawing.Size(200, 21)
        txtfind.TabIndex = 21
        '
        'lblLog
        '
        lblLog.AutoSize = True
        lblLog.BackColor = System.Drawing.Color.Transparent
        lblLog.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblLog.Location = New System.Drawing.Point(365, 8)
        lblLog.Name = "lblLog"
        lblLog.Size = New System.Drawing.Size(141, 13)
        lblLog.TabIndex = 20
        lblLog.Text = "Resultado de ejecución:"
        '
        'lblPaquetes
        '
        lblPaquetes.AutoSize = True
        lblPaquetes.BackColor = System.Drawing.Color.Transparent
        lblPaquetes.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblPaquetes.Location = New System.Drawing.Point(5, 8)
        lblPaquetes.Name = "lblPaquetes"
        lblPaquetes.Size = New System.Drawing.Size(63, 13)
        lblPaquetes.TabIndex = 19
        lblPaquetes.Text = "Paquetes:"
        '
        'lblVersion
        '
        lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblVersion.AutoSize = True
        lblVersion.BackColor = System.Drawing.Color.Transparent
        lblVersion.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblVersion.Location = New System.Drawing.Point(569, 32)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New System.Drawing.Size(52, 13)
        lblVersion.TabIndex = 18
        lblVersion.Text = "Versión:"
        '
        'LblFecha
        '
        LblFecha.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        LblFecha.AutoSize = True
        LblFecha.BackColor = System.Drawing.Color.Transparent
        LblFecha.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        LblFecha.Location = New System.Drawing.Point(569, 56)
        LblFecha.Name = "LblFecha"
        LblFecha.Size = New System.Drawing.Size(111, 13)
        LblFecha.TabIndex = 17
        LblFecha.Text = "Fecha de creación:"
        '
        'LblAyuda
        '
        LblAyuda.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        LblAyuda.AutoSize = True
        LblAyuda.BackColor = System.Drawing.Color.Transparent
        LblAyuda.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        LblAyuda.Location = New System.Drawing.Point(569, 124)
        LblAyuda.Name = "LblAyuda"
        LblAyuda.Size = New System.Drawing.Size(75, 13)
        LblAyuda.TabIndex = 16
        LblAyuda.Text = "Descripción:"
        '
        'btnEjecutar
        '
        btnEjecutar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnEjecutar.BackColor = System.Drawing.Color.Silver
        btnEjecutar.DialogResult = System.Windows.Forms.DialogResult.None
        btnEjecutar.Font = New Font("Tahoma", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnEjecutar.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnEjecutar.Location = New System.Drawing.Point(579, 465)
        btnEjecutar.Name = "btnEjecutar"
        btnEjecutar.Size = New System.Drawing.Size(144, 32)
        btnEjecutar.TabIndex = 15
        btnEjecutar.Text = "Ejecutar"
        '
        'lstLog
        '
        lstLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lstLog.BackColor = System.Drawing.Color.White
        lstLog.Location = New System.Drawing.Point(368, 32)
        lstLog.Name = "lstLog"
        lstLog.Size = New System.Drawing.Size(195, 316)
        lstLog.TabIndex = 1
        '
        'chklstPaquetes
        '
        chklstPaquetes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        chklstPaquetes.BackColor = System.Drawing.Color.White
        chklstPaquetes.Location = New System.Drawing.Point(8, 32)
        chklstPaquetes.Name = "chklstPaquetes"
        chklstPaquetes.Size = New System.Drawing.Size(344, 388)
        chklstPaquetes.TabIndex = 0
        '
        'frmActualizaciones
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(739, 511)
        Controls.Add(ZPanel)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MinimumSize = New System.Drawing.Size(747, 538)
        Name = "frmActualizaciones"
        ShowIcon = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Zamba - Paquetes de Base de Datos"
        ZPanel.ResumeLayout(False)
        ZPanel.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

#Region "Atributos"
    Dim Acts As New SortedList
    Dim startPackage As EnumPaquetes
    Dim DsPaq As New DataSet
#End Region



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            'Carga los paquetes instalados
            CheckZpaq()

            If Me.startPackage = EnumPaquetes.PAQ_NoPackage Then
                Acts = GetExistingAct()
                chklstPaquetes.CheckOnClick = False
                For Each paq As IPAQ In Acts.Values()
                    'EjecutarPaquete(paq, writer)
                    chklstPaquetes.Items.Add(paq, paq.Installed)
                Next
            Else
                chklstPaquetes.Items.Add(GetPackage(startPackage), True)
                btnEjecutar.PerformClick()
            End If

        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Trae los paquetes ya instalados. Si no existe la tabla la crea
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckZpaq()
        Dim sql As String
        Try
            DsPaq = Server.Con.ExecuteDataset(CommandType.Text, "Select * from zpaq")
        Catch ex As Exception
            Try
                If Server.isSQLServer Then
                    sql = "Create table Zpaq(Numero numeric not null,Name nvarchar(200) not null,Installed int not null, Dateinstalled nvarchar(50) Null)"
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


    ''' <summary>
    ''' Devuelve los paquetes q hay en el assembly
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExistingAct() As SortedList
        Try
            Dim Acts As New SortedList
            Dim asem As System.Reflection.Assembly = Reflection.Assembly.Load(Reflection.Assembly.GetExecutingAssembly.FullName)
            Dim typ As New ArrayList(asem.GetTypes)
            For Each t As System.Type In typ
                If t.Name.ToLower.StartsWith("paq") Then
                    Try
                        Dim PAQ As IPAQ = DirectCast(Activator.CreateInstance(t), IPAQ)
                        PAQ.Installed = IsInstalled(PAQ)
                        Acts.Add(PAQ.Orden, PAQ)
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

    ''' <summary>
    ''' Devuelve los paquetes q hay en el assembly
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPackage(ByVal package As EnumPaquetes) As IPAQ
        Try
            Dim asem As System.Reflection.Assembly = Reflection.Assembly.Load(Reflection.Assembly.GetExecutingAssembly.FullName)
            Dim typ As New ArrayList(asem.GetTypes)
            Dim PAQ As IPAQ = Nothing
            For Each t As System.Type In typ
                If String.Compare(t.Name, package.ToString) = 0 Then
                    Try
                        PAQ = DirectCast(Activator.CreateInstance(t), IPAQ)
                        PAQ.Installed = IsInstalled(PAQ)
                        Acts.Add(PAQ.Orden, PAQ)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    Exit For
                End If
            Next
            Return PAQ
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Verifica si el paquete ya fue instalado
    ''' </summary>
    ''' <param name="PAQ"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsInstalled(ByVal PAQ As IPAQ) As Boolean
        Try
            Dim dv As New DataView(DsPaq.Tables(0))
            dv.RowFilter = "Numero= " & PAQ.Number
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
            strinsert = "INSERT INTO ZPAQ (Numero,Name,Installed,DateInstalled) VALUES (" & paq.Number & ",'" & paq.Name & "',1,'" & Now.ToString("dd-MM-yyyy") & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstLog.SelectedIndexChanged
        Try
            If lstLog.SelectedIndex <> -1 Then
                MessageBox.Show(lstLog.SelectedItem.ToString, "Zamba - Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Excepción al querer mostrar error, excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chklstPaquetes.SelectedIndexChanged
        Try
            If chklstPaquetes.SelectedIndex <> -1 Then
                lblDescripcion.Text = chklstPaquetes.SelectedItem.description.ToString()
                Try
                    lbldate.Text = chklstPaquetes.SelectedItem.CreateDate.ToString
                Catch
                End Try
                Try
                    If chklstPaquetes.SelectedItem.EditDate <> "01/01/2001" Then
                        lblmodify.Text = chklstPaquetes.SelectedItem.EditDate.ToString()
                    Else
                        lblmodify.Text = "Sin Modificaciones"
                    End If
                Catch
                End Try
                Try
                    lblver.Text = chklstPaquetes.SelectedItem.ZVersion.ToString()
                Catch
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("Excepción: " & ex.Message, "Zamba - Paquetes - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnEjecutar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEjecutar.Click
        ZTrace.AddListener("Paquetes")

        Try
            Cursor = Cursors.WaitCursor
            If chklstPaquetes.CheckedItems.Count > 0 Then

                For Each paq As IPAQ In chklstPaquetes.CheckedItems
                    EjecutarPaquete(paq)
                    If paq.Installed = True Then
                        'Me.chklstPaquetes.SetItemChecked(Me.chklstPaquetes.SelectedIndex, True)
                        lstLog.Items.Add("Correcto: " & paq.Name & " se ha actualizado con éxito")
                    Else
                        lstLog.Items.Add("Error: " & paq.Name & ", verifique el log de ejecución para mayor información")
                    End If
                Next

            End If
        Catch ex As Exception
            lstLog.Items.Add(ex)
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Cursor = Cursors.Default
            ZTrace.RemoveListener("Paquetes")
        End Try
    End Sub

    Private Sub EjecutarPaquete(ByRef paquete As IPAQ, Optional ByVal writer As System.IO.StreamWriter = Nothing)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "==============================================================================================")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comenzando la ejecución del paquete: " & paquete.Name)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Hora de comienzo: " & Now.ToString)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "==============================================================================================")
        Try
            Cursor = Cursors.WaitCursor

            If paquete.Execute() Then
                If Not IsNothing(writer) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Paquete " & paquete.Name & " instalado correctamente")
                End If
                paquete.Installed = True
                SetInstalled(paquete)
            Else
                If Not IsNothing(writer) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Paquete " & paquete.Name & " no se ha instalado")
                End If
                paquete.Installed = False
            End If
        Catch ex As Exception
            If ex.Message().ToUpper.StartsWith("PAQ_") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message())
                lstLog.Items.Add(ex.Message())
            Else
                Zamba.Core.ZClass.raiseerror(ex)
                lstLog.Items.Add(ex)
            End If
            paquete.Installed = False
        Finally
            Cursor = Cursors.Default
        End Try
        ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "Hora de finalización: " & Now.ToString)
    End Sub

    Private Sub zbtnfind_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles zbtnfind.Click
        lbresultados.DataSource = FindPackage(txtfind.Text)
        lbresultados.Refresh()
    End Sub
    Private Function FindPackage(ByVal name As String) As ArrayList
        Dim vec As New ArrayList
        Dim asem As System.Reflection.Assembly = Reflection.Assembly.Load(asem.GetExecutingAssembly.FullName)
        Dim typ As New ArrayList(asem.GetTypes)
        For Each t As System.Type In typ
            If t.Name.ToLower.StartsWith("paq") Then
                Try
                    Dim PAQ As IPAQ = DirectCast(Activator.CreateInstance(t), IPAQ)
                    If PAQ.Description.ToLower.IndexOf(name.ToLower) <> -1 Then
                        vec.Add(PAQ.Name)
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            End If
        Next
        Return vec
    End Function
End Class