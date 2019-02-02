using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
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
