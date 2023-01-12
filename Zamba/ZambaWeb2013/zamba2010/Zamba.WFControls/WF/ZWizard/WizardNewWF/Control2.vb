Public Class Control2
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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents PanelLine As System.Windows.Forms.Panel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lnkDelete As ZLinkLabel
    Friend WithEvents lnkAdd As ZLinkLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents lstSteps As System.Windows.Forms.ListView
    Friend WithEvents lnkInitial As ZLinkLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtHelp As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Control2))
        Panel1 = New ZPanel
        txtHelp = New TextBox
        Label5 = New ZLabel
        lnkInitial = New ZLinkLabel
        Label2 = New ZLabel
        Label4 = New ZLabel
        txtDescription = New TextBox
        Label3 = New ZLabel
        txtName = New TextBox
        lnkDelete = New ZLinkLabel
        lstSteps = New System.Windows.Forms.ListView
        lnkAdd = New ZLinkLabel
        PanelLine = New System.Windows.Forms.Panel
        Label1 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'Panel1
        '
        Panel1.Controls.Add(txtHelp)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(lnkInitial)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(txtDescription)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(txtName)
        Panel1.Controls.Add(lnkDelete)
        Panel1.Controls.Add(lstSteps)
        Panel1.Controls.Add(lnkAdd)
        Panel1.Controls.Add(PanelLine)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(550, 393)
        Panel1.TabIndex = 1
        '
        'txtHelp
        '
        txtHelp.BackColor = System.Drawing.Color.White
        txtHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtHelp.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtHelp.Location = New System.Drawing.Point(176, 192)
        txtHelp.MaxLength = 100
        txtHelp.Name = "txtHelp"
        txtHelp.Size = New System.Drawing.Size(320, 23)
        txtHelp.TabIndex = 2
        txtHelp.Text = ""
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(40, 256)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(128, 23)
        Label5.TabIndex = 113
        Label5.Text = "Etapas Creadas"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        '
        'lnkInitial
        '
        lnkInitial.ActiveLinkColor = System.Drawing.Color.Black
        lnkInitial.BackColor = System.Drawing.Color.WhiteSmoke
        lnkInitial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lnkInitial.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkInitial.ForeColor = System.Drawing.Color.White
        lnkInitial.LinkBehavior = LinkBehavior.NeverUnderline
        lnkInitial.LinkColor = System.Drawing.Color.Black
        lnkInitial.Location = New System.Drawing.Point(352, 321)
        lnkInitial.Name = "lnkInitial"
        lnkInitial.Size = New System.Drawing.Size(128, 24)
        lnkInitial.TabIndex = 5
        lnkInitial.TabStop = True
        lnkInitial.Text = "Establecer como Inicial"
        lnkInitial.TextAlign = ContentAlignment.MiddleCenter
        lnkInitial.VisitedLinkColor = System.Drawing.Color.Black
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(40, 144)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(128, 23)
        Label2.TabIndex = 111
        Label2.Text = "Nombre "
        Label2.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(40, 192)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(128, 23)
        Label4.TabIndex = 110
        Label4.Text = "Ayuda"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        '
        'txtDescription
        '
        txtDescription.BackColor = System.Drawing.Color.White
        txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtDescription.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtDescription.Location = New System.Drawing.Point(176, 168)
        txtDescription.MaxLength = 100
        txtDescription.Name = "txtDescription"
        txtDescription.Size = New System.Drawing.Size(320, 23)
        txtDescription.TabIndex = 1
        txtDescription.Text = ""
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(40, 168)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(128, 23)
        Label3.TabIndex = 109
        Label3.Text = "Descripcion"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        '
        'txtName
        '
        txtName.BackColor = System.Drawing.Color.White
        txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtName.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        txtName.Location = New System.Drawing.Point(176, 144)
        txtName.MaxLength = 50
        txtName.Name = "txtName"
        txtName.Size = New System.Drawing.Size(320, 23)
        txtName.TabIndex = 0
        txtName.Text = ""
        '
        'lnkDelete
        '
        lnkDelete.ActiveLinkColor = System.Drawing.Color.Black
        lnkDelete.BackColor = System.Drawing.Color.WhiteSmoke
        lnkDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lnkDelete.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkDelete.ForeColor = System.Drawing.Color.White
        lnkDelete.LinkBehavior = LinkBehavior.NeverUnderline
        lnkDelete.LinkColor = System.Drawing.Color.Black
        lnkDelete.Location = New System.Drawing.Point(392, 346)
        lnkDelete.Name = "lnkDelete"
        lnkDelete.Size = New System.Drawing.Size(88, 24)
        lnkDelete.TabIndex = 6
        lnkDelete.TabStop = True
        lnkDelete.Text = "Eliminar"
        lnkDelete.TextAlign = ContentAlignment.MiddleCenter
        lnkDelete.VisitedLinkColor = System.Drawing.Color.Black
        '
        'lstSteps
        '
        lstSteps.BackColor = System.Drawing.Color.White
        lstSteps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstSteps.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lstSteps.FullRowSelect = True
        lstSteps.GridLines = True
        lstSteps.Location = New System.Drawing.Point(176, 256)
        lstSteps.Name = "lstSteps"
        lstSteps.Size = New System.Drawing.Size(320, 64)
        lstSteps.TabIndex = 4
        lstSteps.View = System.Windows.Forms.View.List
        '
        'lnkAdd
        '
        lnkAdd.ActiveLinkColor = System.Drawing.Color.Black
        lnkAdd.BackColor = System.Drawing.Color.WhiteSmoke
        lnkAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lnkAdd.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkAdd.ForeColor = System.Drawing.Color.White
        lnkAdd.LinkBehavior = LinkBehavior.NeverUnderline
        lnkAdd.LinkColor = System.Drawing.Color.Black
        lnkAdd.Location = New System.Drawing.Point(344, 216)
        lnkAdd.Name = "lnkAdd"
        lnkAdd.Size = New System.Drawing.Size(136, 24)
        lnkAdd.TabIndex = 3
        lnkAdd.TabStop = True
        lnkAdd.Text = "Crear Etapa"
        lnkAdd.TextAlign = ContentAlignment.MiddleCenter
        lnkAdd.VisitedLinkColor = System.Drawing.Color.Black
        '
        'PanelLine
        '
        PanelLine.BackColor = System.Drawing.Color.Black
        PanelLine.Location = New System.Drawing.Point(24, 112)
        PanelLine.Name = "PanelLine"
        PanelLine.Size = New System.Drawing.Size(496, 1)
        PanelLine.TabIndex = 2
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 15.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(24, 32)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(504, 72)
        Label1.TabIndex = 0
        Label1.Text = "Por ultimo cree sus etapas de desarrollo y seleccione una inicial (opcional)   "
        '
        'Control2
        '
        Controls.Add(Panel1)
        Name = "Control2"
        Size = New System.Drawing.Size(550, 393)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public ReadOnly Property SWFSteps() As SWfStep()
        Get
            Dim s(lstSteps.Items.Count - 1) As SWfStep
            For i As Int16 = 0 To CShort(lstSteps.Items.Count - 1)
                Dim lvitem As LvItem = DirectCast(lstSteps.Items(i), LvItem)
                s(i) = lvitem.swfstep
            Next
            Return s
        End Get
    End Property

