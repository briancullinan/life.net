--select Concat(DATEPART(Year, [Id]),'-',DATEPART(Month, [Id]),'-',[From]) as [Date],count(*) from TrillianMessage group by Concat(DATEPART(Year, [Id]),'-',DATEPART(Month, [Id]),'-',[From]) order by [Date]
--select Concat(DATEPART(Year, [Id]),'-',DATEPART(Month, [Id])) as [Date],count(*) as [Count] from TrillianMessage group by Concat(DATEPART(Year, [Id]),'-',DATEPART(Month, [Id])) order by [Count]
select [From],count(*) as [Count] from TrillianMessage group by [From] order by [Count]
select count(*) from TrillianMessage
--select * from TrillianMessage where DATEPART(Month, [Id]) = 7 and DATEPART(Year, [Id]) = 2002
-- select Concat(DATEPART(Year, [Id]),'-',DATEPART(Month, [Id])) as [Date],* from TrillianMessage where [From] like '%prettyeuro%'
--delete from TrillianSession
--select * from TrillianMessage where [From] = '860220036'
--select * from TrillianMessage where [from] like '%epiccorpserver%'
select * from TrillianMessage where [Message] like '%youtube%'