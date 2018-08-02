using System;
using System.Collections.Generic;

namespace MailService.ApiClient
{
    class MailServiceClient : Library.ApiClient.Client, Interface.IMailServiceClient
    {
        public MailServiceClient(AppCore.IObjectSerializer objectSerializer, string host)
            : base(objectSerializer, host)
        {
            _host = host;
        }

        public MailServiceClient(AppCore.IObjectSerializer objectSerializer, string host, Func<IDictionary<string, string>> defaultHeaders)
            : base(objectSerializer, host, defaultHeaders)
        {
            _host = host;
        }

        readonly string _host;

        public string Host => _host;
    }
}
