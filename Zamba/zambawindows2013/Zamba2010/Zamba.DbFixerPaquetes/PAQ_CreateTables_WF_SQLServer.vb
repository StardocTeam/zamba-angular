Imports Zamba.Servers
Imports zamba.AppBlock
Public Class PAQ_CreateTables_WF_SQLServer
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea las tablas de Worflow para SQL SERVER"
        End Get
    End Property


    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder()
        Dim exBuilder As New System.Text.StringBuilder()
        Dim flagException As Boolean = False

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

                sql.Append("CREATE TABLE [WFContexts] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[WF_ID] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[V_Name] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[V_Type] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[V_Value] [varchar] (400) COLLATE Modern_Spanish_CI_AS NOT NULL")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE [WFDocument] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Doc_ID] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[DOC_TYPE_ID] [decimal](4, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Folder_ID] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[step_Id] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Do_State_ID] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Name] [varchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[IconId] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[CheckIn] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[User_Asigned] [decimal](10, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Exclusive] [decimal](18, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[ExpireDate] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[User_Asigned_By] [decimal](18, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Date_Asigned_By] [datetime] NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Task_ID] [decimal](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Task_State_ID] [decimal](18, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Remark] [int] NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Tag] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[work_id] [decimal](10, 0) NOT NULL")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WFContexts") Then
                        Throw New Exception(Me.name & ": la tabla WFContexts ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE [WFFaces] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Name] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE [WFRuleParamItems] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Rule_id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Item] [numeric](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Value] [varchar] (1000) COLLATE Modern_Spanish_CI_AS NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE [WFRules] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Name] [varchar] (50) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[step_Id] [numeric](10, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Type] [numeric](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[ParentId] [numeric](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[ParentType] [numeric](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Class] [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Enable] [numeric](10, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Version] [numeric](10, 0) NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WFFaces") Then
                        Throw New Exception(Me.name & ": la tabla WFFaces ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE [WFRulesHst] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Doc_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Doc_Type_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Folder_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Step_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Rule_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Result] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[User_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Ejecution_Date] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Data] [nvarchar] (400) COLLATE Modern_Spanish_CI_AS NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WFRulesHst") Then
                        Throw New Exception(Me.name & ": la tabla WFRulesHst ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE [WFStep] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[step_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[work_id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Name] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Description] [varchar] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Help] [varchar] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[CreateDate] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[ImageIndex] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[EditDate] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[LocationX] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[LocationY] [decimal](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[max_Hours] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[max_docs] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[StartAtOpenDoc] [numeric](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Color] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Width] [numeric](18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Height] [numeric](18, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WFStep") Then
                        Throw New Exception(Me.name & ": la tabla WFStep ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE [WFStepStates] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Doc_State_ID] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Description] [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Step_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Name] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Initial] [numeric](18, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WFStepStates") Then
                        Throw New Exception(Me.name & ": la tabla WFStepStates ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE [WFWorkflow] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[work_id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Wstat_id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Name] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Description] [varchar] (100) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Help] [varchar] (200) COLLATE Modern_Spanish_CI_AS NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[CreateDate] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[EditDate] [datetime] NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Refreshrate] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[InitialStepId] [numeric](10, 0) NOT NULL")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE [WF_DT] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[WFId] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[DocTypeId] [numeric](4, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WFWorkflow") Then
                        Throw New Exception(Me.name & ": la tabla WFWorkflow ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE [WF_Frms] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Step_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Form_Id] [numeric](10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	[Sort] [numeric](10, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON [PRIMARY]")
                Try
                    If ZPaq.ExisteTabla("WF_Frms") Then
                        Throw New Exception(Me.name & ": la tabla WF_Frms ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("ALTER TABLE [WFDocument] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFDocumentBKPMC] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Task_ID],[work_id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")
                Try
                    If Not ZPaq.ExisteTabla("WFDocument") Then
                        Throw New Exception(Me.name & ": la tabla WFDocument no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("ALTER TABLE [WFFaces] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFFaces] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")
                Try
                    If Not ZPaq.ExisteTabla("WFFaces") Then
                        Throw New Exception(Me.name & ": la tabla WFFaces no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("ALTER TABLE [WFRules] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFRules] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")
                Try
                    If Not ZPaq.ExisteTabla("WFRules") Then
                        Throw New Exception(Me.name & ": la tabla WFRules no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFStep] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [DF_WFStep_max_days] DEFAULT (7) FOR [max_Hours],")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [DF_WFStep_max_docs] DEFAULT (15) FOR [max_docs],")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [DF__WFStep__Color__7718ED77] DEFAULT ('') FOR [Color],")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [DF__WFStep__Width__780D11B0] DEFAULT (150) FOR [Width],")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [DF__WFStep__Height__790135E9] DEFAULT (50) FOR [Height],")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFStep] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[step_Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY]")

                Try

                    If Not ZPaq.ExisteTabla("WFStep") Then
                        Throw New Exception(Me.name & ": la tabla WFStep no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)


                sql.Append("ALTER TABLE [WFStepHst] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFStepHst] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Doc_Id],[Step_Id],[CheckIn])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")
                Try

                    If Not ZPaq.ExisteTabla("WFStepHst") Then
                        Throw New Exception(Me.name & ": la tabla WFStepHst no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFStepStates] ADD")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFStepStates] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("		([Doc_State_ID])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")

                Try

                    If Not ZPaq.ExisteTabla("WFStepStates") Then
                        Throw New Exception(Me.name & ": la tabla WFStepStates no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFWorkflow] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WFWorkflow] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[work_id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")
                Try

                    If Not ZPaq.ExisteTabla("WFWorkflow") Then
                        Throw New Exception(Me.name & ": la tabla WFWorkflow no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WF_DT] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WF_DT] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[WFId],[DocTypeId])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [IX_WF_DT] UNIQUE  NONCLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[WFId],[DocTypeId])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")
                Try

                    If Not ZPaq.ExisteTabla("WF_DT") Then
                        Throw New Exception(Me.name & ": la tabla WF_DT no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If


                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WF_Frms] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [PK_WF_Frms] PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(    [Form_Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [IX_WF_Frms] UNIQUE  NONCLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Step_Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON [PRIMARY] ")

                Try

                    If Not ZPaq.ExisteTabla("") Then
                        Throw New Exception(Me.name & ": la tabla WF_Frms no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFContexts] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WFContexts_WFWorkflow] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[WF_ID])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFWorkflow] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[work_id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                Try

                    If Not ZPaq.ExisteTabla("WFContexts") Then
                        Throw New Exception(Me.name & ": la tabla WFContexts no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFFaces] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WFFaces_WFWorkflow] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFWorkflow] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[work_id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                Try
                    If Not ZPaq.ExisteTabla("WFFaces") Then
                        Throw New Exception(Me.name & ": la tabla WFFaces no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If


                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFRuleParamItems] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WFRuleParamItems_WFRules] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Rule_id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFRules] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[Id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                Try
                    If Not ZPaq.ExisteTabla("WFRuleParamItems") Then
                        Throw New Exception(Me.name & ": la tabla WFRuleParamItems no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFRules] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WFRules_WFStep] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[step_Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFStep] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[step_Id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")


                Try

                    If Not ZPaq.ExisteTabla("WFRules") Then
                        Throw New Exception(Me.name & ": la tabla WFRules no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFStep] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WFStep_WFWorkflow] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[work_id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFWorkflow] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[work_id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")


                Try
                    If Not ZPaq.ExisteTabla("WFStep") Then
                        Throw New Exception(Me.name & ": la tabla WFStep no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WFStepStates] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WFStepStates_WFStep] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Step_Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFStep] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[step_Id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE ")

                Try
                    If Not ZPaq.ExisteTabla("WFStepStates") Then
                        Throw New Exception(Me.name & ": la tabla WFStepStates no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)


                sql.Append("ALTER TABLE [WF_DT] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WF_DT_DOC_TYPE] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[DocTypeId])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [DOC_TYPE] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[DOC_TYPE_ID]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WF_DT_WFWorkflow] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[WFId])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFWorkflow] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[work_id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")

                Try
                    If Not ZPaq.ExisteTabla("WF_DT") Then
                        Throw New Exception(Me.name & ": la tabla WF_DT no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE [WF_Frms] ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WF_Frms_WFStep] FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		[Step_Id])")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES [WFStep] (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		[step_Id]")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                sql.Append(ControlChars.NewLine)
                Try
                    If Not ZPaq.ExisteTabla("WF_Frms") Then
                        Throw New Exception(Me.name & ": la tabla WF_Frms no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

            Else 'Oracle case

                sql.Append("CREATE TABLE WFContexts (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 WF_ID   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 V_Name   NVARCHAR2  (50)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 V_Type   NVARCHAR2  (50)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 V_Value   NVARCHAR2  (400)   NOT NULL")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE  WFDocument  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Doc_ID   FLOAT (10) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 DOC_TYPE_ID   FLOAT (4) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Folder_ID   FLOAT (10) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 step_Id   FLOAT (10) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Do_State_ID   FLOAT (10)  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Name   NVARCHAR2  (250)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 IconId   FLOAT (10) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 CheckIn   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 User_Asigned   FLOAT (10) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Exclusive   FLOAT (18) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 ExpireDATE   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 User_Asigned_By   FLOAT (18) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 DATE_Asigned_By   DATE  NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Task_ID   FLOAT (18) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Task_State_ID   FLOAT (18) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Remark   int  NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Tag   NVARCHAR2  (250)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 work_id   FLOAT (10) NOT NULL")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WFContexts") Then
                        Throw New Exception(Me.name & ": la tabla WFContexts ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE  WFFaces  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Name   NVARCHAR2  (50)   NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE  WFRuleParamItems  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Rule_id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Item   NUMBER (18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Value   NVARCHAR2  (1000)   NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE  WFRules  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Name   NVARCHAR2  (50)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 step_Id   NUMBER (10, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Type   NUMBER (18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 ParentId   NUMBER (18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 ParentType   NUMBER (18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Class   NVARCHAR2  (50)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Enable   NUMBER (10, 0) NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Version   NUMBER (10, 0) NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WFFaces") Then
                        Throw New Exception(Me.name & ": la tabla WFFaces ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE  WFRulesHst  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Doc_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Doc_Type_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Folder_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Step_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Rule_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Result   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 User_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Ejecution_DATE   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Data   NVARCHAR2  (400)   NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WFRulesHst") Then
                        Throw New Exception(Me.name & ": la tabla WFRulesHst ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE  WFStep  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 step_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 work_id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Name   NVARCHAR2  (50)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Description   NVARCHAR2  (100)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Help   NVARCHAR2  (100)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 CreateDATE   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 ImageIndex   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 EditDATE   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 LocationX   FLOAT (10)  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 LocationY   FLOAT (10) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 max_Hours   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 max_docs   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 StartAtOpenDoc   NUMBER (18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Color   NVARCHAR2  (50)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Width   NUMBER (18, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Height   NUMBER (18, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WFStep") Then
                        Throw New Exception(Me.name & ": la tabla WFStep ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE  WFStepStates  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Doc_State_ID   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Description   NVARCHAR2  (250)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Step_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Name   NVARCHAR2  (50)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Initial   NUMBER (18, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WFStepStates") Then
                        Throw New Exception(Me.name & ": la tabla WFStepStates ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE  WFWorkflow  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 work_id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Wstat_id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Name   NVARCHAR2  (50)   NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Description   NVARCHAR2  (100)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Help   NVARCHAR2  (200)   NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 CreateDATE   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 EditDATE   DATE  NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Refreshrate   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 InitialStepId   NUMBER (10, 0) NOT NULL")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                sql.Append(ControlChars.NewLine)
                sql.Append(ControlChars.NewLine)
                sql.Append("CREATE TABLE  WF_DT  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 WFId   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 DocTypeId   NUMBER (4, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WFWorkflow") Then
                        Throw New Exception(Me.name & ": la tabla WFWorkflow ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("CREATE TABLE  WF_Frms  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Step_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Form_Id   NUMBER (10, 0) NOT NULL ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 Sort   NUMBER (10, 0) NOT NULL ")
                sql.Append(ControlChars.NewLine)
                sql.Append(") ON  PRIMARY ")
                Try
                    If ZPaq.ExisteTabla("WF_Frms") Then
                        Throw New Exception(Me.name & ": la tabla WF_Frms ya existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("ALTER TABLE  WFDocument  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFDocumentBKPMC  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Task_ID , work_id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")
                Try
                    If Not ZPaq.ExisteTabla("WFDocument") Then
                        Throw New Exception(Me.name & ": la tabla WFDocument no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("ALTER TABLE  WFFaces  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFFaces  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")
                Try
                    If Not ZPaq.ExisteTabla("WFFaces") Then
                        Throw New Exception(Me.name & ": la tabla WFFaces no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)
                sql.Append("ALTER TABLE  WFRules  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFRules  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")
                Try
                    If Not ZPaq.ExisteTabla("WFRules") Then
                        Throw New Exception(Me.name & ": la tabla WFRules no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFStep  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  DF_WFStep_max_days  DEFAULT (7) FOR  max_Hours ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  DF_WFStep_max_docs  DEFAULT (15) FOR  max_docs ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  DF__WFStep__Color__7718ED77  DEFAULT ('') FOR  Color ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  DF__WFStep__Width__780D11B0  DEFAULT (150) FOR  Width ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  DF__WFStep__Height__790135E9  DEFAULT (50) FOR  Height ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFStep  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 step_Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY ")

                Try

                    If Not ZPaq.ExisteTabla("WFStep") Then
                        Throw New Exception(Me.name & ": la tabla WFStep no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)


                sql.Append("ALTER TABLE  WFStepHst  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFStepHst  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Doc_Id , Step_Id , CheckIn )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")
                Try

                    If Not ZPaq.ExisteTabla("WFStepHst") Then
                        Throw New Exception(Me.name & ": la tabla WFStepHst no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFStepStates  ADD")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFStepStates  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("		( Doc_State_ID )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")

                Try

                    If Not ZPaq.ExisteTabla("WFStepStates") Then
                        Throw New Exception(Me.name & ": la tabla WFStepStates no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFWorkflow  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WFWorkflow  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 work_id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")
                Try

                    If Not ZPaq.ExisteTabla("WFWorkflow") Then
                        Throw New Exception(Me.name & ": la tabla WFWorkflow no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WF_DT  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WF_DT  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 WFId , DocTypeId )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  IX_WF_DT  UNIQUE  NONCLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 WFId , DocTypeId )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")
                Try

                    If Not ZPaq.ExisteTabla("WF_DT") Then
                        Throw New Exception(Me.name & ": la tabla WF_DT no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If


                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WF_Frms  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  PK_WF_Frms  PRIMARY KEY  CLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(     Form_Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  IX_WF_Frms  UNIQUE  NONCLUSTERED ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Step_Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	  ON  PRIMARY  ")

                Try

                    If Not ZPaq.ExisteTabla("") Then
                        Throw New Exception(Me.name & ": la tabla WF_Frms no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFContexts  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WFContexts_WFWorkflow  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 WF_ID )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFWorkflow  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 work_id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                Try

                    If Not ZPaq.ExisteTabla("WFContexts") Then
                        Throw New Exception(Me.name & ": la tabla WFContexts no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFFaces  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WFFaces_WFWorkflow  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFWorkflow  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 work_id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                Try
                    If Not ZPaq.ExisteTabla("WFFaces") Then
                        Throw New Exception(Me.name & ": la tabla WFFaces no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If


                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFRuleParamItems  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WFRuleParamItems_WFRules  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Rule_id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFRules  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 Id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                Try
                    If Not ZPaq.ExisteTabla("WFRuleParamItems") Then
                        Throw New Exception(Me.name & ": la tabla WFRuleParamItems no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFRules  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WFRules_WFStep  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 step_Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFStep  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 step_Id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")


                Try

                    If Not ZPaq.ExisteTabla("WFRules") Then
                        Throw New Exception(Me.name & ": la tabla WFRules no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFStep  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WFStep_WFWorkflow  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 work_id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFWorkflow  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 work_id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")


                Try
                    If Not ZPaq.ExisteTabla("WFStep") Then
                        Throw New Exception(Me.name & ": la tabla WFStep no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WFStepStates  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WFStepStates_WFStep  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Step_Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFStep  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 step_Id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE ")

                Try
                    If Not ZPaq.ExisteTabla("WFStepStates") Then
                        Throw New Exception(Me.name & ": la tabla WFStepStates no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try

                sql.Remove(0, sql.Length)


                sql.Append("ALTER TABLE  WF_DT  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WF_DT_DOC_TYPE  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 DocTypeId )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  DOC_TYPE  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 DOC_TYPE_ID ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ,")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WF_DT_WFWorkflow  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 WFId )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFWorkflow  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 work_id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")

                Try
                    If Not ZPaq.ExisteTabla("WF_DT") Then
                        Throw New Exception(Me.name & ": la tabla WF_DT no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)

                sql.Append("ALTER TABLE  WF_Frms  ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT  FK_WF_Frms_WFStep  FOREIGN KEY ")
                sql.Append(ControlChars.NewLine)
                sql.Append("(		 Step_Id )")
                sql.Append(ControlChars.NewLine)
                sql.Append("	 REFERENCES  WFStep  (")
                sql.Append(ControlChars.NewLine)
                sql.Append("		 step_Id ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	) ON DELETE CASCADE  ON UPDATE CASCADE ")
                sql.Append(ControlChars.NewLine)
                Try
                    If Not ZPaq.ExisteTabla("WF_Frms") Then
                        Throw New Exception(Me.name & ": la tabla WF_Frms no existe en la base de datos.")
                    End If
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Catch ex As Exception
                    exBuilder.AppendLine(sql.ToString())
                    exBuilder.Append("Error: ")
                    flagException = True
                    exBuilder.AppendLine(ex.ToString())
                    exBuilder.AppendLine()
                End Try
                sql.Remove(0, sql.Length)
            End If

            If flagexception Then
                Throw New Exception(Me.name & exBuilder.ToString())
            End If

            Return True

    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTablesWF_SQLServer"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTables_WF_SQLServer
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("04/12/06")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 26
        End Get
    End Property
End Class
