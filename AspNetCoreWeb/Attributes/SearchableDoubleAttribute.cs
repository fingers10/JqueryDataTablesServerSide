using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDoubleAttribute:SearchableAttribute
    {
        public SearchableDoubleAttribute()
        {
            ExpressionProvider = new DoubleSearchExpressionProvider();
        }
    }
}
