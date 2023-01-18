Imports Zamba.Core
Imports Zamba.Shapes
Imports zamba.WorkFlow.Business

Public Class UCImgViewer
    Inherits Zamba.AppBlock.ZControl 'System.Windows.Forms.UserControl
    Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not IsDisposed Then

            Try
                If disposing Then
                    If Not (components Is Nothing) Then
                        components.Dispose()
                    End If
                End If

                RemoveHandler MyBase.Load, AddressOf UCImgViewer_Load
                'RemoveHandler Me.OnLeave, AddressOf OnLeave

                If MnuSendToBack IsNot Nothing Then
                    RemoveHandler MnuSendToBack.Click, AddressOf MnuSendToBack_Click
                    MnuSendToBack.Dispose()
                End If
                If MnuBringToFront IsNot Nothing Then
                    RemoveHandler MnuBringToFront.Click, AddressOf MnuBringToFront_Click
                    MnuBringToFront.Dispose()
                End If
                If ContextMenu1 IsNot Nothing Then
                    ContextMenu1.Dispose()
                    ContextMenu1 = Nothing
                End If
                If MenuNuevoNetron IsNot Nothing Then
                    RemoveHandler MenuNuevoNetron.Click, AddressOf MenuAddNetron_Click
                    MenuNuevoNetron.Dispose()
                    MenuNuevoNetron = Nothing
                End If
                If MenuNetron IsNot Nothing Then
                    MenuNetron.Dispose()
                    MenuNetron = Nothing
                End If
                If ArrayIds IsNot Nothing Then
                    ArrayIds.Clear()
                    ArrayIds = Nothing
                End If
                If Hash IsNot Nothing Then
                    Hash.Clear()
                    Hash = Nothing
                End If
                If PicBox2 IsNot Nothing Then
                    RemoveHandler PicBox2.MouseWheel, AddressOf PicBox2_MouseWheel
                    RemoveHandler PicBox2.Scroll, AddressOf PicBox2_Scroll
                    RemoveHandler PicBox2.KeyDown, AddressOf PicBox2_KeyDown
                    PicBox2.Dispose()
                    PicBox2 = Nothing
                End If

                'ESTE RECURSO NO DEBE SER LIBERADO EN NINGUNA CIRCUNSTANCIA YA QUE IMPACTA EN MULTIPLES PARTES DE ZAMBA
                'Result.Dispose()

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                MyBase.Dispose(disposing)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            IsDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ContextMenu1 As ContextMenu
    Friend WithEvents MnuSendToBack As System.Windows.Forms.MenuItem
    Friend WithEvents MnuBringToFront As System.Windows.Forms.MenuItem
    Friend WithEvents PicBox2 As NetronLight.GraphControl
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ContextMenu1 = New ContextMenu
        MnuSendToBack = New System.Windows.Forms.MenuItem
        MnuBringToFront = New System.Windows.Forms.MenuItem
        PicBox2 = New NetronLight.GraphControl
        SuspendLayout()
        '
        'ContextMenu1
        '
        ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {MnuSendToBack, MnuBringToFront})
        '
        'MnuSendToBack
        '
        MnuSendToBack.Index = 0
        MnuSendToBack.Text = "Enviar al Fondo"
        '
        'MnuBringToFront
        '
        MnuBringToFront.Index = 1
        MnuBringToFront.Text = "Traer al Frente"
        '
        'PicBox2
        '
        PicBox2.AutoScroll = True
        PicBox2.AutoSize = False
        PicBox2.BackColor = System.Drawing.Color.Transparent
        PicBox2.Dock = System.Windows.Forms.DockStyle.Fill
        PicBox2.Location = New System.Drawing.Point(0, 0)
        PicBox2.Name = "PicBox2"
        PicBox2.ShowGrid = False
        PicBox2.Size = New System.Drawing.Size(928, 496)
        PicBox2.TabIndex = 0
        PicBox2.Text = Nothing
        '
        'UCImgViewer
        '
        BackColor = System.Drawing.Color.Gainsboro
        Controls.Add(PicBox2)
        Name = "UCImgViewer"
        Size = New System.Drawing.Size(928, 496)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

