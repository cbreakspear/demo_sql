IF OBJECTPROPERTY(object_id('production.sp_SelectProducts'), N'IsProcedure') = 1
DROP PROCEDURE [production].[sp_SelectProducts]
GO
CREATE PROCEDURE sp_SelectProducts @product nchar(30)
AS
SELECT * FROM production.products WHERE product_id=@product
GO

