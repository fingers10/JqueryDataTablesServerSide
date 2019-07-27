using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure
{
    public class SortOptionsProcessor<T, TEntity>
    {
        private static IEnumerable<SortTerm> GetAllTerms(JqueryDataTablesParameters table)
        {
            var dtColumns = table.Columns as IList<DTColumn> ?? table.Columns.ToList();

            if(dtColumns.All(x => !x.Orderable))
            {
                yield break;
            }

            foreach(var term in table.Order)
            {
                var column = dtColumns[term.Column];
                if(!column.Orderable)
                {
                    continue;
                }

                var hasNavigation = column.Data.Contains('.');
                var parentIndex = column.Data.Split('.').Length - 2;

                yield return new SortTerm {
                    ParentName = hasNavigation ? column.Data.Split('.')[parentIndex] : typeof(T).Name,
                    Name = column.Data,
                    Descending = term.Dir.Equals(DTOrderDir.DESC),
                    HasNavigation = hasNavigation
                };
            }
        }

        private static IEnumerable<SortTerm> GetValidTerms(JqueryDataTablesParameters table)
        {
            var queryTerms = GetAllTerms(table).ToArray();
            if(!queryTerms.Any())
            {
                yield break;
            }

            var declaredTerms = GetTermsFromModel(typeof(T));

            foreach(var term in queryTerms)
            {
                var declaredTerm =
                    declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name,StringComparison.OrdinalIgnoreCase));
                if(declaredTerm == null)
                {
                    continue;
                }

                yield return new SortTerm {
                    ParentName = declaredTerm.ParentName,
                    Name = declaredTerm.Name,
                    ParentEntityName = declaredTerm.ParentEntityName,
                    EntityName = declaredTerm.EntityName,
                    Descending = term.Descending,
                    Default = declaredTerm.Default,
                    HasNavigation = declaredTerm.HasNavigation
                };
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query,JqueryDataTablesParameters table)
        {
            var terms = GetValidTerms(table).ToArray();
            if(!terms.Any())
            {
                return query;
            }

            var modifiedQuery = query;
            var useThenBy = false;

            foreach(var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo(typeof(TEntity),term.EntityName ?? term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ Expression backwards:
                // query = query.OrderBy(x => x.Property);
                // If it has navigation then:
                // query = query.OrderBy(x => x.ParentProperty.Property)

                // x => x.Property
                var key = ExpressionHelper.GetMemberExpression(obj, term.EntityName ?? term.Name);
                var keySelector = ExpressionHelper.GetLambda(typeof(TEntity),propertyInfo.PropertyType,obj,key);

                // query.OrderBy/ThenBy[Descending](x => x.Property)
                modifiedQuery = ExpressionHelper.CallOrderByOrThenBy(
                    modifiedQuery,useThenBy,term.Descending,propertyInfo.PropertyType,keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }

        private static IEnumerable<SortTerm> GetTermsFromModel(
            Type parentSortClass,
            string parentsName = null,
            bool hasNavigation = false)
        {
            var properties = parentSortClass.GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SortableAttribute>().Any());

            foreach (var p in properties)
            {
                var attribute = p.GetCustomAttribute<SortableAttribute>();

                yield return new SortTerm
                {
                    ParentName = parentSortClass.Name,
                    Name = hasNavigation ? $"{parentsName}.{p.Name}" : p.Name,
                    EntityName = attribute.EntityProperty,
                    Default = attribute.Default,
                    HasNavigation = hasNavigation
                };
            }

            var complexSortProperties = parentSortClass.GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<NestedSortableAttribute>().Any());

            if (complexSortProperties.Any())
            {
                foreach (var parentProperty in complexSortProperties)
                {
                    var parentType = parentProperty.PropertyType;

                    var complexProperties = GetTermsFromModel(parentType, string.IsNullOrWhiteSpace(parentsName) ? parentType.Name : $"{parentsName}.{parentType.Name}", true);

                    foreach (var complexProperty in complexProperties)
                    {
                        yield return complexProperty;
                    }
                }
            }
        }
    }
}
