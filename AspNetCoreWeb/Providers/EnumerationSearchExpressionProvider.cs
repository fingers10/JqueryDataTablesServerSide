using System.Collections.Generic;
using System.Linq.Expressions;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public class EnumerationSearchExpressionProvider : DefaultSearchExpressionProvider
    {
        public override IEnumerable<string> GetOperators()
        {
            return base.GetOperators();
        }

        public override Expression GetComparison(MemberExpression left, string op, Expression right)
        {
            switch (op.ToLower())
            {
                case EqualsOperator: return Expression.Equal(left, right);
                default: return base.GetComparison(left, op, right);
            }
        }
    }
}