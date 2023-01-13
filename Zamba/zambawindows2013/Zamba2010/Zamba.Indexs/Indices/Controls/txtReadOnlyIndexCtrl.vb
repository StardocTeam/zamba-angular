Imports ZAMBA.Core
Public Class txtReadOnlyIndexCtrl
    Inherits txtBaseIndexCtrl

#Region "Constructores"
    '  Private Mode As Modes
    Public Sub New(ByVal docindex As IIndex, ByVal data2 As Boolean)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        'Me.Mode = Mode
        Index = docindex
        ' Me.FlagData2 = data2
        Init()
        RefreshControl(Index)
    End Sub

#End Region

#Region "Propiedades Publicas"
    '   Public _IsValid As Boolean = True
    Public Overrides Property IsValid() As Boolean
        Get
            If _IsValid = False Then DataChangeProcedure(Combobox1, New ComponentModel.CancelEventArgs)
            Return _IsValid
        End Get
        Set(ByVal Value As Boolean)
            _IsValid = Value
            '  If Value = True Then Me.ErrorProvider1.SetError(Me.Combobox1, "")
        End Set
    End Property
#End Region

#Region "Inicializadores"
    ' Public Index As Index

    '    Private DropDowndata As New ArrayList
    'Este flag me dice si trabajo con data1 o data2
    ' Private FlagData2 As Boolean
    'Picker para las fechas
    '    Private WithEvents DT As Windows.Forms.DateTimePicker
    'Formulario para las listas de Substitucion
    '   Private frmListaSubstitucion As frmIndexSubtitutiom
    Private AutoSubstitucionTable As DataTable

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            RemoveHandlers()
            If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                '                Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                '               Me.DT = New Windows.Forms.DateTimePicker
                '              Me.DT.CustomFormat = "dd/MM/yyyy"
                'Try
                '    Me.DT.Value = Date.ParseExact(Me.RealData, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'Catch ex As Exception
                '   zamba.core.zclass.raiseerror(ex)
                'End Try
                '            Me.Panel1.Controls.Add(DT)
                '           DT.Dock = Windows.Forms.DockStyle.Fill
                '          Me.ComboBox1.MaxLength = 10
                '       Me.Panel1.Visible = False
            Else
                Select Case Index.DropDown
                    'Para auto Substitucion
                    Case IndexAdditionalType.AutoSustitución
                        '                 Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                        '                Me.Button1 = New Zamba.AppBlock.ZButton
                        '               Me.Panel1.Controls.Add(Me.Button1)
                        '              Me.Button1.Dock = Windows.Forms.DockStyle.Fill
                        '''''''''''''''''''''''''''''''                       '                        Me.AutoSubstitucionTable = AutoSubstitutionDataFactory.GetIndexData(Me.Index.Id, False)
                        '  Me.ComboBox1.MaxLength = Me.Index.Len
                        '     Me.Panel1.Visible = False
                    Case IndexAdditionalType.DropDown
                        'Para dropdown
                        '     Me.Panel1.Visible = False
                        '               Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDown
                        '              Me.DropDowndata.Clear()
                        '             Me.DropDowndata.Add("")
                        '            Me.DropDowndata.AddRange(Indexs_Factory.GetDropDownList(Me.Index.Id))
                        '           Me.ComboBox1.Items.Clear()
                        'Try
                        '          Me.ComboBox1.Items.AddRange(Me.DropDowndata.ToArray)
                        'Catch ex As System.OutOfMemoryException
                        '	zamba.core.zclass.raiseerror(ex)
                        'Catch ex As Exception
                        '	zamba.core.zclass.raiseerror(ex)
                        'End Try
                        '              Me.ComboBox1.Dock = Windows.Forms.DockStyle.Fill
                        '             Me.ComboBox1.MaxLength = Me.Index.Len
                    Case IndexAdditionalType.LineText
                        '             Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                        '    Me.Panel1.Visible = False
                        '              Me.ComboBox1.Dock = Windows.Forms.DockStyle.Fill
                        '             Me.ComboBox1.MaxLength = Me.Index.Len
                End Select
            End If
        Catch ex As Exception
            zclass.raiseerror(ex)
        Finally
            '  Me.Combobox1.ReadOnly = True
            AddHandlers()
        End Try
    End Sub

    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        Try
            RemoveHandlers()
            Me.Index = index
            'If IsNothing(index) = False Then Me.Index = index
            If Me.Index.DropDown = IndexAdditionalType.AutoSustitución OrElse Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                If RealData.IndexOf("-") = -1 Then
                    '                    Dim r() As dsSubstitucion.dsSubstitucionRow = Me.AutoSubstitucionTable.Select("Codigo=" & CInt(Me.RealData))
                    RealDataDescription = AutoSubstitutionBusiness.getDescription(RealData.ToString, Me.Index.ID, False, Me.Index.Type)
                    If RealDataDescription.Length > 0 Then
                        'el codigo pertenece a la lista
                        Combobox1.Text = RealDataDescription
                    Else
                        Combobox1.Text = RealData
                    End If
                Else
                    Combobox1.Text = RealData
                End If
            Else
                Combobox1.Text = RealData
            End If
            '        Me.IsValid = True
            AddHandlers()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        Combobox1.Text = index.DataTemp
    End Sub


    Public Shadows Event IndexChanged()
    Private Property RealData() As String
        Get
            ' If FlagData2 Then
            ' Return Me.Index.Data2
            '  Else
            Return Index.Data
            '  End If
        End Get
        Set(ByVal Value As String)
            ' If FlagData2 Then
            ' Me.Index.Data2 = Value
            ' Else
            Index.Data = Value
            '   End If
            RaiseEvent IndexChanged()
        End Set
    End Property
    Private Property RealDataDescription() As String
        Get
            ' If FlagData2 Then
            ' Return Me.Index.dataDescription
            ' Else
            Return Index.dataDescription
            ' End If
        End Get
        Set(ByVal Value As String)
            '  If FlagData2 Then
            'Me.Index.dataDescription = Value
            ' Else
            Index.dataDescription = Value
            ' End If
        End Set
    End Property
    Public Overrides Sub RollBack()
    End Sub
    'Public Property Data() As String
    '    Get
    '        If Me.IsValid = False Then Me.DataChangeProcedure(Me.ComboBox1, New EventArgs)
    '        If FlagData2 Then
    '            Return Me.Index.Data2
    '        Else
    '            Return Me.Index.Data
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        If FlagData2 Then
    '            Me.Index.Data2 = Value
    '        Else
    '            Me.Index.Data = Value
    '        End If
    '    End Set
    'End Property

