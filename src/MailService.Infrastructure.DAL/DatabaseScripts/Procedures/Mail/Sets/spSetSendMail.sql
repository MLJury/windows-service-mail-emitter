USE [MailService]
GO

IF OBJECT_ID('mail.spSetSendMail') IS NOT NULL
	DROP PROCEDURE mail.spSetSendMail
GO

CREATE PROCEDURE mail.spSetSendMail
	@AID UNIQUEIDENTIFIER
	, @AMailID UNIQUEIDENTIFIER
	, @AIsSent BIT
	, @AMessage NVARCHAR(500)

WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @MailID UNIQUEIDENTIFIER = @AMailID
		  , @IsSent BIT = @AIsSent
		  , @Message NVARCHAR(500) = @AMessage
		  , @Result INT = 0 
		  , @SendDate DATETIME

		  set @SendDate = case @IsSent 
								when 0 then null 
								when 1 then GETDATE() 
								end 
	BEGIN TRY
		BEGIN TRAN
			
			update mail.Mail 
			set IsSent = @IsSent
				, SendDate = @SendDate  
			where ID = @MailID

			insert into mail.SendTry (ID, MailID, [Date], Succeed, [Message])
			values(@ID, @MailID, GETDATE(), @IsSent, @Message)

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END