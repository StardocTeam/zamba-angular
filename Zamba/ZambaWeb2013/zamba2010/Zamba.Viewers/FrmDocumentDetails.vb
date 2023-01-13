Imports Zamba.AppBlock
Imports ZAMBA.Core
Public Class FrmDocumentDetails
    Inherits ZForm


    'Public Sub New(ByVal Document As Result)
    '    MyBase.New()

    '    'El Diseñador de Windows Forms requiere esta llamada.
    '    InitializeComponent()
    '    Try
    '        Me.ListBox1.Items.Add("Documento: " & Document.FullFileName)
    '    Catch ex As Exception
    '        Me.zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Try
    '        Me.ListBox1.Items.Add("Fecha de Creacion: " & Document.CreateDate)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Try
    '        Me.ListBox1.Items.Add("Id de tipo de Documento: " & Document.DocTypeId & " Tipo de Documento: " & Document.DocTypeName)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Try
    '        Me.ListBox1.Items.Add("Fecha de Edicion: " & Document.EditDate)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Try
    '        Me.ListBox1.Items.Add("Id de Documento: " & Document.Id)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Try
    '        Me.ListBox1.Items.Add("Documento es Imagen: " & Document.IsImage)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Try
    '        Me.ListBox1.Items.Add("Nombre Automático: " & Document.Name)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    'Try
    '    '    Me.ListBox1.Items.Add("Id de Volumen: " & Document.Volume.Id)
    '    'Catch ex As Exception
    '    '   zamba.core.zclass.raiseerror(ex)
    '    'End Try
    '    Try
    '        Me.ListBox1.Items.Add("Nombre de Archivo: " & Document.FullFileName)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try


    '    'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    'End Sub
    Public Sub New(ByRef Result As Result)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Try
            If Result.Disk_Group_Id <> 0 AndAlso VolumesBusiness.GetVolumeType(Result.Disk_Group_Id) = VolumeType.DataBase Then
                Me.ListBox1.Items.Add("Documento: Información no disponible, el documento se encuentra en base de datos")
            Else
                Me.ListBox1.Items.Add("Documento: " & Result.FullPath)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            Me.ListBox1.Items.Add("Fecha de Creación: " & Result.CreateDate)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Me.ListBox1.Items.Add("Id de tipo de Documento: " & Result.DocType.ID)
            Me.ListBox1.Items.Add("Tipo de Documento: " & Result.DocType.Name)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Me.ListBox1.Items.Add("Fecha de Edición: " & Result.EditDate)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Me.ListBox1.Items.Add("Id de Documento: " & Result.Id)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Me.ListBox1.Items.Add("Documento es Imagen: " & Result.IsImage)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Me.ListBox1.Items.Add("Nombre Automático: " & Result.Name)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        'Try
        '    Me.ListBox1.Items.Add("Id de Volumen: " & Document.Volume.Id)
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        'End Try
        Try
            Me.ListBox1.Items.Add("Id de Volumen: " & Result.Disk_Group_Id)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub


#Region " Código generado por el Diseñador de Windows Forms "


    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As ZTitleLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmDocumentDetails))
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Label1 = New Zamba.AppBlock.ZTitleLabel
        Me.SuspendLayout()
        '
        'ZIconList
        '

        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.BackColor = System.Drawing.Color.White
        Me.ListBox1.Location = New System.Drawing.Point(8, 60)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(632, 264)
        Me.ListBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White

        Me.Label1.Location = New System.Drawing.Point(2, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(644, 43)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ZAMBA SOFTWARE: Información de Documento"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmDocumentDetails
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
        Me.ClientSize = New System.Drawing.Size(648, 351)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox1)
        Me.DockPadding.All = 2
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmDocumentDetails"
        Me.Text = "Informacion de Documento"
        Me.ResumeLayout(False)

    End Sub

#End Region


End Class
