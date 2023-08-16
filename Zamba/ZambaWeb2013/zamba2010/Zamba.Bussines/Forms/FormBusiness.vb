Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.IO
Imports System.Collections.Generic

Public Class FormBusiness
    Inherits ZClass

    Dim UB As New UserBusiness()
    Dim FF As New FormFactory
    Private Const OPTION_FORMAT_SELECTED As String = "<option value=""{0}"" selected=""selected"">{1}</option>"
    Private Const OPTION_FORMAT_NON_SELECTED As String = "<option value=""{0}"">{1}</option>"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los formularios 
    ''' </summary>
    ''' <returns>arrayList de objetos ZwebForm</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetForms() As ArrayList
        Return FF.GetForms()
    End Function
    ''' Tomas 27/02/09
    Public Sub DeleteDynamicFormsConfigs(ByVal IdFormulario As Int64)
        FF.DeleteDynamicFormsConfigs(IdFormulario)
    End Sub

    'Public Function GetMaxSectionId() As Int32
    '    Return FF.GetMaxSectionId()
    'End Function

    Public Function GetFormsByType(ByVal Formtype As FormTypes) As ArrayList
        Return FF.GetFormsByType(Formtype)
    End Function
    ''' <summary>
    ''' Metodo que trae los formularios virtuales de tipo insercion
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [pablo] 10/10/2010 Created
    Public Function GetInsertFormsByDocTypeId(ByVal docTypeId As Int32) As ZwebForm()
        Return FF.GetInsertFormsByDocTypeId(docTypeId)
    End Function

    Public Function GetVirtualDocumentsByRightsOfView(ByVal Formtype As FormTypes, ByVal userid As Int64) As ArrayList
        Dim doctypesByRights As New Generic.List(Of Int64)
        doctypesByRights = RightComponent.GetAllDocTypesByUserRight(userid)
        Return FF.GetVirtualDocumentsByRightsOfView(Formtype, userid, doctypesByRights)
    End Function

    ''' <summary>
    ''' Metodo que trae los formularios virtuales por los cual el usuario tiene permisos de crear.
    ''' </summary>
    ''' <param name="Formtype"></param>
    ''' <param name="userid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 31/03/2009 Created
    Public Function GetVirtualDocumentsByRightsOfCreate(ByVal Formtype As FormTypes, ByVal userid As Int64) As ArrayList
        If Not Cache.Forms.VirtualDocumentsByRightsOfCreate.ContainsKey(Formtype & "-" & userid) Then
            Dim doctypesByRights As New Generic.List(Of Int64)
            doctypesByRights = RightComponent.GetAllDocTypesByUserRightOfCreate(userid)
            Dim VirtualDocuments As ArrayList = FF.GetVirtualDocumentsByRightsOfView(Formtype, userid, doctypesByRights)
            Cache.Forms.VirtualDocumentsByRightsOfCreate.Add(Formtype & "-" & userid, VirtualDocuments)
        End If
        Return Cache.Forms.VirtualDocumentsByRightsOfCreate(Formtype & "-" & userid)
    End Function

    ''' <summary>
    ''' Obtiene el body del html completo
    ''' </summary>
    ''' <param name="CompleteHTML"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInnerHTML(ByVal CompleteHTML As String) As String
        'antes estaba esto, que se comento porque tiraba que estaba fuera de rango el indice del split.
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
    Public Function GetAllForms(ByVal DocTypeId As Int64) As ZwebForm()
        Return (FF.GetAllForms(DocTypeId))
    End Function

    ''' <summary>
    ''' Cargo el valor del indice al elemento html
    ''' </summary>
    ''' <param name="I"></param>
    ''' <param name="E"></param>
    ''' <history> Marcelo 31/07/08 Modified</history>
    ''' <remarks></remarks>
    Public Function AsignValue(ByVal I As IIndex, ByVal E As String, Optional ByVal result As IResult = Nothing) As String
        Dim ASB As New AutoSubstitutionBusiness

        Try
            Dim value As String = E
            Dim aux As String


            'Filtra por tipo de control...
            Select Case E.ToLower.Substring(0, E.ToLower.IndexOf(" ")).Replace("<", String.Empty)
                Case "input" ', "SELECT"
                    'se agrego el id porque en caso de que el codigo html no contenga la palabra type
                    'eso genera una exception al querer hacer un substring.
                    'sebastian [20/01/2009]
                    If E.ToLower.Contains(" type=") = True Then

                        'Dim aux As String = E.ToLower.Substring(E.ToLower.IndexOf("type"))
                        aux = E.ToLower.Substring(E.ToLower.IndexOf(" type=")).Replace(" type=", String.Empty)

                        If aux.Contains(" ") Then
                            aux = aux.Substring(0, aux.ToLower.IndexOf(" ")).Replace(Chr(34), String.Empty).Replace("=", String.Empty).Replace(">", String.Empty)
                        Else
                            aux = aux.Replace(Chr(34), String.Empty).Replace("=", String.Empty).Replace(">", String.Empty)
                        End If
                    Else
                        'en caso de que no contenga la palabra type fuerzo a que entre por text
                        'sebastian [20/01/2009]
                        aux = "text"
                    End If

                    'Tener en cuenta que los tags HTML puede finalizar en ">" y "/>" este ultimo no estaba contemplado
                    Dim valuePosition As Integer = IIf(value.ToString().Substring(value.Length - 2) = "/>", value.Length - 2, value.Length - 1)

                    Select Case CStr(aux)
                        Case "text", "hidden"
                            If I.DropDown = IndexAdditionalType.AutoSustitución Or I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If I.Data = Nothing Then
                                    value = value.Insert(valuePosition, " value= " & Chr(34) & Chr(34))
                                ElseIf I.dataDescription Is Nothing OrElse I.dataDescription.Length = 0 Then

                                    value = value.Insert(valuePosition, " value= " & Chr(34) & I.Data & " - " & ASB.getDescription(I.Data, I.ID) & Chr(34))
                                Else
                                    value = value.Insert(valuePosition, " value= " & Chr(34) & I.Data & " - " & I.dataDescription & Chr(34))
                                End If
                            Else
                                If I.Data = Nothing Then
                                    value = value.Insert(valuePosition, " value= " & Chr(34) & Chr(34))
                                Else
                                    value = value.Insert(valuePosition, " value= " & Chr(34) & I.Data & Chr(34))
                                End If
                            End If
                        Case "checkbox"
                            If IsNothing(I.Data) OrElse I.Data = "0" OrElse I.Data = String.Empty Then
                                'value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "0" & Chr(34))
                            Else
                                value = value.Insert(valuePosition, " checked= " & Chr(34) & "1" & Chr(34))
                            End If
                        Case "radio"
                            Dim id As String = String.Empty
                            If IsNothing(I.Data) Then
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            ElseIf I.Data = "0" Then
                                If id.ToUpper().EndsWith("N") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "True" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("S") = True Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            ElseIf I.Data = "1" Then
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "True" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            Else
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            End If
                        Case "select-one"
                            If I.Data = Nothing Then
                                value = value.Insert(valuePosition, " value= " & Chr(34) & Chr(34))
                            Else
                                value = value.Insert(valuePosition, " value= " & Chr(34) & I.Data & Chr(34))
                            End If
                    End Select
                Case "select"

                    Select Case I.DropDown
                        Case IndexAdditionalType.DropDown
                            Dim selected As Boolean
                            Dim arrOptions As List(Of String) = IndexsBusiness.GetDropDownList(I.ID)
                            For Each s As String In arrOptions
                                If I.Data.Trim = s.Trim Then selected = True
                                'value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("ItemId").ToString() & Chr(34))
                                value = value & "<option value=" & Chr(34) & s & Chr(34) & " " & IIf(I.Data.Trim = s.Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & s & "</option>"
                            Next
                            If Not value.Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) Then
                                value = value + "<option value=" & Chr(34) & String.Empty & Chr(34) & IIf(Not selected, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">A Definir</option>"
                            End If
                        Case IndexAdditionalType.AutoSustitución
                            If E.ToLower.Contains("generatelistdinamic") = False Then
                                Dim SustTable As DataTable = ASB.GetIndexData(I.ID, False)
                                Dim selected As Boolean
                                If E.ToLower.Contains("withcode") = True Then
                                    For Each Row As DataRow In SustTable.Rows
                                        If I.Data.Trim = Row("codigo").ToString().Trim Then selected = True
                                        '   value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("codigo").ToString() & Chr(34))
                                        value = value & "<option value=" & Chr(34) & Row("codigo").ToString() & Chr(34) & " " & IIf(I.Data.Trim = Row("codigo").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & Row("codigo").ToString() & " - " & Row("descripcion").ToString & "</option>"
                                    Next
                                Else
                                    For Each Row As DataRow In SustTable.Rows
                                        If I.Data.Trim = Row("codigo").ToString().Trim Then selected = True
                                        '   value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("codigo").ToString() & Chr(34))
                                        value = value & "<option value=" & Chr(34) & Row("codigo").ToString() & Chr(34) & " " & IIf(I.Data.Trim = Row("codigo").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & Row("descripcion").ToString & "</option>"
                                    Next
                                End If

                                If Not value.Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) Then
                                    value = value + "<option value=" & Chr(34) & String.Empty & Chr(34) & IIf(Not selected, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">A Definir</option>"
                                End If
                            End If
                        Case IndexAdditionalType.DropDownJerarquico
                            Dim selected As Boolean
                            If I.HierarchicalParentID > 0 Then
                                Dim parentIndex As IIndex
                                For Each index As IIndex In result.Indexs
                                    If index.ID = I.HierarchicalParentID Then
                                        parentIndex = index
                                    End If
                                Next
                                Dim searchTable As DataTable
                                Dim IB As New IndexsBusiness
                                If parentIndex Is Nothing Then
                                    parentIndex = IB.GetIndex(I.HierarchicalParentID)
                                End If

                                searchTable = IB.GetHierarchicalTableByValue(I.ID, parentIndex)

                                IB = Nothing
                                For Each row As DataRow In searchTable.Rows
                                    If I.Data.Trim = row("Value").ToString().Trim Then selected = True
                                    If String.IsNullOrEmpty(row("Value")) Then
                                        value = value + "<option value=" & Chr(34) & String.Empty & Chr(34) & IIf(Not selected, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">A Definir</option>"
                                    Else
                                        value = value & "<option value=" & Chr(34) & row("Value").ToString() & Chr(34) & " " & IIf(I.Data.Trim = row("Value").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & row("Value").ToString & "</option>"
                                    End If
                                Next
                            Else
                                Dim arrOptions As List(Of String) = IndexsBusiness.GetDropDownList(I.ID)
                                For Each s As String In arrOptions
                                    If I.Data.Trim = s.Trim Then selected = True
                                    'value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("ItemId").ToString() & Chr(34))
                                    value = value & "<option value=" & Chr(34) & s & Chr(34) & " " & IIf(I.Data.Trim = s.Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & s & "</option>"
                                Next
                                If Not value.Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) Then
                                    value = value + "<option value=" & Chr(34) & String.Empty & Chr(34) & IIf(Not selected, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">A Definir</option>"
                                End If
                            End If
                        Case IndexAdditionalType.AutoSustituciónJerarquico
                            Dim selected As Boolean
                            If I.HierarchicalParentID > 0 Then
                                Dim parentIndex As IIndex
                                For Each index As IIndex In result.Indexs
                                    If index.ID = I.HierarchicalParentID Then
                                        parentIndex = index
                                    End If
                                Next
                                Dim IB As New IndexsBusiness
                                If (parentIndex Is Nothing) Then parentIndex = IB.getIndex(I.HierarchicalParentID)


                                Dim searchTable As DataTable

                                searchTable = IB.GetHierarchicalTableByValue(I.ID, parentIndex)

                                IB = Nothing

                                If E.ToLower.Contains("withcode") = True Then
                                    For Each row As DataRow In searchTable.Rows
                                        If I.Data.Trim = row("Value").ToString().Trim Then selected = True
                                        value = value & "<option value=" & Chr(34) & row("Value").ToString() & Chr(34) & " " & IIf(I.Data.Trim = row("Value").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & row("Value").ToString() & " - " & row("Description").ToString & "</option>"
                                    Next
                                Else
                                    If searchTable IsNot Nothing Then
                                        For Each row As DataRow In searchTable.Rows
                                            If I.Data.Trim = row("Value").ToString().Trim Then selected = True
                                            value = value & "<option value=" & Chr(34) & row("Value").ToString() & Chr(34) & " " & IIf(I.Data.Trim = row("Value").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & row("Description").ToString & "</option>"
                                        Next
                                    End If

                                End If

                                If Not (value.Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) OrElse value.Contains("<option value=" & Chr(34) & "0" & Chr(34))) Then
                                        value = value + "<option value=" & Chr(34) & String.Empty & Chr(34) & IIf(Not selected, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">A Definir</option>"
                                    End If
                                Else
                                    Dim SustTable As DataTable = ASB.GetIndexData(I.ID, False)

                                If E.ToLower.Contains("withcode") = True Then
                                    For Each Row As DataRow In SustTable.Rows
                                        If I.Data.Trim = Row("codigo").ToString().Trim Then selected = True
                                        '   value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("codigo").ToString() & Chr(34))
                                        value = value & "<option value=" & Chr(34) & Row("codigo").ToString() & Chr(34) & " " & IIf(I.Data.Trim = Row("codigo").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & Row("codigo").ToString() & " - " & Row("descripcion").ToString & "</option>"
                                    Next
                                Else
                                    For Each Row As DataRow In SustTable.Rows
                                        If I.Data.Trim = Row("codigo").ToString().Trim Then selected = True
                                        '   value = value.Insert(value.Length - 1, "value=" & Chr(34) & Row("codigo").ToString() & Chr(34))
                                        value = value & "<option value=" & Chr(34) & Row("codigo").ToString() & Chr(34) & " " & IIf(I.Data.Trim = Row("codigo").ToString().Trim, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">" & Row("descripcion").ToString & "</option>"
                                    Next
                                End If
                                If Not value.Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) Then
                                        value = value + "<option value=" & Chr(34) & String.Empty & Chr(34) & IIf(Not selected, "selected=" & Chr(34) & "selected" & Chr(34), String.Empty) & ">A Definir</option>"
                                    End If
                                End If
                    End Select
                Case "textarea"
                    If I.Data = Nothing Then
                        value = value.Insert(value.Length, " ")
                    Else
                        value = value.Insert(value.Length, I.Data)
                    End If
            End Select
            Return value
        Catch ex As System.Runtime.InteropServices.COMException
            Zamba.Core.ZClass.raiseerror(ex)
            Return E
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return E
        Finally
            ASB = Nothing
        End Try
    End Function

    Public Function AsignVarValue(ByVal VarValue As String, ByVal E As String, Optional ByVal result As IResult = Nothing) As String

        Try
            Dim value As String = E
            Dim aux As String
            'Filtra por tipo de control...
            Select Case E.ToLower.Substring(0, E.ToLower.IndexOf(" ")).Replace("<", String.Empty)
                Case "input" ', "SELECT"
                    'se agrego el id porque en caso de que el codigo html no contenga la palabra type
                    'eso genera una exception al querer hacer un substring.
                    'sebastian [20/01/2009]
                    If E.ToLower.Contains(" type=") = True Then

                        'Dim aux As String = E.ToLower.Substring(E.ToLower.IndexOf("type"))
                        aux = E.ToLower.Substring(E.ToLower.IndexOf(" type=")).Replace(" type=", String.Empty)

                        If aux.Contains(" ") Then
                            aux = aux.Substring(0, aux.ToLower.IndexOf(" ")).Replace(Chr(34), String.Empty).Replace("=", String.Empty).Replace(">", String.Empty)
                        Else
                            aux = aux.Replace(Chr(34), String.Empty).Replace("=", String.Empty).Replace(">", String.Empty)
                        End If
                    Else
                        'en caso de que no contenga la palabra type fuerzo a que entre por text
                        'sebastian [20/01/2009]
                        aux = "text"
                    End If

                    'Tener en cuenta que los tags HTML puede finalizar en ">" y "/>" este ultimo no estaba contemplado
                    Dim valuePosition As Integer = IIf(value.ToString().Substring(value.Length - 2) = "/>", value.Length - 2, value.Length - 1)

                    Select Case CStr(aux)
                        Case "text", "hidden"

                            If VarValue = Nothing Then
                                value = value.Insert(valuePosition, " value= " & Chr(34) & Chr(34))
                            Else
                                value = value.Insert(valuePosition, " value= " & Chr(34) & VarValue & Chr(34))
                            End If
                        Case "checkbox"
                            If IsNothing(VarValue) OrElse VarValue = "0" OrElse VarValue = String.Empty Then
                                'value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "0" & Chr(34))
                            Else
                                value = value.Insert(valuePosition, " checked= " & Chr(34) & "1" & Chr(34))
                            End If
                        Case "radio"
                            Dim id As String = String.Empty
                            If IsNothing(VarValue) Then
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            ElseIf VarValue = "0" Then
                                If id.ToUpper().EndsWith("N") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "True" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("S") = True Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            ElseIf VarValue = "1" Then
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "True" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            Else
                                If id.ToUpper().EndsWith("S") Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                ElseIf id.ToUpper().EndsWith("N") = False Then
                                    value = value.Insert(valuePosition, " checked= " & Chr(34) & "False" & Chr(34))
                                End If
                            End If
                        Case "select-one"
                            If VarValue = Nothing Then
                                value = value.Insert(valuePosition, " value= " & Chr(34) & Chr(34))
                            Else
                                value = value.Insert(valuePosition, " value= " & Chr(34) & VarValue & Chr(34))
                            End If
                    End Select
                Case "select"
                    value = value + "<option value=" & Chr(34) & VarValue & Chr(34) & "selected=" & Chr(34) & "selected" & Chr(34) & ">" & VarValue & "</option>"

                Case "textarea"
                    If VarValue = Nothing Then
                        value = value.Insert(value.Length, " ")
                    Else
                        value = value.Insert(value.Length, VarValue)
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

    Public Function RepairHtml(ByVal Html As String) As String
        Return FF.RepairHtml(Html)
    End Function
    Public Function GetShowAndEditForms(ByVal docTypeId As Int64) As ZwebForm()
        If Not Cache.Forms.DicForms.ContainsKey(docTypeId) Then
            Dim forms As ZwebForm() = FF.GetShowAndEditForms(docTypeId)
            Cache.Forms.DicForms.Add(docTypeId, forms)
        End If

        Return Cache.Forms.DicForms(docTypeId)
    End Function

    Public Function GetDynamicFormIndexsNameAndId() As DataSet
        Return FF.GetDynamicFormIndexsNameAndId()
    End Function

    Public Function getFrmIndexOrderSectionByFormId(ByVal FormId As Int64) As DataSet
        Return FF.getFrmIndexOrderSectionByFormId(FormId)
    End Function

    Public Function GetAllDynamicFormSections() As DataSet
        Return FF.GetAllDynamicFormSections()
    End Function

    ''' <summary>
    ''' crea una seccion para ser utilizados en formularios dinamicos
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <param name="sectionname"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 03.04.2009 created</history>
    Public Sub InsertDynamicFormSection(ByVal sectionid As Int64, ByVal sectionname As String)
        FF.InsertDynamicFormSection(sectionid, sectionname)
    End Sub

    ''' <summary>
    ''' elimina uan seccion de un formularios dinamicos
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 03.04.2009 created</history>
    Public Sub DeleteDynamicFormSection(ByVal sectionid As Int64)
        FF.DeleteDynamicFormSection(sectionid)
    End Sub

    ''' <summary>
    ''' Actualiza el nombre de una seccion segun id
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <param name="newSectionName"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 06.04.2009</history>
    Public Sub UpdateDynamicFormSection(ByVal sectionid As Int64, ByVal newSectionName As String)
        FF.UpdateDynamicFormSection(sectionid, newSectionName)
    End Sub

    Public Function HasForms(ByVal docTypeId As Int64) As Boolean
        If Not Cache.Forms.DicHasForms.ContainsKey(docTypeId) Then
            SyncLock Cache.Forms.DicHasForms
                If Not Cache.Forms.DicHasForms.ContainsKey(docTypeId) Then
                    Dim count As Int32 = FF.GetShowAndEditFormsCount(docTypeId)
                    Cache.Forms.DicHasForms.Add(docTypeId, count > 0)
                End If
            End SyncLock
        End If

        Return Cache.Forms.DicHasForms(docTypeId)
    End Function

    ''' <summary>
    ''' Método que sirve para comprobar si el formulario es en verdad un formulario dinámico
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario (supuestamente dinámico)</param>
    ''' <param name="DocTypeId">Tipo de documento al que pertenece o está asignado el formulario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	20/04/2009	Created    
    ''' </history>
    Public Function isDynamicForm(ByVal dynamicFormId As Int32, ByVal DocTypeId As Int64) As Boolean
        Return (FF.isDynamicForm(dynamicFormId, DocTypeId))
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' retorna todos los formularios pertenecientes a un entidad que 
    ''' sean de un tipo de formulario determinado.
    ''' </summary>
    ''' <param name="DocTypeId">Tipo de Docuemento</param>
    ''' <param name="FormType">Tipo de Formulario</param>
    ''' <returns>En caso a firmativo retorna un conjunto de formularios, de lo 
    '''          contrario retorna nothing.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetForms(ByVal DocTypeId As Int64, ByVal FormType As FormTypes) As ZwebForm()
        Return FF.GetForms(DocTypeId, FormType)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los formularios de una etapa de WorkFlow.
    ''' </summary>
    ''' <param name="WfStepId">Etapa de WorkFlow</param>
    ''' <returns>En caso afirmativo retorna un conjunto de formularios, de lo contrario retorna nothing.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetWFForms(ByVal WfStepId As Int32) As ZwebForm()
        Return FF.GetWFForms(WfStepId)
    End Function

    Public Function GetWFForms(ByVal DocTypeId As Int32, ByVal WfStepId As Int32, ByVal FormType As FormTypes) As ZwebForm()
        Return FF.GetWFForms(DocTypeId, WfStepId, FormType)
    End Function

    Public Function GetForm(ByVal formID As Int64) As ZwebForm

        If Cache.DocTypesAndIndexs.hsForms.ContainsKey(formID) = False Then
            Dim f As ZwebForm = FF.GetForm(formID)
            SyncLock (Cache.DocTypesAndIndexs.hsForms)
                If Cache.DocTypesAndIndexs.hsForms.ContainsKey(formID) = False Then
                    Cache.DocTypesAndIndexs.hsForms.Add(formID, f)
                End If
            End SyncLock
        End If
        Return Cache.DocTypesAndIndexs.hsForms.Item(formID)
    End Function

    Public Function GetFormNewId() As Int32
        Return CoreData.GetNewID(IdTypes.ZWEBFORM)
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
    Public Sub DeleteForm(ByVal Form As ZwebForm)

        If (Form.Path = String.Empty) Then
            'Formulario dinamico
            DeleteDynamicFormsConfigs(Form.ID)
        End If

        FF.DeleteForm(Form)
        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UB.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, Zamba.Core.RightsType.Delete, "Se elimino el formulario: " & Form.Name)

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
    Public Sub InsertForm(ByVal Form As ZwebForm)
        FF.InsertForm(Form)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UB.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.insert, "Se agrego el formulario electronico: " & Form.Name)
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
    Public Sub UpdateForm(ByVal Form As ZwebForm)
        FF.UpdateForm(Form)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UB.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.Edit, "Se modifico el formulario electronico: " & Form.Name)
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
    Public Sub AsignForm2WfStep(ByVal Form As ZwebForm, ByVal WfStepId As Int32)
        FF.AsignForm2WfStep(Form, WfStepId)
        Dim name As String = WFStepBusiness.GetStepNameById(WfStepId)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UB.SaveAction(Form.ID, ObjectTypes.FormulariosElectronicos, RightsType.Asign, "Formulario asignado al Workflow: " & name)
    End Sub

    Public Sub CreateAutoForm(ByVal DocType As DocType, ByVal Indexs As Index(), ByVal File As String)
        FF.CreateAutoForm(DocType, Indexs, File)
    End Sub



