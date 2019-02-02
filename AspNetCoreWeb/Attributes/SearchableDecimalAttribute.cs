using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
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
