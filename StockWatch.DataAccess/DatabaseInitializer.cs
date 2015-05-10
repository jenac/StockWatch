using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace StockWatch.DataAccess
{
	public class DatabaseInitializer<T>: DropCreateDatabaseIfModelChanges<T> where T: DataContext
	{
		protected override void Seed(T context)
		{
			Logger.Instance.Info("Update Routines");
			UpdateStructureRoutines (context);
			//context.UpdateStructureRoutines();
			//context.UpdateProgramRoutines();
			Logger.Instance.Info("Update to base line");
			//context.UpdateToBaseLine();
			Logger.Instance.Info("Restore Data");
			//context.RestoreData();
		}

		private void UpdateStructureRoutines(T context)
		{
			List<string> tableStmts = new List<string> {
@"IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Indicator'))
CREATE TABLE [dbo].[Indicator](
	[Symbol] [nvarchar](16) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Data] [xml] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Symbol] ASC,
	[Name] ASC
))", 
@"IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Research'))
CREATE TABLE [dbo].[Research](
    [Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Data] [xml] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name]
))",
@"IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Stock'))
CREATE TABLE [dbo].[Stock](
    [Symbol] [nvarchar](16) NOT NULL,
	[InPossession] [bit] NOT NULL,
    [InIDB50] [bit] NOT NULL,
	[Data] [xml] NULL,
PRIMARY KEY CLUSTERED 
(
	[Symbol]
))",
@"IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'DailySummary'))
CREATE TABLE [dbo].[DailySummary](
    [Symbol] [nvarchar](16) NOT NULL,
	[Date] [datetime] NOT NULL,
    [Version] [nvarchar](64) NOT NULL,
	[Data] [xml] NULL,
PRIMARY KEY CLUSTERED 
(
	[Symbol] ASC,
    [Date] ASC
))",
@"IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'EmailArchive'))
CREATE TABLE [dbo].[EmailArchive](
    [Name] [nvarchar](255) NOT NULL,
	[DateSent] [datetime] NOT NULL,
    [Html] [text] NULL
PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
    [DateSent] ASC
))",

   
        };

			List<string> viewStmts = new List<string> {
@"CREATE VIEW [dbo].[Vw_Symbols]
AS
  SELECT Symbol, MAX([Date]) AS [Last] FROM Eod GROUP BY Symbol",

@"CREATE VIEW [dbo].[Vw_ComputedEod]
AS
  SELECT Symbol
	, [Date]
	, ([Close] - [Open]) AS [GL]
	FROM Eod",
			};

			List<string> procStmts = new List<string> {
@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_EodState_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_EodState_Get]",
@"CREATE PROCEDURE [dbo].[Proc_EodState_Get] 
AS 
BEGIN
  SELECT C.Symbol, VW.[Last] FROM [Vw_Symbols] VW 
    RIGHT JOIN Company C 
    ON VW.Symbol = C.Symbol
END",

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_IndicatorState_Get_FullScan]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_IndicatorState_Get_FullScan]",
@"CREATE PROCEDURE [dbo].[Proc_IndicatorState_Get_FullScan] (
	@Name NVARCHAR(50)
	)
AS 
BEGIN
	WITH CTE AS(
		SELECT Symbol, [Date] FROM Indicator 
			WHERE Name = @Name
	)
	SELECT Vw_Symbols.Symbol, Vw_Symbols.Last FROM CTE
		RIGHT JOIN Vw_Symbols
		ON CTE.Symbol = Vw_Symbols.Symbol 
		WHERE (Vw_Symbols.Last > CTE.Date OR CTE.Date IS NULL) 
END",

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_ComputedEod_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_ComputedEod_Get]",
@"CREATE PROCEDURE [dbo].[Proc_ComputedEod_Get] 
(
	@Symbol NVARCHAR(16) = NULL
)
AS 
BEGIN
	IF (@Symbol IS NULL)
    SELECT * FROM Vw_ComputedEod
  ELSE
    SELECT * FROM Vw_ComputedEod WHERE Symbol = @Symbol
END",

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_Indicator_Upsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_Indicator_Upsert]",
@"CREATE PROCEDURE [dbo].[Proc_Indicator_Upsert] 
(
	@Symbol NVARCHAR(16)
	, @Name NVARCHAR(50)
	, @Date DATETIME
	, @Data XML
)
AS 
BEGIN
	UPDATE [Indicator]
		SET [Date] = @Date,
		[Data] = @Data
		WHERE [Symbol] = @Symbol AND [Name] = @Name
	IF @@ROWCOUNT = 0 
	BEGIN
		INSERT INTO [Indicator] ([Symbol], [Name], [Date], [Data])
		VALUES (@Symbol, @Name, @Date, @Data)
	END;
END",

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_AllWatchedStocks_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_AllWatchedStocks_Get]",
@"CREATE PROCEDURE [dbo].[Proc_AllWatchedStocks_Get] 
AS 
BEGIN
	SELECT [Symbol], [InPossession], [InIDB50],	[Data] FROM dbo.Stock
