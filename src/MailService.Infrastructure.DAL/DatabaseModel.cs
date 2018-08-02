using System;
using DatabaseModel;
using System.Threading.Tasks;

namespace MailService.Infrastructure.DAL
{
class PBL: Database
{
#region Constructors
public PBL(string connectionString)
	:base(connectionString){}

public PBL(string connectionString, IModelValueBinder modelValueBinder)
	:base(connectionString, modelValueBinder){}
#endregion

#region ModifyAccount

public System.Data.SqlClient.SqlCommand GetCommand_ModifyAccount(string _email, bool? _enabled, string _host, Guid? _id, bool? _isNewRecord, string _password, int? _port, bool? _sSL, string _title)
{
return base.CreateCommand("pbl.spModifyAccount", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AEmail", IsOutput = false, Value = string.IsNullOrWhiteSpace(_email) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_email) }, 
					new Parameter { Name = "@AEnabled", IsOutput = false, Value = _enabled == null ? DBNull.Value : (object)_enabled }, 
					new Parameter { Name = "@AHost", IsOutput = false, Value = string.IsNullOrWhiteSpace(_host) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_host) }, 
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIsNewRecord", IsOutput = false, Value = _isNewRecord == null ? DBNull.Value : (object)_isNewRecord }, 
					new Parameter { Name = "@APassword", IsOutput = false, Value = string.IsNullOrWhiteSpace(_password) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_password) }, 
					new Parameter { Name = "@APort", IsOutput = false, Value = _port == null ? DBNull.Value : (object)_port }, 
					new Parameter { Name = "@ASSL", IsOutput = false, Value = _sSL == null ? DBNull.Value : (object)_sSL }, 
					new Parameter { Name = "@ATitle", IsOutput = false, Value = string.IsNullOrWhiteSpace(_title) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_title) }, 
	});

}

