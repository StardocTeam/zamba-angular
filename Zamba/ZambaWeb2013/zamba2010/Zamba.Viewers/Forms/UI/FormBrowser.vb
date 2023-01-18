'VERSION 3
Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.AppBlock
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Web
Imports Zamba.Core

Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports FormulariosDinamicos
Imports System.Text.RegularExpressions
Imports Zamba.Viewers.HelperForm

Public Class FormBrowser
    Inherits ZControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        'Try
        InitializeComponent()
        'Catch ex As Exception
        '    System.Windows.Forms.MessageBox.Show("Ocurrio un error al Visualizar el objeto " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
        'End Try
        'Add any initialization after the InitializeComponent() call
    End Sub

    ''' <summary>
    ''' Constructor que recibe IZControl para asociar el evento Refreshdata cuando se cambiaron los indices.
    ''' </summary>
    ''' <param name="ucindex"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''    [Ezequiel] - 06/10/09  - Created.
    ''' </history>
    Public Sub New(ByRef ucindex As IZControl)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        'Try
        InitializeComponent()
        If Not IsNothing(ucindex) Then
            RemoveHandler ucindex.OnChangeControl, AddressOf RefreshData
            AddHandler ucindex.OnChangeControl, AddressOf RefreshData
        End If
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents List1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxWebBrowser1 As New WebBrowser
    'Friend WithEvents Panel1 As ZBluePanel
    'Friend WithEvents Panel2 As ZBluePanel
    'Friend WithEvents Panel3 As ZBluePanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.AxWebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.List1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxWebBrowser1
        '
        Me.AxWebBrowser1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.AxWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxWebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.AxWebBrowser1.Name = "AxWebBrowser1"
        Me.AxWebBrowser1.Size = New System.Drawing.Size(552, 477)
        Me.AxWebBrowser1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListToolStripMenuItem, Me.List1ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 48)
        '
        'ListToolStripMenuItem
        '
        Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        Me.ListToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.ListToolStripMenuItem.Text = "List"
        '
        'List1ToolStripMenuItem
        '
        Me.List1ToolStripMenuItem.Name = "List1ToolStripMenuItem"
        Me.List1ToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.List1ToolStripMenuItem.Text = "List1"
        '
        'FormBrowser
        '
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.AxWebBrowser1)
        Me.Name = "FormBrowser"
        Me.Size = New System.Drawing.Size(552, 477)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "General"
    '[Ezequiel] 08/04/09 - Created
    Public Event GetIndexsEvent(ByRef indexs As ArrayList)
    Public Event FormChanged(ByVal Ischanged As Boolean)
    Public Event SaveFormChanges()
    Public Event CancelChildRules(ByVal Cancel As Boolean)
    Public Event FormClose()
    Public Event RefreshAfterF5()
    Public Event FormCloseTab()
    Public Event CloseWindow()
    Public Event RefreshTask(ByVal Task As ITaskResult)
    Public Event ReloadAsociatedResult(ByVal AsociatedResult As Core.Result)

    '[AlejandroR] 12/11/09 - Created
    Public Event SaveDocumentVirtualForm()

    Dim localResult As Result

    Dim _file As String

    '[AlejandroR] 28/12/09 - Created
    Private _disableInputControls As Boolean = False

    Public Property DisableInputControls() As Boolean
        Get
            Return _disableInputControls
        End Get
        Set(ByVal value As Boolean)
            _disableInputControls = value
        End Set
    End Property
    Private rulesIds As List(Of Int64)

    Private _userActionDisabledRules As New List(Of Long)

    Public Property UserActionDisabledRules() As List(Of Long)
        Get
            Return _userActionDisabledRules
        End Get
        Set(ByVal value As List(Of Long))
            _userActionDisabledRules = value
        End Set
    End Property

    Private _closeFormWindowAfterRuleExecution As Boolean

    Public Property CloseFormWindowAfterRuleExecution() As Boolean
        Get
            Return _closeFormWindowAfterRuleExecution
        End Get
        Set(ByVal value As Boolean)
            _closeFormWindowAfterRuleExecution = value
        End Set
    End Property

    ''' <summary>
    ''' Método que muestra un formulario
    ''' </summary>
    ''' <param name="myResult">Instancia de una tarea o documento que se selecciona</param>
    ''' <param name="tmpForm">Instancia de un formulario asociado al tipo de documento al que pertenece el documento</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	10/07/2008	Modified
    '''     [Gaston]    05/03/2009  Modified    Llámada al método "navigateToForm"
    ''' </history>
    Public Sub ShowDocument(ByRef myResult As Result, ByVal tmpForm As ZwebForm)
        Me.localResult = myResult

        If (myResult.DocType.IsReindex = False) Then
            If (tmpForm.Type = FormTypes.Show) Then
                If (navigateToForm(tmpForm, "1") = True) Then
                    Exit Sub
                End If
            End If
        Else
            If (tmpForm.Type = FormTypes.Edit) Then
                If (navigateToForm(tmpForm, "2") = True) Then
                    Exit Sub
                End If
            End If
        End If

        If (tmpForm.Type = FormTypes.Show) Then
            If (navigateToForm(tmpForm, "3") = True) Then
                Exit Sub
            End If
        ElseIf (myResult.CurrentFormID = tmpForm.ID) Then
            If (navigateToForm(tmpForm, "4") = True) Then
                Exit Sub
            End If
        End If

        Throw New Exception("No hay formulario para el resultado")
    End Sub


    '''' <summary>
    '''' Evalua las condiciones de un formulario, devuelve true si alguna de las condiciones se cumple
    '''' caso contrario devuelve false
    '''' </summary>
    '''' <param name="myResult">Instancia de una tarea o documento que se selecciona</param>
    '''' <param name="ds">DataSet que contiene las condiciones aplicadas a un formulario</param>
    '''' <remarks></remarks>
    '''' <history>
    ''''     [Pablo]    12/10/2010  Created
    '''' </history>
    Public Function EvaluateDynamicFormConditions(ByRef myResult As Result, ByVal ds As DataSet) As Boolean
        Dim localResult As Result = myResult
        Dim f, j, o, r, h As Int32
        Dim TodosLosIndicesValidos, HayUnIndiceValido As Boolean

        TodosLosIndicesValidos = False
        HayUnIndiceValido = False
        r = 0

        'se verifica que se cumpla la condicion
        For t As Int32 = 0 To ds.Tables(0).Rows.Count - 1

            'itero buscando el indice por el que se asigno la condicion
            For j = 0 To localResult.Indexs.Count - 1
                'comparo el indice de la condicion con el indice del formulario
                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).ID = ds.Tables(0).Rows(r).Item("Iid") Then
                    'entro por OR
                    If ds.Tables(0).Rows(r).Item("Op").ToString.Split("|")(1) = "O" Then

                        'comparo el valor de la condicion con el valor del indice
                        Select Case ds.Tables(0).Rows(r).Item("Op").ToString.Split("|")(0)
                            Case "="
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data = ds.Tables(0).Rows(r).Item("Value") Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case "<>"
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data <> ds.Tables(0).Rows(r).Item("Value") Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case "<="
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data <= ds.Tables(0).Rows(r).Item("Value") Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case ">="
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data >= ds.Tables(0).Rows(r).Item("Value") Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case "<"
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data < ds.Tables(0).Rows(r).Item("Value") Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case ">"
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data > ds.Tables(0).Rows(r).Item("Value") Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case "Contiene"
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data.Contains(ds.Tables(0).Rows(r).Item("Value").ToString) Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case "Empieza"
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data.StartsWith(ds.Tables(0).Rows(r).Item("Value").ToString) Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            Case "Termina"
                                If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data.EndsWith(ds.Tables(0).Rows(r).Item("Value").ToString) Then
                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                        End Select
                    Else
                        'entro por AND
                        For h = 0 To localResult.Indexs.Count - 1
                            If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).ID = ds.Tables(0).Rows(r).Item("Iid") Then
                                'comparo el valor de la condicion con el valor del indice
                                Select Case ds.Tables(0).Rows(r).Item("Op").ToString.Split("|")(0)
                                    Case "="
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data = ds.Tables(0).Rows(r).Item("Value") Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case "<>"
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data <> ds.Tables(0).Rows(r).Item("Value") Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case "<="
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data <= ds.Tables(0).Rows(r).Item("Value") Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case ">="
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data >= ds.Tables(0).Rows(r).Item("Value") Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case "<"
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data < ds.Tables(0).Rows(r).Item("Value") Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case ">"
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data > ds.Tables(0).Rows(r).Item("Value") Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case "Contiene"
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data.Contains(ds.Tables(0).Rows(r).Item("Value").ToString) Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case "Empieza"
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data.StartsWith(ds.Tables(0).Rows(r).Item("Value").ToString) Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                    Case "Termina"
                                        If DirectCast(DirectCast(localResult.Indexs(j), System.Object), Zamba.Core.Index).Data.EndsWith(ds.Tables(0).Rows(r).Item("Value").ToString) Then
                                            TodosLosIndicesValidos = True
                                            Exit For
                                        Else
                                            TodosLosIndicesValidos = False
                                            Exit For
                                        End If
                                End Select
                            End If
                        Next
                    End If

                    If Not r = (ds.Tables(0).Rows.Count - 1) Then
                        r = r + 1
                        Exit For
                    Else
                        Exit For
                    End If
                End If
            Next
        Next

        If HayUnIndiceValido Or TodosLosIndicesValidos Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Método que muestra un formulario
    ''' </summary>
    ''' <param name="myResult">Instancia de una tarea o documento que se selecciona</param>
    ''' <param name="Forms">Colección de instancias de formularios asociados al tipo de documento al que pertenece el documento</param>
    ''' <param name="ComeFromWF">Booleano que determina cuando el metodo es llamado desde busqueda o desde tareas</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    05/03/2009  Modified    Llámada al método "navigateToForm"
    '''     [pablo]     14/10/2010  Modified    se agrega la validacion de condiciones, se cambia el metodo
    '''                                         para que se muestre el tipo visualizacion en caso de que no 
    '''                                         existan condiciones aplicadas en los formularios
    ''' </history>
    Public Sub ShowDocument(ByRef myResult As Result, ByVal Forms() As ZwebForm, ByVal ComeFromWF As Boolean)
        Dim f, i As Int32
        Me.localResult = myResult

        If Forms.Length > 2 Then
            Dim ds As DataSet
            Dim AuxForms(0) As ZwebForm


            For i = 0 To Forms.Length - 1
                'si vengo desde tareas visualizo el formulario de edicion
                If ComeFromWF Then
                    If (navigateToForm(Forms(i), "2") = True) Then
                        Exit Sub
                    End If
                Else
                    'sino, obtengo las condiciones del formulario
                    ds = FormBusiness.GetDynamicFormIndexsConditions(Forms(i).ID)
                    'valido que el formulario tenga condiciones aplicadas
                    If ds.Tables(0).Rows.Count > 0 Then
                        If EvaluateDynamicFormConditions(myResult, ds) Then
                            f = f + 1
                            AuxForms.Resize(AuxForms, f)
                            AuxForms.SetValue(Forms(i), f - 1)
                        End If
                    End If
                End If
            Next

            If Not AuxForms(0) Is Nothing Then
                'si existe mas de un formulario con condicion entonces muestro el panel de seleccion
                If AuxForms.Length > 1 Then
                    Dim FormLst As New frmFormsListOfDocType(AuxForms)
                    FormLst.ShowDialog()
                    Forms = FormLst.FormSelected
                ElseIf AuxForms.Length = 1 Then
                    'sino lo muestro directamente
                    Forms = AuxForms
                End If
            Else
                'sino muestro el panel para seleccionar los formularios existentes
                Dim FormLst As New frmFormsListOfDocType(Forms)
                FormLst.ShowDialog()
                Forms = FormLst.FormSelected
            End If

        End If

        For i = 0 To Forms.Length - 1
            If (myResult.DocType.IsReindex = False) Then
                If (Forms(i).Type = FormTypes.Show) Then
                    If (navigateToForm(Forms(i), "1") = True) Then
                        Exit Sub
                    End If
                End If
            Else
                If (Forms(i).Type = FormTypes.Edit) Then
                    If (navigateToForm(Forms(i), "2") = True) Then
                        Exit Sub
                    End If
                End If
            End If
        Next

        'si no abre ningun form abro el de show
        For i = 0 To Forms.Length - 1
            If (Forms(i).Type = FormTypes.Show) Then
                If (navigateToForm(Forms(i), "3") = True) Then
                    Exit Sub
                End If
            End If
        Next

        Throw New Exception("No hay formulario para el resultado")
    End Sub

    ''' <summary>
    ''' Determina cuando un formulario es de tipo insercion y devuelve true
    ''' </summary>
    ''' <param name="myResult">Instancia de una tarea o documento que se selecciona</param>
    ''' <param name="Forms">Colección de instancias de formularios asociados al tipo de documento al que pertenece el documento</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Pablo]    12/10/2010  Created
    ''' </history>
    Public Function ShowInsertForm(ByRef myResult As Result, ByVal Forms() As ZwebForm) As Boolean
        Me.localResult = myResult
        Dim i As Int32

        'If Forms.Length > 1 Then
        '    Dim FormLst As New frmFormsListOfDocType(Forms)
        '    FormLst.ShowDialog()
        '    Forms = FormLst.FormSelected
        'End If

        For i = 0 To Forms.Length - 1
            If (Forms(i).Type = FormTypes.Insert) Then
                If (navigateToForm(Forms(i), "4") = True) Then
                    Return True
                End If
            End If
        Next
    End Function

    ''' <summary>
    ''' Método que sirve para navegar al formulario
    ''' </summary>
    ''' <param name="form">Instancia de un formulario</param>
    ''' <param name="typeForm">Tipo de formulario, ya sea Show, Edit, etc...  </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    04/03/2009  Created    
    ''      [Gaston]    11/03/2009  Modified    Al método "CreateTable" se le pasa el nombre del formulario
    ''' </history>

    Private Function navigateToForm(ByRef form As ZwebForm, ByVal typeForm As String) As Boolean
        Dim proc As System.Diagnostics.Process = Nothing
        ' Si el path al formulario está vacío entonces el formulario es un formulario dinámico
        If (String.IsNullOrEmpty(form.Path)) Then

            Trace.WriteLineIf(ZTrace.IsVerbose, typeForm & " - Formulario dinámico: se crea el formulario y se guarda en un archivo html temporal")

            Dim dsDynamicForm As DataSet = FormBusiness.GetDynamicForm(form.ID)
            Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()

            If (dsDynamicForm.Tables(0).Rows.Count > 0) Then

                Try
                    ' Se crea el formulario dinámico y se guarda en un archivo html temporal
                    form.Path = formDinamico.CreateTable(dsDynamicForm, form.Name)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Return (False)
                End Try

            Else
                Return (False)
            End If

        Else
            Try
                ' Se crea una copia del formulario del servidor y se pasa a la máquina local
                If Boolean.Parse(UserPreferences.getValue("MakeLocalFormCopy", Sections.FormPreferences, "False")) = True Then
                    Trace.WriteLineIf(ZTrace.IsVerbose, typeForm & " - Hago copia local del Formulario " & Now.ToString())
                    MakeLocalCopy(form)
                Else
                    Dim Dir As IO.DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\temp")
                    'If Dir.Exists = False Then Dir.Create()
                    Dim ServerFile As New IO.FileInfo(form.Path)
                    Dim rutaTemp As String = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & ServerFile.Name

                    If (IO.File.Exists(rutaTemp) = False) Then
                        rutaTemp = Membership.MembershipHelper.StartUpPath & "\temp\" & ServerFile.Name
                        If (IO.File.Exists(rutaTemp) = False) Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, typeForm & " - Hago copia local del Formulario " & Now.ToString())
                            MakeLocalCopy(form)
                        End If
                    End If
                    form.Path = rutaTemp

                End If

                Trace.WriteLineIf(ZTrace.IsVerbose, "Ruta del Formulario: " & form.Path)

                '------------------------------------------------------
                'osanchez
                '06-04-09
                'Si tiene un iframe, busco el documento asociado
                '------------------------------------------------------
                Dim listaTags As New List(Of HelperForm.DtoTag)

                Dim matches As MatchCollection
                matches = HelperForm.HelperFormVirtual.ParseHtml(form.Path, "iframe")

                'Entrar por aca si el html tiene la palabra iframe
                If Not IsNothing(matches) Then
                    For Each item As Match In matches
                        If HelperForm.HelperFormVirtual.buscarHtmlIframe(item) Then
                            Dim id As Int64 = HelperForm.HelperFormVirtual.buscarTagZamba(item)
                            Dim useOriginal As Boolean = False
                            If id = -1 Then
                                If Not String.IsNullOrEmpty(localResult.FullPath) Then
                                    Dim Path As String = localResult.FullPath
                                    Dim fi As IO.FileInfo = Nothing
                                    Dim FTemp As IO.FileInfo = Nothing
                                    Dim dir As IO.DirectoryInfo

                                    Try
                                        fi = New IO.FileInfo(Path)
                                        dir = GetTempDir("\OfficeTemp")
                                        If dir.Exists = False Then dir.Create()
                                        FTemp = New IO.FileInfo(dir.FullName & "\" & fi.Name)


                                        Try
                                            '[Sebastian] 22-06-2009 Se comento la linea y se agrego la condicion para
                                            'copiar el archivo desde la carpeta indexer en caso de que exista
                                            'esto se hizo porque si se realizaban cambios antes de insertar no los tomaba para el 
                                            'archivo.
                                            'fi.CopyTo(FTemp.FullName, True)
                                            If FTemp.Exists = False Then
                                                fi.CopyTo(FTemp.FullName, True)
                                            End If
                                            FTemp.Attributes = IO.FileAttributes.Normal
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try


                                        _file = FTemp.FullName
                                        'End If

                                        'If Result.FullPath.ToUpper.EndsWith(".HTML") Or Result.FullPath.ToUpper.EndsWith(".HTM") Then
                                        '    Results_Business.CopySubDirAndFilesBrowser(dir.FullName, Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")), Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")))
                                        'End If

                                    Catch ex As Exception
                                        ZClass.raiseerror(ex)
                                    Finally

                                    End Try



                                    If localResult.IsMsg AndAlso (File.Exists(localResult.FullPath.ToLower().Replace(".msg", ".html")) OrElse File.Exists(localResult.FullPath.ToLower().Replace(".msg", ".txt"))) Then
                                        Dim DI As DirectoryInfo = New DirectoryInfo(System.IO.Path.GetDirectoryName(localResult.FullPath))
                                        Dim fileList As System.IO.FileInfo() = DI.GetFiles()
                                        Dim fList As List(Of System.IO.FileInfo) = New List(Of System.IO.FileInfo)

                                        For Each fItem As System.IO.FileInfo In fileList
                                            If fItem.Name.Contains(System.IO.Path.GetFileNameWithoutExtension(localResult.FullPath)) AndAlso Not fItem.Name.Trim().ToLower().EndsWith(".msg") Then
                                                fList.Add(fItem)
                                            End If
                                        Next


                                        If fList.Count > 0 Then
                                            Path = fList.Item(0).FullName
                                        End If
                                    End If



                                    Dim tag As String = item.Value
                                    HelperForm.HelperFormVirtual.replazarAtributoSrc(tag, _file)
                                    Dim dto As HelperForm.DtoTag
                                    dto = HelperForm.HelperFormVirtual.instanceDtoTag(item.Value, tag)
                                    listaTags.Add(dto)
                                    useOriginal = True

                                Else
                                    'Si el fullpath esta vacio y el item.Value contiene datos, se busca
                                    'la propiedad src y se la remueve para mostrar un iframe en blanco.
                                    If Not IsNothing(item) AndAlso Not IsNothing(item.Value) Then
                                        'Se intenta remover el atributo src.
                                        Dim tag As String = HelperForm.HelperFormVirtual.RemoveSrcTag(item.Value)
                                        'Si existieron cambios realizo la modificacion.
                                        If String.Compare(item.Value, tag) <> 0 Then
                                            Dim dto As HelperForm.DtoTag = HelperForm.HelperFormVirtual.instanceDtoTag(item.Value, tag)
                                            listaTags.Add(dto)

                                        End If

                                        useOriginal = True
                                    End If
                                End If

                            ElseIf id = 0 Then
                                useOriginal = True
                            End If
                            If useOriginal = False Then
                                Dim docTypesAsocList As ArrayList
                                'Busca el documento asociado
                                docTypesAsocList = DocAsociatedBusiness.getAsociatedResultsFromResult(localResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)))

                                If Not IsNothing(docTypesAsocList) AndAlso docTypesAsocList.Count > 0 Then
                                    For Each docAsoc As Object In docTypesAsocList
                                        If TypeOf docAsoc Is Result Then
                                            Dim myResult As Result
                                            myResult = DirectCast(docAsoc, Result)
                                            Dim path As String = String.Empty
                                            Dim tag As String = item.Value
                                            'Verifica que sea el DocType correcto
                                            If myResult.DocTypeId = id Or id = 0 Then
                                                If myResult.ISVIRTUAL Then
                                                    'Se obtiene el id del formulario actual
                                                    myResult.CurrentFormID = DocAsociatedBusiness.getAsociatedFormId(CType(myResult.DocType.ID, Integer))

                                                    'Agrego una validacion para si no hay form asociado, no tire error - MC
                                                    If myResult.CurrentFormID <> 0 Then
                                                        'Obtiene el formulario Virtual
                                                        Dim formInner As ZwebForm
                                                        formInner = FormBusiness.GetForm(myResult.CurrentFormID)

                                                        ' Se crea una copia del formulario del servidor y se pasa a la máquina local
                                                        If Boolean.Parse(UserPreferences.getValue("MakeLocalFormCopy", Sections.FormPreferences, "False")) = True Then
                                                            MakeLocalCopy(formInner)
                                                        Else
                                                            Dim Dir As IO.DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\temp")
                                                            If Dir.Exists = False Then Dir.Create()
                                                            Dim ServerFile As New IO.FileInfo(formInner.Path)

                                                            formInner.Path = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & ServerFile.Name
                                                        End If

                                                        path = formInner.Path

                                                        'Reemplaza el atributo id
                                                        HelperForm.HelperFormVirtual.replazarAtributoId(tag, myResult.ID)
                                                    End If
                                                Else
                                                    'Consultar si se copia localmente el archivo cuando no es virtual
                                                    path = myResult.FullPath
                                                End If

                                                If myResult.IsMsg AndAlso (File.Exists(myResult.FullPath.ToLower().Replace(".msg", ".html")) OrElse File.Exists(myResult.FullPath.ToLower().Replace(".msg", ".txt"))) Then

                                                    If Boolean.Parse(UserPreferences.getValue("OpenMsgFileInIFrame", Sections.FormPreferences, "False")) Then

                                                        Dim DI As DirectoryInfo = New DirectoryInfo(System.IO.Path.GetDirectoryName(myResult.FullPath))
                                                        Dim fileList As System.IO.FileInfo() = DI.GetFiles()
                                                        Dim fList As List(Of System.IO.FileInfo) = New List(Of System.IO.FileInfo)

                                                        For Each fItem As System.IO.FileInfo In fileList

                                                            If fItem.Name.Contains(System.IO.Path.GetFileNameWithoutExtension(myResult.FullPath)) AndAlso Not fItem.Name.Trim().ToLower().EndsWith(".msg") Then
                                                                fList.Add(fItem)
                                                            End If

                                                        Next
                                                        If fList.Count > 0 Then
                                                            path = fList.Item(0).FullName
                                                        End If



                                                    Else
                                                        If IsNothing(proc) Then

                                                            Dim Dir As DirectoryInfo = GetTempDir("\OfficeTemp")
                                                            Dim strPathLocal As String = Dir.FullName & myResult.FullPath.Remove(0, myResult.FullPath.LastIndexOf("\"))

                                                            Try
                                                                File.Copy(myResult.FullPath, strPathLocal, True)
                                                            Catch ex As Exception
                                                                raiseerror(ex)
                                                            End Try

                                                            Trace.WriteLineIf(ZTrace.IsVerbose, " Se creo Archivo temporal de " & strPathLocal & " " & Date.Now.ToString())



                                                            proc = New System.Diagnostics.Process()
                                                            proc.StartInfo.UseShellExecute = True
                                                            proc.StartInfo.FileName = strPathLocal
                                                            proc.Start()
                                                            Exit For
                                                        End If




                                                    End If

                                                End If

                                                'Reemplaza el atributo src
                                                HelperForm.HelperFormVirtual.replazarAtributoSrc(tag, path)

                                                Dim dto As HelperForm.DtoTag
                                                dto = HelperForm.HelperFormVirtual.instanceDtoTag(item.Value, tag)


                                                listaTags.Add(dto)
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Next
                    HelperForm.HelperFormVirtual.actualizarHtml(listaTags, form.Path)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return (False)
            End Try
        End If

        Trace.WriteLineIf(ZTrace.IsVerbose, typeForm & " - Navego al Formulario " & Now.ToString())

        Try
            'Guardo las reglas
            If form.useRuleRights Then
                rulesIds = getRulesIds(form.Path)
            End If

            Navigate(form.Path)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Obtiene los ids de las reglas que se encuentran en el formulario
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getRulesIds(ByVal path As String) As List(Of Int64)
        Dim html As String
        Dim rulesIds As New List(Of Int64)
        Using reader As New StreamReader(path)
            html = reader.ReadToEnd()
        End Using

        If html.Contains("zamba_rule_") Then
            While html.Contains("zamba_rule_")
                html = html.Trim().Remove(0, html.Trim().IndexOf("zamba_rule_") + 11)
                Dim ruleId As Int64
                If Int64.TryParse(html.Remove(html.Trim().IndexOf(Chr(34))), ruleId) Then
                    If rulesIds.Contains(ruleId) = False Then
                        rulesIds.Add(ruleId)
                    End If
                End If
            End While
        End If

        Return rulesIds
    End Function

    Private Function GetTempDir(ByVal dire As String) As IO.DirectoryInfo
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software" & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(Membership.MembershipHelper.StartUpPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function
    Private Shared Sub MakeLocalCopy(ByVal Form As ZwebForm)

        Dim Dir As IO.DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\temp")
        If Dir.Exists = False Then Dir.Create()
        Dim ServerFile As New IO.FileInfo(Form.Path)
        'If ServerFile.Exists = False Then Throw New Exception(ServerFile.FullName & " no existe")

        Try

            Dim LocalFile As New IO.FileInfo(Dir.FullName & "\" & ServerFile.Name)

            ServerFile.CopyTo(LocalFile.FullName, True)
            Form.Path = LocalFile.FullName

            Dim RutaArchJs As String = ServerFile.FullName.Remove(ServerFile.FullName.Length - ServerFile.Extension.Length, ServerFile.Extension.Length) + ".js"
            Dim RutaLocalJs As String = LocalFile.FullName.Remove(LocalFile.FullName.Length - LocalFile.Extension.Length, LocalFile.Extension.Length) + ".js"
            If File.Exists(RutaArchJs) Then
                'Sebastián: se borra el archivo local y luego se copia del servidor para que este siempre actualizado
                File.Delete(RutaLocalJs)
                File.Copy(RutaArchJs, RutaLocalJs)
            End If

            Dim RutaArchCss As String = ServerFile.FullName.Remove(ServerFile.FullName.Length - ServerFile.Extension.Length, ServerFile.Extension.Length) + ".css"
            Dim RutaLocalCss As String = LocalFile.FullName.Remove(LocalFile.FullName.Length - LocalFile.Extension.Length, LocalFile.Extension.Length) + ".css"

            If File.Exists(RutaArchCss) Then
                'Sebastián: se borra el archivo local y luego se copia del servidor para que este siempre actualizado
                File.Delete(RutaLocalCss)
                File.Copy(RutaArchCss, RutaLocalCss)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Navigate"

    ''' <summary>
    ''' Método que sirve para mostrar el formulario adentro del WebBrowser mediante un path al formulario, o en caso de que el archivo no exista 
    ''' mostrar un mensaje de error
    ''' </summary>
    ''' <param name="fi">Instancia de tipo FileInfo</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Modified    Se arma el formulario dinámico según el formId que se recibe. Si no se obtienen datos del formulario
    '''                                         entonces el WebBrowser muestra un mensaje de error
    ''' </history>
    Public Sub Navigate(ByVal path As String)
        ' [Tomas]   24/02/09    Se comprueba la existencia del archivo, en caso
        '                       de no existir se muestra el formulario virtual.
        'fi.Refresh()
        Dim fi As FileInfo = Nothing
        Try
            fi = New IO.FileInfo(path)

            If fi.Exists = True Then
                AxWebBrowser1.Navigate(fi.FullName)
            Else
                Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
                AxWebBrowser1.Navigate(formDinamico.showErrorMessage("No se ha encontrado el formulario virtual"))
                'Dim dsForm As DataSet = FormBusiness.GetDynamicFormValues()
                'Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
                'AxWebBrowser1.Navigate(formDinamico.CreateTable(dsForm))
            End If

        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As System.ExecutionEngineException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(fi) Then
                fi = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para mostrar el formulario adentro del WebBrowser mediante un path al formulario, o en caso de que el archivo no exista 
    ''' mostrar un mensaje de error
    ''' </summary>
    ''' <param name="fi">Path a un formulario dinámico</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Modified    Se arma el formulario dinámico según el formId que se recibe. Si no se obtienen datos del formulario
    '''                                         entonces el WebBrowser muestra un mensaje de error
    ''' </history>
    'Public Sub Navigate(ByVal fi As String)

    '    Try

    '        ' [Tomas]   24/02/09    Se comprueba la existencia del archivo, en caso
    '        '                       de no existir se muestra el formulario virtual.
    '        If File.Exists(fi) = True Then
    '            AxWebBrowser1.Navigate(fi)

    '        Else
    '            Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
    '            AxWebBrowser1.Navigate(formDinamico.showErrorMessage("No se ha encontrado el formulario virtual"))
    '            'Dim dsForm As DataSet = FormBusiness.GetDynamicFormValues()
    '            'Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
    '            'AxWebBrowser1.Navigate(formDinamico.CreateTable(dsForm))
    '        End If

    '    Catch ex As System.Runtime.InteropServices.COMException
    '    Catch ex As System.ExecutionEngineException
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try

    'End Sub

    ''' <summary>
    ''' Método que sirve para mostrar el formulario adentro del WebBrowser mediante un path al formulario, o en caso de que el archivo no exista 
    ''' mostrar un mensaje de error
    ''' </summary>
    ''' <param name="fi">Path a un formulario dinámico</param>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="frmName">Nombre de un formulario dinámico</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Created     Se arma el formulario dinámico según el formId que se recibe. Si no se obtienen datos del formulario
    '''                                         entonces el WebBrowser muestra un mensaje de error
    ''' </history>
    Public Sub Navigate(ByVal fi As String, ByVal frmId As Integer, ByVal frmName As String)

        Try

            ' [Tomas]   24/02/09    Se comprueba la existencia del archivo, en caso
            '                       de no existir se muestra el formulario virtual.
            If File.Exists(fi) = True Then
                AxWebBrowser1.Navigate(fi)
                '[sebastian 04-05-09] se realiza la copia local del archivo del servidor, esto lo estaba haciendo
                'pero lo dejo de hacer, por eso se agrego la linea.
                'MakeLocalCopy(FormBusiness.GetForm(frmId))
            Else

                Dim dsDynamicForm As DataSet = FormBusiness.GetDynamicForm(frmId)
                Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()

                If (dsDynamicForm.Tables(0).Rows.Count > 0) Then
                    AxWebBrowser1.Navigate(formDinamico.CreateTable(dsDynamicForm, frmName))
                Else
                    AxWebBrowser1.Navigate(formDinamico.showErrorMessage("No se ha encontrado el formulario virtual"))
                End If

            End If

        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As System.ExecutionEngineException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "AxWebBrowser DocumentComplete"
    Public Event LinkSelected(ByVal Result As Result)

    ''' <summary>
    ''' Carga los valores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxWebBrowser2_DocumentComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles AxWebBrowser1.DocumentCompleted
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.FlagAsigned = False Or bolmodified = False Then 'OrElse Me.flagrecover = True Then
                If Not Me.localResult Is Nothing AndAlso Not Me.localResult.Childs Is Nothing AndAlso Me.localResult.Childs.Values Is Nothing Then
                    For Each o As Object In Me.localResult.Childs.Values
                        If String.Compare(o.fullpath.ToString(), e.Url.ToString) = 0 Then
                            '[sebastian] 10-06-2009 se agrego cast para salvar warning
                            RaiseEvent LinkSelected(DirectCast(o, Result))
                            Exit Sub
                        End If
                    Next
                End If

                Trace.WriteLineIf(ZTrace.IsVerbose, "Document complete")
                Trace.WriteLineIf(ZTrace.IsVerbose, "Flag Asigned " & FlagAsigned)

                Me.FlagAsigned = True
                Trace.WriteLineIf(ZTrace.IsVerbose, "Asigno Valores al Formulario " & Now.ToString)
                AsignValues(Me.AxWebBrowser1.Document, Me.localResult)
            End If

        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Me.Cursor = cur
        Me.ResumeLayout()
    End Sub

    Dim FlagAsigned As Boolean
    Dim flagrecover As Boolean

#End Region

#End Region

#Region "AsigValues"
    Dim Reload As Boolean
    Public Sub RefreshData(ByRef result As Result)
        If Me.localResult.ID = result.ID Then
            Me.localResult = result
            RefreshData()
        End If
    End Sub
    Public Sub RefreshData()
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "ReAsigno Valores al Formulario " & Now.ToString)
            Me.Reload = True
            AsignValues(Me.AxWebBrowser1.Document, Me.localResult)
            Me.AxWebBrowser1.Update()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Me.Reload = False
        End Try
    End Sub

    Dim bolmodified As Boolean = False

    ''' <summary>
    ''' Guarda los valores de los indices en la BD
    ''' </summary>
    ''' <param name="doc1"></param>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Sub AsignValues(ByVal doc1 As HtmlDocument, ByVal Result As Result)
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Dim Element As HtmlElement

        Try
            'El Form debe tener Id = zamba_(doctypeid) o Id = zamba_(Nombre del DocType)
            'Los controles deben tener Id = "zamba(Id de Indice)"  o Id = "zamba_(Nombre del indice)"
            Trace.WriteLineIf(ZTrace.IsVerbose, "Ingreso en AsignValues: " & Now)
            Me.FlagAsigned = True

            If IsNothing(Result) = False Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo permisos")
                Dim UseIndexsRights As Boolean = False
                'verifica si utiliza permisos sobre indices
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, CInt(Result.DocType.ID)) = True Then
                    UseIndexsRights = Boolean.Parse(UserPreferences.getValue("UseViewRightsForIndexsOnForm", Sections.FormPreferences, "False"))
                End If

                Dim IndexsRights As Hashtable = Nothing
                If UseIndexsRights Then IndexsRights = UserBusiness.Rights.GetIndexsRights(Result.DocType.ID, UserBusiness.CurrentUser.ID, False, True)

                Trace.WriteLineIf(ZTrace.IsVerbose, "Indices: " & Result.Indexs.Count)
                For Each I As Index In Result.Indexs
                    Try
                        'Dim Element As HtmlElement
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Indice Obtenido: " & I.ID)

                        Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID)

                        If Not IsNothing(Element) Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, Element.Name)
                            bolmodified = True
                            AsignValue(I, Element)
                        Else
                            Element = doc1.GetElementById("ZAMBA_INDEX_" & I.Name)
                            If Not IsNothing(Element) Then
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Entrando en asignvalue")
                                bolmodified = True
                                AsignValue(I, Element)
                            Else
                                Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "S")
                                If Not IsNothing(Element) Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Entrando en asignvalue")
                                    bolmodified = True
                                    AsignValue(I, Element)
                                End If

                                Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "N")
                                If Not IsNothing(Element) Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Entrando en asignvalue")
                                    bolmodified = True
                                    AsignValue(I, Element)
                                End If
                            End If
                        End If

                        Trace.WriteLineIf(ZTrace.IsVerbose, "Indice asignado: " & I.ID)
                        If UseIndexsRights Then
                            Dim IR As IndexsRightsInfo = DirectCast(IndexsRights(I.ID), IndexsRightsInfo)
                            For Each indexid As Int64 In IndexsRights.Keys
                                If indexid = I.ID Then
                                    ''aplica permiso de Edición
                                    'If IR.GetIndexRightValue(RightsType.IndexEdit) = False Then
                                    '    Element.Style = "ReadOnly"
                                    'End If
                                    'aplica permiso Visible
                                    If IR.GetIndexRightValue(RightsType.IndexView) = False Then
                                        If (Not IsNothing(Element)) Then
                                            'Oculta el indice
                                            Element.Style = "display:none"
                                            'Oculta el label del indice
                                            Dim htmlElement_label As HtmlElement = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "_LBL")
                                            If Not IsNothing(htmlElement_label) Then
                                                htmlElement_label.Style = "display:none"
                                            End If

                                        End If
                                    End If

                                    ' [Gaston]    12/05/2009  Si el formulario no es un formulario dinámico entonces no se aplica el permiso de 
                                    '                         edición, porque sino estaría entrando en conflicto con un índice que tenga 
                                    '                         "sólo lectura" (en caso de que se haya configurado un índice para sólo lectura)
                                    If Not ((doc1.Body.InnerHtml.Contains("<FORM id=")) AndAlso (doc1.Body.InnerHtml.Contains("name=frmmain"))) Then

                                        If (Not IsNothing(Element)) Then
                                            'aplica permiso Edicion
                                            Element.Enabled = IR.GetIndexRightValue(RightsType.IndexEdit)
                                        End If
                                    End If
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Next

                'se reemplaza la llamada para la carga de las listas a un thread al finalizar la carga del formulario
                'de esta manera optimizamos la performance
                '                Me.LoadLists()
                '               Me.LoadList1()

                Try
                    'Dim Element As HtmlElement
                    Element = doc1.GetElementById("ZAMBA_IMAGE")
                    Me.LoadImage(Result, Element)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                'If Boolean.Parse(UserPreferences.getValue("HideButtonsIfUserActionIsDisabled", Sections.FormPreferences, "False")) = "True" Then
                Dim CurrentForm As IZwebForm = FormBusiness.GetForm(localResult.CurrentFormID)
                If Not IsNothing(CurrentForm) And CurrentForm.useRuleRights = True Then
                    If TypeOf localResult Is ITaskResult Then
                        'Por cada regla buscar si en el formulario hay un boton que la llame
                        'Si la regla esta deshabilitada, deshabilitar el boton
                        Dim Task As ITaskResult = DirectCast(Me.localResult, ITaskResult)

                        For Each Rule As IWFRuleParent In Task.WfStep.Rules
                            Element = doc1.GetElementById("ZAMBA_RULE_" & Rule.ID)
                            If Not IsNothing(Element) Then

                                If Not Rule.Enable OrElse Not WFRulesBusiness.GetIsRuleEnabled(Task.UserRules, Rule) OrElse Not WFRulesBusiness.GetStateOfHabilitationOfState(Rule, Task.State.ID) Then

                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Ocultando boton en el formulario para la regla: " & Rule.ID)
                                    Element.Style = "display:none"
                                End If
                            End If

                        Next
                    End If
                End If

                Trace.WriteLineIf(ZTrace.IsVerbose, "Ejecuto una funcion al terminar: " & Now)

                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Ejecutando backProcess: " & Now)

                    Dim WorkerProcess As New System.ComponentModel.BackgroundWorker
                    WorkerProcess.WorkerReportsProgress = True
                    AddHandler WorkerProcess.DoWork, AddressOf WP_DoWork
                    WorkerProcess.RunWorkerAsync()

                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsVerbose, ex.ToString())
                    raiseerror(ex)
                End Try

                Try
                    If Boolean.Parse(UserPreferences.getValue("LoadZVarcomponents", Sections.FormPreferences, "True")) = True Then
                        Dim elements As List(Of String) = getItems("zamba_zvar")
                        For Each Str As String In elements
                            Dim varname As String = Str.Replace("zamba_zvar(", "").Replace(")", "")
                            Element = doc1.GetElementById(Str)
                            If Not IsNothing(Element) Then
                                AsignVarValue(varname, Element)
                                Element.Id = varname
                            End If
                        Next
                    End If
                Catch ex As Exception
                    raiseerror(ex)
                End Try

                enableRules(doc1)

                If Not IsNothing(doc1) Then
                    If Not IsNothing(doc1.Body) Then
                        If Not String.IsNullOrEmpty(doc1.Body.InnerHtml) Then
                            Dim strHtml As String = doc1.Body.InnerHtml
                            Result.Html = strHtml
                        End If
                    End If
                End If
            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "El result esta en blanco")
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Trace.WriteLineIf(ZTrace.IsVerbose, ex.ToString())
        End Try

        '[AlejandroR] 28/12/09 - Created
        If _disableInputControls Then
            doDisableInputControls()
        End If

        Me.Cursor = cur
    End Sub

    ''' <summary>
    ''' Se encarga de habilitar las reglas dependiendo del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function enableRules(ByVal doc1 As HtmlDocument)
        If Not IsNothing(rulesIds) Then
            Dim task As TaskResult
            Try
                task = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(localResult.ID, 0)

                For i As Int32 = 0 To rulesIds.Count - 1
                    Dim wfstepID As Int64 = WFStepBusiness.GetStepIdByRuleId(rulesIds(i))

                    Dim _enabled As Boolean = False

                    If wfstepID = task.StepId Then
                        'Obtiene el valor 
                        Dim selectionvalue As RulePreferences = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(rulesIds(i))
                        'Se Evalua el valor de la variable seleccion 
                        Select Case selectionvalue
                            'Caso de trabajo con Estados
                            Case RulePreferences.HabilitationSelectionState
                                _enabled = True
                                'Se Obtienen los ids de estados DESHABILITADOS
                                Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeState, True)
                                'Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
                                'Coincidencia, se deshabilita la regla
                                For Each r As DataRow In Dt.Rows
                                    If Int32.Parse(r.Item("ObjValue").ToString) = task.StateId Then
                                        _enabled = False
                                        Exit For
                                    End If
                                Next
                                'Caso de trabajo con Usuarios o Grupos
                            Case RulePreferences.HabilitationSelectionUser
                                _enabled = True
                                'Se Obtienen los ids de USUARIOS DESHABILITADOS
                                Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser, True)
                                'Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
                                'Coincidencia, se deshabilita la regla
                                For Each r As DataRow In Dt.Rows
                                    If Int64.Parse(r.Item("ObjValue").ToString) = UserBusiness.CurrentUser.ID Then
                                        _enabled = False
                                        Exit For
                                    End If
                                Next
                                'si no se deshabilito la regla por usuario se intenta deshabilitar por grupo
                                If _enabled = True Then
                                    'Se Obtienen los ids de GRUPOS DESHABILITADOS
                                    Dim Dt2 As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup, True)
                                    'Por cada Id de Grupo se recorren sus usuario y se comparan con el id de usuario logeado, en cada de encontrar
                                    'Coincidencia, se deshabilita la regla
                                    For Each r As DataRow In Dt2.Rows
                                        Dim uids As List(Of Int64) = UserGroupBusiness.GetUsersIds(Int64.Parse(r.Item("ObjValue").ToString()), True)
                                        If Not IsNothing(uids) Then
                                            For Each uid As Int64 In uids

                                                If uid = UserBusiness.CurrentUser.ID Then
                                                    _enabled = False
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                        If _enabled = False Then Exit For
                                    Next
                                End If
                            Case RulePreferences.HabilitationSelectionBoth
                                _enabled = True
                                'Se Obtienen los ids de USUARIOS Y ESTADOS DESHABILITADOS
                                Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndUser, True)
                                'Por cada Id de usuario se comparan con el id de usuario logeado y los ids de etapa con el seleccionado en el combobox
                                ', en cada de encontrar coincidencia, se deshabilita la regla
                                For Each r As DataRow In Dt.Rows
                                    If Int64.Parse(r.Item("ObjValue").ToString) = UserBusiness.CurrentUser.ID AndAlso Int32.Parse(r.Item("ObjExtraData").ToString) = task.StateId Then
                                        _enabled = False
                                        Exit For
                                    End If
                                Next
                                'si no se deshabilito la regla por usuario y estado se intenta deshabilitar por grupo y estado
                                If _enabled = True Then
                                    'Se Obtienen los ids de GRUPOS Y ESTADOS DESHABILITADOS
                                    Dim Dt2 As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndGroup, True)
                                    'Por cada Id de grupo se recorren sus usuarios, se comparan con el id de usuario logeado y los ids de etapa con el seleccionado en el combobox
                                    ', en cada de encontrar coincidencia, se deshabilita la regla
                                    For Each r As DataRow In Dt2.Rows
                                        If Int32.Parse(r.Item("ObjValue").ToString) = task.StateId Then
                                            Dim uids As List(Of Int64) = UserBusiness.GetUserGroupsIdsByUserid(Int32.Parse(r.Item("ObjValue").ToString()))
                                            If Not IsNothing(uids) Then
                                                For Each uid As Int64 In uids

                                                    If uid = UserBusiness.CurrentUser.ID Then
                                                        _enabled = False
                                                        Exit For
                                                    End If
                                                Next
                                            End If
                                        End If
                                        If _enabled = False Then
                                            Exit For
                                        End If
                                    Next
                                End If
                            Case Else
                                _enabled = True
                        End Select
                    Else
                        _enabled = False
                    End If
                    Dim ruleElement As HtmlElement = doc1.GetElementById("Zamba_rule_" & rulesIds(i))
                    If Not IsNothing(ruleElement) Then
                        ruleElement.Enabled = _enabled
                    End If
                Next
            Finally
                task = Nothing
            End Try
        End If
    End Function

    ' [AlejandroR] 28/12/09 - Created
    ' Deshabilita todos los controles de tipo input para que no se pueda editar el formulario
    Private Function doDisableInputControls()
        Dim tag As String

        Try
            For Each el As HtmlElement In Me.AxWebBrowser1.Document.Body.All
                tag = el.TagName.ToLower()
                If tag = "input" OrElse tag = "select" OrElse tag = "button" Then
                    el.Enabled = False
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    Public Function GetHtml() As String
        Try
            Dim doc1 As System.Windows.Forms.HtmlDocument ' mshtml.HTMLDocumentClass
            doc1 = Me.AxWebBrowser1.Document
            Dim tag As New List(Of String)
            'Dim tagimg As New ArrayList
            For Each el As HtmlElement In doc1.Body.All
                If Not String.IsNullOrEmpty(el.GetAttribute("src")) AndAlso (el.GetAttribute("src").Contains(".js") OrElse el.GetAttribute("src").Contains("javascript:false")) Then
                    tag.Add(el.OuterHtml)
                End If
            Next

            For Each el As HtmlElement In doc1.Body.GetElementsByTagName("INPUT")
                If el.Id.Contains("zamba_save") OrElse el.Id.Contains("zamba_cancel") Then
                    tag.Add(el.OuterHtml)
                End If
            Next

            For Each el As HtmlElement In doc1.Body.GetElementsByTagName("img")
                el.SetAttribute("src", "cid:" & el.GetAttribute("src").Substring(el.GetAttribute("src").Replace("/", "\").LastIndexOf("\") + 1))
            Next

            Dim strHtml As String = doc1.Body.InnerHtml 'DirectCast(DirectCast(DirectCast(doc1.DomDocument, System.Object), mshtml.HTMLDocumentClass).documentElement, mshtml.IHTMLElement).innerHTML


            For Each remtag As String In tag
                If strHtml.Contains(remtag) Then
                    strHtml = strHtml.Replace(remtag, "")
                End If
            Next

            'For Each reptag As ArrayList In tagimg
            '    If strHtml.Contains(reptag(0).ToString) Then
            '        strHtml = strHtml.Replace(reptag(0).ToString, reptag(1).ToString)
            '    End If
            'Next

            If strHtml.ToLower.Contains("onfocus=" & Chr(34) & "showcalendarcontrol(this)" & Chr(34)) Or strHtml.ToLower.Contains("onfocus=showcalendarcontrol(this);") Then
                Dim straux As String
                If strHtml.ToLower.IndexOf("onfocus=" & Chr(34) & "showcalendarcontrol(this)" & Chr(34)) <> -1 Then
                    straux = strHtml.Substring(strHtml.ToLower.IndexOf("onfocus=" & Chr(34) & "showcalendarcontrol(this)" & Chr(34)), ("onfocus=" & Chr(34) & "showcalendarcontrol(this)" & Chr(34)).Length)
                    strHtml = strHtml.Replace(straux, "")
                End If
                If strHtml.ToLower.IndexOf("onfocus=showcalendarcontrol(this);") <> -1 Then
                    straux = strHtml.Substring(strHtml.ToLower.IndexOf("onfocus=showcalendarcontrol(this);"), ("onfocus=showcalendarcontrol(this);").Length)
                    strHtml = strHtml.Replace(straux, "")
                End If
            End If

            Return strHtml
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Dim WithEvents WP As System.ComponentModel.BackgroundWorker

    Private Sub WP_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles WP.DoWork
        Try
            '            LoadAllLists()
            If Me IsNot Nothing Then
                If Disposing = False AndAlso IsDisposed = False Then
                    Me.Invoke(New DLoadLists(AddressOf LoadAllLists))
                End If
            End If
            '            Me.LoadLists()
            '           Me.LoadList1()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Private Shared _docTypeToFind As Int64
    Public Shared Function HasDocType(ByVal task As Result) As Boolean
        Return task.DocTypeId = _docTypeToFind
    End Function

    ''' <summary>
    ''' [AlejandroR 28-04-2011] Created
    ''' Refresca la tabla de asociados en un formulario para el DocTypeId pasado como parametro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshAsociatedTable(ByVal DocTypeId As Long)
        Dim docTypesIDs As New List(Of String)
        docTypesIDs.Add(DocTypeId)
        Dim Asociated As DataTable = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(localResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)), docTypesIDs)
        Dim Mydoc As HtmlDocument = AxWebBrowser1.Document

        Dim Table As HtmlElement = Mydoc.GetElementById("zamba_associated_documents_" + DocTypeId.ToString())

        If Not IsNothing(Table) Then
            LoadTable(Table, Mydoc, False, localResult, Asociated)
        End If
    End Sub

    ''' <summary>
    ''' [Sebastian 09-06-2009] Modified Cargar asociados si esta insertado
    ''' </summary>
    ''' <remarks></remarks>
    Delegate Sub DLoadLists()
    ''' <summary>
    ''' Agrega los valores a las listas
    ''' </summary>
    ''' <history>
    '''     Marcelo 28/11/2010  Modified
    '''     Javier  22/10/2010  Modified
    '''</history>
    ''' <remarks></remarks>

    Private Sub LoadAllLists()

        If (Not Me.AxWebBrowser1.Document Is Nothing AndAlso Not Me.AxWebBrowser1.Document.ActiveElement Is Nothing AndAlso Me.AxWebBrowser1.Document.ActiveElement.Id Is Nothing) OrElse (Not Me.AxWebBrowser1.Document Is Nothing AndAlso Not Me.AxWebBrowser1.Document.ActiveElement Is Nothing AndAlso Not Me.AxWebBrowser1.Document.ActiveElement.TagName Is Nothing AndAlso String.Compare(Me.AxWebBrowser1.Document.ActiveElement.TagName, "body", True)) Then

            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            'Primero cargo las listas, despues lo asociados
            Me.LoadLists()
            Me.LoadList1()

            Dim doc1 As System.Windows.Forms.HtmlDocument ' mshtml.HTMLDocumentClass
            doc1 = Me.AxWebBrowser1.Document

            Try
                '[Sebastian 09-06-2009] se agrego condicion para que al momento de cargar los asociados, solo
                'lo haga si ya se inserto el form virtual.
                If Boolean.Parse(UserPreferences.getValue("LoadAsociatedResultsComponents", Sections.FormPreferences, "True")) = True AndAlso localResult.ID <> 0 Then

                    Dim Mydoc As HtmlDocument = AxWebBrowser1.Document

                    If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_associated_documents") = True Or Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                        '******Obtengo todos los docTypesIDs a ser usados asi solo obtengo esos asociados en vez de todos
                        Dim elements As List(Of String)
                        Dim blnAll As Boolean
                        Dim docTypesIds As New List(Of String)
                        Dim AsociatedTable As HtmlElement = Mydoc.GetElementById("zamba_associated_documents")

                        If Not IsNothing(AsociatedTable) Then
                            Dim tags As String = AsociatedTable.Name

                            If String.IsNullOrEmpty(tags) = False AndAlso tags.ToLower().StartsWith("doc_type_ids(") Then
                                Dim doc_types_ids As String = tags.Replace("doc_type_ids(", "").Replace(")", "")

                                For Each docTypeID As String In doc_types_ids.Split(",")
                                    If docTypesIds.Contains(docTypeID) = False Then
                                        docTypesIds.Add(docTypeID)
                                    End If
                                Next
                            Else
                                blnAll = True
                            End If
                        Else
                            AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_WF")

                            If Not IsNothing(AsociatedTable) Then
                                blnAll = True
                            End If
                        End If

                        If blnAll = True Then
                            docTypesIds.Clear()
                        Else
                            elements = getItems("zamba_associated_documents_")

                            For Each str As String In elements
                                Dim docTypeID As String = str.Replace("zamba_associated_documents_", "")

                                If docTypesIds.Contains(docTypeID) = False Then
                                    docTypesIds.Add(docTypeID)
                                End If
                            Next

                            If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                Dim lastDocTypeID As String

                                elements = getItems("zamba_asoc")
                                For Each Str As String In elements
                                    If Str.Contains("index") Then
                                        Dim values As String() = Str.Replace("zamba_asoc_", "").Split("_")
                                        Dim docTypeID As String = values(0)

                                        If lastDocTypeID <> docTypeID Then
                                            lastDocTypeID = docTypeID
                                            If docTypesIds.Contains(docTypeID) = False Then
                                                docTypesIds.Add(docTypeID)
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        Dim Asociated As DataTable = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(localResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)), docTypesIds)

                        If Not IsNothing(Asociated) AndAlso Asociated.Rows.Count > 0 Then
                            '******Cargo los elementos de los asociados
                            AsociatedTable = Mydoc.GetElementById("zamba_associated_documents")

                            If Not IsNothing(AsociatedTable) Then
                                Dim tags As String = AsociatedTable.Name

                                If String.IsNullOrEmpty(tags) = False AndAlso tags.ToLower().StartsWith("doc_type_ids(") Then
                                    Dim doc_types_ids As String = tags.Replace("doc_type_ids(", "").Replace(")", "")

                                    Asociated.DefaultView.RowFilter = "doc_type_id=" & doc_types_ids

                                    LoadTable(AsociatedTable, Mydoc, False, localResult, Asociated)
                                Else
                                    LoadTable(AsociatedTable, Mydoc, False, localResult, Asociated)
                                End If
                            Else
                                AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_WF")

                                If Not IsNothing(AsociatedTable) Then
                                    LoadTable(AsociatedTable, Mydoc, True, localResult, Asociated)
                                End If
                            End If

                            elements = getItems("zamba_associated_documents_")

                            For Each str As String In elements
                                Dim docTypeID As String = str.Replace("zamba_associated_documents_", "")
                                Dim number As Int64

                                If Int64.TryParse(docTypeID, number) Then
                                    Dim Table As HtmlElement = Mydoc.GetElementById(str)

                                    If Not IsNothing(Table) Then
                                        Asociated.DefaultView.RowFilter = "doc_type_id=" & number
                                        LoadTable(Table, Mydoc, False, localResult, Asociated.DefaultView.ToTable())
                                    End If
                                End If
                            Next

                            If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                Dim res As DataRow
                                Dim docTypeTable As DataTable
                                Dim lastDocTypeID As String

                                elements = getItems("zamba_asoc")
                                For Each Str As String In elements
                                    If Str.Contains("index") Then
                                        Dim values As String() = Str.Replace("zamba_asoc_", "").Split("_")
                                        Dim docTypeID As String = values(0)
                                        Dim indexName As String = values(2)
                                        Dim indexID As Int64
                                        If Int64.TryParse(indexName, indexID) = False Then
                                            indexID = IndexsBusiness.GetIndexIdByName(indexName.Replace("_s", String.Empty).Replace("_n", String.Empty))
                                        End If

                                        Dim indice As Index = ZCore.GetIndex(indexID)

                                        If lastDocTypeID <> docTypeID Then
                                            lastDocTypeID = docTypeID

                                            Asociated.DefaultView.RowFilter = "doc_type_id=" & docTypeID
                                            docTypeTable = Asociated.DefaultView.ToTable()

                                            If docTypeTable.Rows.Count > 0 Then
                                                res = docTypeTable.Rows(0)
                                            Else
                                                res = Nothing
                                            End If
                                        End If

                                        If Not IsNothing(res) Then
                                            If indice.DropDown = IndexAdditionalType.AutoSustitución Then
                                                If docTypeTable.Columns.Contains(indice.Name) AndAlso docTypeTable.Columns.Contains("I" & indice.ID) Then
                                                    If Not IsDBNull(res("I" & indice.ID)) Then
                                                        indice.Data = res("I" & indice.ID)
                                                    End If
                                                    If Not IsDBNull(res(indice.Name)) Then
                                                        indice.dataDescription = res(indice.Name)
                                                    End If
                                                End If
                                            Else
                                                If docTypeTable.Columns.Contains(indice.Name) Then
                                                    If Not IsDBNull(res(indice.Name)) Then
                                                        indice.Data = res(indice.Name)
                                                    End If
                                                End If
                                            End If

                                            Dim Element As HtmlElement = doc1.GetElementById(Str)

                                            If Not IsNothing(Element) Then
                                                AsignValue(indice, Element)
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
                doc1.InvokeScript("ZFUNCTION")
            Catch ex As Exception
                ZClass.raiseerror(ex)
                'Finally
                'AxWebBrowser1.ResumeLayout()
            End Try
            Me.Cursor = cur
        End If
    End Sub

    Private Function getItems(ByVal elementName As String) As List(Of String)
        Dim elements As New List(Of String)
        Dim Mydoc As HtmlDocument = AxWebBrowser1.Document

        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim body As String = Mydoc.Body.InnerHtml.ToLower()

                While body.Contains(elementName)
                    Dim index As Int32 = body.IndexOf(elementName)
                    Dim elem As String = body.Substring(index)
                    elem = elem.Substring(0, elem.IndexOf(" ")).Replace(Chr(34), "")

                    If elem.Contains(">") Then
                        elem = elem.Substring(0, elem.IndexOf(">"))
                    End If
                    elements.Add(elem)

                    body = body.Substring(index)
                    body = body.Replace(elem, "")
                End While
            End If
        End If
        Return elements
    End Function

    Private Sub LoadImage(ByRef myResult As Result, ByVal E As HtmlElement)
        Try
            If myResult.ISVIRTUAL = False AndAlso myResult.FullPath <> String.Empty AndAlso Not IsNothing(E) Then
                Select Case CStr(E.TagName)
                    Case "IMG"
                        E.DomElement.src = myResult.FullPath
                        '                    doc1.all.item(i).Src()
                End Select
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Cargo el valor del indice al elemento html
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="E"></param>
    ''' <history> Marcelo 31/07/08 Modified</history>
    '''     [Tomas] 21/09/2009  Modified    Se agrego una validación cuando carga los valores 
    '''                                     de los textbox y textareas para que si el campo Data 
    '''                                     del indice se encuentra vacio y el text se encuentre 
    '''                                     con datos (caso del estar completando datos por primera  
    '''                                     vez) estos datos no se pierdan.
    ''' <remarks></remarks>
    Private Sub AsignValue(ByVal I As Index, ByVal E As HtmlElement)
        Try
            Try
                Trace.WriteLineIf(ZTrace.IsVerbose, "Id: " & E.Id & " Tag: " & CStr(E.TagName).ToLower)


            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                If I.AutoIncremental = True Then
                    E.SetAttribute("ReadOnly", "True")
                    E.SetAttribute("Value", I.Data)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            'Se limita la cantidad de caracteres a ingresar en base al indice
            E.SetAttribute("maxlength", I.Len)

            ' Filtra por tipo de control...
            Select Case CStr(E.TagName).ToLower
                Case "input" ', "SELECT"
                    Select Case CStr(E.DomElement.type).ToLower
                        'Select Case CStr(DirectCast(E.DomElement, mshtml.HTMLInputElementClass).type).ToLower
                        Case "text", "hidden"

                            WriteDataIndexTrace(I, True)

                            If I.DropDown = IndexAdditionalType.AutoSustitución Then
                                If I.Data = Nothing Then
                                    'Verifica si en el objeto existe algún valor o no
                                    If IsNothing(E.DomElement.value) Then
                                        E.DomElement.value = ""
                                        'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = ""
                                    End If
                                Else
                                    E.DomElement.value = I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID)
                                End If

                            Else

                                If I.Data = Nothing Then
                                    'Verifica si en el objeto existe algún valor o no
                                    E.DomElement.value = ""
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = ""
                                Else
                                    E.DomElement.value = I.Data
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data
                                End If
                            End If

                            Try
                                If Not IsNothing(E.DomElement) AndAlso Not IsNothing(E.DomElement.value) Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Value= " & E.DomElement.value.ToString)
                                End If
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            WriteDataIndexTrace(I, True)

                        Case "checkbox"

                            WriteDataIndexTrace(I, True)

                            If IsNothing(I.Data) OrElse I.Data = "0" OrElse I.Data = String.Empty Then
                                E.DomElement.checked = 0
                                'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = CBool(0)
                            Else
                                E.DomElement.checked = 1
                                'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = CBool(1)
                            End If

                            Try

                                Trace.WriteLineIf(ZTrace.IsVerbose, "Value " & E.DomElement.checked.ToString)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            WriteDataIndexTrace(I, True)

                        Case "radio"

                            WriteDataIndexTrace(I, True)

                            If IsNothing(I.Data) Then
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es otra cosa, Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            ElseIf I.Data = "0" Then

                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es nothing, es 0, o empty, Valor del checked N= " & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("N") Then
                                    E.DomElement.checked = True
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = True
                                ElseIf E.Id.ToUpper().EndsWith("S") = True Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked N= " & E.Id & " " & E.DomElement.checked.ToString)

                            ElseIf I.Data = "1" Then
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es 1")
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = True
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = True
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            Else
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es otra cosa")
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            End If

                            Try
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Id " & E.Id)
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Value " & E.DomElement.checked.ToString)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            WriteDataIndexTrace(I, False)

                        Case "select-one"

                            WriteDataIndexTrace(I, False)

                            If IsNothing(I.Data) Then
                                E.DomElement.value = I.Data
                                'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data
                            Else
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = ""
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = ""
                                End If
                            End If

                            Try
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Id " & E.Id & " " & E.DomElement.value.ToString)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            WriteDataIndexTrace(I, False)
                    End Select
                Case "select"

                    WriteDataIndexTrace(I, False)

                    If IsNothing(I.Data) = False OrElse I.Data <> "0" OrElse I.Data <> String.Empty Then
                        Select Case CInt(I.DropDown)
                            Case 1
                                'Andres 8/8/07 - Se guarda el valor pero no se usa 
                                'Dim Lista As ArrayList
                                If Reload = False Then
                                    'Lista = Indexs_Factory.retrieveArraylist(indice.ID)

                                    'Si no esta cargada, cargo solo el item seleccionado
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data
                                        E.AppendChild(tag)

                                    Else

                                        Dim ListItem As New ListItem(I, E.Id)
                                        Me.ListToLoad1.Add(ListItem)
                                    End If
                                End If
                            Case 2
                                If Reload = False Then
                                    'Si no esta cargada, cargo solo el item seleccionado
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        Dim desc As String = AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data & " - " & desc
                                        E.AppendChild(tag)
                                    Else
                                        Dim ListItem As New ListItem(I, E.Id)
                                        Me.ListToLoad.Add(ListItem)
                                    End If
                                End If
                                If Not E.Children(I.Data) Is Nothing Then E.Children(I.Data).SetAttribute("selected", True)
                        End Select
                    End If

                    Try
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Id " & E.Id)
                        If Not IsNothing(E.DomElement) AndAlso Not IsNothing(E.DomElement.value) Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Value " & E.DomElement.value.ToString)
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    WriteDataIndexTrace(I, False)

                Case "textarea"

                    WriteDataIndexTrace(I, False)

                    If Not IsNothing(I.Data) Then
                        E.SetAttribute("value", I.Data)
                    Else
                        'Verifica si en el objeto existe algún valor o no
                        If IsNothing(E.DomElement.value) Then
                            E.SetAttribute("value", "")
                        End If
                    End If

                    Try
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Id " & E.Id)
                        If Not IsNothing(E.DomElement) AndAlso Not IsNothing(E.DomElement.value) Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Value " & E.DomElement.value.ToString)
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    WriteDataIndexTrace(I, False)
            End Select
        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Cargo el valor del indice al elemento html
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="E"></param>
    ''' <history></history>
    '''     [Tomas] 21/09/2009  Modified    Se agrego una validación cuando carga los valores 
    '''                                     de los textbox y textareas para que si el campo Data 
    '''                                     del indice se encuentra vacio y el text se encuentre 
    '''                                     con datos (caso del estar completando datos por primera  
    '''                                     vez) estos datos no se pierdan.
    ''' <remarks></remarks>
    Private Sub AsignVarValue(ByVal VarName As String, ByVal E As HtmlElement)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Id: " & E.Id)
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Tag: " & CStr(E.TagName).ToLower)

            ' Filtra por tipo de control...
            Select Case CStr(E.TagName).ToLower
                Case "input" ', "SELECT"
                    Select Case CStr(E.DomElement.type).ToLower
                        Case "text", "hidden"
                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = ""
                                End If
                            Else
                                E.DomElement.value = VariablesInterReglas.Item(VarName)
                            End If
                        Case "checkbox"
                            Try
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor variable = " & VariablesInterReglas.Item(VarName))
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try

                            If IsNothing(VariablesInterReglas.Item(VarName)) OrElse VariablesInterReglas.Item(VarName) = "0" OrElse VariablesInterReglas.Item(VarName) = String.Empty Then
                                E.DomElement.checked = 0
                            Else
                                E.DomElement.checked = 1
                            End If
                        Case "radio"
                            Try
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor variable = " & VariablesInterReglas.Item(VarName))
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try

                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es otra cosa")

                                If E.Id.ToUpper().EndsWith(")") Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                End If
                            ElseIf String.Compare(Trim(VariablesInterReglas.Item(VarName).ToString), "0") = 0 Then
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es nothing, es 0, o empty")
                                If E.Id.ToUpper().EndsWith(")") Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked = " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked = " & E.Id & " " & E.DomElement.checked)
                                End If
                            ElseIf Trim(CType(VariablesInterReglas.Item(VarName), String)) = "1" Then
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es 1")
                                If E.Id.ToUpper().EndsWith(")") Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked =" & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = True
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked =" & E.Id & " " & E.DomElement.checked)
                                End If
                            Else
                                Trace.WriteLineIf(ZTrace.IsVerbose, "El Data es otra cosa")

                                If E.Id.ToUpper().EndsWith(")") Then
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                End If
                            End If
                        Case "select-one"
                            Try
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor variable" & VariablesInterReglas.Item(VarName))
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                E.DomElement.value = VariablesInterReglas.Item(VarName)
                            Else
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = ""
                                End If
                            End If
                    End Select
                Case "select"
                    Try
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Valor variable" & VariablesInterReglas.Item(VarName))
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    If IsNothing(VariablesInterReglas.Item(VarName)) = False OrElse VariablesInterReglas.Item(VarName) <> "0" OrElse VariablesInterReglas.Item(VarName) <> String.Empty Then
                        If Reload = False Then
                            'Si no esta cargada, cargo solo el item seleccionado
                            Dim readonli As String = E.GetAttribute("ReadOnly")

                            If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                Dim tag As HtmlElement = E.Document.CreateElement("option")

                                tag.SetAttribute("selected", VariablesInterReglas.Item(VarName))
                                tag.SetAttribute("value", VariablesInterReglas.Item(VarName))
                                tag.InnerText = VariablesInterReglas.Item(VarName)
                                E.AppendChild(tag)
                            End If
                        End If
                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Id " & E.Id)
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Value " & E.DomElement.value)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "textarea"
                    Try
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Id " & E.Id)
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Value " & E.DomElement.value)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    Try
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Valor variable" & VariablesInterReglas.Item(VarName))
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    If Not IsNothing(VariablesInterReglas.Item(VarName)) Then
                        E.SetAttribute("value", VariablesInterReglas.Item(VarName))
                    Else
                        E.SetAttribute("value", "")
                    End If
                Case "table"
                    'Valido si existe el nodo TBODY , para usarlo para las rows 
                    If E.Children.Count > 0 Then
                        For Each child As HtmlElement In E.Children
                            If String.Compare(child.TagName.ToLower(), "tbody") = 0 Then
                                E = child
                                Exit For
                            End If
                        Next
                    End If

                    If (Not IsNothing(E)) Then
                        If Not IsNothing(E.InnerHtml) Then
                            E.InnerText = String.Empty
                        End If
                        Dim dt As Object = VariablesInterReglas.Item(VarName)

                        If Not IsNothing(dt) Then
                            If Not IsNothing(E.Id) AndAlso String.IsNullOrEmpty(E.Id) = False Then
                                Dim dt2 As DataTable = New DataTable

                                dt2.Columns.Add(New DataColumn("Ejecutar"))

                                If (TypeOf (dt) Is DataSet) Then
                                    dt2.Merge(DirectCast(dt, DataSet).Tables(0))
                                Else
                                    dt2.Merge(dt)
                                End If

                                LoadTableHeader(E, dt2.Columns, AxWebBrowser1.Document)
                                LoadTableVarBody(E, dt2.Rows, AxWebBrowser1.Document)
                            Else
                                If (TypeOf (dt) Is DataSet) Then
                                    LoadTableHeader(E, DirectCast(dt, DataSet).Tables(0).Columns, AxWebBrowser1.Document)
                                    LoadTableVarBody(E, DirectCast(dt, DataSet).Tables(0).Rows, AxWebBrowser1.Document)
                                Else
                                    LoadTableHeader(E, DirectCast(dt, DataTable).Columns, AxWebBrowser1.Document)
                                    LoadTableVarBody(E, DirectCast(dt, DataTable).Rows, AxWebBrowser1.Document)
                                End If
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Escribe el trace del valor Data de un indice
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="withDataTemp">Especifica si se desea escribir el trace de la propiedad DataTemp</param>
    ''' <history>
    '''     [Tomas] 21/09/2009  Created
    ''' </history>
    ''' <remarks>Se crea simplemente mejorar la lectura del método AsignValue y su mantenimiento</remarks>
    Private Sub WriteDataIndexTrace(ByVal I As Index, ByVal withDataTemp As Boolean)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Indice en DATA = " & I.Data)
            If withDataTemp Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Valor Indice en DATATEMP = " & I.DataTemp)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private ListToLoad As New Generic.List(Of ListItem)
    Private ListToLoad1 As New Generic.List(Of ListItem)

    Private Class ListItem
        Public Index As Index
        Public ElementId As String

        Public Sub New(ByVal Index As Index, ByVal ElementId As String)
            Me.Index = Index
            Me.ElementId = ElementId
        End Sub
    End Class

    Private Sub LoadList1()

        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo Lista " & Now.ToString)
            If ListToLoad1.Count > 0 Then
                Dim doc1 As System.Windows.Forms.HtmlDocument ' mshtml.HTMLDocumentClass
                doc1 = Me.AxWebBrowser1.Document

                Dim e As HtmlElement = Nothing
                If Not IsNothing(doc1) Then
                    For Each listitem As ListItem In ListToLoad1
                        Dim tagName As String = ""
                        Dim first As Boolean = True
                        Dim List As New ArrayList
                        List = IndexsBusiness.retrieveArraylist(listitem.Index.ID)

                        If first Then
                            first = False
                            e = doc1.GetElementById(listitem.ElementId)
                        End If
                        If Not e Is Nothing AndAlso e.Children.Count <> List.Count = True Then
                            For Each str As String In List
                                'Dim item As HtmlElement
                                'item = E.Document.CreateElement("option")
                                Dim tag As HtmlElement = doc1.CreateElement("option")

                                If String.Compare(listitem.Index.Data.Trim, str.Trim) = 0 Then
                                    tag.SetAttribute("selected", str.Trim)
                                End If
                                tag.SetAttribute("value", str.Trim)
                                tag.InnerText = str.Trim
                                e.AppendChild(tag)

                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Me.ListToLoad1.Clear()
        End Try
    End Sub
    Private Sub LoadLists()
        Try
            Me.AxWebBrowser1.SuspendLayout()
            Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo Lista " & Now.ToString)
            If ListToLoad.Count > 0 Then
                Dim doc1 As System.Windows.Forms.HtmlDocument ' mshtml.HTMLDocumentClass
                doc1 = Me.AxWebBrowser1.Document
                Dim i As Int64
                Dim e As HtmlElement = Nothing
                If Not IsNothing(doc1) Then
                    For Each listitem As ListItem In ListToLoad
                        Dim tagName As String = ""
                        Dim table As DataTable = AutoSubstitutionBusiness.GetIndexData(listitem.Index.ID, False)

                        e = doc1.GetElementById(listitem.ElementId)

                        If e Is Nothing Then
                            Exit For
                        End If
                        If e.Children.Count = table.Rows.Count = True Then
                            Exit Try
                        End If

                        For i = 0 To table.Rows.Count - 1

                            Dim tag As HtmlElement = doc1.CreateElement("option")
                            Dim optionCode As String = Convert.ToString(table.Rows(i).Item(0)).Trim
                            If Not String.IsNullOrEmpty(optionCode) Then
                                tag.SetAttribute("id", optionCode)
                            End If
                            Dim optionValue As String = Convert.ToString(table.Rows(i).Item(1)).Trim
                            tagName = String.Concat(optionCode, " - ", optionValue)

                            If e.OuterHtml.Contains(tagName) = False Then
                                If String.Compare(listitem.Index.Data.Trim, optionCode) = 0 Then
                                    tag.SetAttribute("selected", optionCode)
                                    tag.SetAttribute("value", optionCode)
                                End If
                                tag.InnerText = tagName
                                ' agrega el tag a el HTMLDocument...
                                e.AppendChild(tag)
                            End If
                        Next
                        'e.OuterHtml = "<!-- Frutah -->"
                        'e.OuterText = "<!-- Frutat -->"
                    Next
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Me.ListToLoad.Clear()
            Me.AxWebBrowser1.ResumeLayout()
        End Try
    End Sub

    ''' <summary>
    '''Carga el contenido de un DataTabla en una tabla Html del documento
    ''' </summary>
    ''' <param name="tableId"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Javier  22/10/2010  Modified    Se modifica llamada a ParseResult. Se le pasa el padre
    '''</history>
    Private Sub LoadTable(ByVal table As HtmlElement, ByRef mydoc As HtmlDocument, ByVal onlyWF As Boolean, ByVal ParentResult As IResult, ByVal AsociatedResults As DataTable)
        Try
            If Not IsNothing(table) AndAlso AsociatedResults.Rows.Count > 0 Then
                'Valido si existe el nodo TBODY , para usarlo para las rows 
                If table.Children.Count > 0 Then
                    For Each child As HtmlElement In table.Children
                        If String.Compare(child.TagName.ToLower(), "tbody") = 0 Then
                            table = child
                            Exit For
                        End If
                    Next
                End If

                If (Not IsNothing(table)) Then
                    If Not IsNothing(table.InnerHtml) Then
                        table.InnerText = String.Empty
                    End If
                    Dim dt As DataTable
                    If Not IsNothing(table.Id) Then
                        dt = ParseResult(ParentResult, AsociatedResults, onlyWF, table.Id)
                    Else
                        dt = ParseResult(ParentResult, AsociatedResults, onlyWF, String.Empty)
                    End If
                    LoadTableHeader(table, dt.Columns, mydoc)
                    LoadTableBody(table, dt.Rows, mydoc)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Carga las columnas header de un DataTable en el una tabla Html del documento
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="dcs"></param>
    ''' <remarks></remarks>
    Private Sub LoadTableHeader(ByRef table As HtmlElement, ByVal dcs As DataColumnCollection, ByRef mydoc As HtmlDocument)
        Dim HeaderRow As HtmlElement = mydoc.CreateElement("tr")

        Dim HeaderColumn As HtmlElement = Nothing

        'Agrego columnas de indices
        For Each Column As DataColumn In dcs
            If String.Compare(Column.ColumnName.ToLower(), "iddoc") = 0 AndAlso Boolean.Parse(UserPreferences.getValue("ResultId", Sections.FormPreferences, "True") = False) Then
            ElseIf String.Compare(Column.ColumnName.ToLower(), "doctypeid") = 0 AndAlso Boolean.Parse(UserPreferences.getValue("DoctypeId", Sections.FormPreferences, "True") = False) Then
            ElseIf String.Compare(Column.ColumnName.ToLower(), "ruta documento") = 0 AndAlso Boolean.Parse(UserPreferences.getValue("RutaDocumento", Sections.FormPreferences, "False") = False) Then
            Else
                HeaderColumn = mydoc.CreateElement("th")
                HeaderColumn.InnerHtml = Column.ColumnName

                HeaderRow.AppendChild(HeaderColumn)
            End If
        Next
        table.AppendChild(HeaderRow)
    End Sub

    ''' <summary>
    ''' Carga el contenido de un DataTabla en el body de una tabla Html del documento
    ''' </summary>
    ''' <param name="table">Tabla HTML donde se cargaran los datos</param>
    ''' <param name="drs">Rows que van a ser cargadas</param>
    ''' <param name="mydoc">Documento HTML que contiene la tabla</param>
    ''' <remarks></remarks>
    Private Sub LoadTableBody(ByRef table As HtmlElement, ByVal drs As DataRowCollection, ByRef mydoc As HtmlDocument)
        Dim CurrentRow As HtmlElement = Nothing
        Dim CurrentCell As HtmlElement = Nothing
        Dim i As Int32

        For Each dr As DataRow In drs
            CurrentRow = mydoc.CreateElement("tr")
            i = 0
            Dim cont As Int32 = dr.ItemArray.Length - 1

            For Each CellValue As Object In dr.ItemArray
                If i = dr.ItemArray.Length - 1 AndAlso Boolean.Parse(UserPreferences.getValue("ResultId", Sections.FormPreferences, "True") = False) Then
                ElseIf i = dr.ItemArray.Length - 2 AndAlso Boolean.Parse(UserPreferences.getValue("DoctypeId", Sections.FormPreferences, "True") = False) Then
                ElseIf i = dr.ItemArray.Length - 3 AndAlso Boolean.Parse(UserPreferences.getValue("RutaDocumento", Sections.FormPreferences, "False") = False) Then
                Else
                    CurrentCell = mydoc.CreateElement("td")
                    '(pablo) 01-03-2011
                    If CellValue.GetType.FullName.ToString = "System.DateTime" Then
                        If Not IsDBNull(CellValue) Then
                            Dim dateValue As String
                            dateValue = CellValue
                            If dateValue.Length <= 10 Then
                                CurrentCell.InnerHtml = dateValue.ToString()
                            End If
                        End If
                    Else
                        CurrentCell.InnerHtml = CellValue.ToString()
                    End If

                    CurrentRow.AppendChild(CurrentCell)
                End If

                i = i + 1
            Next

            table.AppendChild(CurrentRow)
        Next
    End Sub

    ''' <summary>
    ''' Carga el contenido de un DataTabla en el body de una tabla Html del documento
    ''' </summary>
    ''' <param name="table">Tabla HTML donde se cargaran los datos</param>
    ''' <param name="drs">Rows que van a ser cargadas</param>
    ''' <param name="mydoc">Documento HTML que contiene la tabla</param>
    ''' <param name="withBtn">Agregar o no el Boton Ver</param>
    ''' <history>   Marcelo 06/01/10 Created</history>
    ''' <remarks></remarks>
    Private Sub LoadTableVarBody(ByRef table As HtmlElement, ByVal drs As DataRowCollection, ByRef mydoc As HtmlDocument)
        Dim CurrentRow As HtmlElement = Nothing
        Dim CurrentCell As HtmlElement = Nothing
        Dim i As Int32
        Dim textItem2, textAux As String
        Dim itemNum As Int32

        Dim items As Array
        If Not IsNothing(table.Id) AndAlso String.IsNullOrEmpty(table.Id) = False Then
            items = table.Id.Split("/")
        End If
        Dim zvarItems As Array
        Dim params As String

        Dim InnerHtml As StringBuilder = New StringBuilder()
        For Each dr As DataRow In drs
            CurrentRow = mydoc.CreateElement("tr")

            If Not IsNothing(items) Then

                InnerHtml.Append("<INPUT id=")
                InnerHtml.Append(Chr(34))

                'Si tiene zvar
                If items.Length > 2 Then

                    params = String.Empty

                    textItem2 = items(2).ToString()
                    InnerHtml.Append(items(0) + "_")

                    While String.IsNullOrEmpty(textItem2) = False
                        textAux = textItem2.Remove(0, 5)
                        zvarItems = textAux.Remove(textAux.IndexOf(")")).Split("=")
                        textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1)

                        If Int32.TryParse(zvarItems(1).ToString(), itemNum) = False Then
                            If zvarItems(1).ToString().ToLower().Contains("length") Then
                                itemNum = dr.ItemArray.Length - Int32.Parse(zvarItems(1).ToString().Split("-")(1))
                            End If
                        End If
                        InnerHtml.Append("zvar(" + zvarItems(0).ToString() + "=" + dr.ItemArray(itemNum).ToString() + ")")

                        params = params & "'" & dr.ItemArray(itemNum).ToString() & "',"
                    End While
                Else
                    InnerHtml.Append(items(0))
                End If

                InnerHtml.Append(Chr(34))
                InnerHtml.Append(" type=button onclick=")

                'Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                'sino se llama a SetRuleId por default
                If items.Length > 3 Then
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append(items(3) & "(this, ")
                    InnerHtml.Append(params.Substring(0, params.Length - 1))
                    InnerHtml.Append(");")
                    InnerHtml.Append(Chr(34))
                Else
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append("SetRuleId(this);")
                    InnerHtml.Append(Chr(34))
                End If

                InnerHtml.Append(" value = ")
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(items(1))
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(" Name = ")
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(items(0))
                InnerHtml.Append(Chr(34))
                InnerHtml.Append(" >")

                CurrentCell = mydoc.CreateElement("td")
                CurrentCell.InnerHtml = InnerHtml.ToString()
                CurrentRow.AppendChild(CurrentCell)
                InnerHtml.Remove(0, InnerHtml.Length)
            End If

            For i = 0 To dr.ItemArray.Length - 1
                'Salteo el 1er ciclo x el btn
                If i > 0 Or String.IsNullOrEmpty(table.Id) Then
                    CurrentCell = mydoc.CreateElement("td")
                    CurrentCell.InnerHtml = dr.ItemArray(i).ToString()
                    CurrentRow.AppendChild(CurrentCell)
                End If
            Next
            table.AppendChild(CurrentRow)
        Next
    End Sub
