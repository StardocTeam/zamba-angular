Imports Zamba.Core
Imports Zamba.AppBlock
'Imports ZOCRLib
'Imports zamba.WFShapes
Imports Zamba.Shapes
Imports Zamba.Shapes.NetronLight
Imports zamba.WorkFlow.Business

Public Class UCImgViewer
    Inherits Zamba.AppBlock.ZControl 'System.Windows.Forms.UserControl

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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MnuSendToBack As System.Windows.Forms.MenuItem
    Friend WithEvents MnuBringToFront As System.Windows.Forms.MenuItem
    Friend WithEvents PicBox2 As NetronLight.GraphControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MnuSendToBack = New System.Windows.Forms.MenuItem
        Me.MnuBringToFront = New System.Windows.Forms.MenuItem
        Me.PicBox2 = New NetronLight.GraphControl
        Me.SuspendLayout()
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MnuSendToBack, Me.MnuBringToFront})
        '
        'MnuSendToBack
        '
        Me.MnuSendToBack.Index = 0
        Me.MnuSendToBack.Text = "Enviar al Fondo"
        '
        'MnuBringToFront
        '
        Me.MnuBringToFront.Index = 1
        Me.MnuBringToFront.Text = "Traer al Frente"
        '
        'PicBox2
        '
        Me.PicBox2.AutoScroll = True
        Me.PicBox2.AutoSize = False
        Me.PicBox2.BackColor = System.Drawing.Color.Transparent
        Me.PicBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicBox2.Location = New System.Drawing.Point(0, 0)
        Me.PicBox2.Name = "PicBox2"
        Me.PicBox2.ShowGrid = False
        Me.PicBox2.Size = New System.Drawing.Size(928, 496)
        Me.PicBox2.TabIndex = 0
        Me.PicBox2.Text = Nothing
        '
        'UCImgViewer
        '
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.Controls.Add(Me.PicBox2)
        Me.Name = "UCImgViewer"
        Me.Size = New System.Drawing.Size(928, 496)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

