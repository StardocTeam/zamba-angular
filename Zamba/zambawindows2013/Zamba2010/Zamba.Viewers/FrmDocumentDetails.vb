Imports Zamba.Core


Public Class FrmDocumentDetails
    Inherits ZForm

    Public Sub New(ByRef Result As Result)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        Dim sbInfo As New System.Text.StringBuilder

        Try
            sbInfo.AppendLine("Documento: " & Result.FullPath)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Entidad: " & Result.DocType.Name)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Id de Entidad: " & Result.DocTypeId)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Fecha de Creacion: " & Result.CreateDate)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Fecha de Edicion: " & Result.EditDate)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Id de Documento: " & Result.ID)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Documento es Imagen: " & Result.IsImage)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Nombre Automático: " & Result.Name)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            sbInfo.AppendLine("Id de Volumen: " & Result.Disk_Group_Id)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Try
            sbInfo.AppendLine("Id de Usuario Creacion: " & Result.UserID)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Dim ResultId As IUser = UserBusiness.GetUserById(Result.UserID)

        Try
            sbInfo.AppendLine("Nombre de Usuario Creacion: " & ResultId.Name)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try



        txtInfo.Text = sbInfo.ToString
        txtInfo.ScrollToCaret()
        sbInfo = Nothing

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
    Friend WithEvents txtInfo As TextBox

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Label1 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrmDocumentDetails))
        Label1 = New ZLabel()
        txtInfo = New TextBox()
        SuspendLayout()
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.White
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = New Font("Arial", 9.75!, FontStyle.Bold)
        Label1.ForeColor = System.Drawing.Color.White
        Label1.Location = New System.Drawing.Point(2, 2)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(644, 43)
        Label1.TabIndex = 1
        Label1.Text = "ZAMBA SOFTWARE: Información de Documento"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtInfo
        '
        txtInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtInfo.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtInfo.Location = New System.Drawing.Point(2, 49)
        txtInfo.Multiline = True
        txtInfo.Name = "txtInfo"
        txtInfo.ReadOnly = True
        txtInfo.Size = New System.Drawing.Size(644, 297)
        txtInfo.TabIndex = 2
        '
        'FrmDocumentDetails
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        ClientSize = New System.Drawing.Size(648, 351)
        Controls.Add(txtInfo)
        Controls.Add(Label1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "FrmDocumentDetails"
        Text = "Informacion de Documento"
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region


End Class
