-- =============================================
-- Author:		Alex
-- Create date: 2015-12-09
-- Description:	
-- =============================================
--CREATE FUNCTION [dbo].[GetContactFullName]
--(
--	@ContactId INT
--)
--RETURNS VARCHAR(250)
--AS
--BEGIN
--	DECLARE @Name VARCHAR(250)

--	SELECT TOP 1 @Name = ISNULL([c].Surname,'') + ' ' + ISNULL([c].Name,'')
--	FROM dbo.ContactInfo as [c] 
--	WHERE [c].Id = @ContactId

--	RETURN @Name

--END
