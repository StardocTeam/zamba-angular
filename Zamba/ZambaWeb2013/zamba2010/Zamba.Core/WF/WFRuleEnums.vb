
'Se agrego a Icore 13/06/07
'Public Enum AddRuleTypes
'    AgregarSubRegla
'    AgregarRegla
'    InsertRegla
'End Enum

'Public Enum ListofRules
'    'TODO:WF:Enumerador que contiene todos los condicionales disponibles en WorkFlow
'    IfDates = 1
'    IfIndex = 2
'    IfDocumentType = 3
'    IfUser = 4
'    IfCantidadDocumentos = 5
'    IfExpireDate = 6
'    IfFileExist = 7
'    IfDocAsocExist = 8
'    IfTaskState = 9
'    DoDistribuir = 10
'    DOMail = 11
'    DoDelete = 12
'    DoChangeState = 13
'    DOSCREENMESSAGE = 13
'    DoAsign = 14
'    DoChangeExpireDate = 16
'    DoRequestData = 17
'    DoFillIndex = 18
'    DoAutoComplete = 19
'    DoGetDocAsoc = 20
'    DoAutoMail = 21
'    DoCreateForm = 22
'End Enum


'WFRULES
'ID --> id
'NAME --> name
'STEP_ID 
'TYPE --> 1=accion; 2=condición
'PARENTID --> 0= cuando no depende de otra regla ; >0 ID de la regla padre
'PARENTTYPE --> 5= acción usuario; 6=entrada; 7=salida;8=actualización; 9=planificada; 10=regla de condición; 11=regla de acción
'PARENTPARAM -->ID del parametro del que depende de la regla padre