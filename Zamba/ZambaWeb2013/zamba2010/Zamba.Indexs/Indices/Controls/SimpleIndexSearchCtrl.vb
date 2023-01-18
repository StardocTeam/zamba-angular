Imports System.Drawing
Imports ZAMBA.Core
Imports ZAMBA.AppBlock
Public Class SimpleIndexSearchCtrl
    Inherits ZwhiteControl

#Region "Eventos TAB y ENTER"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Public Shadows Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

    Private Sub IndexCtrl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub Item_Changed(ByVal IndexID As Integer, ByVal NewValue As String)
        RaiseEvent ItemChanged(IndexID, NewValue)
    End Sub

    Private Sub keyDeb()

    End Sub

    'Private Sub Enter_KeyDown() Handles txtDataCtrl.EnterPressed, txtData2Ctrl.EnterPressed
    '    IndexCtrl_KeyDown(Me, New KeyEventArgs(Keys.Enter))
    'End Sub

    Private Sub Enter_KeyPress() Handles txtDataCtrl.EnterPressed, txtData2Ctrl.EnterPressed
        IndexCtrl_KeyDown(Me, New KeyEventArgs(Keys.Enter))
    End Sub


    Private Sub Tab_KeyDown() Handles txtDataCtrl.TabPressed, txtData2Ctrl.TabPressed
        IndexCtrl_KeyDown(Me, New KeyEventArgs(Keys.Tab))
    End Sub
#End Region

    Public Function isSearched() As Boolean
        If IsNothing(Index.DataTemp2) = False Then
            If (Index.DataTemp <> "" OrElse Index.DataTemp2 <> "") OrElse Index.OrderSort <> OrderSorts.Ninguno OrElse Index.[Operator].ToLower = "es nulo" OrElse Index.[Operator] = "<>" OrElse Index.[Operator].ToLower = "distinto" Then
                Index.Data = Index.DataTemp
                Index.Data2 = Index.DataTemp2
                Index.dataDescription = Index.dataDescriptionTemp
                Index.dataDescription2 = Index.dataDescriptionTemp2
                Return True
            Else
                Return False
            End If
        Else
            If Index.DataTemp <> "" OrElse Index.OrderSort <> OrderSorts.Ninguno OrElse Index.[Operator].ToLower = "es nulo" Then
                Index.Data = Index.DataTemp
                If IsNothing(Index.dataDescriptionTemp) = False Then Index.dataDescription = Index.dataDescriptionTemp
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Function isValid() As Boolean
        Try
            If IsNothing(Me.txtData2Ctrl) = False Then
                If Me.txtDataCtrl.IsValid AndAlso Me.txtData2Ctrl.IsValid Then
                    Return True
                Else
                    Return False
                End If
            Else
                If Me.txtDataCtrl.IsValid Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Function

#Region "combo"
    Public Event OperatorClicked(ByVal control As SimpleIndexSearchCtrl)
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ComboBox1.LinkClicked
        RaiseEvent OperatorClicked(Me)
    End Sub
    Public Property [Operator]() As String
        Get
            Return Index.[Operator]
        End Get
        Set(ByVal Value As String)
            Index.[Operator] = Value
            If String.Compare(Index.[Operator], "Entre") = 0 Then
                Me.__FlagEntre = True
            Else
                Me.__FlagEntre = False
            End If
            If String.Compare(Index.[Operator], "es nulo", True) = 0 Then
                Me.__FlagIsNull = True
            Else
                Me.__FlagIsNull = False
            End If
            Me.ComboBox1.Text = Index.[Operator]
            Me.ResizeTxtBoxs()
        End Set
    End Property
#End Region

