Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Web.Services.Description
Imports System.Xml.Serialization
Imports Microsoft.CSharp
Imports System.ComponentModel


Public Class UCDoConsumeWebService
    Inherits ZRuleControl

#Region "Atributos"

    Friend WithEvents txtSaveInValue As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents Label7 As ZLabel
    Friend WithEvents Label9 As ZLabel
    Friend WithEvents lblDataType As ZLabel
    Friend WithEvents txtValor As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents Label6 As ZLabel
    Private WithEvents tbWSDL As TextBox
    Private WithEvents messageTextBox As TextBox
    Private WithEvents progressBar1 As System.Windows.Forms.ProgressBar
    Private WithEvents tabPage2 As System.Windows.Forms.TabPage
    Private WithEvents tvWsdl As System.Windows.Forms.TreeView
    Private WithEvents tvParameters As System.Windows.Forms.TreeView
    Private WithEvents lbParameters As ZLabel
    Private WithEvents lbReturn As ZLabel
    Private WithEvents btInvoke As ZButton
    Private WithEvents label5 As ZLabel
    Private WithEvents tabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btSave As ZButton
    Private WithEvents tabControl1 As System.Windows.Forms.TabControl

    Private _methods As List(Of Method) = Nothing
    Private _methodInfo As MethodInfo()
    Private _param As ParameterInfo()
    Private _service As Type
    Private _paramTypes As Type()
    Friend WithEvents tbResponse As TextBox
    Private _methodName As String = [String].Empty
    Private firstSelectionFlag As Boolean = True
    Friend WithEvents chkCredentials As System.Windows.Forms.CheckBox
    Private WithEvents btnLoadWebService As ZButton
    Private WithEvents Label3 As ZLabel
    Private WithEvents TVAsignedParams As System.Windows.Forms.TreeView
    Friend WithEvents lblmethodselected As ZLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private Const _emptyString As String = "          "
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Devuelve el metodo seleccionado en el formulario
    ''' </summary>
    ''' <returns></returns>
    Private Function SelectedMethod() As Method

        If tvWsdl.SelectedNode Is Nothing Then
            Return Nothing
        End If

        Return _methods(tvWsdl.SelectedNode.Index)
    End Function
    ''' <summary>
    ''' Devuelve el paramatro seleccionado en el formulario
    ''' </summary>
    ''' <returns></returns>
    Private Function SelectedParameter() As Parameter

        If SelectedMethod() Is Nothing Then
            Return Nothing
        End If

        If Not IsNothing(tvParameters.SelectedNode) AndAlso SelectedMethod().Paramaters.Count > 0 Then
            Return SelectedMethod().Paramaters(tvParameters.SelectedNode.Index)
        Else
            Return Nothing
        End If
    End Function

    Private Sub UpdateParameterNode(ByVal value As String)
        If (String.IsNullOrEmpty(value)) Then
            tvParameters.SelectedNode.Text = "(" + SelectedParameter.TypeName + ") " + SelectedParameter.Name
        Else
            tvParameters.SelectedNode.Text = "(" + SelectedParameter.TypeName + ") " + SelectedParameter.Name + " " + value
        End If
    End Sub


    ''' <summary>
    '''         
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentRule() As IDoConsumeWebService
        Get
            Return DirectCast(MyBase.Rule, IDoConsumeWebService)
        End Get
        Set(ByVal value As IDoConsumeWebService)
            MyBase.Rule = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Si modifico el textbox, cargo los datos del webservice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbWSDL_TextChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles tbWSDL.TextChanged

        If (tbWSDL.Text.ToUpper().EndsWith("WSDL")) Then
            tvWsdl.Enabled = True
            tvParameters.Enabled = True
            tbResponse.Enabled = True
            tvWsdl.Nodes.Clear()
            tvParameters.Nodes.Clear()
            txtValor.Text = String.Empty
            lblDataType.Text = String.Empty
            messageTextBox.Text = [String].Empty
            tbResponse.Text = String.Empty

            'Me.tabControl1.SelectedTab = Me.tabPage2
            'DynamicInvocation(tbWSDL.Text)

        Else
            tvWsdl.Enabled = False
            tvParameters.Enabled = False
            tbResponse.Enabled = False
        End If
        If tbWSDL.Text.Count > 0 Then
            HasBeenModified = True
        Else
            HasBeenModified = False
        End If

    End Sub


    Private Sub tvParameters_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvParameters.AfterSelect
        If (Not IsNothing(SelectedParameter)) Then
            txtValor.Text = SelectedParameter.Value.ToString()
            tvParameters.SelectedNode.Name = SelectedParameter.TypeName + "  " + SelectedParameter.Name + " = " + SelectedParameter.Value

            For Each Method As MethodInfo In _methodInfo
                If (String.Compare(Method.Name, SelectedMethod.Name) = 0) Then
                    lbReturn.Text = "Devuelve " + Method.ReturnType.Name

                    Exit For
                End If
            Next
        End If
        HasBeenModified = True
    End Sub
    Private Sub tvWsdl_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvWsdl.AfterSelect
        tvParameters.Nodes.Clear()
        txtValor.Text = String.Empty
        lblDataType.Text = String.Empty
        tbResponse.Text = String.Empty

        Dim csvParamTypes As String = String.Empty
        Dim CurrentMethod As Method = _methods(e.Node.Index)

        For Each CurrentParameter As Parameter In CurrentMethod.Paramaters
            If Not IsNothing(CurrentParameter.Value) Then
                tvParameters.Nodes.Add("(" + CurrentParameter.TypeName + ") " + CurrentParameter.Name + " " + CurrentParameter.Value)
            Else
                tvParameters.Nodes.Add("(" + CurrentParameter.TypeName + ") " + CurrentParameter.Name)
            End If
            csvParamTypes += DirectCast(CurrentParameter, Parameter).TypeName
            csvParamTypes += ";"
        Next

        If csvParamTypes.Length > 0 Then
            csvParamTypes = csvParamTypes.Remove(csvParamTypes.Length - 1, 1)
        End If

        If tvParameters.Nodes.Count > 0 Then
            tvParameters.SelectedNode = tvParameters.Nodes(0)
        End If

        If Not tvWsdl.SelectedNode.Text.Contains(_emptyString) AndAlso firstSelectionFlag Then
            tvWsdl.SelectedNode.Text = tvWsdl.SelectedNode.Text + _emptyString
            firstSelectionFlag = False
        Else
            tvWsdl.SelectedNode.Text = tvWsdl.SelectedNode.Text
        End If
        If CurrentRule.ParamTypes = CurrentRule.SaveInValue And csvParamTypes = CurrentRule.SaveInValue Then
            tvWsdl.SelectedNode.NodeFont = New Font(tvWsdl.Font, FontStyle.Bold)
        End If
        tvParameters.ExpandAll()
        ValidateParameters()
        If tvWsdl.Text.Count > 0 Then
            HasBeenModified = True
        Else
            HasBeenModified = False
        End If
    End Sub

    Private Sub btInvoke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btInvoke.Click
        Try
            tbResponse.Text = String.Empty
            Dim ServiceMethod As MethodInfo = Nothing
            For Each method As MethodInfo In _service.GetMethods()
                If method.Name = SelectedMethod.Name Then
                    ServiceMethod = method
                    Exit For
                End If
            Next
            _service.GetMethods()
            Dim param1 As Object() = New Object(SelectedMethod.Paramaters.Count - 1) {}
            Dim p As ParameterInfo() = ServiceMethod.GetParameters()
            Try
                For i As Integer = 0 To SelectedMethod.Paramaters.Count - 1
                    If String.Compare(SelectedMethod.Paramaters(i).Value.ToString().ToUpper(), "NOTHING") = 0 Then
                        param1(i) = Nothing
                    Else

                        Try
                            param1(i) = Convert.ChangeType(SelectedMethod.Paramaters(i).Value, DirectCast(p.GetValue(i), ParameterInfo).ParameterType)
                            'If (p.GetValue(i).GetType.Name = "Automail") Then
                            'param1(i) =      p.GetValue(i).GetType().GetConstructors(
                            'End If
                        Catch ex As Exception
                            param1(i) = SelectedMethod.Paramaters(i).Value
                        End Try
                    End If
                Next

                Dim obj As Object = Nothing
                Dim response As Object = Nothing
                For Each t As MethodInfo In _methodInfo
                    If String.Compare(t.Name, SelectedMethod.Name) = 0 Then
                        'Invoke Method
                        obj = Activator.CreateInstance(_service)
                        Dim propInfo As PropertyInfo = _service.GetProperty("Credentials")
                        propInfo.SetValue(obj, System.Net.CredentialCache.DefaultCredentials, Nothing)
                        response = t.Invoke(obj, param1)
                        tbResponse.Text = String.Empty

                        If (IsNothing(response)) Then
                            tbResponse.Text = "El servicio web devolvio un resultado nulo."
                        Else
                            tbResponse.Text = "Resultado = " + response.ToString()
                        End If
                        Exit For
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub PgMain_Validated(ByVal sender As Object, ByVal e As EventArgs)
        For i As Integer = 0 To _param.Length - 1
            tvParameters.Nodes(0).Nodes(i).Text = _param(i).ParameterType.Name + "  " + _param(i).Name + " = " + SelectedMethod.Paramaters(i).Value
            SelectedParameter.Value = txtValor.Text
        Next
    End Sub
    Private Sub btGetWsdl_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "ARchivos WSDL|*.wsdl|Todos los archivos|*.*"

        If ofd.ShowDialog() = DialogResult.OK Then
            tbWSDL.Text = ofd.FileName
        End If
    End Sub

    Private Sub btSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btSave.Click
        Try

            'ByVal methodName As String, ByVal wsdl As String, ByVal params As ArrayList
            If SelectedMethod() Is Nothing Then
                MessageBox.Show("Debe seleccionar un metodo del Web Service antes de guardar", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If tbWSDL.Text = String.Empty Then
                MessageBox.Show("Debe ingresar la URL del Web Service antes de guardar", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 0, SelectedMethod.Name, ObjectTypes.PreferenciasDeUsuario)
            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 1, tbWSDL.Text, ObjectTypes.PreferenciasDeUsuario)

            Dim csv As String = String.Empty
            Dim csNT As String = String.Empty
            Dim csvParamTypes As String = String.Empty

            CurrentRule.MethodName = SelectedMethod.Name
            CurrentRule.Param.Clear()
            CurrentRule.ParamNT.Clear()
            'Guardo los parametros 



            For Each values As Object In SelectedMethod.Paramaters
                If Not IsNothing(DirectCast(values, Parameter).Value) Then
                    CurrentRule.Param.Add(DirectCast(values, Parameter).Value)
                    CurrentRule.ParamNT.Add(DirectCast(values, Parameter).Name & "(" & DirectCast(values, Parameter).TypeName & ")")
                    csv += DirectCast(values, Parameter).Value
                    csv += ";"
                    csNT += DirectCast(values, Parameter).Name & "(" & DirectCast(values, Parameter).TypeName & ")"
                    csNT += ";"
                End If
                If Not IsNothing(DirectCast(values, Parameter).TypeName) Then
                    csvParamTypes += DirectCast(values, Parameter).TypeName
                    csvParamTypes += ";"
                End If
            Next

            If IsNothing(csv) Then
                csv = String.Empty
            End If
            If csv.Length > 0 Then
                csv = csv.Remove(csv.Length - 1, 1)
            End If
            If IsNothing(csNT) Then
                csNT = String.Empty
            End If
            If csNT.Length > 0 Then
                csNT = csNT.Remove(csNT.Length - 1, 1)
            End If

            If IsNothing(csvParamTypes) Then
                csvParamTypes = String.Empty
            End If
            If csvParamTypes.Length > 0 Then
                csvParamTypes = csvParamTypes.Remove(csvParamTypes.Length - 1, 1)
            End If

            For i As Integer = 0 To tvWsdl.Nodes.Count - 1
                tvWsdl.Nodes(i).NodeFont = New Font(tvWsdl.Font, FontStyle.Regular)
            Next



            'Guardo los valores
            CurrentRule.ParamTypes = csvParamTypes
            CurrentRule.MethodName = SelectedMethod.Name
            CurrentRule.Wsdl = tbWSDL.Text
            CurrentRule.SaveInValue = txtSaveInValue.Text
            CurrentRule.useCredentials = chkCredentials.Checked

            Dim emptyString As String = "          "
            If Not tvWsdl.SelectedNode.Text.Contains(emptyString) Then
                tvWsdl.SelectedNode.Text = tvWsdl.SelectedNode.Text + emptyString
            Else
                tvWsdl.SelectedNode.Text = tvWsdl.SelectedNode.Text
            End If

            tvWsdl.SelectedNode.NodeFont = New Font(tvWsdl.Font, FontStyle.Bold)

            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 2, csv, ObjectTypes.PreferenciasDeUsuario)
            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 6, csNT, ObjectTypes.PreferenciasDeUsuario)

            'Se comento codigo que no tenia en cuenta reglas ya creadas y se salvo el error introducido por ese codigo
            'ingresando 2 veces el nombre de la variable donde se guarda el resultado
            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 3, txtSaveInValue.Text, ObjectTypes.PreferenciasDeUsuario)
            'WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 4, csvParamTypes, ObjectTypes.PreferenciasDeUsuario)
            'WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 3, csvParamTypes, ObjectTypes.PreferenciasDeUsuario)
            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 4, txtSaveInValue.Text, ObjectTypes.PreferenciasDeUsuario)
            WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 5, chkCredentials.Checked, ObjectTypes.PreferenciasDeUsuario)
            UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
            HasBeenModified = False
        Catch ex As Exception
            MessageBox.Show("Error al guardar configuracion")
            ZClass.raiseerror(ex)
        End Try

    End Sub


    Private Sub tabPage1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tabPage1.Click
    End Sub
    Private Sub UCDoConsumeWebService_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        tbWSDL.Text = CurrentRule.Wsdl
        lblmethodselected.Text = "Metodo seleccionado: " & CurrentRule.MethodName

        txtSaveInValue.Text = CurrentRule.SaveInValue

        If (tvWsdl.Nodes.Count > 0) Then
            For Each CurrentNode As TreeNode In tvWsdl.Nodes(0).Nodes
                If (String.Compare(CurrentNode.Text, CurrentRule.MethodName) = 0) Then
                    tvWsdl.SelectedNode = CurrentNode
                    Exit For
                End If
            Next
        End If

        chkCredentials.Checked = CurrentRule.useCredentials

        If (tvParameters.Nodes.Count > 0) Then
            Dim i As Int32 = 0
            For Each CurrentNode As TreeNode In tvParameters.Nodes(0).Nodes
                CurrentNode.Text += " = " + CurrentRule.Param(i).ToString()
                i = i + 1
            Next
        End If

        Dim count As Int32 = 0
        For Each AsignedParam As String In CurrentRule.Param
            If CurrentRule.ParamNT IsNot Nothing AndAlso CurrentRule.ParamNT.Count = CurrentRule.Param.Count Then
                TVAsignedParams.Nodes.Add(CurrentRule.ParamNT(count).ToString() & " = " & AsignedParam)
            Else
                TVAsignedParams.Nodes.Add(" = " + AsignedParam)
            End If
            count += +1
        Next
        HasBeenModified = False

    End Sub

    Private Sub txtValor_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtValor.LostFocus
        UpdateParameterNode(txtValor.Text)
    End Sub
