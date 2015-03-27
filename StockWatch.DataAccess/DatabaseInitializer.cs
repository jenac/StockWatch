using System;
using System.Data.Entity;
using StockWatch.Utility;
using System.Collections.Generic;

namespace LHM.StockWatch.DataAccess
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
@"CREATE TABLE [dbo].[Indicator](
	[Symbol] [nvarchar](16) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Data] [xml] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Symbol] ASC,
	[Name] ASC
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