#Region "InsertVirtualForm"
    Public Function CreateVirtualDocument(ByVal docTypeId As Long) As NewResult
        Dim InsertedResult As New NewResult()
        Dim DTB As New DocTypesBusiness
        InsertedResult.DocType = DTB.GetDocType(docTypeId)
        InsertedResult.Indexs = New IndexsBusiness().GetIndexsData(InsertedResult.ID, InsertedResult.DocTypeId)
        DTB = Nothing
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

        FF.AddRequierement(formId, requirementPath)
    End Sub
    ''' <summary>
    ''' Remueve un archivo requerido del formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <param name="requirementPath"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Sub RemoveRequirement(ByVal formId As Int64, ByVal requirementPath As String)
        FF.RemoveRequirement(formId, requirementPath)
    End Sub
    ''' <summary>
    ''' Añade todos los archivos requerido del formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Sub ClearRequirements(ByVal formId As Int64)
        FF.ClearRequirements(formId)
    End Sub

    ''' <summary>
    ''' [sebastian 06-02-2009]Devuelve un data set con la info para completar el formulario dinamico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDynamicFormValues() As DataSet
        Return FF.GetDynamicFormValues()
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
    Public Function GetDynamicForm(ByVal dynamicFormId As Integer) As DataSet
        Return (FF.GetDynamicForm(dynamicFormId))
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
    Public Function GetDynamicFormFromRuleGenerateDinamicForm(ByRef dynamicFormId As Integer) As DataSet
        Return (FF.GetDynamicFormFromRuleGenerateDinamicForm(dynamicFormId))
    End Function
    ''' <summary>
    ''' [sebastian 10-02-2009]
    ''' Obtiene el nombre de la seccion para luego ser utilizado como titulo de la tabla en el formulario
    ''' dinamico
    ''' </summary>
    ''' <param name="SectionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDynamicFormSectionName(ByVal SectionId As String) As String
        Return FF.GetDynamicFormSectionName(SectionId)
    End Function

    Public Sub SaveZFrmDescValues(ByRef QueryValues As Hashtable)
        FF.SaveZFrmDescValues(QueryValues)
    End Sub

    Public Sub SaveZFrmDynamicForms(ByRef QueryValues As Generic.List(Of Hashtable))
        FF.SaveZFrmDynamicForms(QueryValues)
    End Sub

    Public Sub SaveSectionValues(ByVal Id As Int32, ByVal SectionName As String)
        SaveSectionValues(Id, SectionName)
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
    Public Function getIndexsOfDynamicFormId(ByVal dynamicFormId As Integer) As DataSet
        Return (FF.getIndexsOfDynamicFormId(dynamicFormId))
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
    Public Function getIndexsFromFrmsIndexsDesc(ByVal fId As Integer) As DataSet
        Return (FF.getIndexsFromFrmsIndexsDesc(fId))
    End Function

    ''' <summary>
    ''' Método que sirve para insertar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="fId">Id de formulario</param>
    ''' <param name="iId">Id de un índice</param>
    ''' <param name="type">Elemento de enumerador</param>
    ''' <param name="value">0,1 o id de un índice</param>
    ''' <param name="eId">Identificador único</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Function insertFrmIndexsDesc(ByVal fId As Integer, ByVal iId As Integer, ByVal type As Short, ByVal value As String) As Int32
        Dim eId As Integer = CoreBusiness.GetNewID(IdTypes.formsSpecification)
        FF.insertFrmIndexsDesc(fId, iId, type, value, eId)
        Return (eId)
    End Function

    ''' <summary>
    ''' Método que sirve para actualizar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="fId">Id de formulario</param>
    ''' <param name="iId">Id de un índice</param>
    ''' <param name="type">Elemento de enumerador</param>
    ''' <param name="value">0,1 o id de un índice</param>
    ''' <param name="eId">Identificador único</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    26/02/2009  Created
    ''' </history>
    Public Sub updateFrmIndexsDesc(ByVal fId As Integer, ByVal iId As Integer, ByVal type As Short, ByVal value As String, ByVal eId As Integer)
        FF.updateFrmIndexsDesc(fId, iId, type, value, eId)
    End Sub

    ''' <summary>
    ''' Método que sirve para eliminar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="eId">Identificador único</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    27/02/2009  Created
    ''' </history>
    Public Sub deleteFrmIndexsDesc(ByVal eId As Integer)
        FF.deleteFrmIndexsDesc(eId)
    End Sub

    ''' <summary>
    ''' Se obtienen los atributos obligatorios (requeridos) que posee el formulario dinámico
    ''' </summary>
    ''' <param name="frmId">Id de formulario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    11/03/2009  Created
    ''' </history>
    Public Function getRequiredIndexs(ByVal frmId As Int64) As DataSet
        Return (FF.getRequiredIndexs(frmId))
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
    Public Function getExceptuableIndexs(ByVal frmId As Int64) As DataSet
        Return (FF.getExceptuableIndexs(frmId))
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
    Public Function getDataExceptuableIndexs(ByVal frmId As Int64) As DataSet
        Return (FF.getDataExceptuableIndexs(frmId))
    End Function

    ''' <summary>
    ''' Se obtienen los valores de la propiedad "Sólo Lectura" y "Visible" que posea el índice del formulario dinámico
    ''' </summary>
    ''' <param name="frmId">Id de un formulario dinámico</param>
    ''' <param name="iId">Id de un índice que pertenece al formulario dinámico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    13/03/2009  Created
    ''' </history>
    Public Function getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(ByVal frmId As Int64, ByVal iId As Int32) As DataSet
        Return (FF.getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(frmId, iId))
    End Function

    ''' <summary>
    ''' Método utilizado para guardar las condiciones que se aplican sobre un formulario normal (formulario almacenado en el servidor)
    ''' </summary>
    ''' <param name="state">Instancia que contiene datos del formulario normal</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    05/05/2009  Created
    ''' </history>
    Public Sub updateNormalForm(ByRef state As DynamicFormState)

        ' Se eliminan las condiciones del formulario normal
        FF.deleteNormalFormsConfigs(state.Formid)

        ' Se guardan las condiciones del formulario normal
        For Each Item As Hashtable In state.ConditionsFormValues
            SaveZFrmDescValues(Item)
        Next

    End Sub

    ''' <summary>
    ''' Actualiza un Dynamic form, lo elimina y lo vuelve a crear
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 31.3.2009 created</history>
    Public Sub UpdateDynamicForm(ByRef state As DynamicFormState)

        'borra el formulario
        FF.DeleteDynamicFormsConfigs(state.Formid)
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
    Public Sub generateDynamicFormId(ByRef state As DynamicFormState)

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
    Public Function SaveDynamicForm(ByRef state As DynamicFormState) As Boolean
        Try
            'If Not state.Edit Then
            '    ' Se genera un id para el formulario dinámico
            '    Dim formid As Int64 = ToolsBusiness.GetNewID(IdTypes.ZWEBFORM)
            '    state.UpdateFormId(formid)
            'End If
            'Guarda los datos de los forms
            SaveZFrmDynamicForms(state.DynamicFormValues)

            'guarda las condiciones
            For Each Item As Hashtable In state.ConditionsFormValues
                SaveZFrmDescValues(Item)
            Next

            'Guarda las propiedades de atributos
            For Each Items As String() In state.IndexsPropertiesFormValues
                insertFrmIndexsDesc(Convert.ToInt32(Items(0)),
                                                 Convert.ToInt32(Items(1)),
                                                 Convert.ToInt16(Items(3)),
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
    Public Sub GetDynamicFormState(ByRef state As DynamicFormState)

        'get conditions
        Dim item As New Hashtable
        Dim ds As DataSet

        ds = FF.GetDynamicFormIndexsConditions(state.Formid)

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
        ds = GetDynamicForm(state.Formid)

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
                    {state.Formid, r.Item("iId"),
                     IndexsBusiness.GetIndexName(r.Item("iId")),
                     r.Item("Type"), GetDynamicFormIndexProperty(r.Item("Type")),
                     r.Item("EId"), r.Item("value")}

            state.IndexsPropertiesFormValues.Add(dgvRow)
        Next

    End Sub
    ''' <summary>
    ''' Retorna las condiciones de aplicadas sobre indices en los formularios dinamicos
    ''' </summary>
    ''' <param name="formid">Id del formulario</param>
    ''' <returns></returns>
    ''' <history>[pablo] 10.10.2010 created</history>
    Public Function GetDynamicFormIndexsConditions(ByVal FormId As Int64) As DataSet
        Return FF.GetDynamicFormIndexsConditions(FormId)
    End Function

    ''' <summary>
    ''' Retorna un nombre de tipo en base a un id
    ''' </summary>
    ''' <param name="idtype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 01.04.2009 created</history>
    Private Function GetDynamicFormIndexProperty(ByVal idtype As Int32) As String
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
    ''' Actualiza la fecha de modificacion del formulario
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks></remarks>
    Public Sub UpdateModifiedDate(ByVal formId As Int32)
        FF.UpdateModifiedDate(formId)
    End Sub

    Public Sub CopyWebForm(ByVal form As IZwebForm, ByVal tempPath As String)
        'Verifica la existencia del formulario
        If File.Exists(tempPath) Then
            'Si esta, verifica si debe actualizarlo
            Dim localDate As DateTime = File.GetLastWriteTime(tempPath)

            'Si la fecha del servidor es mayor que la local, se actualiza el form
            If DateTime.Compare(form.ModifiedTime, localDate) > 0 Then
                CopyForm(form.Path, tempPath)
            End If
        Else
            'Si no esta, lo copia del servidor
            CopyForm(form.Path, tempPath)
        End If
    End Sub

    Public Sub CopyForm(ByVal sourcePath As String, ByVal copyPath As String)
        'Si no esta el archivo lo copia
        If Directory.Exists(sourcePath) Then
            Directory.CreateDirectory(sourcePath)
        End If
        Try
            If File.Exists(sourcePath) Then
                File.Copy(sourcePath, copyPath, True)
            End If
        Catch ex As Exception
            'Si el archivo estaba como solo lectura intenta quitarle el atributo y copiarlo
            Try
                If File.GetAttributes(copyPath).ToString().Contains("ReadOnly") Then
                    File.SetAttributes(copyPath, FileAttributes.Normal)
                    File.Copy(sourcePath, copyPath, True)
                Else
                    Throw
                End If
            Catch ex2 As Exception
                Dim ex3 As New Exception("Error al copiar el formulario: " & sourcePath & " en la ruta: " & copyPath, ex2)
                Throw ex3
            End Try
        End Try
    End Sub



    ''' <summary>
    ''' Obtiene una lista de condiciones de un formulario
    ''' </summary>
    ''' <remarks></remarks>
    Function GetFormConditions(ByVal formID As Long) As List(Of IZFormCondition)
        If Not Cache.Forms.FormsConditions.ContainsKey(formID) Then
            Dim forms As List(Of IZFormCondition) = FF.GetFormConditions(formID)
            SyncLock Cache.Forms.FormsConditions
                If Not Cache.Forms.FormsConditions.ContainsKey(formID) Then
                    Cache.Forms.FormsConditions.Add(formID, forms)
                End If
            End SyncLock
        End If

        Return Cache.Forms.FormsConditions(formID)

    End Function

    ''' <summary>
    ''' Obtiene una condicion por su id
    ''' </summary>
    ''' <remarks></remarks>
    Function GetCondition(ByVal optionID As Long) As IZFormCondition
        Return FF.GetCondition(optionID)
    End Function

    ''' <summary>
    ''' Agrega una ciondicion a un formulario
    ''' </summary>
    ''' <remarks></remarks>
    Sub AddCondition(ByVal formID As Long, ByVal indexSource As Int64, ByVal comparator As Comparators,
                            ByVal value As String, ByVal indexTarget As Int64, ByVal action As FormActions)

        FF.AddCondition(formID, indexSource, comparator, value, indexTarget, action)
    End Sub

    ''' <summary>
    ''' Edita una condicion
    ''' </summary>
    ''' <remarks></remarks>
    Sub EditCondition(ByVal id As Int64, ByVal indexSource As Int64,
                             ByVal comparator As Comparators, ByVal value As String, ByVal indexTarget As Int64, ByVal action As FormActions)

        FF.EditCondition(id, indexSource, comparator, value, indexTarget, action)
    End Sub

    ''' <summary>
    ''' Borra una condicon por su id
    ''' </summary>
    ''' <remarks></remarks>
    Sub DeleteCondition(ByVal ConditionID As Long)
        FF.DeleteCondition(ConditionID)
    End Sub

    Public Sub ClearHashTables()
        If Not IsNothing(Cache.DocTypesAndIndexs.hsForms) Then
            Cache.DocTypesAndIndexs.hsForms.Clear()
            Cache.DocTypesAndIndexs.hsForms = Nothing
            Cache.DocTypesAndIndexs.hsForms = New SynchronizedHashtable()
        End If
    End Sub

    Private Function GetDropDownOptions(prevTag As String, arrOptions As ArrayList, indexValue As String) As String
        Dim selected As Boolean = False
        Dim sb As New StringBuilder(prevTag)

        For Each s As String In arrOptions
            If indexValue = s.Trim Then
                selected = True
                sb.AppendFormat(OPTION_FORMAT_SELECTED, s, s)
            Else
                sb.AppendFormat(OPTION_FORMAT_NON_SELECTED, s, s)
            End If
        Next
        If Not sb.ToString().Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) Then
            If selected Then
                sb.AppendFormat(OPTION_FORMAT_NON_SELECTED, String.Empty, "A Definir")
            Else
                sb.AppendFormat(OPTION_FORMAT_SELECTED, String.Empty, "A Definir")
            End If
        End If

        Return sb.ToString()
    End Function

    Private Function GetAutoSustOptions(prevTag As String, dtSource As DataTable, indexValue As String, valueMember As String, displayMember As String) As String
        Dim selected As Boolean = False
        Dim sb As New StringBuilder(prevTag)
        Dim value As String
        Dim displayText As String

        For Each r As DataRow In dtSource.Rows
            value = r(valueMember).ToString.Trim
            displayText = r(displayMember).ToString.Trim

            If indexValue = value Then
                selected = True
                sb.AppendFormat(OPTION_FORMAT_SELECTED, value, displayText)
            Else
                sb.AppendFormat(OPTION_FORMAT_NON_SELECTED, value, displayText)
            End If
        Next
        If Not sb.ToString().Contains("<option value=" & Chr(34) & String.Empty & Chr(34)) Then
            If selected Then
                sb.AppendFormat(OPTION_FORMAT_NON_SELECTED, String.Empty, "A Definir")
            Else
                sb.AppendFormat(OPTION_FORMAT_SELECTED, String.Empty, "A Definir")
            End If
        End If

        Return sb.ToString()
    End Function

    Public Sub SetFormBuilded(ByVal formId As Long)
        FF.SetFormBuilded(formId)
    End Sub
End Class