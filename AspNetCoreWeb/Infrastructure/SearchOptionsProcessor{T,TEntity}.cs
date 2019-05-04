using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure
{
    public class SearchOptionsProcessor<T, TEntity>
    {
        private static IEnumerable<SearchTerm> GetAllTerms(IEnumerable<DTColumn> columns)
        {
            var dtColumns = columns as IList<DTColumn> ?? columns.ToList();

            if(dtColumns.All(x => string.IsNullOrWhiteSpace(x.Search.Value)))
            {
                yield break;
            }

            foreach(var column in dtColumns)
            {
                if(string.IsNullOrWhiteSpace(column.Search.Value))
                {
                    continue;
                }

                yield return new SearchTerm {
                    ValidSyntax = true,
                    Name = column.Data,
                    Operator = string.IsNullOrWhiteSpace(column.Name) ? "eq" : column.Name,
                    Value = column.Search.Value.ToLower()
                };
            }
        }

        private static IEnumerable<SearchTerm> GetValidTerms(IEnumerable<DTColumn> columns)
        {
            var queryTerms = GetAllTerms(columns)
                .Where(x => x.ValidSyntax)
                .ToArray();

            if(!queryTerms.Any())
            {
                yield break;
            }

            var declaredTerms = GetTermsFromModel();

            foreach(var term in queryTerms)
            {
                var declaredTerm =
                    declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name,StringComparison.OrdinalIgnoreCase));
                if(declaredTerm == null)
                {
                    continue;
                }

                yield return new SearchTerm {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    EntityName = declaredTerm.EntityName,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = declaredTerm.ExpressionProvider
                };
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query,IEnumerable<DTColumn> columns)
        {
            var terms = GetValidTerms(columns).ToArray();
            if(!terms.Any())
            {
                return query;
            }

            var modifiedQuery = query;

            foreach(var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(term.EntityName ?? term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ Expression backwards:
                // query = query.Where(x => x.Property == "Value");

                // x.Property
                var left = ExpressionHelper.GetPropertyExpression(obj,propertyInfo);
                // "Value"
                var right = term.ExpressionProvider.GetValue(term.Value);

                // x.Property == "Value"
                var comparisonExpression = term.ExpressionProvider.GetComparison(left,term.Operator,right);

                // x => x.Property == "Value"
                var lambdaExpression = ExpressionHelper.GetLambda<TEntity,bool>(obj,comparisonExpression);

                // query = query.Where...
                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery,lambdaExpression);
            }

            return modifiedQuery;
        }

        private static IEnumerable<SearchTerm> GetTermsFromModel()
        {
            return typeof(T).GetTypeInfo()
                                      .DeclaredProperties
                                      .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                                      .Select(p => {
                                          var attribute = p.GetCustomAttribute<SearchableAttribute>();
                                          return new SearchTerm {
                                              Name = p.Name,
                                              EntityName = attribute.EntityProperty,
                                              ExpressionProvider = attribute.ExpressionProvider
                                          };
                                      });
        }
    }
}
