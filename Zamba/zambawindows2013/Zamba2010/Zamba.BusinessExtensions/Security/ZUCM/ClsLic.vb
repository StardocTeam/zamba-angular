Imports Zamba.Tools
Imports Zamba.Data.LicensesFactory

''' <summary>
''' Clase que se encarga del manejo de las Licencias en Zamba
''' </summary>
''' <remarks></remarks>
''' <history> [Marcelo] Modified 25/04/07 
''' Se cambio la manera en q se encripta la licencia, porque la encriptacion era igual a la de la password
''' Ahora se guarda en la Zopt para no modificar la licencia antigua
'''</history>

Public Class ClsLic

    ''' <summary>
    ''' Devuelve el numero de conexiones permitidas para la licencia documental o de workflow
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	09/06/2009	Modified    Adaptación del código para aceptación de licencia de workflow
    '''     [Gaston]	11/06/2009	Modified    Eliminación de la válidación OK
    '''     [Gaston]	19/06/2009	Modified    Inserción del type 3 que indica un administrador que ingresa con licencia de Workflow
    ''' </history>
    Public Shared ReadOnly Property PermitedConections(ByVal type As Int32) As Int32

        Get

            'Key utilizada para el manejo de licencias en esa version
            Dim newkey As Byte() = {0, 2, 3, 6, 5, 4, 7, 8, 9, 1, 1, 2, 3, 6, 5, 4}
            Dim newiv As Byte() = {0, 2, 3, 6, 5, 4, 7, 8, 9, 1, 1, 2, 3, 6, 5, 4}
            Dim X As String = String.Empty
            Dim i As Double = 0

            Dim typeLic As String = Nothing

            ' Si el tipo es uno entonces la licencia es de workflow, de lo contrario es documental
            'If ((type = 1) Or (type = 3)) Then
            If type = 1 Then
                typeLic = "DocWF"
            Else
                typeLic = "DocLic"
            End If

            ' Se obtiene el número de licencias encriptado
            X = ZOptBusiness.GetValue(typeLic)

            ' Si no es nulo o vacío
            If (String.IsNullOrEmpty(X) = False) Then

                Try

                    ' Se desencripta el número de licencias
                    i = Val(Encryption.DecryptString(X, newkey, newiv))

                    ' Si es númerico y es mayor a cero
                    If ((IsNumeric(i)) AndAlso (i > 0)) Then
                        Return (i)
                    Else
                        i = 0
                    End If

                Catch ex As Exception
                    i = 0
                End Try

            Else

                'Si no se usa traer el valor viejo ya sea de licencia de workflow o documental
                If (type = 1) Then
                    X = GetPermitedWFConnection().ToString()
                Else
                    X = GetLicenses()
                End If

                'Key utilizada anteriormente para el manejo de licencias
                Dim keyenc As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
                Dim ivenc As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

                If (String.IsNullOrEmpty(X)) Then
                    i = 0
                Else

                    i = Val(Encryption.DecryptString(X, keyenc, ivenc))

                    ' Si es númerico y es mayor a cero
                    If ((IsNumeric(i)) AndAlso (i > 0)) Then

                    Else
                        i = 0
                    End If

                End If

                'Convertirlo a la nueva version y guardarla en la base
                If Not IsNothing(ZOptBusiness.GetValue(typeLic)) Then
                    ZOptBusiness.Update(typeLic, Encryption.EncryptString(i, newkey, newiv))
                Else
                    ZOptBusiness.Insert(typeLic, Encryption.EncryptString(i, newkey, newiv))
                End If
            End If

            If (i < 0) Then
                Throw New Exception("Número de licencias negativo. Informe a su proveedor del sistema")
            End If

            Return (i)

        End Get

    End Property

    ''' <summary>
    ''' Obtiene la cantidad de licencias de un tipo determinado.
    ''' </summary>
    ''' <param name="licenseType">Tipo de licencia</param>
    ''' <returns>Cantidad de licencias</returns>
    ''' <remarks></remarks>
    Public Function GetLicenseCount(ByVal licType As LicenseType) As String
        Dim lic As String
        'Obtener si la licencia se utiliza de la nueva manera
        lic = ZOptBusiness.GetValue("DocLicUse")

        'Si se utiliza la obtiene, sino obtiene la vieja y la actualiza
        If Not String.IsNullOrEmpty(lic) AndAlso String.Compare(Encryption.DecryptNewString(lic), "OK") = 0 Then
            lic = Encryption.DecryptNewString(ZOptBusiness.GetValue(GetZoptLicValue(licType)))
        Else
            'Utiliza el metodo que obtiene los datos de tabla LIC
            lic = GetOldLicense(licType)
        End If

        Return lic
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la clave de zopt correspondiente a la licencia ingresada
    ''' </summary>
    ''' <param name="licType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetZoptLicValue(ByVal licType As LicenseType) As String
        Select Case licType
            Case LicenseType.Documental
                Return "DocLic"
            Case LicenseType.Workflow
                Return "DocWf"
        End Select
    End Function

    ''' <summary>
    ''' Obtiene las licencias de la tabla LIC
    ''' </summary>
    ''' <param name="licType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOldLicense(ByVal licType As LicenseType) As String
        Select Case licType
            Case LicenseType.Documental
                Return UserBusiness.Rights.VerLicenciasDocumentales
            Case LicenseType.Workflow
                Return UserBusiness.Rights.VerLicenciasWorkFlow
        End Select
    End Function
End Class