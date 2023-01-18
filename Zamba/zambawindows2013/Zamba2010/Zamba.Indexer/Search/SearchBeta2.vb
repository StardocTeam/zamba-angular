'Imports ZAMBA.Controls
Imports Zamba.Core.Search
Imports Zamba.Core
Imports Zamba.Indexs
Imports System.Collections.Generic
Imports Zamba.Core.Searchs

Public Class SearchBeta2
    Inherits ZControl

#Region " Windows Form Designer generated code "

    Public Event ShowTasksSearch(ByVal data As DataTable, Search As Search, taskCount As Integer)

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RemoveHandler MyBase.Load, AddressOf SearchBeta2_Load


                If btnLimpiar IsNot Nothing Then
                    RemoveHandler btnLimpiar.Click, AddressOf ZButton1_Click
                    btnLimpiar.Dispose()
                    btnLimpiar = Nothing
                End If

                If IndexController IsNot Nothing Then
                    RemoveHandler IndexController.EnterPressed, AddressOf Enter_KeyDown
                    IndexController.Dispose()
                    IndexController = Nothing
                End If

                If UCDocTypes IsNot Nothing Then
                    UCDocTypes.Dispose()
                    UCDocTypes = Nothing
                End If

                If TabIndices IsNot Nothing Then
                    TabIndices.Dispose()
                    TabIndices = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabIndices As ZPanel
    Friend WithEvents Panel12 As ZPanel
    Public WithEvents btnBuscar As ZButton
    Friend WithEvents ZColorPanel1 As ZPanel
    Friend WithEvents txtSearchInAll As TextBox
    Friend WithEvents ZWhitePanel1 As ZPanel
    Public WithEvents btnBuscarDocs As ZButton

    Friend WithEvents btnLimpiar As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel12 = New ZPanel()
        btnBuscar = New ZButton()
        btnLimpiar = New ZButton()
        TabIndices = New ZPanel()
        ZColorPanel1 = New ZPanel()
        btnBuscarDocs = New ZButton()
        txtSearchInAll = New TextBox()
        ZWhitePanel1 = New ZPanel()
        Panel12.SuspendLayout()
        ZColorPanel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel12
        '
        Panel12.BackColor = System.Drawing.Color.White
        Panel12.Controls.Add(btnBuscar)
        Panel12.Controls.Add(btnLimpiar)
        Panel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel12.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Panel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Panel12.Location = New System.Drawing.Point(2, 838)
        Panel12.Name = "Panel12"
        Panel12.Size = New System.Drawing.Size(1310, 26)
        Panel12.TabIndex = 0
        '
        'btnBuscar
        '
        btnBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand
        btnBuscar.Dock = System.Windows.Forms.DockStyle.Fill
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnBuscar.ForeColor = System.Drawing.Color.White
        btnBuscar.Location = New System.Drawing.Point(0, 0)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New System.Drawing.Size(1049, 26)
        btnBuscar.TabIndex = 13
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = False
        '
        'btnLimpiar
        '
        btnLimpiar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand
        btnLimpiar.Dock = System.Windows.Forms.DockStyle.Right
        btnLimpiar.FlatStyle = FlatStyle.Flat
        btnLimpiar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnLimpiar.ForeColor = System.Drawing.Color.White
        btnLimpiar.Location = New System.Drawing.Point(1049, 0)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New System.Drawing.Size(261, 26)
        btnLimpiar.TabIndex = 14
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        'TabIndices
        '
        TabIndices.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        TabIndices.Dock = System.Windows.Forms.DockStyle.Fill
        TabIndices.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TabIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        TabIndices.Location = New System.Drawing.Point(2, 55)
        TabIndices.Name = "TabIndices"
        TabIndices.Size = New System.Drawing.Size(1310, 783)
        TabIndices.TabIndex = 0
        '
        'ZColorPanel1
        '
        ZColorPanel1.BackColor = System.Drawing.Color.White
        ZColorPanel1.Controls.Add(btnBuscarDocs)
        ZColorPanel1.Controls.Add(txtSearchInAll)
        ZColorPanel1.Dock = System.Windows.Forms.DockStyle.Top
        ZColorPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZColorPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZColorPanel1.Location = New System.Drawing.Point(2, 2)
        ZColorPanel1.Name = "ZColorPanel1"
        ZColorPanel1.Size = New System.Drawing.Size(1310, 41)
        ZColorPanel1.TabIndex = 20
        '
        'btnBuscarDocs
        '
        btnBuscarDocs.Anchor = System.Windows.Forms.AnchorStyles.Right
        btnBuscarDocs.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnBuscarDocs.Cursor = System.Windows.Forms.Cursors.Hand
        btnBuscarDocs.FlatStyle = FlatStyle.Flat
        btnBuscarDocs.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnBuscarDocs.ForeColor = System.Drawing.Color.White
        btnBuscarDocs.Location = New System.Drawing.Point(1002, 3)
        btnBuscarDocs.Name = "btnBuscarDocs"
        btnBuscarDocs.Size = New System.Drawing.Size(154, 28)
        btnBuscarDocs.TabIndex = 15
        btnBuscarDocs.Text = "Búsqueda General"
        btnBuscarDocs.UseVisualStyleBackColor = False
        '
        'txtSearchInAll
        '
        txtSearchInAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtSearchInAll.BackColor = System.Drawing.Color.White
        txtSearchInAll.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSearchInAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        txtSearchInAll.Location = New System.Drawing.Point(47, 7)
        txtSearchInAll.Name = "txtSearchInAll"
        txtSearchInAll.Size = New System.Drawing.Size(949, 21)
        txtSearchInAll.TabIndex = 20
        '
        'ZWhitePanel1
        '
        ZWhitePanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZWhitePanel1.Dock = System.Windows.Forms.DockStyle.Top
        ZWhitePanel1.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZWhitePanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZWhitePanel1.Location = New System.Drawing.Point(2, 43)
        ZWhitePanel1.Name = "ZWhitePanel1"
        ZWhitePanel1.Size = New System.Drawing.Size(1310, 12)
        ZWhitePanel1.TabIndex = 1
        '
        'SearchBeta2
        '
        BackColor = System.Drawing.Color.White
        CausesValidation = False
        Controls.Add(TabIndices)
        Controls.Add(ZWhitePanel1)
        Controls.Add(ZColorPanel1)
        Controls.Add(Panel12)
        Font = New Font("Arial", 9.75!)
        Name = "SearchBeta2"
        Padding = New System.Windows.Forms.Padding(2)
        Size = New System.Drawing.Size(1314, 866)
        Panel12.ResumeLayout(False)
        ZColorPanel1.ResumeLayout(False)
        ZColorPanel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal UCDocTypes As UCDocTypes)
        MyBase.New()
        Me.UCDocTypes = UCDocTypes
        InitializeComponent()

        Search_Load()
    End Sub

