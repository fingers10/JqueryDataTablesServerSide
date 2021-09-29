using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableBoolAttribute : SearchableAttribute
    {
        public SearchableBoolAttribute()
        {
            ExpressionProvider = new BoolSearchExpressionProvider();
        }
    }
}
