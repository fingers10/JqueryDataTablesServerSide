using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb
{
    public class StringSearchExpressionProvider:DefaultSearchExpressionProvider
    {
        private const string StartsWithOperator = "sw";
        private const string ContainsOperator = "co";

        private static MethodInfo StartsWithMethod => typeof(string)
            .GetMethods()
            .First(x => x.Name == "StartsWith" && x.GetParameters().Length == 2);

        private static MethodInfo StringEqualsMethod => typeof(string)
            .GetMethods()
            .First(x => x.Name == "Equals" && x.GetParameters().Length == 2);

        private static MethodInfo ContainsMethod => typeof(string)
            .GetMethods()
            .First(x => x.Name == "Contains" && x.GetParameters().Length == 1);

        private static MethodInfo ToLowerMethod => typeof(string)
            .GetMethods()
            .First(x => x.Name == "ToLower");

        private static ConstantExpression IgnoreCase
            => Expression.Constant(StringComparison.OrdinalIgnoreCase);

        public override IEnumerable<string> GetOperators()
        {
            return base.GetOperators()
                           .Concat(
                               new[]
                               {
                        StartsWithOperator,
                        ContainsOperator
                               });
        }

        public override Expression GetComparison(MemberExpression left,string op,ConstantExpression right)
        {
            switch(op.ToLower())
            {
            case StartsWithOperator:
            return Expression.Call(left,StartsWithMethod,right,IgnoreCase);

            case ContainsOperator:
            return Expression.Call(Expression.Call(left,ToLowerMethod),ContainsMethod,right);

            case EqualsOperator:
            return Expression.Call(Expression.Call(left,ToLowerMethod),StringEqualsMethod,right,IgnoreCase);

            default:
            return base.GetComparison(left,op,right);
            }
        }
    }
}
