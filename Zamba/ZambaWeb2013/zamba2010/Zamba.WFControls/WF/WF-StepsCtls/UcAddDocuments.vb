Imports zamba.data
Public Class UcAddDocuments
    Inherits Zamba.AppBlock.ZControl

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
    Friend WithEvents LBDocTypes As ListBox
    Friend WithEvents BtnAdd As ZButton
    Friend WithEvents BtnRemove As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UcAddDocuments))
        LBDocTypes = New ListBox
        BtnAdd = New ZButton
        BtnRemove = New ZButton
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'LBDocTypes
        '
        LBDocTypes.BackColor = System.Drawing.Color.White
        LBDocTypes.Location = New System.Drawing.Point(32, 32)
        LBDocTypes.Name = "LBDocTypes"
        LBDocTypes.Size = New System.Drawing.Size(232, 342)
        LBDocTypes.TabIndex = 0
        '
        'BtnAdd
        '
        BtnAdd.DialogResult = System.Windows.Forms.DialogResult.None
        BtnAdd.Location = New System.Drawing.Point(280, 72)
        BtnAdd.Name = "BtnAdd"
        BtnAdd.Size = New System.Drawing.Size(256, 24)
        BtnAdd.TabIndex = 1
        BtnAdd.Text = "Agregar Documento al Workflow"
        '
        'BtnRemove
        '
        BtnRemove.DialogResult = System.Windows.Forms.DialogResult.None
        BtnRemove.Location = New System.Drawing.Point(280, 112)
        BtnRemove.Name = "BtnRemove"
        BtnRemove.Size = New System.Drawing.Size(256, 23)
        BtnRemove.TabIndex = 2
        BtnRemove.Text = "Quitar Documento del Workflow"
        '
        'UcAddDocuments
        '
        Controls.Add(BtnRemove)
        Controls.Add(BtnAdd)
        Controls.Add(LBDocTypes)
        Name = "UcAddDocuments"
        Size = New System.Drawing.Size(608, 472)
        ResumeLayout(False)

    End Sub

#End Region
    Dim Workflowid As Int32

#Region "New"
    Public Sub New(ByVal WF As Int32)
        MyBase.New()
        InitializeComponent()
        Workflowid = WF
    End Sub

#End Region
    'TODO HERNAN: terminame mañana.

    Private Sub UcAddDocuments_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            LBDocTypes.DataSource = DocTypesFactory.GetDocTypesDsDocType.Tables(0)
            LBDocTypes.DisplayMember = "DOC_TYPE_NAME"
            LBDocTypes.ValueMember = "DOC_TYPE_ID"

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnAdd.Click
        AsignarAWorkflow()
    End Sub
    Private Sub AsignarAWorkflow()
        Try
            If Zamba.Core.Results_Business.IsDocTypeInWF(CInt(LBDocTypes.SelectedValue)) = False Then
                If MessageBox.Show("Se agregará " & Me.LBDocTypes.Text.Trim & " a la etapa de Workflow seleccionada. ¿Desea continuar?", "Zamba WorkFlow", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Zamba.Core.Results_Business.AddDocTypeToWF(CInt(LBDocTypes.SelectedValue), Workflowid)
                    MessageBox.Show("Documento Asignado")
                End If
            Else
                If MessageBox.Show("Este documento ya se encuentra asignado a un Workflow. ¿Desea reemplazarlo?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Zamba.Core.Results_Business.AddDocTypeToWF(CInt(LBDocTypes.SelectedValue), Workflowid)
                    MessageBox.Show("Documento Asignado")
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnRemove.Click
        Try
            Remove()
            MessageBox.Show("Relación eliminada")
        Catch ex As Exception
            MessageBox.Show("No se pudo remover la relacion la relación del documento con el Workflow")
        End Try
    End Sub
    Private Sub Remove()
        Zamba.Core.Results_Business.RemoveDocTypeWF(CInt(LBDocTypes.SelectedValue))
    End Sub
End Class
