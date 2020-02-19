using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsofFate.Common.Const
{
    public static class AppSettingsConst
    {
        public static class ConnectionStrings
        {
            public const string ThreadsOfFate = "ConnectionStrings:ThreadsOfFate";
        }

        public const string AspNetAppDevEnvirontment = "Development";
        public const string AspNetAppTestEnvirontment = "test";
        public const string AspNetAppStableEnvirontment = "stable";

        public const string AccessSettingsSectionName = "AccessVariables";

        public static class SqlCommandsTimeouts
        {
            public const int ThreadsOfFateDefault = 30;
            public const string ThreadsOfFate = "SqlCommandsTimeouts:ThreadsOfFate";
        }

        /// <summary>
        /// Содержит наименование DomainSettings в Environment Variables
        /// </summary>
        public static class ApplicationDomainSettings
        {
            public const string CurrentDomainName = "ASPNETCORE_DOMAIN";
        }

        /// <summary>
        /// Содержит наименование EnvironmentSettings в Environement Variables
        /// </summary>
        public static class ApplicationEviromentSettings
        {
            public const string CurrentEnviromentName = "ASPNETCORE_ENVIRONMENT";
        }

        public static class ElasticHealthServiceSettings
        {
            public const string StopUseThemeScriptsSecondsDefault = "ElasticHealthServiceSettings:StopUseThemeScriptsForPeriodSec";
        }
    }
}
