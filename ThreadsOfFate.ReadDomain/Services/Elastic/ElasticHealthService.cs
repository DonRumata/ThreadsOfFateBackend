using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Nest;
using ThreadsofFate.Common.Const;
using ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions;

namespace ThreadsOfFate.ReadDomain.Services.Elastic
{
    class ElasticHealthService : IElasticHealthService
    {
        private const int StopUseThemeScriptsSecondsDefault = 60;
        private readonly int _stopUseThemeScriptsSeconds;
        private readonly object _useThemeScriptsProhibitedUntilLock;
        private DateTime _useThemeScriptsProhibitedUntil;

        public ElasticHealthService(IConfiguration configuration)
        {
            _useThemeScriptsProhibitedUntilLock = new object();
            _stopUseThemeScriptsSeconds = configuration.GetValue(AppSettingsConst.ElasticHealthServiceSettings.StopUseThemeScriptsSecondsDefault,
                StopUseThemeScriptsSecondsDefault);
            UseThemeScriptsProhibitedUntil = DateTime.MinValue;
        }

        private DateTime UseThemeScriptsProhibitedUntil
        {
            get
            {
                lock (_useThemeScriptsProhibitedUntilLock)
                    return _useThemeScriptsProhibitedUntil;
            }

            set
            {
                lock (_useThemeScriptsProhibitedUntilLock)
                    _useThemeScriptsProhibitedUntil = value;
            }
        }

        public bool CanUseThemeScripts()
        {
            return UseThemeScriptsProhibitedUntil < DateTime.Now;
        }

        public bool HasShardScriptError(ShardStatistics shardStatistics)
        {
            return shardStatistics.Failures.Any(f => f.Reason.Type == "script_exception");
        }

        public void SuspendUseThemeScripts()
        {
            UseThemeScriptsProhibitedUntil = DateTime.Now + TimeSpan.FromSeconds(_stopUseThemeScriptsSeconds);
        }
    }
}