#Region "Load"
    Private Sub Search_Load()
        Try
            IndexController.Dock = DockStyle.Fill
            IndexController.Size = Size
            TabIndices.Controls.Add(IndexController)
            IndexController.AutoScroll = True
            IndexController.BringToFront()
            RemoveHandler IndexController.EnterPressed, AddressOf Enter_KeyDown
            AddHandler IndexController.EnterPressed, AddressOf Enter_KeyDown
            Dim tooltip1 As New ToolTip()
            tooltip1.SetToolTip(btnBuscarDocs, "Permite realizar busquedas de palabras indexadas en documentos e imagenes")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Declarations"
    Dim UCDocTypes As UCDocTypes = Nothing
    Public IndexController As New IndexController
#End Region

#Region "Events"
    Private Sub Enter_KeyDown()
        If btnBuscar.Enabled Then
            Try
                Cursor = Cursors.WaitCursor
                NewIndexSearch(SelectedDocTypes, 0)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Cursor = Cursors.Default
            End Try

        End If
    End Sub

#Region "Control de Results"

    Private Shared Sub getindex(ByRef Result As Result)
        If IsNothing(Result.Index) = False AndAlso Result.Indexs.Count > 0 Then
        Else
            Dim RB As New Results_Business
            RB.LoadIndexs(DirectCast(Result, Result))
            RB.CompleteIndexData(Result)
            RB = Nothing
        End If
    End Sub

