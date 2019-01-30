using AspNetCoreWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AspNetCoreWeb.Providers
{
    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
    {
        protected const string EqualsOperator = "eq";

        public virtual IEnumerable<string> GetOperators()
        {
            yield return EqualsOperator;
        }

        public virtual ConstantExpression GetValue(string input)
            => Expression.Constant(input);

        public virtual Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            switch (op.ToLower())
            {
                case EqualsOperator: return Expression.Equal(left, right);
                default: throw new ArgumentException($"Invalid Operator '{op}'.");
            }
        }
    }
}
