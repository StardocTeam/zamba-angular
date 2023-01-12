Imports System.Collections.Generic
Imports System.Reflection

Namespace Cache
    Public Class GlobalRulesEngine
        Private Shared _singletonEngine As GlobalRulesEngine = Nothing
        Private _engineAssembly As Assembly = Nothing
        Private _classTypes As New Generic.Dictionary(Of String, Type)

        ''' <summary>
        ''' El constructor es privado
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub New()

        End Sub

        ''' <summary>
        ''' Obtiene la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInstance() As GlobalRulesEngine
            If Membership.MembershipHelper.isWeb Then
                If _singletonEngine Is Nothing Then
                    _singletonEngine = New GlobalRulesEngine()
                End If
                Return _singletonEngine
            Else
                Return New GlobalRulesEngine()
            End If
        End Function

        ''' <summary>
        ''' Remueve la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RemoveCurrentInstance() As ZCore
            If Membership.MembershipHelper.isWeb Then
                _singletonEngine = Nothing
            End If
        End Function

        Public Sub AddAssembly(ByVal assembly As Assembly)
            _engineAssembly = assembly
        End Sub

        Public Function GetAssembly() As Assembly
            Return _engineAssembly
        End Function

        Public Function IsAssemblyNull() As Boolean
            Return (_engineAssembly Is Nothing)
        End Function

        Public Sub AddClassType(ByVal className As String, ByVal classType As Type)
            SyncLock _classTypes
                _classTypes.Add(className, classType)
            End SyncLock
        End Sub

        Public Function ContainsClass(ByVal className As String) As Boolean
            Return _classTypes.ContainsKey(className)
        End Function

        Public Function GetClassType(ByVal className As String) As Type
            If _classTypes.ContainsKey(className) Then
                Return _classTypes.Item(className)
            Else
                Return Nothing
            End If
        End Function

        Public Sub ClearAll()
            _engineAssembly = Nothing
            _classTypes.Clear()
        End Sub
    End Class
End Namespace