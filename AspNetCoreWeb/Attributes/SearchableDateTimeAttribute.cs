using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDateTimeAttribute:SearchableAttribute
    {
        public SearchableDateTimeAttribute()
        {
            ExpressionProvider = new DateTimeSearchExpressionProvider();
        }
    }
}
