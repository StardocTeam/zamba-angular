Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.IO

Public Class DynamicFormsGenerate
    Private MaxLenght As Int32


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
            Dim NewColumn As String = String.Empty
            Dim LastNroFila As Int32 = 1
            Dim BloseRow As Boolean = False
            Dim NroFila As Int32
            Dim LastSection As Int32
            Dim IdSection As Int32
            Dim CloseRow As Boolean

            Dim frmId As Integer = dsFormTable.Tables(0).Rows(0).Item("IdFormulario")

            Try

                LastSection = Int32.Parse(dsFormTable.Tables(0).Rows(0).Item("IdSeccion").ToString())

                strgHtml.AppendLine(createHeader(frmId))
                strgHtml.AppendLine(GetTableHeader((dsFormTable.Tables(0).Rows(0)("IdSeccion").ToString())))

                '//[sebastian 10-02-2009] recorro el data set para ir generando las diferentes tablas y celdas
                '//del formulario.
                For Each CurrentRow As DataRow In dsFormTable.Tables(0).Rows

                    '//[sebastian 10-02-2009] numero de orden del formulario
                    NroFila = Int32.Parse(CurrentRow("NroFila").ToString())
                    '//[sebastian 10-02-2009] id de la seccion: que se utiliza para saber cuando es otra tabla
                    '//dentro del formulario
                    IdSection = Int32.Parse(CurrentRow("IdSeccion").ToString())

                    If (LastSection = IdSection) Then

                        If (NroFila = LastNroFila) Then

                            NewColumn = "true"
                            CloseRow = False
                            '//[sebastian 10-02-2009] agrego una nueva celda a la tabla con el control correspondiente
                            strgHtml.AppendLine(GetNewTableCell(CurrentRow, NewColumn, CloseRow, labelName).ToString())

                        Else

                            NewColumn = "false"
                            CloseRow = True
                            '//[sebastian 10-02-2009] agrego una nueva fila a la tabla con su control
                            strgHtml.AppendLine(GetNewTableCell(CurrentRow, NewColumn, CloseRow, labelName).ToString())

                        End If

                        '//[sebastian 10-02-2009] asigno el numero de orden del control actual para saber cuando 
                        '//cambio de fila para ubicar el control siguiente
                        LastNroFila = NroFila

                    Else
                        LastNroFila = NroFila

                        '//[sebastian 10-02-2009] cierro la tabla para comenzar una nueva
                        strgHtml.AppendLine("</tr>")
                        strgHtml.AppendLine("</table>")

                        '//[sebastian 10-02-2009] obtengo el nuevo encabezado de la nueva tabla para generarla 
                        '//dentro del mismo formulario
                        strgHtml.AppendLine(GetTableHeader(CurrentRow("IdSeccion").ToString()))

                        ' [Gaston 13/03/2009] Después de definirse la apertura de una tabla se inserta el índice
                        NewColumn = "true"
                        CloseRow = False
                        strgHtml.AppendLine(GetNewTableCell(CurrentRow, NewColumn, CloseRow, labelName).ToString())

                    End If

                    '//[sebastian 10-02-2009] guardo la última seccion (tabla) que se genero.
                    LastSection = IdSection
                Next

                '//[sebastian 10-02-2009] cierro la última tabla del formulario para luego cerrar el formulario
                '//para ser mostrado y guardado en un archivo html
                strgHtml.AppendLine("</tr>")
                ' [Gaston 13/03/2009] Se define el tag de cierre para la última tabla (Sección)
                strgHtml.AppendLine("</table>")
                ' //Se obtiene el footer del formulario(de la pagina web), para cerrar el formulario definitivamente
                strgHtml.AppendLine(createFooter())

                ' Si el nombre del formulario está vacío entonces se coloca un nombre por defecto
                If (String.IsNullOrEmpty(frmName)) Then
                    frmName = "DynamicForm"
                End If

                '' Si no existe el directorio temp entonces se crea dicho directorio
                'If Not (Directory.Exists(Membership.MembershipHelper.StartUpPath & "\temp\")) Then
                '    Dim dir As New DirectoryInfo(Membership.MembershipHelper.StartUpPath & "\temp\")
                '    dir.Create()
                'End If

                ' El archivo html que se genera se guarda adentro del directorio temp con el nombre del formulario
                If OverrideFile = True Then
                    currentpath = Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & frmName & ".html"
                Else
                    currentpath = FileBusiness.GetUniqueFileName(Zamba.Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\", frmName, ".html")
                End If

                '[sebastian 01-04-09] this function repair html code if contains invelid characters like á
                strgHtml.Replace(strgHtml.ToString, FormBusiness.RepairHtml(strgHtml.ToString))

                Dim htmlstr As String = CenterHtmlControls(strgHtml.ToString)

                Dim Write As New StreamWriter(currentpath)
                Write.AutoFlush = True
                Write.Write(htmlstr)
                Write.Close()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                strgHtml = Nothing
                NewColumn = Nothing
                LastNroFila = Nothing
                BloseRow = Nothing
                NroFila = Nothing
                LastSection = Nothing
                IdSection = Nothing
                CloseRow = Nothing

            End Try

            '[Sebastian 10-09-2009] en caso de ser un formulario dinámico creado desde una regla entra por este lado
            'porque se obvia el hecho de que tenga un id de formulario. 
            'Esto fue desarrollado para BOSTON.
        Else
            Dim strgHtml As New StringBuilder()
            Dim NewColumn As String = String.Empty
            Dim BloseRow As Boolean = False
            Dim CloseRow As Boolean


            Try
                'Se agregaron 2 columns: IdFormulario, RuleGenDynamicFrmDropdown, al data set que es pasado por la regla
                'DoGenerateDinamicForm porque esas dos columnas las usan los metodos de creacion de formulario dinamico.

                Dim IdFormColumn As New DataColumn("IdFormulario", GetType(String), "0")
                Dim IdSeccion As New DataColumn("IdFormulario", GetType(String), "0")
                'esta columna se agrego para que sea un check box siempre el indice.
                Dim DropdownForRule As New DataColumn("RuleGenDynamicFrmDropdown", GetType(String), "3")
                dsFormTable.Tables(0).Columns.Add(IdFormColumn)
                dsFormTable.Tables(0).Columns.Add(DropdownForRule)
                Dim frmId As Integer = dsFormTable.Tables(0).Rows(0).Item("IdFormulario")

                strgHtml.AppendLine(createHeader(frmId))
                strgHtml.AppendLine(GetTableHeader(frmId))
                'se van creando una a una las filas del formulario.
                For Each CurrentRow As DataRow In dsFormTable.Tables(0).Rows

                    NewColumn = "false"

                    CloseRow = True
                    strgHtml.AppendLine(GetNewTableCell(CurrentRow, NewColumn, CloseRow, labelName).ToString())
                Next

                strgHtml.AppendLine("</tr>")
                strgHtml.AppendLine("</table>")
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

                Dim htmlstr As String = CenterHtmlControls(strgHtml.ToString)

                Try
                    Dim Write As New StreamWriter(currentpath)
                    Write.AutoFlush = True
                    Write.Write(htmlstr)
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
                            wr.Write(htmlstr)
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
                NewColumn = Nothing
                BloseRow = Nothing
                CloseRow = Nothing
            End Try
        End If

        Return currentpath
    End Function

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

            'esta columna se agrego para que sea un check box siempre el indice.
            Dim DropdownForRule As New DataColumn("RuleGenDynamicFrmDropdown", GetType(String), "3")

            dsFormTable.Tables(0).Columns.Add(IdFormColumn)
            dsFormTable.Tables(0).Columns.Add(DropdownForRule)

            strTablaDatos.Append("<!-- start dynamic data -->")

            For Each CurrentRow As DataRow In dsFormTable.Tables(0).Rows
                strTablaDatos.Append(GetNewTableCell(CurrentRow, False, False, LabelName).ToString())
                strTablaDatos.Append("</tr>")
            Next

            strTablaDatos.Append("<!-- end dynamic data -->")

            strTablaDatos.Replace(strTablaDatos.ToString, FormBusiness.RepairHtml(strTablaDatos.ToString))

            If (String.IsNullOrEmpty(frmName)) Then
                frmName = "DynamicForm"
            End If

            strTablaDatos.Replace(strTablaDatos.ToString, FormBusiness.RepairHtml(strTablaDatos.ToString))

            'aca tenemos las filas de la nueva tabla con sus datos
            Dim htmlTablaDatos As String = CenterHtmlControls(strTablaDatos.ToString)

            'buscar la tabla con id = "DynamicTable"
            Dim posInsertarDatos As Integer = HTML.IndexOf("DynamicTable")
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
                HTML = HTML.Insert(posInsertarDatos + 1, htmlTablaDatos)

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
        header.AppendLine("         <script type=""text/javascript"">")
        header.AppendLine("             function SetRuleId(sender)")
        header.AppendLine("             {")
        header.AppendLine("             }")
        header.AppendLine("         </script>")
        header.AppendLine("     </head>")
        header.AppendLine("     <body>")
        header.AppendLine("         <FORM name=frmMain id=" & frmId & " >")

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

        If Boolean.Parse(UserPreferences.getValue("IncludeSaveButtonInDynamicForms", Sections.FormPreferences, "True")) = True Then
            footer.AppendLine("             <table>")
            footer.AppendLine("                 <tr>")
            footer.AppendLine("                     <td>")
            footer.AppendLine("                         <input id=""zamba_save"" type=""submit"" value=""Guardar"" name=""zamba_save""  />")
            footer.AppendLine("                     </td>")
            footer.AppendLine("                     <td>")
            footer.AppendLine("                         <input id=""zamba_cancel"" type=""submit"" value=""Cancelar"" name=""zamba_cancel""/>")
            footer.AppendLine("                     </td>")
            footer.AppendLine("                 </tr>")
            footer.AppendLine("             </table>")
        End If

        footer.AppendLine("")
        footer.AppendLine("         </FORM>")
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
    Public Function showErrorMessage(ByVal errorMessage As String) As String

        Dim currentPath As String = Nothing


        Try

            Dim strgHtml As New StringBuilder()
            Dim strgHtmlAux As New StringBuilder()

            strgHtml.AppendLine("<html>")
            strgHtml.AppendLine("   <body>")
            strgHtml.AppendLine("")
            strgHtml.AppendLine("       <br />")
            strgHtml.AppendLine("")
            strgHtml.AppendLine("       <b>")
            strgHtml.AppendLine("           " & errorMessage)
            strgHtml.AppendLine("       </b>")
            strgHtml.AppendLine("")
            strgHtml.AppendLine("   </body>")
            strgHtml.AppendLine("</html>")

            currentPath = Zamba.Tools.EnvironmentUtil.GetTempDir("").FullName & "\DynamicForm.html"

            ' Si el archivo no existe se crea
            If Not (File.Exists(currentPath)) Then
                Dim file As New FileStream(currentPath, FileMode.Create, FileAccess.ReadWrite)
            End If

            strgHtml.Replace(strgHtml.ToString, FormBusiness.RepairHtml(strgHtmlAux.ToString))

            Dim Write As New StreamWriter(currentPath)
            Write.AutoFlush = True
            Write.Write(strgHtml)
            Write.Close()

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (String.Empty)
        End Try

        Return (currentPath)

    End Function

    '/// <summary>
    '/// Genera el encabezado de la tabla con titulo y formato
    '/// </summary>
    '/// <param name="SectionId">id de la seccion para obtener el nombre (titulo) de la nueva tabla</param>
    '/// <returns></returns>
    Public Function GetTableHeader(ByVal SectionId As String)

        Dim title As New StringBuilder()

        title.AppendLine("<table id=""table1"" border=""2"" width=""100%"">")
        title.AppendLine("<caption  style=""background-color:#527F76"">")
        title.AppendLine(FormFactory.GetDynamicFormSectionName(SectionId))
        title.AppendLine("</caption>")
        title.AppendLine("<tr>")

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
    Public Function GetNewTableCell(ByVal CellValue As DataRow, ByVal NewColumn As String, ByVal CloseRow As Boolean, ByVal labelName As String)

        Dim strHTML As New StringBuilder()
        Dim readOnlyAttribute As String = String.Empty

        If (CloseRow = True) Then
            strHTML.AppendLine("</tr>")
        End If

        If (String.Compare(NewColumn, "true") = 0) Then
            buildControlHTML(CellValue, strHTML, readOnlyAttribute)
        Else
            '//[sebastian 10-02-2009] genera una nueva fila con su control dentro
            strHTML.AppendLine("<tr>")
            buildControlHTML(CellValue, strHTML, readOnlyAttribute)
        End If

        Dim indexname As String = String.Empty
        If String.Compare(labelName, String.Empty) = 0 Or String.Compare(labelName.ToLower, "index_name") = 0 Then
            If CellValue.Table.Columns.Contains("index_name") = True Then
                indexname = CellValue("index_name").ToString().Trim()
            Else
                indexname = IndexsBusiness.GetIndexName(CellValue("index_id"), True)
            End If
        Else
            indexname = CellValue(labelName).ToString().Trim()
        End If


        If indexname.Length > MaxLenght Then
            MaxLenght = indexname.Length
        End If
        strHTML.AppendLine("<SPAN STYLE=" & Chr(34) & "width:200px; overflow:hidden;" & Chr(34) & ">")
        strHTML.AppendLine(AddIndexNameLabel(indexname))

        If CellValue.Table.Columns.Contains("RuleGenDynamicFrmDropdown") = False Then
            strHTML.AppendLine(GetControlHtml(CellValue("dropdown").ToString().Trim(), CellValue("index_id").ToString(), indexname.Trim(), readOnlyAttribute))
        Else
            strHTML.AppendLine(GetControlHtml(CellValue("RuleGenDynamicFrmDropdown").ToString().Trim(), CellValue("index_id").ToString(), indexname.Trim(), readOnlyAttribute))
        End If

        strHTML.AppendLine("</SPAN>")
        strHTML.AppendLine("</td>")

        '//[sebastian 10-02-2009] devuelvo el strig con el control dentro.
        Return (strHTML.ToString())

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
        query.Append("<label>")
        query.Append(indexname)
        query.Append("</label>")
        Return query.ToString
    End Function

    ''' <summary>
    ''' Centra los controles html dentro del form dinamico
    ''' </summary>
    ''' <param name="htmlstr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 07.04.2009 created</history>
    Private Function CenterHtmlControls(ByVal htmlstr As String) As String
        Dim tempMaxLenght As Int32 = MaxLenght / 2
        Dim replacestr As String = "<label style=" & Chr(34) & "width:" & tempMaxLenght & "em;" & Chr(34) & ">"
        Dim finalhtml As String = htmlstr.Replace("<label>", replacestr)
        replacestr = "width:" & tempMaxLenght + 12 & "em; overflow:hidden;"
        finalhtml = finalhtml.Replace("width:200px; overflow:hidden;", replacestr)
        Return finalhtml
    End Function

    ''' <summary>
    ''' Método que sirve para insertar el índice en el código HTML con sus respectivas propiedades ("Sólo Lectura" y "Visible") dependiendo de lo
    ''' configurado en el ABM que permite configurar el tipo y valor
    ''' </summary>
    ''' <param name="CellValue">Fila</param>
    ''' <param name="strhtml">StringBuilder con el código HTML</param>
    ''' <param name="readOnlyAttribute">Define si el índice es de sólo lectura o si se pueden ingresar datos</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	13/03/2009	Created    
    '''     [Gaston]	08/05/2009	Modified     Funcionalidad modificada para índice de tipo no visible y/o sólo lectura
    ''' </history>
    Private Sub buildControlHTML(ByVal CellValue As DataRow, ByRef strhtml As StringBuilder, ByRef readOnlyAttribute As String)

        Dim dsValues As New DataSet

        If CellValue.Table.Columns.Contains("IdIndice") = True AndAlso CellValue.Table.Columns.Contains("IdFormulario") = True Then
            dsValues = FormBusiness.getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(CellValue("IdFormulario"), CellValue("IdIndice"))
        End If

        If Not (IsNothing(dsValues)) Then

            ' Si el índice tiene asignado las propiedades "Sólo Lectura" y "Visible" o una de las dos
            If dsValues.Tables.Count > 0 AndAlso ((dsValues.Tables(0).Rows.Count = 1) Or (dsValues.Tables(0).Rows.Count = 2)) Then

                For Each rowValue As DataRow In dsValues.Tables(0).Rows

                    ' Si el tipo es "No Visible"
                    If (rowValue("Type") = formIndexDescriptionType.noVisible) Then
                        strhtml.AppendLine("<td style=""display: none; background-color:#C0D9D9"">")
                        ' Sino, si el tipo es "Sólo Lectura"
                    ElseIf (rowValue("Type") = formIndexDescriptionType.readOnly_) Then
                        readOnlyAttribute = "disabled=""disabled"""
                    End If

                Next

                If ((dsValues.Tables(0).Rows.Count = 1) AndAlso (dsValues.Tables(0).Rows(0).Item("Type") = formIndexDescriptionType.readOnly_)) Then
                    strhtml.AppendLine("<td style=""font-size:smaller; background-color:#C0D9D9"">")
                End If

            Else
                strhtml.AppendLine("<td style=""font-size:smaller; background-color:#C0D9D9"">")
            End If

        End If

    End Sub

    ''' <summary>
    ''' [sebastian 06-02-2009] Obtiene el control de html para ser insertado en la pagina
    ''' </summary>
    ''' <param name="ControlType">Tipo de control ya sea caja de texp, combobox, etc ...</param>
    ''' <param name="ControlId">Id del índice</param>
    ''' <param name="ControlName">Nombre del índice</param>
    ''' <param name="readOnlyAttribute">Define si el índice es de sólo lectura o si se pueden ingresar datos</param>
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

                Return ("<input type=""text"" id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")

            Case "1"

                Dim SelectControl As New StringBuilder()
                ' [Gaston 04/03/2009] Se define un alto y ancho para el combobox
                SelectControl.AppendLine("<select style=""height: 21px; width: 290px"" id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")

                Dim IndexId As Int32
                Dim SustList As New ArrayList()

                If Int32.TryParse(ControlName, IndexId) = True Then

                    SustList = IndexsBusiness.GetDropDownList(IndexId)

                    ' [Gaston 04/03/2009]
                    ' Si SustList es nothing entonces se genero un error al obtener los elementos de la base de datos
                    If (IsNothing(SustList)) Then
                        SustList = New ArrayList()
                    End If

                    If (SustList.Count > 0) Then
                        ' [Gaston 04/03/2009]
                        ' Si hay elementos que deben agregarse al combobox entonces el alto y el ancho que vienen por defecto se eliminan
                        SelectControl.Remove(0, SelectControl.ToString - 1)
                        SelectControl.AppendLine("<select id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")
                    End If

                    For Each ListValue As String In SustList
                        SelectControl.AppendLine("<option value=""" + ListValue + """>")
                        SelectControl.AppendLine(ListValue)
                        SelectControl.AppendLine("</option>")
                    Next

                End If

                SelectControl.Append("</select>")

                Return (SelectControl.ToString())

            Case "2"

                '//[sebastian 10-02-2009] tabla de sustitucion
                Dim SustitutionList As New StringBuilder()
                ' [Gaston 04/03/2009] Se define un alto y ancho para el combobox
                SustitutionList.AppendLine("<select style=""height: 21px; width: 290px"" id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")
                SustitutionList.AppendLine("<option value=""""></option></select>")
                Dim SustLisTable As New DataTable()
                Dim Indice As Int32
                '[Sebastian 14-05-09] se cambio controlname por controlid, porque por eso no podia
                'obtener la lista de sustitucion y en caso de obligatorio no guardaba los valores del form
                If Int32.TryParse(ControlId, Indice) = True Then
                    SustLisTable = AutoSubstitutionBusiness.GetIndexData(Indice, False)
                End If

                Return (SustitutionList.ToString())

                '[Sebastian 10-09-2009] Creado para poder  agregar el control chek box en el formulario de BOSTON
            Case "3"
                Return ("<input type=""checkbox"" id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")
            Case "radio"
                Return ("<input type=""radio"" id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")
            Case "chekbox"
                Return ("<input type=""checkbox"" id=""ZAMBA_INDEX_" & ControlId & """ name=""" & ControlName & """ " & readOnlyAttribute & "/>")
        End Select

        Return (String.Empty)

    End Function

End Class