#End Region

    ''' <summary>
    ''' Invoca el servicio
    ''' </summary>
    ''' <param name="wsdl"></param>
    ''' <remarks></remarks>
    Private Sub DynamicInvocation(ByVal wsdl As String)

        _methods = New List(Of Method)()

        Try
            messageTextBox.Text += "Generando WSDL " & Chr(13) & "" & Chr(10) & ""
            progressBar1.PerformStep()

            Dim uri As New Uri(wsdl)

            progressBar1.PerformStep()

            Dim webRequest As WebRequest = WebRequest.Create(uri)

            If chkCredentials.Checked = True Then
                messageTextBox.Text += "Utilizando credenciales  " & Chr(13) & "" & Chr(10) & ""
                webRequest.PreAuthenticate = True
                webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials
            End If

            Dim requestStream As Stream = webRequest.GetResponse().GetResponseStream()
            ' Get a WSDL file describing a service
            Dim sd As ServiceDescription = ServiceDescription.Read(requestStream)
            Dim sdName As String = sd.Services(0).Name

            messageTextBox.Text += "Generando Proxy " & Chr(13) & "" & Chr(10) & ""
            progressBar1.PerformStep()

            ' Initialize a service description servImport
            Dim servImport As New ServiceDescriptionImporter()
            servImport.AddServiceDescription(sd, [String].Empty, [String].Empty)
            servImport.ProtocolName = "Soap"
            servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties

            messageTextBox.Text += "Generando Assembly " & Chr(13) & "" & Chr(10) & ""
            progressBar1.PerformStep()

            Dim [nameSpace] As New CodeNamespace()
            Dim codeCompileUnit As New CodeCompileUnit()
            codeCompileUnit.Namespaces.Add([nameSpace])
            ' Set Warnings
            Dim warnings As ServiceDescriptionImportWarnings = servImport.Import([nameSpace], codeCompileUnit)

            If warnings = 0 Then
                Dim stringWriter As New IO.StringWriter(CultureInfo.CurrentCulture)
                Dim prov As New CSharpCodeProvider()
                prov.GenerateCodeFromNamespace([nameSpace], stringWriter, New CodeGeneratorOptions())

                messageTextBox.Text += "Compilando Assembly " & Chr(13) & "" & Chr(10) & ""
                progressBar1.PerformStep()

                ' Compile the assembly with the appropriate references
                Dim assemblyReferences As String() = New String(3) {"System.Web.Services.dll", "System.Xml.dll", "System.Data.dll", "mscorlib.dll"}
                Dim param As New CompilerParameters(assemblyReferences)
                param.GenerateExecutable = False
                param.GenerateInMemory = True
                param.TreatWarningsAsErrors = False
                param.WarningLevel = 4

                Dim a As New TempFileCollection()
                Dim results As New CompilerResults(New TempFileCollection())
                results = prov.CompileAssemblyFromDom(param, codeCompileUnit)
                Dim assembly As Assembly = results.CompiledAssembly
                _service = assembly.GetType(sdName)

                messageTextBox.Text += "Obteniendo metodos del Wsdl " & Chr(13) & "" & Chr(10) & ""
                progressBar1.PerformStep()

                _methodInfo = _service.GetMethods()

                Dim CurrentMethod As Method = Nothing
                Dim csvParamTypes As String

                For Each method As MethodInfo In _methodInfo
                    If String.Compare(method.Name, "Discover") = 0 Then
                        Exit For
                    End If

                    CurrentMethod = New Method(method.Name)

                    csvParamTypes = String.Empty

                    If (String.Compare(method.Name, CurrentRule.MethodName) = 0) Then
                        Dim i = 0
                        Dim CurrentParameter As Parameter

                        For Each info As ParameterInfo In method.GetParameters()
                            csvParamTypes += info.ParameterType.Name + ";"
                        Next

                        If csvParamTypes.Length > 0 Then
                            csvParamTypes = csvParamTypes.Remove(csvParamTypes.Length - 1, 1)
                        End If

                        For Each info As ParameterInfo In method.GetParameters()

                            CurrentParameter = New Parameter(info.Name, info.ParameterType.Name)

                            If (i <= CurrentRule.Param.Count - 1) Then
                                CurrentParameter.Value = CurrentRule.Param(i)
                            End If

                            CurrentMethod.Paramaters.Add(CurrentParameter)
                            i += 1
                        Next
                    Else
                        For Each info As ParameterInfo In method.GetParameters()
                            CurrentMethod.Paramaters.Add(New Parameter(info.Name, info.ParameterType.Name))
                        Next
                        For Each info As ParameterInfo In method.GetParameters()
                            csvParamTypes += info.ParameterType.Name + ";"
                        Next

                        If csvParamTypes.Length > 0 Then
                            csvParamTypes = csvParamTypes.Remove(csvParamTypes.Length - 1)
                        End If
                    End If


                    'Cargo los valores de la regla en el metodo del wsdl que corresponda, si existe
                    If (String.Compare(CurrentRule.MethodName, CurrentMethod.Name) = 0) Then
                        Dim CurrentParameter As Parameter = Nothing

                        If (CurrentMethod.Paramaters.Count = CurrentRule.Param.Count) Then
                            For index As Integer = 0 To CurrentRule.Param.Count - 1
                                CurrentMethod.Paramaters(index).Value = CurrentMethod.Paramaters(index).Value
                            Next
                        Else
                            'La cantidad de parametros del metodo de la reglaguardados
                        End If
                    End If
                    _methods.Add(CurrentMethod)

                    tvWsdl.Nodes.Add(method.Name)

                    If method.Name.Trim().ToLower() = CurrentRule.MethodName.Trim().ToLower() Then
                        tvWsdl.Nodes(tvWsdl.Nodes.Count - 1).NodeFont = New Font(tvWsdl.Font, FontStyle.Bold)
                    Else
                        tvWsdl.Nodes(tvWsdl.Nodes.Count - 1).NodeFont = New Font(tvWsdl.Font, FontStyle.Regular)
                    End If

                Next


                'For Each node As TreeNode In tvWsdl.Nodes(0).Nodes
                '    If (String.Compare(node.Text, CurrentRule.MethodName) = 0) Then
                '        tvWsdl.SelectedNode = node
                '        Exit For
                '    End If
                'Next

                messageTextBox.Text += "Finalizado " & Chr(13) & "" & Chr(10) & " "
                progressBar1.PerformStep()

                'Me.tabControl1.SelectedTab = Me.tabPage1
            Else
                messageTextBox.Text += warnings
            End If
            tabControl1.SelectedTab = tabPage1
        Catch ex As Exception
            tabControl1.SelectedTab = tabPage2
            messageTextBox.Text += "" & Chr(13) & "" & Chr(10) & "" + ex.Message + "" & Chr(13) & "" & Chr(10) & "" & Chr(13) & "" & Chr(10) & "" + ex.ToString()


            progressBar1.Value = 70
        End Try

        ValidateMethods()
        ValidateParameters()
    End Sub

    Protected Overloads Sub InitializeComponent()
        tabControl1 = New System.Windows.Forms.TabControl()
        tabPage1 = New System.Windows.Forms.TabPage()
        Label3 = New ZLabel()
        TVAsignedParams = New System.Windows.Forms.TreeView()
        lblmethodselected = New ZLabel()
        chkCredentials = New System.Windows.Forms.CheckBox()
        tbResponse = New TextBox()
        txtValor = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label9 = New ZLabel()
        lblDataType = New ZLabel()
        Label7 = New ZLabel()
        txtSaveInValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        btSave = New ZButton()
        label5 = New ZLabel()
        lbReturn = New ZLabel()
        lbParameters = New ZLabel()
        tvParameters = New System.Windows.Forms.TreeView()
        tvWsdl = New System.Windows.Forms.TreeView()
        tabPage2 = New System.Windows.Forms.TabPage()
        progressBar1 = New System.Windows.Forms.ProgressBar()
        messageTextBox = New TextBox()
        btInvoke = New ZButton()
        tbWSDL = New TextBox()
        Label6 = New ZLabel()
        btnLoadWebService = New ZButton()
        Panel1 = New System.Windows.Forms.Panel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        tabControl1.SuspendLayout()
        tabPage1.SuspendLayout()
        tabPage2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(tabControl1)
        tbRule.Controls.Add(Panel1)
        tbRule.Size = New System.Drawing.Size(838, 507)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(846, 536)
        '
        'tabControl1
        '
        tabControl1.Controls.Add(tabPage1)
        tabControl1.Controls.Add(tabPage2)
        tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        tabControl1.Location = New System.Drawing.Point(3, 72)
        tabControl1.Name = "tabControl1"
        tabControl1.SelectedIndex = 0
        tabControl1.Size = New System.Drawing.Size(832, 432)
        tabControl1.TabIndex = 16
        '
        'tabPage1
        '
        tabPage1.AutoScroll = True
        tabPage1.Controls.Add(Label3)
        tabPage1.Controls.Add(TVAsignedParams)
        tabPage1.Controls.Add(lblmethodselected)
        tabPage1.Controls.Add(chkCredentials)
        tabPage1.Controls.Add(tbResponse)
        tabPage1.Controls.Add(txtValor)
        tabPage1.Controls.Add(Label9)
        tabPage1.Controls.Add(lblDataType)
        tabPage1.Controls.Add(Label7)
        tabPage1.Controls.Add(txtSaveInValue)
        tabPage1.Controls.Add(btSave)
        tabPage1.Controls.Add(label5)
        tabPage1.Controls.Add(lbReturn)
        tabPage1.Controls.Add(lbParameters)
        tabPage1.Controls.Add(tvParameters)
        tabPage1.Controls.Add(tvWsdl)
        tabPage1.Location = New System.Drawing.Point(4, 25)
        tabPage1.Name = "tabPage1"
        tabPage1.Padding = New System.Windows.Forms.Padding(3)
        tabPage1.Size = New System.Drawing.Size(824, 403)
        tabPage1.TabIndex = 0
        tabPage1.Text = "Invocar"
        tabPage1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!)
        Label3.FontSize = 9.75!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(619, 12)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(189, 16)
        Label3.TabIndex = 45
        Label3.Text = "Asignacion de Parametros :"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'TVAsignedParams
        '
        TVAsignedParams.BackColor = System.Drawing.Color.White
        TVAsignedParams.Enabled = False
        TVAsignedParams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        TVAsignedParams.Location = New System.Drawing.Point(601, 31)
        TVAsignedParams.Name = "TVAsignedParams"
        TVAsignedParams.Size = New System.Drawing.Size(207, 287)
        TVAsignedParams.TabIndex = 44
        '
        'lblmethodselected
        '
        lblmethodselected.AutoSize = True
        lblmethodselected.BackColor = System.Drawing.Color.Transparent
        lblmethodselected.Font = New Font("Verdana", 9.75!)
        lblmethodselected.FontSize = 9.75!
        lblmethodselected.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblmethodselected.Location = New System.Drawing.Point(11, 178)
        lblmethodselected.Name = "lblmethodselected"
        lblmethodselected.Size = New System.Drawing.Size(63, 16)
        lblmethodselected.TabIndex = 43
        lblmethodselected.Text = "Metodo:"
        lblmethodselected.TextAlign = ContentAlignment.MiddleLeft
        '
        'chkCredentials
        '
        chkCredentials.AutoSize = True
        chkCredentials.Location = New System.Drawing.Point(9, 298)
        chkCredentials.Name = "chkCredentials"
        chkCredentials.Size = New System.Drawing.Size(236, 20)
        chkCredentials.TabIndex = 42
        chkCredentials.Text = "Utilizar Credenciales del Usuario"
        chkCredentials.UseVisualStyleBackColor = True
        '
        'tbResponse
        '
        tbResponse.Location = New System.Drawing.Point(6, 222)
        tbResponse.Multiline = True
        tbResponse.Name = "tbResponse"
        tbResponse.Size = New System.Drawing.Size(580, 68)
        tbResponse.TabIndex = 41
        '
        'txtValor
        '
        txtValor.Enabled = False
        txtValor.Location = New System.Drawing.Point(404, 175)
        txtValor.Multiline = False
        txtValor.Name = "txtValor"
        txtValor.Size = New System.Drawing.Size(182, 23)
        txtValor.TabIndex = 40
        txtValor.Text = ""
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.BackColor = System.Drawing.Color.Transparent
        Label9.Font = New Font("Verdana", 9.75!)
        Label9.FontSize = 9.75!
        Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label9.Location = New System.Drawing.Point(358, 178)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(40, 16)
        Label9.TabIndex = 39
        Label9.Text = "Valor"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblDataType
        '
        lblDataType.AutoSize = True
        lblDataType.BackColor = System.Drawing.Color.Transparent
        lblDataType.Font = New Font("Verdana", 9.75!)
        lblDataType.FontSize = 9.75!
        lblDataType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblDataType.Location = New System.Drawing.Point(479, 60)
        lblDataType.Name = "lblDataType"
        lblDataType.Size = New System.Drawing.Size(0, 16)
        lblDataType.TabIndex = 38
        lblDataType.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.BackColor = System.Drawing.Color.Transparent
        Label7.Font = New Font("Verdana", 9.75!)
        Label7.FontSize = 9.75!
        Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label7.Location = New System.Drawing.Point(6, 332)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(179, 16)
        Label7.TabIndex = 36
        Label7.Text = "Guardar valor en variable:"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSaveInValue
        '
        txtSaveInValue.Location = New System.Drawing.Point(210, 327)
        txtSaveInValue.MaxLength = 4000
        txtSaveInValue.Name = "txtSaveInValue"
        txtSaveInValue.Size = New System.Drawing.Size(260, 21)
        txtSaveInValue.TabIndex = 35
        txtSaveInValue.Text = ""
        '
        'btSave
        '
        btSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btSave.FlatStyle = FlatStyle.Flat
        btSave.ForeColor = System.Drawing.Color.White
        btSave.Location = New System.Drawing.Point(640, 355)
        btSave.Name = "btSave"
        btSave.Size = New System.Drawing.Size(125, 28)
        btSave.TabIndex = 34
        btSave.Text = "Guardar configuración"
        btSave.UseVisualStyleBackColor = True
        '
        'label5
        '
        label5.AutoSize = True
        label5.BackColor = System.Drawing.Color.Transparent
        label5.Font = New Font("Verdana", 9.75!)
        label5.FontSize = 9.75!
        label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        label5.Location = New System.Drawing.Point(9, 12)
        label5.Name = "label5"
        label5.Size = New System.Drawing.Size(75, 16)
        label5.TabIndex = 32
        label5.Text = "Metodos :"
        label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'lbReturn
        '
        lbReturn.AutoSize = True
        lbReturn.BackColor = System.Drawing.Color.Transparent
        lbReturn.Font = New Font("Verdana", 9.75!)
        lbReturn.FontSize = 9.75!
        lbReturn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lbReturn.Location = New System.Drawing.Point(6, 206)
        lbReturn.Name = "lbReturn"
        lbReturn.Size = New System.Drawing.Size(68, 16)
        lbReturn.TabIndex = 29
        lbReturn.Text = "Devuelve"
        lbReturn.TextAlign = ContentAlignment.MiddleLeft
        '
        'lbParameters
        '
        lbParameters.AutoSize = True
        lbParameters.BackColor = System.Drawing.Color.Transparent
        lbParameters.Font = New Font("Verdana", 9.75!)
        lbParameters.FontSize = 9.75!
        lbParameters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lbParameters.Location = New System.Drawing.Point(345, 12)
        lbParameters.Name = "lbParameters"
        lbParameters.Size = New System.Drawing.Size(93, 16)
        lbParameters.TabIndex = 28
        lbParameters.Text = "Parametros :"
        lbParameters.TextAlign = ContentAlignment.MiddleLeft
        '
        'tvParameters
        '
        tvParameters.Enabled = False
        tvParameters.Location = New System.Drawing.Point(348, 29)
        tvParameters.Name = "tvParameters"
        tvParameters.Size = New System.Drawing.Size(238, 140)
        tvParameters.TabIndex = 27
        '
        'tvWsdl
        '
        tvWsdl.Enabled = False
        tvWsdl.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        tvWsdl.Indent = 19
        tvWsdl.ItemHeight = 16
        tvWsdl.Location = New System.Drawing.Point(11, 29)
        tvWsdl.Margin = New System.Windows.Forms.Padding(1)
        tvWsdl.Name = "tvWsdl"
        tvWsdl.Size = New System.Drawing.Size(333, 140)
        tvWsdl.TabIndex = 26
        '
        'tabPage2
        '
        tabPage2.Controls.Add(progressBar1)
        tabPage2.Controls.Add(messageTextBox)
        tabPage2.Location = New System.Drawing.Point(4, 25)
        tabPage2.Name = "tabPage2"
        tabPage2.Padding = New System.Windows.Forms.Padding(3)
        tabPage2.Size = New System.Drawing.Size(824, 403)
        tabPage2.TabIndex = 1
        tabPage2.Text = "Mensaje"
        tabPage2.UseVisualStyleBackColor = True
        '
        'progressBar1
        '
        progressBar1.Location = New System.Drawing.Point(6, 283)
        progressBar1.Maximum = 70
        progressBar1.Name = "progressBar1"
        progressBar1.Size = New System.Drawing.Size(189, 23)
        progressBar1.TabIndex = 1
        '
        'messageTextBox
        '
        messageTextBox.AllowDrop = True
        messageTextBox.BackColor = System.Drawing.SystemColors.Control
        messageTextBox.Location = New System.Drawing.Point(6, 6)
        messageTextBox.Multiline = True
        messageTextBox.Name = "messageTextBox"
        messageTextBox.ReadOnly = True
        messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        messageTextBox.Size = New System.Drawing.Size(479, 274)
        messageTextBox.TabIndex = 0
        '
        'btInvoke
        '
        btInvoke.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btInvoke.FlatStyle = FlatStyle.Flat
        btInvoke.ForeColor = System.Drawing.Color.White
        btInvoke.Location = New System.Drawing.Point(274, 36)
        btInvoke.Name = "btInvoke"
        btInvoke.Size = New System.Drawing.Size(91, 27)
        btInvoke.TabIndex = 31
        btInvoke.Text = "Invocar"
        btInvoke.UseVisualStyleBackColor = True
        '
        'tbWSDL
        '
        tbWSDL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        tbWSDL.Location = New System.Drawing.Point(131, 9)
        tbWSDL.Name = "tbWSDL"
        tbWSDL.Size = New System.Drawing.Size(690, 23)
        tbWSDL.TabIndex = 14
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New Font("Verdana", 9.75!)
        Label6.FontSize = 9.75!
        Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label6.Location = New System.Drawing.Point(3, 12)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(122, 16)
        Label6.TabIndex = 13
        Label6.Text = "Direccion WSDL :"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnLoadWebService
        '
        btnLoadWebService.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnLoadWebService.FlatStyle = FlatStyle.Flat
        btnLoadWebService.ForeColor = System.Drawing.Color.White
        btnLoadWebService.Location = New System.Drawing.Point(6, 36)
        btnLoadWebService.Name = "btnLoadWebService"
        btnLoadWebService.Size = New System.Drawing.Size(183, 27)
        btnLoadWebService.TabIndex = 43
        btnLoadWebService.Text = "Cargar datos WebService"
        btnLoadWebService.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Panel1.Controls.Add(tbWSDL)
        Panel1.Controls.Add(btnLoadWebService)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(btInvoke)
        Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Panel1.Location = New System.Drawing.Point(3, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(832, 69)
        Panel1.TabIndex = 44
        '
        'UCDoConsumeWebService
        '
        Name = "UCDoConsumeWebService"
        Size = New System.Drawing.Size(846, 536)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        tabControl1.ResumeLayout(False)
        tabPage1.ResumeLayout(False)
        tabPage1.PerformLayout()
        tabPage2.ResumeLayout(False)
        tabPage2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#Region "Constructores"
    Public Sub New(ByRef rule As IDoConsumeWebService, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule
        HasBeenModified = False
    End Sub
#End Region

#Region "Clases privadas"

    ''' <summary>
    ''' Representa 1 metodo de 1 WebService
    ''' </summary>
    Private Class Method
        Implements IDisposable