public async Task<ResultSet> ModifyAccountAsync(string _email, bool? _enabled, string _host, Guid? _id, bool? _isNewRecord, string _password, int? _port, bool? _sSL, string _title)
{
	using(var cmd = GetCommand_ModifyAccount(_email, _enabled, _host, _id, _isNewRecord, _password, _port, _sSL, _title))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet ModifyAccount(string _email, bool? _enabled, string _host, Guid? _id, bool? _isNewRecord, string _password, int? _port, bool? _sSL, string _title)
{
	using(var cmd = GetCommand_ModifyAccount(_email, _enabled, _host, _id, _isNewRecord, _password, _port, _sSL, _title))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetAccounts

public System.Data.SqlClient.SqlCommand GetCommand_GetAccounts(bool? _enabled)
{
return base.CreateCommand("pbl.spGetAccounts", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AEnabled", IsOutput = false, Value = _enabled == null ? DBNull.Value : (object)_enabled }, 
	});

}

public async Task<ResultSet> GetAccountsAsync(bool? _enabled)
{
	using(var cmd = GetCommand_GetAccounts(_enabled))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetAccounts(bool? _enabled)
{
	using(var cmd = GetCommand_GetAccounts(_enabled))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetAccount

public System.Data.SqlClient.SqlCommand GetCommand_GetAccount(Guid? _id)
{
return base.CreateCommand("pbl.spGetAccount", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
	});

}

public async Task<ResultSet> GetAccountAsync(Guid? _id)
{
	using(var cmd = GetCommand_GetAccount(_id))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetAccount(Guid? _id)
{
	using(var cmd = GetCommand_GetAccount(_id))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region DeleteAccount

public System.Data.SqlClient.SqlCommand GetCommand_DeleteAccount(Guid? _id)
{
return base.CreateCommand("pbl.spDeleteAccount", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
	});

}

public async Task<ResultSet> DeleteAccountAsync(Guid? _id)
{
	using(var cmd = GetCommand_DeleteAccount(_id))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet DeleteAccount(Guid? _id)
{
	using(var cmd = GetCommand_DeleteAccount(_id))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetConfig

public System.Data.SqlClient.SqlCommand GetCommand_SetConfig(string _data)
{
return base.CreateCommand("pbl.spSetConfig", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AData", IsOutput = false, Value = string.IsNullOrWhiteSpace(_data) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_data) }, 
	});

}

public async Task<ResultSet> SetConfigAsync(string _data)
{
	using(var cmd = GetCommand_SetConfig(_data))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetConfig(string _data)
{
	using(var cmd = GetCommand_SetConfig(_data))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetConfig

public System.Data.SqlClient.SqlCommand GetCommand_GetConfig(string _name)
{
return base.CreateCommand("pbl.spGetConfig", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AName", IsOutput = false, Value = string.IsNullOrWhiteSpace(_name) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_name) }, 
	});

}

public async Task<ResultSet> GetConfigAsync(string _name)
{
	using(var cmd = GetCommand_GetConfig(_name))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetConfig(string _name)
{
	using(var cmd = GetCommand_GetConfig(_name))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region DeleteConfig

public System.Data.SqlClient.SqlCommand GetCommand_DeleteConfig(Guid? _id, string _iDs)
{
return base.CreateCommand("pbl.spDeleteConfig", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIDs", IsOutput = false, Value = string.IsNullOrWhiteSpace(_iDs) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_iDs) }, 
	});

}

public async Task<ResultSet> DeleteConfigAsync(Guid? _id, string _iDs)
{
	using(var cmd = GetCommand_DeleteConfig(_id, _iDs))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet DeleteConfig(Guid? _id, string _iDs)
{
	using(var cmd = GetCommand_DeleteConfig(_id, _iDs))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

}

class MAIL: Database
{
#region Constructors
public MAIL(string connectionString)
	:base(connectionString){}

public MAIL(string connectionString, IModelValueBinder modelValueBinder)
	:base(connectionString, modelValueBinder){}
#endregion

#region GetUnQueueMailReceivers

public System.Data.SqlClient.SqlCommand GetCommand_GetUnQueueMailReceivers()
{
return base.CreateCommand("mail.spGetUnQueueMailReceivers", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

}

public async Task<ResultSet> GetUnQueueMailReceiversAsync()
{
	using(var cmd = GetCommand_GetUnQueueMailReceivers())
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetUnQueueMailReceivers()
{
	using(var cmd = GetCommand_GetUnQueueMailReceivers())
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetMailReceivers

public System.Data.SqlClient.SqlCommand GetCommand_GetMailReceivers(Guid? _id)
{
return base.CreateCommand("mail.spGetMailReceivers", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
	});

}

public async Task<ResultSet> GetMailReceiversAsync(Guid? _id)
{
	using(var cmd = GetCommand_GetMailReceivers(_id))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetMailReceivers(Guid? _id)
{
	using(var cmd = GetCommand_GetMailReceivers(_id))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddTrySendMail

public System.Data.SqlClient.SqlCommand GetCommand_AddTrySendMail(Guid? _mailID, string _message, bool? _succeed)
{
return base.CreateCommand("mail.spAddTrySendMail", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AMailID", IsOutput = false, Value = _mailID == null ? DBNull.Value : (object)_mailID }, 
					new Parameter { Name = "@AMessage", IsOutput = false, Value = string.IsNullOrWhiteSpace(_message) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_message) }, 
					new Parameter { Name = "@ASucceed", IsOutput = false, Value = _succeed == null ? DBNull.Value : (object)_succeed }, 
	});

}

public async Task<ResultSet> AddTrySendMailAsync(Guid? _mailID, string _message, bool? _succeed)
{
	using(var cmd = GetCommand_AddTrySendMail(_mailID, _message, _succeed))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet AddTrySendMail(Guid? _mailID, string _message, bool? _succeed)
{
	using(var cmd = GetCommand_AddTrySendMail(_mailID, _message, _succeed))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetSendMail

public System.Data.SqlClient.SqlCommand GetCommand_SetSendMail(Guid? _id, bool? _isSent, Guid? _mailID, string _message)
{
return base.CreateCommand("mail.spSetSendMail", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIsSent", IsOutput = false, Value = _isSent == null ? DBNull.Value : (object)_isSent }, 
					new Parameter { Name = "@AMailID", IsOutput = false, Value = _mailID == null ? DBNull.Value : (object)_mailID }, 
					new Parameter { Name = "@AMessage", IsOutput = false, Value = string.IsNullOrWhiteSpace(_message) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_message) }, 
	});

}

public async Task<ResultSet> SetSendMailAsync(Guid? _id, bool? _isSent, Guid? _mailID, string _message)
{
	using(var cmd = GetCommand_SetSendMail(_id, _isSent, _mailID, _message))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetSendMail(Guid? _id, bool? _isSent, Guid? _mailID, string _message)
{
	using(var cmd = GetCommand_SetSendMail(_id, _isSent, _mailID, _message))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetQueueMails

public System.Data.SqlClient.SqlCommand GetCommand_SetQueueMails(string _iDs, bool? _isQueue)
{
return base.CreateCommand("mail.spSetQueueMails", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AIDs", IsOutput = false, Value = string.IsNullOrWhiteSpace(_iDs) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_iDs) }, 
					new Parameter { Name = "@AIsQueue", IsOutput = false, Value = _isQueue == null ? DBNull.Value : (object)_isQueue }, 
	});

}

public async Task<ResultSet> SetQueueMailsAsync(string _iDs, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMails(_iDs, _isQueue))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetQueueMails(string _iDs, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMails(_iDs, _isQueue))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region SetQueueMail

public System.Data.SqlClient.SqlCommand GetCommand_SetQueueMail(Guid? _id, bool? _isQueue)
{
return base.CreateCommand("mail.spSetQueueMail", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AIsQueue", IsOutput = false, Value = _isQueue == null ? DBNull.Value : (object)_isQueue }, 
	});

}

public async Task<ResultSet> SetQueueMailAsync(Guid? _id, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMail(_id, _isQueue))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet SetQueueMail(Guid? _id, bool? _isQueue)
{
	using(var cmd = GetCommand_SetQueueMail(_id, _isQueue))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetUnQueueMails

public System.Data.SqlClient.SqlCommand GetCommand_GetUnQueueMails()
{
return base.CreateCommand("mail.spGetUnQueueMails", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

}

public async Task<ResultSet> GetUnQueueMailsAsync()
{
	using(var cmd = GetCommand_GetUnQueueMails())
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetUnQueueMails()
{
	using(var cmd = GetCommand_GetUnQueueMails())
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetMails

public System.Data.SqlClient.SqlCommand GetCommand_GetMails()
{
return base.CreateCommand("mail.spGetMails", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

}

public async Task<ResultSet> GetMailsAsync()
{
	using(var cmd = GetCommand_GetMails())
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet GetMails()
{
	using(var cmd = GetCommand_GetMails())
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region DeleteMail

public System.Data.SqlClient.SqlCommand GetCommand_DeleteMail(string _iDS)
{
return base.CreateCommand("mail.spDeleteMail", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AIDS", IsOutput = false, Value = string.IsNullOrWhiteSpace(_iDS) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_iDS) }, 
	});

}

public async Task<ResultSet> DeleteMailAsync(string _iDS)
{
	using(var cmd = GetCommand_DeleteMail(_iDS))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet DeleteMail(string _iDS)
{
	using(var cmd = GetCommand_DeleteMail(_iDS))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddMails

public System.Data.SqlClient.SqlCommand GetCommand_AddMails(string _messages)
{
return base.CreateCommand("mail.spAddMails", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AMessages", IsOutput = false, Value = string.IsNullOrWhiteSpace(_messages) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_messages) }, 
	});

}

public async Task<ResultSet> AddMailsAsync(string _messages)
{
	using(var cmd = GetCommand_AddMails(_messages))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet AddMails(string _messages)
{
	using(var cmd = GetCommand_AddMails(_messages))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddMail

public System.Data.SqlClient.SqlCommand GetCommand_AddMail(string _content, byte? _encodingType, Guid? _id, string _mailReveivers, byte? _priority, byte? _sendType, Guid? _sourceAccountID, short? _status, string _subject)
{
return base.CreateCommand("mail.spAddMail", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AContent", IsOutput = false, Value = string.IsNullOrWhiteSpace(_content) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_content) }, 
					new Parameter { Name = "@AEncodingType", IsOutput = false, Value = _encodingType == null ? DBNull.Value : (object)_encodingType }, 
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@AMailReveivers", IsOutput = false, Value = string.IsNullOrWhiteSpace(_mailReveivers) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_mailReveivers) }, 
					new Parameter { Name = "@APriority", IsOutput = false, Value = _priority == null ? DBNull.Value : (object)_priority }, 
					new Parameter { Name = "@ASendType", IsOutput = false, Value = _sendType == null ? DBNull.Value : (object)_sendType }, 
					new Parameter { Name = "@ASourceAccountID", IsOutput = false, Value = _sourceAccountID == null ? DBNull.Value : (object)_sourceAccountID }, 
					new Parameter { Name = "@AStatus", IsOutput = false, Value = _status == null ? DBNull.Value : (object)_status }, 
					new Parameter { Name = "@ASubject", IsOutput = false, Value = string.IsNullOrWhiteSpace(_subject) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_subject) }, 
	});

}

public async Task<ResultSet> AddMailAsync(string _content, byte? _encodingType, Guid? _id, string _mailReveivers, byte? _priority, byte? _sendType, Guid? _sourceAccountID, short? _status, string _subject)
{
	using(var cmd = GetCommand_AddMail(_content, _encodingType, _id, _mailReveivers, _priority, _sendType, _sourceAccountID, _status, _subject))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public ResultSet AddMail(string _content, byte? _encodingType, Guid? _id, string _mailReveivers, byte? _priority, byte? _sendType, Guid? _sourceAccountID, short? _status, string _subject)
{
	using(var cmd = GetCommand_AddMail(_content, _encodingType, _id, _mailReveivers, _priority, _sendType, _sourceAccountID, _status, _subject))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

}

}

