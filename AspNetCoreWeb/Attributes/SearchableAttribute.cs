using AspNetCoreWeb.Contracts;
using AspNetCoreWeb.Providers;
using System;

namespace AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableAttribute : Attribute
    {
        public string EntityProperty { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }
            = new DefaultSearchExpressionProvider();
    }
}
