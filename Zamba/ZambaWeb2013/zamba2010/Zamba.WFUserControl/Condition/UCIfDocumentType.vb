'Imports Zamba.WFBusiness
Imports Zamba.Data

Public Class UCIfDocumentType
    Inherits ZRuleControl


#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl1 reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents optEqual As System.Windows.Forms.RadioButton
    Friend WithEvents optDifferent As System.Windows.Forms.RadioButton
    Friend WithEvents FsButton1 As ZButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Label1 = New ZLabel
        optEqual = New System.Windows.Forms.RadioButton
        optDifferent = New System.Windows.Forms.RadioButton
        Label2 = New ZLabel
        FsButton1 = New ZButton
        ListView1 = New System.Windows.Forms.ListView
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(FsButton1)
        tbRule.Controls.Add(ListView1)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(optDifferent)
        tbRule.Controls.Add(optEqual)
        tbRule.Controls.Add(Label1)
        tbRule.Size = New System.Drawing.Size(464, 422)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(472, 448)
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(40, 40)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(176, 24)
        Label1.TabIndex = 11
        Label1.Text = "Seleccione el operador"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'optEqual
        '
        optEqual.BackColor = System.Drawing.Color.Transparent
        optEqual.Font = New Font("Tahoma", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        optEqual.Location = New System.Drawing.Point(48, 72)
        optEqual.Name = "optEqual"
        optEqual.Size = New System.Drawing.Size(160, 18)
        optEqual.TabIndex = 12
        optEqual.Text = " ="
        optEqual.UseVisualStyleBackColor = False
        '
        'optDifferent
        '
        optDifferent.BackColor = System.Drawing.Color.Transparent
        optDifferent.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        optDifferent.Location = New System.Drawing.Point(48, 96)
        optDifferent.Name = "optDifferent"
        optDifferent.Size = New System.Drawing.Size(160, 16)
        optDifferent.TabIndex = 13
        optDifferent.Text = " <>"
        optDifferent.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label2.Location = New System.Drawing.Point(40, 136)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(248, 24)
        Label2.TabIndex = 14
        Label2.Text = "Seleccione la entidad"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'FsButton1
        '
        FsButton1.Location = New System.Drawing.Point(160, 384)
        FsButton1.Name = "FsButton1"
        FsButton1.Size = New System.Drawing.Size(112, 23)
        FsButton1.TabIndex = 16
        FsButton1.Text = "Aceptar"
        '
        'ListView1
        '
        ListView1.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ListView1.FullRowSelect = True
        ListView1.HideSelection = False
        ListView1.Location = New System.Drawing.Point(40, 168)
        ListView1.MultiSelect = False
        ListView1.Name = "ListView1"
        ListView1.Size = New System.Drawing.Size(352, 208)
        ListView1.TabIndex = 15
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = System.Windows.Forms.View.List
        '
        'UCIfDocumentType
        '
        Name = "UCIfDocumentType"
        Size = New System.Drawing.Size(472, 448)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Dim this As IIfDocumentType
    Public Sub New(ByRef IfDocumentType As IIfDocumentType, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfDocumentType, _wfPanelCircuit)
        InitializeComponent()
        this = IfDocumentType
        Load_UCIfDocumentType()
    End Sub

#Region "Load"
    Dim DsDocTypes As DataSet
    Private Sub Load_UCIfDocumentType()
        Try
            DsDocTypes = DocTypesFactory.GetDocTypesDsDocType()

            For Each r As DataRow In DsDocTypes.Tables("DOC_TYPE").Rows
                Try
                    If this.DocTypeId = r("DOC_TYPE_ID") Then
                        ListView1.Items.Add(New DocTypeItem(r("DOC_TYPE_ID"), r("DOC_TYPE_NAME"), True))
                    Else
                        ListView1.Items.Add(New DocTypeItem(r("DOC_TYPE_ID"), r("DOC_TYPE_NAME"), False))
                    End If
                Catch
                End Try
            Next


            If this.Comp = Comparators.Equal Then
                optEqual.Checked = True
            Else
                optDifferent.Checked = True
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Class DocTypeItem
        Inherits ListViewItem
        Public Id As Int32
        Private _name As String
        Private _SelectedStep As Boolean
        Public Property SelectedDocType() As Boolean
            Get
                Return _SelectedStep
            End Get
            Set(ByVal Value As Boolean)
                _SelectedStep = Value
                If Value = 0 Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property
        Sub New(ByVal Id As Int32, ByVal Name As String, ByVal SelectedDocType As Boolean)
            Me.Id = Id
            Me.Name = Name
            Text = Name
            Me.SelectedDocType = SelectedDocType
        End Sub
    End Class
#End Region

    Private Sub FsButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FsButton1.Click
        Try
            If Not ListView1.SelectedItems.Count = 0 AndAlso Not ListView1.SelectedItems(0) Is Nothing Then
                Dim DocTypeItem As DocTypeItem = ListView1.SelectedItems(0)
                If DocTypeItem.Id <> this.DocTypeId Then
                    WFRulesBusiness.UpdateParamItem(this, 0, DocTypeItem.Id)
                    For Each l As ListViewItem In ListView1.Items
                        DirectCast(l, DocTypeItem).SelectedDocType = False
                    Next
                    DocTypeItem.SelectedDocType = True
                    this.DocTypeId = DocTypeItem.Id
                End If
            End If
            If optEqual.Checked = True AndAlso this.Comp <> Comparators.Equal Then
                this.Comp = Comparators.Equal
                WFRulesBusiness.UpdateParamItem(this, 1, 0)
            ElseIf optDifferent.Checked = True AndAlso this.Comp <> Comparators.Different Then
                WFRulesBusiness.UpdateParamItem(this, 1, 1)
                this.Comp = Comparators.Different
            End If
            UserBusiness.Rights.SaveAction(this.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & this.Name & "(" & this.ID & ")")
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IIfDocumentType
        Get
            Return DirectCast(Rule, IIfDocumentType)
        End Get
    End Property
End Class
