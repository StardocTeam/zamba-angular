'Imports Zamba.WFBusiness

Public Class UCDoDelete
    Inherits ZRuleControl
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByRef DoDel As IDoDelete, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoDel, _wfPanelCircuit)
        InitializeComponent()
    End Sub

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
    Friend WithEvents GroupBox1 As GroupBox
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend WithEvents rdoBorrar As System.Windows.Forms.RadioButton
    Friend WithEvents chkDeleteFile As System.Windows.Forms.CheckBox
    Friend WithEvents rdoBorrarTodo As System.Windows.Forms.RadioButton

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        GroupBox1 = New GroupBox()
        chkDeleteFile = New System.Windows.Forms.CheckBox()
        rdoBorrarTodo = New System.Windows.Forms.RadioButton()
        rdoBorrar = New System.Windows.Forms.RadioButton()
        btnSeleccionar = New ZButton()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Controls.Add(GroupBox1)
        tbRule.Size = New System.Drawing.Size(360, 339)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(368, 368)
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(chkDeleteFile)
        GroupBox1.Controls.Add(rdoBorrarTodo)
        GroupBox1.Controls.Add(rdoBorrar)
        GroupBox1.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        GroupBox1.Location = New System.Drawing.Point(40, 26)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(288, 202)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Tipo de Borrado"
        '
        'chkDeleteFile
        '
        chkDeleteFile.AutoSize = True
        chkDeleteFile.Font = New Font("Tahoma", 8.25!)
        chkDeleteFile.Location = New System.Drawing.Point(49, 141)
        chkDeleteFile.Name = "chkDeleteFile"
        chkDeleteFile.Size = New System.Drawing.Size(124, 17)
        chkDeleteFile.TabIndex = 15
        chkDeleteFile.Text = "Borrar Archivo Fisico"
        chkDeleteFile.UseVisualStyleBackColor = True
        '
        'rdoBorrarTodo
        '
        rdoBorrarTodo.Font = New Font("Tahoma", 8.25!)
        rdoBorrarTodo.Location = New System.Drawing.Point(24, 103)
        rdoBorrarTodo.Name = "rdoBorrarTodo"
        rdoBorrarTodo.Size = New System.Drawing.Size(168, 32)
        rdoBorrarTodo.TabIndex = 1
        rdoBorrarTodo.Text = "Eliminar Registro de Zamba"
        '
        'rdoBorrar
        '
        rdoBorrar.Checked = True
        rdoBorrar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoBorrar.Location = New System.Drawing.Point(24, 48)
        rdoBorrar.Name = "rdoBorrar"
        rdoBorrar.Size = New System.Drawing.Size(176, 24)
        rdoBorrar.TabIndex = 0
        rdoBorrar.TabStop = True
        rdoBorrar.Text = "Quitar Tarea de WorkFlow"
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSeleccionar.FlatStyle = FlatStyle.Flat
        btnSeleccionar.ForeColor = System.Drawing.Color.White
        btnSeleccionar.Location = New System.Drawing.Point(120, 296)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(112, 28)
        btnSeleccionar.TabIndex = 14
        btnSeleccionar.Text = "Guardar"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'UCDoDelete
        '
        Name = "UCDoDelete"
        Size = New System.Drawing.Size(368, 368)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            If rdoBorrar.Checked Then
                MyRule.TipoBorrado = Borrados.Tarea
            End If
            If rdoBorrarTodo.Checked Then
                MyRule.TipoBorrado = Borrados.Total
            End If

            Dim i As String
            i = MyRule.TipoBorrado
            MyRule.DeleteFile = Not chkDeleteFile.Checked
            WFRulesBusiness.UpdateParamItem(MyRule, 0, i)
            WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.DeleteFile)
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Catch ex As Exception
            Dim exn As New Exception("Error en UCDoDelete.btnSeleccionar_Click(), excepción: " & ex.ToString)
            Zamba.Core.ZClass.raiseerror(exn)
            MessageBox.Show("Excepción al guardar: " & ex.Message, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UCDoDelete_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Select Case MyRule.TipoBorrado
            Case Borrados.Tarea
                rdoBorrar.Checked = True
            Case Borrados.Total
                rdoBorrarTodo.Checked = True
        End Select
        chkDeleteFile.Checked = Not MyRule.DeleteFile
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoDelete
        Get
            Return DirectCast(Rule, IDoDelete)
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
