Public Class DBToolsFactory
    Public Shared Sub ReEnumerarColumna(ByVal Tabla As String, ByVal Columna As String)
        Try
            If Server.isOracle Then
                'Dim StrSelect As String = "Select " & Columna & " from " & Tabla & " Order by " & Columna
                Dim StrUpdate As String
                ' Dim Ds As DataSet
                ' Ds = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
                StrUpdate = "Update " & Tabla & " Set " & Columna & "=rownum"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
            Else

                'TODO Falta para SQL
                MessageBox.Show("Esta opción es solo para Servidores Oracle", "Zamba Software - Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la cantidad de documentos insertados para cada Entidad
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ContarDocTypes()
        'Inserta en la Tabla Doc_Type, Columna DocCount la cantidad que hay de cada entidad
        'Es utilizado despúes de la Función GetDocCount() As DataSet
        Try
            Dim DSTablas As New DataSet
            Dim SelectTablas As String = "Select Doc_type_ID from Doc_Type order By Doc_Type_ID"
            DSTablas = Server.Con.ExecuteDataset(CommandType.Text, SelectTablas)
            Dim I As Int32
            Dim StrUpdate As String
            While I < DSTablas.Tables(0).Rows.Count
                StrUpdate = "Update DOC_Type SET DocCount=(Select count(1) from Doc_T" & DSTablas.Tables(0).Rows(I).Item(0) & ") Where Doc_Type_ID=" & CStr(DSTablas.Tables(0).Rows(I).Item(0))
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
                I = I + 1
            End While
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Function GetDocCount(ByVal ID As Int32) As Int32
        'Devuelve la cantidad de Documentos que hay de un mismo tipo
        Try
            Dim Tabla As String = "Doc_T" & (ID)
            Dim Ds As DataSet
            Dim StrSelect As String = "Select count(1) from " & Tabla
            Ds = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

            Return CInt(Ds.Tables(0).Rows(0).Item(0))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
    Public Shared Function GetDocCount() As DataSet
        Try
            Dim StrSelect As String = "Select Doc_Type_Name as Nombre, DocCount as Cantidad from Doc_Type order by Doc_Type_Name"
            Dim Ds As DataSet
            Ds = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            Return Ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Shared Function GetAllDocIByResultid(ByVal DocID As Int32) As DataSet
        Dim Ds As DataSet

        Dim StrSelect As String = "Select * from Doc_I" & DocID
        Ds = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return Ds
    End Function

    Public Shared Function GetActiveDatabase() As String
        Return Server.DB
    End Function

    Public Shared Function GetServerType() As String
        Return Server.ServerType.ToString
    End Function

    Public Shared Function IsOracle() As Boolean
        Return Server.isOracle()
    End Function

End Class
