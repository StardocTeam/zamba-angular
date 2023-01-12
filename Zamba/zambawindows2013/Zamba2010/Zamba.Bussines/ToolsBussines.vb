Imports Zamba.Data
Public Class ToolsBusiness
    Inherits ZClass
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un nuevo ID
    ''' </summary>
    ''' <param name="IdType">Tipo de objeto para el cual se requiere un nuevo ID</param>
    ''' <returns>Integer</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNewID(ByVal IdType As IdTypes) As Integer
        Return CoreData.GetNewID(IdType)
    End Function
    Public Shared Function VerificarTablas()
        Return CoreDataTables.VerificarTablas
    End Function
    Public Overrides Sub Dispose()

    End Sub
    ''' <summary>
    ''' Devuelve si se cumple la comparacion o no
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Ezequiel]	04/03/2009	Created
    ''' </history>
    Public Shared Function ValidateComp(ByVal value1 As String, ByVal value2 As String, ByVal comptype As Comparadores, ByVal tmpCaseInsensitive As Boolean, Optional ByVal r As Result = Nothing) As Boolean

        Dim valor1, valor2 As Decimal
        If value1 Is Nothing OrElse String.IsNullOrEmpty(value1) Then value1 = String.Empty
        If value2 Is Nothing OrElse String.IsNullOrEmpty(value2) Then value2 = String.Empty
        If Decimal.TryParse(value1, valor1) And Decimal.TryParse(value2, valor2) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ambos valores son decimales")
            Select Case comptype
                Case Comparadores.Distinto
                    'If zvalue <> myrule.TxtValue Then
                    If valor1 <> valor2 Then
                        Return True
                    End If
                Case Comparadores.Igual
                    If valor1 = valor2 Then
                        Return True
                    End If
                Case Comparadores.Contiene
                    If value1.ToLower().Contains(value2.Trim().ToLower()) Then
                        Return True
                    End If
                Case Comparadores.Empieza
                    If value1.ToLower().StartsWith(value2.Trim().ToLower()) Then
                        Return True
                    End If
                Case Comparadores.Termina
                    If value1.ToLower().EndsWith(value2.Trim().ToLower()) Then
                        Return True
                    End If
                Case Comparadores.IgualMayor
                    If valor1 >= valor2 Then
                        Return True
                    End If
                Case Comparadores.IgualMenor
                    If valor1 <= valor2 Then
                        Return True
                    End If
                Case Comparadores.Mayor
                    If valor1 > valor2 Then
                        Return True
                    End If
                Case Comparadores.Menor
                    If valor1 < valor2 Then
                        Return True
                    End If
            End Select
        Else

            value1 = value1.Trim
            value2 = value2.Trim
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se compara por string")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "valor de la variable: " & Chr(34) & value1 & Chr(34))
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor para comparar: " & Chr(34) & value2 & Chr(34))


            ' [German] 04/07/2012 - modificacion en  Contiene,Empieza,Termina.

            Select Case comptype
                Case Comparadores.Distinto
                    'If zvalue <> myrule.TxtValue Then
                    If String.Compare(value1.Trim(), value2.Trim(), tmpCaseInsensitive) <> 0 Then
                        Return True
                    End If
                Case Comparadores.Igual
                    If String.Compare(value1.Trim(), value2.Trim(), tmpCaseInsensitive) = 0 Then
                        Return True
                    End If
                Case Comparadores.Contiene
                    If tmpCaseInsensitive Then
                        Return value1.ToLower().Contains(value2.Trim().ToLower())
                    Else
                        Return value1.Contains(value2.Trim())
                    End If
                Case Comparadores.Empieza
                    If tmpCaseInsensitive Then
                        Return value1.ToLower().StartsWith(value2.Trim().ToLower())
                    Else
                        Return value1.StartsWith(value2.Trim())
                    End If
                Case Comparadores.Termina
                    If tmpCaseInsensitive Then
                        Return value1.ToLower().EndsWith(value2.Trim().ToLower())
                    Else
                        Return value1.EndsWith(value2.Trim())
                    End If
                Case Comparadores.IgualMayor
                    If value1 >= value2 Then
                        Return True
                    End If
                Case Comparadores.IgualMenor
                    If value1 <= value2 Then
                        Return True
                    End If
                Case Comparadores.Mayor
                    If value1 > value2 Then
                        Return True
                    End If
                Case Comparadores.Menor
                    If value1 < value2 Then
                        Return True
                    End If
            End Select

            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Valor1: {0} no es {1} que Valor2: {2}", value1.ToString(), comptype.ToString(), value2.ToString()))
            Return False
        End If
    End Function

#Region "Global Variable Bussines"
    Public Shared Function GetGlobalVariables() As DataSet
        Return CoreData.GetGlobalVariables()
    End Function


    Public Shared Sub loadGlobalVariables()
        Try
            If (ZOptBusiness.GetValue("Environment") IsNot Nothing) Then

                Dim ds As DataSet = ToolsBusiness.GetGlobalVariablesByEnvironment(ZOptBusiness.GetValue("Environment"))

                For Each dr As DataRow In ds.Tables(0).Rows
                    If (VariablesInterReglas.ContainsKey(dr(1)) = False) Then
                        VariablesInterReglas.Add(dr(1), dr(2), True)
                    End If
                Next
            Else

            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetGlobalVariablesByEnvironment(ByVal environment As String) As DataSet
        Return CoreData.GetGlobalVariablesByEnvironment(environment)
    End Function

    Public Shared Sub UpdateGlobalVariable(ByVal id As Int32, ByVal Name As String, ByVal value As String)
        CoreData.UpdateGlobalVariable(id.ToString(), Name, value)
    End Sub

    Public Shared Sub DeleteGlobalVariable(ByVal id As Int32)
        CoreData.DeleteGlobalVariable(id.ToString())
    End Sub

    Public Shared Sub InsertGlobalVariable(ByVal Name As String, ByVal value As String, ByVal environment As String)
        Dim ID As Int64 = GetNewID(IdTypes.GlobalVariable)
        CoreData.InsertGlobalVariable(Name, value, environment, ID)
    End Sub
#End Region
End Class
