﻿Imports System.Drawing

Public Class ZambaUIHelpers

    ''' <summary>
    ''' BackColor para ToolBar y Botones Sueltos
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetToolbarsAndButtonsColor() As Color
        Return Color.FromArgb(0, 157, 224)
    End Function

    ''' <summary>
    ''' Blanco para las letras de botones y toolbars
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetToolbarsAndButtonsFontColor() As Color
        Return Color.White
    End Function

    ''' <summary>
    ''' Fuente para nodos de workflows
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetTreeViewWfNodesFont() As Font
        Return New Font("Verdana", 9.75!, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Function

    ''' <summary>
    ''' Color de fuente para nodos de workflows
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetTreeViewStepNodesFontColor() As Color
        Return Color.FromArgb(70, 70, 70)
    End Function

    ''' <summary>
    ''' Fuente para nodos de Etapa
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetTreeViewStepNodesFont() As Font
        Return New Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Function

    ''' <summary>
    ''' Color de fuente para nodos de Etapa
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetTreeViewWfNodesFontColor() As Color
        Return Color.FromArgb(76, 76, 76)
    End Function

    ''' <summary>
    ''' Azul para Titulos o Letras que tienen que estar resaltadas encima de un Blanco
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetTitlesColor() As Color
        Return Color.FromArgb(0, 157, 224)
    End Function

    ''' <summary>
    ''' Gris Oscuro para Letras (ForeColor)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetFontsColor() As Color
        Return Color.FromArgb(76, 76, 76)
    End Function

    ''' <summary>
    ''' Gris Claro para Fondo de Panel con Controles en Blanco
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetPanelBackGroundsColor() As Color
        Return Color.FromArgb(236, 236, 236)
    End Function

    Public Shared Function GetDesignBarColor() As Color
        Return Color.White
    End Function

    ''' <summary>
    ''' Tipo de Letra
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetFontFamily() As Font
        Return New System.Drawing.Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
    End Function

    

End Class

