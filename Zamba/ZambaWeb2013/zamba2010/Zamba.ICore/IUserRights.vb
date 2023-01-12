Public Interface IUserRights
	Inherits ICore

	Property Type() As Usertypes
	Property PictureId() As Int32
	Property Password() As String
	Property State() As UserState
End Interface