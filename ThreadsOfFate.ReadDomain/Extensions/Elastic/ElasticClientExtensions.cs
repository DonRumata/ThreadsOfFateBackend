using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ThreadsOfFate.ReadDomain.Extensions.Elastic
{
    static class ElasticClientExtensions
    {
        public static string RequestToJson(this IElasticClient client, SearchRequest request)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                client.RequestResponseSerializer.Serialize(request, stream);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
