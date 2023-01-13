'Imports ZAMBA.Controls
Imports Zamba.Core.Search
Imports ZAMBA.Core
Imports ZAMBA.AppBlock
Imports System.data
Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Runtime.InteropServices
Imports Zamba.Indexs


Public Class SearchBeta2
    Inherits ZControl

#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '    Friend WithEvents Splitter2 As ZSplitter
    '   Friend WithEvents Panel9 As ZBluePanel

    Friend WithEvents TabIndices As ZWhitePanel
    '    Friend WithEvents IconList As System.Windows.Forms.ImageList
    '    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    '   Friend WithEvents Diseño As System.Windows.Forms.ImageList
    'Friend WithEvents CtrlImageList As System.Windows.Forms.ImageList
    '    Friend WithEvents tabControl1 As ZTabs
    ' Friend WithEvents Panel11 As ZBluePanel
    Friend WithEvents Panel12 As ZColorPanel
    Friend WithEvents ZBluePanel4 As Zamba.AppBlock.ZColorPanel
    Friend WithEvents chkallwords As System.Windows.Forms.CheckBox
    Friend WithEvents txtnotesearch As System.Windows.Forms.TextBox
    Friend WithEvents txttextsearch As System.Windows.Forms.TextBox
    'Friend WithEvents PanelIndices As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents btnBuscar As ZButton
    Friend WithEvents ZColorPanel1 As Zamba.AppBlock.ZColorPanel
    Friend WithEvents chkBusquedaAnd As System.Windows.Forms.CheckBox
    Friend WithEvents chkInAllDocTypes As System.Windows.Forms.CheckBox
    Friend WithEvents lblSearchInAll As System.Windows.Forms.Label
    Friend WithEvents txtSearchInAll As System.Windows.Forms.TextBox
    Friend WithEvents ZWhitePanel1 As Zamba.AppBlock.ZWhitePanel
    Public WithEvents btnBuscarDocs As Zamba.AppBlock.ZButton
    Friend WithEvents ChkSearchInTaks As System.Windows.Forms.CheckBox
    Friend WithEvents btnLimpiar As ZButton1
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel12 = New Zamba.AppBlock.ZColorPanel
        Me.btnBuscar = New Zamba.AppBlock.ZButton
        Me.btnLimpiar = New Zamba.AppBlock.ZButton1
        Me.TabIndices = New Zamba.AppBlock.ZWhitePanel
        Me.ZColorPanel1 = New Zamba.AppBlock.ZColorPanel
        Me.btnBuscarDocs = New Zamba.AppBlock.ZButton
        Me.chkBusquedaAnd = New System.Windows.Forms.CheckBox
        Me.chkInAllDocTypes = New System.Windows.Forms.CheckBox
        Me.lblSearchInAll = New System.Windows.Forms.Label
        Me.txtSearchInAll = New System.Windows.Forms.TextBox
        Me.ZBluePanel4 = New Zamba.AppBlock.ZColorPanel
        Me.ChkSearchInTaks = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkallwords = New System.Windows.Forms.CheckBox
        Me.txtnotesearch = New System.Windows.Forms.TextBox
        Me.txttextsearch = New System.Windows.Forms.TextBox
        Me.ZWhitePanel1 = New Zamba.AppBlock.ZWhitePanel
        Me.Panel12.SuspendLayout()
        Me.ZColorPanel1.SuspendLayout()
        Me.ZBluePanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel12
        '
        Me.Panel12.Color1 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel12.Color2 = System.Drawing.Color.White
        Me.Panel12.Controls.Add(Me.btnBuscar)
        Me.Panel12.Controls.Add(Me.btnLimpiar)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel12.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.Panel12.Location = New System.Drawing.Point(2, 520)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(884, 26)
        Me.Panel12.TabIndex = 0
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.Transparent
        Me.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBuscar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnBuscar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBuscar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.ForeColor = System.Drawing.Color.Black
        Me.btnBuscar.Location = New System.Drawing.Point(0, 0)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(623, 26)
        Me.btnBuscar.TabIndex = 13
        Me.btnBuscar.Text = "Buscar Documentos"
        '
        'btnLimpiar
        '
        Me.btnLimpiar.BackColor = System.Drawing.Color.Transparent
        Me.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLimpiar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLimpiar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.ForeColor = System.Drawing.Color.Black
        Me.btnLimpiar.Location = New System.Drawing.Point(623, 0)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(261, 26)
        Me.btnLimpiar.TabIndex = 14
        Me.btnLimpiar.Text = "Limpiar Búsqueda"
        '
        'TabIndices
        '
        Me.TabIndices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabIndices.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabIndices.Location = New System.Drawing.Point(2, 76)
        Me.TabIndices.Name = "TabIndices"
        Me.TabIndices.Size = New System.Drawing.Size(884, 353)
        Me.TabIndices.TabIndex = 0
        '
        'ZColorPanel1
        '
        Me.ZColorPanel1.BackColor = System.Drawing.Color.Transparent
        Me.ZColorPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ZColorPanel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ZColorPanel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ZColorPanel1.Controls.Add(Me.btnBuscarDocs)
        Me.ZColorPanel1.Controls.Add(Me.chkBusquedaAnd)
        Me.ZColorPanel1.Controls.Add(Me.chkInAllDocTypes)
        Me.ZColorPanel1.Controls.Add(Me.lblSearchInAll)
        Me.ZColorPanel1.Controls.Add(Me.txtSearchInAll)
        Me.ZColorPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZColorPanel1.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.ZColorPanel1.Location = New System.Drawing.Point(2, 2)
        Me.ZColorPanel1.Name = "ZColorPanel1"
        Me.ZColorPanel1.Size = New System.Drawing.Size(884, 62)
        Me.ZColorPanel1.TabIndex = 20
        '
        'btnBuscarDocs
        '
        Me.btnBuscarDocs.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnBuscarDocs.BackColor = System.Drawing.Color.Transparent
        Me.btnBuscarDocs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBuscarDocs.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnBuscarDocs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarDocs.ForeColor = System.Drawing.Color.Black
        Me.btnBuscarDocs.Location = New System.Drawing.Point(749, 6)
        Me.btnBuscarDocs.Name = "btnBuscarDocs"
        Me.btnBuscarDocs.Size = New System.Drawing.Size(126, 22)
        Me.btnBuscarDocs.TabIndex = 15
        Me.btnBuscarDocs.Text = "Buscar"
        '
        'chkBusquedaAnd
        '
        Me.chkBusquedaAnd.BackColor = System.Drawing.Color.Transparent
        Me.chkBusquedaAnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBusquedaAnd.Location = New System.Drawing.Point(442, 29)
        Me.chkBusquedaAnd.Name = "chkBusquedaAnd"
        Me.chkBusquedaAnd.Size = New System.Drawing.Size(202, 23)
        Me.chkBusquedaAnd.TabIndex = 23
        Me.chkBusquedaAnd.Text = "Todas las palabras"
        Me.chkBusquedaAnd.UseVisualStyleBackColor = False
        '
        'chkInAllDocTypes
        '
        Me.chkInAllDocTypes.BackColor = System.Drawing.Color.Transparent
        Me.chkInAllDocTypes.Checked = True
        Me.chkInAllDocTypes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInAllDocTypes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInAllDocTypes.Location = New System.Drawing.Point(149, 28)
        Me.chkInAllDocTypes.Name = "chkInAllDocTypes"
        Me.chkInAllDocTypes.Size = New System.Drawing.Size(287, 25)
        Me.chkInAllDocTypes.TabIndex = 22
        Me.chkInAllDocTypes.Text = "Buscar solamente en los tipos seleccionados"
        Me.chkInAllDocTypes.UseVisualStyleBackColor = False
        '
        'lblSearchInAll
        '
        Me.lblSearchInAll.AutoSize = True
        Me.lblSearchInAll.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchInAll.Location = New System.Drawing.Point(5, 7)
        Me.lblSearchInAll.Name = "lblSearchInAll"
        Me.lblSearchInAll.Size = New System.Drawing.Size(106, 13)
        Me.lblSearchInAll.TabIndex = 21
        Me.lblSearchInAll.Text = "Búsqueda en Índices"
        '
        'txtSearchInAll
        '
        Me.txtSearchInAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchInAll.BackColor = System.Drawing.Color.White
        Me.txtSearchInAll.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchInAll.ForeColor = System.Drawing.Color.Black
        Me.txtSearchInAll.Location = New System.Drawing.Point(149, 4)
        Me.txtSearchInAll.Name = "txtSearchInAll"
        Me.txtSearchInAll.Size = New System.Drawing.Size(596, 21)
        Me.txtSearchInAll.TabIndex = 20
        '
        'ZBluePanel4
        '
        Me.ZBluePanel4.BackColor = System.Drawing.Color.Transparent
        Me.ZBluePanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ZBluePanel4.Color1 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ZBluePanel4.Color2 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ZBluePanel4.Controls.Add(Me.ChkSearchInTaks)
        Me.ZBluePanel4.Controls.Add(Me.Label2)
        Me.ZBluePanel4.Controls.Add(Me.Label1)
        Me.ZBluePanel4.Controls.Add(Me.chkallwords)
        Me.ZBluePanel4.Controls.Add(Me.txtnotesearch)
        Me.ZBluePanel4.Controls.Add(Me.txttextsearch)
        Me.ZBluePanel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZBluePanel4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.ZBluePanel4.Location = New System.Drawing.Point(2, 429)
        Me.ZBluePanel4.Name = "ZBluePanel4"
        Me.ZBluePanel4.Size = New System.Drawing.Size(884, 91)
        Me.ZBluePanel4.TabIndex = 19
        '
        'ChkSearchInTaks
        '
        Me.ChkSearchInTaks.BackColor = System.Drawing.Color.Transparent
        Me.ChkSearchInTaks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSearchInTaks.Location = New System.Drawing.Point(147, 3)
        Me.ChkSearchInTaks.Name = "ChkSearchInTaks"
        Me.ChkSearchInTaks.Size = New System.Drawing.Size(163, 20)
        Me.ChkSearchInTaks.TabIndex = 15
        Me.ChkSearchInTaks.Text = "Buscar en tareas"
        Me.ChkSearchInTaks.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(134, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Búsqueda de Notas y Foro"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Búsqueda de Texto"
        '
        'chkallwords
        '
        Me.chkallwords.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkallwords.BackColor = System.Drawing.Color.Transparent
        Me.chkallwords.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkallwords.Location = New System.Drawing.Point(749, 33)
        Me.chkallwords.Name = "chkallwords"
        Me.chkallwords.Size = New System.Drawing.Size(119, 20)
        Me.chkallwords.TabIndex = 9
        Me.chkallwords.Text = "Todas las palabras"
        Me.chkallwords.UseVisualStyleBackColor = False
        '
        'txtnotesearch
        '
        Me.txtnotesearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtnotesearch.BackColor = System.Drawing.Color.White
        Me.txtnotesearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnotesearch.ForeColor = System.Drawing.Color.Black
        Me.txtnotesearch.Location = New System.Drawing.Point(147, 59)
        Me.txtnotesearch.Name = "txtnotesearch"
        Me.txtnotesearch.Size = New System.Drawing.Size(596, 21)
        Me.txtnotesearch.TabIndex = 1
        '
        'txttextsearch
        '
        Me.txttextsearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txttextsearch.BackColor = System.Drawing.Color.White
        Me.txttextsearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttextsearch.ForeColor = System.Drawing.Color.Black
        Me.txttextsearch.Location = New System.Drawing.Point(147, 32)
        Me.txttextsearch.Name = "txttextsearch"
        Me.txttextsearch.Size = New System.Drawing.Size(596, 21)
        Me.txttextsearch.TabIndex = 0
        '
        'ZWhitePanel1
        '
        Me.ZWhitePanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZWhitePanel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZWhitePanel1.Location = New System.Drawing.Point(2, 64)
        Me.ZWhitePanel1.Name = "ZWhitePanel1"
        Me.ZWhitePanel1.Size = New System.Drawing.Size(884, 12)
        Me.ZWhitePanel1.TabIndex = 1
        '
        'SearchBeta2
        '
        Me.BackColor = System.Drawing.Color.White
        Me.CausesValidation = False
        Me.Controls.Add(Me.TabIndices)
        Me.Controls.Add(Me.ZWhitePanel1)
        Me.Controls.Add(Me.ZColorPanel1)
        Me.Controls.Add(Me.ZBluePanel4)
        Me.Controls.Add(Me.Panel12)
        Me.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Name = "SearchBeta2"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(888, 548)
        Me.Panel12.ResumeLayout(False)
        Me.ZColorPanel1.ResumeLayout(False)
        Me.ZColorPanel1.PerformLayout()
        Me.ZBluePanel4.ResumeLayout(False)
        Me.ZBluePanel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim UCDocTypes As UCDocTypes
    Public Sub New(ByVal UCDocTypes As UCDocTypes)
        MyBase.New()
        Me.UCDocTypes = UCDocTypes
        InitializeComponent()
        ' Me.Visible = False
        chkallwords.Visible = False
        Search_Load()
    End Sub
#Region "Load"
    Private Sub Search_Load()
        '      Me.Visible = True
        Try
            Me.IndexController.Dock = DockStyle.Fill
            Me.TabIndices.Controls.Add(Me.IndexController)
            Me.IndexController.AutoScroll = True
            Me.IndexController.BringToFront()
            RemoveHandler IndexController.EnterPressed, AddressOf Enter_KeyDown
            AddHandler IndexController.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler IndexController.TabPressed, AddressOf Tab_KeyDown

            '[Sebastian] 23-10-2009 Oculta el panel de busqueda en indices.
            Me.ZColorPanel1.Visible = Boolean.Parse(UserPreferences.getValue("EnableSearchIntoIndexs", Sections.UserPreferences, "True"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Declarations"
    'Dim IndexList As New ArrayList
    ' Dim IndexTypeList As New ArrayList
    ' Dim IndexLenList As New ArrayList
#End Region

#Region "Events"
    Private Sub Enter_KeyDown()
        If Me.btnBuscar.Enabled Then
            Try
                Me.Cursor = Cursors.WaitCursor
                NewIndexSearch(SelectedDocTypes, 0)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Me.Cursor = Cursors.Default
            End Try

        End If
    End Sub

#Region "Control de Results"
    Private Shared Sub getindex(ByRef Result As Result)
        If IsNothing(Result.Index) = False AndAlso Result.Indexs.Count > 0 Then
        Else
            Results_Business.LoadIndexs(DirectCast(Result, Result))
            Results_Business.CompleteIndexData(Result)
        End If
    End Sub

#End Region

    Public Sub ClearIndexs()
        Try
            Me.IndexController.ClearIndexs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
#Region "Index"
    Friend Sub ClearIndexVariables()
        Try
            ' IndexList.Clear()
            ' IndexTypeList.Clear()
            'IndexLenList.Clear()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public IndexController As New IndexController

#End Region

#Region "Search"

    ''' <summary>
    ''' [Sebastian] 09-06-2009 se agrego parse para salvar warning
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property SelectedDocTypes() As ArrayList
        Get
            Dim i As Int32
            Dim SelDoc As New ArrayList
            For i = 0 To UcDocTypes.ListBox1.SelectedIndices.Count - 1
                Dim Index As Int32 = UcDocTypes.ListBox1.SelectedIndices(i)
                SelDoc.Add(New Core.DocType(Int32.Parse(UcDocTypes.DSDocType.DocTypes(Index).Doc_Type_Id.ToString), UcDocTypes.DSDocType.DocTypes(Index).Doc_Type_Name, Int32.Parse(UcDocTypes.DSDocType.DocTypes(Index).Icon_Id.ToString)))
            Next
            Return SelDoc
        End Get
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click, btnBuscarDocs.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            NewIndexSearch(SelectedDocTypes, 0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#Region "Search types"
    Public Sub NewIndexSearch(ByVal SelectedDocTypes As ArrayList, ByVal LastPage As Int32)
        Try
            If IndexController.IsValid Then

                Dim ar() As Index
                Try
                    ar = Me.IndexController.GetSearchIndexs()
                Catch
                    Exit Sub
                End Try

                If ar.Length > 0 OrElse txtnotesearch.Text.Trim <> "" OrElse txttextsearch.Text.Trim <> "" OrElse txtSearchInAll.Text.Trim <> "" Then
                    Dim Search As New Searchs.Search(ar, Me.txtSearchInAll.Text, Me.chkInAllDocTypes.Checked, Me.chkBusquedaAnd.Checked, Me.txttextsearch.Text, True, Me.txtnotesearch.Text, "", SelectedDocTypes, True, "")
                    Zamba.Core.Search.ModDocuments.DoSearch(Search, UserBusiness.Rights.CurrentUser.ID, New Filters.FiltersComponent, LastPage, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)), , , Me.ChkSearchInTaks.Checked)
                Else
                    If MessageBox.Show("No ingresó ningún dato para buscar. ¿Desea continuar?", "Búsqueda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim Search As New Searchs.Search(ar, Me.txtSearchInAll.Text, Me.chkInAllDocTypes.Checked, Me.chkBusquedaAnd.Checked, Me.txttextsearch.Text, True, Me.txtnotesearch.Text, "", SelectedDocTypes, True, "")
                        Zamba.Core.Search.ModDocuments.DoSearch(Search, Zamba.Core.UserBusiness.CurrentUser.ID, New Filters.FiltersComponent, LastPage, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)), , , Me.ChkSearchInTaks.Checked, True)
                    End If
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub AbortSearch()
        '        ModDocuments.AbortSearch()
    End Sub
#End Region
#End Region

#Region "Borra los datos de los indices"
    Private Sub ZButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Try
            Me.IndexController.CleanIndexs()
            Me.txttextsearch.Text = ""
            Me.txtnotesearch.Text = ""
            Me.chkallwords.Checked = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Private Sub ZButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.CleanNoteSearch()
    'End Sub
    'Private Sub CleanNoteSearch()
    '    Try
    '        Me.txtnotesearch.Text = ""
    '    Catch ex As Exception
    '        zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Sub CleanTextSearch()
    '    Try
    '        Me.txttextsearch.Text = ""
    '    Catch ex As Exception
    '        zclass.raiseerror(ex)
    '    End Try
    'End Sub
#End Region

    Private Sub SearchBeta2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.IndexController.BringToFront()
        Me.ChkSearchInTaks.Checked = UserPreferences.getValue("SearchInTaks", Sections.Search, True)

    End Sub

  

   
    Private Sub ChkSearchInTaks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSearchInTaks.CheckedChanged
        UserPreferences.setValue("SearchInTaks", Me.ChkSearchInTaks.Checked.ToString, Sections.Search)
    End Sub
End Class
