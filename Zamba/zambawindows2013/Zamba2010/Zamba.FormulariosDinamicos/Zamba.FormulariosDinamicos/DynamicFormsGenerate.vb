Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.IO

Public Class DynamicFormsGenerate
    ''' <summary>
    ''' Crea la tabla del formulario con los datos del dataset
    ''' </summary>
    ''' <param name="dsFormTable">Dataset con los datos para crear el formulario</param>
    ''' <param name="frmName">Nombre del formulario que se quiere armar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	11/03/2009	Modified    Se agrego un nuevo parámetro: el nombre del formulario
    '''     [Gaston]	12/03/2009	Modified    Validación del IdFormulario y llamada a los métodos "createHeader" y "createFooter"
    '''     [Gaston]	13/03/2009	Modified    Correcciones para el armado del código HTML
    '''     [sebastian] 01/04/2009  Modified    Agregado de funcion que corrige el html si tiene acentos
    '''     [Gaston]    07/04/2009  Modified    El valor por defecto de LastSection ya no es 1, sino la sección de la primera fila
    '''     [Gaston]    30/04/2009  Modified    Si el directorio temp no existe, entonces se crea
    '''     [Sebastian] 10/09/2009  Modified    Se modifico para poder crear un formulario dinamico desde la regla "do generate dinamic form".
    ''' </history>
    Public Function CreateTable(ByVal dsFormTable As DataSet, ByVal frmName As String, Optional ByVal OverrideFile As Boolean = False, Optional ByVal labelName As String = "") As String

        Dim currentpath As String = ""

        If ((dsFormTable.Tables(0).Rows.Count > 0) AndAlso (dsFormTable.Tables(0).Columns.Contains("IdFormulario"))) Then

            Dim strgHtml As New StringBuilder()
            Dim anteriorFila As Int32 = 1
            Dim filaActual As Int32
            Dim anteriorSeccion As Int32
            Dim indiceActual As Int32
            Dim SetRow As Boolean = True
            Dim frmId As Integer = dsFormTable.Tables(0).Rows(0).Item("IdFormulario")
            Dim CantColumnas As Integer
            Try

                anteriorSeccion = Int32.Parse(dsFormTable.Tables(0).Rows(0).Item("IdSeccion").ToString())

                strgHtml.AppendLine(createHeader(frmId))
                strgHtml.AppendLine(GetTableHeader((dsFormTable.Tables(0).Rows(0)("IdSeccion").ToString())))


                For Each CurrentRow As DataRow In dsFormTable.Tables(0).Rows

                    filaActual = Int32.Parse(CurrentRow("NroFila").ToString())

                    CantColumnas = dsFormTable.Tables(0).Select("NroFila =" & filaActual).Count

                    indiceActual = Int32.Parse(CurrentRow("IdSeccion").ToString())

                    If (anteriorSeccion = indiceActual) Then

                        If SetRow Then
                            strgHtml.AppendLine("<div class=""form-group row"">")
                            SetRow = False
                        End If

                        If (filaActual = anteriorFila) Then
                            strgHtml.AppendLine(SetNewRow(CurrentRow, labelName, CantColumnas).ToString())
                        Else
                            strgHtml.AppendLine("</div>")
                            SetRow = True
                            strgHtml.AppendLine(SetNewRow(CurrentRow, labelName, CantColumnas).ToString())
                        End If

                        anteriorFila = filaActual

                    Else
                        anteriorFila = filaActual

                        strgHtml.AppendLine("</div>")
                        SetRow = False

                        strgHtml.AppendLine(GetTableHeader(CurrentRow("IdSeccion").ToString()))

                        strgHtml.AppendLine(SetNewRow(CurrentRow, labelName).ToString())

                    End If

                    anteriorSeccion = indiceActual
                Next

                strgHtml.AppendLine("</div>")
                strgHtml.AppendLine(createFooter())
                strgHtml.AppendLine("</form>")
                strgHtml.AppendLine("</body>")


                SaveForm(frmName, OverrideFile, currentpath, strgHtml)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                strgHtml = Nothing
                anteriorFila = Nothing
                filaActual = Nothing
                anteriorSeccion = Nothing
                indiceActual = Nothing

            End Try

        Else
            Dim strgHtml As New StringBuilder()
            Dim BloseRow As Boolean = False

            Try
                'Se agregaron 2 columns: IdFormulario, RuleGenDynamicFrmDropdown, al data set que es pasado por la regla
                'DoGenerateDinamicForm porque esas dos columnas las usan los metodos de creacion de formulario dinamico.

                Dim IdFormColumn As New DataColumn("IdFormulario", GetType(String), "0")
                Dim IdSeccion As New DataColumn("IdFormulario", GetType(String), "0")
                'esta columna se agrego para que sea un check box siempre el atributo.
                Dim DropdownForRule As New DataColumn("RuleGenDynamicFrmDropdown", GetType(String), "3")
                dsFormTable.Tables(0).Columns.Add(IdFormColumn)
                dsFormTable.Tables(0).Columns.Add(DropdownForRule)
                Dim frmId As Integer = dsFormTable.Tables(0).Rows(0).Item("IdFormulario")

                strgHtml.AppendLine(createHeader(frmId))
                strgHtml.AppendLine(GetTableHeader(frmId))
                'se van creando una a una las filas del formulario.
                For Each CurrentRow As DataRow In dsFormTable.Tables(0).Rows
                    strgHtml.AppendLine("</div>")
                    strgHtml.AppendLine(SetNewRow(CurrentRow, labelName).ToString())
                Next

                strgHtml.AppendLine(createFooter())

                If (String.IsNullOrEmpty(frmName)) Then
                    frmName = "DynamicForm"
                End If

                'guarda el formulario como archivo html en data temp
                'currentpath = FileBusiness.GetUniqueFileName(Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\", frmName, ".html")
                If OverrideFile = True Then
                    currentpath = Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & frmName & ".html"
                Else
                    currentpath = FileBusiness.GetUniqueFileName(Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\", frmName, ".html")
                End If

                strgHtml.Replace(strgHtml.ToString, FormBusiness.RepairHtml(strgHtml.ToString))


                Try
                    Dim Write As New StreamWriter(currentpath)
                    Write.AutoFlush = True
                    Write.Write(strgHtml.ToString)
                    Write.Close()
                Catch ex As Exception
                    If TypeOf (ex) Is System.UnauthorizedAccessException Then
                        Dim fl As FileInfo
                        Try
                            fl = New FileInfo(currentpath)
                            fl.Attributes = FileAttributes.Normal
                        Finally
                            fl = Nothing
                        End Try
                        Using wr As New StreamWriter(currentpath, False)
                            wr.Write(strgHtml.ToString)
                            wr.Flush()
                            wr.Close()
                        End Using
                    Else
                        ZClass.raiseerror(ex)
                    End If
                End Try
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                strgHtml = Nothing
                BloseRow = Nothing
            End Try
        End If

        Return currentpath
    End Function

    Private Shared Sub SaveForm(ByRef frmName As String, OverrideFile As Boolean, ByRef currentpath As String, strgHtml As StringBuilder)

        If (String.IsNullOrEmpty(frmName)) Then
            frmName = "DynamicForm"
        End If


        If OverrideFile = True Then
            currentpath = Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & frmName & ".html"
        Else
            currentpath = FileBusiness.GetUniqueFileName(Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\", frmName, ".html")
        End If

        strgHtml.Replace(strgHtml.ToString, FormBusiness.RepairHtml(strgHtml.ToString))

        Dim Write As New StreamWriter(currentpath)
        Write.AutoFlush = True
        Write.Write(strgHtml.ToString)
        Write.Close()
    End Sub

    ''' <summary>
    ''' Actualiza la tabla del formulario con los datos del dataset
    ''' </summary>
    ''' <param name="dsFormTable">Dataset con los datos para crear el formulario</param>
    ''' <param name="HTML">HTML del formulario donde se quieren poner los datos</param>
    ''' <param name="frmName">Nombre del formulario que se quiere armar</param>
    ''' <param name="OverrideFile">Indica si se guarda pisa el formulario existente (si existe) o si se guarda con un nuevo nombre</param>
    ''' <returns>Un string con el HTML del formulario conteniendo los datos</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [AlejandroR] - Created - 05/01/10
    ''' </history>

    Public Function UpdateTable(ByVal dsFormTable As DataSet, ByVal HTML As String, ByVal frmName As String, Optional ByVal OverrideFile As Boolean = False, Optional ByVal LabelName As String = "") As String

        Dim currentpath As String = ""
        Dim strTablaDatos As New StringBuilder()

        Try

            Dim IdFormColumn As New DataColumn("IdFormulario", GetType(String), "0")
            Dim IdSeccion As New DataColumn("IdFormulario", GetType(String), "0")

            'esta columna se agrego para que sea un check box siempre el atributo.
            Dim DropdownForRule As New DataColumn("RuleGenDynamicFrmDropdown", GetType(String), "3")

            dsFormTable.Tables(0).Columns.Add(IdFormColumn)
            dsFormTable.Tables(0).Columns.Add(DropdownForRule)

            strTablaDatos.Append("<!-- start dynamic data -->")

            For Each CurrentRow As DataRow In dsFormTable.Tables(0).Rows
                strTablaDatos.Append(SetNewRow(CurrentRow, LabelName).ToString())
                strTablaDatos.Append("</div>")
            Next

            strTablaDatos.Append("<!-- end dynamic data -->")

            strTablaDatos.Replace(strTablaDatos.ToString, FormBusiness.RepairHtml(strTablaDatos.ToString))

            If (String.IsNullOrEmpty(frmName)) Then
                frmName = "DynamicForm"
            End If

            strTablaDatos.Replace(strTablaDatos.ToString, FormBusiness.RepairHtml(strTablaDatos.ToString))

            'aca tenemos las filas de la nueva tabla con sus datos
            'buscar la tabla con id = "DynamicTable"
            Dim posInsertarDatos As Integer = HTML.IndexOf("DynamicTable", StringComparison.CurrentCultureIgnoreCase)
            Dim posIniDatosViejos As Integer
            Dim posFinDatosViejos As Integer

            If posInsertarDatos <> -1 Then

                'pos donde termina el tag de apertura <table>
                posInsertarDatos = HTML.IndexOf(">", posInsertarDatos)

                'buscar los tags que encierran a los datos viejos para eliminarlos
                posIniDatosViejos = HTML.IndexOf("<!-- start dynamic data -->", posInsertarDatos)
                posFinDatosViejos = HTML.IndexOf("<!-- end dynamic data -->", posInsertarDatos)

                If posIniDatosViejos <> -1 AndAlso posFinDatosViejos <> -1 Then
                    'eliminar el html entre inicio y fin (datos viejos)
                    HTML = HTML.Remove(posIniDatosViejos, (posFinDatosViejos - posIniDatosViejos) + 25)
                End If

                'insertar el nuevo HTML
                HTML = HTML.Insert(posInsertarDatos + 1, strTablaDatos.ToString)

                'guarda el formulario como archivo html en data temp
                If OverrideFile = True Then
                    currentpath = Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & frmName & ".html"
                Else
                    currentpath = FileBusiness.GetUniqueFileName(Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\", frmName, ".html")
                End If

                Dim Writer As New StreamWriter(currentpath)
                Writer.AutoFlush = True
                Writer.Write(HTML)
                Writer.Close()

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            strTablaDatos = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Método que sirve para crear la cabecera del código html del formulario
    ''' </summary>
    ''' <param name="frmId">Id del formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	12/03/2009	Created    Se agrego el frmId y el código es parte del antiguo método GetDynamicFormHeaderAndFooter()
    ''' </history>
    Private Function createHeader(ByVal frmId As Integer) As String

        Dim header As New StringBuilder

        header.AppendLine("<html>")
        header.AppendLine("     <head>")
        header.AppendLine("<script src=""Scripts/Zamba.js"" type=""text/javascript"" language=""JavaScript""></script>")
        header.AppendLine("<script type=""text/javascript"">")
        header.AppendLine("function SetRuleId(sender)")
        header.AppendLine("{")
        header.AppendLine("}")
        header.AppendLine("</script>")
        header.AppendLine("</head>")
        header.AppendLine("<body>")
        header.AppendLine("<form name=frmMain id=""" & frmId & """ >")
        header.AppendLine("<div class=""container-fluid"" > ")

        Return (header.ToString())
    End Function

    ''' <summary>
    ''' Método que sirve para crear el pie del código html del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	12/03/2009	Created    El código es una parte del antiguo método GetDynamicFormHeaderAndFooter()
    ''' </history>
    Private Function createFooter() As String

        Dim footer As New StringBuilder


        footer.AppendLine("<div> <center><button class=""btn btn-primary"" id=""zamba_save"" name=""zamba_save"" >Guardar</button>")
        footer.AppendLine("<button class=""btn btn-primary"" id=""zamba_cancel""  name=""zamba_cancel"">Cancelar</button>")
        footer.AppendLine("</center> </div>")

        footer.AppendLine("         </form>")
        footer.AppendLine("</div>")
        footer.AppendLine("     </body></html>")

        Return (footer.ToString())

    End Function

    ''' <summary>
    ''' Método que sirve para mostrar un mensaje de error en caso de no encontrar el formulario dinámico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Created     
    ''' </history>
    Public Function ShowErrorMessage(ByVal errorMessage As String) As String

        Dim currentPath As String = Nothing
        Dim html As String = "<html><body><br />" & errorMessage & "</body></html>"
        Dim formBusinessExt As New FormBusinessExt
        Dim streamWriter As StreamWriter = Nothing

        html = formBusinessExt.RepairHtml(html)

        Try
            currentPath = Zamba.Tools.EnvironmentUtil.GetTempDir(String.Empty).FullName & "\DynamicForm.html"

            'Se escribe el html
            streamWriter = New StreamWriter(currentPath)
            streamWriter.AutoFlush = True
            streamWriter.Write(html)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            currentPath = String.Empty
        Finally
            formBusinessExt = Nothing
            html = Nothing

            Try
                If streamWriter IsNot Nothing Then
                    streamWriter.Close()
                    streamWriter.Dispose()
                    streamWriter = Nothing
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Try

        Return currentPath
    End Function

    '/// <summary>
    '/// Genera el encabezado de la tabla con titulo y formato
    '/// </summary>
    '/// <param name="SectionId">id de la seccion para obtener el nombre (titulo) de la nueva tabla</param>
    '/// <returns></returns>
    Public Function GetTableHeader(ByVal SectionId As String)

        Dim title As New StringBuilder()

        title.AppendLine("   <center>   <h4>")
        title.AppendLine(FormFactory.GetDynamicFormSectionName(SectionId))
        title.AppendLine(" </h4> </center>")


        Return (title.ToString())

    End Function
    '/// <summary>
    '/// Genera una nueva celda con el control dentro y formato
    '/// </summary>
    '/// <param name="CellValue">controles que van dentro</param>
    '/// <param name="NewColumn">Indica si se genera una  nueva columna o fila</param>
    '/// <param name="CloseRow">Cierra el tag de la fila para general una nueva fila luego</param>
    '/// <returns>sebastian 10-02-2009</returns>
    ''' <history>
    '''     [Gaston]	13/03/2009	Modified    Llamada al método "buildControlHTML"
    '''     [Sebastian] 10/09/2009  Modified    Modificacion para la creacion del control chek box para la regla do generate dinamic form 
    ''' realizada para BOSTON
    ''' </history>
    Public Function SetNewRow(ByVal CellValue As DataRow, ByVal labelName As String, Optional ByVal cantidadColumnas As Integer = 2)

        Dim strHTML As StringBuilder
        Dim readOnlyAttribute As String
        Dim indexname As String

        Try
            strHTML = New StringBuilder()
            readOnlyAttribute = String.Empty
            indexname = String.Empty

            strHTML.AppendLine(buildControlHTML(CellValue, readOnlyAttribute, cantidadColumnas))

            indexname = GetNameIndex(CellValue, labelName)

            strHTML.AppendLine(AddIndexNameLabel(indexname))

            If CellValue.Table.Columns.Contains("RuleGenDynamicFrmDropdown") = False Then
                strHTML.AppendLine(GetControlHtml(CellValue("dropdown").ToString().Trim(), CellValue("index_id").ToString(), indexname.Trim(), readOnlyAttribute))
            Else
                strHTML.AppendLine(GetControlHtml(CellValue("RuleGenDynamicFrmDropdown").ToString().Trim(), CellValue("index_id").ToString(), indexname.Trim(), readOnlyAttribute))
            End If
            strHTML.AppendLine("</div>")
            strHTML.AppendLine("</div>")

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return strHTML.ToString()

    End Function

    Private Shared Function GetNameIndex(CellValue As DataRow, labelName As String) As String
        Dim indexname As String

        If String.IsNullOrEmpty(labelName) Or String.Compare(labelName.ToLower, "index_name") = 0 Then
            If CellValue.Table.Columns.Contains("index_name") = True Then
                indexname = CellValue("index_name").ToString().Trim()
            Else
                indexname = IndexsBusiness.GetIndexName(CellValue("index_id"), True)
            End If
        Else
            indexname = CellValue(labelName).ToString().Trim()
        End If

        Return indexname
    End Function

    ''' <summary>
    ''' Agrega un indice al html del form dinamico
    ''' </summary>
    ''' <param name="indexname"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 07.04.2009 created</history>
    Private Function AddIndexNameLabel(ByVal indexname As String) As String
        Dim query As New StringBuilder
        query.AppendLine("<div class=""input-group"">")
        query.AppendLine("<span class=""input-group-addon"">")
        query.AppendLine(indexname)
        query.AppendLine("</span>")

        Return query.ToString
    End Function

    ''' <summary>
    ''' Método que sirve para insertar el Atributo en el código HTML con sus respectivas propiedades ("Sólo Lectura" y "Visible") dependiendo de lo
    ''' configurado en el ABM que permite configurar el tipo y valor
    ''' </summary>
    ''' <param name="CellValue">Fila</param>
    ''' <param name="strhtml">StringBuilder con el código HTML</param>
    ''' <param name="readOnlyAttribute">Define si el Atributo es de sólo lectura o si se pueden ingresar datos</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	13/03/2009	Created    
    '''     [Gaston]	08/05/2009	Modified     Funcionalidad modificada para Atributo de tipo no visible y/o sólo lectura
    ''' </history>
    Private Function buildControlHTML(ByVal CellValue As DataRow, ByRef readOnlyAttribute As String, ByVal cantidadColumnas As Integer) As String

        Dim dsValues As DataSet
        Dim controlHTML As StringBuilder

        Try
            dsValues = New DataSet
            controlHTML = New StringBuilder

            If CellValue.Table.Columns.Contains("IdIndice") AndAlso CellValue.Table.Columns.Contains("IdFormulario") Then
                dsValues = FormBusiness.getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(CellValue("IdFormulario"), CellValue("IdIndice"))
            End If

            controlHTML.AppendLine("<div class=""col-md-" & (12 / cantidadColumnas).ToString & """")
            If Not IsNothing(dsValues) AndAlso dsValues.Tables.Count > 0 Then

                For Each rowValue As DataRow In dsValues.Tables(0).Rows

                    ' Si el tipo es "No Visible"
                    If (rowValue("Type") = formIndexDescriptionType.noVisible) Then
                        controlHTML.Append("style=""display: none; "">")
                        ' Sino, si el tipo es "Sólo Lectura"
                    ElseIf (rowValue("Type") = formIndexDescriptionType.readOnly_) Then
                        readOnlyAttribute = "disabled=""disabled"""
                    End If

                Next
                controlHTML.Append(" >")
            End If

            Return controlHTML.ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' [sebastian 06-02-2009] Obtiene el control de html para ser insertado en la pagina
    ''' </summary>
    ''' <param name="ControlType">Tipo de control ya sea caja de texp, combobox, etc ...</param>
    ''' <param name="ControlId">Id del Atributo</param>
    ''' <param name="ControlName">Nombre del Atributo</param>
    ''' <param name="readOnlyAttribute">Define si el Atributo es de sólo lectura o si se pueden ingresar datos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	05/03/2009	Modified    Se agrego un nuevo parámetro: ControlId, que se coloca como id del control que se quiere generar
    '''     [Gaston]	13/03/2009	Modified    Se agrego un nuevo parámetro: readOnlyAttribute
    '''     [Sebastian 14-05-09]    Modified    se cambio controlname por controlid, porque por eso no podia
    '''                                         obtener la lista de sustitucion y en caso de obligatorio no guardaba los valores del form
    '''     [Sebastian] 10/09/2009  Modified    Se agrego opcion nueva para poder crear el check box para cada indice del formulario que se crea
    '''                                         con la regla do generate dinamic form para BOSTON
    ''' </history>  
    Public Function GetControlHtml(ByVal ControlType As String, ByVal ControlId As Integer, ByVal ControlName As String, ByVal readOnlyAttribute As String)
        Select Case ControlType
            Case "0"

                Return ("<input type=""text"" class=""form-control input-sm"" id=""zamba_index_" & ControlId & """ name=""zamba_index_" & ControlId & """ " & readOnlyAttribute & " />")

            Case "1", "3"
                Dim SelectControl As New StringBuilder()
                ' [Gaston 04/03/2009] Se define un alto y ancho para el combobox
                SelectControl.AppendLine("<select class=""form-control input-sm"" id=""zamba_index_" & ControlId & """ name=""zamba_index_" & ControlId & """ " & readOnlyAttribute & ">")

                Dim IndexId As Int32
                Dim SustList As New List(Of String)

                If Int32.TryParse(ControlName, IndexId) = True Then

                    SustList = IndexsBusiness.GetDropDownList(IndexId)

                    If Not (IsNothing(SustList)) AndAlso (SustList.Count > 0) Then

                        For Each ListValue As String In SustList
                            SelectControl.AppendLine("<option value=""" & ListValue & """>")
                            SelectControl.AppendLine(ListValue)
                            SelectControl.AppendLine("</option>")
                        Next

                    End If

                End If

                SelectControl.Append("</select>")

                Return (SelectControl.ToString())
            Case "2", "4"
                '//[sebastian 10-02-2009] tabla de sustitucion
                Dim SustitutionList As New StringBuilder()
                ' [Gaston 04/03/2009] Se define un alto y ancho para el combobox
                SustitutionList.AppendLine("<select class=""form-control input-sm"" id=""zamba_index_" & ControlId & """ name=""zamba_index_" & ControlId & """ " & readOnlyAttribute & " >")

                '-------------------------------------------------------------
                'SE COMENTA YA QUE AL INSTANCIAR UN FORMULARIO DINAMICO EL OPTION
                'QUEDA CON EL VALOR VACIO EN MEMORIA Y NO SE CARGAN LOS COMBOS
                '-------------------------------------------------------------
                'SustitutionList.AppendLine("<option value=""""></option></select>")
                SustitutionList.Append("</select>")

                Dim SustLisTable As New DataTable()
                Dim Atributo As Int32
                '[Sebastian 14-05-09] se cambio controlname por controlid, porque por eso no podia
                'obtener la lista de sustitucion y en caso de obligatorio no guardaba los valores del form
                If Int32.TryParse(ControlId, Atributo) = True Then
                    SustLisTable = AutoSubstitutionBusiness.GetIndexData(Atributo, False)
                End If

                Return (SustitutionList.ToString())

                '[Sebastian 10-09-2009] Creado para poder  agregar el control chek box en el formulario de BOSTON
             Case "radio"
                Return ("<input type=""radio"" id=""zamba_index_" & ControlId & """ name=""zamba_index_" & ControlId & """ " & readOnlyAttribute & " />")
            Case "chekbox"
                Return ("<input type=""checkbox"" id=""zamba_index_" & ControlId & """ name=""zamba_index_" & ControlId & """ " & readOnlyAttribute & " />")
        End Select

        Return (String.Empty)
    End Function
End Class