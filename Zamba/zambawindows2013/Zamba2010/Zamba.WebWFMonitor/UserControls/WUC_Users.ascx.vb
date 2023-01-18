Imports Zamba.Core
Imports Zamba.WFBusiness

Partial Class WUC_Asignar
    Inherits System.Web.UI.UserControl

    Dim HashUsers As New Hashtable

    Public Sub LoadUsers(ByVal StepId As Int32)
        If StepId = 0 Then Exit Sub
        Dim Wf As WorkFlow
        Dim DsWF As DsWF = WFBusiness.GetAllWorkflows
        Dim W As WorkFlow() = Zamba.Data.WFFactory.GetWFs
        For Each WW As WorkFlow In W
            If WW.Id = Session("WfId") Then
                Wf = WW
                Exit For
            End If
        Next

        Dim s As WFStep = Zamba.Data.WFStepsFactory.GetStep(Wf, StepId)
        Zamba.WFBusiness.WFBusiness.FillUsersAndGroups(s)

        Dim M As New SortedList
        For Each u As User In s.Users.Values
            Dim g As New User.UserView(u)
            M.Add(g.Id, g)
        Next

        Me.DropDownList1.DataSource = M.Values
        Me.DropDownList1.DataTextField = "Nombre"
        Me.DropDownList1.DataValueField = "Id"
        Me.DropDownList1.DataBind()
    End Sub


    'Public Sub LoadUsers(ByVal SelectedResults As ArrayList)
    '    Try

    '        Dim Array As New ArrayList
    '        For Each R As TaskResult In SelectedResults
    '            Zamba.WFBussines.WFBussines.FillUsersAndGroups(R.WfStep)
    '            For Each U As User In R.WfStep.Users.Values
    '                If Not U.id = 0 Then
    '                    Dim EstaEnTodos As Boolean = False
    '                    For Each RR As TaskResult In SelectedResults
    '                        For Each UU As User In R.WfStep.Users.Values
    '                            If UU Is U Then
    '                                EstaEnTodos = True
    '                                Exit For
    '                            Else
    '                                EstaEnTodos = False
    '                            End If
    '                        Next
    '                    Next

    '                    Dim UUU As New User.UserView(U)
    '                    If EstaEnTodos = True AndAlso ArrayContainsId(Array, UUU.Id) = False Then Array.Add(UUU)

    '                End If
    '            Next
    '        Next

    '        Me.ListBox1.DataSource = Array
    '        Me.ListBox1.DataTextField = "Nombre"
    '        Me.ListBox1.DataValueField = "Id"
    '        Me.ListBox1.DataBind()

    '    Catch ex As Exception
    '        RaiseError(ex)
    '    End Try
    'End Sub

    Private Function ArrayContainsId(ByVal Array As ArrayList, ByVal Id As Int32) As Boolean
        For Each Item As User.UserView In Array
            If Item.Id = ID Then Return True
        Next
        Return False
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadUsers(Session("StepId"))
    End Sub


    Public Sub RemoveHandlers()
        RemoveHandler DropDownList1.SelectedIndexChanged, AddressOf ListBox1_SelectedIndexChanged
    End Sub


    Protected Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Session.Add("SelUser", Me.DropDownList1.SelectedValue)
    End Sub
End Class
