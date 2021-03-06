USE [AdventureWorks]
GO
/****** Object:  StoredProcedure [dbo].[GetProductList]    Script Date: 15.12.2020 12:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	ALTER PROCEDURE [dbo].[GetProductList] AS
BEGIN
	SELECT TOP 20
					ProductId AS ID,
					Name AS ProductName,
					ProductNumber,
					ReorderPoint,
					SellStartDate,
					ModifiedDate
	FROM			Production.Product
END;