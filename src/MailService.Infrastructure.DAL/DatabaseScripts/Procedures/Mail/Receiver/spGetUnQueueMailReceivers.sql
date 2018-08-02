USE [MailService]
GO

IF OBJECT_ID('mail.spGetUnQueueMailReceivers') IS NOT NULL
	DROP PROCEDURE mail.spGetUnQueueMailReceivers
GO

CREATE PROCEDURE mail.spGetUnQueueMailReceivers
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @Result INT = 0

	SELECT mailRec.ID
		 , mailRec.Email ReceiverEmail
		 , mailRec.Bcc
		 , mailRec.Cc 
		 , mail.ID MailID
		 , mail.SourceAccountID
		 , mail.[Priority]
		 , mail.SendType
		 , mail.EncodingType
		 , mail.[Status]
		 , mail.SendDate
		 , mail.Content
		 , mail.[Subject]
		 , mail.IsQueue
		 , mail.IsSent
		 , account.[Type] SourceAccount
		 , account.ID SourceAccountID
		 , account.Email SourceEmail
	FROM mail.MailReceiver mailRec
	INNER JOIN mail.Mail mail ON mail.ID = mailRec.MailID
	INNER JOIN pbl.Account account ON mail.SourceAccountID = account.ID
	WHERE mail.IsQueue is null
	ORDER BY mail.SendDate DESC, mail.SourceAccountID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END