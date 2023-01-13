Imports System.Text
Imports Zamba.Core

''' <summary>
''' Elimina aquellas tareas que no tienen un documento asociado.
''' </summary>
''' <history>
''' Tomas   23/05/2011  Created
''' </history>
''' <remarks></remarks>
Public Class PAQ_LimpiarWfDocument
    Inherits ZPaq
    Implements IPAQ

#Region "Atributos y propiedades"
    Private Const _name As String = "PAQ_LimpiarWfDocument"
    Private Const _description As String = "Elimina aquellas tareas que no tienen un documento asociado."
    Private Const _version As String = "1"
    Private Const fechaCreacion As String = "23/05/2011"
    Private _installed As Boolean

    Public ReadOnly Property Description() As String Implements IPAQ.Description
        Get
            Return _description
        End Get
    End Property
    Public Property Installed() As Boolean Implements IPAQ.Installed
        Get
            Return _installed
        End Get
        Set(ByVal value As Boolean)
            _installed = value
        End Set
    End Property
    Public ReadOnly Property Name() As String Implements IPAQ.Name
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_Normalizar_DoctDiskvolume
        End Get
    End Property
    Public ReadOnly Property Orden() As Long Implements IPAQ.Orden
        Get
            Return 3
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse(fechaCreacion)
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return _version
        End Get
    End Property
    Public ReadOnly Property DependenciesIDs() As System.Collections.Generic.List(Of Int64) Implements IPAQ.DependenciesIDs
        Get
            Return New Generic.List(Of Int64)
        End Get
    End Property

    Public Overrides Sub Dispose() Implements IDisposable.Dispose

    End Sub
#End Region

#Region "Métodos"
    Public Function execute() As Boolean Implements IPAQ.Execute
        ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando los tipos de documento" & vbCrLf)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)

        Dim dtTemp As DataTable = EDataTable("SELECT DOC_TYPE_ID FROM DOC_TYPE")
        Dim docTypeCount As Int32 = dtTemp.Rows.Count
        Dim doct As String
        Dim resultado As Boolean
        Dim arrLeftJoins(docTypeCount - 1) As String
        Dim arrWhereClauses(docTypeCount - 1) As String
        Dim sb As New StringBuilder
        Dim i As Int32

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Armando la consulta para obtener las tareas sin su valor en las DOC_T...")
            For i = 0 To docTypeCount - 1
                doct = dtTemp.Rows(i)(0).ToString
                arrLeftJoins(i) = " left join doc_t" & doct & " t" & i.ToString & " on t" & i.ToString & ".doc_id=wfd.doc_id "
                arrWhereClauses(i) = " t" & i.ToString & ".doc_id is null and "
            Next
            arrWhereClauses(docTypeCount - 1) = arrWhereClauses(docTypeCount - 1).Replace(" and ", String.Empty)
            sb.Append("select wfd.task_id from wfdocument wfd ")
            For i = 0 To docTypeCount - 1
                sb.Append(arrLeftJoins(i))
            Next
            sb.Append(" where ")
            For i = 0 To docTypeCount - 1
                sb.Append(arrWhereClauses(i))
            Next

            dtTemp = EDataTable(sb.ToString)

            If dtTemp.Rows.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron tareas para eliminar. Armando la consulta...")
                sb.Remove(0, sb.Length)
                sb.Append("delete wfdocument where task_id in (")
                For i = 0 To dtTemp.Rows.Count - 1
                    sb.Append(dtTemp.Rows(i)(0).ToString)
                    sb.Append(",")
                Next
                sb.Append("0)")

                ENonQuery(sb.ToString)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay tareas para eliminar.")
            End If

            resultado = True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
            raiseerror(ex)
        End Try

        If resultado Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Proceso ejecutado con éxito")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)
            Return True
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Proceso ejecutado con ERRORES")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)
            Return False
        End If

    End Function
#End Region

End Class
