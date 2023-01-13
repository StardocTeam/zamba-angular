''' <summary>
''' Clase que se encarga del manejo de las licencias de Zamba
''' </summary>
''' <history> [Marcelo] Modified 25/04/07 
''' Se cambio la manera en q se encripta la licencia, por lo que se agregaron nuevos metodos
''' Ahora se guarda en la Zopt para no modificar la licencia antigua
'''</history>
''' <remarks></remarks>
Public Class LicensesFactory

    ''' <summary>
    ''' Metodo que trae de la Lic los valores de las licencias de documental
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Marcelo] Modified 25/04/07 </history>
    Public Shared Function GetLicenses() As String
        Dim X As String = String.Empty
        If Server.IsOracle Then
            X = Server.Con(True).ExecuteScalar(CommandType.Text, "Select * From Lic where Type=0")
        Else
            X = Server.Con(True).ExecuteScalar("zsp_license_100_GetDocumentalLicenses")
        End If
        Return X
    End Function


    ''' <summary>
    ''' Metodo que trae de la Lic los valores de las licencias de WF
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Marcelo] Modified 25/04/07 </history>
    Public Shared ReadOnly Property GetPermitedWFConnection() As Int32
        Get
            'Claves del encriptado
            Dim keyenc As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim ivenc As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

            Dim sql As String = "Select Numero_Licencias from lic where Type=1"
            Dim cantidad As String = Server.Con.ExecuteScalar(CommandType.Text, sql)
            If String.IsNullOrEmpty(cantidad) Then
                cantidad = 0
            Else
                cantidad = Zamba.Tools.Encryption.DecryptString(cantidad, keyenc, ivenc)
            End If
            Return CInt(cantidad)
        End Get
    End Property

End Class
