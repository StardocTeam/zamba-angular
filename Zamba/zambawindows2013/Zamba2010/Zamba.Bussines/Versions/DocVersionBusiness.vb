''' <summary>
''' Clase creada para manejar las opciones de las versiones en Zamba.
''' Se diseño para que trabaje como las clases de cache.
''' </summary>
''' <history>
'''     [Tomas]    05/04/2010	Created    
''' </history>
Public Class DocVersionBusiness
    Private Shared _docVersionType As DocVersion
    Private Shared _replaceWfDocVersion As Boolean
    Private Shared _showAssocLastVersion As Boolean
    Private Shared _showConverstationsOfAllVersions As Boolean
    Private Shared _useVersionRight As Boolean
    Private Shared firstTimeVersionType As Boolean = True
    Private Shared firstTimeReplaceVersion As Boolean = True
    Private Shared firstTimeShowAssocLastVersion As Boolean = True
  
    Private Shared firstTimeUseVersionRight As Boolean = True

    Public Shared ReadOnly Property VersionType() As DocVersion
        Get
            If firstTimeVersionType Then
                Dim version As String = ZOptBusiness.GetValue("DocVersionMode")
                If String.IsNullOrEmpty(version) Then
                    ZOptBusiness.Insert("DocVersionMode", DocVersion.Arbol)
                    _docVersionType = DocVersion.Arbol
                Else
                    _docVersionType = Int32.Parse(version)
                End If
                firstTimeVersionType = False
                version = Nothing
            End If
            Return _docVersionType
        End Get
    End Property
    Public Shared ReadOnly Property ReplaceWfDocVersion() As Boolean
        Get
            If firstTimeReplaceVersion Then
                Dim replace As String = ZOptBusiness.GetValue("ReplaceWfDocVersion")
                If String.IsNullOrEmpty(replace) Then
                    ZOptBusiness.Insert("ReplaceWfDocVersion", "False")
                    _replaceWfDocVersion = False
                Else
                    _replaceWfDocVersion = Boolean.Parse(replace)
                End If
                firstTimeReplaceVersion = False
                replace = Nothing
            End If
            Return _replaceWfDocVersion
        End Get
    End Property
    Public Shared ReadOnly Property ShowAssocLastVersion() As Boolean
        Get
            If firstTimeShowAssocLastVersion Then
                Dim show As String = ZOptBusiness.GetValue("ShowAssocLastVersion")
                If String.IsNullOrEmpty(show) Then
                    ZOptBusiness.Insert("ShowAssocLastVersion", "False")
                    _showAssocLastVersion = False
                Else
                    _showAssocLastVersion = Boolean.Parse(show)
                End If
                firstTimeShowAssocLastVersion = False
                show = Nothing
            End If
            Return _showAssocLastVersion
        End Get
    End Property
  
    Public Shared ReadOnly Property UseVersion() As Boolean
        Get
            If firstTimeUseVersionRight Then
                _useVersionRight = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleVersions, RightsType.Use)
            End If
            Return _useVersionRight
        End Get
    End Property

    Public Shared Sub SaveVersionOption(ByVal docVerType As DocVersion)
        If firstTimeVersionType Then
            ZOptBusiness.Insert("DocVersionMode", docVerType)
            firstTimeVersionType = False
        Else
            ZOptBusiness.Update("DocVersionMode", docVerType)
        End If
        _docVersionType = docVerType
    End Sub
    Public Shared Sub SaveReplaceOption(ByVal replace As Boolean)
        If firstTimeReplaceVersion Then
            ZOptBusiness.Insert("ReplaceWfDocVersion", replace)
            firstTimeReplaceVersion = False
        Else
            ZOptBusiness.Update("ReplaceWfDocVersion", replace)
        End If
        _replaceWfDocVersion = replace
    End Sub
    Public Shared Sub SaveShowLastAssocOption(ByVal showLast As Boolean)
        If firstTimeShowAssocLastVersion Then
            ZOptBusiness.Insert("ShowAssocLastVersion", showLast)
            firstTimeShowAssocLastVersion = False
        Else
            ZOptBusiness.Update("ShowAssocLastVersion", showLast)
        End If
        _showAssocLastVersion = showLast
    End Sub
   
    Public Shared Sub ForceFirstTime()
        firstTimeVersionType = True
        firstTimeReplaceVersion = True
        firstTimeShowAssocLastVersion = True
        firstTimeUseVersionRight = True
    End Sub
End Class
