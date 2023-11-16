Public Class ExternalVisualizer
    Inherits Zamba.AppBlock.ZForm

    Dim Panel As Control
    Dim TabControl As TabControl
    Dim TabPage As TabPage
    Private _tabPage As TabPage
    Private OpenTab As Boolean

    Private Property TaskTabPage() As TabPage
        Get
            Return _tabPage
        End Get
        Set(ByVal value As TabPage)
            _tabPage = value
        End Set
    End Property
    Public Sub New(ByVal u As TabControl)

        ' Llamada necesaria para el Dise�ador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicializaci�n despu�s de la llamada a InitializeComponent().
        Try
            Me.Panel = u.Parent
            Me.TabControl = u
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub New(ByVal u As TabPage)

        ' Llamada necesaria para el Dise�ador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicializaci�n despu�s de la llamada a InitializeComponent().
        Try
            TabControl = New TabControl
            Me.Panel = u.Parent
            Me.TabPage = u
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#Region "Events"
    Public Event ClosedByFormCross(ByVal min As Boolean)

    ''' <summary>
    '''     [Sebastian] 16-06-2009 Evento que se lanza para poder avisar que se cerro el form desde la cruz del 
    '''                 formulario.
    ''' </summary>
    ''' <param name="tab">tab actulmente seleccionado</param>
    ''' <param name="ClosedFromCross">indica si se lo cerro desde la cruz del formulario</param>
    ''' <remarks></remarks>
    Public Event FullScreenClosed(ByVal tab As TabPage, ByVal ClosedFromCross As Boolean)
#End Region

    Private Sub ExternalVisualizer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '[Sebastian] 16-06-2009 Evento que se lanza para poder avisar que se cerro el form desde la cruz del 
        'formulario.
        If IsNothing(Me.TabControl) = False Or IsNothing(TabPage) = False Then
            If OpenTab Then
                RaiseEvent FullScreenClosed(Me.TabControl.SelectedTab, True)
            Else
                RaiseEvent FullScreenClosed(Me.TabPage, True)
            End If
        End If
    End Sub

    Private Sub ExternalVisualizer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If IsNothing(Me.TabControl) = False Then
            If OpenTab Then
                TabPage = TaskTabPage
                Dim AuxTabPage As New TabPage
                AuxTabPage = Me.Panel.Controls.Item(1)

                Me.Panel.Controls.Remove(Me.Panel.Controls.Item(1))
                Me.Panel.Controls.Add(Me.TabPage)
                Me.Panel.Controls.Add(AuxTabPage)

                If TabPage.Controls.Find("toolbar1", True).Length = 0 Then
                    RaiseEvent ClosedByFormCross(False)
                    Exit Sub
                End If
            Else
                Me.Panel.Controls.Add(Me.TabControl)
            End If

            '(pablo) - Valido si es la ultima tarea a cerrar en modo pantalla
            ' completa, si lo es, cierro el tab y vuelvo a Zamba
           

            Dim toolbar As ToolStrip
            If IsNothing(TabControl.SelectedTab) = False Or IsNothing(TabPage) = False Then
                If OpenTab Then
                    toolbar = DirectCast(TabPage.Controls.Find("toolbar1", True)(0), ToolStrip)
                Else
                    toolbar = DirectCast(TabControl.SelectedTab.Controls.Item("toolbar1"), ToolStrip)
                End If
                If IsNothing(toolbar) = False AndAlso String.Compare(toolbar.Items.Item(2).Text.ToLower, "restaurar pantalla") = 0 Then
                    toolbar.Items.Item(2).Text = "Pantalla Completa"
                    RaiseEvent ClosedByFormCross(False)
                End If
            Else
                RaiseEvent ClosedByFormCross(False)
            End If

        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' (Pablo)	09/10/2010 Created
    ''' (Pablo)	09/10/2010 Modificacion del metodo para que pueda trabajar con TabPages
    '''</remarks>
    ''' <history>
    '''     (Pablo)	09/10/2010	Created
    ''' </history>
    Private Sub ExternalVisualizer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(TabPage) Then
            OpenTab = True
            TabControl.Controls.Add(TabPage)
            TabPage.Dock = DockStyle.Fill
            TabControl.Dock = DockStyle.Fill
            Me.Controls.Add(TabControl)
        Else
            TabControl.Dock = DockStyle.Fill
            Me.Controls.Add(TabControl)
        End If

        TaskTabPage = TabPage
    End Sub
End Class