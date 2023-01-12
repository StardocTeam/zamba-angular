Imports Zamba.Core
Imports System.Collections.Generic
Imports Zamba.Core.Cache

Public Class IndexController
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        AddHandler MyBase.KeyDown, AddressOf IndexController_KeyDown
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                'sacar todos los eventos
                'liberar los objetos dinamicos
                RemoveHandler EnterPressed, AddressOf Enter_KeyDown
                RemoveHandler MyBase.KeyDown, AddressOf IndexController_KeyDown

                If DocTypesAndIndexs.loadedControls IsNot Nothing Then
                    'TODO: Verificar que el clear no joda referencias
                    If DocTypesAndIndexs.loadedControls.Count > 0 Then

                        For i As Int32 = 0 To DocTypesAndIndexs.loadedControls.Values.Count - 1
                            If DocTypesAndIndexs.loadedControls.Values(i) IsNot Nothing Then
                                'TODO: verificar que la liberacion en esta parte sea correcta. Tengo dudas sobre si la referencia 
                                'del objeto de loadedControls.Values(i) es la misma que la que se utiliza despues del DirectCast.
                                RemoveHandler DirectCast(DocTypesAndIndexs.loadedControls.Values(i), SimpleIndexSearchCtrl).EnterPressed, AddressOf Enter_KeyDown
                                RemoveHandler DirectCast(DocTypesAndIndexs.loadedControls.Values(i), SimpleIndexSearchCtrl).OperatorClicked, AddressOf Operator_KeyDown
                                RemoveHandler DirectCast(DocTypesAndIndexs.loadedControls.Values(i), SimpleIndexSearchCtrl).TabPressed, AddressOf TabPress
                                RemoveHandler DirectCast(DocTypesAndIndexs.loadedControls.Values(i), SimpleIndexSearchCtrl).ItemChanged, AddressOf ItemChanged

                                DirectCast(DocTypesAndIndexs.loadedControls.Values(i), IDisposable).Dispose()
                            End If
                        Next

                        DocTypesAndIndexs.loadedControls.Clear()

                    End If

                    DocTypesAndIndexs.loadedControls = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        SuspendLayout()
        '
        'IndexController
        '
        AutoScroll = True
        MyBase.BackColor = Color.FromArgb(236, 236, 236)
        Name = "IndexController"
        Size = New Size(328, 208)
        ResumeLayout(False)

    End Sub

#End Region


    Dim doctID As Int64

