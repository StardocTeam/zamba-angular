
Imports System.Text
Imports Zamba.Servers
''' <summary>
''' Pregunta por un indice y guardar el valor ingresado
''' </summary>
''' <history>Marcelo Modified 02/12/08</history>
''' <remarks></remarks>
Public Class PlayDOApproveMatrix
    'Private m_Name As String

    Private _myRule As IDOApproveMatrix
    Private lstResults As System.Collections.Generic.List(Of Core.ITaskResult)
    Private Valor As String
    Private lstIndexsIds As List(Of Int64)
    Private MatrixEntity As IDocType

    Dim UserBusiness As New UserBusiness
    Dim UserGroupBusiness As New UserGroupBusiness

    Sub New(ByVal rule As IDOApproveMatrix)
        _myRule = rule
    End Sub

    'select a.i10253 Aprobador, a.i153 Secuencia, b.name NombreGrupo , i10253  from doc_i1020009 a, usrgroup b where i10248 <= 100000  and  i153 > ( select NVL(MAX(nvl(i153,0)),0) from doc_i107  where i86 = <<Tarea>>.<<Atributo(Id Tramite)>>  and i1020136 in (1,5)  )  and i10253 = b.id  and i153 > (select nvl(max(m.nivel),0) from doc_i107 x, usr_r_group y, usrgroup z, ( select a.i153 nivel, b.name gruposaprobadores from doc_i1020009 a, usrgroup b where a.i10253 = b.id ) m  where x.i86 = <<Tarea>>.<<Atributo(Id Tramite)>> and x.i1020139 = y.usrid  and y.groupid = z.id  and substr(z.name,1,6)||'*'||substr(z.name,instr(z.name,'_',7)) = m.gruposaprobadores  )  and rownum = 1 order by i153 

    'select ID from usrgroup g where g.name in ( select 'Zamba_' || SUBSTR(g.name, INSTR (g.name,'_', 1, 1)+1,INSTR(g.name,'_',1,2)-INSTR(g.name,'_', 1, 1)-1) || REPLACE('zvar(NOMBREGRUPO)','Zamba_*','')  from usrgroup g  inner join usr_r_group r  on r.groupid = g.id inner join usrtable u on u.id = r.usrid inner join doc_i18 s on u.id = s.i53 where g.name like  '%_%_%' and g.descripcion = 'AREA' and S.i16 = <<Tarea>>.<<Atributo(Nro de Solicitud Interna)>>)



    Public Function Play(results As List(Of ITaskResult)) As List(Of ITaskResult)

        lstResults = New System.Collections.Generic.List(Of Core.ITaskResult)

        lstIndexsIds = New List(Of Int64)(1)

        MatrixEntity = (New DocTypesBusiness).GetDocType(_myRule.MatrixEntityId)
        MatrixEntity.Indexs = (New IndexsBusiness).GetIndexsSchemaAsListOfDT(_myRule.MatrixEntityId)


        If VariablesInterReglas.ContainsKey("error") = False Then
            VariablesInterReglas.Add("error", "")
        Else
            VariablesInterReglas.Item("error") = ""
        End If


        Try
            If VariablesInterReglas.ContainsKey("GrupoAreaUsuarioActual") = False Then
                VariablesInterReglas.Add("GrupoAreaUsuarioActual", "")
            Else
                VariablesInterReglas.Item("GrupoAreaUsuarioActual") = ""
            End If

            If VariablesInterReglas.ContainsKey("NombresGruposSecuencia") = False Then
                VariablesInterReglas.Add("NombresGruposSecuencia", "")
            Else
                VariablesInterReglas.Item("NombresGruposSecuencia") = ""
            End If


            If VariablesInterReglas.ContainsKey(_myRule.ApproverVariable) = False Then
                VariablesInterReglas.Add(_myRule.ApproverVariable, "")
            Else
                VariablesInterReglas.Item(_myRule.ApproverVariable) = ""
            End If

            If VariablesInterReglas.ContainsKey(_myRule.SecuenceVariable) = False Then
                VariablesInterReglas.Add(_myRule.SecuenceVariable, "")
            Else
                VariablesInterReglas.Item(_myRule.SecuenceVariable) = ""
            End If

            If VariablesInterReglas.ContainsKey(_myRule.LevelVariable) = False Then
                VariablesInterReglas.Add(_myRule.LevelVariable, "")
            Else
                VariablesInterReglas.Item(_myRule.LevelVariable) = ""
            End If


            If VariablesInterReglas.ContainsKey("IdsGruposSecuencia") = False Then
                VariablesInterReglas.Add("IdsGruposSecuencia", "")
            Else
                VariablesInterReglas.Item("IdsGruposSecuencia") = ""
            End If


            If VariablesInterReglas.ContainsKey(_myRule.ApproversListVariable) = False Then
                VariablesInterReglas.Add(_myRule.ApproversListVariable, "")
            Else
                VariablesInterReglas.Item(_myRule.ApproversListVariable) = ""
            End If



            If (_myRule.OutputVariable1.Trim <> String.Empty) Then
                If VariablesInterReglas.ContainsKey(_myRule.OutputVariable1) = False Then
                    VariablesInterReglas.Add(_myRule.OutputVariable1, "")
                Else
                    VariablesInterReglas.Item(_myRule.OutputVariable1) = ""
                End If
            End If
            If (_myRule.OutputVariable2.Trim <> String.Empty) Then
                If VariablesInterReglas.ContainsKey(_myRule.OutputVariable2) = False Then
                    VariablesInterReglas.Add(_myRule.OutputVariable2, "")
                Else
                    VariablesInterReglas.Item(_myRule.OutputVariable2) = ""
                End If
            End If
            If (_myRule.OutputVariable3.Trim <> String.Empty) Then

                If VariablesInterReglas.ContainsKey(_myRule.OutputVariable3) = False Then
                    VariablesInterReglas.Add(_myRule.OutputVariable3, "")
                Else
                    VariablesInterReglas.Item(_myRule.OutputVariable3) = ""
                End If
            End If

            If VariablesInterReglas.ContainsKey("UsuarioActualSecuencia") = False Then
                VariablesInterReglas.Add("UsuarioActualSecuencia", "")
            Else
                VariablesInterReglas.Item("UsuarioActualSecuencia") = ""
            End If

            If VariablesInterReglas.ContainsKey("UsuarioActualAccion") = False Then
                VariablesInterReglas.Add("UsuarioActualAccion", "")
            Else
                VariablesInterReglas.Item("UsuarioActualAccion") = ""
            End If

            If VariablesInterReglas.ContainsKey("UsuarioActualPuedeAprobar") = False Then
                VariablesInterReglas.Add("UsuarioActualPuedeAprobar", "")
            Else
                VariablesInterReglas.Item("UsuarioActualPuedeAprobar") = ""
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try



        Try
            For Each R As ITaskResult In results
                Dim NotNullTaskIndexs As New List(Of IIndex)
                For Each TI As IIndex In R.Indexs
                    If TI.Data <> String.Empty AndAlso MatrixHasIndex(TI.ID) AndAlso ((TI.ID = 110 AndAlso TI.Data <> "0") OrElse (TI.ID = 2725 AndAlso TI.Data <> "0") OrElse (TI.ID <> 110 AndAlso TI.ID <> 2725)) Then
                        NotNullTaskIndexs.Add(TI)
                    ElseIf TI.ID <> _myRule.SecuenceIndex AndAlso TI.ID <> _myRule.ApproverIndex Then
                        If MatrixHasIndex(TI.ID) AndAlso (TI.Data = String.Empty OrElse (TI.ID = 110 AndAlso TI.Data = "0") OrElse (TI.ID = 2725 AndAlso TI.Data = "0")) Then
                            ZTrace.WriteLineIf(ZTrace.IsError, "Los valores necesarios para resolver la matriz estan vacios: Ej." & TI.Name)
                            If VariablesInterReglas.ContainsKey("error") = False Then
                                VariablesInterReglas.Add("error", "Los valores necesarios para resolver la matriz estan vacios: Ej." & TI.Name)
                            Else
                                VariablesInterReglas.Item("error") = "Los valores necesarios para resolver la matriz estan vacios: Ej." & TI.Name
                            End If
                            Throw New Exception("Los valores necesarios para resolver la matriz estan vacios: Ej." & TI.Name)
                            Exit For
                        End If

                    End If
                Next


                Dim LastSecuenceInRegistryStr As New StringBuilder
                LastSecuenceInRegistryStr.Append("Select NVL(MAX(nvl(i")
                LastSecuenceInRegistryStr.Append(_myRule.SecuenceIndex)
                LastSecuenceInRegistryStr.Append(", 0)), 0) From doc_i")
                LastSecuenceInRegistryStr.Append(_myRule.RegistryEntityId)
                LastSecuenceInRegistryStr.Append(" Where i")
                LastSecuenceInRegistryStr.Append(_myRule.RegistryIdIndex)
                LastSecuenceInRegistryStr.Append(" = ")
                LastSecuenceInRegistryStr.Append(R.GetIndexById(_myRule.RegistryIdIndex).Data)
                LastSecuenceInRegistryStr.Append(" And i")
                LastSecuenceInRegistryStr.Append(_myRule.RegistryActionIndex)
                LastSecuenceInRegistryStr.Append(" In (")
                LastSecuenceInRegistryStr.Append(_myRule.RegistryActions)
                LastSecuenceInRegistryStr.Append(") and I")
                '                LastSecuenceInRegistryStr.Append(_myRule.RegistryValidIndex)
                LastSecuenceInRegistryStr.Append("2826")
                LastSecuenceInRegistryStr.Append(" in (")
                '          LastSecuenceInRegistryStr.Append(_myRule.RegistryValid)
                LastSecuenceInRegistryStr.Append("1")
                LastSecuenceInRegistryStr.Append(") ")

                Dim LastSecuenceInRegistry As Int32 = Server.Con.ExecuteScalar(CommandType.Text, LastSecuenceInRegistryStr.ToString())
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ultima secuencia de aprobacion encontrada en la Entidad de Registro: " & LastSecuenceInRegistry)

                Dim LastSecuenceUserInRegistryStr As New StringBuilder
                LastSecuenceUserInRegistryStr.Append("Select nvl(I1020139,0)  From doc_i")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryEntityId)
                LastSecuenceUserInRegistryStr.Append(" Where i")
                LastSecuenceUserInRegistryStr.Append(_myRule.SecuenceIndex)
                LastSecuenceUserInRegistryStr.Append(" = (")

                LastSecuenceUserInRegistryStr.Append("Select NVL(MAX(nvl(i")
                LastSecuenceUserInRegistryStr.Append(_myRule.SecuenceIndex)
                LastSecuenceUserInRegistryStr.Append(", 0)), 0) From doc_i")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryEntityId)
                LastSecuenceUserInRegistryStr.Append(" Where i")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryIdIndex)
                LastSecuenceUserInRegistryStr.Append(" = ")
                LastSecuenceUserInRegistryStr.Append(R.GetIndexById(_myRule.RegistryIdIndex).Data)
                LastSecuenceUserInRegistryStr.Append(" And i")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryActionIndex)
                LastSecuenceUserInRegistryStr.Append(" In (")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryActions)
                LastSecuenceUserInRegistryStr.Append(") And I")
                '                LastSecuenceInRegistryStr.Append(_myRule.RegistryValidIndex)
                LastSecuenceUserInRegistryStr.Append("2826")
                LastSecuenceUserInRegistryStr.Append(" In (")
                '          LastSecuenceInRegistryStr.Append(_myRule.RegistryValid)
                LastSecuenceUserInRegistryStr.Append("1")
                LastSecuenceUserInRegistryStr.Append(") ")
                LastSecuenceUserInRegistryStr.Append(") and ")

                LastSecuenceUserInRegistryStr.Append(" i")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryIdIndex)
                LastSecuenceUserInRegistryStr.Append(" = ")
                LastSecuenceUserInRegistryStr.Append(R.GetIndexById(_myRule.RegistryIdIndex).Data)
                LastSecuenceUserInRegistryStr.Append(" And i")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryActionIndex)
                LastSecuenceUserInRegistryStr.Append(" In (")
                LastSecuenceUserInRegistryStr.Append(_myRule.RegistryActions)
                LastSecuenceUserInRegistryStr.Append(") And I")
                '                LastSecuenceInRegistryStr.Append(_myRule.RegistryValidIndex)
                LastSecuenceUserInRegistryStr.Append("2826")
                LastSecuenceUserInRegistryStr.Append(" In (")
                '          LastSecuenceInRegistryStr.Append(_myRule.RegistryValid)
                LastSecuenceUserInRegistryStr.Append("1")
                LastSecuenceUserInRegistryStr.Append(") ")


                Dim LastSecuenceUserInRegistry As Object = Server.Con.ExecuteScalar(CommandType.Text, LastSecuenceUserInRegistryStr.ToString())

                If IsDBNull(LastSecuenceUserInRegistry) Then
                    LastSecuenceUserInRegistry = 0
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ultimo usuario de ultima secuencia de aprobacion encontrada en la Entidad de Registro: " & LastSecuenceUserInRegistry)



                '2
                '------------------------------------------------------------------------------------------------------------------


                Dim CurrentUserAreaStr As New StringBuilder
                CurrentUserAreaStr.Append("Select g.name from usrgroup g inner join usr_r_group r on r.groupid = g.id where g.descripcion Like '%AREA%' and r.usrid = ")
                CurrentUserAreaStr.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID)
                Dim CurrentUserArea As String = Server.Con.ExecuteScalar(CommandType.Text, CurrentUserAreaStr.ToString())
                'Zamba_Sistemas_Tecnologia


                If (CurrentUserArea = String.Empty) Then
                    Dim Errorstring As String = "ERROR: El usuario no pertenece a un Grupo AREA y esto no permite ejecutar la secuencia de aprobacion."
                    If VariablesInterReglas.ContainsKey("error") = False Then
                        VariablesInterReglas.Add("error", Errorstring)
                    Else
                        VariablesInterReglas.Item("error") = Errorstring
                    End If

                    Throw New Exception(Errorstring)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Grupo Area del Usuario Actual: " & CurrentUserArea)
                    If VariablesInterReglas.ContainsKey("GrupoAreaUsuarioActual") = False Then
                        VariablesInterReglas.Add("GrupoAreaUsuarioActual", CurrentUserArea)
                    Else
                        VariablesInterReglas.Item("GrupoAreaUsuarioActual") = CurrentUserArea
                    End If
                End If
                '3
                '---------------------------------------------------------------------------------------------------------------------

                Dim CurrentUserAproverStr As New StringBuilder
                CurrentUserAproverStr.Append("Select g.name from usrgroup g inner join usr_r_group r on r.groupid = g.id where ")

                If Server.isOracle Then
                    CurrentUserAproverStr.Append("(upper")
                Else
                    CurrentUserAproverStr.Append("(to_upper")
                End If
                CurrentUserAproverStr.Append("(g.name) Like '%APROBADOR%'")

                If Server.isOracle Then
                    CurrentUserAproverStr.Append(" or upper")
                Else
                    CurrentUserAproverStr.Append(" or to_upper")
                End If
                CurrentUserAproverStr.Append("(g.name) Like '%GERENTE%'")

                If Server.isOracle Then
                    CurrentUserAproverStr.Append(" or upper")
                Else
                    CurrentUserAproverStr.Append(" or to_upper")
                End If
                CurrentUserAproverStr.Append("(g.name) Like '%SUBGTE%')")

                CurrentUserAproverStr.Append("and r.usrid = ")
                CurrentUserAproverStr.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID)
                'Null or Zamba_Sistemas_Tecnologia_Aprobador or Zamba_Sistemas_Gerente

                Dim CurrentUserAprover As Object = Server.Con.ExecuteScalar(CommandType.Text, CurrentUserAproverStr.ToString())


                Dim CurrentUserGenericAprover As String
                If CurrentUserAprover IsNot Nothing Then

                    'Transform To Generic
                    If (Not IsDBNull(CurrentUserAprover)) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Grupo de Aprobacion al que pertenece el usuario actual: " & CurrentUserAprover)

                        Dim GenericAprover As New StringBuilder
                        For Each Part As String In CurrentUserAprover.ToString().Split(Char.Parse("_"))
                            If Part.ToLower = "zamba" OrElse Part.ToLower = "aprobador" OrElse Part.ToLower = "gerente" OrElse Part.ToLower = "subgte" Then
                                GenericAprover.Append(Part)
                            Else
                                If Not GenericAprover.ToString.Contains("*") Then
                                    GenericAprover.Append("_*_")
                                End If
                            End If
                        Next
                        CurrentUserGenericAprover = GenericAprover.ToString
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Grupo Generico de Aprobacion al que pertenece el usuario actual: " & CurrentUserGenericAprover)

                End If

                '----------------------------------------------------------------------------------------------------------------


                'Lista de Grupos Aprobadores
                Dim CurrentAproversMatrixStr As New StringBuilder
                CurrentAproversMatrixStr.Append("Select  a.i153 Secuencia, b.name grupo, i1020136 Accion,  a.i10253 grupid from doc_i")
                CurrentAproversMatrixStr.Append(_myRule.MatrixEntityId)
                CurrentAproversMatrixStr.Append(" a, usrgroup b where a.i10253 = b.id")
                CurrentAproversMatrixStr.Append(" And (")

                Dim OneFound As Boolean = False
                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex1 AndAlso i.ID <> _myRule.OutputIndex2 AndAlso i.ID <> _myRule.OutputIndex3 AndAlso i.ID <> _myRule.ApproverIndex AndAlso i.ID <> _myRule.SecuenceIndex AndAlso i.ID <> _myRule.LevelIndex AndAlso i.ID <> _myRule.AmountIndex AndAlso i.Data <> "" AndAlso (i.Data <> "0" OrElse i.Type = IndexDataType.Si_No) Then

                        CurrentAproversMatrixStr.Append(" I")
                        CurrentAproversMatrixStr.Append(i.ID)
                        CurrentAproversMatrixStr.Append(" = ")
                        If (i.Type = IndexDataType.Alfanumerico OrElse i.Type = IndexDataType.Alfanumerico_Largo) Then
                            CurrentAproversMatrixStr.Append(" '")
                            CurrentAproversMatrixStr.Append(i.Data)
                            CurrentAproversMatrixStr.Append("'")
                        Else
                            CurrentAproversMatrixStr.Append(i.Data)
                        End If
                        CurrentAproversMatrixStr.Append(" and ")
                        OneFound = True
                    End If
                Next

                If OneFound Then
                    CurrentAproversMatrixStr.Remove(CurrentAproversMatrixStr.Length - 4, 4)
                    CurrentAproversMatrixStr.Append(" ) ")
                    CurrentAproversMatrixStr.Append(" order by a.i153 ")

                    Dim CurrentAproversMatrix As DataSet = Server.Con.ExecuteDataset(CommandType.Text, CurrentAproversMatrixStr.ToString)

                    If (CurrentAproversMatrix Is Nothing OrElse CurrentAproversMatrix.Tables.Count = 0 OrElse CurrentAproversMatrix.Tables(0).Rows.Count = 0) Then

                        Dim Errorstring As String = "No hay secuencia de Aprobacion cargada en la Matrix."
                        If VariablesInterReglas.ContainsKey("error") = False Then
                            VariablesInterReglas.Add("error", Errorstring)
                        Else
                            VariablesInterReglas.Item("error") = Errorstring
                        End If

                        Throw New Exception(Errorstring)

                    End If

                    Dim UserCanApprove As Boolean = False
                    Dim UserJustApproved As Boolean = False
                    Dim CurrentUserAproverSecuence As Int32
                    Dim CurrentUserAproverAction As Int32

                    ZTrace.WriteLineIf(ZTrace.IsError, "Secuencia de Aprobacion cargada en la Matrix:")
                    ' Dim i As Int32 = 0
                    Dim SecuenceGroupIdsList As New List(Of String)
                    Dim SecuenceGroupNamesList As New List(Of String)
                    For Each GrupoAprobacion As DataRow In CurrentAproversMatrix.Tables(0).Rows

                        Dim GroupName As String = GrupoAprobacion("grupo").ToString
                        Dim GroupId As String = GrupoAprobacion("grupid").ToString
                        ZTrace.WriteLineIf(ZTrace.IsError, String.Format("Secuencia: {0} - Grupo: {1} ", GrupoAprobacion("secuencia").ToString, GroupName))

                        If GroupName.Contains("*") Then
                            If GroupName.ToLower() = CurrentUserGenericAprover.ToLower() Then
                                'El usuario es aprobador INdirecto, es decir con Zamba_*_*..... hay que ver si la matrix tiene que tener el area para generar mejor la comparacion
                                CurrentUserAproverSecuence = GrupoAprobacion("secuencia").ToString
                                CurrentUserAproverAction = GrupoAprobacion("Accion").ToString
                                ZTrace.WriteLineIf(ZTrace.IsError, "El usuario pertenece a la matriz")
                                ZTrace.WriteLineIf(ZTrace.IsError, String.Format("La secuencia del  usuario actual es {0} con accion {1}", CurrentUserAproverSecuence, CurrentUserAproverAction))

                                If CurrentUserAproverSecuence > LastSecuenceInRegistry Then
                                    UserCanApprove = True
                                    ZTrace.WriteLineIf(ZTrace.IsError, "El usuario puede aprobar")
                                End If
                            End If

                            Try
                                If CurrentUserArea <> String.Empty Then
                                    'Zamba_*_Aprobador
                                    'Zamba_XXXX_Aprobador or Zamba_XXXXX_XXXXX_Aprobador or ZAMBA_XXXX_XXXX_XXXX_Aprobador o su variante de Gerente
                                    Dim ApproverRank As String = GroupName.Replace("Zamba_*", "")
                                    Dim ApproverId As Int64 = UserGroupBusiness.GetUserorGroupIdbyName(CurrentUserArea & ApproverRank)

                                    If ApproverId > 0 Then
                                        GroupId = ApproverId
                                    Else
                                        Dim i As Int16
                                        Dim CurrentUserAreaSplited As String = CurrentUserArea
                                        For i = CurrentUserArea.ToString().Split(Char.Parse("_")).Count - 1 To 0 Step -1
                                            If (CurrentUserAreaSplited.Contains("_")) Then
                                                CurrentUserAreaSplited = CurrentUserAreaSplited.Substring(0, CurrentUserArea.LastIndexOf("_"))
                                                ApproverId = UserGroupBusiness.GetUserorGroupIdbyName(CurrentUserAreaSplited & ApproverRank)
                                                If ApproverId > 0 Then
                                                    GroupId = ApproverId
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Catch ex As Exception

                            End Try

                            Try
                                If GroupName.Contains("Zamba_*_CAC") Then

                                    Dim CurrentDelegacion As String = R.GetIndexById(82).dataDescription
                                    If CurrentDelegacion.StartsWith("Zamba_") = False Then
                                        CurrentDelegacion = "Zamba_" & CurrentDelegacion
                                    End If
                                    If CurrentDelegacion <> String.Empty Then
                                        'Zamba_*_CAC
                                        'Zamba_*_CAC_Aprobador
                                        'Zamba_CAC_XXXX_Aprobador or Zamba_CAC_XXXXX_XXXXX_Aprobador or ZAMBA_CAC_XXXX_XXXX_XXXX_Aprobador o su variante de Gerente
                                        Dim ApproverRank As String = GroupName.Replace("Zamba_*_CAC", "")
                                        Dim ApproverId As Int64 = UserGroupBusiness.GetUserorGroupIdbyName(CurrentDelegacion & ApproverRank)
                                        Dim currentCACGroup As String
                                        If ApproverId > 0 Then
                                            GroupId = ApproverId
                                            currentCACGroup = CurrentDelegacion & ApproverRank
                                        Else
                                            Dim i As Int16
                                            Dim CurrentUserAreaSplited As String = CurrentDelegacion
                                            For i = CurrentDelegacion.ToString().Split(Char.Parse("_")).Count - 1 To 0 Step -1
                                                If (CurrentUserAreaSplited.Contains("_")) Then
                                                    CurrentUserAreaSplited = CurrentUserAreaSplited.Substring(0, CurrentDelegacion.LastIndexOf("_"))
                                                    ApproverId = UserGroupBusiness.GetUserorGroupIdbyName(CurrentUserAreaSplited & ApproverRank)
                                                    If ApproverId > 0 Then
                                                        GroupId = ApproverId
                                                        currentCACGroup = CurrentUserAreaSplited & ApproverRank
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If


                                        If GroupId > 0 AndAlso (currentCACGroup = CurrentUserGenericAprover OrElse currentCACGroup = CurrentUserArea OrElse currentCACGroup = CurrentUserAprover) Then
                                            'El usuario es aprobador INdirecto, es decir con Zamba_*_*..... hay que ver si la matrix tiene que tener el area para generar mejor la comparacion
                                            CurrentUserAproverSecuence = GrupoAprobacion("secuencia").ToString
                                            CurrentUserAproverAction = GrupoAprobacion("Accion").ToString
                                            ZTrace.WriteLineIf(ZTrace.IsError, "El usuario pertenece a la matriz")
                                            ZTrace.WriteLineIf(ZTrace.IsError, String.Format("La secuencia del  usuario actual es {0} con accion {1}", CurrentUserAproverSecuence, CurrentUserAproverAction))

                                            'El usuario es aprobador directo.
                                            If UserCanApprove = False AndAlso UserJustApproved = False Then

                                                If CurrentUserAproverSecuence > LastSecuenceInRegistry Then
                                                    UserCanApprove = True
                                                    ZTrace.WriteLineIf(ZTrace.IsError, "El usuario puede aprobar")
                                                End If
                                                If CurrentUserAproverSecuence = LastSecuenceInRegistry AndAlso LastSecuenceUserInRegistry = Membership.MembershipHelper.CurrentUser.ID Then
                                                    UserJustApproved = True
                                                    ZTrace.WriteLineIf(ZTrace.IsError, "El usuario ya aprobo")
                                                End If


                                            ElseIf UserCanApprove = False AndAlso UserJustApproved = True Then

                                                Dim PreviusCurrentUserAproverSecuence As Int64 = CurrentUserAproverSecuence
                                                Dim NextCurrentUserAproverSecuence As Int64 = GrupoAprobacion("secuencia").ToString

                                                Dim PreviusCurrentUserAproverAction As Int64 = CurrentUserAproverSecuence
                                                Dim NextCurrentUserAproverAction As Int64 = GrupoAprobacion("Accion").ToString

                                                If PreviusCurrentUserAproverSecuence + 1 = NextCurrentUserAproverSecuence Then

                                                    '                                        If PreviusCurrentUserAproverAction = NextCurrentUserAproverAction Then
                                                    CurrentUserAproverSecuence = GrupoAprobacion("secuencia").ToString
                                                    CurrentUserAproverAction = GrupoAprobacion("Accion").ToString

                                                    ZTrace.WriteLineIf(ZTrace.IsError, "El usuario pertenece a la matriz")
                                                    ZTrace.WriteLineIf(ZTrace.IsError, String.Format("La secuencia del  usuario actual es {0} con accion {1}", CurrentUserAproverSecuence, CurrentUserAproverAction))

                                                    If CurrentUserAproverSecuence > LastSecuenceInRegistry Then
                                                        UserCanApprove = True
                                                        ZTrace.WriteLineIf(ZTrace.IsError, "El usuario puede aprobar")
                                                    End If
                                                    If CurrentUserAproverSecuence = LastSecuenceInRegistry AndAlso LastSecuenceUserInRegistry = Membership.MembershipHelper.CurrentUser.ID Then
                                                        UserJustApproved = True
                                                        ZTrace.WriteLineIf(ZTrace.IsError, "El usuario ya aprobo")
                                                    End If

                                                    '                                       End If
                                                End If

                                            End If





                                        End If

                                        End If
                                End If

                            Catch ex As Exception

                            End Try

                        Else

                            Dim UB As New UserGroupBusiness
                            Dim currentUserGroupsIds As List(Of Int64) = UB.GetGroupsAndInheritanceOfGroupsIds(Zamba.Membership.MembershipHelper.CurrentUser.ID, True)

                            If GroupName = CurrentUserAprover OrElse GroupName = CurrentUserArea OrElse currentUserGroupsIds.Contains(GroupId) Then
                                'El usuario es aprobador directo.
                                If UserCanApprove = False AndAlso UserJustApproved = False Then
                                    CurrentUserAproverSecuence = GrupoAprobacion("secuencia").ToString
                                    CurrentUserAproverAction = GrupoAprobacion("Accion").ToString
                                    ZTrace.WriteLineIf(ZTrace.IsError, "El usuario pertenece a la matriz")
                                    ZTrace.WriteLineIf(ZTrace.IsError, String.Format("La secuencia del  usuario actual es {0} con accion {1}", CurrentUserAproverSecuence, CurrentUserAproverAction))

                                    If CurrentUserAproverSecuence > LastSecuenceInRegistry Then
                                        UserCanApprove = True
                                        ZTrace.WriteLineIf(ZTrace.IsError, "El usuario puede aprobar")
                                    End If
                                    If CurrentUserAproverSecuence = LastSecuenceInRegistry AndAlso LastSecuenceUserInRegistry = Membership.MembershipHelper.CurrentUser.ID Then
                                        UserJustApproved = True
                                        ZTrace.WriteLineIf(ZTrace.IsError, "El usuario ya aprobo")
                                    End If

                                ElseIf UserCanApprove = False AndAlso UserJustApproved = True Then

                                    Dim PreviusCurrentUserAproverSecuence As Int64 = CurrentUserAproverSecuence
                                    Dim NextCurrentUserAproverSecuence As Int64 = GrupoAprobacion("secuencia").ToString

                                    Dim PreviusCurrentUserAproverAction As Int64 = CurrentUserAproverSecuence
                                    Dim NextCurrentUserAproverAction As Int64 = GrupoAprobacion("Accion").ToString

                                    If PreviusCurrentUserAproverSecuence + 1 = NextCurrentUserAproverSecuence Then

                                        '                                        If PreviusCurrentUserAproverAction = NextCurrentUserAproverAction Then
                                        CurrentUserAproverSecuence = GrupoAprobacion("secuencia").ToString
                                        CurrentUserAproverAction = GrupoAprobacion("Accion").ToString

                                        ZTrace.WriteLineIf(ZTrace.IsError, "El usuario pertenece a la matriz")
                                        ZTrace.WriteLineIf(ZTrace.IsError, String.Format("La secuencia del  usuario actual es {0} con accion {1}", CurrentUserAproverSecuence, CurrentUserAproverAction))

                                        If CurrentUserAproverSecuence > LastSecuenceInRegistry Then
                                            UserCanApprove = True
                                            ZTrace.WriteLineIf(ZTrace.IsError, "El usuario puede aprobar")
                                        End If
                                        If CurrentUserAproverSecuence = LastSecuenceInRegistry AndAlso LastSecuenceUserInRegistry = Membership.MembershipHelper.CurrentUser.ID Then
                                            UserJustApproved = True
                                            ZTrace.WriteLineIf(ZTrace.IsError, "El usuario ya aprobo")
                                        End If

                                        '                                       End If
                                    End If

                                End If

                            End If
                        End If


                        SecuenceGroupIdsList.Add(GroupId)
                        SecuenceGroupNamesList.Add(GroupName)


                    Next



                    If VariablesInterReglas.ContainsKey("NombresGruposSecuencia") = False Then
                        VariablesInterReglas.Add("NombresGruposSecuencia", String.Join(",", SecuenceGroupNamesList.ToArray()))
                    Else
                        VariablesInterReglas.Item("NombresGruposSecuencia") = String.Join(",", SecuenceGroupNamesList.ToArray())
                    End If

                    If UserCanApprove = False Then
                        ZTrace.WriteLineIf(ZTrace.IsError, "El usuario NO puede aprobar o ya aprobo")

                    End If
                    'Dim CurrentUserLevelStr As New StringBuilder
                    'CurrentUserLevelStr.Append("Select nvl(m.nivel, 0) from ")

                    ''GruposAprobadores
                    'CurrentUserLevelStr.Append(" ( Select a.i")
                    'CurrentUserLevelStr.Append(_myRule.SecuenceIndex)
                    'CurrentUserLevelStr.Append(" Secuencia, b.name grupo from doc_i")
                    'CurrentUserLevelStr.Append(_myRule.MatrixEntityId)
                    'CurrentUserLevelStr.Append("a, usrgroup b where a.i")
                    'CurrentUserLevelStr.Append(_myRule.ApproverIndex)
                    'CurrentUserLevelStr.Append("= b.id ) gruposaprobadores ")

                    'CurrentUserLevelStr.Append(" where ")
                    'CurrentUserLevelStr.Append(" grupoaprobadores.grupo = ")
                    'CurrentUserLevelStr.Append(CurrentUserGenericAprover)
                    'CurrentUserLevelStr.Append(" Or ")

                    'CurrentUserLevelStr.Append("grupoaprobadores. In (Select z.name usr_r_group y, usrgroup z")
                    'CurrentUserLevelStr.Append(" where ")
                    'CurrentUserLevelStr.Append(" y.groupid = z.id And y.usrid = ")
                    'CurrentUserLevelStr.Append(Membership.MembershipHelper.CurrentUser.ID)
                    'CurrentUserLevelStr.Append(" )")
                    'Dim CurrentUserLevel As Int32 = Server.Con.ExecuteScalar(CommandType.Text, CurrentUserLevelStr.ToString())


                    Dim NextLevelStr As New StringBuilder
                    NextLevelStr.Append("select * from (Select ")

                    If Server.isSQLServer Then
                        NextLevelStr.Append(" top (1) ")
                    End If

                    NextLevelStr.Append(" a.i")
                    NextLevelStr.Append(_myRule.ApproverIndex)
                    NextLevelStr.Append(" Aprobador, a.i")
                    NextLevelStr.Append(_myRule.SecuenceIndex)
                    NextLevelStr.Append(" Secuencia, b.name NombreGrupo, ")
                    NextLevelStr.Append(" a.i")
                    NextLevelStr.Append(_myRule.LevelIndex)
                    NextLevelStr.Append(" Nivel")

                    If (_myRule.OutputIndex1 <> 0 AndAlso _myRule.OutputVariable1 <> String.Empty) Then
                        NextLevelStr.Append(", I")
                        NextLevelStr.Append(_myRule.OutputIndex1)
                        NextLevelStr.Append(" As ")
                        NextLevelStr.Append(_myRule.OutputVariable1)
                    End If

                    If (_myRule.OutputIndex2 <> 0 AndAlso _myRule.OutputVariable2 <> String.Empty) Then
                        NextLevelStr.Append(", I")
                        NextLevelStr.Append(_myRule.OutputIndex2)
                        NextLevelStr.Append(" As ")
                        NextLevelStr.Append(_myRule.OutputVariable2)
                    End If

                    If (_myRule.OutputIndex3 <> 0 AndAlso _myRule.OutputVariable3 <> String.Empty) Then

                        NextLevelStr.Append(", I")
                        NextLevelStr.Append(_myRule.OutputIndex3)
                        NextLevelStr.Append(" As ")
                        NextLevelStr.Append(_myRule.OutputVariable3)
                    End If



                    NextLevelStr.Append(" From doc_i")
                    NextLevelStr.Append(_myRule.MatrixEntityId)
                    NextLevelStr.Append(" a, usrgroup b ")
                    NextLevelStr.Append(" Where i")
                    NextLevelStr.Append(_myRule.AmountIndex)
                    NextLevelStr.Append(" <= ")
                    Dim AmountIndex As Int64 = _myRule.AmountIndex
                    NextLevelStr.Append(Math.Round(Double.Parse(DirectCast(R, Zamba.Core.Result).GetIndexById(AmountIndex).Data)))
                    NextLevelStr.Append(" And i")
                    NextLevelStr.Append(_myRule.SecuenceIndex)
                    NextLevelStr.Append(" > (")
                    NextLevelStr.Append(LastSecuenceInRegistry)
                    NextLevelStr.Append(" ) ")
                    NextLevelStr.Append(" And i")
                    NextLevelStr.Append(_myRule.ApproverIndex)
                    NextLevelStr.Append(" = b.id And i")
                    NextLevelStr.Append(_myRule.SecuenceIndex)
                    NextLevelStr.Append(" > (")
                    NextLevelStr.Append(CurrentUserAproverSecuence)
                    NextLevelStr.Append(" ) ")

                    NextLevelStr.Append(" And I")
                    NextLevelStr.Append(_myRule.ApproverIndex)
                    NextLevelStr.Append(" Is Not null ")

                    NextLevelStr.Append(" And (")

                    For Each i As IIndex In NotNullTaskIndexs
                        If i.ID <> _myRule.OutputIndex1 AndAlso i.ID <> _myRule.OutputIndex2 AndAlso i.ID <> _myRule.OutputIndex3 AndAlso i.ID <> _myRule.ApproverIndex AndAlso i.ID <> _myRule.SecuenceIndex AndAlso i.ID <> _myRule.LevelIndex AndAlso i.ID <> _myRule.AmountIndex AndAlso i.Data <> "" AndAlso (i.Data <> "0" OrElse i.Type = IndexDataType.Si_No) Then
                            NextLevelStr.Append(" I")
                            NextLevelStr.Append(i.ID)
                            NextLevelStr.Append(" = ")
                            If (i.Type = IndexDataType.Alfanumerico OrElse i.Type = IndexDataType.Alfanumerico_Largo) Then
                                NextLevelStr.Append(" '")
                                NextLevelStr.Append(i.Data)
                                NextLevelStr.Append("'")
                            Else
                                NextLevelStr.Append(i.Data)
                            End If
                            NextLevelStr.Append(" and ")
                        End If
                    Next

                    NextLevelStr.Remove(NextLevelStr.Length - 4, 4)
                    NextLevelStr.Append(" ) ")

                    NextLevelStr.Append(" order by i")
                    NextLevelStr.Append(_myRule.SecuenceIndex)
                    NextLevelStr.Append(" , I")
                    NextLevelStr.Append(_myRule.LevelIndex)

                    NextLevelStr.Append(" ) ")

                    'If Server.isOracle Then
                    '    NextLevelStr.Append(" where rownum = 1")
                    'End If

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, NextLevelStr.ToString())
                    Dim values As DataSet = Server.Con.ExecuteDataset(CommandType.Text, NextLevelStr.ToString)


                    If values IsNot Nothing AndAlso values.Tables(0).Rows.Count > 0 Then

                        Dim Founded As Boolean
                        Dim RealApproverId As Int64
                        Dim SelectedRow As DataRow

                        For Each RowValue As DataRow In values.Tables(0).Rows
                            SelectedRow = RowValue
                            Dim ApproverValue As String = RowValue("Aprobador").ToString()
                            Dim IsGroup As Boolean
                            Dim Approver As String = UserGroupBusiness.GetUserorGroupNamebyId(Int64.Parse(ApproverValue), IsGroup)

                            Dim RealApproverName As String

                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Siguiente Nivel Obtenido Id: {0} Nombre: {1}", ApproverValue, Approver))


                            If (Approver.Contains("*")) Then
                                If CurrentUserArea <> String.Empty Then
                                    'Zamba_*_Aprobador
                                    'Zamba_XXXX_Aprobador or Zamba_XXXXX_XXXXX_Aprobador or ZAMBA_XXXX_XXXX_XXXX_Aprobador o su variante de Gerente
                                    Dim ApproverRank As String = Approver.Replace("Zamba_*", "")
                                    Dim ApproverId As Int64 = UserGroupBusiness.GetUserorGroupIdbyName(CurrentUserArea & ApproverRank)
                                    If ApproverId > 0 Then
                                        RealApproverId = ApproverId
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Siguiente Nivel REAL Obtenido Id: {0} Nombre: {1}", RealApproverId, CurrentUserArea & ApproverRank))
                                        Founded = True
                                        Exit For
                                    Else
                                        Dim i As Int16
                                        Dim CurrentUserAreaSplited As String = CurrentUserArea
                                        For i = CurrentUserArea.ToString().Split(Char.Parse("_")).Count - 1 To 0 Step -1
                                            If (CurrentUserAreaSplited.Contains("_")) Then
                                                CurrentUserAreaSplited = CurrentUserAreaSplited.Substring(0, CurrentUserArea.LastIndexOf("_"))
                                                ApproverId = UserGroupBusiness.GetUserorGroupIdbyName(CurrentUserAreaSplited & ApproverRank)
                                                If ApproverId > 0 Then
                                                    RealApproverId = ApproverId
                                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Siguiente Nivel REAL Obtenido Id: {0} Nombre: {1}", RealApproverId, CurrentUserAreaSplited & ApproverRank))
                                                    Founded = True
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                        If Founded Then
                                            Exit For
                                        End If
                                    End If
                                End If

                                If Approver.Contains("Zamba_*_CAC") Then
                                    If Not RealApproverId > 0 Then
                                        Try
                                            Dim CurrentDelegacion As String = R.GetIndexById(82).dataDescription
                                            If CurrentDelegacion.StartsWith("Zamba_") = False Then
                                                CurrentDelegacion = "Zamba_" & CurrentDelegacion
                                            End If
                                            If CurrentDelegacion <> String.Empty Then
                                                'Zamba_*_Aprobador
                                                'Zamba_XXXX_Aprobador or Zamba_XXXXX_XXXXX_Aprobador or ZAMBA_XXXX_XXXX_XXXX_Aprobador o su variante de Gerente
                                                Dim ApproverRank As String = Approver.Replace("Zamba_*_CAC", "")
                                                Dim ApproverId As Int64 = UserGroupBusiness.GetUserorGroupIdbyName(CurrentDelegacion & ApproverRank)
                                                If ApproverId > 0 Then
                                                    RealApproverId = ApproverId
                                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Siguiente Nivel REAL Obtenido Id: {0} Nombre: {1}", RealApproverId, CurrentDelegacion & ApproverRank))
                                                    Founded = True
                                                    Exit For
                                                Else
                                                    Dim i As Int16
                                                    Dim CurrentUserAreaSplited As String = CurrentDelegacion
                                                    For i = CurrentDelegacion.ToString().Split(Char.Parse("_")).Count - 1 To 0 Step -1
                                                        If (CurrentUserAreaSplited.Contains("_")) Then
                                                            CurrentUserAreaSplited = CurrentUserAreaSplited.Substring(0, CurrentDelegacion.LastIndexOf("_"))
                                                            ApproverId = UserGroupBusiness.GetUserorGroupIdbyName(CurrentUserAreaSplited & ApproverRank)
                                                            If ApproverId > 0 Then
                                                                RealApproverId = ApproverId
                                                                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Siguiente Nivel REAL Obtenido Id: {0} Nombre: {1}", RealApproverId, CurrentUserAreaSplited & ApproverRank))
                                                                Founded = True
                                                                Exit For
                                                            End If
                                                        End If
                                                    Next
                                                    If Founded Then
                                                        Exit For
                                                    End If
                                                End If
                                            End If
                                        Catch ex As Exception

                                        End Try
                                    End If
                                End If
                            Else

                                If ExistGroup(ApproverValue) Then
                                    RealApproverId = ApproverValue
                                    Founded = True
                                    Exit For
                                Else
                                    Dim Errorstring As String = String.Format("No existe el Grupo aprobador: {0} en Zamba, definido en la matriz. No se puede continuar con el proceso de aprobacion.", ApproverValue)
                                    If VariablesInterReglas.ContainsKey("error") = False Then
                                        VariablesInterReglas.Add("error", Errorstring)
                                    Else
                                        VariablesInterReglas.Item("error") = Errorstring
                                    End If

                                    Throw New Exception(Errorstring)

                                End If
                            End If

                        Next

                        If Founded = False Then
                            Dim Errorstring As String = "No existe el aprobador en Zamba, segun la matriz definida. No se puede continuar con el proceso de aprobacion."
                            If VariablesInterReglas.ContainsKey("error") = False Then
                                VariablesInterReglas.Add("error", Errorstring)
                            Else
                                VariablesInterReglas.Item("error") = Errorstring
                            End If

                            Throw New Exception(Errorstring)

                        End If

                        If VariablesInterReglas.ContainsKey(_myRule.ApproverVariable) = False Then
                            VariablesInterReglas.Add(_myRule.ApproverVariable, RealApproverId)
                        Else
                            VariablesInterReglas.Item(_myRule.ApproverVariable) = RealApproverId
                        End If

                        If VariablesInterReglas.ContainsKey(_myRule.SecuenceVariable) = False Then
                            VariablesInterReglas.Add(_myRule.SecuenceVariable, SelectedRow("Secuencia").ToString)
                        Else
                            VariablesInterReglas.Item(_myRule.SecuenceVariable) = SelectedRow("Secuencia").ToString
                        End If

                        If VariablesInterReglas.ContainsKey(_myRule.LevelVariable) = False Then
                            VariablesInterReglas.Add(_myRule.LevelVariable, SelectedRow("Nivel").ToString)
                        Else
                            VariablesInterReglas.Item(_myRule.LevelVariable) = SelectedRow("Nivel").ToString
                        End If


                        If VariablesInterReglas.ContainsKey("IdsGruposSecuencia") = False Then
                            VariablesInterReglas.Add("IdsGruposSecuencia", String.Join(",", SecuenceGroupIdsList.ToArray()))
                        Else
                            VariablesInterReglas.Item("IdsGruposSecuencia") = String.Join(",", SecuenceGroupIdsList.ToArray())
                        End If
                        Try
                            If (SecuenceGroupIdsList IsNot Nothing AndAlso SecuenceGroupIdsList.Count > 0) Then
                                Dim IGIDS As IIndex = R.GetIndexById(11525195)
                                If IGIDS IsNot Nothing Then
                                    If (IGIDS.Data <> String.Join(",", SecuenceGroupIdsList.ToArray())) Then
                                        IGIDS.Data = String.Join(",", SecuenceGroupIdsList.ToArray())
                                        IGIDS.DataTemp = IGIDS.Data

                                        Dim RB As Results_Business = New Results_Business()
                                        RB.SaveModifiedIndexData(R, True, False, New List(Of Long)() From {IGIDS.ID}, Nothing)
                                        Dim UB As UserBusiness = New UserBusiness()
                                        UB.SaveAction(R.ID, Zamba.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, R.Name, 0)
                                    End If

                                End If
                            End If

                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try

                        If VariablesInterReglas.ContainsKey(_myRule.ApproversListVariable) = False Then
                            VariablesInterReglas.Add(_myRule.ApproversListVariable, String.Join(",", SecuenceGroupIdsList.ToArray()))
                        Else
                            VariablesInterReglas.Item(_myRule.ApproversListVariable) = String.Join(",", SecuenceGroupIdsList.ToArray())
                        End If


                        Try

                            If (_myRule.OutputVariable1.Trim <> String.Empty) Then
                                If VariablesInterReglas.ContainsKey(_myRule.OutputVariable1) = False Then
                                    VariablesInterReglas.Add(_myRule.OutputVariable1, SelectedRow(_myRule.OutputVariable1).ToString)
                                Else
                                    VariablesInterReglas.Item(_myRule.OutputVariable1) = SelectedRow(_myRule.OutputVariable1).ToString
                                End If
                            End If
                            If (_myRule.OutputVariable2.Trim <> String.Empty) Then
                                If VariablesInterReglas.ContainsKey(_myRule.OutputVariable2) = False Then
                                    VariablesInterReglas.Add(_myRule.OutputVariable2, SelectedRow(_myRule.OutputVariable2).ToString)
                                Else
                                    VariablesInterReglas.Item(_myRule.OutputVariable2) = SelectedRow(_myRule.OutputVariable2).ToString
                                End If
                            End If
                            If (_myRule.OutputVariable3.Trim <> String.Empty) Then

                                If VariablesInterReglas.ContainsKey(_myRule.OutputVariable3) = False Then
                                    VariablesInterReglas.Add(_myRule.OutputVariable3, SelectedRow(_myRule.OutputVariable3).ToString)
                                Else
                                    VariablesInterReglas.Item(_myRule.OutputVariable3) = SelectedRow(_myRule.OutputVariable3).ToString
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try

                    End If

                    Try
                        If VariablesInterReglas.ContainsKey("UsuarioActualSecuencia") = False Then
                            VariablesInterReglas.Add("UsuarioActualSecuencia", CurrentUserAproverSecuence)
                        Else
                            VariablesInterReglas.Item("UsuarioActualSecuencia") = CurrentUserAproverSecuence
                        End If

                        If VariablesInterReglas.ContainsKey("UsuarioActualAccion") = False Then
                            VariablesInterReglas.Add("UsuarioActualAccion", CurrentUserAproverAction)
                        Else
                            VariablesInterReglas.Item("UsuarioActualAccion") = CurrentUserAproverAction
                        End If

                        If VariablesInterReglas.ContainsKey("UsuarioActualPuedeAprobar") = False Then
                            VariablesInterReglas.Add("UsuarioActualPuedeAprobar", UserCanApprove)
                        Else
                            VariablesInterReglas.Item("UsuarioActualPuedeAprobar") = UserCanApprove
                        End If


                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    lstResults.Add(R)
                Else
                    Dim Errorstring As String = "No hay secuencia de Aprobacion cargada en la Matrix."
                    If VariablesInterReglas.ContainsKey("error") = False Then
                        VariablesInterReglas.Add("error", Errorstring)
                    Else
                        VariablesInterReglas.Item("error") = Errorstring
                    End If

                    Throw New Exception(Errorstring)
                End If

            Next
        Finally
        End Try
        Return lstResults
    End Function


    Private Function ExistGroup(ByVal GroupId As Int64) As Boolean
        Dim ug As IUserGroup
        Dim UserGroupBusiness As New UserGroupBusiness
        Dim Group As IUserGroup = UserGroupBusiness.GetUserGroup(GroupId)
        If (Group IsNot Nothing) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function MatrixHasIndex(ID As Int64) As Boolean
        For Each I As Index In MatrixEntity.Indexs
            If I.ID = ID Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
