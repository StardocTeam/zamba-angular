Imports Zamba.Core
Public Class SimpleIndexSearchCtrl
    Inherits ZControl
    Implements IDisposable

#Region "Eventos TAB y ENTER"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Public Shadows Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

    Private Sub IndexCtrl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
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
            If IsNothing(txtData2Ctrl) = False Then
                If txtDataCtrl.IsValid AndAlso txtData2Ctrl.IsValid Then
                    Return True
                Else
                    Return False
                End If
            Else
                If txtDataCtrl.IsValid Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private _parentData As String

    ''' <summary>
    ''' Se utiliza esta propiedad para dar a conocer al control el valor del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParentData As String
        Get
            ZTrace.WriteLineIf(ZTrace.IsInfo, "SimpleIndexSearchCtrl - Obteniendo valor padre de indice: " & Index.ID)
            If TypeOf txtDataCtrl Is txtSustIndexCtrl Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es sustitucion y se encontro el control")
                Return DirectCast(txtDataCtrl, txtSustIndexCtrl).ParentData
            ElseIf TypeOf txtDataCtrl Is txtDropDownIndexCtrl Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es dropDown y se encontro el control")
                Return DirectCast(txtDataCtrl, txtDropDownIndexCtrl).ParentData
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es linetext o no se encontro el control")
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            _parentData = value

            ZTrace.WriteLineIf(ZTrace.IsInfo, "SimpleIndexSearchCtrl - Setenado el valor padre del indice: " & Index.ID)
            Dim txtBaseIndexCtrl = TryCast(txtDataCtrl, txtIndexCtrl)
            If (txtBaseIndexCtrl IsNot Nothing) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es sustitucion y se encontro el control")
                txtBaseIndexCtrl.ParentData = value
            End If
        End Set
    End Property

#Region "combo"
    Public Event OperatorClicked(ByVal control As SimpleIndexSearchCtrl)
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles ComboBox1.LinkClicked
        RaiseEvent OperatorClicked(Me)
    End Sub
    Public Property [Operator]() As String
        Get
            Return Index.[Operator]
        End Get
        Set(ByVal Value As String)
            Index.[Operator] = Value
            If String.Compare(Index.[Operator], "Entre") = 0 Then
                __FlagEntre = True
            Else
                __FlagEntre = False
            End If
            If String.Compare(Index.[Operator], "es nulo", True) = 0 Then
                __FlagIsNull = True
            Else
                __FlagIsNull = False
            End If
            ComboBox1.Text = Index.[Operator]
            ResizeTxtBoxs()
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
        SuspendLayout()
        Init(index)
        ResumeLayout(False)
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RemoveHandler MyBase.KeyDown, AddressOf IndexCtrl_KeyDown
                RemoveHandler MyBase.Resize, AddressOf IndexSearchCtrl_Resize
                RemoveHandler MyBase.Load, AddressOf SimpleIndexSearchCtrl_Load

                If ComboBox1 IsNot Nothing Then
                    RemoveHandler ComboBox1.LinkClicked, AddressOf ComboBox1_SelectedIndexChanged
                    ComboBox1.Dispose()
                    ComboBox1 = Nothing
                End If
                If lblName IsNot Nothing Then
                    lblName.Dispose()
                    lblName = Nothing
                End If
                If txtDataCtrl IsNot Nothing Then
                    If TypeOf (txtDataCtrl) Is txtIndexCtrl Then
                        RemoveHandler DirectCast(txtDataCtrl, txtIndexCtrl).ItemChanged, AddressOf Item_Changed
                    End If
                    RemoveHandler txtDataCtrl.EnterPressed, AddressOf Enter_KeyPress
                    RemoveHandler txtDataCtrl.TabPressed, AddressOf Tab_KeyDown
                    txtDataCtrl.Dispose()
                    txtDataCtrl = Nothing
                End If
                If txtData2Ctrl IsNot Nothing Then
                    If TypeOf (txtData2Ctrl) Is txtIndexCtrl Then
                        RemoveHandler DirectCast(txtData2Ctrl, txtIndexCtrl).ItemChanged, AddressOf Item_Changed
                    End If
                    RemoveHandler txtData2Ctrl.EnterPressed, AddressOf Enter_KeyPress
                    RemoveHandler txtData2Ctrl.TabPressed, AddressOf Tab_KeyDown
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
                'If btnSort IsNot Nothing Then
                '    RemoveHandler btnSort.SortClick, AddressOf btnSort_Click
                '    btnSort.Dispose()
                '    btnSort = Nothing
                'End If
                If Index IsNot Nothing Then
                    Index.Dispose()
                    Index = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean

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
    'Friend WithEvents btnSort As SimpleSortButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        lblName = New ZLabel()
        ComboBox1 = New System.Windows.Forms.LinkLabel()
        PanelData = New ZPanel()
        PanelData2 = New ZPanel()
        SuspendLayout
        '
        'lblName
        '
        lblName.BackColor = Color.Transparent
        lblName.CausesValidation = False
        lblName.Dock = DockStyle.Left
        lblName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        lblName.FontSize = 9.75!
        lblName.ForeColor = Color.FromArgb(76, 76, 76)
        lblName.Location = New Point(0, 0)
        lblName.Name = "lblName"
        lblName.Size = New Size(229, 26)
        lblName.TabIndex = 0
        lblName.TextAlign = ContentAlignment.MiddleLeft
        '
        'ComboBox1
        '
        ComboBox1.Dock = DockStyle.Left
        ComboBox1.ForeColor = Color.FromArgb(76, 76, 76)
        ComboBox1.LinkBehavior = LinkBehavior.HoverUnderline
        ComboBox1.Location = New Point(229, 0)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(70, 26)
        ComboBox1.TabIndex = 0
        ComboBox1.TextAlign = ContentAlignment.TopCenter
        ComboBox1.VisitedLinkColor = Color.Blue
        '
        'PanelData
        '
        PanelData.BackColor = Color.FromArgb(236, 236, 236)
        PanelData.Dock = DockStyle.Fill
        PanelData.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        PanelData.ForeColor = Color.FromArgb(76, 76, 76)
        PanelData.Location = New Point(299, 0)
        PanelData.Name = "PanelData"
        PanelData.Size = New Size(281, 26)
        PanelData.TabIndex = 0
        PanelData.TabStop = True
        '
        'PanelData2
        '
        PanelData2.BackColor = Color.FromArgb(236, 236, 236)
        PanelData2.Dock = DockStyle.Right
        PanelData2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        PanelData2.ForeColor = Color.FromArgb(76, 76, 76)
        PanelData2.Location = New Point(580, 0)
        PanelData2.Name = "PanelData2"
        PanelData2.Size = New Size(0, 26)
        PanelData2.TabIndex = 2
        '
        'SimpleIndexSearchCtrl
        '
        Controls.Add(PanelData)
        Controls.Add(PanelData2)
        Controls.Add(ComboBox1)
        Controls.Add(lblName)
        Name = "SimpleIndexSearchCtrl"
        Size = New Size(580, 26)
        ResumeLayout(False)

    End Sub


    'Private Sub Data2Changed(ByVal data As String) Handles txtData2Ctrl.DataChanged
    '    index.Data2 = data
    'End Sub
    'Private Sub DataChanged(ByVal data As String) Handles txtDataCtrl.DataChanged
    '    index.Data = data
    'End Sub