#End Region

#Region "Save"
    Public Sub SaveDocument()
        Try
            AxWebBrowser1.ShowSaveAsDialog()
        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Ejecuta la regla seleccionada en el formulario
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub ExecuteRule(ByVal ruleId As Int64, ByVal result As Result)


        If UserActionDisabledRules.Contains(ruleId) Then

            If UserPreferences.getValue("ShowMsgIfExecDisabledRuleInForm", Sections.WorkFlow, True) Then
                MessageBox.Show("Usted no dispone de los permisos necesarios para ejecutar la accion seleccionada.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Else

            Dim StepId As Int64

            StepId = WFRulesBusiness.GetWFStepIdbyRuleID(ruleId)


            Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndStepIdAAndDocTypeId(result.ID, StepId, result.DocTypeId, 0)

            If IsNothing(task) Then
                task = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(result.ID, 0)
            End If

            Dim tasks As New List(Of ITaskResult)(1)
            tasks.Add(task)

            Trace.WriteLineIf(ZTrace.IsVerbose, "*******************************************************************")
            Trace.WriteLineIf(ZTrace.IsVerbose, "Executing Rule: " & ruleId & "  with results count: " & tasks.Count)
            Dim WFRS As New WFRulesBusiness
            WFRS.ExecuteRule(ruleId, StepId, tasks)
            Trace.WriteLineIf(ZTrace.IsVerbose, "*******************************************************************")

            '[sebastian 04-03-2009]
            'De esta forma le asigno los valores de los indices al formulario. Realizo un refresco
            Me.localResult.Indexs = task.Indexs
            Me.localResult.DocType.Indexs = task.Indexs
            AsignValues(Me.AxWebBrowser1.Document, Me.localResult)

        End If


    End Sub

    Public Sub CloseWebBrowser()
        Try

            Try
                If IsNothing(AxWebBrowser1) = False Then AxWebBrowser1.Navigate("about:blank")
            Catch ex As System.Runtime.InteropServices.COMException
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                If IsNothing(AxWebBrowser1) = False Then
                    AxWebBrowser1.Dispose()
                    AxWebBrowser1 = Nothing
                End If
            Catch ex As System.Runtime.InteropServices.COMException
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                Me.Dispose(True)
            Catch ex As System.Runtime.InteropServices.COMException
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Enum Exec
        OLECMDID_OPTICAL_ZOOM = 63
    End Enum

    Private Enum ExecOpt
        OLECMDEXECOPT_DODEFAULT = 0
        OLECMDEXECOPT_PROMPTUSER = 1
        OLECMDEXECOPT_DONTPROMPTUSER = 2
        OLECMDEXECOPT_SHOWHELP = 3
    End Enum

    ''' <summary>
    ''' Método que sirve para imprimir el formulario virtual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	18/02/2009	Modified    Al imprimir el documento virtual desaparecen los botones, el encabezado y pie de página
    '''     [Gaston]	20/02/2009	Modified    Se elimino el retorno a los valores originales de header y footer. Ahora eso se agrego antes de 
    '''                                         cerrar el cliente
    '''     [Tomás]     23/02/2009  Modified    Se muestra un cuadro de diálogo donde el usuario decide si modifica
    '''                                         o no el registro para no mostrar los botones, etc...
    ''' </history>
    Public Sub Print()

        Dim doc As HtmlDocument
        Dim oKey As RegistryKey
        Dim elemCollection As HtmlElementCollection

        Try
            doc = AxWebBrowser1.Document
            ' Se obtienen los elementos con la etiqueta HTML especificada, en este caso INPUT
            elemCollection = doc.GetElementsByTagName("INPUT")
            ' Los elementos de tipo button se ocultan para que cuando se imprima el formulario virtual no aparezcan los botones
            setButtonsAs(elemCollection, "hidden")

            Try

                If (AreHeaderAndFooterEmpty() = False) Then
                    ' Se obtiene el valor que muestra o no el mensaje de confirmación para modificar el registro.
                    Dim valor As String = ZOptBusiness.GetValue("ShowMsgBox" + Environment.MachineName.ToString())

                    ' Se crean las claves por defecto en caso de ejecutarse por primera vez.
                    If String.IsNullOrEmpty(valor) Then
                        ' Se crea el valor que muestra el mensaje.
                        ZOptBusiness.Insert("ShowMsgBox" & Environment.MachineName.ToString(), "1")
                        ' Se crea el valor que recuerda la acción tomada (por defecto).
                        ZOptBusiness.Insert("ModifyRegistry" & Environment.MachineName.ToString(), "1")
                    End If

                    If String.IsNullOrEmpty(valor) Or valor = "1" Then
                        ' Se muestra el formulario para pedir acción.
                        Dim frmDialog As New Zamba.AdminControls.frmMsgBoxConChkBox( _
                            "Para que el formulario se imprima correctamente se deberá" & vbCrLf & _
                            "modificar el registro. Presione 'Aceptar' para modificarlo" & vbCrLf & _
                            "caso contrario presione 'Cancelar'.", "Confirmar acción", MessageBoxIcon.Question)

                        frmDialog.ShowDialog()

                        If frmDialog.DialogResult = DialogResult.OK Then
                            ' Se abre la subclave PageSetup, que contiene valores de configuración del documento html que se 
                            ' quiere imprimir. Se coloca True para indicar que se va a escribir en el registro
                            oKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\PageSetup", True)

                            If Not (IsNothing(oKey)) Then
                                ' Los valores header y footer se deshabilitan para que la página que se quiere imprimir no 
                                ' muestre el path al archivo, número de página y fecha actual
                                oKey.SetValue("header", "")
                                oKey.SetValue("footer", "")
                            End If

                            ' Se guarda la configuración.
                            ZOptBusiness.Update("ModifyRegistry" + Environment.MachineName.ToString(), "1")
                            Dim volverAMostrarMensaje As Boolean = frmDialog.volverAMostrarMensaje
                            If volverAMostrarMensaje Then
                                ZOptBusiness.Update("ShowMsgBox" & Environment.MachineName.ToString(), "1")
                            Else
                                ZOptBusiness.Update("ShowMsgBox" & Environment.MachineName.ToString(), "0")
                            End If

                        Else
                            ' Se guarda la configuración.
                            ZOptBusiness.Update("ModifyRegistry" + Environment.MachineName.ToString(), "0")
                            Dim volverAMostrarMensaje As Boolean = frmDialog.volverAMostrarMensaje
                            If volverAMostrarMensaje Then
                                ZOptBusiness.Update("ShowMsgBox" & Environment.MachineName.ToString(), "1")
                            Else
                                ZOptBusiness.Update("ShowMsgBox" & Environment.MachineName.ToString(), "0")
                            End If

                        End If

                        frmDialog.Dispose()

                    Else
                        ' Caso que no se muestre el form
                        ' Se obtiene el valor que muestra o no el mensaje de confirmación para modificar el registro.
                        Dim modificarRegistro As String = ZOptBusiness.GetValue("ModifyRegistry" + Environment.MachineName.ToString())
                        If modificarRegistro = "1" Then
                            ' Se abre la subclave PageSetup, que contiene valores de configuración del documento html que se quiere imprimir. Se coloca True para
                            ' indicar que se va a escribir en el registro
                            oKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\PageSetup", True)

                            If Not (IsNothing(oKey)) Then
                                ' Los valores header y footer se deshabilitan para que la página que se quiere imprimir no muestre el path al archivo, número de 
                                ' página y fecha actual
                                oKey.SetValue("header", "")
                                oKey.SetValue("footer", "")
                            End If

                        End If

                    End If

                End If

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            AxWebBrowser1.ShowPrintDialog()

            ' Los elementos de tipo button se muestran para que puedan visualizarse en la pantalla
            setButtonsAs(elemCollection, "visible")

        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            doc = Nothing
            oKey = Nothing
            elemCollection = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Verifica si los valores de header y footer contienen información o no.
    ''' </summary>
    ''' <returns>True si los valores se encuentran vacios</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 11/03/09]    Created
    ''' </history>
    Private Function AreHeaderAndFooterEmpty() As Boolean

        ' Se abre la subclave PageSetup, que contiene valores de configuración del documento html 
        ' que se quiere imprimir. Se coloca False para indicar que se va a leer del registro.
        Dim oKey As RegistryKey
        oKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\PageSetup", False)
        If String.IsNullOrEmpty(oKey.GetValue("header")) And String.IsNullOrEmpty(oKey.GetValue("footer")) Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Método que sirve para colocar los elementos html de tipo button en visible (en pantalla) o invisible (al imprimir)
    ''' </summary>
    ''' <param name="elemCollection">Colección de elementos cuya etiqueta es INPUT</param>
    ''' <param name="type">Visible para ver los botones o hidden para no verlos</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	18/02/2009	Created
    '''     [Gaston]	20/03/2009	Modified    Se agrego la validación "class=button"
    ''' </history>
    Private Sub setButtonsAs(ByRef elemCollection As HtmlElementCollection, ByVal type As String)

        ' Por cada elemento cuya etiqueta es INPUT
        For Each elem As HtmlElement In elemCollection

            ' Si el elemento es de tipo button
            If ((elem.OuterHtml.Contains("type=button")) OrElse (elem.OuterHtml.Contains("class=button"))) Then
                ' Se cambia a visible o a hidden, dependiendo del valor de type
                elem.Style = "VISIBILITY: " & type
            End If

        Next

    End Sub

    Protected Overrides Sub Finalize()
        Me.CloseWebBrowser()
        If Not IsNothing(AxWebBrowser1) Then
            Me.AxWebBrowser1.Dispose()
            Me.AxWebBrowser1 = Nothing
        End If
        Me.Dispose(False)
        MyBase.Finalize()
    End Sub

    Public Event RefreshIndexs(ByVal Result As IResult)
    Public Event ShowAsociatedResult(ByVal Result As Result)
    Public Event showYellowPanel()

    Private Function ValidateIndexsRequiredEmpty() As Boolean
        For Each index As Index In Me.localResult.Indexs

            If index.Required Then
                If index.Data = String.Empty OrElse index.DataTemp = String.Empty Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Método que sirve para guardar los índices (ubicados en el formulario) en la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/11/2008	Modified    Los índices obligatorios no pueden estar vacíos
    ''' 	[Gaston]	11/12/2008	Modified    Invocación al evento ShowYellowPanel
    '''     [Gaston]	12/03/2009	Modified    Validación de índices obligatorios o requeridos
    '''     [Ezequiel]	06/04/2009	Modified    Se inserta el formulario en el caso de que no se halla insertado aun
    '''     [Gaston]	08/05/2009	Modified    Validación de índices de tipo exceptuable
    '''     [Gaston]    18/05/2009  Modified    Inserción de índices con valores vacíos si es que no hay permisos de índices requeridos para dichos índices con valores vacíos
    '''     [Marcelo]   06/01/2010  Modified    Se agrega manejo de variables interreglas
    ''' </history>
    Sub RecoverIndexValues(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles AxWebBrowser1.Navigating
        Dim doc1 As System.Windows.Forms.HtmlDocument = Me.AxWebBrowser1.Document
        'Guardar indices
        Dim blnSaveWithRule As Boolean = False
        Dim AbsoluteUri As String = e.Url.AbsoluteUri.ToLower
        Dim cur As Cursor

        Trace.WriteLineIf(ZTrace.IsVerbose, "proc RecoverIndexValues.")

        'Valido que no haya ninguna regla pendiente de ejecucion
        If Not IsNothing(doc1) AndAlso doc1.ActiveElement Is Nothing Then
            Dim RuleElement As HtmlElement = doc1.GetElementById("hdnRuleId")
            If (Not IsNothing(RuleElement)) Then
                If (String.Compare(RuleElement.Name, String.Empty)) Then
                    Dim RuleValue As String = RuleElement.Name
                    RuleElement.Name = String.Empty

                    RuleValue = RuleValue.Remove(0, "zamba_rule_".Length)

                    If String.Compare(RuleValue.ToLower(), "cancel") = 0 Then
                        RaiseEvent CancelChildRules(True)
                    End If
                End If
            End If
            Exit Sub
        ElseIf IsNothing(doc1) OrElse doc1.ActiveElement Is Nothing OrElse IsNothing(localResult) OrElse IsNothing(localResult.DocType) Then
            RaiseEvent CancelChildRules(True)
            Exit Sub
        ElseIf doc1.ActiveElement.Name.Contains("cancel") Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Se cancela la carga.")
            FlagAsigned = False
            Me.flagrecover = False
            ' ParentForm.Close()
            RaiseEvent CancelChildRules(True)
            'Me.CloseWebBrowser()
            RaiseEvent FormClose()
            Exit Sub
        ElseIf doc1.ActiveElement.Name.StartsWith("zamba_refresh_asoc_") Then

            Dim idAsoc = doc1.ActiveElement.Name.Replace("zamba_refresh_asoc_", String.Empty)
            Dim tableAsoc = doc1.GetElementById("zamba_associated_documents_" + idAsoc)

            If Not tableAsoc Is Nothing Then
                RefreshAsociatedTable(idAsoc)
            End If

        ElseIf doc1.ActiveElement.Name.StartsWith("zamba_refresh") Then
            cur = Me.Cursor
            Me.Cursor = Cursors.WaitCursor

            If localResult.GetType.FullName.ToString = "Zamba.Core.Result" Then
                RaiseEvent ReloadAsociatedResult(localResult)
            Else
                RaiseEvent RefreshTask(localResult)
            End If

            RefreshData()

            Me.Cursor = cur
            Exit Sub
        End If

        cur = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Checking Security.")
            Dim canedit As Boolean = True

            'Si shared es false, valido los demas permisos
            If localResult.isShared = False AndAlso localResult.DocTypeId <> 0 Then
                'Si esta activado q solo el owner pueda modificar el doc y el user no es el owner
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, localResult.DocTypeId) AndAlso UserBusiness.CurrentUser.ID <> localResult.OwnerID Then
                    'Si alguno de los grupos no lo tiene tildado, entonces si lo puede modificar
                    If Not UserBusiness.Rights.DisableOwnerChanges(UserBusiness.CurrentUser, localResult.DocTypeId) Then
                        canedit = False
                    End If
                End If
                'si shared es true, nadie puede modificar el documento
            ElseIf localResult.isShared = True AndAlso localResult.DocTypeId <> 0 Then
                canedit = False
            End If

            Trace.WriteLineIf(ZTrace.IsVerbose, "Security pass.")
            Trace.WriteLineIf(ZTrace.IsVerbose, "FlagAsigned" & FlagAsigned)

            If (FlagAsigned = True) Then
                Me.flagrecover = True

                If AbsoluteUri.IndexOf("mailto:") = -1 _
                    AndAlso AbsoluteUri.IndexOf("javascript:") = -1 _
                    AndAlso e.TargetFrameName.ToLower <> "zamba_innerdoctype_variable" Then
                    e.Cancel = True
                End If

                Dim RuleElement As HtmlElement = doc1.GetElementById("hdnRuleId")
                Dim AsocElement As HtmlElement = doc1.GetElementById("hdnAsocId")

                If (Not IsNothing(RuleElement)) Then
                    If (String.Compare(RuleElement.Name, String.Empty)) Then
                        Dim RuleValue As String = RuleElement.Name
                        RuleElement.Name = String.Empty

                        If (Not IsNothing(AsocElement)) Then
                            AsocElement.Name = String.Empty
                        End If

                        RuleValue = RuleValue.Remove(0, "zamba_rule_".Length)

                        Dim RuleId As Int64

                        If String.Compare(RuleValue.ToLower(), "cancel") = 0 Then
                            RaiseEvent CancelChildRules(True)
                            'Si se van a salvar los datos
                        ElseIf String.Compare(RuleValue.ToLower(), "save") = 0 Then
                            saveValues(doc1, True, canedit, True)
                            'Si es una regla
                        ElseIf (Int64.TryParse(RuleValue, RuleId)) Then
                            saveValues(doc1, True, canedit, False)
                            ExecuteRule(RuleId, localResult)
                            'Si es una regla con variable
                        ElseIf (RuleValue.ToLower().Contains("zvar")) Then
                            Dim textitem2 As String = RuleValue.Split("_")(1)

                            Dim textaux As String
                            Dim items As Array

                            Try
                                textitem2 = RuleValue.Split("_")(1)

                                While String.IsNullOrEmpty(textitem2) = False
                                    textaux = textitem2.Remove(0, 5)
                                    textitem2 = textitem2.Remove(0, textitem2.IndexOf(")") + 1)

                                    items = (textaux.Remove(textaux.IndexOf(")")).Split("="))

                                    If VariablesInterReglas.ContainsKey(items(0).ToString()) Then
                                        VariablesInterReglas.Item(items(0).ToString()) = items(1).ToString()
                                    Else
                                        VariablesInterReglas.Add(items(0).ToString(), items(1).ToString())
                                    End If
                                End While
                            Finally
                                textaux = Nothing
                                textitem2 = Nothing
                                items = Nothing
                            End Try

                            If (Int64.TryParse(RuleValue.Split("_")(0), RuleId)) Then
                                saveValues(doc1, True, canedit, False)
                                ExecuteRule(RuleId, localResult)
                            End If
                        End If
                    Else
                        RuleElement = Nothing
                    End If
                End If

                If (Not IsNothing(AsocElement)) Then
                    If (String.Compare(AsocElement.Name, String.Empty)) Then
                        Dim AsocValue As String = AsocElement.Name
                        AsocElement.Name = String.Empty

                        If (Not IsNothing(RuleElement)) Then
                            RuleElement.Name = String.Empty
                        End If

                        AsocValue = AsocValue.Remove(0, "zamba_asoc_".Length)

                        Dim DocId As Int64
                        Dim DocTypeId As Int64

                        If (Int64.TryParse(AsocValue.Split("_")(0), DocId)) And (Int64.TryParse(AsocValue.Split("_")(1), DocTypeId)) Then
                            Dim res As Result = Results_Business.GetResult(DocId, DocTypeId)
                            RaiseEvent ShowAsociatedResult(res)
                            ' Se ejecuta el evento que permite crear un panel y colocarlo adentro de la propia solapa
                            RaiseEvent showYellowPanel()
                        End If
                    Else
                        AsocElement = Nothing
                    End If
                End If

                If (IsNothing(RuleElement) And IsNothing(AsocElement)) Then
                    saveValues(doc1, blnSaveWithRule, canedit, True)
                End If

                Try
                    Dim keys As Hashtable = VariablesInterReglas.clone()

                    For Each VarName As String In keys.Keys
                        Dim Element As HtmlElement
                        Element = doc1.GetElementById("ZAMBA_ZVAR(" & VarName & ")")
                        If Not IsNothing(Element) Then RecoverVarValue(VarName, Element)
                    Next
                Catch ex As Exception
                    raiseerror(ex)
                End Try

                If Not IsNothing(RuleElement) Then
                    If Me.CloseFormWindowAfterRuleExecution Then
                        RaiseEvent CloseWindow()
                    End If
                End If

            End If

        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Me.Cursor = cur
        Trace.WriteLineIf(ZTrace.IsVerbose, "end proc RecoverIndexValues.")
    End Sub

    ''' <summary>
    ''' Save all indexs values, and close or not the browser
    ''' </summary>
    ''' <param name="doc1"></param>
    ''' <param name="blnSaveWithRule"></param>
    ''' <param name="canedit"></param>
    ''' <remarks></remarks>
    Private Sub saveValues(ByVal doc1 As System.Windows.Forms.HtmlDocument, ByVal closeBrowser As Boolean, ByVal canedit As Boolean, ByVal blnRaiseEvent As Boolean)
        'Para autocompletar
        Dim modifiedIndex As Hashtable = Nothing
        'Para salvar solo los modificados
        Dim listModifiedIndex As New List(Of Int64)()
        'Guarda las modificaciones hechas para el historial
        Dim sbIndexHistory As New StringBuilder
        sbIndexHistory.Append("Modificaciones realizadas en '" & localResult.Name & "': ")
        Dim oldDataValue As String
        Dim oldDescriptionValue As String

        Try
            For Each I As Index In localResult.Indexs
                'Si el indice se autocompleto, no hace falta recuperar su valor
                If IsNothing(modifiedIndex) OrElse modifiedIndex.Contains(I.ID) = False Then

                    oldDataValue = I.Data
                    oldDescriptionValue = I.dataDescription

                    Try
                        Dim Element As HtmlElement

                        Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID)
                        If Not IsNothing(Element) Then
                            RecoverValue(I, Element)
                            listModifiedIndex.Add(I.ID)
                        End If

                        Element = doc1.GetElementById("ZAMBA_INDEX_" & I.Name)
                        If Not IsNothing(Element) Then
                            RecoverValue(I, Element)
                            listModifiedIndex.Add(I.ID)
                        End If

                        Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "S")
                        If Not IsNothing(Element) Then
                            RecoverValue(I, Element)
                            listModifiedIndex.Add(I.ID)
                        End If

                        Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "N")
                        If Not IsNothing(Element) Then
                            RecoverValue(I, Element)
                            listModifiedIndex.Add(I.ID)
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try

                    ' Si el valor del índice es Nothing entonces el valor del índice se encuentra vacío. Por lo tanto dicho índice se
                    ' almacena en una colección temporal (indexsNothing) que se mantiene si se logra atravesar los permisos de índice
                    ' requerido (al estar vacío el valor del índice sí o sí se necesita ingresar algún valor)
                    If (I.Data Is Nothing) Then
                        I.Data = String.Empty
                        I.DataTemp = String.Empty
                    End If

                    If IsNothing(oldDataValue) Then
                        oldDataValue = String.Empty
                    End If

                    If localResult.ID <> 0 And Not IsNothing(localResult.Doc_File) Then
                        If Not IsNothing(oldDataValue) AndAlso String.Compare(oldDataValue.Trim, I.Data.Trim) <> 0 Then
                            If IsNothing(modifiedIndex) Then
                                modifiedIndex = Me.DataChanged(I)
                            End If
                            'Si existen cambios se guardan para el historial
                            If String.IsNullOrEmpty(oldDescriptionValue) Then
                                sbIndexHistory.Append("índice '" & I.Name & "' de '" & oldDataValue.Trim & "' a '" & I.Data.Trim & "', ")
                            Else
                                sbIndexHistory.Append("índice '" & I.Name & "' de '" & oldDescriptionValue.Trim & "' a '" & I.dataDescription.Trim & "', ")
                            End If
                        Else
                            listModifiedIndex.Remove(I.ID)
                        End If
                    End If
                End If
            Next

            sbIndexHistory = sbIndexHistory.Remove(sbIndexHistory.Length - 2, 2)

            If doc1.ActiveElement.Name.ToLower.Contains("save") Or closeBrowser = True Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Saving Indexs.")

                Dim mandatoryIndexs As New ArrayList
                Dim bnModifyIndexs As Boolean = True

                ' Se verifica si el formulario tiene índices obligatorios
                Me.verifyRequiredIndexs(mandatoryIndexs, doc1)

                ' Si no hay índices obligatorios
                If (mandatoryIndexs.Count = 0) Then
                    bnModifyIndexs = True
                Else
                    bnModifyIndexs = verifyThatTheMandatoryIndexsAreNotEmpty(mandatoryIndexs)

                    If (bnModifyIndexs = False) Then
                        Exit Sub
                    End If
                End If

                mandatoryIndexs = Nothing

                ' Si el formulario es un formulario dinámico 
                If ((doc1.Body.InnerHtml.Contains("<FORM id=")) AndAlso (doc1.Body.InnerHtml.Contains("name=frmmain"))) Then
                    ' Si la verificación de índices obligatorios fue correcta
                    If (bnModifyIndexs = True) Then

                        Dim frmCollection As HtmlElementCollection = doc1.GetElementsByTagName("FORM")
                        If (frmCollection.Count = 1) Then
                            Dim frmId As Int64 = frmCollection(0).Id

                            ' Se verifica si el formulario tiene índices de tipo exceptuable
                            Dim exceptuableIndexs As New ArrayList
                            Me.verifyExceptuableIndexs(frmId, exceptuableIndexs)

                            ' Si no hay índices de tipo exceptuable
                            If (exceptuableIndexs.Count = 0) Then
                                bnModifyIndexs = True
                            Else
                                bnModifyIndexs = verifyAccuracyOfExceptuableIndexs(frmId, exceptuableIndexs)
                                If (bnModifyIndexs = False) Then
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If

                If (localResult.ID = 0) OrElse (TypeOf (localResult) Is NewResult) Then

                    Dim insertresult As InsertResult
                    insertresult = Results_Business.InsertDocument(localResult, True, False, False, True, localResult.ISVIRTUAL)

                    Select Case insertresult

                        Case insertresult.ErrorIndicesIncompletos
                            Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo insertar por falta de indices obligatorios")
                            MessageBox.Show("Hay indices obligatorios sin completar", "Atencion", MessageBoxButtons.OK)

                        Case insertresult.ErrorIndicesInvalidos
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Hay indices con datos invalidos")
                            MessageBox.Show("Hay indices con datos invalidos", "Atencion", MessageBoxButtons.OK)

                        Case Core.InsertResult.Insertado

                            RaiseEvent SaveDocumentVirtualForm()
                            RaiseEvent RefreshIndexs(localResult)
                            RaiseEvent FormCloseTab()

                        Case Else
                            Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo insertar el documento, resultado: " & Core.InsertResult.Insertado.ToString)
                            MessageBox.Show("No se pudo insertar el documento, por favor, consulte con el administrador del sistema.", "Atencion", MessageBoxButtons.OK)

                    End Select


                Else
                    If ((canedit) AndAlso (bnModifyIndexs = True)) Then
                        ' Se guardan las modificaciones en la base de datos
                        Dim Results_Business As New Results_Business
                        Results_Business.SaveModifiedIndexData(localResult, True, blnRaiseEvent, listModifiedIndex)

                        UserBusiness.Rights.SaveAction(localResult.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, sbIndexHistory.ToString)
                        sbIndexHistory = Nothing

                        RaiseEvent FormChanged(False)
                        RaiseEvent CancelChildRules(False)

                        If Not IsNothing(modifiedIndex) Then
                            modifiedIndex = Nothing
                            Me.AsignValues(Me.AxWebBrowser1.Document, Me.localResult)
                        End If

                        ' Se actualizan los valores de los índices para mostrarlos en pantalla
                        RaiseEvent RefreshIndexs(localResult)
                        If closeBrowser = False Then
                            RaiseEvent FormClose()
                        Else
                            closeBrowser = False
                            localResult.Indexs = IndexsBusiness.GetResultIndexs(localResult.ID, localResult.DocTypeId, localResult.Indexs)
                            localResult.DocType.Indexs = localResult.Indexs
                            AsignValues(Me.AxWebBrowser1.Document, Me.localResult)
                        End If
                    ElseIf Not canedit Then
                        MessageBox.Show("No se pueden realizar cambios en los índices", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

            End If
        Finally
            modifiedIndex = Nothing
            listModifiedIndex = Nothing
        End Try
    End Sub

#Region "Formularios dinámicos"

#Region "Verificación de índices requeridos"

    ''' <summary>
    ''' Método que sirve para verificar si el formulario tiene ìndices obligatorios
    ''' </summary>
    ''' <param name="mandatoryIndexs">Colección que contendrá los id's de índices obligatorios</param>
    ''' <param name="doc1">Documento html del formulario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	12/03/2009	Created    
    ''' </history>
    Private Sub verifyRequiredIndexs(ByRef mandatoryIndexs As ArrayList, ByVal doc1 As HtmlDocument)

        Try

            ' Si el formulario es un formulario dinámico 
            If ((doc1.Body.InnerHtml.Contains("<FORM id=")) AndAlso (doc1.Body.InnerHtml.Contains("name=frmmain"))) Then

                Dim frmCollection As HtmlElementCollection = doc1.GetElementsByTagName("FORM")

                If (frmCollection.Count = 1) Then

                    ' Se obtienen los índices requeridos del formulario dinámico y que sean True
                    Dim dsRequiredIndexs As DataSet = FormBusiness.getRequiredIndexs(frmCollection(0).Id)

                    If (Not IsNothing(dsRequiredIndexs)) Then

                        If (dsRequiredIndexs.Tables(0).Rows.Count > 0) Then

                            For Each row As DataRow In dsRequiredIndexs.Tables(0).Rows
                                mandatoryIndexs.Add(CType(row.Item("IId"), Long))
                            Next

                        End If

                        frmCollection = Nothing
                        dsRequiredIndexs = Nothing

                    End If

                End If

                ' Sino, el formulario es un formulario común obtenido del servidor
            Else

                If (UserBusiness.CurrentUser.Groups().Count > 0) Then

                    ' Se obtienen los índices obligatorios del formulario común
                    Dim dt As DataTable = UserBusiness.Rights.GetMandatoryIndexs(localResult.DocType.ID, UserBusiness.CurrentUser.Groups(), RightsType.IndexRequired)

                    ' Si hay índices obligatorios
                    If Not (IsNothing(dt)) Then

                        For Each row As DataRow In dt.Rows

                            If Not (mandatoryIndexs.Contains(CType(row.Item("INDEXID"), Long))) Then
                                mandatoryIndexs.Add(CType(row.Item("INDEXID"), Long))
                            End If

                        Next

                    End If

                End If

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que verifica si falta completar todos o algún índice obligatorio
    ''' </summary>
    ''' <param name="mandatoryIndexs">Colección con los ids de índices obligatorios o requeridos</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/11/2008	Created    
    ''' 	[Gaston]	12/03/2009	Modified    El método pasa de procedimiento a función
    ''' </history>
    Private Function verifyThatTheMandatoryIndexsAreNotEmpty(ByRef mandatoryIndexs As ArrayList) As Boolean

        Try

            Dim mandatoryNameIndexs As New ArrayList
            Dim minCounter As Integer = 0

            For Each Index As Index In localResult.Indexs

                If (mandatoryIndexs.Contains(Index.ID)) Then

                    If (String.IsNullOrEmpty(Index.Data)) Then
                        ' Se guarda el nombre del índice
                        mandatoryNameIndexs.Add(Index.Name)
                    End If

                    minCounter = minCounter + 1

                    If (minCounter = mandatoryIndexs.Count) Then
                        Exit For
                    End If

                End If

            Next

            ' Si todos los índices obligatorios están completos
            If (mandatoryNameIndexs.Count = 0) Then
                ' Se retorna true para poder modificar los índices y guardar las modificaciones en la base de datos
                Return (True)
            Else

                showErrorRequiredMessage(mandatoryNameIndexs)
                ' Se retorna false indicando que no se deben modificar los índices ni que se debe guardar en la base de datos
                Return (False)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' Método que muestra un mensaje de error referido a índices requeridos
    ''' </summary>
    ''' <param name="mandatoryNameIndexs">Colección que contiene el o los nombres de los índices del cuál se requieren completar</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/05/2009	Created    Código original perteneciente al método "verifyThatTheMandatoryIndexsAreNotEmpty"
    ''' </history>
    Private Sub showErrorRequiredMessage(ByRef mandatoryNameIndexs As ArrayList)

        Try

            Dim builder As New StringBuilder

            builder.Append("Faltan completar los siguientes índices requeridos: ")
            builder.Append(vbCrLf + vbCrLf)
            Dim counter As Integer = 0

            For Each nameIndex As String In mandatoryNameIndexs

                If (counter = 0) Then
                    builder.Append(Chr(39) & nameIndex & Chr(39))
                Else
                    builder.Append(vbCrLf)
                    builder.Append(Chr(39) & nameIndex & Chr(39))
                End If

                counter = counter + 1

            Next

            MessageBox.Show(builder.ToString(), "Zamba Software", MessageBoxButtons.OK)
            builder = Nothing
            mandatoryNameIndexs = Nothing

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Verificación de índices de tipo exceptuable"

    ''' <summary>
    ''' Método que verifica si el formulario dinámico tiene índices de tipo exceptuable
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="exceptuableIndexs">Colección que va a contener los id de índices de tipo exceptuable (en caso de que el form. tenga alguno)</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	08/05/2009	Created    
    ''' </history>
    Private Sub verifyExceptuableIndexs(ByRef frmId As Int64, ByRef exceptuableIndexs As ArrayList)

        Try

            ' Se obtienen los índices exceptuables del formulario dinámico 
            Dim dsExceptuableIndexs As DataSet = FormBusiness.getExceptuableIndexs(frmId)

            If (Not IsNothing(dsExceptuableIndexs)) Then

                ' Si el formulario dinámico tiene índices exceptuables
                If (dsExceptuableIndexs.Tables(0).Rows.Count > 0) Then

                    For Each row As DataRow In dsExceptuableIndexs.Tables(0).Rows
                        exceptuableIndexs.Add(CType(row.Item("IId"), Long))
                    Next

                End If

                dsExceptuableIndexs = Nothing

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que verifica si falta completar uno o más índices de tipo exceptuable
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="exceptuableIndexs">Colección que contiene los id de índices de tipo exceptuable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	08/05/2009	Created    
    '''     [Gaston]    11/05/2009  Modified
    ''' </history>
    Private Function verifyAccuracyOfExceptuableIndexs(ByVal frmId As Int64, ByRef exceptuableIndexs As ArrayList) As Boolean

        Try

            ' Se obtienen todos los datos referidos a los índices de tipo exceptuable del formulario dinámico
            Dim dsDataExceptuableIndexs As DataSet = FormBusiness.getDataExceptuableIndexs(frmId)

            If (Not (IsNothing(dsDataExceptuableIndexs))) Then

                If (dsDataExceptuableIndexs.Tables(0).Rows.Count > 0) Then

                    ' Colección que almacena los índices de tipo exceptuable incompletos
                    Dim incompleteExceptuableIndexs As New ArrayList

                    ' Por cada índice de tipo exceptuable
                    For Each exceptuableIndex As Long In exceptuableIndexs

                        ' Si el índice de tipo exceptuable se encuentra vacío entonces se verifican si sus índices exceptuables tienen datos, si uno o más
                        ' de estos índices no tienen datos entonces se prepara el mensaje de error
                        If (getIndexExceptuableValue(exceptuableIndex) = False) Then

                            ' Se obtienen los índices exceptuables del índice de tipo exceptuable
                            Dim view As New DataView(dsDataExceptuableIndexs.Tables(0))
                            view.RowFilter = "IId = " & exceptuableIndex

                            Dim exceptuableIndexsTable As DataTable = view.ToTable()

                            ' Por cada índice exceptuable
                            For Each row As DataRow In exceptuableIndexsTable.Rows

                                If (getIndexExceptuableValue(Int64.Parse(row("Value"))) = False) Then
                                    incompleteExceptuableIndexs.Add(exceptuableIndex)
                                    Exit For
                                End If

                            Next

                            view.Dispose()
                            view = Nothing
                            exceptuableIndexsTable.Dispose()
                            exceptuableIndexsTable = Nothing

                        End If

                    Next

                    If (incompleteExceptuableIndexs.Count > 0) Then
                        showErrorExceptuableMessage(incompleteExceptuableIndexs, dsDataExceptuableIndexs)
                        Return (False)
                    End If

                End If

            End If

            Return (True)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' Método que muestra un mensaje de error referido a índices exceptuables
    ''' </summary>
    ''' <param name="incompleteExceptuableIndexs">Colección que contiene los id de índices de tipo exceptuables incompletos</param>
    ''' <param name="dsDataExceptuableIndexs">Dataset que contiene todos los datos referidos a índices exceptuables de un determinado form. dinámico</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/05/2009	Created    
    ''' </history>
    Private Sub showErrorExceptuableMessage(ByRef incompleteExceptuableIndexs As ArrayList, ByRef dsDataExceptuableIndexs As DataSet)

        Try

            Dim builder As New StringBuilder

            builder.Append("Se requieren completar los siguientes índices de tipo exceptuable o sus correspondientes índices alternativos:")
            builder.Append(vbCrLf + vbCrLf)

            Dim exceptuableIndexsCounter As Short = 1

            ' Por cada índice de tipo exceptuable 
            For Each exceptuableIndex As Int64 In incompleteExceptuableIndexs

                builder.Append("Índice de tipo exceptuable: " & Chr(39) & Me.getIndexName(exceptuableIndex) & Chr(39))

                ' Se obtienen los índices exceptuables del índice de tipo exceptuable
                Dim view As New DataView(dsDataExceptuableIndexs.Tables(0))
                view.RowFilter = "IId = " & exceptuableIndex

                Dim exceptuableIndexsTable As DataTable = view.ToTable()

                builder.Append(vbCrLf)
                builder.Append("Índices alternativos: ")

                Dim minCounter As Short = 1

                ' Por cada índice exceptuable
                For Each row As DataRow In exceptuableIndexsTable.Rows

                    If (minCounter = exceptuableIndexsTable.Rows.Count) Then
                        builder.Append(Chr(39) & Me.getIndexName(Int64.Parse(row("Value"))) & Chr(39))
                    Else
                        builder.Append(Chr(39) & Me.getIndexName(Int64.Parse(row("Value"))) & Chr(39) & ", ")
                    End If

                    minCounter = minCounter + 1

                Next

                view.Dispose()
                view = Nothing
                exceptuableIndexsTable.Dispose()
                exceptuableIndexsTable = Nothing
                minCounter = Nothing

                If (exceptuableIndexsCounter <> incompleteExceptuableIndexs.Count) Then
                    builder.Append(vbCrLf + vbCrLf)
                End If

                exceptuableIndexsCounter = exceptuableIndexsCounter + 1

            Next

            exceptuableIndexsCounter = Nothing
            MessageBox.Show(builder.ToString(), "Zamba Software", MessageBoxButtons.OK)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que obtiene el valor (colocado en el formulario dinámico) de un determinado índice
    ''' </summary>
    ''' <param name="indexId">Id de un índice</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	08/05/2009	Created    
    '''     [Gaston]	11/05/2009	Modified    Validación para DataTemp
    ''' </history>
    Private Function getIndexExceptuableValue(ByVal indexId As Int64) As Boolean

        Try

            For Each Index As Index In localResult.Indexs

                If (Index.ID = indexId) Then

                    If (String.IsNullOrEmpty(Index.Data) AndAlso String.IsNullOrEmpty(Index.DataTemp)) Then
                        Return (False)
                    End If

                End If

            Next

            Return (True)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' Método que obtiene el nombre de un índice (que se encuentra en el formulario dinámico)
    ''' </summary>
    ''' <param name="indexId">Id de un índice</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/05/2009	Created    
    ''' </history>
    Private Function getIndexName(ByVal indexId As Int64) As String

        Try

            For Each Index As Index In localResult.Indexs

                If (Index.ID = indexId) Then
                    Return (Index.Name)
                End If

            Next

            Return ("")

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
#End Region

#End Region

    Sub RecoverValue(ByVal I As Index, ByVal e As HtmlElement)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Id: " & e.Id)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Tag: " & CStr(e.TagName).ToLower)
            If IsDBNull(e.DomElement.type) Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Type: DBNULL")
            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Type: " & CStr(e.DomElement.type).ToLower)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Select Case CStr(e.TagName).ToUpper
            Case "INPUT"
                Select Case CStr(e.DomElement.type).ToLower
                    Case "text"
                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, I.ID)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.value)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try

                        If I.DropDown <> IndexAdditionalType.AutoSustitución Then

                            'casteo de Fecha_Hora a Fecha para que la fecha ingresada
                            'desde el formulario se guarde en formato DD/MM/AAAA
                            If I.Type = IndexDataType.Fecha_Hora Then
                                I.Type = IndexDataType.Fecha
                                I.Data = e.DomElement.value
                                I.DataTemp = e.DomElement.value
                            Else
                                I.Data = e.DomElement.value
                                I.DataTemp = e.DomElement.value
                            End If
                        Else
                            If Not IsNothing(e.DomElement) AndAlso Not IsNothing(e.DomElement.value) Then
                                If e.DomElement.value.ToString().Contains("-") Then
                                    I.Data = e.DomElement.value.ToString().Split("-")(0)
                                    I.DataTemp = e.DomElement.value.ToString().Split("-")(0)
                                Else
                                    I.Data = e.DomElement.value
                                    I.DataTemp = e.DomElement.value
                                End If

                            Else
                                I.Data = e.InnerText
                                I.DataTemp = e.InnerText
                            End If
                        End If
                    Case "checkbox"
                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, I.ID)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.checked)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        If 0 = e.DomElement.checked Then

                            I.Data = 0
                            I.DataTemp = 0
                        Else
                            I.Data = 1
                            I.DataTemp = 1
                        End If
                    Case "radio"

                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, I.ID)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.checked)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try

                        If e.Id.ToUpper().EndsWith("N") _
                         And e.DomElement.checked = True Then
                            I.Data = 0
                            I.DataTemp = 0
                        ElseIf e.Id.ToUpper().EndsWith("S") _
                         And e.DomElement.checked = True Then
                            I.Data = 1
                            I.DataTemp = 1
                        ElseIf e.DomElement.checked = True Then
                            I.Data = 1
                            I.DataTemp = 1
                        Else
                            '                            I.Data = 1
                            '                           I.DataTemp = 1
                        End If
                End Select

            Case "SELECT"
                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, I.ID)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.value)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                If I.DropDown = IndexAdditionalType.AutoSustitución Then
                    Try
                        I.Data = e.DomElement.children(e.DomElement.selectedindex).innertext.split("-")(0).trim
                        I.DataTemp = e.DomElement.children(e.DomElement.selectedindex).innertext.split("-")(0).trim
                        I.dataDescription = e.DomElement.children(e.DomElement.selectedindex).innertext.split("-")(1).trim
                        I.dataDescriptionTemp = e.DomElement.children(e.DomElement.selectedindex).innertext.split("-")(1).trim
                    Catch ex As Exception
                        I.Data = ""
                        I.DataTemp = ""
                    End Try
                ElseIf IsNothing(e.DomElement.value) Then
                    I.Data = ""
                    I.DataTemp = ""
                Else
                    I.Data = e.DomElement.value.trim
                    I.DataTemp = e.DomElement.value.trim
                    I.dataDescription = e.DomElement.value.trim
                    I.dataDescriptionTemp = e.DomElement.value.trim
                End If
            Case "TEXTAREA"
                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, I.ID)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.value)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                I.Data = e.GetAttribute("value")
                I.DataTemp = e.GetAttribute("value")
        End Select
    End Sub

    Sub RecoverVarValue(ByVal VarName As String, ByVal e As HtmlElement)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Id: " & e.Id)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Tag: " & CStr(e.TagName).ToLower)
            Trace.WriteLineIf(ZTrace.IsVerbose, "---------------Type: " & CStr(e.DomElement.type).ToLower)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Select Case CStr(e.TagName).ToUpper
            Case "INPUT"
                Select Case CStr(e.DomElement.type).ToLower
                    Case "text"

                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.value)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try


                        VariablesInterReglas.Item(VarName) = e.DomElement.value

                    Case "checkbox"

                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.checked)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        If 0 = e.DomElement.checked Then

                            VariablesInterReglas.Item(VarName) = 0

                        Else
                            VariablesInterReglas.Item(VarName) = 1

                        End If
                    Case "radio"

                        Try
                            Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.checked)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try

                        'If e.Id.ToUpper().EndsWith("N") _
                        ' And e.DomElement.checked = True Then
                        '    VariablesInterReglas.Item(VarName) = 0

                        'ElseIf e.Id.ToUpper().EndsWith("S") _
                        ' And e.DomElement.checked = True Then
                        '    VariablesInterReglas.Item(VarName) = 1

                        If e.DomElement.checked = True Then
                            VariablesInterReglas.Item(VarName) = 1

                        Else
                            '                            I.Data = 1
                            '                           I.DataTemp = 1
                            VariablesInterReglas.Item(VarName) = 0

                        End If
                End Select

            Case "SELECT"

                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.value)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try


                Try
                    VariablesInterReglas.Item(VarName) = e.DomElement.children(e.DomElement.selectedindex).innertext.split("-")(0).trim
                Catch ex As Exception
                    VariablesInterReglas.Item(VarName) = ""

                End Try

            Case "TEXTAREA"

                Try
                    Trace.WriteLineIf(ZTrace.IsVerbose, e.DomElement.value)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                VariablesInterReglas.Item(VarName) = e.GetAttribute("value")

        End Select
    End Sub
    Private Sub openFile()
        Try
            Dim Open As New OpenFileDialog
            Open.ShowDialog()
            '       Me.ShowDocument(Open.FileName)
            Open.Dispose()


        Catch ex As System.Runtime.InteropServices.COMException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        openFile()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'LoadIndex()
    End Sub

    'Private IndexAnterior As String
    ''' <summary>
    ''' Metodo que g
    ''' </summary>
    ''' <param name="_Index"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 30/04/09 Modified: Se adapto el codigo para tomar mas de 1 clave al autocomplete.
    Private Function DataChanged(ByRef _Index As Index) As Hashtable
        Try
            Dim newFrmGrilla As New frmGrilla()
            Return AutocompleteBCBusiness.ExecuteAutoComplete(localResult, _Index, newFrmGrilla)
        Catch ex As Exception
            If String.Compare(ex.Message, "No hay datos") <> 0 Then
                Zamba.Core.ZClass.raiseerror(ex)
            End If
            Return Nothing
        End Try
        ' RaiseEvent IndexsChanged(LocalResult, Index)
        'IndexAnterior = Index.Data
    End Function

    Private Sub ListToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Trace.WriteLineIf(ZTrace.IsVerbose, "Cargando lista 1: " & Now)
        Me.LoadLists()
    End Sub

    Private Sub List1ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Trace.WriteLineIf(ZTrace.IsVerbose, "Cargando lista 2: " & Now)
        Me.LoadList1()
    End Sub


    ''' <summary>
    ''' Convierte el contenido de un listado de results en un Datatable
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    19/11/2008     Modified     Se agrego la columna "Estado" y verificación del documento asociado para ver si es una tarea
    '''     [Gaston]    20/11/2008     Modified     Se agrego la columna "Usuario Asignado" 
    '''     [Gaston]    05/01/2009     Modified     Verificación de la columna "Nombre del Documento" en el UserPreferences para mostrar o ocultar 
    '''                                             dicha columna
    '''     [Gaston]    06/01/2009     Modified     Validación del valor de "Nombre del Documento" y código comentado en donde se intenta colocar
    '''                                             un String.Empty en una columna que no existe
    '''     Marcelo     05/02/2009     Modified     Se modifico la carga de los indices para mejorar la performance 
    '''     Marcelo     06/01/2010     Modified     Se agrego variable para cargar solo las tareas que esten en WF
    '''     Javier      22/10/2010     Modified     Se agrega funcionalidad para filtrar permisos por asociados
    ''' </history>
    Public Shared Function ParseResult(ByVal ParentResult As IResult, ByVal results As DataTable, ByVal onlyWF As Boolean, ByVal tableId As String) As DataTable
        Dim Dt As New DataTable()
        Dt.Columns.Add(New DataColumn("Ver"))

        If String.IsNullOrEmpty(tableId) = False Then
            For Each btn As String In tableId.Split("§")
                Dim items As Array = btn.Split("/")
                Dt.Columns.Add(items(1).ToString())
            Next
        End If

        If (Boolean.Parse(UserPreferences.getValue("NameDocument", Sections.FormPreferences, "True")) = True) Then
            Dt.Columns.Add(New DataColumn("Nombre del Documento"))
        End If

        If (Boolean.Parse(UserPreferences.getValue("State", Sections.FormPreferences, "True")) = True) Then
            Dt.Columns.Add(New DataColumn("Estado"))
        End If

        If (Boolean.Parse(UserPreferences.getValue("UserAsigned", Sections.FormPreferences, "True")) = True) Then
            Dt.Columns.Add(New DataColumn("Usuario Asignado"))
        End If

        Try
            Dim CurrentIndexType As Type

            'Cargo todos los indices de todos los results , como pueden ser diferentes tipos de documento recorro todos
            'Solo visualizo en la tabla los indices sobre los cuales tiene permiso el documento. Mariela
            Dim lastDocTypeId As Int64 = 0
            Dim AIR As Hashtable = Nothing
            For Each CurrentResult As DataRow In results.Rows
                'Se obtiene los permisos para el doctype, doctypeasociado y usuario
                If lastDocTypeId <> CurrentResult("doc_type_ID") Then
                    lastDocTypeId = CurrentResult("doc_type_ID")
                    AIR = UserBusiness.Rights.GetAssociatedIndexsRightsCombined(ParentResult.DocTypeId, CurrentResult("doc_type_ID"), UserBusiness.CurrentUser.ID, True)
                End If

                For Each CurrentIndex As IIndex In ZCore.FilterCIndex(CurrentResult("doc_type_ID"))
                    Dim ShowIndex As Boolean = False

                    Dim IR As AssociatedIndexsRightsInfo = DirectCast(AIR(CurrentIndex.ID), AssociatedIndexsRightsInfo)

                    If IR.GetIndexRightValue(RightsType.AssociateIndexView) Then
                        If Not Dt.Columns.Contains(CurrentIndex.Name) Then
                            CurrentIndexType = GetType(String)

                            If Not IsNothing(CurrentIndex.Type) Then
                                If CurrentIndex.DropDown = IndexAdditionalType.LineText Then
                                    CurrentIndexType = GetIndexType(CurrentIndex.Type)
                                Else
                                    CurrentIndexType = GetType(String)
                                End If
                            End If

                            Dt.Columns.Add(CurrentIndex.Name.Trim(), CurrentIndexType)
                        End If
                    End If
                Next
            Next

            If Boolean.Parse(UserPreferences.getValue("FechaCreacion", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("Fecha Creacion"))
            End If
            If Boolean.Parse(UserPreferences.getValue("TipodeDocumento", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("Tipo de Documento"))
            End If
            If Boolean.Parse(UserPreferences.getValue("FechaModificacion", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("Fecha Modificacion"))
            End If
            If Boolean.Parse(UserPreferences.getValue("NombreOriginal", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("Nombre Original"))
            End If
            If Boolean.Parse(UserPreferences.getValue("NumerodeVersion", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("Numero de Version"))
            End If
            If Boolean.Parse(UserPreferences.getValue("ParentId", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("ParentId"))
            End If
            If Boolean.Parse(UserPreferences.getValue("FolderId", Sections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn("FolderId"))
            End If
            Dt.Columns.Add(New DataColumn("Ruta Documento"))
            Dt.Columns.Add(New DataColumn("DoctypeId"))
            Dt.Columns.Add(New DataColumn("IdDoc"))
            Dt.AcceptChanges()

            Dim CurrentRow As DataRow = Nothing

            For Each CurrentResult As DataRow In results.Rows
                ' Se verifica si el documento es una tarea
                If onlyWF = False OrElse Not IsDBNull(CurrentResult(UserPreferences.getValue("ColumnNameEstadoTarea", Sections.UserPreferences, "Estado Tarea"))) Then
                    CurrentRow = Dt.NewRow()

                    CurrentRow.Item("IdDoc") = CurrentResult("DOC_ID")

                    CurrentRow.Item("Ruta Documento") = CurrentResult("DISK_VOL_PATH") & "\" & CurrentResult("DOC_TYPE_ID") & "\" & CurrentResult("OFFSET") & "\" & CurrentResult("DOC_FILE")

                    If (Boolean.Parse(UserPreferences.getValue("NameDocument", Sections.FormPreferences, "True")) = True) Then
                        CurrentRow.Item("Nombre del Documento") = CurrentResult("Nombre del Documento")
                    End If

                    If Boolean.Parse(UserPreferences.getValue("FechaCreacion", Sections.FormPreferences, "True")) = True Then
                        CurrentRow.Item("Fecha Creacion") = CurrentResult("CreateDate")
                    End If
                    If Boolean.Parse(UserPreferences.getValue("TipodeDocumento", Sections.FormPreferences, "True")) = True Then
                        CurrentRow.Item("Tipo de Documento") = DocTypesBusiness.GetDocTypeName(CurrentResult("doc_type_ID"), True)
                    End If
                    If Boolean.Parse(UserPreferences.getValue("FechaModificacion", Sections.FormPreferences, "True")) = True Then
                        CurrentRow.Item("Fecha Modificacion") = CurrentResult("EditDate")
                    End If
                    If Boolean.Parse(UserPreferences.getValue("NumerodeVersion", Sections.FormPreferences, "True")) = True Then
                        CurrentRow.Item("Numero de Version") = CurrentResult("VersionNumber")
                    End If
                    If Boolean.Parse(UserPreferences.getValue("ParentId", Sections.FormPreferences, "True")) = True Then
                        CurrentRow.Item("ParentId") = CurrentResult("ParentID")
                    End If
                    CurrentRow.Item("DoctypeId") = CurrentResult("doc_type_ID")
                    If Boolean.Parse(UserPreferences.getValue("FolderId", Sections.FormPreferences, "True")) = True Then
                        CurrentRow.Item("FolderId") = CurrentResult("folder_ID")
                    End If

                    Dim IndexType As Type = GetType(String)
                    For Each CurrentIndex As IIndex In ZCore.FilterCIndex(CurrentResult("doc_type_ID"))
                        If Not IsNothing(CurrentIndex.Type) Then
                            IndexType = GetIndexType(CurrentIndex.Type)
                        Else
                            IndexType = GetType(String)
                        End If
                        Try
                            If CurrentRow.Table.Columns.Contains(CurrentIndex.Name) AndAlso CurrentResult.Table.Columns.Contains(CurrentIndex.Name) Then 'Si Data tiene un valor que se le asigne al Item
                                CurrentRow.Item(CurrentIndex.Name) = CurrentResult(CurrentIndex.Name)
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next

                    If Not IsDBNull(CurrentResult(UserPreferences.getValue("ColumnNameEstadoTarea", Sections.UserPreferences, "Estado Tarea"))) Then
                        ' Si el documento es una tarea entonces se coloca el estado actual de la tarea, sino, el estado se coloca como vacío
                        If (Boolean.Parse(UserPreferences.getValue("State", Sections.FormPreferences, "True")) = True) Then
                            Dim StepName As String = WFStepStatesComponent.GetStepStateById(CurrentResult(UserPreferences.getValue("ColumnNameEstadoTarea", Sections.UserPreferences, "Estado Tarea"))).Name
                            If StepName = String.Empty Then StepName = "Ninguno"
                            CurrentRow.Item("Estado") = StepName
                        End If

                        If (Boolean.Parse(UserPreferences.getValue("UserAsigned", Sections.FormPreferences, "True")) = True) Then
                            CurrentRow.Item("Usuario Asignado") = Core.UserGroupBusiness.GetUserorGroupNamebyId(CurrentResult(UserPreferences.getValue("ColumnNameAsignado", Sections.UserPreferences, "Asignado")).ToString())
                        End If
                    End If

                    'Nombre del documento
                    If Boolean.Parse(UserPreferences.getValue("NombreOriginal", Sections.FormPreferences, "True")) = True Then
                        Dim FileName As String = CurrentResult("OriginalName")
                        If FileName Is Nothing Then FileName = CurrentResult("name")

                        Dim indexpath As Int32 = FileName.LastIndexOf("\")
                        If indexpath = -1 OrElse FileName.Length - 1 = -1 Then
                        Else
                            If indexpath = -1 Then indexpath = 0
                            Try
                                FileName = FileName.Substring(indexpath + 1, FileName.Length - indexpath - 1)
                            Catch ex As Exception
                                FileName = CurrentResult("OriginalName")
                            End Try
                        End If
                        CurrentRow.Item("Nombre Original") = FileName
                    End If


                    Dim InnerHtml As StringBuilder = New StringBuilder()
                    Dim htmlName As String = "zamba_asoc_" & CurrentResult("DOC_ID") & "_" & CurrentResult("doc_type_ID")

                    InnerHtml.Append("<INPUT id=")
                    InnerHtml.Append(htmlName)
                    InnerHtml.Append(" type=button onclick=SetAsocId(this); value=")
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append("Ver")
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append("Name = ")
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append(htmlName)
                    InnerHtml.Append(Chr(34))
                    InnerHtml.Append(" >")

                    CurrentRow("Ver") = InnerHtml.ToString()

                    If String.IsNullOrEmpty(tableId) = False Then
                        Dim textItem2, textAux As String

                        For Each btn As String In tableId.Split("§")
                            InnerHtml.Remove(0, InnerHtml.Length)
                            Dim items As Array = btn.Split("/")
                            Dim itemNum As Int32
                            Dim zvarItems As Array
                            Dim params As String

                            InnerHtml.Append("&nbsp;<INPUT id=")
                            InnerHtml.Append(Chr(34))

                            'Si tiene zvar
                            If items.Length > 2 Then
                                textItem2 = items(2).ToString()
                                InnerHtml.Append(items(0) + "_")

                                While String.IsNullOrEmpty(textItem2) = False
                                    textAux = textItem2.Remove(0, 5)
                                    zvarItems = textAux.Remove(textAux.IndexOf(")")).Split("=")
                                    textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1)

                                    If Int32.TryParse(zvarItems(1).ToString(), itemNum) = False Then
                                        If zvarItems(1).ToString().ToLower().Contains("length") Then
                                            itemNum = Dt.Columns.Count - Int32.Parse(zvarItems(1).ToString().Split("-")(1))
                                        End If
                                    End If
                                    InnerHtml.Append("zvar(" + zvarItems(0).ToString() + "=" + CurrentRow.Item(itemNum).ToString() + ")")

                                    params = params & "'" & CurrentRow.ItemArray(itemNum).ToString() & "',"
                                End While
                            Else
                                InnerHtml.Append(items(0))
                            End If

                            InnerHtml.Append(Chr(34))
                            InnerHtml.Append(" type=button onclick=")

                            'Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                            'sino se llama a SetRuleId por default
                            If items.Length > 3 Then
                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append(items(3) & "(this, ")
                                InnerHtml.Append(params.Substring(0, params.Length - 1).Replace("\", "\\"))
                                InnerHtml.Append(");")
                                InnerHtml.Append(Chr(34))
                            Else
                                InnerHtml.Append(Chr(34))
                                InnerHtml.Append("SetRuleId(this);")
                                InnerHtml.Append(Chr(34))
                            End If

                            InnerHtml.Append(" value = ")
                            InnerHtml.Append(Chr(34))
                            InnerHtml.Append(items(1))
                            InnerHtml.Append(Chr(34))
                            InnerHtml.Append(" Name = ")
                            InnerHtml.Append(Chr(34))
                            InnerHtml.Append(items(0))
                            InnerHtml.Append(Chr(34))
                            InnerHtml.Append(" >")

                            CurrentRow.Item(items(1)) = InnerHtml.ToString()
                            params = String.Empty
                        Next
                    End If

                    InnerHtml = Nothing
                    Dt.Rows.Add(CurrentRow)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Dt.AcceptChanges()
        Dt.DefaultView.Sort = "IdDoc DESC"

        Return Dt.DefaultView.ToTable()
    End Function

    ''' <summary>
    ''' Castea el tipo de un índice a Type
    ''' </summary>
    ''' <param name="indexType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetIndexType(ByVal indexType As IndexDataType) As Type
        Dim ParsedIndexType As Type

        Select Case indexType

            Case IndexDataType.Alfanumerico
                ParsedIndexType = GetType(String)
            Case IndexDataType.Alfanumerico_Largo
                ParsedIndexType = GetType(String)
            Case IndexDataType.Fecha
                ParsedIndexType = GetType(Date)
            Case IndexDataType.Fecha_Hora
                ParsedIndexType = GetType(DateTime)
            Case IndexDataType.Moneda
                ParsedIndexType = GetType(Decimal)
            Case IndexDataType.None
                ParsedIndexType = GetType(String)
            Case IndexDataType.Numerico
                ParsedIndexType = GetType(Int64)
            Case IndexDataType.Numerico_Decimales
                ParsedIndexType = GetType(Decimal)
            Case IndexDataType.Numerico_Largo
                ParsedIndexType = GetType(Decimal)
            Case IndexDataType.Si_No
                ParsedIndexType = GetType(String)
            Case Else
                ParsedIndexType = GetType(String)
        End Select

        Return ParsedIndexType
    End Function


    Private Sub AxWebBrowser1_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles AxWebBrowser1.PreviewKeyDown
        If e.KeyCode = Keys.F5 Then
            RaiseEvent RefreshAfterF5()
        Else
            RaiseEvent FormChanged(True)
        End If
    End Sub

    Public Sub AssociateIndexViewer(ByRef ucindexs As IZControl)
        RemoveHandler ucindexs.OnChangeControl, AddressOf RefreshData
        AddHandler ucindexs.OnChangeControl, AddressOf RefreshData
    End Sub

End Class
