using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableShortAttribute:SearchableAttribute
    {
        public SearchableShortAttribute()
        {
            ExpressionProvider = new ShortSearchExpressionProvider();
        }
    }
}
