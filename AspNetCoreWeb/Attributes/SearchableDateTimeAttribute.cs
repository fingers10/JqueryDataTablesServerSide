using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableDateTimeAttribute : SearchableAttribute
    {
        public SearchableDateTimeAttribute()
        {
            ExpressionProvider = new DateTimeSearchExpressionProvider();
        }
    }
}
