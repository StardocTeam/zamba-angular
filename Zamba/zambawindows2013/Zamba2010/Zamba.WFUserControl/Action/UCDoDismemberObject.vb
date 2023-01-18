Imports System.IO
Imports System.Reflection

''' <summary>
''' UserControl dela regla DoDismemberObject
''' </summary>
''' <remarks></remarks>
Public Class UCDoDismemberObject
    Inherits ZRuleControl


    Private Function CurrentRule() As IDoDismemberObject
        Return DirectCast(Rule, IDoDismemberObject)
    End Function

#Region "Eventos"
    Private Sub btPath_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btPath.Click
        Try
            Using dialog As New OpenFileDialog()
                dialog.Multiselect = False
                dialog.Title = "Seleccionar .dll"
                dialog.Filter = "Archivos ensamblados|*.dll"

                If (dialog.ShowDialog() = DialogResult.OK) Then
                    tbPath.Text = dialog.FileName
                End If

            End Using
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub tbPath_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles tbPath.TextChanged
        Try
            CurrentRule.AssemblyPath = tbPath.Text

            If (File.Exists(tbPath.Text)) Then
                Dim info As New FileInfo(tbPath.Text)
                If info.Extension.Contains("dll") Then
                    lsbClasses.Items.Clear()
                    Dim dll As Assembly = Assembly.LoadFrom(info.FullName)
                    Dim AllTypes As Type() = dll.GetTypes()
                    Dim Classes As Type() = Array.FindAll(AllTypes, AddressOf IsClass)
                    LoadClasses(AllTypes)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub btSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btSave.Click
        Try
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.AssemblyPath)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.RawZvars)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.ObjectName)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub tbPropertyValue_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            Dim selectedClassName As String = lsbClasses.SelectedItem.ToString()
            Dim selectedPropertyName As String = lsbProperties.SelectedItem.ToString()

            Dim AlreadyExistsInRule = False
            For Each parent As IDoDismemberObject.IZvarVariable In CurrentRule.Zvars
                If String.Compare(selectedClassName, parent.ClassName) = 0 Then
                    If (String.Compare(selectedPropertyName, parent.PropertyName) = 0) Then
                        parent.ZvarName = tbPropertyValue.Text
                        AlreadyExistsInRule = True
                        Exit For
                    End If
                End If
            Next

            If (AlreadyExistsInRule = False) Then
                CurrentRule.AddZvar(selectedClassName, selectedPropertyName, tbPropertyValue.Text)
            End If

            RefreshValues()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UCDoDismemberObject_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            tbPath.Text = CurrentRule.AssemblyPath
            tbObjectName.Text = CurrentRule.ObjectName
            RefreshValues()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Constructores"
    Public Sub New(ByVal rule As IDoDismemberObject, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)

        InitializeComponent()
    End Sub

