Imports Zamba.appblock
Public Class Form2
    Inherits ZForm
    '    Implements Zamba.Patterns.IPlugIn

#Region "Interno"

    'Dim Files As New ArrayList
    Private m_FileResult As String
    Private m_FileExterno As String

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim OFD As New OpenFileDialog
        If OFD.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim file As String
            file = OFD.FileName
            Dim I As Image
            I = Image.FromFile(file)
            m_FileExterno = file
            GetThumbs2(I)
            'If Files.Count = 0 Then
            '    Files.Add(file)
            '    GetThumbs1(I)
            'ElseIf Files.Count = 1 Then
            '    Files.Add(file)
            '    GetThumbs2(I)
            'Else
            '    Files(1) = file
            '    GetThumbs2(I)
            'End If
        End If
    End Sub
    Private Sub AddResult(ByVal fullPath As Object)
        'Try
        '    Dim file As String
        '    file = CType(fullPath, String)
        '    Dim I As Image
        '    I = Image.FromFile(file)
        '    m_FileResult = file
        '    'If Files.Count = 0 Then
        '    '    Files.Add(file)
        '    'ElseIf Files.Count = 1 Then
        '    '    Files.Insert(0, file)
        '    'End If
        '    GetThumbs1(I)
        'Catch ex As Exception
        '    Throw New ArgumentException(ex.Message)
        'End Try
        System.Threading.ThreadPool.QueueUserWorkItem(New System.Threading.WaitCallback(AddressOf LoadResult), fullPath)
    End Sub

    Private Sub LoadResult(ByVal fullpath As Object)
        Try

            System.Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.Lowest

            Dim file As String
            file = CType(fullpath, String)
            Dim I As Image
            I = Image.FromFile(file)
            m_FileResult = file
            'If Files.Count = 0 Then
            '    Files.Add(file)
            'ElseIf Files.Count = 1 Then
            '    Files.Insert(0, file)
            'End If
            GetThumbs1(I)
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        Finally

        End Try
    End Sub
    Delegate Sub LockMeDelegate(ByVal lock As Boolean)
    Private Sub LockMe(ByVal lock As Boolean)
        Me.Enabled = Not lock
        If lock Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub GetThumbs1(ByVal I As Image)
        Try
            Dim iCount As Integer
            Dim actualFrame As Integer
            Dim oFDimension As System.Drawing.Imaging.FrameDimension
            Me.Invoke(New LockMeDelegate(AddressOf LockMe), New Object() {True})

            iCount = 0
            actualFrame = 0
            oFDimension = New System.Drawing.Imaging.FrameDimension(I.FrameDimensionsList(actualFrame))
            iCount = I.GetFrameCount(oFDimension) - 1
            Dim x As Integer

            If iCount > 0 Then
                For x = 0 To iCount
                    Application.DoEvents()
                    I.SelectActiveFrame(oFDimension, x)
                    Dim CB As New Drawing.Image.GetThumbnailImageAbort(AddressOf CB1)
                    Dim CBData As System.IntPtr
                    Dim T, Toi As Image
                    Toi = I.GetThumbnailImage(I.Width, I.Height, CB, CBData)
                    T = Toi.GetThumbnailImage(zoomw, zoomh(Toi), CB, CBData)
                    Dim Pic As New Pic
                    'Pic.OriginalImage = Toi0
                    Pic.OriginalImage = DirectCast(I.Clone, Drawing.Image)
                    Pic.Size = T.Size
                    Pic.Image = T
                    AddHandler Pic.MouseDown, AddressOf MouseDown
                    AddHandler Pic.DragOver, AddressOf DragOver2
                    AddHandler Pic.MouseEnter, AddressOf MouseEnter1
                    AddHandler Pic.MouseLeave, AddressOf MouseLeave1
                    'Me.FlowLayoutPanel1.Controls.Add(Pic)
                    If bAbort Then
                        Exit Sub
                    End If
                    Me.Invoke(New AddPictureToFlowPanel1Delegate(AddressOf AddPictureToFlowPanel1), New Object() {Pic})
                Next
            ElseIf Not IsNothing(oFDimension) Then
                I.SelectActiveFrame(oFDimension, x)
                Dim CB As New Drawing.Image.GetThumbnailImageAbort(AddressOf CB1)
                Dim CBData As System.IntPtr
                Dim T, Toi As Image
                Toi = I.GetThumbnailImage(I.Width, I.Height, CB, CBData)
                T = Toi.GetThumbnailImage(zoomw, zoomh(Toi), CB, CBData)
                Dim Pic As New Pic
                'Pic.OriginalImage = Toi
                Pic.OriginalImage = DirectCast(I.Clone, Drawing.Image)
                Pic.Size = T.Size
                Pic.Image = T
                AddHandler Pic.MouseDown, AddressOf MouseDown
                AddHandler Pic.DragOver, AddressOf DragOver2
                AddHandler Pic.MouseEnter, AddressOf MouseEnter1
                AddHandler Pic.MouseLeave, AddressOf MouseLeave1
                'Me.FlowLayoutPanel1.Controls.Add(Pic)
                If bAbort Then
                    Exit Sub
                End If
                Me.Invoke(New AddPictureToFlowPanel1Delegate(AddressOf AddPictureToFlowPanel1), New Object() {Pic})
            End If
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        Finally
            Application.DoEvents()
            Me.Invoke(New LockMeDelegate(AddressOf LockMe), New Object() {False})
        End Try

    End Sub

    Delegate Sub AddPictureToFlowPanel1Delegate(ByVal p As Pic)
    Private Sub AddPictureToFlowPanel1(ByVal p As Pic)
        Me.FlowLayoutPanel1.Controls.Add(p)
    End Sub

    Private Sub GetThumbs2(ByVal I As Image)
        Try
            Dim iCount As Integer
            Dim actualFrame As Integer
            Dim oFDimension As System.Drawing.Imaging.FrameDimension

            iCount = 0
            actualFrame = 0
            oFDimension = New System.Drawing.Imaging.FrameDimension(I.FrameDimensionsList(actualFrame))
            iCount = I.GetFrameCount(oFDimension)
            Dim x As Integer
            If iCount > 0 Then
                For x = 0 To iCount - 1
                    I.SelectActiveFrame(oFDimension, x)
                    Dim CB As New Drawing.Image.GetThumbnailImageAbort(AddressOf CB1)
                    Dim CBData As System.IntPtr
                    Dim T, Toi As Image
                    Toi = I.GetThumbnailImage(I.Width, I.Height, CB, CBData)
                    T = Toi.GetThumbnailImage(zoomw, zoomh(Toi), CB, CBData)
                    Dim Pic As New Pic
                    'Pic.OriginalImage = Toi
                    Pic.OriginalImage = DirectCast(I.Clone, Image)
                    Pic.Size = T.Size
                    Pic.Image = T
                    AddHandler Pic.MouseDown, AddressOf MouseDown
                    AddHandler Pic.DragOver, AddressOf DragOver2
                    AddHandler Pic.MouseEnter, AddressOf MouseEnter1
                    AddHandler Pic.MouseLeave, AddressOf MouseLeave1
                    Me.FlowLayoutPanel2.Controls.Add(Pic)
                Next
            ElseIf Not IsNothing(oFDimension) Then
                I.SelectActiveFrame(oFDimension, x)
                Dim CB As New Drawing.Image.GetThumbnailImageAbort(AddressOf CB1)
                Dim CBData As System.IntPtr
                Dim T, Toi As Image
                Toi = I.GetThumbnailImage(I.Width, I.Height, CB, CBData)
                T = Toi.GetThumbnailImage(zoomw, zoomh(Toi), CB, CBData)
                Dim Pic As New Pic
                'Pic.OriginalImage = Toi
                Pic.OriginalImage = DirectCast( I.Clone, Image)
                Pic.Size = T.Size
                Pic.Image = T
                AddHandler Pic.MouseDown, AddressOf MouseDown
                AddHandler Pic.DragOver, AddressOf DragOver2
                AddHandler Pic.MouseEnter, AddressOf MouseEnter1
                AddHandler Pic.MouseLeave, AddressOf MouseLeave1
                Me.FlowLayoutPanel2.Controls.Add(Pic)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function CB1() As Boolean
    End Function
    Dim Zoomfactor As Int16 = 50
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Me.zoomw = Me.zoomw + Zoomfactor
            myResize()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Try
            If Me.zoomw > Zoomfactor Then Me.zoomw = Me.zoomw - Zoomfactor
            myResize()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub myResize()
        Try
            Dim t1 As New Threading.Thread(AddressOf ResizeT1)
            t1.Start()
            Dim t2 As New Threading.Thread(AddressOf ResizeT2)
            t2.Start()
        Catch ex As Threading.AbandonedMutexException
        Catch ex As Threading.SemaphoreFullException
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStartException
        Catch ex As Threading.ThreadStateException
        Catch ex As Threading.WaitHandleCannotBeOpenedException
        Catch ex As Exception
        End Try
    End Sub
    Delegate Sub DAdjustSize(ByVal P As Pic, ByVal Size As Size)
    Private Sub AdjustSize(ByVal P As Pic, ByVal Size As Size)
        P.Size = Size
    End Sub
    Private Resizing1 As Boolean
    Private Resizing2 As Boolean
    Private Sub ResizeT1()
        Try
            If Me.Resizing1 = False Then
                Resizing1 = True
                Dim CB As New Drawing.Image.GetThumbnailImageAbort(AddressOf CB1)
                Dim CBData As System.IntPtr
                For Each P As Pic In Me.FlowLayoutPanel1.Controls
                    '  P.OriginalImage.SelectActiveFrame(P.Dimension, P.Page)
                    Dim Size As New Size(Me.zoomw, Me.zoomh(P.OriginalImage))
                    P.Invoke(New DAdjustSize(AddressOf AdjustSize), New Object() {P, Size})
                    Dim T As Image
                    T = P.OriginalImage.GetThumbnailImage(P.Width, P.Height, CB, CBData)
                    P.Image = T
                Next
                Resizing1 = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ResizeT2()
        Try
            If Me.Resizing2 = False Then
                Resizing2 = True
                Dim CB As New Drawing.Image.GetThumbnailImageAbort(AddressOf CB1)
                Dim CBData As System.IntPtr
                For Each p As Pic In Me.FlowLayoutPanel2.Controls
                    '                    p.OriginalImage.SelectActiveFrame(p.Dimension, p.Page)
                    Dim Size As New Size(Me.zoomw, Me.zoomh(p.OriginalImage))
                    p.Invoke(New DAdjustSize(AddressOf AdjustSize), New Object() {p, Size})
                    Dim T As Image
                    T = p.OriginalImage.GetThumbnailImage(p.Width, p.Height, CB, CBData)
                    p.Image = T
                Next
                Resizing2 = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private zoomw As Integer = 100
    Private zoomtw As Integer = 100
    Private ReadOnly Property zoomh(ByVal I As Image) As Integer
        Get
            Dim R As Decimal
            R = CDec(I.Height / I.Width)
            Return CInt(zoomw * R)
        End Get
    End Property
    Private ReadOnly Property zoomth(ByVal I As Image) As Integer
        Get
            Dim R As Decimal
            R = CDec(I.Height / I.Width)
            Return CInt(zoomtw * R)
        End Get
    End Property

    Dim Dragging As Boolean
    Dim WithEvents DraggingObject As PictureBox
    Dim WithEvents DraggingImage As Image
    Dim WithEvents DraggingRepresentation As New Form3
    Private Shadows Sub MouseDown(ByVal o As Object, ByVal e As MouseEventArgs)
        If Dragging = False Then
            Dragging = True
            Me.DraggingRepresentation.Location = e.Location
            Me.DraggingRepresentation.Size = New Size(zoomtw, zoomth(DirectCast(o, PictureBox).Image))
            Me.DraggingRepresentation.Image = DirectCast(o, PictureBox).Image
            Me.DraggingRepresentation.Visible = True
            Me.DraggingObject = DirectCast(o, PictureBox)
            Me.DraggingImage = DirectCast(o, PictureBox).Image
            DirectCast(o, PictureBox).DoDragDrop(o, DragDropEffects.All)
        End If
    End Sub

    Private Sub DragDrop1(ByVal sender As Object, ByVal e As DragEventArgs) Handles FlowLayoutPanel2.DragDrop, FlowLayoutPanel1.DragDrop, DraggingRepresentation.DragDrop
        Try
            If Dragging = True Then
                Dragging = False
                Me.DraggingRepresentation.Visible = False
                DirectCast(Me.DestinationPanel, FlowLayoutPanel).Controls.Add(Me.DraggingObject)

                If String.Compare(sender.GetType.Name, "FlowLayoutPanel", True) = 0 Then
                    Dim c As PictureBox
                    c = DirectCast(DirectCast(sender, FlowLayoutPanel).GetChildAtPoint(DirectCast(sender, FlowLayoutPanel).PointToClient(New Point(e.X, e.Y)), GetChildAtPointSkip.None), PictureBox)
                    Try
                        If IsNothing(c) Then
                            Me.Position = CInt(DirectCast(sender, FlowLayoutPanel).Controls.Count)
                        Else
                            Me.Position = DirectCast(sender, FlowLayoutPanel).Controls.GetChildIndex(c)
                        End If
                    Catch ex As Exception
                    End Try
                    Try
                        DirectCast(Me.DestinationPanel, FlowLayoutPanel).Controls.SetChildIndex(Me.DraggingObject, Position)
                    Catch ex As Exception
                    End Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Dim DestinationPanel As Object
    Private Sub DragEnter1(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowLayoutPanel2.DragEnter, FlowLayoutPanel1.DragEnter
        e.Effect = DragDropEffects.All
        Me.DestinationPanel = sender
    End Sub
    Private Sub DragEnter2(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DraggingRepresentation.DragEnter
        e.Effect = DragDropEffects.All
    End Sub
    Private Sub DragOver1(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowLayoutPanel1.DragOver, FlowLayoutPanel2.DragOver, ToolStripContainer1.DragOver, ToolStrip1.DragOver, ToolStripButton1.DragOver, MyBase.DragOver, DraggingRepresentation.DragOver
        Try
            If Dragging = True Then
                Me.DraggingRepresentation.Location = New Point(e.X + 1, e.Y + 1)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Dim Position As Int32
    Private Sub DragOver2(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        Try
            If Dragging = True Then
                Me.DraggingRepresentation.Location = New Point(e.X + 1, e.Y + 1)
            End If
            Try
                Me.Position = DirectCast(sender, PictureBox).Parent.Controls.GetChildIndex(DirectCast(sender, PictureBox))
            Catch ex As Exception
            End Try
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub MouseEnter1(ByVal sender As Object, ByVal e As EventArgs)
        Try
            DirectCast(sender, Pic).BorderStyle = BorderStyle.FixedSingle
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MouseLeave1(ByVal sender As Object, ByVal e As EventArgs)
        Try
            DirectCast(sender, Pic).BorderStyle = BorderStyle.None
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            If Me.ToolStripComboBox1.Text = "Horizontal" Then
                Me.SplitContainer1.Orientation = Orientation.Horizontal
                Me.FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight
                Me.FlowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight
            Else
                Me.SplitContainer1.Orientation = Orientation.Vertical
                Me.FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown
                Me.FlowLayoutPanel2.FlowDirection = FlowDirection.TopDown
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Metodos IPlugIn"

    Dim bAbort As Boolean = False
    Dim LocalResult As Object

    Public Sub initialize(ByVal o As Object)
        Try
            Me.localresult = o
            Me.AddResult(DirectCast(o, Zamba.Core.TaskResult).FullPath)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Sub play()
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Show()
    End Sub

    Public Event CloseDocument(ByRef Result As Object)
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        bAbort = True
        RaiseEvent CloseDocument(Me.LocalResult)

        Me.Close()
    End Sub


    Public Event Save(ByRef Result As Object)
    ''' <summary>
    ''' Guarda la imagen 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveDocument()
        Try
            Dim m_path As String
            Dim m_helper As ImageHelper = New ImageHelper()

            m_helper.PicArray = DirectCast(Me.FlowLayoutPanel1.Controls, IList)
            m_helper.PathOriginal = m_FileResult
            m_path = m_helper.Save()
            RaiseEvent Save(Me.localresult)
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Me.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Try
            SaveDocument()
            MessageBox.Show("La imagen se a guardado.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
            Me.Enabled = True
        End Try
    End Sub

End Class