#Region "Atributos"
        Private _name As String = Nothing
        Private _paramaters As List(Of Parameter) = Nothing
#End Region

#Region "Propiedades"
        Friend ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property
        Friend Property Paramaters() As List(Of Parameter)
            Get
                Return _paramaters
            End Get
            Set(ByVal value As List(Of Parameter))
                _paramaters = value
            End Set
        End Property
#End Region

#Region "Constructores"
        Public Sub New()
            _paramaters = New List(Of Parameter)()
        End Sub
        Public Sub New(ByVal name As String)
            Me.New()
            _name = name
        End Sub

#End Region
        Public Sub Dispose() Implements IDisposable.Dispose
            _name = Nothing
            _paramaters = Nothing
        End Sub
    End Class
    ''' <summary>
    ''' Representa 1 parametro de 1 metodo de 1 WebService
    ''' </summary>
    Private Class Parameter
        Implements IDisposable
#Region "Atributos"
        Private _value As Object = String.Empty
        Private _name As String = Nothing
        Private _typeName As String = Nothing
#End Region

#Region "Propiedades"
        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public Property Value() As Object
            Get
                Return _value
            End Get
            Set(ByVal value As Object)
                _value = value
            End Set
        End Property
        Public Property TypeName() As String
            Get
                Return _typeName
            End Get
            Set(ByVal value As String)
                _typeName = value
            End Set
        End Property

#End Region

#Region "Constructores"
        Public Sub New(ByVal name As String)
            _name = name
        End Sub
        Public Sub New(ByVal name As String, ByVal value As Object)
            Me.New(name)
            _value = value
        End Sub
        Public Sub New(ByVal name As String, ByVal typeName As String)
            Me.New(name)
            _typeName = typeName
        End Sub
#End Region

        Public Sub Dispose() Implements IDisposable.Dispose
            _name = Nothing
            _value = Nothing
        End Sub
    End Class
    ''' <summary>
    ''' Esta clase representa el contenido de la grilla de propiedades. En esta se cargan los valores a mostrar cuando se selecciona
    ''' 1 parametro de 1 metodo.
    ''' </summary>
    Private Class GridProperty
#Region "Atributos"
        Private Shared m_myValue As String()
        Private Shared m_index As Integer
        Private Shared MyType As Type()
#End Region

#Region "Propiedades"


        <Category("Data")>
        Public Property Type() As Type
            Get
                Return MyType(m_index)
            End Get

            Set(ByVal value As Type)
                MyType(m_index) = value
            End Set
        End Property

        <Category("Valor")>
        Public Property Value() As String
            Get
                Return m_myValue(m_index)
            End Get
            Set(ByVal value As String)
                m_myValue(m_index) = value
            End Set
        End Property

        <Browsable(False)>
        Public Property Index() As Integer
            Get
                Return m_index
            End Get
            Set(ByVal value As Integer)
                m_index = value
            End Set
        End Property

        <Browsable(False)>
        Public ReadOnly Property MyValue() As String()
            Get
                Return m_myValue
            End Get
        End Property

        <Browsable(False)>
        Public Property TypeParameter() As Type()
            Get
                Return MyType
            End Get
            Set(ByVal value As Type())
                MyType = value
            End Set
        End Property
#End Region

#Region "Constructores"
        Public Sub New(ByVal Capacity As Integer)
            m_myValue = New String(Capacity - 1) {}
            MyType = New Type(Capacity - 1) {}
        End Sub
#End Region
    End Class
#End Region

    Private Sub txtValor_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtValor.TextChanged
        Try
            If Not IsNothing(SelectedParameter) Then
                SelectedParameter.Value = txtValor.Text
                UpdateParameterNode(txtValor.Text)
            End If
            HasBeenModified = True

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ValidateParameters()
        If (tvParameters.Nodes.Count = 0) Then
            tvParameters.Enabled = False
            txtValor.Enabled = False
        Else
            tvParameters.Enabled = True
            txtValor.Enabled = True
        End If
    End Sub
    Private Sub ValidateMethods()
        If (tvWsdl.Nodes.Count = 0) Then
            tvWsdl.Enabled = False
            tvParameters.Enabled = False
            txtValor.Enabled = False
        Else
            tvWsdl.Enabled = True
            tvParameters.Enabled = True
            txtValor.Enabled = True
        End If
    End Sub

    Private Sub btnLoadWebService_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadWebService.Click
        DynamicInvocation(tbWSDL.Text)
        Try

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        HasBeenModified = True

    End Sub

    Private Sub tbResponse_TextChanged(sender As Object, e As EventArgs) Handles tbResponse.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub chkCredentials_CheckedChanged(sender As Object, e As EventArgs) Handles chkCredentials.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub txtSaveInValue_TextChanged(sender As Object, e As EventArgs) Handles txtSaveInValue.TextChanged
        HasBeenModified = True
    End Sub
End Class
