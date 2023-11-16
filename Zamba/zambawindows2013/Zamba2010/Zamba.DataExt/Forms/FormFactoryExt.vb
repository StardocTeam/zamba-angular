﻿Imports Zamba.Core
Imports Zamba.Servers
Imports System.Data.SqlClient
Imports System.Text

Public Class FormFactoryExt

    Shared Function GetFormsByUserProjects(ByVal groups As String, ByVal objectTypes As ObjectTypes) As DataTable
        'Este stored hay que crearlo ya que aun no se implemento
        Dim query As String = "zsp_100_PRJ_GFByPrjGrp"
        Dim ds As DataSet = Server.Con.ExecuteDataset(query, New Object() {groups, objectTypes})

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    Shared Function GetFormsByProject(ByVal projectID As Long) As DataTable
        Dim query As String = String.Format(" SELECT @ProjectID as 'ID Projecto',52 as 'Tipo de objeto',ID as 'ID de fromulario',Name + ' - ' + CASE Type
            WHEN 0 THEN 'Busqueda'
            WHEN 1 THEN 'Editar'
            WHEN 2 THEN 'Visualizar'
            WHEN 3 THEN 'Workflow'
            WHEN 4 THEN 'Insertar'
            WHEN 5 THEN 'Todo'
            WHEN 6 THEN 'InsertarWeb'
            WHEN 7 THEN 'BusquedaWeb'
            WHEN 8 THEN 'EditarWeb'
            WHEN 9 THEN 'VisualizarWeb'
            WHEN 10 THEN 'WorkFlowWeb'
            END
            as 'Nombre de formulario'
            FROM zfrms
            WHERE ID IN
            (SELECT OBJID
            FROM PRJ_R_O
            WHERE PrjID = @ProjectID and OBJTyp = 52)", projectID)
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Repara el html convirtiendo caracteres incorrectos en sus correspondientes códigos html.
    ''' </summary>
    ''' <param name="html">Html crudo con caracteres inválidos</param>
    ''' <returns>Html corregido de caracteres inválidos</returns>
    ''' <remarks>Método corregido y optimizado del original de FormFactory</remarks>
    Public Function RepairHtml(ByVal html As String) As String
        If html.Contains("á") Then html = html.Replace("á", "&aacute;")
        If html.Contains("Á") Then html = html.Replace("Á", "&Aacute;")
        If html.Contains("é") Then html = html.Replace("é", "&eacute;")
        If html.Contains("É") Then html = html.Replace("É", "&Eacute;")
        If html.Contains("í") Then html = html.Replace("í", "&iacute;")
        If html.Contains("Í") Then html = html.Replace("Í", "&Iacute;")
        If html.Contains("ó") Then html = html.Replace("ó", "&oacute;")
        If html.Contains("Ó") Then html = html.Replace("Ó", "&Oacute;")
        If html.Contains("ú") Then html = html.Replace("ú", "&uacute;")
        If html.Contains("Ú") Then html = html.Replace("Ú", "&Uacute;")
        If html.Contains("ü") Then html = html.Replace("ü", "&uuml;")
        If html.Contains("Ü") Then html = html.Replace("Ü", "&Uuml;")
        If html.Contains("ñ") Then html = html.Replace("ñ", "&ntilde;")
        If html.Contains("Ñ") Then html = html.Replace("Ñ", "&Ntilde;")
        Return html
    End Function

    ''' <summary>
    ''' Inserta o actualiza un formulario en formato digital
    ''' </summary>
    ''' <param name="formId">Id de formulario</param>
    ''' <param name="digitalForm">Formato digital del formulario</param>
    ''' <remarks></remarks>
    Public Sub SetDigitalFile(ByVal formId As Int32, ByVal digitalForm As Byte(), Optional ByRef t As Transaction = Nothing)

        Dim query As String
        Dim isDigital As Boolean = IsDigitalForm(formId)

        If Server.isOracle Then

            If isDigital Then
                query = "Declare docfile clob; Begin docfile:= '" & System.Text.Encoding.UTF8.GetString(digitalForm) & "'; UPDATE ZFRMSBLOB SET DOCFILE=docFile WHERE ID=" & formId.ToString & "; END ;"
            Else
                query = "Declare docfile clob; Begin docfile:= '" & System.Text.Encoding.UTF8.GetString(digitalForm) & "'; INSERT INTO ZFRMSBLOB VALUES(" & formId.ToString & ", docfile) " & "; End ;"
            End If

            If t Is Nothing Then
                Server.Con.ExecuteNonQuery(CommandType.Text, query)
            Else
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query)
            End If

        Else
            If isDigital Then
                query = "UPDATE ZFRMSBLOB Set DOCFILE=@docFile WHERE ID=" & formId.ToString
            Else
                query = "INSERT INTO ZFRMSBLOB VALUES(" & formId.ToString & ", @docFile)"
            End If

            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "Select @@version")
            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image, digitalForm.Length, ParameterDirection.Input, False, 0, 0, String.Empty, DataRowVersion.Current, digitalForm)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary, digitalForm.Length, ParameterDirection.Input, False, 0, 0, String.Empty, DataRowVersion.Current, digitalForm)
            End If
            Dim params As IDbDataParameter() = {pDocFile}

            If t Is Nothing Then
                Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
            Else
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query, params)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Impacta la marca de formato digital en la tabla ZFRMS
    ''' </summary>
    ''' <param name="formId">Id de formulario</param>
    ''' <param name="t">Transacción</param>
    ''' <remarks></remarks>
    Public Sub SetBlobFlag(ByVal formId As Int32, Optional ByRef t As Transaction = Nothing)
        Dim query As String = "UPDATE ZFRMS Set USEBLOB=1,Updated=getdate() WHERE ID=" & formId.ToString
        If t Is Nothing Then
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        Else
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query)
        End If
    End Sub

    ''' <summary>
    ''' Verifica si el formulario se encuentra digitalizado
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsDigitalForm(ByVal formId As Int32) As Boolean
        Dim exist As Int16
        If Server.isOracle Then
            exist = Int16.Parse(Server.Con.ExecuteScalar(CommandType.Text, " Select COUNT(1) FROM ZFRMSBLOB WHERE ID=" & formId))
        Else
            exist = Int16.Parse(Server.Con.ExecuteScalar("ZSP_FORMS_100_IsDigitalForm", New Object() {formId}))
        End If

        If exist = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Actualiza un formulario
    ''' </summary>
    ''' <param name="form">Formulario</param>
    ''' <returns>True en caso de actualizarse correctamente</returns>
    ''' <remarks></remarks>
    Public Function UpdateForm(ByVal form As ZwebForm) As Boolean
        Dim Strupdate As New StringBuilder
        Dim updated As Boolean
        Dim useBlob As Int16 = 0
        Dim useRuleRights As Int16 = 0

        If form.UseBlob Then useBlob = 1
        If form.useRuleRights Then useRuleRights = 1

        Dim t As New Transaction(Server.Con(False))

        Try

            'Strupdate = "UPDATE ZFRMS Set Name ='" & form.Name & "',ParentId = " & form.ParentId & ",Type = " & form.Type & ",Objecttypeid = " & form.ObjectTypeId & ",Path = '" & form.Path & "',Description = '" & form.Description & "', useRuleRights= " & useRuleRights & ", Updated=getdate(), UseBlob=" & useBlob & " where id = " & form.ID
            't.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, Strupdate)

            Strupdate.Append("UPDATE ZFRMS set Name ='")
            Strupdate.Append(form.Name)
            Strupdate.Append("',ParentId = ")
            Strupdate.Append(form.ParentId)
            Strupdate.Append(",Type = ")
            Strupdate.Append(form.Type)
            Strupdate.Append(",Objecttypeid = ")
            Strupdate.Append(form.ObjectTypeId)
            Strupdate.Append(",Path = '")
            Strupdate.Append(form.Path)
            Strupdate.Append("',Description = '")
            Strupdate.Append(form.Description)
            Strupdate.Append("', useRuleRights = ")
            Strupdate.Append(useRuleRights)
            Strupdate.Append(", Updated = ")
            Strupdate.Append(If(Server.isOracle, "sysdate", "getdate()"))
            Strupdate.Append(", UseBlob=")
            Strupdate.Append(useBlob)
            Strupdate.Append(" where id = ")
            Strupdate.Append(form.ID)

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, Strupdate.ToString())

            Strupdate.Clear()

            'Strupdate = "UPDATE Ztype_zfrms set doctype_id = " & form.DocTypeId & " where form_id = " & form.ID
            't.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, Strupdate)

            Strupdate.Append("UPDATE Ztype_zfrms set doctype_id = ")
            Strupdate.Append(form.DocTypeId)
            Strupdate.Append(" where form_id = ")
            Strupdate.Append(form.ID)

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, Strupdate.ToString())





            If form.UseBlob Then
                SetDigitalFile(form.ID, form.EncodedFile, t)
            End If

            t.Commit()
            updated = True

        Catch ex As Exception
            ZClass.raiseerror(ex)
            If Not IsNothing(t.Transaction) AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                t.Rollback()
            End If

        Finally
            If Not IsNothing(t) Then
                If Not IsNothing(t.Con) Then
                    t.Con.Close()
                    t.Con.dispose()
                    t.Con = Nothing
                End If
                t.Dispose()
                t = Nothing
            End If
            Strupdate = Nothing
        End Try

        Return updated
    End Function

    ''' <summary>
    ''' Inserta un nuevo formulario
    ''' </summary>
    ''' <param name="form">Formulario</param>
    ''' <returns>True si se inserto correctamente</returns>
    ''' <remarks></remarks>
    Public Function InsertForm(ByVal form As ZwebForm) As Boolean
        Dim query As String
        Dim inserted As Boolean
        Dim useBlob As Int16 = 0
        Dim useRuleRights As Int16 = 0
        If form.UseBlob Then useBlob = 1
        If form.useRuleRights Then useRuleRights = 1

        Dim t As New Transaction(Server.Con(False))

        Try
            query = "INSERT INTO ZFRMS (Id,Name,ParentId,Type,Objecttypeid,Path,Description,UseRuleRights,Updated,UseBlob) VALUES (" &
                form.ID & ",'" & form.Name & "'," & form.ParentId & "," & form.Type & "," & form.ObjectTypeId & ",'" &
                form.Path & "','" & form.Description & "'," & useRuleRights & ",getdate()," & useBlob & ")"
            If Server.isOracle Then
                query = query.Replace("getdate()", "sysdate")
            End If
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query)

            query = "INSERT INTO Ztype_zfrms (doctype_id,form_id) VALUES (" & form.DocTypeId & "," & form.ID & ")"
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query)

            If form.UseBlob Then
                SetDigitalFile(form.ID, form.EncodedFile, t)
            End If

            t.Commit()
            inserted = True

        Catch ex As Exception
            ZClass.raiseerror(ex)
            If Not IsNothing(t.Transaction) AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                t.Rollback()
            End If

        Finally
            If Not IsNothing(t) Then
                If Not IsNothing(t.Con) Then
                    t.Con.Close()
                    t.Con.dispose()
                    t.Con = Nothing
                End If
                t.Dispose()
                t = Nothing
            End If
        End Try

        Return inserted
    End Function

    ''' <summary>
    ''' Obtiene el formato digital de un formulario
    ''' </summary>
    ''' <param name="formId">Id de formulario</param>
    ''' <returns>Array de bytes</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDigitalForm(ByVal formId As Int32) As Byte()
        Dim File As Byte() = DirectCast(Server.Con.ExecuteScalar("ZSP_FORMS_100_GetFormFile", New Object() {formId}), Byte())
        Return File
    End Function

    Sub SetFormToRebuild(formId As Long)
        Dim query As String = "UPDATE zfrms SET RebuildForm = 1 WHERE ID = " & formId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Sub SetAllFormsToRebuild()
        Dim query As String = "UPDATE zfrms SET RebuildForm = 1"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

End Class