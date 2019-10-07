using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Binders
{
    public class JqueryDataTablesBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var contextAccessor = (HttpContextAccessor)context.Services.GetService(typeof(IHttpContextAccessor));
            var httpContext = contextAccessor.HttpContext;

            if (httpContext.Request.Method.Equals("POST"))
            {
                return null;
            }

            return context.Metadata.ModelType == typeof(JqueryDataTablesParameters) ? new JqueryDataTablesBinder() : null;
        }
    }
}