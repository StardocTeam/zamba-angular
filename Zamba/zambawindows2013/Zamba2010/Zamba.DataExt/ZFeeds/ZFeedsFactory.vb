Imports Zamba.Servers
Imports Zamba.Core


Public Class ZFeedsFactory
    Public Sub SaveConfiguration(ByVal refreshInterval As Integer, ByVal linesCount As Integer, ByVal feedCount As Integer, ByVal cleanInterval As Integer)
        Try
            Server.Con.ExecuteDataset(CommandType.Text, String.Format("if exists (select * from zopt where item = 'FeedRefreshInterval' )
            begin
            update zopt set value = {0} where item = 'FeedRefreshInterval'
            end
            else
            begin
            insert into zopt values ('FeedRefreshInterval',{0})
            end

            if exists (select * from zopt where item = 'FeedLinesCount' )
            begin
            update zopt set value = {1} where item = 'FeedLinesCount'
            end
            else
            begin
            insert into zopt values ('FeedLinesCount',{1})
            end

            if exists (select * from zopt where item = 'FeedCount' )
            begin
            update zopt set value = {2} where item = 'FeedCount'
            end
            else
            begin
            insert into zopt values ('FeedCount',{2})
            end

            if exists (select * from zopt where item = 'ZFeedCleanInterval' )
            begin
            update zopt set value = {3} where item = 'ZFeedCleanInterval'
            end
            else
            begin
            insert into zopt values ('ZFeedCleanInterval',{3})
            end", refreshInterval, linesCount, feedCount, cleanInterval))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Function LoadConfiguration() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "select * from zopt where item in ('FeedCount','FeedLinesCount','FeedRefreshInterval','ZFeedCleanInterval')").Tables(0)
    End Function
End Class
