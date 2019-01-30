using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDecimalAttribute : SearchableAttribute
    {
        public SearchableDecimalAttribute()
        {
            ExpressionProvider = new DecimalSearchExpressionProvider();
        }
    }
}
