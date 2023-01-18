Imports ZAMBA.Servers
Imports Zamba.Core
Imports Zamba.data
Public Class ResultsComponent
    Inherits ZClass

    Public Shared Sub DeleteTempFiles()
        Try
            Dim i As Int32
            Dim Fi As IO.FileInfo
            For i = 0 To Results_Factory.FilesForDelete.Count - 1
                Try
                    Fi = Results_Factory.FilesForDelete(i)
                    Fi.Delete()
                    Results_Factory.FilesForDelete.RemoveAt(i)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#Region "Eventos"
    Event CountingDoc()
    Event ReadingDocFile(ByVal DocCount As Int32)
    Event GettingDocumentData(ByVal DocumentId As Int32)
    Event DocumentMigrated(ByVal Document As Result)
    Event MigrationFinalized(ByVal DocCount As Int32, ByVal DocError As Int32)
    Event MigrationError(ByVal Errormessage As String)
    Event DocumentMigrationError(ByVal Document As Result)
    Event VolumeIsFull()
#End Region
    Public Shared Sub MoveTempFiles(ByVal DocTypeId As Int64)
        'Try
        '    Dim VolumeId As Int32 = Volumes.RetrieveDiskGroupId(DocTypeId)
        '    Dim Volume As Volume = Volumes.GetVolume(VolumeId)
        '    If Volume.state = 1 Then
        '        'significa que el volumen esta lleno
        '        RaiseEvent VolumeIsFull()
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        '    RaiseEvent VolumeIsFull()
        '    Exit Sub
        'End Try

        'Dim Dr As IDataReader

        'Try
        '    RaiseEvent CountingDoc()
        '    Dim Table As String = Documents.MakeTable(DocTypeId, Documents.TableType.Document)
        '    Dim Strselect As String = "SELECT COUNT(DOC_ID) FROM " & Table & " WHERE VOL_ID < 0"

        '    Dim Count As Int64 = Server.Con.ExecuteScalar(CommandType.Text, Strselect)

        '    Strselect = "SELECT DOC_ID,DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,DOC_TYPE_ID FROM " & Table & " WHERE VOL_ID < 0"

        '    Dim Procesed As Int64 = 0

        '    Dim DocCount As Int32 = 0
        '    Dim DocError As Int32 = 0

        '    While Procesed < Count

        '        Dr = Server.Con.ExecuteReader(CommandType.Text, Strselect)
        '        Dim Read As Int16 = 0
        '        While Dr.Read And Read < 11
        '            Procesed += +1
        '            Read += +1
        '            Dim TempVolId As Int32 = 0
        '            Dim TempVolOffset As Int32 = 0
        '            Dim Document As New Result
        '            DocCount += +1
        '            RaiseEvent ReadingDocFile(DocCount)
        '            Document.Id = Dr.GetInt32(0)
        '            Document.DocTypeId = Dr.GetInt32(6)
        '            Documents.GetDocumentData(Document)
        '            RaiseEvent GettingDocumentData(Document.Id)
        '            TempVolId = Dr.GetInt32(3)
        '            TempVolOffset = Dr.GetInt32(5)
        '            Try
        '                MoveDocument(Document, EstationId, TempVolId, TempVolOffset)
        '                RaiseEvent DocumentMigrated(Document)
        '            Catch ex As Exception
        '               zamba.core.zclass.raiseerror(ex)
        '                DocError += +1
        '                RaiseEvent DocumentMigrationError(Document)
        '            End Try

        '            'Verifico la cancelacion
        '            If Me.FlagGoOn = False Then
        '                If MsgBox("Esta seguro que desea cancelar el proceso de migracion?", MsgBoxStyle.YesNo, "Cancelacion Proceso de Migracion") = MsgBoxResult.Yes Then
        '                    Me.Canceled = True
        '                    Exit While
        '                Else
        '                    Me.FlagGoOn = True
        '                    Me.Canceled = False
        '                End If
        '            End If
        '        End While
        '        Try
        '            Server.Con.Command.Cancel()
        '        Catch ex As Exception
        '        End Try
        '        Dr.Close()
        '        'Verifico la cancelacion
        '        If Me.FlagGoOn = False Then
        '            If Canceled = True Then Exit While
        '            If MsgBox("Esta seguro que desea cancelar el proceso de migracion?", MsgBoxStyle.YesNo, "Cancelacion Proceso de Migracion") = MsgBoxResult.Yes Then
        '                Me.Canceled = True
        '                Exit While
        '            Else
        '                Me.FlagGoOn = True
        '                Me.Canceled = False
        '            End If
        '        End If
        '    End While
        '    Me.FlagGoOn = True
        '    RaiseEvent MigrationFinalized(DocCount, DocError)
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        '    RaiseEvent MigrationError(ex.tostring)
        'Finally
        '    Try
        '        Server.Con.Command.Cancel()
        '    Catch ex As Exception
        '    End Try
        '    Dr.Close()
        'End Try
    End Sub

    Private _flaggoon As Boolean = True
    Public Property FlagGoOn() As Boolean
        Get
            Return _flaggoon
        End Get
        Set(ByVal Value As Boolean)
            _flaggoon = Value
        End Set
    End Property
    Public Overrides Sub Dispose()

    End Sub
End Class
