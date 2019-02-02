using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
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
