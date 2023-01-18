Imports System.Windows.Forms

Public Class PlayDOEDITTABLE
    Private varName As String
    Private Data As Object
    Private DT As DataTable
    Private dv As DataView
    Private ds As DataSet
    Private myRule As IDOEditTable

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            varName = myRule.VarSource

            If varName.ToLower.Contains("zvar") Then
                varName = varName.Remove(0, 5)
                varName = varName.Remove(varName.Length - 1)
            End If

            'falta hacer que se puedan seleccionar consultas predefinidas Modulo de integracion con Base de Datos
            If VariablesInterReglas.ContainsKey(varName) = False Then
                Throw New Exception("No se encontró la variable de origen de datos")
            Else
                'Obtiene las variables
                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variables inter reglas.")

                Data = VariablesInterReglas.Item(varName)

                If Not IsNothing(Data) Then
                    If (TypeOf (Me.Data) Is DataSet) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Es dataset")
                        DT = DirectCast(Data, DataSet).Tables(0)
                    ElseIf (TypeOf (Me.Data) Is DataTable) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Es datatable")
                        DT = DirectCast(Data, DataTable)
                    Else
                        Throw New Exception("La variable de origen de datos no es un conjunto de datos")
                    End If
                Else
                    Throw New Exception("La variable de origen de datos esta vacia")
                End If

                dv = New DataView(DT)
                Select Case myRule.EditType
                    Case 0
                        Trace.WriteLineIf(ZTrace.IsInfo, "sort: " & myRule.KeyColumn & ", " & myRule.EditColumn & " desc")
                        dv.Sort = myRule.KeyColumn & ", " & myRule.EditColumn & " desc"
                    Case 1
                        Trace.WriteLineIf(ZTrace.IsInfo, "sort: " & myRule.KeyColumn & ", " & myRule.EditColumn & " asc")
                        dv.Sort = myRule.KeyColumn & ", " & myRule.EditColumn & " asc"
                    Case 2
                        Trace.WriteLineIf(ZTrace.IsInfo, "sort: " & myRule.KeyColumn & " desc")
                        dv.Sort = myRule.KeyColumn & " desc"
                    Case 3
                        Trace.WriteLineIf(ZTrace.IsInfo, "sort: " & myRule.KeyColumn & " asc")
                        dv.Sort = myRule.KeyColumn & " asc"
                    Case 4
                        Trace.WriteLineIf(ZTrace.IsInfo, "Sin sort")
                End Select
                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo nueva tabla")

                DT = getValues(dv.ToTable(), myRule.KeyColumn)
                ds = New DataSet()
                ds.Tables.Add(DT)

                If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = False Then
                    VariablesInterReglas.Add(myRule.VarDestiny, ds, False)
                Else
                    VariablesInterReglas.Item(myRule.VarDestiny) = ds
                End If
            End If
        Finally
            Trace.WriteLineIf(ZTrace.IsInfo, "Liberando recursos.")

            If Not IsNothing(varName) Then
                varName = String.Empty
            End If
            varName = Nothing

            If Not IsNothing(dv) Then
                dv.Dispose()
            End If
            dv = Nothing

            If Not IsNothing(DT) Then
                DT.Dispose()
            End If
            DT = Nothing

            If Not IsNothing(ds) Then
                ds.Dispose()
            End If
            ds = Nothing

            Data = Nothing
        End Try

        NewResults.AddRange(results)

        Return NewResults
    End Function

    Private Function getValues(ByVal dt As DataTable, ByVal keyColumn As String) As DataTable
        Dim newdt As DataTable = dt.Clone()

        Dim lastdr As DataRow = Nothing
        For Each dr As DataRow In dt.Rows
            If IsNothing(lastdr) Then
                newdt.Rows.Add(dr.ItemArray())
                lastdr = dr
            Else
                If (dr(keyColumn) <> lastdr(keyColumn)) Then
                    newdt.Rows.Add(dr.ItemArray())
                    lastdr = dr
                End If
            End If
        Next

        Return newdt
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOEditTable)
        Me.myRule = rule
    End Sub
End Class