Imports zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Validar Cantidad de Tareas"), RuleHelp("Valida la cantidad de tareas existentes"), RuleFeatures(False)> _
Public Class IfDOCUMENTSCOUNT
    Inherits WFRuleParent
    Implements IIfDOCUMENTSCOUNT, IRuleIFPlay, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayIfDocumentsCount
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
    Public Overrides Sub Dispose()

    End Sub
    Private Comp As Comparadores
    Private CantidadT As System.Int16

#Region " Constructores "

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal compConstructor As Comparadores, ByVal tareasConstructor As System.Int16)
        MyBase.New(Id, Name, wfstepid)
        Me.Comp = compConstructor
        Me.CantidadT = tareasConstructor
        Me.playRule = New WFExecution.PlayIfDocumentsCount(Me)
    End Sub
#End Region

#Region " Propiedades "
    Public Property Comparacion() As Comparadores Implements IIfDOCUMENTSCOUNT.Comparacion
        Get
            Return Me.Comp
        End Get
        Set(ByVal value As Comparadores)
            Me.Comp = value
        End Set
    End Property

    Public Property CantidadTareas() As Int16 Implements IIfDOCUMENTSCOUNT.CantidadTareas
        Get
            Return Me.CantidadT
        End Get
        Set(ByVal value As Int16)
            Me.CantidadT = value
        End Set

    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property
#End Region

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''      [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf

        Return playRule.Play(results, ifType)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class


'    Public Overrides Function PrivateCheck() As Boolean
'        Dim Result As Boolean = False
'        Try
'            Select Case ZGroupParam.getParam("OPERATOR").Value
'                Case "="
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") = ZGroupParam.getParam("VALUE").Value Then
'                        Return True
'                    End If
'                Case ">"
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") > ZGroupParam.getParam("VALUE").Value Then
'                        Return True
'                    End If
'                Case "<"
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") < ZGroupParam.getParam("VALUE").Value Then
'                        Return True
'                    End If
'                Case ">="
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") >= ZGroupParam.getParam("VALUE").Value Then
'                        Return True
'                    End If
'                Case "<="
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") <= ZGroupParam.getParam("VALUE").Value Then
'                        Return True
'                    End If
'                Case "<>"
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") <> ZGroupParam.getParam("VALUE").Value Then
'                        Return True
'                    End If
'                Case "Entre"
'                    'falta ver el value2 que lo pueda guardar
'                    If Me.GetParams("WFSTEPDOCUMENTSCOUNT") >= ZGroupParam.getParam("VALUE").Value And Me.GetParams("DOCUMENTSCOUNT") <= ZGroupParam.getParam("VALUE2").Value Then
'                        Return True
'                    End If
'            End Select
'        Catch ex As Exception
'            Return False
'        End Try
'        Return Result
'    End Function
'#End Region

'End Class