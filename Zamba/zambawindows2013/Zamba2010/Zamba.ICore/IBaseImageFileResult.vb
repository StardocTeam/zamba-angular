Public Interface IBaseImageFileResult
	Inherits IFileResult

	Property Disk_Group_Id() As Int32
	Property Doc_File() As String
	Property OffSet() As Int32
	Property DISK_VOL_PATH() As String
    Property File() As String
    ReadOnly Property FileName() As String

End Interface