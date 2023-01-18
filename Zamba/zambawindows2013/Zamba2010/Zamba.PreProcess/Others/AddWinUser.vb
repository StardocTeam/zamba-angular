Imports Zamba.Core
<Ipreprocess.PreProcessName("Agregar Usuario Windows"), Ipreprocess.PreProcessHelp("Coloca el Nombre de usuario de Windows® y de Lotus Notes® al principio de cada linea del archivo maestro, además coloca en la 7º posicion el indice Categoría vacío, este preproceso es de utilidad para actualizar archivos maestros de exportaciones de mails antiguos. Parametros: UsuarioWindows , UsuarioNotes . Ver.: 1.01 5/12/2005.")> _
Public Class ippAddWinUser
    Implements Ipreprocess
    Private Const USUARIOWINDOWS As Int32 = 0
    Private Const USUARIONOTES As Int32 = 1
    Private Const INDICECATEGORIA As Int32 = 7
    Private Const SEPARADOR As String = "|"
    'Private Const MAESTROVIEJO As String = "MaestroViejo.txt"
    Private Const MAESTRONUEVO As String = "MaestroNuevo.dat"
    Private Const ARCHERRORES As String = "ADDWINUSER_ERRORES.txt"
    Private Const DESCRIPCION As String = "Coloca el Nombre de usuario de Windows® y de Lotus Notes® al principio de cada linea del archivo maestro, además coloca en la 7º posicion el indice Categoría vacío, este preproceso es de utilidad para actualizar archivos maestros de exportaciones de mails antiguos. Parametros: UsuarioWindows , UsuarioNotes . Ver.: 1.01 5/12/2005."
    Private Const NOMBREPREPROCESO As String = "AddWinUser"


    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return DESCRIPCION
    End Function
#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml

    End Function
#End Region
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        Dim sParams(), sParam As String
        If Not IsNothing(param) Then
            sParam = param(0)
            sParams = sParam.Split(",")
            If sParams.Length = 2 Then

                For i = 0 To Files.Count - 1
                    processFile(Files(i), sParams(USUARIOWINDOWS) & "," & sParams(USUARIONOTES), xml)
                Next
            Else
                RaiseEvent PreprocessError("ERROR: parametro 'param' en Public Function process no tiene 2 parametros (usuario Windows, usuario Notes)")
            End If
        Else
            RaiseEvent PreprocessError("ERROR: parametro 'param' en Public Function process es NULO")
        End If
        Return Files
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile

        Dim i, iLineasActua, iLineasError As Integer
        Dim Arch As New IO.FileInfo(File)
        Dim sr As New System.IO.StreamReader(Arch.OpenRead)
        Dim sw As New System.IO.StreamWriter(MAESTRONUEVO)
        Dim fArchErr As New System.IO.StreamWriter(ARCHERRORES)
        Dim sLinea, sLineaAux, asParametros(), sUsuarioWinNotes As String
        Try
            'Cargo los parametros
            asParametros = param.Split(",")
            sUsuarioWinNotes = asParametros(USUARIOWINDOWS).Trim & SEPARADOR & asParametros(USUARIONOTES).Trim
        Catch
            Return "ERROR"
        End Try

        Try
            RaiseEvent PreprocessMessage("Preproceso " & NOMBREPREPROCESO & "iniciado")
            sw.AutoFlush = True

            iLineasActua = 0
            iLineasError = 0
            While sr.Peek <> -1
                sLinea = sr.ReadLine
                Dim campos() As String = sLinea.Split("|"c)

                'Agrego a la linea los campos Usuario Windows y Notes
                sLineaAux = sUsuarioWinNotes

                If campos.Length > INDICECATEGORIA Then
                    'Agrego todos los campos hasta ASUNTO inclusive
                    For i = 0 To INDICECATEGORIA - 1
                        sLineaAux = sLineaAux & SEPARADOR & campos(i)
                    Next

                    'Agrego el campo CATEGORIA sin datos
                    sLineaAux = sLineaAux & SEPARADOR

                    'Agrego los campos restantes
                    For i = INDICECATEGORIA To campos.Length - 1
                        sLineaAux = sLineaAux & SEPARADOR & campos(i)
                    Next

                    sw.WriteLine(sLineaAux)
                    iLineasActua += 1
                Else
                    fArchErr.WriteLine(sLinea)
                    iLineasError += 1
                End If

            End While
        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
        End Try

        RaiseEvent PreprocessMessage("Líneas procedas exitosamente: " & iLineasActua.ToString)
        RaiseEvent PreprocessMessage("Líneas con errores, no procesadas: " & iLineasError.ToString)

        Try
            sr.Close()
            sw.Close()
            '  Dim fOrg As New System.IO.FileInfo(Arch.FullName)
            Dim fNuevo As New System.IO.FileInfo(MAESTRONUEVO)
            '  Dim sArchDest, sMaestroOrg As String

            'Comentado para testear a PELETES. harcodie el directorio de peletes
            'FEDE 15/12

            'sArchDest = fOrg.DirectoryName() & "\" & MAESTROVIEJO
            'sMaestroOrg = fOrg.FullName
            ''Muevo el archivo maestro original
            'fOrg.MoveTo(sArchDest)
            ''Reemplazo el maestro original
            'fNuevo.CopyTo(sMaestroOrg)

            'fin comentario FEDE
            IO.File.Delete(Arch.Directory.ToString & "\maestro.txt")
            Dim strDir As String = Arch.Directory.ToString & "\maestro.txt"
            fNuevo.CopyTo(strDir)

        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
            fArchErr.WriteLine(ex.ToString())
        End Try
        fArchErr.Close()
        RaiseEvent PreprocessMessage("Preproceso" & NOMBREPREPROCESO & "terminado")
        Return "SALIDA"
    End Function

End Class