using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
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
