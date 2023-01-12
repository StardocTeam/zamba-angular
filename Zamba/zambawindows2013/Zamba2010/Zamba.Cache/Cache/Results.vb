Namespace Cache

    Public Class Results

#Region "Chache decriptacion"
        Private Shared _hsCacheDecryptPassword As New Hashtable
        Private Shared _hsCacheEncryptedDocumets As New Hashtable

        Public Shared Property CacheDecryptPassword As Hashtable
            Get
                If _hsCacheDecryptPassword Is Nothing Then
                    _hsCacheDecryptPassword = New Hashtable()
                End If

                Return _hsCacheDecryptPassword
            End Get
            Set(ByVal value As Hashtable)
                _hsCacheDecryptPassword = value
            End Set
        End Property

        Public Shared Property CacheEncryptedDocumets As Hashtable
            Get
                If _hsCacheEncryptedDocumets Is Nothing Then
                    _hsCacheEncryptedDocumets = New Hashtable()
                End If

                Return _hsCacheEncryptedDocumets
            End Get
            Set(ByVal value As Hashtable)
                _hsCacheEncryptedDocumets = value
            End Set
        End Property
#End Region
        Public Shared Sub clearAll()
            Try
                _hsCacheDecryptPassword.Clear()
                _hsCacheEncryptedDocumets.Clear()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
      
    End Class

End Namespace