using System;

namespace MailService.Core
{
    public interface IRequestInfo
    {
        Guid UserId { get; }

        Guid PositionId { get; }

        string Username { get; }

        string RemoteIP { get; }
    }
}
