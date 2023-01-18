Friend Class StateSingle
    Inherits AbstractState

    Public Sub New(ByVal contex As AbstractPersistImage)
        MyBase.New(contex)
        'Verifica si el estado es para una sola imagen
        'If m_contex.m_CountImg > 1 Then
        '    m_contex.m_State = New StateMulti(m_contex)
        'End If
    End Sub

    Friend Overrides Sub Save(ByVal img As System.Drawing.Image)
        If m_contex.m_CountImg > 1 Then
            m_contex.m_State = New StateMulti(m_contex)
            m_contex.m_State.Save(img)
        ElseIf m_contex.m_CountImg = 1 Then
            '-----------
            'Save
            m_contex.m_image = DirectCast(img.Clone, Image)
            m_contex.m_image.Save(m_contex.PathDestino)
            '----
        Else
            Throw New ArgumentException("No imagenes para guardar.")
        End If
    End Sub
End Class