#End Region

#Region "Eventos de los controles especificos"
    Private Shared Sub AddHandlers()
        Try
            '            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            '            AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            '            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            '            AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged

            '            Select Case Me.Index.Type
            '                Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
            '            RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
            '            AddHandler DT.ValueChanged, AddressOf Me.DateProcedure
            '            RemoveHandler DT.Validating, AddressOf Me.DateProcedure
            '            AddHandler DT.Validating, AddressOf Me.DateProcedure
            '                Case Else
            '            Select Case Me.Index.DropDown
            '                Case IndexAdditionalType.AutoSustitución
            '            RemoveHandler Button1.Click, AddressOf Me.AutoSustitutionProcedure
            '            AddHandler Button1.Click, AddressOf Me.AutoSustitutionProcedure
            '                Case IndexAdditionalType.DropDown
            '            RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
            '            AddHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
            '                Case IndexAdditionalType.LineText
            '            End Select
            '            End Select
        Catch
        End Try
    End Sub
    Private Shared Sub RemoveHandlers()
        Try
            'RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            'RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            'Select Case Me.Index.Type
            '    Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
            '        RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
            '        RemoveHandler DT.Validating, AddressOf Me.DateProcedure
            '    Case Else
            '        Select Case Me.Index.DropDown
            '            Case IndexAdditionalType.AutoSustitución
            '                RemoveHandler Button1.Click, AddressOf Me.AutoSustitutionProcedure
            '            Case IndexAdditionalType.DropDown
            '                RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
            '            Case IndexAdditionalType.LineText
            '        End Select
            'End Select
        Catch
        End Try
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Me.IsValid = False
            'RemoveHandlers()
            'If (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo OrElse Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) AndAlso Me.Index.DropDown <> IndexAdditionalType.AutoSustitución Then
            '    Try
            '        Me.ComboBox1.Text.Replace(",", ".")
            '        Me.ComboBox1.Text.Replace("..", ".")
            '        If IsNumeric(Me.ComboBox1.Text) = False Then
            '            'Deberia reemplazar todas las letras no solo la ultima
            '            Try
            '                Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
            '            Catch
            '            End Try
            '            Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
            '            Me.ComboBox1.SelectionLength = 0
            '        End If
            '    Catch ex As Exception
            '       zamba.core.zclass.raiseerror(ex)
            '    End Try
            'ElseIf Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
            '    Try
            '        Dim C As Char = Me.ComboBox1.Text.Chars(Me.ComboBox1.Text.Length - 1)
            '        If Char.IsLetter(C) Then
            '            Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
            '            Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
            '            Me.ComboBox1.SelectionLength = 0
            '        End If
            '    Catch ex As Exception
            '       zamba.core.zclass.raiseerror(ex)
            '    End Try
            'End If
        Catch ex As Exception
            zclass.raiseerror(ex)
        Finally
            AddHandlers()
        End Try
    End Sub

    Private Sub DropDownProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '            Me.IsValid = False
            'Me.ComboBox1.Select()
            'Me.ComboBox1.SelectionLength = 0  ' Me.ComboBox1.Text.Length - 1
            'Me.ComboBox1.SelectionStart = 0
        Catch
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '     Me.IsValid = False
            'RemoveHandlers()
            'Me.ComboBox1.Text = DT.Value.ToShortDateString
            ''     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            'AddHandlers()
            'Me.ComboBox1.Select()
        Catch
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As System.ComponentModel.CancelEventArgs)
        Try
            '' Me.IsValid = False
            'RemoveHandlers()
            'Me.ComboBox1.Text = DT.Value.ToShortDateString
            ''     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            'AddHandlers()
            'Me.ComboBox1.Select()
        Catch
        End Try
    End Sub
    Private Sub AutoSustitutionProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            ''  Me.IsValid = False
            'RemoveHandlers()
            'If Me.frmListaSubstitucion Is Nothing Then
            '    Me.frmListaSubstitucion = New frmIndexSubtitutiom(Me.Index.Id, Me.AutoSubstitucionTable)
            'End If
            'Me.frmListaSubstitucion.ShowDialog()
            'If Me.frmListaSubstitucion.DialogResult = DialogResult.OK Then
            '    Me.ComboBox1.Text = Me.frmListaSubstitucion.Descripcion
            '    Me.ComboBox1.SelectionStart = 0
            '    Me.ComboBox1.SelectionLength = 0   'Me.ComboBox1.Text.Length - 1
            '    Me.RealData = Me.frmListaSubstitucion.Codigo
            'End If
            'AddHandlers()
            'Me.ComboBox1.Select()
        Catch
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            '    RemoveHandlers()
            '    If Me.ComboBox1.Text = "" AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Me.RealData = Me.ComboBox1.Text
            '        If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
            '            Try
            '                If IsNothing(Me.DT) = False Then
            '                    Me.DT.Value = Now
            '                End If
            '            Catch
            '            End Try
            '        End If
            '        Me.IsValid = True
            '    ElseIf Me.ComboBox1.Text = "" Then
            '        Me.IsValid = True
            '    ElseIf Me.Index.Type = IndexDataType.Fecha AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Try
            '            Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", "/")
            '            Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(".", "/")
            '            Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")
            '            Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
            '            Me.ComboBox1.SelectionLength = 0

            '            Dim d As Date = Date.Parse(Me.ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
            '            If IsNothing(Me.DT) = False Then
            '                If Me.DT.Value = D Then
            '                    Me.ComboBox1.Text = DT.Value.ToShortDateString
            '                Else
            '                    Me.DT.Value = D
            '                End If
            '            End If
            '            'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
            '            '            Me.RealData = d.ToString("dd/MM/yyyy")
            '            Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
            '            Me.IsValid = True
            '        Catch ex As System.FormatException
            '            
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As System.ArgumentOutOfRangeException
            '            
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As System.IndexOutOfRangeException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As System.InvalidCastException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As OverflowException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As Exception
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        End Try
            '    ElseIf Me.Index.Type = IndexDataType.Fecha_Hora AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Try
            '            Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", "/")
            '            Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")
            '            Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
            '            Me.ComboBox1.SelectionLength = 0

            '            Dim d As DateTime = DateTime.Parse(Me.ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
            '            If IsNothing(Me.DT) = False Then
            '                If Me.DT.Value = D Then
            '                    Me.ComboBox1.Text = DT.Value.ToShortTimeString
            '                Else
            '                    Me.DT.Value = D
            '                End If
            '            End If
            '            'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
            '            '            Me.RealData = d.ToString("dd/MM/yyyy")
            '            Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
            '            Me.IsValid = True
            '        Catch ex As System.FormatException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As System.ArgumentOutOfRangeException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As System.IndexOutOfRangeException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As System.InvalidCastException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As OverflowException
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch ex As Exception
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        Catch
            '            If Me.Mode = Modes.Search Then
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
            '                'Muestro el icono de error
            '            Else
            '                Me.ComboBox1.Text = Me.RealData
            '                Me.IsValid = True
            '            End If
            '            Try
            '                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '            Catch
            '            End Try
            '        End Try
            '    ElseIf Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
            '        If IsNumeric(Me.ComboBox1.Text) AndAlso Me.ComboBox1.Text.IndexOf("-") = -1 Then
            '            Dim r() As dsSubstitucion.dsSubstitucionRow = Me.AutoSubstitucionTable.Select("Codigo=" & CInt(Me.ComboBox1.Text))
            '            If r.Length > 0 Then
            '                'el codigo pertenece a la lista
            '                Me.RealData = r(0).Codigo
            '                Me.RealDataDescription = r(0).Descripcion
            '                Me.ComboBox1.Text = r(0).Descripcion
            '                Me.IsValid = True
            '            Else
            '                'el codigo no pertenece a la lista
            '                Me.RealData = CInt(Me.ComboBox1.Text)
            '                Me.IsValid = True
            '            End If
            '        Else
            '            Dim r() As dsSubstitucion.dsSubstitucionRow = Me.AutoSubstitucionTable.Select("Descripcion='" & Me.ComboBox1.Text & "'")
            '            'la descripcion pertenece a la lista
            '            If r.Length = 1 Then
            '                Me.RealData = r(0).Codigo
            '                Me.ComboBox1.Text = r(0).Descripcion
            '                Me.RealDataDescription = r(0).Descripcion
            '                Me.IsValid = True
            '            ElseIf r.Length > 1 Then
            '                Dim i As Int32
            '                For i = 0 To r.Length - 1
            '                    If Me.RealData = r(i).Codigo Then
            '                        Me.ComboBox1.Text = r(i).Descripcion
            '                        Me.RealDataDescription = r(0).Descripcion
            '                        Me.IsValid = True
            '                    End If
            '                Next
            '                If Me.IsValid = False Then
            '                    Me.RealData = r(0).Codigo
            '                    Me.ComboBox1.Text = r(0).Descripcion
            '                    Me.RealDataDescription = r(0).Descripcion
            '                    Me.IsValid = True
            '                End If
            '            Else
            '                'la descripcion no pertenece a la lista
            '                '                        Me.ComboBox1.Text = Me.RealData
            '                
            '                Me.ErrorProvider1.SetError(Me.ComboBox1, "El texto ingresado no pertenece a la lista")
            '            End If
            '        End If
            '    ElseIf Me.Index.DropDown = IndexAdditionalType.DropDown AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Me.RealData = Me.ComboBox1.Text
            '        Me.IsValid = True
            '    ElseIf (Me.Index.Type = IndexDataType.Numerico Or Me.Index.Type = IndexDataType.Numerico_Largo) AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Try
            '            Me.ComboBox1.Text.Replace(",", ".")
            '            Me.ComboBox1.Text.Replace("..", ".")
            '            Decimal.Parse(Me.ComboBox1.Text)
            '            Me.RealData = Me.ComboBox1.Text
            '            Me.IsValid = True
            '        Catch ex As Exception
            '            '                    Me.ComboBox1.Text = Me.RealData
            '            
            '            Me.ErrorProvider1.SetError(Me.ComboBox1, "El valor ingresado no es numerico")
            '            '                   Me.IsValid = True
            '        End Try
            '    ElseIf (Me.Index.Type = IndexDataType.Moneda Or Me.Index.Type = IndexDataType.Numerico_Decimales) AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Try
            '            Me.ComboBox1.Text.Replace(",", ".")
            '            Me.ComboBox1.Text.Replace("..", ".")
            '            Dim n As Decimal = CDec(Me.ComboBox1.Text)
            '            Me.RealData = Me.ComboBox1.Text
            '            Me.IsValid = True
            '        Catch ex As Exception
            '            '                 Me.ComboBox1.Text = Me.RealData
            '            Me.ErrorProvider1.SetError(Me.ComboBox1, "El valor ingresado no es numerico")
            '            '                  Me.IsValid = True
            '        End Try
            '    ElseIf (Me.Index.Type = IndexDataType.Alfanumerico Or Me.Index.Type = IndexDataType.Alfanumerico_Largo) AndAlso Me.RealData <> Me.ComboBox1.Text Then
            '        Me.RealData = Me.ComboBox1.Text
            '        Me.IsValid = True
            '    End If
            'Catch ex As FormatException
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
            '   zamba.core.zclass.raiseerror(ex)
            'Catch ex As ArgumentNullException
            '   
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
            '   zamba.core.zclass.raiseerror(ex)
            'Catch ex As InvalidCastException
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
            '   zamba.core.zclass.raiseerror(ex)
            'Catch ex As OutOfMemoryException
            '    
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
            '   zamba.core.zclass.raiseerror(ex)
            'Catch ex As OverflowException
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
            '   zamba.core.zclass.raiseerror(ex)
            'Catch ex As Exception
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
            '   zamba.core.zclass.raiseerror(ex)
            'Catch
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
        Catch
            '    End Try
            'Finally
            '    AddHandlers()
        End Try
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
                If Button1 IsNot Nothing Then
                    Button1.Dispose()
                    Button1 = Nothing
                End If
                If Combobox1 IsNot Nothing Then
                    Combobox1.Dispose()
                    Combobox1 = Nothing
                End If
                If AutoSubstitucionTable IsNot Nothing Then
                    AutoSubstitucionTable.Dispose()
                    AutoSubstitucionTable = Nothing
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
    ' Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Button1 As ZButton
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    ' Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Combobox1 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(txtReadOnlyIndexCtrl))
        '    Me.Panel1 = New ZPanel
        '   Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        '   Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        Combobox1 = New ZLabel
        SuspendLayout()
        '
        'Panel1
        '
        '   Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        '   Me.Panel1.Location = New System.Drawing.Point(504, 0)
        '   Me.Panel1.Name = "Panel1"
        '   Me.Panel1.Size = New System.Drawing.Size(24, 21)
        '   Me.Panel1.TabIndex = 1
        '
        'ImageList1
        '
        '   Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        '   Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        '  Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ErrorProvider1
        '
        '   Me.ErrorProvider1.ContainerControl = Me
        '
        'Combobox1
        '
        Combobox1.BackColor = Color.White
        Combobox1.BorderStyle = BorderStyle.FixedSingle
        Combobox1.Dock = DockStyle.Fill
        Combobox1.TextAlign = ContentAlignment.MiddleLeft
        Combobox1.ForeColor = Color.FromArgb(76, 76, 76)
        '  Me.ErrorProvider1.SetIconPadding(Me.Combobox1, -40)
        Combobox1.Location = New Point(0, 0)
        Combobox1.Name = "Combobox1"
        Combobox1.Size = New Size(504, 20)
        Combobox1.TabIndex = 0
        Combobox1.Text = ""
        '
        'txtReadOnlyIndexCtrl
        '
        backcolor = Color.FromArgb(214, 213, 217)
        Controls.Add(Combobox1)
        '  Me.Controls.Add(Me.Panel1)
        Name = "txtReadOnlyIndexCtrl"
        Size = New Size(528, 21)
        ResumeLayout(False)

    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Combobox1.GotFocus
        Combobox1.BackColor = Color.FromArgb(255, 224, 192)
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Combobox1.LostFocus
        RemoveHandlers()
        Combobox1.BackColor = Color.White
        If IsValid = False Then DataChangeProcedure(Combobox1, New ComponentModel.CancelEventArgs)
        AddHandlers()
    End Sub

