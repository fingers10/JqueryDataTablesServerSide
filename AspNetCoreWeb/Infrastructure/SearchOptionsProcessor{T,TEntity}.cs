using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure
{
    public class SearchOptionsProcessor<T, TEntity>
    {
        private static IEnumerable<SearchTerm> GetAllTerms(IEnumerable<DTColumn> columns)
        {
            var dtColumns = columns as IList<DTColumn> ?? columns.ToList();

            if (dtColumns.All(x => string.IsNullOrWhiteSpace(x.Search.Value)))
            {
                yield break;
            }

            foreach (var column in dtColumns)
            {
                if (string.IsNullOrWhiteSpace(column.Search.Value))
                {
                    continue;
                }

                var hasNavigation = column.Data.Contains('.');
                var parentIndex = column.Data.Split('.').Length - 2;

                yield return new SearchTerm
                {
                    ValidSyntax = true,
                    Name = column.Data,
                    Operator = string.IsNullOrWhiteSpace(column.Name) ? "eq" : column.Name,
                    Value = column.Search.Value.ToLower(),
                    HasNavigation = hasNavigation
                };
            }
        }

        private static IEnumerable<SearchTerm> GetValidTerms(IEnumerable<DTColumn> columns)
        {
            var queryTerms = GetAllTerms(columns)
                .Where(x => x.ValidSyntax)
                .ToArray();

            if (!queryTerms.Any())
            {
                yield break;
            }

            var declaredTerms = GetTermsFromModel(typeof(T));

            foreach (var term in queryTerms)
            {
                var declaredTerm =
                    declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null)
                {
                    continue;
                }

                yield return new SearchTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    EntityName = declaredTerm.EntityName,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = declaredTerm.ExpressionProvider,
                    HasNavigation = declaredTerm.HasNavigation
                };
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query, IEnumerable<DTColumn> columns)
        {
            var terms = GetValidTerms(columns).ToArray();
            if (!terms.Any())
            {
                return query;
            }

            var modifiedQuery = query;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo(typeof(TEntity), term.EntityName ?? term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ Expression backwards:
                // query = query.Where(x => x.Property == "Value");

                // x.Property
                var left = ExpressionHelper.GetMemberExpression(obj, term.EntityName ?? term.Name);
                // read this!!!!
                // http://askjonskeet.azurewebsites.net/answer/28476847/How-to-get-Expression-for-Nullable-values-(-fields-)-without-converting-from-ExpressionConvert-in-C
                // 

                // "Value"
                //var right = term.ExpressionProvider.GetValue(term.Value);
                var rightPreValue = term.ExpressionProvider.GetValue(term.Value);

                var right = rightPreValue.Type != left.Type
                    ? Expression.Convert(rightPreValue, left.Type)
                    : (Expression)rightPreValue;

                // x.Property == "Value"
                var comparisonExpression = term.ExpressionProvider.GetComparison(left, term.Operator, right);

                // x => x.Property == "Value"
                var lambdaExpression = ExpressionHelper.GetLambda<TEntity, bool>(obj, comparisonExpression);

                // query = query.Where...
                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);
            }

            return modifiedQuery;
        }

        private static IEnumerable<SearchTerm> GetTermsFromModel(
            Type parentSortClass,
            string parentsEntityName = null,
            string parentsName = null,
            bool hasNavigation = false)
        {
            var properties = parentSortClass.GetTypeInfo()
                       .DeclaredProperties
                       .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any());

            foreach (var p in properties)
            {
                var attribute = p.GetCustomAttribute<SearchableAttribute>();

                yield return new SearchTerm
                {
                    Name = hasNavigation ? $"{parentsName}.{p.Name}" : p.Name,
                    EntityName = hasNavigation ? $"{parentsEntityName ?? parentsName}.{attribute.EntityProperty ?? p.Name}" : attribute.EntityProperty,
                    ExpressionProvider = attribute.ExpressionProvider,
                    HasNavigation = hasNavigation
                };
            }

            var complexSearchProperties = parentSortClass.GetTypeInfo()
                       .DeclaredProperties
                       .Where(p => p.GetCustomAttributes<NestedSearchableAttribute>().Any());

            if (complexSearchProperties.Any())
            {
                foreach (var parentProperty in complexSearchProperties)
                {
                    var parentType = parentProperty.PropertyType;
                    var parentAttribute = parentProperty.GetCustomAttribute<NestedSearchableAttribute>();

                    var complexProperties = GetTermsFromModel(
                    parentType,
                    string.IsNullOrWhiteSpace(parentsEntityName) ? parentAttribute.ParentEntityProperty ?? parentProperty.Name : $"{parentsEntityName}.{parentAttribute.ParentEntityProperty ?? parentProperty.Name}",
                    string.IsNullOrWhiteSpace(parentsName) ? parentProperty.Name : $"{parentsName}.{parentProperty.Name}",
                    true);

                    foreach (var complexProperty in complexProperties)
                    {
                        yield return complexProperty;
                    }
                }
            }
        }
    }
}
