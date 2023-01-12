Imports System.Text
Imports System.Collections.Generic
Imports Zamba.Core

Public Class AutonameFactory

    ''' <summary>
    ''' Metodo utilizado por una aplicacion externa para realizar el proceso de autonombre
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[AlejandroR] - 02/03/2010 -	Created - (WI 4419)
    ''' </history>
    Public Shared Sub ExecuteUpdate(ByVal DocTypeId As Int32, ByVal DocTypeName As String, ByVal AutoName As String)
        Dim sbFileds As New StringBuilder()
        Dim sbJoins As New StringBuilder()
        Dim sbUpdateDocI As New StringBuilder()
        Dim sbUpdateWFDoc As New StringBuilder()
        Dim Fields As New List(Of String)
        Dim IdIndice As String
        Dim DOCI As String = "DOC_I" & DocTypeId
        Dim DOCT As String = "DOC_T" & DocTypeId

        AutoName = AutoName.Trim()

        'separar las partes para obtener los ids de los indices
        Dim partes() As String = AutoName.Split("@")

        'Reemplazo los valores y obtengo los campos
        For Each p As String In partes

            If Not String.IsNullOrEmpty(p) Then

                If p.Substring(0, 1).ToUpper() = "I" Then 'es un indice, obtener el numero

                    IdIndice = p.Substring(1, p.Length - 1)

                    'por cada indice ver si es de sustitucion
                    If EsIndiceSustitucion(IdIndice) Then

                        Fields.Add("SLST_S" + IdIndice + ".Descripcion")

                        sbJoins.Append(" LEFT JOIN SLST_S" + IdIndice + " ON SLST_S" + IdIndice + ".Codigo = " + DOCI + ".I" + IdIndice)

                    Else

                        Fields.Add(DOCI + ".I" + IdIndice)

                    End If

                ElseIf p.ToUpper() = "DT" Then 'nombre del documento

                    Fields.Add("'" & DocTypeName.Trim() & "'")

                ElseIf p.ToUpper() = "CD" OrElse p.ToUpper() = "FC" Then 'fecha de creacion

                    Fields.Add(DOCI + ".crdate")

                ElseIf p.ToUpper() = "ED" OrElse p.ToUpper() = "FM" Then 'fecha de modificacion

                    Fields.Add(DOCI + ".lupdate")

                Else 'string harcodeado

                    Fields.Add("'" & p & "'")

                End If

            End If

        Next

        'Concatenar campos para el select
        For Each field As String In Fields

            If Server.isSQLServer Then
                sbFileds.Append(" CONVERT(VARCHAR, IsNull(" + field + ", '')) ")
                sbFileds.Append(" + ")
            Else
                sbFileds.Append(field)
                sbFileds.Append(" || ")
            End If

        Next

        'eliminar el + o los || que sobran al final
        If Server.isSQLServer Then
            sbFileds.Remove(sbFileds.Length - 3, 3)
        Else
            sbFileds.Remove(sbFileds.Length - 4, 4)
        End If

        If Server.isSQLServer Then

            'query para update de DOCI
            sbUpdateDocI.Append(" UPDATE " + DOCT)
            sbUpdateDocI.Append(" SET NAME = ")
            sbUpdateDocI.Append(sbFileds.ToString)
            sbUpdateDocI.Append(" FROM " + DOCI)
            sbUpdateDocI.Append(" INNER JOIN " + DOCT + " ON " + DOCI + ".DOC_ID = " + DOCT + ".DOC_ID ")
            sbUpdateDocI.Append(sbJoins.ToString())

            'query para update de WfDocument
            sbUpdateWFDoc.Append(" UPDATE wfdocument ")
            sbUpdateWFDoc.Append(" SET NAME = ")
            sbUpdateWFDoc.Append(sbFileds.ToString)
            sbUpdateWFDoc.Append(" FROM " + DOCI)
            sbUpdateWFDoc.Append(" INNER JOIN wfdocument ON " + DOCI + ".DOC_ID = wfdocument.DOC_ID ")
            sbUpdateWFDoc.Append(sbJoins.ToString())

        Else

            'query para update de DOCI
            sbUpdateDocI.Append(" UPDATE /*+BYPASS_UJVC*/ (")
            sbUpdateDocI.Append(" SELECT " + DOCT + ".NAME, ")
            sbUpdateDocI.Append(sbFileds.ToString)
            sbUpdateDocI.Append(" AS NuevoNombre ")
            sbUpdateDocI.Append(" FROM " + DOCI)
            sbUpdateDocI.Append(" INNER JOIN " + DOCT + " ON " + DOCI + ".DOC_ID = " + DOCT + ".DOC_ID ")
            sbUpdateDocI.Append(sbJoins.ToString())
            sbUpdateDocI.Append(") SET NAME = NuevoNombre ")

            'query para update de WfDocument
            sbUpdateWFDoc.Append(" UPDATE /*+BYPASS_UJVC*/ (")
            sbUpdateWFDoc.Append(" SELECT wfdocument.NAME, ")
            sbUpdateWFDoc.Append(sbFileds.ToString)
            sbUpdateWFDoc.Append(" AS NuevoNombre ")
            sbUpdateWFDoc.Append(" FROM " + DOCI)
            sbUpdateWFDoc.Append(" INNER JOIN wfdocument ON " + DOCI + ".DOC_ID = wfdocument.DOC_ID ")
            sbUpdateWFDoc.Append(sbJoins.ToString())
            sbUpdateWFDoc.Append(") SET NAME = NuevoNombre ")

        End If

        Try
            'Actualizar DOCI...
            Server.Con.ExecuteNonQuery(CommandType.Text, sbUpdateDocI.ToString())

            'Actualizar WfDocument...
            Server.Con.ExecuteNonQuery(CommandType.Text, sbUpdateWFDoc.ToString())

            MessageBox.Show("Documentos actualizados!", "AutoName", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Se produjo un error al actualizar el nombre de los documentos", "AutoName", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ZClass.raiseerror(ex)
         End Try
    End Sub

    Private Shared Function EsIndiceSustitucion(ByVal IdIndice As String) As Boolean

        Dim sql As String

        sql = "SELECT count(1) FROM DOC_INDEX WHERE DROPDOWN = 2 AND INDEX_ID = " + IdIndice.ToString

        If Server.Con.ExecuteScalar(CommandType.Text, sql) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
