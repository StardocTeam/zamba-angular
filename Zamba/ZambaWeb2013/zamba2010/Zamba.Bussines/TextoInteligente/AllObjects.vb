Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core
Imports System.Text
Imports Zamba.Membership
Imports System.IO
Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Reflection
Imports Zamba.FileTools.eMail
Imports Zamba.FileTools
Imports Zamba.Framework
Imports Spire.Email
Imports Newtonsoft.Json
Imports Zamba.Core.WF.WF

''' <summary>
''' CLASE QUE ALMACENA OBJETOS A LOS CUALES SE PUEDE ACCEDER DESDE EL INTERPRETER DE ZAMBA
''' PARA USAR EL INTERPRETE DE ZAMBA TENES QUE SEGUIR 3 PASOS:
''' 1) CREAR EL OBJETO EN ESTA CLASE CON SU NOMBRE Y SU TIPO
''' 2a) AGREGAR UN CONTROL DEL TIPO 'TEXTO INTELIGENTE' QUE HEREDA DE UN TEXTBOX QUE ES EL QUE VA A ENCARGARSE DESPUES DE INTERPRETAR EL CODIGO
''' 2b) USAR EL INTELLISENSE DEL TEXTO INTELIGENTE PARA IR CONFIGURANDO EL CODIGO Y AGREGAR LA PROPIEDAD QUE CREASTE EN ESTA CLASE
''' 3) ASEGURARSE DE ACCEDER A ESTA CLASE DESDE ALGUN LADO A ESTA CLASE (PUBLICA, SHARED) Y COMPLETAR LA PROPIEDAD CREADA CON UN VALOR
''' POR EJEMPLO, SE CREO EL OBJETO TAREA DEL TIPO TASKRESULT, CADA VEZ QUE ALGUIEN MIRA UNA TAREA SE REEMPLAZA EL VALOR DE ESTE OBJETO POR EL VALOR DE LA TAREA
''' Y DESDE LA REGLA DE MENSAJES DE ZAMBA SE CONFIGURO UN CONTROL DEL TIPO 'TEXTO INTELIGENTE' PARA QUE DIGA ' NOMBRE DE LA TAREA + <TAREA>.<NOMBRE> '
''' </summary>
''' <remarks></remarks>
Public Class AllObjects

    'Private Shared _Tarea As TaskResult
    'Private Shared _Tareas As System.Collections.Generic.List(Of Core.ITaskResult)
    Private Shared _CommonFunctions As New CommonFunctions

    Public Shared Property Tarea() As TaskResult
        Get
            Return MembershipHelper.GetSessionItem(CacheType.AllObjects, CacheSubType.CurrentTask, String.Empty)
        End Get
        Set(ByVal Value As TaskResult)
            MembershipHelper.SetSessionItem(CacheType.AllObjects, CacheSubType.CurrentTask, String.Empty, Value)
        End Set
    End Property
    Public Shared Property Tareas() As System.Collections.Generic.List(Of Core.ITaskResult)
        Get
            Return MembershipHelper.GetSessionItem(CacheType.AllObjects, CacheSubType.CurrentTasks, String.Empty)
        End Get
        Set(ByVal Value As System.Collections.Generic.List(Of Core.ITaskResult))
            MembershipHelper.SetSessionItem(CacheType.AllObjects, CacheSubType.CurrentTasks, String.Empty, Value)
        End Set
    End Property

    Public Shared ReadOnly Property UsuarioActual() As IUser
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser
        End Get
    End Property

    Public Shared Property FuncionesComunes() As CommonFunctions
        Get
            If _CommonFunctions Is Nothing Then
                _CommonFunctions = New CommonFunctions
            End If
            Return _CommonFunctions
        End Get
        Set(ByVal Value As CommonFunctions)
            _CommonFunctions = Value
        End Set
    End Property


End Class


''' <summary>
''' Utilizado en textoInteligente para mostrar el listado de funciones comunes
''' </summary>
''' <history>Marcelo Modified 22/11/2012</history>
''' <remarks></remarks>
Public Class CommonFunctions

    Dim UserGroupBusiness As New UserGroupBusiness

    Public Shared Property Tarea() As TaskResult
        Get
            Return MembershipHelper.GetSessionItem(CacheType.AllObjects, CacheSubType.CurrentTask, String.Empty)
        End Get
        Set(ByVal Value As TaskResult)
            MembershipHelper.SetSessionItem(CacheType.AllObjects, CacheSubType.CurrentTask, String.Empty, Value)
        End Set
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property UsuarioActualId() As Int64
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser.ID
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerNombreUsuarioxId(ByVal UsuarioId As Int64) As String
        Get
            Dim IsGroup As Boolean
            Return UserGroupBusiness.GetUserorGroupNamebyId(UsuarioId, IsGroup)
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property UsuarioActualNombre() As String
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser.Name
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property UsuarioActualApellido() As String
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser.Apellidos
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property UsuarioActualNombreYApellido() As String
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser.Nombres & " " & Zamba.Membership.MembershipHelper.CurrentUser.Apellidos
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property UsuarioActualMail() As String
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail
        End Get
    End Property

    ''' <summary>
    ''' Devuelve el entorno en que esta la aplicacion (Desarrollo, Test, Produccion)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Ambiente() As String
        Get
            Dim ZOPTB As New ZOptBusiness

            If Not IsNothing(ZOPTB.GetValue("Environment")) Then
                Return ZOPTB.GetValue("Environment")

            Else
                Return "Sin definir"
            End If
            ZOPTB = Nothing
        End Get
    End Property

    ''' <summary>
    ''' Devuelve en que entorno se encuentra la aplicacion (Windows, Web)
    ''' </summary>
    ''' <value></value>
    ''' <history>Marcelo    22/11/12    Created</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Entorno() As String
        Get
            If Membership.MembershipHelper.isWeb = True Then
                Return "Web"
            Else
                Return "Windows"
            End If
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property NombreUsuarioWindows() As String
        Get
            Return Environment.UserName
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property NombrePC() As String
        Get
            Return Environment.MachineName
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property FechaActual() As String
        Get
            Return Now.ToString
        End Get
    End Property
    ''' <summary>
    ''' Se cambio el nombre de esta propiedad porque al momento de querer seleccionarla en el admisnitrador
    ''' para configurarla no sesabia cual de las dos era para formato en la fecha y ademas desde la ejecucion 
    ''' tiraba exception porque iba siempre a la propiedad que no tiene ningun parametro para recibir.
    ''' [Sebastian 23/12/2008]
    ''' </summary>
    ''' <param name="Formato"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property FechaActualConFormato(ByVal Formato As String) As String
        'Public  ReadOnly Property FechaActual(ByVal Formato As String) As String
        Get
            Return Now.ToString(Formato)
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property FormatoFecha(ByVal Fecha As String, ByVal Formato As String) As String
        Get
            Try
                Dim dateFecha As Date
                If Date.TryParse(Fecha, dateFecha) Then
                    If Formato = "yyyyMMdd" Then
                        Dim value As String
                        If dateFecha.Month < 10 Then
                            value = dateFecha.Year & "0" & dateFecha.Month
                        Else
                            value = dateFecha.Year & dateFecha.Month
                        End If
                        If dateFecha.Day < 10 Then
                            value = value & "0" & dateFecha.Day
                        Else
                            value = value & dateFecha.Day
                        End If
                        Return value
                    Else
                        Return dateFecha.ToString(Formato)
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La fecha ingresada no es valida")
                    Return Fecha
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en fecha" & ex.ToString)
                Return Fecha
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Sumar(ByVal A As String, ByVal B As String) As String
        Get
            Try
                Return Decimal.Parse(A) + Decimal.Parse(B)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al sumar" & ex.ToString)
                Return 0
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Restar(ByVal A As String, ByVal B As String) As String
        Get
            Try
                Return Decimal.Parse(A) - Decimal.Parse(B)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al restar" & ex.ToString)
                Return 0
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Multiplicar(ByVal A As String, ByVal B As String) As String
        Get
            Try
                Return Decimal.Parse(A) * Decimal.Parse(B)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al multiplicar" & ex.ToString)
                Return 0
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Dividir(ByVal A As String, ByVal B As String) As String
        Get
            Try
                Return Decimal.Parse(A) / Decimal.Parse(B)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al dividir" & ex.ToString)
                Return 0
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Reemplazar(ByVal Original As String, ByVal A As String, ByVal B As String) As String
        Get
            Try
                Return Original.Replace(A, B)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Reemplazar: " & ex.ToString)
                Return Original
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property QuitarEspacios(ByVal Original As String) As String
        Get
            Try
                Return Original.Replace(" ", String.Empty)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Reemplazar: " & ex.ToString)
                Return Original
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerPartePalabra(ByVal palabra As String, ByVal caracteres As String, ByVal parte As String) As String
        Get
            Try
                Dim posicion As Int32

                If palabra.ToLower().Contains("zvar") Then
                    Dim VarInterReglas As New VariablesInterReglas()
                    palabra = VarInterReglas.ReconocerVariablesValuesSoloTexto(palabra)
                    VarInterReglas = Nothing
                End If

                If palabra.ToLower().Contains(caracteres.ToLower()) Then
                    If Int32.TryParse(parte, posicion) Then
                        Return palabra.Split(caracteres)(posicion)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La posicion es invalida: " & parte)
                        Return palabra
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La palabra: " & palabra & " no contiene los caracteres: " & caracteres)
                    Return palabra
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo: " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Subcadena(ByVal palabra As String, ByVal buscar As String, ByVal desde As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                If palabra.ToLower().Contains(buscar.ToLower()) Then
                    Return palabra.Substring(palabra.IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) + buscar.Length, desde)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La palabra: " & palabra & " no contiene los caracteres: " & buscar)
                    Return palabra
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo: " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Palabra", "Desde"})>
    Public ReadOnly Property SubcadenaHasta(ByVal palabra As String, ByVal hasta As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                Return palabra.Substring(0, hasta)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo:  " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Palabra", "Desde"})>
    Public ReadOnly Property SubcadenaDesdeHasta(ByVal palabra As String, ByVal desde As String, ByVal hasta As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                Return palabra.Substring(desde, hasta)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo:  " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property UbicacionDe(ByVal palabra As String, ByVal buscar As String, ByVal desde As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                If palabra.ToLower().Contains(buscar.ToLower()) Then
                    Return palabra.IndexOf(buscar, Integer.Parse(desde), StringComparison.CurrentCultureIgnoreCase)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La palabra: " & palabra & " no contiene los caracteres: " & buscar)
                    Return palabra
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo: " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property SubcadenaHastaFinal(ByVal palabra As String, ByVal buscar As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                If palabra.ToLower().Contains(buscar.ToLower()) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "palabra: " & palabra)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "buscar: " & buscar)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "palabra.IndexOf(buscar): " & palabra.IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase))
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "buscar.Length: " & buscar.Length)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "palabra.Length: " & palabra.Length)
                    Dim Desde, Hasta As Integer
                    Desde = palabra.IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) + buscar.Length
                    Hasta = palabra.Length - (palabra.IndexOf(buscar, StringComparison.CurrentCultureIgnoreCase) + buscar.Length)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Desde: " & Desde)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Hasta: " & Hasta)
                    Return palabra.Substring(Desde, Hasta)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La palabra: " & palabra & " no contiene los caracteres: " & buscar)
                    Return palabra
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo: " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExtraerNumeros(ByVal palabra As String, ByVal Separador As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                Dim resultado As String
                Dim palabras As String() = palabra.Split(" ")
                For Each subpalabra As String In palabras
                    If IsNumeric(subpalabra) Then
                        resultado &= subpalabra & Separador
                    End If
                Next
                Return resultado
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo: " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExtraerPrimerosNumeros(ByVal palabra As String, ByVal Separador As String) As String
        Get
            Try
                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
                End If

                Dim resultado As String
                Dim palabras As String() = palabra.Split(" ")
                For Each subpalabra As String In palabras
                    If IsNumeric(subpalabra.Trim) Then
                        resultado &= subpalabra & Separador
                    ElseIf subpalabra.Trim().Length > 0 Then
                        Exit For
                    End If
                Next
                Return resultado
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en parseo: " & ex.ToString())
                Return palabra
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Contiene(ByVal nombre As String, ByVal valor As String) As Boolean
        Get
            Try
                Dim VarInterReglas As New VariablesInterReglas()
                Dim A As Object = VarInterReglas.ReconocerVariablesAsObject(nombre)
                VarInterReglas = Nothing

                If Not IsNothing(A) Then
                    If String.IsNullOrEmpty(valor) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El comparador esta vacio")
                    Else
                        If TypeOf (A) Is DataTable Then
                            Dim strFilter As New StringBuilder()
                            Dim strValues As String() = valor.Split("§")

                            For i As Int32 = 0 To DirectCast(A, DataTable).PrimaryKey.Length - 1
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Columna: " & DirectCast(A, DataTable).PrimaryKey(i).ColumnName)
                                strFilter.Append(DirectCast(A, DataTable).PrimaryKey(i).ColumnName)
                                strFilter.Append(" in (")
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & strValues(i))
                                strFilter.Append(strValues(i))
                                strFilter.Append(")")
                            Next

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Filtro: " & strFilter.ToString())
                            DirectCast(A, DataTable).DefaultView.RowFilter = strFilter.ToString()

                            strFilter = Nothing
                            strValues = Nothing

                            If DirectCast(A, DataTable).DefaultView.ToTable().Rows.Count > 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor se encuentra en la tabla")
                                Return True
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor no se encuentra en la tabla")
                                Return False
                            End If
                        ElseIf TypeOf (A) Is DataSet Then
                            Dim strFilter As New StringBuilder()
                            Dim strValues As String() = valor.Split("§")

                            For i As Int32 = 0 To DirectCast(A, DataSet).Tables(0).PrimaryKey.Length
                                strFilter.Append(DirectCast(A, DataSet).Tables(0).PrimaryKey(i).ColumnName)
                                strFilter.Append(" in (")
                                strFilter.Append(strValues(i))
                                strFilter.Append(")")
                            Next

                            DirectCast(A, DataSet).Tables(0).DefaultView.RowFilter = strFilter.ToString()

                            strFilter = Nothing
                            strValues = Nothing

                            If DirectCast(A, DataSet).Tables(0).DefaultView.ToTable().Rows.Count > 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor se encuentra en el set de datos")
                                Return True
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor no se encuentra en el set de datos")
                                Return False
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor se encuentra en la cadena de caracteres: " & A.ToString().Contains(valor))
                            Return A.ToString().Contains(valor)
                        End If
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La variable esta vacia")
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al buscar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EsNumerico(ByVal texto As String) As Boolean
        Get
            Try
                If texto.Length = 0 Then Return False
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If IsNumeric(A) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} es Numerico", A))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} NO es Numerico", A))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EsFecha(ByVal texto As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If IsDate(A) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} es Fecha", A))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} NO es Fecha", A))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EsNulo(ByVal texto As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If String.IsNullOrEmpty(A) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} es Nulo", A))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} NO es Nulo", A))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property SuLongitudEsIgualA(ByVal texto As String, ByVal Longitud As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If A.Length = Longitud Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1}", A, A.Length))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} diferente de {2}", A, A.Length, Longitud))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property SuLongitudEsMayorQue(ByVal texto As String, ByVal Longitud As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If A.Length > Longitud Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} mayor de {2}", A, A.Length, Longitud))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} menor de {2}", A, A.Length, Longitud))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property SuLongitudEsMenorQue(ByVal texto As String, ByVal Longitud As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If A.Length < Longitud Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} menor de {2}", A, A.Length, Longitud))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} mayor de {2}", A, A.Length, Longitud))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EsUnNumeroMenorQue(ByVal texto As String, ByVal valor As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If Integer.Parse(A) < valor Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} menor de {2}", A, A.Length, valor))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} mayor de {2}", A, A.Length, valor))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EsUnNumeroMayorQue(ByVal texto As String, ByVal valor As String) As Boolean
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)

                If Integer.Parse(A) > valor Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} menor de {2}", A, A.Length, valor))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El valor {0} tiene una longitud de {1} mayor de {2}", A, A.Length, valor))
                    Return False
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al validar valor " & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ConsultaABase(ByVal texto As String) As String
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)
                Dim x As Object = Servers.Server.Con.ExecuteScalar(CommandType.Text, A)

                If IsDBNull(x) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se obtuvo valor")
                    Return String.Empty
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se obtuvo valor: {0}", x))
                    Return x.ToString
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Ejecutar Consulta " & ex.ToString)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EjecutaEnBase(ByVal texto As String) As Integer
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)
                Dim x As Integer = Servers.Server.Con.ExecuteNonQuery(CommandType.Text, A)
                Return x
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Ejecutar Consulta " & ex.ToString)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EjecutaEnBaseConRetornodeValor(ByVal texto As String) As String
        Get
            Try
                Dim A As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(texto)
                Dim x As String = Servers.Server.Con.ExecuteScalar(CommandType.Text, A)
                Return x
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Ejecutar Consulta " & ex.ToString)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property PasarAMinusculas(ByVal text As String) As String
        Get
            Dim toLowerText As String
            Try
                toLowerText = text.ToLower()
                If toLowerText.Contains("zvar") Then
                    Dim VarInterReglas As New VariablesInterReglas()
                    text = VarInterReglas.ReconocerVariablesValuesSoloTexto(text)
                    VarInterReglas = Nothing
                End If
                Return toLowerText
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property PasarAMayusculas(ByVal text As String) As String
        Get
            Dim toUpperText As String
            Try
                toUpperText = text.ToUpper()
                text = WFRuleParent.ReconocerVariablesValuesSoloTexto(text)
                Return toUpperText
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerPosicion(ByVal Palabra As String, PalabraBuscada As String, ByVal BuscarDesde As String) As String
        Get
            Try
                PalabraBuscada = WFRuleParent.ReconocerVariablesValuesSoloTexto(PalabraBuscada)
                Return Palabra.IndexOf(PalabraBuscada, Integer.Parse(BuscarDesde), StringComparison.CurrentCultureIgnoreCase)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Buscar Posicion: " & ex.ToString)
                Throw ex
            End Try
        End Get
    End Property

    'Se utiliza en la DoExplorer para guardar si esta abierto el explorador
    '  Private _TasksVariables As New Generic.Dictionary(Of Long, Generic.Dictionary(Of String, Object))
    Private Property TasksVariables() As Generic.Dictionary(Of Long, Generic.Dictionary(Of String, Object))
        Get
            Return Membership.MembershipHelper.GetSessionItem(CacheType.AllObjects, CacheSubType.TasksVariables, String.Empty)
        End Get
        Set(value As Generic.Dictionary(Of Long, Generic.Dictionary(Of String, Object)))
            Membership.MembershipHelper.SetSessionItem(CacheType.AllObjects, CacheSubType.TasksVariables, String.Empty, value)
        End Set
    End Property

    ''' <summary>
    ''' Se utiliza en la DoExplorer para guardar si esta abierto el explorador
    ''' </summary>
    ''' <param name="VariableName"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Property VariablesxTarea(ByVal VariableName As String) As String
        Get
            If Not AllObjects.Tarea Is Nothing Then
                If TasksVariables.ContainsKey(AllObjects.Tarea.ID) Then
                    If TasksVariables(AllObjects.Tarea.ID).ContainsKey(VariableName) Then
                        Return TasksVariables(AllObjects.Tarea.ID)(VariableName)
                    Else
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            If Not AllObjects.Tarea Is Nothing Then
                If TasksVariables.ContainsKey(AllObjects.Tarea.ID) Then
                    If TasksVariables(AllObjects.Tarea.ID).ContainsKey(VariableName) Then
                        TasksVariables(AllObjects.Tarea.ID)(VariableName) = value
                    Else
                        TasksVariables(AllObjects.Tarea.ID).Add(VariableName, value)
                    End If
                Else
                    TasksVariables.Add(AllObjects.Tarea.ID, New Generic.Dictionary(Of String, Object))
                    TasksVariables(AllObjects.Tarea.ID).Add(VariableName, value)
                End If
            End If
        End Set
    End Property


    ''' <summary>
    ''' Se utiliza en la DoExplorer para marcar si esta abierto o no el explorador
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property PantallaTieneAbiertoelExplorerenTarea() As Boolean
        Get
            Try
                If String.IsNullOrEmpty(VariablesxTarea("ExplorerIsOpen")) Then Return False
                Return VariablesxTarea("ExplorerIsOpen")
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener variable de la tarea" & ex.ToString)
                Return False
            End Try
        End Get
    End Property

    'todo: metodo que me de el mail de un usuario

    'todo: Hacer stores genericos en la base de datos, para traer consultas complejas de las tareas.
    'y hacer los metodos de acceso para texto inteligente
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property HTMLEncode(ByVal text As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                Dim value As String = VarInterReglas.ReconocerVariables(text)
                Return Web.HttpUtility.HtmlEncode(value)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property DecodificarBase64(ByVal text As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                Dim value As Byte() = Convert.FromBase64String(VarInterReglas.ReconocerVariables(text))
                Dim convertedValue = System.Text.Encoding.UTF8.GetString(value)
                Return convertedValue
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property DecodeText(ByVal text As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                Dim tag As Byte() = DirectCast(VarInterReglas.ReconocerVariablesAsObject(text), Byte())
                Dim mstream As New MemoryStream(tag)
                Dim buffer As [Byte]() = New [Byte](tag.Length) {}
                Dim n As Integer = mstream.Read(buffer, 0, buffer.Length)
                mstream.Close()
                Dim convertedValue = System.Text.Encoding.Unicode.GetString(buffer)
                Return convertedValue
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property DecodificarArchivo(ByVal RutaArchivo As String) As String
        Get
            Try
                Dim text As String
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)
                If File.Exists(RutaArchivo) Then
                    Dim sr As New StreamReader(RutaArchivo)
                    text = sr.ReadToEnd()
                    sr.Close()
                    sr.Dispose()
                End If
                Dim tag As Byte() = DirectCast(VarInterReglas.ReconocerVariablesAsObject(text), Byte())
                Dim mstream As New MemoryStream(tag)
                Dim buffer As [Byte]() = New [Byte](tag.Length) {}
                Dim n As Integer = mstream.Read(buffer, 0, buffer.Length)
                mstream.Close()
                Dim convertedValue = System.Text.Encoding.Unicode.GetString(buffer)
                Return convertedValue
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property LeerArchivo(ByVal RutaArchivo As String) As String
        Get
            Try
                Dim text As String
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)
                If File.Exists(RutaArchivo) Then
                    Dim sr As New StreamReader(RutaArchivo)
                    text = sr.ReadToEnd()
                    sr.Close()
                    sr.Dispose()
                End If
                text = VarInterReglas.ReconocerVariablesValuesSoloTexto(text)
                Return text
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EscribirArchivo(ByVal RutaArchivo As String, ByVal Texto As String) As String
        Get
            Try
                Dim text As String
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)
                text = VarInterReglas.ReconocerVariablesValuesSoloTexto(text)
                Dim sr As New StreamWriter(RutaArchivo)
                sr.Write(text)
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Return RutaArchivo
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExportarReporteATxt(ByVal RutaArchivo As String, ByVal IdReporte As String, ByVal Separator As String) As String
        Get
            Try
                If RutaArchivo.StartsWith("'") AndAlso RutaArchivo.EndsWith("'") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If

                If RutaArchivo.StartsWith("""") AndAlso RutaArchivo.EndsWith("""") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If


                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                IdReporte = VarInterReglas.ReconocerVariablesValuesSoloTexto(IdReporte)

                Dim RB As New ReportBuilderComponent
                Dim resultado As DataSet = RB.RunQueryBuilderReporteGeneral(IdReporte, Nothing, Tarea)

                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)

                Dim sr As New StreamWriter(RutaArchivo)
                For Each r As DataRow In resultado.Tables(0).Rows
                    For Each c As DataColumn In resultado.Tables(0).Columns
                        sr.Write(r(c).ToString())
                        sr.Write(Separator)
                    Next
                    sr.Write(sr.NewLine)
                Next
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Return RutaArchivo
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExportarTablaAExcel(ByVal Directorio As String, ByVal VariableTabla As String, ByVal NombreArchivo As String) As String
        Get
            Try
                If Directorio.StartsWith("'") AndAlso Directorio.EndsWith("'") Then
                    Directorio = Directorio.Substring(0, 1)
                    Directorio = Directorio.Substring(0, Directorio.Length - 2)
                End If

                If Directorio.StartsWith("""") AndAlso Directorio.EndsWith("""") Then
                    Directorio = Directorio.Substring(0, 1)
                    Directorio = Directorio.Substring(0, Directorio.Length - 2)
                End If


                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                Dim dt As DataTable = DirectCast(VarInterReglas.ReconocerVariablesAsObject(VariableTabla), DataTable)


                Directorio = VarInterReglas.ReconocerVariablesValuesSoloTexto(Directorio)
                NombreArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(NombreArchivo)


                Dim engines As Assembly = Assembly.LoadFile(Zamba.Membership.MembershipHelper.StartUpPath & "\Zamba.FileTools.dll")
                Dim classTypes As Type = engines.GetType("Zamba.FileTools.SpireTools", True, True)
                engines = Nothing

                Dim Spire As ISpireTools = Activator.CreateInstance(classTypes)
                classTypes = Nothing

                Spire.ExportToXLS(dt, Path.Combine(Directorio, NombreArchivo))

                Return Directorio
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExportarReporteAExcel(ByVal RutaArchivo As String, ByVal IdReporte As String) As String
        Get
            Try
                If RutaArchivo.StartsWith("'") AndAlso RutaArchivo.EndsWith("'") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If

                If RutaArchivo.StartsWith("""") AndAlso RutaArchivo.EndsWith("""") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If


                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                IdReporte = VarInterReglas.ReconocerVariablesValuesSoloTexto(IdReporte)

                Dim RB As New ReportBuilderComponent
                Dim resultado As DataSet = RB.RunQueryBuilderReporteGeneral(IdReporte, Nothing, Tarea)

                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)


                Dim engine As Assembly = Assembly.LoadFile(Zamba.Membership.MembershipHelper.StartUpPath & "\Zamba.FileTools.dll")
                Dim classType As Type = engine.GetType("Zamba.FileTools.SpireTools", True, True)
                engine = Nothing

                Dim Spire As ISpireTools = Activator.CreateInstance(classType)
                classType = Nothing

                Spire.ExportToXLS(resultado.Tables(0), RutaArchivo)

                Return RutaArchivo
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExportarReporteAXlsx(ByVal RutaArchivo As String, ByVal IdReporte As String) As String
        Get
            Try
                If RutaArchivo.StartsWith("'") AndAlso RutaArchivo.EndsWith("'") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If

                If RutaArchivo.StartsWith("""") AndAlso RutaArchivo.EndsWith("""") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If


                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                IdReporte = VarInterReglas.ReconocerVariablesValuesSoloTexto(IdReporte)

                Dim RB As New ReportBuilderComponent
                Dim resultado As DataSet = RB.RunQueryBuilderReporteGeneral(IdReporte, Nothing, Tarea)

                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)


                Dim engine As Assembly = Assembly.LoadFile(Zamba.Membership.MembershipHelper.StartUpPath & "\Zamba.FileTools.dll")
                Dim classType As Type = engine.GetType("Zamba.FileTools.SpireTools", True, True)
                engine = Nothing

                Dim Spire As ISpireTools = Activator.CreateInstance(classType)
                classType = Nothing

                Spire.ExportToXLSx(resultado.Tables(0), RutaArchivo)

                Return RutaArchivo
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExportarReporteACSV(ByVal RutaArchivo As String, ByVal IdReporte As String) As String
        Get
            Try
                If RutaArchivo.StartsWith("'") AndAlso RutaArchivo.EndsWith("'") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If

                If RutaArchivo.StartsWith("""") AndAlso RutaArchivo.EndsWith("""") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If


                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                IdReporte = VarInterReglas.ReconocerVariablesValuesSoloTexto(IdReporte)

                Dim RB As New ReportBuilderComponent
                Dim resultado As DataSet = RB.RunQueryBuilderReporteGeneral(IdReporte, Nothing, Tarea)

                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)


                Dim engine As Assembly = Assembly.LoadFile(Zamba.Membership.MembershipHelper.StartUpPath & "\Zamba.FileTools.dll")
                Dim classType As Type = engine.GetType("Zamba.FileTools.SpireTools", True, True)
                engine = Nothing

                Dim Spire As ISpireTools = Activator.CreateInstance(classType)
                classType = Nothing

                Spire.ExportToCSV(resultado.Tables(0), RutaArchivo)

                Return RutaArchivo
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property



    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerRutaTareaxIdTarea(ByVal TaskId As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                TaskId = VarInterReglas.ReconocerVariables(TaskId)
                Dim WFTB As New Zamba.Core.WF.WF.WFTaskBusiness
                Dim Task As ITaskResult = WFTB.GetTask(TaskId, Membership.MembershipHelper.CurrentUser.ID)
                WFTB = Nothing
                If Task IsNot Nothing Then
                    Return Task.FullPath
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property CambiarDocumento(ByVal TaskId As String, ByVal Archivo As String) As String
        Get
            Try
                Dim RB As New Results_Business

                Dim WFTB As New Zamba.Core.WF.WF.WFTaskBusiness
                Dim Result As ITaskResult = WFTB.GetTask(TaskId, Membership.MembershipHelper.CurrentUser.ID)
                WFTB = Nothing
                If Result IsNot Nothing Then
                    Dim ZC As New ZCore
                    'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento
                    If ZC.filterVolumes(Result.Disk_Group_Id).VolumeType = VolumeTypes.DataBase Then
                        'Se reemplaza el documento en Zamba
                        Result.EncodedFile = FileEncode.Encode(Archivo)
                        Result.File = Archivo
                        Result.Doc_File = Result.ID & Path.GetExtension(Archivo)
                        Result.IconId = RB.GetFileIcon(Result.File)
                        Dim VolumeListId As Int64 = VolumesBusiness.GetVolumeListId(Result.DocTypeId)
                        Dim DsVols As DataSet = VolumeListsBusiness.GetActiveDiskGroupVolumes(VolumeListId)
                        Dim RF As New Results_Factory
                        RF.ReplaceDocument(Result, New FileInfo(Result.Doc_File), String.Empty, True, Nothing)
                        RF = Nothing
                        'Se copia al temporal para ser abierto al refrescar la tarea
                        Dim dirInfo As DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp")
                        Dim destPath As String = dirInfo.FullName & "\" & Result.Doc_File
                        File.Copy(Archivo, destPath, True)
                        dirInfo = Nothing
                    Else
                        'Se completa la ruta del documento a reemplazante
                        If String.IsNullOrEmpty(Result.FullPath) Then
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reemplazo de Resultado sin documento previo")
                            Dim filename As String = Path.GetFileName(Archivo)
                            Dim volListID As Int32 = VolumesBusiness.GetVolumeListId(Result.DocTypeId)
                            Dim DsVols As DataSet = VolumeListsBusiness.GetActiveDiskGroupVolumes(volListID)
                            Dim volume As IVolume = VolumesBusiness.LoadVolume(Result.DocTypeId, DsVols)

                            Result.File = volume.path & "\" & Result.DocTypeId & "\" & VolumesBusiness.GetOffSet(volume) & "\" & filename
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Archivo: " & Result.File)

                            DsVols.Dispose()
                            DsVols = Nothing
                            volume.Dispose()
                            volume = Nothing
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reemplazo de Resultado con documento previo")
                            Result.File = Archivo
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Archivo: " & Result.File)
                        End If

                        'Se reemplaza el documento y se refresca la tarea completa
                        RB.ReplaceDocument(DirectCast(Result, TaskResult), Archivo, True, Nothing)
                    End If
                    ZC = Nothing
                    Dim UB As New UserBusiness
                    UB.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Edit, Result.Name)
                    UB = Nothing
                End If
            Catch ex As IOException
                ZClass.raiseerror(ex)
                Throw ex
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Throw ex
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerRutaTareaxIdDocumento(ByVal DocId As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                Dim WFTB As New Zamba.Core.WF.WF.WFTaskBusiness
                Dim Task As ITaskResult = WFTB.GetTaskByDocId(DocId, Membership.MembershipHelper.CurrentUser.ID)
                WFTB = Nothing
                If Task IsNot Nothing Then
                    Return Task.FullPath
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerRutaDocumentoxIdDocumentoYIdEntidad(ByVal DocId As String, ByVal EntityId As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)
                Dim WFTB As New Results_Business
                Dim Result As IResult = WFTB.GetResult(DocId, EntityId, False)
                WFTB = Nothing
                If Result IsNot Nothing Then
                    Return Result.FullPath
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExtraerAdjuntosComoArchivos(ByVal DocId As String, ByVal EntityId As String, ByVal newFilesDirectory As String) As List(Of String)
        Get

            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)
                newFilesDirectory = VarInterReglas.ReconocerVariables(newFilesDirectory)
                If Directory.Exists(newFilesDirectory) = False Then
                    Directory.CreateDirectory(newFilesDirectory)
                End If

                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(DocId, EntityId, False)

                If Result IsNot Nothing Then
                    Dim MB As New MessagesBusiness
                    'Return MB.ExtractAttachsAsFiles(Result.FullPath, newFilesDirectory, New List(Of String))
                    Return MB.ExtractAttachsAsFilesWithSpire(CreateInstance("zamba.filetools.dll", "Zamba.FileTools.SpireTools"), Result.FullPath, newFilesDirectory, New List(Of String))
                    ' Return String.Empty
                Else
                    'Return String.Empty
                    Return New List(Of String)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                'Return String.Empty
                Return New List(Of String)
            End Try

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Id de Documento", "Id de Entidad", "RutaDestino"})>
    Public ReadOnly Property ExtraerAdjuntosComoArchivosPorExtension(ByVal DocId As String, ByVal EntityId As String, ByVal newFilesDirectory As String, ByVal AllowedExtensions As String) As List(Of String)
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)
                newFilesDirectory = VarInterReglas.ReconocerVariables(newFilesDirectory)

                If Directory.Exists(newFilesDirectory) = False Then
                    Directory.CreateDirectory(newFilesDirectory)
                End If

                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(DocId, EntityId, False)

                If Result IsNot Nothing Then
                    Dim MB As New MessagesBusiness
                    Dim ListOfExtensions As New List(Of String)
                    For Each ext As String In AllowedExtensions.Replace("'", "").Split(";")
                        ListOfExtensions.Add(ext)
                    Next
                    'Return MB.ExtractAttachsAsFiles(Result.FullPath, newFilesDirectory, ListOfExtensions)
                    Return MB.ExtractAttachsAsFilesWithSpire(CreateInstance("zamba.filetools.dll", "Zamba.FileTools.SpireTools"), Result.FullPath, newFilesDirectory, ListOfExtensions)
                    ' Return String.Empty
                Else
                    'Return String.Empty
                    Return New List(Of String)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                'Return String.Empty
                Return New List(Of String)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerCantidadDeAdjuntosEnMail(ByVal DocId As String, ByVal EntityId As String) As Int64
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)

                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(DocId, EntityId, False)

                If Result IsNot Nothing Then
                    Dim MB As New MessagesBusiness
                    'Return MB.CountAttachsAsFiles(Result.FullPath, New List(Of String))
                    Return MB.CountAttachsAsFilesWithSpire(CreateInstance("zamba.filetools.dll", "Zamba.FileTools.SpireTools"), Result.FullPath, New List(Of String))
                Else
                    Return 0
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return 0
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerCantidadDeAdjuntosEnMailPorExtension(ByVal DocId As String, ByVal EntityId As String, ByVal AllowedExtensions As String) As Int64
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)

                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(DocId, EntityId, False)

                If Result IsNot Nothing Then
                    Dim MB As New MessagesBusiness
                    Dim ListOfExtensions As New List(Of String)
                    For Each ext As String In AllowedExtensions.Replace("'", "").Split(";")
                        ListOfExtensions.Add(ext)
                    Next
                    'Return MB.CountAttachsAsFiles(Result.FullPath, ListOfExtensions)
                    Return MB.CountAttachsAsFilesWithSpire(CreateInstance("zamba.filetools.dll", "Zamba.FileTools.SpireTools"), Result.FullPath, ListOfExtensions)
                Else
                    Return 0
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return 0
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExtraerPrimerAdjuntoenMail(ByVal DocId As String, ByVal EntityId As String, ByVal newFilesDirectory As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)

                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(DocId, EntityId, False)

                If Directory.Exists(newFilesDirectory) = False Then
                    Directory.CreateDirectory(newFilesDirectory)
                End If
                If Result IsNot Nothing Then
                    Dim MB As New MessagesBusiness
                    'Return MB.ExtractFirstAttachsInMail(Result.FullPath, newFilesDirectory, New List(Of String))
                    Return MB.ExtractFirstAttachsInMailWithSpire(CreateInstance("zamba.filetools.dll", "Zamba.FileTools.SpireTools"), Result.FullPath, newFilesDirectory, New List(Of String))
                Else
                    Return String.Empty
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ExtraerPrimerAdjuntoenMailPorExtension(ByVal DocId As String, ByVal EntityId As String, ByVal newFilesDirectory As String, ByVal AllowedExtensions As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)

                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(DocId, EntityId, False)

                If Directory.Exists(newFilesDirectory) = False Then
                    Directory.CreateDirectory(newFilesDirectory)
                End If
                If Result IsNot Nothing Then
                    Dim MB As New MessagesBusiness
                    Dim ListOfExtensions As New List(Of String)
                    For Each ext As String In AllowedExtensions.Replace("'", "").Split(";")
                        ListOfExtensions.Add(ext)
                    Next
                    'Return MB.ExtractFirstAttachsInMail(Result.FullPath, newFilesDirectory, ListOfExtensions)
                    Return MB.ExtractFirstAttachsInMailWithSpire(CreateInstance("zamba.filetools.dll", "Zamba.FileTools.SpireTools"), Result.FullPath, newFilesDirectory, ListOfExtensions)
                Else
                    Return String.Empty
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property SeleccionarArchivoDelDisco() As String
        Get
            Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
            Dim FD As New System.Windows.Forms.OpenFileDialog
            If FD.ShowDialog() = DialogResult.OK Then
                Return FD.FileName
            End If
            Throw New Exception("El usuario cancelo la operacion")
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property SeleccionarCarpetaDelDisco() As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                Dim FD As New System.Windows.Forms.FolderBrowserDialog
                If FD.ShowDialog() = DialogResult.OK Then
                    Return FD.SelectedPath
                End If
                Return String.Empty
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property CrearCarpeta(ByVal RutadeCarpeta As String) As String
        Get
            Try
                Dim dir As New DirectoryInfo(RutadeCarpeta)
                If dir.Exists = False Then dir.Create()
                Return dir.FullName
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property BuscarArchivoEnDisco() As String
        Get
            Try

                Dim FiDlg As New OpenFileDialog
                If FiDlg.ShowDialog() = DialogResult.OK Then
                    Return FiDlg.FileName
                End If
                Return String.Empty
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property BuscarArchivoEnDiscoPorExtension(ByVal Filters As String) As String
        Get
            Try
                Filters = Filters.Replace("'", "")
                Dim FiDlg As New OpenFileDialog
                If Filters <> String.Empty Then
                    FiDlg.Filter = Filters.Replace("-", "|")
                End If
                If FiDlg.ShowDialog() = DialogResult.OK Then
                    Return FiDlg.FileName
                End If
                Return String.Empty
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ValidarExtension(ByVal File As String, ByVal Filters As String) As Boolean
        Get
            Try
                Filters = Filters.Replace("'", "")
                If Filters.Length > 0 Then
                    For Each s As String In Filters.Split(";")
                        If Filters.Contains(s.Replace("*", "")) Then
                            Return True
                        End If
                    Next
                End If
                Return False
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property CopiarDocumento(ByVal EntityId As Int64, ByVal DocId As Int64, ByVal DirectorioDestino As String, ByVal NombreArchivo As String) As String
        Get
            Try
                Dim RB As New Results_Business
                Dim Result As IResult = RB.GetResult(EntityId, DocId, True)
                RB = Nothing
                If Result IsNot Nothing Then
                    Dim dirInfo As New DirectoryInfo(DirectorioDestino)
                    If dirInfo.Exists = False Then
                        dirInfo.Create()
                    End If
                    Dim ResultPath As String = Results_Business.CreateTempFile(Result)
                    Dim ResultFile As New FileInfo(ResultPath)
                    Dim destPath As String = dirInfo.FullName & "\" & NombreArchivo & "." & ResultFile.Extension
                    ResultFile.CopyTo(destPath, True)
                    dirInfo = Nothing
                    ResultFile = Nothing
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Throw ex
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property CopiarArchivo(originalFile As String, targetFile As String) As Boolean
        Get
            Try
                If Not String.IsNullOrEmpty(originalFile) AndAlso Not String.IsNullOrEmpty(targetFile) Then
                    If File.Exists(originalFile) Then
                        File.Copy(originalFile, targetFile, True)
                        If File.Exists(targetFile) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Copia de archivo creada correctamente")
                            Return True
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo generar copia de archivo")
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo copiar archivo, original no existe")
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo crear copia de archivo, path original o nombre de archivo destino es nulo o vacio")
                End If
                Return False
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerNombreEntidadxIdEntidad() As String
        Get
            Try
                Dim DTB As New DocTypesBusiness
                Dim EntityName As String = DTB.GetDocTypeName(AllObjects.Tarea.DocTypeId)
                DTB = Nothing
                Return EntityName
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property



    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerLinkWeb(ByVal nombre As String) As String
        Get
            Try
                Dim sb As New StringBuilder
                Dim url As String = ZOptBusiness.GetValueOrDefault("ThisDomain", "http://imageapd/zamba.web")
                sb.Append("<style>.btn,body{font-weight:400;line-height:1.5}<style>.btn:not(:disabled):not(.disabled){cursor:pointer}[type=reset],[type=submit],button,html [type=button]{-webkit-appearance:button}.btn-outline-success{color:#28a745;background-color:transparent;background-image:none;border-color:#28a745}.btn{display:inline-block;text-align:center;white-space:nowrap;vertical-align:middle;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;border:1px solid transparent;padding:.375rem .75rem;font-size:1rem;border-radius:.25rem;transition:color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out}.btn:hover{color:#fff;background-color:#28a745;border-color:#28a745}button,select{text-transform:none}button,input{overflow:visible}button,input,optgroup,select,textarea{margin:0;font-family:inherit;font-size:inherit;line-height:inherit}button{border-radius:0}body{margin:0;font-family:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";font-size:1rem;color:#212529;text-align:left;background-color:#fff}:root{--blue:#007bff;--indigo:#6610f2;--purple:#6f42c1;--pink:#e83e8c;--red:#dc3545;--orange:#fd7e14;--yellow:#ffc107;--green:#28a745;--teal:#20c997;--cyan:#17a2b8;--white:#fff;--gray:#6c757d;--gray-dark:#343a40;--primary:#007bff;--secondary:#6c757d;--success:#28a745;--info:#17a2b8;--warning:#ffc107;--danger:#dc3545;--light:#f8f9fa;--dark:#343a40;--breakpoint-xs:0;--breakpoint-sm:576px;--breakpoint-md:768px;--breakpoint-lg:992px;--breakpoint-xl:1200px;--font-family-sans-serif:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";--font-family-monospace:SFMono-Regular,Menlo,Monaco,Consolas,""Liberation Mono"",""courier New"",monospace}html{font-family:sans-serif;line-height:1.15;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;-ms-overflow-style:scrollbar;-webkit-tap-highlight-color:transparent}*,::after,::before{box-sizing:border-box}</style>")
                sb.Append(String.Format("<a href=""{4}/views/WF/TaskSelector.ashx?DocTypeId={0}&docid={1}&taskid={2}&wfstepid=0""><button type=""button"" class=""btn btn-outline-success"">{3}</button></a>", AllObjects.Tarea.DocTypeId, AllObjects.Tarea.ID, AllObjects.Tarea.TaskId, nombre, url))
                Return sb.ToString()
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerLinkPublicoWeb(ByVal nombre As String) As String
        Get
            Try
                Dim sb As New StringBuilder
                Dim hrefUrl As String
                Dim url As String = ZOptBusiness.GetValueOrDefault("ThisDomainPublic", "https://bpm.provinciaseguros.com.ar/bpm")
                sb.Append("<style>.btn,body{font-weight:400;line-height:1.5}<style>.btn:not(:disabled):not(.disabled){cursor:pointer}[type=reset],[type=submit],button,html [type=button]{-webkit-appearance:button}.btn-outline-success{color:#28a745;background-color:transparent;background-image:none;border-color:#28a745}.btn{display:inline-block;text-align:center;white-space:nowrap;vertical-align:middle;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;border:1px solid transparent;padding:.375rem .75rem;font-size:1rem;border-radius:.25rem;transition:color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out}.btn:hover{color:#fff;background-color:#28a745;border-color:#28a745}button,select{text-transform:none}button,input{overflow:visible}button,input,optgroup,select,textarea{margin:0;font-family:inherit;font-size:inherit;line-height:inherit}button{border-radius:0}body{margin:0;font-family:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";font-size:1rem;color:#212529;text-align:left;background-color:#fff}:root{--blue:#007bff;--indigo:#6610f2;--purple:#6f42c1;--pink:#e83e8c;--red:#dc3545;--orange:#fd7e14;--yellow:#ffc107;--green:#28a745;--teal:#20c997;--cyan:#17a2b8;--white:#fff;--gray:#6c757d;--gray-dark:#343a40;--primary:#007bff;--secondary:#6c757d;--success:#28a745;--info:#17a2b8;--warning:#ffc107;--danger:#dc3545;--light:#f8f9fa;--dark:#343a40;--breakpoint-xs:0;--breakpoint-sm:576px;--breakpoint-md:768px;--breakpoint-lg:992px;--breakpoint-xl:1200px;--font-family-sans-serif:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";--font-family-monospace:SFMono-Regular,Menlo,Monaco,Consolas,""Liberation Mono"",""courier New"",monospace}html{font-family:sans-serif;line-height:1.15;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;-ms-overflow-style:scrollbar;-webkit-tap-highlight-color:transparent}*,::after,::before{box-sizing:border-box}</style>")

                If (url.ToString().EndsWith("/")) Then
                    hrefUrl = "href='" + url.TrimEnd("/")
                Else
                    hrefUrl = "href='" + url
                End If

                sb.Append(String.Format("<a " + hrefUrl + "/views/WF/TaskSelector.ashx?DocTypeId={0}&docid={1}&taskid={2}&wfstepid=0""><button type=""button"" class=""btn btn-outline-success"">{3}</button></a>", AllObjects.Tarea.DocTypeId, AllObjects.Tarea.ID, AllObjects.Tarea.TaskId, nombre, url))
                Return sb.ToString()
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerLinkPublicoWebSinFormato(ByVal nombre As String) As String
        Get
            Try
                Dim sb As New StringBuilder
                Dim url As String = ZOptBusiness.GetValueOrDefault("ThisDomainPublic", "https://bpm.provinciaseguros.com.ar/bpm")
                ' sb.Append("<style>.btn,body{font-weight:400;line-height:1.5}<style>.btn:not(:disabled):not(.disabled){cursor:pointer}[type=reset],[type=submit],button,html [type=button]{-webkit-appearance:button}.btn-outline-success{color:#28a745;background-color:transparent;background-image:none;border-color:#28a745}.btn{display:inline-block;text-align:center;white-space:nowrap;vertical-align:middle;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;border:1px solid transparent;padding:.375rem .75rem;font-size:1rem;border-radius:.25rem;transition:color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out}.btn:hover{color:#fff;background-color:#28a745;border-color:#28a745}button,select{text-transform:none}button,input{overflow:visible}button,input,optgroup,select,textarea{margin:0;font-family:inherit;font-size:inherit;line-height:inherit}button{border-radius:0}body{margin:0;font-family:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";font-size:1rem;color:#212529;text-align:left;background-color:#fff}:root{--blue:#007bff;--indigo:#6610f2;--purple:#6f42c1;--pink:#e83e8c;--red:#dc3545;--orange:#fd7e14;--yellow:#ffc107;--green:#28a745;--teal:#20c997;--cyan:#17a2b8;--white:#fff;--gray:#6c757d;--gray-dark:#343a40;--primary:#007bff;--secondary:#6c757d;--success:#28a745;--info:#17a2b8;--warning:#ffc107;--danger:#dc3545;--light:#f8f9fa;--dark:#343a40;--breakpoint-xs:0;--breakpoint-sm:576px;--breakpoint-md:768px;--breakpoint-lg:992px;--breakpoint-xl:1200px;--font-family-sans-serif:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";--font-family-monospace:SFMono-Regular,Menlo,Monaco,Consolas,""Liberation Mono"",""courier New"",monospace}html{font-family:sans-serif;line-height:1.15;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;-ms-overflow-style:scrollbar;-webkit-tap-highlight-color:transparent}*,::after,::before{box-sizing:border-box}</style>")
                sb.Append(String.Format("<a href=""{4}/views/WF/Taskviewer.aspx?DocTypeId={0}&docid={1}&taskid={2}&wfstepid=0""><button type=""button"" class=""btn btn-outline-success"">{3}</button></a>", AllObjects.Tarea.DocTypeId, AllObjects.Tarea.ID, AllObjects.Tarea.TaskId, nombre, url))
                Return sb.ToString()
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property LinkPublico() As String
        Get
            Try
                Return ZOptBusiness.GetValueOrDefault("ThisDomainPublic", "https://bpm.provinciaseguros.com.ar/bpm")
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerLinkPublicoEjecucionRegla(ByVal nombre As String, ByVal RuleId As String) As String
        Get
            Try
                Dim sb As New StringBuilder
                Dim url As String = ZOptBusiness.GetValueOrDefault("ThisDomainPublic", "https://bpm.provinciaseguros.com.ar/bpm")
                url = url & "/Execute.html?processId=" & RuleId & "-" & AllObjects.Tarea.TaskId & "-" & AllObjects.Tarea.DocTypeId & "-" & AllObjects.Tarea.ID
                '                sb.Append("<style>.btn,body{font-weight:400;line-height:1.5}<style>.btn:not(:disabled):not(.disabled){cursor:pointer}[type=reset],[type=submit],button,html [type=button]{-webkit-appearance:button}.btn-outline-success{color:#28a745;background-color:transparent;background-image:none;border-color:#28a745}.btn{display:inline-block;text-align:center;white-space:nowrap;vertical-align:middle;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;border:1px solid transparent;padding:.375rem .75rem;font-size:1rem;border-radius:.25rem;transition:color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out}.btn:hover{color:#fff;background-color:#28a745;border-color:#28a745}button,select{text-transform:none}button,input{overflow:visible}button,input,optgroup,select,textarea{margin:0;font-family:inherit;font-size:inherit;line-height:inherit}button{border-radius:0}body{margin:0;font-family:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";font-size:1rem;color:#212529;text-align:left;background-color:#fff}:root{--blue:#007bff;--indigo:#6610f2;--purple:#6f42c1;--pink:#e83e8c;--red:#dc3545;--orange:#fd7e14;--yellow:#ffc107;--green:#28a745;--teal:#20c997;--cyan:#17a2b8;--white:#fff;--gray:#6c757d;--gray-dark:#343a40;--primary:#007bff;--secondary:#6c757d;--success:#28a745;--info:#17a2b8;--warning:#ffc107;--danger:#dc3545;--light:#f8f9fa;--dark:#343a40;--breakpoint-xs:0;--breakpoint-sm:576px;--breakpoint-md:768px;--breakpoint-lg:992px;--breakpoint-xl:1200px;--font-family-sans-serif:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif,""Apple Color Emoji"",""Segoe UI Emoji"",""Segoe UI Symbol"";--font-family-monospace:SFMono-Regular,Menlo,Monaco,Consolas,""Liberation Mono"",""courier New"",monospace}html{font-family:sans-serif;line-height:1.15;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;-ms-overflow-style:scrollbar;-webkit-tap-highlight-color:transparent}*,::after,::before{box-sizing:border-box}</style>")
                sb.Append(String.Format("<a href=""{1}"" target=""_blank""><button type=""button"" class=""btn btn-outline-success"">{0}</button></a>", nombre, url))
                Return sb.ToString()
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerLogoZamba(Width As String, Height As String) As String
        Get
            Try
                Dim sb As New StringBuilder
                Dim logo As String = ZOptBusiness.GetValueOrDefault("LogoZambaBase64", "")
                sb.Append(String.Format("<img src=""{0}"" width=""{1}"" heiht=""{2}""/>", logo, Width, Height))
                Return sb.ToString()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerLogoCliente(Width As String, Height As String) As String
        Get
            Try
                Dim sb As New StringBuilder
                Dim logo As String = ZOptBusiness.GetValueOrDefault("LogoClienteBase64", "")
                sb.Append(String.Format("<img src=""{0}"" width=""{1}"" heiht=""{2}""/>", logo, Width, Height))
                Return sb.ToString()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerVariableporTaskId(TaskId As String, Variable As String) As String
        Get
            Try
                Dim vars As New VarsBusiness
                Dim varValue As String = vars.GetVariableValue(Variable, TaskId)
                Return varValue
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerVariableporUserId(UserId As String, Variable As String) As String
        Get
            Try
                Dim vars As New VarsBusiness
                Dim varValue As String = vars.GetVariableValue(Variable, UserId)
                Return varValue
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property GuardarVariableporTaskId(TaskId As String, Variable As String, varValue As String) As String
        Get
            Try
                Dim vars As New VarsBusiness
                vars.PersistVariable(Variable, TaskId, varValue)
                Return String.Empty
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property GuardarVariableporUserId(UserId As String, Variable As String, varValue As String) As String
        Get
            Try
                Dim vars As New VarsBusiness
                vars.PersistVariable(Variable, UserId, varValue)
                Return String.Empty
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property EsNumeroParImpar(numero As String) As String
        Get
            Try
                Dim parImpar As String
                Dim NumeroConvertido As Int32

                ValidacionesParaStrinParOImpar(numero, NumeroConvertido)

                Return ValidarParOImparEntero(parImpar, NumeroConvertido)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get

    End Property

    Public Sub ValidacionesParaStrinParOImpar(numero As String, ByRef NumeroConvertido As Int32)
        If numero = Nothing Then
            Throw New Exception("El valor ingresado es nulo")
        End If
        If numero = String.Empty Then
            Throw New Exception("El valor ingresado es vacio")
        End If
        If Not Int32.TryParse(numero, NumeroConvertido) Then
            Throw New Exception("El contenido del string no es valido para hacer una conversion")
        End If

    End Sub

    Public Function ValidarParOImparEntero(parImpar As String, NumeroConvertido As Int32) As String
        If (NumeroConvertido Mod 2) <> 0 Then
            parImpar = "Impar"
        Else
            parImpar = "Par"
        End If

        Return parImpar
    End Function

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property GenerarTablaParaMail(Variable As String) As String
        Get
            Try
                Dim t As New StringBuilder
                If Variable.Contains("zvar") = False Then
                    Variable = "zvar(" & Variable & ")"
                End If
                Dim ValorVariable As Object = New VariablesInterReglas().ReconocerVariablesAsObject(Variable)

                If IsNothing(ValorVariable) = False Then
                    If TypeOf (ValorVariable) Is DataSet Then
                        Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                        If ds.Tables.Count > 0 Then
                            Dim dt As DataTable = ds.Tables(0)
                            If dt.Rows.Count > 0 Then

                                Dim Table As String = "<table border='0' cellpadding='10' cellspacing='0' width='100%' class='templateDataTable'>{0}{1}</table>"
                                Dim TableHeader As String = "<tr>{0}</tr>"
                                Dim HeaderColumn As String = "<th scope='col' valign='top' width='25%' class='dataTableHeading' mc:edit='data_table_heading0{1}'> {0} </th>"
                                Dim Row As String = "<tr mc:repeatable>{0} </tr>"
                                Dim cell As String = "<td valign='top' class='dataTableContent' mc:edit='data_table_content0{1}'> {0}  </td>"
                                Dim i As Int32


                                Dim HeaderString As New StringBuilder
                                For Each c As DataColumn In dt.Columns
                                    HeaderString.Append(String.Format(HeaderColumn, c.ColumnName, i))
                                    i = i + 1
                                Next


                                Dim RowsString As New StringBuilder
                                For Each r As DataRow In dt.Rows
                                    i = 0
                                    Dim CellsString As New StringBuilder
                                    For Each c As DataColumn In dt.Columns
                                        CellsString.Append(String.Format(HeaderColumn, r.Item(c.ColumnName).ToString, i))
                                        i = i + 1
                                    Next
                                    RowsString.Append(String.Format(Row, CellsString))
                                Next

                                t.Append(String.Format(Table, String.Format(TableHeader, HeaderString), RowsString.ToString))

                            End If
                        End If
                    End If
                End If

                Return t.ToString
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get

    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property Repeat(Variable As String, template As String) As String
        Get
            Try

                Dim originalVariable As String = Variable
                If originalVariable.Contains("zvar") = False AndAlso originalVariable.EndsWith(")") = True Then
                    originalVariable = originalVariable.Replace(")", "")
                End If

                If Variable.Contains("zvar") = False Then
                    Variable = "zvar(" & Variable & ")"
                End If
                Dim ValorVariable As Object = New VariablesInterReglas().ReconocerVariablesAsObject(Variable)

                Dim HDoc As New HtmlAgilityPack.HtmlDocument

                If IsNothing(ValorVariable) = False Then
                    If TypeOf (ValorVariable) Is DataSet Then
                        Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                        If ds.Tables.Count > 0 Then
                            Dim dt As DataTable = ds.Tables(0)
                            If dt.Rows.Count > 0 Then
                                Dim currentBody = LeerArchivo(template)

                                HDoc.LoadHtml(currentBody)
                                Dim RepeatElement As HtmlAgilityPack.HtmlNode = HDoc.GetElementbyId(originalVariable)
                                Dim TemplateRow As String = RepeatElement.InnerHtml
                                RepeatElement.ChildNodes.Clear()
                                RepeatElement.Attributes.RemoveAll()
                                Dim RowsString As New StringBuilder
                                For Each r As DataRow In dt.Rows
                                    Dim currentRowTemplate As String = TemplateRow
                                    For Each c As DataColumn In dt.Columns
                                        If currentRowTemplate.Contains("{{Zamba.Data." & c.ColumnName & "}}") Then
                                            If IsDBNull(r(c.ColumnName)) = False Then
                                                currentRowTemplate = currentRowTemplate.Replace("{{Zamba.Data." & c.ColumnName & "}}", r(c.ColumnName))
                                            Else
                                                currentRowTemplate = currentRowTemplate.Replace("{{Zamba.Data." & c.ColumnName & "}}", "")

                                            End If
                                        End If
                                        If currentRowTemplate.Contains("<<ZData." & c.ColumnName & ">>") Then
                                            If IsDBNull(r(c.ColumnName)) = False Then
                                                currentRowTemplate = currentRowTemplate.Replace("<<ZData." & c.ColumnName & ">>", r(c.ColumnName))
                                            Else
                                                currentRowTemplate = currentRowTemplate.Replace("<<ZData." & c.ColumnName & ">>", "")

                                            End If
                                        End If
                                    Next
                                    RowsString.Append(currentRowTemplate)
                                Next
                                RepeatElement.InnerHtml = RowsString.ToString
                            End If
                        End If
                    End If
                End If

                Dim NewDoc As String
                Dim writer As New StringWriter
                Using (writer)
                    HDoc.Save(writer)
                    NewDoc = writer.ToString()
                End Using


                Return NewDoc
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get

    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property GenerarTablaParaMailConLink(Variable As String, LinkColumn As String) As String
        Get
            Try
                Dim t As New StringBuilder
                If Variable.Contains("zvar") = False Then
                    Variable = "zvar(" & Variable & ")"
                End If
                Dim ValorVariable As Object = New VariablesInterReglas().ReconocerVariablesAsObject(Variable)

                If IsNothing(ValorVariable) = False Then
                    If TypeOf (ValorVariable) Is DataSet Then
                        Dim ds As DataSet = DirectCast(ValorVariable, DataSet)
                        If ds.Tables.Count > 0 Then
                            Dim dt As DataTable = ds.Tables(0)
                            If dt.Rows.Count > 0 Then

                                Dim Table As String = "<table border='0' cellpadding='10' cellspacing='0' width='100%' class='templateDataTable'>{0}{1}</table>"
                                Dim TableHeader As String = "<tr>{0}</tr>"
                                Dim HeaderColumn As String = "<th scope='col' valign='top' width='25%' class='dataTableHeading' mc:edit='data_table_heading0{1}'> {0} </th>"
                                Dim Row As String = "<tr mc:repeatable>{0} </tr>"
                                Dim cell As String = "<td valign='top' class='dataTableContent' mc:edit='data_table_content0{1}'> {0}  </td>"
                                Dim cellLink As String = "<td onclick='window.open({2})' valign='top' class='dataTableContent' mc:edit='data_table_content0{1}'> {0}  </td>"
                                Dim i As Int32


                                Dim HeaderString As New StringBuilder
                                For Each c As DataColumn In dt.Columns
                                    If c.ColumnName <> LinkColumn Then
                                        HeaderString.Append(String.Format(HeaderColumn, c.ColumnName, i))
                                    End If
                                    i = i + 1
                                Next


                                Dim RowsString As New StringBuilder
                                For Each r As DataRow In dt.Rows
                                    i = 0
                                    Dim CellsString As New StringBuilder
                                    For Each c As DataColumn In dt.Columns
                                        If c.ColumnName <> LinkColumn Then
                                            CellsString.Append(String.Format(HeaderColumn, r.Item(c.ColumnName).ToString, i))
                                        End If
                                        i = i + 1
                                    Next
                                    RowsString.Append(String.Format(Row, CellsString))
                                Next

                                t.Append(String.Format(Table, String.Format(TableHeader, HeaderString), RowsString.ToString))

                            End If
                        End If
                    End If
                End If

                Return t.ToString
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get

    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property FTPBajarArchivo(RemoteFtpPath As String, Username As String, Password As String, LocalDestinationPath As String) As String
        Get
            'Try
            Dim FTP As New FtpHelper
            FTP.DownloadFileFromFtp(RemoteFtpPath, Username, Password, LocalDestinationPath)
            FTP = Nothing
            Return LocalDestinationPath
            'Catch ex As Exception
            '    ZClass.raiseerror(ex)
            'End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerRutaAplicacionWeb() As String
        Get
            Dim relativePath As String = String.Empty
            Try
                relativePath = New DirectoryInfo(MembershipHelper.AppTempPath).Parent.FullName
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return relativePath
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property EliminarDocumento(docId As String, docTypeId As String, deleteFile As String)
        Get

            Dim wfTB As New WFTaskBusiness
            Dim WFB As New WFBusiness
            Dim RB As New Results_Business
            Dim task As ITaskResult = wfTB.GetTaskByDocId(Long.Parse(docId), MembershipHelper.CurrentUser.ID)

            If task IsNot Nothing Then
                WFB.RemoveTask(task, True, Membership.MembershipHelper.CurrentUser.ID, Boolean.Parse(deleteFile))
            Else
                Dim Result As IResult = RB.GetResult(Long.Parse(docId), Long.Parse(docTypeId), False)
                RB.Delete(Result, Boolean.Parse(deleteFile), False)
            End If

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ValidarSiUsuarioActualEsElAsignadoOPerteneceAlGrupoAsignado() As String
        Get
            Dim IsMember As Boolean = False
            Try
                Dim UserGroupBusiness As New UserGroupBusiness
                If Tarea.AsignedToId = Membership.MembershipHelper.CurrentUser.ID OrElse UserGroupBusiness.GetUsersIds(Tarea.AsignedToId).Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
            Return IsMember
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ValidarSiUsuarioActualPerteneceAlGrupoAsignado() As String
        Get
            Dim IsMember As Boolean = False
            Try
                Dim UserGroupBusiness As New UserGroupBusiness
                If UserGroupBusiness.GetUsersIds(Tarea.AsignedToId).Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
            Return IsMember
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ValidarSiUsuarioActualEsElAsignado() As String
        Get
            Dim IsMember As Boolean = False
            Try
                If Tarea.AsignedToId = Membership.MembershipHelper.CurrentUser.ID Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
            Return IsMember
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerBlob(ByVal RutaArchivo As String, ByVal Sentencia As String) As String
        Get
            Try
                If RutaArchivo.StartsWith("'") AndAlso RutaArchivo.EndsWith("'") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If

                If RutaArchivo.StartsWith("""") AndAlso RutaArchivo.EndsWith("""") Then
                    RutaArchivo = RutaArchivo.Substring(0, 1)
                    RutaArchivo = RutaArchivo.Substring(0, RutaArchivo.Length - 2)
                End If


                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                Sentencia = VarInterReglas.ReconocerVariablesValuesSoloTexto(Sentencia)

                Dim File As Byte() = DirectCast(Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, Sentencia, 3000), Byte())

                RutaArchivo = VarInterReglas.ReconocerVariablesValuesSoloTexto(RutaArchivo)

                Dim sr As New StreamWriter(RutaArchivo)
                sr.Write(New String(Encoding.UTF8.GetChars(File)))
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Return RutaArchivo
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerUrlPublica() As String
        Get
            Try
                Return ZOptBusiness.GetValueOrDefault("ThisDomainPublic", "https://bpm.provinciaseguros.com.ar/bpm")
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerUrlPrivada() As String
        Get
            Try
                Return ZOptBusiness.GetValueOrDefault("ThisDomain", "https://bpm.provinciaseguros.com.ar/bpm")
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerRutaLocalServidor(subCarpeta As String) As String
        Get
            Try
                Dim dirInfo As DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\" & subCarpeta)
                Return dirInfo.FullName
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerCantidaddeAttachs(EmailVar As String) As String
        Get
            Try
                Dim e As AttachmentCollection = VariablesInterReglas.Item(EmailVar)
                Return e.Count

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerAttachArchivo(EmailVar As String, ByVal RutaArchivo As String) As String
        Get
            Try
                RutaArchivo += "\"
                For Each item As Attachment In VariablesInterReglas.Item(EmailVar)
                    Dim file As New FileStream(RutaArchivo + item.ContentType.Name, FileMode.Create, FileAccess.Write)

                    CType(item.Data, MemoryStream).WriteTo(file)
                    file.Close()
                    CType(item.Data, MemoryStream).Close()
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Adjunto " + item.ContentType.Name + " almacenado en '" + RutaArchivo + "'.")
                Next

                Return "Los adjuntos se guardaron exitosamente"
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return ex.Message
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property ObtenerAttachName(EmailVar As String) As String
        Get
            Try
                Dim String_Lista_Archivos

                For Each item As Attachment In VariablesInterReglas.Item(EmailVar)

                Next

                Return 1

            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"jsonData"})>
    Public ReadOnly Property JsonToDataTable(data As String) As DataTable
        Get

            Return JsonConvert.DeserializeObject(Of DataTable)(data)

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"jsonData"})>
    Public ReadOnly Property DataTableToJson(data As DataTable) As String
        Get

            Return JsonConvert.SerializeObject(data, Formatting.Indented)

        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Base64", "Extension", "Archivo"})>
    Public ReadOnly Property GuardarArchivoenBase64(Base64 As String, NombreArchivo As String) As String
        Get

            Dim tempPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempDir("temp").FullName, NombreArchivo)
            Dim Base64bytes As Byte() = Convert.FromBase64String(Base64)
            FileEncode.Decode(tempPDFFile, Base64bytes)
            Return tempPDFFile


        End Get
    End Property




    Private Shared Function CreateInstance(assemblyName As String, typeName As String) As Object
        Dim obj As Object
        Dim engine As Assembly
        Dim classType As Type
        Try
            engine = Assembly.LoadFile(String.Concat(MembershipHelper.StartUpPath, "\", assemblyName))
            classType = engine.GetType(typeName, True, True)
            obj = Activator.CreateInstance(classType)
            Return obj
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        Finally
            engine = Nothing
            classType = Nothing
        End Try
    End Function



End Class


