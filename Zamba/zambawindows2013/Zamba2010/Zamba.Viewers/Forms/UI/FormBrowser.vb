'VERSION 3
Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated
Imports Microsoft.Win32
Imports Zamba.Core

Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports Zamba.Viewers.HelperForm
Imports Zamba.Data

Public Class FormBrowser
    Inherits ZControl
    Implements IDisposable


#Region " Windows Form Designer generated code "

    Public Function validateForm() As Boolean
        Try
            If (Not AxWebBrowser1 Is Nothing AndAlso Not AxWebBrowser1.Document Is Nothing) Then
                Return AxWebBrowser1.Document.InvokeScript("ZFUNCTION_ONUNLOAD")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            Try
                If disposing Then
                    If Not (components Is Nothing) Then components.Dispose()
                    If AxWebBrowser1 IsNot Nothing Then
                        AxWebBrowser1.Dispose()
                        AxWebBrowser1 = Nothing
                    End If
                    If ContextMenuStrip1 IsNot Nothing Then
                        ContextMenuStrip1.Dispose()
                        ContextMenuStrip1 = Nothing
                    End If
                    If List1ToolStripMenuItem IsNot Nothing Then
                        List1ToolStripMenuItem.Dispose()
                        List1ToolStripMenuItem = Nothing
                    End If
                    If ASListToLoad IsNot Nothing Then
                        For i As Int32 = 0 To ASListToLoad.Count - 1
                            ASListToLoad(i).Dispose()
                            ASListToLoad(i) = Nothing
                        Next
                        ASListToLoad.Clear()
                        ASListToLoad = Nothing
                    End If
                    If SearchListToLoad IsNot Nothing Then
                        For i As Int32 = 0 To SearchListToLoad.Count - 1
                            SearchListToLoad(i).Dispose()
                            SearchListToLoad(i) = Nothing
                        Next
                        SearchListToLoad.Clear()
                        SearchListToLoad = Nothing
                    End If
                    If ListToolStripMenuItem IsNot Nothing Then
                        ListToolStripMenuItem.Dispose()
                        ListToolStripMenuItem = Nothing
                    End If

                    If localResult IsNot Nothing Then
                        ' No mato el objeto, elimino la referencia.
                        'localResult.Dispose()
                        localResult = Nothing
                    End If

                    If rulesIds IsNot Nothing Then rulesIds.Clear()
                    If _userActionDisabledRules IsNot Nothing Then _userActionDisabledRules.Clear()
                    If WP IsNot Nothing Then WP.Dispose()

                    For i As Int32 = 0 To Controls.Count - 1
                        If Controls(i) IsNot Nothing AndAlso TypeOf (Controls(i)) Is IDisposable Then
                            Controls(i).Dispose()
                        End If
                    Next
                End If
                MyBase.Dispose(disposing)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
            End Try

            isDisposed = True
        End If
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents List1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxWebBrowser1 As New WebBrowser
    'Friend WithEvents Panel1 As ZPanel
    'Friend WithEvents Panel2 As ZPanel
    'Friend WithEvents Panel3 As ZPanel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        AxWebBrowser1 = New System.Windows.Forms.WebBrowser()
        ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(components)
        ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        List1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        ContextMenuStrip1.SuspendLayout()
        SuspendLayout()
        '
        'AxWebBrowser1
        '
        AxWebBrowser1.ContextMenuStrip = ContextMenuStrip1
        AxWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        AxWebBrowser1.Location = New System.Drawing.Point(0, 0)
        AxWebBrowser1.Name = "AxWebBrowser1"
        AxWebBrowser1.Size = New System.Drawing.Size(552, 477)
        AxWebBrowser1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {ListToolStripMenuItem, List1ToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New System.Drawing.Size(99, 48)
        '
        'ListToolStripMenuItem
        '
        ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        ListToolStripMenuItem.Size = New System.Drawing.Size(98, 22)
        ListToolStripMenuItem.Text = "List"
        '
        'List1ToolStripMenuItem
        '
        List1ToolStripMenuItem.Name = "List1ToolStripMenuItem"
        List1ToolStripMenuItem.Size = New System.Drawing.Size(98, 22)
        List1ToolStripMenuItem.Text = "List1"
        '
        'FormBrowser
        '
        AutoScroll = True
        ContextMenuStrip = ContextMenuStrip1
        Controls.Add(AxWebBrowser1)
        Name = "FormBrowser"
        Size = New System.Drawing.Size(552, 477)
        ContextMenuStrip1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Constructores"
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
    ''' Constructor que recibe IZControl para asociar el evento Refreshdata cuando se cambiaron los atributos.
    ''' </summary>
    ''' <param name="ucindex"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''    [Ezequiel] - 06/10/09  - Created.
    ''' </history>
    Dim WebServiceURl(0) As Object
    Public Sub New(ByVal ucindex As IZControl)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        Try
            InitializeComponent()
            If Not IsNothing(ucindex) Then
                RemoveHandler ucindex.OnChangeControl, AddressOf RefreshData
                AddHandler ucindex.OnChangeControl, AddressOf RefreshData
            End If

            WebServiceURl(0) = CObj(ZOptBusiness.GetValue("WSResultsUrl"))
        Catch ex As System.ComponentModel.Win32Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "General"
    Public Event FormChanged(ByVal Ischanged As Boolean)
    Public Event SaveFormChanges()
    Public Event CancelChildRules(ByVal Cancel As Boolean)
    Public Event FormClose()
    Public Event RefreshAfterF5()
    Public Event FormCloseTab(removeTab As Boolean)
    Public Event CloseWindow()
    Public Event RefreshTask(ByVal Task As ITaskResult)
    Public Event ShowOriginal(ByVal result As IResult)
    Public Event ReloadAsociatedResult(ByVal AsociatedResult As Result)

    Private Const _FORMLOADINGERRORMESSAGE As String = "No se ha podido acceder a la ruta del formulario." & vbCrLf &
                                    "Verifique tener permisos de acceso sobre la ruta y que la misma sea válida."
    Private IsDynamicForm As Boolean = False
    '[AlejandroR] 12/11/09 - Created
    Public Event SaveDocumentVirtualForm()

    Dim localResult As Result

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

    Public Property DateIndexsList As New List(Of String)
    Public Property DateTimeIndexsList As New List(Of String)

    ''' <summary>
    ''' Método que muestra un formulario
    ''' </summary>
    ''' <param name="myResult">Instancia de una tarea o documento que se selecciona</param>
    ''' <param name="tmpForm">Instancia de un formulario asociado al entidad al que pertenece el documento</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	10/07/2008	Modified
    '''     [Gaston]    05/03/2009  Modified    Llámada al método "navigateToForm"
    ''' </history>
    Public Sub ShowDocument(ByRef myResult As Result, ByVal tmpForm As ZwebForm)
        localResult = myResult

        If (myResult.DocType.IsReindex = False) Then
            If (tmpForm.Type = FormTypes.Show AndAlso tmpForm.Type = FormTypes.WebShow) Then
                If (NavigateToForm(tmpForm, "1") = True) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "NavigateForm 1")
                    Exit Sub
                End If
            End If
        Else
            If (tmpForm.Type = FormTypes.Edit AndAlso tmpForm.Type = FormTypes.WebEdit) Then
                If (NavigateToForm(tmpForm, "2") = True) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "NavigateForm 2")
                    Exit Sub
                End If
            End If
        End If

        If (tmpForm.Type = FormTypes.Show AndAlso tmpForm.Type = FormTypes.WebShow) Then
            If (NavigateToForm(tmpForm, "3") = True) Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "NavigateForm 3")
                Exit Sub
            End If
        ElseIf (myResult.CurrentFormID = tmpForm.ID) Then
            If (NavigateToForm(tmpForm, "4") = True) Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "NavigateForm 4")
                Exit Sub
            End If
        End If

        Throw New ZambaEx("No hay formulario para el resultado")
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

            'itero buscando el atributo por el que se asigno la condicion
            For j = 0 To localResult.Indexs.Count - 1
                'comparo el atributo de la condicion con el atributo del formulario
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
    ''' <param name="Forms">Colección de instancias de formularios asociados al entidad al que pertenece el documento</param>
    ''' <param name="ComeFromWF">Booleano que determina cuando el metodo es llamado desde busqueda o desde tareas</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    05/03/2009  Modified    Llámada al método "navigateToForm"
    '''     [pablo]     14/10/2010  Modified    se agrega la validacion de condiciones, se cambia el metodo
    '''                                         para que se muestre el tipo visualizacion en caso de que no 
    '''                                         existan condiciones aplicadas en los formularios
    ''' </history>
    Public Sub ShowDocument(ByRef myResult As Result, ByVal Forms As List(Of ZwebForm), ByVal ComeFromWF As Boolean)
        Dim f, i As Int32
        localResult = myResult

        If Forms.Count > 2 Then
            Dim ds As DataSet
            Dim AuxForms As New List(Of ZwebForm)


            For i = 0 To Forms.Count - 1
                'si vengo desde tareas visualizo el formulario de edicion
                If ComeFromWF Then
                    If (NavigateToForm(Forms(i), "2") = True) Then
                        Exit Sub
                    End If
                Else
                    'sino, obtengo las condiciones del formulario
                    ds = FormBusiness.GetDynamicFormIndexsConditions(Forms(i).ID)
                    'valido que el formulario tenga condiciones aplicadas
                    If ds.Tables(0).Rows.Count > 0 Then
                        If EvaluateDynamicFormConditions(myResult, ds) Then
                            f = f + 1
                            AuxForms.Add(Forms(i))
                        End If
                    End If
                End If
            Next

            If Not AuxForms(0) Is Nothing Then
                'si existe mas de un formulario con condicion entonces muestro el panel de seleccion
                If AuxForms.Count > 1 Then
                    Dim FormLst As New frmFormsListOfDocType(AuxForms)
                    FormLst.ShowDialog()
                    Forms = FormLst.FormSelected
                ElseIf AuxForms.Count = 1 Then
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

        For i = 0 To Forms.Count - 1
            If (myResult.DocType.IsReindex = False) Then
                If (Forms(i).Type = FormTypes.Show) Then
                    If (NavigateToForm(Forms(i), "1") = True) Then
                        Exit Sub
                    End If
                End If
            Else
                If (Forms(i).Type = FormTypes.Edit) Then
                    If (NavigateToForm(Forms(i), "2") = True) Then
                        Exit Sub
                    End If
                End If
            End If
        Next

        'si no abre ningun form abro el de show
        For i = 0 To Forms.Count - 1
            If (Forms(i).Type = FormTypes.Show) Then
                If (NavigateToForm(Forms(i), "3") = True) Then
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
    ''' <param name="Forms">Colección de instancias de formularios asociados al entidad al que pertenece el documento</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Pablo]    12/10/2010  Created
    ''' </history>
    Public Function ShowInsertForm(ByRef myResult As Result, ByVal Forms As List(Of ZwebForm)) As Boolean
        localResult = myResult
        Dim i As Int32

        'If Forms.Length > 1 Then
        '    Dim FormLst As New frmFormsListOfDocType(Forms)
        '    FormLst.ShowDialog()
        '    Forms = FormLst.FormSelected
        'End If

        For i = 0 To Forms.Count - 1
            If (Forms(i).Type = FormTypes.Insert) Then
                If (NavigateToForm(Forms(i), "4") = True) Then
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

    Private Function NavigateToForm(ByRef form As ZwebForm, ByVal typeForm As String) As Boolean

        Dim proc As System.Diagnostics.Process = Nothing

        ' Si el path al formulario está vacío entonces el formulario es un formulario dinámico
        If (String.IsNullOrEmpty(form.Path)) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, typeForm & " - Formulario dinámico: se crea el formulario y se guarda en un archivo html temporal")
            Dim dsDynamicForm As DataSet = FormBusiness.GetDynamicForm(form.ID)

            If dsDynamicForm Is Nothing Then Return False
            If dsDynamicForm.Tables(0).Rows.Count = 0 Then
                dsDynamicForm.Dispose()
                dsDynamicForm = Nothing
                Return False
            End If

            Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
            Try
                ' Se crea el formulario dinámico y se guarda en un archivo html temporal
                form.Path = formDinamico.CreateTable(dsDynamicForm, form.Name)
                IsDynamicForm = True
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return (False)
            Finally
                formDinamico.Dispose()
                formDinamico = Nothing
                dsDynamicForm.Dispose()
                dsDynamicForm = Nothing
            End Try

        Else
            Dim listaTags As List(Of DtoTag) = Nothing
            Dim fList As List(Of System.IO.FileInfo) = Nothing
            Dim formInner As ZwebForm = Nothing
            Dim formHelper As New HelperForm.HelperFormVirtual()
            Dim id As Int64
            Dim useOriginal As Boolean
            Dim strPathLocal, tag, path As String

            Dim dir As DirectoryInfo
            Dim docTypesAsocList As ArrayList
            Dim myResult As Result
            Dim matches As MatchCollection
            Dim fileList As System.IO.FileInfo()

            Try

                form.Path = GetFormPath(form)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta del Formulario a utilizar: " & form.Path)
                Dim rutaTemp = Membership.MembershipHelper.AppTempPath + ("\OfficeTemp")
                If localResult IsNot Nothing AndAlso localResult.Doc_File IsNot Nothing Then
                    Dim FileName = localResult.Doc_File.ToString
                    Dim destinationFile = System.IO.Path.Combine(rutaTemp, FileName)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del Archivo " & FileName)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ubicacion del archivo " & localResult.FullPath)
                    'Si tiene un iframe, busco el documento asociado
                    listaTags = New List(Of HelperForm.DtoTag)
                    matches = formHelper.ParseHtml(form.Path, "iframe")

                    'Entrar por aca si el html tiene la palabra iframe
                    If Not IsNothing(matches) Then
                        For Each item As Match In matches
                            If formHelper.buscarHtmlIframe(item) Then

                                id = formHelper.buscarTagZamba(item)
                                useOriginal = False

                                If id = -1 Then
                                    If Not String.IsNullOrEmpty(localResult.FullPath) AndAlso IO.Path.HasExtension(localResult.FullPath) Then
                                        path = Results_Business.GetTempFileFromResult(localResult, True)

                                        Dim UseExternalBrowserPreview As Boolean = UserPreferences.getValue("UseExternalBrowserPreview", UPSections.Viewer, False)
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "UseExternalBrowserPreview: " & UseExternalBrowserPreview)

                                        Dim UseWebBrowserPreview As Boolean = UserPreferences.getValue("UseWebBrowserPreview", UPSections.Viewer, False)
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "UseWebBrowserPreview: " & UseWebBrowserPreview)

                                        Dim UseTempBrowserPreview As Boolean = UserPreferences.getValue("UseTempBrowserPreview", UPSections.Viewer, False)
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "UseTempBrowserPreview: " & UseTempBrowserPreview)


                                        If UseWebBrowserPreview Then
                                            Dim BaseUrl As String = ZOptBusiness.GetValue("WebViewPath")
                                            BaseUrl = BaseUrl.Substring(0, BaseUrl.ToLower().IndexOf("views", StringComparison.CurrentCultureIgnoreCase))
                                            If BaseUrl.EndsWith("/") = False Then BaseUrl = BaseUrl & "/"
                                            path = String.Format("{0}Views/viewers/browserpreview.aspx?docid={1}&userid={2}&token={3}&entityid={4}", BaseUrl, localResult.ID, Membership.MembershipHelper.CurrentUser.ID, String.Empty, localResult.DocTypeId)
                                        End If

                                        If UseTempBrowserPreview Then
                                            path = Membership.MembershipHelper.AppTempPath + String.Format("\temp\browserpreview.html?url={0}", path.Substring(path.LastIndexOf("\") + 1, path.Length - path.LastIndexOf("\") - 1))
                                        End If



                                        If UseExternalBrowserPreview Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Uso el UseExternalBrowserPreview")
                                            useOriginal = True
                                            LoadExternalPreview(path)
                                            tag = item.Value
                                            tag = tag.Replace("style=", "'display: none;>")
                                            tag = tag.Replace(">", "style='display: none'>")
                                            listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cargo en IFRAME el preview")
                                            'Se completa la ruta del iframe
                                            tag = item.Value
                                            If localResult.IsMsg AndAlso
                                                   (File.Exists(localResult.FullPath.ToLower().Replace(".msg", ".html"))) Then
                                                path = path.Replace(".msg", ".html")
                                            End If

                                            formHelper.replazarAtributoSrc(tag, path)
                                            listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))
                                            useOriginal = True
                                            Try
                                                File.Copy(localResult.FullPath, destinationFile.ToString, True)
                                            Catch ex As Exception
                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "error al guardar archivo en el temp: " + ex.Message)
                                            End Try
                                        End If

                                    Else
                                        'Si el fullpath esta vacio y el item.Value contiene datos, se busca
                                        'la propiedad src y se la remueve para mostrar un iframe en blanco.
                                        If Not IsNothing(item) AndAlso Not IsNothing(item.Value) Then
                                            'Se intenta remover el atributo src.
                                            tag = formHelper.RemoveSrcTag(item.Value)
                                            'Si existieron cambios realizo la modificacion.
                                            If String.Compare(item.Value, tag) <> 0 Then
                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))")
                                                listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))
                                            End If

                                            useOriginal = True
                                        End If
                                    End If
                                ElseIf id = -2 Then
                                    If Not String.IsNullOrEmpty(localResult.FullPath) AndAlso IO.Path.HasExtension(localResult.FullPath) Then
                                        path = String.Format("{0}/Views/viewers/browserpreview.aspx?taskid={1}&userid={2}&token={3}&entityid={4}", ZOptBusiness.GetValue(""), localResult.ID, Membership.MembershipHelper.CurrentUser.ID, String.Empty, localResult.DocTypeId)

                                        Dim UseWebBrowserPreview As Boolean = UserPreferences.getValue("UseWebBrowserPreview", UPSections.Viewer, True)

                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "UseWebBrowserPreview: " & UseWebBrowserPreview)

                                        If UseWebBrowserPreview Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Uso el UseWebBrowserPreview")
                                            tag = item.Value
                                            formHelper.replazarAtributoSrc(tag, path)
                                            listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))
                                            useOriginal = True
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cargo en IFRAME el preview")
                                            'Se completa la ruta del iframe
                                            tag = item.Value
                                            formHelper.replazarAtributoSrc(tag, path)
                                            listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))
                                            useOriginal = True
                                            Try
                                                File.Copy(localResult.FullPath, destinationFile.ToString, True)
                                            Catch ex As Exception
                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "error al guardar archivo en el temp: " + ex.Message)
                                            End Try
                                        End If

                                    Else
                                        'Si el fullpath esta vacio y el item.Value contiene datos, se busca
                                        'la propiedad src y se la remueve para mostrar un iframe en blanco.
                                        If Not IsNothing(item) AndAlso Not IsNothing(item.Value) Then
                                            'Se intenta remover el atributo src.
                                            tag = formHelper.RemoveSrcTag(item.Value)
                                            'Si existieron cambios realizo la modificacion.
                                            If String.Compare(item.Value, tag) <> 0 Then
                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))")
                                                listaTags.Add(formHelper.instanceDtoTag(item.Value, tag))
                                            End If

                                            useOriginal = True
                                        End If
                                    End If
                                ElseIf id = 0 Then
                                    Try
                                        File.Copy(localResult.FullPath, destinationFile.ToString, True)
                                    Catch ex As Exception
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "error al guardar archivo en el temp: " + ex.Message)
                                    End Try
                                    useOriginal = True
                                End If

                                If Not useOriginal Then
                                    'Busca el documento asociado
                                    docTypesAsocList = DocAsociatedBusiness.getAsociatedResultsFromResult(localResult)

                                    If Not IsNothing(docTypesAsocList) AndAlso docTypesAsocList.Count > 0 Then
                                        For Each docAsoc As Object In docTypesAsocList
                                            If TypeOf docAsoc Is Result Then
                                                myResult = DirectCast(docAsoc, Result)

                                                'Verifica que sea el DocType correcto
                                                If myResult.DocTypeId = id Or id = 0 Then
                                                    path = String.Empty
                                                    tag = item.Value

                                                    If myResult.ISVIRTUAL Then
                                                        'Se obtiene el id del formulario actual
                                                        myResult.CurrentFormID = FormBusiness.getDTFormId(CType(myResult.DocTypeId, Integer))

                                                        'Agrego una validacion para si no hay form asociado, no tire error - MC
                                                        If myResult.CurrentFormID <> 0 Then
                                                            'Obtiene el formulario Virtual
                                                            formInner = FormBusiness.GetForm(myResult.CurrentFormID)
                                                            formInner.Path = formInner.TempFullPath
                                                            path = formInner.Path
                                                            formHelper.replazarAtributoId(tag, myResult.ID)
                                                        End If
                                                    Else
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "'Consultar si se copia localmente el archivo cuando no es virtual")
                                                        'Consultar si se copia localmente el archivo cuando no es virtual
                                                        path = myResult.FullPath
                                                    End If

                                                    If myResult.IsMsg AndAlso
                                                    (File.Exists(myResult.FullPath.ToLower().Replace(".msg", ".html")) OrElse
                                                     File.Exists(myResult.FullPath.ToLower().Replace(".msg", ".txt"))) Then

                                                        Dim OpenMsgFileInIFrame As Boolean = UserPreferences.getValue("OpenMsgFileInIFrame", UPSections.FormPreferences, False)
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "OpenMsgFileInIFrame: " & OpenMsgFileInIFrame)

                                                        If OpenMsgFileInIFrame Then
                                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Busco los archivos del fullpath para recorrerlos y buscar el msg")
                                                            dir = New DirectoryInfo(System.IO.Path.GetDirectoryName(myResult.FullPath))
                                                            fileList = dir.GetFiles()
                                                            fList = New List(Of System.IO.FileInfo)

                                                            For Each fItem As System.IO.FileInfo In fileList
                                                                If fItem.Name.Contains(System.IO.Path.GetFileNameWithoutExtension(myResult.FullPath)) AndAlso
                                                                Not fItem.Name.Trim().ToLower().EndsWith(".msg") Then
                                                                    fList.Add(fItem)
                                                                End If
                                                            Next

                                                            If fList.Count > 0 Then
                                                                path = fList.Item(0).FullName
                                                            End If

                                                        Else
                                                            If IsNothing(proc) Then
                                                                dir = GetTempDir("\OfficeTemp")
                                                                strPathLocal = dir.FullName & myResult.FullPath.Remove(0, myResult.FullPath.LastIndexOf("\"))

                                                                Try
                                                                    File.Copy(myResult.FullPath, strPathLocal, True)
                                                                Catch ex As Exception
                                                                    ZClass.raiseerror(ex)
                                                                End Try


                                                                proc = New System.Diagnostics.Process()
                                                                proc.StartInfo.UseShellExecute = True
                                                                proc.StartInfo.FileName = strPathLocal
                                                                proc.Start()
                                                                Exit For
                                                            End If
                                                        End If
                                                    End If

                                                    'Reemplaza el atributo src
                                                    formHelper.replazarAtributoSrc(tag, path)

                                                    Dim dto As HelperForm.DtoTag
                                                    dto = formHelper.instanceDtoTag(item.Value, tag)

                                                    listaTags.Add(dto)
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        Next
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se actualizara el formulario en officetemp con el iframe")
                        formHelper.ActualizarHtml(listaTags, form.Path)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se actualizo el formulario en officetemp con el iframe")
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El formulario no tiene documento")
                    'Return (False)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al mostrar el formulario")
                Return (False)

            Finally
                formHelper = Nothing

                dir = Nothing
                docTypesAsocList = Nothing
                myResult = Nothing
                matches = Nothing
                fileList = Nothing
                If listaTags IsNot Nothing Then
                    listaTags.Clear()
                    listaTags = Nothing
                End If
                If formInner IsNot Nothing Then
                    formInner.Dispose()
                    formInner = Nothing
                End If
                If fList IsNot Nothing Then
                    fList.Clear()
                    fList = Nothing
                End If
            End Try
        End If


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
            reader.Close()
        End Using

        If html.ToLower.Contains("zamba_rule_") Then
            While html.ToLower.Contains("zamba_rule_")
                html = html.Trim().Remove(0, html.Trim().IndexOf("zamba_rule_", StringComparison.CurrentCultureIgnoreCase) + 11)
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
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function
    Private Sub MakeLocalCopy(ByVal form As ZwebForm)
        Dim serverFile As FileInfo
        Dim localFile As FileInfo
        Dim tempDir As IO.DirectoryInfo

        Try
            serverFile = New FileInfo(form.Path)
            localFile = New FileInfo(form.TempFullPath)
            tempDir = Tools.EnvironmentUtil.GetTempDir("\temp")

            If Not tempDir.Exists Then tempDir.Create()

            If File.Exists(localFile.FullName) AndAlso File.GetAttributes(localFile.FullName).ToString.ToLower.Contains("readonly") Then
                File.SetAttributes(localFile.FullName, FileAttributes.Normal)
            End If

            If form.UseBlob Then
                Dim frmBusinessExt As New FormBusinessExt
                form.Path = frmBusinessExt.CopyBlobToTemp(form, False)
                frmBusinessExt = Nothing
            Else
                form.Path = localFile.FullName
                Try
                    If File.Exists(localFile.FullName) = False OrElse serverFile.LastWriteTime > localFile.LastWriteTime Then
                        serverFile.CopyTo(localFile.FullName, True)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            CopyFormAssociatedFiles(serverFile, localFile, ".js")
            CopyFormAssociatedFiles(serverFile, localFile, ".css")

        Catch ex As IOException
            ZTrace.WriteLineIf(ZTrace.IsError, "Error al copiar el formulario del servidor a la carpeta del usuario. Descripción: " & ex.ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            localFile = Nothing
            serverFile = Nothing
            tempDir = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Utilizado para copiar archivos asociados al formulario como los css o js
    ''' </summary>
    ''' <param name="server">Archivo de origen</param>
    ''' <param name="local">Archivo de destino</param>
    ''' <param name="extension">Extensión del archivo</param>
    ''' <remarks></remarks>
    Private Sub CopyFormAssociatedFiles(ByVal server As FileInfo, ByVal local As FileInfo, ByVal extension As String)
        Try
            Dim localPath As String = BuildFormPath(local, extension)

            Dim fi As New FileInfo(localPath)

            Dim serverPath As String = BuildFormPath(server, extension)
            Dim fo As New FileInfo(serverPath)

            If fo.Exists AndAlso ((fi.Exists = False OrElse fi.Exists AndAlso fo.LastWriteTime > fi.LastWriteTime)) Then
                fo.CopyTo(localPath, True)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function BuildFormPath(ByVal file As FileInfo, ByVal extension As String)
        Return file.FullName.Remove(file.FullName.Length - file.Extension.Length, file.Extension.Length) + extension
    End Function

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
        Dim fi As FileInfo = Nothing
        Try
            fi = New FileInfo(path)

            If fi.Exists = True Then
                AxWebBrowser1.Navigate(fi.FullName)
            Else
                Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
                AxWebBrowser1.Navigate(formDinamico.ShowErrorMessage("No se ha encontrado el formulario virtual"))
            End If

        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As System.ExecutionEngineException
            Zamba.Core.ZClass.raiseerror(ex)
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
    ''' <param name="path">Path a un formulario dinámico</param>
    ''' <param name="formId">Id de un formulario dinámico</param>
    ''' <param name="formName">Nombre de un formulario dinámico</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Created     Se arma el formulario dinámico según el formId que se recibe. Si no se obtienen datos del formulario
    '''                                         entonces el WebBrowser muestra un mensaje de error
    ''' </history>
    Public Sub Navigate(ByVal path As String, ByVal formId As Integer, ByVal formName As String)
        Try
            If Not String.IsNullOrEmpty(path) Then
                If File.Exists(path) Then
                    AxWebBrowser1.Navigate(path)
                Else
                    MessageBox.Show(_FORMLOADINGERRORMESSAGE,
                                    "Error al acceder al formulario",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)

                    Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
                    AxWebBrowser1.Navigate(formDinamico.ShowErrorMessage(_FORMLOADINGERRORMESSAGE))
                    formDinamico.Dispose()
                    formDinamico = Nothing
                End If
            Else
                Dim dsDynamicForm As DataSet = FormBusiness.GetDynamicForm(formId)
                Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()

                If (dsDynamicForm.Tables(0).Rows.Count > 0) Then
                    IsDynamicForm = True
                    AxWebBrowser1.Navigate(formDinamico.CreateTable(dsDynamicForm, formName))
                Else
                    AxWebBrowser1.Navigate(formDinamico.ShowErrorMessage("No se ha encontrado el formulario virtual"))
                End If

                dsDynamicForm.Dispose()
                dsDynamicForm = Nothing
                formDinamico.Dispose()
                formDinamico = Nothing
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
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "DocumentComplete")

        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor
        Try
            If FlagAsigned = False Or bolmodified = False Then 'OrElse Me.flagrecover = True Then
                If Not localResult Is Nothing AndAlso Not localResult.Childs Is Nothing AndAlso localResult.Childs.Values Is Nothing Then
                    For Each o As Object In localResult.Childs.Values
                        If String.Compare(o.fullpath.ToString(), e.Url.ToString) = 0 Then
                            '[sebastian] 10-06-2009 se agrego cast para salvar warning
                            RaiseEvent LinkSelected(DirectCast(o, Result))
                            Exit Sub
                        End If
                    Next
                End If


                FlagAsigned = True
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se cargan los datos del formulario")
                AsignValues(AxWebBrowser1.Document, localResult)
                Try
                    AxWebBrowser1.Height = DirectCast(AxWebBrowser1.Document.DomDocument, mshtml.HTMLDocumentClass).body.ScrollHeight
                Catch
                    Try
                        AxWebBrowser1.Height = DirectCast(DirectCast(AxWebBrowser1.Document.DomDocument, mshtml.HTMLDocumentClass).body, mshtml.HTMLBodyClass).IHTMLElement2_scrollHeight
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End Try


                ZTrace.WriteLineIf(ZTrace.IsVerbose, "La carga de datos finalizó con éxito")
            End If
        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Cursor = cur
        ResumeLayout()
    End Sub

    Dim FlagAsigned As Boolean
    Dim flagrecover As Boolean

#End Region

#End Region

#Region "AsigValues"
    'Dim Reload As Boolean
    Public Sub RefreshData(ByRef result As Result)
        If localResult IsNot Nothing AndAlso result IsNot Nothing Then
            If localResult.ID = result.ID Then
                localResult = result
                AsignValues(AxWebBrowser1.Document, localResult)

            End If
        Else
            ZTrace.WriteLineIf(ZTrace.IsError, "Exception/Error: Se encontró un error inesperado. Por favor comunicarse con sistemas.")
        End If
    End Sub

    Dim bolmodified As Boolean = False

    ''' <summary>
    ''' Guarda los valores de los atributos en la BD
    ''' </summary>
    ''' <param name="doc1"></param>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Sub AsignValues(ByVal doc1 As HtmlDocument, ByVal Result As Result)
        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor
        Dim Element As HtmlElement
        DateIndexsList.Clear()
        DateTimeIndexsList.Clear()

        If (Result.GetType().Name.ToLower.Contains("taskresult")) Then getInteligentText()

        Try
            'El Form debe tener Id = zamba_(doctypeid) o Id = zamba_(Nombre del DocType)
            'Los controles deben tener Id = "zamba(Id de Atributo)"  o Id = "zamba_(Nombre del indice)"
            If doc1.All.Count > 0 Then
                FlagAsigned = True


                If IsNothing(Result) = False Then

                    Dim UseIndexsRights As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, CInt(Result.DocTypeId))

                    Dim IndexsRights As Hashtable = Nothing
                    If UseIndexsRights Then IndexsRights = UserBusiness.Rights.GetIndexsRights(Result.DocTypeId, Membership.MembershipHelper.CurrentUser.ID, True, True)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de atributos: " & Result.Indexs.Count)
                    For i As Int32 = 0 To Result.Indexs.Count - 1
                        Try

                            Element = doc1.GetElementById("ZAMBA_INDEX_" & Result.Indexs(i).ID)
                            If IsNothing(Element) Then
                                Element = doc1.GetElementById("ZAMBA_INDEX_" & Result.Indexs(i).ID & "S")
                                If IsNothing(Element) Then
                                    Element = doc1.GetElementById("ZAMBA_INDEX_" & Result.Indexs(i).ID & "N")
                                End If
                            End If

                            If Not IsNothing(Element) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, Result.Indexs(i).Name & " ZAMBA_INDEX_" & Result.Indexs(i).ID)

                                bolmodified = True
                                AsignValue(Result.Indexs(i), Element)

                                If UseIndexsRights AndAlso Not IsNothing(Element) Then

                                    Dim IR As IndexsRightsInfo = DirectCast(IndexsRights(Result.Indexs(i).ID), IndexsRightsInfo)
                                    For Each indexid As Int64 In IndexsRights.Keys
                                        If indexid = Result.Indexs(i).ID Then
                                            'aplica permiso Visible
                                            If IR.GetIndexRightValue(RightsType.IndexView) = False Then
                                                'Oculta el atributo
                                                Element.Style = "display:none"
                                                'Oculta el label del indice
                                                Dim htmlElement_label As HtmlElement = doc1.GetElementById("ZAMBA_INDEX_" & Result.Indexs(i).ID & "_LBL")
                                                If Not IsNothing(htmlElement_label) Then
                                                    htmlElement_label.Style = "display:none"
                                                End If
                                            End If

                                            ' [Gaston]    12/05/2009  Si el formulario no es un formulario dinámico entonces no se aplica el permiso de 
                                            '                         edición, porque sino estaría entrando en conflicto con un Atributo que tenga 
                                            '                         "sólo lectura" (en caso de que se haya configurado un Atributo para sólo lectura)
                                            If Not ((doc1.Body.InnerHtml.ToLower.Contains("<form id=")) AndAlso (doc1.Body.InnerHtml.ToLower.Contains("name=frmmain"))) Then
                                                'aplica permiso Edicion
                                                If Element.Enabled Then Element.Enabled = IR.GetIndexRightValue(RightsType.IndexEdit)
                                            End If
                                            Exit For
                                        End If
                                    Next
                                End If
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro el Atributo: " & Result.Indexs(i).Name & "ZAMBA_INDEX_" & Result.Indexs(i).ID)
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    Next

                    Try
                        If DateIndexsList.Count > 0 Then
                            For Each id As String In DateIndexsList
                                AxWebBrowser1.Document.InvokeScript("makeCalendar", New Object() {id})
                            Next
                        End If

                        If DateTimeIndexsList.Count > 0 Then
                            For Each id As String In DateTimeIndexsList
                                AxWebBrowser1.Document.InvokeScript("makeDateTimeCalendar", New Object() {id})
                            Next
                        End If

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        Element = doc1.GetElementById("ZAMBA_IMAGE")
                        LoadImage(Result, Element)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try

                    Dim CurrentForm As IZwebForm = FormBusiness.GetForm(localResult.CurrentFormID)
                    If Not IsNothing(CurrentForm) AndAlso CurrentForm.useRuleRights = True Then
                        If TypeOf localResult Is ITaskResult Then
                            'Por cada regla buscar si en el formulario hay un boton que la llame
                            'Si la regla esta deshabilitada, deshabilitar el boton
                            Dim task As ITaskResult = DirectCast(localResult, ITaskResult)
                            Dim rule As IWFRuleParent
                            Dim tagId, tempTagId As String

                            Try
                                Dim currentLockedUser As String
                                Dim IsTaskLocked As Boolean = WFTaskBusiness.LockTask(task.TaskId, currentLockedUser)

                                'todo: ml: ver si se puede obtener de manera mas directa los tags
                                For Each el As HtmlElement In AxWebBrowser1.Document.Body.All
                                    If el.Id IsNot Nothing AndAlso el.Id.Length > 11 Then
                                        tagId = el.Id.ToLower()
                                        If tagId.ToLower.Contains("zamba_rule_") Then
                                            'Se verifica la correcta construcción del tag
                                            tempTagId = GetRuleIdFromTag(tagId)
                                            If Not String.IsNullOrEmpty(tempTagId) Then
                                                'ML: Se puede mejorar mas no instanciando y verificando con el id y el dsrules del cache
                                                rule = Zamba.Core.WFRulesBusiness.GetInstanceRuleById(GetRuleIdFromTag(tempTagId), True)
                                                If rule IsNot Nothing AndAlso IsTaskLocked = False AndAlso (
                                                    Not rule.Enable _
                                                    OrElse Not WFRulesBusiness.GetIsRuleEnabled(task.UserRules, rule) _
                                                    OrElse Not WFRulesBusiness.GetStateOfHabilitationOfState(rule, task.State.ID)) Then

                                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ocultando botón en el formulario para la regla: " & rule.ID)
                                                    el.Style = "display:none"
                                                End If
                                            Else
                                                LogBadTag(tagId, CurrentForm.Name)
                                            End If
                                        End If
                                    End If
                                Next
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            Finally
                                rule = Nothing
                                task = Nothing
                            End Try
                        End If
                    End If

                    Try

                        Dim WorkerProcess As New System.ComponentModel.BackgroundWorker
                        WorkerProcess.WorkerReportsProgress = True
                        RemoveHandler WorkerProcess.DoWork, AddressOf WP_DoWork
                        RemoveHandler WorkerProcess.RunWorkerCompleted, AddressOf RunWorkerCompleted
                        AddHandler WorkerProcess.DoWork, AddressOf WP_DoWork
                        AddHandler WorkerProcess.RunWorkerCompleted, AddressOf RunWorkerCompleted

                        WorkerProcess.RunWorkerAsync()

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Dim elements As List(Of String)
                    Try
                        elements = getItems("zvar")
                        For Each Str As String In elements
                            'zamba_zvar_nombrevariable
                            Dim varname As String
                            If Str.Contains("(") Then
                                varname = Str.Replace("zamba_zvar(", "zvar(")
                                varname = Str.Replace("zvar(", String.Empty).Replace(")", String.Empty)
                            Else
                                varname = Str.Replace("zamba_zvar(", "zvar(")
                                varname = Str.Remove(0, "zvar_".Length)
                            End If

                            Element = doc1.GetElementById(Str)
                            If IsNothing(Element) Then
                                Element = doc1.GetElementById("zamba_" & Str)
                            End If

                            If Not IsNothing(Element) Then

                                AsignVarValue(varname, Element)

                                If Str.Contains("(") Then
                                    '         Element.Id = varname
                                End If
                            End If
                        Next
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    Finally
                        elements = Nothing
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

                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        If _disableInputControls Then
            doDisableInputControls()
        End If
        Cursor = cur
    End Sub

    Private Sub LogBadTag(ByVal tag As String, ByVal formName As String)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "El formulario contiene tags que no se han podido resolver de manera correcta.")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Debe corregir el tag del formulario para obtener un correcto funcionamiento.")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tag erroneo es: " & tag)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "El formulario afectado es: " & formName)
    End Sub

    ''' <summary>
    ''' Obtiene el id de la regla desde el tag html
    ''' </summary>
    ''' <param name="tagId">Tag Id del html</param>
    ''' <returns>Id de regla</returns>
    ''' <remarks></remarks>
    Public Function GetRuleIdFromTag(ByVal tagId As String) As Long
        tagId = tagId.Replace("zamba_rule_", String.Empty)
        If tagId.Contains("/") Then
            tagId = tagId.Substring(0, tagId.IndexOf("/"))
        End If
        If IsNumeric(tagId) Then
            Return CLng(tagId)
        Else
            Return Nothing
        End If
    End Function


    '''
    Public Sub SetDocumentFile(ByVal FileName As String)
        If Not localResult Is Nothing Then
            localResult.File = FileName
        End If
    End Sub
    ''' <summary>
    ''' Se encarga de habilitar las reglas dependiendo del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function enableRules(ByVal doc1 As HtmlDocument)
        If Not IsNothing(rulesIds) Then
            Dim task As TaskResult
            Dim indexsAndVariables As List(Of IndexAndVariable)
            Dim WFIndexAndVariableBusiness As WFIndexAndVariableBusiness
            Dim DtUsersAndGroup As DataTable
            Dim Dt2 As DataTable

            Try
                task = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(localResult.ID, 0)

                For i As Int32 = 0 To rulesIds.Count - 1
                    Dim wfstepID As Int64 = WFStepBusiness.GetStepIdByRuleId(rulesIds(i), True)
                    Dim DtOptions As DataTable = WFRulesBusiness.GetRuleOptionsDT(True, wfstepID)
                    Dim DV As New DataView(DtOptions)

                    Dim _enabled As Boolean = True

                    'If wfstepID = task.StepId Then
                    'Obtiene el valor 
                    Dim selectionvalue As RulePreferences = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(wfstepID, rulesIds(i), False)
                    'Se Evalua el valor de la variable seleccion 
                    Select Case selectionvalue
                        'Caso de trabajo con Estados
                        Case RulePreferences.HabilitationSelectionState
                            _enabled = True
                            'Se Obtienen los ids de estados DESHABILITADOS
                            DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeState

                            'Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
                            'Coincidencia, se deshabilita la regla
                            For Each r As DataRow In DV.ToTable.Rows
                                If Int32.Parse(r.Item("ObjValue").ToString) = task.StateId Then
                                    _enabled = False
                                    Exit For
                                End If
                            Next
                            'Caso de trabajo con Usuarios o Grupos
                        Case RulePreferences.HabilitationSelectionUser
                            _enabled = True
                            'Se Obtienen los ids de USUARIOS DESHABILITADOS
                            DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeUser
                            '                           Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser, True)
                            'Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
                            'Coincidencia, se deshabilita la regla
                            For Each r As DataRow In DV.ToTable.Rows
                                If Int64.Parse(r.Item("ObjValue").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                    _enabled = False
                                    Exit For
                                End If
                            Next
                            'si no se deshabilito la regla por usuario se intenta deshabilitar por grupo
                            If _enabled = True Then
                                'Se Obtienen los ids de GRUPOS DESHABILITADOS
                                DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeGroup
                                '                                Dt2 = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup, True)
                                'Por cada Id de Grupo se recorren sus usuario y se comparan con el id de usuario logeado, en cada de encontrar
                                'Coincidencia, se deshabilita la regla
                                For Each r As DataRow In DV.ToTable.Rows
                                    Dim uids As List(Of Int64) = UserGroupBusiness.GetUsersIds(Int64.Parse(r.Item("ObjValue").ToString()), True)
                                    If Not IsNothing(uids) Then
                                        For Each uid As Int64 In uids

                                            If uid = Membership.MembershipHelper.CurrentUser.ID Then
                                                _enabled = False
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    If _enabled = False Then Exit For
                                Next
                            End If
                        Case RulePreferences.HabilitationSelectionIndexAndVariable
                            WFIndexAndVariableBusiness = New WFIndexAndVariableBusiness()
                            indexsAndVariables = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(rulesIds(i))
                            'Se obtienen los ids de variables
                            _enabled = True

                            'Se Obtienen los ids de estados DESHABILITADOS
                            '                            Dim Dt As DataTable = WFRulesBusiness.GetRuleOption(rulesIds(i), RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeIndexAndVariable, True)

                            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                If validar(_IndexAndVariable, task, WFIndexAndVariableBusiness) = True Then
                                    DV.RowFilter = "ruleid = " & rulesIds(i) & " and SectionId= " & RuleSectionOptions.Habilitacion & " And ObjectId =" & RulePreferences.HabilitationTypeIndexAndVariable & " and ObjValue= " & _IndexAndVariable.ID
                                    '                                    Dt.DefaultView.RowFilter = "ObjValue=" & _IndexAndVariable.ID
                                    If DV.ToTable().Rows.Count > 0 Then
                                        _enabled = False
                                    Else
                                        _enabled = True
                                        Exit For
                                    End If
                                End If
                            Next

                        Case RulePreferences.HabilitationSelectionBoth
                            _enabled = True
                            'Se Obtienen los ids de estados DESHABILITADOS
                            Dim Dt1 As DataTable = WFBusiness.recoverDisableItemsBoth(rulesIds(i)).Tables(0)

                            'Filtro por estado
                            Dt1.DefaultView.RowFilter = "ObjValue='" & task.StateId & "' and ObjectId in (37,38)"
                            Dt2 = Dt1.DefaultView.ToTable()

                            If Dt2.Rows.Count > 0 Then
                                'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
                                DtUsersAndGroup = WFStepBusiness.GetStepUserGroupsIdsAsDS(WFStepBusiness.GetStepIdByRuleId(rulesIds(i), True), Membership.MembershipHelper.CurrentUser.ID)
                                WFIndexAndVariableBusiness = New WFIndexAndVariableBusiness()
                                indexsAndVariables = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(rulesIds(i))

                                For Each r As DataRow In Dt2.Rows
                                    'Valido por grupo y usuario
                                    If Int32.Parse(r.Item("ObjExtraData").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                        _enabled = False
                                        For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                            If validar(_IndexAndVariable, task, WFIndexAndVariableBusiness) Then
                                                Dt1.DefaultView.RowFilter = "ObjExtraData='" & _IndexAndVariable.ID & "' and ObjectId =62 and ObjValue ='" & task.StateId & "'"

                                                If Dt1.DefaultView.ToTable().Rows.Count > 0 Then
                                                    _enabled = False
                                                Else
                                                    _enabled = True
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If

                                    For Each rUser As DataRow In DtUsersAndGroup.Rows
                                        If rUser.Item(0).ToString() = r.Item("ObjExtraData").ToString() Then
                                            _enabled = False
                                            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                                If validar(_IndexAndVariable, task, WFIndexAndVariableBusiness) Then
                                                    Dt1.DefaultView.RowFilter = "ObjExtraData='" & _IndexAndVariable.ID & "' and ObjectId =62 and ObjValue ='" & task.StateId & "'"

                                                    If Dt1.DefaultView.ToTable().Rows.Count > 0 Then
                                                        _enabled = False
                                                    Else
                                                        _enabled = True
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next
                                Next
                            End If
                    End Select
                    'Else
                    '_enabled = False
                    'End If
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

    Private Function validar(ByVal _IndexAndVariable As IndexAndVariable, ByVal _TaskResult As TaskResult, ByVal IndexAndVariableBusiness As WFIndexAndVariableBusiness) As Boolean
        Dim IndexAndVariableConfList As List(Of IndexAndVariableConfiguration) = IndexAndVariableBusiness.GetIndexAndVariableConfiguration(_IndexAndVariable.ID)
        Dim TextoInteligente As New Core.TextoInteligente()

        Try
            For Each IndexAndVariableConf As IndexAndVariableConfiguration In IndexAndVariableConfList
                Dim value1 As String = IndexAndVariableConf.Name
                If IndexAndVariableConf.Manual = "N" Then
                    For Each i As Index In _TaskResult.Indexs
                        If value1 = i.ID Then
                            value1 = i.Data
                            Exit For
                        End If
                    Next
                Else
                    value1 = WFRuleParent.ReconocerVariablesValuesSoloTexto(value1)
                    value1 = TextoInteligente.ReconocerCodigo(value1, _TaskResult)
                End If

                Dim value2 As String = IndexAndVariableConf.Value
                value2 = WFRuleParent.ReconocerVariablesValuesSoloTexto(value2)
                value2 = TextoInteligente.ReconocerCodigo(value2, _TaskResult)

                Dim comparator As Comparadores
                'Le asigno el comparador al IfIndex
                Select Case IndexAndVariableConf.Operador
                    Case "="
                        comparator = Comparadores.Igual
                    Case "<>"
                        comparator = Comparadores.Distinto
                    Case "<"
                        comparator = Comparadores.Menor
                    Case ">"
                        comparator = Comparadores.Mayor
                    Case "<="
                        comparator = Comparadores.IgualMenor
                    Case "Contiene"
                        comparator = Comparadores.Contiene
                    Case "Empieza"
                        comparator = Comparadores.Empieza
                    Case "Termina"
                        comparator = Comparadores.Termina
                    Case ">="
                        comparator = Comparadores.IgualMayor
                End Select
                ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                If ToolsBusiness.ValidateComp(value1, value2, comparator, False) = False Then
                    Return False
                ElseIf _IndexAndVariable.Operador.ToLower() = "or" Then
                    Return True
                End If
            Next
        Finally
            TextoInteligente = Nothing
        End Try

        Return True
    End Function

    ' [AlejandroR] 28/12/09 - Created
    ' Deshabilita todos los controles de tipo input para que no se pueda editar el formulario
    Private Function doDisableInputControls()
        Dim tag As String

        Try
            For Each el As HtmlElement In AxWebBrowser1.Document.Body.All
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
            doc1 = AxWebBrowser1.Document
            Dim tag As New List(Of String)
            'Dim tagimg As New ArrayList
            For Each el As HtmlElement In doc1.Body.All
                If Not String.IsNullOrEmpty(el.GetAttribute("src")) AndAlso (el.GetAttribute("src").Contains(".js") OrElse el.GetAttribute("src").ToLower.Contains("javascript:false")) Then
                    tag.Add(el.OuterHtml)
                End If
            Next

            For Each el As HtmlElement In doc1.Body.GetElementsByTagName("INPUT")
                If Not IsNothing(el.Id) Then
                    If el.Id.ToLower.Contains("zamba_save") OrElse el.Id.ToLower.Contains("zamba_cancel") Then
                        tag.Add(el.OuterHtml)
                    End If
                End If
            Next

            For Each el As HtmlElement In doc1.Body.GetElementsByTagName("img")
                el.SetAttribute("src", "cid:" & el.GetAttribute("src").Substring(el.GetAttribute("src").Replace("/", "\").LastIndexOf("\") + 1))
            Next

            Dim strHtml As String = doc1.Body.InnerHtml 'DirectCast(DirectCast(DirectCast(doc1.DomDocument, System.Object), mshtml.HTMLDocumentClass).documentElement, mshtml.IHTMLElement).innerHTML


            For Each remtag As String In tag
                If strHtml.Contains(remtag) Then
                    strHtml = strHtml.Replace(remtag, String.Empty)
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
                    strHtml = strHtml.Replace(straux, String.Empty)
                End If
                If strHtml.ToLower.IndexOf("onfocus=showcalendarcontrol(this);") <> -1 Then
                    straux = strHtml.Substring(strHtml.ToLower.IndexOf("onfocus=showcalendarcontrol(this);"), ("onfocus=showcalendarcontrol(this);").Length)
                    strHtml = strHtml.Replace(straux, String.Empty)
                End If
            End If

            Return strHtml
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Dim WithEvents WP As System.ComponentModel.BackgroundWorker

    Private Sub WP_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles WP.DoWork
        Try
            If Me IsNot Nothing AndAlso Not Disposing AndAlso Not isDisposed Then
                'Application.DoEvents()
                Invoke(New DLoadLists(AddressOf LoadAllLists))
            End If
        Catch ex As Threading.SynchronizationLockException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Try
            If sender IsNot Nothing Then
                RemoveHandler DirectCast(sender, System.ComponentModel.BackgroundWorker).DoWork, AddressOf WP_DoWork
                RemoveHandler DirectCast(sender, System.ComponentModel.BackgroundWorker).RunWorkerCompleted, AddressOf RunWorkerCompleted
                DirectCast(sender, System.ComponentModel.BackgroundWorker).Dispose()
                sender = Nothing
            End If
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            'Finally
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ZFUNCTION")
            AxWebBrowser1.Document.InvokeScript("ZFUNCTION")
        Finally
            If Not IsNothing(AxWebBrowser1) AndAlso AxWebBrowser1.Disposing = False AndAlso AxWebBrowser1.IsDisposed = False Then
                AxWebBrowser1.Document.InvokeScript("SetGlobalParams", WebServiceURl)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ProcessAjaxFunctions")
                AxWebBrowser1.Document.InvokeScript("ProcessAjaxFunctions")
            End If
        End Try
    End Sub

    Private Sub RefreshAsociatedTable(ByVal DocTypeId As Long)
        Dim docTypesIDs As New List(Of String)
        docTypesIDs.Add(DocTypeId)
        Dim Asociated As DataTable = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(localResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100)), docTypesIDs, False)
        Dim Mydoc As HtmlDocument = AxWebBrowser1.Document

        Dim Table As HtmlElement = Mydoc.GetElementById("zamba_associated_documents_" + DocTypeId.ToString())

        If Not IsNothing(Table) Then
            LoadTable(Table, Mydoc, localResult, Asociated, False)
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
        Try
            If (Not AxWebBrowser1.Document Is Nothing AndAlso Not AxWebBrowser1.Document.ActiveElement Is Nothing AndAlso AxWebBrowser1.Document.ActiveElement.Id Is Nothing) OrElse (Not AxWebBrowser1.Document Is Nothing AndAlso Not AxWebBrowser1.Document.ActiveElement Is Nothing AndAlso Not AxWebBrowser1.Document.ActiveElement.TagName Is Nothing AndAlso String.Compare(AxWebBrowser1.Document.ActiveElement.TagName, "body", True)) Then
                If Disposing = False AndAlso isDisposed = False Then
                    AxWebBrowser1.SuspendLayout()
                    Dim cur As Cursor = Cursor
                    Cursor = Cursors.WaitCursor
                    'Primero cargo las listas, despues lo asociados
                    LoadAutosustitutionLists()
                    LoadSearchList()

                    Dim doc1 As System.Windows.Forms.HtmlDocument ' mshtml.HTMLDocumentClass
                    doc1 = AxWebBrowser1.Document

                    Try
                        '[Sebastian 09-06-2009] se agrego condicion para que al momento de cargar los asociados, solo
                        'lo haga si ya se inserto el form virtual.
                        If localResult.ID <> 0 Then

                            Dim Mydoc As HtmlDocument = AxWebBrowser1.Document
                            If Not IsNothing(Mydoc.Body.InnerHtml) Then
                                If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_associated_documents") = True OrElse Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                    '******Obtengo todos los docTypesIDs a ser usados asi solo obtengo esos asociados en vez de todos
                                    Dim blnAll As Boolean
                                    Dim docTypesIds As New List(Of String)
                                    Dim elements As List(Of String)
                                    Dim AsociatedTable As HtmlElement

                                    'Se cargan los asociados marcados como importantes
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_importants"))
                                    AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_importants")
                                    If Not IsNothing(AsociatedTable) Then
                                        Dim ImportantAsociatedResults As DataTable = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(localResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100)), docTypesIds, True)
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Busca asociado por entidad {0}en la coleccion", localResult))

                                        If Not IsNothing(ImportantAsociatedResults) AndAlso ImportantAsociatedResults.Rows.Count > 0 Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Si no encuentra asociados{0}en la coleccion", ImportantAsociatedResults))

                                            If Not IsNothing(AsociatedTable) Then
                                                Dim tags As String = AsociatedTable.Name
                                                LoadTable(AsociatedTable, Mydoc, localResult, ImportantAsociatedResults, False)
                                            End If

                                            elements = getItems("zamba_associated_documents_importants_")

                                            For Each str As String In elements
                                                Dim docTypeID As String = str.Replace("zamba_associated_documents_importants_", String.Empty)
                                                Dim number As Int64

                                                If Int64.TryParse(docTypeID, number) Then
                                                    Dim Table As HtmlElement = Mydoc.GetElementById(str)

                                                    If Not IsNothing(Table) Then
                                                        ImportantAsociatedResults.DefaultView.RowFilter = "doc_type_id=" & number
                                                        LoadTable(Table, Mydoc, localResult, ImportantAsociatedResults.DefaultView.ToTable(), False)
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If


                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents"))
                                    AsociatedTable = Mydoc.GetElementById("zamba_associated_documents")
                                    If Not IsNothing(AsociatedTable) Then
                                        Dim tags As String = AsociatedTable.Name

                                        If String.IsNullOrEmpty(tags) = False AndAlso tags.ToLower().StartsWith("doc_type_ids(") Then
                                            Dim doc_types_ids As String = tags.Replace("doc_type_ids(", String.Empty).Replace(")", String.Empty)
                                            'Se recorren los asociados y se guarda el ID en un listado
                                            For Each docTypeID As String In doc_types_ids.Split(",")
                                                If docTypesIds.Contains(docTypeID) = False Then
                                                    docTypesIds.Add(docTypeID)
                                                End If
                                            Next
                                        Else
                                            blnAll = True
                                        End If
                                    End If

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_WF"))
                                    AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_WF")
                                    If Not IsNothing(AsociatedTable) Then
                                        blnAll = True
                                    End If

                                    If blnAll = True Then
                                        docTypesIds.Clear()
                                    Else

                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_"))
                                        elements = getItems("zamba_associated_documents_")
                                        For Each str As String In elements
                                            Dim docTypeID As String = str.Replace("zamba_associated_documents_", String.Empty)
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_ con EntityId: {0}", docTypeID))
                                            If docTypesIds.Contains(docTypeID) = False Then
                                                docTypesIds.Add(docTypeID)
                                            End If
                                        Next

                                        If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                            Dim lastDocTypeID As String

                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_asoc"))
                                            elements = getItems("zamba_asoc")
                                            For Each Str As String In elements
                                                If Str.IndexOf("index", 0, StringComparison.InvariantCultureIgnoreCase) <> 0 Then
                                                    Dim values As String() = Str.ToLower().Replace("zamba_asoc_", String.Empty).Split("_")
                                                    Dim docTypeID As String = values(0)

                                                    If lastDocTypeID <> docTypeID Then
                                                        lastDocTypeID = docTypeID
                                                        If docTypesIds.Contains(docTypeID) = False Then
                                                            docTypesIds.Add(docTypeID)
                                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se agrega ID Entidad {0} en la coleccion", docTypeID))
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If

                                    Dim Asociated As DataTable

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Busca asociados para la entidad {0}", localResult.Name))

                                    Asociated = DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(localResult, Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100)), docTypesIds, False)

                                    If Not IsNothing(Asociated) AndAlso Asociated.Rows.Count > 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se encontraron {0}  documentos asociados", Asociated.Rows.Count))

                                        'Se cargan las tablas que tengan documentos especificos
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents"))
                                        AsociatedTable = Mydoc.GetElementById("zamba_associated_documents")
                                        If Not IsNothing(AsociatedTable) Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("TAG con ID: zamba_associated_documents encontrado"))
                                            Dim tags As String = AsociatedTable.Name
                                            'Se cargan las tablas asociadas o de WF
                                            If String.IsNullOrEmpty(tags) = False AndAlso tags.ToLower().StartsWith("doc_type_ids(") Then
                                                Dim doc_types_ids As String = tags.Replace("doc_type_ids(", String.Empty).Replace(")", String.Empty)
                                                Asociated.DefaultView.RowFilter = "doc_type_id in(" & doc_types_ids & ")"
                                                LoadTable(AsociatedTable, Mydoc, localResult, Asociated.DefaultView.ToTable(), False)
                                            Else
                                                LoadTable(AsociatedTable, Mydoc, localResult, Asociated, False)
                                            End If
                                        End If

                                        'Se cargan los documentos asociados del result que estan unicamente en wf
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_WF"))
                                        AsociatedTable = Mydoc.GetElementById("zamba_associated_documents_WF")
                                        If Not IsNothing(AsociatedTable) Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("TAG con ID: zamba_associated_documents_WF encontrado"))
                                            LoadTable(AsociatedTable, Mydoc, localResult, Asociated, True)
                                        End If


                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_"))
                                        elements = getItems("zamba_associated_documents_")
                                        For Each str As String In elements
                                            Dim docTypeID As String = str.Replace("zamba_associated_documents_", String.Empty)
                                            Dim number As Int64

                                            If Int64.TryParse(docTypeID, number) Then
                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_associated_documents_ con EntityId: {0}", docTypeID))
                                                Dim Table As HtmlElement = Mydoc.GetElementById(str)

                                                If Not IsNothing(Table) Then
                                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Cargando Asociados de la Entidad  {0} ", number))

                                                    Asociated.DefaultView.RowFilter = "doc_type_id=" & number
                                                    LoadTable(Table, Mydoc, localResult, Asociated.DefaultView.ToTable(), False)
                                                End If
                                            End If
                                        Next


                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Buscando TAG con ID: zamba_asoc"))
                                        If Mydoc.Body.InnerHtml.ToLower.Contains("zamba_asoc") = True Then
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("El documento contiene {0} atributos asociados", Asociated))
                                            Dim res As DataRow
                                            Dim docTypeTable As DataTable
                                            Dim lastDocTypeID As String

                                            Dim list As SortedList = getIndexItems("zamba_asoc")
                                            For Each Str As String In list.Keys
                                                If Str.ToLower.Contains("index") Then
                                                    Dim values As String() = Str.Replace("zamba_asoc_", String.Empty).Split("_")
                                                    Dim docTypeID As String = values(0)
                                                    Dim indexName As String = values(2)
                                                    Dim indexID As Int64
                                                    If Int64.TryParse(indexName, indexID) = False Then
                                                        indexID = IndexsBusiness.GetIndexIdByName(indexName.Replace("_s", String.Empty).Replace("_n", String.Empty))
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("reemplazo zamba_asoc {0} en la coleccion", indexName))

                                                    End If

                                                    'Valida que no se hayan ingresado atributos mal escritos
                                                    If indexID > 0 Then
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("valido atributos mal escritos {0} en la coleccion", indexID))
                                                        Dim indice As Index = ZCore.GetIndex(indexID)
                                                        'Verifica que se cargue una sola vez
                                                        If lastDocTypeID <> docTypeID Then
                                                            lastDocTypeID = docTypeID

                                                            'filtro por doc_type_id y cargo los atributos de la entidad
                                                            Asociated.DefaultView.RowFilter = "doc_type_id=" & docTypeID
                                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("filtro por doctypeid {0} en la coleccion", docTypeID))
                                                            docTypeTable = Asociated.DefaultView.ToTable()

                                                            If docTypeTable.Rows.Count > 0 Then
                                                                res = docTypeTable.Rows(0)
                                                            Else
                                                                res = Nothing
                                                            End If
                                                        End If

                                                        If Not IsNothing(res) Then
                                                            If indice.DropDown = IndexAdditionalType.AutoSustitución OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
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
                                                End If
                                            Next

                                        End If
                                    End If
                                End If
                            End If
                        End If
                        AxWebBrowser1.Document.InvokeScript("ZFUNCTION")

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        'Finally
                        'AxWebBrowser1.ResumeLayout()
                    End Try
                    Cursor = cur
                End If
            End If
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If (Not AxWebBrowser1.Document Is Nothing AndAlso
                Not AxWebBrowser1.Document.ActiveElement Is Nothing AndAlso
                AxWebBrowser1.Document.ActiveElement.Id Is Nothing) OrElse
                (Not AxWebBrowser1.Document Is Nothing AndAlso
                 Not AxWebBrowser1.Document.ActiveElement Is Nothing AndAlso
                 Not AxWebBrowser1.Document.ActiveElement.TagName Is Nothing AndAlso
                 String.Compare(AxWebBrowser1.Document.ActiveElement.TagName, "body", True)) Then
                If Disposing = False AndAlso isDisposed = False Then
                    AxWebBrowser1.ResumeLayout()
                End If
            End If
        End Try
    End Sub

    Private Function getItems(ByVal elementName As String) As List(Of String)
        Dim elements As New List(Of String)
        Dim Mydoc As HtmlDocument = AxWebBrowser1.Document

        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim body As String = Mydoc.Body.InnerHtml

                While body.ToLower().Contains(elementName)
                    Dim index As Int32 = body.IndexOf(elementName, StringComparison.InvariantCultureIgnoreCase)
                    Dim elem As String = body.Substring(index)
                    elem = elem.Substring(0, elem.IndexOf(" ")).Replace(Chr(34), String.Empty)

                    If elem.Contains(">") Then
                        elem = elem.Substring(0, elem.IndexOf(">"))
                    End If
                    elements.Add(elem)

                    body = body.Substring(index)
                    body = body.Replace(elem, String.Empty)
                End While
            End If
        End If
        Return elements
    End Function
    'Funcion que obtiene el elemento con id "idsConTextoInteligente" (si existiese), que contiene en su atributo value los ids de los elementos que contengan
    'texto inteligente, busca cada uno de esos elementos por el id y los guarda en una lista, que es posteriormente pasada a la funcion que resuelte el texto inteligente.
    'El elemento con id "idsConTextoInteligente" es creado en el document.ready de zamba.js
    Private Function getInteligentText()
        Dim Mydoc As HtmlDocument = AxWebBrowser1.Document
        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim elementIds As HtmlElement = Mydoc.GetElementById("idsConTextoInteligente")
                If elementIds IsNot Nothing Then
                    Dim arrayIds() As String = Split(elementIds.GetAttribute("value"), ",")
                    Dim elements As New List(Of HtmlElement)
                    If (arrayIds.Length > 0) Then
                        For Each id As String In arrayIds
                            elements.Add(Mydoc.GetElementById(id))
                        Next
                        resolveInteligentText(elements)
                    End If
                End If
            End If
        End If

    End Function
    'Funcion que obtiene una lista de elementos que contengan texto inteligente, busca el atributo que lo contenga y finalmente lo resuelva
    Private Function resolveInteligentText(ByRef elements As List(Of HtmlElement))
        Try

            Dim outerHtml As String
            Dim index As Int32
            Dim indexFinal As Int64
            Dim IndexInicioFound As Boolean = False
            Dim attr As String
            For Each e As HtmlElement In elements
                outerHtml = e.OuterHtml
                index = outerHtml.IndexOf(">>.<<")
                While ((index > -1) And (IndexInicioFound = False))
                    If outerHtml.Chars(index) <> " " Then
                        If (outerHtml.Chars(index) = "=") Then
                            indexFinal = index - 1
                        End If
                        index = index - 1
                    Else
                        IndexInicioFound = True
                        attr = outerHtml.Substring(index + 1, (indexFinal - index))
                    End If
                End While

                If (IndexInicioFound = True) Then
                    IndexInicioFound = False
                    Dim AttrValue As String = e.GetAttribute(attr)
                    Dim value As String = TextoInteligente.ReconocerCodigo(AttrValue, localResult)
                    e.SetAttribute(attr, value)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todos los atributos ordenados por doc_type
    ''' </summary>
    ''' <param name="elementName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getIndexItems(ByVal elementName As String) As SortedList
        Dim elements As New SortedList
        Dim Mydoc As HtmlDocument = AxWebBrowser1.Document

        If Not IsNothing(Mydoc) AndAlso Not IsNothing(Mydoc.Body) Then
            If Not String.IsNullOrEmpty(Mydoc.Body.InnerHtml) Then
                Dim body As String = Mydoc.Body.InnerHtml.ToLower()

                While body.Contains(elementName)
                    Dim index As Int32 = body.IndexOf(elementName)
                    Dim elem As String = body.Substring(index)
                    elem = elem.Substring(0, elem.IndexOf(" ")).Replace(Chr(34), String.Empty)

                    If elem.Contains(">") Then
                        elem = elem.Substring(0, elem.IndexOf(">"))
                    End If
                    elements.Add(elem, elem)

                    body = body.Substring(index)
                    body = body.Replace(elem, String.Empty)
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
                    Dim ElementType As String = "text"
                    Try
                        ElementType = CStr(E.DomElement.type).ToLower
                    Catch
                        Try
                            ElementType = DirectCast(E.DomElement, mshtml.HTMLInputElementClass).IHTMLInputElement_type.ToLower()
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End Try
                    Select Case ElementType
                        'Select Case CStr(DirectCast(E.DomElement, mshtml.HTMLInputElementClass).type).ToLower
                        Case "text", "hidden"



                            If I.DropDown = IndexAdditionalType.AutoSustitución OrElse I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If I.Data = Nothing Then
                                    'Verifica si en el objeto existe algún valor o no
                                    If IsNothing(E.DomElement.value) Then
                                        E.DomElement.value = String.Empty
                                        'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = String.Empty
                                    End If
                                Else
                                    E.DomElement.value = I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID)
                                End If

                            Else

                                If I.Data = Nothing Then
                                    'Verifica si en el objeto existe algún valor o no
                                    E.DomElement.value = String.Empty
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = String.Empty
                                Else
                                    E.DomElement.value = I.Data
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data
                                End If

                                If I.Type = IndexDataType.Fecha Then
                                    DateIndexsList.Add(E.Id)
                                ElseIf I.Type = IndexDataType.Fecha_Hora Then
                                    DateTimeIndexsList.Add(E.Id)
                                End If

                            End If


                            WriteDataIndexTrace(I, True)

                        Case "file"


                            If Not E.DomElement Is Nothing AndAlso Not E.DomElement.value Is Nothing Then
                                ' ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando Valor de Archivo Adjunto a la tarea:       " & E.DomElement.value.ToString())
                                SetDocumentFile(E.DomElement.value)
                            End If

                        Case "checkbox"


                            If IsNothing(I.Data) OrElse I.Data = "0" OrElse I.Data = String.Empty Then
                                E.DomElement.checked = 0
                            Else
                                E.DomElement.checked = 1
                            End If


                            WriteDataIndexTrace(I, True)

                        Case "radio"


                            If IsNothing(I.Data) Then
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '   ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            ElseIf I.Data = "0" Then

                                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es nothing, es 0, o empty, Valor del checked N= " & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("N") Then
                                    E.DomElement.checked = True
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = True
                                ElseIf E.Id.ToUpper().EndsWith("S") = True Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked N= " & E.Id & " " & E.DomElement.checked.ToString)

                            ElseIf I.Data = "1" Then
                                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es 1. Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = True
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = True
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '      ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            Else
                                '   ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es otra cosa. Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)
                                If E.Id.ToUpper().EndsWith("S") Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                ElseIf E.Id.ToUpper().EndsWith("N") = False Then
                                    E.DomElement.checked = False
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).checked = False
                                End If
                                '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked S =" & E.Id & " " & E.DomElement.checked.ToString)

                            End If
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & ". Value " & E.DomElement.checked.ToString)
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If
                            WriteDataIndexTrace(I, False)

                        Case "select-one"

                            ' WriteDataIndexTrace(I, False)

                            If IsNothing(I.Data) Then
                                E.DomElement.value = I.Data
                                'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = I.Data
                            Else
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = String.Empty
                                    'DirectCast(E.DomElement, mshtml.HTMLInputElementClass).value = String.Empty
                                End If
                            End If
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & " " & E.DomElement.value.ToString)
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If
                            WriteDataIndexTrace(I, False)
                    End Select
                Case "select"
                    'E.InnerHtml = ""
                    If E.Children.Count = 0 OrElse IsDynamicForm Then
                        ' WriteDataIndexTrace(I, False)

                        If IsNothing(I.Data) = False OrElse I.Data <> "0" OrElse I.Data <> String.Empty Then
                            Select Case CInt(I.DropDown)
                                Case 1
                                    'Andres 8/8/07 - Se guarda el valor pero no se usa 
                                    'Dim Lista As ArrayList
                                    'If Reload = False Then
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
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se agrega Elemento para carga de Lista", E.Id))
                                        Dim ListItem As New ListItem(I, E.Id)
                                        SearchListToLoad.Add(ListItem)
                                    End If
                                    'End If
                                Case 2
                                    'If Reload = False Then
                                    'Si no esta cargada, cargo solo el item seleccionado
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False OrElse readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        Dim desc As String = AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type)
                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data & " - " & desc
                                        E.AppendChild(tag)
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se agrega Elemento para carga de Lista", E.Id))
                                        Dim ListItem As New ListItem(I, E.Id)
                                        ASListToLoad.Add(ListItem)
                                    End If
                                    'End If
                                    If Not E.Children(I.Data) Is Nothing Then E.Children(I.Data).SetAttribute("selected", True)

                                Case IndexAdditionalType.AutoSustituciónJerarquico
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
                                        ASListToLoad.Add(ListItem)
                                    End If
                                    'End If
                                    If Not E.Children(I.Data) Is Nothing Then E.Children(I.Data).SetAttribute("selected", True)
                                Case IndexAdditionalType.DropDownJerarquico
                                    Dim readonli As String = E.GetAttribute("ReadOnly")

                                    If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                        Dim tag As HtmlElement = E.Document.CreateElement("option")

                                        tag.SetAttribute("selected", I.Data)
                                        tag.SetAttribute("value", I.Data)
                                        tag.InnerText = I.Data
                                        E.AppendChild(tag)

                                    Else

                                        Dim ListItem As New ListItem(I, E.Id)
                                        SearchListToLoad.Add(ListItem)
                                    End If
                            End Select
                        End If

                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id)
                        '        If Not IsNothing(E.DomElement) AndAlso Not IsNothing(E.DomElement.value) Then
                        '            ZTrace.WriteLineIf(ZTrace.IsInfo, "Value " & E.DomElement.value.ToString)
                        '        End If
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If
                        WriteDataIndexTrace(I, False)
                    End If

                Case "textarea"

                    ' WriteDataIndexTrace(I, False)

                    If Not IsNothing(I.Data) Then
                        E.SetAttribute("value", I.Data)
                    Else
                        'Verifica si en el objeto existe algún valor o no
                        If IsNothing(E.DomElement.value) Then
                            E.SetAttribute("value", String.Empty)
                        End If
                    End If

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

            ' Filtra por tipo de control...
            Select Case CStr(E.TagName).ToLower
                Case "input" ', "SELECT"
                    Dim ElementType As String = "text"
                    Try
                        ElementType = CStr(E.DomElement.type).ToLower
                    Catch
                        Try
                            ElementType = DirectCast(E.DomElement, mshtml.HTMLInputElementClass).IHTMLInputElement_type.ToLower()
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End Try

                    Select Case ElementType
                        Case "text", "hidden"
                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = String.Empty
                                End If
                            Else
                                E.DomElement.value = VariablesInterReglas.Item(VarName)
                            End If
                        Case "checkbox"
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable = " & VariablesInterReglas.Item(VarName))
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If


                            If IsNothing(VariablesInterReglas.Item(VarName)) OrElse VariablesInterReglas.Item(VarName) = "0" OrElse VariablesInterReglas.Item(VarName) = String.Empty Then
                                E.DomElement.checked = 0
                            Else
                                E.DomElement.checked = 1
                            End If
                        Case "radio"
                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable = " & VariablesInterReglas.Item(VarName))
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If


                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                ' ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es otra cosa")

                                If E.Id.ToUpper().EndsWith(")") Then
                                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                End If
                            ElseIf String.Compare(Trim(VariablesInterReglas.Item(VarName).ToString), "0") = 0 Then
                                '  ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es nothing, es 0, o empty")
                                If E.Id.ToUpper().EndsWith(")") Then
                                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked = " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                End If
                            ElseIf Trim(CType(VariablesInterReglas.Item(VarName), String)) = "1" Then
                                '  ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es 1")
                                If E.Id.ToUpper().EndsWith(")") Then
                                    '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked =" & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = True
                                End If
                            Else
                                '   ZTrace.WriteLineIf(ZTrace.IsInfo, "El Data es otra cosa")

                                If E.Id.ToUpper().EndsWith(")") Then
                                    '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor del checked N= " & E.Id & " " & E.DomElement.checked)
                                    E.DomElement.checked = False
                                End If
                            End If
                        Case "select-one"

                            If IsNothing(VariablesInterReglas.Item(VarName)) Then
                                E.DomElement.value = VariablesInterReglas.Item(VarName)
                            Else
                                'Verifica si en el objeto existe algún valor o no
                                If IsNothing(E.DomElement.value) Then
                                    E.DomElement.value = String.Empty
                                End If
                            End If
                    End Select
                Case "select"
                    If E.Children.Count = 0 Then

                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable" & VariablesInterReglas.Item(VarName))
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        If IsNothing(VariablesInterReglas.Item(VarName)) = False OrElse VariablesInterReglas.Item(VarName) <> "0" OrElse VariablesInterReglas.Item(VarName) <> String.Empty Then
                            'If Reload Then
                            'Si no esta cargada, cargo solo el item seleccionado
                            Dim readonli As String = E.GetAttribute("ReadOnly")

                            If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                                Dim tag As HtmlElement = E.Document.CreateElement("option")

                                tag.SetAttribute("selected", VariablesInterReglas.Item(VarName))
                                tag.SetAttribute("value", VariablesInterReglas.Item(VarName))
                                tag.InnerText = VariablesInterReglas.Item(VarName)
                                E.AppendChild(tag)
                            End If
                            'End If

                            'If ZTrace.IsVerbose Then
                            '    Try
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & ". Value " & E.DomElement.value)
                            '    Catch ex As Exception
                            '        Zamba.Core.ZClass.raiseerror(ex)
                            '    End Try
                            'End If

                        End If
                    End If
                Case "textarea"
                    'If ZTrace.IsVerbose Then
                    '    Try
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Id " & E.Id & ". Value " & E.DomElement.value)
                    '    Catch ex As Exception
                    '        Zamba.Core.ZClass.raiseerror(ex)
                    '    End Try
                    '    Try
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor variable" & VariablesInterReglas.Item(VarName))
                    '    Catch ex As Exception
                    '        Zamba.Core.ZClass.raiseerror(ex)
                    '    End Try
                    'End If

                    If Not IsNothing(VariablesInterReglas.Item(VarName)) Then
                        E.SetAttribute("value", VariablesInterReglas.Item(VarName))
                    Else
                        E.SetAttribute("value", String.Empty)
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
                Case "div"
                    If Not IsNothing(VariablesInterReglas.Item(VarName)) Then
                        E.InnerText = VariablesInterReglas.Item(VarName)
                    Else
                        E.InnerText = String.Empty
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

            If withDataTemp Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributo " & I.Name & " (" & I.ID & ") DATA: " & I.Data & " DATATEMP: " & I.DataTemp)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributo " & I.Name & " (" & I.ID & ") DATA: " & I.Data)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private ASListToLoad As New Generic.List(Of ListItem)
    Private SearchListToLoad As New Generic.List(Of ListItem)

    Private Class ListItem
        Implements IDisposable

        Public Index As Index
        Public ElementId As String

        Public Sub New(ByVal Index As Index, ByVal ElementId As String)
            Me.Index = Index
            Me.ElementId = ElementId
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            ElementId = Nothing
            Index = Nothing
        End Sub
    End Class

    Private Sub LoadSearchList()
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cargo Listas de busqueda" & SearchListToLoad.Count & " a las " & Now.ToString)
            If SearchListToLoad.Count > 0 Then
                Dim doc1 As HtmlDocument ' mshtml.HTMLDocumentClass
                doc1 = AxWebBrowser1.Document

                Dim e As HtmlElement = Nothing
                Dim listOptions As List(Of String)
                Dim tableOptions As DataTable
                Dim max As Integer
                Dim prompt As String

                If Not IsNothing(doc1) Then
                    For Each listitem As ListItem In SearchListToLoad

                        e = doc1.GetElementById(listitem.ElementId)
                        If Not e Is Nothing Then
                            'Si es jerarquico
                            If listitem.Index.DropDown = IndexAdditionalType.DropDownJerarquico Then
                                tableOptions = IndexsBussinesExt.GetHierarchicalTableByValue(listitem.Index.ID, listitem.Index.HierarchicalParentID, GetLocalIndexValue(listitem.Index.HierarchicalParentID), True)

                                If Not tableOptions Is Nothing Then
                                    max = tableOptions.Rows.Count
                                    For i As Integer = 0 To max - 1
                                        If IsDBNull(tableOptions.Rows(i)("Value")) Then
                                            prompt = String.Empty
                                        Else
                                            prompt = tableOptions.Rows(i)("Value")
                                        End If

                                        e.AppendChild(GetOptionTag(prompt, prompt, listitem.Index.Data))
                                    Next
                                End If
                            Else
                                listOptions = IndexsBusiness.GetDropDownList(listitem.Index.ID)

                                If Not listOptions Is Nothing Then
                                    max = listOptions.Count
                                    For i As Integer = 0 To max - 1
                                        e.AppendChild(GetOptionTag(listOptions(i), listOptions(i), listitem.Index.Data))
                                    Next
                                End If
                            End If

                            If String.IsNullOrEmpty(listitem.Index.Data) Then
                                e.SetAttribute("value", String.Empty)
                            End If

                            If Not listitem.Index.HierarchicalChildID Is Nothing Then
                                Dim countChild As Integer = listitem.Index.HierarchicalChildID.Count

                                For j As Integer = 0 To countChild - 1
                                    If listitem.Index.HierarchicalChildID(j) > 0 Then
                                        'Si tiene hijo para actualizar al disparar el change
                                        'Se instancian las variables para que queden en memoria a la hora de llamar al metodo anonimo
                                        Dim firedElement As HtmlElement = e
                                        Dim firedIndexID As Long = listitem.Index.ID
                                        Dim firedChildIndexID As Long = listitem.Index.HierarchicalChildID(j)
                                        'Se agrega el handler para jerarquicos
                                        e.AttachEventHandler("onchange", Sub() HierarchicalChange(firedElement, firedIndexID, firedChildIndexID))
                                    End If
                                Next

                                If Not String.IsNullOrEmpty(listitem.Index.Data) Then
                                    e.DomElement.value = listitem.Index.Data
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin carga lista")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            SearchListToLoad.Clear()
        End Try
    End Sub

    Private Sub LoadAutosustitutionLists()
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cargo Listas " & ASListToLoad.Count)
            If ASListToLoad.Count > 0 Then
                Dim doc1 As HtmlDocument ' mshtml.HTMLDocumentClass
                doc1 = AxWebBrowser1.Document
                Dim i As Int64
                Dim e As HtmlElement = Nothing
                If Not IsNothing(doc1) Then
                    For Each listitem As ListItem In ASListToLoad
                        e = doc1.GetElementById(listitem.ElementId)
                        If Not e Is Nothing Then
                            Dim table As DataTable

                            If listitem.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                table = IndexsBussinesExt.GetHierarchicalTableByValue(listitem.Index.ID, listitem.Index.HierarchicalParentID, GetLocalIndexValue(listitem.Index.HierarchicalParentID), True)
                            Else
                                table = AutoSubstitutionBusiness.GetIndexData(listitem.Index.ID, False)
                            End If

                            Dim tagName As String = String.Empty

                            If e.Children.Count = table.Rows.Count = True Then
                                Exit Try
                            End If

                            For i = 0 To table.Rows.Count - 1
                                Dim optionCode As String = Convert.ToString(table.Rows(i).Item(0)).Trim
                                Dim optionValue As String = Convert.ToString(table.Rows(i).Item(1)).Trim

                                If String.IsNullOrEmpty(optionCode) Then
                                    tagName = "A definir"
                                Else
                                    If UserPreferences.getValue("ShowAutosustitutionListsIndexId", UPSections.UserPreferences, True) Then
                                        tagName = String.Concat(optionCode, " - ", optionValue)
                                    Else
                                        tagName = optionValue
                                    End If
                                    e.AppendChild(GetOptionTag(optionCode, tagName, listitem.Index.Data))
                                End If
                            Next

                            If String.IsNullOrEmpty(listitem.Index.Data) Then
                                e.SetAttribute("value", String.Empty)
                            End If

                            If Not listitem.Index.HierarchicalChildID Is Nothing Then
                                Dim countChild As Integer = listitem.Index.HierarchicalChildID.Count

                                For j As Integer = 0 To countChild - 1

                                    If listitem.Index.HierarchicalChildID(j) > 0 Then
                                        If listitem.Index.HierarchicalChildID(j) > 0 Then
                                            'Si tiene hijo para actualizar al disparar el change
                                            'Se instancian las variables para que queden en memoria a la hora de llamar al metodo anonimo
                                            Dim firedElement As HtmlElement = e
                                            Dim firedIndexID As Long = listitem.Index.ID
                                            Dim firedChildIndexID As Long = listitem.Index.HierarchicalChildID(j)
                                            'Se agrega el handler para jerarquicos
                                            e.AttachEventHandler("onchange", Sub() HierarchicalChange(firedElement, firedIndexID, firedChildIndexID))

                                            'HierarchicalChange(firedElement, firedIndexID, firedChildIndexID)
                                        End If
                                    End If
                                Next

                                If Not String.IsNullOrEmpty(listitem.Index.Data) Then
                                    e.DomElement.value = listitem.Index.Data
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fin carga listas")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            ASListToLoad.Clear()
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
    Private Sub LoadTable(ByVal table As HtmlElement, ByRef mydoc As HtmlDocument, ByVal ParentResult As IResult, ByVal AsociatedResults As DataTable, ByVal onlyWF As Boolean)
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Cargando en la Tabla {0} asociados", AsociatedResults.Rows.Count.ToString))
            If Not IsNothing(table) AndAlso AsociatedResults.Rows.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Cargando en la Tabla con ID: ", If(Not IsNothing(table.Id), table.Id, String.Empty)))
                'Valido si existe el nodo TBODY , para usarlo para las rows 
                If table.Children.Count > 0 Then
                    For Each child As HtmlElement In table.Children
                        If String.Compare(child.TagName.ToLower(), "tbody") = 0 Then
                            table = child
                            Exit For
                        End If
                    Next
                End If

                For Each CurrentResult As DataRow In AsociatedResults.Rows

                Next

                Dim tableId As String = table.Id

                If (Not IsNothing(table)) Then
                    If Not IsNothing(table.InnerHtml) Then
                        table.InnerText = String.Empty
                    End If

                    Dim dt As DataTable = ParseResult(ParentResult, AsociatedResults, If(Not IsNothing(table.Id), table.Id, String.Empty), onlyWF)
                    'quitar columnas del table segun permisos.
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Aplicando a la Tabla {0} Results ", dt.Rows.Count.ToString))

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Aplicando a la Tabla los permisos de Grilla asociados para EntidadPadre: {0} y Entidad Hija {1}", ParentResult.DocTypeId, AsociatedResults.Rows(0).Item("doc_type_id")))
                    dt = SetColumnsByRights(dt, ParentResult.DocTypeId, AsociatedResults.Rows(0).Item("doc_type_id"))

                    Dim IsOldGrid As Boolean

                    If (UserPreferences.getValue("UseKendoGridInForms", UPSections.UserPreferences, False)) Then
                        IsOldGrid = If(table.GetAttribute("ClassName").Equals("tablesorter"), False, True)
                    End If

                    If IsOldGrid Then
                        LoadKendoGrid(table, dt, mydoc, tableId)
                    Else
                        LoadTableHeader(table, dt.Columns, mydoc)
                        LoadTableBody(table, dt.Rows, mydoc)

                    End If
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("La Tabla a Cargar esta en Nothing o no Hay Asociados para Cargar."))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Function LoadKendoGrid(ByRef table As HtmlElement, ByVal dt As DataTable, ByRef mydoc As HtmlDocument, ByVal tableId As String)

        For Each column As DataColumn In dt.Columns
            dt.Columns(dt.Columns.IndexOf(column)).ColumnName = column.ColumnName.Replace(" ", "_")
        Next

        If dt.Rows.Count > 0 Then
            Dim divID As String = "ZKGrid_" + tableId
            Dim DataTableJson As String = FormBusiness.DataTableToJson(dt)
            Dim DivContainer As HtmlElement = mydoc.CreateElement("div")
            DivContainer.SetAttribute("Id", divID)
            DivContainer.SetAttribute("ClassName", "ZKGrid")
            Dim ScriptContainer As HtmlElement = mydoc.CreateElement("script")
            Dim kendoGridScript As String
            Dim kendoGridScriptBuild As New StringBuilder

            'Se genera el SCRIPT que arma la grilla de Kendo
            kendoGridScriptBuild.Append("$(""#" + divID + """).kendoGrid({")
            kendoGridScriptBuild.Append("dataSource: {")
            kendoGridScriptBuild.Append("data: {")
            kendoGridScriptBuild.Append("""items"" :" & DataTableJson)
            kendoGridScriptBuild.Append("},")
            kendoGridScriptBuild.Append("schema: {")
            kendoGridScriptBuild.Append("data: ""items""")
            kendoGridScriptBuild.Append("}},")
            kendoGridScriptBuild.Append("columns: [")
            For Each column As DataColumn In dt.Columns
                kendoGridScriptBuild.Append("{ field: """)
                If (column.ColumnName = "Ver") Then
                    kendoGridScriptBuild.Append(column.ColumnName.Replace(" ", "_") & """, title: """ & column.ColumnName & """, template:'<input type=""button"" Name=""zamba_asoc_375281_1018"" value=""Ver"" id=""zamba_asoc_375281_1018"" onclick=""SetAsocId(this);"" />'")
                Else
                    kendoGridScriptBuild.Append(column.ColumnName.Replace(" ", "_") & """, title: """ & column.ColumnName & """")
                End If
                kendoGridScriptBuild.Append("},")
            Next
            kendoGridScriptBuild.Remove(kendoGridScriptBuild.Length - 1, 1)
            kendoGridScriptBuild.Append("]")
            kendoGridScriptBuild.Append(",scrollable: false	,resizable: true, sortable: true")
            kendoGridScriptBuild.Append("});")
            kendoGridScript = kendoGridScriptBuild.ToString()

            ScriptContainer.InnerHtml = kendoGridScript



            Dim styleContainer As HtmlElement = mydoc.CreateElement("style")
            Dim kendoGridThStyle As New StringBuilder

            kendoGridThStyle.Append(".ZKGrid .k-grid-header .k-header{font-size:x-small;font-family: arial;background-color: #1A5276;color:red;}.ZKGrid .k-grid-header .k-header a{font-size:x-small;font-family: arial;color: white;}")

            styleContainer.InnerHtml = kendoGridThStyle.ToString()


            table.AppendChild(styleContainer)
            table.AppendChild(DivContainer)
            table.AppendChild(ScriptContainer)
        End If
    End Function

    Private Function SetColumnsByRights(dt As DataTable, parentResultID As Long, asociatedResultID As Long) As DataTable
        Try
            Dim dtColumnsRights As List(Of String) = RightsBusiness.GetAssociatedGridColumnsRightsCombined(Membership.MembershipHelper.CurrentUser.ID, asociatedResultID, parentResultID, True)

            Dim tempDataTable As DataTable = dt.Copy()

            For Each column As DataColumn In dt.Columns
                Dim isIndex As Boolean = False

                If Not IsDBNull(IndexsBusiness.GetIndexIdByName(column.ColumnName)) AndAlso IndexsBusiness.GetIndexIdByName(column.ColumnName) > 0 Then
                    isIndex = True
                End If

                If Not isIndex AndAlso Not column.ColumnName.Equals("ver", StringComparison.CurrentCultureIgnoreCase) Then
                    If Not dtColumnsRights.Contains(column.ColumnName) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Ocultando Columna {0}.", column.ColumnName))
                        tempDataTable.Columns.Remove(column.ColumnName)
                    End If
                End If

            Next
            Return tempDataTable
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return dt
        End Try
    End Function

    ''' <summary>
    ''' Carga las columnas header de un DataTable en el una tabla Html del documento
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="dcs"></param>
    ''' <remarks></remarks>
    Private Sub LoadTableHeader(ByRef table As HtmlElement, ByVal dcs As DataColumnCollection, ByRef mydoc As HtmlDocument)
        Dim Header As HtmlElement = mydoc.CreateElement("thead")

        Dim HeaderRow As HtmlElement = mydoc.CreateElement("tr")

        Dim HeaderColumn As HtmlElement = Nothing

        'Agrego columnas de atributos
        For Each Column As DataColumn In dcs
            HeaderColumn = mydoc.CreateElement("th")
            HeaderColumn.InnerHtml = Column.ColumnName
            HeaderRow.AppendChild(HeaderColumn)
        Next
        Header.AppendChild(HeaderRow)
        table.AppendChild(Header)
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
        Dim tBody As HtmlElement = mydoc.CreateElement("tbody")

        Dim i As Int32

        For Each dr As DataRow In drs
            CurrentRow = mydoc.CreateElement("tr")
            i = 0
            'Dim cont As Int32 = 0
            For Each CellValue As Object In dr.ItemArray

                CurrentCell = mydoc.CreateElement("td")
                '(pablo) 01-03-2011
                If CellValue.GetType.FullName.ToString = "System.DateTime" Then
                    If Not IsDBNull(CellValue) Then
                        Dim dateValue As String
                        dateValue = CellValue
                        If dateValue.Length >= 10 Then
                            Try
                                CurrentCell.InnerHtml = dateValue.ToString().Substring(0, 10)
                            Catch ex As Exception
                                CurrentCell.InnerHtml = ""
                            End Try
                        End If
                    End If
                Else
                    CurrentCell.InnerHtml = CellValue.ToString()
                End If

                CurrentRow.AppendChild(CurrentCell)

                i = i + 1
            Next
            tBody.AppendChild(CurrentRow)
            table.AppendChild(tBody)
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
            MessageBox.Show("Usted no dispone de los permisos necesarios para ejecutar la accion seleccionada.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else
            Dim StepId As Int64
            StepId = WFRulesBusiness.GetWFStepIdbyRuleID(ruleId)

            Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndStepIdAAndDocTypeId(result.ID, StepId, result.DocTypeId, 0)
            If IsNothing(task) Then
                task = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(result.ID, 0)
            End If


            Dim currentLockedUser As String
            If WFTaskBusiness.LockTask(task.TaskId, currentLockedUser) Then

                If Not IsNothing(task) Then
                    Dim tasks As New List(Of ITaskResult)(1)
                    tasks.Add(task)

                    Dim WFRS As New WFRulesBusiness
                    WFRS.ExecuteRule(ruleId, StepId, tasks, True, Nothing)

                    Dim userActionName As String = String.Empty
                    userActionName = WFRulesBusiness.GetRuleName(ruleId)
                    WFTaskBusiness.LogUserAction(task.TaskId, task.Name, task.DocTypeId, task.DocType.Name, task.StepId, task.State.Name, task.WorkId, userActionName)

                    If isDisposed = False Then
                        If IsNothing(ZOptBusiness.GetValue("RefreshIndexsInForm")) Then
                            ZOptBusiness.Insert("RefreshIndexsInForm", "True")
                        End If


                        If ZOptBusiness.GetValue("RefreshIndexsInForm") <> "" AndAlso CBool(ZOptBusiness.GetValue("RefreshIndexsInForm")) Then
                            '[sebastian 04-03-2009]
                            'De esta forma le asigno los valores de los atributos al formulario. Realizo un refresco
                            localResult.Indexs = task.Indexs
                            localResult.DocType.Indexs = task.Indexs
                            AsignValues(AxWebBrowser1.Document, localResult)
                        End If
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha podido recuperar la tarea - DocID: " & result.ID)
                End If
            Else
                MessageBox.Show("La tarea esta siendo ejecutada por " & currentLockedUser & ", no se podra accionar en este momento sobre la misma.", "Ejecucion de Tarea", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

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
                Dispose(True)
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
    Public Sub Print(ByVal FullPath As String)

        Dim doc As HtmlDocument
        Dim oKey As RegistryKey
        Dim elemCollection As HtmlElementCollection

        Try
            doc = AxWebBrowser1.Document
            'pablo
            If IsNothing(doc) Then
                Dim ServerFile As New FileInfo(FullPath)
                Dim rutaTemp As String = Tools.EnvironmentUtil.GetTempDir("\Officetemp").FullName & "\" & ServerFile.Name
                Navigate(rutaTemp)
                doc = AxWebBrowser1.Document
            End If
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
                        Dim frmDialog As New Zamba.AdminControls.frmMsgBoxConChkBox(
                            "Para que el formulario se imprima correctamente se deberá" & vbCrLf &
                            "modificar el registro. Presione 'Aceptar' para modificarlo" & vbCrLf &
                            "caso contrario presione 'Cancelar'.", "Confirmar acción", MessageBoxIcon.Question)

                        frmDialog.ShowDialog()

                        If frmDialog.DialogResult = DialogResult.OK Then
                            ' Se abre la subclave PageSetup, que contiene valores de configuración del documento html que se 
                            ' quiere imprimir. Se coloca True para indicar que se va a escribir en el registro
                            oKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\PageSetup", True)

                            If Not (IsNothing(oKey)) Then
                                ' Los valores header y footer se deshabilitan para que la página que se quiere imprimir no 
                                ' muestre el path al archivo, número de página y fecha actual
                                oKey.SetValue("header", String.Empty)
                                oKey.SetValue("footer", String.Empty)
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
                                oKey.SetValue("header", String.Empty)
                                oKey.SetValue("footer", String.Empty)
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
            If ((elem.OuterHtml.ToLower.Contains("type=button")) OrElse (elem.OuterHtml.ToLower.Contains("class=button"))) Then
                ' Se cambia a visible o a hidden, dependiendo del valor de type
                elem.Style = "VISIBILITY: " & type
            End If

        Next

    End Sub

    Protected Overrides Sub Finalize()
        CloseWebBrowser()
        Try
            If Not IsNothing(AxWebBrowser1) Then
                AxWebBrowser1.Dispose()
                AxWebBrowser1 = Nothing
            End If
        Catch
        End Try
        Dispose(False)
        MyBase.Finalize()
    End Sub

    Public Event ResultModified(ByVal Result As IResult)
    Public Event ShowAsociatedResult(ByVal Result As Result)
    Public Event showYellowPanel()
    Public Event CloseMail(Result As Object)

    Private Function ValidateIndexsRequiredEmpty() As Boolean
        For Each index As Index In localResult.Indexs

            If index.Required Then
                If index.Data = String.Empty OrElse index.DataTemp = String.Empty Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Sub AxWebBrowser1_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles AxWebBrowser1.Navigated
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Navigated")
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar los atributos (ubicados en el formulario) en la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/11/2008	Modified    Los atributos obligatorios no pueden estar vacíos
    ''' 	[Gaston]	11/12/2008	Modified    Invocación al evento ShowYellowPanel
    '''     [Gaston]	12/03/2009	Modified    Validación de atributos obligatorios o requeridos
    '''     [Ezequiel]	06/04/2009	Modified    Se inserta el formulario en el caso de que no se halla insertado aun
    '''     [Gaston]	08/05/2009	Modified    Validación de atributos de tipo exceptuable
    '''     [Gaston]    18/05/2009  Modified    Inserción de atributos con valores vacíos si es que no hay permisos de atributos requeridos para dichos atributos con valores vacíos
    '''     [Marcelo]   06/01/2010  Modified    Se agrega manejo de variables interreglas
    ''' </history>
    Sub RecoverIndexValues(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles AxWebBrowser1.Navigating
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Navigating")
        Dim doc1 As HtmlDocument = AxWebBrowser1.Document
        'Guardar atributos
        Dim blnSaveWithRule As Boolean = False
        Dim AbsoluteUri As String = e.Url.AbsoluteUri.ToLower
        Dim cur As Cursor

        '  ZTrace.WriteLineIf(ZTrace.IsInfo, "Recuperando los valores del formulario")

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
        ElseIf doc1.ActiveElement.Name.ToLower.Contains("cancel") Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cancela la carga.")
            FlagAsigned = False
            flagrecover = False
            RaiseEvent CancelChildRules(True)
            RaiseEvent FormClose()
            RaiseEvent FormCloseTab(True)
            Exit Sub
        ElseIf doc1.ActiveElement.Name.StartsWith("zamba_refresh_asoc_") Then

            Dim idAsoc = doc1.ActiveElement.Name.Replace("zamba_refresh_asoc_", String.Empty)
            Dim tableAsoc = doc1.GetElementById("zamba_associated_documents_" + idAsoc)

            If Not tableAsoc Is Nothing Then
                RefreshAsociatedTable(idAsoc)
            End If

        ElseIf doc1.ActiveElement.Name.StartsWith("zamba_refresh") Then
            cur = Cursor
            Cursor = Cursors.WaitCursor

            If localResult.GetType.FullName.ToString = "Zamba.Core.Result" Then
                RaiseEvent ReloadAsociatedResult(localResult)
            Else
                RaiseEvent RefreshTask(localResult)
            End If

            Cursor = cur
            Exit Sub
        ElseIf doc1.ActiveElement.Name.StartsWith("zamba_showOriginal") Then
            RaiseEvent ShowOriginal(localResult)
        End If

        cur = Cursor
        Cursor = Cursors.WaitCursor

        Try
            Dim canedit As Boolean = True

            'Si shared es false, valido los demas permisos
            If localResult.isShared = False AndAlso localResult.DocTypeId <> 0 Then
                'Si esta activado q solo el owner pueda modificar el doc y el user no es el owner
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes,
                                                     RightsType.OwnerChanges, localResult.DocTypeId) _
                                                 AndAlso Membership.MembershipHelper.CurrentUser.ID <> localResult.OwnerID Then
                    'Si alguno de los grupos no lo tiene tildado, entonces si lo puede modificar
                    If Not UserBusiness.Rights.DisableOwnerChanges(Membership.MembershipHelper.CurrentUser, localResult.DocTypeId) Then
                        canedit = False
                    End If
                End If
                'si shared es true, nadie puede modificar el documento
            ElseIf localResult.isShared = True AndAlso localResult.DocTypeId <> 0 Then
                canedit = False
            End If

            'si no cuenta con el permiso ReIndexar
            If Not UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes,
                                                     RightsType.ReIndex, localResult.DocTypeId) Then
                canedit = False
            End If


            If (FlagAsigned = True) Then
                flagrecover = True

                If AbsoluteUri.IndexOf("mailto:", StringComparison.CurrentCultureIgnoreCase) = -1 _
                    AndAlso AbsoluteUri.IndexOf("javascript:", StringComparison.CurrentCultureIgnoreCase) = -1 _
                    AndAlso e.TargetFrameName.ToLower <> "zamba_innerdoctype_variable" AndAlso AbsoluteUri.IndexOf("officetemp", StringComparison.CurrentCultureIgnoreCase) = -1 Then
                    e.Cancel = True
                End If

                Dim Element As HtmlElement
                Try

                    Dim elements As List(Of String) = getItems("zamba_zvar")
                    For Each Str As String In elements
                        Dim varname As String
                        If Str.Contains("(") Then
                            varname = Str.Replace("zamba_zvar(", String.Empty).Replace(")", String.Empty)
                            Element = doc1.GetElementById("zamba_zvar(" & varname & ")")
                            If Element Is Nothing Then Element = doc1.GetElementById("zamba_zvar(" & varname & ")")
                        Else
                            varname = Str.Remove(0, "zamba_zvar_".Length)
                            Element = doc1.GetElementById(Str)
                            If Element Is Nothing Then Element = doc1.GetElementById("zamba_zvar(" & varname & ")")
                        End If

                        If Not IsNothing(Element) Then
                            RecoverVarValue(varname, Element)

                            If Str.Contains("(") Then
                                Element.Id = varname
                            End If
                        End If
                    Next

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Dim ruleElement As HtmlElement = doc1.GetElementById("hdnRuleId")
                Dim asocElement As HtmlElement = doc1.GetElementById("hdnAsocId")

                If (Not IsNothing(ruleElement)) Then
                    If (ruleElement.Name.Length > 0) Then 'String.Compare(ruleElement.Name, String.Empty)
                        Dim ruleValue As String = ruleElement.Name
                        ruleElement.Name = String.Empty

                        If (Not IsNothing(asocElement)) Then
                            asocElement.Name = String.Empty
                        End If

                        If (ruleValue.Length > 10) Then ruleValue = ruleValue.Remove(0, "zamba_rule_".Length)

                        Dim ruleId As Int64

                        If String.Compare(ruleValue.ToLower(), "cancel") = 0 Then
                            '      ZTrace.WriteLineIf(ZTrace.IsInfo, "hdnRuleId = cancel")
                            RaiseEvent CancelChildRules(True)
                            'Si se van a salvar los datos
                        ElseIf String.Compare(ruleValue.ToLower(), "save") = 0 Then
                            '         ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando datos del formulario. Se dispararan los eventos de edición de atributos.")
                            saveValues(doc1, True, canedit, True)
                            'Si es una regla
                        ElseIf (Int64.TryParse(ruleValue, ruleId)) Then
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se guardaran los datos y luego se ejecutará la regla " & ruleId.ToString() & ". El evento Indices no será disparado.")
                            saveValues(doc1, True, canedit, False)
                            ExecuteRule(ruleId, localResult)
                            'Si es una regla con variable
                        ElseIf (ruleValue.ToLower().Contains("zvar")) Then
                            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "hdnRuleId = zvar")
                            Dim textitem2 As String
                            Dim textaux As String
                            Dim items As Array

                            Try
                                textitem2 = ruleValue.Split("_")(1)

                                While String.IsNullOrEmpty(textitem2) = False
                                    textaux = textitem2.Remove(0, 5)
                                    textitem2 = textitem2.Remove(0, textitem2.IndexOf(")") + 1)

                                    items = (textaux.Remove(textaux.IndexOf(")")).Split("="))

                                    If VariablesInterReglas.ContainsKey(items(0).ToString()) Then
                                        VariablesInterReglas.Item(items(0).ToString()) = items(1).ToString()
                                    Else
                                        VariablesInterReglas.Add(items(0).ToString(), items(1).ToString(), False)
                                    End If
                                End While
                            Finally
                                textaux = Nothing
                                textitem2 = Nothing
                                items = Nothing
                            End Try

                            If (Int64.TryParse(ruleValue.Split("_")(0), ruleId)) Then
                                saveValues(doc1, True, canedit, False)
                                ExecuteRule(ruleId, localResult)
                                If isDisposed = True Then
                                    Exit Sub
                                End If
                            End If
                        End If
                    Else
                        ruleElement = Nothing
                    End If
                End If
                If ruleElement Is Nothing Then ZTrace.WriteLineIf(ZTrace.IsInfo, "hdnRuleId = NOTHING")

                If (Not IsNothing(asocElement)) Then
                    If (String.Compare(asocElement.Name, String.Empty)) Then
                        Dim AsocValue As String = asocElement.Name
                        asocElement.Name = String.Empty

                        If (Not IsNothing(ruleElement)) Then
                            ruleElement.Name = String.Empty
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
                        asocElement = Nothing
                    End If
                End If
                If asocElement Is Nothing Then ZTrace.WriteLineIf(ZTrace.IsInfo, "hdnAsocId = NOTHING")

                If (IsNothing(ruleElement) AndAlso IsNothing(asocElement)) Then
                    '       ZTrace.WriteLineIf(ZTrace.IsInfo, "Los elementos hidden del formulario estaban vacíos. Se dispararan los eventos de edición de atributos.")
                    saveValues(doc1, blnSaveWithRule, canedit, True)
                End If

                If Not IsNothing(ruleElement) Then
                    If CloseFormWindowAfterRuleExecution Then
                        RaiseEvent CloseWindow()
                    End If
                End If

                If (AxWebBrowser1 IsNot Nothing AndAlso AxWebBrowser1.Document IsNot Nothing) Then

                    Dim ZPOSTBACKFUNCTIONELEMENT As HtmlElement = doc1.GetElementById("ZPOSTBACKFUNCTION")

                    If (Not IsNothing(ZPOSTBACKFUNCTIONELEMENT) AndAlso AxWebBrowser1 IsNot Nothing AndAlso AxWebBrowser1.Document IsNot Nothing) Then
                        If (String.Compare(ZPOSTBACKFUNCTIONELEMENT.Name, String.Empty)) Then
                            Dim ZPOSTBACKFUNCTION As String = ZPOSTBACKFUNCTIONELEMENT.Name
                            ZTrace.WriteLineIf(ZTrace.IsInfo, ZPOSTBACKFUNCTION)
                            AxWebBrowser1.Document.InvokeScript(ZPOSTBACKFUNCTION)

                        End If
                    End If
                End If
            End If

        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Cursor = cur
        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Finaliza el proceso de recuperación de valores")
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
        Dim WFTB As New WFTaskBusiness
        Try
            For Each I As Index In localResult.Indexs
                'Si el atributo se autocompleto, no hace falta recuperar su valor
                If IsNothing(modifiedIndex) OrElse modifiedIndex.Contains(I.ID) = False Then

                    oldDataValue = I.Data
                    oldDescriptionValue = I.dataDescription

                    Try
                        Dim Element As HtmlElement = doc1.GetElementById("ZAMBA_INDEX_" & I.ID)
                        If IsNothing(Element) Then
                            Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "S")
                            If IsNothing(Element) Then
                                Element = doc1.GetElementById("ZAMBA_INDEX_" & I.ID & "N")
                            End If
                        End If

                        If Not IsNothing(Element) Then
                            RecoverValue(I, Element)
                            listModifiedIndex.Add(I.ID)
                        End If

                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try

                    ' Si el valor del Atributo es Nothing entonces el valor del Atributo se encuentra vacío. Por lo tanto dicho Atributo se
                    ' almacena en una colección temporal (indexsNothing) que se mantiene si se logra atravesar los permisos de Atributo
                    ' requerido (al estar vacío el valor del Atributo sí o sí se necesita ingresar algún valor)
                    If (I.Data Is Nothing) Then
                        I.Data = String.Empty
                        I.DataTemp = String.Empty
                    End If

                    If IsNothing(oldDataValue) Then
                        oldDataValue = String.Empty
                    End If

                    If localResult.ID <> 0 And Not IsNothing(localResult.Doc_File) Then

                        If Not IsNothing(oldDataValue) AndAlso String.Compare(oldDataValue.Trim, I.Data.Trim) <> 0 Then
                            'ZTrace.WriteLineIf(ZTrace.IsInfo, "el valor de oldDataValue es : " + oldDataValue.Trim.ToString)
                            'ZTrace.WriteLineIf(ZTrace.IsInfo, "el valor de Data es : " + I.Data.Trim.ToString)
                            If IsNothing(modifiedIndex) Then
                                modifiedIndex = DataChanged(I)
                            End If
                            'Si existen cambios se guardan para el historial
                            If String.IsNullOrEmpty(oldDescriptionValue) Then
                                sbIndexHistory.Append("Atributo " & I.Name & " de " & oldDataValue.Trim & " a " & I.Data.Trim & ", ")
                            Else
                                sbIndexHistory.Append("Atributo " & I.Name & " de " & oldDataValue.Trim & " a " & I.Data.Trim & ", ")
                                sbIndexHistory.Append("Atributo " & I.Name & " de " & oldDescriptionValue.Trim & " a " & I.dataDescription.Trim & ", ")
                            End If
                        Else
                            listModifiedIndex.Remove(I.ID)
                        End If
                    End If

                End If
            Next

            'Codigo para insertar documentos utilizando un campo Input de tipo File.
            Dim element1 As HtmlElement

            element1 = doc1.GetElementById("docoriginal")

            If Not IsNothing(element1) Then
                SetDocumentFile(element1.DomElement.value)
            End If

            sbIndexHistory = sbIndexHistory.Remove(sbIndexHistory.Length - 2, 2)

            If doc1.ActiveElement.Name.ToLower.Contains("save") Or closeBrowser Then
                '     ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando atributos")

                Dim mandatoryIndexs As New ArrayList
                Dim bnModifyIndexs As Boolean = True

                ' Se verifica si el formulario tiene atributos obligatorios
                verifyRequiredIndexs(mandatoryIndexs, doc1)

                ' Si no hay atributos obligatorios
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
                If ((doc1.Body.InnerHtml.ToLower.Contains("<form id=")) AndAlso (doc1.Body.InnerHtml.ToLower.Contains("name=frmmain"))) Then
                    ' Si la verificación de atributos obligatorios fue correcta
                    If (bnModifyIndexs = True) Then

                        Dim frmCollection As HtmlElementCollection = doc1.GetElementsByTagName("FORM")
                        If (frmCollection.Count = 1) Then
                            Dim frmId As Int64 = frmCollection(0).Id

                            ' Se verifica si el formulario tiene atributos de tipo exceptuable
                            Dim exceptuableIndexs As New ArrayList
                            verifyExceptuableIndexs(frmId, exceptuableIndexs)

                            ' Si no hay atributos de tipo exceptuable
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
                    insertresult = Results_Business.InsertDocument(localResult, False, False, False, True, localResult.ISVIRTUAL)

                    Select Case insertresult

                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "No se pudo insertar por falta de atributos obligatorios")
                            MessageBox.Show("Hay atributos obligatorios sin completar", "Atencion", MessageBoxButtons.OK)

                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "Hay atributos con datos invalidos")
                            MessageBox.Show("Hay atributos con datos invalidos", "Atencion", MessageBoxButtons.OK)

                        Case InsertResult.Insertado

                            ZTrace.WriteLineIf(ZTrace.IsWarning, "Documento insertado")
                            If TypeOf (localResult) Is NewResult Then
                                DirectCast(localResult, NewResult).State = States.Insertado
                            End If
                            RaiseEvent SaveDocumentVirtualForm()
                            RaiseEvent ResultModified(localResult)
                            RaiseEvent FormCloseTab(False)


                        Case Else
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "No se pudo insertar el documento. Resultado: " & insertresult.ToString)
                            MessageBox.Show("No se pudo insertar el documento, por favor, consulte con el administrador del sistema.", "Atencion", MessageBoxButtons.OK)

                    End Select


                Else
                    '------------------------------------TODO-----------------------------------------
                    'Tomas: Se comenta ya que si un formulario se le modifican los atributos y luego
                    'se ejecuta una regla, los atributos son guardados, entonces al presionar guardar
                    'no detecta cambios y no ejecuta el evento Indices. Ver la manera de solucionar
                    'esto, ya que sería útil que no guarde siempre, solo cuando hay cambios aunque
                    'se haya ejecutado una regla de formulario de por medio.
                    '---------------------------------------------------------------------------------
                    'If listModifiedIndex.Count > 0 Then
                    If ((canedit) AndAlso (bnModifyIndexs = True)) Then

                        Dim Task As ITaskResult = WFTB.GetTaskByDocId(localResult.ID)

                        Dim currentLockedUser As String
                        If Task Is Nothing OrElse WFTaskBusiness.LockTask(Task.TaskId, currentLockedUser) Then

                            ' Se guardan las modificaciones en la base de datos
                            Dim rstBuss As New Results_Business()
                            rstBuss.SaveModifiedIndexData(localResult, True, blnRaiseEvent, listModifiedIndex)
                            rstBuss = Nothing

                            UserBusiness.Rights.SaveAction(localResult.ID, ObjectTypes.Documents, RightsType.ReIndex, sbIndexHistory.ToString)
                            sbIndexHistory = Nothing

                            RaiseEvent FormChanged(False)
                            RaiseEvent CancelChildRules(False)

                            If Not IsNothing(modifiedIndex) Then
                                modifiedIndex = Nothing
                                AsignValues(AxWebBrowser1.Document, localResult)
                            End If

                            ' Se actualizan los valores de los atributos para mostrarlos en pantalla
                            RaiseEvent ResultModified(localResult)
                            If closeBrowser = False Then
                                RaiseEvent FormClose()
                            Else
                                closeBrowser = False
                                localResult.Indexs = IndexsBusiness.GetResultIndexs(localResult.ID, localResult.DocTypeId, localResult.Indexs)
                                localResult.DocType.Indexs = localResult.Indexs
                                AsignValues(AxWebBrowser1.Document, localResult)
                            End If
                        ElseIf Not canedit Then
                            MessageBox.Show("No se pueden realizar cambios en los atributos", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If


                    End If
                End If
            End If
        Finally
            modifiedIndex = Nothing
            listModifiedIndex = Nothing
            WFTB = Nothing
        End Try
    End Sub

#Region "Formularios dinámicos"

#Region "Verificación de atributos requeridos"

    ''' <summary>
    ''' Método que sirve para verificar si el formulario tiene ìndices obligatorios
    ''' </summary>
    ''' <param name="mandatoryIndexs">Colección que contendrá los id's de atributos obligatorios</param>
    ''' <param name="doc1">Documento html del formulario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	12/03/2009	Created    
    ''' </history>
    Private Sub verifyRequiredIndexs(ByRef mandatoryIndexs As ArrayList, ByVal doc1 As HtmlDocument)

        Try

            ' Si el formulario es un formulario dinámico 
            If ((doc1.Body.InnerHtml.ToLower.Contains("<form id=")) AndAlso (doc1.Body.InnerHtml.ToLower.Contains("name=frmmain"))) Then

                Dim frmCollection As HtmlElementCollection = doc1.GetElementsByTagName("FORM")

                If (frmCollection.Count = 1) Then

                    ' Se obtienen los atributos requeridos del formulario dinámico y que sean True
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

                If (Membership.MembershipHelper.CurrentUser.Groups().Count > 0) Then

                    ' Se obtienen los atributos obligatorios del formulario común
                    Dim dt As DataTable = UserBusiness.Rights.GetMandatoryIndexs(localResult.DocTypeId, Membership.MembershipHelper.CurrentUser.Groups(), RightsType.IndexRequired)

                    ' Si hay atributos obligatorios
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
    ''' Método que verifica si falta completar todos o algún Atributo obligatorio
    ''' </summary>
    ''' <param name="mandatoryIndexs">Colección con los ids de atributos obligatorios o requeridos</param>
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
                        ' Se guarda el nombre del Atributo
                        mandatoryNameIndexs.Add(Index.Name)
                    End If

                    minCounter = minCounter + 1

                    If (minCounter = mandatoryIndexs.Count) Then
                        Exit For
                    End If

                End If

            Next

            ' Si todos los atributos obligatorios están completos
            If (mandatoryNameIndexs.Count = 0) Then
                ' Se retorna true para poder modificar los atributos y guardar las modificaciones en la base de datos
                Return (True)
            Else

                showErrorRequiredMessage(mandatoryNameIndexs)
                ' Se retorna false indicando que no se deben modificar los atributos ni que se debe guardar en la base de datos
                Return (False)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' Método que muestra un mensaje de error referido a atributos requeridos
    ''' </summary>
    ''' <param name="mandatoryNameIndexs">Colección que contiene el o los nombres de los atributos del cuál se requieren completar</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/05/2009	Created    Código original perteneciente al método "verifyThatTheMandatoryIndexsAreNotEmpty"
    ''' </history>
    Private Sub showErrorRequiredMessage(ByRef mandatoryNameIndexs As ArrayList)

        Try

            Dim builder As New StringBuilder

            builder.Append("Faltan completar los siguientes atributos requeridos: ")
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

#Region "Verificación de atributos de tipo exceptuable"

    ''' <summary>
    ''' Método que verifica si el formulario dinámico tiene atributos de tipo exceptuable
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="exceptuableIndexs">Colección que va a contener los id de atributos de tipo exceptuable (en caso de que el form. tenga alguno)</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	08/05/2009	Created    
    ''' </history>
    Private Sub verifyExceptuableIndexs(ByRef frmId As Int64, ByRef exceptuableIndexs As ArrayList)

        Try

            ' Se obtienen los atributos exceptuables del formulario dinámico 
            Dim dsExceptuableIndexs As DataSet = FormBusiness.getExceptuableIndexs(frmId)

            If (Not IsNothing(dsExceptuableIndexs)) Then

                ' Si el formulario dinámico tiene atributos exceptuables
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
    ''' Método que verifica si falta completar uno o más atributos de tipo exceptuable
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="exceptuableIndexs">Colección que contiene los id de atributos de tipo exceptuable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	08/05/2009	Created    
    '''     [Gaston]    11/05/2009  Modified
    ''' </history>
    Private Function verifyAccuracyOfExceptuableIndexs(ByVal frmId As Int64, ByRef exceptuableIndexs As ArrayList) As Boolean

        Try

            ' Se obtienen todos los datos referidos a los atributos de tipo exceptuable del formulario dinámico
            Dim dsDataExceptuableIndexs As DataSet = FormBusiness.getDataExceptuableIndexs(frmId)

            If (Not (IsNothing(dsDataExceptuableIndexs))) Then

                If (dsDataExceptuableIndexs.Tables(0).Rows.Count > 0) Then

                    ' Colección que almacena los atributos de tipo exceptuable incompletos
                    Dim incompleteExceptuableIndexs As New ArrayList

                    ' Por cada Atributo de tipo exceptuable
                    For Each exceptuableIndex As Long In exceptuableIndexs

                        ' Si el Atributo de tipo exceptuable se encuentra vacío entonces se verifican si sus atributos exceptuables tienen datos, si uno o más
                        ' de estos atributos no tienen datos entonces se prepara el mensaje de error
                        If (getIndexExceptuableValue(exceptuableIndex) = False) Then

                            ' Se obtienen los atributos exceptuables del Atributo de tipo exceptuable
                            Dim view As New DataView(dsDataExceptuableIndexs.Tables(0))
                            view.RowFilter = "IId = " & exceptuableIndex

                            Dim exceptuableIndexsTable As DataTable = view.ToTable()

                            ' Por cada Atributo exceptuable
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
    ''' Método que muestra un mensaje de error referido a atributos exceptuables
    ''' </summary>
    ''' <param name="incompleteExceptuableIndexs">Colección que contiene los id de atributos de tipo exceptuables incompletos</param>
    ''' <param name="dsDataExceptuableIndexs">Dataset que contiene todos los datos referidos a atributos exceptuables de un determinado form. dinámico</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/05/2009	Created    
    ''' </history>
    Private Sub showErrorExceptuableMessage(ByRef incompleteExceptuableIndexs As ArrayList, ByRef dsDataExceptuableIndexs As DataSet)

        Try

            Dim builder As New StringBuilder

            builder.Append("Se requieren completar los siguientes atributos de tipo exceptuable o sus correspondientes atributos alternativos:")
            builder.Append(vbCrLf + vbCrLf)

            Dim exceptuableIndexsCounter As Short = 1

            ' Por cada Atributo de tipo exceptuable 
            For Each exceptuableIndex As Int64 In incompleteExceptuableIndexs

                builder.Append("Atributo de tipo exceptuable: " & Chr(39) & getIndexName(exceptuableIndex) & Chr(39))

                ' Se obtienen los atributos exceptuables del Atributo de tipo exceptuable
                Dim view As New DataView(dsDataExceptuableIndexs.Tables(0))
                view.RowFilter = "IId = " & exceptuableIndex

                Dim exceptuableIndexsTable As DataTable = view.ToTable()

                builder.Append(vbCrLf)
                builder.Append("Atributos alternativos: ")

                Dim minCounter As Short = 1

                ' Por cada Atributo exceptuable
                For Each row As DataRow In exceptuableIndexsTable.Rows

                    If (minCounter = exceptuableIndexsTable.Rows.Count) Then
                        builder.Append(Chr(39) & getIndexName(Int64.Parse(row("Value"))) & Chr(39))
                    Else
                        builder.Append(Chr(39) & getIndexName(Int64.Parse(row("Value"))) & Chr(39) & ", ")
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
    ''' Método que obtiene el valor (colocado en el formulario dinámico) de un determinado Atributo
    ''' </summary>
    ''' <param name="indexId">Id de un Atributo</param>
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
    ''' Método que obtiene el nombre de un Atributo (que se encuentra en el formulario dinámico)
    ''' </summary>
    ''' <param name="indexId">Id de un Atributo</param>
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

            Return (String.Empty)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
#End Region

#End Region

    Sub RecoverValue(ByVal I As Index, ByVal e As HtmlElement)
        'If ZTrace.IsVerbose Then
        '    Try
        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Id: " & e.Id)
        '    Catch ex As Exception
        '        Zamba.Core.ZClass.raiseerror(ex)
        '    End Try
        '    Try
        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Tag: " & CStr(e.TagName).ToLower)
        '        If IsDBNull(e.DomElement.type) Then
        '            ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Type: DBNULL")
        '        Else
        '            ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Type: " & CStr(e.DomElement.type).ToLower)
        '        End If
        '    Catch ex As Exception
        '        Zamba.Core.ZClass.raiseerror(ex)
        '    End Try
        'End If

        Select Case CStr(e.TagName).ToUpper
            Case "INPUT"
                Dim ElementType As String = "text"
                Try
                    ElementType = CStr(e.DomElement.type).ToLower
                Catch
                    Try
                        ElementType = DirectCast(e.DomElement, mshtml.HTMLInputElementClass).IHTMLInputElement_type.ToLower()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End Try
                Select Case ElementType
                    Case "text"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, I.ID.ToString)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        If I.DropDown <> IndexAdditionalType.AutoSustitución AndAlso I.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then

                            'casteo de Fecha_Hora a Fecha para que la fecha ingresada
                            'desde el formulario se guarde en formato DD/MM/AAAA
                            If I.Type = IndexDataType.Fecha_Hora Then
                                '       I.Type = IndexDataType.Fecha
                                I.Data = e.DomElement.value
                                I.DataTemp = I.Data
                            Else
                                I.Data = e.DomElement.value
                                I.DataTemp = I.Data
                            End If
                        Else
                            If Not IsNothing(e.DomElement) AndAlso Not IsNothing(e.DomElement.value) Then
                                If e.DomElement.value.ToString().Contains(" - ") Then
                                    I.Data = e.DomElement.value.ToString().Split(" - ")(0)
                                    I.DataTemp = I.Data
                                Else
                                    I.Data = e.DomElement.value
                                    I.DataTemp = I.Data
                                End If

                            Else
                                I.Data = e.InnerText
                                I.DataTemp = I.Data
                            End If
                        End If
                    Case "checkbox"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, I.ID)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.checked)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        If 0 = e.DomElement.checked Then

                            I.Data = 0
                            I.DataTemp = 0
                        Else
                            I.Data = 1
                            I.DataTemp = 1
                        End If
                    Case "radio"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, I.ID.ToString)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.checked)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

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
                'If ZTrace.IsVerbose Then
                '    Try
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, I.ID.ToString)
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    End Try
                '    Try
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    End Try
                'End If

                'se agrega validacion adicional para autosustitucionjerarquico
                'para aquellos atributos que tengan codigos de tipo xx - xx
                If I.DropDown = IndexAdditionalType.AutoSustitución _
                    OrElse (I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico AndAlso e.DomElement.selectedindex > -1 AndAlso DirectCast(e.DomElement.children(e.DomElement.selectedindex).innertext.split("-"), String()).Length < 2) Then
                    Try
                        Dim id, text As String
                        id = e.DomElement.children(e.DomElement.selectedindex).value.trim
                        text = e.DomElement.children(e.DomElement.selectedindex).text.trim
                        I.Data = id
                        I.DataTemp = I.Data
                        I.dataDescription = text
                        I.dataDescriptionTemp = text
                    Catch ex As Exception
                        I.Data = String.Empty
                        I.DataTemp = String.Empty
                    End Try
                ElseIf IsNothing(e.DomElement.value) Then
                    I.Data = String.Empty
                    I.DataTemp = String.Empty
                Else
                    I.Data = e.DomElement.value.trim
                    I.DataTemp = I.Data
                    I.dataDescription = e.DomElement.value.trim
                    I.dataDescriptionTemp = e.DomElement.value.trim
                End If
            Case "TEXTAREA"
                'If ZTrace.IsVerbose Then
                '    Try
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, I.ID)
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    End Try
                '    Try
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    End Try
                'End If

                I.Data = e.GetAttribute("value")
                I.DataTemp = I.Data
        End Select
    End Sub

    Sub RecoverVarValue(ByVal VarName As String, ByVal e As HtmlElement)
        'If ZTrace.IsVerbose Then
        '    Try
        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Id: " & e.Id)
        '    Catch ex As Exception
        '        Zamba.Core.ZClass.raiseerror(ex)
        '    End Try
        '    Try
        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Tag: " & CStr(e.TagName).ToLower)
        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "---------------Type: " & CStr(e.DomElement.type).ToLower)
        '    Catch ex As Exception
        '        Zamba.Core.ZClass.raiseerror(ex)
        '    End Try
        'End If

        Select Case CStr(e.TagName).ToUpper
            Case "INPUT"
                Dim ElementType As String = "text"
                Try
                    ElementType = CStr(e.DomElement.type).ToLower
                Catch
                    Try
                        ElementType = DirectCast(e.DomElement, mshtml.HTMLInputElementClass).IHTMLInputElement_type.ToLower()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End Try
                Select Case ElementType
                    Case "text"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        VariablesInterReglas.Item(VarName) = e.DomElement.value

                    Case "file"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        VariablesInterReglas.Item(VarName) = e.DomElement.value
                    Case "checkbox"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.checked)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

                        If 0 = e.DomElement.checked Then

                            VariablesInterReglas.Item(VarName) = 0

                        Else
                            VariablesInterReglas.Item(VarName) = 1

                        End If
                    Case "radio"
                        'If ZTrace.IsVerbose Then
                        '    Try
                        '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.checked)
                        '    Catch ex As Exception
                        '        Zamba.Core.ZClass.raiseerror(ex)
                        '    End Try
                        'End If

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
                'If ZTrace.IsVerbose Then
                '    Try
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    End Try
                'End If

                Try
                    VariablesInterReglas.Item(VarName) = e.DomElement.children(e.DomElement.selectedindex).innertext.split("-")(0).trim
                Catch ex As Exception
                    VariablesInterReglas.Item(VarName) = String.Empty
                End Try

            Case "TEXTAREA"
                'If ZTrace.IsVerbose Then
                '    Try
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, e.DomElement.value)
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    End Try
                'End If

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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        openFile()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As EventArgs)
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
            Return AutocompleteBCBusiness.ExecuteAutoComplete(localResult, _Index, newFrmGrilla, True)
        Catch ex As Exception
            If String.Compare(ex.Message, "No hay datos") <> 0 Then
                Zamba.Core.ZClass.raiseerror(ex)
            End If
            Return Nothing
        End Try
        ' RaiseEvent IndexsChanged(LocalResult, Index)
        'IndexAnterior = Index.Data
    End Function

    Private Sub ListToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        LoadAutosustitutionLists()
    End Sub

    Private Sub List1ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        LoadSearchList()
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
    '''     Marcelo     05/02/2009     Modified     Se modifico la carga de los atributos para mejorar la performance 
    '''     Marcelo     06/01/2010     Modified     Se agrego variable para cargar solo las tareas que esten en WF
    '''     Javier      22/10/2010     Modified     Se agrega funcionalidad para filtrar permisos por asociados
    ''' </history>
    Public Shared Function ParseResult(ByVal ParentResult As IResult, ByVal results As DataTable, ByVal tableId As String, ByVal OnlyWF As Boolean) As DataTable
        Dim Dt As New DataTable()
        Dt.Columns.Add(New DataColumn(GridColumns.VER_COLUMNNAME))

        If String.IsNullOrEmpty(tableId) = False AndAlso tableId.Contains("§") Then
            For Each btn As String In tableId.Split("§")
                Dim items As Array = btn.Split("/")
                Dt.Columns.Add(items(1).ToString())
            Next
        End If

        Dt.Columns.Add(New DataColumn(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME))
        Dt.Columns.Add(New DataColumn(GridColumns.STATE_COLUMNNAME))
        Dt.Columns.Add(New DataColumn(GridColumns.USER_ASIGNEDNAME_COLUMNNAME))

        Try
            Dim CurrentIndexType As Type
            'Cargo todos los atributos de todos los results , como pueden ser diferentes tipos de documento recorro todos
            'Solo visualizo en la tabla los atributos sobre los cuales tiene permiso el documento. Mariela
            Dim lastDocTypeId As Int64 = 0
            Dim AIR As Hashtable = Nothing
            Dim tilde As Boolean = RightsBusiness.GetSpecificAttributeRight(Membership.MembershipHelper.CurrentUser, ParentResult.DocTypeId)

            For Each CurrentResult As DataRow In results.Rows
                If OnlyWF = False OrElse (OnlyWF = True AndAlso CurrentResult.Table.Columns.Contains(GridColumns.TASK_ID_COLUMNNAME) AndAlso Not IsDBNull(CurrentResult(GridColumns.TASK_ID_COLUMNNAME)) AndAlso Not String.IsNullOrEmpty(CurrentResult(GridColumns.TASK_ID_COLUMNNAME))) Then
                    'Se obtiene los permisos para el doctype, doctypeasociado y usuario
                    If lastDocTypeId <> CurrentResult("doc_type_ID") Then
                        lastDocTypeId = CurrentResult("doc_type_ID")

                        If tilde Then
                            AIR = RightsBusiness.GetAssociatedIndexsRightsCombined(ParentResult.DocTypeId, CurrentResult("doc_type_ID"), Membership.MembershipHelper.CurrentUser.ID, True)
                        End If
                    End If


                    For Each CurrentIndex As IIndex In ZCore.FilterIndex(CurrentResult("doc_type_ID"))
                        Dim ShowIndex As Boolean = False
                        Dim IR As AssociatedIndexsRightsInfo
                        If tilde Then
                            IR = DirectCast(AIR(CurrentIndex.ID), AssociatedIndexsRightsInfo)
                        End If

                        If tilde = False OrElse IR.GetIndexRightValue(RightsType.AssociateIndexView) Then
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
                End If

            Next

            Dt.Columns.Add(New DataColumn(GridColumns.CRDATE_COLUMNNAME))
            Dt.Columns.Add(New DataColumn(GridColumns.DOC_TYPE_NAME_COLUMNNAME))
            Dt.Columns.Add(New DataColumn(GridColumns.LASTUPDATE_COLUMNNAME))

            If Boolean.Parse(UserPreferences.getValue("NombreOriginal", UPSections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn(GridColumns.ORIGINAL_FILENAME_COLUMNNAME))
            End If
            If Boolean.Parse(UserPreferences.getValue("NumerodeVersion", UPSections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn(GridColumns.NUMERO_DE_VERSION_COLUMNNAME))
            End If
            If Boolean.Parse(UserPreferences.getValue("ParentId", UPSections.FormPreferences, "True")) = True Then
                Dt.Columns.Add(New DataColumn(GridColumns.VER_PARENT_ID_COLUMNNAME))
            End If

            Dt.Columns.Add(New DataColumn("Ruta Documento"))
            Dt.Columns.Add(New DataColumn(GridColumns.DOCTYPEID_COLUMNNAME))
            Dt.Columns.Add(New DataColumn(GridColumns.DOC_ID_COLUMNNAME, System.Type.GetType("System.Int64")))
            Dt.AcceptChanges()

            Dim CurrentRow As DataRow = Nothing

            For Each CurrentResult As DataRow In results.Rows
                ' Se verifica si el documento es una tarea
                If OnlyWF = False OrElse (OnlyWF = True AndAlso CurrentResult.Table.Columns.Contains(GridColumns.TASK_ID_COLUMNNAME) AndAlso Not IsDBNull(CurrentResult(GridColumns.TASK_ID_COLUMNNAME)) AndAlso Not String.IsNullOrEmpty(CurrentResult(GridColumns.TASK_ID_COLUMNNAME))) Then

                    CurrentRow = Dt.NewRow()

                    CurrentRow.Item(GridColumns.DOC_ID_COLUMNNAME) = CurrentResult("DOC_ID")

                    If CurrentRow.Table.Columns.Contains("Ruta Documento") AndAlso CurrentRow.Table.Columns.Contains("DISK_VOL_PATH") AndAlso CurrentRow.Table.Columns.Contains("OFFSET") AndAlso CurrentRow.Table.Columns.Contains("DOC_FILE") Then
                        CurrentRow.Item("Ruta Documento") = CurrentResult("DISK_VOL_PATH") & "\" & CurrentResult("DOC_TYPE_ID") & "\" & CurrentResult("OFFSET") & "\" & CurrentResult("DOC_FILE")
                    End If

                    CurrentRow.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) = CurrentResult(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                    CurrentRow.Item(GridColumns.CRDATE_COLUMNNAME) = CurrentResult(GridColumns.CRDATE_COLUMNNAME)
                    CurrentRow.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME) = DocTypesBusiness.GetDocTypeName(CurrentResult("doc_type_ID"), True)
                    CurrentRow.Item(GridColumns.LASTUPDATE_COLUMNNAME) = CurrentResult(GridColumns.LASTUPDATE_COLUMNNAME)

                    If CurrentRow.Table.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) Then
                        If Boolean.Parse(UserPreferences.getValue("NumerodeVersion", UPSections.FormPreferences, "True")) = True Then
                            CurrentRow.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) = CurrentResult(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                        End If
                    End If

                    If CurrentRow.Table.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) Then
                        If Boolean.Parse(UserPreferences.getValue("ParentId", UPSections.FormPreferences, "True")) = True Then
                            CurrentRow.Item(GridColumns.VER_PARENT_ID_COLUMNNAME) = CurrentResult(GridColumns.VER_PARENT_ID_COLUMNNAME)
                        End If
                    End If

                    CurrentRow.Item(GridColumns.DOCTYPEID_COLUMNNAME) = CurrentResult("doc_type_ID")


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

                    If CurrentRow.Table.Columns.Contains(GridColumns.STATE_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.STATE_COLUMNNAME) Then
                        CurrentRow.Item(GridColumns.STATE_COLUMNNAME) = CurrentResult(GridColumns.STATE_COLUMNNAME)
                    End If


                    If CurrentRow.Table.Columns.Contains(GridColumns.USER_ASIGNEDNAME_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.USER_ASIGNEDNAME_COLUMNNAME) Then
                        CurrentRow.Item(GridColumns.USER_ASIGNEDNAME_COLUMNNAME) = CurrentResult(GridColumns.USER_ASIGNEDNAME_COLUMNNAME)
                    End If



                    If CurrentRow.Table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) AndAlso CurrentResult.Table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
                        'Nombre del documento
                        If Boolean.Parse(UserPreferences.getValue("NombreOriginal", UPSections.FormPreferences, "True")) = True Then
                            Dim FileName As String = CurrentResult(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                            If FileName Is Nothing Then FileName = CurrentResult("Doc_File")

                            Dim indexpath As Int32 = FileName.LastIndexOf("\")
                            If indexpath = -1 OrElse FileName.Length - 1 = -1 Then
                            Else
                                If indexpath = -1 Then indexpath = 0
                                Try
                                    FileName = FileName.Substring(indexpath + 1, FileName.Length - indexpath - 1)
                                Catch ex As Exception
                                    FileName = CurrentResult(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                                End Try
                            End If
                            CurrentRow.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) = FileName
                        End If
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

                        If tableId.Contains("§") Then
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
                    End If

                    InnerHtml = Nothing
                    Dt.Rows.Add(CurrentRow)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Dt.AcceptChanges()
        Dt.DefaultView.Sort = GridColumns.DOC_ID_COLUMNNAME & " DESC"

        Return Dt.DefaultView.ToTable()
    End Function

    ''' <summary>
    ''' Castea el tipo de un Atributo a Type
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

    Public Sub AssociateIndexViewer(ByVal ucindexs As IZControl)
        If Not IsNothing(ucindexs) Then
            RemoveHandler ucindexs.OnChangeControl, AddressOf RefreshData
            AddHandler ucindexs.OnChangeControl, AddressOf RefreshData
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un indice en particular en forma local
    ''' </summary>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLocalIndexValue(ByVal indexID As Long) As String
        If indexID > 0 Then
            Dim max As Integer = localResult.Indexs.Count

            For i As Integer = 0 To max - 1
                If DirectCast(localResult.Indexs(i), IIndex).ID = indexID Then
                    Return DirectCast(localResult.Indexs(i), IIndex).Data
                End If
            Next

        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Handler del evento change de los indices jerarquicos
    ''' </summary>
    ''' <param name="element"></param>
    ''' <param name="parentID"></param>
    ''' <param name="childID"></param>
    ''' <remarks></remarks>
    Private Sub HierarchicalChange(ByVal element As HtmlElement, ByVal parentID As Long, ByVal childID As Long)
        Try
            'MessageBox.Show("Mensaje para poder debbugear el codigo sin que se cuelgue el cliente y el visual")
            Dim childElement As HtmlElement = AxWebBrowser1.Document.GetElementById("zamba_index_" & childID)
            'Si se encuentra el indice hijo
            If Not childElement Is Nothing Then
                childElement.InnerHtml = String.Empty

                Dim max As Integer = element.Children.Count
                Dim optionElement As HtmlElement
                Dim value As String = element.GetAttribute("value")

                Dim tableOptions As DataTable = IndexsBussinesExt.GetHierarchicalTableByValue(childID, parentID, value, True)

                If Not tableOptions Is Nothing Then
                    max = tableOptions.Rows.Count
                    Dim indexType As IndexAdditionalType = GetIndexDropDown(childID)

                    Dim elementValue As Object
                    Dim elementPrompt As Object
                    'Rerorremos las opciones retornadas
                    For i As Integer = 0 To max - 1

                        elementValue = tableOptions.Rows(i)("Value")
                        If IsDBNull(elementValue) Then
                            elementValue = String.Empty
                        End If

                        If indexType = IndexAdditionalType.DropDown OrElse indexType = IndexAdditionalType.DropDownJerarquico Then
                            elementPrompt = tableOptions.Rows(i)("Value")
                            If IsDBNull(elementPrompt) Then
                                elementPrompt = String.Empty
                            End If
                        Else
                            If IsDBNull(tableOptions.Rows(i)(0)) Then
                                elementPrompt = "A definir"
                            Else
                                elementPrompt = tableOptions.Rows(i)(0) & " - " & tableOptions.Rows(i)(1)
                            End If
                        End If

                        childElement.AppendChild(GetOptionTag(elementValue, elementPrompt, String.Empty))
                    Next

                    'Si el indice tiene hijos
                    Dim currIndex As IIndex = GetLocalIndex(childID)
                    If Not currIndex.HierarchicalChildID Is Nothing Then
                        Dim childCount As Integer = currIndex.HierarchicalChildID.Count

                        For j As Integer = 0 To childCount - 1
                            If currIndex.HierarchicalChildID(j) > 0 Then
                                HierarchicalChange(childElement, childID, currIndex.HierarchicalChildID(j))
                            End If
                        Next
                    End If
                    'AxWebBrowser1.Document.InvokeScript("ZFUNCTION")
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Crea un elemento option
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="prompt"></param>
    ''' <param name="indexData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOptionTag(ByVal value As String, ByVal prompt As String, ByVal indexData As String) As HtmlElement
        Dim tag As HtmlElement = AxWebBrowser1.Document.CreateElement("option")

        If String.Compare(indexData.Trim, value.Trim) = 0 Then
            tag.SetAttribute("selected", value.Trim)
        End If
        tag.SetAttribute("value", value.Trim)
        tag.InnerText = prompt.Trim

        Return tag
    End Function

    ''' <summary>
    ''' Obtiene el tipo de dropdown en indices locales
    ''' </summary>
    ''' <param name="childID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIndexDropDown(ByVal childID As Long) As IndexAdditionalType
        If childID > 0 Then
            Dim max As Integer = localResult.Indexs.Count
            For i As Integer = 0 To max - 1
                If DirectCast(localResult.Indexs(i), IIndex).ID = childID Then
                    Return DirectCast(localResult.Indexs(i), IIndex).DropDown
                End If
            Next
        End If

        Return IndexAdditionalType.NoIndex
    End Function

    ''' <summary>
    ''' Obtiene el indice local
    ''' </summary>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLocalIndex(ByVal indexID As Long) As IIndex
        If indexID > 0 Then
            Dim max As Integer = localResult.Indexs.Count
            For i As Integer = 0 To max - 1
                If DirectCast(localResult.Indexs(i), IIndex).ID = indexID Then
                    Return DirectCast(localResult.Indexs(i), IIndex)
                End If
            Next
        End If
    End Function

    Private Function GetFormPath(form As ZwebForm) As String
        Dim rutaTemp As String = form.TempFullPath
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta de formulario local: " & rutaTemp)

        If File.Exists(rutaTemp) = False Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El formulario no fue encontrado en los temporales del usuario. Se procede a buscar en el temporal de la aplicación.")
            rutaTemp = Application.StartupPath & "\temp\" & form.TempPathName
            If Not File.Exists(rutaTemp) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El formulario no fue encontrado en el temporal de la aplicación. El formulario será obtenido del servidor o base de datos.")
                MakeLocalCopy(form)
                rutaTemp = form.Path
            End If
        End If

        Return rutaTemp
    End Function
    Dim CP As Int64
    Dim ProgressCount As Int64 = -2
    Private Sub AxWebBrowser1_ProgressChanged(sender As Object, e As WebBrowserProgressChangedEventArgs) Handles AxWebBrowser1.ProgressChanged
        '  ZTrace.WriteLineIf(ZTrace.IsVerbose, "ProgressChanged: " & e.CurrentProgress & " de " & e.MaximumProgress)
        If ProgressCount = e.CurrentProgress Then
            CP += +1
        End If
        ProgressCount = e.CurrentProgress

        If CP > UserPreferences.getValue("WebBrowserTimeOut", UPSections.FormPreferences, 10) Then
            AxWebBrowser1.Stop()
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se cancela la carga por bloqueo")
        Else
            '  ZTrace.WriteLineIf(ZTrace.IsVerbose, "ProgressChanged: " & e.CurrentProgress & " de " & e.MaximumProgress)
        End If

    End Sub

    'Private Sub AxWebBrowser1_StatusTextChanged(sender As Object, e As EventArgs) Handles AxWebBrowser1.StatusTextChanged
    '    ZTrace.WriteLineIf(ZTrace.IsVerbose, "StatusChanged: " & AxWebBrowser1.StatusText)
    'End Sub

    Private ucpreview As ucbrowserpreview

    Private Sub LoadExternalPreview(path As String)
        If ucpreview Is Nothing Then
            ' Dim sp As New Splitter
            'sp.Width = 2
            'sp.Dock = DockStyle.Bottom
            'sp.BackColor = Color.SlateBlue
            ucpreview = New ucbrowserpreview()
            ucpreview.Dock = DockStyle.Bottom
            AutoScroll = True
            Controls.Add(ucpreview)
            'Me.Controls.Add(sp)
            ucpreview.BringToFront()
            ' sp.BringToFront()
            AxWebBrowser1.Dock = DockStyle.Top
            ucpreview.Dock = DockStyle.Top
            AxWebBrowser1.BringToFront()
            ucpreview.BringToFront()
            ucpreview.Height = 2000
        End If
        ucpreview.ShowDocument(path)
    End Sub

End Class
