Imports Zamba.Servers
Imports Zamba.Core

Public Class PAQ_CreateTables_ABN
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Paquete de Migracion de estructura de datos para la empresa ABN. Es necesario que en la carpeta de ejecución se encuentren los archivos de script correspondientes (ej.: 7.IncrementaID.sql)."
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim cmdSql As AbstractComand
        '--
        cmdSql = New EliminaTablasCmd()
        cmdSql.Execute()
        '--
        cmdSql = New CreaTablasCmd()
        cmdSql.Execute()
        '--
        cmdSql = New CreaVistasCmd()
        cmdSql.Execute()
        '--
        cmdSql = New CreaProceduresCmd()
        cmdSql.Execute()
        '--
        cmdSql = New CreaTriggersCmd()
        cmdSql.Execute()
        '--
        cmdSql = New CreaDOC_T_I_DCmd()
        cmdSql.Execute()
        '--
        cmdSql = New CreaPoolDatosCmd()
        cmdSql.Execute()
        '--
        cmdSql = New IncrementaIDCmd()
        cmdSql.Execute()
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTables_ABN"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTables_ABN
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return New Date(2007, 2, 13, 9, 30, 0)
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.Installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.Orden
        Get
            Return 18
        End Get
    End Property

End Class

#Region "Context"
Public MustInherit Class AbstractContext
    '  Friend m_state As AbstractComparator

End Class
#End Region

#Region "Command"

Friend MustInherit Class AbstractComand
    Inherits AbstractContext

    Friend file As String
    Friend Linea As Int64

    Public Sub New()
    End Sub

    Friend Sub initialize()
        ' m_state = New ComparatorSingleLine(Me)
        LogTracert("Procesar Archivo: " & file)
    End Sub
    'Valida que el servidor sea del tipo Sql.
    'Retorna true si el servidor es del tipo Sql.
    Friend Function ValidateServerType() As Boolean
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Return True
        End If
        Return False
    End Function
    'Ejecuta un a consulta.
    'Retorna true si se ha ejecutado correctamente.
    Friend Overloads Function Execute(ByVal query As String) As Boolean
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
            LogTracert(query & " -- Execute Ok.")
            Return True
        Catch ex As Exception
            LogTracert("ERROR -- Linea: " & Linea.ToString() & ". " & query & " -- Execute Fail." & vbNewLine & ex.ToString)
            Return False
        End Try
    End Function

    Public Overridable Overloads Function Execute() As Boolean
        Dim filesql As String
        Dim query As String
        Dim strAux As String
        Dim bReturn As Boolean

        If ValidateServerType() Then
            Linea = 0
            filesql = System.IO.Path.Combine(Application.StartupPath, file)
            If System.IO.File.Exists(filesql) Then
                Dim fi As New System.IO.StreamReader(filesql)
                Using (fi)
                    While (Not fi.EndOfStream)
                        strAux = fi.ReadLine().Trim()
                        Linea = Linea + 1
                        'Selecciona la cadena a comparar
                        'm_state.setCadena(strAux)
                        ''Recupera la cadena despues de la comparacion
                        'strAux = m_state.Execute()
                        'Si encuentra la palabra GO, Ejecuta la consulta.
                        'De lo contrario arma la consulta.
                        If String.Compare(strAux.ToLower(), "go") = 0 AndAlso query.Length > 15 Then
                            bReturn = Execute(query)
                            If Not bReturn Then
                                ' Exit While
                            End If
                            query = ""
                        Else
                            query &= " " & strAux
                        End If

                    End While
                    fi.Close()
                End Using
            End If
        End If
        Return bReturn
    End Function
    'Loguea el estado
    Private Sub LogTracert(ByVal log As String)
        Dim s As String
        Dim PathLog As String
        s = vbNewLine
        s &= "Fecha: " & Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        s &= " " & log
        PathLog = System.IO.Path.Combine(Application.StartupPath, "Zamba.Paquetes.SqlOut.log")
        Dim file As New System.IO.StreamWriter(PathLog, True)
        Using (file)
            file.WriteLine(s)
            file.Flush()
            file.Close()
        End Using
    End Sub
End Class

Friend Class EliminaTablasCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "0.Eliminar tablas WF.sql"
        MyBase.initialize()
    End Sub

End Class

