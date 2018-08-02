USE [MailService]
GO

IF OBJECT_ID('pbl.spGetAccount') IS NOT NULL
	DROP PROCEDURE pbl.spGetAccount
GO

CREATE PROCEDURE pbl.spGetAccount
	@AID UNIQUEIDENTIFIER
WITH ENCRYPTION 
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @Result INT = 0

		SELECT ID
			 , Title
			 , Email
			 , [Password]
			 , [Enabled]
			 , [SSL]
			 , Port
			 , Host
			 , [Type]
		FROM pbl.Account
		WHERE ID = @ID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END