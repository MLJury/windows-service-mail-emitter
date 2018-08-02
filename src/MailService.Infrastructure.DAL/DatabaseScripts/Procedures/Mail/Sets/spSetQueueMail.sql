USE [MailService]
GO

IF OBJECT_ID('mail.spSetQueueMail') IS NOT NULL
	DROP PROCEDURE mail.spSetQueueMail
GO

CREATE PROCEDURE mail.spSetQueueMail
	@AID UNIQUEIDENTIFIER,
	@AIsQueue BIT

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @IsQueue BIT = @AIsQueue
		  , @Result INT = 0

	BEGIN TRY
		BEGIN TRAN
			
			update mail.Mail
			set IsQueue = @IsQueue
				, QueueDate = case @IsQueue 
								when 0 then null 
								when 1 then GETDATE() 
								end  
			where ID = @ID
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END