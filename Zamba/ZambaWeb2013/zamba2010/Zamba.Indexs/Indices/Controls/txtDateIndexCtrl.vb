Imports ZAMBA.AppBlock
Imports ZAMBA.Core
Public Class txtDateIndexCtrl
    Inherits txtBaseIndexCtrl

#Region "Constructores"
    Private Mode As Modes
    Public Sub New(ByVal docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Me.Index = docindex
        Me.FlagData2 = data2
        Me.Init()
        RefreshControl(Index)
    End Sub
    Public Sub New(ByVal mode As Modes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Me.Mode = mode

    End Sub
#End Region

#Region "Propiedades Publicas"
    Private Shadows _IsValid As Boolean = True
    Public Overrides Property IsValid() As Boolean
        Get
            If _IsValid = False Then
                Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
                'Me.ErrorProvider1.SetError(Me.ComboBox1, "")
            End If
            Return _IsValid
        End Get
        Set(ByVal Value As Boolean)
            _IsValid = Value
            If Value = True Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "")
            End If
        End Set
    End Property
#End Region

#Region "Inicializadores"
    Private FlagData2 As Boolean
    'Picker para las fechas
    Private WithEvents DT As Windows.Forms.DateTimePicker

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            'Me.SuspendLayout()
            Me.RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                Me.DT = New Windows.Forms.DateTimePicker
                Me.DT.CustomFormat = "dd/MM/yyyy"
                Try
                    Me.DT.TabStop = False
                    If IsNothing(Me.Data) = False AndAlso Me.Data.Length > 0 Then Me.DT.Value = Date.ParseExact(Me.Data, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Catch ex As FormatException
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
                Me.Panel1.Controls.Add(DT)
                DT.Dock = Windows.Forms.DockStyle.Fill
                Me.ComboBox1.MaxLength = 10
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
            '   Me.ResumeLayout()
        End Try
    End Sub

    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        Try
            RemoveHandlers()
            Me.Index = index
            If index Is Nothing = False Then
                If Not (index.Data = String.Empty) Then
                    Dim dia As Date
                    If Date.TryParse(index.Data, dia) Then
                        index.Data = dia.ToString("dd/MM/yyyy")
                        Me.Index.DataTemp = index.Data
                        Me.Index.DataTemp2 = index.Data2
                        Me.Index.Data = index.Data
                        Me.Index.dataDescriptionTemp = index.dataDescription
                        Me.Index.dataDescriptionTemp2 = index.dataDescription2
                    End If
                End If
            End If

            If Me.Data = String.Empty Then
                Me.ComboBox1.Text = Me.Data
            Else
                Me.DT.Value = CType(Me.Data, Date)
                Me.DT.CustomFormat = "dd/MM/yyyy"
                Me.DateProcedure(Me.DT, New EventArgs)
            End If

            'CAMBIE REALDTA POR DATA
            ' Me.ComboBox1.Text = Me.Data
            Me.IsValid = True
            AddHandlers()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        Me.ComboBox1.Text = index.DataTemp
    End Sub
    Public Overrides Sub RollBack()
        Index.DataTemp = Index.Data
        Index.DataTemp2 = Index.Data2
        Index.dataDescriptionTemp = Index.dataDescription
        Index.dataDescriptionTemp2 = Index.dataDescription2
        RefreshControl(Index)
    End Sub
    Public Overrides Sub Commit()
        If IsNothing(Index.DataTemp) = False Then
            Index.Data = Index.DataTemp
            Index.dataDescription = Index.dataDescriptionTemp
            RefreshControl(Index)
        End If
        If IsNothing(Index.DataTemp2) = False Then
            Index.Data2 = Index.DataTemp2
            Index.dataDescription2 = Index.dataDescriptionTemp2
            RefreshControl(Index)
        End If
    End Sub

    Public Shadows Event IndexChanged()
    Private Property RealData() As String
        Get
            If FlagData2 Then
                Return Me.Index.DataTemp2
            Else
                Return Me.Index.DataTemp
            End If
        End Get
        Set(ByVal Value As String)

            Dim FlagCambioElContenido As Boolean = False

            If FlagData2 Then
                If IsNothing(Me.Index.DataTemp2) OrElse Me.Index.DataTemp2.Trim <> Value.Trim Then FlagCambioElContenido = True
                Me.Index.DataTemp2 = Value.Trim
            Else
                If IsNothing(Me.Index.DataTemp) OrElse Me.Index.DataTemp.Trim <> Value.Trim Then FlagCambioElContenido = True
                Me.Index.DataTemp = Value.Trim
            End If
            If FlagCambioElContenido = True Then RaiseEvent IndexChanged()
        End Set
    End Property
    Private ReadOnly Property Data() As String
        Get
            If FlagData2 Then
                Return Me.Index.Data2
            Else
                Return Me.Index.Data
            End If
        End Get
    End Property
    Private Property RealDataDescription() As String
        Get
            If FlagData2 Then
                Return Me.Index.dataDescriptionTemp2
            Else
                Return Me.Index.dataDescriptionTemp
            End If
        End Get
        Set(ByVal Value As String)
            If FlagData2 Then
                Me.Index.dataDescriptionTemp2 = Value
            Else
                Me.Index.dataDescriptionTemp = Value
            End If
        End Set
    End Property

#End Region

#Region "Eventos de los controles especificos"
    Private Sub AddHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
           
            'RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
            'AddHandler DT.ValueChanged, AddressOf Me.DateProcedure
            'RemoveHandler DT.Validating, AddressOf Me.DateProcedure
            'AddHandler DT.Validating, AddressOf Me.DateProcedure
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
           

            If Not IsNothing(DT) Then

                RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
                RemoveHandler DT.Validating, AddressOf Me.DateProcedure
            End If

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ComboBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim FlagLastCharSelected As Boolean = False
            If Me.ComboBox1.SelectionStart = Len(Me.ComboBox1.Text) Then
                FlagLastCharSelected = True
            End If

            Me.IsValid = False
            RemoveHandlers()
            If Me.ComboBox1.Text.CompareTo("") = 0 Then Me.ErrorProvider1.SetError(Me.ComboBox1, "")
            Try
                If Me.ComboBox1.Text.Length > 0 Then
                    Dim C As Char = Me.ComboBox1.Text.Chars(Me.ComboBox1.Text.Length - 1)
                    If Char.IsLetter(C) Then
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                        Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                        Me.ComboBox1.SelectionLength = 0
                    End If
                Else
                    Me.RealData = ""
                End If
            Catch ex As IndexOutOfRangeException
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
            ComboBox1.Tag = ComboBox1.Text
            'sebastian
            ComboBox1.Focus()
        End Try
    End Sub

    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try

            Me.ComboBox1.Text = DT.Value.ToShortDateString
            Me.ComboBox1.Select()

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As System.ComponentModel.CancelEventArgs)
        Try
            ' Me.IsValid = False
            RemoveHandlers()
            Me.ComboBox1.Text = DT.Value.ToShortDateString
            '     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            Me.ComboBox1.Select()
            AddHandlers()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()

            If Me.ComboBox1.Text = "" Then
                Me.IsValid = True
                Exit Sub
            ElseIf Me.Index.Type = IndexDataType.Fecha Then  'AndAlso Me.RealData <> Me.ComboBox1.Text
                Try
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", "/")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(".", "/")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")
                    Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                    Me.ComboBox1.SelectionLength = 0

                    Dim d As Date = Date.Parse(Me.ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(Me.DT) = False Then
                        If Me.DT.Value = d Then
                            Me.ComboBox1.Text = DT.Value.ToShortDateString
                        Else
                            Me.DT.Value = d
                        End If
                    End If
                    'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
                    Me.RealData = d.ToString("dd/MM/yyyy")
                    'Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    Me.IsValid = True
                    Exit Sub
                Catch ex As System.FormatException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.ArgumentOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.IndexOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.InvalidCastException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As OverflowException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                    'Catch
                    '    If Me.Mode = Modes.Search Then
                    '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                    '        'Muestro el icono de error
                    '    Else
                    '        Me.ComboBox1.Text = Me.RealData
                    '        Me.IsValid = True
                    '        Exit Sub
                    '    End If
                    '    Try
                    '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    '    Catch
                    '    End Try
                End Try
            ElseIf Me.Index.Type = IndexDataType.Fecha_Hora Then  'AndAlso Me.RealData <> Me.ComboBox1.Text
                Try
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", "/")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")
                    Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                    Me.ComboBox1.SelectionLength = 0

                    Dim d As DateTime = DateTime.Parse(Me.ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(Me.DT) = False Then
                        If Me.DT.Value = d Then
                            Me.ComboBox1.Text = DT.Value.ToShortTimeString
                        Else
                            Me.DT.Value = d
                        End If
                    End If
                    'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
                    '            Me.RealData = d.ToString("dd/MM/yyyy")
                    Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    Me.IsValid = True
                    Exit Sub
                Catch ex As System.FormatException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.ArgumentOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.IndexOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.InvalidCastException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As OverflowException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                    'Catch
                    '    If Me.Mode = Modes.Search Then
                    '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                    '        'Muestro el icono de error
                    '    Else
                    '        Me.ComboBox1.Text = Me.RealData
                    '        Me.IsValid = True
                    '        Exit Sub
                    '    End If
                    '    Try
                    '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    '    Catch
                    '    End Try
                End Try
            End If
        Catch ex As FormatException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As ArgumentNullException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As InvalidCastException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As OutOfMemoryException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As OverflowException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Exception
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
            'Catch
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '        Exit Sub
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
        Finally
            Me.IsValid = True
            AddHandlers()

        End Try
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

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
    Friend WithEvents ComboBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As ZPanel
    '    Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ComboBox1 = New System.Windows.Forms.TextBox
        Me.Panel1 = New Zamba.AppBlock.ZPanel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox1.ForeColor = System.Drawing.Color.Black
        Me.ErrorProvider1.SetIconPadding(Me.ComboBox1, -40)
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.CausesValidation = False
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(504, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(24, 20)
        Me.Panel1.TabIndex = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'txtIndexCtrl
        '
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "txtDateIndexCtrl"
        Me.Size = New System.Drawing.Size(528, 20)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    ''' <summary>
    ''' Se ejecuta cuando obtiene el foco el combo box del control de indice de zamba
    ''' </summary>
    ''' <history>
    ''' [Sebastian] 27-10-2009 se agreo evento para indice del tipo fecha, porqueno guardaba el cambio la primera vez
    ''' sino la segunda vez que se hacia clic en el control, esto era porque el evento changed se lanzaba cuando se perdia el foco.
    ''' </history>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.GotFocus
        RemoveHandlers()

        Me.ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
        If Me.IsValid = False Then Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
        AddHandlers()
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.LostFocus
        RemoveHandlers()
        Me.ComboBox1.BackColor = Color.White
        If Me.IsValid = False Then Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
        'If Me.IsValid = False Then Me.ErrorProvider1.SetError(Me.ComboBox1, "")
        AddHandlers()
    End Sub

#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Private Sub txtIndexCtrl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub txtIndexCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyChar = Chr(9) Then
            RaiseEvent TabPressed()
        End If

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub DT_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DT.CloseUp
        Try
            Me.IsValid = False
            Me.DateProcedure(Me.DT, New EventArgs)
        Catch
        End Try
    End Sub

    Private Sub DT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DT.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If

    End Sub
#End Region



End Class

