Imports zamba.Core

<RuleCategory("Secciones"), RuleDescription("Validar Tamaño de Archivo"), RuleHelp("Comprueba el tamño del archivo para realizar una tarea determinada"), RuleFeatures(False)> _
Public Class IfFileSize
    Inherits WFRuleParent
    Implements IIfFileSize, IRuleIFPlay
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
#Region " Variables "
    Dim _comparador As Comparacion
    Dim _path As String
    Dim _num1 As System.UInt32
    Dim _num2 As System.UInt32
#End Region

#Region " Constructores "
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal comparador_ As Comparacion, ByVal path_ As String, ByVal num1_ As System.UInt32, ByVal num2_ As System.UInt32)
        MyBase.New(Id, Name, wfstepid)
        Me.Comparador = comparador_
        Me.path = path_
        Me.num1 = num1_
        Me.num2 = num2_
    End Sub
#End Region

#Region " Eventos "

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfFileSize()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfFileSize()
        Return playRule.Play(results, Me)
    End Function
    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''       [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfFileSize()
        Return playRule.Play(results, Me, ifType)
    End Function
#End Region

#Region " Miembros de IIfFileSize "
    Public Property Comparador() As Core.Comparacion Implements Core.IIfFileSize.Comparador
        Get
            Return Me._comparador
        End Get
        Set(ByVal value As Core.Comparacion)
            Me._comparador = value
        End Set
    End Property

    Public Property num1() As UInteger Implements Core.IIfFileSize.num1
        Get
            Return Me._num1
        End Get
        Set(ByVal value As UInteger)
            Me._num1 = value
        End Set
    End Property

    Public Property num2() As UInteger Implements Core.IIfFileSize.num2
        Get
            Return Me._num2
        End Get
        Set(ByVal value As UInteger)
            Me._num2 = value
        End Set
    End Property

    Public Property path() As String Implements Core.IIfFileSize.path
        Get
            Return Me._path
        End Get
        Set(ByVal value As String)
            Me._path = value
        End Set
    End Property
#End Region

End Class

'    Public Overrides Function PrivateCheck() As Boolean
'        Try
'            Dim f As New IO.FileInfo(Task.FullPath)
'            Return FileCompare(f, ZGroupParam.getParam("OPERATOR").Value, ZGroupParam.getParam("FILEATTRIBUTE").Value, ZGroupParam.getParam("FILEDATA").Value)
'        Catch ex As Exception
'            Return False
'        End Try
'    End Function
'#End Region

'#Region "_Propietary Functions"
'    Private Function GetAttribute(ByVal f As IO.FileInfo, ByVal att As String) As Object
'        Select Case att.ToUpper
'            Case "Tamaño"
'                Return f.Length
'            Case "Fecha de Creación"
'                Return f.CreationTime.ToString("dd/mm/yyyy")
'            Case "Fecha de Modificación"
'                Return f.LastWriteTime.ToString("dd/mm/yyyy")
'            Case "Extension"
'                Return f.Extension
'            Case "Nombre"
'                Return f.Name
'        End Select
'    End Function

'    Private Function FileCompare(ByVal f As IO.FileInfo, ByVal op As String, ByVal Attribute As String, ByVal Value As String) As Boolean
'        If Task.ISVIRTUAL Then
'            Return True
'        Else
'            Select Case Attribute.ToUpper
'                Case "Tamaño"
'                    Dim length As Long = f.Length
'                    Return checkInteger(length, Value, op)
'                Case "Fecha de Creación"
'                    Dim d As Date = f.CreationTime
'                    Return checkDate(d, Value, op)
'                Case "Fecha de Modificación"
'                    Dim d As Date
'                    Return checkDate(d, Value, op)
'                Case "Extension"
'                    Return checkString(f.Extension, Value, op)
'                Case "Nombre"
'                    Return checkString(f.Name, Value, op)
'                Case Else
'                    Return False
'            End Select
'        End If

'        'Dim at As Long
'        'Try
'        '    at = Me.GetAttribute(f, Attribute)
'        'Catch ex As Exception
'        '    Return False
'        'End Try


'        'Select Case Operator
'        '    Case "="
'        '        at = Value
'        '    Case "<"
'        '        at < value
'        '    Case ">"
'        '    Case "<="
'        '    Case ">="
'        '    Case "Contiene"
'        '    Case "<>"
'        'End Select
'    End Function

'#Region "Chequeos"
'    Private Function checkDate(ByVal d1 As Date, ByVal val As String, ByVal op As String) As Boolean
'        Dim d2 As Date
'        Try
'            d2 = Date.Parse(val)
'        Catch
'            Return False
'        End Try
'        Select Case op
'            Case "="
'                Return d2 = d1
'            Case "<"
'                Return d1 < d2
'            Case ">"
'                Return d1 > d2
'            Case "<="
'                Return d1 <= d2
'            Case ">="
'                Return d1 >= d2
'            Case "Contiene"
'                Return False
'            Case "<>"
'                Return d1 <> d2
'        End Select
'    End Function
'    Private Function checkString(ByVal s As String, ByVal val As String, ByVal op As String) As Boolean
'        Select Case op
'            Case "="
'                Return s = val
'            Case "Contiene"
'                Return s.IndexOf(val) <> -1
'            Case "<>"
'                Return s <> val
'        End Select
'    End Function
'    Private Function checkInteger(ByVal d1 As Long, ByVal val As String, ByVal op As String) As Boolean
'        Dim d2 As Long
'        Try
'            d2 = Long.Parse(val)
'        Catch
'            Return False
'        End Try
'        Select Case op
'            Case "="
'                Return d2 = d1
'            Case "<"
'                Return d1 < d2
'            Case ">"
'                Return d1 > d2
'            Case "<="
'                Return d1 <= d2
'            Case ">="
'                Return d1 >= d2
'            Case "Contiene"
'                Return False
'            Case "<>"
'                Return d1 <> d2
'        End Select
'    End Function
'#End Region
'#End Region

'#Region "Propietary Fields"


'    'Dim FILEATTRIBUTE As String
'    'Dim Operator As String
'    'Dim FILEDATA As String

'#End Region
'End Class
