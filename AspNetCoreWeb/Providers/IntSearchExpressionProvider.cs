using System;
using System.Linq.Expressions;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public class IntSearchExpressionProvider:ComparableSearchExpressionProvider
    {
        public override ConstantExpression GetValue(string input)
        {
            if(!int.TryParse(input,out var value))
            {
                throw new ArgumentException("Invalid search value.");
            }

            return Expression.Constant(value);
        }
    }
}
