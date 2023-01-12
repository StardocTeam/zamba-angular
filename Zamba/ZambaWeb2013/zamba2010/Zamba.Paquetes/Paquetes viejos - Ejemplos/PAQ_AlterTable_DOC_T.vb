'listo necesita que se ejecute otro paquete antes para crear la columna isvirtual
Imports Zamba.Servers
Imports Zamba.Core

Public Class PAQ_AlterTable_DOC_T
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_DOC_T"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_DOC_T
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Actualiza las doc_t, setea las columnas SHARED,NUMEROVERSION,ROOTID,VERSION,VER_PARENT_ID,ISVIRTUAL en 0."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/01/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.Installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property

    Public ReadOnly Property orden() As Int64 Implements IPAQ.Orden
        Get
            Return 44
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        'TODO store: SPGetDoc_type_id
        Dim strSelect As String
        Dim dsIds As DataSet
        Dim doc_t As String
        Dim cantidad As Int32 = 0
        Dim cantidad2 As Int32 = 0
        Dim cantidad3 As Int32 = 0
        Dim flagstop As Boolean = False
        'traigo ids de doc_type
        strSelect = "select doc_type_id from doc_type"
        dsIds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        cantidad3 = dsIds.Tables(0).Rows.Count
        For i As Integer = 0 To dsIds.Tables(0).Rows.Count - 1
            Try
                If flagstop = True Then Exit For
                cantidad2 += 1
                doc_t = dsIds.Tables(0).Rows(i).Item(0).ToString()
                strSelect = "UPDATE doc_t" & doc_t & " SET SHARED=0,NUMEROVERSION=0,ROOTID=0,VERSION=0,VER_PARENT_ID=0,ISVIRTUAL=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("doc_t" & doc_t) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, strSelect.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(strSelect.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
                cantidad += 1
            Catch ex As Exception
                If (MessageBox.Show(ex.ToString & ", ¿Desea continuar?", "", MessageBoxButtons.YesNo)) = DialogResult.No Then flagstop = True
            End Try
        Next
        MessageBox.Show("Se actualizaron " & cantidad & " de " & cantidad2 & " procesados de un total de " & cantidad3)
        Return True
    End Function
#End Region

End Class
