using JqueryDataTables.ServerSide.AspNetCoreWeb.Providers;
using System;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableEnumAttribute : SearchableAttribute
    {
        public SearchableEnumAttribute(Type enumType)
        {
            ExpressionProvider = (EnumerationSearchExpressionProvider)Activator.CreateInstance(
                typeof(EnumSearchExpressionProvider<>).MakeGenericType(enumType));
        }
    }
}
