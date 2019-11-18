select max([HistoryID]), [ServerID], [ObjectStatusID], [DatabaseName], [ObjectName]
from [dbo].[SVC_ObjectHistory]
where ObjectStatusID = 1 --Check out for edit
and HistoryID in (select max([HistoryID])
                    from [dbo].[SVC_ObjectHistory]
                group by [DatabaseName], [ObjectName])

and ServerID in (select SS.ServerID
                   from SVC_LU_ServerAlias as SL INNER JOIN SVC_Server as SS ON SL.ServerAliasID = SS.ServerAliasID
				   where SL.ServerAliasDesc = 'BEAM Dev')
group by [ServerID], [ObjectStatusID], [DatabaseName], [ObjectName]
order by [DatabaseName], [ObjectName]

select max([HistoryID]), [ServerID], [ObjectStatusID], [DatabaseName], [ObjectName]
from [dbo].[SVC_ObjectHistory]
where ObjectStatusID = 2 --Released to UAT
and HistoryID in (select max([HistoryID])
                    from [dbo].[SVC_ObjectHistory]
                group by [DatabaseName], [ObjectName])

and ServerID in (select SS.ServerID
                   from SVC_LU_ServerAlias as SL INNER JOIN SVC_Server as SS ON SL.ServerAliasID = SS.ServerAliasID
				   where SL.ServerAliasDesc = 'BEAM UAT')
group by [ServerID], [ObjectStatusID], [DatabaseName], [ObjectName]
order by [DatabaseName], [ObjectName]

select max([HistoryID]), [ServerID], [ObjectStatusID], [DatabaseName], [ObjectName]
from [dbo].[SVC_ObjectHistory]
where ObjectStatusID = 7 --Released to Pre-prod
and HistoryID in (select max([HistoryID])
                    from [dbo].[SVC_ObjectHistory]
                group by [DatabaseName], [ObjectName])

and ServerID in (select SS.ServerID
                   from SVC_LU_ServerAlias as SL INNER JOIN SVC_Server as SS ON SL.ServerAliasID = SS.ServerAliasID
				   where SL.ServerAliasDesc = 'BEAM Pre Prod')
group by [ServerID], [ObjectStatusID], [DatabaseName], [ObjectName]
order by [DatabaseName], [ObjectName]