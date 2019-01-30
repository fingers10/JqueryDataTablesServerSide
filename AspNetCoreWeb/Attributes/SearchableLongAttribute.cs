using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableLongAttribute : SearchableAttribute
    {
        public SearchableLongAttribute()
        {
            ExpressionProvider = new LongSearchExpressionProvider();
        }
    }
}
