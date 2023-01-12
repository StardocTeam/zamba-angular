Imports Zamba.Core.WF.WF
Imports Zamba.Core
Imports Zamba.Data
Public Class FrmAddToWorkFlow
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "



    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents PanelWFs As System.Windows.Forms.Panel
    Friend WithEvents BtnClose As ZButton
    Friend WithEvents lbletapa As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents cbAgregarCarpeta As System.Windows.Forms.CheckBox
    Friend WithEvents PanelResults As System.Windows.Forms.Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        btnAceptar = New ZButton
        PanelWFs = New System.Windows.Forms.Panel
        PanelResults = New System.Windows.Forms.Panel
        Label1 = New ZLabel
        Label2 = New ZLabel
        BtnClose = New ZButton
        lbletapa = New ZLabel
        Label4 = New ZLabel
        cbAgregarCarpeta = New System.Windows.Forms.CheckBox
        SuspendLayout()
        '
        'btnAceptar
        '
        btnAceptar.Location = New System.Drawing.Point(24, 446)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(132, 24)
        btnAceptar.TabIndex = 2
        btnAceptar.Text = "Aceptar"
        '
        'PanelWFs
        '
        PanelWFs.AutoScroll = True
        PanelWFs.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        PanelWFs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelWFs.Location = New System.Drawing.Point(24, 240)
        PanelWFs.Name = "PanelWFs"
        PanelWFs.Size = New System.Drawing.Size(276, 136)
        PanelWFs.TabIndex = 1
        '
        'PanelResults
        '
        PanelResults.AutoScroll = True
        PanelResults.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        PanelResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelResults.Location = New System.Drawing.Point(24, 40)
        PanelResults.Name = "PanelResults"
        PanelResults.Size = New System.Drawing.Size(276, 156)
        PanelResults.TabIndex = 0
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(32, 16)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(160, 16)
        Label1.TabIndex = 18
        Label1.Text = "Documentos"
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(36, 216)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(124, 16)
        Label2.TabIndex = 19
        Label2.Text = "Insertar en"
        '
        'BtnClose
        '
        BtnClose.Location = New System.Drawing.Point(168, 446)
        BtnClose.Name = "BtnClose"
        BtnClose.Size = New System.Drawing.Size(132, 24)
        BtnClose.TabIndex = 20
        BtnClose.Text = "Cerrar"
        '
        'lbletapa
        '
        lbletapa.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        lbletapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lbletapa.Location = New System.Drawing.Point(25, 411)
        lbletapa.Name = "lbletapa"
        lbletapa.Size = New System.Drawing.Size(275, 20)
        lbletapa.TabIndex = 21
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(24, 382)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(115, 18)
        Label4.TabIndex = 22
        Label4.Text = "Etapa Inicial"
        '
        'cbAgregarCarpeta
        '
        cbAgregarCarpeta.AutoSize = True
        cbAgregarCarpeta.BackColor = System.Drawing.Color.Transparent
        cbAgregarCarpeta.Location = New System.Drawing.Point(193, 202)
        cbAgregarCarpeta.Name = "cbAgregarCarpeta"
        cbAgregarCarpeta.Size = New System.Drawing.Size(107, 17)
        cbAgregarCarpeta.TabIndex = 23
        cbAgregarCarpeta.Text = "Agregar Carpeta"
        cbAgregarCarpeta.UseVisualStyleBackColor = False
        '
        'FrmAddToWorkFlow
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(324, 492)
        Controls.Add(cbAgregarCarpeta)
        Controls.Add(Label4)
        Controls.Add(lbletapa)
        Controls.Add(BtnClose)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(PanelResults)
        Controls.Add(PanelWFs)
        Controls.Add(btnAceptar)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "FrmAddToWorkFlow"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Agregar a WorkFlow"
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region
    Dim Results As New ArrayList
    Public ReadOnly Property Tasks() As ArrayList
        Get
            Return Results
        End Get
    End Property
    Private Class chkResult
        Inherits CheckBox
        Public Result As Result
        Sub New(ByRef Result As Result, ByVal Number As Int32, ByVal Location As Point)
            Me.Result = Result
            Checked = True
            CheckState = System.Windows.Forms.CheckState.Checked
            FlatStyle = FlatStyle.Flat
            Me.Location = Location
            Size = New System.Drawing.Size(228, 20)
            TabIndex = Number
            Text = Result.Name
        End Sub
    End Class
    Private Class chkWF
        Inherits CheckBox
        Public WF As Core.WorkFlow
        Sub New(ByVal WF As Core.WorkFlow, ByVal Number As Int32, ByVal Location As Point)
            Me.WF = WF
            Checked = False
            CheckState = System.Windows.Forms.CheckState.Unchecked
            FlatStyle = FlatStyle.Flat
            Me.Location = Location
            Size = New System.Drawing.Size(228, 20)
            TabIndex = Number
            Text = WF.Name
        End Sub
        Private Sub chkWF_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.CheckedChanged

        End Sub
    End Class

    Public Sub New(ByVal Results As List(Of IResult))
        MyBase.New()
        InitializeComponent()
        Try
            Dim i As Int32
            'Cargo Cheks de Results
            For Each r As Result In Results
                PanelResults.Controls.Add(New chkResult(r, i, New Point(8, 8 + i * 20)))
                i += 1
            Next
            'Cargo Cheks de WFs
            Dim WFs As New ArrayList
            WFs = WFFactory.GetWFsToAddDocuments(Membership.MembershipHelper.CurrentUser)
            i = 0
            If Not WFs Is Nothing OrElse Not WFs.Count > 0 Then
                For Each wf As Core.WorkFlow In WFs
                    Dim uc As chkWF = New chkWF(wf, i, New Point(8, 8 + i * 20))
                    RemoveHandler uc.CheckedChanged, AddressOf WFCheckedChange
                    AddHandler uc.CheckedChanged, AddressOf WFCheckedChange
                    PanelWFs.Controls.Add(uc)
                    i += 1
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Add the results to the Results ArrayList acording to the CheckBox cbAgregarCarpeta
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddResults(ByVal addChilds As Boolean)
        If addChilds Then
            For Each control As Control In PanelResults.Controls
                If TypeOf control Is chkResult AndAlso DirectCast(control, chkResult).Checked = True Then
                    Dim result As Result = DirectCast(control, chkResult).Result
                    Results.Add(result)
                    If result.ChildsResults.Count > 0 Then
                        Results.AddRange(result.ChildsResults)
                    End If
                End If
            Next
        Else
            For Each control As Control In PanelResults.Controls
                If TypeOf control Is chkResult AndAlso DirectCast(control, chkResult).Checked = True Then
                    Results.Add(DirectCast(control, chkResult).Result)
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' Add the Results to the Workflow
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddResultsToWorkflow(Optional ByVal LogInAction As Boolean = True)
        For Each control As Control In PanelWFs.Controls
            If TypeOf control Is chkWF Then
                If DirectCast(control, chkWF).Checked = True Then
                    Try
                        WFTaskBusiness.AddResultsToWorkFLow(Results, DirectCast(control, chkWF).WF, LogInAction)

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        MessageBox.Show(ex.Message, "Agregar a Workflow", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Try
                End If
            End If
        Next
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor
        Try
            AddResults(cbAgregarCarpeta.Checked)
            AddResultsToWorkflow()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Cursor = cur
            Close()
        End Try
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnClose.Click
        Try
            Close()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub WFCheckedChange(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim R As chkWF = DirectCast(sender, chkWF)
            If R.Checked = True Then
                If Not IsNothing(R.WF.InitialStep) Then
                    lbletapa.Text = R.WF.InitialStep.Name
                    btnAceptar.Visible = True
                Else
                    lbletapa.Text = "Defina una etapa inicial"
                    btnAceptar.Visible = False

                End If
            Else
                lbletapa.Text = ""
                btnAceptar.Visible = True
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class

