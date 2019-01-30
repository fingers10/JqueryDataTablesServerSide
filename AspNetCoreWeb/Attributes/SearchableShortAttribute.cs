using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableShortAttribute : SearchableAttribute
    {
        public SearchableShortAttribute()
        {
            ExpressionProvider = new ShortSearchExpressionProvider();
        }
    }
}
