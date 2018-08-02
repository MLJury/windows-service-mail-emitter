USE [MailService]
GO

IF OBJECT_ID('mail.spGetUnQueueMails') IS NOT NULL
	DROP PROCEDURE mail.spGetUnQueueMails
GO

CREATE PROCEDURE mail.spGetUnQueueMails
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT, XACT_ABORT ON;

	DECLARE @Result INT = 0

	SELECT mail.ID
		 , mail.ID MailID
		 , mail.SourceAccountID
		 , account.[Type] SourceAccount
		 , account.ID SourceAccountID
		 , account.Email SourceEmail
		 , mail.[Priority]
		 , mail.SendType
		 , mail.EncodingType
		 , mail.[Status]
		 , mail.SendDate
		 , mail.Content
		 , mail.[Subject]
		 , mail.IsQueue
		 , mail.QueueDate
		 , mail.IsSent
		 , mail.SendDate
	FROM mail.Mail mail
	INNER JOIN pbl.Account account ON mail.SourceAccountID = account.ID
	WHERE mail.IsQueue is null
	ORDER BY mail.SendDate DESC, mail.SourceAccountID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END