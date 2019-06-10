using JqueryDataTables.ServerSide.AspNetCoreWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public class DefaultSearchExpressionProvider:ISearchExpressionProvider
    {
        protected const string EqualsOperator = "eq";

        public virtual IEnumerable<string> GetOperators()
        {
            yield return EqualsOperator;
        }

        public virtual ConstantExpression GetValue(string input)
        {
            return Expression.Constant(input);
        }

        public virtual Expression GetComparison(MemberExpression left,string op,Expression right)
        {
            switch(op.ToLower())
            {
            case EqualsOperator: return Expression.Equal(left,right);
            default: throw new ArgumentException($"Invalid Operator '{op}'.");
            }
        }
    }
}
