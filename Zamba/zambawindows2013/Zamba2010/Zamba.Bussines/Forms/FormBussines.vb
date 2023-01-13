Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.IO
Imports System.Collections.Generic

Public Class FormBusiness
    Inherits ZClass

    Private Shared hsShowAndEditForms As New Hashtable

  
    ''' Tomas 27/02/09
    Public Shared Sub DeleteDynamicFormsConfigs(ByVal IdFormulario As Int64)
        FormFactory.DeleteDynamicFormsConfigs(IdFormulario)
    End Sub

   
    Public Shared Function GetFormsByType(ByVal Formtype As FormTypes) As ArrayList
        Return FormFactory.GetFormsByType(Formtype)
    End Function
    ''' <summary>
    ''' Metodo que trae los formularios virtuales de tipo insercion
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [pablo] 10/10/2010 Created
    Public Shared Function GetInsertFormsByDocTypeId(ByVal docTypeId As Int64) As ZwebForm()
        Return FormFactory.GetInsertFormsByDocTypeId(docTypeId)
    End Function

    Public Shared Function GetVirtualDocumentsByRightsOfView(ByVal Formtype As FormTypes, ByVal userid As Int64) As ArrayList
        Dim doctypesByRights As New Generic.List(Of Int64)
        doctypesByRights = RightComponent.GetAllDocTypesByUserRight(userid)
        Return FormFactory.GetVirtualDocumentsByRightsOfView(Formtype, userid, doctypesByRights)
    End Function

    ''' <summary>
    ''' Metodo que trae los formularios virtuales por los cual el usuario tiene permisos de crear.
    ''' </summary>
    ''' <param name="Formtype"></param>
    ''' <param name="userid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 31/03/2009 Created
    Public Shared Function GetVirtualDocumentsByRightsOfCreate(ByVal Formtype As FormTypes, ByVal userid As Int64) As ArrayList
        Dim doctypesByRights As New Generic.List(Of Int64)
        doctypesByRights = RightComponent.GetAllDocTypesByUserRightOfCreate(userid)
        Return FormFactory.GetVirtualDocumentsByRightsOfView(Formtype, userid, doctypesByRights)
    End Function

    ''' <summary>
    ''' Obtiene el body del html completo
    ''' </summary>
    ''' <param name="CompleteHTML"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetInnerHTML(ByVal CompleteHTML As String) As String
        'antes estaba esto, que se comento porque tiraba que estaba fuera de rango el atributo del split.
        '[sebastian]
        'Return CompleteHTML.Replace("<form>", "§").Replace("</form>", "§").Split("§")(1)
        Return CompleteHTML.Replace("<form>", "§").Replace("</form>", "§").Split("§")(0)
    End Function

    ''' <summary>
    ''' Método que muestra todos los forms(Show, Edit, Workflow, etc)
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	17/07/2008  Created     
    ''' </history>
    Public Shared Function GetAllForms(ByVal docTypeId As Int64, usecache As Boolean) As List(Of zwebform)
        SyncLock Cache.DocTypesAndIndexs.hsFormsByEntityId
        If Cache.DocTypesAndIndexs.hsFormsByEntityId.ContainsKey(docTypeId) = False OrElse usecache = False Then
        Dim forms As  List(Of zwebform) = FormFactory.GetAllForms(DocTypeId)
        If Not forms Is Nothing then
            SyncLock Cache.DocTypesAndIndexs.hsForms
            For each form as zwebform In forms
                 If Cache.DocTypesAndIndexs.hsForms.ContainsKey(form.ID) = False Then
                       Cache.DocTypesAndIndexs.hsForms.Add(form.ID,form)
                 End If
            Next
            End SyncLock
            If Cache.DocTypesAndIndexs.hsFormsByEntityId.ContainsKey(docTypeId) = False then    Cache.DocTypesAndIndexs.hsFormsByEntityId.Add(docTypeId,forms)
                 Return forms
            Else
                 Return Nothing
            End If
        Else
                 If Cache.DocTypesAndIndexs.hsFormsByEntityId.ContainsKey(docTypeId) = True Then
                    Return        Cache.DocTypesAndIndexs.hsFormsByEntityId(docTypeId)
                Else
                    Return Nothing
                 End If
        End if
        End SyncLock
    End Function
    Public Shared Function GetForms(usecache As Boolean) As  List(Of zwebform)
        SyncLock Cache.DocTypesAndIndexs.hsAllForms
        If Cache.DocTypesAndIndexs.hsAllForms.count = 0 OrElse usecache = False Then
        Dim forms As  List(Of zwebform) = FormFactory.GetForms()
                        SyncLock Cache.DocTypesAndIndexs.hsForms

                For each form as zwebform In forms

             If Cache.DocTypesAndIndexs.hsForms.ContainsKey(form.ID) = False Then
            Cache.DocTypesAndIndexs.hsForms.Add(form.ID,form)
        End If
       Next
                    End SyncLock
                    If Cache.DocTypesAndIndexs.hsAllForms.count = 0 = False then    Cache.DocTypesAndIndexs.hsAllForms = forms
       Return forms
        Else
            Return        Cache.DocTypesAndIndexs.hsAllForms
        End if
      End SyncLock
    End Function

    ''' <summary>
    ''' Cargo el valor del indice al elemento html
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="E"></param>
    ''' <history> Marcelo 31/07/08 Modified</history>
    ''' <remarks></remarks>
    Public Shared Function AsignValue(ByVal I As Index, ByVal E As String) As String
        Try
            Dim value As String = E
            Dim aux As String
            'Filtra por tipo de control...
            Select Case E.ToLower.Substring(0, E.ToLower.IndexOf(" ")).Replace("<", "")
                Case "input" ', "SELECT"
                    'se agrego el id porque en caso de que el codigo html no contenga la palabra type
                    'eso genera una exception al querer hacer un substring.
                    'sebastian [20/01/2009]
                    If E.ToLower.Contains("type") = True Then

                        'Dim aux As String = E.ToLower.Substring(E.ToLower.IndexOf("type"))
                        aux = E.ToLower.Substring(E.ToLower.IndexOf("type"))
                        aux = aux.Substring(0, aux.ToLower.IndexOf(" ")).Replace("type", "").Replace(Chr(34), "").Replace("=", "")
                    Else
                        'en caso de que no contenga la palabra type fuerzo a que entre por text
                        'sebastian [20/01/2009]
                        aux = "text"
                    End If

                    Select Case CStr(aux)
                        Case "text", "hidden"
                            If I.DropDown = IndexAdditionalType.AutoSustitución Then
                                If I.Data = Nothing Then
                                    value = value.Insert(value.Length - 1, " value= " & Chr(34) & " " & Chr(34))
                                Else
                                    value = value.Insert(value.Length - 1, " value= " & Chr(34) & I.Data & " - " & AutoSubstitutionBusiness.getDescription(I.Data, I.ID, False, I.Type) & Chr(34))
                                End If
                            Else
                                If I.Data = Nothing Then
                                    value = value.Insert(value.Length - 1, " value= " & Chr(34) & " " & Chr(34))
                                Else
                                    value = value.Insert(value.Length - 1, " value= " & Chr(34) & I.Data & Chr(34))
                                End If
                            End If
                        Case "checkbox"
                            If IsNothing(I.Data) OrElse I.Data = "0" OrElse I.Data = String.Empty Then
                                value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "0" & Chr(34))
                            Else
                                value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "1" & Chr(34))
                            End If
                        Case "radio"
                            Dim id As String = ""
                            If IsNothing(I.Data) Then
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "False" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            ElseIf I.Data = "0" Then
                                If id.ToUpper().EndsWith("N") Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "True" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("S") = True Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            ElseIf I.Data = "1" Then
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "True" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            Else
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "False" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            End If
                        Case "select-one"
                            If I.Data = Nothing Then
                                value = value.Insert(value.Length - 1, " value= " & Chr(34) & " " & Chr(34))
                            Else
                                value = value.Insert(value.Length - 1, " value= " & Chr(34) & I.Data & Chr(34))
                            End If
                    End Select
                Case "select"

                    Select Case I.DropDown
                        Case 1, 3
                            Dim DropTable As DataSet = IndexsBusiness.GetTable(I.ID)
                            For Each Row As DataRow In DropTable.Tables(0).Rows
                                value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("ItemId").ToString() & Chr(34))
                                value = value & "<option>" & Row("Item").ToString & "</option>"
                            Next

                        Case 2, 4
                            Dim SustTable As DataTable = AutoSubstitutionBusiness.GetIndexData(I.ID, False)

                            For Each Row As DataRow In SustTable.Rows
                                value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("codigo").ToString() & Chr(34))
                                value = value & "<option>" & Row("codigo").ToString() & " - " & Row("descripcion").ToString & "</option>"
                            Next
                    End Select


                    'Case 2

                    '   Indexs_Factory.GetTable(I.ID)
                    'End Select
                    'Else
                    '    value = value.Insert(value.Length - 2, " value= " & Chr(34) & I.Data & Chr(34))
                    'End If

                    'If IsNothing(I.Data) = False OrElse I.Data <> "0" OrElse I.Data <> String.Empty Then
                    '    Dim indice As Index = DirectCast(I, Index)
                    '    Select Case indice.DropDown
                    '        Case 1
                    '            'Andres 8/8/07 - Se guarda el valor pero no se usa 
                    '            'Dim Lista As ArrayList

                    '            'Si no esta cargada, cargo solo el item seleccionado
                    '            Dim readonli As String = E.GetAttribute("ReadOnly")

                    '            If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                    '                'Dim tag As HtmlElement = Me.AxWebBrowser1.Document.CreateElement("option")

                    '                'tag.SetAttribute("selected", I.Data)
                    '                'tag.SetAttribute("value", I.Data)
                    '                'tag.InnerText = I.Data
                    '                'E.AppendChild(tag)

                    '            Else

                    '                'Dim ListItem As New ListItem(I, E.Id)
                    '                'Me.ListToLoad1.Add(ListItem)
                    '            End If
                    '        Case 2
                    '            'Si no esta cargada, cargo solo el item seleccionado
                    '            Dim readonli As String = E.GetAttribute("ReadOnly")

                    '            If E.Enabled = False Or readonli.ToUpper() = "TRUE" Then
                    '                '    Dim tag As HtmlElement = Me.AxWebBrowser1.Document.CreateElement("option")

                    '                '    Dim desc As String = AutoSubstitutionBusiness.getDescription(I.Data, I.ID)
                    '                '    tag.SetAttribute("selected", I.Data)
                    '                '    tag.SetAttribute("value", I.Data)
                    '                '    tag.InnerText = I.Data & " - " & desc
                    '                '    E.AppendChild(tag)
                    '                'Else
                    '                '    Dim ListItem As New ListItem(I, E.Id)
                    '                '    Me.ListToLoad.Add(ListItem)
                    '            End If

                    '    End Select
                    '    Try
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Valor Id " & E.Id)
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Value " & E.DomElement.value)
                    '    Catch ex As Exception
                    '        Zamba.Core.ZClass.raiseerror(ex)
                    '    End Try
                    '    Try
                    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Valor Atributo" & I.Data)
                    '    Catch ex As Exception
                    '        Zamba.Core.ZClass.raiseerror(ex)
                    '    End Try
                    'End If
                Case "textarea"
                    If I.Data = Nothing Then
                        value = value.Insert(value.Length - 1, " value= " & Chr(34) & " " & Chr(34))
                    Else
                        value = value.Insert(value.Length - 1, " value= " & Chr(34) & I.Data & Chr(34))
                    End If
            End Select
            Return value
        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
            Return E
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return E
        End Try
    End Function
    Public Shared Function RepairHtml(ByVal Html As String) As String
        Return FormFactory.RepairHtml(Html)
    End Function
    Public Shared Function GetShowAndEditForms(ByVal docTypeId As Int64) As ZwebForm()
        If Not hsShowAndEditForms.ContainsKey(docTypeId) Then
            hsShowAndEditForms.Add(docTypeId, FormFactory.GetShowAndEditForms(docTypeId))
        End If
        Return hsShowAndEditForms.Item(docTypeId)
    End Function

    Public Shared Function GetDynamicFormIndexsNameAndId() As DataSet
        Return FormFactory.GetDynamicFormIndexsNameAndId()
    End Function

    Public Shared Function getFrmIndexOrderSectionByFormId(ByVal FormId As Int64) As DataSet
        Return FormFactory.getFrmIndexOrderSectionByFormId(FormId)
    End Function

    Public Shared Function GetAllDynamicFormSections() As DataSet
        Return FormFactory.GetAllDynamicFormSections()
    End Function

    ''' <summary>
    ''' crea una seccion para ser utilizados en formularios dinamicos
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <param name="sectionname"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 03.04.2009 created</history>
    Public Shared Sub InsertDynamicFormSection(ByVal sectionid As Int64, ByVal sectionname As String)
        FormFactory.InsertDynamicFormSection(sectionid, sectionname)
    End Sub

    ''' <summary>
    ''' elimina uan seccion de un formularios dinamicos
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 03.04.2009 created</history>
    Public Shared Sub DeleteDynamicFormSection(ByVal sectionid As Int64)
        FormFactory.DeleteDynamicFormSection(sectionid)
    End Sub

    ''' <summary>
    ''' Actualiza el nombre de una seccion segun id
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <param name="newSectionName"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 06.04.2009</history>
    Public Shared Sub UpdateDynamicFormSection(ByVal sectionid As Int64, ByVal newSectionName As String)
        FormFactory.UpdateDynamicFormSection(sectionid, newSectionName)
    End Sub

    Public Shared Function GetShowAndEditFormsCount(ByVal docTypeId As Int64) As Int32
        Return FormFactory.GetShowAndEditFormsCount(DocTypeId)
    End Function
    Public Shared Function GetShowAndEditFormsCountExtended(ByVal docTypeId As Int64) As Int32
        Return FormFactory.GetShowAndEditFormsCountExtended(DocTypeId)
    End Function

  
    Public Shared Function isDynamicForm(ByVal dynamicFormId As Int32, ByVal DocTypeId As Int64) As Boolean
        Return (FormFactory.isDynamicForm(dynamicFormId, DocTypeId))
    End Function


    Public Shared Function GetForms(ByVal DocTypeId As Int64, ByVal FormType As FormTypes) As ZwebForm()
        Dim Forms As ZwebForm() = FormFactory.GetForms(DocTypeId, FormType)
        For Each form As ZwebForm In Forms
            If Cache.DocTypesAndIndexs.hsForms.ContainsKey(form.ID) = False Then
                Cache.DocTypesAndIndexs.hsForms.Add(form.ID, form)
            End If
        Next

    End Function

    Public Shared Function GetForm(ByVal formID As Int64) As ZwebForm
        If Cache.DocTypesAndIndexs.hsForms.ContainsKey(formID) = False Then
            Cache.DocTypesAndIndexs.hsForms.Add(formID, FormFactory.GetForm(formID))
        End If
        Return Cache.DocTypesAndIndexs.hsForms.Item(formID)
    End Function

    Public Shared Function GetFormNameById(ByVal formID As Int64) As String
        If Cache.DocTypesAndIndexs.hsForms.ContainsKey(formID) = False Then
            Cache.DocTypesAndIndexs.hsForms.Add(formID, FormFactory.GetForm(formID))
        End If
        If Cache.DocTypesAndIndexs.hsForms.ContainsKey(formID) Then
            Dim form As ZwebForm = Cache.DocTypesAndIndexs.hsForms.Item(formID)
            If form IsNot Nothing Then
                Return form.Name
            End If
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Método que sirve para recuperar un FormId en base a un docTypeId
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	07/07/2008	Created
    ''' </history>
    Public Shared Function getDTFormId(ByVal docTypeId As Integer) As Integer
        Return FormFactory.getDTFormId(docTypeId)
    End Function

   
    Public Shared Function GetDocTypeId(ByVal formId As Int32) As Int64
        Return FormFactory.GetDocTypeId(formId)
    End Function

   
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un formulario
    ''' </summary>
    ''' <param name="Form">Formulario a eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    '''     [Gaston]    06/05/2009  Modified    Si es un form. dinámico primero se elimina de las tablas relacionadas con forms. dinámicos, y luego
    '''                                         de la tabla ZFrms
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteForm(ByVal Form As ZwebForm)

        If (Form.Path = String.Empty) Then
            'Formulario dinamico
            FormBusiness.DeleteDynamicFormsConfigs(Form.ID)
        End If

        FormFactory.DeleteForm(Form)
        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, Zamba.Core.RightsType.Delete, "Se eliminó el formulario: " & Form.Name & "(" & Form.ID & ")")

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un formulario a la base de datos
    ''' </summary>
    ''' <param name="Form">Formulario a agregar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertForm(ByVal Form As ZwebForm)
        FormFactory.InsertForm(Form)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.insert, "Se agregó el formulario: " & Form.Name & "(" & Form.ID & ")")
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza los datos de un formulario
    ''' </summary>
    ''' <param name="Form">Formulario</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateForm(ByVal Form As ZwebForm)
        FormFactory.UpdateForm(Form)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.Edit, "Se modificó el formulario: " & Form.Name & "(" & Form.ID & ")")
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Asigna un formulario a una etapa de WorkFlow
    ''' </summary>
    ''' <param name="Form">Formulario</param>
    ''' <param name="WfStepId">Etapa de WorkFlow</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub AsignForm2WfStep(ByVal Form As ZwebForm, ByVal WfStepId As Int32)
        FormFactory.AsignForm2WfStep(Form, WfStepId)
        Dim stepName As String = WFStepBusiness.GetStepNameById(WfStepId)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.Asign, "Se asigna el formulario " & Form.Name & "(" & Form.ID & ") a la etapa " & stepName & "(" & WfStepId & ")")
    End Sub

    Public Shared Sub CreateAutoForm(ByVal DocType As DocType, ByVal Indexs As Index(), ByVal File As String)
        FormFactory.CreateAutoForm(DocType, Indexs, File)
    End Sub

#Region "InsertVirtualForm"
    Public Shared Function CreateVirtualDocument(ByVal docTypeId As Long) As NewResult
        Dim InsertedResult As New NewResult()
        InsertedResult.DocType = DocTypesBusiness.GetDocType(docTypeId, True)
        Results_Business.LoadIndexs(DirectCast(InsertedResult, NewResult))
        '  Results_Business.InsertDocument(InsertedResult, False, False, False, True, True)
        '  Results_Factory.CompleteDocument(DirectCast(InsertedResult, NewResult))
        Return InsertedResult
    End Function
#End Region


    Public Overrides Sub Dispose()

    End Sub

    ''' <summary>
    ''' Añade un archivo requerido al formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <param name="requirementPath"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Sub AddRequierement(ByVal formId As Int64, ByVal requirementPath As String)
        If Not IO.File.Exists(requirementPath) Then
            Throw New IO.FileNotFoundException("No existe el archivo requerido " + Environment.NewLine + requirementPath)
        End If

        FormFactory.AddRequierement(formId, requirementPath)
    End Sub
    ''' <summary>
    ''' Remueve un archivo requerido del formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <param name="requirementPath"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Sub RemoveRequirement(ByVal formId As Int64, ByVal requirementPath As String)
        FormFactory.RemoveRequirement(formId, requirementPath)
    End Sub
    ''' <summary>
    ''' Añade todos los archivos requerido del formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Sub ClearRequirements(ByVal formId As Int64)
        FormFactory.ClearRequirements(formId)
    End Sub

    ''' <summary>
    ''' [sebastian 06-02-2009]Devuelve un data set con la info para completar el formulario dinamico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDynamicFormValues() As DataSet
        Return FormFactory.GetDynamicFormValues()
    End Function

    ''' <summary>
    ''' Método que sirve para obtener un formulario dinámico llamando al correspondiente método ubicado en en FormFactory
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    03/03/2009  Created
    ''' </history>
    Public Shared Function GetDynamicForm(ByRef dynamicFormId As Integer) As DataSet
        Return (FormFactory.GetDynamicForm(dynamicFormId))
    End Function
    ''' <summary>
    ''' Método que sirve para obtener un formulario dinámico llamando al correspondiente método ubicado en en FormFactory
    ''' para la regla DO GENERATE DINAMIC FORM
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Sebastian] 10-09-2009
    ''' </history>
    Public Shared Function GetDynamicFormFromRuleGenerateDinamicForm(ByRef dynamicFormId As Integer) As DataSet
        Return (FormFactory.GetDynamicFormFromRuleGenerateDinamicForm(dynamicFormId))
    End Function
    ''' <summary>
    ''' [sebastian 10-02-2009]
    ''' Obtiene el nombre de la seccion para luego ser utilizado como titulo de la tabla en el formulario
    ''' dinamico
    ''' </summary>
    ''' <param name="SectionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDynamicFormSectionName(ByVal SectionId As String) As String
        Return FormFactory.GetDynamicFormSectionName(SectionId)
    End Function

    Public Shared Sub SaveZFrmDescValues(ByRef QueryValues As Hashtable)
        FormFactory.SaveZFrmDescValues(QueryValues)
    End Sub

    Public Shared Sub SaveZFrmDynamicForms(ByRef QueryValues As Generic.List(Of Hashtable))
        FormFactory.SaveZFrmDynamicForms(QueryValues)
    End Sub

    Public Shared Sub SaveSectionValues(ByVal Id As Int32, ByVal SectionName As String)
        FormBusiness.SaveSectionValues(Id, SectionName)
    End Sub

    ''' <summary>
    ''' Método que sirve para obtener los atributos de un formulario dinámico (tabla zFrmDynamicForms)
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Shared Function getIndexsOfDynamicFormId(ByVal dynamicFormId As Integer) As DataSet
        Return (FormFactory.getIndexsOfDynamicFormId(dynamicFormId))
    End Function

    ''' <summary>
    ''' Método que sirve para obtener los atributos de un formulario dinámico (tabla ZFrmIndexsDesc)
    ''' </summary>
    ''' <param name="fId">Id de un formulario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Shared Function getIndexsFromFrmsIndexsDesc(ByVal fId As Integer) As DataSet
        Return (FormFactory.getIndexsFromFrmsIndexsDesc(fId))
    End Function

    ''' <summary>
    ''' Método que sirve para insertar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="fId">Id de formulario</param>
    ''' <param name="iId">Id de un Atributo</param>
    ''' <param name="type">Elemento de enumerador</param>
    ''' <param name="value">0,1 o id de un Atributo</param>
    ''' <param name="eId">Identificador único</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Shared Function insertFrmIndexsDesc(ByVal fId As Integer, ByVal iId As Integer, ByVal type As Short, ByVal value As String) As Int32
        Dim eId As Integer = CoreBusiness.GetNewID(IdTypes.formsSpecification)
        FormFactory.insertFrmIndexsDesc(fId, iId, type, value, eId)
        Return (eId)
    End Function

    ''' <summary>
    ''' Método que sirve para actualizar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="fId">Id de formulario</param>
    ''' <param name="iId">Id de un Atributo</param>
    ''' <param name="type">Elemento de enumerador</param>
    ''' <param name="value">0,1 o id de un Atributo</param>
    ''' <param name="eId">Identificador único</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    26/02/2009  Created
    ''' </history>
    Public Shared Sub updateFrmIndexsDesc(ByVal fId As Integer, ByVal iId As Integer, ByVal type As Short, ByVal value As String, ByVal eId As Integer)
        FormFactory.updateFrmIndexsDesc(fId, iId, type, value, eId)
    End Sub

    ''' <summary>
    ''' Método que sirve para eliminar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="eId">Identificador único</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    27/02/2009  Created
    ''' </history>
    Public Shared Sub deleteFrmIndexsDesc(ByVal eId As Integer)
        FormFactory.deleteFrmIndexsDesc(eId)
    End Sub

    ''' <summary>
    ''' Se obtienen los atributos obligatorios (requeridos) que posee el formulario dinámico
    ''' </summary>
    ''' <param name="frmId">Id de formulario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    11/03/2009  Created
    ''' </history>
    Public Shared Function getRequiredIndexs(ByVal frmId As Int64) As DataSet
        Return (FormFactory.getRequiredIndexs(frmId))
    End Function

    ''' <summary>
    ''' Método que llama a un método que sirve para obtener los atributos de tipo exceptuable que pertenecen a un determinado tipo de formulario dinámico
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    08/05/2009  Created
    ''' </history>
    Public Shared Function getExceptuableIndexs(ByVal frmId As Int64) As DataSet
        Return (FormFactory.getExceptuableIndexs(frmId))
    End Function

    ''' <summary>
    ''' Método que llama a un método que sirve para obtener todos los datos referidos a atributos exceptuables de un determinado tipo de formulario dinámico
    ''' </summary>
    ''' <param name="frmId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    08/05/2009  Created
    ''' </history>
    Public Shared Function getDataExceptuableIndexs(ByVal frmId As Int64) As DataSet
        Return (FormFactory.getDataExceptuableIndexs(frmId))
    End Function

    ''' <summary>
    ''' Se obtienen los valores de la propiedad "Sólo Lectura" y "Visible" que posea el Atributo del formulario dinámico
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="iId">Id de un Atributo que pertenece al formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    13/03/2009  Created
    ''' </history>
    Public Shared Function getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(ByVal frmId As Int64, ByVal iId As Int32) As DataSet
        Return (FormFactory.getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(frmId, iId))
    End Function

    ''' <summary>
    ''' Método utilizado para guardar las condiciones que se aplican sobre un formulario normal (formulario almacenado en el servidor)
    ''' </summary>
    ''' <param name="state">Instancia que contiene datos del formulario normal</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    05/05/2009  Created
    ''' </history>
    Public Shared Sub updateNormalForm(ByRef state As DynamicFormState)

        ' Se eliminan las condiciones del formulario normal
        FormFactory.deleteNormalFormsConfigs(state.Formid)

        ' Se guardan las condiciones del formulario normal
        For Each Item As Hashtable In state.ConditionsFormValues
            FormBusiness.SaveZFrmDescValues(Item)
        Next

    End Sub

    ''' <summary>
    ''' Actualiza un Dynamic form, lo elimina y lo vuelve a crear
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 31.3.2009 created</history>
    Public Shared Sub UpdateDynamicForm(ByRef state As DynamicFormState)

        'borra el formulario
        FormFactory.DeleteDynamicFormsConfigs(state.Formid)
        'Crea de nuevo el formulario
        SaveDynamicForm(state)

    End Sub

    ''' <summary>
    ''' Método que sirve para obtener un nuevo id para el formulario dinámico que se quiere crear
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	05/05/2009	Created    
    ''' </history>
    Public Shared Sub generateDynamicFormId(ByRef state As DynamicFormState)

        Try

            If (Not state.Edit) Then
                ' Se genera un id para el formulario dinámico
                Dim formid As Int64 = ToolsBusiness.GetNewID(IdTypes.ZWEBFORM)
                state.UpdateFormId(formid)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Actualiza un Dynamic form, lo elimina y lo vuelve a crear
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 27.3.2009 created
    '''     [Gaston]	05/05/2009	Modified    Se comento la línea que genera el nuevo id
    '''</history>
    Public Shared Function SaveDynamicForm(ByRef state As DynamicFormState) As Boolean
        Try
            'If Not state.Edit Then
            '    ' Se genera un id para el formulario dinámico
            '    Dim formid As Int64 = ToolsBusiness.GetNewID(IdTypes.ZWEBFORM)
            '    state.UpdateFormId(formid)
            'End If
            'Guarda los datos de los forms
            FormBusiness.SaveZFrmDynamicForms(state.DynamicFormValues)

            'guarda las condiciones
            For Each Item As Hashtable In state.ConditionsFormValues
                FormBusiness.SaveZFrmDescValues(Item)
            Next

            'Guarda las propiedades de atributos
            For Each Items As String() In state.IndexsPropertiesFormValues
                FormBusiness.insertFrmIndexsDesc(Convert.ToInt32(Items(0)), _
                                                 Convert.ToInt32(Items(1)), _
                                                 Convert.ToInt16(Items(3)), _
                                                 Items(6))
            Next
            Return True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el estado de un formulario dinamico, necesario para su edicion
    ''' </summary>
    ''' <param name="state">estado del formulario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''        dalbarellos 01.04.2009 created
    '''     [Gaston]	06/04/2009	Modified    Se elimino el useConditions
    ''' </history>
    Public Shared Sub GetDynamicFormState(ByRef state As DynamicFormState)

        'get conditions
        Dim item As New Hashtable
        Dim ds As DataSet

        ds = FormFactory.GetDynamicFormIndexsConditions(state.Formid)

        'save conditions
        For Each dr As DataRow In ds.Tables(0).Rows

            item = New Hashtable
            item.Add("DocType", state.Doctypeid.ToString.Trim)
            item.Add("Index", dr.Item("IId").ToString.Trim)
            item.Add("CompOperator", dr.Item("Op").ToString.Trim)
            item.Add("Conexion", dr.Item("Qid").ToString.Trim)
            item.Add("Value", dr.Item("Value").ToString.Trim)
            item.Add("FormId", state.Formid.ToString.Trim)
            state.ConditionsFormValues.Add(item)

        Next

        ds = New DataSet
        item = New Hashtable

        'get form values
        ds = FormBusiness.GetDynamicForm(state.Formid)

        'save form values
        For Each r As DataRow In ds.Tables(0).Rows
            item = New Hashtable()
            item.Add("QueryId", state.Formid.ToString.Trim)
            item.Add("IndexId", r.Item("IdIndice").ToString.Trim)
            item.Add("SectionId", r.Item("IdSeccion").ToString.Trim)
            item.Add("NroOrden", r.Item("NroOrden").ToString.Trim)
            item.Add("NroFila", r.Item("NroFila").ToString.Trim)
            state.DynamicFormValues.Add(item)
        Next

        ds = New DataSet
        item = New Hashtable

        'get Index properties
        ds = getIndexsFromFrmsIndexsDesc(state.Formid)
        'save form values
        For Each r As DataRow In ds.Tables(0).Rows
            Dim dgvRow As String() = New String() _
                    {state.Formid, r.Item("iId"), _
                     IndexsBusiness.GetIndexName(r.Item("iId"), True), _
                     r.Item("Type"), GetDynamicFormIndexProperty(r.Item("Type")), _
                     r.Item("EId"), r.Item("value")}

            state.IndexsPropertiesFormValues.Add(dgvRow)
        Next

    End Sub
    ''' <summary>
    ''' Retorna las condiciones de aplicadas sobre atributos en los formularios dinamicos
    ''' </summary>
    ''' <param name="formid">Id del formulario</param>
    ''' <returns></returns>
    ''' <history>[pablo] 10.10.2010 created</history>
    Public Shared Function GetDynamicFormIndexsConditions(ByVal FormId As Int64) As DataSet
        Return FormFactory.GetDynamicFormIndexsConditions(FormId)
    End Function

    ''' <summary>
    ''' Retorna un nombre de tipo en base a un id
    ''' </summary>
    ''' <param name="idtype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 01.04.2009 created</history>
    Private Shared Function GetDynamicFormIndexProperty(ByVal idtype As Int32) As String
        Select Case idtype
            Case 0
                Return "Requerido"
            Case 1
                Return "Sólo lectura"
            Case 2
                Return "Exceptuable"
            Case 3
                Return "Visible"
            Case Else
                Throw New NotImplementedException("El tipo no esta implementado")
        End Select
    End Function

    ''' <summary>
    ''' Actualiza la fecha de modificacion del formulario
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateModifiedDate(ByVal formId As Int32)
        FormFactory.UpdateModifiedDate(formId)
    End Sub

    ''' <summary>
    ''' Obtiene la fecha de modificacion del formulario
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks>Devuelve Nothing en caso de error</remarks>
    Public Shared Function GetModifiedDate(ByVal formId As Int32) As DateTime
        Try
            Return FormFactory.GetModifiedDate(formId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Shared Function GetFormConditionsTable(ByVal formID As Integer) As DataTable
        Return FormFactory.GetFormConditionsTable(formID)
    End Function

    Shared Function GetFormConditions(ByVal formID As Integer) As List(Of IZFormCondition)
        Return FormFactory.GetFormConditions(formID)
    End Function

    Shared Function GetCondition(ByVal optionID As Long) As IZFormCondition
            Return FormFactory.GetCondition(optionID)
    End Function

    Shared Sub AddCondition(ByVal formID As Long, ByVal indexSource As Int64, ByVal comparator As Comparators, _
                            ByVal value As String, ByVal indexTarget As Int64, ByVal action As FormActions)

        FormFactory.AddCondition(formID, indexSource, comparator, value, indexTarget, action)
    End Sub

    Shared Sub EditCondition(ByVal id As Int64, ByVal indexSource As Int64, _
                             ByVal comparator As Comparators, ByVal value As String, ByVal indexTarget As Int64, ByVal action As FormActions)

        FormFactory.EditCondition(id, indexSource, comparator, value, indexTarget, action)
    End Sub

    Shared Sub DeleteCondition(ByVal ConditionID As Long)
        FormFactory.DeleteCondition(ConditionID)
    End Sub

End Class