#End Region

    Public Sub ClearIndexs()
        Try
            If IndexController IsNot Nothing Then IndexController.ClearIndexs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Search"

    ''' <summary>
    ''' [Sebastian] 09-06-2009 se agrego parse para salvar warning
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property SelectedDocTypes() As List(Of IDocType)
        Get
            Dim i As Int32
            Dim SelDoc As New List(Of IDocType)
            For Each crow As Object In UCDocTypes.ListBox1.SelectedItems
                If crow.GetType().Name = "DataRowView" Then
                    Dim row As DataRowView = crow
                    SelDoc.Add(New Core.DocType(row("Doc_Type_Id"), row("Doc_Type_Name"), row("Icon_Id")))
                Else
                    Dim row As DataRow = crow
                    SelDoc.Add(New Core.DocType(row("Doc_Type_Id"), row("Doc_Type_Name"), row("Icon_Id")))
                End If
            Next
            Return SelDoc
        End Get
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBuscar.Click, btnBuscarDocs.Click
        Try

            Cursor = Cursors.WaitCursor
            Dim tooltip1 As New ToolTip()
            tooltip1.SetToolTip(btnBuscarDocs, "Permite realizar busquedas de palabras indexadas en documentos e imagenes")
            NewIndexSearch(SelectedDocTypes, 0)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#Region "Search types"
    Public Sub NewIndexSearch(ByVal SelectedDocTypes As List(Of IDocType), ByVal LastPage As Int32)
        Try
            If IndexController.IsValid Then

                Dim ar As List(Of IIndex)
                ar = IndexController.GetSearchIndexs()
                Dim dt As DataTable
                Dim UserID As Long = UserBusiness.Rights.CurrentUser.ID
                Dim CantidadFilas As Int32 = Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))

                If SelectedDocTypes.Count > 0 Then

                    Dim Search As New Search(ar, txtSearchInAll.Text, SelectedDocTypes, True, String.Empty)
                    Search.Name = LastSearchBusiness.GetSearchName(Search)

                    If Search IsNot Nothing Then
                        '       If Not LastSearchBusiness.LastSearchAlreadyExist(Search.Name) Then
                        dt = ModDocuments.DoSearch(Search, UserID, New Filters.FiltersComponent, LastPage, CantidadFilas, False, False, FilterTypes.Document, False, Nothing, True, False)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            RaiseEvent ShowTasksSearch(dt, Search, dt.MinimumCapacity)
                        Else
                            ZClass.RaiseInfo("No hay resultados, para la busqueda realizada", "Busqueda")
                        End If
                        'Else
                        '    Dim ls As LastSearch = LastSearchBusiness.GetLastSearchByName(Search.Name)
                        '    Search = LastSearchBusiness.GetSerializedSearchObject(ls.Id)
                        '    ModDocuments.ReLoad(ls, Search)
                        'End If
                    End If
                Else
                    'Si se llego a esta instancia quiere decir que se perdio el SelectedDocTypes o el usuario no poseia los permisos necesarios.
                    MessageBox.Show("No Seleccionó ninguna Entidad para buscar. Seleccione una para poder continuar.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Question)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
#End Region

#Region "Borra los datos de los atributos"
    Private Sub ZButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnLimpiar.Click
        Try
            IndexController.CleanIndexs()
            txtSearchInAll.Clear()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Private Sub SearchBeta2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        IndexController.BringToFront()

    End Sub


End Class
