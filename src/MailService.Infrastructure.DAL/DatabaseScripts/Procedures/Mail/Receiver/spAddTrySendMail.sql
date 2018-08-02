USE [MailService]
GO

IF OBJECT_ID('mail.spAddTrySendMail') IS NOT NULL
	DROP PROCEDURE mail.spAddTrySendMail
GO

CREATE PROCEDURE mail.spAddTrySendMail
	@AMailID UNIQUEIDENTIFIER
	, @AMessage NVARCHAR(500)
	, @ASucceed BIT

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @MailID UNIQUEIDENTIFIER = @AMailID
			, @Message nvarchar(500) = @AMessage
			, @Succeed BIT = @ASucceed
			, @Result INT = 0

	BEGIN TRY
		BEGIN TRAN
			insert into mail.SendTry(ID, MailID, [Date], Succeed, [Message])
			values (NewID(), @MailID, GETDATE(), @Succeed, @Message)

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END