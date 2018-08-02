USE [MailService]
GO

IF OBJECT_ID('pbl.spGetAccounts') IS NOT NULL
	DROP PROCEDURE pbl.spGetAccounts
GO

CREATE PROCEDURE pbl.spGetAccounts
	@AEnabled bit
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @Enabled bit = @AEnabled
			, @Result INT = 0
	
		SELECT ID
			 , Title
			 , Email
			 , [Password]
			 , [Enabled]
			 , [SSL]
			 , [Port]
			 , Host
		FROM pbl.Account
		WHERE IsRemoved is null
			  AND [Enabled] = @Enabled

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END