Imports Zamba.Core

Public Class DisplayindexCtl
    Inherits ZControl
    Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Private isDisposed As Boolean
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RaiseEvent ClearReferences(Me)

                If lblIndexName IsNot Nothing Then
                    lblIndexName.Dispose()
                    lblIndexName = Nothing
                End If
                If TxtDataCtrl IsNot Nothing Then
                    RemoveHandler TxtDataCtrl.TextChanged, AddressOf NotifyChange
                    RemoveHandler TxtDataCtrl.EnterPressed, AddressOf Enter_KeyDown
                    RemoveHandler TxtDataCtrl.TabPressed, AddressOf Tab_KeyDown
                    TxtDataCtrl.Dispose()
                    TxtDataCtrl = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Public WithEvents lblIndexName As ZLabel

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        lblIndexName = New ZLabel()
        SuspendLayout()
        '
        'lblIndexName
        '
        lblIndexName.BackColor = Color.White
        lblIndexName.CausesValidation = False
        lblIndexName.Dock = DockStyle.Left
        lblIndexName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        lblIndexName.ForeColor = Color.FromArgb(76, 76, 76)
        lblIndexName.Location = New Point(0, 4)
        lblIndexName.Name = "lblIndexName"
        lblIndexName.Size = New Size(170, 25)
        lblIndexName.TabIndex = 0
        '
        'DisplayindexCtl
        '
        Name = "DisplayindexCtl"
        Size = New Size(472, 25)
        Margin = New Padding(0, 0, 0, 10)
        ResumeLayout(False)

    End Sub

#End Region
    Private WithEvents TxtDataCtrl As txtBaseIndexCtrl
    Public IsEnabled As Boolean
    Public ReadOnly Property Index() As IIndex
        Get
            Return TxtDataCtrl.Index
        End Get
    End Property
    Public Event DataChanged(ByVal Index As IIndex)
    Public Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)
    Public Event ClearReferences(ByRef control As DisplayindexCtl)
    Public Sub Item_Changed(ByVal IndexID As Integer, ByVal NewValue As String)
        RaiseEvent ItemChanged(IndexID, NewValue)
    End Sub
    Private _docTypeId As Integer
    Private _parentIndexs As Hashtable
    Private _isEnabled As Boolean
    Private _index As IIndex
    Private _indCtrl As txtBaseIndexCtrl

    Public Property ParentIndexData() As String
        Get
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DisplayIndexCtl - Obteniendo valor padre de indice: " & Index.ID)
            If TypeOf _indCtrl Is txtSustIndexCtrl Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es sustitucion y se encontro el control")
                Return DirectCast(_indCtrl, txtSustIndexCtrl).ParentData
            ElseIf TypeOf _indCtrl Is txtDropDownIndexCtrl Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es dropDown y se encontro el control")
                Return DirectCast(_indCtrl, txtDropDownIndexCtrl).ParentData
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es linetext o no se encontro el control")
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DisplayIndexCtl - Setenado el valor padre del indice: " & Index.ID)
            Dim txtBaseIndexCtrl = TryCast(_indCtrl, txtSustIndexCtrl)
            If (txtBaseIndexCtrl IsNot Nothing) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es sustitucion y se encontro el control")
                txtBaseIndexCtrl.ParentData = value
            Else
                txtBaseIndexCtrl = TryCast(_indCtrl, txtDropDownIndexCtrl)
                If txtBaseIndexCtrl IsNot Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es dropDown y se encontro el control")
                    txtBaseIndexCtrl.ParentData = value
                End If
            End If
        End Set
    End Property

#Region "Eventos TAB y ENTER"
    Public Shadows Event EnterPressed()
    Private Sub Enter_KeyDown()

        RaiseEvent EnterPressed()
    End Sub
    Public Shadows Event TabPressed()
    Private Sub Tab_KeyDown()
        RaiseEvent TabPressed()
    End Sub
