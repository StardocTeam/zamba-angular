Imports System.IO
Imports Zamba.Core
Imports System.Drawing


Public Class PDFTools

#Region "Exportar a PDF"


    Public Shared Function exportarResultPDF(ByRef Result As Result, ByVal sPdf As String) As Boolean
        Try
            If Result.IsImage Then

                'TODO: Validar la ruta del archivo, si las carpetas no existen crearlas
                Dim fi As New IO.FileInfo(sPdf)
                If fi.Directory.Exists = False Then fi.Directory.Create()


                Dim sRuta As String
                Dim Doc As New ceTe.DynamicPDF.Document
                sRuta = Result.FullPath()
                'Si la imagen del result es un Tif requiere un tratamiento distinto a otras
                If sRuta.ToUpper.EndsWith(".TIF") OrElse sRuta.ToUpper.EndsWith(".TIFF") Then
                    Dim fTif As New ceTe.DynamicPDF.Imaging.TiffFile(sRuta)
                    Dim i, k As Int32
                    Dim Img As ceTe.DynamicPDF.PageElements.Image
                    k = fTif.Images.Count

                    'TODO HERNAN FIJATE Q ES ESTO
                    'If MessageBox.Show("La imagen contiene " & k & " paginas. ¿Desea exportarlas todas?", "Zamba - Exportación a PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = System.Windows.Forms.DialogResult.No Then


                    'End If
                    For i = 0 To k - 1
                        Img = New ceTe.DynamicPDF.PageElements.Image(fTif.Images(i), 0, 0)
                        Img.SetDpi(72)
                        Dim Pag As New ceTe.DynamicPDF.Page
                        Dim Pd As New ceTe.DynamicPDF.PageDimensions(Img.Width, Img.Height)
                        Pag.Elements.Add(Img)
                        Pag.Dimensions = Pd
                        Pag.Dimensions.SetMargins(0)
                        Doc.Pages.Add(Pag)

                        'Doc.Pages.Add(pag)
                    Next
                Else
                    Dim Img As New ceTe.DynamicPDF.PageElements.Image(sRuta, 0, 0)
                    Img.SetDpi(72)
                    Dim Pag As New ceTe.DynamicPDF.Page
                    Dim Pd As New ceTe.DynamicPDF.PageDimensions(Img.Width, Img.Height)
                    Pag.Elements.Add(Img)
                    Pag.Dimensions = Pd
                    Pag.Dimensions.SetMargins(0)
                    Doc.Pages.Add(Pag)
                End If

                Try
                    Doc.Draw(sPdf)
                    Return True
                Catch
                    Return False
                End Try
            Else
                Return False
            End If
        Catch
            Return False
        End Try

    End Function

    ' ''' <summary>
    ' ''' Converts images to PDF Files
    ' ''' </summary>
    ' ''' <param name="Result"></param>
    ' ''' <param name="PrinterName"></param>
    ' ''' <param name="ScriptExportPath"></param>
    ' ''' <param name="CantPdfs"></param>
    ' ''' <remarks></remarks>
    ' ''' <history>
    ' ''' [AlejandroR] - 10/02/2010 - Created
    ' ''' </history>
    'Public Shared Function ConvertDocToPdfFile(ByRef Result As Result, ByVal PrinterName As String, ByVal CurrentUserID As Int64)

    '    Dim OfficeTemp As String
    '    Dim OfficeTempPDF As String
    '    Dim LocalDocFile As String
    '    Dim LocalDocFileToPrint As String
    '    Dim Resul As Boolean

    '    'hace una copia del archivo copiado en officetemp (por que puede estar tomado)
    '    OfficeTemp = Membership.MembershipHelper.AppTempDir("\OfficeTemp").FullName
    '    OfficeTempPDF = OfficeTemp & "\PDFTemp"

    '    LocalDocFile = OfficeTemp & "\" & Result.Doc_File
    '    LocalDocFileToPrint = OfficeTemp & "\PDFTemp\§" & Result.Doc_File

    '    Try

    '        If Not Directory.Exists(OfficeTempPDF) Then
    '            Directory.CreateDirectory(OfficeTempPDF)
    '        End If

    '        If Not File.Exists(LocalDocFile) Then
    '            File.Copy(Result.FullPath, LocalDocFile)
    '        End If

    '        File.Copy(LocalDocFile, LocalDocFileToPrint, True)

    '        Dim word As New WordInterop

    '        Resul = word.Print(LocalDocFileToPrint, PrinterName)

    '        Application.DoEvents()

    '        If Resul Then

    '            'guardar datos en la tabla de exportacion
    '            ExportFactory.InsertExportedPDF(CurrentUserID, Result.ID, Result.DocTypeId, Result.Doc_File)

    '        End If

    '        word = Nothing

    '        If File.Exists(LocalDocFileToPrint) Then
    '            File.Delete(LocalDocFileToPrint)
    '        End If

    '    Catch ex As Exception

    '        ZClass.raiseerror(ex)

    '    End Try

    '    Return Resul

    'End Function

    ''' <summary>
    ''' Converts images to PDF Files
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="pdfFolderPath"></param>
    ''' <param name="CantPdfs"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas] - 27/04/2009 - Modified - Se mueve un bloque de código dentro de un catch que generaba error
    ''' [Tomas] - 19/05/2009 - Modified - Se implementa el método GetUniqueFileName al crear los pdfs.
    ''' </history>
    Public Shared Sub ConvertToPdfFile(ByRef Result As Result, ByVal pdfFolderPath As String, ByRef CantPdfs As Int32)
        If Not Result.IsImage And IsNothing(Result.Picture) Then
            Exit Sub
        End If

        Dim p As ceTe.DynamicPDF.Page
        '   Dim bm As System.Drawing.Bitmap
        Dim pe As ceTe.DynamicPDF.PageElements.Image
        Dim ps As ceTe.DynamicPDF.PageDimensions
        '     Dim i As Int32
        '    Dim iTotal As Int32
        Dim k As Int32
        Dim fTiff As ceTe.DynamicPDF.Imaging.TiffFile = Nothing
        '   Dim imgTiff As TiffImageData
        Dim FlagToJPG As Boolean = False
        Dim ArrayDeTemps As New ArrayList
        Dim aux As New ArrayList
        Dim Img As Image = Nothing

        Dim PathFile As String = Result.FullPath

        'TODO: Buscar codigo de PDFCOnvert de Repsol con ultima version.
        Try

            If PathFile.ToUpper.EndsWith("TIF") Then

                Img = Drawing.Image.FromFile(PathFile)

                FlagToJPG = True
                Dim oFDimension As System.Drawing.Imaging.FrameDimension
                Dim iCount As Int32 = 0
                Dim actualFrame As Int32 = 0
                oFDimension = New System.Drawing.Imaging.FrameDimension(Img.FrameDimensionsList(actualFrame))
                iCount = Img.GetFrameCount(oFDimension)

                If iCount = 1 Then

                    'SI NO ES MULTITIFF

                    Dim NumMagicoW As Decimal
                    Dim NumMagicoH As Decimal

                    Dim ScaleXAnt As Int32
                    Dim ScaleYAnt As Int32

                    p = New ceTe.DynamicPDF.Page
                    pe = New ceTe.DynamicPDF.PageElements.Image(CStr(PathFile), 0.0, 0.0, 100)

                    ScaleXAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX
                    ScaleYAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY

                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = 1
                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = 1

                    Dim Img2 As Drawing.Image
                    Img2 = Drawing.Image.FromFile(PathFile)

                    NumMagicoW = Img2.HorizontalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).HorizontalDpi
                    NumMagicoH = Img2.VerticalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).VerticalDpi

                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = ScaleXAnt
                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = ScaleYAnt

                    pe.Width = Single.Parse(Img2.Width / NumMagicoW)
                    pe.Height = Single.Parse(Img2.Height / NumMagicoH)
                    p.Elements.Add(pe)
                    p.Dimensions.Width = Img2.Width / NumMagicoW
                    p.Dimensions.Height = Img2.Height / NumMagicoH
                    p.Dimensions.SetMargins(0, 0, 0, 0)

                    aux.Add(p)

                Else

                    'SI ES MULTITIFF LO HACE PARA CADA PAGINA

                    For j As Int32 = 0 To iCount - 1

                        oFDimension = New System.Drawing.Imaging.FrameDimension(Img.FrameDimensionsList(actualFrame))
                        Img.SelectActiveFrame(oFDimension, j)
                        Dim Path As String = New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name.ToUpper.Replace(".TIF", "") & j & "_TEMPORAL.TIF"
                        Img.Save(Path)
                        ArrayDeTemps.Add(Path)

                        Dim NumMagicoW As Decimal
                        Dim NumMagicoH As Decimal

                        Dim ScaleXAnt As Int32
                        Dim ScaleYAnt As Int32

                        p = New ceTe.DynamicPDF.Page
                        pe = New ceTe.DynamicPDF.PageElements.Image(CStr(Path), 0.0, 0.0, 100)

                        ScaleXAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX
                        ScaleYAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY

                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = 1
                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = 1

                        Dim Img2 As Drawing.Image
                        Img2 = Drawing.Image.FromFile(Path)

                        NumMagicoW = Img2.HorizontalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).HorizontalDpi
                        NumMagicoH = Img2.VerticalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).VerticalDpi

                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = ScaleXAnt
                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = ScaleYAnt

                        pe.Width = Single.Parse(Img2.Width / NumMagicoW)
                        pe.Height = Single.Parse(Img2.Height / NumMagicoH)
                        p.Elements.Add(pe)
                        p.Dimensions.Width = Img2.Width / NumMagicoW
                        p.Dimensions.Height = Img2.Height / NumMagicoH

                        p.Dimensions.SetMargins(0, 0, 0, 0)

                        aux.Add(p)

                    Next

                End If

            Else

                Try
                    p = New ceTe.DynamicPDF.Page
                    pe = New ceTe.DynamicPDF.PageElements.Image(CStr(PathFile), 0.0, 0.0, 100)
                    Dim Img2 As Drawing.Image
                    Img2 = Drawing.Image.FromFile(PathFile)
                    pe.Width = Single.Parse((Img2.Width / Img2.HorizontalResolution) * 72)
                    pe.Height = Single.Parse((Img2.Height / Img2.VerticalResolution) * 72)
                    p.Elements.Add(pe)
                    p.Dimensions.Width = (Img2.Width / Img2.HorizontalResolution) * 72
                    p.Dimensions.Height = (Img2.Height / Img2.VerticalResolution) * 72
                    p.Dimensions.SetMargins(0, 0, 0, 0)
                    aux.Add(p)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

            End If

        Catch ex As Exception

            Try

                'TIRO ERROR O FORCED = TRUE
                Img = Drawing.Image.FromFile(PathFile)
                Dim Path As String = New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"
                ArrayDeTemps.Add(Path)
                If Img.HorizontalResolution = 0 OrElse Img.VerticalResolution = 0 Then
                End If
                Img.Save(Path, Drawing.Imaging.ImageFormat.Tiff)
                fTiff = New ceTe.DynamicPDF.Imaging.TiffFile(CStr(New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"))

            Catch ex2 As Exception

                Try
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    If Img Is Nothing = False Then Img.Dispose()
                    Img = Nothing
                    Img = Drawing.Bitmap.FromFile(PathFile)
                    Dim Path As String = New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"
                    Try
                        If System.IO.File.Exists(Path) Then System.IO.File.Delete(Path)
                    Catch
                    End Try
                    ArrayDeTemps.Add(Path)
                    Img.Save(Path, Drawing.Imaging.ImageFormat.Tiff)
                    fTiff = New ceTe.DynamicPDF.Imaging.TiffFile(CStr(New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"))
                Catch exc As Exception
                    ZClass.raiseerror(exc)
                End Try

            Finally
                If FlagToJPG = False Then
                    If Not IsNothing(fTiff.Images) Then
                        For k = 0 To fTiff.Images.Count() - 1
                            Try
                                p = fTiff.Images(k).GetPage
                                pe = New ceTe.DynamicPDF.PageElements.Image(fTiff.Images(k), 0.0, 0.0)
                                ps = New ceTe.DynamicPDF.PageDimensions(pe.Width, pe.Height)
                                ps.SetMargins(0, 0, 0, 0)
                                p.Dimensions = ps
                                aux.Add(p)
                            Catch ex3 As Exception
                                ZClass.raiseerror(ex3)
                            End Try
                        Next
                    End If
                End If
                Img.Dispose()
                pe = Nothing
                ps = Nothing

            End Try
        End Try

        '[Tomas]    27/04/2009  Modified    Se comenta el siguiente código y se lo pone en el finally
        '                                   que se encuentra arriba, ya que la instanciación del objeto
        '                                   fTiff se realizaba unicamente en el catch y si el código no
        '                                   generaba exception no se instanciaba y luego si se generaba 
        '                                   exception al pasar por el código comentado.
        'If FlagToJPG = False Then
        '    If Not IsNothing(fTiff.Images) Then
        '        For k = 0 To fTiff.Images.Count() - 1
        '            Try
        '                p = fTiff.Images(k).GetPage
        '                pe = New ceTe.DynamicPDF.PageElements.Image(fTiff.Images(k), 0.0, 0.0)
        '                ps = New ceTe.DynamicPDF.PageDimensions(pe.Width, pe.Height)
        '                ps.SetMargins(0, 0, 0, 0)
        '                p.Dimensions = ps
        '                aux.Add(p)
        '            Catch ex As Exception
        '                ZClass.raiseerror(ex)
        '            End Try
        '        Next
        '    End If
        'End If
        'Img.Dispose()
        'pe = Nothing
        'ps = Nothing

        Try
            If aux.Count > 0 Then
                Dim Documento As New ceTe.DynamicPDF.Document
                For Each Pag As ceTe.DynamicPDF.Page In aux
                    If Not IsNothing(Pag) Then
                        Documento.Pages.Add(Pag)
                    End If
                Next

                Documento.Draw(FileBusiness.GetUniqueFileName(pdfFolderPath & "\", GetValidFileName(Result.Name), ".pdf"))
                CantPdfs += 1
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Shared Function GetValidFileName(ByVal fileName As String) As String
        For Each invalidChar As Char In Path.InvalidPathChars
            If fileName.Contains(invalidChar) Then
                fileName = fileName.Replace(invalidChar, "")
            End If
        Next
        Return fileName.Replace(" ", "_").Replace(".", "").Replace(":", "").Replace("/", "").Replace("__", "_")
    End Function
#End Region

End Class
