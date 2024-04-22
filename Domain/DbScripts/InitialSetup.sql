
--Create Database
IF NOT EXISTS (select * from sys.databases where name = 'Leaderboards')
BEGIN 
    CREATE DATABASE Leaderboards;
END

--Use recently created database
Go
USE Leaderboards

--Create Tables
IF NOT EXISTS (select object_id from sys.objects where object_id = OBJECT_ID(N'[dbo].[Users]'))
BEGIN
CREATE TABLE Users
(
	Id int not null identity(1,1) primary key,
	CreatedAt datetime not null default CURRENT_TIMESTAMP,
	UpdatedAt datetime,
	IsDeleted bit not null default 0,
	FirstName nvarchar(255) not null,
	LastName nvarchar(255) not null,
	UserName nvarchar(255) not null unique
)
END;

IF NOT EXISTS (select object_id from sys.objects where object_id = OBJECT_ID(N'[dbo].[UserScores]'))
BEGIN
CREATE TABLE UserScores
(
	Id int not null identity(1,1) primary key,
	CreatedAt datetime not null default CURRENT_TIMESTAMP,
	UpdatedAt datetime,
	IsDeleted bit not null default 0,
	UserId int not null,
	Date datetime not null,
	Score int not null default 0,
	constraint fk_UserScores_Users foreign key (UserId) references Users (Id)
)
END;

--Procedures
IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'GetScoresByDay'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.GetScoresByDay
GO

CREATE PROCEDURE GetScoresByDay @Date datetime
AS
WITH GroupedByDay AS (select s.UserName, dateadd(DAY,0, datediff(day,0, us.Date)) as 'day', sum(us.Score) as 'Score'
from UserScores us join Users s 
	on us.UserId = s.Id
where cast(us.Date as Date) = cast(@Date as date)
group by s.UserName, dateadd(DAY,0, datediff(day,0, us.Date)))
select s.Id as 'UserId', gpd.UserName, gpd.Score
from Users s join GroupedByDay gpd on s.UserName = gpd.UserName
order by gpd.Score desc
GO

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'GetScoresByMonth'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.GetScoresByMonth
GO

CREATE PROCEDURE GetScoresByMonth @Date datetime
AS
WITH GroupedByMonth AS (select s.UserName, dateadd(MONTH,0, datediff(MONTH,0, us.Date)) as 'Month', sum(us.Score) as 'Score'
from UserScores us join Users s 
	on us.UserId = s.Id
where month(us.Date) = month(@Date) and year(us.Date) = year(@Date)
group by s.UserName, dateadd(MONTH,0, datediff(MONTH,0, us.Date)))
select s.Id as 'UserId', gpm.UserName, gpm.Score
from Users s join GroupedByMonth gpm on s.UserName = gpm.UserName
order by gpm.Score desc
GO

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'GetUsersPlaceForCurrentMonth'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.GetUsersPlaceForCurrentMonth
GO

CREATE PROCEDURE GetUsersPlaceForCurrentMonth @UserId int
AS
WITH UsersOrderByScore AS (select ROW_NUMBER() over (order by sum(us.Score) desc) as 'Place', us.UserId, sum(us.Score) as 'Score'
from UserScores us
where month(us.Date) = month(GETDATE()) and year(us.Date) = year(GETDATE())
group by us.UserId)
select uobs.Place, uobs.Score
from UserScores us join UsersOrderByScore uobs on us.Id = uobs.UserId
where us.Id = @userId
GO


--Functions
IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[GetAverageDailyScore]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].GetAverageDailyScore

GO

CREATE FUNCTION GetAverageDailyScore() RETURNS INTEGER

AS
BEGIN
   DECLARE @averageDailyScore INTEGER

   set @averageDailyScore= (select avg(o.score) as Average
	from (select sum(us.Score) as 'Score'
	from UserScores us join Users s 
		on us.UserId = s.Id
	group by s.UserName, dateadd(DAY,0, datediff(day,0, us.Date))) as o)

   RETURN @averageDailyScore
END
Go

IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[GetAverageMonthlyScore]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].GetAverageMonthlyScore

GO

CREATE FUNCTION GetAverageMonthlyScore() RETURNS INTEGER

AS
BEGIN
   DECLARE @averageMonthlyScore INTEGER

   set @averageMonthlyScore= (select avg(o.score) as Average
	from (select sum(us.Score) as 'Score'
	from UserScores us join Users s 
		on us.UserId = s.Id
	group by s.UserName, dateadd(MONTH,0, datediff(MONTH,0, us.Date))) as o)

   RETURN @averageMonthlyScore
END
Go

IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[GetMaxDailyScore]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].GetMaxDailyScore

GO

CREATE FUNCTION GetMaxDailyScore() RETURNS INTEGER

AS
BEGIN
   DECLARE @maxDailyScore INTEGER

   set @maxDailyScore= (select max(o.score) as Average
	from (select max(us.Score) as 'Score'
	from UserScores us join Users s 
		on us.UserId = s.Id
	group by s.UserName, dateadd(DAY,0, datediff(day,0, us.Date))) as o)

   RETURN @maxDailyScore
END
Go


IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[GetMaxWeeklyScore]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].GetMaxWeeklyScore

GO

CREATE FUNCTION GetMaxWeeklyScore() RETURNS INTEGER

AS
BEGIN
   DECLARE @maxWeeklyScore INTEGER

   set @maxWeeklyScore= (select max(o.score) as Average
	from (select max(us.Score) as 'Score'
	from UserScores us join Users s 
		on us.UserId = s.Id
	group by s.UserName, dateadd(week,0, datediff(week,0, us.Date))) as o)

   RETURN @maxWeeklyScore
END
Go


IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[GetMaxMonthlyScore]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].GetMaxMonthlyScore

GO

CREATE FUNCTION GetMaxMonthlyScore() RETURNS INTEGER

AS
BEGIN
   DECLARE @maxMonthlyScore INTEGER

   set @maxMonthlyScore= (select max(o.score) as Average
	from (select max(us.Score) as 'Score'
	from UserScores us join Users s 
		on us.UserId = s.Id
	group by s.UserName, dateadd(MONTH,0, datediff(MONTH,0, us.Date))) as o)

   RETURN @maxMonthlyScore
END
Go

-- Max daily score will be max weekly and max monthly as well, so all those calculations are extra
--select dbo.GetAverageDailyScore() as 'AverageDailyScore',
-- dbo.GetAverageMonthlyScore() as 'AverageMonthlyScore',
-- dbo.GetMaxDailyScore() as 'MaxDailyScore',
-- dbo.GetMaxWeeklyScore() as 'MaxWeeklyScore',
-- dbo.GetMaxMonthlyScore() as 'MaxMonthlyScore'

