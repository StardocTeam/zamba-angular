Imports Zamba.Data
Imports System.Collections.Generic

Public Class WFServiceManagerBusiness

    Private _versionActual As Integer
    Private _versionNueva As Integer
    Private _pathActual As String
    Private _pathNueva As String
    Private _pathBackup As String
    Private _actualizar As Boolean
    Private _actualizado As Boolean
    Private _fechaUpdate As Date
    Private _forzarUpdate As Boolean
    Private _fallas As Integer

    Private _MonversionActual As Integer
    Private _MonversionNueva As Integer
    Private _MonpathActual As String
    Private _MonpathNueva As String
    Private _MonpathBackup As String
    Private _Monactualizar As Boolean
    Private _Monactualizado As Boolean
    Private _MonfechaUpdate As Date
    Private _MonforzarUpdate As Boolean
    Private _Monfallas As Integer

    Public Property VersionActual() As Integer
        Get
            Return _versionActual
        End Get
        Set(ByVal value As Integer)
            _versionActual = value
        End Set
    End Property

    Public Property VersionNueva() As Integer
        Get
            Return _versionNueva
        End Get
        Set(ByVal value As Integer)
            _versionNueva = value
        End Set
    End Property

    Public Property PathActual() As String
        Get
            Return _pathActual
        End Get
        Set(ByVal value As String)
            _pathActual = value
        End Set
    End Property

    Public Property PathNueva() As String
        Get
            Return _pathNueva
        End Get
        Set(ByVal value As String)
            _pathNueva = value
        End Set
    End Property

    Public Property PathBackup() As String
        Get
            Return _pathBackup
        End Get
        Set(ByVal value As String)
            _pathBackup = value
        End Set
    End Property

    Public Property Actualizar() As Boolean
        Get
            Return _actualizar
        End Get
        Set(ByVal value As Boolean)
            _actualizar = value
        End Set
    End Property

    Public Property Actualizado() As Boolean
        Get
            Return _actualizado
        End Get
        Set(ByVal value As Boolean)
            _actualizado = value
        End Set
    End Property

    Property FechaUpdate() As Date
        Get
            Return _fechaUpdate
        End Get
        Set(ByVal value As Date)
            _fechaUpdate = value
        End Set
    End Property

    Public Property ForzarUpdate() As Boolean
        Get
            Return _forzarUpdate
        End Get
        Set(ByVal value As Boolean)
            _forzarUpdate = value
        End Set
    End Property

    Public Property Fallas() As Integer
        Get
            Return _fallas
        End Get
        Set(ByVal value As Integer)
            _fallas = value
        End Set
    End Property







    Public Property MonVersionActual() As Integer
        Get
            Return _MonversionActual
        End Get
        Set(ByVal value As Integer)
            _MonversionActual = value
        End Set
    End Property

    Public Property MonVersionNueva() As Integer
        Get
            Return _MonversionNueva
        End Get
        Set(ByVal value As Integer)
            _MonversionNueva = value
        End Set
    End Property

    Public Property MonPathActual() As String
        Get
            Return _MonpathActual
        End Get
        Set(ByVal value As String)
            _MonpathActual = value
        End Set
    End Property

    Public Property MonPathNueva() As String
        Get
            Return _MonpathNueva
        End Get
        Set(ByVal value As String)
            _MonpathNueva = value
        End Set
    End Property

    Public Property MonPathBackup() As String
        Get
            Return _MonpathBackup
        End Get
        Set(ByVal value As String)
            _MonpathBackup = value
        End Set
    End Property

    Public Property MonActualizar() As Boolean
        Get
            Return _Monactualizar
        End Get
        Set(ByVal value As Boolean)
            _Monactualizar = value
        End Set
    End Property

    Public Property MonActualizado() As Boolean
        Get
            Return _Monactualizado
        End Get
        Set(ByVal value As Boolean)
            _Monactualizado = value
        End Set
    End Property

    Property MonFechaUpdate() As Date
        Get
            Return _MonfechaUpdate
        End Get
        Set(ByVal value As Date)
            _MonfechaUpdate = value
        End Set
    End Property

    Public Property MonForzarUpdate() As Boolean
        Get
            Return _MonforzarUpdate
        End Get
        Set(ByVal value As Boolean)
            _MonforzarUpdate = value
        End Set
    End Property

    Public Property MonFallas() As Integer
        Get
            Return _Monfallas
        End Get
        Set(ByVal value As Integer)
            _Monfallas = value
        End Set
    End Property







    Public Sub New()

        Dim WF_DB As New WFServiceManagerFactory
        Dim ds As DataSet = WF_DB.getOptions()

        If Not ds Is Nothing Then

            If ds.Tables(0).Rows.Count > 0 Then

                If Not ds.Tables(0).Rows(0).Item("Version_Act").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("Version_Act").ToString, _versionActual)
                End If

                If Not ds.Tables(0).Rows(0).Item("Version_Upd").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("Version_Upd").ToString, _versionNueva)
                End If

                If Not ds.Tables(0).Rows(0).Item("Path_Actual").ToString Is Nothing Then
                    _pathActual = ds.Tables(0).Rows(0).Item("Path_Actual").ToString
                End If

                If Not ds.Tables(0).Rows(0).Item("Path_Nuevo").ToString Is Nothing Then
                    _pathNueva = ds.Tables(0).Rows(0).Item("Path_Nuevo").ToString
                End If

                If Not ds.Tables(0).Rows(0).Item("Path_Backup").ToString Is Nothing Then
                    _pathBackup = ds.Tables(0).Rows(0).Item("Path_Backup").ToString
                End If

                If Not ds.Tables(0).Rows(0).Item("Actualizar").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("Actualizar").ToString, _actualizar)
                End If

                If Not ds.Tables(0).Rows(0).Item("Actualizado").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("Actualizado").ToString, _actualizado)
                End If

                If Not ds.Tables(0).Rows(0).Item("Fecha_Upd").ToString Is Nothing Then
                    Date.TryParse(ds.Tables(0).Rows(0).Item("Fecha_Upd").ToString, _fechaUpdate)
                End If

                If Not ds.Tables(0).Rows(0).Item("Forzar_Upd").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("Forzar_Upd").ToString, _forzarUpdate)
                End If

                If Not ds.Tables(0).Rows(0).Item("Fallas").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("Fallas").ToString, _fallas)
                End If





                If Not ds.Tables(0).Rows(0).Item("MON_Version_Act").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("MON_Version_Act").ToString, _MonversionActual)
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Version_Upd").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("MON_Version_Upd").ToString, _MonversionNueva)
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Path_Actual").ToString Is Nothing Then
                    _MonpathActual = ds.Tables(0).Rows(0).Item("MON_Path_Actual").ToString
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Path_Nuevo").ToString Is Nothing Then
                    _MonpathNueva = ds.Tables(0).Rows(0).Item("MON_Path_Nuevo").ToString
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Path_Backup").ToString Is Nothing Then
                    _MonpathBackup = ds.Tables(0).Rows(0).Item("MON_Path_Backup").ToString
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Actualizar").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("MON_Actualizar").ToString, _Monactualizar)
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Actualizado").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("MON_Actualizado").ToString, _Monactualizado)
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Fecha_Upd").ToString Is Nothing Then
                    Date.TryParse(ds.Tables(0).Rows(0).Item("MON_Fecha_Upd").ToString, _MonfechaUpdate)
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Forzar_Upd").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("MON_Forzar_Upd").ToString, _MonforzarUpdate)
                End If

                If Not ds.Tables(0).Rows(0).Item("MON_Fallas").ToString Is Nothing Then
                    Int32.TryParse(ds.Tables(0).Rows(0).Item("MON_Fallas").ToString, _Monfallas)
                End If



            End If

        End If

        WF_DB = Nothing

    End Sub

    Public Function MustUpdate()
        If VersionNueva > VersionActual AndAlso Fallas < 3 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ServiceUpdated()

        Dim WF_DB As New WFServiceManagerFactory
        WF_DB.ServiceUpdated()
        WF_DB = Nothing

    End Function

    Public Function ServiceUpdateFailed()

        Dim WF_DB As New WFServiceManagerFactory
        WF_DB.ServiceUpdateFailed()
        WF_DB = Nothing

    End Function

    Public Function ServiceMonUpdated()

        Dim WF_DB As New WFServiceManagerFactory
        WF_DB.ServiceMonUpdated()
        WF_DB = Nothing

    End Function

    Public Function ServiceMonUpdateFailed()

        Dim WF_DB As New WFServiceManagerFactory
        WF_DB.ServiceMonUpdateFailed()
        WF_DB = Nothing

    End Function




    Public Function MonMustUpdate()
        If MonVersionNueva > MonVersionActual AndAlso MonFallas < 3 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function MonitoreoUpdated()

        Dim WF_DB As New WFServiceManagerFactory
        WF_DB.MonitoreoUpdated()
        WF_DB = Nothing

    End Function

    Public Function MonitoreoUpdateFailed()

        Dim WF_DB As New WFServiceManagerFactory
        WF_DB.MonitoreoUpdateFailed()
        WF_DB = Nothing

    End Function

    Public Shared Function GetActiveAction() As DataSet
        Return WFServiceManagerFactory.GetActiveAction()
    End Function

    Public Shared Function GetNextAction() As DataSet
        Return WFServiceManagerFactory.GetNextAction()
    End Function

    Public Shared Sub ActiveAction(ByVal id As Int16)
        WFServiceManagerFactory.ActiveAction(id)
    End Sub

    Public Shared Sub DeleteAction(ByVal id As Int16)
        WFServiceManagerFactory.DeleteAction(id)
    End Sub

    Public Shared Sub DeleteAllActions()
        WFServiceManagerFactory.DeleteAllActions()
    End Sub

    ''' <summary>
    ''' Inserta una accion para el servicio
    ''' </summary>
    ''' <param name="serviceID"></param>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertAction(ByVal serviceID As Int32, ByVal action As ServiceManagerAction)
        Trace.WriteLineIf(ZTrace.IsInfo, "Insertando accion: " & action.ToString() & " para el servicio: " & serviceID)
        WFServiceManagerFactory.InsertAction(serviceID, action)
    End Sub

    Public Shared Function GetServiceAction(ByVal serviceID As Int32) As DataSet
        Return WFServiceManagerFactory.GetServiceAction(serviceID)
    End Function

    Public Shared Function GetServicesPendingActions() As DataSet
        Return WFServiceManagerFactory.GetServicesPendingActions
    End Function
End Class