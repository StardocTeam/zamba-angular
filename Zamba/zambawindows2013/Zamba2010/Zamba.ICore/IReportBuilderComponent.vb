Public Interface IReportBuilderComponent
    Sub DeleteChartOption(idquery As Integer)
    Sub DeleteLinkValues(idquery As Integer)
    Sub DeleteQueryBuilder(id As Integer, name As String)
    Sub InsertQueryBuilder(name As String, tables As String, fields As String, relations As String, conditions As String, userid As Integer, sortExpression As String, groupExpression As String)
    Sub InsertQueryReportGeneral(name As String, conditions As String, query As String, group As String)
    Sub InsertUpdateLinkValues(idquery As Integer, relatinalField As String, Entity As String, Description As String, Position As Short)
    Sub UpdateChartOption(ReportID As Integer, xValueColumns As String, yValueColumns As String, ChartType As Integer)
    Sub UpdateQueryBuilder(id As Integer, tables As String, fields As String, relations As String, conditions As String, name As String, sortExpression As String, GroupExpression As String)
    Function CheckReportHaveChart(idquery As Integer) As Boolean
    Function GenerateQueryBuilder(id As Integer, isZamba As Boolean, askCondition As Boolean) As String
    Function GetAllQueryIdsAndNames() As DataSet
    Function GetAllQueryIdsAndNamesReporteGeneral() As DataSet
    Function GetChartType(idquery As Integer) As Integer
    Function GetColumns(name As String) As DataSet
    Function GetConditions(id As Integer) As String
    Function GetConditionsToComplete(id As Integer) As ArrayList
    Function GetFields(id As Integer) As String
    Function GetGeneralReportIdByName(Name As String) As Integer
    Function GetGroupExpression(rptid As Integer, isGral As Boolean) As String
    Function GetLink(id As Integer) As String
    Function GetLinkValuesByReportID(idquery As Integer) As DataTable
    Function GetName(id As Integer) As String
    Function GetQueryIdsAndNames(ids As List(Of Long)) As DataSet
    Function GetQueryIdsAndNamesReportGeneral(ids As List(Of Long)) As DataSet
    Function GetRelations(id As Integer) As String
    Function GetSortExpression(rptid As Integer) As String
    Function GetTables(id As Integer) As String
    Function GetXValueColumn(idquery As Integer) As String
    Function GetYValueColumn(idquery As Integer) As String
    Function RunQueryBuilder(id As Integer, isZamba As Boolean, askCondition As Boolean) As DataSet
    Function RunQueryBuilderReporteGeneral(id As Integer) As DataSet
End Interface