END",

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_DailySummaryState_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_DailySummaryState_Get]",
@"CREATE PROCEDURE [dbo].[Proc_DailySummaryState_Get] 
AS 
BEGIN
	WITH CTE AS(
		SELECT Symbol, MAX([Date]) AS [Date] FROM DailySummary GROUP BY Symbol
	)
	SELECT Vw_Symbols.Symbol, Vw_Symbols.Last FROM CTE
		RIGHT JOIN Vw_Symbols
		ON CTE.Symbol = Vw_Symbols.Symbol 
		WHERE (Vw_Symbols.Last > CTE.Date OR CTE.Date IS NULL) 
END",

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_DailySummary_Upsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_DailySummary_Upsert]",
@"CREATE PROCEDURE [dbo].[Proc_DailySummary_Upsert] 
(
	@Symbol NVARCHAR(16)
    , @Date DATETIME
	, @Version NVARCHAR(64)
	, @Data XML
)
AS 
BEGIN
	UPDATE [DailySummary]
		SET [Version] = @Version,
		[Data] = @Data
		WHERE [Symbol] = @Symbol AND [Date] = @Date
	IF @@ROWCOUNT = 0 
	BEGIN
		INSERT INTO [DailySummary] ([Symbol], [Date], [Version], [Data])
		VALUES (@Symbol, @Date, @Version, @Data)
	END;
END",
@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_Eod_GetLast]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_Eod_GetLast]",
@"CREATE PROCEDURE [dbo].[Proc_Eod_GetLast] 
(
	@Symbol NVARCHAR(16)
)
AS 
BEGIN
	SELECT TOP 1 [Symbol]
      ,[Date]
      ,[Open]
      ,[High]
      ,[Low]
      ,[Close]
      ,[Volume]
  FROM [dbo].[Eod] 
    WHERE Symbol = @Symbol
    ORDER BY Date DESC
END",
@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_DailySummary_GetLast]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_DailySummary_GetLast]",
@"CREATE PROCEDURE [dbo].[Proc_DailySummary_GetLast] 
(
	@Symbol NVARCHAR(16)
)
AS 
BEGIN
	SELECT TOP 1 [Symbol]
      ,[Date]
      ,[Version]
      ,[Data]
  FROM [dbo].[DailySummary]
    WHERE Symbol = @Symbol
    ORDER BY Date DESC
END",    
@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_Indicator_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_Indicator_Get]",
@"CREATE PROCEDURE [dbo].[Proc_Indicator_Get] 
(
	@Symbol NVARCHAR(16)
    , @Name NVARCHAR(50)
	, @Date DATETIME
)
AS 
BEGIN
	SELECT [Symbol]
      ,[Name]
      ,[Date]
      ,[Data]
  FROM [dbo].[Indicator] 
    WHERE Symbol = @Symbol 
        AND [Name] = @Name
        AND [Date] = @Date
END",        
@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_DailySummary_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_DailySummary_Get]",
@"CREATE PROCEDURE [dbo].[Proc_DailySummary_Get] 
(
	@Symbol NVARCHAR(16)
	, @Date DATETIME
)
AS 
BEGIN
	SELECT [Symbol]
      ,[Date]
      ,[Version]
      ,[Data]
  FROM [dbo].[DailySummary]
    WHERE Symbol = @Symbol 
        AND [Date] = @Date
END",            

@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_EmailArchive_Get]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_EmailArchive_Get]",
@"CREATE PROCEDURE [dbo].[Proc_EmailArchive_Get] 
(
	@Name NVARCHAR(255)
	, @Date DATETIME
)
AS 
BEGIN
	SELECT [Name]
      ,[DateSent]
      ,[Html]
  FROM [dbo].[EmailArchive]
    WHERE [Name] = @Name
        AND [DateSent] = @Date
END",            
@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Proc_EmailArchive_Upsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE [dbo].[Proc_EmailArchive_Upsert]",
@"CREATE PROCEDURE [dbo].[Proc_EmailArchive_Upsert] 
(
	@Name NVARCHAR(255)
	, @Date DATETIME
    , @Html TEXT
)
AS 
BEGIN
    UPDATE [EmailArchive]
		SET [Html] = @Html
        WHERE [DateSent] = @Date AND [Name] = @Name
	IF @@ROWCOUNT = 0 
	BEGIN
		INSERT INTO [EmailArchive] ([Name], [DateSent], [Html])
		VALUES (@Name, @Date, @Html)
	END
END",            
    
    

			};

			foreach (string sql in tableStmts) {
				Logger.Instance.Info("Creating additional tables");
				try {
					context.Database.ExecuteSqlCommand (sql);
				}
				catch(Exception e) {
					Logger.Instance.ErrorFormat (
						"Error: {0}\r\nWhile executing: {1}",
						e.Message, sql);
				}
			}

			foreach (string sql in viewStmts) {
				Logger.Instance.Info("Creating views");
				try {
					context.Database.ExecuteSqlCommand (sql);
				}
				catch(Exception e) {
					Logger.Instance.ErrorFormat (
						"Error: {0}\r\nWhile executing: {1}",
						e.Message, sql);
				}
			}

			foreach (string sql in procStmts) {
				Logger.Instance.Info("Creating procs");
				try {
					context.Database.ExecuteSqlCommand (sql);
				}
				catch(Exception e) {
					Logger.Instance.ErrorFormat (
						"Error: {0}\r\nWhile executing: {1}",
						e.Message, sql);
				}
			}
		}
	}
}

