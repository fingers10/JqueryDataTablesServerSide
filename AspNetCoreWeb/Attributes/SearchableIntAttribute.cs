using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableIntAttribute:SearchableAttribute
    {
        public SearchableIntAttribute()
        {
            ExpressionProvider = new IntSearchExpressionProvider();
        }
    }
}
