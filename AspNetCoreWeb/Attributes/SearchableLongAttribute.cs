using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableLongAttribute:SearchableAttribute
    {
        public SearchableLongAttribute()
        {
            ExpressionProvider = new LongSearchExpressionProvider();
        }
    }
}
