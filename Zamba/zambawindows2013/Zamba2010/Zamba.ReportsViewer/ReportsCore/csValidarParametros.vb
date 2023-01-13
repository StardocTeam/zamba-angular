Public Class csValidarParametros
    Public Const iMaxParametros As Int16 = 4
    Public Const iMinParametros As Int16 = 2
    Public Shared Function ValidarParametros() As Int16
        Dim iIndex As Int16
        Dim sRuta As String
        Dim bCorrecto As Boolean = False

        'compruebo si la cant de args es correcta
        If Environment.GetCommandLineArgs().Length = iMaxParametros Then
            'compruebo si el tipo de informe es valido
            If Not (0 < Environment.GetCommandLineArgs(1) < 33) Then
                'No es valido el tipo de informe
                Return 1
            End If
            'compruebo si la ruta es valida
            sRuta = Environment.GetCommandLineArgs(2)
            iIndex = sRuta.LastIndexOf("\") + 1
            If Not System.IO.Directory.Exists(sRuta.Remove(iIndex, sRuta.Length - iIndex)) Then
                'No existe el directorio para dejar el reporte
                Return 2
            End If

            'Verifico si el tipo de formulario es correcto
            Select Case Environment.GetCommandLineArgs(3)
                Case CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
                    bCorrecto = True
                Case CrystalDecisions.[Shared].ExportFormatType.WordForWindows
                    bCorrecto = True
                Case CrystalDecisions.[Shared].ExportFormatType.Excel
                    bCorrecto = True
            End Select
            If Not bCorrecto Then
                'Tipo de Documento incorrecto
                Return 3
            End If
            'CORRECTO
            Return 0
        End If

        If Environment.GetCommandLineArgs().Length = iMinParametros Then
            'compruebo si el tipo de informe es valido
            If Not (0 < Environment.GetCommandLineArgs(1) < 33) Then
                'No es valido el tipo de informe
                Return 1
            End If
            'CORRECTO
            Return 0
        End If
        'No se respeto la cantidad de parametros
        Return 10
    End Function

End Class
