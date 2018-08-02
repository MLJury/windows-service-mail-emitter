USE [MailService]
GO

IF OBJECT_ID('mail.spAddMail') IS NOT NULL
	DROP PROCEDURE mail.spAddMail
GO

CREATE PROCEDURE mail.spAddMail
	@AID UNIQUEIDENTIFIER
  , @ASourceAccountID UNIQUEIDENTIFIER
  , @APriority TINYINT
  , @ASendType TINYINT
  , @AEncodingType TINYINT
  , @AStatus SMALLINT
  , @AContent NVARCHAR(MAX)
  , @ASubject nvarchar(500)
  , @AMailReveivers NVARCHAR(max)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID UNIQUEIDENTIFIER = @AID
		  , @SourceAccountID UNIQUEIDENTIFIER = @ASourceAccountID
		  , @Priority TINYINT = ISNULL(@APriority, 1)
		  , @SendType TINYINT = ISNULL(@ASendType, 0)
		  , @EncodingType TINYINT = ISNULL(@AEncodingType, 0)
		  , @Status SMALLINT = ISNULL(@AStatus, 0)
		  , @Content NVARCHAR(MAX) = LTRIM(RTRIM(@AContent))
		  , @Subject nvarchar(500) = @ASubject
		  , @MailReveivers NVARCHAR(max) = @AMailReveivers
		  , @Result INT = 0

	BEGIN TRY
		BEGIN TRAN

			INSERT INTO mail.Mail
			(ID, SourceAccountID, [Priority], SendType, EncodingType, [Status], Content, [Subject])
			VALUES
			(@ID, @SourceAccountID, @Priority, @SendType, @EncodingType, @Status, @Content, @Subject)

			INSERT INTO mail.MailReceiver (ID, MailID, Email, Cc, Bcc)
			SELECT 
			NEWID()
			, @ID
			, receivers.Email
			, receivers.CC
			, receivers.Bcc
			FROM OPENJSON(@MailReveivers)
			with(
				Email varchar(256)
				, CC varchar(500)
				, Bcc varchar(500)
			) as receivers;

			SET @Result = @@ROWCOUNT
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @Result 
END