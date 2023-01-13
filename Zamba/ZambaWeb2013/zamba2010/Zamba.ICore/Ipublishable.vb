Public Interface Ipublishable
    Inherits IResult

    Property PublishId() As Int32
    Property DocId() As Int64
    Property Publisher() As IUser
    Property PublishDate() As Date
End Interface
