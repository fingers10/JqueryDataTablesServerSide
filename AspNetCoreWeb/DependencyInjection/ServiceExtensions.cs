using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IMvcBuilder AddJqueryDataTables(this IMvcBuilder builder)
        {
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            builder.Services.ConfigureOptions<ConfigureJqueryDataTablesOptions>();

            return builder;
        }
    }
}
