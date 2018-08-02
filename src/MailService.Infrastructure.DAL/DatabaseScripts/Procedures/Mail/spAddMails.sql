USE [MailService]
GO

IF OBJECT_ID('mail.spAddMails') IS NOT NULL
	DROP PROCEDURE mail.spAddMails
GO

CREATE PROCEDURE mail.spAddMails
	@AMessages nvarchar(max)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE 
			@Messages nvarchar(max) = @AMessages
		  , @Result INT = 0

	BEGIN TRY
		BEGIN TRAN

			INSERT INTO mail.Mail(ID
								, SourceAccountID
								, [Priority]
								, SendType
								, EncodingType
								, [Status]
								, [Content]
								, [Subject]
								, IsQueue
								, QueueDate
								, IsSent
								, SendDate)
			SELECT 
			mails.ID
			, mails.SourceAccountID
			, mails.[Priority]
			, mails.SendType
			, mails.EncodingType
			, mails.[Status]
			, mails.[Content]
			, mails.[Subject]
			, 0
			, null
			, 0
			, null
			FROM OPENJSON(@Messages)
			WITH (
				ID UNIQUEIDENTIFIER
				, SourceAccountID UNIQUEIDENTIFIER
				, [Priority] tinyint
				, SendType tinyint
				, EncodingType tinyint
				, [Status] smallint
				, [Content] nvarchar(MAX)
				, [Subject] nvarchar(500) 
			) as mails

			INSERT INTO mail.MailReceiver(ID, MailID, Email, Cc, Bcc)
			SELECT 
			NEWID()
			, null
			, null
			, null
			, null
			FROM OPENJSON(@Messages)
			WITH (
				ID UNIQUEIDENTIFIER
				,  ReceiverNumbers nvarchar(max) AS JSON
			) as recMsgs


			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END