#Region " NETRON "
    Dim Result As Result
    Public Sub SearchInDb(ByRef Result As Result)
        Me.Result = Result
        Try
            Dim DsObjects As DataSet = ZNetron_Business.GetAllObjects(CInt(Result.DocType.ID), CInt(Result.ID))
            Dim DsConnections As DataSet = ZNetron_Business.GetAllConnections(CInt(Result.DocType.ID), CInt(Result.ID))
            Dim i As Integer = 0
            While i <= DsObjects.Tables(0).Rows.Count - 1
                Dim Num As Integer = CType(DsObjects.Tables(0).Rows(i).ItemArray(1), System.Int16)
                Dim Id As Integer = 0
                Dim Shape_Height As System.Int32 = 0
                Dim Shape_Color As Integer = 0
                Dim Shape_Text As System.String = ""
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
        End Try
    End Sub

    Dim Hash As New Hashtable
    Private Sub LoadObject(ByVal Shape_Id As Integer, ByVal Shape_Tipo As Int32, ByVal Shape_Height As Int32, ByVal Shape_Color As System.Drawing.Color, ByVal Shape_Text As String, ByVal Shape_Width As Int32, ByVal Shape_X As Int32, ByVal Shape_Y As Int32, ByVal Shape_Opaque As Boolean)
        Select Case Shape_Tipo
            Case 1
                Dim ent As NetronLight.SimpleRectangle = DirectCast(Me.PicBox2.AddShape(NetronLight.ShapeTypes.Rectangular, New Point(Shape_X, Shape_Y)), NetronLight.SimpleRectangle)
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
                Dim Oval As NetronLight.OvalShape = DirectCast(Me.PicBox2.AddShape(NetronLight.ShapeTypes.Oval, New Point(Shape_X, Shape_Y)), NetronLight.OvalShape)
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
                Dim label As NetronLight.TextLabel = DirectCast(Me.PicBox2.AddShape(NetronLight.ShapeTypes.TextLabel, New Point(Shape_X, Shape_Y)), NetronLight.TextLabel)
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
        Me.PicBox2.AddConnection(obj.Connectors(Shape_StartNum), obj2.Connectors(Shape_EndNum))
    End Sub
    Private ArrayIds As System.Collections.ArrayList = New System.Collections.ArrayList

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
            If Not IsNothing(Result) AndAlso Result.Id > 0 Then
                ArrayIds = ZNetron_Business.GetAllIds("Obj", CInt(Result.DocType.ID), CInt(Result.ID))
                Dim Collection As NetronLight.ShapeCollection = CType(Me.PicBox2.Shapes, NetronLight.ShapeCollection)
                Dim k As Integer = 0
                While k <= Collection.Count - 1
                    Dim obj As NetronLight.ShapeBase = CType(Collection(k), NetronLight.ShapeBase)
                    If ShapesBusiness.ExistsId(obj.Id) = True Then
                        ArrayIds.Remove(obj.Id)
                        ZNetron_Business.UpdateObject(obj.Height, obj.ShapeColor.ToArgb().ToString, obj.Text, obj.Width, obj.X, obj.Y, obj.Opaque, obj.Id, CInt(Result.DocType.ID), CInt(Result.ID))
                    Else
                        ZNetron_Business.SaveObject(obj.GetType.FullName, obj.Height, obj.ShapeColor.ToArgb().ToString, obj.Text, obj.Width, obj.X, obj.Y, obj.Id, obj.Opaque, CInt(Result.DocType.ID), CInt(Result.ID))
                    End If
                    System.Math.Min(System.Threading.Interlocked.Increment(k), k - 1)
                End While
                Dim n As Integer = 0
                While n <= ArrayIds.Count - 1
                    ZNetron_Business.DeleteObject(CInt(ArrayIds(n)), CInt(Result.DocType.ID), CInt(Result.ID))
                    System.Math.Min(System.Threading.Interlocked.Increment(n), n - 1)
                End While
                SaveAllConnections()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SaveAllConnections()
        'todo 2005: No se actualizo bien, descomentar y ver error de point
        ' '' ''NetronLight.ZNetron_Factory.DeleteAllConnectionsFromDB(Result.DocType.Id, Result.Id)
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
        ' '' ''            NetronLight.ZNetron_Factory.SaveConection(FromId, FromConnector, ToId, ToConnector, id, Result.DocType.Id, Result.Id)
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

    Private Sub MnuSendToBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuSendToBack.Click
        'For Each Control As Control In Me.Controls
        'If Control.GetType.ToString.IndexOf("Note") <> -1 OrElse Control.GetType.ToString.IndexOf("Sign") <> -1 Then
        'Control.Visible = False
        'Control.SendToBack()
        'End If
        PicBox2.BringToFront()
        'Next
    End Sub
    Private Sub MnuBringToFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuBringToFront.Click
        'For Each Control As Control In Me.Controls
        'If Control.GetType.ToString.IndexOf("Note") <> -1 OrElse Control.GetType.ToString.IndexOf("Sign") <> -1 Then
        'Control.Visible = True
        'Control.BringToFront()
        'End If
        'Next
        PicBox2.SendToBack()
    End Sub

#Region "Variables"
    Public cropX As Integer
    Public cropY As Integer
    Public cropWidth As Integer
    Public cropHeight As Integer
    Public cropPen As Pen
    Public cropPenSize As Integer = CInt(0.5)
    Public cropDashStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Solid
    Public cropPenColor As Color = Color.DodgerBlue
