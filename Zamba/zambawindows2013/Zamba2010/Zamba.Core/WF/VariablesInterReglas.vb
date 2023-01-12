Imports System.Collections.Generic
Imports System.Text.RegularExpressions

''' <summary>
''' Clase que se encarga del manejo de las variables de las reglas
''' </summary>
''' <history>Marcelo    01/06/2011  Modified</history>
''' <remarks></remarks>
Public Class VariablesInterReglas
    Public Shared _VariablesInterReglas As New Hashtable
    Public Shared _VariablesGlobales As New Hashtable

    ''' <summary>
    ''' Agrega una variable al listado
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <param name="isGlobal"></param>
    ''' <remarks></remarks>
    Public Shared Sub Add(ByVal key As Object, ByVal value As Object, ByVal isGlobal As Boolean)
        If key.Equals(String.Empty) Then Return
        If Not isGlobal Then
            If Not _VariablesInterReglas.ContainsKey(key.ToString().ToLower()) Then
                _VariablesInterReglas.Add(key.ToString().ToLower(), value)
            End If
        Else
            If Not _VariablesGlobales.ContainsKey(key.ToString().ToLower()) Then
                _VariablesGlobales.Add(key.ToString().ToLower(), value)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Quita una variable del listado
    ''' </summary>
    ''' <param name="key"></param>
    ''' <remarks></remarks>
    Public Shared Sub Remove(ByVal key As Object)
        If _VariablesInterReglas.ContainsKey(key.ToString().ToLower()) Then
            _VariablesInterReglas.Remove(key.ToString().ToLower())
        End If
    End Sub

    ''' <summary>
    ''' Verifica si la key esta en los listados
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ContainsKey(ByVal key As Object) As Boolean
        If _VariablesInterReglas.ContainsKey(key.ToString().ToLower()) Then
            Return True
        Else
            Return _VariablesGlobales.ContainsKey(key.ToString().ToLower())
        End If
    End Function

    ''' <summary>
    ''' Devuelve los 2 hash de variables en uno solo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function clone() As Hashtable
        Dim cloneHash As Hashtable = _VariablesInterReglas.Clone()

        For Each key As String In _VariablesGlobales.Keys
            If cloneHash.Contains(key) = False Then
                cloneHash.Add(key, _VariablesGlobales(key).ToString())
            End If
        Next

        Return cloneHash
    End Function

    ''' <summary>
    ''' Devuelve un item del listado
    ''' </summary>
    ''' <param name="key"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property Item(ByVal key As Object) As Object
        Get
            If _VariablesInterReglas.ContainsKey(key.ToString().ToLower()) Then
                Return _VariablesInterReglas.Item(key.ToString().ToLower())
            Else
                Return _VariablesGlobales.Item(key.ToString().ToLower())
            End If
        End Get
        Set(ByVal value As Object)
            If _VariablesInterReglas.ContainsKey(key.ToString().ToLower()) Then
                _VariablesInterReglas.Item(key.ToString().ToLower()) = value
            Else
                _VariablesGlobales.Item(key.ToString().ToLower()) = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia las variables de reglas
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Clear()
        Dim keys As New List(Of String)
        Try
            For Each key As String In _VariablesInterReglas.Keys
                keys.Add(key)
            Next

            For Each key As String In keys
                If TypeOf _VariablesInterReglas(key) Is IDisposable Then
                    _VariablesInterReglas(key).dispose()
                End If
                _VariablesInterReglas(key) = Nothing
            Next

            _VariablesInterReglas.Clear()
            _VariablesInterReglas = Nothing
            _VariablesInterReglas = New Hashtable()
        Finally
            keys.Clear()
            keys = Nothing
        End Try
    End Sub
    ''' <summary> 
    ''' Reconoce las variables 
    ''' </summary> 
    ''' <param name="TextoaValidar"></param> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Function ReconocerVariables(ByVal TextoaValidar As String) As String
        Dim R As String = String.Empty
        Dim Variable As String
        Dim ValorVariable As Object
        Dim ds As DataSet = Nothing
        If TextoaValidar.Equals(String.Empty) Then Return String.Empty
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables en: " & TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar").Replace("ZVAr", "zvar").Replace("ZVar", "zvar").Replace("ZVaR", "zvar").Replace("Zvar", "zvar").Replace("ZvAR", "zvar").Replace("ZvaR", "zvar").Replace("ZvAr", "zvar").Replace("zVAR", "zvar").Replace("zvAR", "zvar").Replace("zvaR", "zvar")

        While TextoaValidar.ToLower().Contains("zvar")
            Try
                Variable = ObtenerNombreVariable(TextoaValidar)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable: " & Variable)
                If Not String.IsNullOrEmpty(Variable) Then
                    Try
                        R = String.Empty
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(TextoaValidar)

                        If ValorVariable IsNot Nothing Then
                            If TypeOf (ValorVariable) Is DataSet Then
                                ds = DirectCast(ValorVariable, DataSet)
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        For Each DR As DataRow In ds.Tables(0).Rows
                                            R &= DR.Item(0).ToString & ","
                                        Next
                                    End If
                                End If
                                R = R.Remove(R.Length - 1, 1)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Variable " & Variable & ": " & R)
                                TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), R, RegexOptions.IgnoreCase)
                            ElseIf TypeOf (ValorVariable) Is Byte() Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Variable: " & ValorVariable)
                                Return ValorVariable
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Variable: " & ValorVariable.ToString())
                                If Variable.Contains("(") AndAlso Not Variable.Contains(")") Then
                                    Variable = Variable & ")"
                                End If
                                TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), ValorVariable.ToString(), RegexOptions.IgnoreCase)
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se recupero el Valor de la Variable")
                            TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                        End If

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                    End Try
                Else
                    Exit While
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            End Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & TextoaValidar)
        End While

        If ds IsNot Nothing Then
            ds.Dispose()
            ds = Nothing
        End If
        ValorVariable = Nothing
        Return TextoaValidar
    End Function

    ''' <summary> 
    ''' Reconoce las variables 
    ''' </summary> 
    ''' <param name="TextoaValidar"></param> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Function ReconocerVariablesAsObject(ByVal TextoaValidar As String) As Object
        Dim R As String = String.Empty
        Dim Variable As String
        Dim ValorVariable As Object
        Dim ds As DataSet = Nothing

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables en: " & TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar").Replace("ZVAr", "zvar").Replace("ZVar", "zvar").Replace("ZVaR", "zvar").Replace("Zvar", "zvar").Replace("ZvAR", "zvar").Replace("ZvaR", "zvar").Replace("ZvAr", "zvar").Replace("zVAR", "zvar").Replace("zvAR", "zvar").Replace("zvaR", "zvar")

        If TextoaValidar.Equals(String.Empty) Then Return String.Empty
        While TextoaValidar.ToLower().Contains("zvar")
            Try
                Variable = ObtenerNombreVariable(TextoaValidar)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable: " & Variable)
                If Not String.IsNullOrEmpty(Variable) Then
                    Try

                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(TextoaValidar)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Variable " & Variable & ": " & R)
                        Return ValorVariable
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), String.Empty, RegexOptions.IgnoreCase)
                    End Try
                Else
                    Exit While
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            End Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
        End While

        ValorVariable = Nothing
        Return TextoaValidar
    End Function
    Public Function ObtenerNombreVariable(ByVal TextoaValidar As String) As String
        Dim variable As String
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar").Replace("ZVAr", "zvar").Replace("ZVar", "zvar").Replace("ZVaR", "zvar").Replace("Zvar", "zvar").Replace("ZvAR", "zvar").Replace("ZvaR", "zvar").Replace("ZvAr", "zvar").Replace("zVAR", "zvar").Replace("zvAR", "zvar").Replace("zvaR", "zvar")

        If TextoaValidar <> String.Empty AndAlso TextoaValidar.ToLower().IndexOf("zvar", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a buscar la variable: " & TextoaValidar)
            variable = TextoaValidar.Trim().Remove(0, TextoaValidar.Trim.ToLower().IndexOf("zvar(") + 5)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variable: " & variable)
            variable = variable.Remove(variable.IndexOf(")"))
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variable: " & variable)
            If variable.Contains("(") AndAlso variable.Substring(variable.Length - 1).CompareTo(")") <> 0 Then
                variable &= ")"
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variable: " & variable)
            End If
        Else
            variable = TextoaValidar
        End If
        Return variable
    End Function

    ''' <summary>
    ''' Reconocimiento de variables de tipo texto
    ''' </summary>
    ''' <param name="TextoaValidar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReconocerVariablesValuesSoloTexto(ByVal TextoaValidar As String) As String
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)

        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar").Replace("ZVAr", "zvar").Replace("ZVar", "zvar").Replace("ZVaR", "zvar").Replace("Zvar", "zvar").Replace("ZvAR", "zvar").Replace("ZvaR", "zvar").Replace("ZvAr", "zvar").Replace("zVAR", "zvar").Replace("zvAR", "zvar").Replace("zvaR", "zvar")


        If String.IsNullOrEmpty(TextoaValidar) Then
            Return String.Empty
        End If
        If Not TextoaValidar.ToLower().Contains("zvar(") Then
            Return TextoaValidar
        End If

        Dim R As String = String.Empty
        Dim TextoReconocido As String = TextoaValidar
        Dim ValorVariable As Object
        Dim Variable As String

        While TextoaValidar.ToLower().Contains("zvar(")
            Try
                Variable = ObtenerNombreVariable(TextoaValidar)
                Variable = "zvar(" & Variable & ")"

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable: " & Variable)
                If Variable <> String.Empty Then
                    Try
                        R = String.Empty
                        TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)

                        If ValorVariable Is Nothing OrElse String.IsNullOrEmpty(ValorVariable) Then
                            Return String.Empty
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Variable: " & ValorVariable.ToString.Trim)
                        TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & TextoReconocido)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        TextoaValidar = TextoaValidar.Replace(Variable, String.Empty)
                    End Try
                Else
                    Exit While
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            End Try
        End While

        ValorVariable = Nothing
        Return TextoReconocido
    End Function
End Class