#Region "add/initial/remove"
    Private Sub lnkAdd_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkAdd.LinkClicked
        Try
            If txtName.Text <> "" Then
                Dim SWfStep As SWfStep
                SWfStep.Name = txtName.Text
                SWfStep.Description = txtDescription.Text
                SWfStep.Help = txtHelp.Text
                lstSteps.Items.Add(New LvItem(SWfStep))
                txtName.Text = ""
                txtDescription.Text = ""
                txtHelp.Text = ""
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub lnkInitial_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkInitial.LinkClicked
        Try
            If lstSteps.SelectedItems.Count > 0 Then
                For Each item As ListViewItem In lstSteps.Items
                    Dim lvitem As LvItem = DirectCast(item, LvItem)
                    If lvitem Is lstSteps.SelectedItems(0) Then
                        lvitem.Initial = True
                    Else
                        lvitem.Initial = False
                    End If
                Next
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub lnkDelete_LinkClicked(ByVal sender As System.Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lnkDelete.LinkClicked
        Try
            If lstSteps.SelectedItems.Count > 0 Then
                For Each item As ListViewItem In lstSteps.SelectedItems
                    item.Remove()
                Next
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "ListView Item"
    Private Class LvItem
        Inherits ListViewItem
        Public swfstep As swfstep
        Sub New(ByVal s As swfstep)
            Text = s.Name
            swfstep = s
        End Sub
        Public WriteOnly Property Initial() As Boolean
            'Get
            '    Return Me.swfstep.Initial
            'End Get
            Set(ByVal Value As Boolean)
                swfstep.Initial = Value
                If swfstep.Initial Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property
    End Class
#End Region

End Class

Public Structure SWfStep
    Public Name As String
    Public Description As String
    Public Help As String
    Public Initial As Boolean
End Structure
