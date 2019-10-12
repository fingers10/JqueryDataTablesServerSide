using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDoubleAttribute : SearchableAttribute
    {
        public SearchableDoubleAttribute()
        {
            ExpressionProvider = new DoubleSearchExpressionProvider();
        }
    }
}