Friend Class CreaTablasCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "1.Tablas.sql"
        MyBase.initialize()
    End Sub
End Class

Friend Class CreaVistasCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "2.Vistas.sql"
        MyBase.initialize()
    End Sub

End Class

Friend Class CreaProceduresCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "3.Proc.sql"
        MyBase.initialize()
    End Sub

End Class

Friend Class CreaTriggersCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "4.Trigger.sql"
        MyBase.initialize()
    End Sub

End Class

Friend Class CreaDOC_T_I_DCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "5. DOC_T_I_D.sql"
        MyBase.initialize()
    End Sub

End Class

Friend Class CreaPoolDatosCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "6.PoolDatos.sql"
        MyBase.initialize()
    End Sub

End Class

Friend Class IncrementaIDCmd
    Inherits AbstractComand

    Public Sub New()
        MyBase.New()
        'Selecciona el archivo a ejecutar
        file = "7.IncrementaID.sql"
        MyBase.initialize()
    End Sub

End Class

#End Region

'#Region "States"

'Public MustInherit Class AbstractComparator

'    Friend m_context As AbstractContext
'    Friend m_Cadena As String

'    Friend i As Integer

'    Public Sub New(ByVal o_context As AbstractContext)
'        m_context = o_context
'    End Sub

'    Public Overridable Sub setCadena(ByVal cadena As String)
'        m_Cadena = cadena
'    End Sub

'    Public MustOverride Function Execute() As String

'End Class

'Public Class ComparatorSingleLine
'    Inherits AbstractComparator

'    Public Sub New(ByVal o_context As AbstractContext)
'        MyBase.New(o_context)
'    End Sub

'    Public Overrides Sub setCadena(ByVal cadena As String)
'        MyBase.setCadena(cadena)
'        Try
'            'Busca el caracter de comentario linea simple
'            i = m_Cadena.IndexOf("--")
'            'No es un comentario de linea simple
'            If i = -1 Then
'                'Intenta delegando a un comentario de linea multiple
'                m_context.m_state = New ComparatorMultiLine(m_context)
'                'Selecciona la cadena.
'                m_context.m_state.setCadena(m_Cadena)
'            End If
'        Catch ex As Exception
'        End Try
'    End Sub

'    Public Overrides Function Execute() As String
'        'Es un comentario de linea simple que
'        'esta al Principio de la cadena
'        If i = 0 Then
'            Return ""
'            'En alguna parte
'        ElseIf i > 0 Then
'            Return m_Cadena.Substring(0, i).Trim()
'        End If
'    End Function

'End Class

'Public Class ComparatorMultiLine
'    Inherits AbstractComparator

'    Private bCaracterCierre As Boolean

'    Public Sub New(ByVal o_context As AbstractContext)
'        MyBase.New(o_context)
'    End Sub

'    Public Overrides Sub setCadena(ByVal cadena As String)
'        MyBase.setCadena(cadena)

'        If Not bCaracterCierre Then
'            'Busca el caracter de inicion de comentario multilinea
'            i = m_Cadena.IndexOf("/*")
'            If i = -1 Then
'                'No es un comentario multilinea
'                m_context.m_state = New ComparatorNormal(m_context)
'                m_context.m_state.setCadena(m_Cadena)
'            Else
'                'Ahora busca el caracter de cierre de comentario multiple
'                bCaracterCierre = True
'            End If
'        Else
'            'Busca el caracter de finalizacion de comentario multilinea
'            i = m_Cadena.IndexOf("*/")
'        End If

'    End Sub

'    Public Overrides Function Execute() As String
'        If i >= 0 Then
'            'El comnetario multilinea ha terminado
'            m_context.m_state = New ComparatorSingleLine(m_context)
'            'Retorna el resto de la cadena que no es comentario.
'            Return m_Cadena.Substring(i + 2)
'        End If
'    End Function
'End Class

'Public Class ComparatorNormal
'    Inherits AbstractComparator

'    Public Sub New(ByVal o_context As AbstractContext)
'        MyBase.New(o_context)
'    End Sub

'    Public Overrides Sub setCadena(ByVal cadena As String)
'        MyBase.setCadena(cadena)
'    End Sub

'    Public Overrides Function Execute() As String
'        m_context.m_state = New ComparatorSingleLine(m_context)
'        Return m_Cadena
'    End Function
'End Class


'#End Region