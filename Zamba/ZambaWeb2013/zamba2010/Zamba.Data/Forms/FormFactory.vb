'Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Servers
Imports System.Text
Imports System.Collections.Generic

Public NotInheritable Class FormFactory
    Inherits ZClass

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los formularios 
    ''' </summary>
    ''' <returns>arrayList de objetos ZwebForm</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	    14/08/2006	Created
    '''     [Gaston]    12/06/2008  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetForms() As ArrayList

        Dim wforms As New ArrayList
        Dim Dstemp As DataSet

        Try

            Dim query As New System.Text.StringBuilder

            If (Server.isOracle) Then

                query.Append("SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description,ZFrms.UseRuleRights, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id ")
                query.Append("FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id ")

            Else

                query.Append("Select * from Zvw_ZViewForms_300")

            End If

            'Strselect = "Select * from zviewforms"
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())

            Dim form As ZwebForm = Nothing
            Dim r As DataRow = Nothing
            Dim rE As DataRow = Nothing

            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1

                form = New ZwebForm
                r = Dstemp.Tables(0).Rows(i)

                Try

                    If Not IsDBNull(r.Item(7)) Then form.ID = r.Item(7)
                    If Not IsDBNull(r.Item(0)) Then form.Name = r.Item(0)
                    If Not IsDBNull(r.Item(5)) Then form.Description = r.Item(5)
                    If Not IsDBNull(r.Item(2)) Then form.Type = r.Item(2)
                    If Not IsDBNull(r.Item(3)) Then form.ObjectTypeId = r.Item(3)
                    If Not IsDBNull(r.Item(4)) Then form.Path = r.Item(4)
                    If Not IsDBNull(r.Item(8)) Then form.DocTypeId = r.Item(8)
                    If Not IsDBNull(r.Item(6)) Then form.useRuleRights = r.Item(6)
                    If Not IsDBNull(r.Item(9)) Then form.ModifiedTime = r.Item(9)

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                wforms.Add(form)

            Next

            Return wforms

        Catch ex As Exception
            ZClass.raiseerror(ex)

            Return Nothing

        End Try

    End Function
    Public Function RepairHtml(ByVal Html As String) As String

        Dim CorrectHtml As String = Html

        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&eacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&aacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&iacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&oacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&uacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Aacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Eacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Iacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Oacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Uacute;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&uuml;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Uuml;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&ntilde;")
        If Html.ToLower.Contains("�") = True Then CorrectHtml = CorrectHtml.ToLower.Replace("�", "&Ntilde;")

        Return CorrectHtml
    End Function
    ''' <summary>
    ''' M�todo utilizado para obtener los datos de un form en base a su id
    ''' </summary>
    ''' <param name="formID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    12/06/2008  Modified    Se agrego consulta en el c�digo fuente para Oracle
    ''' </history>
    Public Function GetForm(ByVal formID As Int64) As ZwebForm

        Dim tmpZWebForm As ZwebForm = Nothing
        Dim Dstemp As DataSet

        Try

            Dim query As New System.Text.StringBuilder

            If (Server.isOracle) Then

                query.Append("SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, ZFrms.USERULERIGHTS, Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id ")
                query.Append("FROM ZFrms INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id ")
                query.Append("where Form_Id = " & formID)

            Else

                query.Append("Select * from Zvw_ZViewForms_600 where id = " & formID)

            End If

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
            Dstemp.Tables(0).Columns(6).SetOrdinal(8)

            If Not ((IsNothing(Dstemp)) AndAlso (Dstemp.Tables.Count > 0) AndAlso (Dstemp.Tables(0).Rows.Count > 0)) Then

                For Each r As DataRow In Dstemp.Tables(0).Rows

                    tmpZWebForm = New ZwebForm

                    Try
                        If Not IsDBNull(r.Item(6)) Then tmpZWebForm.ID = r.Item(6)
                        If Not IsDBNull(r.Item(0)) Then tmpZWebForm.Name = r.Item(0)
                        If Not IsDBNull(r.Item(5)) Then tmpZWebForm.Description = r.Item(5)
                        If Not IsDBNull(r.Item(2)) Then tmpZWebForm.Type = r.Item(2)
                        If Not IsDBNull(r.Item(3)) Then tmpZWebForm.ObjectTypeId = r.Item(3)
                        If Not IsDBNull(r.Item(4)) Then tmpZWebForm.Path = r.Item(4)
                        If Not IsDBNull(r.Item(7)) Then tmpZWebForm.DocTypeId = r.Item(7)
                        If Not IsDBNull(r.Item(8)) Then tmpZWebForm.useRuleRights = r.Item(8)
                        If r.ItemArray.Length > 9 AndAlso Not IsDBNull(r.Item(9)) Then tmpZWebForm.ModifiedTime = r.Item(9)
                        If r.ItemArray.Length > 11 AndAlso Not IsDBNull(r.Item(11)) Then
                            tmpZWebForm.ReBuild = r.Item(11) = 1
                        Else
                            tmpZWebForm.ReBuild = False
                        End If

                    Catch ex2 As Exception
                        ZClass.raiseerror(ex2)
                    End Try

                Next

            End If

            Return tmpZWebForm

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' Obtiene un registro de la zfrms con los datos de un formulario
    ''' </summary>
    ''' <param name="Form">Id de Formulario</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	(pablo)	01/04/2011	Created
    ''' </history>
    Public Function GetFormFromZfrms(ByVal FormId As Int32) As DataSet
        Try
            Dim Strselect As String = "Select * from zfrms where id = " & FormId
            Return Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetFormsByType(ByVal Formtype As FormTypes) As ArrayList
        Dim ArrayZwebForm As New ArrayList
        Dim Dstemp As DataSet = Nothing

        Try
            Dim Strselect As String = "Select * from Zvw_ZViewForms_100 where Type = " & Integer.Parse(Formtype).ToString()

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

            Dim CurrentZWebForm As ZwebForm = Nothing
            Dim CurrentDataRow As DataRow = Nothing
            Dim rE As DataRow = Nothing
            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                CurrentZWebForm = New ZwebForm
                CurrentDataRow = Dstemp.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(CurrentDataRow.Item(6)) Then CurrentZWebForm.ID = CurrentDataRow.Item(6)
                    If Not IsDBNull(CurrentDataRow.Item(0)) Then CurrentZWebForm.Name = CurrentDataRow.Item(0)
                    If Not IsDBNull(CurrentDataRow.Item(5)) Then CurrentZWebForm.Description = CurrentDataRow.Item(5)
                    If Not IsDBNull(CurrentDataRow.Item(2)) Then CurrentZWebForm.Type = CurrentDataRow.Item(2)
                    If Not IsDBNull(CurrentDataRow.Item(3)) Then CurrentZWebForm.ObjectTypeId = CurrentDataRow.Item(3)
                    If Not IsDBNull(CurrentDataRow.Item(4)) Then CurrentZWebForm.Path = CurrentDataRow.Item(4)
                    If Not IsDBNull(CurrentDataRow.Item(7)) Then CurrentZWebForm.DocTypeId = CurrentDataRow.Item(7)


                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                ArrayZwebForm.Add(CurrentZWebForm)
            Next

            Return ArrayZwebForm
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetInsertFormsByDocTypeId(ByVal DocTypeId As Int32) As ZwebForm()
        Dim Dstemp As DataSet
        Try
            'Strselect = "Select * from zviewforms where (type=1 or type=2) and doctype_id = " & DocTypeId
            Dim Strselect As String = "Select * from Zvw_ZViewForms_300 where (type=4 or type=6) and doctype_id = " & DocTypeId
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

            If Dstemp.Tables(0).Rows.Count < 1 Then Return Nothing


            Dim Forms(Dstemp.Tables(0).Rows.Count - 1) As ZwebForm
            Dim form As ZwebForm
            Dim r As DataRow
            Dim rE As DataRow
            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                form = New ZwebForm
                r = Dstemp.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(r.Item(7)) Then form.ID = r.Item(7)
                    If Not IsDBNull(r.Item(0)) Then form.Name = r.Item(0)
                    If Not IsDBNull(r.Item(5)) Then form.Description = r.Item(5)
                    If Not IsDBNull(r.Item(2)) Then form.Type = r.Item(2)
                    If Not IsDBNull(r.Item(3)) Then form.ObjectTypeId = r.Item(3)
                    If Not IsDBNull(r.Item(4)) Then form.Path = r.Item(4)
                    If Not IsDBNull(r.Item(8)) Then form.DocTypeId = r.Item(8)
                    If Not IsDBNull(r.Item(6)) Then form.useRuleRights = r.Item(6)
                    If Not IsDBNull(r.Item(9)) Then form.ModifiedTime = r.Item(9)


                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Forms.SetValue(form, i)
            Next
            Return Forms
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetVirtualDocumentsByRightsOfView(ByVal Formtype As FormTypes, ByVal userid As Int64, ByVal doctypesByRights As Generic.List(Of Int64)) As ArrayList
        Dim ArrayZwebForm As New ArrayList
        Dim Dstemp As DataSet = Nothing

        Dim OnlyOnce As Boolean = False
        Dim count As Int32
        Try
            Dim Strselect As String = "Select * from Zvw_ZViewForms_100 where Type = " & Integer.Parse(Formtype).ToString()
            For Each dtid As Int64 In doctypesByRights
                count += 1
                If OnlyOnce = False Then
                    OnlyOnce = True
                    Strselect += " AND ( DocType_Id = " & dtid.ToString
                    If count = doctypesByRights.Count Then
                        Strselect += " )"
                    End If
                Else
                    Strselect += " OR DocType_Id = " & dtid.ToString
                    If count = doctypesByRights.Count Then
                        Strselect += " )"
                    End If
                End If
            Next

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)


            Dim CurrentZWebForm As ZwebForm = Nothing
            Dim CurrentDataRow As DataRow = Nothing
            Dim rE As DataRow = Nothing
            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                CurrentZWebForm = New ZwebForm
                CurrentDataRow = Dstemp.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(CurrentDataRow.Item(6)) Then CurrentZWebForm.ID = CurrentDataRow.Item(6)
                    If Not IsDBNull(CurrentDataRow.Item(0)) Then CurrentZWebForm.Name = CurrentDataRow.Item(0)
                    If Not IsDBNull(CurrentDataRow.Item(5)) Then CurrentZWebForm.Description = CurrentDataRow.Item(5)
                    If Not IsDBNull(CurrentDataRow.Item(2)) Then CurrentZWebForm.Type = CurrentDataRow.Item(2)
                    If Not IsDBNull(CurrentDataRow.Item(3)) Then CurrentZWebForm.ObjectTypeId = CurrentDataRow.Item(3)
                    If Not IsDBNull(CurrentDataRow.Item(4)) Then CurrentZWebForm.Path = CurrentDataRow.Item(4)
                    If Not IsDBNull(CurrentDataRow.Item(7)) Then CurrentZWebForm.DocTypeId = CurrentDataRow.Item(7)


                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                ArrayZwebForm.Add(CurrentZWebForm)
            Next

            Return ArrayZwebForm
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' M�todo que muestra todos los forms(Show, Edit, Workflow, etc)
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	17/07/2008  Created     El c�digo se tomo del m�todo GetShowAndEditForms. S�lo que se 
    '''                                         quitaron los type en la condici�n de la consulta
    ''' </history>
    Public Function GetAllForms(ByVal DocTypeId As Int64) As ZwebForm()

        Dim Strselect As String
        Dim Dstemp As DataSet


        Try

            'Strselect = "Select * from zviewforms where (type=1 or type=2) and doctype_id = " & DocTypeId
            Strselect = "Select * from Zvw_ZViewForms_100 where doctype_id = " & DocTypeId
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

            If Dstemp.Tables(0).Rows.Count < 1 Then Return Nothing


            Dim Forms(Dstemp.Tables(0).Rows.Count - 1) As ZwebForm
            Dim form As ZwebForm
            Dim r As DataRow
            Dim rE As DataRow
            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                form = New ZwebForm
                r = Dstemp.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(r.Item(6)) Then form.ID = r.Item(6)
                    If Not IsDBNull(r.Item(0)) Then form.Name = r.Item(0)
                    If Not IsDBNull(r.Item(5)) Then form.Description = r.Item(5)
                    If Not IsDBNull(r.Item(2)) Then form.Type = r.Item(2)
                    If Not IsDBNull(r.Item(3)) Then form.ObjectTypeId = r.Item(3)
                    If Not IsDBNull(r.Item(4)) Then form.Path = r.Item(4)
                    If Not IsDBNull(r.Item(7)) Then form.DocTypeId = r.Item(7)


                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Forms.SetValue(form, i)
            Next
            Return Forms

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing

        End Try

    End Function


    ''' <summary>
    ''' Obtiene los formularios de tipo edicion y visualizacion
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>   Pablo   Created 01/11/2010</history>
    Public Function GetShowAndEditForms(ByVal DocTypeId As Int64) As ZwebForm()
        Dim Strselect As String
        Dim Dstemp As DataSet

        Try
            'Strselect = "Select * from zviewforms where (type=1 or type=2) and doctype_id = " & DocTypeId
            Strselect = "Select * from Zvw_ZViewForms_300 where (type=1 or type=2 or type= 8 or type=9) and doctype_id = " & DocTypeId
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

            If Dstemp.Tables(0).Rows.Count < 1 Then Return Nothing


            Dim Forms(Dstemp.Tables(0).Rows.Count - 1) As ZwebForm
            Dim form As ZwebForm
            Dim r As DataRow
            Dim rE As DataRow
            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                form = New ZwebForm
                r = Dstemp.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(r.Item(7)) Then form.ID = r.Item(7)
                    If Not IsDBNull(r.Item(0)) Then form.Name = r.Item(0)
                    If Not IsDBNull(r.Item(5)) Then form.Description = r.Item(5)
                    If Not IsDBNull(r.Item(2)) Then form.Type = r.Item(2)
                    If Not IsDBNull(r.Item(3)) Then form.ObjectTypeId = r.Item(3)
                    If Not IsDBNull(r.Item(4)) Then form.Path = r.Item(4)
                    If Not IsDBNull(r.Item(8)) Then form.DocTypeId = r.Item(8)
                    If Not IsDBNull(r.Item(6)) Then form.useRuleRights = r.Item(6)
                    If Not IsDBNull(r.Item(9)) Then form.ModifiedTime = r.Item(9)


                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Forms.SetValue(form, i)
            Next
            Return Forms
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetShowAndEditFormsCount(ByVal DocTypeId As Int32) As Int32
        Dim Strselect As String
        Strselect = "Select count(1) from Zvw_ZViewForms_100 where type in (1,2,8,9) and doctype_id = " & DocTypeId
        Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
    End Function

    ''' <summary>
    ''' M�todo que sirve para comprobar si el formulario es en verdad un formulario din�mico
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario (supuestamente din�mico)</param>
    ''' <param name="DocTypeId">Tipo de documento al que pertenece o est� asignado el formulario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	20/04/2009	Created    
    ''' </history>
    Public Function isDynamicForm(ByVal dynamicFormId As Int32, ByVal DocTypeId As Int64) As Boolean

        Dim query As String = "SELECT count(1) FROM Ztype_Zfrms Where DocType_id = " & DocTypeId & " AND " & "Form_Id = " & dynamicFormId

        Dim counter As Short = Server.Con.ExecuteScalar(CommandType.Text, query)

        If (counter > 0) Then

            Dim _query As New StringBuilder

            _query.Append("SELECT count(1) ")
            _query.Append("FROM ZFrms ")
            _query.Append("Where Id = " & dynamicFormId & " AND Type = " & FormTypes.Insert)

            counter = Server.Con.ExecuteScalar(CommandType.Text, _query.ToString())

            If (counter > 0) Then

                Dim _query2 As New StringBuilder

                _query2.Append("SELECT count(1) ")
                _query2.Append("FROM zFrmDynamicForms ")
                _query2.Append("Where IdFormulario = " & dynamicFormId)

                counter = Server.Con.ExecuteScalar(CommandType.Text, _query2.ToString())

                If (counter > 0) Then
                    Return (True)
                End If

            End If

        End If

        Return (False)

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
        Dim Strselect As String
        Dim Dstemp As DataSet

        Try
            'Strselect = "Select * from zviewforms where doctype_id = " & DocTypeId & " and Type = " & CInt(FormType)
            If FormType = FormTypes.All Then
                Strselect = "Select * from Zvw_ZViewForms_100 where doctype_id = " & DocTypeId
            Else
                Strselect = "Select * from Zvw_ZViewForms_100 where doctype_id = " & DocTypeId & " and Type = " & (FormType)
            End If
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)


            Dim Forms(Dstemp.Tables(0).Rows.Count - 1) As ZwebForm
            Dim form As ZwebForm
            Dim r As DataRow
            Dim rE As DataRow
            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                form = New ZwebForm
                r = Dstemp.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(r.Item(6)) Then form.ID = r.Item(6)
                    If Not IsDBNull(r.Item(0)) Then form.Name = r.Item(0)
                    If Not IsDBNull(r.Item(5)) Then form.Description = r.Item(5)
                    If Not IsDBNull(r.Item(2)) Then form.Type = r.Item(2)
                    If Not IsDBNull(r.Item(3)) Then form.ObjectTypeId = r.Item(3)
                    If Not IsDBNull(r.Item(4)) Then form.Path = r.Item(4)
                    If Not IsDBNull(r.Item(7)) Then form.DocTypeId = r.Item(7)


                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Forms.SetValue(form, i)
            Next
            Return Forms
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
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

        Try
            If Server.isOracle Then

                Dim Strselect As New System.Text.StringBuilder
                Strselect.Append("SELECT ZFrms.Name, ZFrms.ParentId, ZFrms.Type, ZFrms.ObjectTypeId, ZFrms.Path, ZFrms.Description, ")
                Strselect.Append(" Ztype_Zfrms.Form_Id, Ztype_Zfrms.DocType_Id, WF_Frms.Step_Id, WF_Frms.Sort, REBUILD FROM ZFrms ")
                Strselect.Append(" INNER JOIN Ztype_Zfrms ON ZFrms.Id = Ztype_Zfrms.Form_Id ")
                Strselect.Append(" INNER JOIN WF_Frms ON Ztype_Zfrms.Form_Id = WF_Frms.Form_Id")
                Strselect.Append(" WHERE WF_Frms.STEP_ID = ")
                Strselect.Append(WfStepId)
                Strselect.Append(" ORDER BY ZFrms.ParentId")

                Dim Dstemp As DataSet
                Dim DsForms As New DsForms
                Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect.ToString)
                DsForms.Merge(Dstemp)
                Dim Forms(DsForms.ZViewForms.Count - 1) As ZwebForm
                Dim i As Int32
                For i = 0 To DsForms.ZViewForms.Count - 1
                    Forms.SetValue(New ZwebForm(DsForms.ZViewForms(i).FORM_ID, DsForms.ZViewForms(i).NAME, DsForms.ZViewForms(i).DESCRIPTION, DsForms.ZViewForms(i).TYPE, DsForms.ZViewForms(i).OBJECTTYPEID, DsForms.ZViewForms(i).PATH, DsForms.ZViewForms(i).DOCTYPE_ID, False), i)
                Next
                Return Forms

            Else
                Dim Strselect As String = "Select * from Zvw_WFViewForms_100 where step_id = " & WfStepId & " order by parentid"
                Dim Dstemp As DataSet
                Dim DsForms As New DsForms
                Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
                DsForms.Merge(Dstemp)
                Dim Forms(DsForms.ZViewForms.Count - 1) As ZwebForm
                Dim i As Int32
                For i = 0 To DsForms.ZViewForms.Count - 1
                    Forms.SetValue(New ZwebForm(DsForms.ZViewForms(i).FORM_ID, DsForms.ZViewForms(i).NAME, DsForms.ZViewForms(i).DESCRIPTION, DsForms.ZViewForms(i).TYPE, DsForms.ZViewForms(i).OBJECTTYPEID, DsForms.ZViewForms(i).PATH, DsForms.ZViewForms(i).DOCTYPE_ID, False), i)
                Next
                Return Forms
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetWFForms(ByVal DocTypeId As Int32, ByVal WfStepId As Int32, ByVal FormType As FormTypes) As ZwebForm()
        Try
            'Dim Strselect As String = "Select * from wfviewforms where step_id = " & WfStepId & " and DocType_Id = " & DocTypeId & " and Type = " & CInt(FormType) & " order by parentid"
            Dim Strselect As String = "Select * from Zvw_WFViewForms_100 where step_id = " & WfStepId & " and DocType_Id = " & DocTypeId & " and Type = " & (FormType) & " order by parentid"
            Dim Dstemp As DataSet
            Dim DsForms As New DsForms
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
            DsForms.Merge(Dstemp)
            Dim Forms(DsForms.ZViewForms.Count - 1) As ZwebForm
            Dim i As Int32
            For i = 0 To DsForms.ZViewForms.Count - 1
                Forms.SetValue(New ZwebForm(DsForms.ZViewForms(i).FORM_ID, DsForms.ZViewForms(i).NAME, DsForms.ZViewForms(i).DESCRIPTION, DsForms.ZViewForms(i).TYPE, DsForms.ZViewForms(i).OBJECTTYPEID, DsForms.ZViewForms(i).PATH, DsForms.ZViewForms(i).DOCTYPE_ID, False), i)
            Next
            Return Forms
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
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
    '''     [Tomas] - 08/05/2009 - Modified - Se comenta la actualizaci�n de extensiones.
    ''' [Gaston]    14/05/2009    Modified  Primero se elimina de la tabla ztype_zfrms y luego de la tabla zfrms
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteForm(ByVal Form As ZwebForm)

        Try

            Dim Strdelete As String = "DELETE FROM ztype_zfrms where form_id = " & Form.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, Strdelete)
            Strdelete = "Delete from zfrms where id = " & Form.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, Strdelete)

            'Strdelete = "DELETE from Zext_zfrms where Zfrms_id = " & Form.ID
            'Server.Con.ExecuteNonQuery(CommandType.Text, Strdelete)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

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
    '''     [Tomas] - 08/05/2009 - Modified - Se comenta la actualizaci�n de extensiones.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertForm(ByVal Form As ZwebForm)
        Dim Con As IConnection
        Con = Server.Con(False, False)
        Try
            Dim Strinsert As String
            Try
                Strinsert = "INSERT INTO ZFRMS (Id,Name,ParentId,Type,Objecttypeid,Path,Description) VALUES (" & Form.ID & ",'" & Form.Name & "'," & Form.ParentId & "," & Form.Type & "," & Form.ObjectTypeId & ",'" & Form.Path & "','" & Form.Description & "')"
                Con.ExecuteNonQuery(CommandType.Text, Strinsert)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                'MsgBox(ex.ToString)
            End Try

            Try
                Strinsert = "INSERT INTO Ztype_zfrms (doctype_id,form_id) VALUES (" & Form.DocTypeId & "," & Form.ID & ")"
                Con.ExecuteNonQuery(CommandType.Text, Strinsert)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try


            'For i As Int32 = 0 To Form.Extensions.Count - 1
            '    Strinsert = "Insert into Zext_zfrms values(" & Form.ID.ToString & "," & Form.Extensions(i).ToString & ")"
            '    Con.ExecuteNonQuery(CommandType.Text, Strinsert)
            'Next
            'Con.ExecuteNonQuery(Trans, CommandType.Text, Strinsert)
            '        Trans.Commit()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            '       Trans.Rollback()
        Finally
            Con.Close()
            Con.dispose()
        End Try
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
    '''     [Tomas] - 08/05/2009 - Modified - Se comenta la actualizaci�n de extensiones.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateForm(ByVal Form As ZwebForm)
        Dim Strupdate, UseRlRights As String

        If Form.useRuleRights Then
            UseRlRights = 1
        Else
            UseRlRights = 0
        End If

        Strupdate = "UPDATE ZFRMS set Name ='" & Form.Name & "',ParentId = " & Form.ParentId & ",Type = " & Form.Type & ",Objecttypeid = " & Form.ObjectTypeId & ",Path = '" & Form.Path & "',Description = '" & Form.Description & "', useRuleRights= " & UseRlRights & " where id = " & Form.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Strupdate = "UPDATE Ztype_zfrms set doctype_id = " & Form.DocTypeId & " where form_id = " & Form.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)

        ''actualizo extensions
        'Strupdate = "DELETE from Zext_zfrms where zfrms_id = " & Form.ID
        'Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        'For i As Int32 = 0 To Form.Extensions.Count - 1
        '    Strupdate = "insert into Zext_zfrms values(" & Form.ID & "," & Form.Extensions(i) & ")"
        '    Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        'Next
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
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub AsignForm2WfStep(ByVal Form As ZwebForm, ByVal WfStepId As Int32)
        Dim Con As IConnection
        Con = Server.Con(False, False)
        Dim Trans As IDbTransaction
        Trans = Con.CN.BeginTransaction()
        Try
            Dim Strinsert As String
            Strinsert = "INSERT INTO WF_Frms (form_Id,step_id,sort) VALUES (" & Form.ID & "," & WfStepId & ",0)"
            Con.ExecuteNonQuery(Trans, CommandType.Text, Strinsert)
            Trans.Commit()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Trans.Rollback()
        Finally
            Con.Close()
            Con.dispose()
        End Try

    End Sub

    Public Shared Sub CreateAutoForm(ByVal DocType As DocType, ByVal Indexs As Index(), ByVal File As String)
        Dim SW As New IO.StreamWriter(File)
        SW.AutoFlush = False

        SW.WriteLine("<%@ Page Language=" & Chr(34) & "VB" & Chr(34) & " %>")
        SW.WriteLine()
        SW.WriteLine("<!DOCTYPE html PUBLIC " & Chr(34) & "-//W3C//DTD XHTML 1.0 Transitional//EN" & Chr(34) & " " & Chr(34) & "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" & Chr(34) & ">")
        SW.WriteLine()
        SW.WriteLine("<script runat=" & Chr(34) & "server" & Chr(34) & ">")
        SW.WriteLine()
        SW.WriteLine("</script>")
        SW.WriteLine()
        SW.WriteLine("<html xmlns=" & Chr(34) & "http://www.w3.org/1999/xhtml" & Chr(34) & " >")
        SW.WriteLine("<head runat=" & Chr(34) & "server" & Chr(34) & ">")
        SW.WriteLine()
        SW.WriteLine("<title>" & DocType.Name & "</title>")
        SW.WriteLine()
        SW.WriteLine("</head>")
        SW.WriteLine("<body>")
        SW.WriteLine()
        SW.WriteLine("<form id=" & Chr(34) & "Zamba_" & DocType.ID & Chr(34) & " runat=" & Chr(34) & "server" & Chr(34) & ">")
        SW.WriteLine()
        SW.WriteLine("<div>")
        SW.WriteLine()
        SW.WriteLine("<asp:Table ID=" & Chr(34) & "Table1" & Chr(34) & " runat=" & Chr(34) & "server" & Chr(34) & " Height=" & Chr(34) & "326px" & Chr(34) & " Width=" & Chr(34) & "425px" & Chr(34) & ">")
        SW.WriteLine()

        For Each I As Index In Indexs
            SW.WriteLine("<asp:TableRow runat=" & Chr(34) & "server" & Chr(34) & ">")
            SW.WriteLine("<asp:TableCell runat=" & Chr(34) & "server" & Chr(34) & ">")
            SW.WriteLine("<asp:Label ID=" & Chr(34) & "Zamba_Lbl" & I.ID & Chr(34) & " runat=" & Chr(34) & "server" & Chr(34) & " Text=" & Chr(34) & I.Name & Chr(34) & "></asp:Label>")
            SW.WriteLine("</asp:TableCell>")
            SW.WriteLine("<asp:TableCell runat=" & Chr(34) & "server" & Chr(34) & ">")
            SW.WriteLine("<asp:TextBox ID=" & Chr(34) & "Zamba_" & I.ID & Chr(34) & " runat=" & Chr(34) & "server" & Chr(34) & " Text=" & Chr(34) & I.Data & Chr(34) & "></asp:TextBox>")
            SW.WriteLine("</asp:TableCell>")
            SW.WriteLine("</asp:TableRow>")
            SW.WriteLine()
        Next
        SW.WriteLine("</asp:Table>")
        SW.WriteLine("<asp:Button ID=" & Chr(34) & "Zamba_Save" & Chr(34) & " runat=" & Chr(34) & "server" & Chr(34) & " Text=" & Chr(34) & "Guardar" & Chr(34) & " Width=" & Chr(34) & "102px" & Chr(34) & " />")
        SW.WriteLine("</div>")
        SW.WriteLine("</form>")
        SW.WriteLine()
        SW.WriteLine("</body>")
        SW.WriteLine("</html>")

        SW.Flush()
        SW.Close()
        SW = Nothing
    End Sub

#Region "InsertVirtualForm"
#End Region

    Public Overrides Sub Dispose()

    End Sub


    ''' <summary>
    ''' A�ade un archivo requerido al formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <param name="requirementPath"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Shared Sub AddRequierement(ByVal formId As Int64, ByVal requirementPath As String)
        Dim QueryBuilder As New StringBuilder
        QueryBuilder.Append("INSERT INTO ZFrmsRequirements(FrmId, Path) VALUES(")
        QueryBuilder.Append(formId.ToString())
        QueryBuilder.Append(",'")
        QueryBuilder.Append(requirementPath)
        QueryBuilder.Append("')")

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

    ''' <summary>
    ''' Remueve un archivo requerido del formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <param name="requirementPath"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Shared Sub RemoveRequirement(ByVal formId As Int64, ByVal requirementPath As String)
        Dim QueryBuilder As New StringBuilder
        QueryBuilder.Append("DELETE FROM ZFrmsRequirements WHERE FrmId=")
        QueryBuilder.Append(formId.ToString())
        QueryBuilder.Append("and path = '")
        QueryBuilder.Append(requirementPath)
        QueryBuilder.Append("'")

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

    ''' <summary>
    ''' A�ade todos los archivos requerido del formulario especificado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks>Andres [Create] 5/8/08</remarks>
    Public Shared Sub ClearRequirements(ByVal formId As Int64)
        Dim QueryBuilder As New StringBuilder
        QueryBuilder.Append("DELETE FROM ZFrmsRequirements WHERE FrmId=")
        QueryBuilder.Append(formId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

    Public Shared Sub SaveZFrmDynamicForms(ByRef QueryValues As Generic.List(Of Hashtable))
        Dim query As New StringBuilder()
        Try
            For Each FormConfig As Hashtable In QueryValues
                query.Remove(0, query.Length)
                query.Append("INSERT INTO zfrmdynamicforms (IdFormulario,IdIndice,IdSeccion,NroOrden,NroFila) values(")
                query.Append(Int32.Parse(FormConfig("QueryId")) & ",")
                query.Append(Int32.Parse(FormConfig("IndexId")) & ",")
                query.Append(Int32.Parse(FormConfig("SectionId")) & ",")
                query.Append(Int32.Parse(FormConfig("NroOrden")) & ",")
                query.Append(Int32.Parse(FormConfig("NroFila")) & ")")

                Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''' <summary>
    '''' [sebastian 24-02-2009] retorna el mayor valor del id de la tabla secciones
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetMaxSectionId() As Int32

    '    Dim query As New StringBuilder()
    '    Dim value As Int32

    '    query.Append("SELECT MAX(idseccion) from secciones")

    '    Try
    '        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, query.ToString)) = True Then
    '            value = 0
    '        Else
    '            Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
    '        End If

    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Function

    Public Shared Sub SaveZFrmDescValues(ByRef QueryValues As Hashtable)

        Dim Query As New StringBuilder()
        Dim QueryMaxQueryId As String = "SELECT MAX(qid) from zfrmdesc"
        Dim MaxQueryId As String
        'Dim QueryIfExist As New StringBuilder
        'Dim dsIfexist As New DataSet

        Try
            If Server.isOracle Then
                'QueryIfExist.Append("SELECT * FROM ZFrmDesc WHERE ")
                'QueryIfExist.Append("dtid=" & QueryValues("DocType") & " AND ")
                'QueryIfExist.Append("iid=" & QueryValues("Index") & " AND ")
                'QueryIfExist.Append("c_value=" & "'" & QueryValues("CompOperator") & "|" & QueryValues("Conexion") & "'" & " AND ")
                'QueryIfExist.Append("fid=" & "'" & QueryValues("Value") & "'" & " AND ")
                'QueryIfExist.Append("qid=" & QueryValues("FormId"))

                Query.Append("INSERT INTO ZFrmDesc (dtid,iid,op,c_value,fid,qid)")
            Else
                'QueryIfExist.Append("SELECT * FROM ZFrmDesc WHERE ")
                'QueryIfExist.Append("dtid=" & QueryValues("DocType") & " AND ")
                'QueryIfExist.Append("iid=" & QueryValues("Index") & " AND ")
                'QueryIfExist.Append("value=" & "'" & QueryValues("CompOperator") & "|" & QueryValues("Conexion") & "'" & " AND ")
                'QueryIfExist.Append("fid=" & "'" & QueryValues("Value") & "'" & " AND ")
                'QueryIfExist.Append("qid=" & QueryValues("FormId"))

                Query.Append("INSERT INTO ZFrmDesc (dtid,iid,op,value,fid,qid)")
            End If

            Query.Append("VALUES (")
            Query.Append(QueryValues("DocType") & ",")
            Query.Append(QueryValues("Index") & ",")
            Query.Append("'" & QueryValues("CompOperator") & "|" & QueryValues("Conexion") & "'" & ",")
            Query.Append("'" & QueryValues("Value") & "'" & ",")
            Query.Append(QueryValues("FormId") & ",")



            If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, QueryMaxQueryId)) = True Then
                MaxQueryId = 1
            Else
                MaxQueryId = DirectCast(Server.Con.ExecuteScalar(CommandType.Text, QueryMaxQueryId), Decimal) + 1
            End If

            Query.Append(MaxQueryId & ")")

            'dsIfexist = Server.Con.ExecuteDataset(CommandType.Text, QueryIfExist.ToString)

            'If dsIfexist.Tables(0).Rows.Count <= 0 Then
            Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString)
            'End If


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' [sebastian 10-02-2009] obtiene un dataset con los datos para generar un formulario dinamicamente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 13.04.2009 comento el order by porque es necesario que se recupere en el orden que se graba </history>
    Public Function GetDynamicFormValues() As DataSet
        Try
            Dim dsValues As New DataSet
            Dim query As New StringBuilder

            query.Append("select * from zFrmDynamicForms")
            query.Append(" inner join  doc_index on zFrmDynamicForms.idindice = doc_index.index_id")
            query.Append(" inner join ZSecciones on zFrmDynamicForms.idseccion = ZSecciones.idseccion")
            query.Append(" order by zFrmDynamicForms.NroOrden")
            dsValues = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

            Return dsValues

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener un formulario din�mico 
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario din�mico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    03/03/2009  Created
    '''     [dalbarellos] 13.04.2009 comento el order by porque es necesario que se recupere en el orden que se graba
    ''' </history>
    Public Function GetDynamicForm(ByRef dynamicFormId As Integer) As DataSet

        Dim dsDynamicForm As New DataSet
        Dim query As New StringBuilder

        query.Append("select * from zFrmDynamicForms ")
        query.Append("inner join  doc_index on zFrmDynamicForms.idindice = doc_index.index_id ")
        query.Append("inner join ZSecciones on zFrmDynamicForms.idseccion = ZSecciones.idseccion ")
        query.Append("Where (zFrmDynamicForms.IdFormulario = " & dynamicFormId & ") ")
        query.Append("order by zFrmDynamicForms.NroOrden")

        Try
            dsDynamicForm = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
            Return (dsDynamicForm)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener un formulario din�mico para el uso de la regla do generate dinamic form
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario din�mico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Sebastian]    10-09-2009 Created
    ''' </history>
    Public Function GetDynamicFormFromRuleGenerateDinamicForm(ByRef dynamicFormId As Integer) As DataSet

        Dim dsDynamicForm As New DataSet
        Dim query As New StringBuilder

        query.Append("select * from zFrmDynamicForms ")
        query.Append("inner join  doc_index on zFrmDynamicForms.idindice = doc_index.index_id ")
        query.Append("inner join ZSecciones on zFrmDynamicForms.idseccion = ZSecciones.idseccion ")
        query.Append("Where (zFrmDynamicForms.IdFormulario = " & dynamicFormId & ") ")
        query.Append("order by zFrmDynamicForms.NroOrden")

        Try
            dsDynamicForm = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
            Return (dsDynamicForm)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

    End Function
    ''' <summary>
    ''' [sebastian 10-02-2009] obtengo el nombre de la seccion para luego ser usada como titulo en el formulario dinamico
    ''' </summary>
    ''' <param name="SectionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDynamicFormSectionName(ByVal SectionId As String) As String

        Try
            Dim query As New StringBuilder
            query.Append("SELECT nombre from ZSecciones where idseccion=")
            query.Append(SectionId.ToString)

            Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function


    ''' <summary>
    ''' Obtiene las condiciones de atributos en formularios dinamicos
    ''' </summary>
    ''' <param name="formid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 01.04.2009 created</history>
    Public Function GetDynamicFormIndexsConditions(ByVal formid As Int64) As DataSet
        Dim query As New StringBuilder()

        If Server.isOracle Then
            query.Append("SELECT DTId, IId, Op, C_Value, FId, QId FROM ZFrmDesc ")

        Else
            query.Append("SELECT DTId, IId, Op, Value, FId, QId FROM ZFrmDesc ")

        End If
        'query.Append("SELECT DTId, IId, Op, Value, FId, QId FROM ZFrmDesc ")
        query.Append(" where fid = ")
        query.Append(formid)

        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
    End Function


    ''' <summary>
    ''' Obtiene todas las secciones para ser utilizadas formularios dinamicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDynamicFormSections() As DataSet

        Try
            Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZSecciones"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function


    ''' <summary>
    ''' crea una seccion para ser utilizados en formularios dinamicos
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <param name="sectionname"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 03.04.2009 created</history>
    Public Shared Sub InsertDynamicFormSection(ByVal sectionid As Int64, ByVal sectionname As String)
        Dim query As New StringBuilder
        query.Append("INSERT INTO ZSecciones(IdSeccion, nombre) ")
        query.Append(" VALUES(")
        query.Append(sectionid)
        query.Append(",'")
        query.Append(sectionname)
        query.Append("')")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' elimina uan seccion de un formularios dinamicos
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 03.04.2009 created</history>
    Public Shared Sub DeleteDynamicFormSection(ByVal sectionid As Int64)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZSecciones")
        query.Append(" WHERE IdSeccion = ")
        query.Append(sectionid)

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub


    ''' <summary>
    ''' Actualiza el nombre de una seccion segun id
    ''' </summary>
    ''' <param name="sectionid"></param>
    ''' <param name="newSectionName"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 06.04.2009</history>
    Public Shared Sub UpdateDynamicFormSection(ByVal sectionid As Int64, ByVal newSectionName As String)
        Dim query As New StringBuilder
        query.Append("UPDATE ZSecciones")
        query.Append(" SET nombre=")
        query.Append("'")
        query.Append(newSectionName)
        query.Append("'")
        query.Append(" WHERE IdSeccion=")
        query.Append(sectionid)

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub



    ''' <summary>
    ''' [Tomas 26-02-2009] Obtiene el nombre y el ID de los atributos ingresados en el formulario frmAbmZfrmDesc.vb
    ''' </summary>
    ''' <param name="SectionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDynamicFormIndexsNameAndId() As DataSet

        Dim query As New StringBuilder()

        query.Append("SELECT DISTINCT INDEX_ID, INDEX_NAME From DOC_INDEX")
        query.Append(" inner join ZFrmDesc on DOC_INDEX.INDEX_ID = ZFrmDesc.IId")
        query.Append(" order by DOC_INDEX.INDEX_NAME")

        Try
            Return (Server.Con.ExecuteDataset(CommandType.Text, query.ToString()))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    ''' Obtiene de un formulario determinado el id y nombre de atributos y secciones y el orden.
    Public Function getFrmIndexOrderSectionByFormId(ByVal formId As Int64) As DataSet
        Dim query As New StringBuilder()

        query.Append("SELECT DOC_INDEX.INDEX_NAME, zFrmDynamicForms.IdIndice, ZSecciones.nombre, zFrmDynamicForms.IdSeccion, zFrmDynamicForms.NroFila")
        query.Append(" FROM zFrmDynamicForms inner join DOC_INDEX on DOC_INDEX.INDEX_ID = zFrmDynamicForms.IdIndice")
        query.Append(" inner join ZSecciones on ZSecciones.IdSeccion = zFrmDynamicForms.IdSeccion")
        query.Append(" order by zFrmDynamicForms.IdIndice")

        Try
            Return (Server.Con.ExecuteDataset(CommandType.Text, query.ToString()))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener los atributos de un formulario din�mico (tabla zFrmDynamicForms)
    ''' </summary>
    ''' <param name="dynamicFormId">Id de un formulario din�mico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Function getIndexsOfDynamicFormId(ByVal dynamicFormId As Integer) As DataSet

        Try
            Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IdIndice FROM zFrmDynamicForms Where IdFormulario = " & dynamicFormId))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener los atributos de un formulario din�mico (tabla ZFrmIndexsDesc)
    ''' </summary>
    ''' <param name="fId">Id de un formulario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Function getIndexsFromFrmsIndexsDesc(ByVal fId As Integer) As DataSet

        Try
            If Server.isOracle Then
                Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IId, Type, C_Value as Value, EId FROM ZFrmIndexsDesc Where FId = " & fId))
            Else
                Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IId, Type, Value, EId FROM ZFrmIndexsDesc Where FId = " & fId))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    ''' <summary>
    ''' M�todo que sirve para insertar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="fId">Id de formulario</param>
    ''' <param name="iId">Id de un �ndice</param>
    ''' <param name="type">Elemento de enumerador</param>
    ''' <param name="value">0,1 o id de un �ndice</param>
    ''' <param name="eId">Identificador �nico</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/02/2009  Created
    ''' </history>
    Public Shared Sub insertFrmIndexsDesc(ByVal fId As Integer, ByVal iId As Integer, ByVal type As Short, ByVal value As String, ByVal eId As Integer)
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = "INSERT INTO ZFrmIndexsDesc (FId, IId, Type, C_Value, EId) VALUES (" & fId & "," & iId & "," & type & ",'" & value & "'," & eId & ")"
        Else
            query = "INSERT INTO ZFrmIndexsDesc (FId, IId, Type, Value, EId) VALUES (" & fId & "," & iId & "," & type & ",'" & value & "'," & eId & ")"
        End If

        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            query = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' M�todo que sirve para actualizar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="fId">Id de formulario</param>
    ''' <param name="iId">Id de un �ndice</param>
    ''' <param name="type">Elemento de enumerador</param>
    ''' <param name="value">0,1 o id de un �ndice</param>
    ''' <param name="eId">Identificador �nico</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    26/02/2009  Created
    ''' </history>
    Public Shared Sub updateFrmIndexsDesc(ByVal fId As Integer, ByVal iId As Integer, ByVal type As Short, ByVal value As String, ByVal eId As Integer)
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = "UPDATE ZFrmIndexsDesc SET Type = " & type & ", C_Value = " & value & " Where FId = " & fId & " AND iId = " & iId & " AND eId = " & eId
        Else
            query = "UPDATE ZFrmIndexsDesc SET Type = " & type & ", Value = " & value & " Where FId = " & fId & " AND iId = " & iId & " AND eId = " & eId
        End If

        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            query = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' M�todo que sirve para eliminar un elemento de la tabla ZFrmIndexsDesc
    ''' </summary>
    ''' <param name="eId">Identificador �nico</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    27/02/2009  Created
    ''' </history>
    Public Shared Sub deleteFrmIndexsDesc(ByVal eId As Integer)

        Dim query As String = "DELETE ZFrmIndexsDesc Where eId = " & eId

        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            query = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' M�todo que sirve para eliminar las condiciones aplicadas sobre un formulario normal (formulario almacenado en el servidor)
    ''' </summary>
    ''' <param name="idNormalForm">Id de un formulario normal</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    05/05/2009  Created
    ''' </history>
    Public Shared Sub deleteNormalFormsConfigs(ByVal idNormalForm As Int64)

        Dim query As String = "DELETE FROM zFrmDesc WHERE Fid =" & idNormalForm.ToString()
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
        query = Nothing

    End Sub

    ''' <summary>
    ''' Borra las configuraciones realizadas a un formulario dinamico
    ''' </summary>
    ''' <param name="IdFormulario"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 01.04.2009 created
    '''     [Gaston]    06/05/2009  Modified    Se modifico el orden de eliminaci�n ya que se agregaron relaciones entre las tablas relacionadas
    '''                                         con formularios din�micos
    ''' </history>
    Public Shared Sub DeleteDynamicFormsConfigs(ByVal IdFormulario As Int64)

        Dim query As String = "DELETE FROM zFrmIndexsDesc WHERE Fid =" & IdFormulario.ToString()
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        query = "DELETE FROM zFrmDesc WHERE Fid =" & IdFormulario.ToString()
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

        query = "DELETE FROM zFrmDynamicForms WHERE IdFormulario =" & IdFormulario.ToString()
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

    End Sub

    ''' <summary>
    ''' Se obtienen los atributos obligatorios (requeridos) que posee el formulario din�mico
    ''' </summary>
    ''' <param name="frmId">Id de formulario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    11/03/2009  Created
    ''' </history>
    Public Function getRequiredIndexs(ByVal frmId As Int64) As DataSet

        Try
            If Server.isOracle Then
                Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IId FROM ZFrmIndexsDesc Where FId = " & frmId & " AND Type = " & formIndexDescriptionType.required & " AND C_Value = " & 1))
            Else
                Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IId FROM ZFrmIndexsDesc Where FId = " & frmId & " AND Type = " & formIndexDescriptionType.required & " AND Value = " & 1))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener los atributos de tipo exceptuable que pertenecen a un determinado tipo de formulario din�mico
    ''' </summary>
    ''' <param name="frmId">Id de un formulario din�mico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    08/05/2009  Created
    ''' </history>
    Public Function getExceptuableIndexs(ByVal frmId As Int64)

        Try
            Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT DISTINCT IId FROM ZFrmIndexsDesc Where FId = " & frmId & " AND Type = " & formIndexDescriptionType.exceptuable))

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener todos los datos referidos a atributos exceptuables de un determinado tipo de formulario din�mico
    ''' </summary>
    ''' <param name="frmId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    08/05/2009  Created
    ''' </history>
    Public Function getDataExceptuableIndexs(ByVal frmId As Int64)

        Try
            If Server.isOracle Then
                Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IId, C_Value FROM ZFrmIndexsDesc Where FId = " & frmId & " AND Type = " & formIndexDescriptionType.exceptuable))
            Else
                Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IId, Value FROM ZFrmIndexsDesc Where FId = " & frmId & " AND Type = " & formIndexDescriptionType.exceptuable))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

    End Function

    ''' <summary>
    ''' Se obtienen los valores de la propiedad "S�lo Lectura" y "Visible" que posea el �ndice del formulario din�mico
    ''' </summary>
    ''' <param name="frmId">Id de un formulario din�mico</param>
    ''' <param name="iId">Id de un �ndice que pertenece al formulario din�mico</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    13/03/2009  Created
    ''' </history>
    Public Function getValuesOfReadOnlyAndVisibleIndex_Of_DynamicForm(ByVal frmId As Int64, ByVal iId As Int32) As DataSet
        Dim query As New StringBuilder
        If Server.isOracle Then
            query.AppendLine("SELECT Type, C_Value")
        Else
            query.AppendLine("SELECT Type, [Value]")
        End If

        query.AppendLine("FROM ZFrmIndexsDesc")
        query.AppendLine("WHERE (FId = " & frmId & ") AND (IId = " & iId & ") AND (Type = " & formIndexDescriptionType.readOnly_ & " OR Type = " & formIndexDescriptionType.noVisible & ")")

        Try
            Return (Server.Con.ExecuteDataset(CommandType.Text, query.ToString()))
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try
    End Function

    ''' <summary>
    ''' Actualiza la fecha de modificacion del formulario
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateModifiedDate(ByVal formId As Int32)
        Dim query As String = "UPDATE ZFRMS SET [UPDATED]=GETDATE() WHERE ID=" & formId.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
    End Sub

    'Private Sub DeleteDynamicFormCondition(ByVal formid As Int64, ByVal doctypeid As Int64, _
    '                                ByVal indexId As Int64, ByVal compOperator As String, _
    '                                ByVal conexion As String, ByVal value As String)
    '    'todo diego: mover a data
    '    Dim query As New StringBuilder
    '    query.Append("DELETE FROM ZFrmDesc ")
    '    query.Append(" WHERE ")
    '    query.Append(" DTId = ")
    '    query.Append(doctypeid.ToString)
    '    query.Append(" and ")
    '    query.Append(" IId =")
    '    query.Append(indexId.ToString)
    '    query.Append(" and")
    '    query.Append(" Op =")
    '    query.Append("'")
    '    query.Append(compOperator)
    '    query.Append("'")
    '    query.Append(" and ")
    '    query.Append(" Value =")
    '    query.Append("'")
    '    query.Append(value)
    '    query.Append("'")
    '    query.Append(" and ")
    '    query.Append(" FId =")
    '    query.Append(formid)
    '    query.Append(" and ")
    '    query.Append(" QId")

    '    Server.Con.ExecuteNonQuery(query.ToString)

    'End Sub

    ''' <summary>
    ''' Obtiene una tabla con todas las condiciones de un formulario dado
    ''' </summary>
    ''' <remarks></remarks>
    Function GetFormConditionsTable(ByVal formID As Integer) As DataTable
        Dim ds As DataSet

        If Server.isOracle Then
            Dim parNames() As String = {"formID", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {formID, 2}
            ds = Server.Con.ExecuteDataset("ZSP_FORMS_100.GetConditionsWithDescription", parValues)
        Else
            Dim parameters() As Object = {formID}
            ds = Server.Con.ExecuteDataset("ZSP_FORMS_100_GetFormConditionsWithDescription", parameters)
        End If

        If ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de condiciones de un formulario
    ''' </summary>
    ''' <remarks></remarks>
    Function GetFormConditions(ByVal formID As Integer) As List(Of IZFormCondition)
        Dim ds As DataSet

        If Server.isOracle Then
            Dim parNames() As String = {"formID", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {formID, 2}
            ds = Server.Con.ExecuteDataset("ZSP_FORMS_100.GetFormConditions", parValues)
        Else
            Dim parameters() As Object = {formID}
            ds = Server.Con.ExecuteDataset("ZSP_FORMS_100_GetFormConditions", parameters)
        End If

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim returnList As New List(Of IZFormCondition)
                Dim count As Integer = ds.Tables(0).Rows.Count
                Dim dr As DataRow

                For i As Integer = 0 To count - 1
                    dr = ds.Tables(0).Rows(i)
                    returnList.Add(New ZWebFormCondition(dr("ID"), dr("IndexToValidate"), dr("Comparator"),
                                         dr("ComparateValue"), dr("TargetIndex"), dr("TargetAction")))
                Next

                Return returnList
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene una condicion por su id
    ''' </summary>
    ''' <remarks></remarks>
    Function GetCondition(ByVal conditionID As Long) As IZFormCondition
        Dim ds As DataSet

        If Server.isOracle Then
            Dim parNames() As String = {"condID", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {conditionID, 2}
            ds = Server.Con.ExecuteDataset("ZSP_FORMS_100.GetCondition", parValues)
        Else
            Dim parameters() As Object = {conditionID}
            ds = Server.Con.ExecuteDataset("ZSP_FORMS_100_GetCondition", parameters)
        End If

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                Return New ZWebFormCondition(dr("ID"), dr("IndexToValidate"), dr("Comparator"),
                                         dr("ComparateValue"), dr("TargetIndex"), dr("TargetAction"))
            End If
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Agrega una ciondicion a un formulario
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub AddCondition(ByVal formID As Long, ByVal indexSource As Long, ByVal comparator As Comparators, ByVal value As String, ByVal indexTarget As Long, ByVal action As FormActions)
        Dim newConditionID As Long = CoreData.GetNewID(IdTypes.FormCondition)

        If Server.isOracle Then
            Dim parNames() As String = {"formID", "condID", "indexSource", "comparator", "valuecompare", "indexTarget", "action"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Number, OracleType.Number, OracleType.Number, OracleType.VarChar, OracleType.Number, OracleType.Number}
            Dim parValues() As Object = {formID, newConditionID, indexSource, comparator, value, indexTarget, action}
            Server.Con.ExecuteNonQuery("ZSP_FORMS_100.AddCondition", parValues)
        Else
            Dim parameters() As Object = {formID, newConditionID, indexSource, comparator, value, indexTarget, action}
            Server.Con.ExecuteNonQuery("ZSP_FORMS_100_AddCondition", parameters)
        End If
    End Sub

    ''' <summary>
    ''' Edita una condicion
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub EditCondition(ByVal id As Long, ByVal indexSource As Long, ByVal comparator As Comparators, ByVal value As String, ByVal indexTarget As Long, ByVal action As FormActions)
        If Server.isOracle Then
            Dim parNames() As String = {"condID", "indexSource", "comparator", "valuecompare", "indexTarget", "action"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Number, OracleType.Number, OracleType.VarChar, OracleType.Number, OracleType.Number}
            Dim parValues() As Object = {id, indexSource, CInt(comparator), value, indexTarget, CInt(action)}
            Server.Con.ExecuteNonQuery("ZSP_FORMS_100.EditCondition", parValues)
        Else
            Dim parameters() As Object = {id, indexSource, comparator, value, indexTarget, action}
            Server.Con.ExecuteNonQuery("ZSP_FORMS_100_EditCondition", parameters)
        End If
    End Sub

    ''' <summary>
    ''' Borra una condicon por su id
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub DeleteCondition(ByVal conditionID As Long)
        If Server.isOracle Then
            Dim parNames() As String = {"condID"}
            ' Dim parTypes() As Object = {OracleType.Number}
            Dim parValues() As Object = {conditionID}
            Server.Con.ExecuteNonQuery("ZSP_FORMS_100.DeleteCondition", parValues)
        Else
            Dim parameters() As Object = {conditionID}
            Server.Con.ExecuteNonQuery("ZSP_FORMS_100_DeleteCondition", parameters)
        End If
    End Sub

    Shared Sub SetFormBuilded(formId As Long)
        Dim query As String = "UPDATE zfrms SET RebuildForm = 0 WHERE ID = " & formId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

End Class