#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Private Sub txtIndexCtrl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub txtIndexCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(13) Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyChar = Chr(9) Then
            RaiseEvent TabPressed()
        End If

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Combobox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

#End Region

#Region "Multiplecharacter TextBox"
    'Private Sub ComboBox1_DoubleCLick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combobox1.DoubleClick
    '    'If Me.Index.Type = IndexDataType.Alfanumerico Or Me.Index.Type = IndexDataType.Alfanumerico_Largo Then
    '    '    Dim frm As New Form1(Me.Index.Data, Me.Index.Name)
    '    '    frm.ShowDialog()
    '    '    Me.ComboBox1.Text = frm.TextBox1.Text
    '    '    Me.ComboBox1.Select()
    '    '    Me.ComboBox1.Focus()
    '    'End If
    'End Sub
#End Region

    'Private Sub DT_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DT.MouseDown
    '    Try
    '        'Me.IsValid = False
    '        'Me.DateProcedure(Me.DT, New EventArgs)
    '    Catch
    '    End Try
    'End Sub

    'Private Sub DT_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DT.CloseUp
    '    Try
    '        'Me.IsValid = False
    '        'Me.DateProcedure(Me.DT, New EventArgs)
    '    Catch
    '    End Try
    'End Sub

    Public Overrides Sub Commit()

    End Sub
