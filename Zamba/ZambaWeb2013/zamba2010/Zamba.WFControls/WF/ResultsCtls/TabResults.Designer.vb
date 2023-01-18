Namespace WF.ResultsCtls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class TabResults
        Inherits System.Windows.Forms.Control
        Implements IDisposable

        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        'UserControl overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not isDisposed Then
                If disposing Then
                    If components IsNot Nothing Then
                        components.Dispose()
                    End If
                    If SplitContainer3ResultsHorizontal IsNot Nothing Then
                        RemoveHandler Me.SplitContainer3ResultsHorizontal.SplitterMoved, AddressOf SaveGridSplitterPosition
                        RemoveHandler Me.SplitContainer3ResultsHorizontal.SplitterMoving, AddressOf SaveSplitterMovementsFlag
                        SplitContainer3ResultsHorizontal.Panel1.Controls.Clear()
                        SplitContainer3ResultsHorizontal.Panel2.Controls.Clear()
                        SplitContainer3ResultsHorizontal.Dispose()
                        SplitContainer3ResultsHorizontal = Nothing
                    End If
                    If UCFusion2 IsNot Nothing Then
                        RemoveHandler UCFusion2.ResultDoubleClick, AddressOf ShowResult

                        RemoveHandler UCFusion2.CambiarNombre, AddressOf CambiarNombreResult
                        RemoveHandler UCFusion2.ExportarAExcel, AddressOf ExportarAExcelResult
                        RemoveHandler UCFusion2.ShowComment, AddressOf ShowVersionComment
                        RemoveHandler UCFusion2.GenerarListado, AddressOf GenerarListadoResult
                        RemoveHandler UCFusion2.CloseResult, AddressOf CloseDocumentViewerTab
                        UCFusion2.Dispose()
                        UCFusion2 = Nothing
                    End If
                    If TabViewers IsNot Nothing Then
                        RemoveHandler TabViewers.SelectedIndexChanged, AddressOf TabViewers_SelectedIndexChanged
                        RemoveHandler TabViewers.TabIndexChanged, AddressOf TabViewers_ActiveTabChanged
                        RemoveHandler TabViewers.ControlRemoved, AddressOf TabViewer_TabRemoved
                        For Each ctrlviewer As Object In TabViewers.TabPages
                            If TypeOf (ctrlviewer) Is IDisposable AndAlso ctrlviewer IsNot Nothing Then
                                If TypeOf (ctrlviewer) Is Zamba.Viewers.UCDocumentViewer2 Then
                                    ctrlviewer.DisposeIndexViewer = True
                                End If
                                ctrlviewer.Dispose()
                            End If
                        Next

                        TabViewers.Dispose()
                        TabViewers = Nothing
                    End If
                    If SplitTasks IsNot Nothing Then
                        SplitTasks.Panel1.Controls.Clear()
                        SplitTasks.Panel2.Controls.Clear()
                        SplitTasks.Dispose()
                        SplitTasks = Nothing
                    End If
                    If TabSecondaryTask IsNot Nothing Then
                        TabSecondaryTask.Dispose()
                        TabSecondaryTask = Nothing
                    End If

                    If LocalResult IsNot Nothing Then
                        LocalResult.Dispose()
                        LocalResult = Nothing
                    End If
                    If TabForo IsNot Nothing Then
                        TabForo.Dispose()
                        TabForo = Nothing
                    End If
                    If TabDocAsociated IsNot Nothing Then
                        TabDocAsociated.Dispose()
                        TabDocAsociated = Nothing
                    End If
                    If TabHistorialEmails IsNot Nothing Then
                        TabHistorialEmails.Dispose()
                        TabHistorialEmails = Nothing
                    End If

                    If extVis IsNot Nothing Then
                        extVis.Dispose()
                        extVis = Nothing
                    End If

                    If _selectedresult IsNot Nothing Then
                        _selectedresult.Dispose()
                        _selectedresult = Nothing
                    End If
                    If tabgrid IsNot Nothing Then
                        tabgrid.Dispose()
                        tabgrid = Nothing
                    End If
                    If UcForo IsNot Nothing Then
                        RemoveHandler UcForo.showMailForm, AddressOf sendMailFromForum
                        UcForo.Dispose()
                        UcForo = Nothing
                    End If
                    If ucHistorialEmails IsNot Nothing Then
                        ucHistorialEmails.Dispose()
                        ucHistorialEmails = Nothing
                    End If
                    If toolbar IsNot Nothing Then
                        toolbar.Dispose()
                        toolbar = Nothing
                    End If
                End If

                MyBase.Dispose(disposing)

                isDisposed = True
            End If
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
        Private isDisposed As Boolean

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        Me.SplitContainer3ResultsHorizontal = New System.Windows.Forms.SplitContainer()
        Me.SplitTasks = New System.Windows.Forms.SplitContainer()
        Me.TabViewers = New System.Windows.Forms.TabControl()
        Me.TabForo = New System.Windows.Forms.TabPage()
        Me.TabDocAsociated = New System.Windows.Forms.TabPage()
        Me.TabHistorialEmails = New System.Windows.Forms.TabPage()
        Me.TabSecondaryTask = New System.Windows.Forms.TabControl()
        CType(Me.SplitContainer3ResultsHorizontal,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer3ResultsHorizontal.Panel2.SuspendLayout
        Me.SplitContainer3ResultsHorizontal.SuspendLayout
        CType(Me.SplitTasks,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitTasks.Panel1.SuspendLayout
        Me.SplitTasks.Panel2.SuspendLayout
        Me.SplitTasks.SuspendLayout
        Me.TabViewers.SuspendLayout
        Me.SuspendLayout
        '
        'SplitContainer3ResultsHorizontal
        '
        Me.SplitContainer3ResultsHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3ResultsHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3ResultsHorizontal.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer3ResultsHorizontal.Name = "SplitContainer3ResultsHorizontal"
        Me.SplitContainer3ResultsHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3ResultsHorizontal.Panel2
        '
        Me.SplitContainer3ResultsHorizontal.Panel2.Controls.Add(Me.SplitTasks)
        Me.SplitContainer3ResultsHorizontal.Panel2Collapsed = true
        Me.SplitContainer3ResultsHorizontal.Size = New System.Drawing.Size(886, 556)
        Me.SplitContainer3ResultsHorizontal.SplitterDistance = 180
        Me.SplitContainer3ResultsHorizontal.TabIndex = 0
        '
        'SplitTasks
        '
        Me.SplitTasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitTasks.Location = New System.Drawing.Point(0, 0)
        Me.SplitTasks.Name = "SplitTasks"
        '
        'SplitTasks.Panel1
        '
        Me.SplitTasks.Panel1.Controls.Add(Me.TabViewers)
        '
        'SplitTasks.Panel2
        '
        Me.SplitTasks.Panel2.Controls.Add(Me.TabSecondaryTask)
        Me.SplitTasks.Panel2Collapsed = true
        Me.SplitTasks.Panel2MinSize = 1
        Me.SplitTasks.Size = New System.Drawing.Size(650, 370)
        Me.SplitTasks.SplitterDistance = 216
        Me.SplitTasks.TabIndex = 1
        '
        'TabViewers
        '
        Me.TabViewers.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabViewers.Controls.Add(Me.TabForo)
        Me.TabViewers.Controls.Add(Me.TabDocAsociated)
        Me.TabViewers.Controls.Add(Me.TabHistorialEmails)
        Me.TabViewers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabViewers.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TabViewers.HotTrack = true
        Me.TabViewers.Location = New System.Drawing.Point(0, 0)
        Me.TabViewers.Margin = New System.Windows.Forms.Padding(0)
        Me.TabViewers.Name = "TabViewers"
        Me.TabViewers.Padding = New System.Drawing.Point(0, 0)
        Me.TabViewers.SelectedIndex = 0
        Me.TabViewers.Size = New System.Drawing.Size(650, 370)
        Me.TabViewers.TabIndex = 0
        '
        'TabForo
        '
        Me.TabForo.BackColor = System.Drawing.Color.White
        Me.TabForo.Location = New System.Drawing.Point(4, 28)
        Me.TabForo.Name = "TabForo"
        Me.TabForo.Size = New System.Drawing.Size(642, 338)
        Me.TabForo.TabIndex = 1
        Me.TabForo.Text = "Foro"
        '
        'TabDocAsociated
        '
        Me.TabDocAsociated.BackColor = System.Drawing.Color.White
        Me.TabDocAsociated.Location = New System.Drawing.Point(4, 28)
        Me.TabDocAsociated.Name = "TabDocAsociated"
        Me.TabDocAsociated.Size = New System.Drawing.Size(642, 338)
        Me.TabDocAsociated.TabIndex = 2
        Me.TabDocAsociated.Text = "Asociados"
        '
        'TabHistorialEmails
        '
        Me.TabHistorialEmails.BackColor = System.Drawing.Color.White
        Me.TabHistorialEmails.Location = New System.Drawing.Point(4, 28)
        Me.TabHistorialEmails.Name = "TabHistorialEmails"
        Me.TabHistorialEmails.Padding = New System.Windows.Forms.Padding(3)
        Me.TabHistorialEmails.Size = New System.Drawing.Size(642, 338)
        Me.TabHistorialEmails.TabIndex = 3
        Me.TabHistorialEmails.Text = "Historial Emails"
        '
        'TabSecondaryTask
        '
        Me.TabSecondaryTask.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabSecondaryTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabSecondaryTask.Location = New System.Drawing.Point(0, 0)
        Me.TabSecondaryTask.Name = "TabSecondaryTask"
        Me.TabSecondaryTask.SelectedIndex = 0
        Me.TabSecondaryTask.Size = New System.Drawing.Size(96, 100)
        Me.TabSecondaryTask.TabIndex = 0
        '
        'TabResults
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.SplitContainer3ResultsHorizontal)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Location = New System.Drawing.Point(4, 5)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "_TabResults"
        Me.Size = New System.Drawing.Size(886, 556)
        Me.TabIndex = 1
        Me.SplitContainer3ResultsHorizontal.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer3ResultsHorizontal,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer3ResultsHorizontal.ResumeLayout(false)
        Me.SplitTasks.Panel1.ResumeLayout(false)
        Me.SplitTasks.Panel2.ResumeLayout(false)
        CType(Me.SplitTasks,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitTasks.ResumeLayout(false)
        Me.TabViewers.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

    End Class
End Namespace

