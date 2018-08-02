USE [MailService]
GO

IF OBJECT_ID('mail.spGetMails') IS NOT NULL
	DROP PROCEDURE mail.spGetMails
GO

CREATE PROCEDURE mail.spGetMails
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
	ORDER BY mail.SendDate DESC, mail.SourceAccountID

	SET @Result = @@ROWCOUNT

	RETURN @Result 
END