#End Region

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Public Sub New(ByVal index As IIndex, ByVal isEnabled As Boolean, Optional LabelName As String = "", Optional LabelNameVisible As Boolean = True)
        Me.New()
        _isEnabled = isEnabled
        _index = index
        LoadControl(LabelName, LabelNameVisible)
    End Sub

    Public Sub LoadControl(ByVal DocTypeID As Int64, ByVal ParentIndexs As Hashtable, Optional LabelNameVisible As Boolean = True)
        _docTypeId = DocTypeID
        _parentIndexs = ParentIndexs
        LoadControl(String.Empty, LabelNameVisible)

    End Sub

    Public Sub LoadControl(LabelName As String, LabelNameVisible As Boolean)
        Controls.Clear()

        If LabelNameVisible Then
            lblIndexName.Text = _index.Name
            If LabelName.Length > 0 Then lblIndexName.Text = LabelName
            lblIndexName.Dock = DockStyle.Left
            Dim tooltip As New ToolTip
            tooltip.SetToolTip(lblIndexName, lblIndexName.Text)
            If lblIndexName.Text.Length > 20 Then
                lblIndexName.Text = lblIndexName.Text.Substring(0, 20) & "...:"
            Else
                lblIndexName.Text = lblIndexName.Text & ":"
            End If
            Controls.Add(lblIndexName)
        End If

        IsEnabled = _isEnabled

        If _isEnabled Then

            Select Case _index.Type

                Case IndexDataType.Si_No  ' BOOL Control Check

                    _indCtrl = New ChkIndexCtrl(_index, False, txtBaseIndexCtrl.Modes.Viewer)
                    AddHandler DirectCast(_indCtrl, ChkIndexCtrl).IndexChanged, AddressOf txtData_datachanged

                Case IndexDataType.Fecha  ' FECHA Control TextBox 

                    _indCtrl = New txtDateIndexCtrl(_index, False, txtDateIndexCtrl.Modes.Viewer)
                    AddHandler DirectCast(_indCtrl, txtDateIndexCtrl).IndexChanged, AddressOf txtData_datachanged

                Case IndexDataType.Fecha_Hora  ' FECHA HORA Control TextBox

                    _indCtrl = New txtDateTimeIndexCtrl(_index, False, txtDateTimeIndexCtrl.Modes.Viewer)
                    AddHandler DirectCast(_indCtrl, txtDateTimeIndexCtrl).IndexChanged, AddressOf txtData_datachanged

                Case Else

                    Select Case _index.DropDown

                        Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico

                            _indCtrl = New txtSustIndexCtrl(_index, False, txtSustIndexCtrl.Modes.Viewer, _docTypeId, _parentIndexs)

                            AddHandler DirectCast(_indCtrl, txtSustIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                            AddHandler DirectCast(_indCtrl, txtSustIndexCtrl).ItemChanged, AddressOf Item_Changed

                        Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico  ' Control ComboBox

                            _indCtrl = New txtDropDownIndexCtrl(_index, False, txtDropDownIndexCtrl.Modes.Viewer, _docTypeId, String.Empty)

                            AddHandler DirectCast(_indCtrl, txtDropDownIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                            AddHandler DirectCast(_indCtrl, txtDropDownIndexCtrl).ItemChanged, AddressOf Item_Changed

                        Case IndexAdditionalType.LineText

                            _indCtrl = New txtLineIndexCtrl(_index, False, txtLineIndexCtrl.Modes.Viewer)
                            AddHandler DirectCast(_indCtrl, txtLineIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                    End Select
            End Select
        Else
            If _index.Type = IndexDataType.Si_No Then
                _indCtrl = New ChkReadOnlyIndexCtrl(_index, False)
            Else
                _indCtrl = New txtReadOnlyIndexCtrl(_index, False)
            End If
        End If

        RemoveHandler _indCtrl.TextChanged, AddressOf NotifyChange
        RemoveHandler _indCtrl.EnterPressed, AddressOf Enter_KeyDown
        RemoveHandler _indCtrl.TabPressed, AddressOf Tab_KeyDown
        AddHandler _indCtrl.EnterPressed, AddressOf Enter_KeyDown
        AddHandler _indCtrl.TabPressed, AddressOf Tab_KeyDown
        AddHandler _indCtrl.TextChanged, AddressOf NotifyChange

        TxtDataCtrl = _indCtrl
        TxtDataCtrl.Dock = DockStyle.Fill

        If _index.AutoIncremental Then
            TxtDataCtrl.Enabled = False
        End If


        Controls.Add(TxtDataCtrl)
        TxtDataCtrl.BringToFront()
    End Sub

#Region "Ante un cambio de indice, pero no de tipo de doc o reutilizacion del indice para otro tipo de doc, cargo el nuevo indice con sus valores pero matengo el control existente"
    Public Sub ReloadindexData(ByVal Index As IIndex)
        If IsEnabled Then
            If Index.Type = IndexDataType.Si_No Then
                RemoveHandler DirectCast(TxtDataCtrl, ChkIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                TxtDataCtrl.RefreshControl(Index)
                AddHandler DirectCast(TxtDataCtrl, ChkIndexCtrl).IndexChanged, AddressOf txtData_datachanged
            ElseIf Index.Type = IndexDataType.Fecha Then
                RemoveHandler DirectCast(TxtDataCtrl, txtDateIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                TxtDataCtrl.RefreshControl(Index)
                AddHandler DirectCast(TxtDataCtrl, txtDateIndexCtrl).IndexChanged, AddressOf txtData_datachanged
            ElseIf Index.Type = IndexDataType.Fecha_Hora Then
                RemoveHandler DirectCast(TxtDataCtrl, txtDateTimeIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                TxtDataCtrl.RefreshControl(Index)
                AddHandler DirectCast(TxtDataCtrl, txtDateTimeIndexCtrl).IndexChanged, AddressOf txtData_datachanged
            Else
                Select Case Index.DropDown
                    Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                        RemoveHandler DirectCast(TxtDataCtrl, txtSustIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                        TxtDataCtrl.RefreshControl(Index)
                        AddHandler DirectCast(TxtDataCtrl, txtSustIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                    Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                        RemoveHandler DirectCast(TxtDataCtrl, txtDropDownIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                        TxtDataCtrl.RefreshControl(Index)
                        AddHandler DirectCast(TxtDataCtrl, txtDropDownIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                    Case IndexAdditionalType.LineText
                        RemoveHandler DirectCast(TxtDataCtrl, txtLineIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                        TxtDataCtrl.RefreshControl(Index)
                        AddHandler DirectCast(TxtDataCtrl, txtLineIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                End Select
            End If
        Else
            TxtDataCtrl.RefreshControl(Index)
        End If
    End Sub
#End Region

    Public Sub NotifyChange(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent DataChanged(Index) 'TODO verificar
    End Sub
    Public Sub RollBack()
        TxtDataCtrl.RollBack()
    End Sub
    Public Sub Commit()
        TxtDataCtrl.Commit()
    End Sub
    Public Sub RefreshData()
        TxtDataCtrl.RefreshControl(Index)
    End Sub
    Public Sub RefreshDataTemp()
        TxtDataCtrl.RefreshControlDataTemp(Index)
    End Sub
    Public Sub Clean()
        TxtDataCtrl.Index.Data = String.Empty
        RefreshData()
    End Sub
    Public Sub CleanDataTemp()
        TxtDataCtrl.Index.DataTemp = String.Empty
        RefreshDataTemp()
    End Sub
    Private _ucViewer As UCIndexIndexerViewer
    Private _NewResult As NewResult
    Private Sub txtData_datachanged()
        If Not IsNothing(Index) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cambio en indice: " & Index.Name)
        End If
        RaiseEvent DataChanged(Index)
    End Sub
    Public Function isValid() As Boolean
        Return TxtDataCtrl.IsValid
    End Function
    Public Function getText() As String
        Return TxtDataCtrl.Controls(0).Text
    End Function
End Class