#End Region


    Public Index As Index
    Private _docTypeID As Int64 = 0
    Private _parentValue As String = String.Empty
    Private _parentIndexs As Hashtable = Nothing

    Public Sub Init(ByVal Index As Index, ByVal DocTypeID As Int64, ByVal ParentIndexs As Hashtable)
        _docTypeID = DocTypeID
        _parentIndexs = ParentIndexs
        SuspendLayout()
        Init(Index)
        ResumeLayout(False)
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
        lblName.Text = Index.Name
        ' Me.LoadComboInfo(Me.ComboBox1)
        'Me.Sort = Index.OrderSort
        ComboBox1.Text = Index.[Operator]

        If Index.DropDown = IndexAdditionalType.LineText Then
            Select Case Index.Type
                Case IndexDataType.Si_No
                    ComboBox1.Enabled = False
                    Dim o As ChkIndexCtrl = New ChkIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
                    RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
                    AddHandler o.EnterPressed, AddressOf Enter_KeyPress

                    txtDataCtrl = o

                    txtDataCtrl.Dock = DockStyle.Fill
                    PanelData.Controls.Add(txtDataCtrl)
                Case IndexDataType.Fecha OrElse IndexDataType.Fecha_Hora
                    Dim o As txtBaseIndexCtrl = New txtDateTimeIndexCtrl(Index, False, txtIndexCtrl.Modes.Search)
                    RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
                    AddHandler o.EnterPressed, AddressOf Enter_KeyPress
                    txtDataCtrl = o
                    Dim o2 As txtBaseIndexCtrl = New txtDateTimeIndexCtrl(Index, True, txtIndexCtrl.Modes.Search)
                    RemoveHandler o2.EnterPressed, AddressOf Enter_KeyPress
                    AddHandler o2.EnterPressed, AddressOf Enter_KeyPress
                    txtData2Ctrl = o2
                    txtDataCtrl.Dock = DockStyle.Fill
                    txtData2Ctrl.Dock = DockStyle.Fill
                    PanelData.Controls.Add(txtDataCtrl)
                    PanelData2.Controls.Add(txtData2Ctrl)
                Case Else
                    Dim o As txtIndexCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search, _docTypeID, _parentData)
                    RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
                    AddHandler o.EnterPressed, AddressOf Enter_KeyPress
                    RemoveHandler o.ItemChanged, AddressOf Item_Changed
                    AddHandler o.ItemChanged, AddressOf Item_Changed

                    txtDataCtrl = o


                    Dim o2 As txtIndexCtrl = New txtIndexCtrl(Index, True, txtIndexCtrl.Modes.Search, _docTypeID, _parentData)
                    AddHandler o2.EnterPressed, AddressOf Enter_KeyPress
                    AddHandler o2.ItemChanged, AddressOf Item_Changed

                    txtData2Ctrl = o2

                    txtDataCtrl.Dock = DockStyle.Fill
                    txtData2Ctrl.Dock = DockStyle.Fill
                    PanelData.Controls.Add(txtDataCtrl)
                    PanelData2.Controls.Add(txtData2Ctrl)
            End Select
        Else

            Dim o As txtIndexCtrl = New txtIndexCtrl(Index, False, txtIndexCtrl.Modes.Search, _docTypeID, _parentData)
            RemoveHandler o.EnterPressed, AddressOf Enter_KeyPress
            AddHandler o.EnterPressed, AddressOf Enter_KeyPress

            RemoveHandler o.ItemChanged, AddressOf Item_Changed
            AddHandler o.ItemChanged, AddressOf Item_Changed


            txtDataCtrl = o

            '           Me.txtData2Ctrl = New txtIndexCtrl(Index, True, txtIndexCtrl.Modes.Search)
            txtDataCtrl.Dock = DockStyle.Fill
            '            Me.txtData2Ctrl.Dock = DockStyle.Fill
            PanelData.Controls.Clear()
            PanelData.Controls.Add(txtDataCtrl)
            '          Me.PanelData2.Controls.Add(Me.txtData2Ctrl)
        End If

        loadTabIndexs()
    End Sub

    Private Sub loadTabIndexs()
        PanelData.TabStop = True
        PanelData.TabIndex = 0
    End Sub

    Public Sub RefreshData()
        txtDataCtrl.RefreshControl((Index))
        If IsNothing(txtData2Ctrl) = False Then txtData2Ctrl.RefreshControl((Index))
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
        __FlagEntre = False
        ComboBox1.Text = Index.[Operator]
        ResizeTxtBoxs()
        RefreshData()
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
    Private Sub IndexSearchCtrl_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        ResizeTxtBoxs()
        '   Me.ResizeControls()
    End Sub
    Private Sub ResizeTxtBoxs()
        If __FlagEntre Then
            PanelData2.Visible = True
            PanelData2.Width = CInt((Width - lblName.Width - ComboBox1.Width) / 2) '- Me.btnSort.Width
            ' Me.PanelData.Dock = DockStyle.Left
            ' Dim s As New Size(Me.Panel1.Width / 2, Me.PanelData.Size.Height)
            ' Me.PanelData.SetBounds(Me.Panel1.Location.X, Me.Panel1.Location.Y, s.Width, s.Height)
            ' Me.PanelData2.SetBounds(Me.PanelData.Left + Me.PanelData.Size.Width, Me.PanelData2.Location.Y, s.Width, s.Height)
            'Me.PanelData2.BringToFront()
        Else
            PanelData2.Visible = False
            PanelData2.Width = 0
            '            Me.PanelData.Dock = DockStyle.Fill
            '            Me.PanelData.Location = New Point(Me.ComboBox1.Left + Me.ComboBox1.Width, Me.Location.Y)
            '            Me.PanelData.Size = New Size(Me.Panel1.Width, Me.Panel1.Height)
        End If
        If __FlagIsNull = True Then
            PanelData.Enabled = False
            PanelData.BackColor = Color.Silver
        Else
            PanelData.Enabled = True
            PanelData.BackColor = Color.White
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
    'Private Sub btnSort_Click() Handles btnSort.SortClick
    '    Try
    '        Me.Sort = DirectCast(Me.Sort + 1, OrderSorts)
    '    Catch ex As Exception
    '        Me.Sort = OrderSorts.Ninguno
    '    End Try
    'End Sub
    'Private Property Sort() As OrderSorts
    '    Get
    '        Return Index.OrderSort
    '    End Get
    '    Set(ByVal Value As OrderSorts)
    '        Index.OrderSort = DirectCast(Value Mod 3, OrderSorts)
    '        Select Case Index.OrderSort
    '            Case OrderSorts.Ninguno
    '                Me.btnSort.Label1.Text = "ABC"
    '                '                    Me.btnSort.Image = Me.ImageList1.Images(2)
    '            Case OrderSorts.ASC
    '                Me.btnSort.Label1.Text = "A-Z"
    '                '                    Me.btnSort.Image = Me.ImageList1.Images(0)
    '            Case OrderSorts.DESC
    '                Me.btnSort.Label1.Text = "Z-A"
    '                '                    Me.btnSort.Image = Me.ImageList1.Images(1)
    '        End Select
    '    End Set
    'End Property
#End Region

    Private Sub SimpleIndexSearchCtrl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Me.ComboBox1.TabStop = False
        'Me.btnSort.TabStop = False
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    'Private Sub txtDataCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDataCtrl.KeyPress

    'End Sub


    'Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.LostFocus

    'End Sub
End Class