Imports System.io
Imports zamba.data
''' <summary>
''' Clase que contiene la logica para el postproceso de ordenes de pago
''' </summary>
''' <remarks></remarks>
<Ipreprocess.PreProcessName("Inserta Datos De Ordenes de Pago (ODP)"), Ipreprocess.PreProcessHelp("Inserta en tabla Ordenes_Pago segun valores de atributos De tipos de documento ODP Ordenes de Pago")> _
Public Class ippSetODP_Zamba
    Implements Ipreprocess

    ''' <summary>
    ''' Obtiene la ayuda para el postproceso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Inserta en tabla DOC_Ixxx correspondiente los datos obtenidos de la tabla Ordenes_Pago segun numeros de vouchers"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return xml
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    ''' <summary>
    ''' parsea y procesa una coleccion de archivos que contienen los vouchers a procesar por el postproceso
    ''' </summary>
    ''' <param name="Files"></param>
    ''' <param name="param"></param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "EJECUTANDO EL POSTPROCESO DE ORDENES DE PAGO")
            For Each file As String In Files
                ZTrace.WriteLineIf(ZTrace.IsInfo, file)
                Dim Indexs As Dictionary(Of String, String) = CreateIndexs()
                Dim Dtid As Int64 = PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_DOCTYPE_ID)
                Dim strReader As New StreamReader(file)
                If Not IsNothing(strReader) Then
                    Dim line As String = strReader.ReadLine
                    ZTrace.WriteLineIf(ZTrace.IsInfo, file)
                    While line <> ""
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nro Voucher: " & line.Split("|")(2))
                        Try
                            PostProcess_Factory.SetODP_ZambaProcess(Int32.Parse(line.Split("|")(2)), Dtid, Indexs)
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Orden de Pago Ejecutada")
                        line = strReader.ReadLine
                    End While
                End If
                strReader.Close()
                strReader.Dispose()
                strReader = Nothing
            Next
            Return Files
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Files
        End Try
    End Function

    ''' <summary>
    ''' parsea y procesa un unico archivo que contiene los vouchers a procesar por el postproceso
    ''' </summary>
    ''' <param name="File"></param>
    ''' <param name="param"></param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "EJECUTANDO EL POSTPROCESO DE ORDENES DE PAGO")
            Dim strReader As New StreamReader(File)
            Dim Indexs As Dictionary(Of String, String) = CreateIndexs()
            Dim Dtid As Int64 = PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_DOCTYPE_ID)

            If Not IsNothing(strReader) Then
                Dim line As String = strReader.ReadLine
                ZTrace.WriteLineIf(ZTrace.IsInfo, File)
                While line <> ""
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nro Voucher: " & line.Split("|")(2))
                    Try
                        PostProcess_Factory.SetODP_ZambaProcess(Int32.Parse(line.Split("|")(2)), Dtid, Indexs)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Orden de Pago Ejecutada")
                    line = strReader.ReadLine
                End While
            End If
            Return File
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return File
        End Try
    End Function

    Public Function CreateIndexs() As Dictionary(Of String, String)
        Dim col As New Dictionary(Of String, String)
        col.Add("VOUCHER_NUM", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_VOUCHER_NUM))
        col.Add("ENTITY", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_ENTITY))
        col.Add("BANK_ACCOUNT", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_BANK_ACCOUNT))
        col.Add("DOCUMENT_NUM", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_DOCUMENT_NUM))
        col.Add("PAYMENT_DATE", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_PAYMENT_DATE))
        col.Add("PAYMENT_AMOUNT", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_PAYMENT_AMOUNT))
        col.Add("CURR", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_CURR))
        col.Add("STATUS", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_STATUS))
        col.Add("STATUS_DATE", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_STATUS_DATE))
        col.Add("SUPPLIER", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_SUPPLIER))
        col.Add("RATE_TYPE", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_RATE_TYPE))
        col.Add("RATE_DATE", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_RATE_DATE))
        col.Add("PAYMENT_RATE", PostProcessBusiness.GetPreference(PostProcessPreferences.ODP_PAYMENT_RATE))
        Return col
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

    ''' <summary>
    ''' Propiedad utilizada para obtener el id del postproceso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    ''' <summary>
    ''' Propiedad Utilizada para obtener el nombre del postproceso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Name() As String
        Get
            Return "Inserta Datos De Ordenes de Pago (ODP)"
        End Get
    End Property
End Class
