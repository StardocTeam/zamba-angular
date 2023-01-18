Imports System.Runtime.Serialization
Imports System.IO

Public Class ZClone

    ''' <summary>
    ''' Metodo el cual clona un objecto
    ''' </summary>
    ''' <param name="_item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History>
    '''        [Ezequiel] - 23/11/09 Created.
    ''' </History>

    Public Shared Function CloneObject(ByVal _item As Object) As Object
        Try
            Dim bFormatter As New Formatters.Binary.BinaryFormatter()
            Dim stream As New MemoryStream()
            bFormatter.Serialize(stream, _item)
            stream.Flush()
            stream.Position = 0
            Return bFormatter.Deserialize(stream)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

End Class
