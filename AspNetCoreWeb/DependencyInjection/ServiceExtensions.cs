using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IMvcBuilder AddJqueryDataTables(this IMvcBuilder options)
        {
            options.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            options.Services.ConfigureOptions<ConfigureJqueryDataTablesOptions>();
            //options.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            return options;
        }
    }
}