#Region "Creo los atributos"
    ''' <summary>
    ''' Se modifico el método para poder poner en los text box de búsqueda los valores de los atributos por 
    ''' defecto, así de esta forma se puede realizar una busqueda predeterminada o por defecto. (para BOSTON)
    ''' La ultima modificación fue para que muestre el valor de una consulta sql cargada como valor por
    ''' defecto para un inidice.[sebastián 05/11/2008]
    ''' </summary>
    ''' <param name="DocTypes"></param>
    ''' <remarks>sebastian</remarks>
    Public Sub AddIndexFieldsNew(ByVal DocTypes As ArrayList)

        Dim Indexs As Index() = Nothing

        Try
            Indexs = ZCore.FilterSearchIndex(DocTypes)

            doctID = Int64.Parse(DocTypes.Item(0).ToString())
            ShowIndex(Indexs, DocTypes)
            Dim tindex As Int32 = Controls.Count - 1
            'establesco los tabindex
            For Each control As Control In Controls
                control.TabIndex = tindex
                tindex -= 1
            Next
        Catch ex As ArgumentException
            ZClass.raiseerror(ex)
        Finally
            Indexs = Nothing

        End Try
    End Sub

    Private Sub TabPress()
        SelectNextControl(Me, True, True, True, True)
    End Sub

    'Metodo que se dispara al cambiar el valor de un  indice jerarquico
    'Recibe el ID del incice que cambio y el nuevo valor
    Public Sub ItemChanged(ByVal IndexID As Long, ByVal NewValue As String)
        If DocTypesAndIndexs.loadedControls IsNot Nothing AndAlso DocTypesAndIndexs.loadedControls.Count > 0 Then

            For Each key As Long In DocTypesAndIndexs.loadedControls.Keys
                If DocTypesAndIndexs.loadedControls(key).Index.HierarchicalParentID = IndexID Then
                    If DocTypesAndIndexs.loadedControls(key).ParentData <> NewValue Then
                        DocTypesAndIndexs.loadedControls(key).ParentData = NewValue
                    End If
                End If
            Next
        End If
    End Sub

    Public Function GetControl(ByVal index As IIndex, ByVal cindex As Int32) As SimpleIndexSearchCtrl
        Dim c As SimpleIndexSearchCtrl
        If DocTypesAndIndexs.loadedControls Is Nothing Then DocTypesAndIndexs.loadedControls = New Hashtable()
        If DocTypesAndIndexs.loadedControls.ContainsKey(index.ID) Then
            c = DirectCast(DocTypesAndIndexs.loadedControls(index.ID), SimpleIndexSearchCtrl)
        Else
            c = New SimpleIndexSearchCtrl(index)
            RemoveHandler c.EnterPressed, AddressOf Enter_KeyDown
            AddHandler c.EnterPressed, AddressOf Enter_KeyDown
            RemoveHandler c.OperatorClicked, AddressOf Operator_KeyDown
            AddHandler c.OperatorClicked, AddressOf Operator_KeyDown
            RemoveHandler c.TabPressed, AddressOf TabPress
            AddHandler c.TabPressed, AddressOf TabPress
            RemoveHandler c.ItemChanged, AddressOf ItemChanged
            AddHandler c.ItemChanged, AddressOf ItemChanged
            DocTypesAndIndexs.loadedControls.Add(index.ID, c)
        End If
        c.Dock = DockStyle.Top
        ' c.SetBounds(5, cindex * c.Height + 25, Me.Width - 45, c.Height)
        Return c
    End Function
    Public Sub Operator_KeyDown(ByVal Control As SimpleIndexSearchCtrl)
        Try
            Dim SO As New SimpleOperatorControl(Control.Index)
            SO.Top = MousePosition.Y + 15
            SO.Left = MousePosition.X
            SO.[Operator] = Control.[Operator]
            RemoveHandler SO.LostFocus, AddressOf SOLostFocus
            AddHandler SO.LostFocus, AddressOf SOLostFocus
            SO.ShowDialog()
            Control.[Operator] = SO.[Operator]
            SO = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' [sebastian 04-02-2009] se corrigio para poder visualizar los controles correctamente, uno debajo del otro y no que se 
    ''' generen espacios entre ellos cuando se le da permismo de visualizacion.
    ''' </summary>
    ''' <param name="indexs"></param>
    ''' <param name="doctypes"></param>
    ''' <remarks></remarks>
    Public Sub ShowIndex(ByVal indexs() As Index, Optional ByVal doctypes As ArrayList = Nothing)
        Dim dtids As New List(Of Long)
        Dim ViewSpecifiedIndex As Boolean = True
        Try
            For Each id As Int64 In doctypes
                dtids.Add(id)
                ' Si se hace una busqueda conbinada, si algun doctype tiene permiso para no filtrar atributos
                ' Bastaria para aplicar ese permiso a todos
                '[Sebastian] 10-06-2009 se agrego cast para salvar warning
                ViewSpecifiedIndex = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, id)
                If ViewSpecifiedIndex = False Then Exit For
            Next

            If ViewSpecifiedIndex = False OrElse IsNothing(doctypes) Then
                For i As Int32 = indexs.Count - 1 To 0 Step -1
                    Dim IndexControl As SimpleIndexSearchCtrl = GetControl(indexs(i), i)
                    Controls.Add(IndexControl)
                    RemoveHandler IndexControl.EnterPressed, AddressOf Enter_KeyDown
                    AddHandler IndexControl.EnterPressed, AddressOf Enter_KeyDown
                Next
            Else
                Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(dtids, Membership.MembershipHelper.CurrentUser.ID, True, True)

                '[sebastian 04-02-2009] se cargan los atributos que recibe el metodo en un arraylist para no tocar lo ya existente
                Dim IndexsList As New ArrayList(indexs)
                '[sebastian 04-02-2009] se recorren lo sindices buscando cuales tienen permiso de visualizacion
                'caso de notenerlo se lo saca del arraylist, que luego le va a ser pasado a "indexscontrols"
                'para visualizar los controles en la pantalla de busqueda.
                For Each CurrentIndex As Index In indexs
                    If IRI(CurrentIndex.ID) IsNot Nothing AndAlso DirectCast(IRI(CurrentIndex.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexSearch) = False Then
                        IndexsList.Remove(CurrentIndex)
                    End If
                Next

                For i As Int32 = IndexsList.Count - 1 To 0 Step -1
                    If IRI(DirectCast(IndexsList(i), Index).ID) Is Nothing OrElse DirectCast(IRI(DirectCast(IndexsList(i), Index).ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexSearch) = True Then
                        Dim IndexControl As SimpleIndexSearchCtrl = GetControl(DirectCast(IndexsList(i), Index), i)
                        Controls.Add(IndexControl)
                        RemoveHandler IndexControl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler IndexControl.EnterPressed, AddressOf Enter_KeyDown
                    End If
                Next
            End If
            Dim zp1 As New ZPanel
            zp1.Height = 300
            zp1.Dock = DockStyle.Bottom
            zp1.BackColor = Color.White
            Controls.Add(zp1)
            Dim tindex As Int32 = Controls.Count - 1
            For Each control As Control In Controls
                control.TabIndex = tindex
                tindex -= 1
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            dtids.Clear()
            dtids = Nothing
        End Try
    End Sub

#End Region

#Region "Eventos TAB y ENTER"
    Public Shadows Event EnterPressed()
    Private Sub Enter_KeyDown()
        RaiseEvent EnterPressed()
    End Sub
#End Region

#Region "Metodos que obtienen los atributos validos y de busqueda"
    Public Function IsValid() As Boolean
        Try
            Dim i As Integer
            For i = 0 To Controls.Count - 1
                If TypeOf (Controls(i)) Is SimpleIndexSearchCtrl Then
                    '[sebastian] 10-06-2009 se agrego cast para salvar warning
                    Dim c As SimpleIndexSearchCtrl = DirectCast(Controls(i), SimpleIndexSearchCtrl)
                    If c.isValid = False Then
                        Return False
                        Exit For
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los atributos necesarios para la busqueda, es decir los que fueron completados por el usuario
    ''' </summary>
    ''' <returns>Coleccion de Objetos Atributos</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	18/07/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetSearchIndexs() As List(Of IIndex)
        Dim index As New Index
        Dim r As New List(Of IIndex)
        Dim i As Integer

        For i = 0 To Controls.Count - 1
            If TypeOf (Controls(i)) Is SimpleIndexSearchCtrl Then
                '[sebastian] 10-06-2009 se agrego cast para salvar warning
                Dim c As SimpleIndexSearchCtrl = DirectCast(Controls(i), SimpleIndexSearchCtrl)
                If c.isSearched Then
                    r.Add(New Index(c.Index, True))
                End If
            End If
        Next
        Return r
    End Function
#End Region

    'Metodo que borra los datos ingresados
    Public Sub CleanIndexs()
        Try
            For Each c As SimpleIndexSearchCtrl In DocTypesAndIndexs.loadedControls.Values
                c.Clean()
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Metodo que borra los atributos
    Public Sub ClearIndexs()
        Try
            Controls.Clear()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub IndexController_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
    End Sub

    Private Sub SOLostFocus(sender As Object, e As EventArgs)
        DirectCast(sender, SimpleOperatorControl).Close()
    End Sub

End Class
