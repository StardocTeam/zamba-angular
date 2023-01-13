Imports System.Text

Namespace Search
    Public Class ModDocumentsExt
        Inherits ModDocuments




        ''' <summary>
        ''' Agrega las restricciones y cierra la consulta principal
        ''' </summary>
        ''' <param name="DocType">Entidad asociada</param>
        ''' <param name="ColumnCondstring">Condiciones</param>
        ''' <param name="Orderstring">Ordenamiento</param>
        ''' <param name="CurrentUserId">ID de Usuario</param>
        ''' <param name="strselect">Consulta general, es el cuerpo donde se agregará lo que falte</param>
        ''' <param name="dateDeclarationString">Formato de fecha</param>
        ''' <param name="RestrictionString">Restricciones</param>
        ''' <returns>True, si la consulta se construyó correctamente</returns>
        ''' <remarks>Simplemente agrega datos al StringBuilder original</remarks>
        Private Function InsertRestrictionConditionString(ByVal DocType As DocType, ByVal ColumnCondstring As StringBuilder, ByVal Orderstring As StringBuilder, ByVal CurrentUserId As Long, ByRef strselect As StringBuilder, ByRef dateDeclarationString As StringBuilder, ByVal RestrictionString As StringBuilder) As Boolean
            If CurrentUserId > 0 Then
                Dim FilterString As String = String.Empty
                Dim hayWhere As Boolean = False

                If RestrictionString.Length > 0 Then
                    strselect.Append(" WHERE ")
                    strselect.Append(RestrictionString.ToString)
                    hayWhere = True
                End If

                If ColumnCondstring.Length > 0 Then
                    If Not hayWhere Then
                        strselect.Append(" WHERE ")
                        hayWhere = True
                    Else
                        strselect.Append(" AND ")
                    End If
                    strselect.Append(ColumnCondstring.ToString)
                End If

                If FilterString.Length > 0 Then
                    If Not hayWhere Then
                        strselect.Append(" WHERE ")
                        hayWhere = True
                    Else
                        strselect.Append(" AND ")
                    End If
                    strselect.Append(FilterString.ToString)
                End If

                If dateDeclarationString.Length > 0 Then
                    'Si existe declaracion de variables de fecha las inserta al inicio del select
                    strselect.Insert(0, dateDeclarationString)
                End If

                strselect.Append(" " & Orderstring.ToString)

            End If

            Return True
        End Function



    End Class
End Namespace

