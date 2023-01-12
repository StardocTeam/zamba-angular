Imports Zamba.Core

''' <summary>
''' Modifica permisos específicos de la tarea
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Modificar permisos"), RuleHelp("Permite habilitar o deshabilitar determinados permisos de la tarea"), RuleFeatures(False)> <Serializable()> _
Public Class DoSetRights
    Inherits WFRuleParent
    Implements IDoSetRights, IRuleValidate

#Region "Attributos y Propiedades"
    Private playRule As Zamba.WFExecution.PlayDoSetRights
    Private _encodedRights As String
    Private _isValid As Boolean
    Private _rights As Dictionary(Of RightsType, Boolean) = Nothing

    Public Overrides ReadOnly Property IsFull As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get

        End Get
    End Property
    Public Property EncodedRights As String Implements Core.IDoSetRights.EncodedRights
        Get
            Return _encodedRights
        End Get
        Set(value As String)
            _encodedRights = value
        End Set
    End Property

    ''' <summary>
    ''' Get the rights dictionary to modify the behavior taskresult
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Use KeyValuePair to iterate the keys</remarks>
    Public Property Rights As Dictionary(Of RightsType, Boolean) Implements Core.IDoSetRights.Rights
        Get
            If _rights Is Nothing Then
                _rights = New Dictionary(Of RightsType, Boolean)

                If _encodedRights.Length > 0 Then
                    Dim semicolonSep() As Char = New Char() {Char.Parse(";")}
                    Dim commaSep() As Char = New Char() {Char.Parse(",")}
                    Dim tempRightType As RightsType
                    Dim tempValue As Boolean
                    Dim tempRightToConvert() As String

                    Try
                        For Each rightToConvert As String In _encodedRights.Split(semicolonSep)
                            tempRightToConvert = rightToConvert.Split(commaSep)
                            tempRightType = DirectCast(Int32.Parse(tempRightToConvert(0)), RightsType)
                            tempValue = Boolean.Parse(tempRightToConvert(1))
                            _rights.Add(tempRightType, tempValue)
                        Next
                    Catch ex As Exception
                        raiseerror(ex)
                        _rights.Clear()
                    Finally
                        semicolonSep = Nothing
                        commaSep = Nothing
                        tempRightToConvert = Nothing
                        tempRightType = Nothing
                        tempValue = Nothing
                    End Try
                End If
            End If

            Return _rights
        End Get
        Set(value As Dictionary(Of RightsType, Boolean))
            _rights = value
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

#Region "Constructores"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal encodedRights As String)
        MyBase.New(Id, Name, wfstepid)
        _encodedRights = encodedRights
        playRule = New Zamba.WFExecution.PlayDoSetRights(Me)
    End Sub
#End Region

#Region "Métodos"
    Public Overrides Sub Dispose()
        _encodedRights = Nothing
        If _rights IsNot Nothing Then
            _rights.Clear()
            _rights = Nothing
        End If
        If playRule IsNot Nothing Then
            playRule = Nothing
        End If
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results, Me)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
#End Region

End Class
