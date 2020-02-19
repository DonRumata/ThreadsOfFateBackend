using System;
using System.Collections.Generic;
using System.Text;
using Nest;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;

namespace ThreadsOfFate.ReadDomain.Contexts
{
    class ElasticSearchClient : ElasticClient, IElasticSearchClient
    {
        public ElasticSearchClient(string connectionString) : base(
            new ConnectionSettings(new Uri(connectionString))
                .DisableDirectStreaming()
                //.OnRequestCompleted(details => WriteDebugInfoOnRequestCompleted(details))
                .PrettyJson())
        {
        }
    }
}
