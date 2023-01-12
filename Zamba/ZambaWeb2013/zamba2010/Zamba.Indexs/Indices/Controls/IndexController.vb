Imports ZAMBA.Core
Imports System.Data
Imports Zamba.AppBlock
Imports System.Collections
Public Class IndexController
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'IndexController
        '
        Me.AutoScroll = True
        Me.Name = "IndexController"
        Me.Size = New System.Drawing.Size(328, 208)

    End Sub

#End Region

    '  Dim DSIndex As New DataSet
    Dim loadedControls As New Hashtable
    Dim doctID As Int64

#Region "Creo los indices"
    ''' <summary>
    ''' Se modifico el método para poder poner en los text box de búsqueda los valores de los indices por 
    ''' defecto, así de esta forma se puede realizar una busqueda predeterminada o por defecto. (para BOSTON)
    ''' La ultima modificación fue para que muestre el valor de una consulta sql cargada como valor por
    ''' defecto para un inidice.[sebastián 05/11/2008]
    ''' </summary>
    ''' <param name="DocTypes"></param>
    ''' <remarks>sebastian</remarks>
    Public Sub AddIndexFieldsNew(ByVal DocTypes As ArrayList)
        Dim dt As DataTable = Nothing
        Try
            Dim Indexs As Index() = ZCore.FilterSearchIndex(DocTypes)
            doctID = Int64.Parse(DocTypes.Item(0).ToString())
            'Dim ResultadoConsulta As String = String.Empty
            'Dim ValorIndice As String
            'dt = UserBusiness.Rights.GetDefaultSerchIndex(doctID)
            'Trace.WriteLineIf(ZTrace.IsVerbose,"COMIENZO****")
            'Trace.WriteLineIf(ZTrace.IsVerbose,"doc type id: " & doctID)
            'Trace.WriteLineIf(ZTrace.IsVerbose,"Cantidad Indices: " & Indexs.Length)
            'Trace.WriteLineIf(ZTrace.IsVerbose,"Filas: " & dt.Rows.Count)
            'For i As Integer = 0 To Indexs.Length - 1
            '    For Each row As DataRow In dt.Rows
            '        If String.Equals(Indexs(i).ID.ToString, row("indexid").ToString()) = True Then
            '            ValorIndice = row("value").ToString
            '            Trace.WriteLineIf(ZTrace.IsVerbose,"valor que traigo de default_search" & ValorIndice)
            '            'evaluo el valor que obtengo para saber si es una consulta sql
            '            If ValorIndice.ToLower.Contains("select") Then
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"contiene un select entonces es una consulta")
            '                Dim consulta As String = ValorIndice.ToLower.Substring(ValorIndice.ToLower.IndexOf("select"))
            '                Dim CurrentUser As String = UserBusiness.CurrentUser.ID.ToString
            '                consulta = Replace(consulta, """", "'")
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"valor de consulta" & consulta)
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"valor de CurrentUser" & CurrentUser)
            '                'evaluo si tiene where para saber donde insertar la tabla y el filtro id de usuario
            '                consulta = consulta.ToLower.Replace("currentuserid", CurrentUser)
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"consulta con el current user asignado: " & consulta)
            '                Dim valor As Object = ServersBusiness.BuildExecuteScalar(System.Data.CommandType.Text, consulta)
            '                If Not IsNothing(valor) Then
            '                    If (String.Compare(valor.ToString(), String.Empty) <> 0) Then
            '                        ResultadoConsulta = valor.ToString()
            '                    End If
            '                End If
            '                ValorIndice = ResultadoConsulta
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"valor que se le asigna al indice" & ValorIndice)
            '            End If

            '            Try
            '                Indexs(i).Data = ValorIndice
            '                Indexs(i).DataTemp = ValorIndice
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"Indexs(i).Name = " & Indexs(i).Name)
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"Indexs(i).Data = " & Indexs(i).Data)
            '                Trace.WriteLineIf(ZTrace.IsVerbose,"Indexs(i).Data = " & Indexs(i).DataTemp)
            '            Catch ex As Exception
            '                Zamba.Core.ZClass.raiseerror(ex)
            '                Exit For
            '            End Try
            '        End If
            '    Next
            'Next

            Dim IndexCount As Int32 = Indexs.Length - 1
            ShowIndex(Indexs, DocTypes)

            'establesco los tabindex
            Dim tindex As Int32 = 0
            For Each control As Control In Me.Controls
                control.TabIndex = tindex
                tindex += 1
            Next
        Catch ex As ArgumentException
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub TabPress()
        Me.SelectNextControl(Me, True, True, True, True)
    End Sub

    'Metodo que se dispara al cambiar el valor de un  indice jerarquico
    'Recibe el ID del incice que cambio y el nuevo valor
    Public Sub ItemChanged(ByVal IndexID As Long, ByVal NewValue As String)

        Dim ct As SimpleIndexSearchCtrl
        Dim parentID As Long = -1
        Dim childID As Long = -1

        Dim HierarchicalIndexs As New Hashtable  'todos los indices jerarquicos
        Dim TopIndexs As New Hashtable           'indices padre del indice que cambio
        Dim BottomIndexs As New Hashtable        'indices hijos del indice que cambio

        Dim parentIndexFound As Boolean

        'buscar todos los indices que tengan datos cargados y sean padres o hijos de otros
        For i As Int32 = 0 To Me.Controls.Count - 1
            ct = DirectCast(Me.Controls(i), SimpleIndexSearchCtrl)
            ct.isSearched()
            If ct.Index.HierarchicalChildID > 0 OrElse ct.Index.HierarchicalParentID > 0 Then
                HierarchicalIndexs.Add(ct.Index.ID, ct.Index)
            End If
        Next

        'obtengo el parent del indice que cambio y lo agrego a la coleccion de atributos padre
        If HierarchicalIndexs.Contains(IndexID) Then
            parentID = DirectCast(HierarchicalIndexs(IndexID), Index).HierarchicalParentID
            'If parentID <> -1 Then
            TopIndexs.Add(IndexID, HierarchicalIndexs(IndexID))
            'End If
        End If

        'si tengo un parent
        If parentID <> -1 Then

            'pasar al array definitivo solo los indices padre del que cambio empezando con el parent 
            'obtenido hasta que no encuentre mas
            Do
                If HierarchicalIndexs.Contains(parentID) Then
                    TopIndexs.Add(parentID, HierarchicalIndexs(parentID))
                    parentID = DirectCast(HierarchicalIndexs(parentID), Index).HierarchicalParentID
                End If
            Loop While CInt(parentID) <> CInt(-1)

        End If

        'obtengo el child del indice que cambio
        If HierarchicalIndexs.Contains(IndexID) Then
            childID = DirectCast(HierarchicalIndexs(IndexID), Index).HierarchicalChildID
        End If

        'si tengo un child
        If childID <> -1 Then

            'pasar al array definitivo solo los indices anteriores hijos del que cambio empezando con el child
            'obtenido hasta que no encuentre mas
            Do
                If HierarchicalIndexs.Contains(childID) AndAlso Not TopIndexs.Contains(childID) Then
                    BottomIndexs.Add(childID, HierarchicalIndexs(childID))
                    childID = DirectCast(HierarchicalIndexs(childID), Index).HierarchicalChildID
                Else
                    childID = -1
                End If
            Loop While CInt(childID) <> CInt(-1)

        End If

        'actualizar los indices hijos segun valores del/los padres
        For i As Int32 = 0 To Me.Controls.Count - 1
            ct = DirectCast(Me.Controls(i), SimpleIndexSearchCtrl)

            If BottomIndexs.Contains(ct.Index.ID) Then
                ct.Init(ct.Index, doctID, TopIndexs)
            End If
        Next

    End Sub

    ''Metodo que se dispara al cambiar el valor de un  indice jerarquico
    ''Recibe el ID del incice que cambio y el nuevo valor
    'Public Sub ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

    '    Dim ct As SimpleIndexSearchCtrl
    '    Dim ctNew As SimpleIndexSearchCtrl

    '    'si se disparo el evento es porque algun indice jerarquico cambio su valor
    '    'buscar cual otro indice depende de este y recargar sus valores
    '    For Each dict As DictionaryEntry In Me.loadedControls

    '        ct = DirectCast(dict.Value, SimpleIndexSearchCtrl)

    '        'si es el parent
    '        If ct.Index.HierarchicalParentID = IndexID Then
    '            ct.Init(ct.Index, doctID, NewValue)
    '        End If

    '    Next

    'End Sub

    Public Function GetControl(ByVal index As Index, ByVal cindex As Int32) As SimpleIndexSearchCtrl
        Dim c As SimpleIndexSearchCtrl
        If Me.loadedControls.ContainsKey(index.ID) Then
            c = DirectCast(Me.loadedControls(index.ID), SimpleIndexSearchCtrl)
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
            Me.loadedControls.Add(index.ID, c)
        End If

        c.SetBounds(5, cindex * c.Height, Me.Width - 21, c.Height)
        Return c
    End Function
    Public Sub Operator_KeyDown(ByVal Control As SimpleIndexSearchCtrl)
        Try
            Dim SO As New SimpleOperatorControl(Control.Index)
            SO.Top = MousePosition.Y + 15
            SO.Left = MousePosition.X
            SO.[Operator] = Control.[Operator]
            SO.ShowDialog()
            Control.[Operator] = SO.[Operator]
            SO = Nothing
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Dim IndexsControls() As SimpleIndexSearchCtrl
    Dim IndexsControlsAux() As SimpleIndexSearchCtrl
    ''' <summary>
    ''' [sebastian 04-02-2009] se corrigio para poder visualizar los controles correctamente, uno debajo del otro y no que se 
    ''' generen espacios entre ellos cuando se le da permismo de visualizacion.
    ''' </summary>
    ''' <param name="indexs"></param>
    ''' <param name="doctypes"></param>
    ''' <remarks></remarks>
    Public Sub ShowIndex(ByVal indexs() As Index, Optional ByVal doctypes As ArrayList = Nothing)
        Try
            Me.Invalidate()
            ReDim IndexsControls(indexs.Length)
            Dim dtids As New Generic.List(Of Int64)
            Dim ViewSpecifiedIndex As Boolean = True
            For Each id As Int64 In doctypes
                dtids.Add(id)
                ' Si se hace una busqueda conbinada, si algun doctype tiene permiso para no filtrar indices
                ' Bastaria para aplicar ese permiso a todos
                '[Sebastian] 10-06-2009 se agrego cast para salvar warning
                ViewSpecifiedIndex = UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, CInt(id))
                If ViewSpecifiedIndex = False Then Exit For
            Next

            If ViewSpecifiedIndex = False OrElse IsNothing(doctypes) Then
                For i As Int32 = 0 To indexs.Length - 1
                    IndexsControls.SetValue(Me.GetControl(indexs(i), i), i)
                    RemoveHandler IndexsControls(i).EnterPressed, AddressOf Me.Enter_KeyDown
                    AddHandler IndexsControls(i).EnterPressed, AddressOf Me.Enter_KeyDown
                Next
            Else
                Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(dtids, UserBusiness.CurrentUser.ID, True)

                '[sebastian 04-02-2009] se cargan los indices que recibe el metodo en un arraylist para no tocar lo ya existente
                Dim IndexsList As New ArrayList(indexs)
                '[sebastian 04-02-2009] se recorren lo sindices buscando cuales tienen permiso de visualizacion
                'caso de notenerlo se lo saca del arraylist, que luego le va a ser pasado a "indexscontrols"
                'para visualizar los controles en la pantalla de busqueda.
                For Each CurrentIndex As Index In indexs
                    If DirectCast(IRI(CurrentIndex.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexSearch) = False Then
                        IndexsList.Remove(CurrentIndex)
                    End If

                Next
                '[sebasitan 04-02-2009] se redimensiona el control a la cantidad exacta de controles
                'que se deben visualizar
                ReDim IndexsControls(IndexsList.Count)
                '[sebastian 04-02-2009] se van cargando los controles en "indexscontrols" para luego visualizarlos
                For i As Int32 = 0 To IndexsList.Count - 1
                    If DirectCast(IRI(DirectCast(IndexsList(i), Index).ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexSearch) = True Then
                        IndexsControls.SetValue(Me.GetControl(DirectCast(IndexsList(i), Index), i), i)
                        RemoveHandler IndexsControls(i).EnterPressed, AddressOf Me.Enter_KeyDown
                        AddHandler IndexsControls(i).EnterPressed, AddressOf Me.Enter_KeyDown
                    End If
                Next

                'For i As Int32 = 0 To indexs.Length - 1
                '    If DirectCast(IRI(indexs(i).ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexSearch) = True Then

                '        IndexsControls.SetValue(Me.GetControl(indexs(i), i), i)
                '        RemoveHandler IndexsControls(i).EnterPressed, AddressOf Me.Enter_KeyDown
                '        AddHandler IndexsControls(i).EnterPressed, AddressOf Me.Enter_KeyDown
                '    End If
                'Next

            End If
            'se muestran los controles
            Me.Controls.AddRange(IndexsControls)
            Me.Update()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Eventos TAB y ENTER"
    Public Shadows Event EnterPressed()
    Private Sub Enter_KeyDown()
        RaiseEvent EnterPressed()
    End Sub
#End Region

#Region "Metodos que obtienen los indices validos y de busqueda"
    Public Function IsValid() As Boolean
        Try
            Dim i As Integer
            For i = 0 To Me.Controls.Count - 1
                '[sebastian] 10-06-2009 se agrego cast para salvar warning
                Dim c As SimpleIndexSearchCtrl = DirectCast(Me.Controls(i), SimpleIndexSearchCtrl)
                If c.isValid = False Then
                    Return False
                    Exit For
                End If
            Next
            Return True
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return False
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los indices necesarios para la busqueda, es decir los que fueron completados por el usuario
    ''' </summary>
    ''' <returns>Coleccion de Objetos Indices</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	18/07/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetSearchIndexs() As Index()
        Dim index As New Index
        Dim r As New ArrayList
        Dim i As Integer

        For i = 0 To Me.Controls.Count - 1
            '[sebastian] 10-06-2009 se agrego cast para salvar warning
            Dim c As SimpleIndexSearchCtrl = DirectCast(Me.Controls(i), SimpleIndexSearchCtrl)
            If c.isSearched Then r.Add(c.Index)
        Next
        Return r.ToArray(index.GetType)
    End Function
#End Region

    'Metodo que borra los datos ingresados
    Public Sub CleanIndexs()
        Try
            For Each c As SimpleIndexSearchCtrl In Me.loadedControls.Values
                c.Clean()
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Metodo que borra los indices
    Public Sub ClearIndexs()
        Try
            Me.SuspendLayout()
            Me.Controls.Clear()
            Me.ResumeLayout()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub IndexController_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
    End Sub

    Private Sub IndexController_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

    End Sub
End Class