#Region "Constructores"
    Private Sub New()
        MyBase.New()
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
    End Sub
    Public Sub New(ByVal index As Index)
        Me.New()
        Me.Index = index
        Init(index)
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "
    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If ComboBox1 IsNot Nothing Then
                    ComboBox1.Dispose()
                    ComboBox1 = Nothing
                End If
                If lblName IsNot Nothing Then
                    lblName.Dispose()
                    lblName = Nothing
                End If
                If txtDataCtrl IsNot Nothing Then
                    txtDataCtrl.Dispose()
                    txtDataCtrl = Nothing
                End If
                If txtData2Ctrl IsNot Nothing Then
                    txtData2Ctrl.Dispose()
                    txtData2Ctrl = Nothing
                End If
                If PanelData IsNot Nothing Then
                    PanelData.Dispose()
                    PanelData = Nothing
                End If
                If PanelData2 IsNot Nothing Then
                    PanelData2.Dispose()
                    PanelData2 = Nothing
                End If
                If ComboBox1 IsNot Nothing Then
                    ComboBox1.Dispose()
                    ComboBox1 = Nothing
                End If
                If btnSort IsNot Nothing Then
                    btnSort.Dispose()
                    btnSort = Nothing
                End If
                If Index IsNot Nothing Then
                    Index.Dispose()
                    Index = Nothing
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
    Friend WithEvents lblName As ZLabel

    'Friend WithEvents txtData As System.Windows.Forms.TextBox

    Friend WithEvents txtDataCtrl As txtBaseIndexCtrl
    Friend WithEvents txtData2Ctrl As txtBaseIndexCtrl

    Friend WithEvents PanelData As ZPanel
    Friend WithEvents PanelData2 As ZPanel

    Friend WithEvents ComboBox1 As System.Windows.Forms.LinkLabel

    'Friend WithEvents Panel1 As ZPanel
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnSort As SimpleSortButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblName = New Zamba.AppBlock.ZLabel
        Me.ComboBox1 = New System.Windows.Forms.LinkLabel
        Me.PanelData = New Zamba.AppBlock.ZPanel
        Me.PanelData2 = New Zamba.AppBlock.ZPanel
        Me.btnSort = New Zamba.Indexs.SimpleSortButton
        Me.SuspendLayout()
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.CausesValidation = False
        Me.lblName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(0, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(122, 28)
        Me.lblName.TabIndex = 0
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ComboBox1.ForeColor = System.Drawing.Color.Black
        Me.ComboBox1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ComboBox1.Location = New System.Drawing.Point(122, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(50, 28)
        Me.ComboBox1.TabIndex = 0
        Me.ComboBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ComboBox1.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'PanelData
        '
        Me.PanelData.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PanelData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelData.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelData.ForeColor = System.Drawing.Color.Black
        Me.PanelData.Location = New System.Drawing.Point(172, 0)
        Me.PanelData.Name = "PanelData"
        Me.PanelData.Size = New System.Drawing.Size(410, 28)
        Me.PanelData.TabIndex = 0
        Me.PanelData.TabStop = True
        '
        'PanelData2
        '
        Me.PanelData2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PanelData2.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelData2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelData2.ForeColor = System.Drawing.Color.Black
        Me.PanelData2.Location = New System.Drawing.Point(582, 0)
        Me.PanelData2.Name = "PanelData2"
        Me.PanelData2.Size = New System.Drawing.Size(0, 28)
        Me.PanelData2.TabIndex = 2
        '
        'btnSort
        '
        Me.btnSort.BackColor = System.Drawing.Color.White
        Me.btnSort.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSort.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSort.ForeColor = System.Drawing.Color.Black
        Me.btnSort.Location = New System.Drawing.Point(582, 0)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(26, 28)
        Me.btnSort.TabIndex = 3
        Me.btnSort.TabStop = False
        '
        'SimpleIndexSearchCtrl
        '
        Me.Controls.Add(Me.PanelData)
        Me.Controls.Add(Me.PanelData2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.btnSort)
        Me.Controls.Add(Me.lblName)
        Me.Name = "SimpleIndexSearchCtrl"
        Me.Size = New System.Drawing.Size(608, 28)
        Me.ResumeLayout(False)

    End Sub


    'Private Sub Data2Changed(ByVal data As String) Handles txtData2Ctrl.DataChanged
    '    index.Data2 = data
    'End Sub
    'Private Sub DataChanged(ByVal data As String) Handles txtDataCtrl.DataChanged
    '    index.Data = data
    'End Sub
#End Region


    Public Index As Index
    Private _docTypeID As Int32 = 0
    Private _parentValue As String = String.Empty
    Private _parentIndexs As Hashtable = Nothing

    Public Sub Init(ByVal Index As Index, ByVal DocTypeID As Int32, ByVal ParentIndexs As Hashtable)
        _docTypeID = DocTypeID
        _parentIndexs = ParentIndexs
        Init(Index)
    End Sub

    'Private tabindx As Int16
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Inicializa el control
    ''' </summary>
    ''' <param name="Index"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	08/08/2006	Modify
    '''     se agrego la linea siguiente:
    '''     Dim o As ChkIndexCtrl = New ChkIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
    '''     RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
    '''     AddHandler o.EnterPressed, AddressOf Enter_KeyPress
    '''     Me.txtDataCtrl = o
    '''     Esto fue debido a que cuando el control invocaba un RaiseEvent EnterPressed
    '''     el evento no era posible atraparlo atrapado
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Init(ByVal Index As Index)
        Me.lblName.Text = Index.Name
        ' Me.LoadComboInfo(Me.ComboBox1)
        Me.Sort = Index.OrderSort
        Me.ComboBox1.Text = Index.[Operator]
        'Me.ComboBox1.SelectionLength = 0
        '  Try
        '  Me.ComboBox1.SelectionStart = 0
        '  Catch ex As System.ArgumentException
        '  End Try
        If Index.Type = IndexDataType.Si_No Then
            Me.ComboBox1.Enabled = False
            'Me.txtDataCtrl = New ChkIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
            Dim o As ChkIndexCtrl = New ChkIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
            RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o.EnterPressed, AddressOf Enter_KeyPress
            'RemoveHandler o.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler o.EnterPressed, AddressOf Enter_KeyDown

            Me.txtDataCtrl = o

            Me.txtDataCtrl.Dock = DockStyle.Fill
            Me.PanelData.Controls.Add(Me.txtDataCtrl)
        ElseIf Index.Type = IndexDataType.Fecha_Hora Then

            'Me.txtDataCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
            Dim o As txtBaseIndexCtrl = New txtDateTimeIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
            RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o.EnterPressed, AddressOf Enter_KeyPress
            'RemoveHandler o.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler o.EnterPressed, AddressOf Enter_KeyDown

            Me.txtDataCtrl = o

            'Me.txtData2Ctrl = New txtIndexCtrl(Index, True, txtIndexCtrl.Modes.Search)
            Dim o2 As txtBaseIndexCtrl = New txtDateTimeIndexCtrl(Index, True, txtIndexCtrl.Modes.Search)
            RemoveHandler o2.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o2.EnterPressed, AddressOf Enter_KeyPress
            'RemoveHandler o2.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler o2.EnterPressed, AddressOf Enter_KeyDown
            Me.txtData2Ctrl = o2

            Me.txtDataCtrl.Dock = DockStyle.Fill
            Me.txtData2Ctrl.Dock = DockStyle.Fill
            Me.PanelData.Controls.Add(Me.txtDataCtrl)
            Me.PanelData2.Controls.Add(Me.txtData2Ctrl)
        ElseIf Index.Type <> IndexDataType.Alfanumerico AndAlso Index.Type <> IndexDataType.Alfanumerico_Largo Then

            'Me.txtDataCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
            Dim o As txtIndexCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search, _docTypeID, _parentIndexs)
            RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o.EnterPressed, AddressOf Enter_KeyPress
            RemoveHandler o.ItemChanged, AddressOf Item_Changed
            AddHandler o.ItemChanged, AddressOf Item_Changed
            'RemoveHandler o.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler o.EnterPressed, AddressOf Enter_KeyDown

            Me.txtDataCtrl = o

            'Me.txtData2Ctrl = New txtIndexCtrl(Index, True, txtIndexCtrl.Modes.Search)
            Dim o2 As txtIndexCtrl = New txtIndexCtrl(Index, True, txtIndexCtrl.Modes.Search, _docTypeID, _parentIndexs)
            RemoveHandler o2.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o2.EnterPressed, AddressOf Enter_KeyPress

            RemoveHandler o2.ItemChanged, AddressOf Item_Changed
            AddHandler o2.ItemChanged, AddressOf Item_Changed
            RemoveHandler o2.ItemChanged, AddressOf Item_Changed
            AddHandler o2.ItemChanged, AddressOf Item_Changed
            'RemoveHandler o2.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler o2.EnterPressed, AddressOf Enter_KeyDown
            Me.txtData2Ctrl = o2

            Me.txtDataCtrl.Dock = DockStyle.Fill
            Me.txtData2Ctrl.Dock = DockStyle.Fill
            Me.PanelData.Controls.Add(Me.txtDataCtrl)
            Me.PanelData2.Controls.Add(Me.txtData2Ctrl)
        Else
            'Me.txtDataCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
            Dim o As txtIndexCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search, _docTypeID, _parentIndexs)
            RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o.EnterPressed, AddressOf Enter_KeyPress

            RemoveHandler o.ItemChanged, AddressOf Item_Changed
            AddHandler o.ItemChanged, AddressOf Item_Changed
            'RemoveHandler o.EnterPressed, AddressOf Enter_KeyDown
            'AddHandler o.EnterPressed, AddressOf Enter_KeyDown
            Me.txtDataCtrl = o

            '           Me.txtData2Ctrl = New txtIndexCtrl(Index, True, txtIndexCtrl.Modes.Search)
            Me.txtDataCtrl.Dock = DockStyle.Fill
            '            Me.txtData2Ctrl.Dock = DockStyle.Fill
            If Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico OrElse Me.Index.DropDown = IndexAdditionalType.DropDownJerarquico Then Me.PanelData.Controls.Clear()
            Me.PanelData.Controls.Add(Me.txtDataCtrl)
            '          Me.PanelData2.Controls.Add(Me.txtData2Ctrl)
        End If
        loadTabIndexs()
    End Sub

    Private Sub loadTabIndexs()
        Me.PanelData.TabStop = True
        Me.PanelData.TabIndex = 0
    End Sub

    Public Sub RefreshData()
        Me.txtDataCtrl.RefreshControl(DirectCast(Index, Index))
        If IsNothing(Me.txtData2Ctrl) = False Then Me.txtData2Ctrl.RefreshControl(DirectCast(Index, Index))
    End Sub
    Public Sub Clean()
        Index.Data = String.Empty
        Index.Data2 = String.Empty
        Index.DataTemp = String.Empty
        Index.DataTemp2 = String.Empty
        Index.dataDescription = String.Empty
        Index.dataDescription2 = String.Empty
        Index.dataDescriptionTemp = String.Empty
        Index.dataDescriptionTemp2 = String.Empty
        'If Index.Type = IndexDataType.Alfanumerico OrElse Index.Type = IndexDataType.Alfanumerico_Largo Then
        '    Index.[Operator] = "Contiene"
        'Else
        Index.[Operator] = "="
        'End If
        Index.OrderSort = OrderSorts.Ninguno
        Me.__FlagEntre = False
        Me.ComboBox1.Text = Index.[Operator]
        Me.ResizeTxtBoxs()
        Me.RefreshData()
    End Sub

#Region "Resize Control"
    '   Private __LastSize As Size
    'Public _LabelWidth As Integer = 100
    'Public Property LabelWidth() As Integer
    '    Get
    '        Return Me._LabelWidth
    '    End Get
    '    Set(ByVal Value As Integer)
    '        Me._LabelWidth = Value
    '    End Set
    'End Property
    Private Sub IndexSearchCtrl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.ResizeTxtBoxs()
        '   Me.ResizeControls()
    End Sub
    Private Sub ResizeTxtBoxs()
        If Me.__FlagEntre Then
            Me.PanelData2.Visible = True
            Me.PanelData2.Width = CInt((Me.Width - Me.lblName.Width - Me.ComboBox1.Width - Me.btnSort.Width) / 2)
            ' Me.PanelData.Dock = DockStyle.Left
            ' Dim s As New Size(Me.Panel1.Width / 2, Me.PanelData.Size.Height)
            ' Me.PanelData.SetBounds(Me.Panel1.Location.X, Me.Panel1.Location.Y, s.Width, s.Height)
            ' Me.PanelData2.SetBounds(Me.PanelData.Left + Me.PanelData.Size.Width, Me.PanelData2.Location.Y, s.Width, s.Height)
            'Me.PanelData2.BringToFront()
        Else
            Me.PanelData2.Visible = False
            Me.PanelData2.Width = 0
            '            Me.PanelData.Dock = DockStyle.Fill
            '            Me.PanelData.Location = New Point(Me.ComboBox1.Left + Me.ComboBox1.Width, Me.Location.Y)
            '            Me.PanelData.Size = New Size(Me.Panel1.Width, Me.Panel1.Height)
        End If
        If Me.__FlagIsNull = True Then
            Me.PanelData.Enabled = False
            Me.PanelData.BackColor = Color.Silver
        Else
            Me.PanelData.Enabled = True
            Me.PanelData.BackColor = Color.White
        End If
    End Sub
    'Private Shared Sub ResizeControls()
    '    'Me.lblName.Top = Me.Top
    '    'Me.lblName.Left = Me.Left
    '    'Me.lblName.Height = Me.Size.Height
    '    'Me.lblName.Width = Me.LabelWidth
    'End Sub
#End Region

#Region "Entre DATA2"
    Private __FlagEntre As Boolean
    Private __FlagIsNull As Boolean = False
    'Private Sub ShowEntre()
    '    If __FlagEntre = False Then
    '        'Me.__LastSize = Me.PanelData.Size
    '        Me.PanelData2.Visible = True
    '        Dim s As New Size(Me.PanelData.Size.Width / 2, Me.PanelData.Size.Height)
    '        Me.PanelData.Size = s
    '        Me.PanelData2.Size = s
    '        Dim p As New Point(Me.PanelData.Location.X + s.Width, Me.PanelData.Location.Y)
    '        Me.PanelData2.Location = p
    '        Me.PanelData.Anchor = AnchorStyles.Left
    '        Me.PanelData2.Anchor = AnchorStyles.Right
    '        Me.__FlagEntre = True
    '    End If
    'End Sub
    'Private Sub HideEntre()
    '    Me.PanelData2.Visible = False
    '    Me.PanelData.Anchor = AnchorStyles.Left + AnchorStyles.Right
    '    Me.PanelData.Size = New Size(Me.btnSort.Location.X, Me.PanelData.Size.Height)
    'End Sub
#End Region

#Region "Sort"
    Private Sub btnSort_Click() Handles btnSort.SortClick
        Try
            Me.Sort = DirectCast(Me.Sort + 1, OrderSorts)
        Catch ex As Exception
            Me.Sort = OrderSorts.Ninguno
        End Try
    End Sub
    Private Property Sort() As OrderSorts
        Get
            Return Index.OrderSort
        End Get
        Set(ByVal Value As OrderSorts)
            Index.OrderSort = DirectCast(Value Mod 3, OrderSorts)
            Select Case Index.OrderSort
                Case OrderSorts.Ninguno
                    Me.btnSort.Label1.Text = "ABC"
                    '                    Me.btnSort.Image = Me.ImageList1.Images(2)
                Case OrderSorts.ASC
                    Me.btnSort.Label1.Text = "A-Z"
                    '                    Me.btnSort.Image = Me.ImageList1.Images(0)
                Case OrderSorts.DESC
                    Me.btnSort.Label1.Text = "Z-A"
                    '                    Me.btnSort.Image = Me.ImageList1.Images(1)
            End Select
        End Set
    End Property
#End Region

    Private Sub SimpleIndexSearchCtrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.ComboBox1.TabStop = False
        Me.btnSort.TabStop = False

    End Sub

    'Private Sub txtDataCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDataCtrl.KeyPress

    'End Sub

 
    'Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.LostFocus

    'End Sub
End Class