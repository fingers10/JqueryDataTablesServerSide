using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableIntAttribute : SearchableAttribute
    {
        public SearchableIntAttribute()
        {
            ExpressionProvider = new IntSearchExpressionProvider();
        }
    }
}
