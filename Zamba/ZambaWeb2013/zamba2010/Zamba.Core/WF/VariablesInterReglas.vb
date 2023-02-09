Imports System.Text.RegularExpressions

Public Class VariablesInterReglas

    Private Shared _repo As IZVarsRulesRepo

    Public Shared Property ZVarsRulesRepo() As IZVarsRulesRepo
        Get
            Return _repo
        End Get
        Set(ByVal value As IZVarsRulesRepo)
            _repo = value
        End Set
    End Property



    Public Function ReconocerVariablesAsObject(ByVal TextoaValidar As String) As Object
        Dim R As String = String.Empty
        Dim ValorVariable As Object = Nothing

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
        If TextoaValidar.ToLower().Contains("zvar") = False Then
            ValorVariable = TextoaValidar
        Else
            While TextoaValidar.ToLower().Contains("zvar")
                Try
                    Dim Variable As String = ObtenerNombreVariable(TextoaValidar)
                    Variable = "zvar(" & Variable & ")"
                    If Variable <> String.Empty Then
                        Try
                            R = String.Empty
                            TextoaValidar = TextoaValidar.Replace(Variable, "")
                            ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)
                        Catch ex As Exception
                            TextoaValidar = TextoaValidar.Replace(Variable, "")
                            ZClass.raiseerror(ex)
                        End Try
                    Else
                        Exit While
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                End Try
            End While
        End If

        Return ValorVariable
    End Function

    Public Function ObtenerNombreVariable(ByVal TextoaValidar As String) As String
        Dim variable As String
        If TextoaValidar <> String.Empty AndAlso TextoaValidar.ToLower().IndexOf("zvar") <> -1 Then
            variable = TextoaValidar.Trim().Remove(0, TextoaValidar.Trim.ToLower().IndexOf("zvar(") + 5)
            variable = variable.Remove(variable.IndexOf(")"))
            If variable.Contains("(") AndAlso variable.Substring(variable.Length - 1).CompareTo(")") <> 0 Then
                variable &= ")"
            End If
        Else
            variable = TextoaValidar
        End If
        Return variable
    End Function
    ''' <summary> 
    ''' Reconoce las variables 
    ''' </summary> 
    ''' <param name="TextoaValidar"></param> 
    ''' <returns></returns> 
    ''' <remarks></remarks> 
    Public Function ReconocerVariables(ByVal TextoaValidar As String, Optional ByVal LogFull As Boolean = True) As String
        Dim R As String = String.Empty

        If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
        TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar")
        TextoaValidar = TextoaValidar.Replace("ZVar", "zvar")

        If TextoaValidar = String.Empty Or TextoaValidar = " " Then
            Return TextoaValidar
        End If

        While TextoaValidar.ToLower().Contains("zvar")
            Try
                Dim Variable As String = ObtenerNombreVariable(TextoaValidar)

                If Variable <> String.Empty Then
                    Try
                        R = String.Empty

                        Dim ValorVariable As Object
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(TextoaValidar)

                        If IsNothing(ValorVariable) = False Then
                            If TypeOf (ValorVariable) Is DataSet Then
                                Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        For Each DR As DataRow In ds.Tables(0).Rows
                                            R &= DR.Item(0).ToString & ","
                                        Next
                                    End If
                                End If
                                If R.Length > 0 Then
                                    R = R.Remove(R.Length - 1, 1)
                                End If

                                TextoaValidar = Regex.Replace(TextoaValidar, Regex.Escape("zvar(" & Variable & ")"), R, RegexOptions.IgnoreCase)

                                Else

                                    If Variable.Contains("(") = True AndAlso Variable.Contains(")") = False Then
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

        End While
        If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & TextoaValidar)
        Return TextoaValidar
    End Function
    Public Function ReconocerVariablesValuesSoloTextoAsHashTB(ByVal TextoaValidar As String) As Hashtable
        Dim R As String = String.Empty

        Dim ValorVariable As Object
        Dim cambios As New Hashtable()


        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)
        If String.IsNullOrEmpty(TextoaValidar) Then
            Return cambios
        End If
        If TextoaValidar.ToLower().Contains("zvar") = False Then
            Return cambios
        End If

        While TextoaValidar.ToLower().Contains("zvar")
            Try
                Dim Variable As String = ObtenerNombreVariable(TextoaValidar)
                Variable = "zvar(" & Variable & ")"

                If Variable <> String.Empty Then
                    Try
                        R = String.Empty
                        TextoaValidar = TextoaValidar.Replace(Variable, "")
                        ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)


                        cambios.Add(Variable, ValorVariable.ToString.Trim)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        TextoaValidar = TextoaValidar.Replace(Variable, "")
                    End Try
                Else
                    Exit While
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            End Try
        End While

        Return cambios
    End Function

    Public Function ReconocerVariablesValuesSoloTexto(ByVal TextoaValidar As String) As String
        Try
            TextoaValidar = TextoaValidar.Trim

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconocer Variables")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a Validar: " & TextoaValidar)

            TextoaValidar = ReconocerZambaDot(TextoaValidar)
            TextoaValidar = TextoaValidar.Replace("ZVAR", "zvar").Replace("ZVAr", "zvar").Replace("ZVar", "zvar").Replace("ZVaR", "zvar").Replace("Zvar", "zvar").Replace("ZvAR", "zvar").Replace("ZvaR", "zvar").Replace("ZvAr", "zvar").Replace("zVAR", "zvar").Replace("zvAR", "zvar").Replace("zvaR", "zvar")

            Dim R As String = String.Empty
            Dim TextoReconocido As String = TextoaValidar
            Dim ValorVariable As Object

            If String.IsNullOrEmpty(TextoaValidar) Then
                Return String.Empty
            End If
            If TextoaValidar.ToLower().Contains("zvar") = False Then
                Return TextoaValidar
            End If

            Dim errorcount As Int64 = 0
            While TextoaValidar.ToLower().Contains("zvar")
                Try
                    errorcount = errorcount + 1
                    Dim Variable As String = ObtenerNombreVariable(TextoaValidar)
                    Variable = "zvar(" & Variable & ")"

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable: " & Variable)
                    If Variable <> String.Empty Then
                        Try
                            R = String.Empty
                            TextoaValidar = TextoaValidar.Replace(Variable, "")
                            ValorVariable = WFRuleParent.ObtenerValorVariableObjeto(Variable)

                            If ValorVariable Is Nothing OrElse IsDBNull(ValorVariable) OrElse String.IsNullOrEmpty(ValorVariable) Then
                                ValorVariable = ""
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor Variable: " & ValorVariable.ToString.Trim)
                            TextoReconocido = TextoReconocido.Replace(Variable, ValorVariable.ToString.Trim)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & TextoReconocido)

                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                            TextoReconocido = TextoaValidar.Replace(Variable, "")
                            If (errorcount > 10) Then
                                Exit While
                            End If
                        End Try
                    Else
                        Exit While
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                    If (errorcount > 10) Then
                        Exit While
                    End If
                End Try
            End While
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Reconocido: " & TextoReconocido)
            ValorVariable = Nothing
            Return TextoReconocido
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Falló intento de envio de mail")
            Return String.Empty
        End Try
    End Function

    Public Shared Function ReconocerZambaDot(TextoaValidar As String) As String
        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.id")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.id", Membership.MembershipHelper.CurrentUser.ID.ToString())
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.usuario")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.usuario", Membership.MembershipHelper.CurrentUser.Name)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.nombre")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.nombre", Membership.MembershipHelper.CurrentUser.Nombres)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.apellido")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.apellido", Membership.MembershipHelper.CurrentUser.Apellidos)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.usuarioactual.mail")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.usuarioactual.mail", Membership.MembershipHelper.CurrentUser.eMail.Mail)
        End If

        If (TextoaValidar.ToLower().Contains("zamba.fechaactual")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.fechaactual", DateTime.Now.ToShortDateString())
        End If

        If (TextoaValidar.ToLower().Contains("zamba.temp")) Then
            TextoaValidar = TextoaValidar.Replace("zamba.temp", Membership.MembershipHelper.AppTempPath)
        End If

        Return TextoaValidar
    End Function

    Private Shared ReadOnly Property _VariablesInterReglas() As Hashtable
        Get
            If Not IsNothing(Web.HttpContext.Current) Then
                If Web.HttpContext.Current.Session("VariablesInterReglas") Is Nothing Then
                    Web.HttpContext.Current.Session("VariablesInterReglas") = New Hashtable()
                End If
                Return DirectCast(Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable)
            Else
                Return pVariablesInterReglas
            End If
        End Get
    End Property

    Private Shared pVariablesInterReglas As New Hashtable

    Public Shared Sub Add(ByVal key As Object, ByVal value As Object)

        If Not IsNothing(Web.HttpContext.Current) Then
            If Not IsNothing(_VariablesInterReglas) Then
                DirectCast(Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).Add(key.ToString().ToLower(), value)
            End If
        Else
            _VariablesInterReglas.Add(key.ToString().ToLower(), value)
        End If

        If ZVarsRulesRepo IsNot Nothing Then
            ZVarsRulesRepo.Add(Membership.MembershipHelper.CurrentUser.ID, key.ToString().ToLower(), value, 0)
        End If

        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("VARS: Se agrega la Variable {0} con valor: {1}", key.ToString(), value.ToString()))

    End Sub

    Public Shared Sub Remove(ByVal key As Object)

        If Not IsNothing(Web.HttpContext.Current) Then
            DirectCast(Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).Remove(key.ToString().ToLower())
        Else
            _VariablesInterReglas.Remove(key.ToString().ToLower())
        End If

        If ZVarsRulesRepo IsNot Nothing Then
            ZVarsRulesRepo.Remove()
        End If

    End Sub

    Public Shared Property Item(ByVal key As Object) As Object

        Get
            If Not IsNothing(Web.HttpContext.Current) Then
                Return DirectCast(Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).Item(key.ToString().ToLower())
            Else
                Return _VariablesInterReglas.Item(key.ToString().ToLower())
            End If
        End Get

        Set(ByVal value As Object)

            _VariablesInterReglas.Item(key.ToString().ToLower()) = value

            If ZVarsRulesRepo IsNot Nothing Then
                ZVarsRulesRepo.Update(Membership.MembershipHelper.CurrentUser.ID, key.ToString().ToLower(), value, 0)
            End If

            Try
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("VARS: Se asigna a la Variable {0} con valor: {1}", key.ToString(), If(value Is Nothing, "nulo", value.ToString())))
            Catch ex As Exception
            End Try
        End Set

    End Property

    Public Shared Function ContainsKey(ByVal key As Object) As Boolean
        If Not IsNothing(System.Web.HttpContext.Current) Then
            If Not IsNothing(_VariablesInterReglas) Then
                Return DirectCast(System.Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).ContainsKey(key.ToString().ToLower())
            End If
        Else
            Return _VariablesInterReglas.ContainsKey(key.ToString().ToLower())
        End If
    End Function

    Public Shared Function clone() As Hashtable
        If Not IsNothing(System.Web.HttpContext.Current) Then
            If Not IsNothing(_VariablesInterReglas) Then
                Return DirectCast(System.Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).Clone()
            End If
        Else
            Return _VariablesInterReglas.Clone()
        End If
    End Function

    Public Shared Function Keys() As System.Collections.ICollection
        If Not IsNothing(System.Web.HttpContext.Current) AndAlso Not IsNothing(System.Web.HttpContext.Current.Session("VariablesInterReglas")) Then
            Return DirectCast(System.Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).Keys
        Else
            Return _VariablesInterReglas.Keys
        End If
    End Function

    Public Shared Sub clear()
        If Not IsNothing(System.Web.HttpContext.Current) Then
            DirectCast(System.Web.HttpContext.Current.Session("VariablesInterReglas"), Hashtable).Clear()
        Else
            pVariablesInterReglas = Nothing
            pVariablesInterReglas = New Hashtable()
        End If

        'Todo Ivan: Ver cuando hace clear si hay que borrar todas las variables para el usuario.

    End Sub

End Class
