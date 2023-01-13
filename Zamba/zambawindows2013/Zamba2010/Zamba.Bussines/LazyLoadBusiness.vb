Imports System.Reflection
Imports System.Reflection.Assembly

''' <summary>
''' Esta clase se encarga de la implementacion del patron de diseño "Lazy Load".
''' </summary>
''' <remarks></remarks>
Public Class LazyLoadBusiness

    ''' <summary>
    ''' Nombre completo de esta clase en el ensamblado
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ASSEMBLY_FULL_PATH As String = "Zamba.Core.LazyLoadBusiness"

    ''' <summary>
    ''' Prefijo que tienen los nombres de los metodos para levantarlos por Reflection
    ''' </summary>
    ''' <remarks></remarks>
    Private Const METHOD_NAME_PREFIX As String = "Load"


    ''' <summary>
    ''' Se encarga de cargar completamente una instancia. Llama al metodo correspondiente a su clase para cargarlo por Reflection
    ''' </summary>
    ''' <param name="instance"></param>
    ''' <remarks></remarks>
    Public Shared Sub LoadInstance(ByVal instance As IZBaseCore)
        If Not IsNothing(instance) Then
            Dim FactoryBusiness As Type = GetExecutingAssembly.GetType(ASSEMBLY_FULL_PATH)

            If Not IsNothing(FactoryBusiness) Then
                FactoryBusiness.GetMethods(BindingFlags.Static)
                Dim name As String = instance.GetType.Name
                If TypeOf instance Is IRule Then
                    name = "ExecuteRule"
                End If
                Dim LoadedMethod As MethodInfo = FactoryBusiness.GetMethod(METHOD_NAME_PREFIX & name)

                If Not IsNothing(LoadedMethod) Then
                    Dim Parameter As Object() = {instance}
                    LoadedMethod.Invoke(Nothing, Parameter)
                    Array.Clear(Parameter, 0, 1)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="instance"></param>
    ''' <remarks></remarks>
    Public Shared Sub LoadZBaseCore(ByRef instance As IZBaseCore)
    End Sub


    'Public Shared Sub LoadUser(ByRef instance As IUser)
    '    LoadZBaseCore(instance)
    '    UserBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadZambaCore(ByRef instance As IZambaCore)
    '    LoadZBaseCore(instance)
    '    Results_Business.Fill(instance)
    'End Sub
    'Public Shared Sub LoadUserGroup(ByRef instance As IUserGroup)
    '    LoadZBaseCore(instance)
    '    UserGroupBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadVolume(ByRef instance As IVolume)
    '    LoadZBaseCore(instance)
    '    VolumesBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadVolumeList(ByRef instance As IVolumeList)
    '    LoadZBaseCore(instance)
    '    VolumeListsBusiness.Fill(instance)
    'End Sub

    'Public Shared Sub LoadDocGroup(ByRef instance As IDocGroup)
    '    LoadZambaCore(instance)
    '    Section_Factory.Fill(instance)
    'End Sub
    'Public Shared Sub LoadZBatch(ByRef instance As IZBatch)
    '    LoadZambaCore(instance)
    '    Results_Business.Fill(instance)
    'End Sub
    'Public Shared Sub LoadWFStep(ByRef instance As IWFStep)
    '    LoadZambaCore(instance)
    '    WFStepBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadDocType(ByRef instance As IDocType)
    '    LoadZambaCore(instance)
    '    DocTypesBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadWorkflow(ByRef instance As IWorkFlow)
    '    LoadZambaCore(instance)
    '    WFBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadIndex(ByRef instance As IIndex)
    '    LoadZambaCore(instance)
    '    IndexsBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadBaseImageFileResult(ByRef instance As IBaseImageFileResult)
    '    LoadZambaCore(instance)
    '    Results_Business.Fill(instance)
    'End Sub
    'Public Shared Sub LoadMensajeForo(ByRef instance As IMensajeForo)
    '    LoadZambaCore(instance)
    '    MessagesBusiness.Fill(instance)
    'End Sub
    'Public Shared Sub LoadWFRuleParent(ByRef instance As IWFRuleParent)
    '    LoadZambaCore(instance)
    '    WFRulesBusiness.Fill(instance)
    'End Sub

    'Public Shared Sub LoadResult(ByRef instance As IResult)
    '    LoadBaseImageFileResult(instance)
    '    Results_Business.Fill(instance)
    'End Sub

    'Public Shared Sub LoadPublishState(ByRef instance As IPublishState)
    '    LoadResult(instance)
    '    Results_Business.Fill(instance)
    'End Sub
    'Public Shared Sub LoadTaskResult(ByRef instance As ITaskResult)
    '    LoadResult(instance)
    '    Results_Business.Fill(instance)
    'End Sub
    'Public Shared Sub LoadNewResult(ByRef instance As INewResult)
    '    LoadResult(instance)
    '    Results_Business.Fill(instance)
    'End Sub
    'Public Shared Sub LoadPublishableResult(ByRef instance As Ipublishable)
    '    LoadResult(instance)
    '    Results_Business.Fill(instance)
    'End Sub

    ''' <summary>
    ''' Load the Rule
    ''' </summary>
    ''' <param name="instance"></param>
    ''' <History>Marcelo modified 30/09/2009
    ''' Commented because it wasn't finished and was generating errors on runtime
    '''</History>
    ''' <remarks></remarks>



    ''' <summary>
    ''' Load Rule
    ''' </summary>
    ''' <param name="instance"></param>
    ''' <history>
    '''         Marcelo Modified    01/10/2009 Code Commented until its development
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Sub LoadExecuteRule(ByRef instance As IRule)
        'LoadZBaseCore(instance)
        'WFRulesBusiness.Fill(instance)
    End Sub
End Class