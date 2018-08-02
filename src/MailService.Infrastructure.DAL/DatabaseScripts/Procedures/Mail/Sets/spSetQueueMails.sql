USE [MailService]
GO

IF OBJECT_ID('mail.spSetQueueMails') IS NOT NULL
	DROP PROCEDURE mail.spSetQueueMails
GO

CREATE PROCEDURE mail.spSetQueueMails
	@AIDs NVARCHAR(Max),
	@AIsQueue BIT

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @IDs NVARCHAR(Max) = @AIDs
		  , @IsQueue BIT = @AIsQueue
		  , @QueueDate SMALLDATETIME
		  , @Result INT = 0

		  set @QueueDate = case @IsQueue 
								when 0 then null 
								when 1 then GETDATE() 
								end
	BEGIN TRY
		BEGIN TRAN
			
			update mail.Mail 
			set IsQueue = @IsQueue
				, QueueDate = @QueueDate 
			from openjson(@IDs)
			where ID = value

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END