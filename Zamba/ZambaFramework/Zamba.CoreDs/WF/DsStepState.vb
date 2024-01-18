﻿'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Data
Imports System.Runtime.Serialization
Imports System.Xml


<Serializable(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Diagnostics.DebuggerStepThrough(),  _
 System.ComponentModel.ToolboxItem(true)>  _
Public Class DsStepState
    Inherits DataSet
    
    Private tableWFStepStates As WFStepStatesDataTable
    
    Public Sub New()
        MyBase.New
        Me.InitClass
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        RemoveHandler Me.Tables.CollectionChanged, schemaChangedHandler
        RemoveHandler Me.Relations.CollectionChanged, schemaChangedHandler
        AddHandler Me.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New
        Dim strSchema As String = CType(info.GetValue("XmlSchema", GetType(System.String)),String)
        If (Not (strSchema) Is Nothing) Then
            Dim ds As DataSet = New DataSet
            ds.ReadXmlSchema(New XmlTextReader(New System.IO.StringReader(strSchema)))
            If (Not (ds.Tables("WFStepStates")) Is Nothing) Then
                Me.Tables.Add(New WFStepStatesDataTable(ds.Tables("WFStepStates")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.InitClass
        End If
        Me.GetSerializationData(info, context)
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        RemoveHandler Me.Tables.CollectionChanged, schemaChangedHandler
        RemoveHandler Me.Relations.CollectionChanged, schemaChangedHandler
        AddHandler Me.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    <System.ComponentModel.Browsable(false),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)>  _
    Public ReadOnly Property WFStepStates As WFStepStatesDataTable
        Get
            Return Me.tableWFStepStates
        End Get
    End Property
    
    Public Overrides Function Clone() As DataSet
        Dim cln As DsStepState = CType(MyBase.Clone,DsStepState)
        cln.InitVars
        Return cln
    End Function
    
    Protected Overrides Function ShouldSerializeTables() As Boolean
        Return false
    End Function
    
    Protected Overrides Function ShouldSerializeRelations() As Boolean
        Return false
    End Function
    
    Protected Overrides Sub ReadXmlSerializable(ByVal reader As XmlReader)
        Me.Reset
        Dim ds As DataSet = New DataSet
        ds.ReadXml(reader)
        If (Not (ds.Tables("WFStepStates")) Is Nothing) Then
            Me.Tables.Add(New WFStepStatesDataTable(ds.Tables("WFStepStates")))
        End If
        Me.DataSetName = ds.DataSetName
        Me.Prefix = ds.Prefix
        Me.Namespace = ds.Namespace
        Me.Locale = ds.Locale
        Me.CaseSensitive = ds.CaseSensitive
        Me.EnforceConstraints = ds.EnforceConstraints
        Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
        Me.InitVars
    End Sub
    
    Protected Overrides Function GetSchemaSerializable() As System.Xml.Schema.XmlSchema
        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        Me.WriteXmlSchema(New XmlTextWriter(stream, Nothing))
        stream.Position = 0
        Return System.Xml.Schema.XmlSchema.Read(New XmlTextReader(stream), Nothing)
    End Function
    
    Friend Sub InitVars()
        Me.tableWFStepStates = CType(Me.Tables("WFStepStates"),WFStepStatesDataTable)
        If (Not (Me.tableWFStepStates) Is Nothing) Then
            Me.tableWFStepStates.InitVars
        End If
    End Sub
    
    Private Sub InitClass()
        Me.DataSetName = "DsStepState"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/DsStepState.xsd"
        Me.Locale = New System.Globalization.CultureInfo("en-US")
        Me.CaseSensitive = false
        Me.EnforceConstraints = true
        Me.tableWFStepStates = New WFStepStatesDataTable
        Me.Tables.Add(Me.tableWFStepStates)
    End Sub
    
    Private Function ShouldSerializeWFStepStates() As Boolean
        Return false
    End Function
    
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    Public Delegate Sub WFStepStatesRowChangeEventHandler(ByVal sender As Object, ByVal e As WFStepStatesRowChangeEvent)
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class WFStepStatesDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columnDoc_State_ID As DataColumn
        
        Private columnDescription As DataColumn
        
        Private columnStep_Id As DataColumn
        
        Private columnName As DataColumn
        
        Private columnInitial As DataColumn
        
        Friend Sub New()
            MyBase.New("WFStepStates")
            Me.InitClass
        End Sub
        
        Friend Sub New(ByVal table As DataTable)
            MyBase.New(table.TableName)
            If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                Me.CaseSensitive = table.CaseSensitive
            End If
            If (table.Locale.ToString <> table.DataSet.Locale.ToString) Then
                Me.Locale = table.Locale
            End If
            If (table.Namespace <> table.DataSet.Namespace) Then
                Me.Namespace = table.Namespace
            End If
            Me.Prefix = table.Prefix
            Me.MinimumCapacity = table.MinimumCapacity
            Me.DisplayExpression = table.DisplayExpression
        End Sub
        
        <System.ComponentModel.Browsable(false)>  _
        Public ReadOnly Property Count As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property
        
        Friend ReadOnly Property Doc_State_IDColumn As DataColumn
            Get
                Return Me.columnDoc_State_ID
            End Get
        End Property
        
        Friend ReadOnly Property DescriptionColumn As DataColumn
            Get
                Return Me.columnDescription
            End Get
        End Property
        
        Friend ReadOnly Property Step_IdColumn As DataColumn
            Get
                Return Me.columnStep_Id
            End Get
        End Property
        
        Friend ReadOnly Property NameColumn As DataColumn
            Get
                Return Me.columnName
            End Get
        End Property
        
        Friend ReadOnly Property InitialColumn As DataColumn
            Get
                Return Me.columnInitial
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As WFStepStatesRow
            Get
                Return CType(Me.Rows(index),WFStepStatesRow)
            End Get
        End Property
        
        Public Event WFStepStatesRowChanged As WFStepStatesRowChangeEventHandler
        
        Public Event WFStepStatesRowChanging As WFStepStatesRowChangeEventHandler
        
        Public Event WFStepStatesRowDeleted As WFStepStatesRowChangeEventHandler
        
        Public Event WFStepStatesRowDeleting As WFStepStatesRowChangeEventHandler
        
        Public Overloads Sub AddWFStepStatesRow(ByVal row As WFStepStatesRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddWFStepStatesRow(ByVal Doc_State_ID As Decimal, ByVal Description As String, ByVal Step_Id As Decimal, ByVal Name As String, ByVal Initial As Decimal) As WFStepStatesRow
            Dim rowWFStepStatesRow As WFStepStatesRow = CType(Me.NewRow,WFStepStatesRow)
            rowWFStepStatesRow.ItemArray = New Object() {Doc_State_ID, Description, Step_Id, Name, Initial}
            Me.Rows.Add(rowWFStepStatesRow)
            Return rowWFStepStatesRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As WFStepStatesDataTable = CType(MyBase.Clone,WFStepStatesDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New WFStepStatesDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columnDoc_State_ID = Me.Columns("Doc_State_ID")
            Me.columnDescription = Me.Columns("Description")
            Me.columnStep_Id = Me.Columns("Step_Id")
            Me.columnName = Me.Columns("Name")
            Me.columnInitial = Me.Columns("Initial")
        End Sub
        
        Private Sub InitClass()
            Me.columnDoc_State_ID = New DataColumn("Doc_State_ID", GetType(System.Decimal), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnDoc_State_ID)
            Me.columnDescription = New DataColumn("Description", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnDescription)
            Me.columnStep_Id = New DataColumn("Step_Id", GetType(System.Decimal), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnStep_Id)
            Me.columnName = New DataColumn("Name", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnName)
            Me.columnInitial = New DataColumn("Initial", GetType(System.Decimal), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnInitial)
        End Sub
        
        Public Function NewWFStepStatesRow() As WFStepStatesRow
            Return CType(Me.NewRow,WFStepStatesRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New WFStepStatesRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(WFStepStatesRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.WFStepStatesRowChangedEvent) Is Nothing) Then
                RaiseEvent WFStepStatesRowChanged(Me, New WFStepStatesRowChangeEvent(CType(e.Row,WFStepStatesRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.WFStepStatesRowChangingEvent) Is Nothing) Then
                RaiseEvent WFStepStatesRowChanging(Me, New WFStepStatesRowChangeEvent(CType(e.Row,WFStepStatesRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.WFStepStatesRowDeletedEvent) Is Nothing) Then
                RaiseEvent WFStepStatesRowDeleted(Me, New WFStepStatesRowChangeEvent(CType(e.Row,WFStepStatesRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.WFStepStatesRowDeletingEvent) Is Nothing) Then
                RaiseEvent WFStepStatesRowDeleting(Me, New WFStepStatesRowChangeEvent(CType(e.Row,WFStepStatesRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveWFStepStatesRow(ByVal row As WFStepStatesRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class WFStepStatesRow
        Inherits DataRow
        
        Private tableWFStepStates As WFStepStatesDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableWFStepStates = CType(Me.Table,WFStepStatesDataTable)
        End Sub
        
        Public Property Doc_State_ID As Decimal
            Get
                Try 
                    Return CType(Me(Me.tableWFStepStates.Doc_State_IDColumn),Decimal)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("No se puede obtener el valor porque es DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableWFStepStates.Doc_State_IDColumn) = value
            End Set
        End Property
        
        Public Property Description As String
            Get
                Try 
                    Return CType(Me(Me.tableWFStepStates.DescriptionColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("No se puede obtener el valor porque es DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableWFStepStates.DescriptionColumn) = value
            End Set
        End Property
        
        Public Property Step_Id As Decimal
            Get
                Try 
                    Return CType(Me(Me.tableWFStepStates.Step_IdColumn),Decimal)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("No se puede obtener el valor porque es DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableWFStepStates.Step_IdColumn) = value
            End Set
        End Property
        
        Public Property Name As String
            Get
                Try 
                    Return CType(Me(Me.tableWFStepStates.NameColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("No se puede obtener el valor porque es DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableWFStepStates.NameColumn) = value
            End Set
        End Property
        
        Public Property Initial As Decimal
            Get
                Try 
                    Return CType(Me(Me.tableWFStepStates.InitialColumn),Decimal)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("No se puede obtener el valor porque es DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableWFStepStates.InitialColumn) = value
            End Set
        End Property
        
        Public Function IsDoc_State_IDNull() As Boolean
            Return Me.IsNull(Me.tableWFStepStates.Doc_State_IDColumn)
        End Function
        
        Public Sub SetDoc_State_IDNull()
            Me(Me.tableWFStepStates.Doc_State_IDColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsDescriptionNull() As Boolean
            Return Me.IsNull(Me.tableWFStepStates.DescriptionColumn)
        End Function
        
        Public Sub SetDescriptionNull()
            Me(Me.tableWFStepStates.DescriptionColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsStep_IdNull() As Boolean
            Return Me.IsNull(Me.tableWFStepStates.Step_IdColumn)
        End Function
        
        Public Sub SetStep_IdNull()
            Me(Me.tableWFStepStates.Step_IdColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsNameNull() As Boolean
            Return Me.IsNull(Me.tableWFStepStates.NameColumn)
        End Function
        
        Public Sub SetNameNull()
            Me(Me.tableWFStepStates.NameColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsInitialNull() As Boolean
            Return Me.IsNull(Me.tableWFStepStates.InitialColumn)
        End Function
        
        Public Sub SetInitialNull()
            Me(Me.tableWFStepStates.InitialColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class WFStepStatesRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As WFStepStatesRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As WFStepStatesRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As WFStepStatesRow
            Get
                Return Me.eventRow
            End Get
        End Property
        
        Public ReadOnly Property Action As DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
End Class