Imports System.Text
Imports Zamba.Framework.Tools

Namespace Search
    Namespace Components
        Public Class SearchTools


        End Class
    End Namespace

    Namespace Data

        Public Class SearchToolsData
            Implements IDisposable

            Private con As IConnection
            Public Sub New(ByVal Connection As IConnection)
                con = Connection

            End Sub

            ''' <summary>
            ''' Realiza la busqueda en el indexado de la palabra especificada
            ''' </summary>
            ''' <param name="Search"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function SearchInAllIndexs(ByVal Search As ISearch) As DataSet

                'Se quitan los acentos y mayúsculas para que busque sobre las palabras indexadas
                Search.Textsearch = TextTools.ReemplazarAcentos(Search.Textsearch).ToLower()

                Dim strSelect, strEndSelect, strWhere As String
                Dim sqlBuilder As New StringBuilder()
                Dim ds As New DataSet()

                strSelect = "Select resultid, dtid, count(1) as Ocurrency from zsearchvalues_dt t inner join zsearchvalues w on t.wordid = w.id AND "

                strWhere = "w.word = "

                strEndSelect = "group by resultid, dtid order by ocurrency desc"

                sqlBuilder.Append(strSelect)

                Dim strSplitedWord() As String = Search.Textsearch.Trim.Split(" ")
                For Each _strword As String In strSplitedWord
                    sqlBuilder.Append(strWhere)
                    sqlBuilder.Append("'")
                    sqlBuilder.Append(_strword.ToLower)
                    sqlBuilder.Append("' or ")
                Next
                sqlBuilder.Remove(sqlBuilder.ToString().Length - 4, 4)
                'sqlBuilder.Append(") ")

                sqlBuilder.Append(" AND DTID IN (")
                For Each _docType As IDocType In Search.Doctypes
                    sqlBuilder.Append(_docType.ID.ToString() & ",")
                Next
                sqlBuilder.Remove(sqlBuilder.ToString().Length - 1, 1)
                sqlBuilder.Append(" )")

                sqlBuilder.Append(strEndSelect)

                Return con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

            End Function

            ''' <summary>
            ''' Realiza la busqueda en el indexado de la palabra especificada
            ''' </summary>
            ''' <param name="Search"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function SearchGlobalInAllIndexs(ByVal Search As ISearch) As DataSet

                'Se quitan los acentos y mayúsculas para que busque sobre las palabras indexadas
                Search.Textsearch = TextTools.ReemplazarAcentos(Search.Textsearch).ToLower()
                Dim strWhere As New StringBuilder
                Dim strSplitedWord() As String = Search.Textsearch.Trim.Split(" ")

                Dim DocTypesString As StringBuilder
                'DocTypes
                If Search.Doctypes.Count > 0 Then
                    DocTypesString = New StringBuilder
                    DocTypesString.Append(" AND DTID IN (")
                    For Each _docType As IDocType In Search.Doctypes
                        DocTypesString.Append(_docType.ID.ToString() & ",")
                    Next
                    DocTypesString.Remove(DocTypesString.ToString().Length - 1, 1)
                    DocTypesString.Append(" )")
                End If

                ' Armo desde la consulta mas anidada hacia la de mas afuera, 4 niveles.
                Dim fourthQuery As New StringBuilder
                fourthQuery.Append("Select id ")
                fourthQuery.Append("From zsearchvalues ")
                fourthQuery.Append("WHERE ")

                For Each _strword As String In strSplitedWord
                    strWhere.Append(" word Like ")
                    strWhere.Append("'%")
                    strWhere.Append(_strword.ToLower)
                    strWhere.Append("%' or ")
                Next

                strWhere.Remove(strWhere.ToString().Length - 4, 4)
                fourthQuery.Append(strWhere.ToString())

                Dim thirdQuery As New StringBuilder
                thirdQuery.Append("SELECT resultid, dtid, count(1) AS Ocurrency ")
                thirdQuery.Append("FROM zsearchvalues_dt t ")
                thirdQuery.Append("INNER JOIN zsearchvalues w ON t.wordid = w.id ")
                thirdQuery.Append(String.Format("WHERE wordid = ANY ({0}) ", fourthQuery.ToString()))
                thirdQuery.Append(If(DocTypesString IsNot Nothing, DocTypesString.ToString(), String.Empty))
                thirdQuery.Append("GROUP BY resultid, dtid")

                Dim secondQuery As New StringBuilder
                secondQuery.Append("select DISTINCT t.ResultId, t.DTID, t.ResultName as Nombre, t.ICON_ID, Ocurrency ")
                secondQuery.Append("FROM zsearchvalues_dt t ")
                secondQuery.Append(String.Format("INNER JOIN ({0}) G ", thirdQuery.ToString()))
                secondQuery.Append("ON G.resultid = t.resultid AND g.dtid = t.dtid")

                Dim firstQuery As New StringBuilder
                firstQuery.Append("Select r.*, (u.APELLIDO + ' ' + u.NOMBRES) as Asignado, s.Name as Etapa , '' as THUMB ")
                firstQuery.Append(String.Format("FROM({0}) r ", secondQuery.ToString()))
                ' firstQuery.Append("Left outer join ZThumb on r.ResultId = ZThumb.DOC_ID And r.DTID = ZThumb.DOC_TYPE_ID ")
                firstQuery.Append("Left Join wfdocument w on w.Doc_ID = r.ResultId ")
                firstQuery.Append("Left Join usrtable u on u.ID = w.User_Asigned ")
                firstQuery.Append("Left Join WFStep s on s.step_Id = w.step_Id ")
                firstQuery.Append("order by Ocurrency desc")

                Return con.ExecuteDataset(CommandType.Text, firstQuery.ToString())

            End Function

#Region "IDisposable Support"
            Private disposedValue As Boolean ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not disposedValue Then
                    If disposing Then
                        ' TODO: dispose managed state (managed objects).
                    End If

                    ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                    ' TODO: set large fields to null.
                End If
                disposedValue = True
            End Sub

            ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
            'Protected Overrides Sub Finalize()
            '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            '    Dispose(False)
            '    MyBase.Finalize()
            'End Sub

            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class
    End Namespace

End Namespace
