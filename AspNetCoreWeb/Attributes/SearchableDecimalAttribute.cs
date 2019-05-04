using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDecimalAttribute:SearchableAttribute
    {
        public SearchableDecimalAttribute()
        {
            ExpressionProvider = new DecimalSearchExpressionProvider();
        }
    }
}
