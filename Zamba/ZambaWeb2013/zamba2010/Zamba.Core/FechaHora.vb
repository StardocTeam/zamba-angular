Public Class FechaHora
    'Private FechaHora As DateTime
    Public Shared Function FechaHoraAFecha(ByVal Fecha As DateTime) As DateTime
        Return Date.Parse(Fecha.Day.ToString & "/" & Fecha.Month.ToString & "/" & Fecha.Year.ToString)
    End Function
    Public Shared Function compararFecha(ByVal Fecha1 As DateTime, ByVal Fecha2 As DateTime) As Int32
        Dim Dif As Int32
        Dif = (Fecha1.Year - Fecha2.Year) * 365
        Dif += (Fecha1.Month - Fecha2.Month) * 30
        Dif += Fecha1.Day - Fecha2.Day
        Return Dif
    End Function

End Class
