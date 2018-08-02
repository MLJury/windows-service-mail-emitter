USE [MailService]
GO

IF OBJECT_ID('pbl.spModifyAccount') IS NOT NULL
	DROP PROCEDURE pbl.spModifyAccount
GO

CREATE PROCEDURE pbl.spModifyAccount
	@AIsNewRecord BIT
  , @AID UNIQUEIDENTIFIER
  , @ATitle nvarchar(256)
  , @AEmail varchar(50)
  , @APassword varchar(256)	
  , @AEnabled bit
  , @ASSL bit
  , @APort int
  , @AHost nvarchar(50)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @IsNewRecord BIT = ISNULL(@AIsNewRecord, 0)
		  , @ID UNIQUEIDENTIFIER = @AID
		  , @Title nvarchar(256) = @ATitle
		  , @Email varchar(50) = @AEmail
		  , @Password varchar(256) = @APassword	
		  , @Enabled bit = @AEnabled
		  , @SSL bit = @ASSL
		  , @Port int = @APort
		  , @Host nvarchar(50) = @AHost
		  , @Result INT = 0

	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1
				INSERT INTO pbl.Account
				(ID, Title, Email, [Password], [Enabled], [SSL], [Port], Host)
				VALUES
				(@ID, @Title, @Email, @Password, @Enabled, @SSL, @Port, @Host)
			ELSE
				UPDATE pbl.Account
				SET Title = @Title
				, Email = @Email
				, [Password] = @Password
				, [Enabled] = @Enabled
				, [SSL] = @SSL
				, [Port] = @Port
				, Host = @Host
				WHERE ID = @ID

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END