#Region "Variables"
    Public cropX As Integer
    Public cropY As Integer
    Public cropWidth As Integer
    Public cropHeight As Integer
    'Public cropPen As Pen
    Public cropPenSize As Integer = CInt(0.5)
    Public cropDashStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Solid
    Public cropPenColor As Color = Color.FromArgb(0, 157, 224)
    Dim ColorBlue As Color = Drawing.Color.FromArgb(191, 211, 249)
    Dim ColorRed As Color = Color.LightSalmon
    'Dim FrmTextbox As FrmTextBox
    Public MenuNuevoNetron As MenuItem = Nothing
    Public MenuNetron As Menu = Nothing
    Dim Result As Result = Nothing
    Dim Hash As New Hashtable
    Private ArrayIds As New ArrayList
    Private isDisposed As Boolean
    Shadows vscroll As Int32 = 0
    Shadows hscroll As Int32 = 0
#End Region

#Region " NETRON "

    Public Sub SearchInDb(ByRef Result As Result)
        Me.Result = Result
        Dim DsObjects As DataSet = Nothing
        Dim DsConnections As DataSet = Nothing
        Try
            DsObjects = ZNetron_Business.GetAllObjects(CInt(Result.DocTypeId), CInt(Result.ID))
            DsConnections = ZNetron_Business.GetAllConnections(CInt(Result.DocTypeId), CInt(Result.ID))
            Dim i As Integer = 0
            While i <= DsObjects.Tables(0).Rows.Count - 1
                Dim Num As Integer = CType(DsObjects.Tables(0).Rows(i).ItemArray(1), System.Int16)
                Dim Id As Integer = 0
                Dim Shape_Height As System.Int32 = 0
                Dim Shape_Color As Integer = 0
                Dim Shape_Text As System.String = String.Empty
                Dim ShapeColor As System.Drawing.Color = System.Drawing.Color.White
                Dim Shape_Width As System.Int32 = 0
                Dim Shape_X As System.Int32 = 0
                Dim Shape_Y As System.Int32 = 0
                Dim Shape_Opaque As Integer = 0
                Dim ShapeOpaque As Boolean = False
                Select Case Num
                    Case 1
                        Id = CInt(DsObjects.Tables(0).Rows(i).ItemArray(0))
                        Shape_Height = CType(DsObjects.Tables(0).Rows(i).ItemArray(2), System.Int32)
                        Shape_Color = CInt(DsObjects.Tables(0).Rows(i).ItemArray(3))
                        Shape_Text = CType(DsObjects.Tables(0).Rows(i).ItemArray(4), System.String)
                        ShapeColor = System.Drawing.Color.FromArgb(Shape_Color)
                        Shape_Width = CType(DsObjects.Tables(0).Rows(i).ItemArray(5), System.Int32)
                        Shape_X = CType(DsObjects.Tables(0).Rows(i).ItemArray(6), System.Int32)
                        Shape_Y = CType(DsObjects.Tables(0).Rows(i).ItemArray(7), System.Int32)
                        Shape_Opaque = CInt(DsObjects.Tables(0).Rows(i).ItemArray(8))
                        If Shape_Opaque = 0 Then
                            ShapeOpaque = False
                        Else
                            ShapeOpaque = True
                        End If
                        LoadObject(Id, 1, Shape_Height, ShapeColor, Shape_Text, Shape_Width, Shape_X, Shape_Y, ShapeOpaque)
                        ' break 
                    Case 2
                        Id = CInt(DsObjects.Tables(0).Rows(i).ItemArray(0))
                        Shape_Height = CType(DsObjects.Tables(0).Rows(i).ItemArray(2), System.Int32)
                        Shape_Color = CInt(DsObjects.Tables(0).Rows(i).ItemArray(3))
                        Shape_Text = CType(DsObjects.Tables(0).Rows(i).ItemArray(4), System.String)
                        ShapeColor = System.Drawing.Color.FromArgb(Shape_Color)
                        Shape_Width = CType(DsObjects.Tables(0).Rows(i).ItemArray(5), System.Int32)
                        Shape_X = CType(DsObjects.Tables(0).Rows(i).ItemArray(6), System.Int32)
                        Shape_Y = CType(DsObjects.Tables(0).Rows(i).ItemArray(7), System.Int32)
                        Shape_Opaque = CInt(DsObjects.Tables(0).Rows(i).ItemArray(8))
                        If Shape_Opaque = 0 Then
                            ShapeOpaque = False
                        Else
                            ShapeOpaque = True
                        End If
                        LoadObject(Id, 2, Shape_Height, ShapeColor, Shape_Text, Shape_Width, Shape_X, Shape_Y, ShapeOpaque)
                        ' break 
                    Case 3
                        Id = CInt(DsObjects.Tables(0).Rows(i).ItemArray(0))
                        Shape_Height = CType(DsObjects.Tables(0).Rows(i).ItemArray(2), System.Int32)
                        Shape_Color = CInt(DsObjects.Tables(0).Rows(i).ItemArray(3))
                        Shape_Text = CType(DsObjects.Tables(0).Rows(i).ItemArray(4), System.String)
                        ShapeColor = System.Drawing.Color.FromArgb(Shape_Color)
                        Shape_Width = CType(DsObjects.Tables(0).Rows(i).ItemArray(5), System.Int32)
                        Shape_X = CType(DsObjects.Tables(0).Rows(i).ItemArray(6), System.Int32)
                        Shape_Y = CType(DsObjects.Tables(0).Rows(i).ItemArray(7), System.Int32)
                        Shape_Opaque = CInt(DsObjects.Tables(0).Rows(i).ItemArray(8))
                        If Shape_Opaque = 0 Then
                            ShapeOpaque = False
                        Else
                            ShapeOpaque = True
                        End If
                        LoadObject(Id, 3, Shape_Height, ShapeColor, Shape_Text, Shape_Width, Shape_X, Shape_Y, ShapeOpaque)
                        ' break 
                End Select
                System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
            End While
            Dim j As Integer = 0
            While j <= DsConnections.Tables(0).Rows.Count - 1
                '   Dim Num As Integer = CType(DsConnections.Tables(0).Rows(j).ItemArray(1), System.Int16)
                Dim Id As Integer = 0
                Dim Shape_StartId As System.Int16 = 0
                Dim Shape_EndId As System.Int16 = 0
                Dim Shape_StartNum As System.Byte = 0
                Dim Shape_EndNum As System.Byte = 0
                Id = CInt(DsConnections.Tables(0).Rows(j).ItemArray(0))
                Shape_StartId = CType(DsConnections.Tables(0).Rows(j).ItemArray(1), System.Int16)
                Shape_EndId = CType(DsConnections.Tables(0).Rows(j).ItemArray(2), System.Int16)
                Shape_StartNum = CType(DsConnections.Tables(0).Rows(j).ItemArray(3), System.Byte)
                Shape_EndNum = CType(DsConnections.Tables(0).Rows(j).ItemArray(4), System.Byte)
                LoadConnection(Shape_StartId, Shape_EndId, Shape_StartNum, Shape_EndNum)
                System.Math.Min(System.Threading.Interlocked.Increment(j), j - 1)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If DsConnections IsNot Nothing Then
                DsConnections.Dispose()
                DsConnections = Nothing
            End If
            If DsObjects IsNot Nothing Then
                DsObjects.Dispose()
                DsObjects = Nothing
            End If
        End Try
    End Sub

    Private Sub LoadObject(ByVal Shape_Id As Integer, ByVal Shape_Tipo As Int32, ByVal Shape_Height As Int32, ByVal Shape_Color As System.Drawing.Color, ByVal Shape_Text As String, ByVal Shape_Width As Int32, ByVal Shape_X As Int32, ByVal Shape_Y As Int32, ByVal Shape_Opaque As Boolean)
        Select Case Shape_Tipo
            Case 1
                Dim ent As NetronLight.SimpleRectangle = DirectCast(PicBox2.AddShape(NetronLight.ShapeTypes.Rectangular, New Point(Shape_X, Shape_Y)), NetronLight.SimpleRectangle)
                If Shape_Id > 0 Then ent.Id = Shape_Id
                ent.Text = Shape_Text
                ent.Height = Shape_Height
                ent.Width = Shape_Width
                ent.ShapeColor = Shape_Color
                ent.Opaque = Shape_Opaque
                If ent.Opaque = False Then
                    Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(128, ent.ShapeColor)
                    ent.shapeBrush = New SolidBrush(newcolor)
                Else
                    Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, ent.ShapeColor)
                    ent.shapeBrush = New SolidBrush(newcolor)
                End If
                Hash.Add(ent.Id, ent)
                ' break 
            Case 2
                Dim Oval As NetronLight.OvalShape = DirectCast(PicBox2.AddShape(NetronLight.ShapeTypes.Oval, New Point(Shape_X, Shape_Y)), NetronLight.OvalShape)
                Oval.Id = Shape_Id
                Oval.Text = Shape_Text
                Oval.Height = Shape_Height
                Oval.Width = Shape_Width
                Oval.ShapeColor = Shape_Color
                Oval.Opaque = Shape_Opaque
                If Oval.Opaque = False Then
                    Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(128, Oval.ShapeColor)
                    Oval.shapeBrush = New SolidBrush(newcolor)
                Else
                    Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, Oval.ShapeColor)
                    Oval.shapeBrush = New SolidBrush(newcolor)
                End If
                Hash.Add(Oval.Id, Oval)
                ' break 
            Case 3
                Dim label As NetronLight.TextLabel = DirectCast(PicBox2.AddShape(NetronLight.ShapeTypes.TextLabel, New Point(Shape_X, Shape_Y)), NetronLight.TextLabel)
                label.Id = Shape_Id
                label.Text = Shape_Text
                label.Height = Shape_Height
                label.Width = Shape_Width
                label.ShapeColor = Shape_Color
                label.Opaque = Shape_Opaque
                If label.Opaque = False Then
                    Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(128, label.ShapeColor)
                    label.shapeBrush = New SolidBrush(newcolor)
                Else
                    Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, label.ShapeColor)
                    label.shapeBrush = New SolidBrush(newcolor)
                End If
                Hash.Add(label.Id, label)
                ' break 
        End Select
    End Sub

    Private Sub LoadConnection(ByVal Shape_StartId As System.Int16, ByVal Shape_EndId As System.Int16, ByVal Shape_StartNum As System.Byte, ByVal Shape_EndNum As System.Byte)
        Dim obj As NetronLight.ShapeBase = CType(Hash(CInt(Shape_StartId)), NetronLight.ShapeBase)
        Dim obj2 As NetronLight.ShapeBase = CType(Hash(CInt(Shape_EndId)), NetronLight.ShapeBase)
        PicBox2.AddConnection(obj.Connectors(Shape_StartNum), obj2.Connectors(Shape_EndNum))
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para almacenar todos los objetos Netron contenidos en el Viewer
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	13/07/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SaveAllObjects()
        'If YaGuarde = True Then
        '    YaGuarde = False
        '    Exit Sub
        'End If
        'YaGuarde = True
        Try
            If Not IsNothing(Result) AndAlso Result.ID > 0 Then
                ArrayIds = ZNetron_Business.GetAllIds("Obj", CInt(Result.DocTypeId), CInt(Result.ID))
                Dim Collection As NetronLight.ShapeCollection = CType(PicBox2.Shapes, NetronLight.ShapeCollection)
                Dim k As Integer = 0
                While k <= Collection.Count - 1
                    Dim obj As NetronLight.ShapeBase = CType(Collection(k), NetronLight.ShapeBase)
                    If ShapesBusiness.ExistsId(obj.Id) = True Then
                        ArrayIds.Remove(obj.Id)
                        ZNetron_Business.UpdateObject(obj.Height, obj.ShapeColor.ToArgb().ToString, obj.Text, obj.Width, obj.X, obj.Y, obj.Opaque, obj.Id, CInt(Result.DocTypeId), CInt(Result.ID))
                    Else
                        ZNetron_Business.SaveObject(obj.GetType.FullName, obj.Height, obj.ShapeColor.ToArgb().ToString, obj.Text, obj.Width, obj.X, obj.Y, obj.Id, obj.Opaque, CInt(Result.DocTypeId), CInt(Result.ID))
                    End If
                    System.Math.Min(System.Threading.Interlocked.Increment(k), k - 1)
                End While
                Dim n As Integer = 0
                While n <= ArrayIds.Count - 1
                    ZNetron_Business.DeleteObject(CInt(ArrayIds(n)), CInt(Result.DocTypeId), CInt(Result.ID))
                    System.Math.Min(System.Threading.Interlocked.Increment(n), n - 1)
                End While
                SaveAllConnections()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SaveAllConnections()
        'todo 2005: No se actualizo bien, descomentar y ver error de point
        ' '' ''NetronLight.ZNetron_Factory.DeleteAllConnectionsFromDB(Result.DocTypeId, Result.Id)
        ' '' ''Dim Collection As NetronLight.ShapeCollection = CType(Me.PicBox2.Shapes, NetronLight.ShapeCollection)
        ' '' ''Dim FromId As Integer = 0
        ' '' ''Dim ToId As Integer = 0
        ' '' ''Dim FromConnector As Integer = 0
        ' '' ''Dim ToConnector As Integer = 0
        ' '' ''Dim id As Integer = 0
        ' '' ''Dim k As Integer = 0
        ' '' ''While k <= Me.PicBox2.connections.Count - 1
        ' '' ''    FromId = 0
        ' '' ''    ToId = 0
        ' '' ''    FromConnector = 0
        ' '' ''    ToConnector = 0
        ' '' ''    id = CInt(Me.PicBox2.connections(k).Id)
        ' '' ''    Dim X As Integer = CInt(Me.PicBox2.connections(k).From.Point.X)
        ' '' ''    Dim Y As Integer = CInt(Me.PicBox2.connections(k).From.Point.Y)
        ' '' ''    Dim X2 As Integer = CInt(Me.PicBox2.connections(k).To.Point.X)
        ' '' ''    Dim Y2 As Integer = CInt(Me.PicBox2.connections(k).To.Point.Y)
        ' '' ''    Dim Point1 As Point = New Point(X, Y)
        ' '' ''    Dim Point2 As Point = New Point(X2, Y2)
        ' '' ''    Dim p As Integer = 0
        ' '' ''    While p <= Collection.Count - 1
        ' '' ''        Dim obj As NetronLight.ShapeBase = Collection(p)
        ' '' ''        If Not (obj.GetType.FullName = "NetronLight.TextLabel") Then
        ' '' ''            Dim t As Integer = 0
        ' '' ''            While t <= obj.Connectors(0).attachedConnectors.Count - 1
        ' '' ''                Dim Point As Point = New Point(CInt(obj.Connectors(0).attachedConnectors(t).Point.X), CInt(obj.Connectors(0).attachedConnectors(t).Point.Y))
        ' '' ''                If Point1.X & "-" & Point1.Y = Point.X & "-" & Point.Y Then
        ' '' ''                    FromId = obj.Id
        ' '' ''                    FromConnector = 0
        ' '' ''                Else
        ' '' ''                    If Point2.X & "-" & Point2.Y = Point.X & "-" & Point.Y Then
        ' '' ''                        ToId = obj.Id
        ' '' ''                        ToConnector = 0
        ' '' ''                    End If
        ' '' ''                End If
        ' '' ''                System.Math.Min(System.Threading.Interlocked.Increment(t), t - 1)
        ' '' ''            End While
        ' '' ''            Dim q As Integer = 0
        ' '' ''            While q <= obj.Connectors(1).attachedConnectors.Count - 1
        ' '' ''                Dim Point As Point = New Point(obj.Connectors(1).attachedConnectors(q).Point.X, obj.Connectors(1).attachedConnectors(q).Point.Y)
        ' '' ''                If Point1.X & "-" & Point1.Y = Point.X & "-" & Point.Y Then
        ' '' ''                    FromId = obj.Id
        ' '' ''                    FromConnector = 1
        ' '' ''                Else
        ' '' ''                    If Point2.X & "-" & Point2.Y = Point.X & "-" & Point.Y Then
        ' '' ''                        ToId = obj.Id
        ' '' ''                        ToConnector = 1
        ' '' ''                    End If
        ' '' ''                End If
        ' '' ''                System.Math.Min(System.Threading.Interlocked.Increment(q), q - 1)
        ' '' ''            End While
        ' '' ''            Dim r As Integer = 0
        ' '' ''            While r <= obj.Connectors(2).attachedConnectors.Count - 1
        ' '' ''                Dim Point As Point = New Point(CInt(obj.Connectors(2).attachedConnectors(r).Point.X), CInt(obj.Connectors(2).attachedConnectors(r).Point.Y))
        ' '' ''                If Point1.X & "-" & Point1.Y = Point.X & "-" & Point.Y Then
        ' '' ''                    FromId = obj.Id
        ' '' ''                    FromConnector = 2
        ' '' ''                Else
        ' '' ''                    If Point2.X & "-" & Point2.Y = Point.X & "-" & Point.Y Then
        ' '' ''                        ToId = obj.Id
        ' '' ''                        ToConnector = 2
        ' '' ''                    End If
        ' '' ''                End If
        ' '' ''                System.Math.Min(System.Threading.Interlocked.Increment(r), r - 1)
        ' '' ''            End While
        ' '' ''            Dim s As Integer = 0
        ' '' ''            While s <= obj.Connectors(3).attachedConnectors.Count - 1
        ' '' ''                Dim Point As Point = New Point(CInt(obj.Connectors(3).attachedConnectors(s).Point.X), CInt(obj.Connectors(3).attachedConnectors(s).Point.Y))
        ' '' ''                If Point1.X & "-" & Point1.Y = Point.X & "-" & Point.Y Then
        ' '' ''                    FromId = obj.Id
        ' '' ''                    FromConnector = 3
        ' '' ''                Else
        ' '' ''                    If Point1.X & "-" & Point1.Y = Point.X & "-" & Point.Y Then
        ' '' ''                        ToId = obj.Id
        ' '' ''                        ToConnector = 3
        ' '' ''                    End If
        ' '' ''                End If
        ' '' ''                System.Math.Min(System.Threading.Interlocked.Increment(s), s - 1)
        ' '' ''            End While
        ' '' ''        End If
        ' '' ''        System.Math.Min(System.Threading.Interlocked.Increment(p), p - 1)
        ' '' ''    End While
        ' '' ''    If Not (FromId = 0) Then
        ' '' ''        If Not (ToId = 0) Then
        ' '' ''            NetronLight.ZNetron_Factory.SaveConection(FromId, FromConnector, ToId, ToConnector, id, Result.DocTypeId, Result.Id)
        ' '' ''        End If
        ' '' ''    End If
        ' '' ''    System.Math.Min(System.Threading.Interlocked.Increment(k), k - 1)
        ' '' ''End While
    End Sub

    'Private Shared Function ExistsId(ByVal id As Integer) As Boolean
    '    Dim strSelect As String = "Select * From ZNetronShapes Where Shape_Id = " + id.ToString + " And Shape_Tipo <> 4"
    '    Try
    '        Dim DsId As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    '        If DsId.Tables(0).Rows.Count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch
    '        Return False
    '    End Try
    'End Function

