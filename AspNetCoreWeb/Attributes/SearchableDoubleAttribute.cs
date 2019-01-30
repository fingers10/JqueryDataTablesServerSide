using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDoubleAttribute : SearchableAttribute
    {
        public SearchableDoubleAttribute()
        {
            ExpressionProvider = new DoubleSearchExpressionProvider();
        }
    }
}
