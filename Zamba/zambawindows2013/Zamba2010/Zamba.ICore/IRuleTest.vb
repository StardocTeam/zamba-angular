Public Interface IRuleTest
    Property TestResult() As Object ' DESPUES BORRAR YA QUE NO SE USA. SE DEBEN BORRAR TODAS LAS IMPLEMENTACIONES QUE EXISTEN EN LAS REGLAS.
    Function PlayTest() As Boolean ' DESPUES BORRAR YA QUE NO SE USA. SE DEBEN BORRAR TODAS LAS IMPLEMENTACIONES QUE EXISTEN EN LAS REGLAS.
    Function TestRule(ByVal tasks As List(Of ITaskResult)) As List(Of ITaskResult)
    Function DiscoverParams() As List(Of String)
End Interface