#End Region

    Private Sub MnuSendToBack_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MnuSendToBack.Click
        'For Each Control As Control In Me.Controls
        'If Control.GetType.ToString.IndexOf("Note") <> -1 OrElse Control.GetType.ToString.IndexOf("Sign") <> -1 Then
        'Control.Visible = False
        'Control.SendToBack()
        'End If
        PicBox2.BringToFront()
        'Next
    End Sub

    Private Sub MnuBringToFront_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MnuBringToFront.Click
        'For Each Control As Control In Me.Controls
        'If Control.GetType.ToString.IndexOf("Note") <> -1 OrElse Control.GetType.ToString.IndexOf("Sign") <> -1 Then
        'Control.Visible = True
        'Control.BringToFront()
        'End If
        'Next
        PicBox2.SendToBack()
    End Sub

    Public Sub MenuAddNetron_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ent As NetronLight.SimpleRectangle = DirectCast(PicBox2.AddShape(NetronLight.ShapeTypes.Rectangular, New Point(200, 200)), NetronLight.SimpleRectangle)
        If ent.Opaque = False Then
            Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(128, ent.ShapeColor)
            ent.shapeBrush = New SolidBrush(newcolor)
        Else
            Dim newcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, ent.ShapeColor)
            ent.shapeBrush = New SolidBrush(newcolor)
        End If

        Hash.Add(ent.Id, ent)
        '     Me.TextBox1.Focus()
    End Sub

    Private Sub UCImgViewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            If IsNothing(PicBox2.ContextMenu) = False Then

                MenuNuevoNetron = New MenuItem
                MenuNuevoNetron.Text = "Nuevo Rectángulo"
                RemoveHandler MenuNuevoNetron.Click, AddressOf MenuAddNetron_Click
                AddHandler MenuNuevoNetron.Click, AddressOf MenuAddNetron_Click
                MenuNetron = PicBox2.ContextMenu

                Dim Flag As Boolean
                For Each D As MenuItem In MenuNetron.MenuItems
                    If D.Text.ToUpper = MenuNuevoNetron.Text.ToUpper Then Flag = True
                Next
                If Flag = False Then MenuNetron.MenuItems.Add(MenuNuevoNetron)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

        PicBox2.State = NetronLight.GraphControl.States.Ninguno
    End Sub

    Private Sub OCRArea()
        Try
            If PicBox2.recOCR.Height > 0 And PicBox2.recOCR.Width > 0 Then
                If PicBox2.recOCR.Height * PicBox2.recOCR.Height < 3000 Then
                    MessageBox.Show("DEBE SELECCIONAR UNA PORCION DE LA IMAGEN MAS GRANDE PARA QUE LA FUNCION DE OCR PUEDA RECONOCER TEXTO EN LA IMAGEN", "")
                Else
                    'Dim bit As Bitmap = New Bitmap(Me.PicBox2.Image, PicBox2.Width, PicBox2.Height)
                    'Dim cropBitmap As Bitmap = New Bitmap(System.Math.Abs(cropWidth), System.Math.Abs(cropHeight))
                    'Dim g As Graphics = Graphics.FromImage(cropBitmap)
                    'g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel)
                    Dim bmp As New Bitmap(PicBox2.recOCR.Width, PicBox2.recOCR.Height)

                    Dim gr As Graphics = Graphics.FromImage(bmp)
                    Using (gr)
                        If PicBox2.isTif = False Then
                            gr.DrawImage(PicBox2.Image, New Rectangle(0, 0, PicBox2.recOCR.Width, PicBox2.recOCR.Height), PicBox2.recOCR, GraphicsUnit.Pixel)
                        Else
                            Dim recDes As New Rectangle(PicBox2.recOCR.Location.X * 2 + 20, PicBox2.recOCR.Location.Y * 2 + 20, PicBox2.recOCR.Width * 2 + 30, PicBox2.recOCR.Height * 2 + 15)
                            Dim recIni As New Rectangle(0, 0, CInt(PicBox2.recOCR.Width / 2), CInt(PicBox2.recOCR.Height / 2))
                            gr.ScaleTransform(2, 2)
                            gr.DrawImage(PicBox2.Image, recIni, recDes, GraphicsUnit.Pixel)
                        End If
                    End Using

                    Cursor = Cursors.WaitCursor
                    GenerateOCR(bmp)

                    'cropX = 0
                    'cropY = 0
                    'cropWidth = 0
                    'cropHeight = 0

                    PicBox2.Refresh()
                    Cursor = Cursors.Default
                End If
            End If

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub GenerateOCR(ByVal Image As Image)
        Try
            'todo patricio: ver que comente dos lineas
            If IO.File.Exists(Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName & "\T.bmp") Then IO.File.Delete(Application.StartupPath & "\IndexerTemp\T.bmp")
            If IO.Directory.Exists(Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName) = False Then IO.Directory.CreateDirectory(Application.StartupPath & "\IndexerTemp")
            Image.Save(Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName & "\T.bmp")
            'todo: buscar zamba.ocr proyecto. y hacer referencia
            ' Dim OCR As New ZOCRLib.ZOCRLib
            ' Dim Text As String = OCR.OCR(Application.StartupPath & "\IndexerTemp\T.bmp")
            ' Clipboard.SetDataObject(Text, True)
            'FrmTextbox = New FrmTextBox(Text)
            'FrmTextbox.ShowDialog()
        Catch ex As Exception
            SendKeys.Send(Keys.Enter.ToString)
            MessageBox.Show("NO SE RECONOCIO TEXTO EN LA IMAGEN, INTENTE SELECCIONAR UNA PORCION DE LA IMAGEN MAYOR", "", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub PicBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PicBox2.KeyDown
        If e.KeyValue = 13 Then
            OCRArea()
        End If
    End Sub

    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        If Not isDisposed Then SaveAllObjects()
    End Sub

    Public Sub RotateNetron()
        Try
            Dim Collection As NetronLight.ShapeCollection = CType(PicBox2.Shapes, NetronLight.ShapeCollection)

            For Each Shape As NetronLight.ShapeBase In Collection
                Try
                    Dim PrevH As Int32 = Shape.Height
                    Dim PrevW As Int32 = Shape.Width
                    Dim PrevX As Int32 = Shape.X
                    Dim PrevY As Int32 = Shape.Y

                    Shape.Height = PrevW
                    Shape.Width = PrevH

                    Dim ShapeY As Int32 = PrevX
                    Dim ShapeX As Int32 = PrevY

                    Shape.Move(New Point(ShapeX - Shape.X, ShapeY - Shape.Y))

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
            Next

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub PicBox2_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicBox2.MouseWheel
        For Each control As Control In Controls
            If control.GetType().ToString = "Zamba.Notes.Note" Or control.GetType().ToString = "Zamba.Notes.Sign" Then
                If vscroll <> PicBox2.VerticalScroll.Value Then
                    control.Location = New Point(control.Location.X, control.Location.Y - PicBox2.VerticalScroll.Value + vscroll)
                ElseIf hscroll <> PicBox2.HorizontalScroll.Value Then
                    control.Location = New Point(control.Location.X - PicBox2.HorizontalScroll.Value + hscroll, control.Location.Y)
                End If
            End If
        Next
        hscroll = PicBox2.HorizontalScroll.Value
        vscroll = PicBox2.VerticalScroll.Value
    End Sub

    'Handles de move of the notes
    Private Sub PicBox2_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles PicBox2.Scroll
        For Each control As Control In Controls
            If control.GetType().ToString = "Zamba.Notes.Note" Or control.GetType().ToString = "Zamba.Notes.Sign" Then
                If vscroll <> PicBox2.VerticalScroll.Value Then
                    control.Location = New Point(control.Location.X, control.Location.Y - PicBox2.VerticalScroll.Value + vscroll)
                ElseIf hscroll <> PicBox2.HorizontalScroll.Value Then
                    control.Location = New Point(control.Location.X - PicBox2.HorizontalScroll.Value + hscroll, control.Location.Y)
                End If
            End If
        Next
        hscroll = PicBox2.HorizontalScroll.Value
        vscroll = PicBox2.VerticalScroll.Value
    End Sub

End Class
