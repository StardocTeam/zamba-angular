Imports System.Windows.Forms

Public Interface IOutlookInterop
    Event CloseMailItemEvent()
    Property ActiveInspectorCaption() As String
    Property closeFromControlbox() As Boolean
    Property DisposingParent() As Boolean
    Function OpenMailItem(ByVal mailItemPath As String, ByVal modal As Boolean, Optional ByVal winState As FormWindowState = FormWindowState.Maximized, Optional ByVal UpdateCaption As Boolean = False) As Boolean
    Function CloseMailItem() As Boolean
    Function GetNewMailItem(ByVal tempPath As String, Optional ByVal modal As Boolean = True, Optional ByVal waitForSend As Boolean = True, Optional ByRef Params As Hashtable = Nothing, Optional ByVal automaticSend As Boolean = False) As Boolean

    ''' <summary>
    ''' Agrega el encabezado de una respuesta a un mail existente.
    ''' </summary>
    ''' <param name="mail">MailItem al que se le agregará el encabezado de respuesta</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas   15/06/2010  Created
    ''' <history>
    Sub AddReplyHeader(ByVal mail As Object)

    Sub NewAppointment(ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ShowForm As Boolean)
    Function NewCalendar(ByVal Organizer As String, ByVal toMails As String, ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AllDayEvent As Boolean) As String
End Interface


Public Interface ISharedOutlook
    ''' <summary>
    ''' Devuelve una instancia de la clase Zamba.OS.OfficeSelector.OutlookInterop()
    '''  utilizando una única instancia de Outlook. 
    ''' </summary>
    ''' <returns>Un objeto de la clase OutlookInterop utilizando una única instancia de OUTLOOK.EXE</returns>
    Function GetOutlook() As IOutlookInterop

    ''' <summary>
    ''' Obtiene la ruta de Outlook buscando en el registro.
    ''' </summary>
    ''' <returns>Devuelve la ruta de outlook.</returns>
    Function GetOutlookPath() As String
End Interface

