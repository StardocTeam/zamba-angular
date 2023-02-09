'Namespace Caching
Imports Zamba.Data

Public Class CacheBusiness
    Public Shared Sub ClearCaches()
        Cache.Rules.ClearAll()
        Cache.Rules.RemoveCurrentInstance()
        Cache.RulesOptions.ClearAll()

        Cache.ZModule.RemoveCurrentInstance()
        Cache.DocTypes.RemoveCurrentInstance()
        Cache.DocTypesWF.RemoveCurrentInstance()
        Cache.RestrictionsStrings.RemoveCurrentInstance()
        DynamicButtonBusiness.RemoveCurrentInstance()
        Cache.DocAsociations.RemoveCurrentInstance()
        Cache.GlobalRulesEngine.RemoveCurrentInstance()
        Cache.SpecificIndexsRights.RemoveCurrentInstance()
        Cache.StepsOpt.RemoveCurrentInstance()

        Cache.Forms.RemoveCurrentInstance()
        Cache.Outlook.RemoveCurrentInstance()
        Cache.DocTypesAndIndexs.RemoveCurrentInstance()
        Cache.UsersAndGroups.RemoveCurrentInstance()
        Cache.Volumes.RemoveCurrentInstance()
        Cache.Workflows.RemoveCurrentInstance()


        ZCore.RemoveCurrentInstance()

        Membership.MembershipHelper.CurrentSession.Clear()

        RightFactory.Permisos.Clear()

    End Sub

    Public Shared Sub ClearCachesByUser(userId As Int64)
        'Cache.Rules.ClearAll()
        'Cache.Rules.RemoveCurrentInstance()
        'Cache.RulesOptions.ClearAll()

        'Cache.ZModule.RemoveCurrentInstance()
        'Cache.DocTypes.RemoveCurrentInstance()
        'Cache.DocTypesWF.RemoveCurrentInstance()
        'Cache.RestrictionsStrings.RemoveCurrentInstance()
        'DynamicButtonBusiness.RemoveCurrentInstance()
        'Cache.DocAsociations.RemoveCurrentInstance()
        'Cache.GlobalRulesEngine.RemoveCurrentInstance()
        'Cache.SpecificIndexsRights.RemoveCurrentInstance()
        'Cache.StepsOpt.RemoveCurrentInstance()

        'Cache.Forms.RemoveCurrentInstance()
        'Cache.Outlook.RemoveCurrentInstance()
        'Cache.DocTypesAndIndexs.RemoveCurrentInstance()
        'Cache.UsersAndGroups.RemoveCurrentInstance()
        'Cache.Volumes.RemoveCurrentInstance()
        'Cache.Workflows.RemoveCurrentInstance()


        'ZCore.RemoveCurrentInstance()

        'Membership.MembershipHelper.CurrentSession.Clear()

        'RightFactory.Permisos.Clear()

    End Sub

    Public Shared Sub ClearRulesCaches()
        Cache.Rules.RemoveCurrentInstance()
        Cache.RulesOptions.ClearAll()
        DynamicButtonBusiness.RemoveCurrentInstance()
        Cache.GlobalRulesEngine.RemoveCurrentInstance()
        Cache.StepsOpt.RemoveCurrentInstance()
        Cache.Workflows.RemoveCurrentInstance()



    End Sub


    Public Shared Sub ClearRightsCaches(userId)

        Cache.ZModule.RemoveCurrentInstance()
        Cache.RestrictionsStrings.RemoveCurrentInstance()
        Cache.SpecificIndexsRights.RemoveCurrentInstance()
        Cache.UsersAndGroups.RemoveCurrentInstance()
        Membership.MembershipHelper.CurrentSession.Clear()

    End Sub

    Public Shared Sub ClearStructureCaches()
        Cache.DocTypes.RemoveCurrentInstance()
        Cache.DocTypesWF.RemoveCurrentInstance()

        Cache.DocAsociations.RemoveCurrentInstance()

        Cache.Forms.RemoveCurrentInstance()
        Cache.Outlook.RemoveCurrentInstance()
        Cache.DocTypesAndIndexs.RemoveCurrentInstance()
        Cache.Volumes.RemoveCurrentInstance()
        ZCore.RemoveCurrentInstance()

    End Sub

End Class
