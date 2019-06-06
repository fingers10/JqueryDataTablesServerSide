using JqueryDataTables.ServerSide.AspNetCoreWeb.Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.DependencyInjection
{
    public class ConfigureJqueryDataTablesOptions:IConfigureOptions<MvcOptions>, IConfigureOptions<MvcJsonOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0,new JqueryDataTablesBinderProvider());
        }

        public void Configure(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }
    }
}
