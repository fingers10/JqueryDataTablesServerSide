using JqueryDataTables.ServerSide.AspNetCoreWeb.Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.DependencyInjection
{
    public class ConfigureJqueryDataTablesOptions : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new JqueryDataTablesBinderProvider());
        }
    }
}