End Class


'                Catch ex As Exception
'Try
'    Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'    If IsNothing(Me.DT) = False Then
'        If Me.DT.Value = D Then
'            Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'        Else
'            Me.DT.Value = D
'        End If
'    End If
'    Me.RealData = d.ToString("dd/MM/yyyy")
'    Me.IsValid = True
'catch ex as exception
'                        Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                            If IsNothing(Me.DT) = False Then
'                                If Me.DT.Value = D Then
'                                    Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                Else
'                                    Me.DT.Value = D
'                                End If
'                            End If
'                            Me.RealData = d.ToString("dd/MM/yyyy")
'                            Me.IsValid = True
'                        Catch
'                            Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/M/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                If IsNothing(Me.DT) = False Then
'                                    If Me.DT.Value = D Then
'                                        Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                    Else
'                                        Me.DT.Value = D
'                                    End If
'                                End If
'                                Me.RealData = d.ToString("dd/MM/yyyy")
'                                Me.IsValid = True
'                            Catch
'                                Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                    If IsNothing(Me.DT) = False Then
'                                        If Me.DT.Value = D Then
'                                            Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                        Else
'                                            Me.DT.Value = D
'                                        End If
'                                    End If
'                                    Me.RealData = d.ToString("dd/MM/yyyy")
'                                    Me.IsValid = True
'                                Catch
'                                    Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                        If IsNothing(Me.DT) = False Then
'                                            If Me.DT.Value = D Then
'                                                Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                            Else
'                                                Me.DT.Value = D
'                                            End If
'                                        End If
'                                        Me.RealData = d.ToString("dd/MM/yyyy")
'                                        Me.IsValid = True
'                                    Catch
'                                        Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/MM/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                            If IsNothing(Me.DT) = False Then
'                                                If Me.DT.Value = D Then
'                                                    Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                Else
'                                                    Me.DT.Value = D
'                                                End If
'                                            End If
'                                            Me.RealData = d.ToString("dd/MM/yyyy")
'                                            Me.IsValid = True
'                                        Catch
'                                            Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/MM/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                If IsNothing(Me.DT) = False Then
'                                                    If Me.DT.Value = D Then
'                                                        Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                    Else
'                                                        Me.DT.Value = D
'                                                    End If
'                                                End If
'                                                Me.RealData = d.ToString("dd/MM/yyyy")
'                                                Me.IsValid = True
'                                            Catch
'                                                Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/M/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                    If IsNothing(Me.DT) = False Then
'                                                        If Me.DT.Value = D Then
'                                                            Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                        Else
'                                                            Me.DT.Value = D
'                                                        End If
'                                                    End If
'                                                    Me.RealData = d.ToString("dd/MM/yyyy")
'                                                    Me.IsValid = True
'                                                Catch
'                                                    Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/M/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                        If IsNothing(Me.DT) = False Then
'                                                            If Me.DT.Value = D Then
'                                                                Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                            Else
'                                                                Me.DT.Value = D
'                                                            End If
'                                                        End If
'                                                        Me.RealData = d.ToString("dd/MM/yyyy")
'                                                        Me.IsValid = True
'                                                    Catch
'                                                        Try
'                                                            If IsNothing(Me.DT) = False Then Me.DT.Value = Date.ParseExact(Now, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                        Catch
'                                                        End Try
'                                                        Me.IsValid = True
'                                                        If Me.Mode = Modes.Search Then
'                                                        Else
'                                                            Me.ComboBox1.Text = Me.RealData
'                                                        End If
'                                                    End Try
'                                                End Try
'                                            End Try
'                                        End Try
'                                    End Try
'                                End Try
'                            End Try
'                        End Try
