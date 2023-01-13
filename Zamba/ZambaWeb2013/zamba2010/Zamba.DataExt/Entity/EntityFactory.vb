Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.Collections.Generic

Public Class EntityFactory
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")> Public Function GetEntitiesByRightType(RightType As Int64) As ArrayList

        Dim dr As IDataReader
        Dim parValues() As Object
        Dim parNames() As String
        ' Dim parTypes() As Object
        Dim dstemp As DataSet
        Dim DocTypes As New ArrayList
        Dim i As Int32

        Try
            If Server.IsOracle Then
                parNames = New String() {"righttype", "io_cursor"}
                parTypes = New Object() {13, 5}
                parValues = New Object() {CInt(RightType), 2}

                'dstemp = Server.Con.ExecuteDataset("zsp_doctypes_300.GetDocTypesByUserRights",  parValues)
                dstemp = Server.Con.ExecuteDataset("zsp_doctypes_300_GetDocTypesByRights", parValues)

                For i = 0 To dstemp.Tables(0).Rows.Count - 1
                    Dim DocType As New DocType(Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DOC_TYPE_ID")), _
                    dstemp.Tables(0).Rows(i).Item("DOC_TYPE_NAME").ToString(), _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("FILE_FORMAT_ID")), _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DISK_GROUP_ID")), _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("THUMBNAILS")), _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("ICON_ID")), _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("OBJECT_TYPE_ID")), _
                    dstemp.Tables(0).Rows(i).Item("AUTONAME").ToString(), _
                    dstemp.Tables(0).Rows(i).Item("AUTONAME").ToString(), _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DOCCOUNT")), 0, _
                    Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DOCUMENTALID")))
                    DocTypes.Add(DocType)
                Next
                Return DocTypes
            Else
                parValues = New Object() {RightType}
                dr = Server.Con.ExecuteReader("zsp_doctypes_300_GetDocTypesByRights", parValues)

                While dr.Read
                    Dim DocType As New DocType(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOC_TYPE_ID"))), _
                    dr.GetValue(dr.GetOrdinal("DOC_TYPE_NAME")).ToString(), _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FILE_FORMAT_ID"))), _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DISK_GROUP_ID"))), _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("THUMBNAILS"))), _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ICON_ID"))), _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OBJECT_TYPE_ID"))), _
                    dr.GetValue(dr.GetOrdinal("AUTONAME")).ToString(), _
                    dr.GetValue(dr.GetOrdinal("AUTONAME")).ToString(), _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOCCOUNT"))), 0, _
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOCUMENTALID"))))
                    DocTypes.Add(DocType)
                End While
                Return DocTypes
            End If
        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr.Dispose()
                dr = Nothing
            End If
            parValues = Nothing
            parNames = Nothing
            parTypes = Nothing
            If Not IsNothing(dstemp) Then
                dstemp.Dispose()
                dstemp = Nothing
            End If
            If Not IsNothing(DocTypes) Then
                DocTypes = Nothing
            End If
            i = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el ID de las entidades que utilizan el volumen
    ''' </summary>
    ''' <param name="DiskGroupID"></param>
    ''' <history>Marcelo Created 14/12/12</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocTypesIdsByDiskGroupID(ByVal VolumeID As Int64) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "select DOC_TYPE_ID from DISK_GROUP_R_DISK_VOLUME inner join DOC_TYPE on DOC_TYPE.DISK_GROUP_ID = DISK_GROUP_R_DISK_VOLUME.DISK_GROUP_ID where DISK_VOLUME_ID = " & VolumeID)
    End Function
End Class