using JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public class EnumerationSearchExpressionProvider : DefaultSearchExpressionProvider
    {
        private static readonly MethodInfo _stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        protected const string ContainsOperator = "co";
        public override IEnumerable<string> GetOperators()
        {
            return base.GetOperators();
        }

        public override Expression GetComparison(MemberExpression left, string op, Expression right)
        {
            switch (op.ToLower())
            {
                case EqualsOperator: return Expression.Equal(left.CastToObjectAndString().TrimToLower(), right.TrimToLower());
                case ContainsOperator: return Expression.Call(left.CastToObjectAndString().TrimToLower(), _stringContainsMethod, right.TrimToLower());
                default: return base.GetComparison(left, op, right);
            }
        }
    }
}