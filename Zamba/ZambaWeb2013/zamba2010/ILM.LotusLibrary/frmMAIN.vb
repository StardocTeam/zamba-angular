Imports System
Imports Microsoft.Win32

Public Class frmMAIN
    Private Event LogError(ByVal ex As Exception)
    Private NuevoODBC As New ILM.LotusLibrary.Odbc

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub GenerarODBCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerarODBCToolStripMenuItem.Click
        Try        
            '--valores comunes para cualquier origen de datos + el nombre del DSN
            If Me.grupoOracle.Enabled = True Then
                NuevoODBC.ClaveNombre = Me.txtDsn.Text
                NuevoODBC.RutaClavesConexion = "SOFTWARE\ODBC\ODBC.INI\" & Me.txtDsn.Text
            Else
                NuevoODBC.ClaveNombre = Me.txtDsnSQLServer.Text
                NuevoODBC.RutaClavesConexion = "SOFTWARE\ODBC\ODBC.INI\" & Me.txtDsnSQLServer.Text
            End If
            NuevoODBC.RutaClave = "SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources"

            If Me.cboControlador.Text = "Oracle en OraHome92" Then
                AsignarValores()
                NuevoODBC.GenerarODBCOracle()
            Else
                AsignarValores()
                NuevoODBC.GenerarODBCSqlServer()
            End If
            NuevoODBC = Nothing
            MsgBox("Se ha creado exitosamente el ODBC ...", MsgBoxStyle.Information, "Generador ODBC")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    Private Sub AsignarValores()
        Try
            NuevoODBC.ApplicationAttributes = Me.txtApplicationAttributes.Text
            NuevoODBC.Attributes = Me.txtAttributes.Text
            NuevoODBC.BatchAutocommitMode = Me.txtBatchAutocommitMode.Text
            NuevoODBC.CloseCursor = Me.txtCloseCursor.Text
            NuevoODBC.Database = Me.txtDatabase.Text
            NuevoODBC.DescriptionOracle = Me.txtDescription.Text
            NuevoODBC.DisableMTS = Me.txtDisableMTS.Text
            NuevoODBC.Driver = Me.txtDriver.Text
            NuevoODBC.DSN = Me.txtDsn.Text
            NuevoODBC.EXECSchemaOpt = Me.txtEXECSchemaOpt.Text
            NuevoODBC.EXECSyntax = Me.txtEXECSyntax.Text
            NuevoODBC.Failover = Me.txtFailOver.Text
            NuevoODBC.FailoverDelay = Me.txtFailoverDelay.Text
            NuevoODBC.FailoverRetryCount = Me.txtFailoverRetryCount.Text
            NuevoODBC.ForceWCHAR = Me.txtForceWCHAR.Text
            NuevoODBC.Lobs = Me.txtLobs.Text
            NuevoODBC.Longs = Me.txtLongs.Text
            NuevoODBC.MetadataIdDefault = Me.txtMetadataIdDefault.Text
            'NuevoODBC.Password
            NuevoODBC.PrefetchCount = Me.txtPrefetchCount.Text
            NuevoODBC.QueryTimeout = Me.txtQueryTimeout.Text
            NuevoODBC.ResultSets = Me.txtResultSets.Text
            NuevoODBC.ServerName = Me.txtServerName.Text
            NuevoODBC.ServerSQL = Me.txtServer.Text
            NuevoODBC.SQLGetDataExtensions = Me.txtSQLGetDataExtensions.Text
            NuevoODBC.TransationOption = Me.txtTranslationOption.Text
            NuevoODBC.TranslationDLL = Me.txtTranslationDLL.Text
            NuevoODBC.UserID = Me.txtUserId.Text
            NuevoODBC.LastUser = Me.txtLastUser.Text
            NuevoODBC.DescriptionSQLServer = Me.txtDescriptionSQL.Text
            NuevoODBC.DriverSQL = Me.txtDriverSQL.Text
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try

    End Sub

    Private Sub cboControlador_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboControlador.SelectedIndexChanged
        If cboControlador.Text = "Oracle en OraHome92" Then
            Me.grupoOracle.Enabled = True
            Me.grupoSqlServer.Enabled = False
        Else
            Me.grupoOracle.Enabled = False
            Me.grupoSqlServer.Enabled = True
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            '--valores comunes para cualquier origen de datos + el nombre del DSN
            If Me.grupoOracle.Enabled = True Then
                NuevoODBC.ClaveNombre = Me.txtDsn.Text
                NuevoODBC.RutaClavesConexion = "SOFTWARE\ODBC\ODBC.INI\" & Me.txtDsn.Text
            Else
                NuevoODBC.ClaveNombre = Me.txtDsnSQLServer.Text
                NuevoODBC.RutaClavesConexion = "SOFTWARE\ODBC\ODBC.INI\" & Me.txtDsnSQLServer.Text
            End If
            NuevoODBC.RutaClave = "SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources"

            If Me.cboControlador.Text = "Oracle en OraHome92" Then
                AsignarValores()
                NuevoODBC.GenerarODBCOracle()
            Else
                AsignarValores()
                NuevoODBC.GenerarODBCSqlServer()
            End If
            NuevoODBC = Nothing
            MsgBox("Se ha creado exitosamente el ODBC ...", MsgBoxStyle.Information, "Generador ODBC")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class