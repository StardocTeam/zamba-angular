Imports Zamba.Core.WF.WF
Imports Zamba.Core
Imports Zamba.Data

Public Class ControllerViewer
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

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
    Friend WithEvents PicBox As System.Windows.Forms.PictureBox
    Friend WithEvents LabelError As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ControllerViewer))
        PicBox = New System.Windows.Forms.PictureBox
        LabelError = New ZLabel
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'PicBox
        '
        PicBox.BackColor = System.Drawing.Color.Transparent
        PicBox.Location = New System.Drawing.Point(0, 0)
        PicBox.Name = "PicBox"
        PicBox.Size = New System.Drawing.Size(336, 344)
        PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PicBox.TabIndex = 17
        PicBox.TabStop = False
        PicBox.Visible = False
        '
        'LabelError
        '
        LabelError.BackColor = System.Drawing.Color.Transparent
        LabelError.Dock = System.Windows.Forms.DockStyle.Fill
        LabelError.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        LabelError.ForeColor = System.Drawing.Color.DarkBlue
        LabelError.Location = New System.Drawing.Point(0, 0)
        LabelError.Name = "LabelError"
        LabelError.Size = New System.Drawing.Size(336, 344)
        LabelError.TabIndex = 18
        LabelError.TextAlign = ContentAlignment.MiddleCenter
        LabelError.Visible = False
        '
        'ControllerViewer
        '
        AutoScroll = True
        BackColor = System.Drawing.Color.White
        Controls.Add(PicBox)
        Controls.Add(LabelError)
        Name = "ControllerViewer"
        Size = New System.Drawing.Size(336, 344)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private mTaskResult As ITaskResult
    Public Property TaskResult() As ITaskResult
        Get
            Return mTaskResult
        End Get
        Set(ByVal Value As ITaskResult)
            mTaskResult = Value

        End Set
    End Property

    Public Sub ShowImage()
        Try
            If TaskResult.Doc_File = "" Then WFTasksFactory.CompleteTask(DirectCast(TaskResult, TaskResult))

            WFTaskBusiness.OpenDocument(DirectCast(TaskResult, TaskResult))
        Catch ex As Exception
            ShowLabelError(ex.Message)
            Exit Sub
        End Try

        If TaskResult.IsImage Then
            Try
                ShowGrafics()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Else
            Try
                ShowLabelError("Documento de Office")
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

#Region "Graphics"
    ' Dim iCount As Integer = 0
    Dim actualFrame As Integer
    'Dim oFDimension As System.Drawing.Imaging.FrameDimension
    Private Sub ShowGrafics()
        Try
            'Mutitiff
            Try
                'iCount = 0
                actualFrame = 0
                'oFDimension = New System.Drawing.Imaging.FrameDimension(Result.Picture.Image.FrameDimensionsList(actualFrame))
                'iCount = Result.Picture.Image.GetFrameCount(oFDimension) - 1
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try

            'Ajuste de imagen inicial
            Try
                PicBox.Size = TaskResult.Picture.Size
                PicBox.Image = TaskResult.Picture.Image
                AnchoPantalla()
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try

            LabelError.Visible = False
            PicBox.Visible = True
            PicBox.BringToFront()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AnchoPantalla()
        Try
            Dim Size As System.Drawing.Size
            TaskResult.Picture.Resolution = TaskResult.Picture.AdjustImageToScreenWidth(Width - 40, TaskResult.Picture.Resolution, TaskResult.Picture.Size.Width)
            Size = TaskResult.Picture.adjustImage(TaskResult.Picture.Resolution, TaskResult.Picture.Size.Height, TaskResult.Picture.ResV, TaskResult.Picture.Size.Width, TaskResult.Picture.ResH)
            PicBox.Size = Size
            TaskResult.Picture.Size = Size
            TaskResult.Picture.ResH = TaskResult.Picture.Resolution
            TaskResult.Picture.ResV = TaskResult.Picture.Resolution
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Error"
    Private Sub ShowLabelError(ByVal ex As String)
        Try
            LabelError.Text = ex
            LabelError.Visible = True
            LabelError.BringToFront()
            PicBox.Visible = False
        Catch e As Exception
            zamba.core.zclass.raiseerror(e)
        End Try
    End Sub
#End Region

#Region "Focus"
    Private Sub PicBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PicBox.Click
        Try
            Focus()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ControllerViewer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Click
        Try
            Focus()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Double Click"
    Public Event TaskDoubleClick(ByRef TaskId As Int64)
    Private Sub PicBox_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles PicBox.DoubleClick
        Try
            RaiseEvent TaskDoubleClick(TaskResult.TaskId)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region


End Class
