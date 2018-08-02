USE [MailService]
GO

IF OBJECT_ID('mail.spGetMailReceivers') IS NOT NULL
	DROP PROCEDURE mail.spGetMailReceivers
GO

CREATE PROCEDURE mail.spGetMailReceivers
	@AID uniqueidentifier
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @ID uniqueidentifier = @AID
			, @Result INT = 0

	SELECT mailRec.ID
		 , mailRec.Email
		 , mailRec.Bcc
		 , mailRec.Cc 
	FROM mail.MailReceiver mailRec
	WHERE (MailID = @ID)
	ORDER BY mailrec.Email DESC 

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END