#End Region

    Private Shared Function IsClass(ByVal type As Type) As Boolean
        If (type.IsInterface) Then
            Return False
        End If

        If (type.IsEnum) Then
            Return False
        End If

        Return type.IsClass
    End Function

    Private Sub LoadClasses(ByVal classes As Type())
        For Each CurrentType As Type In classes
            lsbClasses.Items.Add(CurrentType.FullName)
        Next
    End Sub

    Private Sub LoadProperties(ByVal type As Type)
        lsbProperties.Items.Clear()
        For Each CurrentProperty As PropertyInfo In type.GetProperties()
            lsbProperties.Items.Add(CurrentProperty.Name)
        Next
    End Sub

    Private Class PropertyNode
        Inherits TreeNode

        Private _value As String
        Private _type As String
        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property

        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()

        End Sub
        Public Sub New(ByVal text As String, ByVal value As String, ByVal typeName As String)
            MyBase.New()
            MyBase.Text = text
            _value = value
            _type = typeName
        End Sub
    End Class

    Private Sub lsbClasses_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lsbClasses.SelectedIndexChanged
        Try
            Dim dll As Assembly = Assembly.LoadFrom(tbPath.Text)
            Dim ClassType As Type = dll.GetType(lsbClasses.SelectedItem.ToString())
            LoadProperties(ClassType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub lsbProperties_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lsbProperties.SelectedIndexChanged
        Try
            lbPropertyValue.Visible = True
            lbPropertyType.Visible = True
            tbPropertyValue.Visible = True

            Dim selectedClassName As String = lsbClasses.SelectedItem.ToString()
            Dim selectedPropertyName As String = lsbProperties.SelectedItem.ToString()

            Dim dll As Assembly = Assembly.LoadFrom(tbPath.Text)
            Dim ClassType As Type = dll.GetType(selectedClassName)
            Dim PropertyType As PropertyInfo = ClassType.GetProperty(selectedPropertyName)

            lbPropertyType.Text = PropertyType.PropertyType.Name

            RemoveHandler tbPropertyValue.TextChanged, AddressOf tbPropertyValue_TextChanged
            tbPropertyValue.Text = String.Empty

            For Each parent As IDoDismemberObject.IZvarVariable In CurrentRule.Zvars
                If (String.Compare(parent.ClassName, selectedClassName) = 0) Then
                    If (String.Compare(parent.PropertyName, selectedPropertyName) = 0) Then
                        tbPropertyValue.Text = parent.ZvarName
                        Exit For
                    End If
                End If
            Next

            AddHandler tbPropertyValue.TextChanged, AddressOf tbPropertyValue_TextChanged
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAdd.Click
        Dim frm As FrmTxtInteligente
        Dim PropertyName As String
        Try
            PropertyName = InputBox("Ingrese el nombre de la propiedad.", "Zamba")
            If (IsNothing(PropertyName) OrElse String.IsNullOrEmpty(PropertyName.Trim())) Then
                MessageBox.Show("El nombre de la propiedad no puede estar vacio")
                Exit Sub
            End If

            'Dim PropertyValue As String = InputBox("Ingrese el valor de la propiedad.")
            frm = New FrmTxtInteligente
            frm.ShowDialog()

            'If (frm.ShowDialog = DialogResult.OK) Then
            If frm.DialogResult = DialogResult.OK Then
                lsbValues.Items.Add(PropertyName + " = " + frm.Valor)
                CurrentRule.AddZvar("", PropertyName, frm.txtTextoInteligente.Text)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)

        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            PropertyName = Nothing
        End Try
    End Sub

    Private Sub DeleteVar(ByVal sender As System.Object, ByVal e As EventArgs) Handles btDelete.Click
        Try
            CurrentRule.Zvars.RemoveAt(lsbValues.SelectedIndex)
            If (lsbValues.SelectedIndices.Count = 1) Then
                lsbValues.Items.RemoveAt(lsbValues.SelectedIndex)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub lsbValues_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lsbValues.SelectedIndexChanged
        Try
            If (lsbValues.SelectedIndices.Count = 0) Then
                btDelete.Enabled = False
                btnEdit.Enabled = False
            ElseIf lsbValues.SelectedIndices.Count > 1 Then
                btnEdit.Enabled = False
                btDelete.Enabled = True
            Else
                btnEdit.Enabled = True
                btDelete.Enabled = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub RefreshValues()
        lsbValues.Items.Clear()

        For Each parent As IDoDismemberObject.IZvarVariable In CurrentRule.Zvars
            lsbValues.Items.Add(parent.PropertyName + " = " + parent.ZvarName)
        Next
    End Sub

    Private Sub btDeleteAll_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btDeleteAll.Click
        Try
            If MessageBox.Show("¿Desea borrar la lista de variables?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                CurrentRule.Zvars.Clear()
                lsbValues.Items.Clear()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub tbObjectName_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles tbObjectName.TextChanged
        CurrentRule.ObjectName = tbObjectName.Text
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEdit.Click
        Dim frm As FrmTxtInteligente
        Dim values() As String
        Try
            'Obtiene los valores seleccionados
            values = lsbValues.SelectedItem.ToString.Split(New String() {" = "}, StringSplitOptions.RemoveEmptyEntries)

            'Muestra el formulario configurado
            frm = New FrmTxtInteligente
            frm.lblTextoInteligente.Text = "Nombre de la propiedad"
            If Not IsNothing(values(0)) Then
                frm.txtTextoInteligente.Text = values(0).Trim
            End If
            frm.ShowDialog()

            'Edita la propiedad seleccionada
            If frm.DialogResult = DialogResult.OK Then
                CurrentRule.Zvars.Item(lsbValues.SelectedIndex).PropertyName = frm.txtTextoInteligente.Text
                values(0) = frm.txtTextoInteligente.Text
            End If
            frm.Close()

            'Muestra el formulario configurado
            frm = New FrmTxtInteligente
            frm.lblTextoInteligente.Text = "Valor de la propiedad " & values(0).Trim
            If Not IsNothing(values(1)) Then
                frm.txtTextoInteligente.Text = values(1).Trim
            End If
            frm.ShowDialog()

            'Edita la propiedad seleccionada
            If frm.DialogResult = DialogResult.OK Then
                CurrentRule.Zvars.Item(lsbValues.SelectedIndex).ZvarName = frm.txtTextoInteligente.Text
                values(1) = frm.txtTextoInteligente.Text
            End If

            lsbValues.Items.Item(lsbValues.SelectedIndex) = values(0).Trim + " = " + values(1).Trim
            frm.Close()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            values = Nothing
        End Try
    End Sub
End Class