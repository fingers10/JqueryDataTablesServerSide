using AspNetCoreWeb.Attributes;
using AspNetCoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCoreWeb.Infrastructure
{
    public class SortOptionsProcessor<T, TEntity>
    {
        private static IEnumerable<SortTerm> GetAllTerms(DTParameters table)
        {
            var dtColumns = table.Columns as IList<DTColumn> ?? table.Columns.ToList();

            if (dtColumns.All(x => !x.Orderable)) yield break;

            foreach (var term in table.Order)
            {
                var column = dtColumns[term.Column];
                if (!column.Orderable) continue;

                yield return new SortTerm
                {
                    Name = column.Data,
                    Descending = term.Dir.Equals(DTOrderDir.DESC)
                };
            }
        }

        private static IEnumerable<SortTerm> GetValidTerms(DTParameters table)
        {
            var queryTerms = GetAllTerms(table).ToArray();
            if (!queryTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in queryTerms)
            {
                var declaredTerm =
                    declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;

                yield return new SortTerm
                {
                    Name = declaredTerm.Name,
                    EntityName = declaredTerm.EntityName,
                    Descending = term.Descending,
                    Default = declaredTerm.Default
                };
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query, DTParameters table)
        {
            var terms = GetValidTerms(table).ToArray();
            if (!terms.Any()) return query;

            var modifiedQuery = query;
            var useThenBy = false;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(term.EntityName ?? term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ Expression backwards:
                // query = query.OrderBy(x => x.Property);

                // x => x.Property
                var key = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper.GetLambda(typeof(TEntity), propertyInfo.PropertyType, obj, key);

                // query.OrderBy/ThenBy[Descending](x => x.Property)
                modifiedQuery = ExpressionHelper.CallOrderByOrThenBy(
                    modifiedQuery, useThenBy, term.Descending, propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }

        private static IEnumerable<SortTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
                .Select(p =>
                {
                    var attribute = p.GetCustomAttribute<SortableAttribute>();
                    return new SortTerm
                    {
                        Name = p.Name,
                        EntityName = attribute.EntityProperty,
                        Default = attribute.Default
                    };
                });
    }
}
