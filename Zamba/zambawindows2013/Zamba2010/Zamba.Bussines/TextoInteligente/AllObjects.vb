Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core
Imports System.Text
Imports System.IO

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

    Private Shared _Tarea As TaskResult
    Private Shared _Tareas As System.Collections.Generic.List(Of Core.ITaskResult)
    Private Shared _CommonFunctions As New CommonFunctions

    Public Shared Property Tarea() As TaskResult
        Get
            Return _Tarea
        End Get
        Set(ByVal Value As TaskResult)
            _Tarea = Value
        End Set
    End Property
    Public Shared Property Tareas() As System.Collections.Generic.List(Of Core.ITaskResult)
        Get
            Return _Tareas
        End Get
        Set(ByVal Value As System.Collections.Generic.List(Of Core.ITaskResult))
            _Tareas = Value
        End Set
    End Property

    Public Shared ReadOnly Property UsuarioActual() As IUser
        Get
            Return Membership.MembershipHelper.CurrentUser
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
    Private Shared _Hash As New Hashtable


    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property UsuarioActualId() As Int64
        Get
            Return Membership.MembershipHelper.CurrentUser.ID
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property ObtenerNombreUsuarioxId(ByVal UsuarioId As string) As String
        Get
            Return UserGroupBusiness.GetUserorGroupNamebyId(UsuarioId)
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property UsuarioActualNombre() As String
        Get
            Return Membership.MembershipHelper.CurrentUser.Name
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property UsuarioActualApellido() As String
        Get
            Return Membership.MembershipHelper.CurrentUser.Apellidos
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property UsuarioActualNombreYApellido() As String
        Get
            Return Membership.MembershipHelper.CurrentUser.Nombres & Membership.MembershipHelper.CurrentUser.Apellidos
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property UsuarioActualMail() As String
        Get
            Return Membership.MembershipHelper.CurrentUser.eMail.Mail
        End Get
    End Property

    ''' <summary>
    ''' Devuelve el entorno en que esta la aplicacion (Desarrollo, Test, Produccion)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property Ambiente() As String
        Get
            If Not IsNothing(ZOptBusiness.GetValue("Environment")) Then
                Return ZOptBusiness.GetValue("Environment")
            Else
                Return "Sin definir"
            End If
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
    Public Shared ReadOnly Property Entorno() As String
        Get
            If Membership.MembershipHelper.isWeb = True Then
                Return "Web"
            Else
                Return "Windows"
            End If
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property NombreUsuarioWindows() As String
        Get
            Return Environment.UserName
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property NombrePC() As String
        Get
            Return Environment.MachineName
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared Property Hash() As Hashtable
        Get
            Return _Hash
        End Get
        Set(ByVal Value As Hashtable)
            _Hash = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property FechaActual() As String
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
    Public Shared ReadOnly Property FechaActualConFormato(ByVal Formato As String) As String
        'Public Shared ReadOnly Property FechaActual(ByVal Formato As String) As String
        Get
            Return Now.ToString(Formato)
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property FormatoFecha(ByVal Fecha As String, ByVal Formato As String) As String
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
                    ZTrace.WriteLineIf(ZTrace.IsError, "La fecha ingresada no es valida")
                    Return Fecha
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "Error en fecha" & ex.ToString)
                Return Fecha
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property Sumar(ByVal A As String, ByVal B As String) As String
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
    Public Shared ReadOnly Property Restar(ByVal A As String, ByVal B As String) As String
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
    Public Shared ReadOnly Property Multiplicar(ByVal A As String, ByVal B As String) As String
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
    Public Shared ReadOnly Property Dividir(ByVal A As String, ByVal B As String) As String
        Get
            Try
                Return Decimal.Parse(A) / Decimal.Parse(B)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al dividir: " & ex.ToString)
                Return 0
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property Reemplazar(ByVal Original As String, ByVal A As String, ByVal B As String) As String
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
    Public Shared ReadOnly Property ObtenerPartePalabra(ByVal palabra As String, ByVal caracteres As String, ByVal parte As String) As String
        Get
            Try
                Dim posicion As Int32

                If palabra.ToLower().Contains("zvar") Then
                    palabra = WFRuleParent.ReconocerVariablesValuesSoloTexto(palabra)
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
    Public Shared ReadOnly Property Subcadena(ByVal palabra As String, ByVal buscar As String, ByVal desde As string) As String
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

     <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property UbicacionDe(ByVal palabra As String, ByVal buscar As String, ByVal desde As string) As String
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
    Public Shared ReadOnly Property SubcadenaHastaFinal(ByVal palabra As String, ByVal buscar As String) As String
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
    Public Shared ReadOnly Property ExtraerNumeros(ByVal palabra As String, ByVal Separador As String) As String
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
    Public Shared ReadOnly Property ExtraerPrimerosNumeros(ByVal palabra As String, ByVal Separador As String) As String
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
    Public Shared ReadOnly Property Contiene(ByVal nombre As String, ByVal valor As String) As Boolean
        Get
            Try
                Dim A As Object = WFRuleParent.ReconocerVariablesAsObject(nombre)

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
    Public Shared ReadOnly Property EsNumerico(ByVal texto As String) As Boolean
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
    Public Shared ReadOnly Property EsFecha(ByVal texto As String) As Boolean
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
    Public Shared ReadOnly Property EsNulo(ByVal texto As String) As Boolean
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
    Public Shared ReadOnly Property SuLongitudEsIgualA(ByVal texto As String, ByVal Longitud As string) As Boolean
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
    Public Shared ReadOnly Property SuLongitudEsMayorQue(ByVal texto As String, ByVal Longitud As string) As Boolean
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
    Public Shared ReadOnly Property SuLongitudEsMenorQue(ByVal texto As String, ByVal Longitud As string) As Boolean
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
    Public Shared ReadOnly Property EsUnNumeroMenorQue(ByVal texto As String, ByVal valor As string) As Boolean
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
    Public Shared ReadOnly Property EsUnNumeroMayorQue(ByVal texto As String, ByVal valor As string) As Boolean
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
    Public Shared ReadOnly Property ConsultaABase(ByVal texto As String, ByVal valor As string) As String
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
    Public Shared ReadOnly Property EjecutaEnBase(ByVal texto As String, ByVal valor As string) As Integer
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
    Public Shared ReadOnly Property PasarAMinusculas(ByVal text As String) As String
        Get
            Dim toLowerText As String
            Try
                toLowerText = text.ToLower()
                text = WFRuleParent.ReconocerVariablesValuesSoloTexto(text)
                Return toLowerText
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property PasarAMayusculas(ByVal text As String) As String
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
    Public Shared ReadOnly Property ObtenerPosicion(ByVal Palabra As String, PalabraBuscada As String, ByVal BuscarDesde As string) As String
        Get
            Try
                PalabraBuscada = WFRuleParent.ReconocerVariablesValuesSoloTexto(PalabraBuscada)
                Return Palabra.IndexOf(PalabraBuscada, integer.Parse(BuscarDesde), StringComparison.CurrentCultureIgnoreCase)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al Buscar Posicion: " & ex.ToString)
                Throw ex
            End Try
        End Get
    End Property

    'Se utiliza en la DoExplorer para guardar si esta abierto el explorador
    Private Shared TasksVariables As New Generic.Dictionary(Of Long, Generic.Dictionary(Of String, Object))

    ''' <summary>
    ''' Se utiliza en la DoExplorer para guardar si esta abierto el explorador
    ''' </summary>
    ''' <param name="VariableName"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared Property VariablesxTarea(ByVal VariableName As String) As String
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
                If String.IsNullOrEmpty(CommonFunctions.VariablesxTarea("ExplorerIsOpen")) Then Return False
                Return CommonFunctions.VariablesxTarea("ExplorerIsOpen")
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
    Public Shared ReadOnly Property HTMLEncode(ByVal text As String) As String
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
    Public Shared ReadOnly Property DecodificarBase64(ByVal text As String) As String
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
    Public Shared ReadOnly Property DecodeFile(ByVal text As String) As String
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
    Public Shared ReadOnly Property ObtenerRutaTareaxIdTarea(ByVal TaskId As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                TaskId = VarInterReglas.ReconocerVariables(TaskId)
                Dim Task As ITaskResult = Zamba.Core.WF.WF.WFTaskBusiness.GetTask(TaskId)

                If Task IsNot Nothing Then
                    Return Task.FullPath
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property ObtenerRutaTareaxIdDocumento(ByVal DocId As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                Dim Task As ITaskResult = Zamba.Core.WF.WF.WFTaskBusiness.GetTaskByDocId(DocId)
                If Task IsNot Nothing Then
                    Return Task.FullPath
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public Shared ReadOnly Property ObtenerRutaDocumentoxIdDocumentoYIdEntidad(ByVal DocId As String, ByVal EntityId As String) As String
        Get
            Try
                Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
                DocId = VarInterReglas.ReconocerVariables(DocId)
                EntityId = VarInterReglas.ReconocerVariables(EntityId)
                Dim Result As IResult = Zamba.Core.Results_Business.GetResult(DocId, EntityId)
                If Result IsNot Nothing Then
                    Return Result.FullPath
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Get
    End Property

End Class