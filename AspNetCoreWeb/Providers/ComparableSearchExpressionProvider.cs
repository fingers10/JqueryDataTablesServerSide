using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Providers
{
    public abstract class ComparableSearchExpressionProvider : DefaultSearchExpressionProvider
    {
        private const string GreaterThanOperator = "gt";
        private const string GreaterThanEqualToOperator = "gte";
        private const string LessThanOperator = "lt";
        private const string LessThanEqualToOperator = "lte";

        public override IEnumerable<string> GetOperators()
        {
            return base.GetOperators()
                       .Concat(
                        new[]
                        {
                GreaterThanOperator,
                GreaterThanEqualToOperator,
                LessThanOperator,
                LessThanEqualToOperator
                        });
        }

        public override Expression GetComparison(MemberExpression left, string op, Expression right)
        {
            return (op.ToLower()) switch
            {
                GreaterThanOperator => Expression.GreaterThan(left, right),
                GreaterThanEqualToOperator => Expression.GreaterThanOrEqual(left, right),
                LessThanOperator => Expression.LessThan(left, right),
                LessThanEqualToOperator => Expression.LessThanOrEqual(left, right),
                _ => base.GetComparison(left, op, right),
            };
        }
    }
}
