//using JqueryDataTables.ServerSide.AspNetCoreWeb.Binders;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Newtonsoft.Json.Serialization;

//namespace JqueryDataTables.ServerSide.AspNetCoreWeb.DependencyInjection
//{
//    public static class ServiceExtensions
//    {
//        public static IServiceCollection AddJqueryDataTables(this IServiceCollection services)
//        {

//            services.TryAddSingleton<IHttpContextAccessor,HttpContextAccessor>();

//            services
//                .AddMvcCore(options => options.ModelBinderProviders.Insert(0,new JqueryDataTablesBinderProvider()))
//                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

//            return services;
//        }
//    }
//}
