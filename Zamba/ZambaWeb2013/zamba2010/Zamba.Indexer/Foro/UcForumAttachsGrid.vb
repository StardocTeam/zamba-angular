﻿Imports Zamba.Core
Imports System.Collections.Generic

''' <summary>
''' UserControl que contiene un DataGridView y métodos para 
''' manejar los adjuntos de los mensajes del foro.
''' </summary>
''' <history>
'''   Tomas     04/05/2010  Created
'''</history>
Public Class UcForumAttachsGrid

    ''' <summary>
    ''' Carga la grilla con los adjuntos.
    ''' </summary>
    Public Sub FillAttachs(ByVal idMensaje As Int32)
        ClearGrid()

        'Se cargan los adjuntos
        Me.dgvAttachs.DataSource = ZForoBusiness.GetAttachs(idMensaje)

        'Se configuran las columnas
        Me.dgvAttachs.Columns("Adjuntos").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Me.dgvAttachs.Columns("Adjuntos").ToolTipText = "Doble click sobre un adjunto para visualizarlo"
        Me.dgvAttachs.Columns("PATH").Visible = False

        'Carga el número de adjuntos en el nombre de la solapa.
        If Not Me.Parent Is Nothing Then
            DirectCast(Me.Parent, TabPage).Text = "Adjuntos (" + Me.dgvAttachs.RowCount.ToString + ")"
        End If

    End Sub

    ''' <summary>
    ''' Inserta los adjuntos en el servidor y luego refresca la grilla.
    ''' </summary>
    Public Sub CreateAttachs(ByVal idMensaje As Int32, ByVal attachs As List(Of String))
        'Se obtiene la ruta del servidor.
        Dim serverPath As String = ZOptBusiness.GetValue("ServAdjuntosRuta")
        Dim serverAttachs As New List(Of String)

        'Se verifica que se haya configurado la ruta.
        If Not String.IsNullOrEmpty(serverPath) Then
            serverPath += "\" + idMensaje.ToString()

            'Se verifica la existencia de la ruta.
            If IO.Directory.Exists(serverPath.Remove(serverPath.LastIndexOf("\"))) Then
                Try
                    serverAttachs = CopyFiles(attachs, serverPath)
                    ZForoBusiness.InsertAttach(idMensaje, serverAttachs)
                    FillAttachs(idMensaje)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Ha ocurrido un error al insertar los archivos adjuntos." + vbCrLf + _
                                "Consulte con el Departamento de Sistemas.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            Else
                MessageBox.Show("La ruta de servidor de archivos adjuntos de foro es inválida." + vbCrLf + _
                                "Consulte con el Departamento de Sistemas.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No se ha configurado la ruta de servidor de archivos adjuntos de foro." + vbCrLf + _
                           "Consulte con el Departamento de Sistemas.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    ''' <summary>
    ''' Elimina los adjuntos de la base y los archivos físicos (si es que esta configurado).
    ''' </summary>
    Public Sub DeleteAttachs(ByVal idMensaje As Int32, ByVal isSelectedMessage As Boolean)

        If isSelectedMessage Then
            '
            'Si el mensaje es el seleccionado se obtienen los datos de la grilla y luego se limpia.
            '
            If Me.dgvAttachs.RowCount > 0 Then
                Try
                    'Se eliminan los adjuntos de la base.
                    ZForoBusiness.DeleteAttachs(idMensaje)

                    Dim deleteAttachs As String = ZOptBusiness.GetValue("DeleteForumAttachments")
                    If Not String.IsNullOrEmpty(deleteAttachs) AndAlso Boolean.Parse(deleteAttachs) Then
                        'Se eliminan todos los archivos y la carpeta del servidor.
                        IO.Directory.Delete(ZOptBusiness.GetValue("ServAdjuntosRuta") + "\" + idMensaje.ToString(), True)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Error al intentar eliminar los archivos adjuntos." + vbCrLf + _
                                    "Consulte los archivos de excepciones para obtener mayor información.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    'Se limpia la grilla.
                    ClearGrid()
                End Try
            End If
        Else
            '
            'Si el mensaje no es el seleccionado (respuestas) se eliminan los adjuntos del servidor
            '
            If ZForoBusiness.DeleteAttachs(idMensaje) > 0 Then
                Dim deleteAttachs As String = ZOptBusiness.GetValue("DeleteForumAttachments")
                If Not String.IsNullOrEmpty(deleteAttachs) AndAlso Boolean.Parse(deleteAttachs) Then
                    'Se obtiene la ruta de la carpeta con los adjuntos a eliminar.
                    Dim serverPath As String = ZOptBusiness.GetValue("ServAdjuntosRuta") + "\" + idMensaje.ToString()
                    IO.Directory.Delete(serverPath, True)
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Abre el adjunto seleccionado por fuera de Zamba, realizando de antemano una copia local.
    ''' </summary>
    Private Sub dgvAttachs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAttachs.CellDoubleClick
        If Me.dgvAttachs.RowCount > 0 AndAlso e.RowIndex > -1 Then
            'Se obtiene el path completo del adjunto (servidor).
            Dim path As String = Me.dgvAttachs.Rows(e.RowIndex).Cells("PATH").Value.ToString()
            'Se obtiene la ruta temporal.
            Dim temp As String = GetTempPath(path)

            'Se realiza una copia local del adjunto.
            If CopyFile(path, temp) Then
                OpenFile(temp)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia el contenido de la grilla.
    ''' </summary>
    Private Sub ClearGrid()
        Me.dgvAttachs.DataSource = Nothing
        If Me.dgvAttachs.RowCount > 0 Then
            Me.dgvAttachs.Rows.Clear()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la ruta temporal del documento adjunto desde donde se visualizará.
    ''' </summary>
    Private Function GetTempPath(ByVal path As String) As String
        Dim dir As IO.DirectoryInfo
        Dim tempPath As String

        Try
            'Verifica la existencia del directorio temporal en application data.
            dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
            If Not dir.Exists Then
                dir.Create()
            End If

            'Se obtiene la ruta temporal del archivo adjunto.
            tempPath = FileBusiness.GetUniqueFileName(dir.FullName, IO.Path.GetFileName(path))
        Catch
            Try
                'En caso de error se intenta copiar el temporal en el compilado.
                dir = New IO.DirectoryInfo(Membership.MembershipHelper.StartUpPath & path)
                If Not dir.Exists Then
                    dir.Create()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("Error al intentar copiar el archivo adjunto." + vbCrLf + _
                                "Consulte los archivos de excepciones para obtener mayor información.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Finally
            dir = Nothing
        End Try

        Return tempPath
    End Function

    ''' <summary>
    ''' Copia un archivo de origen a destino.
    ''' </summary>
    Private Function CopyFile(ByVal sourceFileName As String, ByVal destFileName As String) As Boolean
        Try
            'Obtiene una ruta única.
            destFileName = FileBusiness.GetUniqueFileName(destFileName)
            'Se realiza una copia local del adjunto.
            IO.File.Copy(sourceFileName, destFileName)
            Return True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("La copia del archivo adjunto no pudo ser realizada." + vbCrLf + _
                            "Consulte los archivos de excepciones para obtener mayor información.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Copia un conjunto de archivos a destino.
    ''' </summary>
    Private Function CopyFiles(ByVal attachs As List(Of String), ByVal destDirectory As String) As List(Of String)
        Dim serverAttachs As New List(Of String)
        Dim serverAttach As String

        'Verifica la existencia del directorio de destino.
        If Not IO.Directory.Exists(destDirectory) Then
            IO.Directory.CreateDirectory(destDirectory)
        End If

        'Se realiza la copia de archivos.
        For Each path As String In attachs
            serverAttach = FileBusiness.GetUniqueFileName(destDirectory, IO.Path.GetFileName(path))
            IO.File.Copy(path, serverAttach)
            serverAttachs.Add(serverAttach)
        Next

        Return serverAttachs
    End Function

    ''' <summary>
    ''' Abre un archivo específico.
    ''' </summary>
    Private Sub OpenFile(ByVal path As String)
        Try
            'Se abre el archivo adjunto.
            System.Diagnostics.Process.Start(path)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("El archivo adjunto no pudo ser abierto." + vbCrLf + _
                            "Consulte los archivos de excepciones para obtener mayor información.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class