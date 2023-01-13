''' -----------------------------------------------------------------------------
''' <summary>
''' Macro para exportar un Mail de MS Outlook a Zamba
''' </summary>
''' <remarks>
''' El codigo debe copiarse dentro de lotus como Macro.
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Module MacroExport
    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''Macro para enviar un mail a Zamba''''''''''
    ''''''''''''''''Lenguaje: VBA'''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    'Public Sub Import()
    '    Dim nombre As String
    '    nombre = Day(Time) & Month(Time) & Year(Date) & Hour(Time) & Minute(Time) & Second(Time)
    '    Dim myMail As Outlook.MailItem
    '    Dim myolapp As Outlook.Application
    '    Dim myinspector As Outlook.Inspector
    '    myolapp = CreateObject("Outlook.Application")
    '    myinspector = myolapp.ActiveInspector

    '    'Test if an inspector is active
    '    If Not TypeName(myinspector) = "Nothing" Then
    '        myinspector.CommandBars.DisplayKeysInTooltips = True
    '        myMail = myinspector.CurrentItem
    '        myMail.SaveAs("C:\temp\" & nombre & ".msg")
    '        Shell("C:\ZambaSoftware\GroupClient\Cliente\bin\cliente.exe insm " & "C:\temp\" & nombre & ".msg")
    '    End If
    'End Sub

End Module