#End Region

    'Private Sub PicBox2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicBox2.MouseDown
    '    Try
    '        'If e.Button = Windows.Forms.MouseButtons.Left Then
    '        '    'If Me.Estado = Estados.OCR Then
    '        '    If Me.PicBox2.State = Me.PicBox2.State.OCR Then
    '        '        Dim i As Int32
    '        '        For i = 0 To Me.PicBox2.Shapes.Count - 1
    '        '            If e.X >= PicBox2.Shapes(i).X AndAlso e.X <= PicBox2.Shapes(i).X + PicBox2.Shapes(i).Width Then
    '        '                If e.Y >= PicBox2.Shapes(i).Y AndAlso e.Y <= PicBox2.Shapes(i).Y + PicBox2.Shapes(i).Height Then
    '        '                    Exit Sub
    '        '                End If
    '        '            End If
    '        '        Next

    '        '        cropX = e.X
    '        '        cropY = e.Y

    '        '        cropPen = New Pen(cropPenColor, cropPenSize)
    '        '        cropPen.DashStyle = cropDashStyle

    '        '    End If
    '        'End If
    '    Catch exc As Exception
    '       zamba.core.zclass.raiseerror(exc)
    '    End Try
    'End Sub

    'Private Sub PicBox2_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicBox2.MouseMove
    '    Try
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    '        'If e.Button = Windows.Forms.MouseButtons.Left Then

    '        'If Me.PicBox2.State = Me.PicBox2.State.OCR Then
    '        '    'clear the previous drawn crop lines
    '        '    Dim PrevPoint As Point = PicBox2.AutoScrollPosition
    '        '    Me.PicBox2.Refresh()
    '        '    PicBox2.AutoScrollPosition = PrevPoint
    '        '    'need to take into count where the mouse started at
    '        '    'and calculate the new position

    '        '    cropWidth = e.X - cropX
    '        '    cropHeight = e.Y - cropY

    '        '    If cropWidth <= 0 AndAlso cropHeight >= 0 Then
    '        '        PicBox2.CreateGraphics.DrawRectangle(cropPen, cropX + cropWidth, cropY, -cropWidth, cropHeight)
    '        '    ElseIf cropWidth <= 0 AndAlso cropHeight <= 0 Then
    '        '        PicBox2.CreateGraphics.DrawRectangle(cropPen, cropX + cropWidth, cropY + cropHeight, -cropWidth, -cropHeight)
    '        '    ElseIf cropWidth >= 0 AndAlso cropHeight <= 0 Then
    '        '        PicBox2.CreateGraphics.DrawRectangle(cropPen, cropX, cropY + cropHeight, cropWidth, -cropHeight)
    '        '    Else
    '        '        PicBox2.CreateGraphics.DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight)
    '        '    End If

    '        'End If
    '        'End If
    '        'release as much memory as possible
    '        'GC.Collect()
    '    Catch exc As Exception
    '        'this is a error when the cropping parameters are less than 1
    '        If Err.Number = 5 Then Exit Sub
    '    End Try
    'End Sub

    'Private Sub PicBox2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicBox2.MouseUp
    '    Try
    '        'change the mouse pointer back to the default cursor
    '        Cursor = Cursors.Default
    '    Catch exc As Exception
    '       zamba.core.zclass.raiseerror(exc)
    '    End Try
    'End Sub

    'Private Sub PicBox_MouseMove1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicBox.MouseMove
    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    'End Sub

    '   Private Sub Btn1A1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn1A1.Click
    '       RaiseEvent ToolBarClick(sender.Tag)
    '       Me.TextBox1.Focus()
    '   End Sub

    'Private Sub BtnZoomMenos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomMenos.Click
    '    Me.PrevH = Me.PicBox2.Height
    '    Me.PrevW = Me.PicBox2.Width
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnZoomMas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomMas.Click
    '    Me.PrevH = Me.PicBox2.Height
    '    Me.PrevW = Me.PicBox2.Width
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnRotarIzq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRotarIzq.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnRotarDer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRotarDer.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnTodaLaPagina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
    '    Me.PrevH = Me.PicBox2.Height
    '    Me.PrevW = Me.PicBox2.Width
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnAnchoPagina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnchoPagina.Click
    '    Me.PrevH = Me.PicBox2.Height
    '    Me.PrevW = Me.PicBox2.Width
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnAltoPagina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAltoPagina.Click
    '    Me.PrevH = Me.PicBox2.Height
    '    Me.PrevW = Me.PicBox2.Width
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnHistorial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnReemplazar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReemplazar.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnNota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNota.Click
    '    Me.Estado = Estados.Nota
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnFirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirma.Click
    '    Me.Estado = Estados.Firma
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    Public Sub MenuAddNetron_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ent As NetronLight.SimpleRectangle = DirectCast(Me.PicBox2.AddShape(NetronLight.ShapeTypes.Rectangular, New Point(200, 200)), NetronLight.SimpleRectangle)
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


    'Private Sub BtnNetron_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNetron.Click
    '    Me.Estado = Estados.Netron
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnPrimeraPag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrimeraPag.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnPagAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPagAnterior.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnPagSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPagSiguiente.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    'Private Sub BtnUltimaPag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUltimaPag.Click
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    Private Sub UCImgViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IsNothing(Me.PicBox2.ContextMenu) = False Then

                MenuNuevoNetron = New MenuItem
                MenuNuevoNetron.Text = "Nuevo Rectángulo"
                RemoveHandler MenuNuevoNetron.Click, AddressOf Me.MenuAddNetron_Click
                AddHandler MenuNuevoNetron.Click, AddressOf Me.MenuAddNetron_Click
                MenuNetron = Me.PicBox2.ContextMenu

                Dim Flag As Boolean
                For Each D As MenuItem In MenuNetron.MenuItems
                    If D.Text.ToUpper = MenuNuevoNetron.Text.ToUpper Then Flag = True
                Next
                If Flag = False Then MenuNetron.MenuItems.Add(MenuNuevoNetron)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try


        'AjustarTam()
        'Me.Estado = Estados.Ninguno
        Me.PicBox2.State = NetronLight.GraphControl.States.Ninguno
    End Sub

    'Enum Estados
    '    Ninguno = 0
    '    OCR = 1
    '    Netron = 2
    '    Nota = 3
    '    Firma = 4
    'End Enum



    Dim ColorBlue As Color = Drawing.Color.FromArgb(191, 211, 249)
    Dim ColorRed As Color = Color.LightSalmon

    Public MenuNuevoNetron As MenuItem
    Public MenuNetron As Menu


    'Private Sub AjustarTam()
    '    If Me.Button3.Visible = True Then
    '        If Panel2.Width <= Me.Button3.Left + Me.Button3.Width Then
    '            Panel2.Height = 54
    '        Else
    '            Panel2.Height = 37
    '        End If
    '    Else
    '        If Panel2.Width <= Me.BtnNetron.Left + Me.BtnNetron.Width Then
    '            Panel2.Height = 54
    '        Else
    '            Panel2.Height = 37
    '        End If
    '    End If
    'End Sub

    ' Public Event GotoPage(ByVal Page As Int32)

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    '    If IsNumeric(Me.txtNumPag.Text) = True Then
    '        RaiseEvent GotoPage(Val(Me.txtNumPag.Text.Trim))
    '    Else
    '        Me.txtNumPag.Text = ""
    '    End If
    'End Sub
    Private Sub OCRArea()
        Try
            If Me.PicBox2.recOCR.Height > 0 And Me.PicBox2.recOCR.Width > 0 Then
                If Me.PicBox2.recOCR.Height * Me.PicBox2.recOCR.Height < 3000 Then
                    MessageBox.Show("DEBE SELECCIONAR UNA PORCION DE LA IMAGEN MAS GRANDE PARA QUE LA FUNCION DE OCR PUEDA RECONOCER TEXTO EN LA IMAGEN", "")
                Else
                    'Dim bit As Bitmap = New Bitmap(Me.PicBox2.Image, PicBox2.Width, PicBox2.Height)
                    'Dim cropBitmap As Bitmap = New Bitmap(System.Math.Abs(cropWidth), System.Math.Abs(cropHeight))
                    'Dim g As Graphics = Graphics.FromImage(cropBitmap)
                    'g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel)
                    Dim bmp As New Bitmap(Me.PicBox2.recOCR.Width, Me.PicBox2.recOCR.Height)

                    Dim gr As Graphics = Graphics.FromImage(bmp)
                    Using (gr)
                        If Me.PicBox2.isTif = False Then
                            gr.DrawImage(Me.PicBox2.Image, New Rectangle(0, 0, Me.PicBox2.recOCR.Width, Me.PicBox2.recOCR.Height), Me.PicBox2.recOCR, GraphicsUnit.Pixel)
                        Else
                            Dim recDes As New Rectangle(Me.PicBox2.recOCR.Location.X * 2 + 20, Me.PicBox2.recOCR.Location.Y * 2 + 20, Me.PicBox2.recOCR.Width * 2 + 30, Me.PicBox2.recOCR.Height * 2 + 15)
                            Dim recIni As New Rectangle(0, 0, CInt(Me.PicBox2.recOCR.Width / 2), CInt(Me.PicBox2.recOCR.Height / 2))
                            gr.ScaleTransform(2, 2)
                            gr.DrawImage(Me.PicBox2.Image, recIni, recDes, GraphicsUnit.Pixel)
                        End If
                    End Using

                    Me.Cursor = Cursors.WaitCursor
                    GenerateOCR(bmp)

                    'cropX = 0
                    'cropY = 0
                    'cropWidth = 0
                    'cropHeight = 0

                    Me.PicBox2.Refresh()
                    Me.Cursor = Cursors.Default
                End If
            End If
            'If cropWidth = 0 AndAlso cropHeight = 0 Then
            'ElseIf cropWidth * cropHeight < 3300 Then
            '    MessageBox.Show("DEBE SELECCIONAR UNA PORCION DE LA IMAGEN MAS GRANDE PARA QUE LA FUNCION DE OCR PUEDA RECONOCER TEXTO EN LA IMAGEN", "")
            'Else

            '    'Dim NewPic As PictureBox = PicBox
            '    'Dim bit As Bitmap = New Bitmap(NewPic.Image, NewPic.Width, NewPic.Height)
            '    Dim bit As Bitmap = New Bitmap(Me.PicBox2.Image, PicBox2.Width, PicBox2.Height)
            '    Dim rect As Rectangle

            '    If cropWidth <= 0 AndAlso cropHeight >= 0 Then
            '        rect = New Rectangle(cropX + cropWidth, cropY, -cropWidth, cropHeight)
            '    ElseIf cropWidth <= 0 AndAlso cropHeight <= 0 Then
            '        rect = New Rectangle(cropX + cropWidth, cropY + cropHeight, -cropWidth, -cropHeight)
            '    ElseIf cropWidth >= 0 AndAlso cropHeight <= 0 Then
            '        rect = New Rectangle(cropX, cropY + cropHeight, cropWidth, -cropHeight)
            '    Else
            '        rect = New Rectangle(cropX, cropY, cropWidth, cropHeight)
            '    End If

            '    Dim cropBitmap As Bitmap = New Bitmap(System.Math.Abs(cropWidth), System.Math.Abs(cropHeight))
            '    Dim g As Graphics = Graphics.FromImage(cropBitmap)
            '    g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel)

            '    Me.Cursor = Cursors.WaitCursor
            '    GenerateOCR(cropBitmap)

            '    cropX = 0
            '    cropY = 0
            '    cropWidth = 0
            '    cropHeight = 0

            '    Me.PicBox2.Refresh()
            '    Me.Cursor = Cursors.Default
            'End If

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Dim FrmTextbox As FrmTextBox

    Private Sub GenerateOCR(ByVal Image As Image)
        Try
            'todo patricio: ver que comente dos lineas
            If IO.File.Exists(Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName & "\T.bmp") Then IO.File.Delete(Membership.MembershipHelper.StartUpPath & "\IndexerTemp\T.bmp")
            If IO.Directory.Exists(Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName) = False Then IO.Directory.CreateDirectory(Membership.MembershipHelper.StartUpPath & "\IndexerTemp")
            Image.Save(Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName & "\T.bmp")
            'todo: buscar zamba.ocr proyecto. y hacer referencia
            ' Dim OCR As New ZOCRLib.ZOCRLib
            ' Dim Text As String = OCR.OCR(Membership.MembershipHelper.StartUpPath & "\IndexerTemp\T.bmp")
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

    'Private Sub BtnOCR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOCR.Click
    '    Me.Estado = Estados.OCR
    '    RaiseEvent ToolBarClick(sender.Tag)
    '    Me.TextBox1.Focus()
    'End Sub

    Protected Overrides Sub OnLeave(ByVal e As System.EventArgs)
        SaveAllObjects()
    End Sub

    'Public Sub ZoomNetron()
    '    Try

    '        Dim Collection As NetronLight.ShapeCollection = CType(Me.PicBox2.Shapes, NetronLight.ShapeCollection)

    '        For Each Shape As NetronLight.ShapeBase In Collection
    '            Try

    '                Shape.Move(New Point(ShapeX - Shape.X, ShapeY - Shape.Y))

    '            Catch ex As Exception
    '               zamba.core.zclass.raiseerror(ex)
    '            End Try
    '        Next
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

    Public Sub RotateNetron()
        Try
            Dim Collection As NetronLight.ShapeCollection = CType(Me.PicBox2.Shapes, NetronLight.ShapeCollection)

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
    'Dim _Estado As Int32
    'Public Property Estado() As Integer
    '    Get
    '        Return _Estado
    '    End Get
    '    Set(ByVal Value As Integer)
    '        _Estado = Value
    '    End Set
    'End Property

    Shadows vscroll As Int32 = 0
    Shadows hscroll As Int32 = 0

    Private Sub PicBox2_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicBox2.MouseWheel
        For Each control As Control In Me.Controls
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
        For Each control As Control In Me.Controls
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
