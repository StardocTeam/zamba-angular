Imports Zamba.Data

Public Class SectionDiagramBusiness


    Public Shared Function GetSectionAndEntities() As Generic.List(Of ISectionDiagram)
        Dim Sections As New Generic.List(Of ISectionDiagram)
        Dim SectionsFactory As New SectionDiagramFactory
        Dim SectionsDS As DataSet = SectionsFactory.GetSectionAndEntities()

        Dim lastSectionId As Int64 = 0
        For Each r As DataRow In SectionsDS.Tables(0).Rows
            If lastSectionId <> r.Item("SectionID") Then
                ' Sections = GetSectionsChild(r.Item("SectionParentId"), SectionsDS)

                Dim Section As New SectionDiagram
                Section.ID = r("SectionId")
                Section.Name = r("SectionNAME")
                Section.ObjectTypeId = r("SectionObjectTypeId")
                Section.ChildSection = GetSectionsChild(r.Item("SectionId"), SectionsDS)

                Section.EntitiesSection = GetEntitesBySectionId(Section.ID, SectionsDS)

                Sections.Add(Section)
            End If
            lastSectionId = r.Item("SectionID")
        Next

        Return Sections

    End Function


    'Public Shared Function GetSections() As ArrayList
    '    Dim docGroups As DataSet = Section_Factory.GetDocGroups()
    '    Dim arrList As ArrayList = New ArrayList

    '    For Each r As DataRow In docGroups.Tables(0).Rows
    '        Dim sd As New SectionDiagram
    '        sd.DocTypeGroupId = r("DOC_TYPE_GROUP_ID")
    '        sd.DocTypeGroupName = r("DOC_TYPE_GROUP_NAME")
    '        sd.Icon = r("ICON")
    '        sd.ParentSection = r("PARENT_ID")
    '        sd.ObjectTypeId = r("OBJECT_TYPE_ID")
    '        If sd.ParentSection = 0 Then
    '            sd.ChildSection = GetSectionsChild(sd.DocTypeGroupId)
    '            arrList.Add(sd)
    '        End If
    '    Next
    '    Return arrList
    'End Function

    Private Shared Function GetSectionsChild(SectionId As Int64, SectionsDs As DataSet) As Generic.List(Of ISectionDiagram)

        Dim Sections As New Generic.List(Of ISectionDiagram)

        Dim Dv As DataView = New DataView(SectionsDs.Tables(0))
        Dv.RowFilter = "SectionParentId = " & SectionId

        For Each r As DataRow In Dv.ToTable().Rows

            Dim Section As New SectionDiagram
            Section.ID = r("SectionId")
            Section.Name = r("SectionNAME")
            Section.ObjectTypeId = r("SectionObjectTypeId")

            '      Section.ChildSection = GetSectionsChild(Section.ID, SectionsDs)

            '      Section.EntitiesSection = GetEntitesBySectionId(Section.ID, SectionsDs)

            Sections.Add(Section)
        Next


        'Dim docGroups As DataSet = Section_Factory.GetDocGroups()
        'Dim arrList As ArrayList = New ArrayList
        'For Each r As DataRow In docGroups.Tables(0).Rows
        '    Dim sd As New SectionDiagram
        '    sd.DocTypeGroupId = r("DOC_TYPE_GROUP_ID")
        '    sd.DocTypeGroupName = r("DOC_TYPE_GROUP_NAME")
        '    sd.Icon = r("ICON")
        '    sd.ParentSection = r("PARENT_ID")
        '    sd.ObjectTypeId = r("OBJECT_TYPE_ID")

        '    If sd.ParentSection = id Then
        '        arrList.Add(sd)
        '    End If
        'Next
        Return Sections

    End Function

    Private Shared Function GetEntitesBySectionId(ByVal SectionID As Int64, SectionsDs As DataSet) As Generic.List(Of IEntityDiagram)
        Dim entities As New Generic.List(Of IEntityDiagram)
        Dim Dv As DataView = New DataView(SectionsDs.Tables(0))
        Dv.RowFilter = "SectionId = " & SectionID
        For Each r As DataRow In Dv.ToTable().Rows

            Dim Entity As New EntityDiagram
            Entity.ID = r("EntityId")
            Entity.Name = r("EntityNAME")
            Entity.ObjectTypeId = r("EntityObjectTypeId")

            entities.Add(Entity)
        Next
        Return entities

    End Function
End Class