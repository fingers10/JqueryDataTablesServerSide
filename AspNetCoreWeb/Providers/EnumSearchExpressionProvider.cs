using System;
using System.Linq.Expressions;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public class EnumSearchExpressionProvider<TEnum> : EnumerationSearchExpressionProvider where TEnum : struct
    {
        public override ConstantExpression GetValue(string input)
        {
            //if (!Enum.TryParse<TEnum>(input.Trim().Replace(" ", string.Empty), true, out var value))
            //{
            //    throw new ArgumentException("Invalid Search value.");
            //}

            return Expression.Constant(input);
        }
    }
}
