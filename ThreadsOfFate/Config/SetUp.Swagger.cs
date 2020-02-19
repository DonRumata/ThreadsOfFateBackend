using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using ThreadsOfFate.Extensions;
using ThreadsOfFate.Filters;

namespace ThreadsOfFate.Config
{
    public static partial class SetUp
    {
        /// <summary>
        /// Swagger
        /// </summary>
        public static void Swagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1.0", new Info
            {
                Version = "v1.0",
                Title = "Threads Of Fate Backend API",
                Description = "Описание базового API системы",
                Contact = new Contact
                {
                    Name = "",
                    Email = "",
                    Url = ""
                }
            });


            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            options.IncludeXmlComments(xmlFile);

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                // would mean this action is unversioned and should be included everywhere
                if (actionApiVersionModel == null)
                {
                    return true;
                }
                if (actionApiVersionModel.DeclaredApiVersions.Any())
                {
                    return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                }
                return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
            });

            options.OperationFilter<ApiVersionOperationFilter>();
        }
    }
}
