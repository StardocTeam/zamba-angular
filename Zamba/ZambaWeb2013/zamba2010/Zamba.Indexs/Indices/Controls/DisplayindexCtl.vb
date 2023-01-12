Imports Zamba.AppBlock
Imports Zamba.Core

Public Class DisplayindexCtl
    Inherits ZControl
    Private WithEvents TxtDataCtrl As txtBaseIndexCtrl
    Public IsEnabled As Boolean
    Public ReadOnly Property Index() As IIndex
        Get
            Return TxtDataCtrl.Index
        End Get
    End Property
    Public Event DataChanged(ByVal Index As IIndex)
    Public Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

    Public Sub Item_Changed(ByVal IndexID As Integer, ByVal NewValue As String)
        RaiseEvent ItemChanged(IndexID, NewValue)
    End Sub

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
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub
    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If lblIndexName IsNot Nothing Then
                    lblIndexName.Dispose()
                    lblIndexName = Nothing
                End If
                If Panel1 IsNot Nothing Then
                    Panel1.Dispose()
                    Panel1 = Nothing
                End If
                If Panel5 IsNot Nothing Then
                    Panel5.Dispose()
                    Panel5 = Nothing
                End If
                If Splitter2 IsNot Nothing Then
                    Splitter2.Dispose()
                    Splitter2 = Nothing
                End If
                If TxtDataCtrl IsNot Nothing Then
                    TxtDataCtrl.Dispose()
                    TxtDataCtrl = Nothing
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
    Friend WithEvents Panel1 As ZWhitePanel
    Friend WithEvents lblIndexName As System.Windows.Forms.Label
    Friend WithEvents Panel5 As ZWhitePanel
    Friend WithEvents Splitter2 As ZWhitePanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblIndexName = New System.Windows.Forms.Label
        Me.Panel1 = New Zamba.AppBlock.ZWhitePanel
        Me.Panel5 = New Zamba.AppBlock.ZWhitePanel
        Me.Splitter2 = New Zamba.AppBlock.ZWhitePanel
        Me.SuspendLayout()
        '
        'lblIndexName
        '
        Me.lblIndexName.BackColor = System.Drawing.Color.White
        Me.lblIndexName.CausesValidation = False
        Me.lblIndexName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblIndexName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndexName.ForeColor = System.Drawing.Color.Black
        Me.lblIndexName.Location = New System.Drawing.Point(0, 4)
        Me.lblIndexName.Name = "lblIndexName"
        Me.lblIndexName.Size = New System.Drawing.Size(125, 28)
        Me.lblIndexName.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(80, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(392, 28)
        Me.Panel1.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel5.CausesValidation = False
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Location = New System.Drawing.Point(75, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(5, 28)
        Me.Panel5.TabIndex = 5
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Splitter2.CausesValidation = False
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(472, 4)
        Me.Splitter2.TabIndex = 9
        '
        'DisplayindexCtl
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.lblIndexName)
        Me.Controls.Add(Me.Splitter2)
        Me.Name = "DisplayindexCtl"
        Me.Size = New System.Drawing.Size(472, 32)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _docTypeId As Integer
    Private _parentIndexs As Hashtable
    Private _isEnabled As Boolean
    Private _index As IIndex

    Public Sub New(ByVal index As IIndex, ByVal isEnabled As Boolean, ByVal ParentIndexs As Hashtable)
        Me.New()
        _parentIndexs = ParentIndexs
        _isEnabled = isEnabled
        _index = index
        LoadControl()
    End Sub

    Public Sub New(ByVal index As IIndex, ByVal isEnabled As Boolean)
        Me.New()
        _isEnabled = isEnabled
        _index = index
        LoadControl()
    End Sub

    Public Sub LoadControl(ByVal DocTypeID As Int32, ByVal ParentIndexs As Hashtable)
        _docTypeId = DocTypeID
        _parentIndexs = ParentIndexs
        LoadControl()
    End Sub

    Public Sub LoadControl()
        lblIndexName.Text = _index.Name
        Me.IsEnabled = _isEnabled

        Dim indCtrl As txtBaseIndexCtrl

        If _isEnabled Then

            Select Case _index.Type

                Case IndexDataType.Si_No  ' BOOL Control Check

                    indCtrl = New ChkIndexCtrl(_index, False, txtBaseIndexCtrl.Modes.Viewer)
                    AddHandler DirectCast(indCtrl, ChkIndexCtrl).IndexChanged, AddressOf txtData_datachanged

                Case Core.IndexDataType.Fecha  ' FECHA Control TextBox 

                    indCtrl = New txtDateIndexCtrl(_index, False, txtDateIndexCtrl.Modes.Viewer)
                    AddHandler DirectCast(indCtrl, txtDateIndexCtrl).IndexChanged, AddressOf txtData_datachanged

                Case Core.IndexDataType.Fecha_Hora  ' FECHA HORA Control TextBox

                    indCtrl = New txtDateTimeIndexCtrl(_index, False, txtDateTimeIndexCtrl.Modes.Viewer)
                    AddHandler DirectCast(indCtrl, txtDateTimeIndexCtrl).IndexChanged, AddressOf txtData_datachanged

                Case Else

                    Select Case _index.DropDown

                        Case Core.IndexAdditionalType.AutoSustitución, Core.IndexAdditionalType.AutoSustituciónJerarquico

                            indCtrl = New txtSustIndexCtrl(_index, False, txtSustIndexCtrl.Modes.Viewer, _docTypeId, _parentIndexs)

                            AddHandler DirectCast(indCtrl, txtSustIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                            AddHandler DirectCast(indCtrl, txtSustIndexCtrl).ItemChanged, AddressOf Item_Changed

                        Case Core.IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico  ' Control ComboBox

                            indCtrl = New txtDropDownIndexCtrl(_index, False, txtDropDownIndexCtrl.Modes.Viewer, _docTypeId, _parentIndexs)

                            AddHandler DirectCast(indCtrl, txtDropDownIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                            AddHandler DirectCast(indCtrl, txtDropDownIndexCtrl).ItemChanged, AddressOf Item_Changed

                        Case Core.IndexAdditionalType.LineText

                            indCtrl = New txtLineIndexCtrl(_index, False, txtLineIndexCtrl.Modes.Viewer)
                            AddHandler DirectCast(indCtrl, txtLineIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                            
                    End Select

            End Select

        Else

            If _index.Type = Core.IndexDataType.Si_No Then
                indCtrl = New ChkReadOnlyIndexCtrl(_index, False)
            Else
                indCtrl = New txtReadOnlyIndexCtrl(_index, False)
            End If

        End If

        RemoveHandler indCtrl.TextChanged, AddressOf NotifyChange
        RemoveHandler indCtrl.EnterPressed, AddressOf Enter_KeyDown
        RemoveHandler indCtrl.TabPressed, AddressOf Tab_KeyDown
        AddHandler indCtrl.EnterPressed, AddressOf Enter_KeyDown
        AddHandler indCtrl.TabPressed, AddressOf Tab_KeyDown
        AddHandler indCtrl.TextChanged, AddressOf NotifyChange

        TxtDataCtrl = indCtrl
        TxtDataCtrl.Dock = DockStyle.Fill

        If _index.AutoIncremental Then
            TxtDataCtrl.Enabled = False
        End If

        Me.Panel1.Controls.Clear()
        Me.Panel1.Controls.Add(TxtDataCtrl)
    End Sub

#Region "Ante un cambio de indice, pero no de tipo de doc o reutilizacion del indice para otro tipo de doc, cargo el nuevo indice con sus valores pero matengo el control existente"
    Public Sub ReloadindexData(ByVal Index As IIndex)
        If Me.IsEnabled Then
            If Index.Type = Core.IndexDataType.Si_No Then
                RemoveHandler DirectCast(TxtDataCtrl, ChkIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                TxtDataCtrl.RefreshControl(Index)
                AddHandler DirectCast(TxtDataCtrl, ChkIndexCtrl).IndexChanged, AddressOf txtData_datachanged
            ElseIf Index.Type = Core.IndexDataType.Fecha Then
                RemoveHandler DirectCast(TxtDataCtrl, txtDateIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                TxtDataCtrl.RefreshControl(Index)
                AddHandler DirectCast(TxtDataCtrl, txtDateIndexCtrl).IndexChanged, AddressOf txtData_datachanged
            ElseIf Index.Type = Core.IndexDataType.Fecha_Hora Then
                RemoveHandler DirectCast(TxtDataCtrl, txtDateTimeIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                TxtDataCtrl.RefreshControl(Index)
                AddHandler DirectCast(TxtDataCtrl, txtDateTimeIndexCtrl).IndexChanged, AddressOf txtData_datachanged
            Else
                Select Case Index.DropDown
                    Case Core.IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                        RemoveHandler DirectCast(TxtDataCtrl, txtSustIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                        TxtDataCtrl.RefreshControl(Index)
                        AddHandler DirectCast(TxtDataCtrl, txtSustIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                    Case Core.IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                        RemoveHandler DirectCast(TxtDataCtrl, txtDropDownIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                        TxtDataCtrl.RefreshControl(Index)
                        AddHandler DirectCast(TxtDataCtrl, txtDropDownIndexCtrl).IndexChanged, AddressOf txtData_datachanged
                    Case Core.IndexAdditionalType.LineText
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
        RaiseEvent DataChanged(Me.Index) 'TODO verificar
    End Sub
    Public Sub RollBack()
        Me.TxtDataCtrl.RollBack()
    End Sub
    Public Sub Commit()
        Me.TxtDataCtrl.Commit()
    End Sub
    Public Sub RefreshData()
        Me.TxtDataCtrl.RefreshControl(Index)
    End Sub
    Public Sub RefreshDataTemp()
        Me.TxtDataCtrl.RefreshControlDataTemp(Index)
    End Sub
    Public Sub Clean()
        Me.TxtDataCtrl.Index.Data = String.Empty
        Me.RefreshData()
    End Sub
    Public Sub CleanDataTemp()
        Me.TxtDataCtrl.Index.DataTemp = String.Empty
        Me.RefreshDataTemp()
    End Sub
    Private Sub txtData_datachanged()
        If Not IsNothing(Index) Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Cambio en indice: " & Index.Name)
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