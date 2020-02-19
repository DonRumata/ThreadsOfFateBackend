using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions
{
    interface IElasticHealthService
    {
        bool CanUseThemeScripts();
        void SuspendUseThemeScripts();
        bool HasShardScriptError(ShardStatistics shardStatistics);
    }
}
