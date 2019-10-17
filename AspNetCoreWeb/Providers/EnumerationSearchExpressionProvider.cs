using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public class EnumerationSearchExpressionProvider : DefaultSearchExpressionProvider
    {
        private static readonly MethodInfo ToStringMethod = typeof(Enum)
            .GetMethods()
            .First(x => x.Name == "ToString" && x.GetParameters().Length == 0);

        private static readonly MethodInfo StringEqualsMethod = typeof(string)
            .GetMethods()
            .First(x => x.Name == "Equals" && x.GetParameters().Length == 2);

        private static ConstantExpression IgnoreCase
            => Expression.Constant(StringComparison.OrdinalIgnoreCase);

        public override IEnumerable<string> GetOperators()
        {
            return base.GetOperators();
        }

        public override Expression GetComparison(MemberExpression left, string op, Expression right)
        {
            switch (op.ToLower())
            {
                case EqualsOperator: return Expression.Call(Expression.Call(left, ToStringMethod), StringEqualsMethod, Expression.Call(right, ToStringMethod), IgnoreCase);
                default: return base.GetComparison(left, op, right);
            }
        }
    }
}